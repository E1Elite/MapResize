using System;
using System.Collections.Generic;

public class CellTag : XYElement
{
	public string Tag { get; set; }

	public CellTag(string tag, int x, int y) : base(x, y)
	{
		Tag = tag;
	}

	public override string ToString()
	{
		return Tag;
	}

}
