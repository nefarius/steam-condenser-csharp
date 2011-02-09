/**
 * This code is free software; you can redistribute it and/or modify it under
 * the terms of the new BSD License.
 *
 * Copyright (c) 2010, Andrius Bentkus
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace SteamCondenser.Steam.Packets
{
	public class ServerRulesResponsePacket : SteamPacket
	{
		public List<ServerRule> ServerRules { get; set; }

		public ServerRulesResponsePacket(byte[] data)
			: base(SteamPacketTypes.S2A_RULES, data)
		{
			if (this.byteReader.BaseStream.Length == 0) {
				throw new Exception("Wrong formatted S2A_RULES response packet.");
			}

			short numRules = this.byteReader.ReadInt16();
			ServerRules = new List<ServerRule>((int)numRules);

			for (short i = 0; i < numRules; i++) {
				string cvar = ReadString();
				string value = ReadString();
				
				ServerRules.Add(new ServerRule(cvar, value));
			}
		}
	}
}
