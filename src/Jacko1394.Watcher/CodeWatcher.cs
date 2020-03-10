﻿// Jack Della - 2020

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Jacko1394.Watcher.Models;
using Jacko1394.Watcher.Google;
using Jacko1394.Watcher.Interfaces;
using Magiq.Mobile.Extensions;
using LiteDB;

namespace Jacko1394.Watcher {

	public class CodeWatcher : ICodeWatcher {

		public event EventHandler<string>? OnNewDiff;
		public event EventHandler<string>? OnNewEntity;
		public event EventHandler<string>? OnAddDirectory;

		public IEnumerable<string> WatcherFileTypes => IncludeFilters;
		public IEnumerable<string> WatcherDirectorires => IncludeFilters;

		private readonly ILogger _logger;
		private readonly ILiteCollection<CodeWatcherEntity> _diffs;
		// private readonly ILiteQueryable<CodeWatcherEntity> _diffsQueriable;
		private readonly IDictionary<string, FileSystemWatcher> Watchers = new Dictionary<string, FileSystemWatcher>();
		private readonly IDiffMatchPatch _differ;
		private readonly ILiteDatabase _db;

		private readonly string[] IncludeFilters;
		private readonly string[] ExcludeDirectories;

		public CodeWatcher(
			ILogger<CodeWatcher> logger,
			IOptions<WatcherSettings> settings,
			IDbProvider db,
			IDiffMatchPatch differ
		) {

			_logger = logger;
			_differ = differ;
			_db = db.GetLiteDatabase();
			_diffs = _db.GetCollection<CodeWatcherEntity>("Diffs");

			var s = settings.Value;
			IncludeFilters = s.IncludeFiles;
			ExcludeDirectories = s.ExcludeDirectories;

			var test = _diffs.FindAll().ToJson();
			// _diffsQueriable = _diffs.Query();
			// var str = _diffs.FindAll().ToJson();
			File.WriteAllText("/Users/jd/Desktop/lite.json", test);
		}

		public void Add(string dir) {

			if (Watchers.TryGetValue(dir, out var watcher)) {
				_logger.LogError($"Already watching {dir}");
				return;
			}

			watcher = new FileSystemWatcher(dir) {
				EnableRaisingEvents = true,
				IncludeSubdirectories = true
			};

			watcher.Changed += DiffUpdate; // FileSystem
			watcher.Renamed += DiffUpdate; // Renamed

			Watchers[dir] = watcher;
			OnAddDirectory?.Invoke(this, dir);
		}

		private void DiffUpdate(object sender, FileSystemEventArgs e) {

			var path = e.FullPath;
			var type = Path.GetExtension(path);

			if (!IncludeFilters.Contains('*' + type)) {
				//foreach (var item in IncludeFilters) {
				//	watcher.Filters.Add(item);
				//}
				_logger.LogError($"IncludeFilters: {path}");
				return;
			}

			foreach (var dir in ExcludeDirectories) {
				if (path.Contains(dir)) {
					_logger.LogWarning($"Build file: {path}");
					return;
				}
			}

			if (!File.Exists(path)) {
				_logger.LogWarning($"File not found: {path}");
				return;
			}

			using var data = File.Open(path, FileMode.Open);

			if (data.Length > 1_048_576) { // 1 mb
				_logger.LogWarning($"File {path} is Larger than 1MB: {data.Length}");
				return;
			}

			using var reader = new StreamReader(data);
			var current = reader.ReadToEnd();

			if (string.IsNullOrWhiteSpace(current)) {
				_logger.LogError($"Empty file: {path}");
				return;
			}

			if (_diffs.FindById(path) is CodeWatcherEntity codeWatcherEntity) {
				// do diff
				var diffs = _differ.diff_main(codeWatcherEntity.Latest, current);
				_differ.diff_cleanupSemantic(diffs);

				var final = diffs.Where(x => x.Operation != Operation.EQUAL)
					.Where(x => !string.IsNullOrWhiteSpace(x.Text));

				if (!final.Any()) {
					return; // all content equal
				}

				//updates
				codeWatcherEntity.Latest = current;
				codeWatcherEntity.Diffs.AddRange(final);

				//save
				var html = _differ.diff_prettyHtml(final.ToList());
				OnNewDiff?.Invoke(this, html);
				_logger.LogInformation(html);
				_diffs.Update(codeWatcherEntity);

			} else {

				// new
				codeWatcherEntity = new CodeWatcherEntity {
					Latest = current,
					Path = path
				};

				// add
				OnNewEntity?.Invoke(this, current);
				_logger.LogInformation($"Added new file for tracking: {path}");
				_diffs.Insert(codeWatcherEntity);
				_diffs.EnsureIndex(x => x.Path);
			}

			_db.Checkpoint();

		}

	}
}
