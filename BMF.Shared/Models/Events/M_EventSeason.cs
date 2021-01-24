using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Models.Events
{
    public partial class M_EventSeason : BaseEntity
    {
        #region Properties

        public string Title { get; set; }

        #endregion

        #region Relationships

        public ICollection<M_EventCategory> EventCategories { get; set; }

        #endregion
    }
}
