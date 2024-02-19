using BMF_LicenseApp.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BMF_LicenseApp.Interfaces
{
    public interface ILicenseService
    {
        Task<List<Models.License>> GetLicensesListAsync(int userId);
        Task<List<Models.License>> GetLicensesByCPRAsync(string cpr);
        Task<List<Models.License>> GetExpiredLicensesListAsync(int userId);
        Task<List<LicenseType>> GetLicensesTypeAsync();
    }
}
