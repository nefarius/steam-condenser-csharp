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
			if (reader.Length == 0) {
				throw new Exception("Wrong formatted S2A_RULES response packet.");
			}

			short numRules = reader.ReadShort();
			ServerRules = new List<ServerRule>((int)numRules);

			for (short i = 0; i < numRules; i++) {
				string cvar  = reader.ReadString();
				string value = reader.ReadString();
				
				ServerRules.Add(new ServerRule(cvar, value));
			}
		}
	}
}
