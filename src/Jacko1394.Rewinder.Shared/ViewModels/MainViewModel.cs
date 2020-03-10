// MainViewModel.cs
// MAGIQ Mobile
// Created by Jack Della on 9/03/2020
// Copyright © 2020 MAGIQ Software Ltd. All rights reserved.

using System.Windows.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;
//using Jacko1394.Watcher.Interfaces;
using Jacko1394.Zsh;
using MvvmHelpers.Interfaces;
using System.Threading.Tasks;

namespace Jacko1394.Rewinder.Shared.ViewModels {

	public class MainViewModel : BaseViewModel {

		public class Item : ObservableObject {

			public string? Display { get; set; }

			private bool show;
			public bool Show {
				set => SetProperty(ref show, value);
				get => show;
			}

			public Item() {
				Click = new Command(() => {
					Show = !Show;
				});
			}

			public ICommand Click { get; set; }
		}

		public Item Tap {
			set => Select(value);
		}

		public ObservableRangeCollection<Item> List { get; } = new ObservableRangeCollection<Item>();

		public IAsyncCommand AddCommand { get; }

		//private readonly ICodeWatcher _watcher;
		private readonly IZsh _zsh;

		private void Select(Item item) {

			if (item is null) {
				return;
			}


		}

		private async Task Add() {

			var str = await _zsh.CheckOutputAsync("ls");

		}

		public MainViewModel(IZsh zsh) {

			_zsh = zsh;

			AddCommand = new AsyncCommand(Add);

			//code.OnAddDirectory += Code_OnAddDirectory;
			//code.Add("/Users/jd/GIT/Rewinder");

			zsh.Dir = "/Users/jd";

			//foreach (var item in _watcher.WatcherFileTypes) {
			//	List.Add(new Item {
			//		Display = item
			//	});
			//}

		}

		private void Code_OnAddDirectory(object sender, string e) {
			List.Add(new Item {
				Display = e
			});
		}
	}
}
