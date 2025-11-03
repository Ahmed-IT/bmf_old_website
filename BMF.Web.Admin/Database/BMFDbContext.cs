using Microsoft.EntityFrameworkCore;
using BMF.Shared.Models;
using System.Reflection;

namespace BMF.Web.Admin.Database
{
    public class BMFDbContext : DbContext
    {
        public BMFDbContext(DbContextOptions<BMFDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Apply configurations from shared models assembly (placeholder - requires explicit IEntityTypeConfiguration classes)
            // modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseEntity).Assembly);
        }

        // DbSets inferred from shared models will need explicit declarations if using conventions
        // Example:
        // public DbSet<M_User> Users { get; set; }
    }
}}