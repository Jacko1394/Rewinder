using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly:XamlCompilation(XamlCompilationOptions.Compile)]
namespace Jacko1394.Rewinder.Shared {

    public partial class MainPage : ContentPage {

        public MainPage() {
            InitializeComponent();
        }

        private void GotoCalculatorClicked(object sender, EventArgs e) {
            //await DisplayAlert("Jack Della", "Test", "OK");
        }
    }
}
