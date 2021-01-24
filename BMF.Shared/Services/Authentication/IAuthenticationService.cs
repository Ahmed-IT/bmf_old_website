using BMF.Shared.Entities.Security;

namespace BMF.Shared.Services.Authentication
{
    /// <summary>
    /// Authentication service interface
    /// </summary>
    public partial interface IAuthenticationService 
    {
        void SignIn_AdminUser(AdminUser customer, bool isPersistent);
        void SignOut_AdminUser();
        AdminUser GetAuthenticated_AdminUser();
    }
}
