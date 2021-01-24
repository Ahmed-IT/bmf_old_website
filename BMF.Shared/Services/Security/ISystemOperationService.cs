using System.Collections.Generic;
using BMF.Shared.Entities.Security;

namespace BMF.Shared.Services.Security
{
    public interface ISystemOperationService
    {
        public List<SystemOperation> GetAll_Local(bool nested = true);
        public List<SystemOperation> GetAll(List<long> SelectedItems = null);
        public List<SystemOperation> GetAll_Nested(bool bIncludeIAMRelated = false, List<long> SelectedItems = null);
    }
}
