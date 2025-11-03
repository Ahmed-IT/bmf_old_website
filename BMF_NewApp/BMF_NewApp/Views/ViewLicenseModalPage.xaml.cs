using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace BMF_NewApp.Views
{
    public partial class ViewLicenseModalPage : PopupPage
    {
        public ViewLicenseModalPage()
        {
            InitializeComponent();
        }

        private async void CloseClick(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}