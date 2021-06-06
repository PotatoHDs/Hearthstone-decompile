using System;
using System.Collections.Generic;
using bgs;

// Token: 0x02000765 RID: 1893
public class BnetEventMgr
{
	// Token: 0x06006A21 RID: 27169 RVA: 0x0022907C File Offset: 0x0022727C
	public static BnetEventMgr Get()
	{
		if (BnetEventMgr.s_instance == null)
		{
			BnetEventMgr.s_instance = new BnetEventMgr();
			BnetEventMgr.s_instance.Initialize();
		}
		return BnetEventMgr.s_instance;
	}

	// Token: 0x06006A22 RID: 27170 RVA: 0x0022909E File Offset: 0x0022729E
	public void Initialize()
	{
		Network.Get().SetBnetStateHandler(new Network.BnetEventHandler(this.OnBnetEventsOccurred));
	}

	// Token: 0x06006A23 RID: 27171 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x06006A24 RID: 27172 RVA: 0x002290B8 File Offset: 0x002272B8
	private void OnBnetEventsOccurred(BattleNet.BnetEvent[] bnetEvents)
	{
		foreach (BattleNet.BnetEvent stateChange in bnetEvents)
		{
			this.FireChangeEvent(stateChange);
		}
	}

	// Token: 0x06006A25 RID: 27173 RVA: 0x002290E0 File Offset: 0x002272E0
	public bool AddChangeListener(BnetEventMgr.ChangeCallback callback)
	{
		return this.AddChangeListener(callback, null);
	}

	// Token: 0x06006A26 RID: 27174 RVA: 0x002290EC File Offset: 0x002272EC
	public bool AddChangeListener(BnetEventMgr.ChangeCallback callback, object userData)
	{
		BnetEventMgr.ChangeListener changeListener = new BnetEventMgr.ChangeListener();
		changeListener.SetCallback(callback);
		changeListener.SetUserData(userData);
		if (this.m_changeListeners.Contains(changeListener))
		{
			return false;
		}
		this.m_changeListeners.Add(changeListener);
		return true;
	}

	// Token: 0x06006A27 RID: 27175 RVA: 0x0022912A File Offset: 0x0022732A
	public bool RemoveChangeListener(BnetEventMgr.ChangeCallback callback)
	{
		return this.RemoveChangeListener(callback, null);
	}

	// Token: 0x06006A28 RID: 27176 RVA: 0x00229134 File Offset: 0x00227334
	public bool RemoveChangeListener(BnetEventMgr.ChangeCallback callback, object userData)
	{
		BnetEventMgr.ChangeListener changeListener = new BnetEventMgr.ChangeListener();
		changeListener.SetCallback(callback);
		changeListener.SetUserData(userData);
		return this.m_changeListeners.Remove(changeListener);
	}

	// Token: 0x06006A29 RID: 27177 RVA: 0x00229164 File Offset: 0x00227364
	private void FireChangeEvent(BattleNet.BnetEvent stateChange)
	{
		BnetEventMgr.ChangeListener[] array = this.m_changeListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(stateChange);
		}
	}

	// Token: 0x040056D4 RID: 22228
	private static BnetEventMgr s_instance;

	// Token: 0x040056D5 RID: 22229
	private List<BnetEventMgr.ChangeListener> m_changeListeners = new List<BnetEventMgr.ChangeListener>();

	// Token: 0x02002336 RID: 9014
	// (Invoke) Token: 0x06012A38 RID: 76344
	public delegate void ChangeCallback(BattleNet.BnetEvent stateChange, object userData);

	// Token: 0x02002337 RID: 9015
	private class ChangeListener : global::EventListener<BnetEventMgr.ChangeCallback>
	{
		// Token: 0x06012A3B RID: 76347 RVA: 0x00511BFC File Offset: 0x0050FDFC
		public void Fire(BattleNet.BnetEvent stateChange)
		{
			this.m_callback(stateChange, this.m_userData);
		}
	}
}
