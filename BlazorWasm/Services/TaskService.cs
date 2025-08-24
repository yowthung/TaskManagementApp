using System.Net.Http.Json;
using BlazorWasm.Models;

public class TaskService
{
    private readonly HttpClient _http;

    public TaskService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<TaskDto>> GetAllTasksAsync()
    {
        return await _http.GetFromJsonAsync<List<TaskDto>>("/api/Tasks/GetAll")
               ?? new List<TaskDto>();
    }

    public async Task AddTaskAsync(TaskDto task)
    {
        await _http.PostAsJsonAsync("/api/Tasks/Create", task);
    }
}