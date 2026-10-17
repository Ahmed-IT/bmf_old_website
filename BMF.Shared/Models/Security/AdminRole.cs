using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Security
{
    public partial class AdminRole
    {
        #region Fields

        [NotMapped]
        public bool DB_UpdateUsers { get; set; }

        [NotMapped]
        public bool DB_UpdateOperations { get; set; }

        [NotMapped]
        [JsonIgnore]
        public List<long?> Cleared_List { get; set; }

        [NotMapped]
        [JsonProperty(PropertyName = nameof(Cleared))]
        public bool Cleared => Cleared_List != null && Cleared_List.Contains(ID);

        [NotMapped]
        [JsonIgnore]
        public List<long> Selected_List { get; set; }

        [NotMapped]
        [JsonProperty(PropertyName = nameof(Selected))]
        public bool Selected => Selected_List != null && Selected_List.Contains(ID);

        #endregion

        #region Ctor

        public AdminRole()
        {
            AssignedUsers = new List<AdminUserRole>();
            AssignedOperations = new List<AdminRoleOperation>();
        }

        #endregion
    }
}
