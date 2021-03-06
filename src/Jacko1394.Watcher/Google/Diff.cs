﻿// Diff.cs
// MAGIQ Mobile
// Created by Jack Della on 12/01/2020
// Copyright © 2020 MAGIQ Software Ltd. All rights reserved.

namespace Jacko1394.Watcher.Google {

    /// <summary>
    /// Class representing one diff operation.
    /// </summary>
    public class Diff {

        public Operation Operation { get; set; } = Operation.EQUAL;

        public string Text { get; set; } = string.Empty;

        public Diff() {

        }

        /**
         * Constructor.  Initializes the diff with the provided values.
         * @param operation One of INSERT, DELETE or EQUAL.
         * @param text The text being applied.
         */
        public Diff(Operation operation, string text) {
            // Construct a diff with the specified operation and text.
            Operation = operation;
            Text = text;
        }

        /**
         * Display a human-readable version of this Diff.
         * @return text version.
         */
        public override string ToString() {
            string prettyText = Text.Replace('\n', '\u00b6');
            return "Diff(" + Operation + ",\"" + prettyText + "\")";
        }

        /**
         * Is this Diff equivalent to another Diff?
         * @param d Another Diff to compare against.
         * @return true or false.
         */
        public override bool Equals(object? obj) {
            // If parameter is null return false.
            if (obj is null) {
                return false;
            }

            // If parameter cannot be cast to Diff return false.
            if (!(obj is Diff p)) {
                return false;
            }

            // Return true if the fields match.
            return p.Operation == Operation && p.Text == Text;
        }

        public bool Equals(Diff obj) {
            // If parameter is null return false.
            if (obj is null) {
                return false;
            }

            // Return true if the fields match.
            return obj.Operation == Operation && obj.Text == Text;
        }

        public override int GetHashCode() {
            return Text.GetHashCode() ^ Operation.GetHashCode();
        }

    }

}
