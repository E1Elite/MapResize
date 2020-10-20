using System;
using System.Collections.Generic;

public class Aircraft : XYElement
{
	public string Owner { get; set; }
	public string AircraftID  { get; set; }
	public int Health { get; set; }
	public int Facing { get; set; }
	public string Mission { get; set; }
	public string Tag { get; set; }
	public int Veterancy { get; set; }
	public int Group { get; set; }
	public int AutocreateNoRecruitable { get; set; }
	public int AutocreateYesRecruitable { get; set; }

	public Aircraft(string owner, string aircraftID, int health, int x, int y, int facing, string mission, string tag, int veterancy, int group, 
		int acNoRecruitable, int acYesRecruitable) : base(x, y)
	{
		Owner = owner;
		AircraftID = aircraftID;
		Health = health;
		Facing = facing;
		Mission = mission;
		Tag = tag;
		Veterancy = veterancy;
		Group = group;
		AutocreateNoRecruitable = acNoRecruitable;
		AutocreateYesRecruitable = acYesRecruitable;
	}

	public override string ToString()
	{
		return Owner + "," + AircraftID + "," + Health + "," + MapX + "," + MapY + "," + Facing + "," + Mission + "," +
			Tag + "," + Veterancy + "," + Group + "," + AutocreateNoRecruitable + "," + AutocreateYesRecruitable;
	}

}

//  Index=OWNER,ID,HEALTH,X,Y,FACING,MISSION,TAG,VETERANCY,GROUP,AUTOCREATE_NO_RECRUITABLE,AUTOCREATE_YES_RECRUITABLE
