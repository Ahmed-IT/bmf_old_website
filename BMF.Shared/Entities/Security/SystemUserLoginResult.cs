namespace BMF.Shared.Entities.Security
{
    public class SystemUserLoginResult
    {
        public SystemUserType systemUserType { get; set; }

        public SystemUserLoginType systemUserLoginType { get; set; }

        public SystemUserLoginResultStatus systemUserLoginResultStatus { get; set; }
    }
}
