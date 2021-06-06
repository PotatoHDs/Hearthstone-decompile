using System;
using System.Collections.Generic;

namespace Hearthstone.UI.Internal
{
	[Serializable]
	public struct FlagStateTracker
	{
		[Serializable]
		private struct Listener
		{
			public Action<object> Action;

			public object Context;

			public bool DoOnce;
		}

		private List<Listener> m_listeners;

		public bool IsSet { get; private set; }

		public void RegisterSetListener(Action<object> listener, object context = null, bool callImmediatelyIfSet = true, bool doOnce = false)
		{
			if (IsSet && callImmediatelyIfSet)
			{
				listener(context);
				if (doOnce)
				{
					return;
				}
			}
			if (m_listeners == null)
			{
				m_listeners = new List<Listener>();
			}
			int num = -1;
			for (int i = 0; i < m_listeners.Count; i++)
			{
				if (m_listeners[i].Action == listener)
				{
					num = i;
					break;
				}
			}
			if (num >= 0)
			{
				Listener value = m_listeners[num];
				value.Context = context;
				m_listeners[num] = value;
			}
			else
			{
				m_listeners.Add(new Listener
				{
					Action = listener,
					Context = context,
					DoOnce = doOnce
				});
			}
		}

		public void RemoveSetListener(Action<object> listener)
		{
			if (m_listeners == null)
			{
				return;
			}
			for (int i = 0; i < m_listeners.Count; i++)
			{
				if (m_listeners[i].Action == listener)
				{
					m_listeners.RemoveAt(i);
					break;
				}
			}
		}

		public void RemoveAllSetListeners()
		{
			if (m_listeners != null)
			{
				m_listeners.Clear();
			}
		}

		public bool SetAndDispatch()
		{
			IsSet = true;
			if (m_listeners != null)
			{
				for (int i = 0; i < m_listeners.Count; i++)
				{
					Listener listener = m_listeners[i];
					if (listener.Action != null)
					{
						listener.Action(listener.Context);
						if (listener.DoOnce)
						{
							m_listeners.RemoveAt(i);
							i--;
						}
					}
				}
			}
			return IsSet;
		}

		public bool SetWithoutDispatch()
		{
			IsSet = true;
			return IsSet;
		}

		public void Clear()
		{
			IsSet = false;
		}
	}
}
