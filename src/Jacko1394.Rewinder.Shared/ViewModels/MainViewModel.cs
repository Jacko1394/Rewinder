// MainViewModel.cs
// MAGIQ Mobile
// Created by Jack Della on 9/03/2020
// Copyright © 2020 MAGIQ Software Ltd. All rights reserved.

using MvvmHelpers;

namespace Jacko1394.Rewinder.Shared.ViewModels {

	public class MainViewModel : BaseViewModel {

		public class Item {
			public string? Display { get; set; }
		}

		public ObservableRangeCollection<Item> List { get; } = new ObservableRangeCollection<Item>();

		public MainViewModel() {

			List.AddRange(new Item[] {
				new Item {
					Display = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consect"
				},
				new Item {
					Display = "oing to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem"
				},
				new Item {
					Display = "at predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined "
				},
				new Item {
					Display = "e English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ips"
				},
				new Item {
					Display = "simply dummy text of the pri"
				}
			});

		}

	}
}
