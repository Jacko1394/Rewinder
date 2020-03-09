using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Jacko1394.Rewinder.Shared.ViewModels;

[assembly:XamlCompilation(XamlCompilationOptions.Compile)]
namespace Jacko1394.Rewinder.Shared.Views {

    public partial class MainPage : ContentPage {

        public MainPage(MainViewModel viewModel) {
            InitializeComponent();
            BindingContext = viewModel;
			thyList.ItemSelected += ThyList_ItemSelected;
        }

		private async void ThyList_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
			thyList.SelectedItem = null;
			await Navigation.PushAsync(new DirectoryPage());
		}
	}
}
