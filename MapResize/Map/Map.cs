using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Text;
using System.Linq;
using NLog;
using CNCMaps.FileFormats.Encodings;
using CNCMaps.FileFormats.VirtualFileSystem;

public class Map
{
	static Logger log = LogManager.GetCurrentClassLogger();

	public List<MapSection> MapSections { get; set; }

	private List<Aircraft> aircrafts = new List<Aircraft>();
	private List<Infantry> infantries = new List<Infantry>();
	private List<Unit> units = new List<Unit>();
	private List<Structure> structures = new List<Structure>();

	private List<CellTag> cellTags = new List<CellTag>();
	private List<Smudge> smudges = new List<Smudge>();
	private List<Terrain> terrains = new List<Terrain>();
	private List<TunnelLine> tunnelLines = new List<TunnelLine>();
	private List<Waypoint> waypoints = new List<Waypoint>();

	private List<Overlay> overlays = new List<Overlay>();
	private List<Tile> tiles = new List<Tile>();

	private List<BaseNode> baseNodes = new List<BaseNode>();
	private List<string> sectionsWithNodes = new List<string>();

	private int MapWidth;
	private int MapHeight;
    private int[] localSize = new int[6];
	private HashSet<Tuple<int, int>> coordList = new HashSet<Tuple<int, int>>();

	private Options options = new Options();

	private int left;
	private int right;
	private int top;
	private int bottom;
	private int newMapWidth;
	private int newMapHeight;

	public Map() { }

	public bool Initialize(string filename, Options opts)
	{
		bool result = false;
		MapParser parser = new MapParser();
		options = opts;
		MapSections = parser.ParseMap(filename);
		
		if (FetchMapDimension())
		{
			InitializeXY();
			result = true;
		}
		return result;
	}

	private bool FetchMapDimension()
	{
		bool result = false;
		MapSection map = GetSection("Map");
		if (map != null && !String.IsNullOrEmpty(map.Entries["Size"]) && !String.IsNullOrEmpty(map.Entries["LocalSize"]))
		{
			string[] parts = map.Entries["Size"].Split(',');
			int x, y, width, height = -1;
			if (parts.Length >= 4 && !String.IsNullOrEmpty(parts[0]) && !String.IsNullOrEmpty(parts[1]) && 
				!String.IsNullOrEmpty(parts[2]) && !String.IsNullOrEmpty(parts[3]))
			{
				int.TryParse(parts[0], out x);
				int.TryParse(parts[1], out y);
				int.TryParse(parts[2], out width);
				int.TryParse(parts[3], out height);
				if (x != 0 || y != 0)
					log.Error("Map size does not start at 0,0.");
				else if (width < 1 || width > 511 || height < 1 || height > 511)
					log.Error("Map size value(s) invalid. Width = " + width + " Height = " + height);
				else
				{
					MapWidth = width;
					MapHeight = height;
					log.Info("Map size (original): " + width + "x" + height);
					string[] localParts = map.Entries["LocalSize"].Split(',');
					if (localParts.Length >= 4 && !String.IsNullOrEmpty(localParts[0]) && !String.IsNullOrEmpty(localParts[1]) && 
						!String.IsNullOrEmpty(localParts[2]) && !String.IsNullOrEmpty(localParts[3]))
					{
						int.TryParse(localParts[0], out localSize[0]);
						int.TryParse(localParts[1], out localSize[1]);
						int.TryParse(localParts[2], out localSize[2]);
						int.TryParse(localParts[3], out localSize[3]);
						localSize[4] = width - localSize[2];
						localSize[5] = height - localSize[3];
					}
					for (int j = 0; j < height; j++)
					{
						for (int i = 0; i <= width * 2 - 2; i++)
						{
							int dx = i;
							int dy = (j * 2 + i % 2);
							int rx = ((dx + dy) / 2 + 1);
							int ry = (dy - rx + width + 1);
							coordList.Add(Tuple.Create(rx, ry));
						}
					}
					result = true;
				}
			}
		}
		return result;
	}

	private MapSection GetSection(string sectionName)
	{
		MapSection mapSection = null;
        if (MapSections != null && !String.IsNullOrEmpty(sectionName))
            try
            {
                mapSection = MapSections.Find(section => section.Name.Equals(sectionName));
            }
            catch (Exception)
            {
                log.Info("Section not found: [" + sectionName + "]");
            }
		return mapSection;
	}

	private void InitializeXY()
	{
		InitializeAircrafts();
		InitializeInfantries();
		InitializeUnits();
		InitializeStructures();

		InitializeCellTags();
		InitializeSmudges();
		InitializeTerrains();
		InitializeTunnelLines();
		InitializeWaypoints();

		InitializeOverlays();
		InitializeTiles();

		InitializeBaseNodes();
	}

	private void InitializeAircrafts()
	{
		MapSection section = GetSection("Aircraft");
		if (section != null)
		{
			aircrafts = new List<Aircraft>();
			foreach (KeyValuePair<string, string> entry in section.Entries)
			{
				string[] parts = entry.Value.Split(',');
				if (parts.Length >= 12)
				{
					try
					{
						string owner = parts[0].Trim();
						string aircraftID = parts[1].Trim();
						int health = int.Parse(parts[2], NumberStyles.Integer);
						int x = int.Parse(parts[3], NumberStyles.Integer);
						int y = int.Parse(parts[4], NumberStyles.Integer);
						int facing = int.Parse(parts[5], NumberStyles.Integer);
						string mission = parts[6].Trim();
						string tag = parts[7].Trim();
						int veterancy = int.Parse(parts[8], NumberStyles.Integer);
						int group = int.Parse(parts[9], NumberStyles.Integer);
						int acNoRecruitable = int.Parse(parts[10], NumberStyles.Integer);
						int acYesRecruitable = int.Parse(parts[11], NumberStyles.Integer);
						
						aircrafts.Add(new Aircraft(owner, aircraftID, health, x, y, facing, mission, tag, veterancy,
							group, acNoRecruitable, acYesRecruitable));
					}
					catch (Exception)
					{
						log.Error("Error reading Aircraft entry with index: " + entry.Key);
					}
				}
				else
					log.Error("Error reading Aircraft entry having less parameters with index: " + entry.Key);
			}
		}
	}

	private void InitializeInfantries()
	{
		MapSection section = GetSection("Infantry");
		if (section != null)
		{
			infantries = new List<Infantry>();
			foreach (KeyValuePair<string, string> entry in section.Entries)
			{
				string[] parts = entry.Value.Split(',');
				if (parts.Length >= 14)
				{
					try
					{
						string owner = parts[0].Trim();
						string infantryID = parts[1].Trim();
						int health = int.Parse(parts[2], NumberStyles.Integer);
						int x = int.Parse(parts[3], NumberStyles.Integer);
						int y = int.Parse(parts[4], NumberStyles.Integer);
						int subcell = int.Parse(parts[5], NumberStyles.Integer);
						string mission = parts[6].Trim();
						int facing = int.Parse(parts[7], NumberStyles.Integer);
						string tag = parts[8].Trim();
						int veterancy = int.Parse(parts[9], NumberStyles.Integer);
						int group = int.Parse(parts[10], NumberStyles.Integer);
						int highOnBridge = int.Parse(parts[11], NumberStyles.Integer);
						int acNoRecruitable = int.Parse(parts[12], NumberStyles.Integer);
						int acYesRecruitable = int.Parse(parts[13], NumberStyles.Integer);
						
						infantries.Add(new Infantry(owner, infantryID, health, x, y, subcell, mission, facing, tag, veterancy, group,
							highOnBridge, acNoRecruitable, acYesRecruitable));
					}
					catch (Exception)
					{
						log.Error("Error reading Infantry entry with index: " + entry.Key);
					}
				}
				else
					log.Error("Error reading Infantry entry having less parameters with index: " + entry.Key);
			}
		}
	}

	private void InitializeUnits()
	{
		MapSection section = GetSection("Units");
		if (section != null)
		{
			units = new List<Unit>();
			foreach (KeyValuePair<string, string> entry in section.Entries)
			{
				string[] parts = entry.Value.Split(',');
				if (parts.Length >= 14)
				{
					try
					{
						string index = entry.Key;
						string owner = parts[0].Trim();
						string unitID = parts[1].Trim();
						int health = int.Parse(parts[2], NumberStyles.Integer);
						int x = int.Parse(parts[3], NumberStyles.Integer);
						int y = int.Parse(parts[4], NumberStyles.Integer);
						int facing = int.Parse(parts[5], NumberStyles.Integer);
						string mission = parts[6].Trim();
						string tag = parts[7].Trim();
						int veterancy = int.Parse(parts[8], NumberStyles.Integer);
						int group = int.Parse(parts[9], NumberStyles.Integer);
						int highOnBridge = int.Parse(parts[10], NumberStyles.Integer);
						string followerIndex =  parts[11].Trim();
						int acNoRecruitable = int.Parse(parts[12], NumberStyles.Integer);
						int acYesRecruitable = int.Parse(parts[13], NumberStyles.Integer);
						
						units.Add(new Unit(index, owner, unitID, health, x, y, facing, mission, tag, veterancy, group,
							highOnBridge, followerIndex, acNoRecruitable, acYesRecruitable));
					}
					catch (Exception)
					{
						log.Error("Error reading Unit entry with index: " + entry.Key);
					}
				}
				else
					log.Error("Error reading Unit entry having less parameters with index: " + entry.Key);
			}
		}
	}

	private void InitializeStructures()
	{
		MapSection section = GetSection("Structures");
		if (section != null)
		{
			structures = new List<Structure>();
			foreach (KeyValuePair<string, string> entry in section.Entries)
			{
				string[] parts = entry.Value.Split(',');
				if (parts.Length >= 17)
				{
					try
					{
						string owner = parts[0].Trim();
						string structureID = parts[1].Trim();
						int health = int.Parse(parts[2], NumberStyles.Integer);
						int x = int.Parse(parts[3], NumberStyles.Integer);
						int y = int.Parse(parts[4], NumberStyles.Integer);
						int facing = int.Parse(parts[5], NumberStyles.Integer);
						string tag = parts[6].Trim();
						int sellable = int.Parse(parts[7], NumberStyles.Integer);
						int rebuildable = int.Parse(parts[8], NumberStyles.Integer);
						int poweredOn = int.Parse(parts[9], NumberStyles.Integer);
						int upgradesCount = int.Parse(parts[10], NumberStyles.Integer);
						int spotlight = int.Parse(parts[11], NumberStyles.Integer);
						string upgrade1 = parts[12].Trim();
						string upgrade2 = parts[13].Trim();
						string upgrade3 = parts[14].Trim();
						int repairable = int.Parse(parts[15], NumberStyles.Integer);
						int nominal = int.Parse(parts[16], NumberStyles.Integer);
						
						structures.Add(new Structure(owner, structureID, health, x, y, facing, tag, sellable, rebuildable, poweredOn,
							upgradesCount, spotlight, upgrade1, upgrade2, upgrade3, repairable, nominal));
					}
					catch (Exception)
					{
						log.Error("Error reading Structures entry with index: " + entry.Key);
					}
				}
				else
					log.Error("Error reading Structures entry having less parameters with index: " + entry.Key);
			}
		}
	}

	private void InitializeCellTags()
	{
		MapSection section = GetSection("CellTags");
		if (section != null)
		{
			cellTags = new List<CellTag>();
			foreach (KeyValuePair<string, string> entry in section.Entries)
			{
				try
				{
					int coord =  int.Parse(entry.Key, NumberStyles.Integer);
					int x = coord % 1000;
					int y = coord / 1000;
	
					cellTags.Add(new CellTag(entry.Value, x, y));
				}
				catch (Exception)
				{
					log.Error("Error reading CellTags entry with index: " + entry.Key);
				}
			}
		}
	}

	private void InitializeSmudges()
	{
		MapSection section = GetSection("Smudge");
		if (section != null)
		{
			smudges = new List<Smudge>();
			foreach (KeyValuePair<string, string> entry in section.Entries)
			{
				string[] parts = entry.Value.Split(',');
				if (parts.Length >= 4)
				{
					try
					{
						string smudgeType = parts[0].Trim();
						int x = int.Parse(parts[1], NumberStyles.Integer);
						int y = int.Parse(parts[2], NumberStyles.Integer);
						int ignore = int.Parse(parts[3], NumberStyles.Integer);
						
						smudges.Add(new Smudge(smudgeType, x, y, ignore));
					}
					catch (Exception)
					{
						log.Error("Error reading Smudge entry with index: " + entry.Key);
					}
				}
				else
					log.Error("Error reading Smudge entry having less parameters with index: " + entry.Key);
			}
		}
	}

	private void InitializeTerrains()
	{
		MapSection section = GetSection("Terrain");
		if (section != null)
		{
			terrains = new List<Terrain>();
			foreach (KeyValuePair<string, string> entry in section.Entries)
			{
				try
				{
					int coord =  int.Parse(entry.Key, NumberStyles.Integer);
					int x = coord % 1000;
					int y = coord / 1000;
	
					terrains.Add(new Terrain(entry.Value, x, y));
				}
				catch (Exception)
				{
					log.Error("Error reading Terrain entry with index: " + entry.Key);
				}
			}
		}
	}

	private void InitializeTunnelLines()
	{
		MapSection section = GetSection("Tubes");
		if (section != null)
		{
			tunnelLines = new List<TunnelLine>();
			foreach (KeyValuePair<string, string> entry in section.Entries)
			{
				string[] parts = entry.Value.Split(',');
				if (parts.Length > 5)
				{
					try
					{
						int sx = int.Parse(parts[0], NumberStyles.Integer);
						int sy = int.Parse(parts[1], NumberStyles.Integer);
						int facing = int.Parse(parts[2], NumberStyles.Integer);
						int ex = int.Parse(parts[3], NumberStyles.Integer);
						int ey = int.Parse(parts[4], NumberStyles.Integer);
		
						List<int> ds = new List<int>();
						for (int i =5; i < parts.Length; i++)
							ds.Add(int.Parse(parts[i], NumberStyles.Integer));

						tunnelLines.Add(new TunnelLine(sx, sy, facing, ex, ey, ds));
					}
					catch (Exception)
					{
						log.Error("Error reading Tubes (Tunnel) entry with index: " + entry.Key);
					}
				}
			}
		}
	}

	private void InitializeWaypoints()
	{
		MapSection section = GetSection("Waypoints");
		if (section != null)
		{
			waypoints = new List<Waypoint>();
			foreach (KeyValuePair<string, string> entry in section.Entries)
			{
				try
				{
					int number = int.Parse(entry.Key, NumberStyles.Integer);
					int coord = int.Parse(entry.Value, NumberStyles.Integer);
					int x = coord % 1000;
					int y = coord / 1000;
	
					waypoints.Add(new Waypoint(number, x, y));
				}
				catch (Exception)
				{
					log.Error("Error reading Waypoint entry with index: " + entry.Key);
				}
			}
		}
	}

	private void InitializeOverlays()
	{
		MapSection overlaySection = GetSection("OverlayPack");
		MapSection overlayDataSection = GetSection("OverlayDataPack");

		if (overlaySection != null && overlayDataSection != null)
		{
			overlays = new List<Overlay>();

			StringBuilder sb = new StringBuilder();
			foreach (KeyValuePair<string, string> entry in overlaySection.Entries)
				sb.Append(entry.Value);
			byte[] format80Data = Convert.FromBase64String(sb.ToString());
			byte[] overlayPack = new byte[1 << 18];
			Format5.DecodeInto(format80Data, overlayPack, 80);

			sb.Clear();
			foreach (KeyValuePair<string, string> entry in overlayDataSection.Entries)
				sb.Append(entry.Value);
			format80Data = Convert.FromBase64String(sb.ToString());
			byte[] overlayDataPack = new byte[1 << 18];
			Format5.DecodeInto(format80Data, overlayDataPack, 80);

			foreach (Tuple<int, int> cellCoordinate in coordList)
			{
				int location = cellCoordinate.Item1 + 512 * cellCoordinate.Item2;
				byte overlayIndex = overlayPack[location];
				if (overlayIndex != 0xFF)
				{
					overlays.Add(new Overlay(overlayIndex, (int)overlayDataPack[location], cellCoordinate.Item1, cellCoordinate.Item2));
				}
			}
		}
	}

	private void InitializeTiles()
	{
		MapSection section = GetSection("IsoMapPack5");
		if (section != null)
		{
			tiles = new List<Tile>();
			StringBuilder sb = new StringBuilder();
			foreach (KeyValuePair<string, string> entry in section.Entries)
				sb.Append(entry.Value);
			byte[] lzoData = Convert.FromBase64String(sb.ToString());
			int cells = (MapWidth * 2 - 1) * MapHeight;
			int lzoPackSize = cells * 11 + 4; // last 4 bytes used for termination
			byte[] isoMapPack = new byte[lzoPackSize];

			int j = 0;
			for (int i = 0; i < cells; i++) 
			{
				isoMapPack[j] = 0x88;
				isoMapPack[j + 1] = 0x40;
				isoMapPack[j + 2] = 0x88;
				isoMapPack[j + 3] = 0x40;
				j += 11;
			}

			Format5.DecodeInto(lzoData, isoMapPack);

			foreach (Tuple<int, int> cellCoordinate in coordList)
				tiles.Add(new Tile(0, 0, 0, 0, cellCoordinate.Item1, cellCoordinate.Item2));

			MemoryFile mf = new MemoryFile(isoMapPack);
			for (int i = 0; i < cells; i++) 
			{
				int rx = (int)mf.ReadUInt16();
				int ry = (int)mf.ReadUInt16();
				int tileIndex = mf.ReadInt32();
				byte subIndex = mf.ReadByte();
				byte heightLevel = mf.ReadByte();
				byte iceGrowth = mf.ReadByte();

				if (tileIndex >= 65535) tileIndex = 0; // Tile 0xFFFF used as empty/clear

				if (rx < 512 && ry < 512)
				{
					int index = tiles.FindIndex(tile => tile.MapX == rx && tile.MapY == ry);
					if (index >= 0)
					{
						tiles[index].TileIndex = tileIndex;
						tiles[index].SubIndex = subIndex;
						tiles[index].Level = heightLevel;
						tiles[index].IceGrowth = iceGrowth;
					}
				}
			}
		}
	}

	private void InitializeBaseNodes()
	{
		foreach (MapSection section in MapSections)
		{
			string nodeCount;
			if (section.Entries.TryGetValue("NodeCount", out nodeCount))
			{
				int count = 0;
				int.TryParse(nodeCount, out count);
				if (count > 0)
				{
					sectionsWithNodes.Add(section.Name);
					for (int i=0; i < count; i++)
					{
						string countStr = i.ToString("000");
						if (section.Entries.ContainsKey(countStr))
						{
							string[] parts = section.Entries[countStr].Split(',');
							if (parts.Length > 2)
							{
								try
								{
									string structID = parts[0].Trim();
									int x = int.Parse(parts[1], NumberStyles.Integer);
									int y = int.Parse(parts[2], NumberStyles.Integer);

									baseNodes.Add(new BaseNode(section.Name, structID, x, y));
								}
								catch (Exception)
								{
									log.Error("Error reading BaseNode entry in [" + section.Name + "] at " + countStr);
								}
							}
						}
					}
				}
			}
		}
	}

	public void Resize(int topValue, int rightValue, int bottomValue, int leftValue)
	{
		left = leftValue;
		right = rightValue;
		top = topValue;
		bottom = bottomValue;

		newMapWidth = MapWidth + left + right;
		newMapHeight = MapHeight + top + bottom;

		log.Info("Expected new map size: " + newMapWidth + "x" + newMapHeight);
		coordList.Clear();
		for (int j = 0; j < newMapHeight; j++)
		{
			for (int i = 0; i <= newMapWidth * 2 - 2; i++)
			{
				int dx = i;
				int dy = (j * 2 + i % 2);
				int rx = ((dx + dy) / 2 + 1);
				int ry = (dy - rx + newMapWidth + 1);
				coordList.Add(Tuple.Create(rx, ry));
			}
		}
		UpdateXYSections();
		UpdateMapSections();
	}

	private void UpdateXYSections()
	{
		List<Aircraft> aircraftsNew = new List<Aircraft>();
		List<Infantry> infantriesNew = new List<Infantry>();
		List<Unit> unitsNew = new List<Unit>();
		List<Structure> structuresNew = new List<Structure>();
		List<CellTag> cellTagsNew = new List<CellTag>();
		List<Smudge> smudgesNew = new List<Smudge>();
		List<Terrain> terrainsNew = new List<Terrain>();
		List<TunnelLine> tunnelLinesNew = new List<TunnelLine>();
		List<Waypoint> waypointsNew = new List<Waypoint>();
		List<Overlay> overlaysNew = new List<Overlay>();
		List<Tile> tilesNew = new List<Tile>();
		List<BaseNode> baseNodesNew = new List<BaseNode>();
		HashSet<string> unitIndices = new HashSet<string>();

		foreach (Aircraft aircraft in aircrafts)
		{
			aircraft.UpdateXY(left, top, right, bottom);
			if (coordList.Contains(Tuple.Create(aircraft.MapX, aircraft.MapY)))
				aircraftsNew.Add(aircraft);
		}

		foreach (Infantry infantry in infantries)
		{
			infantry.UpdateXY(left, top, right, bottom);
			if (coordList.Contains(Tuple.Create(infantry.MapX, infantry.MapY)))
				infantriesNew.Add(infantry);
		}

		foreach (Unit unit in units)
		{
			unit.UpdateXY(left, top, right, bottom);
			if (coordList.Contains(Tuple.Create(unit.MapX, unit.MapY)))
			{
				unitsNew.Add(unit);
				unitIndices.Add(unit.Index);
			}
		}

		if (unitIndices.Count > 0)
		{
			foreach (Unit unit in units)
			{
				if (!unitIndices.Contains(unit.FollowerIndex))
					unit.FollowerIndex = "-1";
			}
		}

		foreach (Structure structure in structures)
		{
			structure.UpdateXY(left, top, right, bottom);
			if (coordList.Contains(Tuple.Create(structure.MapX, structure.MapY)))
				structuresNew.Add(structure);
		}

		foreach (CellTag cellTag in cellTags)
		{
			cellTag.UpdateXY(left, top, right, bottom);
			if (coordList.Contains(Tuple.Create(cellTag.MapX, cellTag.MapY)))
				cellTagsNew.Add(cellTag);
		}

		foreach (Smudge smudge in smudges)
		{
			smudge.UpdateXY(left, top, right, bottom);
			if (coordList.Contains(Tuple.Create(smudge.MapX, smudge.MapY)))
				smudgesNew.Add(smudge);
		}

		foreach (Terrain terrain in terrains)
		{
			terrain.UpdateXY(left, top, right, bottom);
			if (coordList.Contains(Tuple.Create(terrain.MapX, terrain.MapY)))
				terrainsNew.Add(terrain);
		}

		foreach (TunnelLine tunnelLine in tunnelLines)
		{
			tunnelLine.UpdateXY(left, top, right, bottom, true);
			if (coordList.Contains(Tuple.Create(tunnelLine.MapX, tunnelLine.MapY)) &&
				coordList.Contains(Tuple.Create(tunnelLine.MapX2, tunnelLine.MapY2)))
				tunnelLinesNew.Add(tunnelLine);
		}

		List<int> removedWaypoints = new List<int>();
		foreach (Waypoint waypoint in waypoints)
		{
			waypoint.UpdateXY(left, top, right, bottom);
			if (coordList.Contains(Tuple.Create(waypoint.MapX, waypoint.MapY)))
				waypointsNew.Add(waypoint);
			else
				removedWaypoints.Add(waypoint.Number);
		}
		if (!options.RemOutsideWaypoints)
		{
			List<Tuple<int, int>> coordinatesList = coordList.OrderByDescending(item => item.Item1 + item.Item2).ToList();
			int bottomCellIndex = 0;
			foreach (Waypoint waypoint in waypoints)
			{
				if (!coordList.Contains(Tuple.Create(waypoint.MapX, waypoint.MapY)))
				{
					while (waypointsNew.FindIndex(wp => wp.MapX == coordinatesList[bottomCellIndex].Item1 &&
						wp.MapY == coordinatesList[bottomCellIndex].Item2) >= 0)
						bottomCellIndex++;
					
					waypoint.MapX = coordinatesList[bottomCellIndex].Item1;
					waypoint.MapY = coordinatesList[bottomCellIndex].Item2;
					bottomCellIndex++;
					waypointsNew.Add(waypoint);
				}
			}
		}
		else
		{
			if (removedWaypoints != null && removedWaypoints.Count > 0)
			{
				foreach (int rwp in  removedWaypoints)
					log.Info("Waypoint removed: " + rwp);
			}
		}

		foreach (Overlay overlay in overlays)
		{
			overlay.UpdateXY(left, top, right, bottom);
			if (coordList.Contains(Tuple.Create(overlay.MapX, overlay.MapY)))
				overlaysNew.Add(overlay);
		}

		foreach (Tile tile in tiles)
		{
			tile.UpdateXY(left, top, right, bottom);
			if (coordList.Contains(Tuple.Create(tile.MapX, tile.MapY)))
				tilesNew.Add(tile);
		}

		foreach (BaseNode baseNode in baseNodes)
		{
			baseNode.UpdateXY(left, top, right, bottom);
			if (coordList.Contains(Tuple.Create(baseNode.MapX, baseNode.MapY)))
				baseNodesNew.Add(baseNode);
		}

		aircrafts.Clear();
		aircrafts.AddRange(aircraftsNew);

		infantries.Clear();
		infantries.AddRange(infantriesNew);

		units.Clear();
		units.AddRange(unitsNew);

		structures.Clear();
		structures.AddRange(structuresNew);

		cellTags.Clear();
		cellTags.AddRange(cellTagsNew);

		smudges.Clear();
		smudges.AddRange(smudgesNew);

		terrains.Clear();
		terrains.AddRange(terrainsNew);

		tunnelLines.Clear();
		tunnelLines.AddRange(tunnelLinesNew);

		waypoints.Clear();
		waypoints.AddRange(waypointsNew.OrderBy(w => w.Number).ToList());

		overlays.Clear();
		overlays.AddRange(overlaysNew);

		tiles.Clear();
		tiles.AddRange(tilesNew);

		baseNodes.Clear();
		baseNodes.AddRange(baseNodesNew);
	}

	public void UpdateMapSections()
	{
		int index = 0;
		MapSection section = GetSection("Aircraft");
		if (section != null)
		{
			section.Entries.Clear();
			if (aircrafts != null && aircrafts.Count > 0)
			{
				foreach (Aircraft entry in aircrafts)
					section.Entries.Add(Convert.ToString(index++), entry.ToString());
			}
		}

		index = 0;
		section = GetSection("Infantry");
		if (section != null)
		{
			section.Entries.Clear();
			if (infantries != null && infantries.Count > 0)
			{
				foreach (Infantry entry in infantries)
					section.Entries.Add(Convert.ToString(index++), entry.ToString());
			}
		}

		section = GetSection("Units");
		if (section != null)
		{
			section.Entries.Clear();
			if (units != null && units.Count > 0)
			{
				foreach (Unit entry in units)
					section.Entries.Add(entry.Index, entry.ToString());
			}
		}

		index = 0;
		section = GetSection("Structures");
		if (section != null)
		{
			section.Entries.Clear();
			if (structures != null && structures.Count > 0)
			{
				foreach (Structure entry in structures)
					section.Entries.Add(Convert.ToString(index++), entry.ToString());
			}
		}

		section = GetSection("CellTags");
		if (section != null)
		{
			section.Entries.Clear();
			if (cellTags != null && cellTags.Count > 0)
			{
				foreach (CellTag entry in cellTags)
					section.Entries.Add(Convert.ToString(entry.MapY * 1000 + entry.MapX), entry.ToString());
			}
		}

		index = 0;
		section = GetSection("Smudge");
		if (section != null)
		{
			section.Entries.Clear();
			if (smudges != null && smudges.Count > 0)
			{
				foreach (Smudge entry in smudges)
					section.Entries.Add(Convert.ToString(index++), entry.ToString());
			}
		}

		section = GetSection("Terrain");
		if (section != null)
		{
			section.Entries.Clear();
			if (terrains != null && terrains.Count > 0)
			{
				foreach (Terrain entry in terrains)
					section.Entries.Add(Convert.ToString(entry.MapY * 1000 + entry.MapX), entry.ToString());
			}
		}

		index = 0;
		section = GetSection("Tubes");
		if (section != null)
		{
			section.Entries.Clear();
			if (tunnelLines != null && tunnelLines.Count > 0)
			{
				foreach (TunnelLine entry in tunnelLines)
					section.Entries.Add(Convert.ToString(index++), entry.ToString());
			}
		}

		section = GetSection("Waypoints");
		if (section != null)
		{
			section.Entries.Clear();
			if (waypoints != null && waypoints.Count > 0)
			{
				foreach (Waypoint entry in waypoints)
					section.Entries.Add(entry.ToString(), Convert.ToString(entry.MapY * 1000 + entry.MapX));
			}
		}

		UpdateOverlaySections();
		UpdateTileSection();

		if (sectionsWithNodes != null && sectionsWithNodes.Count > 0)
		{
			foreach (string sectionName in sectionsWithNodes)
			{
				section = GetSection(sectionName);
				if (section != null)
				{
					string nodeCount;
					if (section.Entries.TryGetValue("NodeCount", out nodeCount))
					{
						int count = 0;
						int.TryParse(nodeCount, out count);
						if (count > 0)
						{
							for (int i=0; i < count; i++)
							{
								string countStr = i.ToString("000");
								if (section.Entries.ContainsKey(countStr))
									section.Entries.Remove(countStr);
							}
						}

						List<BaseNode> baseNodesInSection = baseNodes.FindAll(bn => bn.SectionName.Equals(sectionName));
						Dictionary<string, string> sectionEntries = new Dictionary<string, string>();
						foreach (KeyValuePair<string, string> entry in section.Entries)
							sectionEntries.Add(entry.Key, entry.Value);
						nodeCount = "0"; 
						if (baseNodesInSection != null && baseNodesInSection.Count > 0)
						{
							for (int bnCount = 0; bnCount < baseNodesInSection.Count; bnCount++)
							{
								sectionEntries.Add(bnCount.ToString("000"), baseNodesInSection[bnCount].ToString());
							}
							nodeCount = baseNodesInSection.Count.ToString();
						}
						if (sectionEntries.ContainsKey("NodeCount"))
							sectionEntries["NodeCount"] = nodeCount;
						else
							sectionEntries.Add("NodeCount", nodeCount);
						section.Entries = sectionEntries;
					}
				}
			}
		}

		Dictionary<string, string> sizeEntries = new Dictionary<string, string>();
		section = GetSection("Map");
		if (section != null)
		{
			int localX = localSize[0];
			int localY = localSize[1];
			int localWidth = newMapWidth - localSize[4];
			int localHeight = newMapHeight - localSize[5];

			sizeEntries.Add("Size", "0,0," + newMapWidth + "," + newMapHeight);
			if (left >= 0 && top >= 0 && right >= 0 && bottom >= 0 && options.MaintainLocalSize)
			{
				localX = localSize[0] + left;
				localY = localSize[1] + top;
				localWidth = newMapWidth - localSize[4] - right - left;
				localHeight = newMapHeight - localSize[5] - bottom - top;
			}

			if (localX < 1) localX = 1;
			if (localY < 2) localY = 2;
			if (localWidth < 1) localWidth = 1;
			if (localHeight < 2) localHeight = 2;

			string mapLocalSize = Convert.ToString(localX) + "," + Convert.ToString(localY) + "," +
				Convert.ToString(localWidth) + "," + Convert.ToString(localHeight);
			sizeEntries.Add("LocalSize", mapLocalSize);

			section.Merge(sizeEntries);
		}
	}

	private void UpdateOverlaySections()
	{
		MapSection overlaySection = GetSection("OverlayPack");
		MapSection overlayDataSection = GetSection("OverlayDataPack");
		if (overlaySection != null && overlayDataSection != null)
		{
			byte[] overlayPack = new byte[1 << 18];
			byte[] overlayDataPack = new byte[1 << 18];

			for (int i = 0; i < 262144; i++) 
			{
				overlayPack[i] = 0xFF;
				overlayDataPack[i] = 0;
			}

			if (overlays != null && overlays.Count > 0)
			{
				foreach (Overlay entry in overlays)
				{
					int location = (entry.MapY * 512) + entry.MapX;
					overlayPack[location] = (byte)entry.OverlayIndex;
					overlayDataPack[location] = (byte)entry.OverlayFrame;
				}
			}

			string oPackEncoded = Convert.ToBase64String(Format5.Encode(overlayPack, 80), Base64FormattingOptions.None);
			string oDataPackEncoded = Convert.ToBase64String(Format5.Encode(overlayDataPack, 80), Base64FormattingOptions.None);

			overlaySection.Entries.Clear();
			overlayDataSection.Entries.Clear();

			for (int i = 0, j = 1; i < oPackEncoded.Length; i += 70)
				overlaySection.Entries.Add(j++.ToString(CultureInfo.InvariantCulture), oPackEncoded.Substring(i, Math.Min(70, oPackEncoded.Length - i)));

			for (int i = 0, j = 1; i < oDataPackEncoded.Length; i += 70) 
				overlayDataSection.Entries.Add(j++.ToString(CultureInfo.InvariantCulture), oDataPackEncoded.Substring(i, Math.Min(70, oDataPackEncoded.Length - i)));
		}
	}

	private void UpdateTileSection()
	{
		MapSection tileSection = GetSection("IsoMapPack5");
		if (tileSection != null)
		{
			byte[] encoded;
			List<Tile> tileSetStage = new List<Tile>();
			List<byte[]> sortedTiles = new List<byte[]>();

			if (!options.KeepL0ClearTiles)
			{
				foreach (Tile t in tiles)
				{
					if (t.TileIndex > 0 || t.SubIndex > 0 || t.Level > 0 || t.IceGrowth > 0)
						tileSetStage.Add(t);
				}
			}
			else
				tileSetStage.AddRange(tiles);

			if (tileSetStage.Count == 0)
			{
				tileSetStage.Add(tiles.First());
				encoded = GetEncoded(tileSetStage);
			}
			else
			{
				sortedTiles.Add(GetEncoded(tileSetStage.OrderBy(x => x.MapX).ThenBy(x => x.TileIndex).ThenBy(x => x.SubIndex).ThenBy(x => x.Level).ToList()));
				sortedTiles.Add(GetEncoded(tileSetStage.OrderBy(x => x.TileIndex).ThenBy(x => x.MapX).ThenBy(x => x.SubIndex).ThenBy(x => x.Level).ToList()));

				if (options.BetterTilesPackCompression)
				{
					sortedTiles.Add(GetEncoded(tileSetStage.OrderBy(x => x.MapX).ThenBy(x => x.SubIndex).ThenBy(x => x.TileIndex).ThenBy(x => x.Level).ToList()));
					sortedTiles.Add(GetEncoded(tileSetStage.OrderBy(x => x.SubIndex).ThenBy(x => x.TileIndex).ThenBy(x => x.MapX).ThenBy(x => x.Level).ToList()));
					sortedTiles.Add(GetEncoded(tileSetStage.OrderBy(x => x.SubIndex).ThenBy(x => x.TileIndex).ThenBy(x => x.Level).ThenBy(x => x.MapX).ToList()));
					sortedTiles.Add(GetEncoded(tileSetStage.OrderBy(x => x.SubIndex).ThenBy(x => x.TileIndex).ThenBy(x => x.Level).ThenBy(x => x.MapY).ToList()));
					sortedTiles.Add(GetEncoded(tileSetStage.OrderBy(x => x.SubIndex).ThenBy(x => x.Level).ThenBy(x => x.TileIndex).ThenBy(x => x.MapX).ToList()));
					sortedTiles.Add(GetEncoded(tileSetStage.OrderBy(x => x.SubIndex).ThenBy(x => x.Level).ThenBy(x => x.TileIndex).ThenBy(x => x.MapY).ToList()));
					sortedTiles.Add(GetEncoded(tileSetStage.OrderBy(x => x.TileIndex).ThenBy(x => x.SubIndex).ThenBy(x => x.MapY).ThenBy(x => x.Level).ToList()));
					sortedTiles.Add(GetEncoded(tileSetStage.OrderBy(x => x.TileIndex).ThenBy(x => x.SubIndex).ThenBy(x => x.Level).ThenBy(x => x.MapX).ToList()));
					sortedTiles.Add(GetEncoded(tileSetStage.OrderBy(x => x.TileIndex).ThenBy(x => x.SubIndex).ThenBy(x => x.Level).ThenBy(x => x.MapY).ToList()));
					sortedTiles.Add(GetEncoded(tileSetStage.OrderBy(x => x.TileIndex).ThenBy(x => x.Level).ThenBy(x => x.SubIndex).ThenBy(x => x.MapX).ToList()));
					sortedTiles.Add(GetEncoded(tileSetStage.OrderBy(x => x.TileIndex).ThenBy(x => x.Level).ThenBy(x => x.SubIndex).ThenBy(x => x.MapY).ToList()));
					sortedTiles.Add(GetEncoded(tileSetStage.OrderBy(x => x.Level).ThenBy(x => x.SubIndex).ThenBy(x => x.TileIndex).ThenBy(x => x.MapX).ToList()));
					sortedTiles.Add(GetEncoded(tileSetStage.OrderBy(x => x.Level).ThenBy(x => x.SubIndex).ThenBy(x => x.TileIndex).ThenBy(x => x.MapY).ToList()));
					sortedTiles.Add(GetEncoded(tileSetStage.OrderBy(x => x.Level).ThenBy(x => x.TileIndex).ThenBy(x => x.MapX).ThenBy(x => x.SubIndex).ToList()));
					sortedTiles.Add(GetEncoded(tileSetStage.OrderBy(x => x.Level).ThenBy(x => x.TileIndex).ThenBy(x => x.MapY).ThenBy(x => x.SubIndex).ToList()));
					sortedTiles.Add(GetEncoded(tileSetStage.OrderBy(x => x.Level).ThenBy(x => x.TileIndex).ThenBy(x => x.SubIndex).ThenBy(x => x.MapX).ToList()));
					sortedTiles.Add(GetEncoded(tileSetStage.OrderBy(x => x.Level).ThenBy(x => x.TileIndex).ThenBy(x => x.SubIndex).ThenBy(x => x.MapY).ToList()));
				}

				int smallest = sortedTiles[0].Length;
				int smallestIndex = 0;
				for (int index = 0; index < sortedTiles.Count; index++) {
					if (sortedTiles[index].Length < smallest) {
						smallest = sortedTiles[index].Length;
						smallestIndex = index;
					}
				}
				encoded = sortedTiles[smallestIndex];
			}

			string compressed64 = Convert.ToBase64String(encoded, Base64FormattingOptions.None);

			tileSection.Entries.Clear();
			for (int i = 0, j = 1; i < compressed64.Length; i += 74) 
				tileSection.Entries.Add(j++.ToString(CultureInfo.InvariantCulture), compressed64.Substring(i, Math.Min(74, compressed64.Length - i)));
		}
	}

	private byte[] GetEncoded(List<Tile> tileSet)
	{
		byte[] isoMapPack = new byte[tileSet.Count * 11 + 4];

		long byteIndex = 0;
		foreach (Tile tile in tileSet)
		{
			byte[] tileInBytes = tile.GetTileForMapPack();
			Array.Copy(tileInBytes, 0, isoMapPack, byteIndex, 11);
			byteIndex += 11;
		}

		return Format5.Encode(isoMapPack, 5);
	}

	public void Save(string filename)
	{
		if (!String.IsNullOrEmpty(filename) && MapSections != null && MapSections.Count > 0)
		{
            List<string> output = new List<string>();
            foreach (MapSection section in MapSections)
            {
                output.Add("[" + section.Name + "]");
				foreach (KeyValuePair<string, string> entry in section.Entries)
					output.Add(entry.Key + "=" + entry.Value);
				output.Add("");
			}
			try
			{
				File.WriteAllLines(filename, output.ToArray());
				log.Info("Saving map file: " + filename);
			}
			catch (Exception) 
			{ 
				log.Error("Failed to write file: " + filename);
			}
		}
	}
}

