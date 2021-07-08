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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BMF_NewApp.ViewModels
{
    public class ViewPdfPageViewModel : ViewModelBase
    {
        public ClsBulletins _bulletin;
        public ClsBulletins Bulletin
        {
            get { return _bulletin; }
            set { SetProperty(ref _bulletin, value); }
        }

        public ClsRulesRegs _rulesRegs;
        public ClsRulesRegs RulesRegs
        {
            get { return _rulesRegs; }
            set { SetProperty(ref _rulesRegs, value); }
        }

        public string _pdfUrl;
        public string PdfUrl
        {
            get { return _pdfUrl; }
            set { SetProperty(ref _pdfUrl, value); }
        }

        private Stream _pdfDocumentStream;
        public Stream PDFDocumentStream
        {
            get { return _pdfDocumentStream; }
            set { SetProperty(ref _pdfDocumentStream, value); }
        }

        //  Injected service
        private readonly IRestService _restService;
        private readonly ILocalService _localService;

        public DelegateCommand AddToCalendar { get; set; }

        public ViewPdfPageViewModel(INavigationService navigationService, IPageDialogService dialogService,
            IRestService restService, ILocalService localService)
            : base(navigationService, dialogService)
        {
            _restService = restService;
            _localService = localService;

            Title = "";
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            Bulletin = new ClsBulletins();

            try
            {
                IsBusy = true;

                if (parameters["bulletins"] is ClsBulletins bulletins)
                {
                    Bulletin = bulletins;

                    Title = Bulletin.Title;

                    WebClient client = new WebClient();
                    PDFDocumentStream = new MemoryStream(client.DownloadData(Bulletin.PDF_Url));
                }
                else if (parameters["rulesRegs"] is ClsRulesRegs rulesRegs)
                {
                    RulesRegs = rulesRegs;

                    Title = RulesRegs.Title;

                    WebClient client = new WebClient();
                    PDFDocumentStream = new MemoryStream(client.DownloadData(RulesRegs.PDF_Url));
                }

                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;

                await DialogService.DisplayAlertAsync("Error", "Loading Min X Min File.", "OK");

                var properties = new Dictionary<string, string>
                {
                    { "ViewPdfPageViewModel", "OnNavigatedTo" }
                };

                Crashes.TrackError(ex, properties);
            }
        }
    }

}
