using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using SteamCondenser.Steam.Packets;

namespace SteamCondenser.Steam.Sockets
{
	public class SourceServerQuerySocket : ServerQuerySocket
	{
		public SourceServerQuerySocket(IPAddress ipAddress, int port)
			: base(ipAddress, port)
		{
		}

		public override SteamPacket GetReply()
		{
			int bytesRead;
			SteamPacket packet;

			bytesRead = this.ReceivePacket(SteamPacket.PACKET_SIZE);

			if(this.PacketIsSplit())
			{
				bool isCompressed = false;
				byte[] splitData;
				int packetCount, packetNumber, requestId;
				int packetsReceived = 1;
				int packetChecksum = 0;
				int packetSplit = 0;
				short splitSize;
				short uncompressedSize = 0;
				List<byte[]> splitPackets = new List<byte[]>();

				do
				{
					// Parsing of split packet headers
					requestId = ReverseBytes(this.bufferReader.ReadInt32());

					isCompressed = this.PacketIsCompressed(requestId);

					packetCount = (int)this.bufferReader.ReadByte();
					packetNumber = (int)this.bufferReader.ReadByte() + 1;
					splitSize = (this.bufferReader.ReadInt16());
					splitSize -= 4; // FIXME: do we get incorrect split sizes????

					if(packetsReceived == 1)
					{
						for(int i = 0; i < packetCount; i++)
						{
							splitPackets.Add(new byte[] { });
						}
					}

					if(isCompressed)
					{
						uncompressedSize = ReverseBytes(this.bufferReader.ReadInt16()); // int32 ??
						packetChecksum = ReverseBytes(this.bufferReader.ReadInt32());
					}

					// Omit additional header on the first packet 
					if(packetNumber == 1)
					{
						this.bufferReader.ReadInt32();
					}

					// Caching of split packet Data
					splitData = new byte[splitSize];
					this.bufferReader.Read(splitData, 0, splitSize);
					//splitPackets.Capacity = packetCount;
					//splitPackets.Add(splitData);
					splitPackets[packetNumber - 1] = splitData;

					// Receiving the next packet
					if(packetsReceived < packetCount)
					{
						bytesRead = this.ReceivePacket();

						packetSplit = this.bufferReader.ReadInt32();

						packetsReceived++;
					}
					else
					{
						bytesRead = 0;
					}
				}
				while(packetsReceived <= packetCount && bytesRead > 0 && packetSplit == SteamPacket.PACKET_SPLIT_MARKER);

				if(isCompressed)
				{
					packet = SteamPacket.ReassemblePacket(splitPackets, true, uncompressedSize, packetChecksum);
				}
				else
				{
					packet = SteamPacket.ReassemblePacket(splitPackets);
				}
			}
			else
			{
				packet = this.CreatePacket();
			}

			return packet;
		}

		private bool PacketIsCompressed(int requestId)
		{
			return (requestId & 0x8000) != 0;
		}

		private int ReverseBytes(int value)
		{
			byte[] bytes = BitConverter.GetBytes(value);

			if(BitConverter.IsLittleEndian)
			{
				Array.Reverse(bytes);
			}

			return BitConverter.ToInt32(bytes, 0);
		}

		private short ReverseBytes(short value)
		{
			byte[] bytes = BitConverter.GetBytes(value);

			if(BitConverter.IsLittleEndian)
			{
				Array.Reverse(bytes);
			}

			return BitConverter.ToInt16(bytes, 0);
		}
	}

}
