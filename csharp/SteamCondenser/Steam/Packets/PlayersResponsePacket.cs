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

namespace SteamCondenser.Steam.Packets
{
	public class PlayersResponsePacket : SteamPacket
	{
		public List<SteamPlayer> PlayerList { get; protected set; }

		public PlayersResponsePacket(byte[] data)
			: base(SteamPacketTypes.S2A_PLAYER, data)
		{
			if (this.byteReader.BaseStream.Length == 0)
				throw new Exception("Wrong formatted S2A_PLAYER response packet.");

			byte numPlayers = this.byteReader.ReadByte();

			PlayerList = new List<SteamPlayer>((int)numPlayers);

			for (byte i = 0; i < numPlayers && this.byteReader.BaseStream.Position < this.byteReader.BaseStream.Length; i++) {
				int id = (int)this.byteReader.ReadByte();
				string name = ReadString();
				int score = this.byteReader.ReadInt32();
				float connectTime = this.byteReader.ReadSingle();

				PlayerList.Add(new SteamPlayer(id, name, score, connectTime));
			}

		}
	}
}
