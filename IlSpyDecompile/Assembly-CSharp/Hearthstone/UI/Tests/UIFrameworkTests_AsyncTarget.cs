using System;
using UnityEngine;

namespace Hearthstone.UI.Tests
{
	[AddComponentMenu("")]
	[ExecuteAlways]
	public class UIFrameworkTests_AsyncTarget : MonoBehaviour, IAsyncInitializationBehavior
	{
		private Action<object> m_callback;

		public bool IsReady { get; private set; }

		public bool IsActive { get; private set; }

		public Behaviour Container { get; private set; }

		public void RegisterActivatedListener(Action<object> listener, object payload = null)
		{
		}

		public void RegisterDeactivatedListener(Action<object> listener, object payload = null)
		{
		}

		public void RegisterReadyListener(Action<object> listener, object payload, bool callImmediatelyIfReady = true)
		{
			m_callback = listener;
		}

		public void RemoveReadyListener(Action<object> listener)
		{
			m_callback = null;
		}

		public void BecomeReady()
		{
			IsReady = true;
			if (m_callback != null)
			{
				m_callback(null);
			}
		}
	}
}
