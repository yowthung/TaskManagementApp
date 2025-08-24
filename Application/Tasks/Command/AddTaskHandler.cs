using MediatR;
using Domain.Entities;
using Domain.Repositories;
 
namespace Application.Tasks.Command;

public record AddTaskCommand(string Title, string Description, bool IsCompleted, DateTime CreatedAt) : IRequest<int>;

public class AddTaskHandler : IRequestHandler<AddTaskCommand, int>
{
    private readonly ITaskRepository _repo;

    public AddTaskHandler(ITaskRepository repo)
    {
        _repo = repo;
    }

    public async Task<int> Handle(AddTaskCommand cmd, CancellationToken ct)
    {
        var entity = new TaskItem { Title = cmd.Title, Description = cmd.Description, IsCompleted = cmd.IsCompleted, CreatedAt = cmd.CreatedAt };

        await _repo.AddAsync(entity, ct);
        await _repo.SaveChangesAsync(ct);

        return entity.Id;
    }
}