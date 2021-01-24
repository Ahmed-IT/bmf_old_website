using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Models.Events
{
    public partial class M_EventCategory : BaseEntity
    {
        #region Foreign Keys

        public long ID_EventSeason { get; set; }

        #endregion

        #region Properties

        public string Title { get; set; }

        public string Image_URL { get; set; }

        #endregion

        #region Relationships

        public M_EventSeason EventSeason { get; set; }

        public ICollection<M_EventCategoryRules> EventCategoryRules { get; set; }

        public ICollection<M_EventRound> EventRounds { get; set; }

        #endregion
    }
}
