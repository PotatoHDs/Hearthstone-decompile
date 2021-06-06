using System;

namespace Hearthstone.UI
{
	// Token: 0x02001015 RID: 4117
	public interface IStatefulWidgetComponent
	{
		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x0600B2EA RID: 45802
		bool IsChangingStates { get; }

		// Token: 0x0600B2EB RID: 45803
		void RegisterStartChangingStatesListener(Action<object> listener, object payload = null, bool callImmediatelyIfSet = true, bool doOnce = false);

		// Token: 0x0600B2EC RID: 45804
		void RemoveStartChangingStatesListener(Action<object> listener);

		// Token: 0x0600B2ED RID: 45805
		void RegisterDoneChangingStatesListener(Action<object> listener, object payload = null, bool callImmediatelyIfSet = true, bool doOnce = false);

		// Token: 0x0600B2EE RID: 45806
		void RemoveDoneChangingStatesListener(Action<object> listener);
	}
}
