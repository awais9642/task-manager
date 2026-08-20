using TaskManager.Models;

namespace TaskManager.Services;

public class TaskService : ITaskService
{
    private readonly List<TaskItem> _tasks = new();

    private int _nextId = 1;

    public List<TaskItem> GetAll()
    {
        return _tasks;
    }

    public TaskItem? GetById(int id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }

    public TaskItem Create(TaskItem task)
    {
        task.Id = _nextId++;

        task.CreatedAt = DateTime.Now;

        _tasks.Add(task);

        return task;
    }

    public bool Update(int id, TaskItem task)
    {
        var existingTask = GetById(id);

        if (existingTask == null)
        {
            return false;
        }

        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.IsCompleted = task.IsCompleted;

        return true;
    }

    public bool Delete(int id)
    {
        var task = GetById(id);

        if (task == null)
        {
            return false;
        }

        _tasks.Remove(task);

        return true;
    }
}