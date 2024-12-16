using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Entities;
using TaskManager.Core.Enums;
using TaskManager.Infrastructure.Data;

public class WorkItemRepository : IWorkItemRepository
{
    private readonly ApplicationDbContext _context;

    public WorkItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<WorkItem?> GetByIdAsync(int id)
    {
        return await _context.WorkItems
            .Include(w => w.AssignedUser)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<IEnumerable<WorkItem>> GetAllAsync()
    {
        return await _context.WorkItems
            .Include(w => w.AssignedUser)
            .ToListAsync();
    }

    public async Task<IEnumerable<WorkItem>> GetByStatusAsync(WorkItemStatus status)
    {
        return await _context.WorkItems
            .Include(w => w.AssignedUser)
            .Where(w => w.Status == status)
            .ToListAsync();
    }

    public async Task<WorkItem> AddAsync(WorkItem workItem)
    {
        await _context.WorkItems.AddAsync(workItem);
        await _context.SaveChangesAsync();
        return workItem;
    }

    public async Task UpdateAsync(WorkItem workItem)
    {
        _context.Entry(workItem).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var workItem = await _context.WorkItems.FindAsync(id);
        if (workItem != null)
        {
            _context.WorkItems.Remove(workItem);
            await _context.SaveChangesAsync();
        }
    }
} 