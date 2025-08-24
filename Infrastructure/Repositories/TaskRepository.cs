using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TaskItem?> GetByIdAsync(int id, CancellationToken ct = default) =>
        await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id, ct);

    public async Task<List<TaskItem>> GetAllAsync(CancellationToken ct = default) =>
        await _context.Tasks.AsNoTracking().ToListAsync(ct);

    public async Task AddAsync(TaskItem task, CancellationToken ct = default) =>
        await _context.Tasks.AddAsync(task, ct);

    public async Task UpdateAsync(TaskItem task, CancellationToken ct = default) =>
        await _context.Tasks.Where(e => e.Id == task.Id) .ExecuteUpdateAsync(s => s.SetProperty(e => e.IsCompleted, e => task.IsCompleted));

    public async Task SaveChangesAsync(CancellationToken ct = default) =>
        await _context.SaveChangesAsync(ct);
}
