using BMF_NewApp.Models;
using BMF_NewApp.Services;
using Microsoft.AppCenter.Crashes;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BMF_NewApp.ViewModels
{
    public class RenewLicensePageViewModel : ViewModelBase
    {
        private readonly IRestService _restService;
        private readonly ILocalService _localService;

        private ClsLicense _myLicense;
        public ClsLicense MyLicense
        {
            get { return _myLicense; }
            set { SetProperty(ref _myLicense, value); }
        }

        public RenewLicensePageViewModel(INavigationService navigationService, IPageDialogService dialogService,
    IRestService restService, ILocalService localService)
    : base(navigationService, dialogService)
        {
            try
            {
                _restService = restService;
                _localService = localService;

                Title = "Renew License";

                MyLicense = new ClsLicense();

            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string>
                        {
                            { "RenewLicensePageViewModel", "ViewLicenseModalPageViewModel" }
                        };

                Crashes.TrackError(ex, properties);
            }
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            try
            {
                if (parameters["license"] is ClsLicense _License)
                {
                    MyLicense = _License;

                    
                }
            }
            catch (Exception ex)
            {
                await DialogService.DisplayAlertAsync("Error", "Error Loading Data.", "OK");

                var properties = new Dictionary<string, string>
                {
                    { "RenewLicensePageViewModel", "OnNavigatedTo" }
                };

                Crashes.TrackError(ex, properties);
            }
        }

    }
}
