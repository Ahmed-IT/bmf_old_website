using BMF_NewApp.Services;
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
        //  Injected service
        private readonly IRestService _restService;
        private readonly ILocalService _localService;

        public ForgotPasswordPageViewModel(INavigationService navigationService, IPageDialogService dialogService,
            IRestService restService, ILocalService localService)
            : base(navigationService, dialogService)
        {
            Title = "Forgot Password";

            _restService = restService;
            _localService = localService;

        }
    }
}
