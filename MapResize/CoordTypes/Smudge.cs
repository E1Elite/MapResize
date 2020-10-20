using System;
using System.Collections.Generic;

public class Smudge : XYElement
{
	public string SmudgeType { get; set; }
	public int Ignore { get; set; }

	public Smudge(string smudgeType, int x, int y, int ignore) : base(x, y)
	{
		SmudgeType = smudgeType;
		Ignore = ignore;
	}

	public override string ToString()
	{
		return SmudgeType + "," + MapX + "," + MapY + "," + Ignore;
	}

}
