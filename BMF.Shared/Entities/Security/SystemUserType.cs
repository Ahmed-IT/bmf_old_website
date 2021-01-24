using System.ComponentModel.DataAnnotations;

namespace BMF.Shared.Entities.Security
{
    public enum SystemUserType
    {
        [Display(Name = "Not Signed In")]
        None = 0,

        [Display(Name = "Administrator")]
        Administrator = 10,

        [Display(Name = "Tribe Member")]
        ServiceProvider = 20,

        [Display(Name = "Customer")]
        Customer = 30,
    }
}
