using System;
using System.Collections.Generic;
using System.Linq;

public class Waypoint : XYElement
{
	public int  Number { get; set; }

	public Waypoint(int number, int x, int y) : base(x, y)
	{
		Number = number;
	}

	public override string ToString()
	{
		return Convert.ToString(Number);
	}

}
