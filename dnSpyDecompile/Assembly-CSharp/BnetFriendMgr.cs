using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using bgs;
using bgs.types;
using Hearthstone;
using PegasusClient;
using PegasusShared;
using UnityEngine;

// Token: 0x02000768 RID: 1896
public class BnetFriendMgr
{
	// Token: 0x06006A52 RID: 27218 RVA: 0x002296BC File Offset: 0x002278BC
	public static BnetFriendMgr Get()
	{
		if (BnetFriendMgr.s_instance == null)
		{
			BnetFriendMgr.s_instance = new BnetFriendMgr();
			HearthstoneApplication.Get().WillReset += BnetFriendMgr.s_instance.Clear;
		}
		return BnetFriendMgr.s_instance;
	}

	// Token: 0x06006A53 RID: 27219 RVA: 0x002296F0 File Offset: 0x002278F0
	public void Initialize()
	{
		FriendMgr.Get().Initialize();
		BnetEventMgr.Get().AddChangeListener(new BnetEventMgr.ChangeCallback(this.OnBnetEventOccurred));
		Network.Get().SetFriendsHandler(new Network.FriendsHandler(this.OnFriendsUpdate));
		Network.Get().AddBnetErrorListener(BnetFeature.Friends, new Network.BnetErrorCallback(this.OnBnetError));
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		this.InitMaximums();
	}

	// Token: 0x06006A54 RID: 27220 RVA: 0x00229768 File Offset: 0x00227968
	public void Shutdown()
	{
		Network.Get().RemoveBnetErrorListener(BnetFeature.Friends, new Network.BnetErrorCallback(this.OnBnetError));
		Network.Get().SetFriendsHandler(null);
		if (FatalErrorMgr.Get() != null)
		{
			FatalErrorMgr.Get().RemoveErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		}
	}

	// Token: 0x06006A55 RID: 27221 RVA: 0x002297B6 File Offset: 0x002279B6
	public int GetMaxFriends()
	{
		return this.m_maxFriends;
	}

	// Token: 0x06006A56 RID: 27222 RVA: 0x002297BE File Offset: 0x002279BE
	public int GetMaxReceivedInvites()
	{
		return this.m_maxReceivedInvites;
	}

	// Token: 0x06006A57 RID: 27223 RVA: 0x002297C6 File Offset: 0x002279C6
	public int GetMaxSentInvites()
	{
		return this.m_maxSentInvites;
	}

	// Token: 0x06006A58 RID: 27224 RVA: 0x002297D0 File Offset: 0x002279D0
	public BnetPlayer FindFriend(BnetAccountId id)
	{
		BnetPlayer bnetPlayer = this.FindNonPendingFriend(id);
		if (bnetPlayer != null)
		{
			return bnetPlayer;
		}
		bnetPlayer = this.FindPendingFriend(id);
		if (bnetPlayer != null)
		{
			return bnetPlayer;
		}
		return null;
	}

	// Token: 0x06006A59 RID: 27225 RVA: 0x002297F8 File Offset: 0x002279F8
	public BnetPlayer FindFriend(BnetGameAccountId id)
	{
		BnetPlayer bnetPlayer = this.FindNonPendingFriend(id);
		if (bnetPlayer != null)
		{
			return bnetPlayer;
		}
		bnetPlayer = this.FindPendingFriend(id);
		if (bnetPlayer != null)
		{
			return bnetPlayer;
		}
		return null;
	}

	// Token: 0x06006A5A RID: 27226 RVA: 0x00229820 File Offset: 0x00227A20
	public bool IsFriend(BnetPlayer player)
	{
		return this.IsNonPendingFriend(player) || this.IsPendingFriend(player);
	}

	// Token: 0x06006A5B RID: 27227 RVA: 0x00229839 File Offset: 0x00227A39
	public bool IsFriend(BnetAccountId id)
	{
		return this.IsNonPendingFriend(id) || this.IsPendingFriend(id);
	}

	// Token: 0x06006A5C RID: 27228 RVA: 0x00229852 File Offset: 0x00227A52
	public bool IsFriend(BnetGameAccountId id)
	{
		return this.IsNonPendingFriend(id) || this.IsPendingFriend(id);
	}

	// Token: 0x06006A5D RID: 27229 RVA: 0x0022986B File Offset: 0x00227A6B
	public List<BnetPlayer> GetFriends()
	{
		return this.m_friends;
	}

	// Token: 0x06006A5E RID: 27230 RVA: 0x00229873 File Offset: 0x00227A73
	public int GetFriendCount()
	{
		return this.m_friends.Count;
	}

	// Token: 0x06006A5F RID: 27231 RVA: 0x00229880 File Offset: 0x00227A80
	public bool HasOnlineFriends()
	{
		using (List<BnetPlayer>.Enumerator enumerator = this.m_friends.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsOnline())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06006A60 RID: 27232 RVA: 0x002298DC File Offset: 0x00227ADC
	public int GetOnlineFriendCount()
	{
		int num = 0;
		using (List<BnetPlayer>.Enumerator enumerator = this.m_friends.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsOnline())
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x06006A61 RID: 27233 RVA: 0x00229938 File Offset: 0x00227B38
	public int GetActiveOnlineFriendCount()
	{
		if (!Network.IsLoggedIn())
		{
			return 0;
		}
		int num = 0;
		foreach (BnetPlayer bnetPlayer in this.m_friends)
		{
			if (bnetPlayer.IsOnline() && !(bnetPlayer.GetBestProgramId() == null) && (!bnetPlayer.GetBestProgramId().IsPhoenix() || (bnetPlayer.GetBestAwayTimeMicrosec() <= 0L && !bnetPlayer.IsBusy())))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06006A62 RID: 27234 RVA: 0x002299CC File Offset: 0x00227BCC
	public BnetPlayer FindNonPendingFriend(BnetAccountId id)
	{
		foreach (BnetPlayer bnetPlayer in this.m_friends)
		{
			if (bnetPlayer.GetAccountId() == id)
			{
				return bnetPlayer;
			}
		}
		return null;
	}

	// Token: 0x06006A63 RID: 27235 RVA: 0x00229A30 File Offset: 0x00227C30
	public BnetPlayer FindNonPendingFriend(BnetGameAccountId id)
	{
		foreach (BnetPlayer bnetPlayer in this.m_friends)
		{
			if (bnetPlayer.HasGameAccount(id))
			{
				return bnetPlayer;
			}
		}
		return null;
	}

	// Token: 0x06006A64 RID: 27236 RVA: 0x00229A8C File Offset: 0x00227C8C
	public bool IsNonPendingFriend(BnetPlayer player)
	{
		if (player == null)
		{
			return false;
		}
		if (this.m_friends.Contains(player))
		{
			return true;
		}
		BnetAccountId accountId = player.GetAccountId();
		if (accountId != null)
		{
			return this.IsFriend(accountId);
		}
		foreach (BnetGameAccountId id in player.GetGameAccounts().Keys)
		{
			if (this.IsFriend(id))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06006A65 RID: 27237 RVA: 0x00229B1C File Offset: 0x00227D1C
	public bool IsNonPendingFriend(BnetAccountId id)
	{
		return this.FindNonPendingFriend(id) != null;
	}

	// Token: 0x06006A66 RID: 27238 RVA: 0x00229B28 File Offset: 0x00227D28
	public bool IsNonPendingFriend(BnetGameAccountId id)
	{
		return this.FindNonPendingFriend(id) != null;
	}

	// Token: 0x06006A67 RID: 27239 RVA: 0x00229B34 File Offset: 0x00227D34
	public BnetPlayer FindPendingFriend(BnetAccountId id)
	{
		return this.m_pendingChangelist.FindFriend(id);
	}

	// Token: 0x06006A68 RID: 27240 RVA: 0x00229B42 File Offset: 0x00227D42
	public BnetPlayer FindPendingFriend(BnetGameAccountId id)
	{
		return this.m_pendingChangelist.FindFriend(id);
	}

	// Token: 0x06006A69 RID: 27241 RVA: 0x00229B50 File Offset: 0x00227D50
	public bool IsPendingFriend(BnetPlayer player)
	{
		return this.m_pendingChangelist.IsFriend(player);
	}

	// Token: 0x06006A6A RID: 27242 RVA: 0x00229B5E File Offset: 0x00227D5E
	public bool IsPendingFriend(BnetAccountId id)
	{
		return this.m_pendingChangelist.IsFriend(id);
	}

	// Token: 0x06006A6B RID: 27243 RVA: 0x00229B6C File Offset: 0x00227D6C
	public bool IsPendingFriend(BnetGameAccountId id)
	{
		return this.m_pendingChangelist.IsFriend(id);
	}

	// Token: 0x06006A6C RID: 27244 RVA: 0x00229B7A File Offset: 0x00227D7A
	public List<BnetPlayer> GetPendingFriends()
	{
		return this.m_pendingChangelist.GetFriends();
	}

	// Token: 0x06006A6D RID: 27245 RVA: 0x00229B87 File Offset: 0x00227D87
	public List<BnetInvitation> GetReceivedInvites()
	{
		return this.m_receivedInvites;
	}

	// Token: 0x06006A6E RID: 27246 RVA: 0x00229B8F File Offset: 0x00227D8F
	public List<BnetInvitation> GetSentInvites()
	{
		return this.m_sentInvites;
	}

	// Token: 0x06006A6F RID: 27247 RVA: 0x00229B97 File Offset: 0x00227D97
	public void AcceptInvite(BnetInvitationId inviteId)
	{
		Network.AcceptFriendInvite(inviteId);
	}

	// Token: 0x06006A70 RID: 27248 RVA: 0x00229B9F File Offset: 0x00227D9F
	public void DeclineInvite(BnetInvitationId inviteId)
	{
		Network.DeclineFriendInvite(inviteId);
	}

	// Token: 0x06006A71 RID: 27249 RVA: 0x00229BA7 File Offset: 0x00227DA7
	public void IgnoreInvite(BnetInvitationId inviteId)
	{
		Network.IgnoreFriendInvite(inviteId);
	}

	// Token: 0x06006A72 RID: 27250 RVA: 0x00229BAF File Offset: 0x00227DAF
	public void RevokeInvite(BnetInvitationId inviteId)
	{
		Network.RevokeFriendInvite(inviteId);
	}

	// Token: 0x06006A73 RID: 27251 RVA: 0x00229BB7 File Offset: 0x00227DB7
	public bool SendInvite(string name)
	{
		if (name.Contains("@"))
		{
			return this.SendInviteByEmail(name);
		}
		return name.Contains("#") && this.SendInviteByBattleTag(name);
	}

	// Token: 0x06006A74 RID: 27252 RVA: 0x00229BE4 File Offset: 0x00227DE4
	public bool SendInviteByEmail(string email)
	{
		if (!new Regex("^\\S[^@]+@[A-Za-z0-9-]+(\\.[A-Za-z0-9-]+)+$").IsMatch(email))
		{
			return false;
		}
		Network.SendFriendInviteByEmail(BnetPresenceMgr.Get().GetMyPlayer().GetFullName(), email);
		return true;
	}

	// Token: 0x06006A75 RID: 27253 RVA: 0x00229C10 File Offset: 0x00227E10
	public bool SendInviteByBattleTag(string battleTagString)
	{
		if (!new Regex("^[^\\W\\d_][^\\W_]{1,11}#\\d+$").IsMatch(battleTagString))
		{
			return false;
		}
		Network.SendFriendInviteByBattleTag(BnetPresenceMgr.Get().GetMyPlayer().GetBattleTag().GetString(), battleTagString);
		return true;
	}

	// Token: 0x06006A76 RID: 27254 RVA: 0x00229C44 File Offset: 0x00227E44
	public bool RemoveFriend(BnetPlayer friend)
	{
		bool flag = false;
		for (int i = 0; i < this.m_friends.Count; i++)
		{
			if (this.m_friends[i].GetAccountId().Equals(friend.GetAccountId()))
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

	// Token: 0x06006A77 RID: 27255 RVA: 0x00229C9C File Offset: 0x00227E9C
	public bool AddChangeListener(BnetFriendMgr.ChangeCallback callback)
	{
		return this.AddChangeListener(callback, null);
	}

	// Token: 0x06006A78 RID: 27256 RVA: 0x00229CA8 File Offset: 0x00227EA8
	public bool AddChangeListener(BnetFriendMgr.ChangeCallback callback, object userData)
	{
		BnetFriendMgr.ChangeListener changeListener = new BnetFriendMgr.ChangeListener();
		changeListener.SetCallback(callback);
		changeListener.SetUserData(userData);
		if (this.m_changeListeners.Contains(changeListener))
		{
			return false;
		}
		this.m_changeListeners.Add(changeListener);
		return true;
	}

	// Token: 0x06006A79 RID: 27257 RVA: 0x00229CE6 File Offset: 0x00227EE6
	public bool RemoveChangeListener(BnetFriendMgr.ChangeCallback callback)
	{
		return this.RemoveChangeListener(callback, null);
	}

	// Token: 0x06006A7A RID: 27258 RVA: 0x00229CF0 File Offset: 0x00227EF0
	private bool RemoveChangeListener(BnetFriendMgr.ChangeCallback callback, object userData)
	{
		BnetFriendMgr.ChangeListener changeListener = new BnetFriendMgr.ChangeListener();
		changeListener.SetCallback(callback);
		changeListener.SetUserData(userData);
		return this.m_changeListeners.Remove(changeListener);
	}

	// Token: 0x06006A7B RID: 27259 RVA: 0x00229D1D File Offset: 0x00227F1D
	public static bool RemoveChangeListenerFromInstance(BnetFriendMgr.ChangeCallback callback, object userData = null)
	{
		return BnetFriendMgr.s_instance != null && BnetFriendMgr.s_instance.RemoveChangeListener(callback, userData);
	}

	// Token: 0x06006A7C RID: 27260 RVA: 0x00229D34 File Offset: 0x00227F34
	private void InitMaximums()
	{
		FriendsInfo friendsInfo = default(FriendsInfo);
		BattleNet.GetFriendsInfo(ref friendsInfo);
		this.m_maxFriends = friendsInfo.maxFriends;
		this.m_maxReceivedInvites = friendsInfo.maxRecvInvites;
		this.m_maxSentInvites = friendsInfo.maxSentInvites;
	}

	// Token: 0x06006A7D RID: 27261 RVA: 0x00229D74 File Offset: 0x00227F74
	private void ProcessPendingFriends()
	{
		bool flag = false;
		foreach (BnetPlayer bnetPlayer in this.m_pendingChangelist.GetFriends())
		{
			if (bnetPlayer.IsDisplayable())
			{
				flag = true;
				this.m_friends.Add(bnetPlayer);
			}
		}
		if (flag)
		{
			this.FirePendingFriendsChangedEvent();
		}
	}

	// Token: 0x06006A7E RID: 27262 RVA: 0x00229DE8 File Offset: 0x00227FE8
	private void OnBnetEventOccurred(BattleNet.BnetEvent bnetEvent, object userData)
	{
		if (bnetEvent == BattleNet.BnetEvent.Disconnected)
		{
			this.Clear();
		}
	}

	// Token: 0x06006A7F RID: 27263 RVA: 0x00229DF3 File Offset: 0x00227FF3
	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		this.Clear();
	}

	// Token: 0x06006A80 RID: 27264 RVA: 0x00229DFC File Offset: 0x00227FFC
	private void OnFriendsUpdate(FriendsUpdate[] updates)
	{
		BnetFriendChangelist bnetFriendChangelist = new BnetFriendChangelist();
		foreach (FriendsUpdate friendsUpdate in updates)
		{
			FriendsUpdate.Action action = (FriendsUpdate.Action)friendsUpdate.action;
			if (action == FriendsUpdate.Action.FRIEND_ADDED)
			{
				BnetAccountId accountId = BnetAccountId.CreateFromBnetEntityId(friendsUpdate.entity1);
				BnetPlayer bnetPlayer = BnetPresenceMgr.Get().RegisterPlayer(BnetPlayerSource.FRIENDLIST, accountId, null, null);
				if (bnetPlayer.IsDisplayable())
				{
					this.m_friends.Add(bnetPlayer);
					bnetFriendChangelist.AddAddedFriend(bnetPlayer);
				}
				else
				{
					this.AddPendingFriend(bnetPlayer);
				}
			}
			else if (action == FriendsUpdate.Action.FRIEND_REMOVED)
			{
				BnetAccountId bnetAccountId = BnetAccountId.CreateFromBnetEntityId(friendsUpdate.entity1);
				BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(bnetAccountId);
				this.m_friends.Remove(player);
				bnetFriendChangelist.AddRemovedFriend(player);
				this.RemovePendingFriend(player);
				BnetPresenceMgr.Get().CheckSubscriptionsAndClearTransientStatus(bnetAccountId);
			}
			else if (action != FriendsUpdate.Action.FRIEND_ATTR_CHANGE && action != FriendsUpdate.Action.FRIEND_ROLE_CHANGE && action != FriendsUpdate.Action.FRIEND_GAME_ADDED && action != FriendsUpdate.Action.FRIEND_GAME_REMOVED)
			{
				if (action == FriendsUpdate.Action.FRIEND_INVITE)
				{
					BnetInvitation bnetInvitation = BnetInvitation.CreateFromFriendsUpdate(friendsUpdate);
					this.m_receivedInvites.Add(bnetInvitation);
					bnetFriendChangelist.AddAddedReceivedInvite(bnetInvitation);
				}
				else if (action == FriendsUpdate.Action.FRIEND_INVITE_REMOVED)
				{
					BnetInvitation bnetInvitation2 = BnetInvitation.CreateFromFriendsUpdate(friendsUpdate);
					this.m_receivedInvites.Remove(bnetInvitation2);
					bnetFriendChangelist.AddRemovedReceivedInvite(bnetInvitation2);
				}
				else if (action == FriendsUpdate.Action.FRIEND_SENT_INVITE)
				{
					BnetInvitation bnetInvitation3 = BnetInvitation.CreateFromFriendsUpdate(friendsUpdate);
					this.m_sentInvites.Add(bnetInvitation3);
					bnetFriendChangelist.AddAddedSentInvite(bnetInvitation3);
				}
				else if (action == FriendsUpdate.Action.FRIEND_SENT_INVITE_REMOVED)
				{
					BnetInvitation bnetInvitation4 = BnetInvitation.CreateFromFriendsUpdate(friendsUpdate);
					this.m_sentInvites.Remove(bnetInvitation4);
					bnetFriendChangelist.AddRemovedSentInvite(bnetInvitation4);
				}
			}
		}
		if (!bnetFriendChangelist.IsEmpty())
		{
			this.FireChangeEvent(bnetFriendChangelist);
		}
	}

	// Token: 0x06006A81 RID: 27265 RVA: 0x00229F9E File Offset: 0x0022819E
	private void OnPendingPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		this.ProcessPendingFriends();
	}

	// Token: 0x06006A82 RID: 27266 RVA: 0x000052EC File Offset: 0x000034EC
	private bool OnBnetError(BnetErrorInfo info, object userData)
	{
		return true;
	}

	// Token: 0x06006A83 RID: 27267 RVA: 0x00229FA8 File Offset: 0x002281A8
	private void Clear()
	{
		this.m_friends.Clear();
		this.m_receivedInvites.Clear();
		this.m_sentInvites.Clear();
		this.m_pendingChangelist.Clear();
		BnetPresenceMgr.Get().RemovePlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPendingPlayersChanged));
	}

	// Token: 0x06006A84 RID: 27268 RVA: 0x00229FF8 File Offset: 0x002281F8
	private void FireChangeEvent(BnetFriendChangelist changelist)
	{
		BnetFriendMgr.ChangeListener[] array = this.m_changeListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(changelist);
		}
	}

	// Token: 0x06006A85 RID: 27269 RVA: 0x0022A028 File Offset: 0x00228228
	private void AddPendingFriend(BnetPlayer friend)
	{
		if (!this.m_pendingChangelist.Add(friend))
		{
			return;
		}
		if (this.m_pendingChangelist.GetCount() == 1)
		{
			BnetPresenceMgr.Get().AddPlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPendingPlayersChanged));
		}
	}

	// Token: 0x06006A86 RID: 27270 RVA: 0x0022A05E File Offset: 0x0022825E
	private void RemovePendingFriend(BnetPlayer friend)
	{
		if (!this.m_pendingChangelist.Remove(friend))
		{
			return;
		}
		if (this.m_pendingChangelist.GetCount() == 0)
		{
			BnetPresenceMgr.Get().RemovePlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPendingPlayersChanged));
			return;
		}
		this.ProcessPendingFriends();
	}

	// Token: 0x06006A87 RID: 27271 RVA: 0x0022A09C File Offset: 0x0022829C
	private void FirePendingFriendsChangedEvent()
	{
		BnetFriendChangelist changelist = this.m_pendingChangelist.CreateChangelist();
		if (this.m_pendingChangelist.GetCount() == 0)
		{
			BnetPresenceMgr.Get().RemovePlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPendingPlayersChanged));
		}
		this.FireChangeEvent(changelist);
	}

	// Token: 0x06006A88 RID: 27272 RVA: 0x0022A0E0 File Offset: 0x002282E0
	public BnetPlayer Cheat_CreatePlayer(string fullName, int leagueId, int starLevel, BnetProgramId programId, bool isFriend, bool isOnline)
	{
		BnetBattleTag bnetBattleTag = new BnetBattleTag();
		bnetBattleTag.SetString(string.Format("friend#{0}", BnetFriendMgr.nextIdToken));
		BnetAccountId bnetAccountId = new BnetAccountId();
		BnetEntityId bnetEntityId = bnetAccountId;
		ulong num = BnetFriendMgr.nextIdToken;
		BnetFriendMgr.nextIdToken = num + 1UL;
		bnetEntityId.SetHi(num);
		BnetEntityId bnetEntityId2 = bnetAccountId;
		ulong num2 = BnetFriendMgr.nextIdToken;
		BnetFriendMgr.nextIdToken = num2 + 1UL;
		bnetEntityId2.SetLo(num2);
		BnetAccount bnetAccount = new BnetAccount();
		bnetAccount.SetId(bnetAccountId);
		bnetAccount.SetFullName(fullName);
		bnetAccount.SetBattleTag(bnetBattleTag);
		BnetGameAccountId bnetGameAccountId = new BnetGameAccountId();
		BnetEntityId bnetEntityId3 = bnetGameAccountId;
		ulong num3 = BnetFriendMgr.nextIdToken;
		BnetFriendMgr.nextIdToken = num3 + 1UL;
		bnetEntityId3.SetHi(num3);
		BnetEntityId bnetEntityId4 = bnetGameAccountId;
		ulong num4 = BnetFriendMgr.nextIdToken;
		BnetFriendMgr.nextIdToken = num4 + 1UL;
		bnetEntityId4.SetLo(num4);
		BnetGameAccount bnetGameAccount = new BnetGameAccount();
		bnetGameAccount.SetId(bnetGameAccountId);
		bnetGameAccount.SetBattleTag(bnetBattleTag);
		bnetGameAccount.SetOnline(isOnline);
		bnetGameAccount.SetProgramId(programId);
		GamePresenceRank gamePresenceRank = new GamePresenceRank();
		foreach (object obj in Enum.GetValues(typeof(FormatType)))
		{
			FormatType formatType = (FormatType)obj;
			if (formatType != FormatType.FT_UNKNOWN)
			{
				GamePresenceRankData item = new GamePresenceRankData
				{
					FormatType = formatType,
					LeagueId = leagueId,
					StarLevel = starLevel,
					LegendRank = UnityEngine.Random.Range(1, 99999)
				};
				gamePresenceRank.Values.Add(item);
			}
		}
		byte[] val = ProtobufUtil.ToByteArray(gamePresenceRank);
		bnetGameAccount.SetGameField(18U, val);
		BnetPlayer bnetPlayer = new BnetPlayer(BnetPlayerSource.CREATED_BY_CHEAT);
		bnetPlayer.SetAccount(bnetAccount);
		bnetPlayer.AddGameAccount(bnetGameAccount);
		bnetPlayer.IsCheatPlayer = true;
		if (isFriend)
		{
			this.m_friends.Add(bnetPlayer);
		}
		return bnetPlayer;
	}

	// Token: 0x06006A89 RID: 27273 RVA: 0x0022A294 File Offset: 0x00228494
	public BnetPlayer Cheat_CreateFriend(string fullName, int leagueId, int starLevel, BnetProgramId programId, bool isOnline)
	{
		return this.Cheat_CreatePlayer(fullName, leagueId, starLevel, programId, true, isOnline);
	}

	// Token: 0x06006A8A RID: 27274 RVA: 0x0022A2A4 File Offset: 0x002284A4
	public int Cheat_RemoveCheatFriends()
	{
		int num = 0;
		for (int i = this.m_friends.Count - 1; i >= 0; i--)
		{
			if (this.m_friends[i].IsCheatPlayer)
			{
				this.m_friends.RemoveAt(i);
				num++;
			}
		}
		return num;
	}

	// Token: 0x06006A8B RID: 27275 RVA: 0x0022A2F0 File Offset: 0x002284F0
	public BnetInvitation Cheat_CreateReceivedInvite(ref ulong nextIdToken, BnetPlayer myself, string fullName, ulong creationTime, ulong expirationTime)
	{
		ulong num = nextIdToken;
		nextIdToken = num + 1UL;
		BnetInvitationId id = new BnetInvitationId(num);
		BnetAccountId bnetAccountId = new BnetAccountId();
		BnetEntityId bnetEntityId = bnetAccountId;
		num = nextIdToken;
		nextIdToken = num + 1UL;
		bnetEntityId.SetHi(num);
		BnetEntityId bnetEntityId2 = bnetAccountId;
		num = nextIdToken;
		nextIdToken = num + 1UL;
		bnetEntityId2.SetLo(num);
		BnetInvitation bnetInvitation = new BnetInvitation();
		bnetInvitation.SetId(id);
		bnetInvitation.SetInviterId(bnetAccountId);
		bnetInvitation.SetInviterName(fullName);
		bnetInvitation.SetInviteeId(myself.GetAccountId());
		bnetInvitation.SetInviteeName(myself.GetFullName());
		bnetInvitation.SetCreationTimeMicrosec(creationTime);
		bnetInvitation.SetExpirationTimeMicroSec(expirationTime);
		this.m_receivedInvites.Add(bnetInvitation);
		return bnetInvitation;
	}

	// Token: 0x06006A8C RID: 27276 RVA: 0x0022A384 File Offset: 0x00228584
	public BnetInvitation Cheat_CreateSentInvite(ref ulong nextIdToken, BnetPlayer myself, string fullName, ulong creationTime, ulong expirationTime)
	{
		ulong num = nextIdToken;
		nextIdToken = num + 1UL;
		BnetInvitationId id = new BnetInvitationId(num);
		BnetAccountId bnetAccountId = new BnetAccountId();
		BnetEntityId bnetEntityId = bnetAccountId;
		num = nextIdToken;
		nextIdToken = num + 1UL;
		bnetEntityId.SetHi(num);
		BnetEntityId bnetEntityId2 = bnetAccountId;
		num = nextIdToken;
		nextIdToken = num + 1UL;
		bnetEntityId2.SetLo(num);
		BnetInvitation bnetInvitation = new BnetInvitation();
		bnetInvitation.SetId(id);
		bnetInvitation.SetInviterId(myself.GetAccountId());
		bnetInvitation.SetInviterName(myself.GetFullName());
		bnetInvitation.SetInviteeId(bnetAccountId);
		bnetInvitation.SetInviteeName(fullName);
		bnetInvitation.SetCreationTimeMicrosec(creationTime);
		bnetInvitation.SetExpirationTimeMicroSec(expirationTime);
		this.m_sentInvites.Add(bnetInvitation);
		return bnetInvitation;
	}

	// Token: 0x040056DD RID: 22237
	private static BnetFriendMgr s_instance;

	// Token: 0x040056DE RID: 22238
	private int m_maxFriends;

	// Token: 0x040056DF RID: 22239
	private int m_maxReceivedInvites;

	// Token: 0x040056E0 RID: 22240
	private int m_maxSentInvites;

	// Token: 0x040056E1 RID: 22241
	private List<BnetPlayer> m_friends = new List<BnetPlayer>();

	// Token: 0x040056E2 RID: 22242
	private List<BnetInvitation> m_receivedInvites = new List<BnetInvitation>();

	// Token: 0x040056E3 RID: 22243
	private List<BnetInvitation> m_sentInvites = new List<BnetInvitation>();

	// Token: 0x040056E4 RID: 22244
	private List<BnetFriendMgr.ChangeListener> m_changeListeners = new List<BnetFriendMgr.ChangeListener>();

	// Token: 0x040056E5 RID: 22245
	private PendingBnetFriendChangelist m_pendingChangelist = new PendingBnetFriendChangelist();

	// Token: 0x040056E6 RID: 22246
	private static ulong nextIdToken;

	// Token: 0x02002338 RID: 9016
	// (Invoke) Token: 0x06012A3E RID: 76350
	public delegate void ChangeCallback(BnetFriendChangelist changelist, object userData);

	// Token: 0x02002339 RID: 9017
	private class ChangeListener : global::EventListener<BnetFriendMgr.ChangeCallback>
	{
		// Token: 0x06012A41 RID: 76353 RVA: 0x00511C18 File Offset: 0x0050FE18
		public void Fire(BnetFriendChangelist changelist)
		{
			this.m_callback(changelist, this.m_userData);
		}
	}
}
