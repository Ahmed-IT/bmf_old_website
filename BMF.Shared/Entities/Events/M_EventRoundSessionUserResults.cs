using BMF.Shared.Entities.UserManagement;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Events
{
    public partial class M_EventRoundSessionUserResults : BaseEntity
    {
        #region Foreign Keys

        [JsonProperty(PropertyName = nameof(ID_EventRoundSession))]
        public long ID_EventRoundSession { get; set; }

        [JsonProperty(PropertyName = nameof(ID_User))]
        public long ID_User { get; set; }

        #endregion

        #region Properties

        [JsonProperty(PropertyName = nameof(Points))]
        public int Points { get; set; }

        #endregion

        #region Relationships

        [InverseProperty(nameof(M_EventRoundSession.UserResults))]
        public M_EventRoundSession EventRoundSession { get; set; }
        
        [InverseProperty(nameof(M_User.UserResults))]
        public M_User User { get; set; }

        #endregion
    }
}
