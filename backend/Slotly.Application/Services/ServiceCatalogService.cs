using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Slotly.Application.Common;
using Slotly.Application.Exceptions;
using Slotly.Application.Services.Dtos;
using Slotly.Domain.Entities;

namespace Slotly.Application.Services;

public class ServiceCatalogService : IServiceCatalogService
{
    private readonly IAppDbContext _db;
    private readonly IValidator<CreateServiceRequest> _createValidator;

    public ServiceCatalogService(IAppDbContext db, IValidator<CreateServiceRequest> createValidator)
    {
        _db = db;
        _createValidator = createValidator;
    }

    public async Task<IReadOnlyList<ServiceDto>> GetAllAsync(Guid tenantId, CancellationToken ct = default)
    {
        return await _db.Services
            .AsNoTracking()
            .Where(s => s.TenantId == tenantId && !s.IsDeleted)
            .OrderBy(s => s.Name)
            .Select(s => ToDto(s))
            .ToListAsync(ct);
    }

    public async Task<ServiceDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var service = await _db.Services
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted, ct)
            ?? throw new NotFoundException("Hizmet", id);

        return ToDto(service);
    }

    public async Task<ServiceDto> CreateAsync(CreateServiceRequest request, CancellationToken ct = default)
    {
        var validation = await _createValidator.ValidateAsync(request, ct);
        if (!validation.IsValid)
        {
            var errors = validation.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

            throw new Exceptions.ValidationException(errors);
        }

        var tenantExists = await _db.Tenants.AnyAsync(t => t.Id == request.TenantId && !t.IsDeleted, ct);
        if (!tenantExists)
        {
            throw new NotFoundException("İşletme", request.TenantId);
        }

        var service = new Service(request.TenantId, request.Name, request.DurationMinutes, request.Price)
        {
            Description = request.Description,
            BufferMinutes = request.BufferMinutes
        };

        _db.Services.Add(service);
        await _db.SaveChangesAsync(ct);

        return ToDto(service);
    }

    private static ServiceDto ToDto(Service s) =>
        new(s.Id, s.Name, s.Description, s.DurationMinutes, s.BufferMinutes, s.Price, s.IsActive);
}
