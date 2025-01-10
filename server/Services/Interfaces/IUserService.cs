using server.Models;

namespace server.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> Get();
        Task<User?> GetById(int id);
        Task<User> Create(User user);
        Task UpdateById(int id, User user);
        Task DeleteById(int id);
    }
}