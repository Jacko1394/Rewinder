// ICodeWatcher.cs
// Created by Jack Della on 8/01/2020
// Copyright © 2020 MAGIQ Software Ltd. A //

using System;

namespace Jacko1394.Watcher.Interfaces {

    public interface ICodeWatcher {

        event EventHandler<string>? OnNewDiff; // html
        event EventHandler<string>? OnNewEntity; // file content

        void Add(string dir);

    }
}
