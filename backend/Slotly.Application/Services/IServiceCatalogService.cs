using Slotly.Application.Services.Dtos;

namespace Slotly.Application.Services;

public interface IServiceCatalogService
{
    Task<IReadOnlyList<ServiceDto>> GetAllAsync(Guid tenantId, CancellationToken ct = default);
    Task<ServiceDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ServiceDto> CreateAsync(CreateServiceRequest request, CancellationToken ct = default);
}
