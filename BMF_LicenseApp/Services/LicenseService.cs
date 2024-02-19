using BMF_LicenseApp.Interfaces;
using BMF_LicenseApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BMF_LicenseApp.Services
{
    public class LicenseService : ILicenseService
    {
        private readonly HttpClient _httpClient;

        public LicenseService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<Models.License>> GetLicensesListAsync(int userId)
        {
            string url = $"https://bmftest.azurewebsites.net/api/GetLicensesList/{userId}";
            return await GetAsync<List<Models.License>>(url);
        }

        public async Task<List<Models.License>> GetLicensesByCPRAsync(string cpr)
        {
            string url = $"https://bmftest.azurewebsites.net/api/GetLicensesByCPR/{cpr}";
            return await GetAsync<List<Models.License>>(url);
        }

        public async Task<List<Models.License>> GetExpiredLicensesListAsync(int userId)
        {
            string url = $"https://bmftest.azurewebsites.net/api/GetExpiredLicensesList/{userId}";
            return await GetAsync<List<Models.License>>(url);
        }

        private async Task<T> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public async Task<List<LicenseType>> GetLicensesTypeAsync()
        {
            string url = $"https://bmftest.azurewebsites.net/api/GetLicensesType";
            return await GetAsync<List<LicenseType>>(url);
        }
    }
}
