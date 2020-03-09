// ICodeWatcher.cs
// Created by Jack Della on 8/01/2020
// Copyright © 2020 MAGIQ Software Ltd. A //

using System;
using System.Collections.Generic;

namespace Jacko1394.Watcher.Interfaces {

    public interface ICodeWatcher {

        IEnumerable<string> WatcherFileTypes { get; }
        IEnumerable<string> WatcherDirectorires { get; }

        event EventHandler<string>? OnNewDiff; // html
        event EventHandler<string>? OnNewEntity; // file content
        event EventHandler<string>? OnAddDirectory; // Add(string dir);

        void Add(string dir);

    }
}
