using System.Collections.Generic;
using bgs;
using Hearthstone;

public class FriendMgr
{
	public delegate void RecentOpponentCallback(BnetPlayer recentOpponent, object userData);

	private class RecentOpponentListener : EventListener<RecentOpponentCallback>
	{
		public void Fire(BnetPlayer recentOpponent)
		{
			m_callback(recentOpponent, m_userData);
		}
	}

	private static FriendMgr s_instance;

	private BnetPlayer m_selectedFriend;

	private BnetPlayer m_recentOpponent;

	private bool m_friendListScrollEnabled;

	private float m_friendListScrollCamPosY;

	private List<RecentOpponentListener> m_recentOpponentListeners = new List<RecentOpponentListener>();

	public static FriendMgr Get()
	{
		if (s_instance == null)
		{
			s_instance = new FriendMgr();
			HearthstoneApplication.Get().WillReset += s_instance.WillReset;
		}
		return s_instance;
	}

	public BnetPlayer GetSelectedFriend()
	{
		return m_selectedFriend;
	}

	public void SetSelectedFriend(BnetPlayer friend)
	{
		m_selectedFriend = friend;
	}

	public bool IsFriendListScrollEnabled()
	{
		return m_friendListScrollEnabled;
	}

	public void SetFriendListScrollEnabled(bool enabled)
	{
		m_friendListScrollEnabled = enabled;
	}

	public float GetFriendListScrollCamPosY()
	{
		return m_friendListScrollCamPosY;
	}

	public void SetFriendListScrollCamPosY(float y)
	{
		m_friendListScrollCamPosY = y;
	}

	public BnetPlayer GetRecentOpponent()
	{
		return m_recentOpponent;
	}

	private void UpdateRecentOpponent()
	{
		if (SpectatorManager.Get().IsSpectatingOrWatching || GameState.Get() == null)
		{
			return;
		}
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		if (opposingSidePlayer != null)
		{
			BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(opposingSidePlayer.GetGameAccountId());
			if (player != null && m_recentOpponent != player)
			{
				m_recentOpponent = player;
				FireRecentOpponentEvent(m_recentOpponent);
			}
		}
	}

	public void AddRecentOpponentListener(RecentOpponentCallback callback)
	{
		RecentOpponentListener recentOpponentListener = new RecentOpponentListener();
		recentOpponentListener.SetCallback(callback);
		recentOpponentListener.SetUserData(null);
		if (!m_recentOpponentListeners.Contains(recentOpponentListener))
		{
			m_recentOpponentListeners.Add(recentOpponentListener);
		}
	}

	public bool RemoveRecentOpponentListener(RecentOpponentCallback callback)
	{
		RecentOpponentListener recentOpponentListener = new RecentOpponentListener();
		recentOpponentListener.SetCallback(callback);
		recentOpponentListener.SetUserData(null);
		return m_recentOpponentListeners.Remove(recentOpponentListener);
	}

	public void FireRecentOpponentEvent(BnetPlayer recentOpponent)
	{
		RecentOpponentListener[] array = m_recentOpponentListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(recentOpponent);
		}
	}

	public void Initialize()
	{
		BnetFriendMgr.Get().AddChangeListener(OnFriendsChanged);
		Network.Get().AddBnetErrorListener(BnetFeature.Friends, OnBnetError);
		BnetPresenceMgr.Get().AddPlayersChangedListener(OnPlayersChanged);
		SceneMgr.Get().RegisterSceneLoadedEvent(OnSceneLoaded);
	}

	private void OnFriendsChanged(BnetFriendChangelist changelist, object userData)
	{
		List<BnetPlayer> removedFriends = changelist.GetRemovedFriends();
		if (removedFriends != null && removedFriends.Contains(m_selectedFriend))
		{
			m_selectedFriend = null;
		}
	}

	private bool OnBnetError(BnetErrorInfo info, object userData)
	{
		BnetFeature feature = info.GetFeature();
		BnetFeatureEvent featureEvent = info.GetFeatureEvent();
		if (feature == BnetFeature.Friends && featureEvent == BnetFeatureEvent.Friends_OnSendInvitation)
		{
			switch (info.GetError())
			{
			case BattleNetErrors.ERROR_OK:
			{
				string message = GameStrings.Get("GLOBAL_ADDFRIEND_SENT_CONFIRMATION");
				UIStatus.Get().AddInfo(message);
				return true;
			}
			case BattleNetErrors.ERROR_FRIENDS_FRIENDSHIP_ALREADY_EXISTS:
			{
				string message = GameStrings.Get("GLOBAL_ADDFRIEND_ERROR_ALREADY_FRIEND");
				UIStatus.Get().AddError(message);
				return true;
			}
			}
		}
		return false;
	}

	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		BnetPlayerChange bnetPlayerChange = changelist.FindChange(m_selectedFriend);
		if (bnetPlayerChange != null)
		{
			BnetPlayer oldPlayer = bnetPlayerChange.GetOldPlayer();
			BnetPlayer newPlayer = bnetPlayerChange.GetNewPlayer();
			if (oldPlayer == null || oldPlayer.IsOnline() != newPlayer.IsOnline())
			{
				m_selectedFriend = null;
			}
		}
	}

	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (mode == SceneMgr.Mode.GAMEPLAY)
		{
			GameState gameState = GameState.Get();
			if (gameState == null)
			{
				Log.All.PrintWarning("FriendMgr.OnSceneLoaded event was fired when GameState was null!");
				gameState = GameState.Initialize();
			}
			gameState?.RegisterGameOverListener(OnGameOver);
		}
	}

	private void OnGameOver(TAG_PLAYSTATE playState, object userData)
	{
		GameState.Get().UnregisterGameOverListener(OnGameOver);
		UpdateRecentOpponent();
	}

	private void WillReset()
	{
		BnetFriendMgr.Get().RemoveChangeListener(OnFriendsChanged);
		Network.Get().RemoveBnetErrorListener(BnetFeature.Friends, OnBnetError);
		BnetPresenceMgr.Get().RemovePlayersChangedListener(OnPlayersChanged);
		SceneMgr.Get().UnregisterSceneLoadedEvent(OnSceneLoaded);
	}
}
