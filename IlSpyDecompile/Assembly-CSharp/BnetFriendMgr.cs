using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using bgs;
using bgs.types;
using Hearthstone;
using PegasusClient;
using PegasusShared;
using UnityEngine;

public class BnetFriendMgr
{
	public delegate void ChangeCallback(BnetFriendChangelist changelist, object userData);

	private class ChangeListener : EventListener<ChangeCallback>
	{
		public void Fire(BnetFriendChangelist changelist)
		{
			m_callback(changelist, m_userData);
		}
	}

	private static BnetFriendMgr s_instance;

	private int m_maxFriends;

	private int m_maxReceivedInvites;

	private int m_maxSentInvites;

	private List<BnetPlayer> m_friends = new List<BnetPlayer>();

	private List<BnetInvitation> m_receivedInvites = new List<BnetInvitation>();

	private List<BnetInvitation> m_sentInvites = new List<BnetInvitation>();

	private List<ChangeListener> m_changeListeners = new List<ChangeListener>();

	private PendingBnetFriendChangelist m_pendingChangelist = new PendingBnetFriendChangelist();

	private static ulong nextIdToken;

	public static BnetFriendMgr Get()
	{
		if (s_instance == null)
		{
			s_instance = new BnetFriendMgr();
			HearthstoneApplication.Get().WillReset += s_instance.Clear;
		}
		return s_instance;
	}

	public void Initialize()
	{
		FriendMgr.Get().Initialize();
		BnetEventMgr.Get().AddChangeListener(OnBnetEventOccurred);
		Network.Get().SetFriendsHandler(OnFriendsUpdate);
		Network.Get().AddBnetErrorListener(BnetFeature.Friends, OnBnetError);
		FatalErrorMgr.Get().AddErrorListener(OnFatalError);
		InitMaximums();
	}

	public void Shutdown()
	{
		Network.Get().RemoveBnetErrorListener(BnetFeature.Friends, OnBnetError);
		Network.Get().SetFriendsHandler(null);
		if (FatalErrorMgr.Get() != null)
		{
			FatalErrorMgr.Get().RemoveErrorListener(OnFatalError);
		}
	}

	public int GetMaxFriends()
	{
		return m_maxFriends;
	}

	public int GetMaxReceivedInvites()
	{
		return m_maxReceivedInvites;
	}

	public int GetMaxSentInvites()
	{
		return m_maxSentInvites;
	}

	public BnetPlayer FindFriend(BnetAccountId id)
	{
		BnetPlayer bnetPlayer = FindNonPendingFriend(id);
		if (bnetPlayer != null)
		{
			return bnetPlayer;
		}
		bnetPlayer = FindPendingFriend(id);
		if (bnetPlayer != null)
		{
			return bnetPlayer;
		}
		return null;
	}

	public BnetPlayer FindFriend(BnetGameAccountId id)
	{
		BnetPlayer bnetPlayer = FindNonPendingFriend(id);
		if (bnetPlayer != null)
		{
			return bnetPlayer;
		}
		bnetPlayer = FindPendingFriend(id);
		if (bnetPlayer != null)
		{
			return bnetPlayer;
		}
		return null;
	}

	public bool IsFriend(BnetPlayer player)
	{
		if (IsNonPendingFriend(player))
		{
			return true;
		}
		if (IsPendingFriend(player))
		{
			return true;
		}
		return false;
	}

	public bool IsFriend(BnetAccountId id)
	{
		if (IsNonPendingFriend(id))
		{
			return true;
		}
		if (IsPendingFriend(id))
		{
			return true;
		}
		return false;
	}

	public bool IsFriend(BnetGameAccountId id)
	{
		if (IsNonPendingFriend(id))
		{
			return true;
		}
		if (IsPendingFriend(id))
		{
			return true;
		}
		return false;
	}

	public List<BnetPlayer> GetFriends()
	{
		return m_friends;
	}

	public int GetFriendCount()
	{
		return m_friends.Count;
	}

	public bool HasOnlineFriends()
	{
		foreach (BnetPlayer friend in m_friends)
		{
			if (friend.IsOnline())
			{
				return true;
			}
		}
		return false;
	}

	public int GetOnlineFriendCount()
	{
		int num = 0;
		foreach (BnetPlayer friend in m_friends)
		{
			if (friend.IsOnline())
			{
				num++;
			}
		}
		return num;
	}

	public int GetActiveOnlineFriendCount()
	{
		if (!Network.IsLoggedIn())
		{
			return 0;
		}
		int num = 0;
		foreach (BnetPlayer friend in m_friends)
		{
			if (friend.IsOnline() && !(friend.GetBestProgramId() == null) && (!friend.GetBestProgramId().IsPhoenix() || (friend.GetBestAwayTimeMicrosec() <= 0 && !friend.IsBusy())))
			{
				num++;
			}
		}
		return num;
	}

	public BnetPlayer FindNonPendingFriend(BnetAccountId id)
	{
		foreach (BnetPlayer friend in m_friends)
		{
			if (friend.GetAccountId() == id)
			{
				return friend;
			}
		}
		return null;
	}

	public BnetPlayer FindNonPendingFriend(BnetGameAccountId id)
	{
		foreach (BnetPlayer friend in m_friends)
		{
			if (friend.HasGameAccount(id))
			{
				return friend;
			}
		}
		return null;
	}

	public bool IsNonPendingFriend(BnetPlayer player)
	{
		if (player == null)
		{
			return false;
		}
		if (m_friends.Contains(player))
		{
			return true;
		}
		BnetAccountId accountId = player.GetAccountId();
		if (accountId != null)
		{
			return IsFriend(accountId);
		}
		foreach (BnetGameAccountId key in player.GetGameAccounts().Keys)
		{
			if (IsFriend(key))
			{
				return true;
			}
		}
		return false;
	}

	public bool IsNonPendingFriend(BnetAccountId id)
	{
		return FindNonPendingFriend(id) != null;
	}

	public bool IsNonPendingFriend(BnetGameAccountId id)
	{
		return FindNonPendingFriend(id) != null;
	}

	public BnetPlayer FindPendingFriend(BnetAccountId id)
	{
		return m_pendingChangelist.FindFriend(id);
	}

	public BnetPlayer FindPendingFriend(BnetGameAccountId id)
	{
		return m_pendingChangelist.FindFriend(id);
	}

	public bool IsPendingFriend(BnetPlayer player)
	{
		return m_pendingChangelist.IsFriend(player);
	}

	public bool IsPendingFriend(BnetAccountId id)
	{
		return m_pendingChangelist.IsFriend(id);
	}

	public bool IsPendingFriend(BnetGameAccountId id)
	{
		return m_pendingChangelist.IsFriend(id);
	}

	public List<BnetPlayer> GetPendingFriends()
	{
		return m_pendingChangelist.GetFriends();
	}

	public List<BnetInvitation> GetReceivedInvites()
	{
		return m_receivedInvites;
	}

	public List<BnetInvitation> GetSentInvites()
	{
		return m_sentInvites;
	}

	public void AcceptInvite(BnetInvitationId inviteId)
	{
		Network.AcceptFriendInvite(inviteId);
	}

	public void DeclineInvite(BnetInvitationId inviteId)
	{
		Network.DeclineFriendInvite(inviteId);
	}

	public void IgnoreInvite(BnetInvitationId inviteId)
	{
		Network.IgnoreFriendInvite(inviteId);
	}

	public void RevokeInvite(BnetInvitationId inviteId)
	{
		Network.RevokeFriendInvite(inviteId);
	}

	public bool SendInvite(string name)
	{
		if (name.Contains("@"))
		{
			return SendInviteByEmail(name);
		}
		if (name.Contains("#"))
		{
			return SendInviteByBattleTag(name);
		}
		return false;
	}

	public bool SendInviteByEmail(string email)
	{
		if (!new Regex("^\\S[^@]+@[A-Za-z0-9-]+(\\.[A-Za-z0-9-]+)+$").IsMatch(email))
		{
			return false;
		}
		Network.SendFriendInviteByEmail(BnetPresenceMgr.Get().GetMyPlayer().GetFullName(), email);
		return true;
	}

	public bool SendInviteByBattleTag(string battleTagString)
	{
		if (!new Regex("^[^\\W\\d_][^\\W_]{1,11}#\\d+$").IsMatch(battleTagString))
		{
			return false;
		}
		Network.SendFriendInviteByBattleTag(BnetPresenceMgr.Get().GetMyPlayer().GetBattleTag()
			.GetString(), battleTagString);
		return true;
	}

	public bool RemoveFriend(BnetPlayer friend)
	{
		bool flag = false;
		for (int i = 0; i < m_friends.Count; i++)
		{
			if (m_friends[i].GetAccountId().Equals(friend.GetAccountId()))
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			return false;
		}
		Network.RemoveFriend(friend.GetAccountId());
		return true;
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

	private bool RemoveChangeListener(ChangeCallback callback, object userData)
	{
		ChangeListener changeListener = new ChangeListener();
		changeListener.SetCallback(callback);
		changeListener.SetUserData(userData);
		return m_changeListeners.Remove(changeListener);
	}

	public static bool RemoveChangeListenerFromInstance(ChangeCallback callback, object userData = null)
	{
		if (s_instance == null)
		{
			return false;
		}
		return s_instance.RemoveChangeListener(callback, userData);
	}

	private void InitMaximums()
	{
		FriendsInfo info = default(FriendsInfo);
		BattleNet.GetFriendsInfo(ref info);
		m_maxFriends = info.maxFriends;
		m_maxReceivedInvites = info.maxRecvInvites;
		m_maxSentInvites = info.maxSentInvites;
	}

	private void ProcessPendingFriends()
	{
		bool flag = false;
		foreach (BnetPlayer friend in m_pendingChangelist.GetFriends())
		{
			if (friend.IsDisplayable())
			{
				flag = true;
				m_friends.Add(friend);
			}
		}
		if (flag)
		{
			FirePendingFriendsChangedEvent();
		}
	}

	private void OnBnetEventOccurred(BattleNet.BnetEvent bnetEvent, object userData)
	{
		if (bnetEvent == BattleNet.BnetEvent.Disconnected)
		{
			Clear();
		}
	}

	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		Clear();
	}

	private void OnFriendsUpdate(FriendsUpdate[] updates)
	{
		BnetFriendChangelist bnetFriendChangelist = new BnetFriendChangelist();
		for (int i = 0; i < updates.Length; i++)
		{
			FriendsUpdate src = updates[i];
			switch (src.action)
			{
			case 1:
			{
				BnetAccountId accountId = BnetAccountId.CreateFromBnetEntityId(src.entity1);
				BnetPlayer bnetPlayer = BnetPresenceMgr.Get().RegisterPlayer(BnetPlayerSource.FRIENDLIST, accountId);
				if (bnetPlayer.IsDisplayable())
				{
					m_friends.Add(bnetPlayer);
					bnetFriendChangelist.AddAddedFriend(bnetPlayer);
				}
				else
				{
					AddPendingFriend(bnetPlayer);
				}
				break;
			}
			case 2:
			{
				BnetAccountId bnetAccountId = BnetAccountId.CreateFromBnetEntityId(src.entity1);
				BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(bnetAccountId);
				m_friends.Remove(player);
				bnetFriendChangelist.AddRemovedFriend(player);
				RemovePendingFriend(player);
				BnetPresenceMgr.Get().CheckSubscriptionsAndClearTransientStatus(bnetAccountId);
				break;
			}
			case 3:
			{
				BnetInvitation bnetInvitation4 = BnetInvitation.CreateFromFriendsUpdate(src);
				m_receivedInvites.Add(bnetInvitation4);
				bnetFriendChangelist.AddAddedReceivedInvite(bnetInvitation4);
				break;
			}
			case 4:
			{
				BnetInvitation bnetInvitation3 = BnetInvitation.CreateFromFriendsUpdate(src);
				m_receivedInvites.Remove(bnetInvitation3);
				bnetFriendChangelist.AddRemovedReceivedInvite(bnetInvitation3);
				break;
			}
			case 5:
			{
				BnetInvitation bnetInvitation2 = BnetInvitation.CreateFromFriendsUpdate(src);
				m_sentInvites.Add(bnetInvitation2);
				bnetFriendChangelist.AddAddedSentInvite(bnetInvitation2);
				break;
			}
			case 6:
			{
				BnetInvitation bnetInvitation = BnetInvitation.CreateFromFriendsUpdate(src);
				m_sentInvites.Remove(bnetInvitation);
				bnetFriendChangelist.AddRemovedSentInvite(bnetInvitation);
				break;
			}
			}
		}
		if (!bnetFriendChangelist.IsEmpty())
		{
			FireChangeEvent(bnetFriendChangelist);
		}
	}

	private void OnPendingPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		ProcessPendingFriends();
	}

	private bool OnBnetError(BnetErrorInfo info, object userData)
	{
		return true;
	}

	private void Clear()
	{
		m_friends.Clear();
		m_receivedInvites.Clear();
		m_sentInvites.Clear();
		m_pendingChangelist.Clear();
		BnetPresenceMgr.Get().RemovePlayersChangedListener(OnPendingPlayersChanged);
	}

	private void FireChangeEvent(BnetFriendChangelist changelist)
	{
		ChangeListener[] array = m_changeListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(changelist);
		}
	}

	private void AddPendingFriend(BnetPlayer friend)
	{
		if (m_pendingChangelist.Add(friend) && m_pendingChangelist.GetCount() == 1)
		{
			BnetPresenceMgr.Get().AddPlayersChangedListener(OnPendingPlayersChanged);
		}
	}

	private void RemovePendingFriend(BnetPlayer friend)
	{
		if (m_pendingChangelist.Remove(friend))
		{
			if (m_pendingChangelist.GetCount() == 0)
			{
				BnetPresenceMgr.Get().RemovePlayersChangedListener(OnPendingPlayersChanged);
			}
			else
			{
				ProcessPendingFriends();
			}
		}
	}

	private void FirePendingFriendsChangedEvent()
	{
		BnetFriendChangelist changelist = m_pendingChangelist.CreateChangelist();
		if (m_pendingChangelist.GetCount() == 0)
		{
			BnetPresenceMgr.Get().RemovePlayersChangedListener(OnPendingPlayersChanged);
		}
		FireChangeEvent(changelist);
	}

	public BnetPlayer Cheat_CreatePlayer(string fullName, int leagueId, int starLevel, BnetProgramId programId, bool isFriend, bool isOnline)
	{
		BnetBattleTag bnetBattleTag = new BnetBattleTag();
		bnetBattleTag.SetString($"friend#{nextIdToken}");
		BnetAccountId bnetAccountId = new BnetAccountId();
		bnetAccountId.SetHi(nextIdToken++);
		bnetAccountId.SetLo(nextIdToken++);
		BnetAccount bnetAccount = new BnetAccount();
		bnetAccount.SetId(bnetAccountId);
		bnetAccount.SetFullName(fullName);
		bnetAccount.SetBattleTag(bnetBattleTag);
		BnetGameAccountId bnetGameAccountId = new BnetGameAccountId();
		bnetGameAccountId.SetHi(nextIdToken++);
		bnetGameAccountId.SetLo(nextIdToken++);
		BnetGameAccount bnetGameAccount = new BnetGameAccount();
		bnetGameAccount.SetId(bnetGameAccountId);
		bnetGameAccount.SetBattleTag(bnetBattleTag);
		bnetGameAccount.SetOnline(isOnline);
		bnetGameAccount.SetProgramId(programId);
		GamePresenceRank gamePresenceRank = new GamePresenceRank();
		foreach (FormatType value in Enum.GetValues(typeof(FormatType)))
		{
			if (value != 0)
			{
				GamePresenceRankData item = new GamePresenceRankData
				{
					FormatType = value,
					LeagueId = leagueId,
					StarLevel = starLevel,
					LegendRank = UnityEngine.Random.Range(1, 99999)
				};
				gamePresenceRank.Values.Add(item);
			}
		}
		byte[] val = ProtobufUtil.ToByteArray(gamePresenceRank);
		bnetGameAccount.SetGameField(18u, val);
		BnetPlayer bnetPlayer = new BnetPlayer(BnetPlayerSource.CREATED_BY_CHEAT);
		bnetPlayer.SetAccount(bnetAccount);
		bnetPlayer.AddGameAccount(bnetGameAccount);
		bnetPlayer.IsCheatPlayer = true;
		if (isFriend)
		{
			m_friends.Add(bnetPlayer);
		}
		return bnetPlayer;
	}

	public BnetPlayer Cheat_CreateFriend(string fullName, int leagueId, int starLevel, BnetProgramId programId, bool isOnline)
	{
		return Cheat_CreatePlayer(fullName, leagueId, starLevel, programId, isFriend: true, isOnline);
	}

	public int Cheat_RemoveCheatFriends()
	{
		int num = 0;
		for (int num2 = m_friends.Count - 1; num2 >= 0; num2--)
		{
			if (m_friends[num2].IsCheatPlayer)
			{
				m_friends.RemoveAt(num2);
				num++;
			}
		}
		return num;
	}

	public BnetInvitation Cheat_CreateReceivedInvite(ref ulong nextIdToken, BnetPlayer myself, string fullName, ulong creationTime, ulong expirationTime)
	{
		BnetInvitationId id = new BnetInvitationId(nextIdToken++);
		BnetAccountId bnetAccountId = new BnetAccountId();
		bnetAccountId.SetHi(nextIdToken++);
		bnetAccountId.SetLo(nextIdToken++);
		BnetInvitation bnetInvitation = new BnetInvitation();
		bnetInvitation.SetId(id);
		bnetInvitation.SetInviterId(bnetAccountId);
		bnetInvitation.SetInviterName(fullName);
		bnetInvitation.SetInviteeId(myself.GetAccountId());
		bnetInvitation.SetInviteeName(myself.GetFullName());
		bnetInvitation.SetCreationTimeMicrosec(creationTime);
		bnetInvitation.SetExpirationTimeMicroSec(expirationTime);
		m_receivedInvites.Add(bnetInvitation);
		return bnetInvitation;
	}

	public BnetInvitation Cheat_CreateSentInvite(ref ulong nextIdToken, BnetPlayer myself, string fullName, ulong creationTime, ulong expirationTime)
	{
		BnetInvitationId id = new BnetInvitationId(nextIdToken++);
		BnetAccountId bnetAccountId = new BnetAccountId();
		bnetAccountId.SetHi(nextIdToken++);
		bnetAccountId.SetLo(nextIdToken++);
		BnetInvitation bnetInvitation = new BnetInvitation();
		bnetInvitation.SetId(id);
		bnetInvitation.SetInviterId(myself.GetAccountId());
		bnetInvitation.SetInviterName(myself.GetFullName());
		bnetInvitation.SetInviteeId(bnetAccountId);
		bnetInvitation.SetInviteeName(fullName);
		bnetInvitation.SetCreationTimeMicrosec(creationTime);
		bnetInvitation.SetExpirationTimeMicroSec(expirationTime);
		m_sentInvites.Add(bnetInvitation);
		return bnetInvitation;
	}
}
