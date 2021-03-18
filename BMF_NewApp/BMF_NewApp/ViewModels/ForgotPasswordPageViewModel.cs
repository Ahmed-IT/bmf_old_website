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
    public class ForgotPasswordPageViewModel : ViewModelBase
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

        private DelegateCommand _commandSendPass;
        public DelegateCommand CommandSendPass => _commandSendPass ?? (_commandSendPass = new DelegateCommand(ExecuteSendPass));

        public ForgotPasswordPageViewModel(INavigationService navigationService, IPageDialogService dialogService,
            IRestService restService, ILocalService localService)
            : base(navigationService, dialogService)
        {
            Title = "Forgot Password";

            User = new ClsAccount();

            _restService = restService;
            _localService = localService;

        }

        private async void ExecuteSendPass()
        {
            try
            {
                if (User != null)
                {
                    if (User.Email_Address != null)
                    {
                        IsBusy = true;

                        var send = await _restService.SendAccountPassword(User);

                        if (send == "Sent")
                        {
                            await DialogService.DisplayAlertAsync("Forgot Password", "Email Sent", "OK");
                        }
                        else if (send == "Not Found")
                        {
                            await DialogService.DisplayAlertAsync("Forgot Password", "Account Not Found", "OK");
                        }
                        else
                        {
                            await DialogService.DisplayAlertAsync("Forgot Password", "Login Failed, Wrong Username or Password", "OK");
                        }

                        IsBusy = false;
                    }
                    else
                    {
                        await DialogService.DisplayAlertAsync("Forgot Password", "Missing Email Address", "OK");
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
    }
}
