/**
 * This code is free software; you can redistribute it and/or modify it under
 * the terms of the new BSD License.
 *
 * Copyright (c) 2008, Thomas Schulz
 * Copyright (c) 2010, Andrius Bentkus
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace SteamCondenser.Steam.Packets
{
	class GameServerQueryRequestPacket : SteamPacket
	{
		public GameServerQueryRequestPacket(SteamPacketTypes requestType)
			: base(requestType)
		{
			
		}

		public GameServerQueryRequestPacket(SteamPacketTypes requestType, byte[] data)
			: base(requestType, data)
		{

		}
	}
}
