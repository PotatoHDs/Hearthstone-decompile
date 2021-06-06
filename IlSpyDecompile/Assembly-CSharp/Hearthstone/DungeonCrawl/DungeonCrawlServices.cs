namespace Hearthstone.DungeonCrawl
{
	public class DungeonCrawlServices
	{
		public IDungeonCrawlData DungeonCrawlData { get; set; }

		public ISubsceneController SubsceneController { get; set; }

		public AssetLoadingHelper AssetLoadingHelper { get; set; }
	}
}
