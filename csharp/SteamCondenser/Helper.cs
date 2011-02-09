/**
 * This code is free software; you can redistribute it and/or modify it under
 * the terms of the new BSD License.
 *
 * Copyright (c) 2010, Andrius Bentkus
 */

using System;
using System.Net;
using System.Xml;
using System.Collections.Generic;

namespace SteamCondenser
{
	public static class Helper
	{
		public static string GetString(this byte[] value)
		{
			return System.Text.Encoding.ASCII.GetString(value);
		}
		
		#region GetBytes
		
		public static byte[] GetBytes(this string value)
		{
			return System.Text.Encoding.ASCII.GetBytes(value);
		}
		
		public static byte[] GetBytes(this uint value)
		{
			return System.BitConverter.GetBytes(value);
		}
		
		public static byte[] GetBytes(this int value)
		{
			return System.BitConverter.GetBytes(value);
		}
		
		#endregion
		
		#region RevereseBytes
		
		public static uint ReverseBytes(this uint value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (BitConverter.IsLittleEndian) {
				Array.Reverse(bytes);
			}
			return BitConverter.ToUInt32(bytes, 0);
		}
		
		public static int ReverseBytes(this int value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (BitConverter.IsLittleEndian) {
				Array.Reverse(bytes);
			}
			return BitConverter.ToInt32(bytes, 0);
		}
		
		public static ushort ReverseBytes(this ushort value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (BitConverter.IsLittleEndian) {
				Array.Reverse(bytes);
			}
			return BitConverter.ToUInt16(bytes, 0);
		}
		
		#endregion
		
		public static string GetInnerText(this XmlDocument doc, string tag)
		{
			return doc.GetElementsByTagName(tag).Item(0).InnerText;
		}
		
		public static string GetInnerText(this XmlElement element, string tag)
		{
			return element.GetElementsByTagName(tag).Item(0).InnerText;
		}
		
		public static void LoadUrl(this XmlDocument xmldoc, string url)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			xmldoc.Load(request.GetResponse().GetResponseStream());
		}
		
		public static XmlElement GetXmlElement(this XmlDocument xmldoc, string name) 
		{
			return xmldoc.GetElementsByTagName(name).Item(0) as XmlElement;
		}
		
		public static XmlElement GetXmlElement(this XmlNode xmlnode, string name) 
		{
			return (xmlnode as XmlElement).GetElementsByTagName(name).Item(0) as XmlElement;
		}
		
		public static string GetValueText(this XmlElement element, string name)
		{
			return element.GetXmlElement(name).GetInnerText("value");
		}
	}
}
