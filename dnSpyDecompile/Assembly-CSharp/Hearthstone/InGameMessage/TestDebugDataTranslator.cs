using System;
using Hearthstone.InGameMessage.UI;

namespace Hearthstone.InGameMessage
{
	// Token: 0x0200115E RID: 4446
	public class TestDebugDataTranslator : IDataTranslator
	{
		// Token: 0x0600C2CA RID: 49866 RVA: 0x003B00CD File Offset: 0x003AE2CD
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
