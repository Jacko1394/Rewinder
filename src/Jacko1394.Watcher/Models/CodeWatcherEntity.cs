// CodeWatcherEntity.cs
// MAGIQ Mobile
// Created by Jack Della on 8/01/2020
// Copyright © 2020 MAGIQ Software Ltd. All rights reserved.

using System.Collections.Generic;
using Jacko1394.Watcher.Google;
using LiteDB;

namespace Jacko1394.Watcher.Models {

    public class CodeWatcherEntity {

        [BsonId(false)]
        public string? Path { get; set; }

        public string Latest { get; set; } = string.Empty;

        public List<Diff> Diffs { get; set; } = new List<Diff>();
    }

}
