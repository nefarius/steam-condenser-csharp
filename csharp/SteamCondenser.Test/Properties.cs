using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SteamCondenser.Test
{
	/// <summary>
	/// java.util.properties replacement.
	/// </summary>
	public class Properties : Dictionary<string, string>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public Properties()
		{
		}
		
		static Regex optionRegex = new Regex(@"#(.+)[ \t]+=[ \t]+(.+)", RegexOptions.Compiled);
		public bool Load(Stream stream)
		{
			try {
				if ((stream == null) || !stream.CanRead) return false;
				StreamReader sr = new StreamReader(stream);
				
				while (!sr.EndOfStream) {
					Match match = optionRegex.Match(sr.ReadLine());
					this.Add(match.Groups[1].Value, match.Groups[2].Value);
				}

				return true;
			}
			catch { return false; }
			
		}
	}
}

