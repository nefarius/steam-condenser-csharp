/**
 * This code is free software; you can redistribute it and/or modify it under
 * the terms of the new BSD License.
 *
 * Copyright (c) 2008, Thomas Schulz
 * Copyright (c) 2010, Andrius Bentkus
 */

using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using SteamCondenser.Steam.Packets;
using SteamCondenser.Steam.Sockets;


namespace SteamCondenser
{
	public enum Regions : byte 
	{
		USEastCoast = 0x00,
		USWestCoast = 0x01,
		SouthAmerica = 0x02,
		Europe = 0x03,
		Asia = 0x04,
		Australia = 0x05,
		MiddleEast = 0x06,
		Africa = 0x07,
		All = 0xFF,
	}

}

namespace SteamCondenser.Steam.Packets
{
	public class MasterServerRequestBatchPacket : SteamPacket
	{
		private string filter;
		private Regions regionCode;
		private string startIp;
		
		public MasterServerRequestBatchPacket()
			: this(Regions.All, "0.0.0.0:0", "")
		{
		}
		
		public MasterServerRequestBatchPacket(Regions regionCode, string startIp, string filter)
			: base(SteamPacketTypes.A2M_GET_SERVERS_BATCH2)
		{
			this.startIp = startIp;
			this.regionCode = regionCode;
			this.filter = filter;
		}
		
		public override byte[] GetBytes()
		{
			MemoryStream byteStream = new MemoryStream();
			BinaryWriter bw = new BinaryWriter(byteStream);
			
			bw.Write((byte)this.packetType);
			bw.Write((byte)regionCode);
			bw.Write((this.startIp + "\0").GetBytes());
			bw.Write((this.filter + "\0").GetBytes());
			
			return byteStream.ToArray();
		}
	}
	
	public class MasterServerResponseBatchPacket : SteamPacket
	{
		private List<IPEndPoint> servers;
		
		public MasterServerResponseBatchPacket(byte[] data)
			: base(SteamPacketTypes.M2A_SERVER_BATCH, data)
		{
			if (this.byteReader.ReadByte() != 0x0A)
			{
				throw new PacketFormatException("Master query response is missing additional 0x0A byte.");
			}
			
			servers = new List<IPEndPoint>();
			byte[] octets = new byte[4];
			int portNumber;
	
			try
			{
				do {
					octets[0] = this.byteReader.ReadByte();
					octets[1] = this.byteReader.ReadByte();
					octets[2] = this.byteReader.ReadByte();
					octets[3] = this.byteReader.ReadByte();
					// this is network ordered, which is unlike every other Stream protocol
					portNumber = this.byteReader.ReadUInt16().ReverseBytes();
					
					IPEndPoint endpoint = new IPEndPoint(new IPAddress(octets), portNumber);
					servers.Add(endpoint);
					
				} while (this.byteReader.BaseStream.Length - this.byteReader.BaseStream.Position > 0); // there is something to read
			}
			catch { }
		}
		
		public IList<IPEndPoint> GetServers()
		{
			return servers.AsReadOnly();
		}
	}
}

namespace SteamCondenser.Steam.Servers
{
	public struct HostEndPoint
	{
		public HostEndPoint(string hostname, int port)
		{
			this.hostname = hostname;
			this.port     = port;
		}
		
		public string hostname;
		public int port;
		
		public IPEndPoint Resolve()
		{
			return new IPEndPoint(Dns.GetHostAddresses(hostname)[0], port);
		}
		
	}
	
	public class MasterServer
	{
		public static HostEndPoint GoldSrcMasterServer = new HostEndPoint("hl1master.steampowered.com", 27010);
		public static HostEndPoint SourceMasterServer = new HostEndPoint("hl2master.steampowered.com", 27011);
		
		private MasterServerSocket socket;
		public MasterServer(HostEndPoint hostEndPoint)
		{
			socket = new MasterServerSocket(hostEndPoint.Resolve());
		}
		
		public List<IPEndPoint> GetServers()
		{
			return GetServers(Regions.All, "");
		}
		
		public List<IPEndPoint> GetServers(Regions regionCode, string filter)
		{
			IPEndPoint lastIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0);
			
			List<IPEndPoint> servers = new List<IPEndPoint>();
			do {
				MasterServerRequestBatchPacket requestPacket = new MasterServerRequestBatchPacket(regionCode, lastIP.Address.ToString() + ":" + lastIP.Port, filter);
				this.socket.Send(requestPacket);
				
				MasterServerResponseBatchPacket msrbp = (this.socket.GetReply() as MasterServerResponseBatchPacket);
				
				foreach (var server in msrbp.GetServers())
				{
					servers.Add(server);
					lastIP = server;
				}
				
			} while (!(lastIP.Address.ToString() == "0.0.0.0" && lastIP.Port == 0));
			
			return servers;
		}
	}
}

namespace SteamCondenser.Steam.Sockets
{
	public class MasterServerSocket : ServerQuerySocket
	{
		public MasterServerSocket(IPAddress ipAddress, int port)
			: base(ipAddress, port)
		{
		}
		
		public MasterServerSocket(IPEndPoint endpoint)
			: base(endpoint.Address, endpoint.Port)
		{
		}
		
		public override SteamPacket GetReply()
		{
			this.ReceivePacket(1500);
			if (this.bufferReader.ReadUInt32() != 0xFFFFFFFF)
			{
				throw new Exception("Master query response has wrong packet header.");
			}
			return this.CreatePacket();
		}
	}
}
