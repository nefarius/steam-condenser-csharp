using System;
using System.Net;
using SteamCondenser.Steam.Sockets;

namespace SteamCondenser.Steam.Servers
{
	public class GoldSrcServer : GameServer
	{
		private string rconPassword;
		
		public GoldSrcSocket Socket { 
			get {
				return this.querySocket as GoldSrcSocket;
			}
		}
		
		public GoldSrcServer(string ipAddress)
			: this(ipAddress, 27015)
		{
		}
		
		public GoldSrcServer(string ipAddress, int portNumber)
			: this(ipAddress, portNumber, false)
		{
		}
		
		public GoldSrcServer(string ipAddress, int portNumber, bool isHLTV)
			: this(IPAddress.Parse(ipAddress), portNumber, isHLTV)
		{
		}
		
		public GoldSrcServer(IPAddress ipAddress)
			: this(ipAddress, 27015)
		{
		}
		
		public GoldSrcServer(IPAddress ipAddress, int portNumber)
			: this(ipAddress, portNumber, false)
		{
		}
		
		public GoldSrcServer(IPEndPoint endpoint)
			: this(endpoint, false)
		{
		}
		
		public GoldSrcServer(IPEndPoint endpoint, bool isHLTV)
			: this(endpoint.Address, endpoint.Port, isHLTV)
		{
			
		}
		
		public GoldSrcServer(IPAddress ipAddress, int portNumber, bool isHLTV)
			: base(portNumber)
		{
			this.querySocket = new GoldSrcSocket(ipAddress, portNumber, isHLTV);
		}
		
		public bool RconAuth(string password)
		{
			this.rconPassword = password;
			return true;
		}
		
		public string RconExec(string command)
		{
			return ((GoldSrcSocket)this.querySocket).RconExec(this.rconPassword, command);
		}
	}
}
