using System.Collections.Generic;
using bgs;
using Hearthstone;

public class BnetWhisperMgr
{
	public delegate void WhisperCallback(BnetWhisper whisper, object userData);

	private class WhisperListener : EventListener<WhisperCallback>
	{
		public void Fire(BnetWhisper whisper)
		{
			m_callback(whisper, m_userData);
		}
	}

	private const int MAX_WHISPERS_PER_PLAYER = 100;

	private static BnetWhisperMgr s_instance;

	private List<BnetWhisper> m_whispers = new List<BnetWhisper>();

	private Map<BnetGameAccountId, List<BnetWhisper>> m_whisperMap = new Map<BnetGameAccountId, List<BnetWhisper>>();

	private int m_firstPendingWhisperIndex = -1;

	private List<WhisperListener> m_whisperListeners = new List<WhisperListener>();

	public static BnetWhisperMgr Get()
	{
		if (s_instance == null)
		{
			s_instance = new BnetWhisperMgr();
			HearthstoneApplication.Get().WillReset += delegate
			{
				s_instance.m_whispers.Clear();
				s_instance.m_whisperMap.Clear();
				s_instance.m_firstPendingWhisperIndex = -1;
				BnetPresenceMgr.Get().RemovePlayersChangedListener(Get().OnPlayersChanged);
			};
		}
		return s_instance;
	}

	public void Initialize()
	{
		Network.Get().SetWhisperHandler(OnWhispers);
		Network.Get().AddBnetErrorListener(BnetFeature.Whisper, OnBnetError);
	}

	public void Shutdown()
	{
		Network.Get().RemoveBnetErrorListener(BnetFeature.Whisper, OnBnetError);
		Network.Get().SetWhisperHandler(null);
	}

	public List<BnetWhisper> GetWhispersWithPlayer(BnetPlayer player)
	{
		if (player == null)
		{
			return null;
		}
		List<BnetWhisper> list = new List<BnetWhisper>();
		foreach (BnetGameAccountId key in player.GetGameAccounts().Keys)
		{
			if (m_whisperMap.TryGetValue(key, out var value))
			{
				list.AddRange(value);
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
			return (timestampMicrosec > timestampMicrosec2) ? 1 : 0;
		});
		return list;
	}

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

	public bool HavePendingWhispers()
	{
		return m_firstPendingWhisperIndex >= 0;
	}

	public bool AddWhisperListener(WhisperCallback callback)
	{
		return AddWhisperListener(callback, null);
	}

	public bool AddWhisperListener(WhisperCallback callback, object userData)
	{
		WhisperListener whisperListener = new WhisperListener();
		whisperListener.SetCallback(callback);
		whisperListener.SetUserData(userData);
		if (m_whisperListeners.Contains(whisperListener))
		{
			return false;
		}
		m_whisperListeners.Add(whisperListener);
		return true;
	}

	public bool RemoveWhisperListener(WhisperCallback callback)
	{
		return RemoveWhisperListener(callback, null);
	}

	public bool RemoveWhisperListener(WhisperCallback callback, object userData)
	{
		WhisperListener whisperListener = new WhisperListener();
		whisperListener.SetCallback(callback);
		whisperListener.SetUserData(userData);
		return m_whisperListeners.Remove(whisperListener);
	}

	private void OnWhispers(BnetWhisper[] whispers)
	{
		for (int i = 0; i < whispers.Length; i++)
		{
			BnetWhisper bnetWhisper = whispers[i];
			m_whispers.Add(bnetWhisper);
			if (!HavePendingWhispers())
			{
				if (WhisperUtil.IsDisplayable(bnetWhisper))
				{
					ProcessWhisper(m_whispers.Count - 1);
					continue;
				}
				m_firstPendingWhisperIndex = i;
				BnetPresenceMgr.Get().AddPlayersChangedListener(OnPlayersChanged);
			}
		}
	}

	private bool OnBnetError(BnetErrorInfo info, object userData)
	{
		return true;
	}

	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		if (CanProcessPendingWhispers())
		{
			BnetPresenceMgr.Get().RemovePlayersChangedListener(OnPlayersChanged);
			ProcessPendingWhispers();
		}
	}

	private void FireWhisperEvent(BnetWhisper whisper)
	{
		WhisperListener[] array = m_whisperListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(whisper);
		}
	}

	private bool CanProcessPendingWhispers()
	{
		if (m_firstPendingWhisperIndex < 0)
		{
			return true;
		}
		for (int i = m_firstPendingWhisperIndex; i < m_whispers.Count; i++)
		{
			if (!WhisperUtil.IsDisplayable(m_whispers[i]))
			{
				return false;
			}
		}
		return true;
	}

	private void ProcessPendingWhispers()
	{
		if (m_firstPendingWhisperIndex >= 0)
		{
			for (int i = m_firstPendingWhisperIndex; i < m_whispers.Count; i++)
			{
				ProcessWhisper(i);
			}
			m_firstPendingWhisperIndex = -1;
		}
	}

	private void ProcessWhisper(int index)
	{
		BnetWhisper bnetWhisper = m_whispers[index];
		BnetGameAccountId theirGameAccountId = WhisperUtil.GetTheirGameAccountId(bnetWhisper);
		if (!BnetUtils.CanReceiveWhisperFrom(theirGameAccountId))
		{
			m_whispers.RemoveAt(index);
			return;
		}
		if (!m_whisperMap.TryGetValue(theirGameAccountId, out var value))
		{
			value = new List<BnetWhisper>();
			m_whisperMap.Add(theirGameAccountId, value);
		}
		else if (value.Count == 100)
		{
			RemoveOldestWhisper(value);
		}
		value.Add(bnetWhisper);
		FireWhisperEvent(bnetWhisper);
	}

	private void RemoveOldestWhisper(List<BnetWhisper> whispers)
	{
		BnetWhisper item = whispers[0];
		whispers.RemoveAt(0);
		m_whispers.Remove(item);
	}
}
