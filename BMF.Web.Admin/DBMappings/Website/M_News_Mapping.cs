using BMF.Shared.Models.Website;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace BMF.Web.Admin.DBMappings.Website
{
    public partial class M_News_Mapping : EFMapping<M_News>
    {
        public M_News_Mapping()
        {
            Property(t => t.Title).HasMaxLength(255).IsRequired();
        }

        public M_News_Mapping(EntityTypeConfiguration<M_News> entityBuilder)
        {
            MapDefaultProperties(entityBuilder);
            MapCustomProperties(entityBuilder);
        }

        public override void MapCustomProperties(EntityTypeConfiguration<M_News> entityBuilder)
        {
            // Attributes
            entityBuilder.Property(t => t.Title).HasMaxLength(255).IsRequired();
            entityBuilder.Property(t => t.Content).HasMaxLength(3000).IsRequired();
            entityBuilder.Property(t => t.Image_URL).HasMaxLength(1024).IsRequired();
            entityBuilder.Property(t => t.NewsTimestampUtc).HasColumnType("DATETIME2(7)");
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            // Indexes
        }
    }
}