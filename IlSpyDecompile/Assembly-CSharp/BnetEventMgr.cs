using System.Collections.Generic;
using bgs;

public class BnetEventMgr
{
	public delegate void ChangeCallback(BattleNet.BnetEvent stateChange, object userData);

	private class ChangeListener : EventListener<ChangeCallback>
	{
		public void Fire(BattleNet.BnetEvent stateChange)
		{
			m_callback(stateChange, m_userData);
		}
	}

	private static BnetEventMgr s_instance;

	private List<ChangeListener> m_changeListeners = new List<ChangeListener>();

	public static BnetEventMgr Get()
	{
		if (s_instance == null)
		{
			s_instance = new BnetEventMgr();
			s_instance.Initialize();
		}
		return s_instance;
	}

	public void Initialize()
	{
		Network.Get().SetBnetStateHandler(OnBnetEventsOccurred);
	}

	public void Shutdown()
	{
	}

	private void OnBnetEventsOccurred(BattleNet.BnetEvent[] bnetEvents)
	{
		foreach (BattleNet.BnetEvent stateChange in bnetEvents)
		{
			FireChangeEvent(stateChange);
		}
	}

	public bool AddChangeListener(ChangeCallback callback)
	{
		return AddChangeListener(callback, null);
	}

	public bool AddChangeListener(ChangeCallback callback, object userData)
	{
		ChangeListener changeListener = new ChangeListener();
		changeListener.SetCallback(callback);
		changeListener.SetUserData(userData);
		if (m_changeListeners.Contains(changeListener))
		{
			return false;
		}
		m_changeListeners.Add(changeListener);
		return true;
	}

	public bool RemoveChangeListener(ChangeCallback callback)
	{
		return RemoveChangeListener(callback, null);
	}

	public bool RemoveChangeListener(ChangeCallback callback, object userData)
	{
		ChangeListener changeListener = new ChangeListener();
		changeListener.SetCallback(callback);
		changeListener.SetUserData(userData);
		return m_changeListeners.Remove(changeListener);
	}

	private void FireChangeEvent(BattleNet.BnetEvent stateChange)
	{
		ChangeListener[] array = m_changeListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(stateChange);
		}
	}
}
