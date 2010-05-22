using System;
using System.Collections.Generic;
using System.Text;

namespace SteamCondenser.Steam.Packets
{
	public class ServerRulesResponsePacket : SteamPacket
	{
		private List<ServerRule> serverRules;

		public List<ServerRule> ServerRules
		{
			get { return serverRules; }
			set { serverRules = value; }
		}

		public ServerRulesResponsePacket(byte[] data)
			: base(SteamPacketTypes.S2A_RULES, data)
		{
			if(this.byteReader.BaseStream.Length == 0)
			{
				throw new Exception("Wrong formatted S2A_RULES response packet.");
			}

			short numRules = this.byteReader.ReadInt16();
			this.serverRules = new List<ServerRule>((int)numRules);

			for(short i = 0; i < numRules; i++)
			{
				string cvar = ReadString();
				string value = ReadString();

				//if(cvar != "")
				{
					this.serverRules.Add(new ServerRule(cvar, value));
				}
			}
		}
	}
}
