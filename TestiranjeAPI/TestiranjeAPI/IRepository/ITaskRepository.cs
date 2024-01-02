using TestiranjeAPI.Models;
using TestiranjeAPI.Models.Response;
using Task = System.Threading.Tasks.Task;

namespace TestiranjeAPI.IRepository;

public interface ITaskRepository
{
    Task<List<UserTaskResponse>> GetUserTasksAsync(int userId);
    Task<Models.Task?> GetTaskById(int taskId);
    Task AddTaskAsync(Models.Task task);
    void RemoveTask(Models.Task task);
    Task SaveChangesAsync();
}
