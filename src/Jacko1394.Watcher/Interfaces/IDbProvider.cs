// IDbProvider.cs
// MAGIQ Mobile
// Created by Jack Della on 13/01/2020
// Copyright © 2020 MAGIQ Software Ltd. All rights reserved.

using LiteDB;

namespace Jacko1394.Watcher.Interfaces {

    public interface IDbProvider {

        LiteDatabase GetLiteDatabase(string? path = null);

    }
}
