using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Entities.Website
{
    public partial class M_GalleryImages : BaseEntity
    {
        #region Foreign Keys

        public long ID_GalleryCategory { get; set; }

        #endregion

        #region Properties

        public string Image_URL { get; set; }

        #endregion

        #region Relationships

        public M_GalleryCategory GalleryCategory { get; set; }

        #endregion
    }
}
