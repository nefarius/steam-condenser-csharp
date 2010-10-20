// /**
//  * This code is free software; you can redistribute it and/or modify it under
//  * the terms of the new BSD License.
//  *
//  * Copyright (c) 2009-2010, Andrius Bentkus
//  */
// 

using System;
using SteamCondenser.Steam.Servers;
using NUnit.Framework;

namespace SteamCondenser.Test
{
	[TestFixture]
	public class QueryTests
	{
		[Test]
		[ExpectedException(typeof(System.Net.Sockets.SocketException))]
		public void InvalidGoldSrcServer()
		{
			GoldSrcServer server = new GoldSrcServer("10.0.0.1");
			int ping = server.Ping;
		}
	}
}

