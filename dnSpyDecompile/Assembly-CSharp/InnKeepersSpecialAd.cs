using System;
using System.Security.Cryptography;
using System.Text;

// Token: 0x02000632 RID: 1586
public class InnKeepersSpecialAd
{
	// Token: 0x17000531 RID: 1329
	// (get) Token: 0x06005936 RID: 22838 RVA: 0x001D1EBC File Offset: 0x001D00BC
	// (set) Token: 0x06005937 RID: 22839 RVA: 0x001D1EC4 File Offset: 0x001D00C4
	public string GameAction { get; set; }

	// Token: 0x17000532 RID: 1330
	// (get) Token: 0x06005938 RID: 22840 RVA: 0x001D1ECD File Offset: 0x001D00CD
	// (set) Token: 0x06005939 RID: 22841 RVA: 0x001D1ED5 File Offset: 0x001D00D5
	public int MaxViewCount { get; set; }

	// Token: 0x17000533 RID: 1331
	// (get) Token: 0x0600593A RID: 22842 RVA: 0x001D1EDE File Offset: 0x001D00DE
	// (set) Token: 0x0600593B RID: 22843 RVA: 0x001D1EE6 File Offset: 0x001D00E6
	public int CurrentViewCount { get; set; }

	// Token: 0x17000534 RID: 1332
	// (get) Token: 0x0600593C RID: 22844 RVA: 0x001D1EEF File Offset: 0x001D00EF
	// (set) Token: 0x0600593D RID: 22845 RVA: 0x001D1EF7 File Offset: 0x001D00F7
	public int Importance { get; set; }

	// Token: 0x17000535 RID: 1333
	// (get) Token: 0x0600593E RID: 22846 RVA: 0x001D1F00 File Offset: 0x001D0100
	// (set) Token: 0x0600593F RID: 22847 RVA: 0x001D1F08 File Offset: 0x001D0108
	public long PublishDate { get; set; }

	// Token: 0x17000536 RID: 1334
	// (get) Token: 0x06005940 RID: 22848 RVA: 0x001D1F11 File Offset: 0x001D0111
	// (set) Token: 0x06005941 RID: 22849 RVA: 0x001D1F19 File Offset: 0x001D0119
	public string CampaignName { get; set; }

	// Token: 0x17000537 RID: 1335
	// (get) Token: 0x06005942 RID: 22850 RVA: 0x001D1F22 File Offset: 0x001D0122
	// (set) Token: 0x06005943 RID: 22851 RVA: 0x001D1F2A File Offset: 0x001D012A
	public string Link { get; set; }

	// Token: 0x17000538 RID: 1336
	// (get) Token: 0x06005944 RID: 22852 RVA: 0x001D1F33 File Offset: 0x001D0133
	// (set) Token: 0x06005945 RID: 22853 RVA: 0x001D1F3B File Offset: 0x001D013B
	public string Contents { get; set; }

	// Token: 0x17000539 RID: 1337
	// (get) Token: 0x06005946 RID: 22854 RVA: 0x001D1F44 File Offset: 0x001D0144
	// (set) Token: 0x06005947 RID: 22855 RVA: 0x001D1F4C File Offset: 0x001D014C
	public string Title { get; set; }

	// Token: 0x1700053A RID: 1338
	// (get) Token: 0x06005948 RID: 22856 RVA: 0x001D1F55 File Offset: 0x001D0155
	// (set) Token: 0x06005949 RID: 22857 RVA: 0x001D1F5D File Offset: 0x001D015D
	public string SubTitle { get; set; }

	// Token: 0x1700053B RID: 1339
	// (get) Token: 0x0600594A RID: 22858 RVA: 0x001D1F66 File Offset: 0x001D0166
	// (set) Token: 0x0600594B RID: 22859 RVA: 0x001D1F6E File Offset: 0x001D016E
	public string ButtonText { get; set; }

	// Token: 0x1700053C RID: 1340
	// (get) Token: 0x0600594C RID: 22860 RVA: 0x001D1F77 File Offset: 0x001D0177
	// (set) Token: 0x0600594D RID: 22861 RVA: 0x001D1F7F File Offset: 0x001D017F
	public int TitleOffsetX { get; set; }

	// Token: 0x1700053D RID: 1341
	// (get) Token: 0x0600594E RID: 22862 RVA: 0x001D1F88 File Offset: 0x001D0188
	// (set) Token: 0x0600594F RID: 22863 RVA: 0x001D1F90 File Offset: 0x001D0190
	public int TitleOffsetY { get; set; }

	// Token: 0x1700053E RID: 1342
	// (get) Token: 0x06005950 RID: 22864 RVA: 0x001D1F99 File Offset: 0x001D0199
	// (set) Token: 0x06005951 RID: 22865 RVA: 0x001D1FA1 File Offset: 0x001D01A1
	public int SubTitleOffsetX { get; set; }

	// Token: 0x1700053F RID: 1343
	// (get) Token: 0x06005952 RID: 22866 RVA: 0x001D1FAA File Offset: 0x001D01AA
	// (set) Token: 0x06005953 RID: 22867 RVA: 0x001D1FB2 File Offset: 0x001D01B2
	public int SubTitleOffsetY { get; set; }

	// Token: 0x17000540 RID: 1344
	// (get) Token: 0x06005954 RID: 22868 RVA: 0x001D1FBB File Offset: 0x001D01BB
	// (set) Token: 0x06005955 RID: 22869 RVA: 0x001D1FC3 File Offset: 0x001D01C3
	public int TitleFontSize { get; set; }

	// Token: 0x17000541 RID: 1345
	// (get) Token: 0x06005956 RID: 22870 RVA: 0x001D1FCC File Offset: 0x001D01CC
	// (set) Token: 0x06005957 RID: 22871 RVA: 0x001D1FD4 File Offset: 0x001D01D4
	public int SubTitleFontSize { get; set; }

	// Token: 0x17000542 RID: 1346
	// (get) Token: 0x06005958 RID: 22872 RVA: 0x001D1FDD File Offset: 0x001D01DD
	// (set) Token: 0x06005959 RID: 22873 RVA: 0x001D1FE5 File Offset: 0x001D01E5
	public string ImageUrl { get; set; }

	// Token: 0x17000543 RID: 1347
	// (get) Token: 0x0600595A RID: 22874 RVA: 0x001D1FEE File Offset: 0x001D01EE
	// (set) Token: 0x0600595B RID: 22875 RVA: 0x001D1FF6 File Offset: 0x001D01F6
	public string ClientVersion { get; set; }

	// Token: 0x17000544 RID: 1348
	// (get) Token: 0x0600595C RID: 22876 RVA: 0x001D1FFF File Offset: 0x001D01FF
	// (set) Token: 0x0600595D RID: 22877 RVA: 0x001D2007 File Offset: 0x001D0207
	public string Platform { get; set; }

	// Token: 0x17000545 RID: 1349
	// (get) Token: 0x0600595E RID: 22878 RVA: 0x001D2010 File Offset: 0x001D0210
	// (set) Token: 0x0600595F RID: 22879 RVA: 0x001D2018 File Offset: 0x001D0218
	public string AndroidStore { get; set; }

	// Token: 0x17000546 RID: 1350
	// (get) Token: 0x06005960 RID: 22880 RVA: 0x001D2021 File Offset: 0x001D0221
	// (set) Token: 0x06005961 RID: 22881 RVA: 0x001D2029 File Offset: 0x001D0229
	public bool Visibility { get; set; }

	// Token: 0x06005962 RID: 22882 RVA: 0x001D2034 File Offset: 0x001D0234
	public static int ComparisonDescending(InnKeepersSpecialAd a, InnKeepersSpecialAd b)
	{
		if (a.Importance < b.Importance)
		{
			return 1;
		}
		if (a.Importance > b.Importance)
		{
			return -1;
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

	// Token: 0x06005963 RID: 22883 RVA: 0x001D2084 File Offset: 0x001D0284
	public string GetHash()
	{
		string s = string.Concat(new string[]
		{
			this.GameAction,
			this.Contents,
			this.Title,
			this.SubTitle,
			this.ButtonText,
			this.Link
		});
		string result;
		using (SHA256 sha = SHA256.Create())
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in sha.ComputeHash(Encoding.UTF8.GetBytes(s)))
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			result = stringBuilder.ToString();
		}
		return result;
	}
}
