using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace BMF.Shared.Entities
{
    public interface IDBViewModel<OriginalModel, DatabaseContext>
        where OriginalModel : BaseEntity
        where DatabaseContext : class
    {
        #region Database Methods

        public void SetDatabaseValues(OriginalModel Database);
        public void GetDatabaseValues(OriginalModel Database);

        //public OriginalModel AsDatabaseNew();
        public void AsDatabaseUpdate(OriginalModel Database);

        #endregion

        #region Methods

        //public void SetDefaults(OriginalModel Database);
        public void GetDefaults(OriginalModel Database);
        public void UpdateDefaults(OriginalModel Database);
        public void Validate(ModelStateDictionary ModelState, string propertyQualifier = "", DbSet<OriginalModel> databaseContext = null);

        #endregion
    }
}
