using Microsoft.EntityFrameworkCore;
using Trackio_API.Controllers.Entities;
namespace Trackio_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Application> Applications { get; set; }

    }
}
