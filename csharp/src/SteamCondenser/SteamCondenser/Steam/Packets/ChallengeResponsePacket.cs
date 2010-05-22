using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SteamCondenser.Steam.Packets
{
	public class ChallengeResponsePacket : SteamPacket
	{
		private int challengeID;

		public int ChallengeID
		{
			get { return challengeID; }
			set { challengeID = value; }
		}

		public ChallengeResponsePacket(byte[] data)
			: base(SteamPacketTypes.S2C_CHALLENGE, data)
		{
			this.challengeID = this.byteReader.ReadInt32();
		}
	}
}
