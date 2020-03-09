// This file has been autogenerated from a class added in the UI designer.

using System;
using AppKit;
using Jacko1394.Rewinder.Shared;
using Xamarin.Forms;

namespace Jacko1394.Rewinder.MacOS
{
    public partial class Container : NSViewController
	{
		private NSViewController? _contentView;
        private readonly NavigationPage? CurrentNavigationPage = Application.Current.MainPage as NavigationPage;

        public Container (IntPtr handle) : base (handle)
		{
		}

        partial void BackButtonPressed(Foundation.NSObject sender)
        {
            ((NavigationPage)App.Current.MainPage).PopAsync();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (_contentView?.View is null) {
                throw new Exception("no view");
            }

            AddChildViewController(_contentView);
            Content.AddSubview(_contentView.View);
            _contentView.View.Frame = Content.Frame;

            if (CurrentNavigationPage is { } page)
            {
                page.Pushed += Container_Pushed;
                page.Popped += Container_Pushed;
                SetNavigationBarState();
            }
        }

        private void Container_Pushed(object sender, NavigationEventArgs e) {
            SetNavigationBarState();
        }

        private void SetNavigationBarState() {
            if (CurrentNavigationPage?.CurrentPage is Page currentPage) {
                TitleLabel.StringValue = currentPage.Title;
                var isPoppedToRootPage = currentPage == CurrentNavigationPage.RootPage;
                BackButton.Hidden = isPoppedToRootPage;
            }
        }

        public override void ViewDidLayout() {
            base.ViewDidLayout();
            if (_contentView?.View is { } view) {
                view.Frame = Content.Frame;
            }
        }

        public override void LoadView() {
            base.LoadView();
        }

        public void SetContent(NSViewController viewController) {
            _contentView = viewController;
        }
    }
}