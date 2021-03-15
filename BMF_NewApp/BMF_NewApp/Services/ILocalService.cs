using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static BMF_NewApp.Models.BMFAppTables;

namespace BMF_NewApp.Services
{
    public interface ILocalService
    {
        Task<TblUsersAccount> GetUserAccount(string CPR);
        Task<bool> InsertUserAccount(TblUsersAccount item);
    }
}
