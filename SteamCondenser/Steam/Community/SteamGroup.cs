using System;
using System.Net;
using System.Xml;
using System.Collections.Generic;

namespace SteamCondenser.Steam.Community
{
	public class SteamGroup
	{
		#region Fields
		
		private static Dictionary<object, SteamGroup> cacheMemory = new Dictionary<object, SteamGroup>();
		
		public string   CustomUrl { get; protected set; }
		public long     GroupID64 { get; protected set; }
		public DateTime FetchTime { get; protected set; }

		public long ID { get { return GroupID64; } }
		
		public bool IsFetched { get { return FetchTime.Ticks != 0; } }
		
		
		private SteamID[] members = null;
		public SteamID[] Members {
			get {
				if (members == null) FetchMembers(true);
				return members;
			}
		}
		
		#endregion
		
		#region Constructors
		
		public static SteamGroup Create(long id)
		{
			return SteamGroup.Create(id, true, true);
		}
		
		public static SteamGroup Create(string id)
		{
			return SteamGroup.Create(id, true, true);
		}
		
		public static SteamGroup Create(long id, bool fetch)
		{
			return SteamGroup.Create(id, fetch, true);
		}
		
		public static SteamGroup Create(string id, bool fetch)
		{
			return SteamGroup.Create(id, fetch, true);
		}
		
		public static SteamGroup Create(long id, bool fetch, bool cache)
		{
			return SteamGroup.Create((object)id, fetch, cache);
		}
		
		public static SteamGroup Create(string id, bool fetch, bool cache)
		{
			return SteamGroup.Create((object)id.ToLower(), fetch, cache);
		}
		
		private static SteamGroup Create(object id, bool fetch, bool cache)
		{
			SteamGroup grp;
			
			if (!cache) {
				grp = new SteamGroup(id);
			} else if (!IsCached(id)) {
				grp = new SteamGroup(id);
				cacheMemory[id] = grp;
			} else {
				grp = cacheMemory[id];
			}
			
			if (fetch && !grp.IsFetched) grp.FetchMembers(cache);
			
			return grp;
			
		}
		
		private SteamGroup(object id)
		{
			if (id is string)
				CustomUrl = (id as string).ToLower();
			else
				GroupID64 = (long)id;
		}
		
		#endregion
		
		#region URL Handling
		
		public static string GroupPage   { get { return "http://steamcommunity.com/groups/"; } }
		public static string GroupIDPage { get { return "http://steamcommunity.com/gid/"; } }

		public static string GetPage(string id)
		{
			return SteamGroup.GroupPage + id;
		}
		
		public static string GetPage(long id)
		{
			return SteamGroup.GroupIDPage + id;
		}
		
		public string GroupUrl { get { return GetPage(CustomUrl); } }
		public string GroupIDUrl { get { return GetPage(GroupID64); } }
		
		public string BaseUrl {
			get {
				if (CustomUrl == null) return GroupIDUrl;
				else return GroupUrl;
			}
		}
		
		#endregion
		
		public static bool IsCached(string id)
		{
			return cacheMemory.ContainsKey(id.ToLower());
		}
		
		public static bool IsCached(object id)	
		{
			if (id is string) return IsCached(id as string);
			return cacheMemory.ContainsKey(id);
		}
		
		public void Cache()
		{
			if (!cacheMemory.ContainsKey(this.GroupID64)) {
				cacheMemory[GroupID64] = this;
			}
			if ((CustomUrl != null) && !cacheMemory.ContainsKey(CustomUrl)) {
				cacheMemory[CustomUrl] = this;
			}
		}
		
		public static void ClearCache()
		{
			cacheMemory = new Dictionary<object, SteamGroup>();
		}
		
		public bool FetchMembers()	
		{
			return FetchMembers(false);
		}
		
		public bool FetchMembers(bool cache)
		{
			
			int page = 0;
			int totalPages;
			
			List<SteamID> members = new List<SteamID>();
			try {
				do {
					page++;
					
					XmlDocument doc = new XmlDocument();
					doc.LoadUrl(BaseUrl + "/memberslistxml?p=" + page);
					
					totalPages = int.Parse(doc.GetInnerText("totalPages"));

					if (page == 1) {
						GroupID64 = long.Parse(doc.GetInnerText("groupID64"));
						if (cache) Cache();
					}
				
					foreach (XmlElement bla in doc.GetElementsByTagName("members").Item(0)) {
						members.Add(SteamID.Create(long.Parse(bla.InnerText), false));
					}
			
				} while (page < totalPages);
			} catch (XmlException) {
				return false;
			} catch (Exception e) {
				throw e;
			}
			
			this.members = members.ToArray();
			
			FetchTime = DateTime.Now;
			return true;
		}
		
		public int MemberCount {
			get {
				if (members == null) {
					XmlDocument doc = new XmlDocument();
					doc.LoadUrl(BaseUrl + "/memberslistxml");
					return int.Parse(doc.GetInnerText("memberCount"));
				} else {
					return members.Length;
				}
			}
		}
		
	}
}