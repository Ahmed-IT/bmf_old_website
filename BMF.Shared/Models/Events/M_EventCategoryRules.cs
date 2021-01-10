using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Models.Events
{
    public partial class M_EventCategoryRules : BaseEntity
    {
        #region Foreign Keys

        public long ID_EventCategory { get; set; }

        #endregion

        #region Properties

        public string Title { get; set; }

        public string PDF_Url { get; set; }

        #endregion

        #region Relationships

        public M_EventCategory EventCategory { get; set; }

        #endregion
    }
}
