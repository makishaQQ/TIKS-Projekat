using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestiranjeAPI.Models;

namespace TestiranjeAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PartyController : ControllerBase
{
    public required PartyContext Context { get; set; }
    public PartyController(PartyContext context)
    {
        Context = context;
    }

    [HttpPost("create/{userId}")]
    public async Task<ActionResult> CreateParty([FromBody]PartyCreate party, [FromRoute]int userId)
    {
        try
        {
            var user = await Context.Users
                .FindAsync(userId);

            Party newParty = new Party(party.Name, party.City, party.Address, party.Image);
            newParty.Creator = user;

            await Context.Parties
                .AddAsync(newParty);
            await Context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("attend/{partyId}/{userId}")]
    public async Task<ActionResult> AttendParty([FromRoute]int partyId, [FromRoute]int userId)
    {
        try
        {
            var user = await Context.Users
                .FindAsync(userId);
            var party = await Context.Parties
                .FindAsync(partyId);

            PartyAttendance partyAttendance = new PartyAttendance(user, party);

            await Context.PartyAttendances
                .AddAsync(partyAttendance);
            await Context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("myparties/{userId}")]
    public async Task<ActionResult> GetUserCreatedParties([FromRoute] int userId)
    {
        try
        {
            var parties = await Context.Parties
                .Include(p => p.Creator)
                .Where(p => p.Creator.Id == userId)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.City,
                    p.Address,
                })
                .ToListAsync();

            return Ok(parties);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("attendingparties/{userId}")]
    public async Task<ActionResult> GetUserAttendingParties([FromRoute] int userId)
    {
        try
        {
            var parties = await Context.PartyAttendances
                .Include(pa => pa.User)
                .Include(pa => pa.Party)
                .Where(pa => pa.User.Id == userId)
                .Select(pa => new
                {
                    pa.Party.Id,
                    pa.Party.Name,
                    pa.Party.City,
                    pa.Party.Address,
                    pa.Party.Image
                })
                .ToListAsync();

            return Ok(parties);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("unattend/{partyId}/{userId}")]
    public async Task<ActionResult> CancelUserAttendance([FromRoute]int partyId, [FromRoute]int userId)
    {
        try
        {
            var partyAttendence = await Context
                .PartyAttendances
                .Include(pa => pa.User)
                .Include(pa => pa.Party)
                .Where(pa => pa.Party.Id == partyId && pa.User.Id == userId)
                .FirstOrDefaultAsync();

            Context.PartyAttendances.Remove(partyAttendence);
            await Context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("cancelparty/{partyId}")]
    public async Task<ActionResult> CancelUserParty([FromRoute]int partyId)
    {
        try
        {
            var partyAttendees = await Context.PartyAttendances
                .Include(pa => pa.Party)
                .Where(pa => pa.Party.Id == partyId)
                .ToListAsync();

            var party = await Context.Parties
                .Where(p => p.Id == partyId)
                .FirstOrDefaultAsync();

            Context.PartyAttendances.RemoveRange(partyAttendees);
            Context.Parties.Remove(party);
            await Context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("editparty/{partyId}")]
    public async Task<ActionResult> EditParty([FromBody]PartyUpdate party, [FromRoute]int partyId)
    {
        try
        {
            var partyToEdit = await Context.Parties
                .FindAsync(partyId);

            partyToEdit.Name = party.Name;
            partyToEdit.City = party.City;
            partyToEdit.Address = party.Address;
            partyToEdit.Image = party.Image;

            await Context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
