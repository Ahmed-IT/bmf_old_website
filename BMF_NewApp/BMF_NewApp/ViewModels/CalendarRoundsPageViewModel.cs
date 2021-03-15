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

namespace BMF_NewApp.ViewModels
{
	public class CalendarRoundsPageViewModel : ViewModelBase
    {
        private ClsCalendar _calendar;
        public ClsCalendar Calendar
        {
            get { return _calendar; }
            set { SetProperty(ref _calendar, value); }
        }

        private ObservableRangeCollection<ClsRound> _rounds;
        public ObservableRangeCollection<ClsRound> Rounds
        {
            get { return _rounds; }
            set { SetProperty(ref _rounds, value); }
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

        public CalendarRoundsPageViewModel(INavigationService navigationService, IPageDialogService dialogService
            , ILocalService localService, IRestService restService)
            : base(navigationService, dialogService)
        {
            Title = "Calendar Page";

            Calendar = new ClsCalendar();
            Profile = new ClsProfile();

            _localService = localService;
            _restService = restService;
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            try
            {
                if (parameters["events"] is ClsCalendar _Category)
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

                    Calendar = _Category;

                    if (Calendar.Rounds.Count > 0)
                    {
                        Rounds = new ObservableRangeCollection<ClsRound>(Calendar.Rounds.OrderBy(a => a.DateTime_Start));
                    }

                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlertAsync("Error", "Error Loading Data", "OK");

                var properties = new Dictionary<string, string>
                {
                    { "CalendarRoundsPageViewModel", "OnNavigatedTo" }
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
