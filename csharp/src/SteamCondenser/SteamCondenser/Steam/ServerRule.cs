/**
 * This code is free software; you can redistribute it and/or modify it under
 * the terms of the new BSD License.
 *
 * Copyright (c) 2008, Thomas Schulz
 * Copyright (c) 2010, Andrius Bentkus
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace SteamCondenser.Steam
{
	public class ServerRule
	{
		private string cvar;

		public string CVar
		{
			get { return cvar; }
		}
		private string val;

		public string Value
		{
			get { return val; }
		}

		public ServerRule(string cvar, string value)
		{
			this.cvar = cvar;
			this.val = value;
		}

		public override string ToString()
		{
			return string.Format("\"{0}\" = \"{1}\"", this.cvar, this.val);
		}
	}
}
