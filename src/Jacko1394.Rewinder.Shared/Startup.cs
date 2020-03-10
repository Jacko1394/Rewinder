// Startup.cs
// MAGIQ Mobile
// Created by Jack Della on 8/03/2020
// Copyright © 2020 MAGIQ Software Ltd. All rights reserved.

using System.Linq;
using System.IO.Compression;
using Magiq.Mobile.Hosting;
using Magiq.Mobile.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Jacko1394.Rewinder.Shared.Views;
using Jacko1394.Rewinder.Shared.ViewModels;
using Jacko1394.Zsh;
using System.Diagnostics;
using System;

namespace Jacko1394.Rewinder.Shared {

	public static class Startup {

		private static Process ShellProcess(string path) => new Process {

			EnableRaisingEvents = true,

			StartInfo = new ProcessStartInfo {

				// shell
				FileName = path,
				UseShellExecute = false,
				CreateNoWindow = true,

				// intercept
				RedirectStandardInput = true,
				RedirectStandardOutput = true,
				RedirectStandardError = true
			}
		};

		private static void LogOutput(object sender, DataReceivedEventArgs e) => Console.WriteLine(e.Data);

		public static IHostBuilder Init() {

			using var data = GeneralExtensions.GetStream("osx");
			using var zip = new ZipArchive(data);
			var test = zip.Entries.Select(x => x.FullName).ToJson();

			using var proc = ShellProcess("path");

			if (proc.Start()) {
				proc.OutputDataReceived += LogOutput;
				proc.ErrorDataReceived += LogOutput;
			}

			proc.WaitForExit(10_000);
			// var run = new Process();
			// run.

			return new HostBuilder()

				.ConfigureHostConfiguration(config => {
					config.AddJsonFromAssembly(typeof(Startup).Assembly);
				})

				.ConfigureServices((context, services) => {

					var config = context.Configuration;

					services.AddTransient<MainPage>();
					services.AddTransient<DirectoryPage>();

					services.AddTransient<MainViewModel>();

					services.AddTransient<IZsh, Zsh.Zsh>();
					services.AddSingleton<App>();
				})

				.ConfigureLogging(logger => {
					logger.AddConsole();
				});
		}
	}
}
