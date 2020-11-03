using System;
using System.Collections.Generic;

public class Options
{
	public bool RemOutsideWaypoints { get; set; }
	public bool KeepL0ClearTiles { get; set; }
	public bool BetterTilesPackCompression { get; set; }
	public bool MaintainLocalSize { get; set; }
	public string MapName { get; set; }

	public Options()
	{
	}
}
