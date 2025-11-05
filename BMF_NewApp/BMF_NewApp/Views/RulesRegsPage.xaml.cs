using System;
using Xamarin.Forms;

namespace BMF_NewApp.Views
{
    public partial class RulesRegsPage : ContentPage
    {
        public RulesRegsPage()
        {
            InitializeComponent();
        }

        private void hamburgerButton_Clicked(object sender, EventArgs e)
        {
            navigationDrawer.ToggleDrawer();
        }
    }
}
