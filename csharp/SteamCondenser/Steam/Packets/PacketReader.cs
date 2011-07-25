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
		{
			this.data = data;
			ms = new MemoryStream(data);
			ms.Position = 0;
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
				return (int)ms.Length;
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
			return encoding.GetString(data, Position, GetRestLength());
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

		public bool ReadBoolean()
		{
			return br.ReadBoolean();
		}

		public byte[] Data {
			get {
				return data;
			}
		}
	}
}

