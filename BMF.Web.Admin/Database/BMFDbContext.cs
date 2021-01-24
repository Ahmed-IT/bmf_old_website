using BMF.Shared.Models;
using BMF.Shared.Models.AccessManagement;
using BMF.Shared.Models.Events;
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

        #region Access Management
        public DbSet<S_User> Table_SystemUser { get; set; }
        public DbSet<S_Role> Table_SystemRole { get; set; }
        public DbSet<S_Feature> Table_SystemFeature { get; set; }
        public DbSet<S_RoleUser> Table_SystemRoleUser { get; set; }
        public DbSet<S_RoleFeature> Table_SystemRoleFeature { get; set; }

        #endregion

        #region Events
        public DbSet<M_EventCategory> M_EventCategory { get; set; }
        public DbSet<M_EventCategoryRules> M_EventCategoryRules { get; set; }
        public DbSet<M_EventRound> M_EventRound { get; set; }
        public DbSet<M_EventRoundSession> M_EventRoundSession { get; set; }
        public DbSet<M_EventRoundSessionUserResults> M_EventRoundSessionUserResults { get; set; }
        public DbSet<M_EventSeason> M_EventSeason { get; set; } 
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Dynamic Configuration
            modelBuilder.Configurations.AddFromAssembly(typeof(BaseEntity).Assembly);
        }
    }
}