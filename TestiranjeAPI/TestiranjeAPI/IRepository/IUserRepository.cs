using TestiranjeAPI.Models;
using Task = System.Threading.Tasks.Task;

namespace TestiranjeAPI.IRepository;

public interface IUserRepository
{
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByUsernameAndPasswordAsync(string username, string password);
    Task<User?> GetUserByIdAsync(int userId);
    Task<UserViewModel> GetUserInfoAsync(int userId);
    Task AddUserAsnyc(UserRegister user);
    Task UpdateUserAsnyc(User user, UserUpdate userUpdate);
}
