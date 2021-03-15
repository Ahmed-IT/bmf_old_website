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
    public class NewAccountPageViewModel : ViewModelBase
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

        private DelegateCommand _commandNewMember;
        public DelegateCommand commandNewMember => _commandNewMember ?? (_commandNewMember = new DelegateCommand(ExecuteNewMember));


        public NewAccountPageViewModel(INavigationService navigationService, IPageDialogService dialogService,
            IRestService restService, ILocalService localService)
            : base(navigationService, dialogService)
        {
            Title = "New Account";

            User = new ClsAccount();

            _restService = restService;
            _localService = localService;

        }

        private async void ExecuteNewMember()
        {
            try
            {
                if (User != null)
                {
                    if (User.CPR_Number != null && User.Email_Address != null
                        && User.Password != null && User.ConfirmPassword != null)
                    {
                        if (User.Password == User.ConfirmPassword)
                        {
                            IsBusy = true;

                            var postUser = await _restService.CreateMemebrAccount(new ClsAccount
                            {
                                Email_Address = User.Email_Address,
                                Password = User.Password,
                                CPR_Number = User.CPR_Number,
                                ConfirmPassword = User.ConfirmPassword
                            });

                            IsBusy = false;

                            if (postUser == "Account Created")
                            {
                                await DialogService.DisplayAlertAsync("New Account", "Account Created.", "OK");

                                await NavigationService.GoBackAsync();
                            }
                            else if (postUser == "Membership Not Found")
                            {
                                await DialogService.DisplayAlertAsync("New Account", "Membership Not Found.", "OK");
                            }
                            else if (postUser == "Account Exist")
                            {
                                await DialogService.DisplayAlertAsync("New Account", "Account Already Exist.", "OK");
                            }
                            else
                            {
                                await DialogService.DisplayAlertAsync("Error", "Error Creating New Account.", "OK");
                            }
                        }
                        else
                        {
                            await DialogService.DisplayAlertAsync("Error", "Password Does Not Match Confirm Password.", "OK");
                        }
                    }
                    else
                    {
                        await DialogService.DisplayAlertAsync("Error", "Please Fill All Information.", "OK");
                    }
                }
                else
                {
                    await DialogService.DisplayAlertAsync("Error", "Please Fill All Information.", "OK");
                }
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string>
                {
                    { "NewAccountPageViewModel", "ExecuteNewMember" }
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
