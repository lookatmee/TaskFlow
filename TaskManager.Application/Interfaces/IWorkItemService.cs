using TaskManager.Application.DTOs;
using TaskManager.Core.Enums;

public interface IWorkItemService
{
    Task<WorkItemDto> GetByIdAsync(int id);
    Task<IEnumerable<WorkItemDto>> GetAllAsync();
    Task<IEnumerable<WorkItemDto>> GetByStatusAsync(WorkItemStatus status);
    Task<WorkItemDto> CreateAsync(WorkItemDto workItem);
    Task UpdateAsync(int id, WorkItemDto workItem);
    Task DeleteAsync(int id);
} 