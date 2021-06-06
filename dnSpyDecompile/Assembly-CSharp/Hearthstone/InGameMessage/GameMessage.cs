using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Hearthstone.InGameMessage
{
	// Token: 0x02001150 RID: 4432
	public class GameMessage
	{
		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x0600C21A RID: 49690 RVA: 0x003ADE88 File Offset: 0x003AC088
		// (set) Token: 0x0600C21B RID: 49691 RVA: 0x003ADE90 File Offset: 0x003AC090
		public string ContentType { get; set; }

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x0600C21C RID: 49692 RVA: 0x003ADE99 File Offset: 0x003AC099
		// (set) Token: 0x0600C21D RID: 49693 RVA: 0x003ADEA1 File Offset: 0x003AC0A1
		public string UID { get; set; }

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x0600C21E RID: 49694 RVA: 0x003ADEAA File Offset: 0x003AC0AA
		// (set) Token: 0x0600C21F RID: 49695 RVA: 0x003ADEB2 File Offset: 0x003AC0B2
		public string EntryTitle { get; set; }

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x0600C220 RID: 49696 RVA: 0x003ADEBB File Offset: 0x003AC0BB
		// (set) Token: 0x0600C221 RID: 49697 RVA: 0x003ADEC3 File Offset: 0x003AC0C3
		public string Title { get; set; }

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x0600C222 RID: 49698 RVA: 0x003ADECC File Offset: 0x003AC0CC
		// (set) Token: 0x0600C223 RID: 49699 RVA: 0x003ADED4 File Offset: 0x003AC0D4
		public string TextureUrl { get; set; }

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x0600C224 RID: 49700 RVA: 0x003ADEDD File Offset: 0x003AC0DD
		// (set) Token: 0x0600C225 RID: 49701 RVA: 0x003ADEE5 File Offset: 0x003AC0E5
		public string Link { get; set; }

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x0600C226 RID: 49702 RVA: 0x003ADEEE File Offset: 0x003AC0EE
		// (set) Token: 0x0600C227 RID: 49703 RVA: 0x003ADEF6 File Offset: 0x003AC0F6
		public string Effect { get; set; }

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x0600C228 RID: 49704 RVA: 0x003ADEFF File Offset: 0x003AC0FF
		// (set) Token: 0x0600C229 RID: 49705 RVA: 0x003ADF07 File Offset: 0x003AC107
		public int MaxViewCount { get; set; }

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x0600C22A RID: 49706 RVA: 0x003ADF10 File Offset: 0x003AC110
		// (set) Token: 0x0600C22B RID: 49707 RVA: 0x003ADF18 File Offset: 0x003AC118
		public DateTime PublishDate { get; set; }

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x0600C22C RID: 49708 RVA: 0x003ADF21 File Offset: 0x003AC121
		// (set) Token: 0x0600C22D RID: 49709 RVA: 0x003ADF29 File Offset: 0x003AC129
		public DateTime BeginningDate { get; set; }

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x0600C22E RID: 49710 RVA: 0x003ADF32 File Offset: 0x003AC132
		// (set) Token: 0x0600C22F RID: 49711 RVA: 0x003ADF3A File Offset: 0x003AC13A
		public DateTime ExpiryDate { get; set; }

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x0600C230 RID: 49712 RVA: 0x003ADF43 File Offset: 0x003AC143
		// (set) Token: 0x0600C231 RID: 49713 RVA: 0x003ADF4B File Offset: 0x003AC14B
		public int PriorityLevel { get; set; }

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x0600C232 RID: 49714 RVA: 0x003ADF54 File Offset: 0x003AC154
		// (set) Token: 0x0600C233 RID: 49715 RVA: 0x003ADF5C File Offset: 0x003AC15C
		public int GameVersion { get; set; }

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x0600C234 RID: 49716 RVA: 0x003ADF65 File Offset: 0x003AC165
		// (set) Token: 0x0600C235 RID: 49717 RVA: 0x003ADF6D File Offset: 0x003AC16D
		public int MinGameVersion { get; set; }

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x0600C236 RID: 49718 RVA: 0x003ADF76 File Offset: 0x003AC176
		// (set) Token: 0x0600C237 RID: 49719 RVA: 0x003ADF7E File Offset: 0x003AC17E
		public int MaxGameVersion { get; set; }

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x0600C238 RID: 49720 RVA: 0x003ADF87 File Offset: 0x003AC187
		// (set) Token: 0x0600C239 RID: 49721 RVA: 0x003ADF8F File Offset: 0x003AC18F
		public List<string> Platform { get; set; }

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x0600C23A RID: 49722 RVA: 0x003ADF98 File Offset: 0x003AC198
		// (set) Token: 0x0600C23B RID: 49723 RVA: 0x003ADFA0 File Offset: 0x003AC1A0
		public List<string> AndroidStore { get; set; }

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x0600C23C RID: 49724 RVA: 0x003ADFA9 File Offset: 0x003AC1A9
		// (set) Token: 0x0600C23D RID: 49725 RVA: 0x003ADFB1 File Offset: 0x003AC1B1
		public string GameAttrs { get; set; }

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x0600C23E RID: 49726 RVA: 0x003ADFBA File Offset: 0x003AC1BA
		// (set) Token: 0x0600C23F RID: 49727 RVA: 0x003ADFC2 File Offset: 0x003AC1C2
		public string DismissCond { get; set; }

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x0600C240 RID: 49728 RVA: 0x003ADFCB File Offset: 0x003AC1CB
		// (set) Token: 0x0600C241 RID: 49729 RVA: 0x003ADFD3 File Offset: 0x003AC1D3
		public string LayoutType { get; set; }

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x0600C242 RID: 49730 RVA: 0x003ADFDC File Offset: 0x003AC1DC
		// (set) Token: 0x0600C243 RID: 49731 RVA: 0x003ADFE4 File Offset: 0x003AC1E4
		public string DisplayImageType { get; set; }

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x0600C244 RID: 49732 RVA: 0x003ADFED File Offset: 0x003AC1ED
		// (set) Token: 0x0600C245 RID: 49733 RVA: 0x003ADFF5 File Offset: 0x003AC1F5
		public long ProductID { get; set; }

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x0600C246 RID: 49734 RVA: 0x003ADFFE File Offset: 0x003AC1FE
		// (set) Token: 0x0600C247 RID: 49735 RVA: 0x003AE006 File Offset: 0x003AC206
		public string TextBody { get; set; }

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x0600C248 RID: 49736 RVA: 0x003AE00F File Offset: 0x003AC20F
		// (set) Token: 0x0600C249 RID: 49737 RVA: 0x003AE017 File Offset: 0x003AC217
		public bool OpenFullShop { get; set; }

		// Token: 0x0600C24A RID: 49738 RVA: 0x003AE020 File Offset: 0x003AC220
		public static int CompareByOrder(GameMessage a, GameMessage b)
		{
			int num = a.PriorityLevel - b.PriorityLevel;
			if (num != 0)
			{
				return num;
			}
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

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x0600C24B RID: 49739 RVA: 0x003AE06C File Offset: 0x003AC26C
		public byte[] HashValue
		{
			get
			{
				if (this.m_hash == null)
				{
					string dataToHash = string.Concat(new string[]
					{
						this.UID,
						this.Title,
						this.Link,
						this.BeginningDate.ToString(),
						this.ExpiryDate.ToString()
					});
					this.m_textGroups.ForEach(delegate(TextGroup t)
					{
						dataToHash += t.Text;
					});
					using (SHA256 sha = SHA256.Create())
					{
						this.m_hash = sha.ComputeHash(Encoding.UTF8.GetBytes(dataToHash));
					}
				}
				return this.m_hash;
			}
		}

		// Token: 0x04009C7F RID: 40063
		public List<TextGroup> m_textGroups = new List<TextGroup>();

		// Token: 0x04009C94 RID: 40084
		private byte[] m_hash;
	}
}
