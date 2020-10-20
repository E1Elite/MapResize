using System;
using System.Collections.Generic;

public class Structure : XYElement
{
	public string Owner { get; set; }
	public string StructureID  { get; set; }
	public int Health { get; set; }
	public int Facing { get; set; }
	public string Tag { get; set; }
	public int Sellable { get; set; }
	public int Rebuildable { get; set; }
	public int PoweredOn { get; set; }
	public int UpgradesCount { get; set; }
	public int Spotlight { get; set; }
	public string Upgrade1 { get; set; }
	public string Upgrade2 { get; set; }
	public string Upgrade3 { get; set; }
	public int Repairable { get; set; }
	public int Nominal { get; set; }

	public Structure(string owner, string structureID, int health, int x, int y, int facing, string tag, int sellable, int rebuildable, int poweredOn,
		int upgradesCount, int spotlight, string upgrade1, string upgrade2, string upgrade3, int repairable, int nominal) : base(x, y)
	{
		Owner = owner;
		StructureID = structureID;
		Health = health;
		Facing = facing;
		Tag = tag;
		Sellable = sellable;
		Rebuildable = rebuildable;
		PoweredOn = poweredOn;
		UpgradesCount = upgradesCount;
		Spotlight = spotlight;
		Upgrade1 = upgrade1;
		Upgrade2 = upgrade2;
		Upgrade3 = upgrade3;
		Repairable = repairable;
		Nominal = nominal;
	}

	public override string ToString()
	{
		return Owner + "," + StructureID + "," + Health + "," + MapX + "," + MapY + "," + Facing + "," + Tag + "," + Sellable + "," + Rebuildable + "," +
			PoweredOn + "," + UpgradesCount + "," + Spotlight + "," + Upgrade1 + "," + Upgrade2 + "," + Upgrade3 + "," + Repairable + "," + Nominal;
	}

}

// INDEX=OWNER,ID,HEALTH,X,Y,FACING,TAG,AI_SELLABLE,AI_REBUILDABLE,POWERED_ON,UPGRADES,SPOTLIGHT,UPGRADE_1,UPGRADE_2,UPGRADE_3,AI_REPAIRABLE,NOMINAL
