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
    public class NewsPageViewModel : ViewModelBase
    {
        private ObservableRangeCollection<ClsNews> _news;
        public ObservableRangeCollection<ClsNews> News
        {
            get { return _news; }
            set { SetProperty(ref _news, value); }
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

        private DelegateCommand<ClsNews> _newsTappedCommand;
        public DelegateCommand<ClsNews> NewsTappedCommand =>
            _newsTappedCommand ?? (_newsTappedCommand = new DelegateCommand<ClsNews>(ExecuteNewsTappedCommand));


        public NewsPageViewModel(INavigationService navigationService, IPageDialogService dialogService
            , ILocalService localService, IRestService restService)
            : base(navigationService, dialogService)
        {
            Title = "News Page";

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

                var getCalendar = await _restService.GetNews();

                News = new ObservableRangeCollection<ClsNews>(getCalendar);

                IsBusy = false;
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlertAsync("Error", "Error Loading Data", "OK");

                var properties = new Dictionary<string, string>
                {
                    { "NewsPageViewModel", "OnNavigatedTo" }
                };

                Crashes.TrackError(ex, properties);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void ExecuteNewsTappedCommand(ClsNews obj)
        {
            try
            {
                // create and add some navigation paremters we will send an Event object
                var parameter = new NavigationParameters
            {
                { "news", obj }
            };

                await NavigationService.NavigateAsync("NewsDetailsPage", parameter);
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlertAsync("Error", "Navigating to News Detail", "OK");

                var properties = new Dictionary<string, string>
                {
                    { "NewsDetailPageViewModel", "ExecuteNewsTappedCommand" }
                };

                Crashes.TrackError(ex, properties);
            }
        }
    }
}
