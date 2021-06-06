using System;

namespace Hearthstone.UI
{
	public interface IStatefulWidgetComponent
	{
		bool IsChangingStates { get; }

		void RegisterStartChangingStatesListener(Action<object> listener, object payload = null, bool callImmediatelyIfSet = true, bool doOnce = false);

		void RemoveStartChangingStatesListener(Action<object> listener);

		void RegisterDoneChangingStatesListener(Action<object> listener, object payload = null, bool callImmediatelyIfSet = true, bool doOnce = false);

		void RemoveDoneChangingStatesListener(Action<object> listener);
	}
}
