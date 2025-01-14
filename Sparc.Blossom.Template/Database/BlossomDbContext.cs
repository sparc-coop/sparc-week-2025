using Microsoft.EntityFrameworkCore;
using Sparc.Blossom.Template.Entities;

namespace Sparc.Blossom.Template.Database
{
    public class BlossomDbContext : DbContext
    {
        public BlossomDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>();
        }
    }
}
