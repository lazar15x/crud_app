using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;

namespace server.Services
{
    public class UserService
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
            var updateToUser = await _context.Users.FindAsync(id);
            
            if(updateToUser is not null)
            {
                updateToUser.FirstName = user.FirstName;
                updateToUser.LastName = user.LastName;
                updateToUser.UpdatedAt = DateTime.UtcNow;
                _context.Users.Update(updateToUser);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteById(int id)
        {
            var userToDelete = await _context.Users.FindAsync(id);

            if(userToDelete is not null)
            {
                 _context.Users.Remove(userToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
