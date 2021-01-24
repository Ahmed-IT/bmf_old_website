using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMF.Shared.Entities.Security
{
    public partial class AdminRoleOperation : BaseEntity
    {
        #region Properties

        #endregion

        #region Foreign Keys

        [ForeignKey(nameof(Role))]
        [Display(Name = "Role")]
        [JsonProperty(PropertyName = nameof(ID_Role))]
        public long ID_Role { get; set; }

        [ForeignKey(nameof(Operation))]
        [Display(Name = "Operation")]
        [JsonProperty(PropertyName = nameof(ID_Operation))]
        public long ID_Operation { get; set; }

        #endregion

        #region Relationships

        public AdminRole Role { get; set; }

        public SystemOperation Operation { get; set; }

        #endregion
    }
}
