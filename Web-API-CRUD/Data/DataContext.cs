using Microsoft.EntityFrameworkCore;
using Web_API_CRUD.Models;

namespace Web_API_CRUD.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
