using BMF_NewApp.Models;
using BMF_NewApp.Services;
using Microsoft.AppCenter.Crashes;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace BMF_NewApp.ViewModels
{

    public class ProfilePageViewModel : ViewModelBase
    {
        public ClsProfile _profile;
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

        public ProfilePageViewModel(INavigationService navigationService, IPageDialogService dialogService
            , ILocalService localService, IRestService restService)
            : base(navigationService, dialogService)
        {
            Title = "Profile Page";

            Profile = new ClsProfile();

            _localService = localService;
            _restService = restService;
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            try
            {
                if (App.CPR != null)
                {
                    IsBusy = true;

                    if(App.IsLoggedIn == true)
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

                    Profile = await _restService.GetProfile(App.CPR);

                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlertAsync("Error", "Error Loading Data", "OK");

                var properties = new Dictionary<string, string>
                {
                    { "ProfilePageViewModel", "OnNavigatedTo" }
                };

                Crashes.TrackError(ex, properties);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
