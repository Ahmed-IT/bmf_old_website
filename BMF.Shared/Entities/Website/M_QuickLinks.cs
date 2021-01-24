using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Entities.Website
{
    public partial class M_QuickLinks : BaseEntity
    {
        #region Properties

        public string Title { get; set; }

        public string Website_Link { get; set; }

        #endregion
    }
}
