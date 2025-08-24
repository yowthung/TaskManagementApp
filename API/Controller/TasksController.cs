using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Tasks.Queries;
using Application.Tasks.Command;

[ApiController]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator) => _mediator = mediator;



    [HttpGet]
    [Route("api/[controller]/GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _mediator.Send(new GetAllTasksQuery());
        return Ok(tasks);
    }


    [HttpPost]
    [Route("api/[controller]/Create")]
    public async Task<IActionResult> Create([FromBody] AddTaskCommand cmd)
    {
        var id = await _mediator.Send(cmd);
        return CreatedAtAction(nameof(GetAll), new { id }, id);
    }
}