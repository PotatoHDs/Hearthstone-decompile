using System;
using Hearthstone.InGameMessage.UI;

namespace Hearthstone.InGameMessage
{
	// Token: 0x02001158 RID: 4440
	public interface IDataTranslator
	{
		// Token: 0x0600C2A9 RID: 49833
		MessageUIData CreateData(GameMessage message);
	}
}
