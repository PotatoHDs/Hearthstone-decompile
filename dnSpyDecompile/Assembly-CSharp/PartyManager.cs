using System;
using System.Collections.Generic;
using System.Linq;
using bgs;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using bnet.protocol;
using bnet.protocol.account.v1;
using bnet.protocol.v2;
using Hearthstone;
using PegasusShared;
using SpectatorProto;

// Token: 0x0200009F RID: 159
public class PartyManager : IService
{
	// Token: 0x060009EC RID: 2540 RVA: 0x00038862 File Offset: 0x00036A62
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		this.m_partyData = new PartyManager.PartyData();
		BnetParty.OnJoined += this.BnetParty_OnJoined;
		BnetParty.OnReceivedInvite += this.BnetParty_OnReceivedInvite;
		BnetParty.OnPartyAttributeChanged += this.BnetParty_OnPartyAttributeChanged;
		BnetParty.OnMemberAttributeChanged += this.BnetParty_OnMemberAttributeChanged;
		BnetParty.OnMemberEvent += this.BnetParty_OnMemberEvent;
		BnetParty.OnSentInvite += this.BnetParty_OnSentInvite;
		BnetParty.OnReceivedInviteRequest += this.BnetParty_OnReceivedInviteRequest;
		BnetPresenceMgr.Get().AddPlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPresenceUpdated));
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		LoginManager.Get().OnInitialClientStateReceived += this.OnLoginComplete;
		HearthstoneApplication.Get().WillReset += this.WillReset;
		yield break;
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x00038874 File Offset: 0x00036A74
	public void Shutdown()
	{
		BnetParty.OnJoined -= this.BnetParty_OnJoined;
		BnetParty.OnReceivedInvite -= this.BnetParty_OnReceivedInvite;
		BnetParty.OnPartyAttributeChanged -= this.BnetParty_OnPartyAttributeChanged;
		BnetParty.OnMemberAttributeChanged -= this.BnetParty_OnMemberAttributeChanged;
		BnetParty.OnMemberEvent -= this.BnetParty_OnMemberEvent;
		BnetParty.OnSentInvite -= this.BnetParty_OnSentInvite;
		BnetParty.OnReceivedInviteRequest -= this.BnetParty_OnReceivedInviteRequest;
		BnetPresenceMgr.Get().RemovePlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPresenceUpdated));
		FatalErrorMgr.Get().RemoveErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		LoginManager.Get().OnInitialClientStateReceived -= this.OnLoginComplete;
		HearthstoneApplication.Get().WillReset -= this.WillReset;
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x00038952 File Offset: 0x00036B52
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(LoginManager),
			typeof(Network)
		};
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x00038974 File Offset: 0x00036B74
	public static PartyManager Get()
	{
		return HearthstoneServices.Get<PartyManager>();
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x0003897B File Offset: 0x00036B7B
	private void WillReset()
	{
		this.ClearPartyData();
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x00038984 File Offset: 0x00036B84
	public static bool IsPartyTypeEnabledInGuardian(PartyType partyType)
	{
		NetCache.NetCacheFeatures.CacheGames games = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games;
		if (partyType != PartyType.FRIENDLY_CHALLENGE)
		{
			return partyType != PartyType.BATTLEGROUNDS_PARTY || games.BattlegroundsFriendlyChallenge;
		}
		return games.Friendly;
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x000389B8 File Offset: 0x00036BB8
	public bool IsInParty()
	{
		return this.m_partyData.m_partyId != null;
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x000389CB File Offset: 0x00036BCB
	public bool IsInBattlegroundsParty()
	{
		return this.IsInParty() && this.m_partyData.m_type == PartyType.BATTLEGROUNDS_PARTY;
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x000389E5 File Offset: 0x00036BE5
	public bool IsPlayerInCurrentPartyOrPending(BnetGameAccountId playerGameAccountId)
	{
		return this.IsPlayerInCurrentParty(playerGameAccountId) || this.IsPlayerPendingInCurrentParty(playerGameAccountId);
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x000389FE File Offset: 0x00036BFE
	public bool IsPlayerInCurrentParty(BnetGameAccountId playerGameAccountId)
	{
		return BnetParty.IsMember(this.m_partyData.m_partyId, playerGameAccountId);
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x00038A18 File Offset: 0x00036C18
	public bool IsPlayerPendingInCurrentParty(BnetGameAccountId playerGameAccountId)
	{
		PartyInvite[] pendingInvites = this.GetPendingInvites();
		for (int i = 0; i < pendingInvites.Length; i++)
		{
			if (pendingInvites[i].InviteeId == playerGameAccountId)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x00038A50 File Offset: 0x00036C50
	public bool IsPlayerInAnyParty(BnetGameAccountId playerGameAccountId)
	{
		BnetPlayer player = BnetUtils.GetPlayer(playerGameAccountId);
		if (player == null)
		{
			return false;
		}
		BnetGameAccount hearthstoneGameAccount = player.GetHearthstoneGameAccount();
		return !(hearthstoneGameAccount == null) && hearthstoneGameAccount.GetGameFields() != null && hearthstoneGameAccount.GetPartyId() != PartyId.Empty;
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x00038A93 File Offset: 0x00036C93
	public bool IsPartyLeader()
	{
		return BnetParty.IsLeader(this.m_partyData.m_partyId);
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x00038AA8 File Offset: 0x00036CA8
	public bool CanInvite(BnetGameAccountId playerGameAccountId)
	{
		if (this.IsInParty() && !this.IsPartyLeader())
		{
			return false;
		}
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		if (!myPlayer.IsOnline() || myPlayer.IsAppearingOffline())
		{
			return false;
		}
		if (this.IsPlayerInAnyParty(playerGameAccountId))
		{
			return false;
		}
		if (this.IsPlayerPendingInCurrentParty(playerGameAccountId))
		{
			return false;
		}
		if (this.IsInParty() && this.GetCurrentPartySize() >= this.GetMaxPartySizeByPartyType(this.m_partyData.m_type))
		{
			return false;
		}
		BnetPlayer player = BnetUtils.GetPlayer(playerGameAccountId);
		return player != null && FriendChallengeMgr.Get().IsOpponentAvailable(player);
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x00038B37 File Offset: 0x00036D37
	public bool CanKick(BnetGameAccountId playerGameAccountId)
	{
		return (!this.IsInParty() || this.IsPartyLeader()) && this.IsPlayerInCurrentPartyOrPending(playerGameAccountId);
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x00038B58 File Offset: 0x00036D58
	public bool CanSpectatePartyMember(BnetGameAccountId gameAccountId)
	{
		JoinInfo spectatorJoinInfoForPlayer = this.GetSpectatorJoinInfoForPlayer(gameAccountId);
		return spectatorJoinInfoForPlayer != null && SpectatorManager.Get().CanSpectate(gameAccountId, spectatorJoinInfoForPlayer);
	}

	// Token: 0x060009FC RID: 2556 RVA: 0x00038B80 File Offset: 0x00036D80
	public bool SpectatePartyMember(BnetGameAccountId gameAccountId)
	{
		JoinInfo spectatorJoinInfoForPlayer = this.GetSpectatorJoinInfoForPlayer(gameAccountId);
		if (spectatorJoinInfoForPlayer == null)
		{
			return false;
		}
		if (!this.CanSpectatePartyMember(gameAccountId))
		{
			return false;
		}
		SpectatorManager.Get().SpectatePlayer(gameAccountId, spectatorJoinInfoForPlayer);
		return true;
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x00038BB4 File Offset: 0x00036DB4
	public void SendInvite(PartyType partyType, BnetGameAccountId playerGameAccountId)
	{
		if (!this.CanInvite(playerGameAccountId))
		{
			return;
		}
		if (this.IsPlayerInCurrentPartyOrPending(playerGameAccountId))
		{
			return;
		}
		if (!this.IsInParty() && this.ShouldSupportPartyType(partyType))
		{
			this.CreateParty(partyType, playerGameAccountId);
			return;
		}
		if (partyType == PartyType.BATTLEGROUNDS_PARTY)
		{
			this.InvitePlayerToBattlegroundsParty(playerGameAccountId);
			return;
		}
		this.SendInvite_Internal(playerGameAccountId);
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x00038C04 File Offset: 0x00036E04
	public void KickPlayerFromParty(BnetGameAccountId playerGameAccountId)
	{
		if (!this.IsInParty())
		{
			return;
		}
		if (BnetParty.IsMember(this.m_partyData.m_partyId, playerGameAccountId))
		{
			BnetParty.KickMember(this.m_partyData.m_partyId, playerGameAccountId);
			return;
		}
		ulong? pendingInviteIdFromGameAccount = this.GetPendingInviteIdFromGameAccount(playerGameAccountId);
		if (pendingInviteIdFromGameAccount != null)
		{
			BnetPlayer bnetPlayer = BnetNearbyPlayerMgr.Get().FindNearbyStranger(playerGameAccountId);
			if (bnetPlayer != null)
			{
				bnetPlayer.GetHearthstoneGameAccount().SetGameField(1U, false);
			}
			BnetParty.RevokeSentInvite(this.m_partyData.m_partyId, pendingInviteIdFromGameAccount.Value);
			this.FireChangedEvent(PartyManager.PartyInviteEvent.I_RESCINDED_INVITE, playerGameAccountId);
			return;
		}
		global::Log.Party.PrintError("Unable to kick player {0} from party. Player not found in party.", new object[]
		{
			playerGameAccountId.ToString()
		});
	}

	// Token: 0x060009FF RID: 2559 RVA: 0x00038CAF File Offset: 0x00036EAF
	public PartyMember[] GetMembers()
	{
		if (this.m_partyData.m_partyId == null)
		{
			return new PartyMember[0];
		}
		return BnetParty.GetMembers(this.m_partyData.m_partyId);
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x00038CDB File Offset: 0x00036EDB
	public PartyInvite[] GetPendingInvites()
	{
		if (this.m_partyData.m_partyId == null)
		{
			return new PartyInvite[0];
		}
		return BnetParty.GetSentInvites(this.m_partyData.m_partyId);
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x00038D07 File Offset: 0x00036F07
	public void FindGame()
	{
		BnetParty.SetPartyAttributeString(this.m_partyData.m_partyId, "queue", "in_queue");
		if (this.IsBaconParty())
		{
			Network.Get().EnterBattlegroundsWithParty(this.GetMembers(), 3459);
		}
		this.WaitForGame();
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x00038D48 File Offset: 0x00036F48
	public BnetGameAccountId GetLeader()
	{
		if (!this.IsInParty())
		{
			return null;
		}
		PartyMember leader = BnetParty.GetLeader(this.m_partyData.m_partyId);
		if (leader != null)
		{
			return leader.GameAccountId;
		}
		global::Log.Party.PrintError("PartyManager.GetLeader() - Unable to get party leader!", Array.Empty<object>());
		return null;
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x00038D8F File Offset: 0x00036F8F
	public bool IsBaconParty()
	{
		return this.m_partyData.m_partyId != null && this.m_partyData.m_scenarioId == ScenarioDbId.TB_BACONSHOP_8P;
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x00038DB8 File Offset: 0x00036FB8
	public void LeaveParty()
	{
		if (!this.IsInParty())
		{
			return;
		}
		if (this.IsPartyLeader())
		{
			BnetParty.DissolveParty(this.m_partyData.m_partyId);
		}
		else
		{
			BnetParty.Leave(this.m_partyData.m_partyId);
		}
		this.ClearPartyData();
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x00038DF4 File Offset: 0x00036FF4
	public void CancelQueue()
	{
		BnetGameAccountId hearthstoneGameAccountId = BnetPresenceMgr.Get().GetMyPlayer().GetHearthstoneGameAccountId();
		byte[] value = ProtobufUtil.ToByteArray(new BnetId
		{
			Hi = hearthstoneGameAccountId.GetHi(),
			Lo = hearthstoneGameAccountId.GetLo()
		});
		BnetParty.SetPartyAttributeBlob(this.m_partyData.m_partyId, "canceled_by", value);
		BnetParty.SetPartyAttributeString(this.m_partyData.m_partyId, "queue", "cancel_queue");
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x00038E64 File Offset: 0x00037064
	public int GetCurrentPartySize()
	{
		return this.GetCurrentAndPendingPartyMembers().Count<BnetGameAccountId>();
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x00038E74 File Offset: 0x00037074
	public int GetReadyPartyMemberCount()
	{
		int num = 0;
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		PartyMember[] members = BnetParty.GetMembers(this.m_partyData.m_partyId);
		for (int i = 0; i < members.Length; i++)
		{
			BnetPlayer player = BnetUtils.GetPlayer(members[i].GameAccountId);
			if (player != null && (myPlayer == player || FriendChallengeMgr.Get().IsOpponentAvailable(player)))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x00038ED8 File Offset: 0x000370D8
	public List<BnetGameAccountId> GetCurrentAndPendingPartyMembers()
	{
		List<BnetGameAccountId> list = new List<BnetGameAccountId>();
		foreach (PartyMember partyMember in BnetParty.GetMembers(this.m_partyData.m_partyId))
		{
			list.Add(partyMember.GameAccountId);
		}
		PartyInvite[] pendingInvites = this.GetPendingInvites();
		for (int i = 0; i < pendingInvites.Length; i++)
		{
			BnetGameAccountId inviteeId = pendingInvites[i].InviteeId;
			if (!list.Contains(inviteeId))
			{
				list.Add(inviteeId);
			}
		}
		return list;
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x00038F4F File Offset: 0x0003714F
	public int GetMaxPartySizeByPartyType(PartyType type)
	{
		if (type == PartyType.BATTLEGROUNDS_PARTY)
		{
			return PartyManager.BATTLEGROUNDS_PARTY_LIMIT;
		}
		global::Log.Party.PrintError("GetMaxPartySizeByPartyType() - Unsupported party type {0}.", new object[]
		{
			type.ToString()
		});
		return 2;
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x00038F84 File Offset: 0x00037184
	public void UpdateSpectatorJoinInfo(JoinInfo joinInfo)
	{
		if (!this.IsInParty())
		{
			return;
		}
		BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
		byte[] value = (joinInfo == null) ? null : ProtobufUtil.ToByteArray(joinInfo);
		BnetParty.SetMemberAttributeBlob(this.m_partyData.m_partyId, myGameAccountId.GetGameAccountHandle(), "spectator_info", value);
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x00038FD0 File Offset: 0x000371D0
	public JoinInfo GetSpectatorJoinInfoForPlayer(BnetGameAccountId gameAccountId)
	{
		if (!this.IsInParty())
		{
			return null;
		}
		byte[] memberAttributeBlob = BnetParty.GetMemberAttributeBlob(this.m_partyData.m_partyId, gameAccountId.GetGameAccountHandle(), "spectator_info");
		if (memberAttributeBlob != null && memberAttributeBlob.Length != 0)
		{
			return ProtobufUtil.ParseFrom<JoinInfo>(memberAttributeBlob, 0, -1);
		}
		return null;
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x00039014 File Offset: 0x00037214
	public string GetPartyMemberName(BnetGameAccountId playerGameAccountId)
	{
		BnetPlayer player = BnetUtils.GetPlayer(playerGameAccountId);
		if (player != null)
		{
			return player.GetBestName();
		}
		foreach (PartyMember partyMember in this.GetMembers())
		{
			if (partyMember.GameAccountId == playerGameAccountId)
			{
				if (!string.IsNullOrEmpty(partyMember.BattleTag))
				{
					BnetBattleTag bnetBattleTag = BnetBattleTag.CreateFromString(partyMember.BattleTag);
					if (!(bnetBattleTag == null))
					{
						return bnetBattleTag.GetName();
					}
					return partyMember.BattleTag;
				}
				else
				{
					global::Log.Party.PrintError("GetPartyMemberName() - No name for party member {0}.", new object[]
					{
						playerGameAccountId.ToString()
					});
				}
			}
		}
		foreach (PartyInvite partyInvite in this.GetPendingInvites())
		{
			if (partyInvite.InviteeId == playerGameAccountId)
			{
				if (!string.IsNullOrEmpty(partyInvite.InviteeName))
				{
					BnetBattleTag bnetBattleTag2 = BnetBattleTag.CreateFromString(partyInvite.InviteeName);
					if (!(bnetBattleTag2 == null))
					{
						return bnetBattleTag2.GetName();
					}
					return partyInvite.InviteeName;
				}
				else
				{
					global::Log.Party.PrintError("GetPartyMemberName() - No name for pending invitee {0}.", new object[]
					{
						playerGameAccountId.ToString()
					});
				}
			}
		}
		return GameStrings.Get("GLUE_PARTY_MEMBER_NO_NAME");
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x00039134 File Offset: 0x00037334
	public bool HasPendingPartyInviteOrDialog()
	{
		return this.m_pendingParty != null || this.m_inviteDialog != null;
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x00039154 File Offset: 0x00037354
	public int GetBattlegroundsMaxRankedPartySize()
	{
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		if (netObject != null)
		{
			return netObject.BattlegroundsMaxRankedPartySize;
		}
		return PartyManager.BATTLEGROUNDS_MAX_RANKED_PARTY_SIZE_FALLBACK;
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x0003917C File Offset: 0x0003737C
	private void InvitePlayerToBattlegroundsParty(BnetGameAccountId playerGameAccountId)
	{
		int currentPartySize = this.GetCurrentPartySize();
		if (currentPartySize >= PartyManager.BATTLEGROUNDS_PARTY_LIMIT)
		{
			return;
		}
		if (currentPartySize == this.GetBattlegroundsMaxRankedPartySize())
		{
			this.ShowBattlegroundsPrivatePartyDialog(playerGameAccountId);
			return;
		}
		this.SendInvite_Internal(playerGameAccountId);
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x000391B4 File Offset: 0x000373B4
	private void ShowBattlegroundsPrivatePartyDialog(BnetGameAccountId playerGameAccountId)
	{
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_BACON_PRIVATE_PARTY_TITLE"),
			m_text = GameStrings.Format("GLUE_BACON_PRIVATE_PARTY_WARNING", new object[]
			{
				this.GetBattlegroundsMaxRankedPartySize()
			}),
			m_iconSet = AlertPopup.PopupInfo.IconSet.Default,
			m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
			m_confirmText = GameStrings.Get("GLUE_COLLECTION_DECK_COMPLETE_POPUP_CONFIRM"),
			m_cancelText = GameStrings.Get("GLUE_COLLECTION_DECK_COMPLETE_POPUP_CANCEL"),
			m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				if (response == AlertPopup.Response.CONFIRM)
				{
					this.SendInvite_Internal(playerGameAccountId);
				}
			}
		};
		DialogManager.Get().ShowPopup(info);
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x0003925C File Offset: 0x0003745C
	private void ClearPartyData()
	{
		this.m_partyData = new PartyManager.PartyData();
		this.UpdateMyAvailability();
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x0003926F File Offset: 0x0003746F
	private bool ShouldSupportPartyType(PartyType partyType)
	{
		return partyType == PartyType.BATTLEGROUNDS_PARTY;
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x00039275 File Offset: 0x00037475
	private void WaitForGame()
	{
		GameMgr.Get().WaitForFriendChallengeToStart(this.m_partyData.m_format, BrawlType.BRAWL_TYPE_UNKNOWN, (int)this.m_partyData.m_scenarioId, 0, this.IsBaconParty());
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x0003929F File Offset: 0x0003749F
	private ScenarioDbId GetScenario(PartyType type)
	{
		if (type == PartyType.BATTLEGROUNDS_PARTY)
		{
			return ScenarioDbId.TB_BACONSHOP_8P;
		}
		global::Log.Party.PrintError("PartyManager.GetScenario() received an unsupported party type: {0}", new object[]
		{
			type
		});
		return ScenarioDbId.INVALID;
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x0001FA65 File Offset: 0x0001DC65
	private FormatType GetFormat(PartyType type)
	{
		return FormatType.FT_UNKNOWN;
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x0001FA65 File Offset: 0x0001DC65
	private int GetSeason(PartyType type)
	{
		return 0;
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x000392CA File Offset: 0x000374CA
	private SceneMgr.Mode GetMode(PartyType type)
	{
		if (type == PartyType.BATTLEGROUNDS_PARTY)
		{
			return SceneMgr.Mode.BACON;
		}
		global::Log.Party.PrintError("PartyManager.GetMode() received an unsupported party type: {0}", new object[]
		{
			type
		});
		return SceneMgr.Mode.HUB;
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x000392F4 File Offset: 0x000374F4
	private void CreateParty(PartyType type, BnetGameAccountId playerToInvite)
	{
		if (this.IsInParty())
		{
			return;
		}
		this.m_partyData.m_type = type;
		this.m_partyData.m_scenarioId = this.GetScenario(type);
		this.m_partyData.m_format = this.GetFormat(type);
		this.m_partyData.m_season = this.GetSeason(type);
		bnet.protocol.Attribute attributeV = ProtocolHelper.CreateAttribute("WTCG.Game.ScenarioId", (long)this.m_partyData.m_scenarioId);
		bnet.protocol.Attribute attributeV2 = ProtocolHelper.CreateAttribute("WTCG.Format.Type", (long)this.m_partyData.m_format);
		bnet.protocol.Attribute attributeV3 = ProtocolHelper.CreateAttribute("WTCG.Season.Id", (long)this.m_partyData.m_season);
		BnetParty.CreateParty(type, PrivacyLevel.OPEN_INVITATION, delegate(PartyType pType, PartyId newlyCreatedPartyId)
		{
			this.m_partyData.m_partyId = newlyCreatedPartyId;
			this.UpdateMyAvailability();
			BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
			this.SetPersonalPartyMemberAttributes();
			this.FireChangedEvent(PartyManager.PartyInviteEvent.I_CREATED_PARTY, myGameAccountId);
			if (playerToInvite != null)
			{
				this.SendInvite(type, playerToInvite);
			}
		}, new bnet.protocol.v2.Attribute[]
		{
			ProtocolHelper.V1AttributeToV2Attribute(attributeV),
			ProtocolHelper.V1AttributeToV2Attribute(attributeV2),
			ProtocolHelper.V1AttributeToV2Attribute(attributeV3)
		});
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x000393FC File Offset: 0x000375FC
	private void SetPersonalPartyMemberAttributes()
	{
		GameAccountHandle gameAccountHandle = BnetPresenceMgr.Get().GetMyPlayer().GetBestGameAccountId().GetGameAccountHandle();
		BnetParty.SetMemberAttributeString(this.m_partyData.m_partyId, gameAccountHandle, "ready", "ready");
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x00039439 File Offset: 0x00037639
	private void SendInvite_Internal(BnetGameAccountId bnetGameAccountId)
	{
		BnetParty.SendInvite(this.m_partyData.m_partyId, bnetGameAccountId, true);
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x00039450 File Offset: 0x00037650
	private void UpdateMyAvailability()
	{
		if (!Network.ShouldBeConnectedToAurora() || !Network.IsLoggedIn())
		{
			return;
		}
		PartyId partyId = this.m_partyData.m_partyId;
		BnetPresenceMgr.Get().SetGameField(26U, (partyId != null) ? partyId.ToEntityId() : PartyId.Empty.ToEntityId());
		BnetNearbyPlayerMgr.Get().SetPartyId(partyId ?? PartyId.Empty);
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x000394B4 File Offset: 0x000376B4
	private void ShowInviteDialog(BnetGameAccountId leaderGameAccountId, PartyType partyType)
	{
		BnetPlayer player = BnetUtils.GetPlayer(leaderGameAccountId);
		if (player == null)
		{
			global::Log.Party.PrintError("PartyManager.ShowInviteDialog() - Received invite from player {0} with no presence!", new object[]
			{
				leaderGameAccountId
			});
		}
		DialogManager.Get().ShowFriendlyChallenge(FormatType.FT_UNKNOWN, player, false, partyType, new FriendlyChallengeDialog.ResponseCallback(this.OnInviteReceivedDialogResponse), new DialogManager.DialogProcessCallback(this.OnInviteReceivedDialogProcessed));
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x0003950C File Offset: 0x0003770C
	private void ShowSimpleAlertDialog(string header, string body, bool showAlertIcon = false)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get(header);
		popupInfo.m_text = GameStrings.Get(body);
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_showAlertIcon = showAlertIcon;
		popupInfo.m_okText = GameStrings.Get("GLOBAL_OKAY");
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x06000A1E RID: 2590 RVA: 0x00039560 File Offset: 0x00037760
	private bool OnInviteReceivedDialogProcessed(DialogBase dialog, object userData)
	{
		this.m_inviteDialog = dialog;
		return true;
	}

	// Token: 0x06000A1F RID: 2591 RVA: 0x0003956C File Offset: 0x0003776C
	private void OnInviteReceivedDialogResponse(bool accept)
	{
		BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
		if (accept)
		{
			if (BnetPresenceMgr.Get().GetMyPlayer().IsAppearingOffline())
			{
				this.DeclinePartyInvite(this.m_partyData.m_inviteId);
				this.ShowSimpleAlertDialog("GLUE_BACON_INVITE_WHILE_APPEARING_OFFLINE_HEADER", "GLUE_BACON_INVITE_WHILE_APPEARING_OFFLINE", true);
			}
			else if (this.m_pendingParty != null && !this.IsInParty())
			{
				this.m_partyData.m_partyId = this.m_pendingParty;
				BnetParty.AcceptReceivedInvite(this.m_partyData.m_inviteId);
				this.UpdateMyAvailability();
				this.FireChangedEvent(PartyManager.PartyInviteEvent.I_ACCEPTED_INVITE, myGameAccountId);
				this.TransitionModeIfNeeded();
			}
			else if (this.IsInParty())
			{
				this.ShowSimpleAlertDialog("GLUE_BACON_EXPIRED_INVITE_HEADER", "GLUE_BACON_PARTY_INVITE_WHILE_IN_PARTY", false);
			}
			else
			{
				this.ShowSimpleAlertDialog("GLUE_BACON_EXPIRED_INVITE_HEADER", "GLUE_BACON_EXPIRD_INVITE_BODY", false);
			}
		}
		else
		{
			this.DeclinePartyInvite(this.m_partyData.m_inviteId);
		}
		this.m_inviteDialog = null;
		this.m_pendingParty = null;
		FriendChallengeMgr.Get().UpdateMyAvailability();
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x00039668 File Offset: 0x00037868
	private void DeclinePartyInvite(ulong inviteId)
	{
		BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
		BnetParty.DeclineReceivedInvite(inviteId);
		this.FireChangedEvent(PartyManager.PartyInviteEvent.I_DECLINED_INVITE, myGameAccountId);
		this.m_pendingParty = null;
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x00039698 File Offset: 0x00037898
	private void TransitionModeIfNeeded()
	{
		SceneMgr.Mode mode = this.GetMode(this.m_partyData.m_type);
		SceneMgr.Mode mode2 = SceneMgr.Get().GetMode();
		if (mode != mode2)
		{
			SceneMgr.Get().SetNextMode(mode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		}
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x000396D4 File Offset: 0x000378D4
	private void OnPresenceUpdated(BnetPlayerChangelist changelist, object userData)
	{
		foreach (BnetPlayerChange bnetPlayerChange in changelist.GetChanges())
		{
			BnetPlayer player = bnetPlayerChange.GetPlayer();
			BnetGameAccountId bestGameAccountId = player.GetBestGameAccountId();
			if (this.IsPlayerInCurrentPartyOrPending(bestGameAccountId) && !player.IsOnline())
			{
				this.KickPlayerFromParty(bestGameAccountId);
			}
		}
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x0003897B File Offset: 0x00036B7B
	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		this.ClearPartyData();
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x00039744 File Offset: 0x00037944
	private void OnLoginComplete()
	{
		this.UpdateMyAvailability();
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x0003974C File Offset: 0x0003794C
	private ulong? GetPendingInviteIdFromGameAccount(BnetGameAccountId gameAccountId)
	{
		foreach (PartyInvite partyInvite in this.GetPendingInvites())
		{
			if (partyInvite.InviteeId == gameAccountId)
			{
				return new ulong?(partyInvite.InviteId);
			}
		}
		return null;
	}

	// Token: 0x06000A26 RID: 2598 RVA: 0x00039798 File Offset: 0x00037998
	private void BnetParty_OnJoined(OnlineEventType evt, PartyInfo party, LeaveReason? reason)
	{
		if (!this.ShouldSupportPartyType(party.Type))
		{
			return;
		}
		if (party.Id != this.m_partyData.m_partyId)
		{
			return;
		}
		if (evt == OnlineEventType.ADDED)
		{
			this.m_partyData.m_partyId = party.Id;
			this.UpdateMyAvailability();
			this.SetPersonalPartyMemberAttributes();
			long? partyAttributeLong = BnetParty.GetPartyAttributeLong(party.Id, "WTCG.Game.ScenarioId");
			if (partyAttributeLong != null)
			{
				this.m_partyData.m_scenarioId = (ScenarioDbId)partyAttributeLong.Value;
			}
			long? partyAttributeLong2 = BnetParty.GetPartyAttributeLong(party.Id, "WTCG.Format.Type");
			if (partyAttributeLong2 != null)
			{
				this.m_partyData.m_format = (FormatType)partyAttributeLong2.Value;
			}
			long? partyAttributeLong3 = BnetParty.GetPartyAttributeLong(party.Id, "WTCG.Season.Id");
			if (partyAttributeLong3 != null)
			{
				this.m_partyData.m_season = (int)partyAttributeLong3.Value;
			}
		}
		if (evt == OnlineEventType.REMOVED)
		{
			this.ClearPartyData();
			this.UpdateMyAvailability();
			LeaveReason? leaveReason = reason;
			if (leaveReason != null)
			{
				switch (leaveReason.GetValueOrDefault())
				{
				case LeaveReason.MEMBER_KICKED:
					this.ShowSimpleAlertDialog("GLUE_BACON_PARTY_KICKED_HEADER", "GLUE_BACON_PARTY_KICKED_BODY", false);
					break;
				case LeaveReason.DISSOLVED_BY_MEMBER:
				case LeaveReason.DISSOLVED_BY_SERVICE:
					this.ShowSimpleAlertDialog("GLUE_BACON_PARTY_DISBANDED_HEADER", "GLUE_BACON_PARTY_DISBANDED_BODY", false);
					break;
				}
			}
			this.FireChangedEvent(PartyManager.PartyInviteEvent.LEADER_DISSOLVED_PARTY, null);
		}
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x000398E8 File Offset: 0x00037AE8
	private void BnetParty_OnReceivedInvite(OnlineEventType evt, PartyInfo party, ulong inviteId, BnetGameAccountId inviter, BnetGameAccountId invitee, InviteRemoveReason? reason)
	{
		if (!this.ShouldSupportPartyType(party.Type))
		{
			return;
		}
		if (evt == OnlineEventType.ADDED)
		{
			if (!PartyManager.IsPartyTypeEnabledInGuardian(party.Type))
			{
				this.DeclinePartyInvite(inviteId);
				return;
			}
			if (!FriendChallengeMgr.Get().AmIAvailable())
			{
				this.DeclinePartyInvite(inviteId);
				return;
			}
			this.m_partyData.m_inviteId = inviteId;
			this.m_partyData.m_type = party.Type;
			this.m_pendingParty = party.Id;
			this.ShowInviteDialog(inviter, party.Type);
		}
		else if (evt == OnlineEventType.REMOVED)
		{
			this.m_pendingParty = null;
			if (this.m_inviteDialog != null)
			{
				this.m_inviteDialog.AddHiddenOrDestroyedListener(delegate(DialogBase dialog, object o)
				{
					this.m_inviteDialog = null;
					FriendChallengeMgr.Get().UpdateMyAvailability();
				});
			}
		}
		FriendChallengeMgr.Get().UpdateMyAvailability();
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x000399A4 File Offset: 0x00037BA4
	private void BnetParty_OnMemberEvent(OnlineEventType evt, PartyInfo party, BnetGameAccountId memberId, bool isRolesUpdate, LeaveReason? reason)
	{
		if (!this.ShouldSupportPartyType(party.Type))
		{
			return;
		}
		if (party.Id != this.m_partyData.m_partyId)
		{
			return;
		}
		BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
		global::Log.Party.PrintDebug("PartyManager.BnetParty_OnMemberEvent() received event {0} for member {1}", new object[]
		{
			evt.ToString(),
			memberId.ToString()
		});
		if (evt == OnlineEventType.REMOVED && BnetParty.IsInParty(party.Id) && memberId != myGameAccountId)
		{
			this.FireChangedEvent(PartyManager.PartyInviteEvent.FRIEND_LEFT, memberId);
			BnetGameAccountId leader = this.GetLeader();
			if (leader == null || leader == memberId)
			{
				this.LeaveParty();
				this.FireChangedEvent(PartyManager.PartyInviteEvent.LEADER_DISSOLVED_PARTY, memberId);
				return;
			}
		}
		else if (evt == OnlineEventType.ADDED && BnetParty.IsInParty(party.Id) && memberId != myGameAccountId)
		{
			if (this.IsPartyLeader() && !BnetPresenceMgr.Get().GetGameAccount(memberId).IsOnline())
			{
				this.KickPlayerFromParty(memberId);
				return;
			}
			this.FireChangedEvent(PartyManager.PartyInviteEvent.FRIEND_ACCEPTED_INVITE, memberId);
		}
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x00039AA4 File Offset: 0x00037CA4
	private void BnetParty_OnPartyAttributeChanged(PartyInfo party, string attributeKey, bnet.protocol.Variant attributeValue)
	{
		if (!this.ShouldSupportPartyType(party.Type))
		{
			return;
		}
		if (this.m_partyData.m_partyId != party.Id)
		{
			return;
		}
		if (!(attributeKey == "queue"))
		{
			if (!(attributeKey == "canceled_by"))
			{
				return;
			}
			if (attributeValue.HasBlobValue)
			{
				BnetId bnetId = ProtobufUtil.ParseFrom<BnetId>(attributeValue.BlobValue, 0, -1);
				BnetGameAccountId bnetGameAccountId = new BnetGameAccountId();
				bnetGameAccountId.SetHi(bnetId.Hi);
				bnetGameAccountId.SetLo(bnetId.Lo);
				BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
				if (!(bnetGameAccountId == myGameAccountId))
				{
					string partyMemberName = this.GetPartyMemberName(bnetGameAccountId);
					AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
					popupInfo.m_headerText = GameStrings.Get("GLUE_BACON_PRIVATE_PARTY_TITLE");
					popupInfo.m_text = GameStrings.Format("GLUE_BACON_QUEUE_CANCELED", new object[]
					{
						"5ecaf0ff",
						partyMemberName
					});
					popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
					popupInfo.m_showAlertIcon = false;
					popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
					popupInfo.m_okText = GameStrings.Get("GLOBAL_OKAY");
					DialogManager.Get().ShowPopup(popupInfo);
				}
			}
		}
		else
		{
			if (attributeValue.HasStringValue && attributeValue.StringValue.Equals("in_queue"))
			{
				Shop.Get().Close(true);
				this.WaitForGame();
			}
			if (attributeValue.HasStringValue && attributeValue.StringValue.Equals("cancel_queue"))
			{
				GameMgr.Get().CancelFindGame();
				return;
			}
		}
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x00039C10 File Offset: 0x00037E10
	private void BnetParty_OnMemberAttributeChanged(PartyInfo party, BnetGameAccountId partyMember, string attributeKey, bnet.protocol.Variant attributeValue)
	{
		if (!this.ShouldSupportPartyType(party.Type))
		{
			return;
		}
		if (this.m_partyData.m_partyId != party.Id)
		{
			return;
		}
		global::Log.Party.PrintDebug("PartyManager.BnetParty_OnMemberAttributeChanged() - Key={0}, Value={1}", new object[]
		{
			attributeKey,
			attributeValue
		});
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x00039C64 File Offset: 0x00037E64
	private void BnetParty_OnSentInvite(OnlineEventType evt, PartyInfo party, ulong inviteId, BnetGameAccountId inviter, BnetGameAccountId invitee, bool senderIsMyself, InviteRemoveReason? reason)
	{
		if (!this.ShouldSupportPartyType(party.Type))
		{
			return;
		}
		if (this.m_partyData.m_partyId != party.Id)
		{
			return;
		}
		if (evt == OnlineEventType.ADDED)
		{
			if (inviter != BnetPresenceMgr.Get().GetMyGameAccountId())
			{
				this.FireChangedEvent(PartyManager.PartyInviteEvent.I_SENT_INVITE, invitee);
			}
			else
			{
				this.FireChangedEvent(PartyManager.PartyInviteEvent.FRIEND_RECEIVED_INVITE, invitee);
			}
		}
		if (evt == OnlineEventType.REMOVED)
		{
			InviteRemoveReason? inviteRemoveReason = reason;
			if (inviteRemoveReason != null)
			{
				switch (inviteRemoveReason.GetValueOrDefault())
				{
				case InviteRemoveReason.DECLINED:
					this.FireChangedEvent(PartyManager.PartyInviteEvent.FRIEND_DECLINED_INVITE, invitee);
					return;
				case InviteRemoveReason.REVOKED:
				case InviteRemoveReason.EXPIRED:
				case InviteRemoveReason.CANCELED:
					this.FireChangedEvent(PartyManager.PartyInviteEvent.INVITE_EXPIRED, invitee);
					break;
				case InviteRemoveReason.IGNORED:
					break;
				default:
					return;
				}
			}
		}
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x00039D0B File Offset: 0x00037F0B
	private void BnetParty_OnReceivedInviteRequest(OnlineEventType evt, PartyInfo party, InviteRequest request, InviteRequestRemovedReason? reason)
	{
		if (!this.ShouldSupportPartyType(party.Type))
		{
			return;
		}
		this.m_partyData.m_partyId != party.Id;
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x00039D34 File Offset: 0x00037F34
	private void FireChangedEvent(PartyManager.PartyInviteEvent challengeEvent, BnetGameAccountId playerGameAccountId)
	{
		PartyManager.ChangedListener[] array = this.m_changedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(challengeEvent, playerGameAccountId, this.m_partyData);
		}
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x00039D6B File Offset: 0x00037F6B
	public bool AddChangedListener(PartyManager.ChangedCallback callback)
	{
		return this.AddChangedListener(callback, null);
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x00039D78 File Offset: 0x00037F78
	public bool AddChangedListener(PartyManager.ChangedCallback callback, object userData)
	{
		PartyManager.ChangedListener changedListener = new PartyManager.ChangedListener();
		changedListener.SetCallback(callback);
		changedListener.SetUserData(userData);
		if (this.m_changedListeners.Contains(changedListener))
		{
			return false;
		}
		this.m_changedListeners.Add(changedListener);
		return true;
	}

	// Token: 0x06000A30 RID: 2608 RVA: 0x00039DB6 File Offset: 0x00037FB6
	public bool RemoveChangedListener(PartyManager.ChangedCallback callback)
	{
		return this.RemoveChangedListener(callback, null);
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x00039DC0 File Offset: 0x00037FC0
	public bool RemoveChangedListener(PartyManager.ChangedCallback callback, object userData)
	{
		PartyManager.ChangedListener changedListener = new PartyManager.ChangedListener();
		changedListener.SetCallback(callback);
		changedListener.SetUserData(userData);
		return this.m_changedListeners.Remove(changedListener);
	}

	// Token: 0x0400067B RID: 1659
	private PartyManager.PartyData m_partyData = new PartyManager.PartyData();

	// Token: 0x0400067C RID: 1660
	private DialogBase m_inviteDialog;

	// Token: 0x0400067D RID: 1661
	private PartyId m_pendingParty;

	// Token: 0x0400067E RID: 1662
	private const string ATTRIBUTE_KEY_QUEUE = "queue";

	// Token: 0x0400067F RID: 1663
	private const string ATTRIBUTE_VALUE_IN_QUEUE = "in_queue";

	// Token: 0x04000680 RID: 1664
	private const string ATTRIBUTE_VALUE_CANCEL_QUEUE = "cancel_queue";

	// Token: 0x04000681 RID: 1665
	private const string ATTRIBUTE_KEY_CANCELED_BY = "canceled_by";

	// Token: 0x04000682 RID: 1666
	private const string ATTRIBUTE_KEY_STATUS = "ready";

	// Token: 0x04000683 RID: 1667
	private const string ATTRIBUTE_VALUE_READY = "ready";

	// Token: 0x04000684 RID: 1668
	private const string ATTRIBUTE_VALUE_NOT_READY = "not_ready";

	// Token: 0x04000685 RID: 1669
	private const string ATTRIBUTE_VALUE_DECLINED = "declined";

	// Token: 0x04000686 RID: 1670
	private const string ATTRIBUTE_VALUE_LEADER = "leader";

	// Token: 0x04000687 RID: 1671
	private const string ATTRIBUTE_KEY_SPECTATOR_INFO = "spectator_info";

	// Token: 0x04000688 RID: 1672
	public static int BATTLEGROUNDS_PARTY_LIMIT = 8;

	// Token: 0x04000689 RID: 1673
	public static int BATTLEGROUNDS_MAX_RANKED_PARTY_SIZE_FALLBACK = 4;

	// Token: 0x0400068A RID: 1674
	private List<PartyManager.ChangedListener> m_changedListeners = new List<PartyManager.ChangedListener>();

	// Token: 0x020013A6 RID: 5030
	public enum PartyInviteEvent
	{
		// Token: 0x0400A757 RID: 42839
		I_CREATED_PARTY,
		// Token: 0x0400A758 RID: 42840
		I_SENT_INVITE,
		// Token: 0x0400A759 RID: 42841
		I_RESCINDED_INVITE,
		// Token: 0x0400A75A RID: 42842
		FRIEND_RECEIVED_INVITE,
		// Token: 0x0400A75B RID: 42843
		FRIEND_ACCEPTED_INVITE,
		// Token: 0x0400A75C RID: 42844
		FRIEND_DECLINED_INVITE,
		// Token: 0x0400A75D RID: 42845
		INVITE_EXPIRED,
		// Token: 0x0400A75E RID: 42846
		I_ACCEPTED_INVITE,
		// Token: 0x0400A75F RID: 42847
		I_DECLINED_INVITE,
		// Token: 0x0400A760 RID: 42848
		FRIEND_RESCINDED_INVITE,
		// Token: 0x0400A761 RID: 42849
		FRIEND_LEFT,
		// Token: 0x0400A762 RID: 42850
		LEADER_DISSOLVED_PARTY
	}

	// Token: 0x020013A7 RID: 5031
	public class PartyData
	{
		// Token: 0x0400A763 RID: 42851
		public PartyType m_type;

		// Token: 0x0400A764 RID: 42852
		public ulong m_inviteId;

		// Token: 0x0400A765 RID: 42853
		public PartyId m_partyId;

		// Token: 0x0400A766 RID: 42854
		public ScenarioDbId m_scenarioId;

		// Token: 0x0400A767 RID: 42855
		public FormatType m_format;

		// Token: 0x0400A768 RID: 42856
		public int m_season;
	}

	// Token: 0x020013A8 RID: 5032
	private class ChangedListener : global::EventListener<PartyManager.ChangedCallback>
	{
		// Token: 0x0600D81D RID: 55325 RVA: 0x003ED940 File Offset: 0x003EBB40
		public void Fire(PartyManager.PartyInviteEvent challengeEvent, BnetGameAccountId playerGameAccountId, PartyManager.PartyData challengeData)
		{
			this.m_callback(challengeEvent, playerGameAccountId, challengeData, this.m_userData);
		}
	}

	// Token: 0x020013A9 RID: 5033
	// (Invoke) Token: 0x0600D820 RID: 55328
	public delegate void ChangedCallback(PartyManager.PartyInviteEvent challengeEvent, BnetGameAccountId playerGameAccountId, PartyManager.PartyData challengeData, object userData);
}
