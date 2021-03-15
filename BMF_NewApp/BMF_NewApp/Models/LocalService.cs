using BMF_NewApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BMF_NewApp.Models.BMFAppTables;

namespace BMF_NewApp.Models
{
    public class LocalService : ILocalService
    {
        public async Task<BMFAppTables.TblUsersAccount> GetUserAccount(string CPR)
        {
            try
            {
                var query = (from user in await App.Database.GetItemsAsync<TblUsersAccount>()
                             where user.CPR == CPR
                             select new TblUsersAccount
                             {
                                 CPR = user.CPR,
                                 Email = user.Email,
                                 Id = user.Id,
                                 LoggedIn = user.LoggedIn,
                                 IsAdmin = user.IsAdmin,
                             }).SingleOrDefault();

                if (query != null)
                {
                    return query;
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public async Task<bool> InsertUserAccount(BMFAppTables.TblUsersAccount item)
        {
            try
            {
                var query = (from n in await App.Database.GetItemsAsync<TblUsersAccount>()
                             where n.CPR == item.CPR
                             select n).SingleOrDefault();

                if (query == null)
                {
                    var insert = await App.Database.InsertItem(item);

                    if (insert == 1)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return false;
        }
    }

}
