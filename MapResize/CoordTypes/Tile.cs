using System;
using System.Collections.Generic;

public class Tile : XYElement
{
	public int TileIndex { get; set; }
	public byte SubIndex { get; set; }
	public byte Level { get; set; }
	public byte IceGrowth { get; set; }

	public Tile(int tileIndex, byte subIndex, byte level, byte iceGrowth, int x, int y) : base(x, y)
	{
		TileIndex = tileIndex;
		SubIndex = subIndex;
		Level = level;
		IceGrowth = iceGrowth;
	}

	public byte[] GetTileForMapPack()
	{
		List<byte> byteList = new List<byte>();

		byteList.AddRange(BitConverter.GetBytes((ushort)MapX));
		byteList.AddRange(BitConverter.GetBytes((ushort)MapY));
		byteList.AddRange(BitConverter.GetBytes(TileIndex));
		byteList.Add(SubIndex);
		byteList.Add(Level);
		byteList.Add(IceGrowth);

		return byteList.ToArray();
	}
}
