using Domain.Entities;

namespace Domain.Repositories;

public interface ITaskRepository
{
    Task<TaskItem?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<List<TaskItem>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(TaskItem task, CancellationToken ct = default);
    Task UpdateAsync(TaskItem task, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}