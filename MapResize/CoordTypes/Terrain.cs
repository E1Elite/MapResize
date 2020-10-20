using System;
using System.Collections.Generic;

public class Terrain : XYElement
{
	public string TerrainType { get; set; }

	public Terrain(string terrainType, int x, int y) : base(x, y)
	{
		TerrainType = terrainType;
	}

	public override string ToString()
	{
		return TerrainType;
	}

}
