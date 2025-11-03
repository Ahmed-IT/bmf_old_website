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
using static BMF_NewApp.App;
using static BMF_NewApp.Models.BMFAppTables;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel.Communication;

namespace BMF_NewApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
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

        //  Injected service
        private readonly IRestService _restService;
        private readonly ILocalService _localService;

        //  Command Login
        private DelegateCommand _commandLogin;
        public DelegateCommand CommandLogin => _commandLogin ?? (_commandLogin = new DelegateCommand(ExecuteLogin));

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService,
            IRestService restService, ILocalService localService)
            : base(navigationService, dialogService)
        {

            Title = "Login Page";

            User = new ClsAccount();

            _restService = restService;
            _localService = localService;

        }

        private async void ExecuteLogin()
        {
            try
            {
                if (User.Email_Address != "" && User.Password != "")
                {
                    IsBusy = true;

                    //Auth User Account from Server
                    var login = await _restService.AuthUser(User);

                    if (login != null)
                    {
                        //Get User Info from local DB
                        var CheckUserInfo = await _localService.GetUserAccount(login.Email_Address);

                        //Check if User Account Exist Locally
                        if (CheckUserInfo == null)
                        {
                            //Insert User Info into local DB
                            var userInfo = await _localService.InsertUserAccount(new TblUsersAccount
                            {
                                CPR = login.CPR_Number,
                                Email = login.Email_Address,
                                LoggedIn = true,
                                Id = login.ID
                            });

                            if (userInfo == true)
                            {
                                //Navigate User to Main Page
                                App.ID = login.ID;
                                App.CPR = login.CPR_Number;
                                App.IsLoggedIn = true;
                                await NavigationService.NavigateAsync("MainPage");
                            }
                            else
                            {
                                await DialogService.DisplayAlertAsync("Error", "In saving user information.", "OK");
                            }
                        }
                        else
                        {
                            //Save Login Info
                            var UpdateUserInfo = await App.Database.SaveItemAsync(new TblUsersAccount
                            {
                                Id = CheckUserInfo.Id,
                                CPR = CheckUserInfo.CPR,
                                Email = CheckUserInfo.Email,
                                LoggedIn = true
                            });

                            if (UpdateUserInfo == 1)
                            {
                                App.ID = login.ID;
                                App.CPR = login.CPR_Number;
                                App.IsLoggedIn = true;
                                await NavigationService.NavigateAsync("MainPage");
                            }
                            else
                            {
                                await DialogService.DisplayAlertAsync("Error", "In saving user information.", "OK");
                            }
                        }

                    }
                    else
                    {
                        await DialogService.DisplayAlertAsync("Login", "Login Failed, Wrong Username or Password.", "OK");
                    }

                    IsBusy = false;
                }
                else
                {
                    await DialogService.DisplayAlertAsync("Login", "Login Failed, Missing Username or Password.", "OK");
                }
            }
            catch (Exception ex)
            {
                IsBusy = false;

                await DialogService.DisplayAlertAsync("Error", "Error In Login.", "OK");

                var properties = new Dictionary<string, string>
                {
                    { "LoginPageViewModel", "ExecuteLogin" }
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
