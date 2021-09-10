using BMF.Shared.Entities.Website;
using BMF.Shared.EntityMappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMF.Web.Admin.DBMappings.Website
{
    public partial class M_News_EFCoreBuilder : EFCoreMappingBuilder<M_News>
    {
        public M_News_EFCoreBuilder() { }

        public M_News_EFCoreBuilder(EntityTypeBuilder<M_News> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeBuilder<M_News> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Title).HasMaxLength(255).IsRequired(true);
            entityBuilder.Property(t => t.Content).HasMaxLength(int.MaxValue).IsRequired(true);
            entityBuilder.Property(t => t.Image_URL).HasMaxLength(1024).IsRequired(true);
            entityBuilder.Property(t => t.NewsTimestampUtc).HasColumnType("DATETIME2(7)").IsRequired(true);
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            // Indexes
        }
    }
}