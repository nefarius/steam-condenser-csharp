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
using System.IO;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Checksums;
using SteamCondenser.Steam.Packets.RCON;

namespace SteamCondenser.Steam.Packets
{
	public class SteamPacket
	{
		public const int PACKET_SIZE = 1400;
		public const int PACKET_SPLIT_MARKER = -2;

		protected SteamPacketTypes packetType;
		protected byte[] data;
		protected MemoryStream dataStream;
		protected BinaryReader byteReader;

		public SteamPacket(SteamPacketTypes packetType)
			: this(packetType, new byte[] { })
		{
		}

		public SteamPacket(SteamPacketTypes packetType, byte[] data)
		{
			this.packetType = packetType;
			this.data = data;
			this.dataStream = new MemoryStream(data);
			this.byteReader = new BinaryReader(this.dataStream);
		}

		public virtual byte[] GetBytes()
		{
			MemoryStream byteStream = new MemoryStream(5 + this.data.Length);

			byteStream.Write(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, (byte)packetType }, 0, 5);
			byteStream.Write(this.data, 0, this.data.Length);

			return byteStream.ToArray();
		}

		public SteamPacketTypes PacketType {
			get {
				return this.packetType;
			}
		}

		public byte[] Data {
			get {
				return this.data;
			}
		}

		public static SteamPacket ReassemblePacket(List<byte[]> splitPackets)
		{
			return SteamPacket.ReassemblePacket(splitPackets, false, (short)0, 0);
		}

		public static SteamPacket ReassemblePacket(List<byte[]> splitPackets, bool isCompressed, short uncompressedSize, int packetChecksum)
		{
			byte[] packetData;
			packetData = new byte[0];
			MemoryStream memStream = new MemoryStream();
			
			foreach (byte[] splitPacket in splitPackets) {
				memStream.Write(splitPacket, 0, splitPacket.Length);
			}

			if (isCompressed) {
				BZip2InputStream bzip2 = new BZip2InputStream(new MemoryStream(packetData));
				bzip2.Read(packetData, 0, uncompressedSize);

				Crc32 crc32 = new Crc32();
				crc32.Update(packetData);

				if (crc32.Value != packetChecksum) {
					throw new Exception("CRC32 checksum mismatch of uncompressed packet data.");
				}
			}

			return SteamPacket.CreatePacket(memStream.ToArray());
		}

		public static SteamPacket CreatePacket(byte[] rawData)
		{
			
			SteamPacket packet;
			SteamPacketTypes packetType = (SteamPacketTypes)rawData[0];

			MemoryStream byteStream = new MemoryStream(rawData.Length - 1);
			byteStream.Write(rawData, 1, rawData.Length - 1);

			switch (packetType)
			{
			case SteamPacketTypes.S2C_CHALLENGE:
				packet = new ChallengeResponsePacket(byteStream.ToArray());
				break;

			case SteamPacketTypes.S2A_INFO:
				packet = new S2A_INFO_Packet(byteStream.ToArray());
				break;

			case SteamPacketTypes.S2A_INFO2:
				packet = new SourceServerInfoResponsePacket(byteStream.ToArray());
				//packet = new S2A_INFO2_Packet(byteStream.ToArray());
				break;

			case SteamPacketTypes.S2A_INFO_DETAILED:
				packet = new S2A_INFO_DETAILED_Packet(byteStream.ToArray());
				break;
			
			case SteamPacketTypes.S2A_RULES:
				packet = new ServerRulesResponsePacket(byteStream.ToArray());
				break;

			case SteamPacketTypes.S2A_PLAYER:
				packet = new PlayersResponsePacket(byteStream.ToArray());
				break;
			
			case SteamPacketTypes.RCON_GOLDSRC_CHALLENGE_HEADER:
			case SteamPacketTypes.RCON_GOLDSRC_NO_CHALLENGE_HEADER:
			case SteamPacketTypes.RCON_GOLDSRC_RESPONSE_HEADER:
				packet = new RCONGoldSrcResponsePacket(byteStream.ToArray());
				break;
			
			case SteamPacketTypes.M2A_SERVER_BATCH:
			 	packet = new MasterServerResponseBatchPacket(byteStream.ToArray());
				break;
			
			default:
				packet = new SteamPacket(packetType, byteStream.ToArray());
				break;
			}

			return packet;
		}

		protected string ReadString()
		{
			char currentChar = byteReader.ReadChar();
			StringBuilder str = new StringBuilder();

			while (currentChar != '\0') {
				str.Append(currentChar);
				currentChar = byteReader.ReadChar();
			}
			return str.ToString();
		}
	}
}
