using Microsoft.EntityFrameworkCore;
using MR.Web.Models;

namespace MR.Dal
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<UserEntity> Users => Set<UserEntity>();
    }
}