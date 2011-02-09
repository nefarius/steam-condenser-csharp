/**
 * This code is free software; you can redistribute it and/or modify it under
 * the terms of the new BSD License.
 *
 * Copyright (c) 2008, Thomas Schulz
 * Copyright (c) 2010, Andrius Bentkus
 */

using System;
using System.Reflection;
using SteamCondenser.Steam.Packets;

namespace SteamCondenser.Steam.Packets
{
	public class ServerInfo
	{
		public short    Port                { get; set; }
		public short    SpectatorPort       { get; set; }
		public string   SpectatorServerName { get; set; }
		public string[] Tags                { get; set; }
		
		public OSTypes OSType           { get; set; }
		public bool    PasswordRequired { get; set; }
		
		public bool    IsSecure        { get; set; }
		public string  GameVersion     { get; set; }
		public byte    BotsCount       { get; set; }
		public byte    Players         { get; set; }
		public byte    MaxPlayers      { get; set; }
		public short   ApplicationID   { get; set; }
		public byte    ProtocolVersion { get; set; }
		public string  GameDirectory   { get; set; }
		public string  GameDescription { get; set; }
		public string  MapName         { get; set; }
		public string  ServerName      { get; set; }

		public ServerTypes ServerType { get; set; }
		
		public override string ToString()
		{
			return string.Format("[ServerInfo: Port={0}, SpectatorPort={1}, SpectatorServerName={2}, Tags={3}, ServerType={4}, OSType={5}, PasswordRequired={6}, IsSecure={7}, GameVersion={8}, BotsCount={9}, Players={10}, MaxPlayers={11}, ApplicationID={12}, ProtocolVersion={13}, GameDirectory={14}, GameDescription={15}, MapName={16}, ServerName={17}]",
			                     Port, SpectatorPort, SpectatorServerName, Tags, ServerType, OSType, PasswordRequired, IsSecure, GameVersion, BotsCount, Players, MaxPlayers, ApplicationID, ProtocolVersion, GameDirectory, GameDescription, MapName, ServerName);
		}

	}
	
	public class ServerInfoBasePacket : SteamPacket
	{
		protected int bla;
		public int bla2;
		public ServerInfoBasePacket(SteamPacketTypes packetType, byte[] dataBytes)
			: base(packetType, dataBytes)
		{
			Type t = this.GetType();
			foreach (var bla in t.GetFields(BindingFlags.Instance)) {
				Console.WriteLine(bla.Name);
			}
			
		}
	}
	
	public class S2A_INFO_Packet : ServerInfoBasePacket
	{
		public S2A_INFO_Packet(byte[] dataBytes)
			: base(SteamPacketTypes.S2A_INFO, dataBytes)
		{
		}
	}
	
	public class S2A_INFO2_Packet : ServerInfoBasePacket
	{

		public S2A_INFO2_Packet(byte[] dataBytes)
			: base(SteamPacketTypes.S2A_INFO2, dataBytes)
		{
		}
	}
	
	public class S2A_INFO_DETAILED_Packet : ServerInfoBasePacket
	{
		public S2A_INFO_DETAILED_Packet(byte[] dataBytes)
			: base(SteamPacketTypes.S2A_INFO_DETAILED, dataBytes)
		{
		}
	}
	
	public class SourceServerInfoResponsePacket : ServerInfoBasePacket
	{
		public ServerInfo ServerInfo { get; set; }

		public SourceServerInfoResponsePacket(byte[] data)
			: base(SteamPacketTypes.S2A_INFO2, data)
		{
			ServerInfo = new ServerInfo();

			ServerInfo.ProtocolVersion  = byteReader.ReadByte();
			ServerInfo.ServerName       = ReadString();
			ServerInfo.MapName          = ReadString();
			ServerInfo.GameDirectory    = ReadString();
			ServerInfo.GameDescription  = ReadString();
			ServerInfo.ApplicationID    = byteReader.ReadInt16();
			ServerInfo.Players          = byteReader.ReadByte();
			ServerInfo.MaxPlayers       = byteReader.ReadByte();
			ServerInfo.BotsCount        = byteReader.ReadByte();
			ServerInfo.PasswordRequired = byteReader.ReadBoolean();
			ServerInfo.IsSecure         = byteReader.ReadBoolean();
			ServerInfo.GameVersion      = ReadString();

			char serverType = byteReader.ReadChar();
			char osType     = byteReader.ReadChar();
			
			switch (serverType) {
			case 'l':
				ServerInfo.ServerType = ServerTypes.Listen;
				break;

			case 'd':
				ServerInfo.ServerType = ServerTypes.Dedicated;
				break;

			case 'p':
				ServerInfo.ServerType = ServerTypes.Proxy;
				break;

			default:
				ServerInfo.ServerType = ServerTypes.Unknown;
				break;
			}

			switch(osType) {
			case 'l':
				ServerInfo.OSType = OSTypes.Linux;
				break;

			case 'w':
				ServerInfo.OSType = OSTypes.Windows;
				break;

			default:
				ServerInfo.OSType = OSTypes.Unknown;
				break;
			}

			ServerInfo.Port = 0;
			ServerInfo.SpectatorPort = 0;
			ServerInfo.SpectatorServerName = "";
			ServerInfo.Tags = new string[] { };

			if(byteReader.BaseStream.Position < byteReader.BaseStream.Length)
			{
				byte edf = byteReader.ReadByte();

				if ((edf & 0x80) == 0x80) {
					ServerInfo.Port = byteReader.ReadInt16();
				}

				if ((edf & 0x40) == 0x40) {
					ServerInfo.SpectatorPort = byteReader.ReadInt16();
					ServerInfo.SpectatorServerName = ReadString();
				}

				if ((edf & 0x20) == 0x20) {
					ServerInfo.Tags = ReadString().Split(',');
				}
			}
		}

	}
}
