using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

public class MapSection
{
	public string Name { get; set; }
	public Dictionary<string, string> Entries { get; set; }

	public MapSection() { }

	public void Merge(Dictionary<string, string> entries)
	{
		if (entries != null && entries.Count > 0)
		{
			bool isTypeList = MapConstants.TypesList.Contains(Name);
			foreach (KeyValuePair<string, string> newEntry in entries)
			{
				if (isTypeList)
				{
					if (!Entries.ContainsKey(newEntry.Key) || !Entries.ContainsValue(newEntry.Value))
						Entries.Add(newEntry.Key, newEntry.Value);
				}
				else
				{
					if (Entries.ContainsKey(newEntry.Key))
						Entries[newEntry.Key] = newEntry.Value;
					else
						Entries.Add(newEntry.Key, newEntry.Value);
				}
			}
		}
	}
}
