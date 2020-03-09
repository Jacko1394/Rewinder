// DbProvider.cs
// MAGIQ Mobile
// Created by Jack Della on 13/01/2020
// Copyright © 2020 MAGIQ Software Ltd. All rights reserved.

using LiteDB;
using Jacko1394.Watcher.Interfaces;

namespace Jacko1394.Watcher {

    public class DbProvider : IDbProvider {

        public LiteDatabase GetLiteDatabase(string? path = null) {
            return new LiteDatabase("/Users/jd/Desktop/diff.db");
        }
    }
}
