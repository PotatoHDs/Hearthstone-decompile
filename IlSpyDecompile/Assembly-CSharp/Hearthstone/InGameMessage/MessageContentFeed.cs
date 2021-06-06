namespace Hearthstone.InGameMessage
{
	public class MessageContentFeed
	{
		public string ContentId { get; set; }

		public IDataTranslator DataTranslator { get; set; }
	}
}
