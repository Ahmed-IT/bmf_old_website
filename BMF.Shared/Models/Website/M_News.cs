using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Models.Website
{   
    public partial class M_News : BaseEntity
    {
        #region Properties

        public string Title { get; set; }

        public string Content { get; set; }

        public string Image_URL { get; set; }

        public DateTime NewsTimestapUtc { get; set; }
        
        #endregion
    }
}
