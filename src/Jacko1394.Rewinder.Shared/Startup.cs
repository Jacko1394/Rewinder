// Startup.cs
// MAGIQ Mobile
// Created by Jack Della on 8/03/2020
// Copyright © 2020 MAGIQ Software Ltd. All rights reserved.

using Magiq.Mobile.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Jacko1394.Rewinder.Shared {

	public static class Startup {

		//internal static async Task Main() {
		//	var builder = Init(args);
		//	var host = builder.Build();
		//	await host.RunAsync();
		//}

		public static IHostBuilder Init() {

			return Host.CreateDefaultBuilder()

				.ConfigureHostConfiguration(config => {
					config.AddJsonFromAssembly(typeof(Startup).Assembly);
				})

				.ConfigureServices((context, services) => {
					var config = context.Configuration;

					// ICodeWatcher
					// IOptions<WatcherSettings> settings,
					// IDbProvider db,
					// IDiffMatchPatch differ

					// services.AddHostedService<App>();
				})

				.ConfigureLogging(logger => {
					logger.AddConsole();
				});
		}
	}
}
