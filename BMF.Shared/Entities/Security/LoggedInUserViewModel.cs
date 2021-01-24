using TN.Shared.Entities.Customers;
using TN.Shared.Entities.ServiceProviders;

namespace BMF.Shared.Entities.Security
{
    public partial class LoggedInUserViewModel
    {
        #region Properties

        public string FullName { get; set; }

        #endregion

        #region Ctor

        public LoggedInUserViewModel(AdminUser adminUser)
        {
            FullName = adminUser.FullName;
        }

        public LoggedInUserViewModel(TNServiceProvider serviceProvider)
        {
            FullName = serviceProvider.FullName;
        }

        public LoggedInUserViewModel(Customer customerUser)
        {
            FullName = $"{customerUser.FirstName} {customerUser.LastName}";
        }

        #endregion
    }
}
