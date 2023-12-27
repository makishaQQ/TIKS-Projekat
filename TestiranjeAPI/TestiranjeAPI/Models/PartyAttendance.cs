namespace TestiranjeAPI.Models;

public class PartyAttendance
{
    public int Id { get; set; }
    public User User { get; set; }
    public Party Party { get; set; }

    public PartyAttendance() { }

    public PartyAttendance(User user, Party party)
    {
        User = user;
        Party = party;
    }
}
