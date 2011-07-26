using System;
using System.Collections.Generic;
using System.Text;

namespace SteamCondenser.Steam.Packets
{
	public class PlayersResponsePacket : SteamPacket
	{
		public SteamPlayer[] PlayerList { get; protected set; }

		public PlayersResponsePacket(SteamPlayer[] playerList)
			: base(SteamPacketTypes.S2A_PLAYER)
		{
			PlayerList = playerList;
		}

		public override void Serialize(PacketWriter writer, bool prefix)
		{
			base.Serialize (writer, prefix);

			writer.WriteByte((byte)PlayerList.Length);

			foreach (var player in PlayerList) {
				player.Serialize(writer);
			}
		}

		public PlayersResponsePacket(byte[] data)
			: base(SteamPacketTypes.S2A_PLAYER, data)
		{
			if (reader.Length == 0) {
				throw new Exception("Wrong formatted S2A_PLAYER response packet.");
			}

			byte numPlayers = reader.ReadByte();

			PlayerList = new SteamPlayer[numPlayers];


			Console.WriteLine (reader.EndOfData);

			for (byte i = 0; i < numPlayers && !reader.EndOfData; i++) {
				PlayerList[i] = SteamPlayer.Deserialize(reader);
			}
		}
	}
}
