using BMF_NewApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BMF_NewApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : ContentView
    {
        public MainMenuModalPageViewModel Menu { get; set; }

        public static readonly BindableProperty MenuProperty = BindableProperty.Create(
                                                  propertyName: "Menu",
                                                  returnType: typeof(MainMenuModalPageViewModel),
                                                  declaringType: typeof(MainMenu),
                                                  defaultValue: null,
                                                  defaultBindingMode: BindingMode.TwoWay,
                                                  propertyChanged: MenuPropertyChanged);

        private static void MenuPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var control = bindable as MainMenu;

                if (control != null)
                {
                    control.BindingContext = newValue as MainMenuModalPageViewModel;
                }
            }
        }

        public MainMenu()
        {
            InitializeComponent();
        }

    }
}