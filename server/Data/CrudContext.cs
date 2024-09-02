using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data
{
    public class CrudContext : DbContext
    {
        public CrudContext(DbContextOptions<CrudContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
