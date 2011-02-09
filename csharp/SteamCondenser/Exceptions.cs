/**
 * This code is free software; you can redistribute it and/or modify it under
 * the terms of the new BSD License.
 *
 * Copyright (c) 2010, Andrius Bentkus
 */

using System;

namespace SteamCondenser
{
	
	[Serializable]
	public class SteamCondenserException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:SteamCondenserException"/> class
		/// </summary>
		public SteamCondenserException ()
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:SteamCondenserException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		public SteamCondenserException (string message) : base (message)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:SteamCondenserException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. </param>
		public SteamCondenserException (string message, Exception inner) : base (message, inner)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:SteamCondenserException"/> class
		/// </summary>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <param name="info">The object that holds the serialized object data.</param>
		protected SteamCondenserException (System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base (info, context)
		{
		}
	}
	
	
	[Serializable]
	public class PacketFormatException : SteamCondenserException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PacketFormatException"/> class
		/// </summary>
		public PacketFormatException ()
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PacketFormatException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		public PacketFormatException (string message) : base (message)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PacketFormatException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. </param>
		public PacketFormatException (string message, Exception inner) : base (message, inner)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PacketFormatException"/> class
		/// </summary>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <param name="info">The object that holds the serialized object data.</param>
		protected PacketFormatException (System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base (info, context)
		{
		}
	}
	
	
	[Serializable]
	public class UncompletePacketException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:UncompletePacketException"/> class
		/// </summary>
		public UncompletePacketException ()
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:UncompletePacketException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		public UncompletePacketException (string message) : base (message)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:UncompletePacketException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. </param>
		public UncompletePacketException (string message, Exception inner) : base (message, inner)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:UncompletePacketException"/> class
		/// </summary>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <param name="info">The object that holds the serialized object data.</param>
		protected UncompletePacketException (System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base (info, context)
		{
		}
	}
	
	
	[Serializable]
	public class RCONNoAuthException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:RCONNoAuthException"/> class
		/// </summary>
		public RCONNoAuthException ()
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:RCONNoAuthException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		public RCONNoAuthException (string message) : base (message)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:RCONNoAuthException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. </param>
		public RCONNoAuthException (string message, Exception inner) : base (message, inner)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:RCONNoAuthException"/> class
		/// </summary>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <param name="info">The object that holds the serialized object data.</param>
		protected RCONNoAuthException (System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base (info, context)
		{
		}
	}
	
	
	[Serializable]
	public class RCONBanException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:RconBanException"/> class
		/// </summary>
		public RCONBanException ()
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:RconBanException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		public RCONBanException (string message) : base (message)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:RconBanException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. </param>
		public RCONBanException (string message, Exception inner) : base (message, inner)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:RconBanException"/> class
		/// </summary>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <param name="info">The object that holds the serialized object data.</param>
		protected RCONBanException (System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base (info, context)
		{
		}
	}


}
