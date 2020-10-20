using System;
using System.Collections.Generic;

public class BaseNode : XYElement
{
	public string SectionName { get; set; }
	public string BuildingID { get; set; }

	public BaseNode(string sectionName, string buildingID, int x, int y) : base(x, y)
	{
		SectionName = sectionName;
		BuildingID = buildingID;
	}

	public override string ToString()
	{
		return BuildingID + "," + MapX + "," + MapY;
	}
}
