using System;
using System.Net;
using System.Collections.Generic;

using SteamCondenser.Steam.Packets;

namespace SteamCondenser.Steam.Packets
{
	public class MasterServerRequestBatchPacket : SteamPacket
	{
		public MasterServerRequestBatchPacket()
			: this(Regions.All, "0.0.0.0:0", "")
		{
		}

		public MasterServerRequestBatchPacket(Regions regionCode, string startIp, string filter)
			: base(SteamPacketTypes.A2M_GET_SERVERS_BATCH2)
		{
			Region = regionCode;
			StartIP = startIp;
			Filter = filter;
		}

		public string Filter { get; protected set; }

		public Regions Region { get; protected set; }

		public string StartIP { get; protected set; }

		public override void Serialize(PacketWriter pw, bool prefix)
		{
			base.Serialize(pw, prefix);

			pw.WriteByte((byte)PacketType);
			pw.WriteByte((byte)Region);
			pw.WriteString(StartIP);
			pw.WriteString(Filter);
		}
	}

	public class MasterServerResponseBatchPacket : SteamPacket
	{
		private List<IPEndPoint> servers;

		public MasterServerResponseBatchPacket(byte[] data)
			: base(SteamPacketTypes.M2A_SERVER_BATCH, data)
		{
			if (reader.ReadByte() != 0x0A) {
				throw new PacketFormatException("Master query response is missing additional 0x0A byte.");
			}

			servers = new List<IPEndPoint>();
			byte[] octets = new byte[4];
			int portNumber;

			try {
				do {
					octets[0] = reader.ReadByte();
					octets[1] = reader.ReadByte();
					octets[2] = reader.ReadByte();
					octets[3] = reader.ReadByte();
					// this is network ordered, which is unlike every other Stream protocol
					portNumber = reader.ReadUShort().ReverseBytes();

					IPEndPoint endpoint = new IPEndPoint(new IPAddress(octets), portNumber);
					servers.Add(endpoint);

				} while (!reader.EndOfData); // there is something to read
			}
			catch { }
		}

		public IList<IPEndPoint> GetServers()
		{
			return servers.AsReadOnly();
		}
	}
}
