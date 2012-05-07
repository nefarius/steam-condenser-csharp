using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SteamCondenser.Steam.Packets
{
	public class ChallengeResponsePacket : SteamPacket
	{
		public int ChallengeID { get; protected set; }

		public ChallengeResponsePacket(int challengeID)
			: base(SteamPacketTypes.S2C_CHALLENGE)
		{
			ChallengeID = challengeID;
		}

		public ChallengeResponsePacket(byte[] data)
			: base(SteamPacketTypes.S2C_CHALLENGE, data)
		{
			ChallengeID = reader.ReadInt();
		}

		public override void Serialize(PacketWriter writer, bool prefix)
		{
			base.Serialize(writer, prefix);

			writer.WriteInt(ChallengeID);
		}
	}
}
