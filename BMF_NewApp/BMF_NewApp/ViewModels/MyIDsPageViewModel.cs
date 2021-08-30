using BMF_NewApp.Models;
using BMF_NewApp.Services;
using Microsoft.AppCenter.Crashes;
using MvvmHelpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BMF_NewApp.Models.BMFAppTables;

namespace BMF_NewApp.ViewModels
{
	public class MyIDsPageViewModel : ViewModelBase
    {
        private ObservableRangeCollection<ClsLicense> _myLicenseList;
        public ObservableRangeCollection<ClsLicense> MyLicenseList
        {
            get { return _myLicenseList; }
            set { SetProperty(ref _myLicenseList, value); }
        }

        public DelegateCommand<ClsLicense> ViewLicenseCommand { get; set; }

        private ClsProfile _profile;
        public ClsProfile Profile
        {
            get { return _profile; }
            set { SetProperty(ref _profile, value); }
        }

        private bool _isLoggedIn;
        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set { SetProperty(ref _isLoggedIn, value); }
        }

        private bool _showLogin;
        public bool ShowLogin
        {
            get { return _showLogin; }
            set { SetProperty(ref _showLogin, value); }
        }

        private bool _showLogout;
        public bool ShowLogout
        {
            get { return _showLogout; }
            set { SetProperty(ref _showLogout, value); }
        }

        //  Injected service
        private readonly IRestService _restService;
        private readonly ILocalService _localService;

        private DelegateCommand _commandLogout;
        public DelegateCommand CommandLogout =>
            _commandLogout ?? (_commandLogout = new DelegateCommand(ExecuteLogout));


        public MyIDsPageViewModel(INavigationService navigationService, IPageDialogService dialogService
            , ILocalService localService, IRestService restService)
            : base(navigationService, dialogService)
        {
            Title = "My ID";

            Profile = new ClsProfile();

            ViewLicenseCommand = new DelegateCommand<ClsLicense>(ViewLicense);

            _localService = localService;
            _restService = restService;
        }

        private async void ViewLicense(ClsLicense license)
        {
            try
            {
                if (license != null)
                {
                    var parameter = new NavigationParameters
                        {
                            { "license", license }
                        };

                    await NavigationService.NavigateAsync("ViewLicenseModalPage", parameter);
                }
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string>
                {
                    { "IncidentReportsViewModel", "View" }
                };

                Crashes.TrackError(ex, properties);
            }
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            try
            {
                IsBusy = true;

                if (App.IsLoggedIn == true)
                {
                    IsLoggedIn = true;
                    ShowLogout = true;
                    ShowLogin = false;
                }
                else
                {
                    IsLoggedIn = false;
                    ShowLogout = false;
                    ShowLogin = true;
                }

                if (App.ID != 0)                
                {

                    var getLicense = await _restService.GetMyLicense(App.ID);

                    MyLicenseList = new ObservableRangeCollection<ClsLicense>(getLicense);

                }

                if (App.profile != null)
                {
                    Profile = App.profile;
                }

                IsBusy = false;
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlertAsync("Error", "Error Loading Data", "OK");

                var properties = new Dictionary<string, string>
                {
                    { "MyIDsPageViewModel", "OnNavigatedTo" }
                };

                Crashes.TrackError(ex, properties);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void ExecuteLogout()
        {
            try
            {
                var logout = await App.Current.MainPage.DisplayAlert("Logout", "Are you sure you want to logout?", "OK", "Cancel");

                if (logout == true)
                {
                    var UserInfo = (from user in await App.Database.GetItemsAsync<TblUsersAccount>()
                                    where user.LoggedIn == true
                                    select user).SingleOrDefault();

                    if (UserInfo != null)
                    {
                        Xamarin.Forms.Application.Current.Properties["CPR"] = null;
                        App.CPR = null;
                        UserInfo.LoggedIn = false;

                        var UpdateUserInfo = await App.Database.SaveItemAsync(UserInfo);

                        if (UpdateUserInfo == 1)
                        {
                            await NavigationService.NavigateAsync("LoginPage");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlertAsync("Error", "Error In Logout.", "OK");

                var properties = new Dictionary<string, string>
                {
                    { "MyIDsPageViewModel", "ExecuteLogout" }
                };

                Crashes.TrackError(ex, properties);
            }
        }
    }

}
