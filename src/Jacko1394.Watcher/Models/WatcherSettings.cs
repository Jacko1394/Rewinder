// WatcherSettings.cs
// MAGIQ Mobile
// Created by Jack Della on 20/01/2020
// Copyright © 2020 MAGIQ Software Ltd. All rights reserved.

namespace Jacko1394.Watcher.Models {

    public class WatcherSettings {

        public string[] IncludeFiles { get; set; } = new string[] { "*.txt" };
        public string[] ExcludeDirectories { get; set; } = new string[] { "/bin/", "/obj/", "/.vs/", "/.git/" };
    }
}
