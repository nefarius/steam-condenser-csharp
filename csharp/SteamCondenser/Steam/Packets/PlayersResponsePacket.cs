using System;
using System.Collections.Generic;
using System.Text;

namespace SteamCondenser.Steam.Packets
{
	public class PlayersResponsePacket : SteamPacket
	{
		public List<SteamPlayer> PlayerList { get; protected set; }

		public PlayersResponsePacket(byte[] data)
			: base(SteamPacketTypes.S2A_PLAYER, data)
		{
			if (reader.Length == 0) {
				throw new Exception("Wrong formatted S2A_PLAYER response packet.");
			}

			byte numPlayers = reader.ReadByte();

			PlayerList = new List<SteamPlayer>((int)numPlayers);

			for (byte i = 0; i < numPlayers && !reader.EndOfData; i++) {
				int    id     = (int)reader.ReadByte();
				string name        = reader.ReadString();
				int    score       = reader.ReadInt();
				float  connectTime = reader.ReadSingle();

				PlayerList.Add(new SteamPlayer(id, name, score, connectTime));
			}

		}
	}
}
