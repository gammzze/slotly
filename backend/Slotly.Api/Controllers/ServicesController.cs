using Microsoft.AspNetCore.Mvc;
using Slotly.Application.Services;
using Slotly.Application.Services.Dtos;

namespace Slotly.Api.Controllers;

[ApiController]
[Route("api/services")]
public class ServicesController : ControllerBase
{
    private readonly IServiceCatalogService _services;

    public ServicesController(IServiceCatalogService services)
    {
        _services = services;
    }

    // GEÇİCİ: Hafta 3-4'te tenantId JWT'den alınacak, query parametresi kalkacak.
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ServiceDto>>> GetAll(
        [FromQuery] Guid tenantId, CancellationToken ct)
    {
        return Ok(await _services.GetAllAsync(tenantId, ct));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ServiceDto>> GetById(Guid id, CancellationToken ct)
    {
        return Ok(await _services.GetByIdAsync(id, ct));
    }

    [HttpPost]
    public async Task<ActionResult<ServiceDto>> Create(
        [FromBody] CreateServiceRequest request, CancellationToken ct)
    {
        var created = await _services.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
}
