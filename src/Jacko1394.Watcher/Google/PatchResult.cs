// PatchResult.cs
// MAGIQ Mobile
// Created by Jack Della on 12/01/2020
// Copyright © 2020 MAGIQ Software Ltd. All rights reserved.

using System.Linq;

namespace Jacko1394.Watcher.Google {

    public class PatchResult {

        public string Text { get; }
        public bool Success => Results.All(x => x == true);
        public bool[] Results { get; } = new bool[0];

        public PatchResult(string text, bool[]? results = null) {

            Text = text;

            if (results is bool[] bools) {
                Results = bools;
            }
        }

    }

}
