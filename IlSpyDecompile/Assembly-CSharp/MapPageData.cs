public class MapPageData : PageData
{
	public Map<int, ChapterPageData> ChapterData = new Map<int, ChapterPageData>();

	public string NumChaptersCompletedText;

	public int NumSectionsInBook;

	public override AdventureBookPageType PageType => AdventureBookPageType.MAP;
}
