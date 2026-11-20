namespace Slotly.Application.Services.Dtos;

public record ServiceDto(
    Guid Id,
    string Name,
    string? Description,
    int DurationMinutes,
    int BufferMinutes,
    decimal Price,
    bool IsActive);
