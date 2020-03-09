// MainViewModel.cs
// MAGIQ Mobile
// Created by Jack Della on 9/03/2020
// Copyright © 2020 MAGIQ Software Ltd. All rights reserved.

using MvvmHelpers;
using Jacko1394.Watcher.Interfaces;
using System.Windows.Input;
using MvvmHelpers.Commands;

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

		private readonly ICodeWatcher _watcher;

		private void Select(Item item) {

			if (item is null) {
				return;
			}


		}

		public MainViewModel(ICodeWatcher code) {

			_watcher = code;

			foreach (var item in _watcher.Directories) {
				List.Add(new Item {
					Display = item
				});
			}

		}

	}
}
