using System;
using System.Collections.Generic;
using System.Text;

namespace SteamCondenser.Steam
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
	}
}
