using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Controllers;

public class TasksController : Controller
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    // GET: /Tasks
    public IActionResult Index()
    {
        var tasks = _taskService.GetAll();

        return View(tasks);
    }

    // GET: /Tasks/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Tasks/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(TaskItem task)
    {
        if (!ModelState.IsValid)
        {
            return View(task);
        }

        _taskService.Create(task);

        return RedirectToAction(nameof(Index));
    }

    // POST: /Tasks/Complete/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Complete(int id)
    {
        var task = _taskService.GetById(id);

        if (task == null)
        {
            return NotFound();
        }

        task.IsCompleted = !task.IsCompleted;

        return RedirectToAction(nameof(Index));
    }

    // POST: /Tasks/Delete/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var deleted = _taskService.Delete(id);

        if (!deleted)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }
}