using Microsoft.EntityFrameworkCore;
using System.Linq;
using TestiranjeAPI.IRepository;
using TestiranjeAPI.Models;
using TestiranjeAPI.Models.Response;

namespace TestiranjeAPI.Repository;

public class TaskRepository : ITaskRepository
{
    private PartyContext _context;

    public TaskRepository(PartyContext context)
    {
        _context = context;
    }

    public async System.Threading.Tasks.Task AddTaskAsync(Models.Task task)
    {
        await _context.Tasks.AddAsync(task);
        return;
    }

    public async Task<Models.Task?> GetTaskById(int taskId)
    {
        var task = await _context.Tasks
            .FindAsync(taskId);

        return task;
    }

    public async Task<List<UserTaskResponse>> GetUserTasksAsync(int userId)
    {
        var tasks = await _context.Tasks
            .Include(t => t.User)
            .Include(t => t.Party)
            .Where(t => t.User.Id == userId)
            .GroupBy(t => new { t.Party.Id, t.Party.Name })
            .Select(group => new UserTaskResponse
            (group.Key.Id, group.Key.Name, group.Select(t => new TaskDataResponse(t.Id, t.Name, t.Description))))
            .ToListAsync();

        return tasks;
        
    }

    public void RemoveTask(Models.Task task)
    {
        _context.Tasks.Remove(task);
        return;
    }

    public async System.Threading.Tasks.Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
        return;
    }
}
