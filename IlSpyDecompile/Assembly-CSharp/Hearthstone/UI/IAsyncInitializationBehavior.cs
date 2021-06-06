using System;
using UnityEngine;

namespace Hearthstone.UI
{
	public interface IAsyncInitializationBehavior
	{
		bool IsReady { get; }

		bool IsActive { get; }

		Behaviour Container { get; }

		void RegisterActivatedListener(Action<object> listener, object payload = null);

		void RegisterDeactivatedListener(Action<object> listener, object payload = null);

		void RegisterReadyListener(Action<object> listener, object payload = null, bool callImmediatelyIfReady = true);

		void RemoveReadyListener(Action<object> listener);
	}
}
