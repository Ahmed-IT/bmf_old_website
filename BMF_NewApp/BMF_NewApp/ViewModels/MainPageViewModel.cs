using BMF_NewApp.Models;
using BMF_NewApp.Models.ViewModel;
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
using System.Timers;
using Xamarin.Forms;
using static BMF_NewApp.Models.BMFAppTables;

namespace BMF_NewApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public DashboardVM _dashboard;
        public DashboardVM Dashboard
        {
            get { return _dashboard; }
            set { SetProperty(ref _dashboard, value); }
        }

        public ClsCountdown _countdown;
        public ClsCountdown Countdown
        {
            get { return _countdown; }
            set { SetProperty(ref _countdown, value); }
        }

        private ObservableRangeCollection<ClsRound> _comingEventsList;
        public ObservableRangeCollection<ClsRound> ComingEventsList
        {
            get { return _comingEventsList; }
            set { SetProperty(ref _comingEventsList, value); }
        }

        private ObservableRangeCollection<ClsNews> _newsList;
        public ObservableRangeCollection<ClsNews> NewsList
        {
            get { return _newsList; }
            set { SetProperty(ref _newsList, value); }
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

        private bool _noComingEvent;
        public bool NoComingEvent
        {
            get { return _noComingEvent; }
            set { SetProperty(ref _noComingEvent, value); }
        }

        private bool _isComingEvent;
        public bool IsComingEvent
        {
            get { return _isComingEvent; }
            set { SetProperty(ref _isComingEvent, value); }
        }

        private DelegateCommand _commandLogout;
        public DelegateCommand CommandLogout =>
            _commandLogout ?? (_commandLogout = new DelegateCommand(ExecuteLogout));

        private DelegateCommand _commandLogin;
        public DelegateCommand CommandLogin =>
            _commandLogin ?? (_commandLogin = new DelegateCommand(ExecuteLogin));

        private async void ExecuteLogin()
        {
            if(IsLoggedIn == false)
            {
                await NavigationService.NavigateAsync("LoginPage");
            }
        }


        //  Injected service
        private readonly IRestService _restService;
        private readonly ILocalService _localService;

        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService
            , ILocalService localService, IRestService restService)
            : base(navigationService, dialogService)
        {
            Title = "Main Page";

            Dashboard = new DashboardVM();
            Countdown = new ClsCountdown();           

            _localService = localService;
            _restService = restService;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (Countdown != null)
                {
                    DateTime myDate = Countdown.DateTime_Start;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (DateTime.Now.ToString("dd-MMM-yyyy") == Countdown.DateTime_Start.ToString("dd-MMM-yyyy"))
                        {
                            Countdown.Sec = "0";
                            Countdown.Min = "0";
                            Countdown.Hour = "0";
                            Countdown.Day = "0";
                        }
                        else
                        {
                            TimeSpan ts = myDate.Subtract(DateTime.Now);
                            Countdown.Sec = ts.Seconds.ToString();
                            Countdown.Min = ts.Minutes.ToString();
                            Countdown.Hour = ts.Hours.ToString();
                            Countdown.Day = ts.Days.ToString();
                        }
                    });
                }
            }
            catch (Exception ex)
            {

            }
        }


        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            try
            {
                var AddUserInfo = (from user in App.Database.GetItemsAsync<TblUsersAccount>().Result
                                   where user.LoggedIn == true
                                   select user).FirstOrDefault();

                if (AddUserInfo == null)
                {
                    IsLoggedIn = false;
                    ShowLogin = true;
                    ShowLogout = false;

                    App.ID = 0;
                    App.CPR = null;
                    App.IsLoggedIn = false;
                }
                else
                {
                    IsLoggedIn = true;
                    ShowLogout = true;
                    ShowLogin = false;
                    App.CPR = AddUserInfo.CPR;
                    App.ID = AddUserInfo.Id;
                    App.IsLoggedIn = true;
                }

                if (App.CPR != null)
                {
                    IsBusy = true;

                    Dashboard = await _restService.GetDashboard(App.CPR);

                    if (Dashboard.Countdown != null)
                    {
                        IsComingEvent = true;

                        if(Dashboard.Profile != null)
                        {
                            //App.ID = AddUserInfo.Id;
                            //App.CPR = Dashboard.Profile.CPR;

                            App.profile = Dashboard.Profile;
                        }

                        Countdown = new ClsCountdown
                        {
                            DateTime_End = Dashboard.Countdown.DateTime_End,
                            DateTime_Start = Dashboard.Countdown.DateTime_Start,
                            Round_Number = Dashboard.Countdown.Round_Number,
                            ID = Dashboard.Countdown.ID,
                            EventCategory = new ClsEventCategory
                            {
                                ID = Dashboard.Countdown.EventCategory.ID,
                                Image_URL = Dashboard.Countdown.EventCategory.Title,
                                Title = Dashboard.Countdown.EventCategory.Title
                            }
                        };
                    }
                    else
                    {
                        IsComingEvent = false;
                    }

                    ComingEventsList = new ObservableRangeCollection<ClsRound>(Dashboard.UpComingEvents);
                    NewsList = new ObservableRangeCollection<ClsNews>(Dashboard.News);

                    Timer timer = new Timer();
                    timer.Interval = 1000;
                    timer.Elapsed += Timer_Elapsed;
                    timer.Start();

                    IsBusy = false;
                }
                else
                {
                    IsBusy = true;

                    Dashboard = await _restService.GetDashboard();

                    if (Dashboard.Countdown != null)
                    {
                        IsComingEvent = true;

                        Countdown = new ClsCountdown
                        {
                            DateTime_End = Dashboard.Countdown.DateTime_End,
                            DateTime_Start = Dashboard.Countdown.DateTime_Start,
                            Round_Number = Dashboard.Countdown.Round_Number,
                            ID = Dashboard.Countdown.ID,
                            EventCategory = new ClsEventCategory
                            {
                                ID = Dashboard.Countdown.EventCategory.ID,
                                Image_URL = Dashboard.Countdown.EventCategory.Title,
                                Title = Dashboard.Countdown.EventCategory.Title
                            }
                        };
                    }
                    else
                    {
                        IsComingEvent = false;
                    }

                    ComingEventsList = new ObservableRangeCollection<ClsRound>(Dashboard.UpComingEvents);
                    NewsList = new ObservableRangeCollection<ClsNews>(Dashboard.News);

                    Timer timer = new Timer();
                    timer.Interval = 1000;
                    timer.Elapsed += Timer_Elapsed;
                    timer.Start();

                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlertAsync("Error", "Error Loading Data", "OK");

                var properties = new Dictionary<string, string>
                {
                    { "MainPageViewModel", "OnNavigatedTo" }
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
                        Application.Current.Properties["CPR"] = null;
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
                    { "MainPageViewModel", "ExecuteLogout" }
                };

                Crashes.TrackError(ex, properties);
            }
        }

    }
}
