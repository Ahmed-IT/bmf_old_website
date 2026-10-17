namespace BMF.Shared.Services.Security
{
    public interface IPermissionService
    {
        bool Authorize(string operationSystemName);
    }
}
