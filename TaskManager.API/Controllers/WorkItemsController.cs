using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs;
using TaskManager.Core.Enums;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WorkItemsController : ControllerBase
    {
        private readonly IWorkItemService _workItemService;

        public WorkItemsController(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WorkItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var workItems = await _workItemService.GetAllAsync();
            return Ok(workItems);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WorkItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var workItem = await _workItemService.GetByIdAsync(id);
                return Ok(workItem);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("status/{status}")]
        [ProducesResponseType(typeof(IEnumerable<WorkItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByStatus(WorkItemStatus status)
        {
            var workItems = await _workItemService.GetByStatusAsync(status);
            return Ok(workItems);
        }

        [HttpPost]
        [ProducesResponseType(typeof(WorkItemDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] WorkItemDto workItemDto)
        {
            var createdWorkItem = await _workItemService.CreateAsync(workItemDto);
            return CreatedAtAction(nameof(GetById), new { id = createdWorkItem.Id }, createdWorkItem);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] WorkItemDto workItemDto)
        {
            try
            {
                await _workItemService.UpdateAsync(id, workItemDto);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _workItemService.DeleteAsync(id);
            return NoContent();
        }
    }
} 