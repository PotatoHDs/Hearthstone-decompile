using System;

// Token: 0x02000022 RID: 34
public class MapPageData : PageData
{
	// Token: 0x17000026 RID: 38
	// (get) Token: 0x060000E8 RID: 232 RVA: 0x000052EC File Offset: 0x000034EC
	public override AdventureBookPageType PageType
	{
		get
		{
			return AdventureBookPageType.MAP;
		}
	}

	// Token: 0x04000099 RID: 153
	public Map<int, ChapterPageData> ChapterData = new Map<int, ChapterPageData>();

	// Token: 0x0400009A RID: 154
	public string NumChaptersCompletedText;

	// Token: 0x0400009B RID: 155
	public int NumSectionsInBook;
}
