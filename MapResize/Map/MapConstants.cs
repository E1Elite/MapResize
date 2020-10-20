using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

public static class MapConstants
{
	// Used in parsing, these can't have duplicates in either key or value. 
	public static readonly HashSet<string> TypesList = new HashSet<string>()
	{
		"InfantryTypes",
		"VehicleTypes",
		"AircraftTypes",
		"BuildingTypes",
		"TerrainTypes",
		"SmudgeTypes",
		"OverlayTypes",
		"Houses",
		"Countries",
		"Animations",
		"VoxelAnims",
		"Particles",
		"ParticleSystems",
		"SuperWeaponTypes",
		"Warheads",
		"TaskForces",
		"ScriptTypes",
		"TeamTypes",
	};

}

