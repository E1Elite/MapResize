using System;
using System.Collections.Generic;

public abstract class XYElement
{
	public int MapX { get; set; }
	public int MapY { get; set; }

	public int MapX2 { get; set; }		// Tunnel lines have 2 set of coordinates, from and to.
	public int MapY2 { get; set; }

	public XYElement()
	{
		MapX = MapY = MapX2 = MapY2 = -1;
	}

	public XYElement(int mapX, int mapY)
	{
		MapX = mapX;
		MapY = mapY;
	}

	public XYElement(int mapX, int mapY, int mapX2, int mapY2)
	{
		MapX = mapX;
		MapY = mapY;
		MapX2 = mapX2;
		MapY2 = mapY2;
	}

	public void UpdateXY(int leftDelta, int topDelta, int rightDelta, int bottomDelta, bool isTunnel = false)
	{
		MapX += leftDelta + topDelta;
		MapY += topDelta + rightDelta;

		if (isTunnel)
		{
			MapX2 += leftDelta + topDelta;
			MapY2 += topDelta + rightDelta;
		}
	}
}

