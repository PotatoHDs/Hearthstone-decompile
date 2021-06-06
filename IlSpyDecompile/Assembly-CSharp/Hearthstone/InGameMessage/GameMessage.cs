using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Hearthstone.InGameMessage
{
	public class GameMessage
	{
		public List<TextGroup> m_textGroups = new List<TextGroup>();

		private byte[] m_hash;

		public string ContentType { get; set; }

		public string UID { get; set; }

		public string EntryTitle { get; set; }

		public string Title { get; set; }

		public string TextureUrl { get; set; }

		public string Link { get; set; }

		public string Effect { get; set; }

		public int MaxViewCount { get; set; }

		public DateTime PublishDate { get; set; }

		public DateTime BeginningDate { get; set; }

		public DateTime ExpiryDate { get; set; }

		public int PriorityLevel { get; set; }

		public int GameVersion { get; set; }

		public int MinGameVersion { get; set; }

		public int MaxGameVersion { get; set; }

		public List<string> Platform { get; set; }

		public List<string> AndroidStore { get; set; }

		public string GameAttrs { get; set; }

		public string DismissCond { get; set; }

		public string LayoutType { get; set; }

		public string DisplayImageType { get; set; }

		public long ProductID { get; set; }

		public string TextBody { get; set; }

		public bool OpenFullShop { get; set; }

		public byte[] HashValue
		{
			get
			{
				if (m_hash == null)
				{
					string dataToHash = UID + Title + Link + BeginningDate.ToString() + ExpiryDate.ToString();
					m_textGroups.ForEach(delegate(TextGroup t)
					{
						dataToHash += t.Text;
					});
					using SHA256 sHA = SHA256.Create();
					m_hash = sHA.ComputeHash(Encoding.UTF8.GetBytes(dataToHash));
				}
				return m_hash;
			}
		}

		public static int CompareByOrder(GameMessage a, GameMessage b)
		{
			int num = a.PriorityLevel - b.PriorityLevel;
			if (num == 0)
			{
				if (a.PublishDate < b.PublishDate)
				{
					return 1;
				}
				if (a.PublishDate > b.PublishDate)
				{
					return -1;
				}
				return 0;
			}
			return num;
		}
	}
}
