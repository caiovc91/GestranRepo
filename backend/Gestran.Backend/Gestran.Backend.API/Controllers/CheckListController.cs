using Gestran.Backend.Application.DTOs;
using Gestran.Backend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gestran.Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckListController : ControllerBase
    {
        ICheckListService _service;
        public CheckListController(ICheckListService service) { _service = service; }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        => Ok(await _service.GetAllCheckListsAsync(ct));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct = default)
        {
            var checklist = await _service.GetByIdAsync(id, ct);
            if (checklist == null) return NotFound();
            return Ok(checklist);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CheckListCreateDto dto, CancellationToken ct = default)
        {
            var created = await _service.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CheckListUpdateDto dto, CancellationToken ct = default)
        {
            var updated = await _service.UpdateAsync(id, dto, ct);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct = default)
        {
            var deleted = await _service.DeleteAsync(id, ct);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // Execution endpoints
        [HttpPost("{id:guid}/start/{executorId:guid}")]
        public async Task<IActionResult> StartExecution(Guid id, Guid executorId, CancellationToken ct = default)
        {
            var ok = await _service.StartExecutionAsync(id, executorId, ct);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpPut("{id:guid}/items/{executorId:guid}")]
        public async Task<IActionResult> UpdateItems(Guid id, Guid executorId, [FromBody] List<CheckListItemUpdateDto> items, CancellationToken ct = default)
        {
            var ok = await _service.UpdateItemsAsync(id, executorId, items, ct);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpPost("{id:guid}/finish/{executorId:guid}")]
        public async Task<IActionResult> FinishExecution(Guid id, Guid executorId, CancellationToken ct = default)
        {
            var ok = await _service.FinishExecutionAsync(id, executorId, ct);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpPost("{id:guid}/comment")]
        public async Task<IActionResult> AddComment(Guid id, [FromBody] string comment, CancellationToken ct = default)
        {
            var ok = await _service.AddCommentAsync(id, comment, ct);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
