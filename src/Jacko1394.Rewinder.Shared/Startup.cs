// Startup.cs
// MAGIQ Mobile
// Created by Jack Della on 8/03/2020
// Copyright © 2020 MAGIQ Software Ltd. All rights reserved.

using Magiq.Mobile.Hosting;
using Jacko1394.Watcher;
using Jacko1394.Watcher.Models;
using Jacko1394.Watcher.Google;
using Jacko1394.Watcher.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Jacko1394.Rewinder.Shared.Views;
using Jacko1394.Rewinder.Shared.ViewModels;
using Jacko1394.Zsh;

namespace Jacko1394.Rewinder.Shared {

	public static class Startup {

		public static IHostBuilder Init() {

			return new HostBuilder()

				.ConfigureHostConfiguration(config => {
					config.AddJsonFromAssembly(typeof(Startup).Assembly);
				})

				.ConfigureServices((context, services) => {

					var config = context.Configuration;

					var watcher = config.GetSection(nameof(WatcherSettings)).Get<WatcherSettings>();
					services.AddOptions<WatcherSettings>().Configure(config => {
						config.IncludeFiles = watcher.IncludeFiles;
						config.ExcludeDirectories = watcher.ExcludeDirectories;
					});

					services.AddTransient<MainPage>();
					services.AddTransient<DirectoryPage>();

					services.AddTransient<MainViewModel>();

					services.AddTransient<IZsh, Zsh.Zsh>();
					services.AddTransient<IDbProvider, DbProvider>();
					services.AddTransient<ICodeWatcher, CodeWatcher>();
					services.AddTransient<IDiffMatchPatch, DiffMatchPatch>();

					services.AddSingleton<App>();
				})

				.ConfigureLogging(logger => {
					logger.AddConsole();
				});
		}
	}
}
