using System;
using System.Collections.Generic;
using System.Text;

namespace SteamCondenser.Steam.Packets
{
	public class ServerRulesResponsePacket : SteamPacket
	{
		public ServerRule[] ServerRules { get; protected set; }

		public ServerRulesResponsePacket(ServerRule[] rules)
			: base(SteamPacketTypes.S2A_RULES)
		{
			ServerRules = rules;
		}

		public ServerRulesResponsePacket(byte[] data)
			: base(SteamPacketTypes.S2A_RULES, data)
		{
			if (reader.Length == 0) {
				throw new Exception("Wrong formatted S2A_RULES response packet.");
			}

			short numRules = reader.ReadShort();
			ServerRules = new ServerRule[(int)numRules];

			for (short i = 0; i < numRules; i++) {
				string cvar  = reader.ReadString();
				string value = reader.ReadString();
				
				ServerRules[i] = new ServerRule(cvar, value);
			}
		}

		public override void Serialize(PacketWriter writer, bool prefix)
		{
			writer.WriteShort((short)ServerRules.Length);

			foreach (var rule in ServerRules) {
				writer.WriteString(rule.CVar);
				writer.WriteString(rule.Value);
			}
		}
	}
}
