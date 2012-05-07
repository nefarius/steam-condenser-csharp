using System;
using System.Net;
using System.Xml;
using System.Text;
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

		#region Join

		public static string Join(this object[] objectArray)
		{
			return objectArray.Join("");
		}

		public static string Join(this object[] objectArray, char c)
		{
			return objectArray.Join(new string(new char[] { c }));
		}

		public static string Join(this object[] objectArray, string delimeter)
		{
			return objectArray.Join(0, delimeter);
		}

		public static string Join(this object[] objectArray, int startIndex)
		{
			return objectArray.Join(startIndex, "");
		}

		public static string Join(this object[] objectArray, int startIndex, char c)
		{
			return objectArray.Join(startIndex, new string(new char[] { c }));
		}

		public static string Join(this object[] objectArray, int startIndex, int endIndex)
		{
			return objectArray.Join(startIndex, endIndex, "");
		}

		public static string Join(this object[] objectArray, int startIndex, int endIndex, char c)
		{
			return objectArray.Join(startIndex, endIndex, new string(new char[] { c }));
		}

		public static string Join(this object[] objectArray, int startIndex, string delimeter)
		{
			return objectArray.Join(startIndex, objectArray.Length, delimeter);
		}

		public static string Join(this object[] objectArray, int startIndex, int endIndex, string delimeter)
		{
			int length = objectArray.Length;

			if ((length == 0) || (objectArray.Length < startIndex)) {
				return string.Empty;
			}

			if (endIndex > length) {
				endIndex = length;
			}

			StringBuilder sb = new StringBuilder();

			for (int i = startIndex; i < endIndex; i++) {
				if (i > startIndex) {
					sb.Append(delimeter);
				}
				sb.Append(objectArray[i]);
			}

			return sb.ToString();
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
