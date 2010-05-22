using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SteamCondenser.Steam.Packets
{
	public class SourceServerInfoResponsePacket : SteamPacket
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
