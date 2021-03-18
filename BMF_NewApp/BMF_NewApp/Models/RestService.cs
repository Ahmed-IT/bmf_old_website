using BMF_NewApp.Models.ViewModel;
using BMF_NewApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BMF_NewApp.Models
{
    public class RestService : IRestService, IDisposable
    {
        //  Http client
        HttpClient client;

        public RestService()
        {

            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            // Initialize HTTP Client
            client = new HttpClient(handler);
            // set base URI
            client.BaseAddress = new Uri(Constants.RestUrl);
            client.DefaultRequestHeaders.Accept.Add(new
                    MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ClsAccount> AuthUser(ClsAccount user)
        {
            try
            {
                var content = JsonConvert.SerializeObject(user);
                var response = await client.PostAsync("AccountAuth", new StringContent(content, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ClsAccount>(json);

                    return result;
                }
            }
            catch (Exception ex)
            {

            }

            return default(ClsAccount);
        }

        public async Task<string> ChangePassword(ClsAccount user)
        {
            try
            {
                var content = JsonConvert.SerializeObject(user);
                var response = await client.PostAsync("ChangePassword", new StringContent(content, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<string>(json);

                    return result;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return "Not Found";
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception ex)
            {

            }

            return "Error";
        }

        public async Task<string> CreateMemebrAccount(ClsAccount UserAccount)
        {
            try
            {
                var content = JsonConvert.SerializeObject(UserAccount);
                var response = await client.PostAsync("CreateAccount", new StringContent(content, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<string>(json);

                    return "Account Created";
                }
                else if(response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    return "Exist";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return "Not Found";
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception ex)
            {

            }

            return "Error";
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ClsCalendar>> GetCalendar()
        {
            try
            {
                string ApiCall = $"GetCalendar";

                var response = await client.GetAsync(ApiCall).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<List<ClsCalendar>>(content);
                }
            }
            catch (Exception ex)
            {

            }

            return default(List<ClsCalendar>);
        }

        public async Task<DashboardVM> GetDashboard(string CPR)
        {
            try
            {
                string ApiCall = $"GetDashboard/" + CPR;

                var response = await client.GetAsync(ApiCall).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<DashboardVM>(content);
                }
            }
            catch (Exception ex)
            {

            }

            return default(DashboardVM);
        }

        public async Task<DashboardVM> GetDashboard()
        {
            try
            {
                string ApiCall = $"GetDashboard";

                var response = await client.GetAsync(ApiCall).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<DashboardVM>(content);
                }
            }
            catch (Exception ex)
            {

            }

            return default(DashboardVM);
        }

        public async Task<List<ClsLicense>> GetMyLicense(int Id)
        {
            try
            {
                string ApiCall = $"GetMyLicense/" + Id;

                var response = await client.GetAsync(ApiCall).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<List<ClsLicense>>(content);
                }
            }
            catch (Exception ex)
            {

            }

            return default(List<ClsLicense>);
        }

        public async Task<List<ClsNews>> GetNews()
        {
            try
            {
                string ApiCall = $"GetNews";

                var response = await client.GetAsync(ApiCall).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<List<ClsNews>>(content);
                }
            }
            catch (Exception ex)
            {

            }

            return default(List<ClsNews>);
        }

        public async Task<ClsProfile> GetProfile(string CPR)
        {
            try
            {
                string ApiCall = $"GetProfile/" + CPR;

                var response = await client.GetAsync(ApiCall).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<ClsProfile>(content);
                }
            }
            catch (Exception ex)
            {

            }

            return default(ClsProfile);
        }

        public async Task<string> SendAccountPassword(ClsAccount user)
        {
            try
            {
                var content = JsonConvert.SerializeObject(user);
                var response = await client.PostAsync("SendAccountPassword", new StringContent(content, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<string>(json);

                    return "Sent";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return "Not Found";
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception ex)
            {

            }

            return "Error";
        }
    }

}
