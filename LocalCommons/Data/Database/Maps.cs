﻿// Copyright (c) Aura development team - Licensed under GNU GPL
// For more information, see licence.txt in the main folder
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace LocalCommons.Data.Database
{
	[Serializable]
	public class MapData
	{
		public int Id { get; set; }
		public string ClassName { get; set; }
		public string EngName { get; set; }
		public string LocalKey { get; set; }
	}

	/// <summary>
	/// Map database, indexed by map id.
	/// </summary>
	public class MapDb : DatabaseJsonIndexed<int, MapData>
	{
		private Dictionary<string, MapData> _nameIndex = new Dictionary<string, MapData>();

		public MapData Find(string className)
		{
			MapData result;
			this._nameIndex.TryGetValue(className, out result);
			return result;
		}

		protected override void ReadEntry(JObject entry)
		{
			entry.AssertNotMissing("mapId", "className", "engName", "localKey");

			var info = new MapData();

			info.Id = entry.ReadInt("mapId");
			info.ClassName = entry.ReadString("className");
			info.EngName = entry.ReadString("engName");
			info.LocalKey = entry.ReadString("localKey");

			this.Entries[info.Id] = info;
			this._nameIndex[info.ClassName] = info;
		}
	}
}