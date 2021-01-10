using BMF.Shared.Models;
using System.Data.Entity.ModelConfiguration;

namespace BMF.Web.Admin.DBMappings
{
    public abstract class EFMapping<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public void Map()
        {
            MapDefaultProperties();
            MapCustomProperties();
        }

        public void MapDefaultProperties()
        {
            HasKey(t => t.ID);
            Property(t => t.CreatedOnUtc).HasColumnType("DATETIME2");
            Property(t => t.UpdatedOnUtc).HasColumnType("DATETIME2");
            Property(t => t.DeletedBy).IsRequired();
            Property(t => t.DeletedOnUtc).IsRequired();
            Property(t => t.DeletedOnUtc).HasColumnType("DATETIME2");
            Property(t => t.RowVersion).IsRowVersion();
        }

        public abstract void MapCustomProperties();
    }
}