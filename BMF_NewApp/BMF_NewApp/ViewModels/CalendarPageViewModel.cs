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
    public class CalendarPageViewModel : ViewModelBase
    {
        private ObservableRangeCollection<ClsCalendar> _calendar;
        public ObservableRangeCollection<ClsCalendar> Calendar
        {
            get { return _calendar; }
            set { SetProperty(ref _calendar, value); }
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

        private DelegateCommand<ClsCalendar> _categoryTappedCommand;
        public DelegateCommand<ClsCalendar> CategoryTappedCommand =>
            _categoryTappedCommand ?? (_categoryTappedCommand = new DelegateCommand<ClsCalendar>(ExecuteCatrogyTappedCommand));

        private DelegateCommand _commandLogout;
        public DelegateCommand CommandLogout =>
            _commandLogout ?? (_commandLogout = new DelegateCommand(ExecuteLogout));


        public CalendarPageViewModel(INavigationService navigationService, IPageDialogService dialogService
            , ILocalService localService, IRestService restService)
            : base(navigationService, dialogService)
        {
            Title = "Calendar Page";

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

                var getCalendar = await _restService.GetCalendar();

                Calendar = new ObservableRangeCollection<ClsCalendar>(getCalendar);

                IsBusy = false;
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlertAsync("Error", "Error Loading Data", "OK");

                var properties = new Dictionary<string, string>
                {
                    { "CalendarPageViewModel", "OnNavigatedTo" }
                };

                Crashes.TrackError(ex, properties);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void ExecuteCatrogyTappedCommand(ClsCalendar obj)
        {
            try
            {
                // create and add some navigation paremters we will send an Event object
                var parameter = new NavigationParameters
            {
                { "events", obj }
            };

                await NavigationService.NavigateAsync("CalendarRoundsPage", parameter);
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlertAsync("Error", "Navigating to Calendar", "OK");

                var properties = new Dictionary<string, string>
                {
                    { "CalendarPageViewModel", "ExecuteCatrogyTappedCommand" }
                };

                Crashes.TrackError(ex, properties);
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
                    { "CalendarPageViewModel", "ExecuteLogout" }
                };

                Crashes.TrackError(ex, properties);
            }
        }
    }
}
