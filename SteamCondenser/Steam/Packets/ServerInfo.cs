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
		public long    SteamID         { get; set; }

		public ServerTypes ServerType { get; set; }

		public override string ToString()
		{
			return string.Format("[ServerInfo: Port={0}, SpectatorPort={1}, SpectatorServerName={2}, Tags={3}, ServerType={4}, OSType={5}, PasswordRequired={6}, IsSecure={7}, GameVersion={8}, BotsCount={9}, Players={10}, MaxPlayers={11}, ApplicationID={12}, ProtocolVersion={13}, GameDirectory={14}, GameDescription={15}, MapName={16}, ServerName={17}]",
			                     Port, SpectatorPort, SpectatorServerName, Tags, ServerType, OSType, PasswordRequired, IsSecure, GameVersion, BotsCount, Players, MaxPlayers, ApplicationID, ProtocolVersion, GameDirectory, GameDescription, MapName, ServerName);
		}

	}

	public class ServerInfoBasePacket : SteamPacket
	{
		public ServerInfoBasePacket(SteamPacketTypes packetType, byte[] dataBytes)
			: base(packetType, dataBytes)
		{
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
		public ServerInfo ServerInfo { get; protected set; }

		public override void Serialize(PacketWriter writer, bool prefix)
		{
			writer.WriteByte  (ServerInfo.ProtocolVersion);
			writer.WriteString(ServerInfo.ServerName);
			writer.WriteString(ServerInfo.MapName);
			writer.WriteString(ServerInfo.GameDirectory);
			writer.WriteString(ServerInfo.GameDescription);
			writer.WriteShort (ServerInfo.ApplicationID);
			writer.WriteByte  (ServerInfo.Players);
			writer.WriteByte  (ServerInfo.MaxPlayers);

			writer.WriteChar(GetOSType(ServerInfo.OSType));
			writer.WriteChar(GetServerType(ServerInfo.ServerType));

			writer.WriteByte  (ServerInfo.BotsCount);
			writer.WriteBool  (ServerInfo.PasswordRequired);
			writer.WriteBool  (ServerInfo.IsSecure);
			writer.WriteString(ServerInfo.GameVersion);

			byte edf = 0;

			if (ServerInfo.Port != 0) {
				edf |= 0x80;
			}

			if (ServerInfo.SteamID != 0) {
				edf |= 0x10;
			}

			if (ServerInfo.SpectatorPort != 0 && ServerInfo.SpectatorServerName != null) {
				edf |= 0x40;
			}

			if (ServerInfo.Tags != null || ServerInfo.Tags.Length != 0) {
				edf |= 0x20;
			}

			if (edf > 0) {
				writer.WriteByte(edf);

				if ((edf & 0x80) > 0) {
					writer.WriteShort(ServerInfo.Port);
				}

				if ((edf & 0x10) > 0) {
					writer.WriteLong(ServerInfo.SteamID);
				}

				if ((edf & 0x40) > 0) {
					writer.WriteShort(ServerInfo.SpectatorPort);
					writer.WriteString(ServerInfo.SpectatorServerName);
				}

				if ((edf & 0x20) == 0x20) {
					writer.WriteString(ServerInfo.Tags.Join(' '));
				}
			}

		}

		public ServerTypes GetServerType(char serverType)
		{
			switch (serverType) {
			case 'l':
				return ServerTypes.Listen;
			case 'd':
				return ServerTypes.Dedicated;
			case 'p':
				return ServerTypes.Proxy;
			default:
				return ServerTypes.Unknown;
			}
		}
		public char GetServerType(ServerTypes serverType)
		{
			switch (serverType) {
			case ServerTypes.Listen:
				return 'l';
			case ServerTypes.Dedicated:
				return 'd';
			case ServerTypes.Proxy:
				return 'p';
			default:
				return ' ';
			}
		}

		public char GetOSType(OSTypes osType)
		{
			switch (osType) {
			case OSTypes.Linux:
				return 'l';
			case OSTypes.Windows:
				return 'w';
			default:
				return ' ';
			}
		}

		public OSTypes GetOSType(char osType)
		{
			switch (osType) {
			case 'l':
				return OSTypes.Linux;
			case 'w':
				return OSTypes.Windows;
			default:
				return OSTypes.Unknown;
			}
		}

		public SourceServerInfoResponsePacket(byte[] data)
			: base(SteamPacketTypes.S2A_INFO2, data)
		{
			ServerInfo = new ServerInfo();

			ServerInfo.ProtocolVersion  = reader.ReadByte();
			ServerInfo.ServerName       = reader.ReadString();
			ServerInfo.MapName          = reader.ReadString();
			ServerInfo.GameDirectory    = reader.ReadString();
			ServerInfo.GameDescription  = reader.ReadString();
			ServerInfo.ApplicationID    = reader.ReadShort();
			ServerInfo.Players          = reader.ReadByte();
			ServerInfo.MaxPlayers       = reader.ReadByte();
			ServerInfo.BotsCount        = reader.ReadByte();

			ServerInfo.ServerType = GetServerType(reader.ReadChar());
			ServerInfo.OSType     = GetOSType    (reader.ReadChar());

			ServerInfo.PasswordRequired = reader.ReadBool();
			ServerInfo.IsSecure         = reader.ReadBool();
			ServerInfo.GameVersion      = reader.ReadString();


			ServerInfo.Port = 0;
			ServerInfo.SpectatorPort = 0;
			ServerInfo.SpectatorServerName = "";
			ServerInfo.Tags = new string[] { };

			if (!reader.EndOfData) {
				byte edf = reader.ReadByte();

				if ((edf & 0x80) > 0) {
					ServerInfo.Port = reader.ReadShort();
				}

				if ((edf & 0x10) > 0) {
					ServerInfo.SteamID = reader.ReadLong();
				}

				if ((edf & 0x40) > 0) {
					ServerInfo.SpectatorPort       = reader.ReadShort();
					ServerInfo.SpectatorServerName = reader.ReadString();
				}

				if ((edf & 0x20) > 0) {
					ServerInfo.Tags = reader.ReadString().Split(',');
				}
			}
		}

	}
}
