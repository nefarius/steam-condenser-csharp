using System;
using System.Collections.Generic;
using System.Text;

namespace SteamCondenser.Steam
{
	public class SteamPlayer
	{
		public int    ID    { get; protected set; }
		public String Name  { get; protected set; }
		public int    Score { get; protected set; }
		public bool   IsBot { get; protected set; }

		public TimeSpan ConnectTime { get; protected set; }

		public SteamPlayer(int id, string name, int score)
			: this(id, name, score, -1.0f)
		{
		}

		public SteamPlayer(int id, string name, int score, float connectTime)
		{
			ID    = id;
			Name  = name;
			Score = score;

			if (connectTime == -1.0f) {
				IsBot = true;
				ConnectTime = TimeSpan.FromSeconds(0);
			} else {
				IsBot = false;
				ConnectTime = TimeSpan.FromSeconds((double)Math.Round((double)connectTime));
			}
		}

		public void Serialize(PacketWriter writer)
		{
			writer.WriteByte  ((byte)ID);
			writer.WriteString(Name);
			writer.WriteInt   (Score);
			if (IsBot) {
				writer.WriteFloat(-1.0f);
			} else {
				writer.WriteFloat((float)ConnectTime.TotalSeconds);
			}
		}

		public static SteamPlayer Deserialize(PacketReader reader)
		{
			int    id     = (int)reader.ReadByte();
			string name        = reader.ReadString();
			int    score       = reader.ReadInt();
			float  connectTime = reader.ReadSingle();

			return new SteamPlayer(id, name, score, connectTime);
		}

		public override string ToString()
		{
			return "#" + ID + " \"" + Name + "\" " + Score + " " + ConnectTime;
		}
	}
}
