using CoreDotNetToken.Models;
// using CoreDotNetToken.Data;
using MongoDB.Driver;

namespace CoreDotNetToken.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDbContext _context;

        public UserRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.Find(u => u.Username == username).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(User user)
        {
            await _context.Users.InsertOneAsync(user);
        }
    }
}

