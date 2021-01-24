using System.ComponentModel.DataAnnotations;

namespace BMF.Shared.ViewModels
{
    public class LogInViewModel
    {
        #region Properties

        [Display(Name = "Email/Username", Prompt = "Email/Username")]
        [MaxLength(50), MinLength(1), Required(ErrorMessage = "Please enter your email or username")]
        public string Username { get; set; }

        [Display(Name = "Password", Prompt = "Password")]
        [MaxLength(50), MinLength(1), Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password", Prompt = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        #endregion

        #region Ctor

        #endregion

        #region Functions

        #endregion
    }
}
