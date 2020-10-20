using System;
using System.Collections.Generic;

public class Overlay : XYElement
{
	public int OverlayIndex { get; set; }
	public int OverlayFrame { get; set; }

	public Overlay(int index, int frame, int x, int y) : base(x, y)
	{
		OverlayIndex = index;
		OverlayFrame = frame;
	}
}
