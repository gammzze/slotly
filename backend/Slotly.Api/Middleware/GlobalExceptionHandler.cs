using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Slotly.Application.Exceptions;
using Slotly.Domain.Exceptions;

namespace Slotly.Api.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problem = exception switch
        {
            NotFoundException ex => Create(StatusCodes.Status404NotFound, "Kayıt bulunamadı", ex.Message),
            ConflictException ex => Create(StatusCodes.Status409Conflict, "Çakışma", ex.Message),
            InvalidStateTransitionException ex => Create(StatusCodes.Status409Conflict, "Geçersiz durum geçişi", ex.Message),
            ValidationException ex => CreateValidation(ex),
            _ => null
        };

        if (problem is null)
        {
            _logger.LogError(exception, "Beklenmeyen hata: {Path}", httpContext.Request.Path);
            problem = Create(
                StatusCodes.Status500InternalServerError,
                "Sunucu hatası",
                "Beklenmeyen bir hata oluştu. Lütfen daha sonra tekrar dene.");
        }
        else
        {
            _logger.LogWarning("İstek reddedildi ({Status}): {Message}", problem.Status, exception.Message);
        }

        problem.Instance = httpContext.Request.Path;
        httpContext.Response.StatusCode = problem.Status ?? StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(problem, problem.GetType(), cancellationToken);
        return true;
    }

    private static ProblemDetails Create(int status, string title, string detail) => new()
    {
        Status = status,
        Title = title,
        Detail = detail
    };

    private static ValidationProblemDetails CreateValidation(ValidationException ex) =>
        new(ex.Errors.ToDictionary(k => k.Key, v => v.Value))
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Doğrulama hatası",
            Detail = ex.Message
        };
}
