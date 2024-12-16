namespace TaskManager.Core.Entities;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public virtual ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
} 