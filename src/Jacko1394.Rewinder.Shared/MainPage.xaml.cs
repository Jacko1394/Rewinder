using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Jacko1394.Rewinder.Shared.ViewModels;

[assembly:XamlCompilation(XamlCompilationOptions.Compile)]
namespace Jacko1394.Rewinder.Shared {

    public partial class MainPage : ContentPage {

        public MainPage(MainViewModel viewModel) {
            InitializeComponent();
            BindingContext = viewModel;
        }

    }
}
