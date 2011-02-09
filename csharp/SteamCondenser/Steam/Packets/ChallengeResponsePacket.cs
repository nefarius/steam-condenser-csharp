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
using System.IO;

namespace SteamCondenser.Steam.Packets
{
	public class ChallengeResponsePacket : SteamPacket
	{
		public int ChallengeID { get; set; }

		public ChallengeResponsePacket(byte[] data)
			: base(SteamPacketTypes.S2C_CHALLENGE, data)
		{
			ChallengeID = this.byteReader.ReadInt32();
		}
	}
}
