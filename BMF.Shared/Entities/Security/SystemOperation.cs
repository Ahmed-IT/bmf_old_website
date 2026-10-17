using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Security
{
    public partial class SystemOperation : BaseEntity
    {
        #region Properties

        [StringLength(500), MaxLength(500), MinLength(1), Required]
        [JsonProperty(PropertyName = nameof(SystemName))]
        public string SystemName { get; set; }

        #endregion

        #region Foreign Keys

        [ForeignKey(nameof(Parent))]
        [JsonProperty(PropertyName = nameof(ID_Parent))]
        public long? ID_Parent { get; set; }

        #endregion

        #region Relationships

        public SystemOperation Parent { get; set; }

        [InverseProperty(nameof(Parent))]
        public ICollection<SystemOperation> Children { get; set; }

        [InverseProperty(nameof(AdminRoleOperation.Operation))]
        public ICollection<AdminRoleOperation> AssignedAdminRoles { get; set; }

        #endregion
    }
}
