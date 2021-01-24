using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Entities.Website
{   
    public partial class M_News : BaseEntity
    {
        #region Properties

        public string Title { get; set; }

        public string Content { get; set; }

        public string Image_URL { get; set; }

        public DateTime NewsTimestampUtc { get; set; }
        
        #endregion
    }
}
