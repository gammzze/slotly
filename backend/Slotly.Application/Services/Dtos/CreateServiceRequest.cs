namespace Slotly.Application.Services.Dtos;

public record CreateServiceRequest(
    Guid TenantId,
    string Name,
    string? Description,
    int DurationMinutes,
    int BufferMinutes,
    decimal Price);
