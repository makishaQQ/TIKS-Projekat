using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestiranjeAPI.IRepository;
using TestiranjeAPI.Models;
using TestiranjeAPI.Models.Request;

namespace TestiranjeAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PartyController : ControllerBase
{
    private IUserRepository _userRepository;
    private IPartyRepository _partyRepository;
    public PartyController(
        IUserRepository userRepository,
        IPartyRepository partyRepository)
    {
        _userRepository = userRepository;
        _partyRepository = partyRepository;
    }

    #region GET_REQUESTS
    [HttpGet("my-parties/{userId}")]
    public async Task<ActionResult> GetUserCreatedParties([FromRoute] int userId)
    {
        try
        {
            var existingUser = await _userRepository.GetUserByIdAsync(userId);

            if (existingUser == null) return BadRequest();

            var userParties = await _partyRepository.GetUserCreatedPartiesAsync(userId);

            return Ok(userParties);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("available-parties/{userId}")]
    public async Task<ActionResult> GetAllParties([FromRoute] int userId)
    {
        try
        {
            var availableParties = await _partyRepository.GetAllPartiesAsync(userId);

            return Ok(availableParties);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("my-attending-parties/{userId}")]
    public async Task<ActionResult> GetUserAttendingParties([FromRoute] int userId)
    {
        try
        {
            var userAttendingParties = await _partyRepository.GetUserAttendingPartiesAsync(userId);

            return Ok(userAttendingParties);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("parties-names/{userId}")]
    public async Task<ActionResult> GetUserCreatedPartiesNames([FromRoute] int userId)
    {
        try
        {
            var parties = await _partyRepository.GetUserCreatedPartiesNamesAsync(userId);

            return Ok(parties);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    #endregion

    #region POST_REQUESTS
    [HttpPost("create/{userId}")]
    public async Task<ActionResult> CreateParty([FromBody] PartyCreateRequest party, [FromRoute] int userId)
    {
        try
        {
            var existingUser = await _userRepository.GetUserByIdAsync(userId);

            if (existingUser == null) return BadRequest();

            Party newParty = new Party(party.Name, party.City, party.Address, party.Image);
            newParty.Creator = existingUser;

            await _partyRepository.AddPartyAsync(newParty);
            await _partyRepository.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("attend/{partyId}/{userId}")]
    public async Task<ActionResult> AttendParty([FromRoute] int partyId, [FromRoute] int userId)
    {
        try
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            var party = await _partyRepository.GetPartyByIdAsync(partyId);

            if (party == null || user == null) return BadRequest();

            PartyAttendance partyAttendance = new PartyAttendance(user, party);

            await _partyRepository.AddPartyAttendanceAsync(partyAttendance);
            await _partyRepository.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    #endregion

    #region DELETE_REQUESTS

    [HttpDelete("unattend/{partyId}/{userId}")]
    public async Task<ActionResult> CancelUserAttendance([FromRoute] int partyId, [FromRoute] int userId)
    {
        try
        {

            var partyAttendanceToDelete = await _partyRepository.GetUserAttendanceAsync(partyId, userId);

            if (partyAttendanceToDelete == null) return BadRequest();

            _partyRepository.RemovePartyAttendance(partyAttendanceToDelete);
            await _partyRepository.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("cancel/{partyId}")]
    public async Task<ActionResult> CancelUserParty([FromRoute] int partyId)
    {
        try
        {
            var partyToDelete = await _partyRepository.GetPartyByIdAsync(partyId);

            if (partyToDelete == null) return BadRequest();

            _partyRepository.RemoveParty(partyToDelete);
            await _partyRepository.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    #endregion

    #region PUT_REQUESTS

    [HttpPut("edit/{partyId}")]
    public async Task<ActionResult> EditParty([FromBody] PartyUpdate partyUpdate, [FromRoute] int partyId)
    {
        try
        {
            var partyToEdit = await _partyRepository.GetPartyByIdAsync(partyId);

            if (partyToEdit == null) return BadRequest();

            partyToEdit.Name = partyUpdate.Name;
            partyToEdit.City = partyUpdate.City;
            partyToEdit.Address = partyUpdate.Address;
            partyToEdit.Image = partyUpdate.Image;

            await _partyRepository.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    #endregion
}
