using System;

namespace Hearthstone.UI
{
	// Token: 0x02001019 RID: 4121
	public interface IWidgetEventListener
	{
		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x0600B2F4 RID: 45812
		WidgetTemplate OwningWidget { get; }

		// Token: 0x0600B2F5 RID: 45813
		WidgetEventListenerResponse EventReceived(string eventName);
	}
}
