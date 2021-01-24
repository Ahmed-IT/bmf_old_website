using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Entities.Website
{
    public partial class M_GalleryCategory : BaseEntity
    {
        #region Properties

        public string Title { get; set; }

        public string Image_URL { get; set; }

        #endregion

        #region Relationships

        public ICollection<M_GalleryImages> GalleryImages { get; set; }

        #endregion
    }
}
