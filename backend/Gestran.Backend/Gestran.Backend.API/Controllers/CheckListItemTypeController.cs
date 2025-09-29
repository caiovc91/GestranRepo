using Gestran.Backend.Application.DTOs;
using Gestran.Backend.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gestran.Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckListItemTypeController : ControllerBase
    {
        private readonly ICheckListItemTypeService _service;

        public CheckListItemTypeController(ICheckListItemTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
                => Ok(await _service.GetAllAsync(ct));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct = default)
        {
            var item = await _service.GetByIdAsync(id, ct);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CheckListItemTypeCreateDto dto, CancellationToken ct = default)
        {
            var created = await _service.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct = default)
        {
            var deleted = await _service.DeleteAsync(id, ct);
            if (!deleted) return NotFound();
            return NoContent();
        }

    }
}
