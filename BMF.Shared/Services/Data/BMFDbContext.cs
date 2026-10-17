using Microsoft.EntityFrameworkCore;
using BMF.Shared.Entities;

namespace BMF.Shared.Services.Data
{
    public class BMFDbContext : DbContext
    {
        public BMFDbContext(DbContextOptions<BMFDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Dynamic Configuration
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseEntity).Assembly);
        }
    }
}
