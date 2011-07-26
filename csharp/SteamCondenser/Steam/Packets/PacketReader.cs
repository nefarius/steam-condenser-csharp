using System;
using System.IO;
using System.Text;

namespace SteamCondenser
{
	public class PacketReader
	{
		byte[] data = null;
		MemoryStream ms = null;
		BinaryReader br = null;

		public PacketReader(byte[] data)
			: this(data, 0)
		{
		}

		public PacketReader(byte[] data, int offset)
		{
			this.data = data;
			ms = new MemoryStream(data);
			ms.Position = offset;
			br = new BinaryReader(ms);

			Encoding = Encoding.Default;
		}

		public Encoding Encoding { get; set; }

		public bool EndOfData {
			get {
				return Position >= Length;
			}
		}

		public byte CurrentByte {
			get {
				return data[Position];
			}
		}

		public int Position {
			get {
				return (int)ms.Position;
			}
			set {
				ms.Position = value;
			}
		}

		public int Length {
			get {
				return (int)ms.Length;
			}
		}

		#region ReadString

		public string ReadString()
		{
			return ReadString(Encoding);
		}

		public int GetRestLength()
		{
			int pos = Position;
			int start = pos;
			while (!EndOfData && data[pos] != 0) {
				pos++;
			}

			return pos - start;
		}

		public string ReadString(Encoding encoding)
		{
			string ret = encoding.GetString(data, Position, GetRestLength());
			Position += ret.Length + 1;
			return ret;
		}

		#endregion

		public byte ReadByte()
		{
			return br.ReadByte();
		}

		public short ReadShort()
		{
			return br.ReadInt16();
		}

		public ushort ReadUShort()
		{
			return br.ReadUInt16();
		}

		public int ReadInt()
		{
			return br.ReadInt32();
		}

		public float ReadSingle()
		{
			return br.ReadSingle();
		}

		public char ReadChar()
		{
			return br.ReadChar();
		}

		public bool ReadBool()
		{
			return br.ReadBoolean();
		}

		public long ReadLong()
		{
			return br.ReadInt64();
		}

		public byte[] Data {
			get {
				return data;
			}
		}
	}
}

