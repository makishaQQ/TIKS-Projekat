using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestiranjeAPI.Models;

namespace TestiranjeAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public required PartyContext Context { get; set; }
    public UserController(PartyContext context)
    {
        Context = context;
    }

    [HttpPost("signup")]
    public async Task<ActionResult> UserSignup([FromBody] UserRegister user)
    {
        try
        {
            var existingUser = await Context.Users
                .Where(u => u.Username == user.Username)
                .FirstOrDefaultAsync();

            if (existingUser != null) return BadRequest();

            User userRegister = new User(user.Username, user.Email, user.Password, user.Avatar);

            await Context.Users
                .AddAsync(userRegister);
            await Context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult> UserLogin([FromBody]UserLogin user)
    {
        try
        {
            var userLogin = await Context.Users
                .Where(u => u.Username == user.Username && u.Password == user.Password)
                .FirstOrDefaultAsync();

            if (userLogin == null) return BadRequest();

            return Ok(userLogin.Id);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult> UserUpdate([FromBody]UserUpdate user, [FromRoute]int id)
    {
        try
        {
            var existingUser = await Context.Users
                .FindAsync(id);

            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.Avatar = user.Avatar;

            await Context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
