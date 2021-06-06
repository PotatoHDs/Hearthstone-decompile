using System.Collections.Generic;

public class ChapterPageData : PageData
{
	public int ChapterNumber;

	public WingDbfRecord WingRecord;

	public List<ScenarioDbfRecord> ScenarioRecords = new List<ScenarioDbfRecord>();

	public int ChapterToFlipToWhenCompleted;

	public override AdventureBookPageType PageType => AdventureBookPageType.CHAPTER;
}
