using System;
using System.Collections.Generic;
using System.Text;

namespace SteamCondenser.Steam
{
	public class ServerRule
	{
		public string CVar { get; protected set; }
		public string Value { get; protected set; }

		public ServerRule(string cvar, string value)
		{
			CVar = cvar;
			Value = value;
		}

		public override string ToString()
		{
			return string.Format("\"{0}\" = \"{1}\"", CVar, Value);
		}
	}
}
