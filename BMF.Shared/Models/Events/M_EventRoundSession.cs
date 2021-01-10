using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Models.Events
{
    public partial class M_EventRoundSession : BaseEntity
    {
        #region Foreign Keys

        public int ID_EventRound { get; set; }

        #endregion

        #region Properties

        public string Title { get; set; }

        public string Results_PDF_URL { get; set; }

        #endregion

        #region Relationships

        public M_EventRound EventRound { get; set; }

        public List<M_EventRoundSession_UserResults> UserResults { get; set; }

        #endregion
    }
}
