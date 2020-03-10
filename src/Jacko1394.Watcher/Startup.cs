// Startup.cs
// MAGIQ Mobile
// Created by Jack Della on 8/03/2020
// Copyright © 2020 MAGIQ Software Ltd. All rights reserved.

using System.Threading.Tasks;
using Magiq.Mobile.Hosting;
using Jacko1394.Zsh;
using Jacko1394.Watcher.Models;
using Jacko1394.Watcher.Google;
using Jacko1394.Watcher.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jacko1394.Watcher {

	public static class Startup {

		internal static async Task Main() {

			var builder = Init();
			var host = builder.Build();

			var test = host.Services.GetRequiredService<ICodeWatcher>();
			test.Add("/Users/jd/Downloads");

			await host.RunAsync();

		}

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

					services.AddTransient<IZsh, Zsh.Zsh>();
					services.AddTransient<IDbProvider, DbProvider>();
					services.AddTransient<ICodeWatcher, CodeWatcher>();
					services.AddTransient<IDiffMatchPatch, DiffMatchPatch>();
				})

				.ConfigureLogging(logger => {
					logger.AddConsole();
				});
		}
	}
}
