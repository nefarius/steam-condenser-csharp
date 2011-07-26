using System;
using System.Collections.Generic;
using System.Text;

namespace SteamCondenser
{
	public enum OSTypes
	{
		Unknown,
		Windows,
		Linux,
	}
	
	public enum ServerTypes
	{
		Unknown,
		Listen,
		Dedicated,
		Proxy,
	}

	public enum Regions : byte
	{
		USEastCoast  = 0x00,
		USWestCoast  = 0x01,
		SouthAmerica = 0x02,
		Europe       = 0x03,
		Asia         = 0x04,
		Australia    = 0x05,
		MiddleEast   = 0x06,
		Africa       = 0x07,
		All          = 0xFF,
	}
}
