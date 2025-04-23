using CoreDotNetToken.Models;

namespace CoreDotNetToken.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
        Task CreateAsync(User user);
    }
}


