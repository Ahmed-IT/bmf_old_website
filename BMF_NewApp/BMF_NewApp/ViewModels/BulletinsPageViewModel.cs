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

namespace BMF_NewApp.ViewModels
{
    public class BulletinsPageViewModel : ViewModelBase
    {
        private ObservableRangeCollection<ClsBulletinCategory> _bulletins;
        public ObservableRangeCollection<ClsBulletinCategory> Bulletins
        {
            get { return _bulletins; }
            set { SetProperty(ref _bulletins, value); }
        }

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

        private DelegateCommand<ClsBulletinCategory> _categoryTappedCommand;
        public DelegateCommand<ClsBulletinCategory> CategoryTappedCommand =>
            _categoryTappedCommand ?? (_categoryTappedCommand = new DelegateCommand<ClsBulletinCategory>(ExecuteCatrogyTappedCommand));

        public BulletinsPageViewModel(INavigationService navigationService, IPageDialogService dialogService
            , ILocalService localService, IRestService restService)
            : base(navigationService, dialogService)
        {
            Title = "Bulletins";

            Profile = new ClsProfile();

            _localService = localService;
            _restService = restService;
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

                if (App.profile != null)
                {
                    Profile = App.profile;
                }

                var getBulletins = await _restService.GetBulletins();

                Bulletins = new ObservableRangeCollection<ClsBulletinCategory>(getBulletins);

                IsBusy = false;
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlertAsync("Error", "Error Loading Data", "OK");

                var properties = new Dictionary<string, string>
                {
                    { "BulletinsPageViewModel", "OnNavigatedTo" }
                };

                Crashes.TrackError(ex, properties);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void ExecuteCatrogyTappedCommand(ClsBulletinCategory obj)
        {
            try
            {
                // create and add some navigation paremters we will send an Event object
                var parameter = new NavigationParameters
            {
                { "bulletins", obj }
            };

                await NavigationService.NavigateAsync("BulletinsListPage", parameter);
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlertAsync("Error", "Navigating to Bulletins", "OK");

                var properties = new Dictionary<string, string>
                {
                    { "BulletinsPageViewModel", "ExecuteCatrogyTappedCommand" }
                };

                Crashes.TrackError(ex, properties);
            }

        }
    }
}
