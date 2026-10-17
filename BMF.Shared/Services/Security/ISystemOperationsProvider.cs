using System.Collections.Generic;
using BMF.Shared.Entities.Security;

namespace BMF.Shared.Services.Security
{
    public interface ISystemOperationsProvider
    {
        public List<SystemOperation> GetAll_Local(bool nested = true);
    }
}
