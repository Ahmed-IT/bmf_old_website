using System.ComponentModel.DataAnnotations;

namespace BMF.Shared.Entities.Security
{
    public enum SystemUserType
    {
        [Display(Name = "Not Signed In")]
        None = 0,

        [Display(Name = "Administrator")]
        Administrator = 10,
    }
}
