using TaskManager.Core.Entities;
using TaskManager.Core.Enums;

public interface IWorkItemRepository
{
    Task<WorkItem?> GetByIdAsync(int id);
    Task<IEnumerable<WorkItem>> GetAllAsync();
    Task<IEnumerable<WorkItem>> GetByStatusAsync(WorkItemStatus status);
    Task<WorkItem> AddAsync(WorkItem workItem);
    Task UpdateAsync(WorkItem workItem);
    Task DeleteAsync(int id);
} 