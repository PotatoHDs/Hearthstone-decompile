using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using bgs;
using Hearthstone;
using UnityEngine;

public class BnetNearbyPlayerMgr
{
	public delegate void ChangeCallback(BnetNearbyPlayerChangelist changelist, object userData);

	private class ChangeListener : EventListener<ChangeCallback>
	{
		public void Fire(BnetNearbyPlayerChangelist changelist)
		{
			m_callback(changelist, m_userData);
		}
	}

	private class NearbyPlayer : IEquatable<NearbyPlayer>
	{
		public float m_lastReceivedTime;

		public BnetPlayer m_bnetPlayer;

		public bool m_availability;

		public ulong m_sessionStartTime;

		public PartyId m_partyId = PartyId.Empty;

		public bool Equals(NearbyPlayer other)
		{
			if (other == null)
			{
				return false;
			}
			return GetGameAccountId() == other.GetGameAccountId();
		}

		public BnetAccountId GetAccountId()
		{
			return m_bnetPlayer.GetAccountId();
		}

		public BnetGameAccountId GetGameAccountId()
		{
			return m_bnetPlayer.GetHearthstoneGameAccountId();
		}

		public BnetGameAccount GetGameAccount()
		{
			return m_bnetPlayer.GetHearthstoneGameAccount();
		}

		public bool IsFriend()
		{
			BnetAccountId accountId = GetAccountId();
			return BnetFriendMgr.Get().IsFriend(accountId);
		}
	}

	private class UdpState
	{
		public UdpClient u;

		public IPEndPoint e;
	}

	private const int UDP_PORT = 1228;

	private const float UPDATE_INTERVAL = 12f;

	private const float INACTIVITY_TIMEOUT = 60f;

	private static BnetNearbyPlayerMgr s_instance;

	private bool m_enabled = true;

	private bool m_listening;

	private ulong m_myGameAccountLo;

	private string m_bnetVersion;

	private string m_bnetEnvironment;

	private string m_idString;

	private bool m_availability;

	private PartyId m_partyId = PartyId.Empty;

	private UdpClient m_client;

	private int m_port;

	private float m_lastCallTime;

	private List<NearbyPlayer> m_nearbyPlayers = new List<NearbyPlayer>();

	private List<BnetPlayer> m_nearbyBnetPlayers = new List<BnetPlayer>();

	private List<BnetPlayer> m_nearbyFriends = new List<BnetPlayer>();

	private List<BnetPlayer> m_nearbyStrangers = new List<BnetPlayer>();

	private object m_mutex = new object();

	private object m_mutexClient = new object();

	private List<NearbyPlayer> m_nearbyAdds = new List<NearbyPlayer>();

	private List<NearbyPlayer> m_nearbyUpdates = new List<NearbyPlayer>();

	private List<ChangeListener> m_changeListeners = new List<ChangeListener>();

	private byte[] m_broadcastBuffer;

	private StringBuilder m_broadcastStringBuilder = new StringBuilder(128);

	private IPEndPoint m_broadcastEndpoint;

	private UdpClient m_broadcastSender;

	private bool m_isBroadcasting;

	public static BnetNearbyPlayerMgr Get()
	{
		if (s_instance == null)
		{
			s_instance = new BnetNearbyPlayerMgr();
			HearthstoneApplication.Get().WillReset += s_instance.Clear;
			FiresideGatheringManager.OnPatronListUpdated += NearbyPlayers_OnFSGPatronsUpdated;
		}
		return s_instance;
	}

	public void Initialize()
	{
		m_bnetVersion = BattleNet.GetVersion();
		m_bnetEnvironment = BattleNet.GetEnvironment();
		UpdateEnabled();
		Options.Get().RegisterChangedListener(Option.NEARBY_PLAYERS, OnEnabledOptionChanged);
		BnetFriendMgr.Get().AddChangeListener(OnFriendsChanged);
		FatalErrorMgr.Get().AddErrorListener(OnFatalError);
	}

	public void Shutdown()
	{
		StopListening();
		Options.Get().UnregisterChangedListener(Option.NEARBY_PLAYERS, OnEnabledOptionChanged);
		BnetFriendMgr.Get().RemoveChangeListener(OnFriendsChanged);
		if (FatalErrorMgr.Get() != null)
		{
			FatalErrorMgr.Get().RemoveErrorListener(OnFatalError);
		}
	}

	public bool IsEnabled()
	{
		if (TemporaryAccountManager.IsTemporaryAccount())
		{
			return false;
		}
		if (!Options.Get().GetBool(Option.NEARBY_PLAYERS))
		{
			return false;
		}
		if (!m_enabled)
		{
			return false;
		}
		return true;
	}

	public void SetEnabled(bool enabled)
	{
		m_enabled = enabled;
		UpdateEnabled();
	}

	public bool GetNearbySessionStartTime(BnetPlayer bnetPlayer, out ulong sessionStartTime)
	{
		sessionStartTime = 0uL;
		if (bnetPlayer == null)
		{
			return false;
		}
		NearbyPlayer nearbyPlayer = null;
		lock (m_mutex)
		{
			nearbyPlayer = m_nearbyPlayers.Find((NearbyPlayer obj) => obj.m_bnetPlayer.GetAccountId() == bnetPlayer.GetAccountId());
		}
		if (nearbyPlayer == null)
		{
			return false;
		}
		sessionStartTime = nearbyPlayer.m_sessionStartTime;
		return true;
	}

	public bool HasNearbyStrangers()
	{
		if (m_nearbyStrangers.Count > 0)
		{
			return m_nearbyStrangers.Any((BnetPlayer p) => p?.IsOnline() ?? false);
		}
		return false;
	}

	public List<BnetPlayer> GetNearbyPlayers()
	{
		return m_nearbyBnetPlayers;
	}

	public List<BnetPlayer> GetNearbyFriends()
	{
		return m_nearbyFriends;
	}

	public List<BnetPlayer> GetNearbyStrangers()
	{
		return m_nearbyStrangers;
	}

	public bool IsNearbyPlayer(BnetPlayer player)
	{
		return FindNearbyPlayer(player) != null;
	}

	public bool IsNearbyPlayer(BnetGameAccountId id)
	{
		return FindNearbyPlayer(id) != null;
	}

	public bool IsNearbyPlayer(BnetAccountId id)
	{
		return FindNearbyPlayer(id) != null;
	}

	public bool IsNearbyFriend(BnetPlayer player)
	{
		return FindNearbyFriend(player) != null;
	}

	public bool IsNearbyFriend(BnetGameAccountId id)
	{
		return FindNearbyFriend(id) != null;
	}

	public bool IsNearbyFriend(BnetAccountId id)
	{
		return FindNearbyFriend(id) != null;
	}

	public bool IsNearbyStranger(BnetPlayer player)
	{
		return FindNearbyStranger(player) != null;
	}

	public bool IsNearbyStranger(BnetGameAccountId id)
	{
		return FindNearbyStranger(id) != null;
	}

	public bool IsNearbyStranger(BnetAccountId id)
	{
		return FindNearbyStranger(id) != null;
	}

	public BnetPlayer FindNearbyPlayer(BnetPlayer player)
	{
		return FindNearbyPlayer(player, m_nearbyBnetPlayers);
	}

	public BnetPlayer FindNearbyPlayer(BnetGameAccountId id)
	{
		return FindNearbyPlayer(id, m_nearbyBnetPlayers);
	}

	public BnetPlayer FindNearbyPlayer(BnetAccountId id)
	{
		return FindNearbyPlayer(id, m_nearbyBnetPlayers);
	}

	public BnetPlayer FindNearbyFriend(BnetGameAccountId id)
	{
		return FindNearbyPlayer(id, m_nearbyFriends);
	}

	public BnetPlayer FindNearbyFriend(BnetPlayer player)
	{
		return FindNearbyPlayer(player, m_nearbyFriends);
	}

	public BnetPlayer FindNearbyFriend(BnetAccountId id)
	{
		return FindNearbyPlayer(id, m_nearbyFriends);
	}

	public BnetPlayer FindNearbyStranger(BnetPlayer player)
	{
		return FindNearbyPlayer(player, m_nearbyStrangers);
	}

	public BnetPlayer FindNearbyStranger(BnetGameAccountId id)
	{
		return FindNearbyPlayer(id, m_nearbyStrangers);
	}

	public BnetPlayer FindNearbyStranger(BnetAccountId id)
	{
		return FindNearbyPlayer(id, m_nearbyStrangers);
	}

	public bool GetAvailability()
	{
		return m_availability;
	}

	public void SetAvailability(bool av)
	{
		m_availability = av;
		CreateBroadcastString();
	}

	public void SetPartyId(PartyId partyId)
	{
		m_partyId = partyId ?? PartyId.Empty;
		CreateBroadcastString();
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

	private void BeginListening()
	{
		if (!m_listening)
		{
			m_listening = true;
			IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 1228);
			UdpClient udpClient = new UdpClient();
			udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, optionValue: true);
			udpClient.Client.Bind(iPEndPoint);
			m_port = 1228;
			m_client = udpClient;
			m_broadcastSender = new UdpClient();
			m_broadcastEndpoint = new IPEndPoint(IPAddress.Broadcast, m_port);
			UdpState udpState = new UdpState();
			udpState.e = iPEndPoint;
			udpState.u = m_client;
			m_lastCallTime = Time.realtimeSinceStartup;
			m_client.BeginReceive(OnUdpReceive, udpState);
		}
	}

	private void OnUdpReceive(IAsyncResult ar)
	{
		lock (m_mutexClient)
		{
			if (!m_listening)
			{
				return;
			}
		}
		UdpClient u = ((UdpState)ar.AsyncState).u;
		IPEndPoint remoteEP = ((UdpState)ar.AsyncState).e;
		if (u == null || remoteEP == null)
		{
			return;
		}
		byte[] bytes = u.EndReceive(ar, ref remoteEP);
		u.BeginReceive(OnUdpReceive, ar.AsyncState);
		string[] array = Encoding.UTF8.GetString(bytes).Split(',');
		ulong result = 0uL;
		ulong result2 = 0uL;
		ulong result3 = 0uL;
		ulong result4 = 0uL;
		int result5 = 0;
		bool flag = false;
		ulong result6 = 0uL;
		ulong result7 = 0uL;
		ulong result8 = 0uL;
		int num = 0;
		if (num >= array.Length || !ulong.TryParse(array[num++], out result) || num >= array.Length || !ulong.TryParse(array[num++], out result2) || num >= array.Length || !ulong.TryParse(array[num++], out result3) || num >= array.Length || !ulong.TryParse(array[num++], out result4) || m_myGameAccountLo == result4 || num >= array.Length)
		{
			return;
		}
		string name = array[num++];
		if (num >= array.Length || !int.TryParse(array[num++], out result5) || num >= array.Length)
		{
			return;
		}
		string text = array[num++];
		if (string.IsNullOrEmpty(text) || text != m_bnetVersion || num >= array.Length)
		{
			return;
		}
		string text2 = array[num++];
		if (string.IsNullOrEmpty(text2) || text2 != m_bnetEnvironment || num >= array.Length)
		{
			return;
		}
		string text3 = array[num++];
		if (text3 == "1")
		{
			flag = true;
		}
		else
		{
			if (!(text3 == "0"))
			{
				return;
			}
			flag = false;
		}
		if (num >= array.Length || !ulong.TryParse(array[num++], out result6) || num >= array.Length || !ulong.TryParse(array[num++], out result7) || num >= array.Length || !ulong.TryParse(array[num++], out result8))
		{
			return;
		}
		BnetBattleTag bnetBattleTag = new BnetBattleTag();
		bnetBattleTag.SetName(name);
		bnetBattleTag.SetNumber(result5);
		BnetAccountId bnetAccountId = new BnetAccountId();
		bnetAccountId.SetHi(result);
		bnetAccountId.SetLo(result2);
		BnetGameAccountId bnetGameAccountId = new BnetGameAccountId();
		bnetGameAccountId.SetHi(result3);
		bnetGameAccountId.SetLo(result4);
		PartyId partyId = new PartyId(result7, result8);
		BnetPlayer bnetPlayer = BnetPresenceMgr.Get().GetPlayer(bnetGameAccountId);
		if (bnetPlayer == null)
		{
			BnetAccount bnetAccount = new BnetAccount();
			bnetAccount.SetId(bnetAccountId);
			bnetAccount.SetBattleTag(bnetBattleTag);
			bnetAccount.SetAppearingOffline(appearingOffline: false);
			BnetGameAccount bnetGameAccount = new BnetGameAccount();
			bnetGameAccount.SetId(bnetGameAccountId);
			bnetGameAccount.SetOwnerId(bnetAccountId);
			bnetGameAccount.SetBattleTag(bnetBattleTag);
			bnetGameAccount.SetOnline(online: true);
			bnetGameAccount.SetProgramId(BnetProgramId.HEARTHSTONE);
			bnetGameAccount.SetGameField(1u, flag);
			bnetGameAccount.SetGameField(19u, text);
			bnetGameAccount.SetGameField(20u, text2);
			bnetGameAccount.SetGameField(26u, partyId.ToEntityId());
			bnetPlayer = new BnetPlayer(BnetPlayerSource.NEARBY_PLAYER);
			bnetPlayer.SetAccount(bnetAccount);
			bnetPlayer.AddGameAccount(bnetGameAccount);
		}
		NearbyPlayer nearbyPlayer = new NearbyPlayer();
		nearbyPlayer.m_bnetPlayer = bnetPlayer;
		nearbyPlayer.m_availability = flag;
		nearbyPlayer.m_partyId = partyId;
		nearbyPlayer.m_sessionStartTime = result6;
		lock (m_mutex)
		{
			if (!m_listening)
			{
				return;
			}
			foreach (NearbyPlayer nearbyAdd in m_nearbyAdds)
			{
				if (nearbyAdd.Equals(nearbyPlayer))
				{
					UpdateNearbyPlayer(nearbyAdd, flag, result6, partyId);
					return;
				}
			}
			foreach (NearbyPlayer nearbyUpdate in m_nearbyUpdates)
			{
				if (nearbyUpdate.Equals(nearbyPlayer))
				{
					UpdateNearbyPlayer(nearbyUpdate, flag, result6, partyId);
					return;
				}
			}
			foreach (NearbyPlayer nearbyPlayer2 in m_nearbyPlayers)
			{
				if (nearbyPlayer2.Equals(nearbyPlayer))
				{
					UpdateNearbyPlayer(nearbyPlayer2, flag, result6, partyId);
					m_nearbyUpdates.Add(nearbyPlayer2);
					return;
				}
			}
			m_nearbyAdds.Add(nearbyPlayer);
		}
	}

	private void StopListening()
	{
		lock (m_mutexClient)
		{
			if (!m_listening)
			{
				return;
			}
			m_listening = false;
			m_client.Close();
		}
		BnetNearbyPlayerChangelist bnetNearbyPlayerChangelist = new BnetNearbyPlayerChangelist();
		lock (m_mutex)
		{
			foreach (BnetPlayer nearbyBnetPlayer in m_nearbyBnetPlayers)
			{
				bnetNearbyPlayerChangelist.AddRemovedPlayer(nearbyBnetPlayer);
			}
			foreach (BnetPlayer nearbyFriend in m_nearbyFriends)
			{
				bnetNearbyPlayerChangelist.AddRemovedFriend(nearbyFriend);
			}
			foreach (BnetPlayer nearbyStranger in m_nearbyStrangers)
			{
				bnetNearbyPlayerChangelist.AddRemovedStranger(nearbyStranger);
			}
			m_nearbyPlayers.Clear();
			m_nearbyBnetPlayers.Clear();
			m_nearbyFriends.Clear();
			m_nearbyStrangers.Clear();
			m_nearbyAdds.Clear();
			m_nearbyUpdates.Clear();
		}
		FireChangeEvent(bnetNearbyPlayerChangelist);
		m_broadcastSender.Close();
	}

	public void Update()
	{
		if (m_listening)
		{
			CacheMyAccountInfo();
			CheckIntervalAndBroadcast();
			ProcessPlayerChanges();
		}
	}

	private void Clear()
	{
		lock (m_mutex)
		{
			m_nearbyPlayers.Clear();
			m_nearbyBnetPlayers.Clear();
			m_nearbyFriends.Clear();
			m_nearbyStrangers.Clear();
			m_nearbyAdds.Clear();
			m_nearbyUpdates.Clear();
		}
	}

	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		Clear();
	}

	private void UpdateEnabled()
	{
		bool flag = IsEnabled();
		if (flag != m_listening)
		{
			if (flag)
			{
				BeginListening();
			}
			else
			{
				StopListening();
			}
		}
	}

	private void FireChangeEvent(BnetNearbyPlayerChangelist changelist)
	{
		if (!changelist.IsEmpty())
		{
			ChangeListener[] array = m_changeListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Fire(changelist);
			}
		}
	}

	private void CacheMyAccountInfo()
	{
		if (m_idString != null)
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
		if (!(accountId == null))
		{
			BnetBattleTag battleTag = myPlayer.GetBattleTag();
			if (!(battleTag == null))
			{
				m_myGameAccountLo = myGameAccountId.GetLo();
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
				m_idString = stringBuilder.ToString();
				CreateBroadcastString();
			}
		}
	}

	private void ProcessPlayerChanges()
	{
		BnetNearbyPlayerChangelist changelist = new BnetNearbyPlayerChangelist();
		lock (m_mutex)
		{
			ProcessAddedPlayers(changelist);
			ProcessUpdatedPlayers(changelist);
			RemoveInactivePlayers(changelist);
		}
		FireChangeEvent(changelist);
	}

	private void ProcessAddedPlayers(BnetNearbyPlayerChangelist changelist)
	{
		if (m_nearbyAdds.Count == 0)
		{
			return;
		}
		for (int i = 0; i < m_nearbyAdds.Count; i++)
		{
			NearbyPlayer nearbyPlayer = m_nearbyAdds[i];
			nearbyPlayer.m_lastReceivedTime = Time.realtimeSinceStartup;
			BnetGameAccountId id = nearbyPlayer.GetGameAccount().GetId();
			if (BnetPresenceMgr.Get().GetPlayer(id) == null)
			{
				BnetPresenceMgr.Get().RegisterBnetPlayer(nearbyPlayer.m_bnetPlayer);
			}
			m_nearbyPlayers.Add(nearbyPlayer);
			m_nearbyBnetPlayers.Add(nearbyPlayer.m_bnetPlayer);
			changelist.AddAddedPlayer(nearbyPlayer.m_bnetPlayer);
			if (nearbyPlayer.IsFriend())
			{
				m_nearbyFriends.Add(nearbyPlayer.m_bnetPlayer);
				changelist.AddAddedFriend(nearbyPlayer.m_bnetPlayer);
			}
			else
			{
				m_nearbyStrangers.Add(nearbyPlayer.m_bnetPlayer);
				changelist.AddAddedStranger(nearbyPlayer.m_bnetPlayer);
			}
		}
		m_nearbyAdds.Clear();
	}

	private void ProcessUpdatedPlayers(BnetNearbyPlayerChangelist changelist)
	{
		if (m_nearbyUpdates.Count == 0)
		{
			return;
		}
		for (int i = 0; i < m_nearbyUpdates.Count; i++)
		{
			NearbyPlayer nearbyPlayer = m_nearbyUpdates[i];
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
		m_nearbyUpdates.Clear();
	}

	private void RemoveInactivePlayers(BnetNearbyPlayerChangelist changelist)
	{
		List<NearbyPlayer> list = null;
		for (int i = 0; i < m_nearbyPlayers.Count; i++)
		{
			NearbyPlayer nearbyPlayer = m_nearbyPlayers[i];
			if (Time.realtimeSinceStartup - nearbyPlayer.m_lastReceivedTime >= 60f)
			{
				if (list == null)
				{
					list = new List<NearbyPlayer>();
				}
				list.Add(nearbyPlayer);
			}
		}
		if (list == null)
		{
			return;
		}
		foreach (NearbyPlayer item in list)
		{
			m_nearbyPlayers.Remove(item);
			if (m_nearbyBnetPlayers.Remove(item.m_bnetPlayer))
			{
				changelist.AddRemovedPlayer(item.m_bnetPlayer);
			}
			if (m_nearbyFriends.Remove(item.m_bnetPlayer))
			{
				changelist.AddRemovedFriend(item.m_bnetPlayer);
			}
			if (m_nearbyStrangers.Remove(item.m_bnetPlayer))
			{
				changelist.AddRemovedStranger(item.m_bnetPlayer);
			}
		}
	}

	private bool CheckIntervalAndBroadcast()
	{
		if (!IsMyPlayerOnline())
		{
			return false;
		}
		if (Time.realtimeSinceStartup - m_lastCallTime < 12f)
		{
			return false;
		}
		m_lastCallTime = Time.realtimeSinceStartup;
		Broadcast();
		return true;
	}

	private async void Broadcast()
	{
		if (m_isBroadcasting)
		{
			return;
		}
		m_isBroadcasting = true;
		try
		{
			m_broadcastSender.EnableBroadcast = true;
			await m_broadcastSender.SendAsync(m_broadcastBuffer, m_broadcastBuffer.Length, m_broadcastEndpoint);
		}
		catch
		{
		}
		finally
		{
			m_isBroadcasting = false;
		}
	}

	private void CreateBroadcastString()
	{
		ulong sessionStartTime = HealthyGamingMgr.Get().GetSessionStartTime();
		m_broadcastStringBuilder.Clear();
		m_broadcastStringBuilder.Append(m_idString);
		m_broadcastStringBuilder.Append(',');
		m_broadcastStringBuilder.Append(m_availability ? "1" : "0");
		m_broadcastStringBuilder.Append(',');
		m_broadcastStringBuilder.Append(sessionStartTime);
		m_broadcastStringBuilder.Append(',');
		m_broadcastStringBuilder.Append(m_partyId.Hi);
		m_broadcastStringBuilder.Append(',');
		m_broadcastStringBuilder.Append(m_partyId.Lo);
		string s = m_broadcastStringBuilder.ToString();
		m_broadcastBuffer = Encoding.UTF8.GetBytes(s);
	}

	private int FindNearbyPlayerIndex(BnetPlayer bnetPlayer, List<BnetPlayer> bnetPlayers)
	{
		if (bnetPlayer == null)
		{
			return -1;
		}
		BnetAccountId accountId = bnetPlayer.GetAccountId();
		if (accountId != null)
		{
			return FindNearbyPlayerIndex(accountId, bnetPlayers);
		}
		BnetGameAccountId hearthstoneGameAccountId = bnetPlayer.GetHearthstoneGameAccountId();
		return FindNearbyPlayerIndex(hearthstoneGameAccountId, bnetPlayers);
	}

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

	private BnetPlayer FindNearbyPlayer(BnetPlayer bnetPlayer, List<BnetPlayer> bnetPlayers)
	{
		if (bnetPlayer == null)
		{
			return null;
		}
		BnetAccountId accountId = bnetPlayer.GetAccountId();
		if (accountId != null)
		{
			return FindNearbyPlayer(accountId, bnetPlayers);
		}
		BnetGameAccountId hearthstoneGameAccountId = bnetPlayer.GetHearthstoneGameAccountId();
		return FindNearbyPlayer(hearthstoneGameAccountId, bnetPlayers);
	}

	private BnetPlayer FindNearbyPlayer(BnetGameAccountId id, List<BnetPlayer> bnetPlayers)
	{
		int num = FindNearbyPlayerIndex(id, bnetPlayers);
		if (num < 0)
		{
			return null;
		}
		return bnetPlayers[num];
	}

	private BnetPlayer FindNearbyPlayer(BnetAccountId id, List<BnetPlayer> bnetPlayers)
	{
		int num = FindNearbyPlayerIndex(id, bnetPlayers);
		if (num < 0)
		{
			return null;
		}
		return bnetPlayers[num];
	}

	private void UpdateNearbyPlayer(NearbyPlayer player, bool available, ulong sessionStartTime, PartyId partyId)
	{
		BnetGameAccount gameAccount = player.GetGameAccount();
		bool num = BnetPresenceMgr.Get().IsSubscribedToPlayer(gameAccount.GetId());
		BnetPlayer player2 = BnetPresenceMgr.Get().GetPlayer(gameAccount.GetId());
		if (num && player2 != null)
		{
			player.m_bnetPlayer = player2;
		}
		else
		{
			gameAccount.SetGameField(1u, available);
			gameAccount.SetGameField(26u, partyId.ToEntityId());
		}
		player.m_sessionStartTime = sessionStartTime;
	}

	private bool IsMyPlayerOnline()
	{
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		if (myPlayer == null)
		{
			return false;
		}
		if (myPlayer.IsOnline())
		{
			return !myPlayer.IsAppearingOffline();
		}
		return false;
	}

	private void OnEnabledOptionChanged(Option option, object prevValue, bool existed, object userData)
	{
		UpdateEnabled();
	}

	private void OnFriendsChanged(BnetFriendChangelist friendChangelist, object userData)
	{
		List<BnetPlayer> addedFriends = friendChangelist.GetAddedFriends();
		List<BnetPlayer> removedFriends = friendChangelist.GetRemovedFriends();
		bool num = addedFriends != null && addedFriends.Count > 0;
		bool flag = removedFriends != null && removedFriends.Count > 0;
		if (!num && !flag)
		{
			return;
		}
		BnetNearbyPlayerChangelist bnetNearbyPlayerChangelist = new BnetNearbyPlayerChangelist();
		lock (m_mutex)
		{
			if (addedFriends != null)
			{
				foreach (BnetPlayer item in addedFriends)
				{
					int num2 = FindNearbyPlayerIndex(item, m_nearbyStrangers);
					if (num2 >= 0)
					{
						BnetPlayer bnetPlayer = m_nearbyStrangers[num2];
						m_nearbyStrangers.RemoveAt(num2);
						m_nearbyFriends.Add(bnetPlayer);
						bnetNearbyPlayerChangelist.AddAddedFriend(bnetPlayer);
						bnetNearbyPlayerChangelist.AddRemovedStranger(bnetPlayer);
					}
				}
			}
			if (removedFriends != null)
			{
				foreach (BnetPlayer item2 in removedFriends)
				{
					int num3 = FindNearbyPlayerIndex(item2, m_nearbyFriends);
					if (num3 >= 0)
					{
						BnetPlayer bnetPlayer2 = m_nearbyFriends[num3];
						m_nearbyFriends.RemoveAt(num3);
						m_nearbyStrangers.Add(bnetPlayer2);
						bnetNearbyPlayerChangelist.AddAddedStranger(bnetPlayer2);
						bnetNearbyPlayerChangelist.AddRemovedFriend(bnetPlayer2);
					}
				}
			}
		}
		FireChangeEvent(bnetNearbyPlayerChangelist);
	}

	private static void NearbyPlayers_OnFSGPatronsUpdated(List<BnetPlayer> addedPatrons, List<BnetPlayer> removedPatrons)
	{
		BnetNearbyPlayerChangelist bnetNearbyPlayerChangelist = null;
		if (addedPatrons != null)
		{
			foreach (BnetPlayer addedPatron in addedPatrons)
			{
				if (Get().IsNearbyPlayer(addedPatron))
				{
					if (bnetNearbyPlayerChangelist == null)
					{
						bnetNearbyPlayerChangelist = new BnetNearbyPlayerChangelist();
					}
					bnetNearbyPlayerChangelist.AddRemovedPlayer(addedPatron);
				}
			}
		}
		if (removedPatrons != null)
		{
			foreach (BnetPlayer removedPatron in removedPatrons)
			{
				if (Get().IsNearbyPlayer(removedPatron))
				{
					if (bnetNearbyPlayerChangelist == null)
					{
						bnetNearbyPlayerChangelist = new BnetNearbyPlayerChangelist();
					}
					bnetNearbyPlayerChangelist.AddAddedPlayer(removedPatron);
				}
			}
		}
		if (bnetNearbyPlayerChangelist != null)
		{
			Get().FireChangeEvent(bnetNearbyPlayerChangelist);
		}
	}

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
		NearbyPlayer nearbyPlayer = new NearbyPlayer();
		nearbyPlayer.m_bnetPlayer = bnetPlayer;
		nearbyPlayer.m_availability = true;
		nearbyPlayer.m_partyId = PartyId.Empty;
		m_nearbyAdds.Add(nearbyPlayer);
		ProcessAddedPlayers(bnetNearbyPlayerChangelist);
		return bnetPlayer;
	}

	public int Cheat_RemoveCheatFriends()
	{
		int num = 0;
		BnetNearbyPlayerChangelist changelist = new BnetNearbyPlayerChangelist();
		for (int num2 = m_nearbyPlayers.Count - 1; num2 >= 0; num2--)
		{
			NearbyPlayer nearbyPlayer = m_nearbyPlayers[num2];
			if (nearbyPlayer.m_bnetPlayer.IsCheatPlayer)
			{
				nearbyPlayer.m_lastReceivedTime = 0f;
				num++;
			}
		}
		RemoveInactivePlayers(changelist);
		FireChangeEvent(changelist);
		return num;
	}
}
