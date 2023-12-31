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
    public async Task<ActionResult> CreateParty([FromBody] PartyCreate party, [FromRoute] int userId)
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
    public async Task<ActionResult> AttendParty([FromRoute] int partyId, [FromRoute] int userId)
    {
        try
        {
            var user = await Context.Users
                .FindAsync(userId);
            var party = await Context.Parties
                .FindAsync(partyId);

            if (party == null) return Ok();

            PartyAttendance partyAttendance = new PartyAttendance(user!, party);

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
                .Where(p => p.Creator!.Id == userId)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.City,
                    p.Address,
                    p.Image
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
    public async Task<ActionResult> CancelUserAttendance([FromRoute] int partyId, [FromRoute] int userId)
    {
        try
        {
            await Context.PartyAttendances
                .Include(pa => pa.User)
                .Include(pa => pa.Party)
                .Where(pa => pa.Party.Id == partyId && pa.User.Id == userId)
                .ExecuteDeleteAsync();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("cancelparty/{partyId}")]
    public async Task<ActionResult> CancelUserParty([FromRoute] int partyId)
    {
        try
        {
            await Context.Parties
                .Where(p => p.Id == partyId)
                .ExecuteDeleteAsync();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("editparty/{partyId}")]
    public async Task<ActionResult> EditParty([FromBody] PartyUpdate party, [FromRoute] int partyId)
    {
        try
        {
            await Context.Parties
                .Where(p => p.Id == partyId)
                .ExecuteUpdateAsync(p => p.SetProperty(pn => pn.Name, party.Name)
                .SetProperty(pn => pn.City, party.City)
                .SetProperty(pn => pn.Address, party.Address)
                .SetProperty(pn => pn.Image, party.Image));

            await Context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("parties/{userId}")]
    public async Task<ActionResult> GetAllParties([FromRoute]int userId)
    {
        try
        {
            var parties = await Context.Parties
                .Include(p => p.Creator)
                .Select(p => new PartyViewModel(p.Id, p.Name, p.City, p.Address, p.Image, p.Creator!.Username, p.Creator.Id))
                .ToListAsync();

            return Ok(parties);
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
            var parties = await Context.Parties
                .Include(p => p.Creator)
                .Where(p => p.Creator!.Id == userId)
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToListAsync();

            return Ok(parties);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
