public abstract class PageData
{
	public AdventureDbId Adventure;

	public AdventureModeDbId AdventureMode;

	public const int NO_SECTION = -1;

	public abstract AdventureBookPageType PageType { get; }

	public int BookSection { get; set; }
}
