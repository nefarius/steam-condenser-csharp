using System;
using System.Collections.Generic;
using System.Text;

namespace SteamCondenser.Steam
{
	public class SteamPlayer
	{
		private TimeSpan connectTime;

		public TimeSpan ConnectTime
		{
			get { return connectTime; }
		}

		private int id;

		public int ID
		{
			get { return id; }
		}

		private String name;

		public String Name
		{
			get { return name; }
		}

		private int score;

		public int Score
		{
			get { return score; }
		}
		private bool isBot;

		public bool IsBot
		{
			get { return isBot; }
		}

		public SteamPlayer(int id, String name, int score, float connectTime)
		{
			this.id = id;
			this.name = name;
			this.score = score;

			if(connectTime == -1)
			{
				this.isBot = true;
				this.connectTime = TimeSpan.FromSeconds(0);
			}
			else
			{
				this.isBot = false;
				this.connectTime = TimeSpan.FromSeconds((double)Math.Round((double)connectTime));
			}
		}

		public override string ToString()
		{
			return "#" + this.id + " \"" + this.name + "\" " + this.score + " " + this.connectTime;
		}
	}
}
