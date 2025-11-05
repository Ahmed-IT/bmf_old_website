using System;
using Xamarin.Forms;

namespace BMF_NewApp.Views
{
    public partial class MyIDsPage : ContentPage
    {
        public MyIDsPage()
        {
            InitializeComponent();
        }

        private void hamburgerButton_Clicked(object sender, EventArgs e)
        {
            navigationDrawer.ToggleDrawer();
        }
    }
}
