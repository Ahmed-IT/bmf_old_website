using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BMF_NewApp.ViewModels;

namespace BMF_NewApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : ContentView
    {
        public MainMenuModalPageViewModel Menu { get; set; }

        public static readonly BindableProperty MenuProperty = BindableProperty.Create(
            propertyName: nameof(Menu),
            returnType: typeof(MainMenuModalPageViewModel),
            declaringType: typeof(MainMenu),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: MenuPropertyChanged);

        private static void MenuPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue is MainMenuModalPageViewModel vm)
            {
                if (bindable is MainMenu control)
                {
                    control.BindingContext = vm;
                }
            }
        }

        public MainMenu()
        {
            InitializeComponent();
        }
    }
}