using BMF.Shared.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace BMF.Shared.Models.Events
{
    public partial class M_EventRoundSession : BaseEntity
    {
        #region Foreign Keys

        public long ID_EventRound { get; set; }

        #endregion

        #region Properties

        public string Title { get; set; }

        public string Results_PDF_URL { get; set; }

        #endregion

        #region Relationships

        public M_EventRound EventRound { get; set; }

        public List<M_EventRoundSessionUserResults> UserResults { get; set; }

        public List<M_MyRaces> User_MyRaces { get; set; }

        public List<M_MyFavRaces> User_MyFavRaces { get; set; }

        #endregion
    }
}
