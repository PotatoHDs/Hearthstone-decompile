using System;

// Token: 0x02000023 RID: 35
public class RewardPageData : PageData
{
	// Token: 0x17000027 RID: 39
	// (get) Token: 0x060000EA RID: 234 RVA: 0x00005302 File Offset: 0x00003502
	public override AdventureBookPageType PageType
	{
		get
		{
			return AdventureBookPageType.REWARD;
		}
	}

	// Token: 0x0400009C RID: 156
	public Map<int, ChapterPageData> ChapterData = new Map<int, ChapterPageData>();
}
