using BMF.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Shared.EntityMappings
{
    /// <summary>
    /// Represents base entity mapping and builder for SQL Server
    /// </summary>
    public abstract class EFCoreMappingBuilder<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Apply default entity configuration using the ApplyConfiguration call from DbContext on Model Building
        /// </summary>
        public void Configure(EntityTypeBuilder<TEntity> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        /// <summary>
        /// Apply default entity configuration
        /// </summary>
        /// <param name="entityBuilder">The entity type to configure the defaults</param>
        public void MapDefaultProperties(EntityTypeBuilder<TEntity> entityBuilder)
        {
            entityBuilder.HasKey(t => t.ID);
            entityBuilder.Property(t => t.CreatedOnUtc).HasColumnType("DATETIME2(7)");
            entityBuilder.Property(t => t.UpdatedOnUtc).HasColumnType("DATETIME2(7)");
            entityBuilder.Property(t => t.DeletedBy).IsRequired(false);
            entityBuilder.Property(t => t.DeletedOnUtc).IsRequired(false);
            entityBuilder.Property(t => t.DeletedOnUtc).HasColumnType("DATETIME2(7)");
            entityBuilder.Property(t => t.RowVersion).IsRowVersion();
        }

        /// <summary>
        /// Apply custom entity configuration
        /// </summary>
        /// <param name="entityBuilder">The entity type to configure the custom properties mappings (if any)</param>
        public abstract void MapCustomProperties(EntityTypeBuilder<TEntity> entityBuilder);
    }
}