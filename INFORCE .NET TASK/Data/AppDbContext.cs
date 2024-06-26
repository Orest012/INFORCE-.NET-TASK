using INFORCE_.NET_TASK.Models;
using Microsoft.EntityFrameworkCore;


namespace INFORCE_.NET_TASK.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
            
        }
        public DbSet<User> dataUser { get; set; }
        public DbSet<URLModel> dataUrl { get; set; }

    }
}
