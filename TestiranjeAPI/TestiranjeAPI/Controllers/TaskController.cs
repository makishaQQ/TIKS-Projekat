using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestiranjeAPI.Models;

namespace TestiranjeAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    public required PartyContext Context { get; set; }
    public TaskController(PartyContext context)
    {
        Context = context;
    }

    [HttpGet("mytasks/{userId}")]
    public async Task<ActionResult> GetUserTasks([FromRoute]int userId)
    {
        try
        {
            var tasks = await Context.Tasks
                .Include(t => t.User)
                .Include(t => t.Party)
                .Where(t => t.User.Id == userId)
                .GroupBy(t => new { t.Party.Id, t.Party.Name })
                .Select(group => new
                {
                    PartyId = group.Key.Id,
                    PartyName = group.Key.Name,
                    Tasks = group.Select(t => new
                    {
                        taskId = t.Id,
                        taskName = t.Name,
                        taskDescription = t.Description
                    })
                })
                .ToListAsync();

            return Ok(tasks);
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
            var user = await Context.Users
                .FindAsync(userId);
            var party = await Context.Parties
                .FindAsync(partyId);

            Models.Task newTask = new Models.Task(task.Name, task.Description);
            newTask.User = user;
            newTask.Party = party;

            await Context.Tasks.AddAsync(newTask);
            await Context.SaveChangesAsync();

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
            var taskToUpdate = await Context.Tasks
                .FindAsync(taskId);

            taskToUpdate.Name = task.Name;
            taskToUpdate.Description = task.Description;

            await Context.SaveChangesAsync();

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
            var task = await Context.Tasks
                .FindAsync(taskId);

            Context.Tasks.Remove(task);
            await Context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
