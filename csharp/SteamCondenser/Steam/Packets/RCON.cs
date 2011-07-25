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
		
		protected int Header { get; set; }
		public int RequestId { get; protected set; }
		protected int requestId;

		protected RCONPacket(int requestId, int rconHeader, string rconData)
			: base((SteamPacketTypes)0, System.Text.Encoding.ASCII.GetBytes(rconData))
		{
			Header = rconHeader;
			RequestId = requestId;
		}
		
		public new byte[] GetBytes()
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
			: base((SteamPacketTypes)0, System.Text.Encoding.ASCII.GetBytes(request))
		{
		}
		
		public override byte[] GetBytes()
		{
			MemoryStream byteStream = new MemoryStream(reader.Length + 4);
			byteStream.Write(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF }, 0, 4);
			byteStream.Write(reader.Data, 0, reader.Length);
			return byteStream.ToArray();
		}
	}
	
	public class RCONGoldSrcResponsePacket : SteamPacket
	{
		public RCONGoldSrcResponsePacket(byte[] commandResponse)
			: base(SteamPacketTypes.RCON_GOLDSRC_CHALLENGE_HEADER, commandResponse)
		{
		}
		
		public string Response	{
			get {
				return System.Text.Encoding.ASCII.GetString(reader.Data, 0, reader.Length - 2);
			}
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
