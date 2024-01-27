using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TestiranjeAPI.IRepository;
using TestiranjeAPI.Models;
using Task = System.Threading.Tasks.Task;

namespace TestiranjeAPI.Repository;

public class UserRepository : IUserRepository
{
    private PartyContext _context;

    public UserRepository(PartyContext context)
    {
        _context = context;
    }
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        var user = await _context.Users
            .Where(u => u.Username == username)
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<User?> GetUserByUsernameAndPasswordAsync(string username, string password)
    {
        var user = await _context.Users
            .Where(u => u.Username == username && u.Password == password)
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        var user = await _context.Users
            .FindAsync(userId);

        return user;
    }
    public async Task<UserViewModel> GetUserInfoAsync(int userId)
    {
        var existingUser = await GetUserByIdAsync(userId);

        return new UserViewModel(
            existingUser!.Username,
            existingUser!.Email,
            existingUser!.Password,
            existingUser!.Avatar);
    }
    public async Task AddUserAsnyc(UserRegister user)
    {
        User userToAdd = new User(user.Username, user.Email, user.Password, user.Avatar);
        await _context.Users
            .AddAsync(userToAdd);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsnyc(User user, UserUpdate userUpdate)
    {
        user.Username = userUpdate.Username;
        user.Email = userUpdate.Email;
        user.Password = userUpdate.Password;
        user.Avatar = userUpdate.Avatar;

        await _context.SaveChangesAsync();
    }

}
