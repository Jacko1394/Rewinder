// JavaExtensions.cs
// MAGIQ Mobile
// Created by Jack Della on 12/01/2020
// Copyright © 2020 MAGIQ Software Ltd. All rights reserved.

using System.Collections.Generic;

namespace Jacko1394.Watcher.Google {

    internal static class JavaExtensions {

        // JScript splice function
        public static List<T> Splice<T>(this List<T> input, int start, int count,
            params T[] objects) {
            var deletedRange = input.GetRange(start, count);
            input.RemoveRange(start, count);
            input.InsertRange(start, objects);

            return deletedRange;
        }

        // Java substring function
        public static string JavaSubstring(this string s, int begin, int end) => s.Substring(begin, end - begin);
    }

    //public static class DiffMatchPatchExtensions {

    //    public static List<Patch> AsPatches (this IEnumerable<string> patches) {

    //        var patchesl = patches
    //            .SelectMany(x => dmp.patch_fromText(x))
    //            .ToList();

    //    }

    //}

}
