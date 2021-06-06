using System;
using System.Collections.Generic;
using bgs;
using Hearthstone;

// Token: 0x02000775 RID: 1909
public class BnetWhisperMgr
{
	// Token: 0x06006BE7 RID: 27623 RVA: 0x0022F730 File Offset: 0x0022D930
	public static BnetWhisperMgr Get()
	{
		if (BnetWhisperMgr.s_instance == null)
		{
			BnetWhisperMgr.s_instance = new BnetWhisperMgr();
			HearthstoneApplication.Get().WillReset += delegate()
			{
				BnetWhisperMgr.s_instance.m_whispers.Clear();
				BnetWhisperMgr.s_instance.m_whisperMap.Clear();
				BnetWhisperMgr.s_instance.m_firstPendingWhisperIndex = -1;
				BnetPresenceMgr.Get().RemovePlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(BnetWhisperMgr.Get().OnPlayersChanged));
			};
		}
		return BnetWhisperMgr.s_instance;
	}

	// Token: 0x06006BE8 RID: 27624 RVA: 0x0022F77C File Offset: 0x0022D97C
	public void Initialize()
	{
		Network.Get().SetWhisperHandler(new Network.WhisperHandler(this.OnWhispers));
		Network.Get().AddBnetErrorListener(BnetFeature.Whisper, new Network.BnetErrorCallback(this.OnBnetError));
	}

	// Token: 0x06006BE9 RID: 27625 RVA: 0x0022F7AB File Offset: 0x0022D9AB
	public void Shutdown()
	{
		Network.Get().RemoveBnetErrorListener(BnetFeature.Whisper, new Network.BnetErrorCallback(this.OnBnetError));
		Network.Get().SetWhisperHandler(null);
	}

	// Token: 0x06006BEA RID: 27626 RVA: 0x0022F7D0 File Offset: 0x0022D9D0
	public List<BnetWhisper> GetWhispersWithPlayer(BnetPlayer player)
	{
		if (player == null)
		{
			return null;
		}
		List<BnetWhisper> list = new List<BnetWhisper>();
		foreach (BnetGameAccountId key in player.GetGameAccounts().Keys)
		{
			List<BnetWhisper> collection;
			if (this.m_whisperMap.TryGetValue(key, out collection))
			{
				list.AddRange(collection);
			}
		}
		if (list.Count == 0)
		{
			return null;
		}
		list.Sort(delegate(BnetWhisper a, BnetWhisper b)
		{
			ulong timestampMicrosec = a.GetTimestampMicrosec();
			ulong timestampMicrosec2 = b.GetTimestampMicrosec();
			if (timestampMicrosec < timestampMicrosec2)
			{
				return -1;
			}
			if (timestampMicrosec > timestampMicrosec2)
			{
				return 1;
			}
			return 0;
		});
		return list;
	}

	// Token: 0x06006BEB RID: 27627 RVA: 0x0022F874 File Offset: 0x0022DA74
	public bool SendWhisper(BnetPlayer player, string message)
	{
		if (player == null)
		{
			return false;
		}
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		if (myPlayer == null || !myPlayer.IsOnline() || myPlayer.IsAppearingOffline())
		{
			return false;
		}
		BnetGameAccount bestGameAccount = player.GetBestGameAccount();
		if (bestGameAccount == null)
		{
			return false;
		}
		Network.SendWhisper(bestGameAccount.GetId(), message);
		return true;
	}

	// Token: 0x06006BEC RID: 27628 RVA: 0x0022F8C5 File Offset: 0x0022DAC5
	public bool HavePendingWhispers()
	{
		return this.m_firstPendingWhisperIndex >= 0;
	}

	// Token: 0x06006BED RID: 27629 RVA: 0x0022F8D3 File Offset: 0x0022DAD3
	public bool AddWhisperListener(BnetWhisperMgr.WhisperCallback callback)
	{
		return this.AddWhisperListener(callback, null);
	}

	// Token: 0x06006BEE RID: 27630 RVA: 0x0022F8E0 File Offset: 0x0022DAE0
	public bool AddWhisperListener(BnetWhisperMgr.WhisperCallback callback, object userData)
	{
		BnetWhisperMgr.WhisperListener whisperListener = new BnetWhisperMgr.WhisperListener();
		whisperListener.SetCallback(callback);
		whisperListener.SetUserData(userData);
		if (this.m_whisperListeners.Contains(whisperListener))
		{
			return false;
		}
		this.m_whisperListeners.Add(whisperListener);
		return true;
	}

	// Token: 0x06006BEF RID: 27631 RVA: 0x0022F91E File Offset: 0x0022DB1E
	public bool RemoveWhisperListener(BnetWhisperMgr.WhisperCallback callback)
	{
		return this.RemoveWhisperListener(callback, null);
	}

	// Token: 0x06006BF0 RID: 27632 RVA: 0x0022F928 File Offset: 0x0022DB28
	public bool RemoveWhisperListener(BnetWhisperMgr.WhisperCallback callback, object userData)
	{
		BnetWhisperMgr.WhisperListener whisperListener = new BnetWhisperMgr.WhisperListener();
		whisperListener.SetCallback(callback);
		whisperListener.SetUserData(userData);
		return this.m_whisperListeners.Remove(whisperListener);
	}

	// Token: 0x06006BF1 RID: 27633 RVA: 0x0022F958 File Offset: 0x0022DB58
	private void OnWhispers(BnetWhisper[] whispers)
	{
		for (int i = 0; i < whispers.Length; i++)
		{
			BnetWhisper bnetWhisper = whispers[i];
			this.m_whispers.Add(bnetWhisper);
			if (!this.HavePendingWhispers())
			{
				if (WhisperUtil.IsDisplayable(bnetWhisper))
				{
					this.ProcessWhisper(this.m_whispers.Count - 1);
				}
				else
				{
					this.m_firstPendingWhisperIndex = i;
					BnetPresenceMgr.Get().AddPlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged));
				}
			}
		}
	}

	// Token: 0x06006BF2 RID: 27634 RVA: 0x000052EC File Offset: 0x000034EC
	private bool OnBnetError(BnetErrorInfo info, object userData)
	{
		return true;
	}

	// Token: 0x06006BF3 RID: 27635 RVA: 0x0022F9C6 File Offset: 0x0022DBC6
	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		if (!this.CanProcessPendingWhispers())
		{
			return;
		}
		BnetPresenceMgr.Get().RemovePlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged));
		this.ProcessPendingWhispers();
	}

	// Token: 0x06006BF4 RID: 27636 RVA: 0x0022F9F0 File Offset: 0x0022DBF0
	private void FireWhisperEvent(BnetWhisper whisper)
	{
		BnetWhisperMgr.WhisperListener[] array = this.m_whisperListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(whisper);
		}
	}

	// Token: 0x06006BF5 RID: 27637 RVA: 0x0022FA20 File Offset: 0x0022DC20
	private bool CanProcessPendingWhispers()
	{
		if (this.m_firstPendingWhisperIndex < 0)
		{
			return true;
		}
		for (int i = this.m_firstPendingWhisperIndex; i < this.m_whispers.Count; i++)
		{
			if (!WhisperUtil.IsDisplayable(this.m_whispers[i]))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06006BF6 RID: 27638 RVA: 0x0022FA6C File Offset: 0x0022DC6C
	private void ProcessPendingWhispers()
	{
		if (this.m_firstPendingWhisperIndex < 0)
		{
			return;
		}
		for (int i = this.m_firstPendingWhisperIndex; i < this.m_whispers.Count; i++)
		{
			this.ProcessWhisper(i);
		}
		this.m_firstPendingWhisperIndex = -1;
	}

	// Token: 0x06006BF7 RID: 27639 RVA: 0x0022FAAC File Offset: 0x0022DCAC
	private void ProcessWhisper(int index)
	{
		BnetWhisper bnetWhisper = this.m_whispers[index];
		BnetGameAccountId theirGameAccountId = WhisperUtil.GetTheirGameAccountId(bnetWhisper);
		if (!BnetUtils.CanReceiveWhisperFrom(theirGameAccountId))
		{
			this.m_whispers.RemoveAt(index);
			return;
		}
		List<BnetWhisper> list;
		if (!this.m_whisperMap.TryGetValue(theirGameAccountId, out list))
		{
			list = new List<BnetWhisper>();
			this.m_whisperMap.Add(theirGameAccountId, list);
		}
		else if (list.Count == 100)
		{
			this.RemoveOldestWhisper(list);
		}
		list.Add(bnetWhisper);
		this.FireWhisperEvent(bnetWhisper);
	}

	// Token: 0x06006BF8 RID: 27640 RVA: 0x0022FB28 File Offset: 0x0022DD28
	private void RemoveOldestWhisper(List<BnetWhisper> whispers)
	{
		BnetWhisper item = whispers[0];
		whispers.RemoveAt(0);
		this.m_whispers.Remove(item);
	}

	// Token: 0x0400573B RID: 22331
	private const int MAX_WHISPERS_PER_PLAYER = 100;

	// Token: 0x0400573C RID: 22332
	private static BnetWhisperMgr s_instance;

	// Token: 0x0400573D RID: 22333
	private List<BnetWhisper> m_whispers = new List<BnetWhisper>();

	// Token: 0x0400573E RID: 22334
	private global::Map<BnetGameAccountId, List<BnetWhisper>> m_whisperMap = new global::Map<BnetGameAccountId, List<BnetWhisper>>();

	// Token: 0x0400573F RID: 22335
	private int m_firstPendingWhisperIndex = -1;

	// Token: 0x04005740 RID: 22336
	private List<BnetWhisperMgr.WhisperListener> m_whisperListeners = new List<BnetWhisperMgr.WhisperListener>();

	// Token: 0x02002346 RID: 9030
	// (Invoke) Token: 0x06012A67 RID: 76391
	public delegate void WhisperCallback(BnetWhisper whisper, object userData);

	// Token: 0x02002347 RID: 9031
	private class WhisperListener : global::EventListener<BnetWhisperMgr.WhisperCallback>
	{
		// Token: 0x06012A6A RID: 76394 RVA: 0x00511EDF File Offset: 0x005100DF
		public void Fire(BnetWhisper whisper)
		{
			this.m_callback(whisper, this.m_userData);
		}
	}
}
