using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksApiController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksApiController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_taskService.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var task = _taskService.GetById(id);

        if (task == null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    [HttpPost]
    public IActionResult Create(TaskItem task)
    {
        var createdTask = _taskService.Create(task);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdTask.Id },
            createdTask);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, TaskItem task)
    {
        var updated = _taskService.Update(id, task);

        if (!updated)
        {
            return NotFound();
        }

        return Ok(_taskService.GetById(id));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _taskService.Delete(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}