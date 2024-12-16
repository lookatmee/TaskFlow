namespace TaskManager.Core.Entities;

public class ApplicationUser
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public int RoleId { get; set; }
    
    public virtual Role Role { get; set; } = null!;
    public virtual ICollection<WorkItem> AssignedWorkItems { get; set; } = new List<WorkItem>();
} 