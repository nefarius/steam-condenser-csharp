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

namespace SteamCondenser.Steam.Community
{
	public class SteamGroup
	{
		private static Dictionary<object, SteamGroup> cache = new Dictionary<object, SteamGroup>();
		
		public string   CustomUrl { get; protected set; }
		public long     GroupID64 { get; protected set; }
		public DateTime FetchTime { get; protected set; }

		public long ID { get { return GroupID64; } }
		
		public bool IsFetched { get { return FetchTime.Ticks != 0; } }
		
		public string BaseUrl { 
			get {
				if (CustomUrl == null)
					return "http://steamcommunity.com/gid/" + GroupID64;
				else
					return "http://steamcommunity.com/groups/" + CustomUrl;
			}
		}
		
		private SteamID[] members = null;
		public SteamID[] Members
		{
			get {
				if (members == null) FetchMembers();
				return members;
			}
		}
		
		public static SteamGroup Create(long id)
		{
			return SteamGroup.Create((object)id, true, false);
		}
		
		public static SteamGroup Create(string id)
		{
			return SteamGroup.Create((object)id, true, false);
		}
		
		public static SteamGroup Create(long id, bool fetch)
		{
			return SteamGroup.Create((object)id, fetch, false);
		}
		
		public static SteamGroup Create(string id, bool fetch)
		{
			return SteamGroup.Create((object)id, fetch, false);
		}
		
		public static SteamGroup Create(long id, bool fetch, bool bypassCache)
		{
			return SteamGroup.Create((object)id, fetch, bypassCache);
		}
		
		public static SteamGroup Create(string id, bool fetch, bool bypassCache)
		{
			return SteamGroup.Create((object)id, fetch, bypassCache);
		}
		
		private static SteamGroup Create(object id, bool fetch, bool bypassCache)
		{
			if (SteamGroup.IsCached(id) && !bypassCache)
			{
				SteamGroup grp = cache[id];
				if (fetch && !grp.IsFetched) grp.FetchMembers();
				return grp;
			}
			else
				return new SteamGroup(id, fetch);
		}
		
		public static bool IsCached(object id)	
		{
			return cache.ContainsKey(id);
		}
		
		public bool Cache()
		{
			if (!cache.ContainsKey(this.GroupID64))
			{
				cache[GroupID64] = this;
				if ((CustomUrl != null) && !cache.ContainsKey(CustomUrl))
					cache[CustomUrl] = this;
				return true;
			}
			return false;
		}
		
		private SteamGroup(object id, bool fetch)
		{
			if (id is string)
				CustomUrl = (string)id;
			else
				GroupID64 = (long)id;
		}
		
		public bool FetchMembers()
		{
			
			int page = 0;
			int totalPages;
			
			List<SteamID> members = new List<SteamID>();
			try {
				do {
					page++;
					string url = BaseUrl + "/memberlistxml?p=" + page;
					
					HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
					XmlDocument doc = new XmlDocument();
					doc.Load(request.GetResponse().GetResponseStream());
					
					totalPages = int.Parse(doc.GetInnerText("totalPages"));
					
					var memberList = (doc.GetElementsByTagName("members").Item(0) as XmlElement).GetElementsByTagName("steamID64");
					
					foreach (XmlElement member in memberList)
					{
						members.Add(SteamID.Create(long.Parse(member.InnerText)));
					}
				} while (page < totalPages);
			} catch { return false; }
			
			this.members = members.ToArray();
			
			FetchTime = DateTime.Now;
			return true;
		}
		
		public int MemberCount {
			get {
				if (members == null)
				{
					string url = BaseUrl + "/memberlistxml";
					HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
					XmlDocument doc = new XmlDocument();
					doc.Load(request.GetResponse().GetResponseStream());
					return int.Parse(doc.GetInnerText("memberCount"));
				}
				else 
					return members.Length;
			}
		}
		
	}
}