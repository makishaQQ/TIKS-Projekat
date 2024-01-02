using Microsoft.AspNetCore.Mvc;
using TestiranjeAPI.IRepository;
using TestiranjeAPI.Models;

namespace TestiranjeAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserRepository _userRepository;
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("signup")]
    public async Task<ActionResult> UserSignup([FromBody] UserRegister userRegister)
    {
        try
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(userRegister.Username);

            if (existingUser != null) return BadRequest();

            await _userRepository.AddUserAsnyc(userRegister);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult> UserLogin([FromBody]UserLogin userLogin)
    {
        try
        {
            var existingUser = await _userRepository
                .GetUserByUsernameAndPasswordAsync(userLogin.Username, userLogin.Password);

            if (existingUser == null) return BadRequest();

            return Ok(existingUser.Id);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPut("update/{userId}")]
    public async Task<ActionResult> UserUpdate([FromBody]UserUpdate userUpdate, [FromRoute]int userId)
    {
        try
        {
            var userToUpdate = await _userRepository.GetUserByIdAsync(userId);

            var existingUser = await _userRepository.GetUserByUsernameAsync(userUpdate.Username);

            if (existingUser != null && existingUser.Username != userToUpdate!.Username) return Conflict();

            await _userRepository.UpdateUserAsnyc(userToUpdate!, userUpdate);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("info/{id}")]
    public async Task<ActionResult> GetUserInfo([FromRoute] int id)
    {
        try
        {
            var user = await _userRepository.GetUserInfoAsync(id);

            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
