using TestiranjeAPI.Models;
using TestiranjeAPI.Models.Response;
using Task = System.Threading.Tasks.Task;

namespace TestiranjeAPI.IRepository;

public interface IPartyRepository
{
    Task<List<UserPartyResponse>> GetUserCreatedPartiesAsync(int userId);
    Task<List<PartyCardResponse>> GetAllPartiesAsync(int userId);
    Task<List<UserAttendingPartyResponse>> GetUserAttendingPartiesAsync(int userId);
    Task<List<PartyNameIdResponse>> GetUserCreatedPartiesNamesAsync(int userId);
    Task<Party?> GetPartyByIdAsync(int partyId);
    Task<PartyAttendance?> GetUserAttendanceAsync(int partyId, int userId);
    Task AddPartyAsync(Party party);
    Task AddPartyAttendanceAsync(PartyAttendance partyAttendance);
    void RemovePartyAttendance(PartyAttendance partyAttendance);
    void RemoveParty(Party party);
    Task SaveChangesAsync();
}
