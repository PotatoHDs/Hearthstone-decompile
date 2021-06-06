public class RewardPageData : PageData
{
	public Map<int, ChapterPageData> ChapterData = new Map<int, ChapterPageData>();

	public override AdventureBookPageType PageType => AdventureBookPageType.REWARD;
}
