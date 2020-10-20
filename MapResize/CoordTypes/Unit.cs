using System;
using System.Collections.Generic;
using System.Linq;

public class Unit : XYElement
{
	public string Index { get; set; }
	public string Owner { get; set; }
	public string UnitID  { get; set; }
	public int Health { get; set; }
	public int Facing { get; set; }
	public string Mission { get; set; }
	public string Tag { get; set; }
	public int Veterancy { get; set; }
	public int Group { get; set; }
	public int HighOnBridge { get; set; }
	public string FollowerIndex { get; set; }
	public int AutocreateNoRecruitable { get; set; }
	public int AutocreateYesRecruitable { get; set; }

	public Unit(string index, string owner, string unitID, int health, int x, int y, int facing, string mission, string tag, int veterancy, int group, 
		int highOnBridge, string followerIndex, int acNoRecruitable, int acYesRecruitable) : base(x, y)
	{
		Index = index;
		Owner = owner;
		UnitID = unitID;
		Health = health;
		Facing = facing;
		Mission = mission;
		Tag = tag;
		Veterancy = veterancy;
		Group = group;
		HighOnBridge = highOnBridge;
		FollowerIndex = followerIndex;
		AutocreateNoRecruitable = acNoRecruitable;
		AutocreateYesRecruitable = acYesRecruitable;
	}

	public override string ToString()
	{
		return Owner + "," + UnitID + "," + Health + "," + MapX + "," + MapY + "," + Facing + "," + Mission + "," + Tag + "," + 	Veterancy +
			"," + Group + "," + HighOnBridge + "," + FollowerIndex + "," + AutocreateNoRecruitable + "," + AutocreateYesRecruitable;
	}

}


//  Index=OWNER,ID,HEALTH,X,Y,FACING,MISSION,TAG,VETERANCY,GROUP,HIGH,FOLLOWS_ID,AUTOCREATE_NO_RECRUITABLE,AUTOCREATE_YES_RECRUITABLE
