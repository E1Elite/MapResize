using System;
using System.Collections.Generic;
using System.Linq;

public class TunnelLine : XYElement
{
	public int Facing { get; set; }
	public List<int> Direction { get; set; }

	public TunnelLine(): base(-1, -1, -1, -1)
	{
		Facing = -1;
		Direction = new List<int>();
	}

	public TunnelLine(int sx, int sy, int facing, int ex, int ey,  List<int> ds) : base(sx, sy, ex, ey)
	{
		Facing = facing;
		Direction = new List<int>();
		if (ds != null)
		{
			Direction = new List<int>();
			Direction = ds.ToList();
		}
	}

	public override string ToString()
	{
		string result = MapX + "," + MapY + "," + Facing + "," + MapX2 + "," + MapY2;
		if (Direction != null && Direction.Count > 0)
		{
			foreach (int i in Direction)
				result += "," + i;
		}
		return result;
	}

}
