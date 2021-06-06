using Hearthstone.InGameMessage.UI;

namespace Hearthstone.InGameMessage
{
	public class TestDebugDataTranslator : IDataTranslator
	{
		public MessageUIData CreateData(GameMessage message)
		{
			return new MessageUIData
			{
				ContentType = MessageContentType.DEBUG,
				MessageData = new TestDebugMessageUIData
				{
					TestString = message.Title
				}
			};
		}
	}
}
