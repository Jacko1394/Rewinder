using Microsoft.Extensions.Logging;
using Xamarin.Forms;

namespace Jacko1394.Rewinder.Shared {

    public class App : Application {

        private readonly ILogger _logger;

        public App(MainPage main, ILogger<App> logger) {
            MainPage = new NavigationPage(main);
            _logger = logger;
        }

        protected override void OnStart() {
            base.OnStart();
            _logger.LogInformation("Application started");
        }

        protected override void OnSleep() {
            base.OnSleep();
            _logger.LogInformation("Application sleeps");
        }

        protected override void OnResume() {
            base.OnResume();
            _logger.LogInformation("Application resumes");
        }
    }
}
