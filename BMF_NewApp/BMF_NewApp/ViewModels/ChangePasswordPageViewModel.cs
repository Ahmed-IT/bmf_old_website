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

namespace BMF_NewApp.ViewModels
{
    public class ChangePasswordPageViewModel : ViewModelBase
    {
        private ClsAccount user;
        public ClsAccount User
        {
            get { return this.user; }
            set
            {
                if (!string.Equals(this.user, value))
                {
                    this.user = value;
                    this.RaisePropertyChanged();
                }
            }
        }

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

        private DelegateCommand _commandChangePass;
        public DelegateCommand CommandChangePass => _commandChangePass ?? (_commandChangePass = new DelegateCommand(ExecuteChangePass));

        public ChangePasswordPageViewModel(INavigationService navigationService, IPageDialogService dialogService,
            IRestService restService, ILocalService localService)
            : base(navigationService, dialogService)
        {
            Title = "Change Password";

            User = new ClsAccount();

            _restService = restService;
            _localService = localService;

        }

        private async void ExecuteChangePass()
        {
            try
            {
                if (User != null)
                {
                    if (User.Password != null && User.ConfirmPassword != null && User.OldPassword != null)
                    {
                        if (User.Password == User.ConfirmPassword)
                        {
                            IsBusy = true;

                            var send = await _restService.ChangePassword(User);

                            if (send == "Password Changed")
                            {
                                await DialogService.DisplayAlertAsync("Change Password", "Password Changed Successfully", "OK");
                            }
                            else if (send == "Wrong Old Password")
                            {
                                await DialogService.DisplayAlertAsync("Change Password", "Your Old Password is Wrong, Please Try Again", "OK");
                            }
                            else
                            {
                                await DialogService.DisplayAlertAsync("Change Password", "Error Changing Password", "OK");
                            }

                            IsBusy = false;
                        }
                        else
                        {
                            await DialogService.DisplayAlertAsync("Change Password", "New Password and Confirm Password Dose Not Match", "OK");
                        }
                    }
                    else
                    {
                        await DialogService.DisplayAlertAsync("Change Password", "Please Fill All Fields", "OK");
                    }
                }
                else
                {
                    await DialogService.DisplayAlertAsync("Forgot Password", "Missing Email Address", "OK");
                }
            }
            catch (Exception ex)
            {
                IsBusy = false;

                await DialogService.DisplayAlertAsync("Error", "Error Sending Request", "OK");

                var properties = new Dictionary<string, string>
                {
                    { "ForgotPasswordPageViewModel", "ExecuteLogin" }
                };

                Crashes.TrackError(ex, properties);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            try
            {
                if (App.CPR != null)
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

                    Profile = await _restService.GetProfile(App.CPR);

                    User.ID = Profile.ID;
                    User.Email_Address = Profile.Account.Email_Address;

                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlertAsync("Error", "Error Loading Data", "OK");

                var properties = new Dictionary<string, string>
                {
                    { "ChangePasswordPageViewModel", "OnNavigatedTo" }
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
