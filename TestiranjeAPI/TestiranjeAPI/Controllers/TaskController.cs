using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestiranjeAPI.IRepository;
using TestiranjeAPI.Models;

namespace TestiranjeAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private ITaskRepository _taskRepository;
    private IUserRepository _userRepository;
    private IPartyRepository _partyRepository;
    public TaskController(
        ITaskRepository taskRepository,
        IUserRepository userRepository,
        IPartyRepository partyRepository)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _partyRepository = partyRepository;
    }

    [HttpGet("my-tasks/{userId}")]
    public async Task<ActionResult> GetUserTasks([FromRoute]int userId)
    {
        try
        {
            var existingUser = await _userRepository.GetUserByIdAsync(userId);

            if (existingUser == null) return BadRequest();

            var userTasks = await _taskRepository.GetUserTasksAsync(userId);

            return Ok(userTasks);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("create/{userId}/{partyId}")]
    public async Task<ActionResult> CreateTask([FromBody]TaskCreate task, [FromRoute]int userId, [FromRoute]int partyId)
    {
        try
        {
            var existingUser = await _userRepository.GetUserByIdAsync(userId);
            var existingParty = await _partyRepository.GetPartyByIdAsync(partyId);

            if (existingParty == null || existingParty == null) return BadRequest();

            Models.Task newTask = new Models.Task(task.Name, task.Description);
            newTask.User = existingUser!;
            newTask.Party = existingParty!;

            await _taskRepository.AddTaskAsync(newTask);
            await _taskRepository.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("edit/{taskId}")]
    public async Task<ActionResult> EditTask([FromBody]TaskUpdate task, [FromRoute]int taskId)
    {
        try
        {
            var existingTask = await _taskRepository.GetTaskById(taskId);

            if (existingTask == null) return BadRequest();

            existingTask.Name = task.Name;
            existingTask.Description = task.Description;

            await _taskRepository.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("remove/{taskId}")]
    public async Task<ActionResult> RemoveTask([FromRoute]int taskId)
    {
        try
        {
            var taskToDelete = await _taskRepository.GetTaskById(taskId);

            if (taskToDelete == null) return BadRequest();


            _taskRepository.RemoveTask(taskToDelete);
            await _taskRepository.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
