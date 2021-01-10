using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Models.Website
{
    public partial class M_GalleryImages : BaseEntity
    {
        #region Foreign Keys

        public int ID_GalleryCategory { get; set; }

        #endregion

        #region Properties

        public string Image_URL { get; set; }

        #endregion

        #region Relationships

        public M_GalleryCategory GalleryCategory { get; set; }

        #endregion
    }
}
