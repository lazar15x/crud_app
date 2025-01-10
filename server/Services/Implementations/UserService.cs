using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Services.Interfaces;

namespace server.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly CrudContext _context;
        public UserService(CrudContext context) => _context = context;

        public async Task<List<User>> Get() 
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<User> Create(User newUser)
        {
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        public async Task UpdateById(int id, User user)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser is null) return;

            existingUser.FirstName = user.FirstName ?? existingUser.FirstName;
            existingUser.LastName = user.LastName ?? existingUser.LastName;
            existingUser.UpdatedAt = DateTime.UtcNow;
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user is not null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
