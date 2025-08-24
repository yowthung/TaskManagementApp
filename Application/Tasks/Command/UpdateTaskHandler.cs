using MediatR;
using Domain.Entities;
using Domain.Repositories;
 
namespace Application.Tasks.Command;

public record UpdateTaskCommand(int id, string Title, string Description, bool IsCompleted, DateTime CreatedAt) : IRequest<bool>;

public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, bool>
{
    private readonly ITaskRepository _repo;

    public UpdateTaskHandler(ITaskRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(UpdateTaskCommand cmd, CancellationToken ct)
    {
        var entity = new TaskItem {Id = cmd.id, Title = cmd.Title, Description = cmd.Description, IsCompleted = cmd.IsCompleted, CreatedAt = cmd.CreatedAt };

        await _repo.UpdateAsync(entity, ct);
        await _repo.SaveChangesAsync(ct);

        return true;
    }
}