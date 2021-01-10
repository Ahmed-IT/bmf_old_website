using BMF.Shared.Models;
using System.Data.Entity.ModelConfiguration;

namespace BMF.Web.Admin.DBMappings
{
    public abstract class EFMapping<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public void Map(EntityTypeConfiguration<TEntity> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public void MapDefaultProperties(EntityTypeConfiguration<TEntity> entityBuilder)
        {
            entityBuilder.HasKey(t => t.ID);
            entityBuilder.Property(t => t.CreatedOnUtc).HasColumnType("DATETIME2(7)");
            entityBuilder.Property(t => t.UpdatedOnUtc).HasColumnType("DATETIME2(7)");
            entityBuilder.Property(t => t.DeletedBy).IsRequired();
            entityBuilder.Property(t => t.DeletedOnUtc).IsRequired();
            entityBuilder.Property(t => t.DeletedOnUtc).HasColumnType("DATETIME2(7)");
            entityBuilder.Property(t => t.RowVersion).IsRowVersion();
        }

        public abstract void MapCustomProperties(EntityTypeConfiguration<TEntity> entityBuilder);
    }
}