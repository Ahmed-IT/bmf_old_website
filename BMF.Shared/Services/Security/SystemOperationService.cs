using System.Collections.Generic;
using System.Linq;
using BMF.Shared.Entities.Security;
using BMF.Shared.Services.Data;

namespace BMF.Shared.Services.Security
{
    public class SystemOperationService : ISystemOperationService
    {
        private readonly IRepository<SystemOperation> _repository;
        private readonly ISystemOperationsProvider _systemOperationsProvider;

        #region Ctor

        public SystemOperationService(IRepository<SystemOperation> repository, ISystemOperationsProvider systemOperationsProvider)
        {
            _repository = repository;
            _systemOperationsProvider = systemOperationsProvider;
        }

        #endregion

        #region Methods

        public List<SystemOperation> GetAll_Local(bool nested = true)
        {
            return _systemOperationsProvider.GetAll_Local(nested);
        }

        public List<SystemOperation> GetAll(List<long> SelectedItems = null)
        {
            var Items = _repository.QTable.ToList()
                .Select(s =>
                {
                    s.Selected_List = SelectedItems;
                    return s;
                }).ToList();
            return Items;
        }

        public List<SystemOperation> GetAll_ByParent(long? ID_Parent, bool bIncludeIAMRelated = false, List<long> SelectedItems = null)
        {
            var Items = _repository.QTable.Where(w => w.ID_Parent == ID_Parent).ToList()
                .Select(s =>
                {
                    s.Children = GetAll_ByParent(s.ID, bIncludeIAMRelated: bIncludeIAMRelated, SelectedItems: SelectedItems);
                    s.Selected_List = SelectedItems;
                    return s;
                }).ToList();
            return Items;
        }

        public List<SystemOperation> GetAll_Nested(bool bIncludeIAMRelated = false, List<long> SelectedItems = null)
        {
            var Items = GetAll_ByParent(null, bIncludeIAMRelated: bIncludeIAMRelated, SelectedItems: SelectedItems);
            return Items;
        }

        #endregion
    }
}
