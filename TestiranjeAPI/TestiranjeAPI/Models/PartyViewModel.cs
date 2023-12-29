namespace TestiranjeAPI.Models;

public record class PartyViewModel(
    int Id,
    string Name,
    string City,
    string Address,
    string Image,
    string Creator,
    int CreatorId);
