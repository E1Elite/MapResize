using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using NLog;

public class MapParser
{
	static Logger log = LogManager.GetCurrentClassLogger();

	public MapParser() { 	}

	public List<MapSection> ParseMap(string filename)
	{
		List<MapSection> mapSections = new List<MapSection>();
		log.Info("\r\n#############################################");
		log.Info("Parsing map: " + filename);
		if (!String.IsNullOrEmpty(filename) && File.Exists(filename))
		{
			List<string> mapLines = CleanUp(File.ReadAllLines(filename));
			if (mapLines.Count > 0)
			{
				int lineIndex = 0;
    			bool isTypeListSection = false;

				while (lineIndex < mapLines.Count)
				{
					if (mapLines[lineIndex].IndexOf('[') == 0 && mapLines[lineIndex].IndexOf(']') > 1)
					{
						MapSection currentSection = new MapSection();
						Dictionary<string, string> sectionEntries = new Dictionary<string, string>();
						currentSection.Name = mapLines[lineIndex].Substring(1, mapLines[lineIndex].IndexOf(']') - 1);
                        isTypeListSection = MapConstants.TypesList.Contains(currentSection.Name);
                        lineIndex++;
						while (lineIndex < mapLines.Count && mapLines[lineIndex].IndexOf('[') < 0)
						{
							if (mapLines[lineIndex].IndexOf('=') > 0)
							{
								int indexOfEquals = mapLines[lineIndex].IndexOf('=');
								string key = mapLines[lineIndex].Substring(0, indexOfEquals).Trim();
								string value = mapLines[lineIndex].Substring(indexOfEquals + 1, mapLines[lineIndex].Length - indexOfEquals - 1).Trim();
								if (!String.IsNullOrEmpty(key))
								{
									if (isTypeListSection)
									{
										if (!String.IsNullOrEmpty(value) && !sectionEntries.ContainsKey(key) && !sectionEntries.ContainsValue(value))
											sectionEntries.Add(key, value);
										else
											log.Info("Skipping [" + currentSection.Name + "] index: " + key + "=" + value);
									}
									else
									{
										if (sectionEntries.ContainsKey(key))
										{
											sectionEntries[key] = value;
											log.Info("Overwriting [" + currentSection.Name + "] " + key + " with " + value);
										}
										else
											sectionEntries.Add(key, value);
									}
								}
								lineIndex++;
							}
							else
								lineIndex++;
						}
						currentSection.Entries = sectionEntries;

						int existingIndex = mapSections.FindIndex(x => x.Name.Equals(currentSection.Name));
						if (existingIndex >= 0)
						{
							mapSections[existingIndex].Merge(sectionEntries);
							log.Info("Merging section [" + currentSection.Name + "] ");
						}
						else
							mapSections.Add(currentSection);
					}
					else
						lineIndex++;
				}
			}
		}
		return mapSections;
	}

	private List<string> CleanUp(string[] lines)
	{
		List<string> resultantLines = new List<string>();
		if (lines.Length > 0)
		{
            foreach (string line in lines)
			{
                string lineTemp = "";
                if (line.IndexOf(';') >= 0)
					lineTemp = line.Substring(0, line.IndexOf(';'));
				else
					lineTemp = String.Copy(line);
                lineTemp.Trim();
				if (!String.IsNullOrEmpty(lineTemp))
					resultantLines.Add(lineTemp);
			}
		}
		return resultantLines;
	}

	// Assuming no duplicate of [Map] section for UI calculation.
	public string GetMapSize(string mapname)
	{
		string value = "Error: Unable to find map size.";
		if (!String.IsNullOrEmpty(mapname) && File.Exists(mapname))
		{
			log.Info("\r\n#############################################");
			log.Info("Reading map for size calculation: " + mapname);

			List<string> mapLines = CleanUp(File.ReadAllLines(mapname));
			if (mapLines.Count > 0)
			{
				int lineIndex = mapLines.FindIndex(line => line.Equals("[Map]"));
				if (lineIndex >= 0)
				{
					do
					{
						lineIndex++;
						if (mapLines[lineIndex].IndexOf('=') > 0)
						{
							int indexOfEquals = mapLines[lineIndex].IndexOf('=');
							string key = mapLines[lineIndex].Substring(0, indexOfEquals).Trim();
							if (!String.IsNullOrEmpty(key) && key.Equals("Size"))
							{
								string size = mapLines[lineIndex].Substring(indexOfEquals + 1, mapLines[lineIndex].Length - indexOfEquals - 1).Trim();
								string[] parts = size.Split(',');
								int x, y, width, height = -1;
								if (parts.Length >= 4 && parts[0] != null  && parts[1] != null && parts[2] != null && parts[3] != null)
								{
									int.TryParse(parts[0], out x);
									int.TryParse(parts[1], out y);
									int.TryParse(parts[2], out width);
									int.TryParse(parts[3], out height);
									if (x != 0 || y != 0)
										value = "Error: Map size does not start at 0,0.";
									else if (width < 1 || width > 511 || height < 1 || height > 511)
										value = "Error: Map size value(s) invalid. Width = " + width + " Height = " + height;
									else
										value = width + "|" + height;
								}
							}
						}
					}
					while (mapLines[lineIndex].IndexOf('[') < 0 && lineIndex < mapLines.Count);
				}
				else
					value = "Error: Map section does not exist.";
			}
		}
		else
			value = "Error: Invalid map name";

		return value;
	}

}
