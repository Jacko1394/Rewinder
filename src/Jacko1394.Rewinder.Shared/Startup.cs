// Startup.cs
// MAGIQ Mobile
// Created by Jack Della on 8/03/2020
// Copyright © 2020 MAGIQ Software Ltd. All rights reserved.

using Magiq.Mobile.Hosting;
using Jacko1394.Watcher;
using Jacko1394.Watcher.Google;
using Jacko1394.Watcher.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Jacko1394.Watcher.Models;

namespace Jacko1394.Rewinder.Shared {

	public static class Startup {

		public static IHostBuilder Init() {

			return new HostBuilder()

				.ConfigureHostConfiguration(config => {
					config.AddJsonFromAssembly(typeof(Startup).Assembly);
				})

				.ConfigureServices((context, services) => {

					var config = context.Configuration;

					// IOptions<WatcherSettings> settings,

					var test = config.GetSection(nameof(WatcherSettings));

					services.AddTransient<MainPage>();
					services.AddTransient<IDbProvider, DbProvider>();
					services.AddTransient<IDiffMatchPatch, DiffMatchPatch>();

					services.AddSingleton<ICodeWatcher, CodeWatcher>();
					services.AddSingleton<App>();

				})

				.ConfigureLogging(logger => {
					logger.AddConsole();
				});
		}
	}
}
