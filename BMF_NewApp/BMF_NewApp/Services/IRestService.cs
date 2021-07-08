using BMF_NewApp.Models;
using BMF_NewApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BMF_NewApp.Services
{
    public interface IRestService
    {
        Task<DashboardVM> GetDashboard(string CPR);
        Task<DashboardVM> GetDashboard();
        Task<ClsProfile> GetProfile(string CPR);
        Task<List<ClsLicense>> GetMyLicense(int Id);
        Task<List<ClsCalendar>> GetCalendar();
        Task<List<ClsNews>> GetNews();
        Task<ClsAccount> AuthUser(ClsAccount user);
        Task<string> CreateMemebrAccount(ClsAccount UserAccount);
        Task<string> SendAccountPassword(ClsAccount user);
        Task<string> ChangePassword(ClsAccount user);
        Task<List<ClsBulletinCategory>> GetBulletins();
        Task<List<ClsRulesRegsCategory>> GetRulesRegs();

    }
}
