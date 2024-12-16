using TaskManager.Core.Enums;

namespace TaskManager.Core.Entities;

public class WorkItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public WorkItemStatus Status { get; set; }
    public int? AssignedUserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public virtual ApplicationUser? AssignedUser { get; set; }
} 