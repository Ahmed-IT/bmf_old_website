using BMF.Shared.Models;
using BMF.Web.Admin.DBMappings.Website;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace BMF.Web.Admin.Database
{
    public class BMFDbContext : DbContext
    {
        public BMFDbContext() : base("BMFDbContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Dynamic Configuration
            modelBuilder.Configurations.AddFromAssembly(typeof(BaseEntity).Assembly);
        }
    }
}