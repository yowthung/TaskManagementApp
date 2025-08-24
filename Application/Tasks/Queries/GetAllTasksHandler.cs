namespace Application.Tasks.Queries;


using MediatR;
using Application.Tasks.DTOs;
using Domain.Repositories;
using Application.Tasks.Queries;

public record GetAllTasksQuery() : IRequest<List<TaskDto>>;
public class GetAllTasksHandler : IRequestHandler<GetAllTasksQuery, List<TaskDto>>
{
    private readonly ITaskRepository _repo;

    public GetAllTasksHandler(ITaskRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<TaskDto>> Handle(GetAllTasksQuery request, CancellationToken ct)
    {
        var tasks = await _repo.GetAllAsync(ct);

        return tasks.Select(t => new TaskDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            IsCompleted = t.IsCompleted,
            CreatedAt = t.CreatedAt
        }).ToList();
    }
}