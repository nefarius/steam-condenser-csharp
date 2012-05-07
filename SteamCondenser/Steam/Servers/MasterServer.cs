using System;
using System.IO;
using System.Net;
using System.Collections.Generic;

using SteamCondenser.Steam.Packets;
using SteamCondenser.Steam.Sockets;

namespace SteamCondenser.Steam.Servers
{
	public class HostEndPoint
	{
		public HostEndPoint(string hostname, int port)
		{
			this.Hostname = hostname;
			this.Port     = port;
		}

		public string Hostname { get; protected set; }
		public int Port { get; protected set; }

		public IPEndPoint Resolve()
		{
			return new IPEndPoint(Dns.GetHostAddresses(Hostname)[0], Port);
		}
	}

	public class MasterServer
	{
		public static HostEndPoint GoldSrcMasterServer = new HostEndPoint("hl1master.steampowered.com", 27010);
		public static HostEndPoint SourceMasterServer  = new HostEndPoint("hl2master.steampowered.com", 27011);

		private MasterServerSocket socket;
		public MasterServer(IPAddress address, int port)
			: this(new IPEndPoint(address, port))
		{
		}

		public MasterServer(HostEndPoint hostEndPoint)
			: this(hostEndPoint.Resolve())
		{
		}

		public MasterServer(IPEndPoint endpoint)
		{
			socket = new MasterServerSocket(endpoint);
		}

		public int Timeout {
			get { return socket.Timeout; }
			set { socket.Timeout = value; }
		}


		public List<IPEndPoint> GetServers()
		{
			List<IPEndPoint> list = GetServers(Regions.All, "");

			return list;
		}

		public List<IPEndPoint> GetServers(Regions regionCode, string filter)
		{
			IPEndPoint lastIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0);
			IPEndPoint zeroIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0);

			List<IPEndPoint> servers = new List<IPEndPoint>();
			do {

				MasterServerRequestBatchPacket requestPacket =
					new MasterServerRequestBatchPacket(regionCode,
					                                   lastIP.Address.ToString() + ":" + lastIP.Port,
					                                   filter);

				this.socket.Send(requestPacket);

				MasterServerResponseBatchPacket msrbp = (this.socket.GetReply() as MasterServerResponseBatchPacket);

				foreach (var server in msrbp.GetServers()) {
					servers.Add(server);
					lastIP = server;
				}

			} while (!(zeroIP.Equals(lastIP)));
			return servers;
		}
	}
}

