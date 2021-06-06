using Hearthstone.InGameMessage.UI;

namespace Hearthstone.InGameMessage
{
	public interface IDataTranslator
	{
		MessageUIData CreateData(GameMessage message);
	}
}
