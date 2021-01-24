using BMF.Shared.Entities.Security;

namespace BMF.Shared.Core
{
    /// <summary>
    /// Represents work context
    /// </summary>
    public interface IWorkContext
    {
        public bool CreateGuestAccount { get; set; }

        SystemUserType? CurrentSystemUserType { get; set; }

        LoggedInUserViewModel CurrentLoggedInUserViewModel { get; set; }

        AdminUser CurrentAdminUser { get; set; }
    }
}
