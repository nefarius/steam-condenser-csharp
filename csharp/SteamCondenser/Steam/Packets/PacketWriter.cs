using System;
using System.IO;
using System.Text;

namespace SteamCondenser
{
	public class PacketWriter
	{
		byte[] data = null;
		MemoryStream ms = null;
		BinaryWriter bw = null;

		public PacketWriter()
			: this(0)
		{
		}

		public PacketWriter(int startPosition)
		{
			ms = new MemoryStream();
			ms.Position = startPosition;
		}

		public PacketWriter(byte[] data)
			: this(data, 0)
		{
		}

		public PacketWriter(byte[] data, int startPosition)
		{
			this.data = data;
			ms = new MemoryStream(data);
			ms.Position = startPosition;
			bw = new BinaryWriter(ms);

			Encoding = Encoding.Default;
		}

		public Encoding Encoding { get; set; }

		public int Position {
			get {
				return (int)ms.Position;
			}
		}

		public int Length {
			get {
				return (int)ms.Length;
			}
		}

		public byte[] Data {
			get {
				return data ?? ms.ToArray();
			}
		}

		public void WriteByte(byte value)
		{
			bw.Write(value);
		}

		public void WriteShort(short value)
		{
			bw.Write(value);
		}

		public void WriteUShort(ushort value)
		{
			bw.Write(value);
		}

		public void WriteInt(int integer)
		{
			bw.Write(integer);
		}

		public void WriteUInt(uint integer)
		{
			bw.Write(integer);
		}

		public void WriteBytes(byte[] data)
		{
			WriteBytes(data, 0);
		}

		public void WriteBytes(byte[] data, int index)
		{
			WriteBytes(data, index, 0);
		}

		public void WriteBytes(byte[] data, int index, int count)
		{
			bw.Write(data, index, count);
		}

		public void WriteString(string str)
		{
			WriteString(str, Encoding);
		}

		public void WriteString(string str, Encoding encoding)
		{
			bw.Write(encoding.GetBytes(str));
			WriteByte(0);
		}

		public void BlockCopy(byte[] src)
		{
			BlockCopy(src, 0);
		}

		public void BlockCopy(byte[] src, int srcOffset)
		{
			if (data != null) {
				Buffer.BlockCopy(src, srcOffset, data, Position, data.Length - Position);
			} else {
				bw.Write(src, srcOffset, src.Length - srcOffset);
			}
		}
	}
}

