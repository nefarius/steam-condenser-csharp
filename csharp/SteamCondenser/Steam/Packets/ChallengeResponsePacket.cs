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
