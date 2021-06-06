using System;
using System.Collections.Generic;
using bgs;
using Hearthstone;

// Token: 0x02000096 RID: 150
public class FriendMgr
{
	// Token: 0x0600096E RID: 2414 RVA: 0x000370FC File Offset: 0x000352FC
	public static FriendMgr Get()
	{
		if (FriendMgr.s_instance == null)
		{
			FriendMgr.s_instance = new FriendMgr();
			HearthstoneApplication.Get().WillReset += FriendMgr.s_instance.WillReset;
		}
		return FriendMgr.s_instance;
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x0003712E File Offset: 0x0003532E
	public BnetPlayer GetSelectedFriend()
	{
		return this.m_selectedFriend;
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x00037136 File Offset: 0x00035336
	public void SetSelectedFriend(BnetPlayer friend)
	{
		this.m_selectedFriend = friend;
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x0003713F File Offset: 0x0003533F
	public bool IsFriendListScrollEnabled()
	{
		return this.m_friendListScrollEnabled;
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x00037147 File Offset: 0x00035347
	public void SetFriendListScrollEnabled(bool enabled)
	{
		this.m_friendListScrollEnabled = enabled;
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x00037150 File Offset: 0x00035350
	public float GetFriendListScrollCamPosY()
	{
		return this.m_friendListScrollCamPosY;
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x00037158 File Offset: 0x00035358
	public void SetFriendListScrollCamPosY(float y)
	{
		this.m_friendListScrollCamPosY = y;
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x00037161 File Offset: 0x00035361
	public BnetPlayer GetRecentOpponent()
	{
		return this.m_recentOpponent;
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x0003716C File Offset: 0x0003536C
	private void UpdateRecentOpponent()
	{
		if (SpectatorManager.Get().IsSpectatingOrWatching)
		{
			return;
		}
		if (GameState.Get() == null)
		{
			return;
		}
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		if (opposingSidePlayer == null)
		{
			return;
		}
		BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(opposingSidePlayer.GetGameAccountId());
		if (player == null)
		{
			return;
		}
		if (this.m_recentOpponent == player)
		{
			return;
		}
		this.m_recentOpponent = player;
		this.FireRecentOpponentEvent(this.m_recentOpponent);
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x000371D0 File Offset: 0x000353D0
	public void AddRecentOpponentListener(FriendMgr.RecentOpponentCallback callback)
	{
		FriendMgr.RecentOpponentListener recentOpponentListener = new FriendMgr.RecentOpponentListener();
		recentOpponentListener.SetCallback(callback);
		recentOpponentListener.SetUserData(null);
		if (this.m_recentOpponentListeners.Contains(recentOpponentListener))
		{
			return;
		}
		this.m_recentOpponentListeners.Add(recentOpponentListener);
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x0003720C File Offset: 0x0003540C
	public bool RemoveRecentOpponentListener(FriendMgr.RecentOpponentCallback callback)
	{
		FriendMgr.RecentOpponentListener recentOpponentListener = new FriendMgr.RecentOpponentListener();
		recentOpponentListener.SetCallback(callback);
		recentOpponentListener.SetUserData(null);
		return this.m_recentOpponentListeners.Remove(recentOpponentListener);
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x0003723C File Offset: 0x0003543C
	public void FireRecentOpponentEvent(BnetPlayer recentOpponent)
	{
		FriendMgr.RecentOpponentListener[] array = this.m_recentOpponentListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(recentOpponent);
		}
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x0003726C File Offset: 0x0003546C
	public void Initialize()
	{
		BnetFriendMgr.Get().AddChangeListener(new BnetFriendMgr.ChangeCallback(this.OnFriendsChanged));
		Network.Get().AddBnetErrorListener(BnetFeature.Friends, new Network.BnetErrorCallback(this.OnBnetError));
		BnetPresenceMgr.Get().AddPlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged));
		SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x000372D4 File Offset: 0x000354D4
	private void OnFriendsChanged(BnetFriendChangelist changelist, object userData)
	{
		List<BnetPlayer> removedFriends = changelist.GetRemovedFriends();
		if (removedFriends == null)
		{
			return;
		}
		if (removedFriends.Contains(this.m_selectedFriend))
		{
			this.m_selectedFriend = null;
		}
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x00037304 File Offset: 0x00035504
	private bool OnBnetError(BnetErrorInfo info, object userData)
	{
		int feature = (int)info.GetFeature();
		BnetFeatureEvent featureEvent = info.GetFeatureEvent();
		if (feature == 1 && featureEvent == BnetFeatureEvent.Friends_OnSendInvitation)
		{
			BattleNetErrors error = info.GetError();
			if (error == BattleNetErrors.ERROR_OK)
			{
				string message = GameStrings.Get("GLOBAL_ADDFRIEND_SENT_CONFIRMATION");
				UIStatus.Get().AddInfo(message);
				return true;
			}
			if (error == BattleNetErrors.ERROR_FRIENDS_FRIENDSHIP_ALREADY_EXISTS)
			{
				string message = GameStrings.Get("GLOBAL_ADDFRIEND_ERROR_ALREADY_FRIEND");
				UIStatus.Get().AddError(message, -1f);
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x00037370 File Offset: 0x00035570
	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		BnetPlayerChange bnetPlayerChange = changelist.FindChange(this.m_selectedFriend);
		if (bnetPlayerChange == null)
		{
			return;
		}
		BnetPlayer oldPlayer = bnetPlayerChange.GetOldPlayer();
		BnetPlayer newPlayer = bnetPlayerChange.GetNewPlayer();
		if (oldPlayer == null || oldPlayer.IsOnline() != newPlayer.IsOnline())
		{
			this.m_selectedFriend = null;
		}
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x000373B4 File Offset: 0x000355B4
	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (mode == SceneMgr.Mode.GAMEPLAY)
		{
			GameState gameState = GameState.Get();
			if (gameState == null)
			{
				global::Log.All.PrintWarning("FriendMgr.OnSceneLoaded event was fired when GameState was null!", Array.Empty<object>());
				gameState = GameState.Initialize();
			}
			if (gameState != null)
			{
				gameState.RegisterGameOverListener(new GameState.GameOverCallback(this.OnGameOver), null);
			}
		}
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x000373FF File Offset: 0x000355FF
	private void OnGameOver(TAG_PLAYSTATE playState, object userData)
	{
		GameState.Get().UnregisterGameOverListener(new GameState.GameOverCallback(this.OnGameOver), null);
		this.UpdateRecentOpponent();
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x00037420 File Offset: 0x00035620
	private void WillReset()
	{
		BnetFriendMgr.Get().RemoveChangeListener(new BnetFriendMgr.ChangeCallback(this.OnFriendsChanged));
		Network.Get().RemoveBnetErrorListener(BnetFeature.Friends, new Network.BnetErrorCallback(this.OnBnetError));
		BnetPresenceMgr.Get().RemovePlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged));
		SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
	}

	// Token: 0x04000655 RID: 1621
	private static FriendMgr s_instance;

	// Token: 0x04000656 RID: 1622
	private BnetPlayer m_selectedFriend;

	// Token: 0x04000657 RID: 1623
	private BnetPlayer m_recentOpponent;

	// Token: 0x04000658 RID: 1624
	private bool m_friendListScrollEnabled;

	// Token: 0x04000659 RID: 1625
	private float m_friendListScrollCamPosY;

	// Token: 0x0400065A RID: 1626
	private List<FriendMgr.RecentOpponentListener> m_recentOpponentListeners = new List<FriendMgr.RecentOpponentListener>();

	// Token: 0x0200139C RID: 5020
	// (Invoke) Token: 0x0600D806 RID: 55302
	public delegate void RecentOpponentCallback(BnetPlayer recentOpponent, object userData);

	// Token: 0x0200139D RID: 5021
	private class RecentOpponentListener : global::EventListener<FriendMgr.RecentOpponentCallback>
	{
		// Token: 0x0600D809 RID: 55305 RVA: 0x003ED7C8 File Offset: 0x003EB9C8
		public void Fire(BnetPlayer recentOpponent)
		{
			this.m_callback(recentOpponent, this.m_userData);
		}
	}
}
