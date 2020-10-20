using System;
using System.Collections.Generic;

public class Infantry : XYElement
{
	public string Owner { get; set; }
	public string InfantryID  { get; set; }
	public int Health { get; set; }
	public int SubCell { get; set; }
	public int Facing { get; set; }
	public string Mission { get; set; }
	public string Tag { get; set; }
	public int Veterancy { get; set; }
	public int Group { get; set; }
	public int HighOnBridge { get; set; }
	public int AutocreateNoRecruitable { get; set; }
	public int AutocreateYesRecruitable { get; set; }

	public Infantry(string owner, string infantryID, int health, int x, int y, int subcell, string mission, int facing, string tag, int veterancy, int group, 
		int highOnBridge, int acNoRecruitable, int acYesRecruitable) : base(x, y)
	{
		Owner = owner;
		InfantryID = infantryID;
		Health = health;
		SubCell = subcell;
		Mission = mission;
		Facing = facing;
		Tag = tag;
		Veterancy = veterancy;
		Group = group;
		HighOnBridge = highOnBridge;
		AutocreateNoRecruitable = acNoRecruitable;
		AutocreateYesRecruitable = acYesRecruitable;
	}

	public override string ToString()
	{
		return Owner + "," + InfantryID + "," + Health + "," + MapX + "," + MapY + "," + SubCell + "," +  Mission + "," + Facing + "," +
			Tag + "," + Veterancy + "," + Group + "," + HighOnBridge + "," + AutocreateNoRecruitable + "," + AutocreateYesRecruitable;
	}

}

//  Index=OWNER,ID,HEALTH,X,Y,SUB_CELL,MISSION,FACING,TAG,VETERANCY,GROUP,HIGH,AUTOCREATE_NO_RECRUITABLE,AUTOCREATE_YES_RECRUITABLE
