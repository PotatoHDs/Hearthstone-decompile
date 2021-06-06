using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using bgs;
using Hearthstone;
using UnityEngine;

// Token: 0x0200076D RID: 1901
public class BnetNearbyPlayerMgr
{
	// Token: 0x06006B17 RID: 27415 RVA: 0x0022B1D0 File Offset: 0x002293D0
	public static BnetNearbyPlayerMgr Get()
	{
		if (BnetNearbyPlayerMgr.s_instance == null)
		{
			BnetNearbyPlayerMgr.s_instance = new BnetNearbyPlayerMgr();
			HearthstoneApplication.Get().WillReset += BnetNearbyPlayerMgr.s_instance.Clear;
			FiresideGatheringManager.OnPatronListUpdated += BnetNearbyPlayerMgr.NearbyPlayers_OnFSGPatronsUpdated;
		}
		return BnetNearbyPlayerMgr.s_instance;
	}

	// Token: 0x06006B18 RID: 27416 RVA: 0x0022B220 File Offset: 0x00229420
	public void Initialize()
	{
		this.m_bnetVersion = BattleNet.GetVersion();
		this.m_bnetEnvironment = BattleNet.GetEnvironment();
		this.UpdateEnabled();
		Options.Get().RegisterChangedListener(Option.NEARBY_PLAYERS, new Options.ChangedCallback(this.OnEnabledOptionChanged));
		BnetFriendMgr.Get().AddChangeListener(new BnetFriendMgr.ChangeCallback(this.OnFriendsChanged));
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
	}

	// Token: 0x06006B19 RID: 27417 RVA: 0x0022B290 File Offset: 0x00229490
	public void Shutdown()
	{
		this.StopListening();
		Options.Get().UnregisterChangedListener(Option.NEARBY_PLAYERS, new Options.ChangedCallback(this.OnEnabledOptionChanged));
		BnetFriendMgr.Get().RemoveChangeListener(new BnetFriendMgr.ChangeCallback(this.OnFriendsChanged));
		if (FatalErrorMgr.Get() != null)
		{
			FatalErrorMgr.Get().RemoveErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		}
	}

	// Token: 0x06006B1A RID: 27418 RVA: 0x0022B2F1 File Offset: 0x002294F1
	public bool IsEnabled()
	{
		return !TemporaryAccountManager.IsTemporaryAccount() && Options.Get().GetBool(Option.NEARBY_PLAYERS) && this.m_enabled;
	}

	// Token: 0x06006B1B RID: 27419 RVA: 0x0022B317 File Offset: 0x00229517
	public void SetEnabled(bool enabled)
	{
		this.m_enabled = enabled;
		this.UpdateEnabled();
	}

	// Token: 0x06006B1C RID: 27420 RVA: 0x0022B328 File Offset: 0x00229528
	public bool GetNearbySessionStartTime(BnetPlayer bnetPlayer, out ulong sessionStartTime)
	{
		sessionStartTime = 0UL;
		if (bnetPlayer == null)
		{
			return false;
		}
		BnetNearbyPlayerMgr.NearbyPlayer nearbyPlayer = null;
		object mutex = this.m_mutex;
		lock (mutex)
		{
			nearbyPlayer = this.m_nearbyPlayers.Find((BnetNearbyPlayerMgr.NearbyPlayer obj) => obj.m_bnetPlayer.GetAccountId() == bnetPlayer.GetAccountId());
		}
		if (nearbyPlayer == null)
		{
			return false;
		}
		sessionStartTime = nearbyPlayer.m_sessionStartTime;
		return true;
	}

	// Token: 0x06006B1D RID: 27421 RVA: 0x0022B3A8 File Offset: 0x002295A8
	public bool HasNearbyStrangers()
	{
		if (this.m_nearbyStrangers.Count > 0)
		{
			return this.m_nearbyStrangers.Any((BnetPlayer p) => p != null && p.IsOnline());
		}
		return false;
	}

	// Token: 0x06006B1E RID: 27422 RVA: 0x0022B3E4 File Offset: 0x002295E4
	public List<BnetPlayer> GetNearbyPlayers()
	{
		return this.m_nearbyBnetPlayers;
	}

	// Token: 0x06006B1F RID: 27423 RVA: 0x0022B3EC File Offset: 0x002295EC
	public List<BnetPlayer> GetNearbyFriends()
	{
		return this.m_nearbyFriends;
	}

	// Token: 0x06006B20 RID: 27424 RVA: 0x0022B3F4 File Offset: 0x002295F4
	public List<BnetPlayer> GetNearbyStrangers()
	{
		return this.m_nearbyStrangers;
	}

	// Token: 0x06006B21 RID: 27425 RVA: 0x0022B3FC File Offset: 0x002295FC
	public bool IsNearbyPlayer(BnetPlayer player)
	{
		return this.FindNearbyPlayer(player) != null;
	}

	// Token: 0x06006B22 RID: 27426 RVA: 0x0022B408 File Offset: 0x00229608
	public bool IsNearbyPlayer(BnetGameAccountId id)
	{
		return this.FindNearbyPlayer(id) != null;
	}

	// Token: 0x06006B23 RID: 27427 RVA: 0x0022B414 File Offset: 0x00229614
	public bool IsNearbyPlayer(BnetAccountId id)
	{
		return this.FindNearbyPlayer(id) != null;
	}

	// Token: 0x06006B24 RID: 27428 RVA: 0x0022B420 File Offset: 0x00229620
	public bool IsNearbyFriend(BnetPlayer player)
	{
		return this.FindNearbyFriend(player) != null;
	}

	// Token: 0x06006B25 RID: 27429 RVA: 0x0022B42C File Offset: 0x0022962C
	public bool IsNearbyFriend(BnetGameAccountId id)
	{
		return this.FindNearbyFriend(id) != null;
	}

	// Token: 0x06006B26 RID: 27430 RVA: 0x0022B438 File Offset: 0x00229638
	public bool IsNearbyFriend(BnetAccountId id)
	{
		return this.FindNearbyFriend(id) != null;
	}

	// Token: 0x06006B27 RID: 27431 RVA: 0x0022B444 File Offset: 0x00229644
	public bool IsNearbyStranger(BnetPlayer player)
	{
		return this.FindNearbyStranger(player) != null;
	}

	// Token: 0x06006B28 RID: 27432 RVA: 0x0022B450 File Offset: 0x00229650
	public bool IsNearbyStranger(BnetGameAccountId id)
	{
		return this.FindNearbyStranger(id) != null;
	}

	// Token: 0x06006B29 RID: 27433 RVA: 0x0022B45C File Offset: 0x0022965C
	public bool IsNearbyStranger(BnetAccountId id)
	{
		return this.FindNearbyStranger(id) != null;
	}

	// Token: 0x06006B2A RID: 27434 RVA: 0x0022B468 File Offset: 0x00229668
	public BnetPlayer FindNearbyPlayer(BnetPlayer player)
	{
		return this.FindNearbyPlayer(player, this.m_nearbyBnetPlayers);
	}

	// Token: 0x06006B2B RID: 27435 RVA: 0x0022B477 File Offset: 0x00229677
	public BnetPlayer FindNearbyPlayer(BnetGameAccountId id)
	{
		return this.FindNearbyPlayer(id, this.m_nearbyBnetPlayers);
	}

	// Token: 0x06006B2C RID: 27436 RVA: 0x0022B486 File Offset: 0x00229686
	public BnetPlayer FindNearbyPlayer(BnetAccountId id)
	{
		return this.FindNearbyPlayer(id, this.m_nearbyBnetPlayers);
	}

	// Token: 0x06006B2D RID: 27437 RVA: 0x0022B495 File Offset: 0x00229695
	public BnetPlayer FindNearbyFriend(BnetGameAccountId id)
	{
		return this.FindNearbyPlayer(id, this.m_nearbyFriends);
	}

	// Token: 0x06006B2E RID: 27438 RVA: 0x0022B4A4 File Offset: 0x002296A4
	public BnetPlayer FindNearbyFriend(BnetPlayer player)
	{
		return this.FindNearbyPlayer(player, this.m_nearbyFriends);
	}

	// Token: 0x06006B2F RID: 27439 RVA: 0x0022B4B3 File Offset: 0x002296B3
	public BnetPlayer FindNearbyFriend(BnetAccountId id)
	{
		return this.FindNearbyPlayer(id, this.m_nearbyFriends);
	}

	// Token: 0x06006B30 RID: 27440 RVA: 0x0022B4C2 File Offset: 0x002296C2
	public BnetPlayer FindNearbyStranger(BnetPlayer player)
	{
		return this.FindNearbyPlayer(player, this.m_nearbyStrangers);
	}

	// Token: 0x06006B31 RID: 27441 RVA: 0x0022B4D1 File Offset: 0x002296D1
	public BnetPlayer FindNearbyStranger(BnetGameAccountId id)
	{
		return this.FindNearbyPlayer(id, this.m_nearbyStrangers);
	}

	// Token: 0x06006B32 RID: 27442 RVA: 0x0022B4E0 File Offset: 0x002296E0
	public BnetPlayer FindNearbyStranger(BnetAccountId id)
	{
		return this.FindNearbyPlayer(id, this.m_nearbyStrangers);
	}

	// Token: 0x06006B33 RID: 27443 RVA: 0x0022B4EF File Offset: 0x002296EF
	public bool GetAvailability()
	{
		return this.m_availability;
	}

	// Token: 0x06006B34 RID: 27444 RVA: 0x0022B4F7 File Offset: 0x002296F7
	public void SetAvailability(bool av)
	{
		this.m_availability = av;
		this.CreateBroadcastString();
	}

	// Token: 0x06006B35 RID: 27445 RVA: 0x0022B506 File Offset: 0x00229706
	public void SetPartyId(PartyId partyId)
	{
		this.m_partyId = (partyId ?? PartyId.Empty);
		this.CreateBroadcastString();
	}

	// Token: 0x06006B36 RID: 27446 RVA: 0x0022B51E File Offset: 0x0022971E
	public bool AddChangeListener(BnetNearbyPlayerMgr.ChangeCallback callback)
	{
		return this.AddChangeListener(callback, null);
	}

	// Token: 0x06006B37 RID: 27447 RVA: 0x0022B528 File Offset: 0x00229728
	public bool AddChangeListener(BnetNearbyPlayerMgr.ChangeCallback callback, object userData)
	{
		BnetNearbyPlayerMgr.ChangeListener changeListener = new BnetNearbyPlayerMgr.ChangeListener();
		changeListener.SetCallback(callback);
		changeListener.SetUserData(userData);
		if (this.m_changeListeners.Contains(changeListener))
		{
			return false;
		}
		this.m_changeListeners.Add(changeListener);
		return true;
	}

	// Token: 0x06006B38 RID: 27448 RVA: 0x0022B566 File Offset: 0x00229766
	public bool RemoveChangeListener(BnetNearbyPlayerMgr.ChangeCallback callback)
	{
		return this.RemoveChangeListener(callback, null);
	}

	// Token: 0x06006B39 RID: 27449 RVA: 0x0022B570 File Offset: 0x00229770
	private bool RemoveChangeListener(BnetNearbyPlayerMgr.ChangeCallback callback, object userData)
	{
		BnetNearbyPlayerMgr.ChangeListener changeListener = new BnetNearbyPlayerMgr.ChangeListener();
		changeListener.SetCallback(callback);
		changeListener.SetUserData(userData);
		return this.m_changeListeners.Remove(changeListener);
	}

	// Token: 0x06006B3A RID: 27450 RVA: 0x0022B59D File Offset: 0x0022979D
	public static bool RemoveChangeListenerFromInstance(BnetNearbyPlayerMgr.ChangeCallback callback, object userData = null)
	{
		return BnetNearbyPlayerMgr.s_instance != null && BnetNearbyPlayerMgr.s_instance.RemoveChangeListener(callback, userData);
	}

	// Token: 0x06006B3B RID: 27451 RVA: 0x0022B5B4 File Offset: 0x002297B4
	private void BeginListening()
	{
		if (this.m_listening)
		{
			return;
		}
		this.m_listening = true;
		IPEndPoint ipendPoint = new IPEndPoint(IPAddress.Any, 1228);
		UdpClient udpClient = new UdpClient();
		udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
		udpClient.Client.Bind(ipendPoint);
		this.m_port = 1228;
		this.m_client = udpClient;
		this.m_broadcastSender = new UdpClient();
		this.m_broadcastEndpoint = new IPEndPoint(IPAddress.Broadcast, this.m_port);
		BnetNearbyPlayerMgr.UdpState udpState = new BnetNearbyPlayerMgr.UdpState();
		udpState.e = ipendPoint;
		udpState.u = this.m_client;
		this.m_lastCallTime = Time.realtimeSinceStartup;
		this.m_client.BeginReceive(new AsyncCallback(this.OnUdpReceive), udpState);
	}

	// Token: 0x06006B3C RID: 27452 RVA: 0x0022B678 File Offset: 0x00229878
	private void OnUdpReceive(IAsyncResult ar)
	{
		object obj = this.m_mutexClient;
		lock (obj)
		{
			if (!this.m_listening)
			{
				return;
			}
		}
		UdpClient u = ((BnetNearbyPlayerMgr.UdpState)ar.AsyncState).u;
		IPEndPoint e = ((BnetNearbyPlayerMgr.UdpState)ar.AsyncState).e;
		if (u == null || e == null)
		{
			return;
		}
		byte[] bytes = u.EndReceive(ar, ref e);
		u.BeginReceive(new AsyncCallback(this.OnUdpReceive), ar.AsyncState);
		string[] array = Encoding.UTF8.GetString(bytes).Split(new char[]
		{
			','
		});
		ulong hi = 0UL;
		ulong lo = 0UL;
		ulong hi2 = 0UL;
		ulong num = 0UL;
		int number = 0;
		bool flag2 = false;
		ulong sessionStartTime = 0UL;
		ulong highBits = 0UL;
		ulong lowBits = 0UL;
		int num2 = 0;
		if (num2 >= array.Length)
		{
			return;
		}
		if (!ulong.TryParse(array[num2++], out hi))
		{
			return;
		}
		if (num2 >= array.Length)
		{
			return;
		}
		if (!ulong.TryParse(array[num2++], out lo))
		{
			return;
		}
		if (num2 >= array.Length)
		{
			return;
		}
		if (!ulong.TryParse(array[num2++], out hi2))
		{
			return;
		}
		if (num2 >= array.Length)
		{
			return;
		}
		if (!ulong.TryParse(array[num2++], out num))
		{
			return;
		}
		if (this.m_myGameAccountLo == num)
		{
			return;
		}
		if (num2 >= array.Length)
		{
			return;
		}
		string name = array[num2++];
		if (num2 >= array.Length)
		{
			return;
		}
		if (!int.TryParse(array[num2++], out number))
		{
			return;
		}
		if (num2 >= array.Length)
		{
			return;
		}
		string text = array[num2++];
		if (string.IsNullOrEmpty(text) || text != this.m_bnetVersion)
		{
			return;
		}
		if (num2 >= array.Length)
		{
			return;
		}
		string text2 = array[num2++];
		if (string.IsNullOrEmpty(text2) || text2 != this.m_bnetEnvironment)
		{
			return;
		}
		if (num2 >= array.Length)
		{
			return;
		}
		string a = array[num2++];
		if (a == "1")
		{
			flag2 = true;
		}
		else
		{
			if (!(a == "0"))
			{
				return;
			}
			flag2 = false;
		}
		if (num2 >= array.Length)
		{
			return;
		}
		if (!ulong.TryParse(array[num2++], out sessionStartTime))
		{
			return;
		}
		if (num2 >= array.Length)
		{
			return;
		}
		if (!ulong.TryParse(array[num2++], out highBits))
		{
			return;
		}
		if (num2 >= array.Length)
		{
			return;
		}
		if (!ulong.TryParse(array[num2++], out lowBits))
		{
			return;
		}
		BnetBattleTag bnetBattleTag = new BnetBattleTag();
		bnetBattleTag.SetName(name);
		bnetBattleTag.SetNumber(number);
		BnetAccountId bnetAccountId = new BnetAccountId();
		bnetAccountId.SetHi(hi);
		bnetAccountId.SetLo(lo);
		BnetGameAccountId bnetGameAccountId = new BnetGameAccountId();
		bnetGameAccountId.SetHi(hi2);
		bnetGameAccountId.SetLo(num);
		PartyId partyId = new PartyId(highBits, lowBits);
		BnetPlayer bnetPlayer = BnetPresenceMgr.Get().GetPlayer(bnetGameAccountId);
		if (bnetPlayer == null)
		{
			BnetAccount bnetAccount = new BnetAccount();
			bnetAccount.SetId(bnetAccountId);
			bnetAccount.SetBattleTag(bnetBattleTag);
			bnetAccount.SetAppearingOffline(false);
			BnetGameAccount bnetGameAccount = new BnetGameAccount();
			bnetGameAccount.SetId(bnetGameAccountId);
			bnetGameAccount.SetOwnerId(bnetAccountId);
			bnetGameAccount.SetBattleTag(bnetBattleTag);
			bnetGameAccount.SetOnline(true);
			bnetGameAccount.SetProgramId(BnetProgramId.HEARTHSTONE);
			bnetGameAccount.SetGameField(1U, flag2);
			bnetGameAccount.SetGameField(19U, text);
			bnetGameAccount.SetGameField(20U, text2);
			bnetGameAccount.SetGameField(26U, partyId.ToEntityId());
			bnetPlayer = new BnetPlayer(BnetPlayerSource.NEARBY_PLAYER);
			bnetPlayer.SetAccount(bnetAccount);
			bnetPlayer.AddGameAccount(bnetGameAccount);
		}
		BnetNearbyPlayerMgr.NearbyPlayer nearbyPlayer = new BnetNearbyPlayerMgr.NearbyPlayer();
		nearbyPlayer.m_bnetPlayer = bnetPlayer;
		nearbyPlayer.m_availability = flag2;
		nearbyPlayer.m_partyId = partyId;
		nearbyPlayer.m_sessionStartTime = sessionStartTime;
		obj = this.m_mutex;
		lock (obj)
		{
			if (this.m_listening)
			{
				foreach (BnetNearbyPlayerMgr.NearbyPlayer nearbyPlayer2 in this.m_nearbyAdds)
				{
					if (nearbyPlayer2.Equals(nearbyPlayer))
					{
						this.UpdateNearbyPlayer(nearbyPlayer2, flag2, sessionStartTime, partyId);
						return;
					}
				}
				foreach (BnetNearbyPlayerMgr.NearbyPlayer nearbyPlayer3 in this.m_nearbyUpdates)
				{
					if (nearbyPlayer3.Equals(nearbyPlayer))
					{
						this.UpdateNearbyPlayer(nearbyPlayer3, flag2, sessionStartTime, partyId);
						return;
					}
				}
				foreach (BnetNearbyPlayerMgr.NearbyPlayer nearbyPlayer4 in this.m_nearbyPlayers)
				{
					if (nearbyPlayer4.Equals(nearbyPlayer))
					{
						this.UpdateNearbyPlayer(nearbyPlayer4, flag2, sessionStartTime, partyId);
						this.m_nearbyUpdates.Add(nearbyPlayer4);
						return;
					}
				}
				this.m_nearbyAdds.Add(nearbyPlayer);
			}
		}
	}

	// Token: 0x06006B3D RID: 27453 RVA: 0x0022BBBC File Offset: 0x00229DBC
	private void StopListening()
	{
		object obj = this.m_mutexClient;
		lock (obj)
		{
			if (!this.m_listening)
			{
				return;
			}
			this.m_listening = false;
			this.m_client.Close();
		}
		BnetNearbyPlayerChangelist bnetNearbyPlayerChangelist = new BnetNearbyPlayerChangelist();
		obj = this.m_mutex;
		lock (obj)
		{
			foreach (BnetPlayer player in this.m_nearbyBnetPlayers)
			{
				bnetNearbyPlayerChangelist.AddRemovedPlayer(player);
			}
			foreach (BnetPlayer friend in this.m_nearbyFriends)
			{
				bnetNearbyPlayerChangelist.AddRemovedFriend(friend);
			}
			foreach (BnetPlayer stranger in this.m_nearbyStrangers)
			{
				bnetNearbyPlayerChangelist.AddRemovedStranger(stranger);
			}
			this.m_nearbyPlayers.Clear();
			this.m_nearbyBnetPlayers.Clear();
			this.m_nearbyFriends.Clear();
			this.m_nearbyStrangers.Clear();
			this.m_nearbyAdds.Clear();
			this.m_nearbyUpdates.Clear();
		}
		this.FireChangeEvent(bnetNearbyPlayerChangelist);
		this.m_broadcastSender.Close();
	}

	// Token: 0x06006B3E RID: 27454 RVA: 0x0022BD68 File Offset: 0x00229F68
	public void Update()
	{
		if (!this.m_listening)
		{
			return;
		}
		this.CacheMyAccountInfo();
		this.CheckIntervalAndBroadcast();
		this.ProcessPlayerChanges();
	}

	// Token: 0x06006B3F RID: 27455 RVA: 0x0022BD88 File Offset: 0x00229F88
	private void Clear()
	{
		object mutex = this.m_mutex;
		lock (mutex)
		{
			this.m_nearbyPlayers.Clear();
			this.m_nearbyBnetPlayers.Clear();
			this.m_nearbyFriends.Clear();
			this.m_nearbyStrangers.Clear();
			this.m_nearbyAdds.Clear();
			this.m_nearbyUpdates.Clear();
		}
	}

	// Token: 0x06006B40 RID: 27456 RVA: 0x0022BE04 File Offset: 0x0022A004
	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		this.Clear();
	}

	// Token: 0x06006B41 RID: 27457 RVA: 0x0022BE0C File Offset: 0x0022A00C
	private void UpdateEnabled()
	{
		bool flag = this.IsEnabled();
		if (flag == this.m_listening)
		{
			return;
		}
		if (flag)
		{
			this.BeginListening();
			return;
		}
		this.StopListening();
	}

	// Token: 0x06006B42 RID: 27458 RVA: 0x0022BE3C File Offset: 0x0022A03C
	private void FireChangeEvent(BnetNearbyPlayerChangelist changelist)
	{
		if (changelist.IsEmpty())
		{
			return;
		}
		BnetNearbyPlayerMgr.ChangeListener[] array = this.m_changeListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(changelist);
		}
	}

	// Token: 0x06006B43 RID: 27459 RVA: 0x0022BE78 File Offset: 0x0022A078
	private void CacheMyAccountInfo()
	{
		if (this.m_idString != null)
		{
			return;
		}
		BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
		if (myGameAccountId == null)
		{
			return;
		}
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		if (myPlayer == null)
		{
			return;
		}
		BnetAccountId accountId = myPlayer.GetAccountId();
		if (accountId == null)
		{
			return;
		}
		BnetBattleTag battleTag = myPlayer.GetBattleTag();
		if (battleTag == null)
		{
			return;
		}
		this.m_myGameAccountLo = myGameAccountId.GetLo();
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(accountId.GetHi());
		stringBuilder.Append(',');
		stringBuilder.Append(accountId.GetLo());
		stringBuilder.Append(',');
		stringBuilder.Append(myGameAccountId.GetHi());
		stringBuilder.Append(',');
		stringBuilder.Append(myGameAccountId.GetLo());
		stringBuilder.Append(',');
		stringBuilder.Append(battleTag.GetName());
		stringBuilder.Append(',');
		stringBuilder.Append(battleTag.GetNumber());
		stringBuilder.Append(',');
		stringBuilder.Append(BattleNet.GetVersion());
		stringBuilder.Append(',');
		stringBuilder.Append(BattleNet.GetEnvironment());
		this.m_idString = stringBuilder.ToString();
		this.CreateBroadcastString();
	}

	// Token: 0x06006B44 RID: 27460 RVA: 0x0022BFB0 File Offset: 0x0022A1B0
	private void ProcessPlayerChanges()
	{
		BnetNearbyPlayerChangelist changelist = new BnetNearbyPlayerChangelist();
		object mutex = this.m_mutex;
		lock (mutex)
		{
			this.ProcessAddedPlayers(changelist);
			this.ProcessUpdatedPlayers(changelist);
			this.RemoveInactivePlayers(changelist);
		}
		this.FireChangeEvent(changelist);
	}

	// Token: 0x06006B45 RID: 27461 RVA: 0x0022C00C File Offset: 0x0022A20C
	private void ProcessAddedPlayers(BnetNearbyPlayerChangelist changelist)
	{
		if (this.m_nearbyAdds.Count == 0)
		{
			return;
		}
		for (int i = 0; i < this.m_nearbyAdds.Count; i++)
		{
			BnetNearbyPlayerMgr.NearbyPlayer nearbyPlayer = this.m_nearbyAdds[i];
			nearbyPlayer.m_lastReceivedTime = Time.realtimeSinceStartup;
			BnetGameAccountId id = nearbyPlayer.GetGameAccount().GetId();
			if (BnetPresenceMgr.Get().GetPlayer(id) == null)
			{
				BnetPresenceMgr.Get().RegisterBnetPlayer(nearbyPlayer.m_bnetPlayer);
			}
			this.m_nearbyPlayers.Add(nearbyPlayer);
			this.m_nearbyBnetPlayers.Add(nearbyPlayer.m_bnetPlayer);
			changelist.AddAddedPlayer(nearbyPlayer.m_bnetPlayer);
			if (nearbyPlayer.IsFriend())
			{
				this.m_nearbyFriends.Add(nearbyPlayer.m_bnetPlayer);
				changelist.AddAddedFriend(nearbyPlayer.m_bnetPlayer);
			}
			else
			{
				this.m_nearbyStrangers.Add(nearbyPlayer.m_bnetPlayer);
				changelist.AddAddedStranger(nearbyPlayer.m_bnetPlayer);
			}
		}
		this.m_nearbyAdds.Clear();
	}

	// Token: 0x06006B46 RID: 27462 RVA: 0x0022C100 File Offset: 0x0022A300
	private void ProcessUpdatedPlayers(BnetNearbyPlayerChangelist changelist)
	{
		if (this.m_nearbyUpdates.Count == 0)
		{
			return;
		}
		for (int i = 0; i < this.m_nearbyUpdates.Count; i++)
		{
			BnetNearbyPlayerMgr.NearbyPlayer nearbyPlayer = this.m_nearbyUpdates[i];
			nearbyPlayer.m_lastReceivedTime = Time.realtimeSinceStartup;
			changelist.AddUpdatedPlayer(nearbyPlayer.m_bnetPlayer);
			if (nearbyPlayer.IsFriend())
			{
				changelist.AddUpdatedFriend(nearbyPlayer.m_bnetPlayer);
			}
			else
			{
				changelist.AddUpdatedStranger(nearbyPlayer.m_bnetPlayer);
			}
		}
		this.m_nearbyUpdates.Clear();
	}

	// Token: 0x06006B47 RID: 27463 RVA: 0x0022C188 File Offset: 0x0022A388
	private void RemoveInactivePlayers(BnetNearbyPlayerChangelist changelist)
	{
		List<BnetNearbyPlayerMgr.NearbyPlayer> list = null;
		for (int i = 0; i < this.m_nearbyPlayers.Count; i++)
		{
			BnetNearbyPlayerMgr.NearbyPlayer nearbyPlayer = this.m_nearbyPlayers[i];
			if (Time.realtimeSinceStartup - nearbyPlayer.m_lastReceivedTime >= 60f)
			{
				if (list == null)
				{
					list = new List<BnetNearbyPlayerMgr.NearbyPlayer>();
				}
				list.Add(nearbyPlayer);
			}
		}
		if (list != null)
		{
			foreach (BnetNearbyPlayerMgr.NearbyPlayer nearbyPlayer2 in list)
			{
				this.m_nearbyPlayers.Remove(nearbyPlayer2);
				if (this.m_nearbyBnetPlayers.Remove(nearbyPlayer2.m_bnetPlayer))
				{
					changelist.AddRemovedPlayer(nearbyPlayer2.m_bnetPlayer);
				}
				if (this.m_nearbyFriends.Remove(nearbyPlayer2.m_bnetPlayer))
				{
					changelist.AddRemovedFriend(nearbyPlayer2.m_bnetPlayer);
				}
				if (this.m_nearbyStrangers.Remove(nearbyPlayer2.m_bnetPlayer))
				{
					changelist.AddRemovedStranger(nearbyPlayer2.m_bnetPlayer);
				}
			}
		}
	}

	// Token: 0x06006B48 RID: 27464 RVA: 0x0022C298 File Offset: 0x0022A498
	private bool CheckIntervalAndBroadcast()
	{
		if (!this.IsMyPlayerOnline())
		{
			return false;
		}
		if (Time.realtimeSinceStartup - this.m_lastCallTime < 12f)
		{
			return false;
		}
		this.m_lastCallTime = Time.realtimeSinceStartup;
		this.Broadcast();
		return true;
	}

	// Token: 0x06006B49 RID: 27465 RVA: 0x0022C2CC File Offset: 0x0022A4CC
	private async void Broadcast()
	{
		if (!this.m_isBroadcasting)
		{
			this.m_isBroadcasting = true;
			try
			{
				this.m_broadcastSender.EnableBroadcast = true;
				await this.m_broadcastSender.SendAsync(this.m_broadcastBuffer, this.m_broadcastBuffer.Length, this.m_broadcastEndpoint);
			}
			catch
			{
			}
			finally
			{
				this.m_isBroadcasting = false;
			}
		}
	}

	// Token: 0x06006B4A RID: 27466 RVA: 0x0022C308 File Offset: 0x0022A508
	private void CreateBroadcastString()
	{
		ulong sessionStartTime = HealthyGamingMgr.Get().GetSessionStartTime();
		this.m_broadcastStringBuilder.Clear();
		this.m_broadcastStringBuilder.Append(this.m_idString);
		this.m_broadcastStringBuilder.Append(',');
		this.m_broadcastStringBuilder.Append(this.m_availability ? "1" : "0");
		this.m_broadcastStringBuilder.Append(',');
		this.m_broadcastStringBuilder.Append(sessionStartTime);
		this.m_broadcastStringBuilder.Append(',');
		this.m_broadcastStringBuilder.Append(this.m_partyId.Hi);
		this.m_broadcastStringBuilder.Append(',');
		this.m_broadcastStringBuilder.Append(this.m_partyId.Lo);
		string s = this.m_broadcastStringBuilder.ToString();
		this.m_broadcastBuffer = Encoding.UTF8.GetBytes(s);
	}

	// Token: 0x06006B4B RID: 27467 RVA: 0x0022C3F0 File Offset: 0x0022A5F0
	private int FindNearbyPlayerIndex(BnetPlayer bnetPlayer, List<BnetPlayer> bnetPlayers)
	{
		if (bnetPlayer == null)
		{
			return -1;
		}
		BnetAccountId accountId = bnetPlayer.GetAccountId();
		if (accountId != null)
		{
			return this.FindNearbyPlayerIndex(accountId, bnetPlayers);
		}
		BnetGameAccountId hearthstoneGameAccountId = bnetPlayer.GetHearthstoneGameAccountId();
		return this.FindNearbyPlayerIndex(hearthstoneGameAccountId, bnetPlayers);
	}

	// Token: 0x06006B4C RID: 27468 RVA: 0x0022C42C File Offset: 0x0022A62C
	private int FindNearbyPlayerIndex(BnetGameAccountId id, List<BnetPlayer> bnetPlayers)
	{
		if (id == null)
		{
			return -1;
		}
		for (int i = 0; i < bnetPlayers.Count; i++)
		{
			BnetPlayer bnetPlayer = bnetPlayers[i];
			if (id == bnetPlayer.GetHearthstoneGameAccountId())
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06006B4D RID: 27469 RVA: 0x0022C470 File Offset: 0x0022A670
	private int FindNearbyPlayerIndex(BnetAccountId id, List<BnetPlayer> bnetPlayers)
	{
		if (id == null)
		{
			return -1;
		}
		for (int i = 0; i < bnetPlayers.Count; i++)
		{
			BnetPlayer bnetPlayer = bnetPlayers[i];
			if (id == bnetPlayer.GetAccountId())
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06006B4E RID: 27470 RVA: 0x0022C4B4 File Offset: 0x0022A6B4
	private BnetPlayer FindNearbyPlayer(BnetPlayer bnetPlayer, List<BnetPlayer> bnetPlayers)
	{
		if (bnetPlayer == null)
		{
			return null;
		}
		BnetAccountId accountId = bnetPlayer.GetAccountId();
		if (accountId != null)
		{
			return this.FindNearbyPlayer(accountId, bnetPlayers);
		}
		BnetGameAccountId hearthstoneGameAccountId = bnetPlayer.GetHearthstoneGameAccountId();
		return this.FindNearbyPlayer(hearthstoneGameAccountId, bnetPlayers);
	}

	// Token: 0x06006B4F RID: 27471 RVA: 0x0022C4F0 File Offset: 0x0022A6F0
	private BnetPlayer FindNearbyPlayer(BnetGameAccountId id, List<BnetPlayer> bnetPlayers)
	{
		int num = this.FindNearbyPlayerIndex(id, bnetPlayers);
		if (num < 0)
		{
			return null;
		}
		return bnetPlayers[num];
	}

	// Token: 0x06006B50 RID: 27472 RVA: 0x0022C514 File Offset: 0x0022A714
	private BnetPlayer FindNearbyPlayer(BnetAccountId id, List<BnetPlayer> bnetPlayers)
	{
		int num = this.FindNearbyPlayerIndex(id, bnetPlayers);
		if (num < 0)
		{
			return null;
		}
		return bnetPlayers[num];
	}

	// Token: 0x06006B51 RID: 27473 RVA: 0x0022C538 File Offset: 0x0022A738
	private void UpdateNearbyPlayer(BnetNearbyPlayerMgr.NearbyPlayer player, bool available, ulong sessionStartTime, PartyId partyId)
	{
		BnetGameAccount gameAccount = player.GetGameAccount();
		bool flag = BnetPresenceMgr.Get().IsSubscribedToPlayer(gameAccount.GetId());
		BnetPlayer player2 = BnetPresenceMgr.Get().GetPlayer(gameAccount.GetId());
		if (flag && player2 != null)
		{
			player.m_bnetPlayer = player2;
		}
		else
		{
			gameAccount.SetGameField(1U, available);
			gameAccount.SetGameField(26U, partyId.ToEntityId());
		}
		player.m_sessionStartTime = sessionStartTime;
	}

	// Token: 0x06006B52 RID: 27474 RVA: 0x0022C5A4 File Offset: 0x0022A7A4
	private bool IsMyPlayerOnline()
	{
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		return myPlayer != null && myPlayer.IsOnline() && !myPlayer.IsAppearingOffline();
	}

	// Token: 0x06006B53 RID: 27475 RVA: 0x0022C5D4 File Offset: 0x0022A7D4
	private void OnEnabledOptionChanged(Option option, object prevValue, bool existed, object userData)
	{
		this.UpdateEnabled();
	}

	// Token: 0x06006B54 RID: 27476 RVA: 0x0022C5DC File Offset: 0x0022A7DC
	private void OnFriendsChanged(BnetFriendChangelist friendChangelist, object userData)
	{
		List<BnetPlayer> addedFriends = friendChangelist.GetAddedFriends();
		List<BnetPlayer> removedFriends = friendChangelist.GetRemovedFriends();
		bool flag = addedFriends != null && addedFriends.Count > 0;
		bool flag2 = removedFriends != null && removedFriends.Count > 0;
		if (!flag && !flag2)
		{
			return;
		}
		BnetNearbyPlayerChangelist bnetNearbyPlayerChangelist = new BnetNearbyPlayerChangelist();
		object mutex = this.m_mutex;
		lock (mutex)
		{
			if (addedFriends != null)
			{
				foreach (BnetPlayer bnetPlayer in addedFriends)
				{
					int num = this.FindNearbyPlayerIndex(bnetPlayer, this.m_nearbyStrangers);
					if (num >= 0)
					{
						BnetPlayer bnetPlayer2 = this.m_nearbyStrangers[num];
						this.m_nearbyStrangers.RemoveAt(num);
						this.m_nearbyFriends.Add(bnetPlayer2);
						bnetNearbyPlayerChangelist.AddAddedFriend(bnetPlayer2);
						bnetNearbyPlayerChangelist.AddRemovedStranger(bnetPlayer2);
					}
				}
			}
			if (removedFriends != null)
			{
				foreach (BnetPlayer bnetPlayer3 in removedFriends)
				{
					int num2 = this.FindNearbyPlayerIndex(bnetPlayer3, this.m_nearbyFriends);
					if (num2 >= 0)
					{
						BnetPlayer bnetPlayer4 = this.m_nearbyFriends[num2];
						this.m_nearbyFriends.RemoveAt(num2);
						this.m_nearbyStrangers.Add(bnetPlayer4);
						bnetNearbyPlayerChangelist.AddAddedStranger(bnetPlayer4);
						bnetNearbyPlayerChangelist.AddRemovedFriend(bnetPlayer4);
					}
				}
			}
		}
		this.FireChangeEvent(bnetNearbyPlayerChangelist);
	}

	// Token: 0x06006B55 RID: 27477 RVA: 0x0022C798 File Offset: 0x0022A998
	private static void NearbyPlayers_OnFSGPatronsUpdated(List<BnetPlayer> addedPatrons, List<BnetPlayer> removedPatrons)
	{
		BnetNearbyPlayerChangelist bnetNearbyPlayerChangelist = null;
		if (addedPatrons != null)
		{
			foreach (BnetPlayer player in addedPatrons)
			{
				if (BnetNearbyPlayerMgr.Get().IsNearbyPlayer(player))
				{
					if (bnetNearbyPlayerChangelist == null)
					{
						bnetNearbyPlayerChangelist = new BnetNearbyPlayerChangelist();
					}
					bnetNearbyPlayerChangelist.AddRemovedPlayer(player);
				}
			}
		}
		if (removedPatrons != null)
		{
			foreach (BnetPlayer player2 in removedPatrons)
			{
				if (BnetNearbyPlayerMgr.Get().IsNearbyPlayer(player2))
				{
					if (bnetNearbyPlayerChangelist == null)
					{
						bnetNearbyPlayerChangelist = new BnetNearbyPlayerChangelist();
					}
					bnetNearbyPlayerChangelist.AddAddedPlayer(player2);
				}
			}
		}
		if (bnetNearbyPlayerChangelist != null)
		{
			BnetNearbyPlayerMgr.Get().FireChangeEvent(bnetNearbyPlayerChangelist);
		}
	}

	// Token: 0x06006B56 RID: 27478 RVA: 0x0022C868 File Offset: 0x0022AA68
	public BnetPlayer Cheat_CreateNearbyPlayer(string fullName, int leagueId, int starLevel, BnetProgramId programId, bool isFriend, bool isOnline)
	{
		BnetPlayer bnetPlayer = BnetFriendMgr.Get().Cheat_CreatePlayer(fullName, leagueId, starLevel, programId, isFriend, isOnline);
		BnetNearbyPlayerChangelist bnetNearbyPlayerChangelist = new BnetNearbyPlayerChangelist();
		if (isFriend)
		{
			bnetNearbyPlayerChangelist.AddAddedFriend(bnetPlayer);
		}
		else
		{
			bnetNearbyPlayerChangelist.AddAddedPlayer(bnetPlayer);
		}
		BnetNearbyPlayerMgr.NearbyPlayer nearbyPlayer = new BnetNearbyPlayerMgr.NearbyPlayer();
		nearbyPlayer.m_bnetPlayer = bnetPlayer;
		nearbyPlayer.m_availability = true;
		nearbyPlayer.m_partyId = PartyId.Empty;
		this.m_nearbyAdds.Add(nearbyPlayer);
		this.ProcessAddedPlayers(bnetNearbyPlayerChangelist);
		return bnetPlayer;
	}

	// Token: 0x06006B57 RID: 27479 RVA: 0x0022C8D8 File Offset: 0x0022AAD8
	public int Cheat_RemoveCheatFriends()
	{
		int num = 0;
		BnetNearbyPlayerChangelist changelist = new BnetNearbyPlayerChangelist();
		for (int i = this.m_nearbyPlayers.Count - 1; i >= 0; i--)
		{
			BnetNearbyPlayerMgr.NearbyPlayer nearbyPlayer = this.m_nearbyPlayers[i];
			if (nearbyPlayer.m_bnetPlayer.IsCheatPlayer)
			{
				nearbyPlayer.m_lastReceivedTime = 0f;
				num++;
			}
		}
		this.RemoveInactivePlayers(changelist);
		this.FireChangeEvent(changelist);
		return num;
	}

	// Token: 0x04005704 RID: 22276
	private const int UDP_PORT = 1228;

	// Token: 0x04005705 RID: 22277
	private const float UPDATE_INTERVAL = 12f;

	// Token: 0x04005706 RID: 22278
	private const float INACTIVITY_TIMEOUT = 60f;

	// Token: 0x04005707 RID: 22279
	private static BnetNearbyPlayerMgr s_instance;

	// Token: 0x04005708 RID: 22280
	private bool m_enabled = true;

	// Token: 0x04005709 RID: 22281
	private bool m_listening;

	// Token: 0x0400570A RID: 22282
	private ulong m_myGameAccountLo;

	// Token: 0x0400570B RID: 22283
	private string m_bnetVersion;

	// Token: 0x0400570C RID: 22284
	private string m_bnetEnvironment;

	// Token: 0x0400570D RID: 22285
	private string m_idString;

	// Token: 0x0400570E RID: 22286
	private bool m_availability;

	// Token: 0x0400570F RID: 22287
	private PartyId m_partyId = PartyId.Empty;

	// Token: 0x04005710 RID: 22288
	private UdpClient m_client;

	// Token: 0x04005711 RID: 22289
	private int m_port;

	// Token: 0x04005712 RID: 22290
	private float m_lastCallTime;

	// Token: 0x04005713 RID: 22291
	private List<BnetNearbyPlayerMgr.NearbyPlayer> m_nearbyPlayers = new List<BnetNearbyPlayerMgr.NearbyPlayer>();

	// Token: 0x04005714 RID: 22292
	private List<BnetPlayer> m_nearbyBnetPlayers = new List<BnetPlayer>();

	// Token: 0x04005715 RID: 22293
	private List<BnetPlayer> m_nearbyFriends = new List<BnetPlayer>();

	// Token: 0x04005716 RID: 22294
	private List<BnetPlayer> m_nearbyStrangers = new List<BnetPlayer>();

	// Token: 0x04005717 RID: 22295
	private object m_mutex = new object();

	// Token: 0x04005718 RID: 22296
	private object m_mutexClient = new object();

	// Token: 0x04005719 RID: 22297
	private List<BnetNearbyPlayerMgr.NearbyPlayer> m_nearbyAdds = new List<BnetNearbyPlayerMgr.NearbyPlayer>();

	// Token: 0x0400571A RID: 22298
	private List<BnetNearbyPlayerMgr.NearbyPlayer> m_nearbyUpdates = new List<BnetNearbyPlayerMgr.NearbyPlayer>();

	// Token: 0x0400571B RID: 22299
	private List<BnetNearbyPlayerMgr.ChangeListener> m_changeListeners = new List<BnetNearbyPlayerMgr.ChangeListener>();

	// Token: 0x0400571C RID: 22300
	private byte[] m_broadcastBuffer;

	// Token: 0x0400571D RID: 22301
	private StringBuilder m_broadcastStringBuilder = new StringBuilder(128);

	// Token: 0x0400571E RID: 22302
	private IPEndPoint m_broadcastEndpoint;

	// Token: 0x0400571F RID: 22303
	private UdpClient m_broadcastSender;

	// Token: 0x04005720 RID: 22304
	private bool m_isBroadcasting;

	// Token: 0x0200233A RID: 9018
	// (Invoke) Token: 0x06012A44 RID: 76356
	public delegate void ChangeCallback(BnetNearbyPlayerChangelist changelist, object userData);

	// Token: 0x0200233B RID: 9019
	private class ChangeListener : global::EventListener<BnetNearbyPlayerMgr.ChangeCallback>
	{
		// Token: 0x06012A47 RID: 76359 RVA: 0x00511C34 File Offset: 0x0050FE34
		public void Fire(BnetNearbyPlayerChangelist changelist)
		{
			this.m_callback(changelist, this.m_userData);
		}
	}

	// Token: 0x0200233C RID: 9020
	private class NearbyPlayer : IEquatable<BnetNearbyPlayerMgr.NearbyPlayer>
	{
		// Token: 0x06012A49 RID: 76361 RVA: 0x00511C50 File Offset: 0x0050FE50
		public bool Equals(BnetNearbyPlayerMgr.NearbyPlayer other)
		{
			return other != null && this.GetGameAccountId() == other.GetGameAccountId();
		}

		// Token: 0x06012A4A RID: 76362 RVA: 0x00511C68 File Offset: 0x0050FE68
		public BnetAccountId GetAccountId()
		{
			return this.m_bnetPlayer.GetAccountId();
		}

		// Token: 0x06012A4B RID: 76363 RVA: 0x00511C75 File Offset: 0x0050FE75
		public BnetGameAccountId GetGameAccountId()
		{
			return this.m_bnetPlayer.GetHearthstoneGameAccountId();
		}

		// Token: 0x06012A4C RID: 76364 RVA: 0x00511C82 File Offset: 0x0050FE82
		public BnetGameAccount GetGameAccount()
		{
			return this.m_bnetPlayer.GetHearthstoneGameAccount();
		}

		// Token: 0x06012A4D RID: 76365 RVA: 0x00511C90 File Offset: 0x0050FE90
		public bool IsFriend()
		{
			BnetAccountId accountId = this.GetAccountId();
			return BnetFriendMgr.Get().IsFriend(accountId);
		}

		// Token: 0x0400E60D RID: 58893
		public float m_lastReceivedTime;

		// Token: 0x0400E60E RID: 58894
		public BnetPlayer m_bnetPlayer;

		// Token: 0x0400E60F RID: 58895
		public bool m_availability;

		// Token: 0x0400E610 RID: 58896
		public ulong m_sessionStartTime;

		// Token: 0x0400E611 RID: 58897
		public PartyId m_partyId = PartyId.Empty;
	}

	// Token: 0x0200233D RID: 9021
	private class UdpState
	{
		// Token: 0x0400E612 RID: 58898
		public UdpClient u;

		// Token: 0x0400E613 RID: 58899
		public IPEndPoint e;
	}
}
