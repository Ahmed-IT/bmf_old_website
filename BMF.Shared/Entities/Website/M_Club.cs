using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Entities.Website
{
    public partial class M_Club : BaseEntity
    {
        #region Properties

        public string Title { get; set; }

        public string Description { get; set; }

        public string Website_Link { get; set; }

        public string Image_URL { get; set; }

        #endregion
    }
}
