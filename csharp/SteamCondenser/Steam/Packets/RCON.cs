using System;
using System.Net;
using System.IO;
using System.Text;

namespace SteamCondenser.Steam.Packets.RCON
{
	public abstract class RCONPacket : SteamPacket
	{
		public const byte SERVERDATA_AUTH = 3;
		public const byte SERVERDATA_AUTH_RESPONSE = 2;
		public const byte SERVERDATA_EXECCOMMANDO = 2;
		public const byte SERVERDATA_RESPONSE_VALUE = 0;
		
		public int Header { get; protected set; }
		public int RequestId { get; protected set; }
		public string RconData { get; protected set; }

		protected RCONPacket(int requestId, int rconHeader, string rconData)
			: base((SteamPacketTypes)0)
		{
			Header    = rconHeader;
			RequestId = requestId;
			RconData  = rconData;
		}

		public override void Serialize(PacketWriter writer, bool prefix)
		{
			PacketWriter pw = new PacketWriter();

			base.Serialize(pw, prefix);

			int lengthPosition = pw.Position;
			pw.WriteInt(0); // length

			pw.WriteInt(RequestId);
			pw.WriteInt(Header);
			pw.WriteStringNoZero(RconData);

			pw.Position = lengthPosition;

			pw.WriteInt(pw.Length);
		}

		public new byte[] GetBytes(bool prefix)
		{
			MemoryStream byteStream = new MemoryStream(12 + reader.Length);
			BinaryWriter sw = new BinaryWriter(byteStream);
			sw.Write(byteStream.Length - 4);
			sw.Write(RequestId);
			sw.Write(Header);
			sw.Write(reader.Data);
			sw.Flush();
			return byteStream.ToArray();
		}
		
		public static RCONPacket CreatePacket(byte[] rawData, int offset)
		{
			MemoryStream byteStream = new MemoryStream(rawData);
			StreamReader sr = new StreamReader(byteStream);

			int    requestId = sr.Read();
			int    header    = sr.Read();
			string data      = sr.ReadToEnd();
			
			switch (header) {
			case RCONPacket.SERVERDATA_AUTH_RESPONSE:
				return new RCONAuthResponsePacket(requestId);
			case RCONPacket.SERVERDATA_RESPONSE_VALUE:
				return new RCONExecResponsePacket(requestId, data);
			default:
				// TODO: Fix this into a proper Exception
				throw new Exception("Uknown packet with header ...");
			}
		}
	}
	
	#region GoldSrc
	public class RCONGoldSrcRequestPacket : SteamPacket
	{
		public RCONGoldSrcRequestPacket(string request)
			: base((SteamPacketTypes)0)
		{
			Request = request;
		}

		public string Request { get; protected set; }
		
		public override void Serialize(PacketWriter writer, bool prefix)
		{
			if (prefix) {
				writer.BlockCopy(SteamPacket.Prefix);
			}

			writer.WriteStringNoZero(Request);
		}
	}
	
	public class RCONGoldSrcResponsePacket : SteamPacket
	{
		public RCONGoldSrcResponsePacket(string response)
			: base(SteamPacketTypes.RCON_GOLDSRC_CHALLENGE_HEADER)
		{
			Response = response;
		}

		public RCONGoldSrcResponsePacket(byte[] response)
			: base(SteamPacketTypes.RCON_GOLDSRC_CHALLENGE_HEADER, response)
		{
			Response = reader.ReadString();
		}
		
		public string Response { get; protected set; }

		public override void Serialize(PacketWriter writer, bool prefix)
		{
			base.Serialize(writer, prefix);

			writer.WriteStringNoZero(Response);
		}
	}
	#endregion
	
	#region Exec
	public class RCONExecRequestPacket : RCONPacket
	{
		public RCONExecRequestPacket(int requestId, string rconCommand)
			: base(requestId, RCONPacket.SERVERDATA_EXECCOMMANDO, rconCommand)
		{
		}
	}
	public class RCONExecResponsePacket : RCONPacket
	{
		public RCONExecResponsePacket(int requestId, string commandReturn)
			: base(requestId, RCONPacket.SERVERDATA_RESPONSE_VALUE, commandReturn)
		{
		}
		
		public string GetResponse()
		{
			return System.Text.Encoding.ASCII.GetString(reader.Data, 0, reader.Length - 2);
		}
	}
	#endregion
	
	#region Auth
	public class RCONAuthRequestPacket : RCONPacket
	{
		public RCONAuthRequestPacket(int requestId, string rconPassword)
			: base(requestId, RCONPacket.SERVERDATA_AUTH, rconPassword)
		{
		}
	}
	
	public class RCONAuthResponsePacket : RCONPacket
	{
		public RCONAuthResponsePacket(int requestId)
			: base(requestId, RCONPacket.SERVERDATA_AUTH_RESPONSE, "")
		{
		}
	}
	#endregion
}
