using BMF.Shared.Models.Website;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMF.Web.Admin.DBMappings.Website
{
    public partial class M_GalleryImages_Mapping : EFMapping<M_GalleryImages>
    {
        public M_GalleryImages_Mapping()
        {
            MapDefaultProperties();
            MapCustomProperties();
        }

        public override void MapCustomProperties()
        {
            // Attributes
            //Property(t => t.ID_GalleryCategory).IsRequired();
            Property(t => t.Image_URL).HasMaxLength(1024).IsRequired();
            // Ignored Attributes
            // Foreign Keys
            // Relationships
            HasRequired(g => g.GalleryCategory).WithMany(s => s.GalleryImages).HasForeignKey(s => s.ID_GalleryCategory);
            // Indexes
        }
    }
}