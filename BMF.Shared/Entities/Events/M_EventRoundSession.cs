using BMF.Shared.Entities.UserManagement;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Events
{
    public partial class M_EventRoundSession : BaseEntity
    {
        #region Foreign Keys

        [JsonProperty(PropertyName = nameof(ID_EventRound))]
        public long ID_EventRound { get; set; }

        #endregion

        #region Properties

        [JsonProperty(PropertyName = nameof(Title))]
        public string Title { get; set; }

        [JsonProperty(PropertyName = nameof(Results_PDF_URL))]
        public string Results_PDF_URL { get; set; }

        #endregion

        #region Relationships

        [InverseProperty(nameof(M_EventRound.EventRoundSessions))]
        public M_EventRound EventRound { get; set; }

        [InverseProperty(nameof(M_EventRoundSessionUserResults.EventRoundSession))]
        public ICollection<M_EventRoundSessionUserResults> UserResults { get; set; }

        [InverseProperty(nameof(M_MyRaces.EventRoundSession))]
        public ICollection<M_MyRaces> User_MyRaces { get; set; }

        [InverseProperty(nameof(M_MyFavRaces.EventRoundSession))]
        public ICollection<M_MyFavRaces> User_MyFavRaces { get; set; }

        #endregion
    }
}
