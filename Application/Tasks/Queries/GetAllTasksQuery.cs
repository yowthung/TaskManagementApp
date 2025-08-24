using MediatR;
using Application.Tasks.DTOs;

namespace Application.Tasks.Queries;

public record GetAllTasksQuery() : IRequest<List<TaskDto>>;