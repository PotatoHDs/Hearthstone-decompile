using System;
using System.Collections.Generic;

// Token: 0x02000021 RID: 33
public class ChapterPageData : PageData
{
	// Token: 0x17000025 RID: 37
	// (get) Token: 0x060000E6 RID: 230 RVA: 0x000052D6 File Offset: 0x000034D6
	public override AdventureBookPageType PageType
	{
		get
		{
			return AdventureBookPageType.CHAPTER;
		}
	}

	// Token: 0x04000095 RID: 149
	public int ChapterNumber;

	// Token: 0x04000096 RID: 150
	public WingDbfRecord WingRecord;

	// Token: 0x04000097 RID: 151
	public List<ScenarioDbfRecord> ScenarioRecords = new List<ScenarioDbfRecord>();

	// Token: 0x04000098 RID: 152
	public int ChapterToFlipToWhenCompleted;
}
