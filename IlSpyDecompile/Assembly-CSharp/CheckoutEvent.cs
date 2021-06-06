using System;
using System.Collections.Generic;

public class CheckoutEvent
{
	private List<Action> m_listeners;

	public void Fire()
	{
		if (m_listeners == null)
		{
			return;
		}
		foreach (Action listener in m_listeners)
		{
			listener?.Invoke();
		}
	}

	public void AddListener(Action listener)
	{
		if (m_listeners == null)
		{
			m_listeners = new List<Action>();
		}
		if (!m_listeners.Contains(listener))
		{
			m_listeners.Add(listener);
		}
	}

	public void RemoveListener(Action listener)
	{
		if (m_listeners != null)
		{
			m_listeners.Remove(listener);
		}
	}
}
public class CheckoutEvent<T>
{
	private List<Action<T>> m_listeners;

	public void Fire(T obj)
	{
		if (m_listeners == null)
		{
			return;
		}
		foreach (Action<T> listener in m_listeners)
		{
			listener?.Invoke(obj);
		}
	}

	public void AddListener(Action<T> listener)
	{
		if (m_listeners == null)
		{
			m_listeners = new List<Action<T>>();
		}
		if (!m_listeners.Contains(listener))
		{
			m_listeners.Add(listener);
		}
	}

	public void RemoveListener(Action<T> listener)
	{
		if (m_listeners != null)
		{
			m_listeners.Remove(listener);
		}
	}
}
