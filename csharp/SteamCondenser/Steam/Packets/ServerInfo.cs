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
		private string mapName;
		private string serverName;
		private byte protocolVersion;
		private string gameDirectory;
		private string gameDescription;
		private short applicationID;
		private byte players;
		private byte maxPlayers;
		private byte botsCount;
		private ServerTypes serverType;
		private OSTypes osType;
		private bool passwordRequired;
		private bool isSecure;
		private string gameVersion;
		private short port;
		private short spectatorPort;
		private string spectatorServerName;
		private string[] tags;

		public short Port
		{
			get { return port; }
			set { port = value; }
		}

		public short SpectatorPort
		{
			get { return spectatorPort; }
			set { spectatorPort = value; }
		}

		public string SpectatorServerName
		{
			get { return spectatorServerName; }
			set { spectatorServerName = value; }
		}

		public string[] Tags
		{
			get { return tags; }
			set { tags = value; }
		}

		public ServerTypes ServerType
		{
			get { return serverType; }
			set { serverType = value; }
		}

		public OSTypes OSType
		{
			get { return osType; }
			set { osType = value; }
		}

		public bool PasswordRequired
		{
			get { return passwordRequired; }
			set { passwordRequired = value; }
		}

		public bool IsSecure
		{
			get { return isSecure; }
			set { isSecure = value; }
		}

		public string GameVersion
		{
			get { return gameVersion; }
			set { gameVersion = value; }
		}

		public byte BotsCount
		{
			get { return botsCount; }
			set { botsCount = value; }
		}

		public byte Players
		{
			get { return players; }
			set { players = value; }
		}

		public byte MaxPlayers
		{
			get { return maxPlayers; }
			set { maxPlayers = value; }
		}

		public short ApplicationID
		{
			get { return applicationID; }
			set { applicationID = value; }
		}

		public byte ProtocolVersion
		{
			get { return protocolVersion; }
			set { protocolVersion = value; }
		}

		public string GameDirectory
		{
			get { return gameDirectory; }
			set { gameDirectory = value; }
		}

		public string GameDescription
		{
			get { return gameDescription; }
			set { gameDescription = value; }
		}

		public string MapName
		{
			get { return mapName; }
			set { mapName = value; }
		}

		public string ServerName
		{
			get { return serverName; }
			set { serverName = value; }
		}
		
		public override string ToString ()
		{
			return string.Format("[ServerInfo: Port={0}, SpectatorPort={1}, SpectatorServerName={2}, Tags={3}, ServerType={4}, OSType={5}, PasswordRequired={6}, IsSecure={7}, GameVersion={8}, BotsCount={9}, Players={10}, MaxPlayers={11}, ApplicationID={12}, ProtocolVersion={13}, GameDirectory={14}, GameDescription={15}, MapName={16}, ServerName={17}]", Port, SpectatorPort, SpectatorServerName, Tags, ServerType, OSType, PasswordRequired, IsSecure, GameVersion, BotsCount, Players, MaxPlayers, ApplicationID, ProtocolVersion, GameDirectory, GameDescription, MapName, ServerName);
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
			foreach (var bla in t.GetFields(BindingFlags.Instance))
			{
				Console.WriteLine (bla.Name);
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
		private ServerInfo serverInfo;

		public ServerInfo ServerInfo
		{
			get { return serverInfo; }
			set { serverInfo = value; }
		}

		public SourceServerInfoResponsePacket(byte[] data)
			: base(SteamPacketTypes.S2A_INFO2, data)
		{
			this.serverInfo = new ServerInfo();

			this.serverInfo.ProtocolVersion = byteReader.ReadByte();
			this.serverInfo.ServerName = ReadString();
			this.serverInfo.MapName = ReadString();
			this.serverInfo.GameDirectory = ReadString();
			this.serverInfo.GameDescription = ReadString();
			this.serverInfo.ApplicationID = byteReader.ReadInt16();
			this.serverInfo.Players = byteReader.ReadByte();
			this.serverInfo.MaxPlayers = byteReader.ReadByte();
			this.serverInfo.BotsCount = byteReader.ReadByte();
			char serverType = byteReader.ReadChar();
			char osType = byteReader.ReadChar();
			this.serverInfo.PasswordRequired = byteReader.ReadBoolean();
			this.serverInfo.IsSecure = byteReader.ReadBoolean();
			this.serverInfo.GameVersion = ReadString();

			switch(serverType)
			{
				case 'l':
					this.serverInfo.ServerType = ServerTypes.Listen;
					break;

				case 'd':
					this.serverInfo.ServerType = ServerTypes.Dedicated;
					break;

				case 'p':
					this.serverInfo.ServerType = ServerTypes.Proxy;
					break;

				default:
					this.serverInfo.ServerType = ServerTypes.Unknown;
					break;
			}

			switch(osType)
			{
				case 'l':
					this.serverInfo.OSType = OSTypes.Linux;
					break;

				case 'w':
					this.serverInfo.OSType = OSTypes.Windows;
					break;

				default:
					this.serverInfo.OSType = OSTypes.Unknown;
					break;
			}

			this.serverInfo.Port = 0;
			this.serverInfo.SpectatorPort = 0;
			this.serverInfo.SpectatorServerName = "";
			this.serverInfo.Tags = new string[] { };

			if(byteReader.BaseStream.Position < byteReader.BaseStream.Length)
			{
				byte edf = byteReader.ReadByte();

				if((edf & 0x80) == 0x80)
				{
					this.serverInfo.Port = byteReader.ReadInt16();
				}

				if((edf & 0x40) == 0x40)
				{
					this.serverInfo.SpectatorPort = byteReader.ReadInt16();
					this.serverInfo.SpectatorServerName = ReadString();
				}

				if((edf & 0x20) == 0x20)
				{
					this.serverInfo.Tags = ReadString().Split(',');
				}
			}
		}

	}
}
