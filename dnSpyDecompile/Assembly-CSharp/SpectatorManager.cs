using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Assets;
using bgs;
using bgs.types;
using Blizzard.T5.Core;
using bnet.protocol;
using Hearthstone;
using Hearthstone.Core;
using PegasusGame;
using PegasusShared;
using SpectatorProto;
using UnityEngine;

// Token: 0x02000910 RID: 2320
public class SpectatorManager
{
	// Token: 0x14000085 RID: 133
	// (add) Token: 0x060080F6 RID: 33014 RVA: 0x0029E02C File Offset: 0x0029C22C
	// (remove) Token: 0x060080F7 RID: 33015 RVA: 0x0029E064 File Offset: 0x0029C264
	public event SpectatorManager.InviteReceivedHandler OnInviteReceived;

	// Token: 0x14000086 RID: 134
	// (add) Token: 0x060080F8 RID: 33016 RVA: 0x0029E09C File Offset: 0x0029C29C
	// (remove) Token: 0x060080F9 RID: 33017 RVA: 0x0029E0D4 File Offset: 0x0029C2D4
	public event SpectatorManager.InviteSentHandler OnInviteSent;

	// Token: 0x14000087 RID: 135
	// (add) Token: 0x060080FA RID: 33018 RVA: 0x0029E10C File Offset: 0x0029C30C
	// (remove) Token: 0x060080FB RID: 33019 RVA: 0x0029E144 File Offset: 0x0029C344
	public event Action OnSpectateRejected;

	// Token: 0x14000088 RID: 136
	// (add) Token: 0x060080FC RID: 33020 RVA: 0x0029E17C File Offset: 0x0029C37C
	// (remove) Token: 0x060080FD RID: 33021 RVA: 0x0029E1B4 File Offset: 0x0029C3B4
	public event SpectatorManager.SpectatorToMyGameHandler OnSpectatorToMyGame;

	// Token: 0x14000089 RID: 137
	// (add) Token: 0x060080FE RID: 33022 RVA: 0x0029E1EC File Offset: 0x0029C3EC
	// (remove) Token: 0x060080FF RID: 33023 RVA: 0x0029E224 File Offset: 0x0029C424
	public event SpectatorManager.SpectatorModeChangedHandler OnSpectatorModeChanged;

	// Token: 0x06008100 RID: 33024 RVA: 0x0029E259 File Offset: 0x0029C459
	public static SpectatorManager Get()
	{
		if (SpectatorManager.s_instance == null && SceneMgr.Get() != null)
		{
			SpectatorManager.CreateInstance();
		}
		return SpectatorManager.s_instance;
	}

	// Token: 0x06008101 RID: 33025 RVA: 0x0029E274 File Offset: 0x0029C474
	public static bool InstanceExists()
	{
		return SpectatorManager.s_instance != null;
	}

	// Token: 0x06008102 RID: 33026 RVA: 0x0029E280 File Offset: 0x0029C480
	public static JoinInfo GetSpectatorJoinInfo(BnetGameAccount gameAccount)
	{
		if (gameAccount == null)
		{
			return null;
		}
		byte[] array = gameAccount.GetGameFieldBytes(21U);
		if (array != null && array.Length != 0)
		{
			return ProtobufUtil.ParseFrom<JoinInfo>(array, 0, -1);
		}
		array = gameAccount.GetGameFieldBytes(23U);
		if (array != null && array.Length != 0)
		{
			try
			{
				SecretJoinInfo secretJoinInfo = ProtobufUtil.ParseFrom<SecretJoinInfo>(array, 0, -1);
				if (secretJoinInfo != null)
				{
					byte[] array2 = null;
					SecretSource source = secretJoinInfo.Source;
					if (source == SecretSource.SECRET_SOURCE_FIRESIDE_GATHERING && secretJoinInfo.HasSpecificSourceIdentity && secretJoinInfo.SpecificSourceIdentity == FiresideGatheringManager.Get().CurrentFsgId)
					{
						array2 = FiresideGatheringManager.Get().CurrentFsgSharedSecretKey;
					}
					if (array2 != null)
					{
						byte[] secretKey = SHA256.Create().ComputeHash(array2, 0, array2.Length);
						array = Crypto.Rijndael.Decrypt(secretJoinInfo.EncryptedMessage, secretKey);
						return ProtobufUtil.ParseFrom<JoinInfo>(array, 0, -1);
					}
				}
			}
			catch (Exception ex)
			{
				global::Log.All.PrintError("{0} parsing/decrypting secret JoinInfo, isInFsg={1}: {2}", new object[]
				{
					ex.GetType().Name,
					FiresideGatheringManager.Get().IsCheckedIn,
					ex.ToString()
				});
			}
		}
		JoinInfo spectatorJoinInfoForPlayer = PartyManager.Get().GetSpectatorJoinInfoForPlayer(gameAccount.GetId());
		if (spectatorJoinInfoForPlayer != null)
		{
			return spectatorJoinInfoForPlayer;
		}
		return null;
	}

	// Token: 0x06008103 RID: 33027 RVA: 0x0029E3A8 File Offset: 0x0029C5A8
	public static int GetSpectatorGameHandleFromPlayer(BnetPlayer player)
	{
		JoinInfo spectatorJoinInfo = SpectatorManager.GetSpectatorJoinInfo(player.GetHearthstoneGameAccount());
		if (spectatorJoinInfo == null)
		{
			return -1;
		}
		return spectatorJoinInfo.GameHandle;
	}

	// Token: 0x06008104 RID: 33028 RVA: 0x0029E3CC File Offset: 0x0029C5CC
	public static bool IsSpectatorSlotAvailable(JoinInfo info)
	{
		if (info == null)
		{
			return false;
		}
		if (!info.HasPartyId)
		{
			if (!info.HasServerIpAddress || !info.HasSecretKey)
			{
				return false;
			}
			if (string.IsNullOrEmpty(info.SecretKey))
			{
				return false;
			}
		}
		return (!info.HasIsJoinable || info.IsJoinable) && (!info.HasMaxNumSpectators || !info.HasCurrentNumSpectators || info.CurrentNumSpectators < info.MaxNumSpectators);
	}

	// Token: 0x06008105 RID: 33029 RVA: 0x0029E43A File Offset: 0x0029C63A
	public static bool IsSpectatorSlotAvailable(BnetGameAccount gameAccount)
	{
		return SpectatorManager.IsSpectatorSlotAvailable(SpectatorManager.GetSpectatorJoinInfo(gameAccount));
	}

	// Token: 0x06008106 RID: 33030 RVA: 0x0029E448 File Offset: 0x0029C648
	public void InitializeConnectedToBnet()
	{
		if (this.m_initialized)
		{
			return;
		}
		this.m_initialized = true;
		foreach (PartyInfo party in BnetParty.GetJoinedParties())
		{
			this.BnetParty_OnJoined(OnlineEventType.ADDED, party, null);
		}
		foreach (PartyInvite partyInvite in BnetParty.GetReceivedInvites())
		{
			this.BnetParty_OnReceivedInvite(OnlineEventType.ADDED, new PartyInfo(partyInvite.PartyId, partyInvite.PartyType), partyInvite.InviteId, null, null, null);
		}
	}

	// Token: 0x06008107 RID: 33031 RVA: 0x0029E4D8 File Offset: 0x0029C6D8
	private bool IsInSpectableContextWithPlayer(BnetGameAccountId gameAccountId)
	{
		BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(gameAccountId);
		return this.IsInSpectableContextWithPlayer(player);
	}

	// Token: 0x06008108 RID: 33032 RVA: 0x0029E4F8 File Offset: 0x0029C6F8
	private bool IsInSpectableContextWithPlayer(BnetPlayer player)
	{
		return player != null && (BnetFriendMgr.Get().IsFriend(player) || FiresideGatheringManager.Get().IsPlayerInMyFSG(player) || PartyManager.Get().IsPlayerInCurrentPartyOrPending(player.GetBestGameAccountId()));
	}

	// Token: 0x06008109 RID: 33033 RVA: 0x0029E534 File Offset: 0x0029C734
	public bool CanSpectate(BnetPlayer player)
	{
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		if (player == myPlayer)
		{
			return false;
		}
		if (!this.IsInSpectableContextWithPlayer(player))
		{
			return false;
		}
		BnetGameAccount hearthstoneGameAccount = player.GetHearthstoneGameAccount();
		BnetGameAccount hearthstoneGameAccount2 = myPlayer.GetHearthstoneGameAccount();
		if (hearthstoneGameAccount == null || hearthstoneGameAccount2 == null || !hearthstoneGameAccount.IsOnline() || !hearthstoneGameAccount2.IsOnline())
		{
			return false;
		}
		if (HearthstoneApplication.IsPublic() && (string.Compare(hearthstoneGameAccount.GetClientVersion(), hearthstoneGameAccount2.GetClientVersion()) != 0 || string.Compare(hearthstoneGameAccount.GetClientEnv(), hearthstoneGameAccount2.GetClientEnv()) != 0))
		{
			return false;
		}
		BnetGameAccountId hearthstoneGameAccountId = player.GetHearthstoneGameAccountId();
		JoinInfo spectatorJoinInfo = SpectatorManager.GetSpectatorJoinInfo(hearthstoneGameAccount);
		return this.CanSpectate(hearthstoneGameAccountId, spectatorJoinInfo);
	}

	// Token: 0x0600810A RID: 33034 RVA: 0x0029E5D8 File Offset: 0x0029C7D8
	public bool CanSpectate(BnetGameAccountId gameAccountId, JoinInfo joinInfo)
	{
		if (this.IsSpectatingPlayer(gameAccountId))
		{
			return false;
		}
		if (this.m_spectateeOpposingSide != null)
		{
			return false;
		}
		if (this.HasPreviouslyKickedMeFromGame(gameAccountId, (joinInfo == null) ? -1 : joinInfo.GameHandle) && !this.HasInvitedMeToSpectate(gameAccountId))
		{
			return false;
		}
		if (GameMgr.Get().IsFindingGame())
		{
			return false;
		}
		if (GameMgr.Get().IsNextSpectator())
		{
			return false;
		}
		if (FriendChallengeMgr.Get().HasChallenge())
		{
			return false;
		}
		if (!SpectatorManager.IsSpectatorSlotAvailable(joinInfo) && !this.HasInvitedMeToSpectate(gameAccountId))
		{
			return false;
		}
		if (GameMgr.Get().IsSpectator())
		{
			if (!this.IsPlayerInGame(gameAccountId))
			{
				return false;
			}
		}
		else if (SceneMgr.Get().IsInGame())
		{
			return false;
		}
		return GameUtils.AreAllTutorialsComplete() && SceneMgr.Get().GetMode() != SceneMgr.Mode.LOGIN && !BnetPresenceMgr.Get().GetMyPlayer().IsAppearingOffline() && (!PartyManager.Get().IsInParty() || PartyManager.Get().IsPlayerInCurrentPartyOrPending(gameAccountId));
	}

	// Token: 0x1700075A RID: 1882
	// (get) Token: 0x0600810B RID: 33035 RVA: 0x0029E6C9 File Offset: 0x0029C8C9
	public bool IsSpectatingOrWatching
	{
		get
		{
			return (GameMgr.Get() != null && GameMgr.Get().IsSpectator()) || this.IsInSpectatorMode();
		}
	}

	// Token: 0x0600810C RID: 33036 RVA: 0x0029E6EC File Offset: 0x0029C8EC
	public bool IsInSpectatorMode()
	{
		return !(this.m_spectateeFriendlySide == null) && !(this.m_spectatorPartyIdMain == null) && this.IsStillInParty(this.m_spectatorPartyIdMain) && this.m_initialized && !(this.GetPartyCreator(this.m_spectatorPartyIdMain) == null) && !this.ShouldBePartyLeader(this.m_spectatorPartyIdMain);
	}

	// Token: 0x0600810D RID: 33037 RVA: 0x0029E75A File Offset: 0x0029C95A
	public bool ShouldBeSpectatingInGame()
	{
		return !(this.m_spectatorPartyIdMain == null) && BnetParty.GetPartyAttributeBlob(this.m_spectatorPartyIdMain, "WTCG.Party.ServerInfo") != null;
	}

	// Token: 0x0600810E RID: 33038 RVA: 0x0029E781 File Offset: 0x0029C981
	public bool IsSpectatingPlayer(BnetGameAccountId gameAccountId)
	{
		return (this.m_spectateeFriendlySide != null && this.m_spectateeFriendlySide == gameAccountId) || (this.m_spectateeOpposingSide != null && this.m_spectateeOpposingSide == gameAccountId);
	}

	// Token: 0x0600810F RID: 33039 RVA: 0x0029E7C0 File Offset: 0x0029C9C0
	public bool IsSpectatingMe(BnetGameAccountId gameAccountId)
	{
		return !this.IsInSpectatorMode() && (this.m_gameServerKnownSpectators.Contains(gameAccountId) || (gameAccountId != BnetPresenceMgr.Get().GetMyGameAccountId() && BnetParty.IsMember(this.m_spectatorPartyIdMain, gameAccountId)));
	}

	// Token: 0x06008110 RID: 33040 RVA: 0x0029E800 File Offset: 0x0029CA00
	public int GetCountSpectatingMe()
	{
		if (this.m_spectatorPartyIdMain != null && !this.ShouldBePartyLeader(this.m_spectatorPartyIdMain))
		{
			return 0;
		}
		int count = this.m_gameServerKnownSpectators.Count;
		return Mathf.Max(BnetParty.CountMembers(this.m_spectatorPartyIdMain) - 1, count);
	}

	// Token: 0x06008111 RID: 33041 RVA: 0x0029E84A File Offset: 0x0029CA4A
	public bool IsBeingSpectated()
	{
		return this.GetCountSpectatingMe() > 0;
	}

	// Token: 0x06008112 RID: 33042 RVA: 0x0029E858 File Offset: 0x0029CA58
	public BnetGameAccountId[] GetSpectatorPartyMembers(bool friendlySide = true, bool includeSelf = false)
	{
		List<BnetGameAccountId> list = new List<BnetGameAccountId>(this.m_gameServerKnownSpectators);
		bgs.PartyMember[] members = BnetParty.GetMembers(friendlySide ? this.m_spectatorPartyIdMain : this.m_spectatorPartyIdOpposingSide);
		BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
		foreach (bgs.PartyMember partyMember in members)
		{
			if ((includeSelf || partyMember.GameAccountId != myGameAccountId) && !list.Contains(partyMember.GameAccountId))
			{
				list.Add(partyMember.GameAccountId);
			}
		}
		return list.ToArray();
	}

	// Token: 0x06008113 RID: 33043 RVA: 0x0029E8DA File Offset: 0x0029CADA
	public bool IsInSpectatableGame()
	{
		return SceneMgr.Get().IsInGame() && !GameMgr.Get().IsSpectator() && !SpectatorManager.IsGameOver;
	}

	// Token: 0x06008114 RID: 33044 RVA: 0x0029E902 File Offset: 0x0029CB02
	public bool IsInSpectatableScene()
	{
		return this.IsInSpectatableScene(false);
	}

	// Token: 0x06008115 RID: 33045 RVA: 0x0029E90B File Offset: 0x0029CB0B
	private bool IsInSpectatableScene(bool alsoCheckRequestedScene)
	{
		return SceneMgr.Get().IsInGame() || SpectatorManager.IsSpectatableScene(SceneMgr.Get().GetMode()) || (alsoCheckRequestedScene && SpectatorManager.IsSpectatableScene(SceneMgr.Get().GetNextMode()));
	}

	// Token: 0x06008116 RID: 33046 RVA: 0x0029E945 File Offset: 0x0029CB45
	private static bool IsSpectatableScene(SceneMgr.Mode sceneMode)
	{
		return sceneMode == SceneMgr.Mode.GAMEPLAY;
	}

	// Token: 0x06008117 RID: 33047 RVA: 0x0029E950 File Offset: 0x0029CB50
	public bool CanAddSpectators()
	{
		if (GameMgr.Get() != null && GameMgr.Get().IsSpectator())
		{
			return false;
		}
		if (this.m_spectateeFriendlySide != null || this.m_spectateeOpposingSide != null)
		{
			return false;
		}
		int countSpectatingMe = this.GetCountSpectatingMe();
		if (!this.IsInSpectatableGame())
		{
			if (this.m_spectatorPartyIdMain == null)
			{
				return false;
			}
			if (countSpectatingMe <= 0)
			{
				return false;
			}
		}
		if (countSpectatingMe >= 10)
		{
			return false;
		}
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		return myPlayer == null || !myPlayer.IsAppearingOffline();
	}

	// Token: 0x06008118 RID: 33048 RVA: 0x0029E9D8 File Offset: 0x0029CBD8
	public bool CanInviteToSpectateMyGame(BnetGameAccountId gameAccountId)
	{
		if (!this.CanAddSpectators())
		{
			return false;
		}
		BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
		if (gameAccountId == myGameAccountId)
		{
			return false;
		}
		if (!this.IsInSpectableContextWithPlayer(gameAccountId))
		{
			return false;
		}
		if (this.IsSpectatingMe(gameAccountId))
		{
			return false;
		}
		if (this.IsInvitedToSpectateMyGame(gameAccountId))
		{
			return false;
		}
		if (PartyManager.Get().IsPlayerInAnyParty(gameAccountId))
		{
			return false;
		}
		BnetGameAccount gameAccount = BnetPresenceMgr.Get().GetGameAccount(gameAccountId);
		if (gameAccount == null || !gameAccount.IsOnline())
		{
			return false;
		}
		if (!gameAccount.CanBeInvitedToGame() && !this.IsPlayerSpectatingMyGamesOpposingSide(gameAccountId))
		{
			return false;
		}
		BnetGameAccount hearthstoneGameAccount = BnetPresenceMgr.Get().GetMyPlayer().GetHearthstoneGameAccount();
		return string.Compare(gameAccount.GetClientVersion(), hearthstoneGameAccount.GetClientVersion()) == 0 && (!HearthstoneApplication.IsPublic() || string.Compare(gameAccount.GetClientEnv(), hearthstoneGameAccount.GetClientEnv()) == 0) && SceneMgr.Get().IsInGame();
	}

	// Token: 0x06008119 RID: 33049 RVA: 0x0029EAB8 File Offset: 0x0029CCB8
	public bool IsPlayerSpectatingMyGamesOpposingSide(BnetGameAccountId gameAccountId)
	{
		BnetGameAccount gameAccount = BnetPresenceMgr.Get().GetGameAccount(gameAccountId);
		if (gameAccount == null)
		{
			return false;
		}
		BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
		bool result = false;
		if (this.IsInSpectableContextWithPlayer(gameAccountId))
		{
			JoinInfo spectatorJoinInfo = SpectatorManager.GetSpectatorJoinInfo(gameAccount);
			global::Map<int, global::Player>.ValueCollection valueCollection = (GameState.Get() == null) ? null : GameState.Get().GetPlayerMap().Values;
			if (spectatorJoinInfo != null && spectatorJoinInfo.SpectatedPlayers.Count > 0 && valueCollection != null && valueCollection.Count > 0)
			{
				for (int i = 0; i < spectatorJoinInfo.SpectatedPlayers.Count; i++)
				{
					BnetGameAccountId spectatedPlayerId = BnetGameAccountId.CreateFromNet(spectatorJoinInfo.SpectatedPlayers[i]);
					if (spectatedPlayerId != myGameAccountId && valueCollection.Any((global::Player p) => p.GetGameAccountId() == spectatedPlayerId))
					{
						result = true;
						break;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x0600811A RID: 33050 RVA: 0x0029EB9C File Offset: 0x0029CD9C
	public bool IsInvitedToSpectateMyGame(BnetGameAccountId gameAccountId)
	{
		return BnetParty.GetSentInvites(this.m_spectatorPartyIdMain).FirstOrDefault((PartyInvite i) => i.InviteeId == gameAccountId) != null;
	}

	// Token: 0x0600811B RID: 33051 RVA: 0x0029EBD7 File Offset: 0x0029CDD7
	public bool CanKickSpectator(BnetGameAccountId gameAccountId)
	{
		return this.IsSpectatingMe(gameAccountId);
	}

	// Token: 0x0600811C RID: 33052 RVA: 0x0029EBE5 File Offset: 0x0029CDE5
	public bool HasInvitedMeToSpectate(BnetGameAccountId gameAccountId)
	{
		return BnetParty.GetReceivedInviteFrom(gameAccountId, PartyType.SPECTATOR_PARTY) != null;
	}

	// Token: 0x0600811D RID: 33053 RVA: 0x0029EBF3 File Offset: 0x0029CDF3
	public bool HasAnyReceivedInvites()
	{
		return (from i in BnetParty.GetReceivedInvites()
		where i.PartyType == PartyType.SPECTATOR_PARTY
		select i).ToArray<PartyInvite>().Length != 0;
	}

	// Token: 0x0600811E RID: 33054 RVA: 0x0029EC27 File Offset: 0x0029CE27
	public bool MyGameHasSpectators()
	{
		return SceneMgr.Get().IsInGame() && this.m_gameServerKnownSpectators.Count > 0;
	}

	// Token: 0x0600811F RID: 33055 RVA: 0x0029EC45 File Offset: 0x0029CE45
	public BnetGameAccountId GetSpectateeFriendlySide()
	{
		return this.m_spectateeFriendlySide;
	}

	// Token: 0x06008120 RID: 33056 RVA: 0x0029EC4D File Offset: 0x0029CE4D
	public BnetGameAccountId GetSpectateeOpposingSide()
	{
		return this.m_spectateeOpposingSide;
	}

	// Token: 0x06008121 RID: 33057 RVA: 0x0029EC55 File Offset: 0x0029CE55
	public bool IsSpectatingOpposingSide()
	{
		return this.m_spectateeOpposingSide != null;
	}

	// Token: 0x06008122 RID: 33058 RVA: 0x0029EC64 File Offset: 0x0029CE64
	public bool HasPreviouslyKickedMeFromGame(BnetGameAccountId playerId, int currentGameHandle)
	{
		if (this.m_kickedFromSpectatingList == null)
		{
			return false;
		}
		uint num = 0U;
		if (this.m_kickedFromSpectatingList.TryGetValue(playerId, out num))
		{
			if ((ulong)num == (ulong)((long)currentGameHandle))
			{
				return true;
			}
			this.m_kickedFromSpectatingList.Remove(playerId);
			if (this.m_kickedFromSpectatingList.Count == 0)
			{
				this.m_kickedFromSpectatingList = null;
			}
		}
		return false;
	}

	// Token: 0x06008123 RID: 33059 RVA: 0x0029ECB8 File Offset: 0x0029CEB8
	public void SpectatePlayer(BnetPlayer player)
	{
		if (!this.CanSpectate(player))
		{
			return;
		}
		BnetGameAccountId hearthstoneGameAccountId = player.GetHearthstoneGameAccountId();
		JoinInfo spectatorJoinInfo = SpectatorManager.GetSpectatorJoinInfo(player.GetHearthstoneGameAccount());
		this.SpectatePlayer(hearthstoneGameAccountId, spectatorJoinInfo);
	}

	// Token: 0x06008124 RID: 33060 RVA: 0x0029ECEC File Offset: 0x0029CEEC
	public void SpectatePlayer(BnetGameAccountId gameAccountId, JoinInfo joinInfo)
	{
		if (this.m_pendingSpectatePlayerAfterLeave != null)
		{
			return;
		}
		PartyInvite receivedInviteFrom = BnetParty.GetReceivedInviteFrom(gameAccountId, PartyType.SPECTATOR_PARTY);
		if (receivedInviteFrom != null)
		{
			if (this.m_spectateeFriendlySide == null || (this.m_spectateeOpposingSide == null && this.IsPlayerInGame(gameAccountId)))
			{
				this.CloseWaitingForNextGameDialog();
				if (this.m_spectateeFriendlySide != null && gameAccountId != this.m_spectateeFriendlySide)
				{
					this.m_spectateeOpposingSide = gameAccountId;
				}
				BnetParty.AcceptReceivedInvite(receivedInviteFrom.InviteId);
				this.HideShownUI();
				return;
			}
			this.LogInfoParty("SpectatePlayer: trying to accept an invite even though there is no room for another spectatee: player={0} spectatee1={1} spectatee2={2} isPlayerInGame={3} inviteId={4}", new object[]
			{
				string.Concat(new object[]
				{
					gameAccountId,
					" (",
					BnetUtils.GetPlayerBestName(gameAccountId),
					")"
				}),
				this.m_spectateeFriendlySide,
				this.m_spectateeOpposingSide,
				this.IsPlayerInGame(gameAccountId),
				receivedInviteFrom.InviteId
			});
			BnetParty.DeclineReceivedInvite(receivedInviteFrom.InviteId);
			return;
		}
		else
		{
			if (joinInfo == null)
			{
				global::Error.AddWarningLoc("Bad Spectator Key", "Spectator key is blank!", Array.Empty<object>());
				return;
			}
			if (!joinInfo.HasPartyId && string.IsNullOrEmpty(joinInfo.SecretKey))
			{
				global::Error.AddWarningLoc("No Party/Bad Spectator Key", "No party information and Spectator key is blank!", Array.Empty<object>());
				return;
			}
			if (joinInfo.HasPartyId && this.m_requestedInvite != null)
			{
				this.LogInfoParty("SpectatePlayer: already requesting invite from {0}:party={1}, cannot request another from {2}:party={3}", new object[]
				{
					this.m_requestedInvite.SpectateeId,
					this.m_requestedInvite.PartyId,
					gameAccountId,
					BnetUtils.CreatePartyId(joinInfo.PartyId)
				});
				return;
			}
			this.HideShownUI();
			if (!(this.m_spectateeFriendlySide != null) || !(this.m_spectateeOpposingSide == null) || GameMgr.Get() == null || !GameMgr.Get().IsSpectator())
			{
				if (this.m_spectatorPartyIdMain != null)
				{
					if (this.IsInSpectatorMode())
					{
						this.EndSpectatorMode(true);
					}
					else
					{
						this.LeaveParty(this.m_spectatorPartyIdMain, this.ShouldBePartyLeader(this.m_spectatorPartyIdMain));
					}
					this.m_pendingSpectatePlayerAfterLeave = new SpectatorManager.PendingSpectatePlayer(gameAccountId, joinInfo);
					return;
				}
				if (this.m_spectatorPartyIdOpposingSide != null)
				{
					this.m_pendingSpectatePlayerAfterLeave = new SpectatorManager.PendingSpectatePlayer(gameAccountId, joinInfo);
					this.LeaveParty(this.m_spectatorPartyIdOpposingSide, false);
					return;
				}
			}
			this.SpectatePlayer_Internal(gameAccountId, joinInfo);
			return;
		}
	}

	// Token: 0x06008125 RID: 33061 RVA: 0x0029EF28 File Offset: 0x0029D128
	private void HideShownUI()
	{
		ShownUIMgr shownUIMgr = ShownUIMgr.Get();
		if (shownUIMgr != null)
		{
			switch (shownUIMgr.GetShownUI())
			{
			case ShownUIMgr.UI_WINDOW.GENERAL_STORE:
				if (GeneralStore.Get() != null)
				{
					GeneralStore.Get().Close(false);
					return;
				}
				break;
			case ShownUIMgr.UI_WINDOW.ARENA_STORE:
				if (ArenaStore.Get() != null)
				{
					ArenaStore.Get().Hide();
					return;
				}
				break;
			case ShownUIMgr.UI_WINDOW.TAVERN_BRAWL_STORE:
				if (TavernBrawlStore.Get() != null)
				{
					TavernBrawlStore.Get().Hide();
				}
				break;
			case ShownUIMgr.UI_WINDOW.QUEST_LOG:
				if (QuestLog.Get() != null)
				{
					QuestLog.Get().Hide();
					return;
				}
				break;
			default:
				return;
			}
		}
	}

	// Token: 0x06008126 RID: 33062 RVA: 0x0029EFC4 File Offset: 0x0029D1C4
	private void FireSpectatorModeChanged(OnlineEventType evt, BnetPlayer spectatee)
	{
		if (FriendChallengeMgr.Get() != null)
		{
			FriendChallengeMgr.Get().UpdateMyAvailability();
		}
		if (this.OnSpectatorModeChanged != null)
		{
			this.OnSpectatorModeChanged(evt, spectatee);
		}
		if (evt == OnlineEventType.ADDED)
		{
			Screen.sleepTimeout = -1;
			return;
		}
		if (evt == OnlineEventType.REMOVED && SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY)
		{
			Screen.sleepTimeout = -2;
		}
	}

	// Token: 0x06008127 RID: 33063 RVA: 0x0029F01C File Offset: 0x0029D21C
	private void SpectatePlayer_Internal(BnetGameAccountId gameAccountId, JoinInfo joinInfo)
	{
		if (!this.m_initialized)
		{
			this.LogInfoParty("ERROR: SpectatePlayer_Internal called before initialized; spectatee={0}", new object[]
			{
				gameAccountId
			});
		}
		this.m_pendingSpectatePlayerAfterLeave = null;
		if (WelcomeQuests.Get() != null)
		{
			WelcomeQuests.Hide();
		}
		PartyInvite receivedInviteFrom = BnetParty.GetReceivedInviteFrom(gameAccountId, PartyType.SPECTATOR_PARTY);
		if (this.m_spectateeFriendlySide == null)
		{
			this.LogInfoPower("================== Begin Spectating 1st player ==================", Array.Empty<object>());
			this.m_spectateeFriendlySide = gameAccountId;
			if (receivedInviteFrom != null)
			{
				this.CloseWaitingForNextGameDialog();
				BnetParty.AcceptReceivedInvite(receivedInviteFrom.InviteId);
				return;
			}
			if (joinInfo.HasPartyId)
			{
				PartyId partyId = BnetUtils.CreatePartyId(joinInfo.PartyId);
				this.m_requestedInvite = new SpectatorManager.IntendedSpectateeParty(gameAccountId, partyId);
				BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
				BnetParty.RequestInvite(partyId, gameAccountId, myGameAccountId, PartyType.SPECTATOR_PARTY);
				Processor.ScheduleCallback(5f, true, new Processor.ScheduledCallback(this.SpectatePlayer_RequestInvite_FriendlySide_Timeout), null);
				return;
			}
			this.CloseWaitingForNextGameDialog();
			this.m_isExpectingArriveInGameplayAsSpectator = true;
			GameMgr.Get().SpectateGame(joinInfo);
			return;
		}
		else if (this.m_spectateeOpposingSide == null)
		{
			if (!this.IsPlayerInGame(gameAccountId))
			{
				global::Error.AddWarning(GameStrings.Get("GLOBAL_ERROR_GENERIC_HEADER"), GameStrings.Get("GLOBAL_SPECTATOR_ERROR_CANNOT_SPECTATE_2_GAMES"), Array.Empty<object>());
				return;
			}
			if (this.m_spectateeFriendlySide == gameAccountId)
			{
				this.LogInfoParty("SpectatePlayer: already spectating player {0}", new object[]
				{
					gameAccountId
				});
				if (receivedInviteFrom != null)
				{
					BnetParty.AcceptReceivedInvite(receivedInviteFrom.InviteId);
				}
				return;
			}
			this.LogInfoPower("================== Begin Spectating 2nd player ==================", Array.Empty<object>());
			this.m_spectateeOpposingSide = gameAccountId;
			if (receivedInviteFrom != null)
			{
				BnetParty.AcceptReceivedInvite(receivedInviteFrom.InviteId);
				return;
			}
			if (joinInfo.HasPartyId)
			{
				PartyId partyId2 = BnetUtils.CreatePartyId(joinInfo.PartyId);
				this.m_requestedInvite = new SpectatorManager.IntendedSpectateeParty(gameAccountId, partyId2);
				BnetGameAccountId myGameAccountId2 = BnetPresenceMgr.Get().GetMyGameAccountId();
				BnetParty.RequestInvite(partyId2, gameAccountId, myGameAccountId2, PartyType.SPECTATOR_PARTY);
				Processor.ScheduleCallback(5f, true, new Processor.ScheduledCallback(this.SpectatePlayer_RequestInvite_OpposingSide_Timeout), null);
				return;
			}
			this.SpectateSecondPlayer_Network(joinInfo);
			return;
		}
		else
		{
			if (this.m_spectateeFriendlySide == gameAccountId || this.m_spectateeOpposingSide == gameAccountId)
			{
				this.LogInfoParty("SpectatePlayer: already spectating player {0}", new object[]
				{
					gameAccountId
				});
				return;
			}
			global::Error.AddDevFatal("Cannot spectate more than 2 players.", Array.Empty<object>());
			return;
		}
	}

	// Token: 0x06008128 RID: 33064 RVA: 0x0029F238 File Offset: 0x0029D438
	private void SpectatePlayer_RequestInvite_FriendlySide_Timeout(object userData)
	{
		if (this.m_requestedInvite == null)
		{
			return;
		}
		this.m_spectateeFriendlySide = null;
		this.EndSpectatorMode(true);
		string header = GameStrings.Get("GLOBAL_SPECTATOR_SERVER_REJECTED_HEADER");
		string body = GameStrings.Get("GLOBAL_SPECTATOR_SERVER_REJECTED_TEXT");
		SpectatorManager.DisplayErrorDialog(header, body);
		if (this.OnSpectateRejected != null)
		{
			this.OnSpectateRejected();
		}
	}

	// Token: 0x06008129 RID: 33065 RVA: 0x0029F28C File Offset: 0x0029D48C
	private void SpectatePlayer_RequestInvite_OpposingSide_Timeout(object userData)
	{
		if (this.m_requestedInvite == null)
		{
			return;
		}
		this.m_requestedInvite = null;
		this.m_spectateeOpposingSide = null;
		string header = GameStrings.Get("GLOBAL_SPECTATOR_SERVER_REJECTED_HEADER");
		string body = GameStrings.Get("GLOBAL_SPECTATOR_SERVER_REJECTED_TEXT");
		SpectatorManager.DisplayErrorDialog(header, body);
		if (this.OnSpectateRejected != null)
		{
			this.OnSpectateRejected();
		}
	}

	// Token: 0x0600812A RID: 33066 RVA: 0x0029F2E0 File Offset: 0x0029D4E0
	private static JoinInfo CreateJoinInfo(PartyServerInfo serverInfo)
	{
		JoinInfo joinInfo = new JoinInfo();
		joinInfo.ServerIpAddress = serverInfo.ServerIpAddress;
		joinInfo.ServerPort = serverInfo.ServerPort;
		joinInfo.GameHandle = serverInfo.GameHandle;
		joinInfo.SecretKey = serverInfo.SecretKey;
		if (serverInfo.HasGameType)
		{
			joinInfo.GameType = serverInfo.GameType;
		}
		if (serverInfo.HasFormatType)
		{
			joinInfo.FormatType = serverInfo.FormatType;
		}
		if (serverInfo.HasMissionId)
		{
			joinInfo.MissionId = serverInfo.MissionId;
		}
		return joinInfo;
	}

	// Token: 0x0600812B RID: 33067 RVA: 0x0029F360 File Offset: 0x0029D560
	private static bool IsSameGameAndServer(PartyServerInfo a, PartyServerInfo b)
	{
		if (a == null)
		{
			return b == null;
		}
		return b != null && a.ServerIpAddress == b.ServerIpAddress && a.GameHandle == b.GameHandle;
	}

	// Token: 0x0600812C RID: 33068 RVA: 0x0029F392 File Offset: 0x0029D592
	private static bool IsSameGameAndServer(PartyServerInfo a, GameServerInfo b)
	{
		if (a == null)
		{
			return b == null;
		}
		return b != null && a.ServerIpAddress == b.Address && (long)a.GameHandle == (long)((ulong)b.GameHandle);
	}

	// Token: 0x0600812D RID: 33069 RVA: 0x0029F3C8 File Offset: 0x0029D5C8
	private void SpectateSecondPlayer_Network(JoinInfo joinInfo)
	{
		GameServerInfo gameServerInfo = new GameServerInfo();
		gameServerInfo.Address = joinInfo.ServerIpAddress;
		gameServerInfo.Port = joinInfo.ServerPort;
		gameServerInfo.GameHandle = (uint)joinInfo.GameHandle;
		gameServerInfo.SpectatorPassword = joinInfo.SecretKey;
		gameServerInfo.SpectatorMode = true;
		Network.Get().SpectateSecondPlayer(gameServerInfo);
	}

	// Token: 0x0600812E RID: 33070 RVA: 0x0029F420 File Offset: 0x0029D620
	private void JoinPartyGame(PartyId partyId)
	{
		if (partyId == null)
		{
			return;
		}
		PartyInfo joinedParty = BnetParty.GetJoinedParty(partyId);
		if (joinedParty == null)
		{
			return;
		}
		this.BnetParty_OnPartyAttributeChanged_ServerInfo(joinedParty, "WTCG.Party.ServerInfo", BnetParty.GetPartyAttributeVariant(partyId, "WTCG.Party.ServerInfo"));
	}

	// Token: 0x0600812F RID: 33071 RVA: 0x0029F45C File Offset: 0x0029D65C
	public void LeaveSpectatorMode()
	{
		if (GameMgr.Get().IsSpectator())
		{
			if (Network.Get().IsConnectedToGameServer())
			{
				Network.Get().DisconnectFromGameServer();
			}
			else
			{
				this.LeaveGameScene();
			}
		}
		if (this.m_spectatorPartyIdOpposingSide != null)
		{
			this.LeaveParty(this.m_spectatorPartyIdOpposingSide, false);
		}
		if (this.m_spectatorPartyIdMain != null)
		{
			this.LeaveParty(this.m_spectatorPartyIdMain, false);
		}
	}

	// Token: 0x06008130 RID: 33072 RVA: 0x0029F4CC File Offset: 0x0029D6CC
	public void InviteToSpectateMe(BnetPlayer player)
	{
		if (player == null)
		{
			return;
		}
		BnetGameAccountId hearthstoneGameAccountId = player.GetHearthstoneGameAccountId();
		if (this.m_kickedPlayers != null && this.m_kickedPlayers.Contains(hearthstoneGameAccountId))
		{
			this.m_kickedPlayers.Remove(hearthstoneGameAccountId);
		}
		if (!this.CanInviteToSpectateMyGame(hearthstoneGameAccountId))
		{
			return;
		}
		if (this.m_userInitiatedOutgoingInvites == null)
		{
			this.m_userInitiatedOutgoingInvites = new HashSet<BnetGameAccountId>();
		}
		this.m_userInitiatedOutgoingInvites.Add(hearthstoneGameAccountId);
		if (this.m_spectatorPartyIdMain == null)
		{
			byte[] creatorBlob = ProtobufUtil.ToByteArray(BnetUtils.CreatePegasusBnetId(BnetPresenceMgr.Get().GetMyGameAccountId()));
			BnetParty.CreateParty(PartyType.SPECTATOR_PARTY, PrivacyLevel.OPEN_INVITATION, creatorBlob, null);
			return;
		}
		BnetParty.SendInvite(this.m_spectatorPartyIdMain, hearthstoneGameAccountId, false);
	}

	// Token: 0x06008131 RID: 33073 RVA: 0x0029F56B File Offset: 0x0029D76B
	public void KickSpectator(BnetPlayer player, bool regenerateSpectatorPassword)
	{
		this.KickSpectator_Internal(player, regenerateSpectatorPassword, true);
	}

	// Token: 0x06008132 RID: 33074 RVA: 0x0029F578 File Offset: 0x0029D778
	private void KickSpectator_Internal(BnetPlayer player, bool regenerateSpectatorPassword, bool addToKickList)
	{
		if (player == null)
		{
			return;
		}
		BnetGameAccountId hearthstoneGameAccountId = player.GetHearthstoneGameAccountId();
		if (!this.CanKickSpectator(hearthstoneGameAccountId))
		{
			return;
		}
		if (addToKickList)
		{
			if (this.m_kickedPlayers == null)
			{
				this.m_kickedPlayers = new HashSet<BnetGameAccountId>();
			}
			this.m_kickedPlayers.Add(hearthstoneGameAccountId);
		}
		if (Network.Get().IsConnectedToGameServer())
		{
			Network.Get().SendRemoveSpectators(regenerateSpectatorPassword, new BnetGameAccountId[]
			{
				hearthstoneGameAccountId
			});
		}
		if (this.m_spectatorPartyIdMain != null && this.ShouldBePartyLeader(this.m_spectatorPartyIdMain) && BnetParty.IsMember(this.m_spectatorPartyIdMain, hearthstoneGameAccountId))
		{
			BnetParty.KickMember(this.m_spectatorPartyIdMain, hearthstoneGameAccountId);
		}
	}

	// Token: 0x06008133 RID: 33075 RVA: 0x0029F614 File Offset: 0x0029D814
	public void UpdateMySpectatorInfo()
	{
		this.UpdateSpectatorPresence();
		this.UpdateSpectatorPartyServerInfo();
	}

	// Token: 0x06008134 RID: 33076 RVA: 0x0029F624 File Offset: 0x0029D824
	private JoinInfo GetMyGameJoinInfo()
	{
		JoinInfo result = null;
		JoinInfo joinInfo = new JoinInfo();
		if (this.IsInSpectatorMode())
		{
			if (this.m_spectateeFriendlySide != null)
			{
				BnetId item = BnetUtils.CreatePegasusBnetId(this.m_spectateeFriendlySide);
				joinInfo.SpectatedPlayers.Add(item);
			}
			if (this.m_spectateeOpposingSide != null)
			{
				BnetId item2 = BnetUtils.CreatePegasusBnetId(this.m_spectateeOpposingSide);
				joinInfo.SpectatedPlayers.Add(item2);
			}
			if (joinInfo.SpectatedPlayers.Count > 0)
			{
				result = joinInfo;
			}
		}
		else if (SceneMgr.Get().IsInGame())
		{
			int countSpectatingMe = this.GetCountSpectatingMe();
			if (this.CanAddSpectators())
			{
				GameServerInfo lastGameServerJoined = Network.Get().GetLastGameServerJoined();
				if (this.m_spectatorPartyIdMain == null && lastGameServerJoined != null && SceneMgr.Get().IsInGame() && !SpectatorManager.IsGameOver)
				{
					joinInfo.ServerIpAddress = lastGameServerJoined.Address;
					joinInfo.ServerPort = lastGameServerJoined.Port;
					joinInfo.GameHandle = (int)lastGameServerJoined.GameHandle;
					joinInfo.SecretKey = (lastGameServerJoined.SpectatorPassword ?? "");
				}
				if (this.m_spectatorPartyIdMain != null)
				{
					BnetId partyId = BnetUtils.CreatePegasusBnetId(this.m_spectatorPartyIdMain);
					joinInfo.PartyId = partyId;
					joinInfo.GameHandle = (int)lastGameServerJoined.GameHandle;
				}
			}
			joinInfo.CurrentNumSpectators = countSpectatingMe;
			joinInfo.MaxNumSpectators = 10;
			joinInfo.IsJoinable = (joinInfo.CurrentNumSpectators < joinInfo.MaxNumSpectators);
			joinInfo.GameType = GameMgr.Get().GetGameType();
			joinInfo.FormatType = GameMgr.Get().GetFormatType();
			joinInfo.MissionId = GameMgr.Get().GetMissionId();
			result = joinInfo;
		}
		return result;
	}

	// Token: 0x06008135 RID: 33077 RVA: 0x0029F7BC File Offset: 0x0029D9BC
	private static PartyServerInfo GetPartyServerInfo(PartyId partyId)
	{
		byte[] partyAttributeBlob = BnetParty.GetPartyAttributeBlob(partyId, "WTCG.Party.ServerInfo");
		if (partyAttributeBlob != null)
		{
			return ProtobufUtil.ParseFrom<PartyServerInfo>(partyAttributeBlob, 0, -1);
		}
		return null;
	}

	// Token: 0x06008136 RID: 33078 RVA: 0x0029F7E2 File Offset: 0x0029D9E2
	public bool HandleDisconnectFromGameplay()
	{
		bool flag = this.m_expectedDisconnectReason != null;
		this.EndCurrentSpectatedGame(false);
		if (flag)
		{
			if (GameMgr.Get().IsTransitionPopupShown())
			{
				GameMgr.Get().GetTransitionPopup().Cancel();
				return flag;
			}
			this.LeaveGameScene();
		}
		return flag;
	}

	// Token: 0x06008137 RID: 33079 RVA: 0x0029F81B File Offset: 0x0029DA1B
	public void OnRealTimeGameOver()
	{
		this.UpdateMySpectatorInfo();
	}

	// Token: 0x06008138 RID: 33080 RVA: 0x0029F824 File Offset: 0x0029DA24
	private void EndCurrentSpectatedGame(bool isLeavingGameplay)
	{
		if (isLeavingGameplay && this.IsInSpectatorMode())
		{
			SoundManager.Get().LoadAndPlay("SpectatorMode_Exit.prefab:f1d7dab96facdc64fb6648ff1dd22073");
		}
		this.m_expectedDisconnectReason = null;
		this.m_isExpectingArriveInGameplayAsSpectator = false;
		this.ClearAllGameServerKnownSpectators();
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null && !hearthstoneApplication.IsResetting())
		{
			this.UpdateSpectatorPresence();
		}
		if (GameMgr.Get() != null && GameMgr.Get().GetTransitionPopup() != null)
		{
			GameMgr.Get().GetTransitionPopup().OnHidden -= this.EnterSpectatorMode_OnTransitionPopupHide;
		}
	}

	// Token: 0x06008139 RID: 33081 RVA: 0x0029F8BC File Offset: 0x0029DABC
	private void EndSpectatorMode(bool wasKnownSpectating = false)
	{
		bool isExpectingArriveInGameplayAsSpectator = this.m_isExpectingArriveInGameplayAsSpectator;
		bool flag = wasKnownSpectating || this.m_spectateeFriendlySide != null || this.m_spectateeOpposingSide != null;
		this.LeaveSpectatorMode();
		this.EndCurrentSpectatedGame(false);
		this.m_spectateeFriendlySide = null;
		this.m_spectateeOpposingSide = null;
		this.m_requestedInvite = null;
		this.CloseWaitingForNextGameDialog();
		this.m_pendingSpectatePlayerAfterLeave = null;
		this.m_isExpectingArriveInGameplayAsSpectator = false;
		if (flag)
		{
			this.LogInfoPower("================== End Spectator Mode ==================", Array.Empty<object>());
			BnetPlayer player = BnetUtils.GetPlayer(this.m_spectateeFriendlySide);
			this.FireSpectatorModeChanged(OnlineEventType.REMOVED, player);
		}
		SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
		if (postGameSceneMode != SceneMgr.Mode.HUB && postGameSceneMode != SceneMgr.Mode.INVALID)
		{
			return;
		}
		if (PartyManager.Get().IsInParty())
		{
			return;
		}
		if (!HearthstoneApplication.Get().IsResetting())
		{
			if (isExpectingArriveInGameplayAsSpectator)
			{
				this.ReturnToHub(true);
				return;
			}
			this.ReturnToHub(false);
		}
	}

	// Token: 0x0600813A RID: 33082 RVA: 0x0029F98C File Offset: 0x0029DB8C
	public void ReturnToHub(bool allowReloadHub = false)
	{
		SceneMgr.Mode mode = SceneMgr.Mode.HUB;
		bool flag = SceneMgr.Get().GetMode() == mode;
		if (!GameUtils.AreAllTutorialsComplete() && Network.ShouldBeConnectedToAurora())
		{
			Network.Get().ShowBreakingNewsOrError("GLOBAL_ERROR_NETWORK_LOST_GAME_CONNECTION", 0f);
		}
		else if (!SceneMgr.Get().IsModeRequested(mode))
		{
			SceneMgr.Get().SetNextMode(mode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		}
		else if (flag && allowReloadHub)
		{
			SceneMgr.Get().ReloadMode();
		}
		if (flag && !allowReloadHub)
		{
			this.CheckShowWaitingForNextGameDialog();
		}
	}

	// Token: 0x0600813B RID: 33083 RVA: 0x0029FA04 File Offset: 0x0029DC04
	private void ClearAllCacheForReset()
	{
		this.EndSpectatorMode(false);
		this.m_initialized = false;
		this.m_spectatorPartyIdMain = null;
		this.m_spectatorPartyIdOpposingSide = null;
		this.m_requestedInvite = null;
		this.m_waitingForNextGameDialog = null;
		this.m_pendingSpectatePlayerAfterLeave = null;
		this.m_userInitiatedOutgoingInvites = null;
		this.m_kickedPlayers = null;
		this.m_kickedFromSpectatingList = null;
		this.m_expectedDisconnectReason = null;
		this.m_isExpectingArriveInGameplayAsSpectator = false;
		this.m_isShowingRemovedAsSpectatorPopup = false;
		this.m_gameServerKnownSpectators.Clear();
	}

	// Token: 0x0600813C RID: 33084 RVA: 0x0029FA7C File Offset: 0x0029DC7C
	private void WillReset()
	{
		this.ClearAllCacheForReset();
		Processor.CancelScheduledCallback(new Processor.ScheduledCallback(this.SpectatorManager_UpdatePresenceNextFrame), null);
	}

	// Token: 0x0600813D RID: 33085 RVA: 0x0029FA98 File Offset: 0x0029DC98
	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		FindGameState state = eventData.m_state;
		if (state - FindGameState.CLIENT_CANCELED > 1 && state - FindGameState.BNET_QUEUE_CANCELED > 1)
		{
			if (state == FindGameState.SERVER_GAME_CANCELED)
			{
				if (this.IsInSpectatorMode())
				{
					string header = GameStrings.Get("GLOBAL_SPECTATOR_SERVER_REJECTED_HEADER");
					string body = GameStrings.Get("GLOBAL_SPECTATOR_SERVER_REJECTED_TEXT");
					SpectatorManager.DisplayErrorDialog(header, body);
					this.EndSpectatorMode(true);
					if (this.OnSpectateRejected != null)
					{
						this.OnSpectateRejected();
					}
				}
			}
		}
		else if (this.IsInSpectatorMode())
		{
			this.EndSpectatorMode(true);
		}
		return false;
	}

	// Token: 0x0600813E RID: 33086 RVA: 0x0029FB0E File Offset: 0x0029DD0E
	private void GameState_InitializedEvent(GameState instance, object userData)
	{
		if (this.m_spectatorPartyIdOpposingSide != null)
		{
			GameState.Get().RegisterCreateGameListener(new GameState.CreateGameCallback(this.GameState_CreateGameEvent), null);
		}
	}

	// Token: 0x0600813F RID: 33087 RVA: 0x0029FB36 File Offset: 0x0029DD36
	private void GameState_CreateGameEvent(GameState.CreateGamePhase createGamePhase, object userData)
	{
		if (createGamePhase < GameState.CreateGamePhase.CREATED)
		{
			return;
		}
		GameState.Get().UnregisterCreateGameListener(new GameState.CreateGameCallback(this.GameState_CreateGameEvent));
		if (this.m_spectatorPartyIdOpposingSide != null)
		{
			this.AutoSpectateOpposingSide();
		}
	}

	// Token: 0x06008140 RID: 33088 RVA: 0x0029FB68 File Offset: 0x0029DD68
	private void AutoSpectateOpposingSide()
	{
		if (GameState.Get() == null)
		{
			return;
		}
		if (GameState.Get().GetCreateGamePhase() < GameState.CreateGamePhase.CREATED)
		{
			GameState.Get().RegisterCreateGameListener(new GameState.CreateGameCallback(this.GameState_CreateGameEvent), null);
			return;
		}
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY)
		{
			return;
		}
		if (GameMgr.Get().GetTransitionPopup() != null && GameMgr.Get().GetTransitionPopup().IsShown())
		{
			GameMgr.Get().GetTransitionPopup().OnHidden += this.EnterSpectatorMode_OnTransitionPopupHide;
			return;
		}
		if (this.m_spectatorPartyIdOpposingSide != null && this.m_spectateeOpposingSide != null && this.IsStillInParty(this.m_spectatorPartyIdOpposingSide))
		{
			if (this.IsPlayerInGame(this.m_spectateeOpposingSide))
			{
				PartyServerInfo partyServerInfo = SpectatorManager.GetPartyServerInfo(this.m_spectatorPartyIdOpposingSide);
				JoinInfo joinInfo = (partyServerInfo == null) ? null : SpectatorManager.CreateJoinInfo(partyServerInfo);
				if (joinInfo != null)
				{
					this.SpectateSecondPlayer_Network(joinInfo);
					return;
				}
			}
			else
			{
				this.LogInfoPower("================== End Spectating 2nd player ==================", Array.Empty<object>());
				this.LeaveParty(this.m_spectatorPartyIdOpposingSide, false);
			}
		}
	}

	// Token: 0x06008141 RID: 33089 RVA: 0x0029FC6C File Offset: 0x0029DE6C
	private void OnSceneUnloaded(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode == SceneMgr.Mode.GAMEPLAY)
		{
			this.m_gameServerKnownSpectators.Clear();
		}
		if (mode == SceneMgr.Mode.GAMEPLAY && prevMode != SceneMgr.Mode.GAMEPLAY)
		{
			if (this.m_spectateeFriendlySide != null)
			{
				BnetBar.Get().HideFriendList();
			}
			if (GameMgr.Get().IsSpectator())
			{
				if (GameMgr.Get().GetTransitionPopup() != null)
				{
					GameMgr.Get().GetTransitionPopup().OnHidden += this.EnterSpectatorMode_OnTransitionPopupHide;
				}
				BnetPlayer player = BnetUtils.GetPlayer(this.m_spectateeOpposingSide ?? this.m_spectateeFriendlySide);
				this.FireSpectatorModeChanged(OnlineEventType.ADDED, player);
			}
			else
			{
				this.m_kickedPlayers = null;
			}
			this.CloseWaitingForNextGameDialog();
			this.DeclineAllReceivedInvitations();
			this.UpdateMySpectatorInfo();
			return;
		}
		if (prevMode == SceneMgr.Mode.GAMEPLAY && mode != SceneMgr.Mode.GAMEPLAY)
		{
			if (this.IsInSpectatorMode())
			{
				this.LogInfoPower("================== End Spectator Game ==================", Array.Empty<object>());
				TimeScaleMgr.Get().SetGameTimeScale(1f);
			}
			this.EndCurrentSpectatedGame(true);
			this.UpdateMySpectatorInfo();
			if (this.IsInSpectatorMode())
			{
				PartyServerInfo partyServerInfo = SpectatorManager.GetPartyServerInfo(this.m_spectatorPartyIdMain);
				if (partyServerInfo == null)
				{
					this.ShowWaitingForNextGameDialog();
					return;
				}
				GameServerInfo lastGameServerJoined = Network.Get().GetLastGameServerJoined();
				if (!SpectatorManager.IsSameGameAndServer(partyServerInfo, lastGameServerJoined))
				{
					this.LogInfoPower("================== OnSceneUnloaded: auto-spectating game after leaving game ==================", Array.Empty<object>());
					this.BnetParty_OnPartyAttributeChanged_ServerInfo(new PartyInfo(this.m_spectatorPartyIdMain, PartyType.SPECTATOR_PARTY), "WTCG.Party.ServerInfo", BnetParty.GetPartyAttributeVariant(this.m_spectatorPartyIdMain, "WTCG.Party.ServerInfo"));
					return;
				}
				this.ShowWaitingForNextGameDialog();
			}
		}
	}

	// Token: 0x06008142 RID: 33090 RVA: 0x0029FDE0 File Offset: 0x0029DFE0
	public void CheckShowWaitingForNextGameDialog()
	{
		bool flag = true;
		if (!this.IsInSpectatorMode())
		{
			flag = false;
		}
		else if (SceneMgr.Get().GetNextMode() != SceneMgr.Mode.INVALID)
		{
			flag = false;
		}
		else if (this.IsInSpectatableScene(true))
		{
			flag = false;
		}
		if (flag)
		{
			this.ShowWaitingForNextGameDialog();
			return;
		}
		this.CloseWaitingForNextGameDialog();
	}

	// Token: 0x06008143 RID: 33091 RVA: 0x0029FE28 File Offset: 0x0029E028
	public void ShowWaitingForNextGameDialog()
	{
		if (!Network.IsLoggedIn())
		{
			return;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_id = "SPECTATOR_WAITING_FOR_NEXT_GAME";
		popupInfo.m_layerToUse = new GameLayer?(GameLayer.UI);
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_SPECTATOR_WAITING_FOR_NEXT_GAME_HEADER");
		popupInfo.m_text = this.GetWaitingForNextGameDialogText();
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CANCEL;
		popupInfo.m_cancelText = GameStrings.Get("GLOBAL_LEAVE_SPECTATOR_MODE");
		popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnSceneUnloaded_AwaitingNextGame_LeaveSpectatorMode);
		popupInfo.m_keyboardEscIsCancel = false;
		DialogManager.Get().ShowUniquePopup(popupInfo, new DialogManager.DialogProcessCallback(this.OnSceneUnloaded_AwaitingNextGame_DialogProcessCallback));
		Processor.CancelScheduledCallback(new Processor.ScheduledCallback(SpectatorManager.WaitingForNextGame_AutoLeaveSpectatorMode), null);
		if (SpectatorManager.WAITING_FOR_NEXT_GAME_AUTO_LEAVE_SECONDS >= 0f)
		{
			Processor.ScheduleCallback(SpectatorManager.WAITING_FOR_NEXT_GAME_AUTO_LEAVE_SECONDS, true, new Processor.ScheduledCallback(SpectatorManager.WaitingForNextGame_AutoLeaveSpectatorMode), null);
		}
	}

	// Token: 0x06008144 RID: 33092 RVA: 0x0029FF00 File Offset: 0x0029E100
	private void CloseWaitingForNextGameDialog()
	{
		if (SpectatorManager.DISABLE_MENU_BUTTON_WHILE_WAITING)
		{
			BnetBar.Get().m_menuButton.SetEnabled(true, false);
		}
		if (DialogManager.Get() != null)
		{
			DialogManager.Get().RemoveUniquePopupRequestFromQueue("SPECTATOR_WAITING_FOR_NEXT_GAME");
		}
		if (this.m_waitingForNextGameDialog != null)
		{
			this.m_waitingForNextGameDialog.Hide();
			this.m_waitingForNextGameDialog = null;
		}
		Processor.CancelScheduledCallback(new Processor.ScheduledCallback(SpectatorManager.WaitingForNextGame_AutoLeaveSpectatorMode), null);
	}

	// Token: 0x06008145 RID: 33093 RVA: 0x0029FF7C File Offset: 0x0029E17C
	private void UpdateWaitingForNextGameDialog()
	{
		if (this.m_waitingForNextGameDialog == null)
		{
			return;
		}
		string waitingForNextGameDialogText = this.GetWaitingForNextGameDialogText();
		this.m_waitingForNextGameDialog.BodyText = waitingForNextGameDialogText;
	}

	// Token: 0x06008146 RID: 33094 RVA: 0x0029FFAC File Offset: 0x0029E1AC
	private string GetWaitingForNextGameDialogText()
	{
		BnetPlayer player = BnetUtils.GetPlayer(this.m_spectateeFriendlySide);
		string playerBestName = BnetUtils.GetPlayerBestName(this.m_spectateeFriendlySide);
		string text;
		string key;
		if (player != null && player.IsOnline())
		{
			text = (PresenceMgr.Get().GetStatusText(player) ?? "");
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Trim();
				key = "GLOBAL_SPECTATOR_WAITING_FOR_NEXT_GAME_TEXT";
			}
			else
			{
				key = "GLOBAL_SPECTATOR_WAITING_FOR_NEXT_GAME_TEXT_ONLINE";
			}
			Enum[] statusEnums = PresenceMgr.Get().GetStatusEnums(player);
			if (statusEnums.Length != 0 && (Global.PresenceStatus)statusEnums[0] == Global.PresenceStatus.ADVENTURE_SCENARIO_SELECT)
			{
				if (statusEnums.Length > 1 && (PresenceAdventureMode)statusEnums[1] < PresenceAdventureMode.RETURNING_PLAYER_CHALLENGE)
				{
					key = "GLOBAL_SPECTATOR_WAITING_FOR_NEXT_GAME_TEXT_ENTERING";
				}
			}
			else if (statusEnums.Length != 0 && (Global.PresenceStatus)statusEnums[0] == Global.PresenceStatus.ADVENTURE_SCENARIO_PLAYING_GAME)
			{
				if (statusEnums.Length > 1 && GameUtils.IsHeroicAdventureMission((int)((ScenarioDbId)statusEnums[1])))
				{
					key = "GLOBAL_SPECTATOR_WAITING_FOR_NEXT_GAME_TEXT_BATTLING";
				}
				else if (statusEnums.Length > 1 && GameUtils.IsClassChallengeMission((int)((ScenarioDbId)statusEnums[1])))
				{
					key = "GLOBAL_SPECTATOR_WAITING_FOR_NEXT_GAME_TEXT_PLAYING";
				}
			}
		}
		else
		{
			key = "GLOBAL_SPECTATOR_WAITING_FOR_NEXT_GAME_TEXT_OFFLINE";
			text = GameStrings.Get("GLOBAL_OFFLINE");
		}
		return GameStrings.Format(key, new object[]
		{
			playerBestName,
			text
		});
	}

	// Token: 0x06008147 RID: 33095 RVA: 0x002A00C8 File Offset: 0x0029E2C8
	private bool OnSceneUnloaded_AwaitingNextGame_DialogProcessCallback(DialogBase dialog, object userData)
	{
		if (SceneMgr.Get().IsInGame() || (GameMgr.Get() != null && GameMgr.Get().IsFindingGame()))
		{
			return false;
		}
		this.m_waitingForNextGameDialog = (AlertPopup)dialog;
		this.UpdateWaitingForNextGameDialog();
		if (SpectatorManager.DISABLE_MENU_BUTTON_WHILE_WAITING)
		{
			BnetBar.Get().m_menuButton.SetEnabled(false, false);
		}
		return true;
	}

	// Token: 0x06008148 RID: 33096 RVA: 0x002A0128 File Offset: 0x0029E328
	private static void WaitingForNextGame_AutoLeaveSpectatorMode(object userData)
	{
		if (!SpectatorManager.Get().IsInSpectatorMode() || SceneMgr.Get().IsInGame())
		{
			return;
		}
		SpectatorManager.Get().LeaveSpectatorMode();
		string header = GameStrings.Get("GLOBAL_SPECTATOR_WAITING_FOR_NEXT_GAME_HEADER");
		string body = GameStrings.Format("GLOBAL_SPECTATOR_WAITING_FOR_NEXT_GAME_TIMEOUT", Array.Empty<object>());
		SpectatorManager.DisplayErrorDialog(header, body);
	}

	// Token: 0x06008149 RID: 33097 RVA: 0x002A0178 File Offset: 0x0029E378
	private void OnSceneUnloaded_AwaitingNextGame_LeaveSpectatorMode(AlertPopup.Response response, object userData)
	{
		this.LeaveSpectatorMode();
	}

	// Token: 0x0600814A RID: 33098 RVA: 0x002A0180 File Offset: 0x0029E380
	private void EnterSpectatorMode_OnTransitionPopupHide(TransitionPopup popup)
	{
		popup.OnHidden -= this.EnterSpectatorMode_OnTransitionPopupHide;
		if (SoundManager.Get() != null)
		{
			SoundManager.Get().LoadAndPlay("SpectatorMode_Enter.prefab:e0c11cb0f554e6c4cb9f24994bf13e1c");
		}
		if (this.m_spectateeOpposingSide != null)
		{
			this.AutoSpectateOpposingSide();
		}
	}

	// Token: 0x0600814B RID: 33099 RVA: 0x002A01D0 File Offset: 0x0029E3D0
	private void OnSpectatorOpenJoinOptionChanged(global::Option option, object prevValue, bool existed, object userData)
	{
		bool @bool = Options.Get().GetBool(global::Option.SPECTATOR_OPEN_JOIN);
		if ((!existed || (bool)prevValue != @bool) && HearthstoneServices.IsAvailable<SceneMgr>() && SceneMgr.Get().IsInGame() && (GameMgr.Get() == null || !GameMgr.Get().IsSpectator()))
		{
			JoinInfo presenceSpectatorJoinInfo;
			if (@bool)
			{
				presenceSpectatorJoinInfo = this.GetMyGameJoinInfo();
			}
			else
			{
				presenceSpectatorJoinInfo = null;
			}
			if (Network.ShouldBeConnectedToAurora() && Network.IsLoggedIn())
			{
				BnetPresenceMgr.Get().SetPresenceSpectatorJoinInfo(presenceSpectatorJoinInfo);
			}
		}
	}

	// Token: 0x0600814C RID: 33100 RVA: 0x002A0250 File Offset: 0x0029E450
	private void Network_OnSpectatorInviteReceived(Invite protoInvite)
	{
		BnetGameAccountId inviterId = BnetGameAccountId.CreateFromNet(protoInvite.InviterGameAccountId);
		this.AddReceivedInvitation(inviterId, protoInvite.JoinInfo);
	}

	// Token: 0x0600814D RID: 33101 RVA: 0x002A0278 File Offset: 0x0029E478
	private void Network_OnSpectatorInviteReceived_ResponseCallback(AlertPopup.Response response, object userData)
	{
		BnetGameAccountId bnetGameAccountId = (BnetGameAccountId)userData;
		if (response == AlertPopup.Response.CANCEL)
		{
			this.RemoveReceivedInvitation(bnetGameAccountId);
			return;
		}
		BnetPlayer player = BnetUtils.GetPlayer(bnetGameAccountId);
		if (player == null)
		{
			return;
		}
		this.SpectatePlayer(player);
	}

	// Token: 0x0600814E RID: 33102 RVA: 0x002A02AC File Offset: 0x0029E4AC
	private void Network_OnSpectatorNotifyEvent()
	{
		SpectatorNotify spectatorNotify = Network.Get().GetSpectatorNotify();
		if (spectatorNotify == null)
		{
			TelemetryManager.Client().SendLiveIssue("Network_OnSpectatorNotifyEvent Exception", "'notify' is null.");
			return;
		}
		if (spectatorNotify.HasSpectatorPasswordUpdate && !string.IsNullOrEmpty(spectatorNotify.SpectatorPasswordUpdate))
		{
			GameServerInfo lastGameServerJoined = Network.Get().GetLastGameServerJoined();
			if (lastGameServerJoined == null)
			{
				TelemetryManager.Client().SendLiveIssue("Network_OnSpectatorNotifyEvent Exception", "'serverInfo' is null.");
			}
			else if (!spectatorNotify.SpectatorPasswordUpdate.Equals(lastGameServerJoined.SpectatorPassword))
			{
				lastGameServerJoined.SpectatorPassword = spectatorNotify.SpectatorPasswordUpdate;
				this.UpdateMySpectatorInfo();
				this.RevokeAllSentInvitations();
			}
		}
		if (spectatorNotify.HasSpectatorRemoved)
		{
			this.m_expectedDisconnectReason = new int?(spectatorNotify.SpectatorRemoved.ReasonCode);
			GameMgr gameMgr = GameMgr.Get();
			if (gameMgr == null)
			{
				TelemetryManager.Client().SendLiveIssue("Network_OnSpectatorNotifyEvent Exception", "GameMgr is null.");
			}
			bool flag = gameMgr != null && gameMgr.IsTransitionPopupShown();
			if (spectatorNotify.SpectatorRemoved.ReasonCode == 0)
			{
				if (spectatorNotify.SpectatorRemoved.HasRemovedBy)
				{
					GameServerInfo lastGameServerJoined2 = Network.Get().GetLastGameServerJoined();
					if (lastGameServerJoined2 != null)
					{
						if (this.m_kickedFromSpectatingList == null)
						{
							this.m_kickedFromSpectatingList = new global::Map<BnetGameAccountId, uint>();
						}
						BnetGameAccountId key = BnetGameAccountId.CreateFromNet(spectatorNotify.SpectatorRemoved.RemovedBy);
						this.m_kickedFromSpectatingList[key] = lastGameServerJoined2.GameHandle;
					}
				}
				if (!this.m_isShowingRemovedAsSpectatorPopup)
				{
					AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
					popupInfo.m_headerText = GameStrings.Get("GLOBAL_SPECTATOR_REMOVED_PROMPT_HEADER");
					popupInfo.m_text = GameStrings.Get("GLOBAL_SPECTATOR_REMOVED_PROMPT_TEXT");
					popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
					if (flag)
					{
						popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.Network_OnSpectatorNotifyEvent_Removed_GoToNextMode);
					}
					else
					{
						popupInfo.m_responseCallback = delegate(AlertPopup.Response r, object data)
						{
							SpectatorManager spectatorManager = SpectatorManager.Get();
							if (spectatorManager != null)
							{
								spectatorManager.m_isShowingRemovedAsSpectatorPopup = false;
								return;
							}
							TelemetryManager.Client().SendLiveIssue("Network_OnSpectatorNotifyEvent Exception", "SpectatorManager is null in response callback.");
						};
					}
					this.m_isShowingRemovedAsSpectatorPopup = true;
					DialogManager dialogManager = DialogManager.Get();
					if (dialogManager != null)
					{
						dialogManager.ShowPopup(popupInfo);
					}
					else
					{
						TelemetryManager.Client().SendLiveIssue("Network_OnSpectatorNotifyEvent Exception", "DialogManager is null.");
					}
				}
			}
			else if (flag)
			{
				this.Network_OnSpectatorNotifyEvent_Removed_GoToNextMode(AlertPopup.Response.OK, null);
			}
			SoundManager soundManager = SoundManager.Get();
			if (soundManager != null)
			{
				soundManager.LoadAndPlay("SpectatorMode_Exit.prefab:f1d7dab96facdc64fb6648ff1dd22073");
			}
			else
			{
				TelemetryManager.Client().SendLiveIssue("Network_OnSpectatorNotifyEvent Exception", "SoundManager is null.");
			}
			this.EndSpectatorMode(true);
			this.m_expectedDisconnectReason = new int?(spectatorNotify.SpectatorRemoved.ReasonCode);
		}
		if (spectatorNotify == null || spectatorNotify.SpectatorChange.Count == 0)
		{
			return;
		}
		if (GameMgr.Get() != null && GameMgr.Get().IsSpectator())
		{
			return;
		}
		foreach (SpectatorChange spectatorChange in spectatorNotify.SpectatorChange)
		{
			BnetGameAccountId gameAccountId = BnetGameAccountId.CreateFromNet(spectatorChange.GameAccountId);
			if (spectatorChange.IsRemoved)
			{
				this.RemoveKnownSpectator(gameAccountId);
			}
			else
			{
				this.AddKnownSpectator(gameAccountId);
				this.ReinviteKnownSpectatorsNotInParty();
			}
		}
	}

	// Token: 0x0600814F RID: 33103 RVA: 0x002A0590 File Offset: 0x0029E790
	private void Network_OnSpectatorNotifyEvent_Removed_GoToNextMode(AlertPopup.Response response, object userData)
	{
		this.m_isShowingRemovedAsSpectatorPopup = false;
	}

	// Token: 0x06008150 RID: 33104 RVA: 0x002A059C File Offset: 0x0029E79C
	private void ReceivedInvitation_ExpireTimeout(object userData)
	{
		this.PruneOldInvites();
		if (this.m_receivedSpectateMeInvites.Count > 0)
		{
			float num = this.m_receivedSpectateMeInvites.Min((KeyValuePair<BnetGameAccountId, SpectatorManager.ReceivedInvite> kv) => kv.Value.m_timestamp);
			Processor.ScheduleCallback(Mathf.Max(0f, num + 300f - Time.realtimeSinceStartup), true, new Processor.ScheduledCallback(this.ReceivedInvitation_ExpireTimeout), null);
		}
	}

	// Token: 0x06008151 RID: 33105 RVA: 0x002A0614 File Offset: 0x0029E814
	private void Presence_OnGameAccountPresenceChange(PresenceUpdate[] updates)
	{
		for (int k = 0; k < updates.Length; k++)
		{
			PresenceUpdate presenceUpdate = updates[k];
			BnetGameAccountId entityId = BnetGameAccountId.CreateFromEntityId(presenceUpdate.entityId);
			bool flag = presenceUpdate.fieldId == 1U && presenceUpdate.programId == BnetProgramId.BNET;
			bool flag2 = presenceUpdate.programId == BnetProgramId.HEARTHSTONE && presenceUpdate.fieldId == 17U;
			if (this.m_waitingForNextGameDialog != null && this.m_spectateeFriendlySide != null && (flag || flag2) && entityId == this.m_spectateeFriendlySide)
			{
				this.UpdateWaitingForNextGameDialog();
			}
			if (flag && presenceUpdate.boolVal)
			{
				Func<PartyInvite, bool> <>9__0;
				foreach (PartyId partyId in BnetParty.GetJoinedPartyIds())
				{
					if (BnetParty.IsLeader(partyId) && !BnetParty.IsMember(partyId, entityId))
					{
						BnetGameAccountId partyCreator = this.GetPartyCreator(partyId);
						if (partyCreator != null && partyCreator == entityId)
						{
							IEnumerable<PartyInvite> sentInvites = BnetParty.GetSentInvites(partyId);
							Func<PartyInvite, bool> predicate;
							if ((predicate = <>9__0) == null)
							{
								predicate = (<>9__0 = ((PartyInvite i) => i.InviteeId == entityId));
							}
							if (!sentInvites.Any(predicate))
							{
								BnetParty.SendInvite(partyId, entityId, false);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06008152 RID: 33106 RVA: 0x002A0784 File Offset: 0x0029E984
	private void BnetFriendMgr_OnFriendsChanged(BnetFriendChangelist changelist, object userData)
	{
		if (changelist == null)
		{
			return;
		}
		List<BnetPlayer> removedFriends = changelist.GetRemovedFriends();
		this.CheckSpectatorsOnChangedContext(removedFriends);
	}

	// Token: 0x06008153 RID: 33107 RVA: 0x002A07A3 File Offset: 0x0029E9A3
	private void FiresideGatheringManager_OnPatronListUpdated(List<BnetPlayer> addedList, List<BnetPlayer> removedList)
	{
		this.CheckSpectatorsOnChangedContext(removedList);
	}

	// Token: 0x06008154 RID: 33108 RVA: 0x002A07AC File Offset: 0x0029E9AC
	private void CheckSpectatorsOnChangedContext(List<BnetPlayer> players)
	{
		if (!this.IsBeingSpectated() || players == null)
		{
			return;
		}
		foreach (BnetPlayer bnetPlayer in players)
		{
			BnetGameAccountId hearthstoneGameAccountId = bnetPlayer.GetHearthstoneGameAccountId();
			if (this.IsSpectatingMe(hearthstoneGameAccountId) && !this.IsInSpectableContextWithPlayer(hearthstoneGameAccountId))
			{
				this.KickSpectator_Internal(bnetPlayer, true, false);
			}
		}
	}

	// Token: 0x06008155 RID: 33109 RVA: 0x002A0824 File Offset: 0x0029EA24
	private void EndGameScreen_OnTwoScoopsShown(bool shown, EndGameTwoScoop twoScoops)
	{
		if (!this.IsSpectatingOrWatching)
		{
			return;
		}
		if (shown)
		{
			Processor.ScheduleCallback(5f, false, new Processor.ScheduledCallback(this.EndGameScreen_OnTwoScoopsShown_AutoClose), null);
			return;
		}
		Processor.CancelScheduledCallback(new Processor.ScheduledCallback(this.EndGameScreen_OnTwoScoopsShown_AutoClose), null);
	}

	// Token: 0x06008156 RID: 33110 RVA: 0x002A0860 File Offset: 0x0029EA60
	private void EndGameScreen_OnTwoScoopsShown_AutoClose(object userData)
	{
		if (EndGameScreen.Get() == null)
		{
			return;
		}
		if (SpectatorManager.WAITING_FOR_NEXT_GAME_AUTO_LEAVE_SECONDS >= 0f)
		{
			int num = 0;
			while (EndGameScreen.Get().ContinueEvents())
			{
				num++;
				if (num > 100)
				{
					return;
				}
			}
			return;
		}
		EndGameScreen.Get().ContinueEvents();
	}

	// Token: 0x06008157 RID: 33111 RVA: 0x002A08B1 File Offset: 0x0029EAB1
	private void EndGameScreen_OnBackOutOfGameplay()
	{
		if (PartyManager.Get().IsInParty())
		{
			this.LeaveSpectatorMode();
		}
	}

	// Token: 0x06008158 RID: 33112 RVA: 0x002A08C8 File Offset: 0x0029EAC8
	private void BnetParty_OnError(PartyError error)
	{
		if (error.IsOperationCallback)
		{
			BnetFeatureEvent featureEvent = error.FeatureEvent;
			if (featureEvent != BnetFeatureEvent.Party_Create_Callback)
			{
				if (featureEvent - BnetFeatureEvent.Party_Leave_Callback <= 1)
				{
					if (this.m_leavePartyIdsRequested != null)
					{
						this.m_leavePartyIdsRequested.Remove(error.PartyId);
					}
					if (this.m_pendingSpectatePlayerAfterLeave != null && error.ErrorCode != BattleNetErrors.ERROR_OK)
					{
						string playerBestName = BnetUtils.GetPlayerBestName(this.m_pendingSpectatePlayerAfterLeave.SpectateeId);
						string header = GameStrings.Get("GLOBAL_ERROR_GENERIC_HEADER");
						string body = GameStrings.Format("GLOBAL_SPECTATOR_ERROR_LEAVE_FOR_SPECTATE_PLAYER_TEXT", new object[]
						{
							playerBestName
						});
						SpectatorManager.DisplayErrorDialog(header, body);
						this.m_pendingSpectatePlayerAfterLeave = null;
						return;
					}
				}
			}
			else if (error.ErrorCode != BattleNetErrors.ERROR_OK)
			{
				this.m_userInitiatedOutgoingInvites = null;
				string header2 = GameStrings.Get("GLOBAL_ERROR_GENERIC_HEADER");
				string body2 = GameStrings.Format("GLOBAL_SPECTATOR_ERROR_CREATE_PARTY_TEXT", Array.Empty<object>());
				SpectatorManager.DisplayErrorDialog(header2, body2);
			}
		}
	}

	// Token: 0x06008159 RID: 33113 RVA: 0x002A09A0 File Offset: 0x0029EBA0
	private static void DisplayErrorDialog(string header, string body)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = header;
		popupInfo.m_text = body;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x0600815A RID: 33114 RVA: 0x002A09D4 File Offset: 0x0029EBD4
	private void BnetParty_OnJoined(OnlineEventType evt, PartyInfo party, LeaveReason? reason)
	{
		if (!this.m_initialized)
		{
			return;
		}
		if (party.Type != PartyType.SPECTATOR_PARTY)
		{
			return;
		}
		if (evt == OnlineEventType.REMOVED)
		{
			bool flag = false;
			if (this.m_leavePartyIdsRequested != null)
			{
				flag = this.m_leavePartyIdsRequested.Remove(party.Id);
			}
			this.LogInfoParty("SpectatorParty_OnLeft: left party={0} current={1} reason={2} wasRequested={3}", new object[]
			{
				party,
				this.m_spectatorPartyIdMain,
				(reason != null) ? reason.Value.ToString() : "null",
				flag
			});
			bool flag2 = false;
			if (party.Id == this.m_spectatorPartyIdOpposingSide)
			{
				this.m_spectatorPartyIdOpposingSide = null;
				flag2 = true;
			}
			else if (this.m_spectateeFriendlySide != null)
			{
				if (party.Id == this.m_spectatorPartyIdMain)
				{
					this.m_spectatorPartyIdMain = null;
					flag2 = true;
				}
			}
			else if (this.m_spectateeFriendlySide == null && this.m_spectateeOpposingSide == null)
			{
				if (party.Id != this.m_spectatorPartyIdMain)
				{
					this.CreatePartyIfNecessary();
					return;
				}
				this.m_userInitiatedOutgoingInvites = null;
				this.m_spectatorPartyIdMain = null;
				this.UpdateSpectatorPresence();
				if (reason != null && reason.Value != LeaveReason.MEMBER_LEFT && reason.Value != LeaveReason.DISSOLVED_BY_MEMBER)
				{
					Processor.ScheduleCallback(1f, true, delegate(object userData)
					{
						this.CreatePartyIfNecessary();
					}, null);
				}
			}
			if (this.m_pendingSpectatePlayerAfterLeave != null && this.m_spectatorPartyIdMain == null && this.m_spectatorPartyIdOpposingSide == null)
			{
				this.SpectatePlayer_Internal(this.m_pendingSpectatePlayerAfterLeave.SpectateeId, this.m_pendingSpectatePlayerAfterLeave.JoinInfo);
			}
			else if (flag2 && this.m_spectatorPartyIdMain == null)
			{
				if (flag)
				{
					this.EndSpectatorMode(true);
				}
				else
				{
					bool flag3 = reason != null && reason.Value == LeaveReason.MEMBER_KICKED;
					bool flag4 = this.m_expectedDisconnectReason != null && this.m_expectedDisconnectReason.Value == 0;
					this.EndSpectatorMode(true);
					if (flag3 && !flag4)
					{
						if (flag3)
						{
							BnetGameAccountId bnetGameAccountId = this.GetPartyCreator(party.Id);
							if (bnetGameAccountId == null)
							{
								bgs.PartyMember leader = BnetParty.GetLeader(party.Id);
								bnetGameAccountId = ((leader == null) ? null : leader.GameAccountId);
							}
							if (bnetGameAccountId != null)
							{
								GameServerInfo lastGameServerJoined = Network.Get().GetLastGameServerJoined();
								if (lastGameServerJoined != null)
								{
									if (this.m_kickedFromSpectatingList == null)
									{
										this.m_kickedFromSpectatingList = new global::Map<BnetGameAccountId, uint>();
									}
									this.m_kickedFromSpectatingList[bnetGameAccountId] = lastGameServerJoined.GameHandle;
								}
							}
						}
						if (!this.m_isShowingRemovedAsSpectatorPopup)
						{
							bool flag5 = GameMgr.Get().IsTransitionPopupShown();
							AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
							popupInfo.m_headerText = GameStrings.Get("GLOBAL_SPECTATOR_REMOVED_PROMPT_HEADER");
							popupInfo.m_text = (BnetPresenceMgr.Get().GetMyPlayer().IsAppearingOffline() ? GameStrings.Get("GLOBAL_SPECTATOR_REMOVED_PROMPT_APPEAR_OFFLINE_TEXT") : GameStrings.Get("GLOBAL_SPECTATOR_REMOVED_PROMPT_TEXT"));
							popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
							if (flag5)
							{
								popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.Network_OnSpectatorNotifyEvent_Removed_GoToNextMode);
							}
							else
							{
								popupInfo.m_responseCallback = delegate(AlertPopup.Response r, object data)
								{
									SpectatorManager.Get().m_isShowingRemovedAsSpectatorPopup = false;
								};
							}
							this.m_isShowingRemovedAsSpectatorPopup = true;
							DialogManager.Get().ShowPopup(popupInfo);
						}
					}
				}
			}
			Processor.ScheduleCallback(0.5f, false, new Processor.ScheduledCallback(this.BnetParty_OnLostPartyReference_RemoveKnownCreator), party.Id);
		}
		if (evt == OnlineEventType.ADDED)
		{
			BnetGameAccountId partyCreator = this.GetPartyCreator(party.Id);
			if (partyCreator == null)
			{
				this.LogInfoParty("SpectatorParty_OnJoined: joined party={0} without creator.", new object[]
				{
					party.Id
				});
				this.LeaveParty(party.Id, BnetParty.IsLeader(party.Id));
				return;
			}
			if (this.m_requestedInvite != null && this.m_requestedInvite.PartyId == party.Id)
			{
				this.m_requestedInvite = null;
				Processor.CancelScheduledCallback(new Processor.ScheduledCallback(this.SpectatePlayer_RequestInvite_FriendlySide_Timeout), null);
				Processor.CancelScheduledCallback(new Processor.ScheduledCallback(this.SpectatePlayer_RequestInvite_OpposingSide_Timeout), null);
			}
			bool flag6 = this.ShouldBePartyLeader(party.Id);
			bool flag7 = this.m_spectatorPartyIdMain == null;
			bool flag8 = flag7;
			if (this.m_spectatorPartyIdMain != null && this.m_spectatorPartyIdMain != party.Id && (flag6 || partyCreator != this.m_spectateeOpposingSide))
			{
				flag8 = true;
				string format = "SpectatorParty_OnJoined: joined party={0} when different current={1} (will be clobbered) joinedParties={2}";
				object[] array = new object[3];
				array[0] = party.Id;
				array[1] = this.m_spectatorPartyIdMain;
				array[2] = string.Join(", ", (from i in BnetParty.GetJoinedParties()
				select i.ToString()).ToArray<string>());
				this.LogInfoParty(format, array);
			}
			if (flag6)
			{
				this.m_spectatorPartyIdMain = party.Id;
				if (flag8)
				{
					this.UpdateSpectatorPresence();
				}
				this.UpdateSpectatorPartyServerInfo();
				this.ReinviteKnownSpectatorsNotInParty();
				if (this.m_userInitiatedOutgoingInvites != null)
				{
					foreach (BnetGameAccountId recipientId in this.m_userInitiatedOutgoingInvites)
					{
						BnetParty.SendInvite(this.m_spectatorPartyIdMain, recipientId, false);
					}
				}
				if (flag7 && this.OnSpectatorToMyGame != null)
				{
					foreach (bgs.PartyMember partyMember in BnetParty.GetMembers(this.m_spectatorPartyIdMain))
					{
						if (!(partyMember.GameAccountId == BnetPresenceMgr.Get().GetMyGameAccountId()))
						{
							Processor.RunCoroutine(this.WaitForPresenceThenToast(partyMember.GameAccountId, SocialToastMgr.TOAST_TYPE.SPECTATOR_ADDED), null);
							BnetPlayer player = BnetUtils.GetPlayer(partyMember.GameAccountId);
							this.OnSpectatorToMyGame(OnlineEventType.ADDED, player);
						}
					}
					return;
				}
			}
			else
			{
				bool flag9 = true;
				if (this.m_spectateeFriendlySide == null)
				{
					this.m_spectateeFriendlySide = partyCreator;
					this.m_spectatorPartyIdMain = party.Id;
					flag9 = false;
				}
				else if (partyCreator == this.m_spectateeFriendlySide)
				{
					this.m_spectatorPartyIdMain = party.Id;
				}
				else if (partyCreator == this.m_spectateeOpposingSide)
				{
					this.m_spectatorPartyIdOpposingSide = party.Id;
				}
				if (BnetParty.GetPartyAttributeBlob(party.Id, "WTCG.Party.ServerInfo") != null)
				{
					this.LogInfoParty("SpectatorParty_OnJoined: joined party={0} as spectator, begin spectating game.", new object[]
					{
						party.Id
					});
					if (!flag9)
					{
						if (partyCreator == this.m_spectateeOpposingSide)
						{
							this.LogInfoPower("================== Begin Spectating 2nd player ==================", Array.Empty<object>());
						}
						else
						{
							this.LogInfoPower("================== Begin Spectating 1st player ==================", Array.Empty<object>());
						}
					}
					this.JoinPartyGame(party.Id);
					return;
				}
				if (PartyManager.Get().IsInParty())
				{
					string header = GameStrings.Get("GLOBAL_SPECTATOR_SERVER_REJECTED_HEADER");
					string body = GameStrings.Get("GLOBAL_SPECTATOR_SERVER_REJECTED_TEXT");
					SpectatorManager.DisplayErrorDialog(header, body);
					this.EndSpectatorMode(true);
					if (this.OnSpectateRejected != null)
					{
						this.OnSpectateRejected();
					}
				}
				else if (!SceneMgr.Get().IsInGame())
				{
					this.ShowWaitingForNextGameDialog();
				}
				BnetPlayer player2 = BnetUtils.GetPlayer(partyCreator);
				this.FireSpectatorModeChanged(OnlineEventType.ADDED, player2);
			}
		}
	}

	// Token: 0x0600815B RID: 33115 RVA: 0x002A10D0 File Offset: 0x0029F2D0
	private void BnetParty_OnLostPartyReference_RemoveKnownCreator(object userData)
	{
		PartyId partyId = userData as PartyId;
		if (partyId != null && !BnetParty.IsInParty(partyId) && !BnetParty.GetReceivedInvites().Any((PartyInvite i) => i.PartyId == partyId))
		{
			SpectatorManager.Get().m_knownPartyCreatorIds.Remove(partyId);
		}
	}

	// Token: 0x0600815C RID: 33116 RVA: 0x002A1138 File Offset: 0x0029F338
	private void BnetParty_OnReceivedInvite(OnlineEventType evt, PartyInfo party, ulong inviteId, BnetGameAccountId inviterId, BnetGameAccountId inviteeId, InviteRemoveReason? reason)
	{
		if (!this.m_initialized)
		{
			return;
		}
		if (party.Type != PartyType.SPECTATOR_PARTY)
		{
			return;
		}
		PartyInvite receivedInvite = BnetParty.GetReceivedInvite(inviteId);
		bool flag = receivedInvite != null && (receivedInvite.IsRejoin || (receivedInvite.InviterId == receivedInvite.InviteeId && receivedInvite.InviteeId == BnetPresenceMgr.Get().GetMyGameAccountId()));
		BnetGameAccountId bnetGameAccountId = (receivedInvite == null) ? null : this.GetPartyCreator(receivedInvite.PartyId);
		BnetPlayer inviter = (receivedInvite == null) ? null : BnetUtils.GetPlayer(receivedInvite.InviterId);
		bool flag2 = false;
		bool flag3 = false;
		string text = string.Empty;
		if (evt == OnlineEventType.ADDED)
		{
			if (receivedInvite == null)
			{
				return;
			}
			if (flag || this.ShouldBePartyLeader(receivedInvite.PartyId))
			{
				if (this.ShouldBePartyLeader(receivedInvite.PartyId))
				{
					flag2 = true;
					text = "should_be_leader";
				}
				else if (this.m_spectatorPartyIdMain != null)
				{
					if (this.m_spectatorPartyIdMain == receivedInvite.PartyId)
					{
						flag2 = true;
						text = "spectating_this_party";
					}
					else
					{
						flag3 = true;
						text = "spectating_other_party";
					}
				}
				else
				{
					flag3 = true;
					text = "not_spectating";
					if (bnetGameAccountId != null && this.m_spectateeFriendlySide == null)
					{
						this.m_spectateeFriendlySide = bnetGameAccountId;
						flag2 = true;
						flag3 = false;
						text = "rejoin_spectating";
					}
				}
			}
			else if (receivedInvite.InviterId == this.m_spectateeFriendlySide || receivedInvite.InviterId == this.m_spectateeOpposingSide || (this.m_requestedInvite != null && this.m_requestedInvite.PartyId == receivedInvite.PartyId))
			{
				flag2 = true;
				text = "spectating_this_player";
				if (this.m_requestedInvite != null)
				{
					this.m_requestedInvite = null;
					Processor.CancelScheduledCallback(new Processor.ScheduledCallback(this.SpectatePlayer_RequestInvite_FriendlySide_Timeout), null);
					Processor.CancelScheduledCallback(new Processor.ScheduledCallback(this.SpectatePlayer_RequestInvite_OpposingSide_Timeout), null);
				}
			}
			else if (!UserAttentionManager.CanShowAttentionGrabber("SpectatorManager.BnetParty_OnReceivedInvite:" + evt))
			{
				flag3 = true;
				text = "user_attention_blocked";
			}
			else
			{
				if (this.m_kickedFromSpectatingList != null)
				{
					this.m_kickedFromSpectatingList.Remove(receivedInvite.InviterId);
				}
				if (SocialToastMgr.Get() != null)
				{
					string inviterBestName = BnetUtils.GetInviterBestName(receivedInvite);
					SocialToastMgr.Get().AddToast(UserAttentionBlocker.NONE, inviterBestName, SocialToastMgr.TOAST_TYPE.SPECTATOR_INVITE_RECEIVED);
				}
			}
		}
		else if (evt == OnlineEventType.REMOVED && (reason == null || reason.Value == InviteRemoveReason.ACCEPTED))
		{
			Processor.ScheduleCallback(0.5f, false, new Processor.ScheduledCallback(this.BnetParty_OnLostPartyReference_RemoveKnownCreator), party.Id);
		}
		this.LogInfoParty("Spectator_OnReceivedInvite {0} rejoin={1} partyId={2} creatorId={3} accept={4} decline={5} acceptDeclineReason={6} removeReason={7}", new object[]
		{
			evt,
			flag,
			party.Id,
			bnetGameAccountId,
			flag2,
			flag3,
			text,
			reason
		});
		if (flag2)
		{
			BnetParty.AcceptReceivedInvite(inviteId);
			return;
		}
		if (flag3)
		{
			BnetParty.DeclineReceivedInvite(inviteId);
			return;
		}
		if (this.OnInviteReceived != null)
		{
			this.OnInviteReceived(evt, inviter);
		}
	}

	// Token: 0x0600815D RID: 33117 RVA: 0x002A142C File Offset: 0x0029F62C
	private void BnetParty_OnSentInvite(OnlineEventType evt, PartyInfo party, ulong inviteId, BnetGameAccountId inviterId, BnetGameAccountId inviteeId, bool senderIsMyself, InviteRemoveReason? reason)
	{
		if (party.Type != PartyType.SPECTATOR_PARTY)
		{
			return;
		}
		if (!senderIsMyself)
		{
			return;
		}
		PartyInvite sentInvite = BnetParty.GetSentInvite(party.Id, inviteId);
		BnetPlayer invitee = (sentInvite == null) ? null : BnetUtils.GetPlayer(sentInvite.InviteeId);
		if (evt == OnlineEventType.ADDED)
		{
			bool flag = false;
			if (this.m_userInitiatedOutgoingInvites != null && sentInvite != null)
			{
				flag = this.m_userInitiatedOutgoingInvites.Remove(sentInvite.InviteeId);
			}
			if (flag && sentInvite != null && this.ShouldBePartyLeader(party.Id) && !this.m_gameServerKnownSpectators.Contains(sentInvite.InviteeId) && SocialToastMgr.Get() != null)
			{
				string playerBestName = BnetUtils.GetPlayerBestName(sentInvite.InviteeId);
				SocialToastMgr.Get().AddToast(UserAttentionBlocker.NONE, playerBestName, SocialToastMgr.TOAST_TYPE.SPECTATOR_INVITE_SENT);
			}
		}
		if (sentInvite != null && !this.m_gameServerKnownSpectators.Contains(sentInvite.InviteeId) && this.OnInviteSent != null)
		{
			this.OnInviteSent(evt, invitee);
		}
	}

	// Token: 0x0600815E RID: 33118 RVA: 0x002A1504 File Offset: 0x0029F704
	private void BnetParty_OnReceivedInviteRequest(OnlineEventType evt, PartyInfo party, InviteRequest request, InviteRequestRemovedReason? reason)
	{
		if (party.Type != PartyType.SPECTATOR_PARTY)
		{
			return;
		}
		if (evt == OnlineEventType.ADDED)
		{
			bool flag = false;
			if (party.Id != this.m_spectatorPartyIdMain)
			{
				flag = true;
			}
			if (request.RequesterId != null && request.RequesterId == request.TargetId && !Options.Get().GetBool(global::Option.SPECTATOR_OPEN_JOIN))
			{
				flag = true;
			}
			if (!this.IsInSpectableContextWithPlayer(request.RequesterId))
			{
				flag = true;
			}
			if (!this.IsInSpectableContextWithPlayer(request.TargetId))
			{
				flag = true;
			}
			if (this.m_kickedPlayers != null && (this.m_kickedPlayers.Contains(request.RequesterId) || this.m_kickedPlayers.Contains(request.TargetId)))
			{
				flag = true;
			}
			if (flag)
			{
				BnetParty.IgnoreInviteRequest(party.Id, request.TargetId);
				return;
			}
			BnetParty.AcceptInviteRequest(party.Id, request.TargetId, false);
		}
	}

	// Token: 0x0600815F RID: 33119 RVA: 0x002A15E4 File Offset: 0x0029F7E4
	private void BnetParty_OnMemberEvent(OnlineEventType evt, PartyInfo party, BnetGameAccountId memberId, bool isRolesUpdate, LeaveReason? reason)
	{
		if (party.Id == null)
		{
			return;
		}
		if (party.Id != this.m_spectatorPartyIdMain && party.Id != this.m_spectatorPartyIdOpposingSide)
		{
			return;
		}
		if (evt == OnlineEventType.ADDED && BnetParty.IsLeader(party.Id))
		{
			BnetGameAccountId partyCreator = this.GetPartyCreator(party.Id);
			if (partyCreator != null && partyCreator == memberId)
			{
				BnetParty.SetLeader(party.Id, memberId);
			}
		}
		if (this.m_initialized && evt != OnlineEventType.UPDATED && memberId != BnetPresenceMgr.Get().GetMyGameAccountId() && this.ShouldBePartyLeader(party.Id) && (!SceneMgr.Get().IsInGame() || !Network.Get().IsConnectedToGameServer() || !this.m_gameServerKnownSpectators.Contains(memberId)))
		{
			SocialToastMgr.TOAST_TYPE toastType = (evt == OnlineEventType.ADDED) ? SocialToastMgr.TOAST_TYPE.SPECTATOR_ADDED : SocialToastMgr.TOAST_TYPE.SPECTATOR_REMOVED;
			Processor.RunCoroutine(this.WaitForPresenceThenToast(memberId, toastType), null);
			if (this.OnSpectatorToMyGame != null)
			{
				BnetPlayer player = BnetUtils.GetPlayer(memberId);
				this.OnSpectatorToMyGame(evt, player);
			}
		}
	}

	// Token: 0x06008160 RID: 33120 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void BnetParty_OnChatMessage(PartyInfo party, BnetGameAccountId speakerId, string chatMessage)
	{
	}

	// Token: 0x06008161 RID: 33121 RVA: 0x002A16F8 File Offset: 0x0029F8F8
	private void BnetParty_OnPartyAttributeChanged_ServerInfo(PartyInfo party, string attributeKey, Variant value)
	{
		if (party.Type != PartyType.SPECTATOR_PARTY)
		{
			return;
		}
		if (value == null)
		{
			return;
		}
		byte[] array = value.HasBlobValue ? value.BlobValue : null;
		if (array == null)
		{
			return;
		}
		PartyServerInfo partyServerInfo = ProtobufUtil.ParseFrom<PartyServerInfo>(array, 0, -1);
		if (partyServerInfo == null)
		{
			return;
		}
		if (!partyServerInfo.HasSecretKey || string.IsNullOrEmpty(partyServerInfo.SecretKey))
		{
			this.LogInfoParty("BnetParty_OnPartyAttributeChanged_ServerInfo: no secret key in serverInfo.", Array.Empty<object>());
			return;
		}
		GameServerInfo lastGameServerJoined = Network.Get().GetLastGameServerJoined();
		bool flag = Network.Get().IsConnectedToGameServer() && SpectatorManager.IsSameGameAndServer(partyServerInfo, lastGameServerJoined);
		if (!flag && SceneMgr.Get().IsInGame())
		{
			this.LogInfoParty("BnetParty_OnPartyAttributeChanged_ServerInfo: cannot join game while in gameplay new={0} curr={1}.", new object[]
			{
				partyServerInfo.GameHandle,
				lastGameServerJoined.GameHandle
			});
			return;
		}
		JoinInfo joinInfo = SpectatorManager.CreateJoinInfo(partyServerInfo);
		if (party.Id == this.m_spectatorPartyIdOpposingSide)
		{
			if (GameMgr.Get().GetTransitionPopup() == null && GameMgr.Get().IsSpectator())
			{
				this.SpectateSecondPlayer_Network(joinInfo);
				return;
			}
		}
		else if (!flag && party.Id == this.m_spectatorPartyIdMain)
		{
			this.LogInfoPower("================== Start Spectator Game ==================", Array.Empty<object>());
			this.m_isExpectingArriveInGameplayAsSpectator = true;
			GameMgr.Get().SpectateGame(joinInfo);
			this.CloseWaitingForNextGameDialog();
		}
	}

	// Token: 0x1700075B RID: 1883
	// (get) Token: 0x06008162 RID: 33122 RVA: 0x002A183C File Offset: 0x0029FA3C
	private static bool IsGameOver
	{
		get
		{
			return GameState.Get() != null && GameState.Get().IsGameOverNowOrPending();
		}
	}

	// Token: 0x06008163 RID: 33123 RVA: 0x002A1856 File Offset: 0x0029FA56
	private void LogInfoParty(string format, params object[] args)
	{
		global::Log.Party.Print(format, args);
	}

	// Token: 0x06008164 RID: 33124 RVA: 0x002A1864 File Offset: 0x0029FA64
	private void LogInfoPower(string format, params object[] args)
	{
		global::Log.Party.Print(format, args);
		global::Log.Power.Print(format, args);
	}

	// Token: 0x06008165 RID: 33125 RVA: 0x002A1880 File Offset: 0x0029FA80
	private bool IsPlayerInGame(BnetGameAccountId gameAccountId)
	{
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return false;
		}
		foreach (KeyValuePair<int, global::Player> keyValuePair in gameState.GetPlayerMap())
		{
			BnetPlayer bnetPlayer = keyValuePair.Value.GetBnetPlayer();
			if (bnetPlayer != null && bnetPlayer.GetHearthstoneGameAccountId() == gameAccountId)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06008166 RID: 33126 RVA: 0x002A1900 File Offset: 0x0029FB00
	private bool IsStillInParty(PartyId partyId)
	{
		return BnetParty.IsInParty(partyId) && (this.m_leavePartyIdsRequested == null || !this.m_leavePartyIdsRequested.Contains(partyId));
	}

	// Token: 0x06008167 RID: 33127 RVA: 0x002A1928 File Offset: 0x0029FB28
	private void BnetPresenceMgr_OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
		BnetPlayerChange myOwnChange = changelist.FindChange(myGameAccountId);
		if (myOwnChange != null)
		{
			bool flag = myOwnChange.GetNewPlayer().IsAppearingOffline();
			bool flag2 = myOwnChange.GetOldPlayer().IsAppearingOffline();
			if (flag && !flag2 && this.MyGameHasSpectators())
			{
				foreach (BnetGameAccountId id in this.GetSpectatorPartyMembers(true, false).ToArray<BnetGameAccountId>())
				{
					this.KickSpectator_Internal(BnetPresenceMgr.Get().GetPlayer(id), true, false);
				}
			}
			else if (flag2 && !flag)
			{
				this.UpdateMySpectatorInfo();
			}
		}
		if (this.IsBeingSpectated())
		{
			foreach (BnetPlayerChange bnetPlayerChange in from c in changelist.GetChanges()
			where c != myOwnChange && c.GetOldPlayer() != null && c.GetOldPlayer().IsOnline() && !c.GetNewPlayer().IsOnline()
			select c)
			{
				this.KickSpectator_Internal(BnetPresenceMgr.Get().GetPlayer(bnetPlayerChange.GetPlayer().GetAccountId()), true, false);
			}
		}
	}

	// Token: 0x06008168 RID: 33128 RVA: 0x002A1A4C File Offset: 0x0029FC4C
	private void PruneOldInvites()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		List<BnetGameAccountId> list = new List<BnetGameAccountId>();
		foreach (KeyValuePair<BnetGameAccountId, SpectatorManager.ReceivedInvite> keyValuePair in this.m_receivedSpectateMeInvites)
		{
			float timestamp = keyValuePair.Value.m_timestamp;
			if (realtimeSinceStartup - timestamp > 300f)
			{
				list.Add(keyValuePair.Key);
			}
		}
		foreach (BnetGameAccountId inviterId in list)
		{
			this.RemoveReceivedInvitation(inviterId);
		}
		list.Clear();
		foreach (KeyValuePair<BnetGameAccountId, float> keyValuePair2 in this.m_sentSpectateMeInvites)
		{
			float value = keyValuePair2.Value;
			if (realtimeSinceStartup - value > 30f)
			{
				list.Add(keyValuePair2.Key);
			}
		}
		foreach (BnetGameAccountId inviteeId in list)
		{
			this.RemoveSentInvitation(inviteeId);
		}
	}

	// Token: 0x06008169 RID: 33129 RVA: 0x002A1BB0 File Offset: 0x0029FDB0
	private void AddReceivedInvitation(BnetGameAccountId inviterId, JoinInfo joinInfo)
	{
		bool flag = !this.m_receivedSpectateMeInvites.ContainsKey(inviterId);
		SpectatorManager.ReceivedInvite value = new SpectatorManager.ReceivedInvite(joinInfo);
		this.m_receivedSpectateMeInvites[inviterId] = value;
		if (flag)
		{
			BnetPlayer player = BnetUtils.GetPlayer(inviterId);
			if (SocialToastMgr.Get() != null)
			{
				SocialToastMgr.Get().AddToast(UserAttentionBlocker.NONE, BnetUtils.GetPlayerBestName(inviterId), SocialToastMgr.TOAST_TYPE.SPECTATOR_INVITE_RECEIVED);
			}
			if (this.OnInviteReceived != null)
			{
				this.OnInviteReceived(OnlineEventType.ADDED, player);
			}
		}
		float num = this.m_receivedSpectateMeInvites.Min((KeyValuePair<BnetGameAccountId, SpectatorManager.ReceivedInvite> kv) => kv.Value.m_timestamp);
		float secondsToWait = Mathf.Max(0f, num + 300f - Time.realtimeSinceStartup);
		Processor.CancelScheduledCallback(new Processor.ScheduledCallback(this.ReceivedInvitation_ExpireTimeout), null);
		Processor.ScheduleCallback(secondsToWait, true, new Processor.ScheduledCallback(this.ReceivedInvitation_ExpireTimeout), null);
	}

	// Token: 0x0600816A RID: 33130 RVA: 0x002A1C88 File Offset: 0x0029FE88
	private void RemoveReceivedInvitation(BnetGameAccountId inviterId)
	{
		if (inviterId == null)
		{
			return;
		}
		if (this.m_receivedSpectateMeInvites.Remove(inviterId))
		{
			BnetPlayer player = BnetUtils.GetPlayer(inviterId);
			if (this.OnInviteReceived != null)
			{
				this.OnInviteReceived(OnlineEventType.REMOVED, player);
			}
		}
	}

	// Token: 0x0600816B RID: 33131 RVA: 0x002A1CCC File Offset: 0x0029FECC
	private void ClearAllReceivedInvitations()
	{
		BnetGameAccountId[] array = this.m_receivedSpectateMeInvites.Keys.ToArray<BnetGameAccountId>();
		this.m_receivedSpectateMeInvites.Clear();
		if (this.OnInviteReceived != null)
		{
			BnetGameAccountId[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				BnetPlayer player = BnetUtils.GetPlayer(array2[i]);
				this.OnInviteReceived(OnlineEventType.REMOVED, player);
			}
		}
	}

	// Token: 0x0600816C RID: 33132 RVA: 0x002A1D24 File Offset: 0x0029FF24
	private void AddSentInvitation(BnetGameAccountId inviteeId)
	{
		if (inviteeId == null)
		{
			return;
		}
		bool flag = !this.m_sentSpectateMeInvites.ContainsKey(inviteeId);
		this.m_sentSpectateMeInvites[inviteeId] = Time.realtimeSinceStartup;
		if (flag)
		{
			BnetPlayer player = BnetUtils.GetPlayer(inviteeId);
			if (this.OnInviteSent != null)
			{
				this.OnInviteSent(OnlineEventType.ADDED, player);
			}
		}
	}

	// Token: 0x0600816D RID: 33133 RVA: 0x002A1D7C File Offset: 0x0029FF7C
	private void RemoveSentInvitation(BnetGameAccountId inviteeId)
	{
		if (inviteeId == null)
		{
			return;
		}
		if (this.m_sentSpectateMeInvites.Remove(inviteeId))
		{
			BnetPlayer player = BnetUtils.GetPlayer(inviteeId);
			if (this.OnInviteSent != null)
			{
				this.OnInviteSent(OnlineEventType.REMOVED, player);
			}
		}
	}

	// Token: 0x0600816E RID: 33134 RVA: 0x002A1DC0 File Offset: 0x0029FFC0
	private void DeclineAllReceivedInvitations()
	{
		foreach (PartyInvite partyInvite in BnetParty.GetReceivedInvites())
		{
			if (partyInvite.PartyType == PartyType.SPECTATOR_PARTY)
			{
				BnetParty.DeclineReceivedInvite(partyInvite.InviteId);
			}
		}
	}

	// Token: 0x0600816F RID: 33135 RVA: 0x002A1DFC File Offset: 0x0029FFFC
	private void RevokeAllSentInvitations()
	{
		this.ClearAllSentInvitations();
		BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
		foreach (PartyId partyId in new PartyId[]
		{
			this.m_spectatorPartyIdMain,
			this.m_spectatorPartyIdOpposingSide
		})
		{
			if (!(partyId == null))
			{
				foreach (PartyInvite partyInvite in BnetParty.GetSentInvites(partyId))
				{
					if (!(partyInvite.InviterId != myGameAccountId))
					{
						BnetParty.RevokeSentInvite(partyId, partyInvite.InviteId);
					}
				}
			}
		}
	}

	// Token: 0x06008170 RID: 33136 RVA: 0x002A1E8C File Offset: 0x002A008C
	private void ClearAllSentInvitations()
	{
		BnetGameAccountId[] array = this.m_sentSpectateMeInvites.Keys.ToArray<BnetGameAccountId>();
		this.m_sentSpectateMeInvites.Clear();
		if (this.OnInviteSent != null)
		{
			BnetGameAccountId[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				BnetPlayer player = BnetUtils.GetPlayer(array2[i]);
				this.OnInviteSent(OnlineEventType.REMOVED, player);
			}
		}
	}

	// Token: 0x06008171 RID: 33137 RVA: 0x002A1EE4 File Offset: 0x002A00E4
	private void AddKnownSpectator(BnetGameAccountId gameAccountId)
	{
		if (gameAccountId == null)
		{
			return;
		}
		bool flag = this.m_gameServerKnownSpectators.Add(gameAccountId);
		this.CreatePartyIfNecessary();
		this.RemoveSentInvitation(gameAccountId);
		this.RemoveReceivedInvitation(gameAccountId);
		if (flag)
		{
			if (SceneMgr.Get().IsInGame() && Network.Get().IsConnectedToGameServer())
			{
				bool flag2 = BnetParty.IsMember(this.m_spectatorPartyIdMain, gameAccountId);
				BnetPlayer player = BnetUtils.GetPlayer(gameAccountId);
				if (!flag2)
				{
					Processor.RunCoroutine(this.WaitForPresenceThenToast(gameAccountId, SocialToastMgr.TOAST_TYPE.SPECTATOR_ADDED), null);
				}
				if (this.OnSpectatorToMyGame != null)
				{
					this.OnSpectatorToMyGame(OnlineEventType.ADDED, player);
				}
			}
			this.UpdateSpectatorPresence();
		}
	}

	// Token: 0x06008172 RID: 33138 RVA: 0x002A1F78 File Offset: 0x002A0178
	private void RemoveKnownSpectator(BnetGameAccountId gameAccountId)
	{
		if (gameAccountId == null)
		{
			return;
		}
		if (this.m_gameServerKnownSpectators.Remove(gameAccountId))
		{
			if (SceneMgr.Get().IsInGame() && Network.Get().IsConnectedToGameServer())
			{
				bool flag = BnetParty.IsMember(this.m_spectatorPartyIdMain, gameAccountId);
				BnetPlayer player = BnetUtils.GetPlayer(gameAccountId);
				if (!flag)
				{
					Processor.RunCoroutine(this.WaitForPresenceThenToast(gameAccountId, SocialToastMgr.TOAST_TYPE.SPECTATOR_REMOVED), null);
				}
				if (this.OnSpectatorToMyGame != null)
				{
					this.OnSpectatorToMyGame(OnlineEventType.REMOVED, player);
				}
			}
			this.UpdateSpectatorPresence();
		}
	}

	// Token: 0x06008173 RID: 33139 RVA: 0x002A1FF8 File Offset: 0x002A01F8
	private void ClearAllGameServerKnownSpectators()
	{
		BnetGameAccountId[] array = this.m_gameServerKnownSpectators.ToArray<BnetGameAccountId>();
		this.m_gameServerKnownSpectators.Clear();
		if (this.OnSpectatorToMyGame != null && SceneMgr.Get().IsInGame() && Network.Get().IsConnectedToGameServer())
		{
			BnetGameAccountId[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				BnetPlayer player = BnetUtils.GetPlayer(array2[i]);
				this.OnSpectatorToMyGame(OnlineEventType.REMOVED, player);
			}
		}
		if (array.Length != 0)
		{
			this.UpdateSpectatorPresence();
		}
	}

	// Token: 0x06008174 RID: 33140 RVA: 0x002A206C File Offset: 0x002A026C
	private void UpdateSpectatorPresence()
	{
		if (HearthstoneApplication.Get() != null)
		{
			Processor.CancelScheduledCallback(new Processor.ScheduledCallback(this.SpectatorManager_UpdatePresenceNextFrame), null);
			Processor.ScheduleCallback(0f, true, new Processor.ScheduledCallback(this.SpectatorManager_UpdatePresenceNextFrame), null);
			return;
		}
		this.SpectatorManager_UpdatePresenceNextFrame(null);
	}

	// Token: 0x06008175 RID: 33141 RVA: 0x002A20BC File Offset: 0x002A02BC
	private void SpectatorManager_UpdatePresenceNextFrame(object userData)
	{
		bool flag = Options.Get().GetBool(global::Option.SPECTATOR_OPEN_JOIN) || this.IsInSpectatorMode();
		JoinInfo myGameJoinInfo = this.GetMyGameJoinInfo();
		if (Network.ShouldBeConnectedToAurora() && Network.IsLoggedIn())
		{
			BnetPresenceMgr.Get().SetPresenceSpectatorJoinInfo(flag ? myGameJoinInfo : null);
		}
		PartyManager.Get().UpdateSpectatorJoinInfo(myGameJoinInfo);
	}

	// Token: 0x06008176 RID: 33142 RVA: 0x002A2118 File Offset: 0x002A0318
	private void UpdateSpectatorPartyServerInfo()
	{
		if (this.m_spectatorPartyIdMain == null)
		{
			return;
		}
		if (!this.ShouldBePartyLeader(this.m_spectatorPartyIdMain))
		{
			if (BnetParty.IsLeader(this.m_spectatorPartyIdMain))
			{
				BnetParty.ClearPartyAttribute(this.m_spectatorPartyIdMain, "WTCG.Party.ServerInfo");
			}
			return;
		}
		byte[] partyAttributeBlob = BnetParty.GetPartyAttributeBlob(this.m_spectatorPartyIdMain, "WTCG.Party.ServerInfo");
		GameServerInfo lastGameServerJoined = Network.Get().GetLastGameServerJoined();
		if (SpectatorManager.IsGameOver || !SceneMgr.Get().IsInGame() || !Network.Get().IsConnectedToGameServer() || lastGameServerJoined == null || string.IsNullOrEmpty(lastGameServerJoined.Address))
		{
			if (partyAttributeBlob != null)
			{
				BnetParty.ClearPartyAttribute(this.m_spectatorPartyIdMain, "WTCG.Party.ServerInfo");
				return;
			}
		}
		else
		{
			byte[] array = ProtobufUtil.ToByteArray(new PartyServerInfo
			{
				ServerIpAddress = lastGameServerJoined.Address,
				ServerPort = lastGameServerJoined.Port,
				GameHandle = (int)lastGameServerJoined.GameHandle,
				SecretKey = (lastGameServerJoined.SpectatorPassword ?? ""),
				GameType = GameMgr.Get().GetGameType(),
				FormatType = GameMgr.Get().GetFormatType(),
				MissionId = GameMgr.Get().GetMissionId()
			});
			if (!GeneralUtils.AreArraysEqual<byte>(array, partyAttributeBlob))
			{
				BnetParty.SetPartyAttributeBlob(this.m_spectatorPartyIdMain, "WTCG.Party.ServerInfo", array);
			}
		}
	}

	// Token: 0x06008177 RID: 33143 RVA: 0x002A2250 File Offset: 0x002A0450
	private bool ShouldBePartyLeader(PartyId partyId)
	{
		if (GameMgr.Get().IsSpectator())
		{
			return false;
		}
		if (this.m_spectateeFriendlySide != null || this.m_spectateeOpposingSide != null)
		{
			return false;
		}
		BnetGameAccountId partyCreator = this.GetPartyCreator(partyId);
		return !(partyCreator == null) && !(partyCreator != BnetPresenceMgr.Get().GetMyGameAccountId());
	}

	// Token: 0x06008178 RID: 33144 RVA: 0x002A22B4 File Offset: 0x002A04B4
	private BnetGameAccountId GetPartyCreator(PartyId partyId)
	{
		if (partyId == null)
		{
			return null;
		}
		BnetGameAccountId bnetGameAccountId = null;
		if (this.m_knownPartyCreatorIds.TryGetValue(partyId, out bnetGameAccountId) && bnetGameAccountId != null)
		{
			return bnetGameAccountId;
		}
		byte[] partyAttributeBlob = BnetParty.GetPartyAttributeBlob(partyId, "WTCG.Party.Creator");
		if (partyAttributeBlob == null)
		{
			return null;
		}
		bnetGameAccountId = BnetGameAccountId.CreateFromNet(ProtobufUtil.ParseFrom<BnetId>(partyAttributeBlob, 0, -1));
		if (bnetGameAccountId.IsValid())
		{
			this.m_knownPartyCreatorIds[partyId] = bnetGameAccountId;
		}
		return bnetGameAccountId;
	}

	// Token: 0x06008179 RID: 33145 RVA: 0x002A2320 File Offset: 0x002A0520
	private bool CreatePartyIfNecessary()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return false;
		}
		if (this.m_spectatorPartyIdMain != null)
		{
			if (this.GetPartyCreator(this.m_spectatorPartyIdMain) != null && !this.ShouldBePartyLeader(this.m_spectatorPartyIdMain))
			{
				return false;
			}
			PartyInfo[] joinedParties = BnetParty.GetJoinedParties();
			if (joinedParties.FirstOrDefault((PartyInfo i) => i.Id == this.m_spectatorPartyIdMain && i.Type == PartyType.SPECTATOR_PARTY) == null)
			{
				string format = "CreatePartyIfNecessary stored PartyId={0} is not in joined party list: {1}";
				object[] array = new object[2];
				array[0] = this.m_spectatorPartyIdMain;
				array[1] = string.Join(", ", (from i in joinedParties
				select i.ToString()).ToArray<string>());
				this.LogInfoParty(format, array);
				this.m_spectatorPartyIdMain = null;
				this.UpdateSpectatorPresence();
			}
			PartyInfo partyInfo = joinedParties.FirstOrDefault((PartyInfo i) => i.Type == PartyType.SPECTATOR_PARTY);
			if (partyInfo != null && this.m_spectatorPartyIdMain != partyInfo.Id)
			{
				this.LogInfoParty("CreatePartyIfNecessary repairing mismatching PartyIds current={0} new={1}", new object[]
				{
					this.m_spectatorPartyIdMain,
					partyInfo.Id
				});
				this.m_spectatorPartyIdMain = partyInfo.Id;
				this.UpdateSpectatorPresence();
			}
			if (this.m_spectatorPartyIdMain != null)
			{
				return false;
			}
		}
		if (this.GetCountSpectatingMe() <= 0)
		{
			return false;
		}
		byte[] creatorBlob = ProtobufUtil.ToByteArray(BnetUtils.CreatePegasusBnetId(BnetPresenceMgr.Get().GetMyGameAccountId()));
		BnetParty.CreateParty(PartyType.SPECTATOR_PARTY, PrivacyLevel.OPEN_INVITATION, creatorBlob, null);
		return true;
	}

	// Token: 0x0600817A RID: 33146 RVA: 0x002A2490 File Offset: 0x002A0690
	private void ReinviteKnownSpectatorsNotInParty()
	{
		if (this.m_spectatorPartyIdMain == null || !this.ShouldBePartyLeader(this.m_spectatorPartyIdMain))
		{
			return;
		}
		bgs.PartyMember[] members = BnetParty.GetMembers(this.m_spectatorPartyIdMain);
		using (HashSet<BnetGameAccountId>.Enumerator enumerator = this.m_gameServerKnownSpectators.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				BnetGameAccountId knownSpectator = enumerator.Current;
				if (members.FirstOrDefault((bgs.PartyMember m) => m.GameAccountId == knownSpectator) == null)
				{
					BnetParty.SendInvite(this.m_spectatorPartyIdMain, knownSpectator, false);
				}
			}
		}
	}

	// Token: 0x0600817B RID: 33147 RVA: 0x002A2538 File Offset: 0x002A0738
	private void LeaveParty(PartyId partyId, bool dissolve)
	{
		if (partyId == null)
		{
			return;
		}
		if (this.m_leavePartyIdsRequested == null)
		{
			this.m_leavePartyIdsRequested = new HashSet<PartyId>();
		}
		this.m_leavePartyIdsRequested.Add(partyId);
		if (dissolve)
		{
			BnetParty.DissolveParty(partyId);
			return;
		}
		BnetParty.Leave(partyId);
	}

	// Token: 0x0600817C RID: 33148 RVA: 0x002A2574 File Offset: 0x002A0774
	public void LeaveGameScene()
	{
		if (EndGameScreen.Get() != null)
		{
			EndGameScreen.Get().m_hitbox.TriggerPress();
			EndGameScreen.Get().m_hitbox.TriggerRelease();
			return;
		}
		if (!HearthstoneApplication.Get().IsResetting())
		{
			SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
			SceneMgr.Get().SetNextMode(postGameSceneMode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		}
	}

	// Token: 0x0600817D RID: 33149 RVA: 0x002A25D1 File Offset: 0x002A07D1
	private IEnumerator WaitForPresenceThenToast(BnetGameAccountId gameAccountId, SocialToastMgr.TOAST_TYPE toastType)
	{
		float timeStarted = Time.time;
		float num = Time.time - timeStarted;
		while (num < 30f && !BnetUtils.HasPlayerBestNamePresence(gameAccountId))
		{
			yield return null;
			num = Time.time - timeStarted;
		}
		if (SocialToastMgr.Get() != null)
		{
			string playerBestName = BnetUtils.GetPlayerBestName(gameAccountId);
			SocialToastMgr.Get().AddToast(UserAttentionBlocker.NONE, playerBestName, toastType);
		}
		yield break;
	}

	// Token: 0x0600817E RID: 33150 RVA: 0x002A25E7 File Offset: 0x002A07E7
	private SpectatorManager()
	{
	}

	// Token: 0x0600817F RID: 33151 RVA: 0x002A261C File Offset: 0x002A081C
	private static SpectatorManager CreateInstance()
	{
		SpectatorManager.s_instance = new SpectatorManager();
		HearthstoneApplication.Get().WillReset += SpectatorManager.s_instance.WillReset;
		GameMgr.Get().RegisterFindGameEvent(new GameMgr.FindGameCallback(SpectatorManager.s_instance.OnFindGameEvent));
		SceneMgr.Get().RegisterSceneUnloadedEvent(new SceneMgr.SceneUnloadedCallback(SpectatorManager.s_instance.OnSceneUnloaded));
		GameState.RegisterGameStateInitializedListener(new GameState.GameStateInitializedCallback(SpectatorManager.s_instance.GameState_InitializedEvent), null);
		Options.Get().RegisterChangedListener(global::Option.SPECTATOR_OPEN_JOIN, new Options.ChangedCallback(SpectatorManager.s_instance.OnSpectatorOpenJoinOptionChanged));
		BnetPresenceMgr.Get().OnGameAccountPresenceChange += SpectatorManager.s_instance.Presence_OnGameAccountPresenceChange;
		BnetFriendMgr.Get().AddChangeListener(new BnetFriendMgr.ChangeCallback(SpectatorManager.s_instance.BnetFriendMgr_OnFriendsChanged));
		FiresideGatheringManager.OnPatronListUpdated += SpectatorManager.s_instance.FiresideGatheringManager_OnPatronListUpdated;
		EndGameScreen.OnTwoScoopsShown = (EndGameScreen.OnTwoScoopsShownHandler)Delegate.Combine(EndGameScreen.OnTwoScoopsShown, new EndGameScreen.OnTwoScoopsShownHandler(SpectatorManager.s_instance.EndGameScreen_OnTwoScoopsShown));
		EndGameScreen.OnBackOutOfGameplay = (Action)Delegate.Combine(EndGameScreen.OnBackOutOfGameplay, new Action(SpectatorManager.s_instance.EndGameScreen_OnBackOutOfGameplay));
		BnetPresenceMgr.Get().AddPlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(SpectatorManager.s_instance.BnetPresenceMgr_OnPlayersChanged));
		Network.Get().RegisterNetHandler(SpectatorNotify.PacketID.ID, new Network.NetHandler(SpectatorManager.s_instance.Network_OnSpectatorNotifyEvent), null);
		BnetParty.OnError += SpectatorManager.s_instance.BnetParty_OnError;
		BnetParty.OnJoined += SpectatorManager.s_instance.BnetParty_OnJoined;
		BnetParty.OnReceivedInvite += SpectatorManager.s_instance.BnetParty_OnReceivedInvite;
		BnetParty.OnSentInvite += SpectatorManager.s_instance.BnetParty_OnSentInvite;
		BnetParty.OnReceivedInviteRequest += SpectatorManager.s_instance.BnetParty_OnReceivedInviteRequest;
		BnetParty.OnMemberEvent += SpectatorManager.s_instance.BnetParty_OnMemberEvent;
		BnetParty.OnChatMessage += SpectatorManager.s_instance.BnetParty_OnChatMessage;
		BnetParty.RegisterAttributeChangedHandler("WTCG.Party.ServerInfo", new BnetParty.PartyAttributeChangedHandler(SpectatorManager.s_instance.BnetParty_OnPartyAttributeChanged_ServerInfo));
		return SpectatorManager.s_instance;
	}

	// Token: 0x040069A6 RID: 27046
	public const int MAX_SPECTATORS_PER_SIDE = 10;

	// Token: 0x040069AC RID: 27052
	private const float RECEIVED_INVITE_TIMEOUT_SECONDS = 300f;

	// Token: 0x040069AD RID: 27053
	private const float SENT_INVITE_TIMEOUT_SECONDS = 30f;

	// Token: 0x040069AE RID: 27054
	private const bool SPECTATOR_PARTY_INVITES_USE_RESERVATIONS = false;

	// Token: 0x040069AF RID: 27055
	private const float REQUEST_INVITE_TIMEOUT_SECONDS = 5f;

	// Token: 0x040069B0 RID: 27056
	private const string ALERTPOPUPID_WAITINGFORNEXTGAME = "SPECTATOR_WAITING_FOR_NEXT_GAME";

	// Token: 0x040069B1 RID: 27057
	private const float ENDGAMESCREEN_AUTO_CLOSE_SECONDS = 5f;

	// Token: 0x040069B2 RID: 27058
	private static readonly PlatformDependentValue<float> WAITING_FOR_NEXT_GAME_AUTO_LEAVE_SECONDS = new PlatformDependentValue<float>(PlatformCategory.OS)
	{
		iOS = 300f,
		Android = 300f,
		PC = -1f,
		Mac = -1f
	};

	// Token: 0x040069B3 RID: 27059
	private static readonly PlatformDependentValue<bool> DISABLE_MENU_BUTTON_WHILE_WAITING = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		iOS = true,
		Android = true,
		PC = false,
		Mac = false
	};

	// Token: 0x040069B4 RID: 27060
	private static SpectatorManager s_instance = null;

	// Token: 0x040069B5 RID: 27061
	private bool m_initialized;

	// Token: 0x040069B6 RID: 27062
	private BnetGameAccountId m_spectateeFriendlySide;

	// Token: 0x040069B7 RID: 27063
	private BnetGameAccountId m_spectateeOpposingSide;

	// Token: 0x040069B8 RID: 27064
	private PartyId m_spectatorPartyIdMain;

	// Token: 0x040069B9 RID: 27065
	private PartyId m_spectatorPartyIdOpposingSide;

	// Token: 0x040069BA RID: 27066
	private global::Map<PartyId, BnetGameAccountId> m_knownPartyCreatorIds = new global::Map<PartyId, BnetGameAccountId>();

	// Token: 0x040069BB RID: 27067
	private SpectatorManager.IntendedSpectateeParty m_requestedInvite;

	// Token: 0x040069BC RID: 27068
	private AlertPopup m_waitingForNextGameDialog;

	// Token: 0x040069BD RID: 27069
	private HashSet<PartyId> m_leavePartyIdsRequested;

	// Token: 0x040069BE RID: 27070
	private SpectatorManager.PendingSpectatePlayer m_pendingSpectatePlayerAfterLeave;

	// Token: 0x040069BF RID: 27071
	private HashSet<BnetGameAccountId> m_userInitiatedOutgoingInvites;

	// Token: 0x040069C0 RID: 27072
	private HashSet<BnetGameAccountId> m_kickedPlayers;

	// Token: 0x040069C1 RID: 27073
	private global::Map<BnetGameAccountId, uint> m_kickedFromSpectatingList;

	// Token: 0x040069C2 RID: 27074
	private int? m_expectedDisconnectReason;

	// Token: 0x040069C3 RID: 27075
	private bool m_isExpectingArriveInGameplayAsSpectator;

	// Token: 0x040069C4 RID: 27076
	private bool m_isShowingRemovedAsSpectatorPopup;

	// Token: 0x040069C5 RID: 27077
	private HashSet<BnetGameAccountId> m_gameServerKnownSpectators = new HashSet<BnetGameAccountId>();

	// Token: 0x040069C6 RID: 27078
	private global::Map<BnetGameAccountId, SpectatorManager.ReceivedInvite> m_receivedSpectateMeInvites = new global::Map<BnetGameAccountId, SpectatorManager.ReceivedInvite>();

	// Token: 0x040069C7 RID: 27079
	private global::Map<BnetGameAccountId, float> m_sentSpectateMeInvites = new global::Map<BnetGameAccountId, float>();

	// Token: 0x020025DC RID: 9692
	// (Invoke) Token: 0x060134E6 RID: 79078
	public delegate void InviteReceivedHandler(OnlineEventType evt, BnetPlayer inviter);

	// Token: 0x020025DD RID: 9693
	// (Invoke) Token: 0x060134EA RID: 79082
	public delegate void InviteSentHandler(OnlineEventType evt, BnetPlayer invitee);

	// Token: 0x020025DE RID: 9694
	// (Invoke) Token: 0x060134EE RID: 79086
	public delegate void SpectatorToMyGameHandler(OnlineEventType evt, BnetPlayer spectator);

	// Token: 0x020025DF RID: 9695
	// (Invoke) Token: 0x060134F2 RID: 79090
	public delegate void SpectatorModeChangedHandler(OnlineEventType evt, BnetPlayer spectatee);

	// Token: 0x020025E0 RID: 9696
	private struct ReceivedInvite
	{
		// Token: 0x060134F5 RID: 79093 RVA: 0x00530B27 File Offset: 0x0052ED27
		public ReceivedInvite(JoinInfo joinInfo)
		{
			this.m_timestamp = Time.realtimeSinceStartup;
			this.m_joinInfo = joinInfo;
		}

		// Token: 0x0400EEF0 RID: 61168
		public float m_timestamp;

		// Token: 0x0400EEF1 RID: 61169
		public JoinInfo m_joinInfo;
	}

	// Token: 0x020025E1 RID: 9697
	private class IntendedSpectateeParty
	{
		// Token: 0x060134F6 RID: 79094 RVA: 0x00530B3B File Offset: 0x0052ED3B
		public IntendedSpectateeParty(BnetGameAccountId spectateeId, PartyId partyId)
		{
			this.SpectateeId = spectateeId;
			this.PartyId = partyId;
		}

		// Token: 0x0400EEF2 RID: 61170
		public BnetGameAccountId SpectateeId;

		// Token: 0x0400EEF3 RID: 61171
		public PartyId PartyId;
	}

	// Token: 0x020025E2 RID: 9698
	private class PendingSpectatePlayer
	{
		// Token: 0x060134F7 RID: 79095 RVA: 0x00530B51 File Offset: 0x0052ED51
		public PendingSpectatePlayer(BnetGameAccountId spectateeId, JoinInfo joinInfo)
		{
			this.SpectateeId = spectateeId;
			this.JoinInfo = joinInfo;
		}

		// Token: 0x0400EEF4 RID: 61172
		public BnetGameAccountId SpectateeId;

		// Token: 0x0400EEF5 RID: 61173
		public JoinInfo JoinInfo;
	}
}
