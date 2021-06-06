using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Assets;
using bgs;
using bgs.types;
using Blizzard.T5.Core;
using bnet.protocol;
using bnet.protocol.v2;
using Hearthstone;
using Hearthstone.Core;
using PegasusShared;
using PegasusUtil;
using SpectatorProto;
using UnityEngine;

// Token: 0x02000089 RID: 137
public class FriendChallengeMgr
{
	// Token: 0x060007B1 RID: 1969 RVA: 0x0002B828 File Offset: 0x00029A28
	public static FriendChallengeMgr Get()
	{
		if (FriendChallengeMgr.s_instance == null)
		{
			FriendChallengeMgr.s_instance = new FriendChallengeMgr();
			HearthstoneApplication.Get().WillReset += FriendChallengeMgr.s_instance.WillReset;
			AchieveManager.Get().RegisterAchievesUpdatedListener(new AchieveManager.AchievesUpdatedCallback(FriendChallengeMgr.s_instance.AchieveManager_OnAchievesUpdated), null);
			BnetParty.OnJoined += FriendChallengeMgr.s_instance.BnetParty_OnJoined;
			BnetParty.OnReceivedInvite += FriendChallengeMgr.s_instance.BnetParty_OnReceivedInvite;
			BnetParty.OnPartyAttributeChanged += FriendChallengeMgr.s_instance.BnetParty_OnPartyAttributeChanged;
			BnetParty.OnMemberEvent += FriendChallengeMgr.s_instance.BnetParty_OnMemberEvent;
			BnetParty.OnSentInvite += FriendChallengeMgr.s_instance.BnetParty_OnSentInvite;
		}
		return FriendChallengeMgr.s_instance;
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x0002B8F0 File Offset: 0x00029AF0
	public void OnLoggedIn()
	{
		NetCache.Get().RegisterFriendChallenge(new NetCache.NetCacheCallback(this.OnNetCacheReady));
		SceneMgr.Get().RegisterSceneUnloadedEvent(new SceneMgr.SceneUnloadedCallback(this.OnSceneUnloaded));
		SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
		BnetPresenceMgr.Get().AddPlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersChanged));
		BnetFriendMgr.Get().AddChangeListener(new BnetFriendMgr.ChangeCallback(this.OnFriendsChanged));
		BnetNearbyPlayerMgr.Get().AddChangeListener(new BnetNearbyPlayerMgr.ChangeCallback(this.OnNearbyPlayersChanged));
		BnetEventMgr.Get().AddChangeListener(new BnetEventMgr.ChangeCallback(this.OnBnetEventOccurred));
		GameMgr.Get().RegisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		LoginManager.Get().OnInitialClientStateReceived += this.OnReconnectLoginComplete;
		this.AddChangedListener(new FriendChallengeMgr.ChangedCallback(this.OnChallengeChanged));
		BnetPresenceMgr.Get().SetGameField(19U, BattleNet.GetVersion());
		BnetPresenceMgr.Get().SetGameField(20U, BattleNet.GetEnvironment());
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x0002BA18 File Offset: 0x00029C18
	private void BnetParty_OnJoined(OnlineEventType evt, PartyInfo party, LeaveReason? reason)
	{
		if (party.Type != PartyType.FRIENDLY_CHALLENGE)
		{
			return;
		}
		if (evt == OnlineEventType.ADDED)
		{
			if (this.DidSendChallenge() && !BnetParty.IsLeader(party.Id))
			{
				BnetParty.DissolveParty(party.Id.ToBnetEntityId());
				return;
			}
			if (this.m_data.m_partyId != null && this.m_data.m_partyId != party.Id.ToBnetEntityId())
			{
				BnetParty.DissolveParty(party.Id.ToBnetEntityId());
				return;
			}
			this.m_data.m_partyId = party.Id.ToBnetEntityId();
			long? partyAttributeLong = BnetParty.GetPartyAttributeLong(party.Id, "WTCG.Game.ScenarioId");
			if (partyAttributeLong != null)
			{
				this.m_data.m_scenarioId = (int)partyAttributeLong.Value;
			}
			long? partyAttributeLong2 = BnetParty.GetPartyAttributeLong(party.Id, "WTCG.Format.Type");
			if (partyAttributeLong2 != null)
			{
				this.m_data.m_challengeFormatType = (FormatType)partyAttributeLong2.Value;
			}
			else
			{
				this.m_data.m_challengeFormatType = FormatType.FT_UNKNOWN;
			}
			this.m_data.m_challengeBrawlType = BrawlType.BRAWL_TYPE_UNKNOWN;
			long? partyAttributeLong3 = BnetParty.GetPartyAttributeLong(party.Id, "WTCG.Brawl.Type");
			if (partyAttributeLong3 != null && partyAttributeLong3.Value >= 1L && partyAttributeLong3.Value < 3L)
			{
				this.m_data.m_challengeBrawlType = (BrawlType)partyAttributeLong3.Value;
			}
			this.m_data.m_seasonId = 0;
			long? partyAttributeLong4 = BnetParty.GetPartyAttributeLong(party.Id, "WTCG.Season.Id");
			if (partyAttributeLong4 != null)
			{
				this.m_data.m_seasonId = (int)partyAttributeLong4.Value;
			}
			this.m_data.m_brawlLibraryItemId = 0;
			long? partyAttributeLong5 = BnetParty.GetPartyAttributeLong(party.Id, "WTCG.Brawl.LibraryItemId");
			if (partyAttributeLong5 != null)
			{
				this.m_data.m_brawlLibraryItemId = (int)partyAttributeLong5.Value;
			}
			string attributeKey = this.DidSendChallenge() ? "s1" : "s2";
			BnetParty.SetPartyAttributeString(party.Id, attributeKey, "wait");
			this.UpdateMyFsgSharedSecret(party.Id, FiresideGatheringManager.Get().CurrentFsgSharedSecretKey);
			this.m_data.m_challengerDeckShareState = "none";
			this.m_data.m_challengeeDeckShareState = "none";
			this.m_data.m_sharedDecks = null;
			if (this.DidSendChallenge())
			{
				BnetParty.SendInvite(party.Id, this.m_data.m_challengee.GetHearthstoneGameAccountId(), true);
			}
			else
			{
				foreach (bnet.protocol.Attribute attribute in BnetParty.GetAllPartyAttributesVariant(party.Id))
				{
					this.BnetParty_OnPartyAttributeChanged(party, attribute.Name, attribute.Value);
				}
			}
			if (this.m_data.m_challengerDeckId != 0L)
			{
				this.SelectDeck(this.m_data.m_challengerDeckId);
			}
			if (this.m_data.m_challengerHeroId != 0L)
			{
				this.SelectHero(this.m_data.m_challengerHeroId);
				return;
			}
		}
		else if (evt == OnlineEventType.REMOVED)
		{
			if (!BnetParty.GetJoinedParties().Any((PartyInfo i) => i.Type == PartyType.FRIENDLY_CHALLENGE))
			{
				this.m_data.m_scenarioId = 2;
			}
			if (party.Id.ToBnetEntityId() == this.m_data.m_partyId)
			{
				string data = (reason != null) ? ((int)reason.Value).ToString() : "NO_SUPPLIED_REASON";
				this.PushPartyEvent(party.Id.ToBnetEntityId(), "left", data, null);
			}
		}
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x0002BD88 File Offset: 0x00029F88
	private void BnetParty_OnReceivedInvite(OnlineEventType evt, PartyInfo party, ulong inviteId, BnetGameAccountId inviter, BnetGameAccountId invitee, InviteRemoveReason? reason)
	{
		if (party.Type != PartyType.FRIENDLY_CHALLENGE)
		{
			return;
		}
		if (evt == OnlineEventType.ADDED)
		{
			if (!PartyManager.IsPartyTypeEnabledInGuardian(party.Type))
			{
				BnetParty.DeclineReceivedInvite(inviteId);
				return;
			}
			if (BnetParty.IsInParty(this.m_data.m_partyId) || this.DidSendChallenge())
			{
				BnetParty.DeclineReceivedInvite(inviteId);
				return;
			}
			BnetParty.AcceptReceivedInvite(inviteId);
		}
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x0002BDE4 File Offset: 0x00029FE4
	private void BnetParty_OnPartyAttributeChanged(PartyInfo party, string attributeKey, bnet.protocol.Variant value)
	{
		if (party.Type != PartyType.FRIENDLY_CHALLENGE)
		{
			return;
		}
		if (this.m_data.m_partyId != party.Id.ToBnetEntityId())
		{
			return;
		}
		uint num = <PrivateImplementationDetails>.ComputeStringHash(attributeKey);
		if (num <= 2266829395U)
		{
			if (num <= 1181906872U)
			{
				if (num != 563185489U)
				{
					if (num == 1181906872U)
					{
						if (attributeKey == "hero1")
						{
							this.m_data.m_challengerHeroId = (value.HasIntValue ? value.IntValue : 0L);
							this.m_data.m_challengerDeckOrHeroSelected = (this.m_data.m_challengerHeroId > 0L);
						}
					}
				}
				else if (attributeKey == "error")
				{
					this.BnetParty_OnPartyAttributeChanged_Error(party, attributeKey, value);
				}
			}
			else if (num != 1232239729U)
			{
				if (num == 2266829395U)
				{
					if (attributeKey == "d2")
					{
						this.m_data.m_challengeeDeckId = (value.HasIntValue ? value.IntValue : 0L);
						this.m_data.m_challengeeDeckOrHeroSelected = (this.m_data.m_challengeeDeckId > 0L);
					}
				}
			}
			else if (attributeKey == "hero2")
			{
				this.m_data.m_challengeeHeroId = (value.HasIntValue ? value.IntValue : 0L);
				this.m_data.m_challengeeDeckOrHeroSelected = (this.m_data.m_challengeeHeroId > 0L);
			}
		}
		else if (num <= 2524164865U)
		{
			if (num != 2283607014U)
			{
				if (num == 2524164865U)
				{
					if (attributeKey == "WTCG.Friendly.DeclineReason")
					{
						this.BnetParty_OnPartyAttributeChanged_DeclineReason(party, attributeKey, value);
					}
				}
			}
			else if (attributeKey == "d1")
			{
				this.m_data.m_challengerDeckId = (value.HasIntValue ? value.IntValue : 0L);
				this.m_data.m_challengerDeckOrHeroSelected = (this.m_data.m_challengerDeckId > 0L);
			}
		}
		else if (num != 3696775083U)
		{
			if (num == 3713552702U)
			{
				if (attributeKey == "fsg1")
				{
					this.m_data.m_challengerFsgSharedSecret = (value.HasBlobValue ? value.BlobValue : null);
				}
			}
		}
		else if (attributeKey == "fsg2")
		{
			this.m_data.m_challengeeFsgSharedSecret = (value.HasBlobValue ? value.BlobValue : null);
		}
		BnetGameAccountId bnetGameAccountId = null;
		if (this.DidSendChallenge())
		{
			if (this.m_data.m_challengee != null)
			{
				bnetGameAccountId = this.m_data.m_challengee.GetHearthstoneGameAccountId();
			}
		}
		else if (this.m_data.m_challenger != null)
		{
			bnetGameAccountId = this.m_data.m_challenger.GetHearthstoneGameAccountId();
		}
		if (bnetGameAccountId == null)
		{
			BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
			foreach (bgs.PartyMember partyMember in BnetParty.GetMembers(party.Id))
			{
				if (partyMember.GameAccountId != myGameAccountId)
				{
					bnetGameAccountId = partyMember.GameAccountId;
					break;
				}
			}
		}
		string data = value.HasStringValue ? value.StringValue : string.Empty;
		this.PushPartyEvent(party.Id.ToBnetEntityId(), attributeKey, data, bnetGameAccountId);
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x0002C148 File Offset: 0x0002A348
	private void BnetParty_OnPartyAttributeChanged_DeclineReason(PartyInfo party, string attributeKey, bnet.protocol.Variant value)
	{
		if (party.Type != PartyType.FRIENDLY_CHALLENGE)
		{
			return;
		}
		if (!this.DidSendChallenge())
		{
			return;
		}
		if (!value.HasIntValue)
		{
			return;
		}
		FriendChallengeMgr.DeclineReason declineReason = (FriendChallengeMgr.DeclineReason)value.IntValue;
		string text = null;
		switch (declineReason)
		{
		case FriendChallengeMgr.DeclineReason.NoValidDeck:
			text = "GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGEE_NO_DECK";
			break;
		case FriendChallengeMgr.DeclineReason.StandardNoValidDeck:
			text = "GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGEE_NO_STANDARD_DECK";
			break;
		case FriendChallengeMgr.DeclineReason.TavernBrawlNoValidDeck:
			text = "GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGEE_NO_TAVERN_BRAWL_DECK";
			break;
		case FriendChallengeMgr.DeclineReason.TavernBrawlNotUnlocked:
			text = "GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGEE_TAVERN_BRAWL_LOCKED";
			break;
		case FriendChallengeMgr.DeclineReason.UserIsBusy:
			text = "GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGEE_USER_IS_BUSY";
			break;
		case FriendChallengeMgr.DeclineReason.NotSeenWild:
			text = "GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGEE_NOT_SEEN_WILD";
			break;
		case FriendChallengeMgr.DeclineReason.BattlegroundsNoEarlyAccess:
			text = "GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGEE_NO_BATTLEGROUNDS_EARLY_ACCESS";
			break;
		case FriendChallengeMgr.DeclineReason.ClassicNoValidDeck:
			text = "GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGEE_NO_CLASSIC_DECK";
			break;
		}
		if (text != null)
		{
			if (this.m_challengeDialog != null)
			{
				this.m_challengeDialog.Hide();
				this.m_challengeDialog = null;
			}
			this.m_hasSeenDeclinedReason = true;
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER"),
				m_text = GameStrings.Get(text),
				m_responseDisplay = AlertPopup.ResponseDisplay.OK
			};
			DialogManager.Get().ShowPopup(info);
		}
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x0002C244 File Offset: 0x0002A444
	private void BnetParty_OnPartyAttributeChanged_Error(PartyInfo party, string attributeKey, bnet.protocol.Variant value)
	{
		if (party.Type != PartyType.FRIENDLY_CHALLENGE)
		{
			return;
		}
		if (this.DidReceiveChallenge() && value.HasIntValue)
		{
			global::Log.Party.Print(global::Log.LogLevel.Error, "BnetParty_OnPartyAttributeChanged_Error - code={0}", new object[]
			{
				value.IntValue
			});
			BnetErrorInfo info = new BnetErrorInfo(BnetFeature.Games, BnetFeatureEvent.Games_OnCreated, (BattleNetErrors)value.IntValue);
			GameMgr.Get().OnBnetError(info, null);
		}
		if (BnetParty.IsLeader(party.Id) && !value.IsNone())
		{
			BnetParty.ClearPartyAttribute(party.Id, attributeKey);
		}
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x0002C2CD File Offset: 0x0002A4CD
	private void BnetParty_OnMemberEvent(OnlineEventType evt, PartyInfo party, BnetGameAccountId memberId, bool isRolesUpdate, LeaveReason? reason)
	{
		if (party.Type != PartyType.FRIENDLY_CHALLENGE)
		{
			return;
		}
		if (evt == OnlineEventType.REMOVED && BnetParty.IsInParty(party.Id))
		{
			BnetParty.DissolveParty(party.Id);
		}
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x0002C2F8 File Offset: 0x0002A4F8
	private void BnetParty_OnSentInvite(OnlineEventType evt, PartyInfo party, ulong inviteId, BnetGameAccountId inviter, BnetGameAccountId invitee, bool senderIsMyself, InviteRemoveReason? reason)
	{
		if (party.Type != PartyType.FRIENDLY_CHALLENGE)
		{
			return;
		}
		if (evt == OnlineEventType.REMOVED)
		{
			InviteRemoveReason? inviteRemoveReason = reason;
			InviteRemoveReason inviteRemoveReason2 = InviteRemoveReason.DECLINED;
			if (inviteRemoveReason.GetValueOrDefault() == inviteRemoveReason2 & inviteRemoveReason != null)
			{
				this.DeclineFriendChallenge_Internal(party.Id.ToBnetEntityId());
				if (party.Id.ToBnetEntityId() == this.m_data.m_partyId)
				{
					BnetPlayer challengee = this.m_data.m_challengee;
					FriendlyChallengeData challengeData = this.CleanUpChallengeData(true);
					this.FireChangedEvent(FriendChallengeEvent.OPPONENT_DECLINED_CHALLENGE, challengee, challengeData);
				}
			}
		}
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x0002C378 File Offset: 0x0002A578
	private void AchieveManager_OnAchievesUpdated(List<global::Achievement> updatedAchieves, List<global::Achievement> completedAchievements, object userData)
	{
		if (!completedAchievements.Any((global::Achievement a) => a.IsFriendlyChallengeQuest))
		{
			return;
		}
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
		{
			this.m_data.m_updatePartyQuestInfoOnGameplaySceneUnload = true;
			return;
		}
		this.UpdatePartyQuestInfo();
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x0002C3D0 File Offset: 0x0002A5D0
	private void UpdatePartyQuestInfo()
	{
		if (!this.DidSendChallenge() || !BnetParty.IsInParty(this.m_data.m_partyId))
		{
			return;
		}
		byte[] value = null;
		IEnumerable<global::Achievement> source = from q in AchieveManager.Get().GetActiveQuests(false)
		where q.IsFriendlyChallengeQuest
		select q;
		if (source.Any<global::Achievement>())
		{
			PartyQuestInfo partyQuestInfo = new PartyQuestInfo();
			partyQuestInfo.QuestIds.AddRange(from q in source
			select q.ID);
			value = ProtobufUtil.ToByteArray(partyQuestInfo);
		}
		BnetParty.SetPartyAttributeBlob(this.m_data.m_partyId, "quests", value);
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x0002C48D File Offset: 0x0002A68D
	public void OnStoreOpened()
	{
		this.UpdateMyAvailability();
	}

	// Token: 0x060007BD RID: 1981 RVA: 0x0002C48D File Offset: 0x0002A68D
	public void OnStoreClosed()
	{
		this.UpdateMyAvailability();
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x0002C496 File Offset: 0x0002A696
	public BnetPlayer GetChallengee()
	{
		return this.m_data.m_challengee;
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x0002C4A3 File Offset: 0x0002A6A3
	public BnetPlayer GetChallenger()
	{
		return this.m_data.m_challenger;
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x0002C4B0 File Offset: 0x0002A6B0
	public bool DidReceiveChallenge()
	{
		return this.m_data.DidReceiveChallenge;
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x0002C4BD File Offset: 0x0002A6BD
	public bool DidSendChallenge()
	{
		return this.m_data.DidSendChallenge;
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x0002C4CA File Offset: 0x0002A6CA
	public bool HasChallenge()
	{
		return this.DidSendChallenge() || this.DidReceiveChallenge();
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x0002C4DC File Offset: 0x0002A6DC
	public bool DidChallengeeAccept()
	{
		return this.m_data.m_challengeeAccepted;
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x0002C4E9 File Offset: 0x0002A6E9
	public bool AmIInGameState()
	{
		if (this.DidSendChallenge())
		{
			return this.m_data.m_challengerInGameState;
		}
		return this.m_data.m_challengeeInGameState;
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x0002C50A File Offset: 0x0002A70A
	public BnetPlayer GetOpponent(BnetPlayer player)
	{
		if (player == this.m_data.m_challenger)
		{
			return this.m_data.m_challengee;
		}
		if (player == this.m_data.m_challengee)
		{
			return this.m_data.m_challenger;
		}
		return null;
	}

	// Token: 0x060007C6 RID: 1990 RVA: 0x0002C544 File Offset: 0x0002A744
	public BnetPlayer GetMyOpponent()
	{
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		return this.GetOpponent(myPlayer);
	}

	// Token: 0x060007C7 RID: 1991 RVA: 0x0002C564 File Offset: 0x0002A764
	public bool CanChallenge(BnetPlayer player)
	{
		if (player == null)
		{
			return false;
		}
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		return player != myPlayer && this.AmIAvailable() && this.IsOpponentAvailable(player) && !PartyManager.Get().IsPlayerInAnyParty(player.GetBestGameAccountId());
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x0002C5B4 File Offset: 0x0002A7B4
	public bool AmIAvailable()
	{
		if (!this.m_netCacheReady)
		{
			return false;
		}
		if (!this.m_myPlayerReady)
		{
			return false;
		}
		if (SpectatorManager.Get().IsSpectatingOrWatching)
		{
			return false;
		}
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		BnetGameAccount hearthstoneGameAccount = myPlayer.GetHearthstoneGameAccount();
		return !(hearthstoneGameAccount == null) && myPlayer.IsOnline() && !myPlayer.IsAppearingOffline() && Network.IsLoggedIn() && !PopupDisplayManager.Get().IsShowing && !PartyManager.Get().IsInParty() && hearthstoneGameAccount.CanBeInvitedToGame();
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x0002C640 File Offset: 0x0002A840
	public bool IsOpponentAvailable(BnetPlayer player)
	{
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		BnetGameAccount hearthstoneGameAccount = player.GetHearthstoneGameAccount();
		if (hearthstoneGameAccount == null)
		{
			return false;
		}
		if (!hearthstoneGameAccount.IsOnline())
		{
			return false;
		}
		if (!hearthstoneGameAccount.CanBeInvitedToGame())
		{
			return false;
		}
		if (HearthstoneApplication.IsPublic())
		{
			BnetGameAccount hearthstoneGameAccount2 = myPlayer.GetHearthstoneGameAccount();
			if (string.Compare(hearthstoneGameAccount.GetClientVersion(), hearthstoneGameAccount2.GetClientVersion()) != 0)
			{
				return false;
			}
			if (string.Compare(hearthstoneGameAccount.GetClientEnv(), hearthstoneGameAccount2.GetClientEnv()) != 0)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060007CA RID: 1994 RVA: 0x0002C6B7 File Offset: 0x0002A8B7
	public bool DidISelectDeckOrHero()
	{
		if (this.DidSendChallenge())
		{
			return this.m_data.m_challengerDeckOrHeroSelected;
		}
		return !this.DidReceiveChallenge() || this.m_data.m_challengeeDeckOrHeroSelected;
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x0002C6E2 File Offset: 0x0002A8E2
	public bool DidOpponentSelectDeckOrHero()
	{
		if (this.DidSendChallenge())
		{
			return this.m_data.m_challengeeDeckOrHeroSelected;
		}
		return !this.DidReceiveChallenge() || this.m_data.m_challengerDeckOrHeroSelected;
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x0002C710 File Offset: 0x0002A910
	public static void ShowChallengerNeedsToCreateTavernBrawlDeckAlert()
	{
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER"),
			m_text = GameStrings.Format("GLOBAL_FRIENDLIST_CHALLENGE_CHALLENGER_NO_TAVERN_BRAWL_DECK", Array.Empty<object>()),
			m_showAlertIcon = true,
			m_responseDisplay = AlertPopup.ResponseDisplay.OK
		};
		DialogManager.Get().ShowPopup(info);
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x0002C761 File Offset: 0x0002A961
	public void SendChallenge(BnetPlayer player, FormatType formatType, bool enableDeckShare)
	{
		if (!this.CanChallenge(player))
		{
			return;
		}
		this.SendChallenge_Internal(player, formatType, BrawlType.BRAWL_TYPE_UNKNOWN, enableDeckShare, 0, 0, false);
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x0002C77C File Offset: 0x0002A97C
	public void SendTavernBrawlChallenge(BnetPlayer player, BrawlType brawlType, int seasonId, int brawlLibraryItemId)
	{
		if (!this.CanChallenge(player))
		{
			return;
		}
		TavernBrawlManager.Get().EnsureAllDataReady(brawlType, delegate()
		{
			this.TavernBrawl_SendChallenge_OnEnsureServerDataReady(player, brawlType, seasonId, brawlLibraryItemId);
		});
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x0002C7E0 File Offset: 0x0002A9E0
	private void TavernBrawl_SendChallenge_OnEnsureServerDataReady(BnetPlayer player, BrawlType brawlType, int seasonId, int brawlLibraryItemId)
	{
		TavernBrawlManager tavernBrawlManager = TavernBrawlManager.Get();
		if (!this.CanChallenge(player))
		{
			return;
		}
		if (!tavernBrawlManager.IsTavernBrawlActive(brawlType))
		{
			return;
		}
		if (this.HasChallenge())
		{
			return;
		}
		if (!tavernBrawlManager.CanChallengeToTavernBrawl(brawlType))
		{
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER"),
				m_text = GameStrings.Format("GLOBAL_FRIENDLIST_CHALLENGE_TOOLTIP_TAVERN_BRAWL_NOT_CHALLENGEABLE", Array.Empty<object>()),
				m_showAlertIcon = true,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK
			};
			DialogManager.Get().ShowPopup(info);
			return;
		}
		if (tavernBrawlManager.GetMission(brawlType).canCreateDeck && !tavernBrawlManager.HasValidDeck(brawlType, 0))
		{
			FriendChallengeMgr.ShowChallengerNeedsToCreateTavernBrawlDeckAlert();
			return;
		}
		this.SendChallenge_Internal(player, FormatType.FT_UNKNOWN, brawlType, false, seasonId, brawlLibraryItemId, false);
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x0002C88C File Offset: 0x0002AA8C
	private void SendChallenge_Internal(BnetPlayer player, FormatType formatType, BrawlType brawlType, bool enableDeckShare, int seasonId, int brawlLibraryItemId, bool isBaconGame)
	{
		if (this.m_data.m_partyId != null)
		{
			BnetParty.DissolveParty(this.m_data.m_partyId);
		}
		this.CleanUpChallengeData(true);
		if (this.m_hasPreSelectedDeckOrHero)
		{
			this.m_data.m_challengerDeckId = this.m_preSelectedDeckId;
			this.m_data.m_challengerHeroId = this.m_preSelectedHeroId;
			this.m_data.m_challengerDeckOrHeroSelected = this.m_hasPreSelectedDeckOrHero;
		}
		this.m_data.m_challenger = BnetPresenceMgr.Get().GetMyPlayer();
		this.m_data.m_challengerId = this.m_data.m_challenger.GetHearthstoneGameAccount().GetId();
		this.m_data.m_challengee = player;
		this.m_hasSeenDeclinedReason = false;
		this.m_data.m_scenarioId = 2;
		this.m_data.m_seasonId = seasonId;
		this.m_data.m_brawlLibraryItemId = brawlLibraryItemId;
		this.m_data.m_challengeBrawlType = brawlType;
		this.m_data.m_challengeFormatType = formatType;
		if (isBaconGame)
		{
			this.m_data.m_scenarioId = 3459;
		}
		else if (this.IsChallengeTavernBrawl())
		{
			TavernBrawlManager.Get().CurrentBrawlType = this.m_data.m_challengeBrawlType;
			TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(brawlType);
			mission.SetSelectedBrawlLibraryItemId(brawlLibraryItemId);
			this.m_data.m_scenarioId = mission.missionId;
			this.m_data.m_challengeFormatType = mission.formatType;
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				Global.PresenceStatus.TAVERN_BRAWL_FRIENDLY_WAITING
			});
		}
		bnet.protocol.Attribute attributeV = null;
		IEnumerable<global::Achievement> source = from q in AchieveManager.Get().GetActiveQuests(false)
		where q.IsFriendlyChallengeQuest
		select q;
		if (source.Any<global::Achievement>())
		{
			PartyQuestInfo partyQuestInfo = new PartyQuestInfo();
			partyQuestInfo.QuestIds.AddRange(from q in source
			select q.ID);
			byte[] val = ProtobufUtil.ToByteArray(partyQuestInfo);
			attributeV = ProtocolHelper.CreateAttribute("quests", val);
		}
		bnet.protocol.Attribute attributeV2 = ProtocolHelper.CreateAttribute("WTCG.Game.ScenarioId", (long)this.m_data.m_scenarioId);
		bnet.protocol.Attribute attributeV3 = ProtocolHelper.CreateAttribute("WTCG.Format.Type", (long)this.m_data.m_challengeFormatType);
		bnet.protocol.Attribute attributeV4 = this.IsChallengeTavernBrawl() ? ProtocolHelper.CreateAttribute("WTCG.Brawl.Type", (long)this.m_data.m_challengeBrawlType) : null;
		bnet.protocol.Attribute attributeV5 = ProtocolHelper.CreateAttribute("WTCG.Season.Id", (long)this.m_data.m_seasonId);
		bnet.protocol.Attribute attributeV6 = this.IsChallengeTavernBrawl() ? ProtocolHelper.CreateAttribute("WTCG.Brawl.LibraryItemId", (long)this.m_data.m_brawlLibraryItemId) : null;
		bnet.protocol.Attribute attributeV7 = null;
		if (FiresideGatheringManager.Get().IsCheckedIn && FiresideGatheringManager.Get().CurrentFsgSharedSecretKey != null)
		{
			byte[] val2 = SHA256.Create().ComputeHash(FiresideGatheringManager.Get().CurrentFsgSharedSecretKey, 0, FiresideGatheringManager.Get().CurrentFsgSharedSecretKey.Length);
			attributeV7 = ProtocolHelper.CreateAttribute("fsg1", val2);
		}
		bnet.protocol.Attribute attributeV8 = (this.m_data.m_challengerDeckId != 0L) ? ProtocolHelper.CreateAttribute("d1", this.m_data.m_challengerDeckId) : null;
		bnet.protocol.Attribute attributeV9 = (this.m_data.m_challengerHeroId != 0L) ? ProtocolHelper.CreateAttribute("hero1", this.m_data.m_challengerHeroId) : null;
		bnet.protocol.Attribute attributeV10 = this.m_data.m_challengerDeckOrHeroSelected ? ProtocolHelper.CreateAttribute("s1", "ready") : null;
		string val3 = enableDeckShare ? "deckShareEnabled" : "deckShareDisabled";
		bnet.protocol.Attribute attributeV11 = ProtocolHelper.CreateAttribute("isDeckShareEnabled", val3);
		bnet.protocol.Attribute attributeV12 = ProtocolHelper.CreateAttribute("p1DeckShareState", "none");
		bnet.protocol.Attribute attributeV13 = ProtocolHelper.CreateAttribute("p2DeckShareState", "none");
		BnetParty.CreateParty(PartyType.FRIENDLY_CHALLENGE, PrivacyLevel.OPEN_INVITATION, null, new bnet.protocol.v2.Attribute[]
		{
			ProtocolHelper.V1AttributeToV2Attribute(attributeV2),
			ProtocolHelper.V1AttributeToV2Attribute(attributeV3),
			ProtocolHelper.V1AttributeToV2Attribute(attributeV),
			ProtocolHelper.V1AttributeToV2Attribute(attributeV4),
			ProtocolHelper.V1AttributeToV2Attribute(attributeV5),
			ProtocolHelper.V1AttributeToV2Attribute(attributeV6),
			ProtocolHelper.V1AttributeToV2Attribute(attributeV8),
			ProtocolHelper.V1AttributeToV2Attribute(attributeV9),
			ProtocolHelper.V1AttributeToV2Attribute(attributeV10),
			ProtocolHelper.V1AttributeToV2Attribute(attributeV7),
			ProtocolHelper.V1AttributeToV2Attribute(attributeV11),
			ProtocolHelper.V1AttributeToV2Attribute(attributeV12),
			ProtocolHelper.V1AttributeToV2Attribute(attributeV13)
		});
		this.UpdateMyAvailability();
		this.FireChangedEvent(FriendChallengeEvent.I_SENT_CHALLENGE, player, null);
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x0002CCC0 File Offset: 0x0002AEC0
	public void CancelChallenge()
	{
		if (!this.HasChallenge())
		{
			return;
		}
		if (this.DidSendChallenge())
		{
			this.RescindChallenge();
			return;
		}
		if (this.DidReceiveChallenge())
		{
			this.DeclineChallenge();
		}
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x0002CCE8 File Offset: 0x0002AEE8
	public void AcceptChallenge()
	{
		if (!this.DidReceiveChallenge())
		{
			return;
		}
		this.m_data.m_challengeeAccepted = true;
		string attributeKey = this.DidSendChallenge() ? "s1" : "s2";
		BnetParty.SetPartyAttributeString(this.m_data.m_partyId, attributeKey, "deck");
		this.FireChangedEvent(FriendChallengeEvent.I_ACCEPTED_CHALLENGE, this.m_data.m_challenger, null);
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x0002CD50 File Offset: 0x0002AF50
	public void DeclineChallenge()
	{
		if (!this.DidReceiveChallenge())
		{
			return;
		}
		this.RevertTavernBrawlPresenceStatus();
		this.DeclineFriendChallenge_Internal(this.m_data.m_partyId);
		BnetPlayer challenger = this.m_data.m_challenger;
		FriendlyChallengeData challengeData = this.CleanUpChallengeData(true);
		this.FireChangedEvent(FriendChallengeEvent.I_DECLINED_CHALLENGE, challenger, challengeData);
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x0002CD9B File Offset: 0x0002AF9B
	private void DeclineFriendChallenge_Internal(BnetEntityId partyId)
	{
		if (!BnetParty.IsInParty(partyId))
		{
			return;
		}
		BnetParty.DissolveParty(partyId);
	}

	// Token: 0x060007D5 RID: 2005 RVA: 0x0002CDB8 File Offset: 0x0002AFB8
	public void QueueCanceled()
	{
		BnetPlayer player;
		if (this.DidReceiveChallenge())
		{
			player = this.m_data.m_challenger;
		}
		else
		{
			if (!this.DidSendChallenge())
			{
				return;
			}
			player = this.m_data.m_challengee;
		}
		FriendlyChallengeData challengeData = this.CleanUpChallengeData(true);
		this.FireChangedEvent(FriendChallengeEvent.QUEUE_CANCELED, player, challengeData);
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x0002CE04 File Offset: 0x0002B004
	private void PushPartyEvent(BnetEntityId partyId, string type, string data, BnetGameAccountId otherPlayerGameAccountId = null)
	{
		if (otherPlayerGameAccountId == null)
		{
			BnetPlayer bnetPlayer = this.DidSendChallenge() ? this.m_data.m_challenger : this.m_data.m_challengee;
			otherPlayerGameAccountId = ((bnetPlayer == null) ? null : bnetPlayer.GetHearthstoneGameAccountId());
		}
		PartyEvent partyEvent = default(PartyEvent);
		partyEvent.partyId.hi = partyId.GetHi();
		partyEvent.partyId.lo = partyId.GetLo();
		partyEvent.eventName = type;
		partyEvent.eventData = data;
		if (otherPlayerGameAccountId != null)
		{
			partyEvent.otherMemberId.hi = otherPlayerGameAccountId.GetHi();
			partyEvent.otherMemberId.lo = otherPlayerGameAccountId.GetLo();
		}
		this.OnPartyUpdate(new PartyEvent[]
		{
			partyEvent
		});
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x0002CECC File Offset: 0x0002B0CC
	public void RescindChallenge()
	{
		if (!this.DidSendChallenge())
		{
			return;
		}
		this.RevertTavernBrawlPresenceStatus();
		if (BnetParty.IsInParty(this.m_data.m_partyId))
		{
			BnetParty.DissolveParty(this.m_data.m_partyId);
		}
		BnetPlayer challengee = this.m_data.m_challengee;
		FriendlyChallengeData challengeData = this.CleanUpChallengeData(true);
		this.FireChangedEvent(FriendChallengeEvent.I_RESCINDED_CHALLENGE, challengee, challengeData);
	}

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0002CF32 File Offset: 0x0002B132
	public bool IsChallengeFriendlyDuel
	{
		get
		{
			return this.IsChallengeStandardDuel() || this.IsChallengeWildDuel() || this.IsChallengeClassicDuel();
		}
	}

	// Token: 0x060007D9 RID: 2009 RVA: 0x0002CF4C File Offset: 0x0002B14C
	public bool IsChallengeStandardDuel()
	{
		return this.HasChallenge() && !this.IsChallengeTavernBrawl() && this.m_data.m_challengeFormatType == FormatType.FT_STANDARD;
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x0002CF70 File Offset: 0x0002B170
	public bool IsChallengeWildDuel()
	{
		return this.HasChallenge() && !this.IsChallengeTavernBrawl() && this.m_data.m_challengeFormatType == FormatType.FT_WILD;
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x0002CF94 File Offset: 0x0002B194
	public bool IsChallengeClassicDuel()
	{
		return this.HasChallenge() && !this.IsChallengeTavernBrawl() && this.m_data.m_challengeFormatType == FormatType.FT_CLASSIC;
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x0002CFB8 File Offset: 0x0002B1B8
	public bool IsChallengeTavernBrawl()
	{
		return this.HasChallenge() && this.m_data.m_challengeBrawlType > BrawlType.BRAWL_TYPE_UNKNOWN;
	}

	// Token: 0x060007DD RID: 2013 RVA: 0x0002CFD2 File Offset: 0x0002B1D2
	public bool IsChallengeFiresideBrawl()
	{
		return this.IsChallengeTavernBrawl() && this.m_data.m_challengeBrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING;
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x0002CFEC File Offset: 0x0002B1EC
	public bool IsChallengeBacon()
	{
		return this.HasChallenge() && this.m_data.m_scenarioId == 3459;
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x0002D00A File Offset: 0x0002B20A
	public BrawlType GetChallengeBrawlType()
	{
		if (!this.HasChallenge())
		{
			return BrawlType.BRAWL_TYPE_UNKNOWN;
		}
		return this.m_data.m_challengeBrawlType;
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x0002D021 File Offset: 0x0002B221
	public bool IsDeckShareEnabled()
	{
		return this.HasChallenge() && BnetParty.GetPartyAttributeString(this.m_data.m_partyId, "isDeckShareEnabled") == "deckShareEnabled";
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x0002D054 File Offset: 0x0002B254
	public void RequestDeckShare()
	{
		if (this.DidSendChallenge())
		{
			if (BnetParty.GetPartyAttributeString(this.m_data.m_partyId, "p1DeckShareState") == "sharingUnused")
			{
				BnetParty.SetPartyAttributeString(this.m_data.m_partyId, "p1DeckShareState", "sharing");
				return;
			}
			if (BnetParty.GetPartyAttributeString(this.m_data.m_partyId, "p1DeckShareState") == "none")
			{
				BnetParty.SetPartyAttributeString(this.m_data.m_partyId, "p1DeckShareState", "requested");
				return;
			}
		}
		else if (this.DidReceiveChallenge())
		{
			if (BnetParty.GetPartyAttributeString(this.m_data.m_partyId, "p2DeckShareState") == "sharingUnused")
			{
				BnetParty.SetPartyAttributeString(this.m_data.m_partyId, "p2DeckShareState", "sharing");
				return;
			}
			if (BnetParty.GetPartyAttributeString(this.m_data.m_partyId, "p2DeckShareState") == "none")
			{
				BnetParty.SetPartyAttributeString(this.m_data.m_partyId, "p2DeckShareState", "requested");
			}
		}
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x0002D194 File Offset: 0x0002B394
	public void EndDeckShare()
	{
		if (this.DidSendChallenge())
		{
			if (BnetParty.GetPartyAttributeString(this.m_data.m_partyId, "p1DeckShareState") == "sharing")
			{
				BnetParty.SetPartyAttributeString(this.m_data.m_partyId, "p1DeckShareState", "sharingUnused");
				return;
			}
		}
		else if (this.DidReceiveChallenge() && BnetParty.GetPartyAttributeString(this.m_data.m_partyId, "p2DeckShareState") == "sharing")
		{
			BnetParty.SetPartyAttributeString(this.m_data.m_partyId, "p2DeckShareState", "sharingUnused");
		}
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x0002D23C File Offset: 0x0002B43C
	private void ShareDecks_InternalParty()
	{
		List<CollectionDeck> decks = CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK);
		byte[] array = this.SerializeSharedDecks(decks);
		if (array == null)
		{
			global::Log.Party.PrintError("{0}.ShareDecks_InternalParty(): Unable to Serialize decks!.", new object[]
			{
				this
			});
			if (this.DidSendChallenge())
			{
				BnetParty.SetPartyAttributeString(this.m_data.m_partyId, "p2DeckShareState", "error");
				return;
			}
			if (this.DidReceiveChallenge())
			{
				BnetParty.SetPartyAttributeString(this.m_data.m_partyId, "p1DeckShareState", "error");
				return;
			}
		}
		else
		{
			if (this.DidSendChallenge())
			{
				BnetParty.SetPartyAttributeBlob(this.m_data.m_partyId, "p1DeckShareDecks", array);
				return;
			}
			if (this.DidReceiveChallenge())
			{
				BnetParty.SetPartyAttributeBlob(this.m_data.m_partyId, "p2DeckShareDecks", array);
			}
		}
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x0002D310 File Offset: 0x0002B510
	public List<CollectionDeck> GetSharedDecks()
	{
		if (this.m_data.m_sharedDecks != null)
		{
			return new List<CollectionDeck>(this.m_data.m_sharedDecks);
		}
		byte[] array = null;
		if (this.DidSendChallenge() && (this.m_data.m_challengerDeckShareState == "sharing" || this.m_data.m_challengerDeckShareState == "sharingUnused"))
		{
			array = BnetParty.GetPartyAttributeBlob(this.m_data.m_partyId, "p2DeckShareDecks");
		}
		else if (this.DidReceiveChallenge() && (this.m_data.m_challengeeDeckShareState == "sharing" || this.m_data.m_challengeeDeckShareState == "sharingUnused"))
		{
			array = BnetParty.GetPartyAttributeBlob(this.m_data.m_partyId, "p1DeckShareDecks");
		}
		if (array == null)
		{
			return null;
		}
		return this.DeserializeSharedDecks(array);
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x0002D3F0 File Offset: 0x0002B5F0
	private byte[] SerializeSharedDecks(List<CollectionDeck> collectionDecks)
	{
		if (collectionDecks == null || collectionDecks.Count <= 0)
		{
			return null;
		}
		DeckList deckList = new DeckList();
		FormatType formatType = Options.GetFormatType();
		foreach (CollectionDeck collectionDeck in collectionDecks)
		{
			if (collectionDeck.IsValidForRuleset && collectionDeck.IsValidForFormat(formatType))
			{
				ulong num = 0UL;
				if (collectionDeck.NeedsName)
				{
					num |= 512UL;
				}
				if (formatType == FormatType.FT_STANDARD)
				{
					num |= 128UL;
				}
				if (collectionDeck.Locked)
				{
					num |= 1024UL;
				}
				DeckInfo deckInfo = new DeckInfo
				{
					Id = collectionDeck.ID,
					Name = collectionDeck.Name,
					Hero = GameUtils.TranslateCardIdToDbId(collectionDeck.HeroCardID, false),
					HeroPremium = (int)collectionDeck.HeroPremium,
					DeckType = collectionDeck.Type,
					CardBack = collectionDeck.CardBackID,
					CardBackOverride = collectionDeck.CardBackOverridden,
					HeroOverride = collectionDeck.HeroOverridden,
					SeasonId = collectionDeck.SeasonId,
					BrawlLibraryItemId = collectionDeck.BrawlLibraryItemId,
					SortOrder = collectionDeck.SortOrder,
					FormatType = collectionDeck.FormatType,
					SourceType = collectionDeck.SourceType,
					Validity = num
				};
				if (collectionDeck.HasUIHeroOverride())
				{
					deckInfo.UiHeroOverride = GameUtils.TranslateCardIdToDbId(collectionDeck.UIHeroOverrideCardID, false);
					deckInfo.UiHeroOverridePremium = (int)collectionDeck.UIHeroOverridePremium;
				}
				deckList.Decks.Add(deckInfo);
			}
		}
		return ProtobufUtil.ToByteArray(deckList);
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x0002D5A0 File Offset: 0x0002B7A0
	private List<CollectionDeck> DeserializeSharedDecks(byte[] blob)
	{
		if (blob == null)
		{
			return null;
		}
		try
		{
			DeckList deckList = ProtobufUtil.ParseFrom<DeckList>(blob, 0, -1);
			this.m_data.m_sharedDecks = new List<CollectionDeck>();
			foreach (DeckInfo deckInfo in deckList.Decks)
			{
				CollectionDeck collectionDeck = new CollectionDeck
				{
					ID = deckInfo.Id,
					Name = deckInfo.Name,
					HeroCardID = GameUtils.TranslateDbIdToCardId(deckInfo.Hero, false),
					HeroPremium = (TAG_PREMIUM)deckInfo.HeroPremium,
					Type = deckInfo.DeckType,
					CardBackID = deckInfo.CardBack,
					CardBackOverridden = deckInfo.CardBackOverride,
					HeroOverridden = deckInfo.HeroOverride,
					SeasonId = deckInfo.SeasonId,
					BrawlLibraryItemId = deckInfo.BrawlLibraryItemId,
					NeedsName = Network.DeckNeedsName(deckInfo.Validity),
					SortOrder = (deckInfo.HasSortOrder ? deckInfo.SortOrder : deckInfo.Id),
					FormatType = deckInfo.FormatType,
					SourceType = (deckInfo.HasSourceType ? deckInfo.SourceType : DeckSourceType.DECK_SOURCE_TYPE_UNKNOWN),
					Locked = Network.AreDeckFlagsLocked(deckInfo.Validity),
					IsShared = true
				};
				if (deckInfo.HasUiHeroOverride)
				{
					collectionDeck.UIHeroOverrideCardID = GameUtils.TranslateDbIdToCardId(deckInfo.UiHeroOverride, false);
					collectionDeck.UIHeroOverridePremium = (TAG_PREMIUM)deckInfo.UiHeroOverridePremium;
				}
				this.m_data.m_sharedDecks.Add(collectionDeck);
			}
		}
		catch
		{
			global::Log.Party.PrintError("{0}.ShareDecks_InternalParty(): Unable to Deserialize decks!.", new object[]
			{
				this
			});
			this.m_data.m_sharedDecks = null;
		}
		return this.m_data.m_sharedDecks;
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x0002D78C File Offset: 0x0002B98C
	public bool HasOpponentSharedDecks()
	{
		return this.GetSharedDecks() != null;
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x0002D798 File Offset: 0x0002B998
	public bool ShouldUseSharedDecks()
	{
		return this.HasOpponentSharedDecks() && (!this.DidSendChallenge() || !(this.m_data.m_challengerDeckShareState != "sharing")) && (!this.DidReceiveChallenge() || !(this.m_data.m_challengeeDeckShareState != "sharing"));
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x0002D7F4 File Offset: 0x0002B9F4
	private void OnFriendChallengeDeckShareRequestDialogWaitingResponse(AlertPopup.Response response, object userData)
	{
		if (response != AlertPopup.Response.CANCEL)
		{
			return;
		}
		if (this.DidSendChallenge())
		{
			if (BnetParty.GetPartyAttributeString(this.m_data.m_partyId, "p1DeckShareState") == "requested")
			{
				BnetParty.SetPartyAttributeString(this.m_data.m_partyId, "p1DeckShareState", "none");
				return;
			}
		}
		else if (this.DidReceiveChallenge() && BnetParty.GetPartyAttributeString(this.m_data.m_partyId, "p2DeckShareState") == "requested")
		{
			BnetParty.SetPartyAttributeString(this.m_data.m_partyId, "p2DeckShareState", "none");
		}
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x0002D8A4 File Offset: 0x0002BAA4
	private void OnFriendChallengeDeckShareRequestDialogResponse(AlertPopup.Response response, object userData)
	{
		string value = (response == AlertPopup.Response.CANCEL) ? "declined" : "sharing";
		if (this.DidSendChallenge())
		{
			if (BnetParty.GetPartyAttributeString(this.m_data.m_partyId, "p2DeckShareState") == "requested")
			{
				BnetParty.SetPartyAttributeString(this.m_data.m_partyId, "p2DeckShareState", value);
			}
			if (BnetParty.GetPartyAttributeString(this.m_data.m_partyId, "p1DeckShareState") == "requested")
			{
				FriendlyChallengeHelper.Get().ShowDeckShareRequestWaitingDialog(new AlertPopup.ResponseCallback(this.OnFriendChallengeDeckShareRequestDialogWaitingResponse));
				return;
			}
		}
		else if (this.DidReceiveChallenge())
		{
			if (BnetParty.GetPartyAttributeString(this.m_data.m_partyId, "p1DeckShareState") == "requested")
			{
				BnetParty.SetPartyAttributeString(this.m_data.m_partyId, "p1DeckShareState", value);
			}
			if (BnetParty.GetPartyAttributeString(this.m_data.m_partyId, "p2DeckShareState") == "requested")
			{
				FriendlyChallengeHelper.Get().ShowDeckShareRequestWaitingDialog(new AlertPopup.ResponseCallback(this.OnFriendChallengeDeckShareRequestDialogWaitingResponse));
			}
		}
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x0002D9D4 File Offset: 0x0002BBD4
	private DeckShareState GetDeckShareStateEnumFromAttribute(string deckShareStateAttribute)
	{
		DeckShareState result = DeckShareState.NO_DECK_SHARE;
		if (deckShareStateAttribute == "sharingUnused")
		{
			result = DeckShareState.DECK_SHARED_UNUSED;
		}
		else if (deckShareStateAttribute == "sharing")
		{
			result = DeckShareState.USING_SHARED_DECK;
		}
		return result;
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x0002DA04 File Offset: 0x0002BC04
	public void SkipDeckSelection()
	{
		this.SelectDeck(1L);
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x0002DA10 File Offset: 0x0002BC10
	public void SelectDeck(long deckId)
	{
		if (this.DidSendChallenge())
		{
			this.m_data.m_challengerDeckOrHeroSelected = true;
		}
		else
		{
			if (!this.DidReceiveChallenge())
			{
				return;
			}
			this.m_data.m_challengeeDeckOrHeroSelected = true;
		}
		this.SelectMyDeck_InternalParty(deckId);
		this.FireChangedEvent(FriendChallengeEvent.SELECTED_DECK_OR_HERO, BnetPresenceMgr.Get().GetMyPlayer(), null);
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x0002DA63 File Offset: 0x0002BC63
	public void SelectDeckBeforeSendingChallenge(long deckId)
	{
		this.m_hasPreSelectedDeckOrHero = true;
		this.m_preSelectedDeckId = deckId;
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x0002DA73 File Offset: 0x0002BC73
	public void ClearSelectedDeckAndHeroBeforeSendingChallenge()
	{
		this.m_hasPreSelectedDeckOrHero = false;
		this.m_preSelectedDeckId = 0L;
		this.m_preSelectedHeroId = 0L;
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x0002DA8C File Offset: 0x0002BC8C
	public void SelectHero(long heroCardDbId)
	{
		if (this.DidSendChallenge())
		{
			this.m_data.m_challengerDeckOrHeroSelected = true;
		}
		else
		{
			if (!this.DidReceiveChallenge())
			{
				return;
			}
			this.m_data.m_challengeeDeckOrHeroSelected = true;
		}
		this.SelectMyHero_InternalParty(heroCardDbId);
		this.FireChangedEvent(FriendChallengeEvent.SELECTED_DECK_OR_HERO, BnetPresenceMgr.Get().GetMyPlayer(), null);
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x0002DADF File Offset: 0x0002BCDF
	public void SelectHeroBeforeSendingChallenge(long heroCardDbId)
	{
		this.m_hasPreSelectedDeckOrHero = true;
		this.m_preSelectedHeroId = heroCardDbId;
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x0002DAF0 File Offset: 0x0002BCF0
	public void DeselectDeckOrHero()
	{
		if (this.m_hasPreSelectedDeckOrHero)
		{
			this.m_hasPreSelectedDeckOrHero = false;
			this.m_preSelectedDeckId = 0L;
			this.m_preSelectedHeroId = 0L;
		}
		if (this.DidSendChallenge() && this.m_data.m_challengerDeckOrHeroSelected)
		{
			this.m_data.m_challengerDeckOrHeroSelected = false;
			this.m_data.m_challengerDeckId = 0L;
			this.m_data.m_challengerHeroId = 0L;
			this.m_data.m_challengerInGameState = false;
		}
		else
		{
			if (!this.DidReceiveChallenge() || !this.m_data.m_challengeeDeckOrHeroSelected)
			{
				return;
			}
			this.m_data.m_challengeeDeckOrHeroSelected = false;
			this.m_data.m_challengeeDeckId = 0L;
			this.m_data.m_challengeeHeroId = 0L;
			this.m_data.m_challengeeInGameState = false;
		}
		this.SelectMyDeck_InternalParty(0L);
		this.SelectMyHero_InternalParty(0L);
		this.FireChangedEvent(FriendChallengeEvent.DESELECTED_DECK_OR_HERO, BnetPresenceMgr.Get().GetMyPlayer(), null);
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x0002DBD2 File Offset: 0x0002BDD2
	public void SetChallengeMethod(FriendChallengeMgr.ChallengeMethod challengeMethod)
	{
		this.m_challengeMethod = challengeMethod;
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x0002DBDB File Offset: 0x0002BDDB
	private bool ShouldTransitionToFriendlySceneAccordingToChallengeMethod()
	{
		return this.m_challengeMethod != FriendChallengeMgr.ChallengeMethod.FROM_FIRESIDE_GATHERING_OPPONENT_PICKER;
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x0002DBEC File Offset: 0x0002BDEC
	private void SelectMyDeck_InternalParty(long deckId)
	{
		string val = (deckId == 0L) ? "deck" : "ready";
		bnet.protocol.Attribute[] attributeV;
		if (this.DidSendChallenge())
		{
			this.m_data.m_challengerDeckId = deckId;
			attributeV = new bnet.protocol.Attribute[]
			{
				ProtocolHelper.CreateAttribute("s1", val),
				ProtocolHelper.CreateAttribute("d1", deckId)
			};
		}
		else
		{
			this.m_data.m_challengeeDeckId = deckId;
			attributeV = new bnet.protocol.Attribute[]
			{
				ProtocolHelper.CreateAttribute("s2", val),
				ProtocolHelper.CreateAttribute("d2", deckId)
			};
		}
		BnetParty.SetPartyAttributes(this.m_data.m_partyId, ProtocolHelper.V1AttributeToV2Attribute(attributeV));
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x0002DC8C File Offset: 0x0002BE8C
	private void SelectMyHero_InternalParty(long heroCardDbId)
	{
		string val = (heroCardDbId == 0L) ? "deck" : "ready";
		bnet.protocol.Attribute[] attributeV;
		if (this.DidSendChallenge())
		{
			this.m_data.m_challengerHeroId = heroCardDbId;
			attributeV = new bnet.protocol.Attribute[]
			{
				ProtocolHelper.CreateAttribute("s1", val),
				ProtocolHelper.CreateAttribute("hero1", heroCardDbId)
			};
		}
		else
		{
			this.m_data.m_challengeeHeroId = heroCardDbId;
			attributeV = new bnet.protocol.Attribute[]
			{
				ProtocolHelper.CreateAttribute("s2", val),
				ProtocolHelper.CreateAttribute("hero2", heroCardDbId)
			};
		}
		BnetParty.SetPartyAttributes(this.m_data.m_partyId, ProtocolHelper.V1AttributeToV2Attribute(attributeV));
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x0002DD2C File Offset: 0x0002BF2C
	private void SetMyFsgSharedSecretKey_InternalParty(PartyId partyId, byte[] fsgSharedSecretKey)
	{
		bnet.protocol.Attribute[] attributeV;
		if (this.DidSendChallenge())
		{
			this.m_data.m_challengerFsgSharedSecret = fsgSharedSecretKey;
			attributeV = new bnet.protocol.Attribute[]
			{
				ProtocolHelper.CreateAttribute("fsg1", fsgSharedSecretKey)
			};
		}
		else
		{
			this.m_data.m_challengeeFsgSharedSecret = fsgSharedSecretKey;
			attributeV = new bnet.protocol.Attribute[]
			{
				ProtocolHelper.CreateAttribute("fsg2", fsgSharedSecretKey)
			};
		}
		BnetParty.SetPartyAttributes(partyId, ProtocolHelper.V1AttributeToV2Attribute(attributeV));
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x0002DD91 File Offset: 0x0002BF91
	public int GetScenarioId()
	{
		return this.m_data.m_scenarioId;
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x0002DD9E File Offset: 0x0002BF9E
	public FormatType GetFormatType()
	{
		return this.m_data.m_challengeFormatType;
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x0002DDAC File Offset: 0x0002BFAC
	public PartyQuestInfo GetPartyQuestInfo()
	{
		PartyQuestInfo result = null;
		byte[] partyAttributeBlob = BnetParty.GetPartyAttributeBlob(this.m_data.m_partyId, "quests");
		if (partyAttributeBlob != null && partyAttributeBlob.Length != 0)
		{
			result = ProtobufUtil.ParseFrom<PartyQuestInfo>(partyAttributeBlob, 0, -1);
		}
		return result;
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x0002DDE7 File Offset: 0x0002BFE7
	public bool PlayersInSameFiresideGathering()
	{
		return this.m_data.m_challengerFsgSharedSecret != null && this.m_data.m_challengeeFsgSharedSecret != null && GeneralUtils.AreArraysEqual<byte>(this.m_data.m_challengerFsgSharedSecret, this.m_data.m_challengeeFsgSharedSecret);
	}

	// Token: 0x060007FC RID: 2044 RVA: 0x0002DE20 File Offset: 0x0002C020
	public void UpdateMyFsgSharedSecret(byte[] currentFsgSharedSecretKey)
	{
		this.UpdateMyFsgSharedSecret(this.m_data.m_partyId, currentFsgSharedSecretKey);
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x0002DE3C File Offset: 0x0002C03C
	public void UpdateMyFsgSharedSecret(PartyId partyId, byte[] currentFsgSharedSecretKey)
	{
		if (partyId == null)
		{
			return;
		}
		if (!FiresideGatheringManager.Get().IsCheckedIn || currentFsgSharedSecretKey == null)
		{
			this.SetMyFsgSharedSecretKey_InternalParty(partyId, null);
			return;
		}
		byte[] fsgSharedSecretKey = SHA256.Create().ComputeHash(currentFsgSharedSecretKey, 0, currentFsgSharedSecretKey.Length);
		this.SetMyFsgSharedSecretKey_InternalParty(partyId, fsgSharedSecretKey);
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x0002DE83 File Offset: 0x0002C083
	public bool AddChangedListener(FriendChallengeMgr.ChangedCallback callback)
	{
		return this.AddChangedListener(callback, null);
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x0002DE90 File Offset: 0x0002C090
	public bool AddChangedListener(FriendChallengeMgr.ChangedCallback callback, object userData)
	{
		FriendChallengeMgr.ChangedListener changedListener = new FriendChallengeMgr.ChangedListener();
		changedListener.SetCallback(callback);
		changedListener.SetUserData(userData);
		if (this.m_changedListeners.Contains(changedListener))
		{
			return false;
		}
		this.m_changedListeners.Add(changedListener);
		return true;
	}

	// Token: 0x06000800 RID: 2048 RVA: 0x0002DECE File Offset: 0x0002C0CE
	public bool RemoveChangedListener(FriendChallengeMgr.ChangedCallback callback)
	{
		return this.RemoveChangedListener(callback, null);
	}

	// Token: 0x06000801 RID: 2049 RVA: 0x0002DED8 File Offset: 0x0002C0D8
	private bool RemoveChangedListener(FriendChallengeMgr.ChangedCallback callback, object userData)
	{
		FriendChallengeMgr.ChangedListener changedListener = new FriendChallengeMgr.ChangedListener();
		changedListener.SetCallback(callback);
		changedListener.SetUserData(userData);
		return this.m_changedListeners.Remove(changedListener);
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x0002DF05 File Offset: 0x0002C105
	public static bool RemoveChangedListenerFromInstance(FriendChallengeMgr.ChangedCallback callback, object userData = null)
	{
		return FriendChallengeMgr.s_instance != null && FriendChallengeMgr.s_instance.RemoveChangedListener(callback, userData);
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x0002DF1C File Offset: 0x0002C11C
	private void OnPartyUpdate(PartyEvent[] updates)
	{
		foreach (PartyEvent partyEvent in updates)
		{
			BnetEntityId partyId = BnetEntityId.CreateFromEntityId(partyEvent.partyId);
			BnetGameAccountId otherMemberId = BnetGameAccountId.CreateFromEntityId(partyEvent.otherMemberId);
			if (partyEvent.eventName == "s1")
			{
				if (partyEvent.eventData == "wait")
				{
					this.OnPartyUpdate_CreatedParty(partyId, otherMemberId);
				}
				else if (partyEvent.eventData == "deck")
				{
					if (this.DidReceiveChallenge() && this.m_data.m_challengerDeckOrHeroSelected)
					{
						this.m_data.m_challengerDeckOrHeroSelected = false;
						this.m_data.m_challengerInGameState = false;
						this.FireChangedEvent(FriendChallengeEvent.DESELECTED_DECK_OR_HERO, this.m_data.m_challenger, null);
					}
				}
				else if (partyEvent.eventData == "ready")
				{
					if (this.DidReceiveChallenge())
					{
						this.m_data.m_challengerDeckOrHeroSelected = true;
						this.FireChangedEvent(FriendChallengeEvent.SELECTED_DECK_OR_HERO, this.m_data.m_challenger, null);
						this.SetIAmInGameState();
					}
				}
				else if (partyEvent.eventData == "game")
				{
					if (this.DidReceiveChallenge())
					{
						this.m_data.m_challengerInGameState = true;
						this.SetIAmInGameState();
						this.StartFriendlyChallengeGameIfReady();
						FriendlyChallengeHelper.Get().WaitForFriendChallengeToStart();
						this.m_data.m_findGameErrorOccurred = false;
					}
				}
				else if (partyEvent.eventData == "goto")
				{
					this.m_data.m_challengerDeckOrHeroSelected = false;
					this.m_data.m_challengerInGameState = false;
				}
			}
			else if (partyEvent.eventName == "s2")
			{
				if (partyEvent.eventData == "wait")
				{
					this.OnPartyUpdate_JoinedParty(partyId, otherMemberId);
				}
				else if (partyEvent.eventData == "deck")
				{
					if (this.DidSendChallenge())
					{
						if (this.m_data.m_challengeeAccepted)
						{
							this.m_data.m_challengeeDeckOrHeroSelected = false;
							this.m_data.m_challengeeInGameState = false;
							this.FireChangedEvent(FriendChallengeEvent.DESELECTED_DECK_OR_HERO, this.m_data.m_challengee, null);
						}
						else
						{
							this.m_data.m_challengeeAccepted = true;
							this.FireChangedEvent(FriendChallengeEvent.OPPONENT_ACCEPTED_CHALLENGE, this.m_data.m_challengee, null);
						}
					}
				}
				else if (partyEvent.eventData == "ready")
				{
					if (this.DidSendChallenge())
					{
						this.m_data.m_challengeeDeckOrHeroSelected = true;
						this.FireChangedEvent(FriendChallengeEvent.SELECTED_DECK_OR_HERO, this.m_data.m_challengee, null);
						this.SetIAmInGameState();
					}
				}
				else if (partyEvent.eventData == "game")
				{
					if (this.DidSendChallenge())
					{
						this.m_data.m_challengeeInGameState = true;
						this.SetIAmInGameState();
						if (this.StartFriendlyChallengeGameIfReady())
						{
							FriendlyChallengeHelper.Get().WaitForFriendChallengeToStart();
						}
					}
				}
				else if (partyEvent.eventData == "goto")
				{
					this.m_data.m_challengeeDeckOrHeroSelected = false;
					this.m_data.m_challengeeInGameState = false;
				}
			}
			else if (partyEvent.eventName == "left")
			{
				if (this.DidSendChallenge())
				{
					BnetPlayer challengee = this.m_data.m_challengee;
					bool challengeeAccepted = this.m_data.m_challengeeAccepted;
					this.RevertTavernBrawlPresenceStatus();
					FriendlyChallengeData challengeData = this.CleanUpChallengeData(true);
					if (challengeeAccepted)
					{
						this.FireChangedEvent(FriendChallengeEvent.OPPONENT_CANCELED_CHALLENGE, challengee, challengeData);
					}
					else
					{
						this.FireChangedEvent(FriendChallengeEvent.OPPONENT_DECLINED_CHALLENGE, challengee, challengeData);
					}
				}
				else if (this.DidReceiveChallenge())
				{
					BnetPlayer challenger = this.m_data.m_challenger;
					bool challengeeAccepted2 = this.m_data.m_challengeeAccepted;
					this.RevertTavernBrawlPresenceStatus();
					FriendlyChallengeData challengeData2 = this.CleanUpChallengeData(true);
					if (challenger != null)
					{
						if (challengeeAccepted2)
						{
							this.FireChangedEvent(FriendChallengeEvent.OPPONENT_CANCELED_CHALLENGE, challenger, challengeData2);
						}
						else
						{
							this.FireChangedEvent(FriendChallengeEvent.OPPONENT_RESCINDED_CHALLENGE, challenger, challengeData2);
						}
					}
				}
				else
				{
					this.CleanUpChallengeData(true);
				}
			}
			else if (partyEvent.eventName == "p1DeckShareState")
			{
				if (this.m_data.m_challenger != null)
				{
					string challengerDeckShareState = this.m_data.m_challengerDeckShareState;
					this.m_data.m_challengerDeckShareState = partyEvent.eventData;
					if (challengerDeckShareState == "none" && this.m_data.m_challengerDeckShareState == "requested")
					{
						if (this.DidReceiveChallenge())
						{
							this.FireChangedEvent(FriendChallengeEvent.OPPONENT_REQUESTED_DECK_SHARE, this.m_data.m_challenger, null);
						}
						else if (this.DidSendChallenge())
						{
							this.FireChangedEvent(FriendChallengeEvent.I_REQUESTED_DECK_SHARE, this.m_data.m_challenger, null);
						}
					}
					else if (challengerDeckShareState == "requested" && this.m_data.m_challengerDeckShareState == "none")
					{
						if (this.DidSendChallenge())
						{
							this.FireChangedEvent(FriendChallengeEvent.I_CANCELED_DECK_SHARE_REQUEST, this.m_data.m_challenger, null);
						}
						if (this.DidReceiveChallenge())
						{
							this.FireChangedEvent(FriendChallengeEvent.OPPONENT_CANCELED_DECK_SHARE_REQUEST, this.m_data.m_challenger, null);
						}
					}
					else if (challengerDeckShareState == "requested" && this.m_data.m_challengerDeckShareState == "declined")
					{
						if (this.DidReceiveChallenge())
						{
							this.FireChangedEvent(FriendChallengeEvent.I_DECLINED_DECK_SHARE_REQUEST, this.m_data.m_challenger, null);
						}
						else if (this.DidSendChallenge())
						{
							this.FireChangedEvent(FriendChallengeEvent.OPPONENT_DECLINED_DECK_SHARE_REQUEST, this.m_data.m_challenger, null);
						}
					}
					else if (challengerDeckShareState == "requested" && this.m_data.m_challengerDeckShareState == "sharing")
					{
						if (this.DidReceiveChallenge())
						{
							this.FireChangedEvent(FriendChallengeEvent.I_ACCEPTED_DECK_SHARE_REQUEST, this.m_data.m_challenger, null);
						}
						else if (this.DidSendChallenge())
						{
							this.FireChangedEvent(FriendChallengeEvent.OPPONENT_ACCEPTED_DECK_SHARE_REQUEST, this.m_data.m_challenger, null);
						}
					}
					else if (challengerDeckShareState == "sharing" && this.m_data.m_challengerDeckShareState == "sharingUnused")
					{
						if (this.DidSendChallenge())
						{
							this.FireChangedEvent(FriendChallengeEvent.I_ENDED_DECK_SHARE, this.m_data.m_challenger, null);
						}
					}
					else if (challengerDeckShareState == "sharingUnused" && this.m_data.m_challengerDeckShareState == "sharing")
					{
						if (this.DidSendChallenge())
						{
							this.FireChangedEvent(FriendChallengeEvent.I_RECEIVED_SHARED_DECKS, this.m_data.m_challenger, null);
						}
					}
					else if (this.m_data.m_challengerDeckShareState == "error" && this.DidSendChallenge())
					{
						this.FireChangedEvent(FriendChallengeEvent.DECK_SHARE_ERROR_OCCURED, this.m_data.m_challenger, null);
					}
				}
			}
			else if (partyEvent.eventName == "p2DeckShareState")
			{
				if (this.m_data.m_challengee != null)
				{
					string challengeeDeckShareState = this.m_data.m_challengeeDeckShareState;
					this.m_data.m_challengeeDeckShareState = partyEvent.eventData;
					if (challengeeDeckShareState == "none" && this.m_data.m_challengeeDeckShareState == "requested")
					{
						if (this.DidReceiveChallenge())
						{
							this.FireChangedEvent(FriendChallengeEvent.I_REQUESTED_DECK_SHARE, this.m_data.m_challengee, null);
						}
						else if (this.DidSendChallenge())
						{
							this.FireChangedEvent(FriendChallengeEvent.OPPONENT_REQUESTED_DECK_SHARE, this.m_data.m_challengee, null);
						}
					}
					else if (challengeeDeckShareState == "requested" && this.m_data.m_challengeeDeckShareState == "none")
					{
						if (this.DidSendChallenge())
						{
							this.FireChangedEvent(FriendChallengeEvent.OPPONENT_CANCELED_DECK_SHARE_REQUEST, this.m_data.m_challengee, null);
						}
						else if (this.DidReceiveChallenge())
						{
							this.FireChangedEvent(FriendChallengeEvent.I_CANCELED_DECK_SHARE_REQUEST, this.m_data.m_challengee, null);
						}
					}
					else if (challengeeDeckShareState == "requested" && this.m_data.m_challengeeDeckShareState == "declined")
					{
						if (this.DidReceiveChallenge())
						{
							this.FireChangedEvent(FriendChallengeEvent.OPPONENT_DECLINED_DECK_SHARE_REQUEST, this.m_data.m_challengee, null);
						}
						else if (this.DidSendChallenge())
						{
							this.FireChangedEvent(FriendChallengeEvent.I_DECLINED_DECK_SHARE_REQUEST, this.m_data.m_challengee, null);
						}
					}
					else if (challengeeDeckShareState == "requested" && this.m_data.m_challengeeDeckShareState == "sharing")
					{
						if (this.DidReceiveChallenge())
						{
							this.FireChangedEvent(FriendChallengeEvent.OPPONENT_ACCEPTED_DECK_SHARE_REQUEST, this.m_data.m_challengee, null);
						}
						else if (this.DidSendChallenge())
						{
							this.FireChangedEvent(FriendChallengeEvent.I_ACCEPTED_DECK_SHARE_REQUEST, this.m_data.m_challengee, null);
						}
					}
					else if (challengeeDeckShareState == "sharing" && this.m_data.m_challengeeDeckShareState == "sharingUnused")
					{
						if (this.DidReceiveChallenge())
						{
							this.FireChangedEvent(FriendChallengeEvent.I_ENDED_DECK_SHARE, this.m_data.m_challengee, null);
						}
					}
					else if (challengeeDeckShareState == "sharingUnused" && this.m_data.m_challengeeDeckShareState == "sharing")
					{
						if (this.DidReceiveChallenge())
						{
							this.FireChangedEvent(FriendChallengeEvent.I_RECEIVED_SHARED_DECKS, this.m_data.m_challengee, null);
						}
					}
					else if (this.m_data.m_challengeeDeckShareState == "error" && this.DidReceiveChallenge())
					{
						this.FireChangedEvent(FriendChallengeEvent.DECK_SHARE_ERROR_OCCURED, this.m_data.m_challengee, null);
					}
				}
			}
			else if (partyEvent.eventName == "p1DeckShareDecks")
			{
				if (this.DidReceiveChallenge() && this.m_data.m_challengeeDeckShareState == "sharing")
				{
					if (this.HasOpponentSharedDecks())
					{
						this.FireChangedEvent(FriendChallengeEvent.I_RECEIVED_SHARED_DECKS, this.m_data.m_challengee, null);
					}
					else
					{
						BnetParty.SetPartyAttributeString(this.m_data.m_partyId, "p2DeckShareState", "error");
					}
				}
			}
			else if (partyEvent.eventName == "p2DeckShareDecks" && this.DidSendChallenge() && this.m_data.m_challengerDeckShareState == "sharing")
			{
				if (this.HasOpponentSharedDecks())
				{
					this.FireChangedEvent(FriendChallengeEvent.I_RECEIVED_SHARED_DECKS, this.m_data.m_challenger, null);
				}
				else
				{
					BnetParty.SetPartyAttributeString(this.m_data.m_partyId, "p1DeckShareState", "error");
				}
			}
		}
	}

	// Token: 0x06000804 RID: 2052 RVA: 0x0002E959 File Offset: 0x0002CB59
	private void OnPartyUpdate_CreatedParty(BnetEntityId partyId, BnetGameAccountId otherMemberId)
	{
		this.UpdateChallengeSentDialog();
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x0002E961 File Offset: 0x0002CB61
	private void OnPartyUpdate_JoinedParty(BnetEntityId partyId, BnetGameAccountId otherMemberId)
	{
		if (this.DidSendChallenge())
		{
			return;
		}
		if (!FriendChallengeMgr.CanReceiveChallengeFrom(otherMemberId, partyId))
		{
			this.DeclineFriendChallenge_Internal(partyId);
			return;
		}
		if (!this.AmIAvailable())
		{
			this.DeclineFriendChallenge_Internal(partyId);
			return;
		}
		this.HandleJoinedParty(partyId, otherMemberId);
	}

	// Token: 0x06000806 RID: 2054 RVA: 0x0002E998 File Offset: 0x0002CB98
	private static bool CanReceiveChallengeFrom(BnetGameAccountId challengerPlayer, BnetEntityId challengerPartyId)
	{
		if (BnetFriendMgr.Get().IsFriend(challengerPlayer))
		{
			return true;
		}
		if (BnetNearbyPlayerMgr.Get().IsNearbyStranger(challengerPlayer))
		{
			return true;
		}
		FiresideGatheringManager firesideGatheringManager = FiresideGatheringManager.Get();
		if (firesideGatheringManager.IsCheckedIn)
		{
			if (firesideGatheringManager.IsPlayerInMyFSG(BnetUtils.GetPlayer(challengerPlayer)))
			{
				return true;
			}
			if (firesideGatheringManager.CurrentFsgSharedSecretKey != null)
			{
				byte[] arr = SHA256.Create().ComputeHash(firesideGatheringManager.CurrentFsgSharedSecretKey, 0, firesideGatheringManager.CurrentFsgSharedSecretKey.Length);
				if (GeneralUtils.AreArraysEqual<byte>(BnetParty.GetPartyAttributeBlob(challengerPartyId, "fsg1"), arr))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000807 RID: 2055 RVA: 0x0002EA20 File Offset: 0x0002CC20
	private bool StartFriendlyChallengeGameIfReady()
	{
		if (!this.DidSendChallenge())
		{
			return false;
		}
		if (!BnetParty.IsInParty(this.m_data.m_partyId))
		{
			return false;
		}
		bool flag = this.m_data.m_challengerDeckId != 0L && this.m_data.m_challengeeDeckId != 0L;
		bool flag2 = this.m_data.m_challengerHeroId != 0L && this.m_data.m_challengeeHeroId != 0L;
		if (!flag && !flag2)
		{
			return false;
		}
		if (!this.m_data.m_challengerInGameState || !this.m_data.m_challengeeInGameState)
		{
			return false;
		}
		this.m_data.m_findGameErrorOccurred = false;
		bnet.protocol.Attribute attributeV = ProtocolHelper.CreateAttribute("s1", "goto");
		bnet.protocol.Attribute attributeV2 = ProtocolHelper.CreateAttribute("s2", "goto");
		BnetParty.SetPartyAttributes(this.m_data.m_partyId, new bnet.protocol.v2.Attribute[]
		{
			ProtocolHelper.V1AttributeToV2Attribute(attributeV),
			ProtocolHelper.V1AttributeToV2Attribute(attributeV2)
		});
		FormatType formatType = this.GetFormatType();
		if (this.IsChallengeBacon())
		{
			Network.Get().EnterBattlegroundsWithFriend(this.m_data.m_challengee.GetHearthstoneGameAccountId(), this.m_data.m_scenarioId);
		}
		else if (flag)
		{
			string partyAttributeString = BnetParty.GetPartyAttributeString(this.m_data.m_partyId, "p1DeckShareState");
			DeckShareState deckShareStateEnumFromAttribute = this.GetDeckShareStateEnumFromAttribute(partyAttributeString);
			string partyAttributeString2 = BnetParty.GetPartyAttributeString(this.m_data.m_partyId, "p2DeckShareState");
			DeckShareState deckShareStateEnumFromAttribute2 = this.GetDeckShareStateEnumFromAttribute(partyAttributeString2);
			GameMgr.Get().EnterFriendlyChallengeGameWithDecks(formatType, this.m_data.m_challengeBrawlType, this.m_data.m_scenarioId, this.m_data.m_seasonId, this.m_data.m_brawlLibraryItemId, deckShareStateEnumFromAttribute, this.m_data.m_challengerDeckId, deckShareStateEnumFromAttribute2, this.m_data.m_challengeeDeckId, this.m_data.m_challengee.GetHearthstoneGameAccountId());
		}
		else
		{
			GameMgr.Get().EnterFriendlyChallengeGameWithHeroes(formatType, this.m_data.m_challengeBrawlType, this.m_data.m_scenarioId, this.m_data.m_seasonId, this.m_data.m_brawlLibraryItemId, this.m_data.m_challengerHeroId, this.m_data.m_challengeeHeroId, this.m_data.m_challengee.GetHearthstoneGameAccountId());
		}
		if (this.m_challengeDialog != null)
		{
			this.m_challengeDialog.Hide();
			this.m_challengeDialog = null;
		}
		return true;
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x0002EC7C File Offset: 0x0002CE7C
	private void SetIAmInGameState()
	{
		if (!BnetParty.IsInParty(this.m_data.m_partyId))
		{
			return;
		}
		if (!this.m_data.m_challengerDeckOrHeroSelected || !this.m_data.m_challengeeDeckOrHeroSelected)
		{
			return;
		}
		if (this.AmIInGameState())
		{
			return;
		}
		if (this.DidSendChallenge())
		{
			this.m_data.m_challengerInGameState = true;
			BnetParty.SetPartyAttributeString(this.m_data.m_partyId, "s1", "game");
			return;
		}
		this.m_data.m_challengeeInGameState = true;
		BnetParty.SetPartyAttributeString(this.m_data.m_partyId, "s2", "game");
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x0002ED24 File Offset: 0x0002CF24
	private void OnNetCacheReady()
	{
		NetCache.Get().UnregisterNetCacheHandler(new NetCache.NetCacheCallback(this.OnNetCacheReady));
		this.m_netCacheReady = true;
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.FATAL_ERROR)
		{
			return;
		}
		this.UpdateMyAvailability();
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x0002ED59 File Offset: 0x0002CF59
	private void OnSceneUnloaded(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		if (prevMode != SceneMgr.Mode.GAMEPLAY)
		{
			this.UpdateMyAvailability();
		}
		if (this.m_data.m_updatePartyQuestInfoOnGameplaySceneUnload && prevMode == SceneMgr.Mode.GAMEPLAY)
		{
			this.m_data.m_updatePartyQuestInfoOnGameplaySceneUnload = false;
			this.UpdatePartyQuestInfo();
		}
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x0002ED8C File Offset: 0x0002CF8C
	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (SceneMgr.Get().GetPrevMode() != SceneMgr.Mode.GAMEPLAY)
		{
			return;
		}
		if (mode == SceneMgr.Mode.GAMEPLAY)
		{
			return;
		}
		if (mode == SceneMgr.Mode.FATAL_ERROR)
		{
			return;
		}
		this.m_netCacheReady = false;
		if (mode == SceneMgr.Mode.FRIENDLY || (mode == SceneMgr.Mode.TAVERN_BRAWL && FriendChallengeMgr.Get().IsChallengeTavernBrawl()))
		{
			this.UpdateMyAvailability();
		}
		else
		{
			this.CancelChallenge();
		}
		NetCache.Get().RegisterFriendChallenge(new NetCache.NetCacheCallback(this.OnNetCacheReady));
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x0002EDF4 File Offset: 0x0002CFF4
	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		BnetPlayerChange bnetPlayerChange = changelist.FindChange(myPlayer);
		if (bnetPlayerChange != null)
		{
			bool flag = this.AmIAvailable();
			BnetGameAccount hearthstoneGameAccount = myPlayer.GetHearthstoneGameAccount();
			if (hearthstoneGameAccount != null && !this.m_myPlayerReady && hearthstoneGameAccount.HasGameField(20U) && hearthstoneGameAccount.HasGameField(19U))
			{
				this.m_myPlayerReady = true;
				if (!this.UpdateMyAvailability())
				{
					flag = false;
				}
			}
			if (!flag && this.m_data.m_challengerPending)
			{
				this.DeclineFriendChallenge_Internal(this.m_data.m_partyId);
				this.CleanUpChallengeData(true);
			}
		}
		if (this.m_data.m_challengerPending)
		{
			bnetPlayerChange = changelist.FindChange(this.m_data.m_challengerId);
			if (bnetPlayerChange != null)
			{
				BnetPlayer player = bnetPlayerChange.GetPlayer();
				if (player.IsDisplayable())
				{
					this.m_data.m_challenger = player;
					this.m_data.m_challengerPending = false;
					this.FireChangedEvent(FriendChallengeEvent.I_RECEIVED_CHALLENGE, this.m_data.m_challenger, null);
				}
			}
		}
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x0002EEE4 File Offset: 0x0002D0E4
	private void OnFriendsChanged(BnetFriendChangelist changelist, object userData)
	{
		if (!this.HasChallenge())
		{
			return;
		}
		List<BnetPlayer> removedFriends = changelist.GetRemovedFriends();
		if (removedFriends == null)
		{
			return;
		}
		BnetPlayer opponent = this.GetOpponent(BnetPresenceMgr.Get().GetMyPlayer());
		if (opponent == null)
		{
			return;
		}
		using (List<BnetPlayer>.Enumerator enumerator = removedFriends.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == opponent)
				{
					PartyInfo[] joinedParties = BnetParty.GetJoinedParties();
					BnetGameAccountId hearthstoneGameAccountId = opponent.GetHearthstoneGameAccountId();
					foreach (PartyInfo partyInfo in joinedParties)
					{
						if (BnetParty.IsMember(partyInfo.Id, hearthstoneGameAccountId))
						{
							BnetParty.Leave(partyInfo.Id);
						}
					}
					this.RevertTavernBrawlPresenceStatus();
					FriendlyChallengeData challengeData = this.CleanUpChallengeData(true);
					this.FireChangedEvent(FriendChallengeEvent.OPPONENT_REMOVED_FROM_FRIENDS, opponent, challengeData);
					break;
				}
			}
		}
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x0002EFB8 File Offset: 0x0002D1B8
	private void OnNearbyPlayersChanged(BnetNearbyPlayerChangelist changelist, object userData)
	{
		if (!this.HasChallenge())
		{
			return;
		}
		List<BnetPlayer> removedPlayers = changelist.GetRemovedPlayers();
		if (removedPlayers == null)
		{
			return;
		}
		BnetPlayer opponent = this.GetOpponent(BnetPresenceMgr.Get().GetMyPlayer());
		if (opponent == null)
		{
			return;
		}
		using (List<BnetPlayer>.Enumerator enumerator = removedPlayers.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == opponent)
				{
					FriendlyChallengeData challengeData = this.CleanUpChallengeData(true);
					this.FireChangedEvent(FriendChallengeEvent.OPPONENT_CANCELED_CHALLENGE, opponent, challengeData);
					break;
				}
			}
		}
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x0002F040 File Offset: 0x0002D240
	private void OnBnetEventOccurred(BattleNet.BnetEvent bnetEvent, object userData)
	{
		if (bnetEvent == BattleNet.BnetEvent.Disconnected)
		{
			this.OnDisconnect();
		}
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x0002F04B File Offset: 0x0002D24B
	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		this.OnDisconnect();
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x0002F053 File Offset: 0x0002D253
	private void OnDisconnect()
	{
		if (this.m_challengeDialog != null)
		{
			this.m_challengeDialog.Hide();
			this.m_challengeDialog = null;
		}
		this.CleanUpChallengeData(true);
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x0002C48D File Offset: 0x0002A68D
	private void OnReconnectLoginComplete()
	{
		this.UpdateMyAvailability();
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x0002F080 File Offset: 0x0002D280
	private void OnChallengeChanged(FriendChallengeEvent challengeEvent, BnetPlayer player, FriendlyChallengeData challengeData, object userData)
	{
		switch (challengeEvent)
		{
		case FriendChallengeEvent.I_SENT_CHALLENGE:
			this.ShowISentChallengeDialog(player);
			return;
		case FriendChallengeEvent.I_RESCINDED_CHALLENGE:
		case FriendChallengeEvent.I_DECLINED_CHALLENGE:
			this.OnChallengeCanceled();
			return;
		case FriendChallengeEvent.OPPONENT_ACCEPTED_CHALLENGE:
			this.StartChallengeProcess();
			return;
		case FriendChallengeEvent.OPPONENT_DECLINED_CHALLENGE:
			this.ShowOpponentDeclinedChallengeDialog(player, challengeData);
			return;
		case FriendChallengeEvent.I_RECEIVED_CHALLENGE:
			if (this.CanPromptReceivedChallenge())
			{
				if (this.IsChallengeTavernBrawl())
				{
					PresenceMgr.Get().SetStatus(new Enum[]
					{
						Global.PresenceStatus.TAVERN_BRAWL_FRIENDLY_WAITING
					});
				}
				this.ShowIReceivedChallengeDialog(player);
				return;
			}
			break;
		case FriendChallengeEvent.I_ACCEPTED_CHALLENGE:
			this.StartChallengeProcess();
			return;
		case FriendChallengeEvent.OPPONENT_RESCINDED_CHALLENGE:
			this.OnChallengeCanceled();
			this.ShowOpponentCanceledChallengeDialog(player, challengeData);
			return;
		case FriendChallengeEvent.SELECTED_DECK_OR_HERO:
		case FriendChallengeEvent.DESELECTED_DECK_OR_HERO:
		case FriendChallengeEvent.I_ENDED_DECK_SHARE:
		case FriendChallengeEvent.I_RECEIVED_SHARED_DECKS:
			break;
		case FriendChallengeEvent.OPPONENT_CANCELED_CHALLENGE:
			FriendlyChallengeHelper.Get().HideAllDeckShareDialogs();
			this.OnChallengeCanceled();
			this.ShowOpponentCanceledChallengeDialog(player, challengeData);
			return;
		case FriendChallengeEvent.OPPONENT_REMOVED_FROM_FRIENDS:
			FriendlyChallengeHelper.Get().HideAllDeckShareDialogs();
			this.ShowOpponentRemovedFromFriendsDialog(player, challengeData);
			return;
		case FriendChallengeEvent.I_REQUESTED_DECK_SHARE:
			if (!FriendlyChallengeHelper.Get().IsShowingDeckShareRequestDialog())
			{
				FriendlyChallengeHelper.Get().ShowDeckShareRequestWaitingDialog(new AlertPopup.ResponseCallback(this.OnFriendChallengeDeckShareRequestDialogWaitingResponse));
				return;
			}
			break;
		case FriendChallengeEvent.I_ACCEPTED_DECK_SHARE_REQUEST:
			FriendlyChallengeHelper.Get().HideDeckShareRequestDialog();
			this.ShareDecks_InternalParty();
			return;
		case FriendChallengeEvent.I_DECLINED_DECK_SHARE_REQUEST:
			FriendlyChallengeHelper.Get().HideDeckShareRequestDialog();
			return;
		case FriendChallengeEvent.I_CANCELED_DECK_SHARE_REQUEST:
			FriendlyChallengeHelper.Get().HideDeckShareRequestWaitingDialog();
			return;
		case FriendChallengeEvent.DECK_SHARE_ERROR_OCCURED:
			if (this.DidSendChallenge())
			{
				BnetParty.SetPartyAttributeString(this.m_data.m_partyId, "p1DeckShareState", "none");
			}
			else if (this.DidReceiveChallenge())
			{
				BnetParty.SetPartyAttributeString(this.m_data.m_partyId, "p2DeckShareState", "none");
			}
			FriendlyChallengeHelper.Get().HideAllDeckShareDialogs();
			FriendlyChallengeHelper.Get().ShowDeckShareErrorDialog();
			return;
		case FriendChallengeEvent.OPPONENT_REQUESTED_DECK_SHARE:
			FriendlyChallengeHelper.Get().HideAllDeckShareDialogs();
			FriendlyChallengeHelper.Get().HideFriendChallengeWaitingForOpponentDialog();
			FriendlyChallengeHelper.Get().ShowDeckShareRequestDialog(new AlertPopup.ResponseCallback(this.OnFriendChallengeDeckShareRequestDialogResponse));
			return;
		case FriendChallengeEvent.OPPONENT_DECLINED_DECK_SHARE_REQUEST:
			if (this.DidSendChallenge())
			{
				BnetParty.SetPartyAttributeString(this.m_data.m_partyId, "p1DeckShareState", "none");
			}
			else if (this.DidReceiveChallenge())
			{
				BnetParty.SetPartyAttributeString(this.m_data.m_partyId, "p2DeckShareState", "none");
			}
			FriendlyChallengeHelper.Get().ShowDeckShareRequestDeclinedDialog();
			FriendlyChallengeHelper.Get().HideDeckShareRequestWaitingDialog();
			return;
		case FriendChallengeEvent.OPPONENT_ACCEPTED_DECK_SHARE_REQUEST:
			FriendlyChallengeHelper.Get().HideDeckShareRequestWaitingDialog();
			return;
		case FriendChallengeEvent.OPPONENT_CANCELED_DECK_SHARE_REQUEST:
			FriendlyChallengeHelper.Get().ShowDeckShareRequestCanceledDialog();
			FriendlyChallengeHelper.Get().HideDeckShareRequestDialog();
			break;
		case FriendChallengeEvent.QUEUE_CANCELED:
			this.OnChallengeCanceled();
			this.ShowQueueCanceledDialog(player, challengeData);
			return;
		default:
			return;
		}
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x0002F2FB File Offset: 0x0002D4FB
	private void OnChallengeCanceled()
	{
		if (SceneMgr.Get() != null && SceneMgr.Get().GetMode() != SceneMgr.Mode.FIRESIDE_GATHERING && FiresideGatheringManager.Get() != null)
		{
			FiresideGatheringManager.Get().CurrentFiresideGatheringMode = FiresideGatheringManager.FiresideGatheringMode.NONE;
		}
		GameMgr.Get().CancelFindGame();
		GameMgr.Get().HideTransitionPopup();
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x0002F33C File Offset: 0x0002D53C
	private bool CanPromptReceivedChallenge()
	{
		bool flag = !UserAttentionManager.CanShowAttentionGrabber("FriendlyChallengeMgr.CanPromptReceivedChallenge");
		if (!flag)
		{
			if (GameMgr.Get().IsFindingGame())
			{
				flag = true;
			}
			else if (RankMgr.Get().IsLegendRankInAnyFormat)
			{
				flag = SceneMgr.Get().IsModeRequested(SceneMgr.Mode.TOURNAMENT);
			}
		}
		if (flag)
		{
			BnetParty.SetPartyAttributeLong(this.m_data.m_partyId, "WTCG.Friendly.DeclineReason", 6L);
			this.DeclineChallenge();
			return false;
		}
		if (this.IsChallengeTavernBrawl())
		{
			if (!TavernBrawlManager.Get().HasUnlockedTavernBrawl(this.m_data.m_challengeBrawlType) && !this.PlayersInSameFiresideGathering())
			{
				FriendChallengeMgr.DeclineReason declineReason = FriendChallengeMgr.DeclineReason.TavernBrawlNotUnlocked;
				BnetParty.SetPartyAttributeLong(this.m_data.m_partyId, "WTCG.Friendly.DeclineReason", (long)declineReason);
				this.DeclineChallenge();
				return false;
			}
			TavernBrawlManager.Get().EnsureAllDataReady(this.m_data.m_challengeBrawlType, new TavernBrawlManager.CallbackEnsureServerDataReady(this.TavernBrawl_ReceivedChallenge_OnEnsureServerDataReady));
			return false;
		}
		else
		{
			if (!CollectionManager.Get().AreAllDeckContentsReady())
			{
				CollectionManager.Get().RequestDeckContentsForDecksWithoutContentsLoaded(new CollectionManager.DelOnAllDeckContents(this.CanPromptReceivedChallenge_OnDeckContentsLoaded));
				return false;
			}
			if (this.IsChallengeStandardDuel() && !CollectionManager.Get().AccountHasValidDeck(FormatType.FT_STANDARD))
			{
				FriendChallengeMgr.DeclineReason declineReason2 = FriendChallengeMgr.DeclineReason.StandardNoValidDeck;
				BnetParty.SetPartyAttributeLong(this.m_data.m_partyId, "WTCG.Friendly.DeclineReason", (long)declineReason2);
				this.DeclineChallenge();
				return false;
			}
			if (this.IsChallengeWildDuel())
			{
				if (!CollectionManager.Get().ShouldAccountSeeStandardWild())
				{
					FriendChallengeMgr.DeclineReason declineReason3 = FriendChallengeMgr.DeclineReason.NotSeenWild;
					BnetParty.SetPartyAttributeLong(this.m_data.m_partyId, "WTCG.Friendly.DeclineReason", (long)declineReason3);
					this.DeclineChallenge();
					return false;
				}
				if (!CollectionManager.Get().AccountHasAnyValidDeck())
				{
					FriendChallengeMgr.DeclineReason declineReason4 = FriendChallengeMgr.DeclineReason.NoValidDeck;
					BnetParty.SetPartyAttributeLong(this.m_data.m_partyId, "WTCG.Friendly.DeclineReason", (long)declineReason4);
					this.DeclineChallenge();
					return false;
				}
			}
			else
			{
				if (this.IsChallengeClassicDuel() && !CollectionManager.Get().AccountHasValidDeck(FormatType.FT_CLASSIC))
				{
					FriendChallengeMgr.DeclineReason declineReason5 = FriendChallengeMgr.DeclineReason.ClassicNoValidDeck;
					BnetParty.SetPartyAttributeLong(this.m_data.m_partyId, "WTCG.Friendly.DeclineReason", (long)declineReason5);
					this.DeclineChallenge();
					return false;
				}
				if (this.IsChallengeBacon() && SpecialEventManager.Get().IsEventActive("battlegrounds_early_access", false) && !AccountLicenseMgr.Get().OwnsAccountLicense(NetCache.Get().GetBattlegroundsEarlyAccessLicenseId()))
				{
					FriendChallengeMgr.DeclineReason declineReason6 = FriendChallengeMgr.DeclineReason.BattlegroundsNoEarlyAccess;
					BnetParty.SetPartyAttributeLong(this.m_data.m_partyId, "WTCG.Friendly.DeclineReason", (long)declineReason6);
					this.DeclineChallenge();
					return false;
				}
			}
			return true;
		}
	}

	// Token: 0x06000816 RID: 2070 RVA: 0x0002F581 File Offset: 0x0002D781
	private void CanPromptReceivedChallenge_OnDeckContentsLoaded()
	{
		if (!this.DidReceiveChallenge())
		{
			return;
		}
		if (this.CanPromptReceivedChallenge())
		{
			this.ShowIReceivedChallengeDialog(this.m_data.m_challenger);
		}
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x0002F5A8 File Offset: 0x0002D7A8
	private void TavernBrawl_ReceivedChallenge_OnEnsureServerDataReady()
	{
		TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(this.m_data.m_challengeBrawlType);
		FriendChallengeMgr.DeclineReason? declineReason = null;
		if (mission == null)
		{
			declineReason = new FriendChallengeMgr.DeclineReason?(FriendChallengeMgr.DeclineReason.None);
		}
		if (mission != null && mission.CanCreateDeck(this.m_data.m_brawlLibraryItemId) && !TavernBrawlManager.Get().HasValidDeck(this.m_data.m_challengeBrawlType, 0))
		{
			declineReason = new FriendChallengeMgr.DeclineReason?(FriendChallengeMgr.DeclineReason.TavernBrawlNoValidDeck);
		}
		if (declineReason != null)
		{
			BnetParty.SetPartyAttributeLong(this.m_data.m_partyId, "WTCG.Friendly.DeclineReason", (long)declineReason.Value);
			this.DeclineChallenge();
			return;
		}
		if (this.IsChallengeTavernBrawl())
		{
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				Global.PresenceStatus.TAVERN_BRAWL_FRIENDLY_WAITING
			});
		}
		this.ShowIReceivedChallengeDialog(this.m_data.m_challenger);
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x0002F67A File Offset: 0x0002D87A
	private bool RevertTavernBrawlPresenceStatus()
	{
		if (this.IsChallengeTavernBrawl() && PresenceMgr.Get().CurrentStatus == Global.PresenceStatus.TAVERN_BRAWL_FRIENDLY_WAITING)
		{
			PresenceMgr.Get().SetPrevStatus();
			return true;
		}
		return false;
	}

	// Token: 0x06000819 RID: 2073 RVA: 0x0002F6A0 File Offset: 0x0002D8A0
	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		this.UpdateMyAvailability();
		switch (eventData.m_state)
		{
		case FindGameState.BNET_QUEUE_ENTERED:
		case FindGameState.SERVER_GAME_CONNECTING:
			if (this.HasChallenge())
			{
				this.DeselectDeckOrHero();
			}
			break;
		case FindGameState.BNET_QUEUE_CANCELED:
		case FindGameState.BNET_ERROR:
		{
			this.m_data.m_findGameErrorOccurred = true;
			if (this.DidSendChallenge())
			{
				BnetParty.SetPartyAttributeLong(this.m_data.m_partyId, "error", (long)((ulong)GameMgr.Get().GetLastEnterGameError()));
				BnetParty.SetPartyAttributeString(this.m_data.m_partyId, "s1", "deck");
			}
			else if (this.DidReceiveChallenge())
			{
				BnetParty.SetPartyAttributeString(this.m_data.m_partyId, "s2", "deck");
			}
			SceneMgr.Mode mode = SceneMgr.Get().GetMode();
			bool flag = mode != SceneMgr.Mode.FRIENDLY && mode != SceneMgr.Mode.TAVERN_BRAWL && mode != SceneMgr.Mode.FIRESIDE_GATHERING;
			if (this.DidSendChallenge() && this.IsChallengeFiresideBrawl())
			{
				flag = true;
			}
			if (flag)
			{
				this.QueueCanceled();
			}
			break;
		}
		}
		return false;
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x0002F7AD File Offset: 0x0002D9AD
	private void WillReset()
	{
		this.CleanUpChallengeData(false);
		if (this.m_challengeDialog != null)
		{
			this.m_challengeDialog.Hide();
			this.m_challengeDialog = null;
		}
		FriendlyChallengeHelper.Get().HideAllDeckShareDialogs();
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x0002F7E4 File Offset: 0x0002D9E4
	private void ShowISentChallengeDialog(BnetPlayer challengee)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER");
		popupInfo.m_text = GameStrings.Format("GLOBAL_FRIEND_CHALLENGE_OPPONENT_WAITING_RESPONSE", new object[]
		{
			FriendUtils.GetUniqueName(challengee)
		});
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.NONE;
		popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnChallengeSentDialogResponse);
		popupInfo.m_layerToUse = new GameLayer?(GameLayer.UI);
		DialogManager.Get().ShowPopup(popupInfo, new DialogManager.DialogProcessCallback(this.OnChallengeSentDialogProcessed));
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x0002F86C File Offset: 0x0002DA6C
	private void ShowOpponentDeclinedChallengeDialog(BnetPlayer challengee, FriendlyChallengeData challengeData)
	{
		if (this.m_challengeDialog != null)
		{
			this.m_challengeDialog.Hide();
			this.m_challengeDialog = null;
		}
		if (!this.m_hasSeenDeclinedReason)
		{
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER"),
				m_text = GameStrings.Format("GLOBAL_FRIEND_CHALLENGE_OPPONENT_DECLINED", new object[]
				{
					FriendUtils.GetUniqueName(challengee)
				}),
				m_alertTextAlignment = UberText.AlignmentOptions.Center,
				m_showAlertIcon = false,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK,
				m_responseCallback = new AlertPopup.ResponseCallback(this.OnOpponentDeclinedChallengeDialogDismissed)
			};
			DialogManager.Get().ShowPopup(info);
		}
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x0002F90A File Offset: 0x0002DB0A
	private void OnOpponentDeclinedChallengeDialogDismissed(AlertPopup.Response response, object userData)
	{
		ChatMgr.Get().UpdateFriendItemsWhenAvailable();
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x0002F918 File Offset: 0x0002DB18
	private void ShowIReceivedChallengeDialog(BnetPlayer challenger)
	{
		if (this.m_challengeDialog != null)
		{
			this.m_challengeDialog.Hide();
			this.m_challengeDialog = null;
		}
		DialogManager.Get().ShowFriendlyChallenge(this.m_data.m_challengeFormatType, challenger, this.IsChallengeTavernBrawl(), PartyType.FRIENDLY_CHALLENGE, new FriendlyChallengeDialog.ResponseCallback(this.OnChallengeReceivedDialogResponse), new DialogManager.DialogProcessCallback(this.OnChallengeReceivedDialogProcessed));
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x0002F97C File Offset: 0x0002DB7C
	private void ShowOpponentCanceledChallengeDialog(BnetPlayer otherPlayer, FriendlyChallengeData challengeData)
	{
		if (this.m_challengeDialog != null)
		{
			this.m_challengeDialog.Hide();
			this.m_challengeDialog = null;
		}
		if (GameMgr.Get() != null && this.SuppressChallengeCanceledDialogByMissionId(GameMgr.Get().GetMissionId()))
		{
			return;
		}
		if (SceneMgr.Get() != null && SceneMgr.Get().IsInGame() && GameState.Get() != null && !GameState.Get().IsGameOverNowOrPending())
		{
			return;
		}
		if ((challengeData.m_challengeBrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING || SceneMgr.Get().GetMode() == SceneMgr.Mode.FIRESIDE_GATHERING) && (!challengeData.m_challengeeAccepted || challengeData.IsPendingGotoGame || challengeData.m_findGameErrorOccurred))
		{
			return;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER");
		popupInfo.m_text = GameStrings.Format("GLOBAL_FRIEND_CHALLENGE_OPPONENT_CANCELED", new object[]
		{
			FriendUtils.GetUniqueName(otherPlayer)
		});
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnOpponentCanceledChallengeDialogClosed);
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x06000820 RID: 2080 RVA: 0x0002FA7F File Offset: 0x0002DC7F
	public void OnOpponentCanceledChallengeDialogClosed(AlertPopup.Response response, object userData)
	{
		if (SceneMgr.Get().IsTransitionNowOrPending() && SceneMgr.Get().GetPrevMode() != SceneMgr.Mode.FRIENDLY)
		{
			SceneMgr.Get().ReturnToPreviousMode();
		}
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x0002FAA4 File Offset: 0x0002DCA4
	private void ShowOpponentRemovedFromFriendsDialog(BnetPlayer otherPlayer, FriendlyChallengeData challengeData)
	{
		if (this.m_challengeDialog != null)
		{
			this.m_challengeDialog.Hide();
			this.m_challengeDialog = null;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER");
		popupInfo.m_text = GameStrings.Format("GLOBAL_FRIEND_CHALLENGE_OPPONENT_FRIEND_REMOVED", new object[]
		{
			FriendUtils.GetUniqueName(otherPlayer)
		});
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x0002FB20 File Offset: 0x0002DD20
	private void ShowQueueCanceledDialog(BnetPlayer otherPlayer, FriendlyChallengeData challengeData)
	{
		if (this.m_challengeDialog != null)
		{
			this.m_challengeDialog.Hide();
			this.m_challengeDialog = null;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_FRIEND_CHALLENGE_HEADER");
		popupInfo.m_text = GameStrings.Format("GLOBAL_FRIEND_CHALLENGE_QUEUE_CANCELED", Array.Empty<object>());
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x0002FB91 File Offset: 0x0002DD91
	private bool OnChallengeSentDialogProcessed(DialogBase dialog, object userData)
	{
		if (!this.DidSendChallenge())
		{
			return false;
		}
		if (this.m_data.m_challengeeAccepted)
		{
			return false;
		}
		this.m_challengeDialog = dialog;
		this.UpdateChallengeSentDialog();
		return true;
	}

	// Token: 0x06000824 RID: 2084 RVA: 0x0002FBBC File Offset: 0x0002DDBC
	private void UpdateChallengeSentDialog()
	{
		if (this.m_data.m_partyId == null)
		{
			return;
		}
		if (this.m_challengeDialog == null)
		{
			return;
		}
		AlertPopup alertPopup = (AlertPopup)this.m_challengeDialog;
		AlertPopup.PopupInfo info = alertPopup.GetInfo();
		if (info == null)
		{
			return;
		}
		info.m_responseDisplay = AlertPopup.ResponseDisplay.CANCEL;
		alertPopup.UpdateInfo(info);
	}

	// Token: 0x06000825 RID: 2085 RVA: 0x0002FC11 File Offset: 0x0002DE11
	private void OnChallengeSentDialogResponse(AlertPopup.Response response, object userData)
	{
		this.m_challengeDialog = null;
		this.RescindChallenge();
	}

	// Token: 0x06000826 RID: 2086 RVA: 0x0002FC20 File Offset: 0x0002DE20
	private bool OnChallengeReceivedDialogProcessed(DialogBase dialog, object userData)
	{
		if (!this.DidReceiveChallenge())
		{
			return false;
		}
		this.m_challengeDialog = dialog;
		PartyQuestInfo partyQuestInfo = this.GetPartyQuestInfo();
		if (partyQuestInfo != null)
		{
			((FriendlyChallengeDialog)dialog).SetQuestInfo(partyQuestInfo);
		}
		return true;
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x0002FC55 File Offset: 0x0002DE55
	private void OnChallengeReceivedDialogResponse(bool accept)
	{
		this.m_challengeDialog = null;
		if (accept)
		{
			this.AcceptChallenge();
			return;
		}
		this.DeclineChallenge();
	}

	// Token: 0x06000828 RID: 2088 RVA: 0x0002FC70 File Offset: 0x0002DE70
	private void HandleJoinedParty(BnetEntityId partyId, BnetGameAccountId otherMemberId)
	{
		this.m_data.m_partyId = partyId;
		this.m_data.m_challengerId = otherMemberId;
		this.m_data.m_challenger = BnetUtils.GetPlayer(this.m_data.m_challengerId);
		this.m_data.m_challengee = BnetPresenceMgr.Get().GetMyPlayer();
		this.m_hasSeenDeclinedReason = false;
		if (this.m_data.m_challenger == null || !this.m_data.m_challenger.IsDisplayable())
		{
			this.m_data.m_challengerPending = true;
			this.UpdateMyAvailability();
			return;
		}
		this.UpdateMyAvailability();
		this.FireChangedEvent(FriendChallengeEvent.I_RECEIVED_CHALLENGE, this.m_data.m_challenger, null);
	}

	// Token: 0x06000829 RID: 2089 RVA: 0x0002FD1C File Offset: 0x0002DF1C
	public bool UpdateMyAvailability()
	{
		if (!Network.ShouldBeConnectedToAurora() || !Network.IsLoggedIn())
		{
			return false;
		}
		bool flag = !this.HasAvailabilityBlocker();
		global::Log.Presence.PrintDebug("UpdateMyAvailability: Available=" + flag.ToString(), Array.Empty<object>());
		this.m_canBeInvitedToGame = flag;
		if (!this.m_updateMyAvailabilityCallbackScheduledThisFrame)
		{
			Processor.ScheduleCallback(0f, false, new Processor.ScheduledCallback(this.UpdateMyAvailabilityScheduledCallback), null);
		}
		this.m_updateMyAvailabilityCallbackScheduledThisFrame = true;
		return flag;
	}

	// Token: 0x0600082A RID: 2090 RVA: 0x0002FD94 File Offset: 0x0002DF94
	private void UpdateMyAvailabilityScheduledCallback(object userData)
	{
		if (!this.m_updateMyAvailabilityCallbackScheduledThisFrame)
		{
			return;
		}
		this.m_updateMyAvailabilityCallbackScheduledThisFrame = false;
		global::Log.Presence.PrintDebug("UpdateMyAvailabilityScheduledCallback: Available=" + this.m_canBeInvitedToGame.ToString(), Array.Empty<object>());
		BnetPresenceMgr.Get().SetGameField(1U, this.m_canBeInvitedToGame);
		BnetNearbyPlayerMgr.Get().SetAvailability(this.m_canBeInvitedToGame);
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x0002FDF7 File Offset: 0x0002DFF7
	private bool HasAvailabilityBlocker()
	{
		return this.GetAvailabilityBlockerReason() != AvailabilityBlockerReasons.NONE;
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x0002FE04 File Offset: 0x0002E004
	private AvailabilityBlockerReasons GetAvailabilityBlockerReason()
	{
		AvailabilityBlockerReasons availabilityBlockerReasons = AvailabilityBlockerReasons.NONE;
		if (!this.m_netCacheReady)
		{
			availabilityBlockerReasons = AvailabilityBlockerReasons.NETCACHE_NOT_READY;
		}
		if (!this.m_myPlayerReady)
		{
			availabilityBlockerReasons = AvailabilityBlockerReasons.MY_PLAYER_NOT_READY;
		}
		if (this.HasChallenge())
		{
			availabilityBlockerReasons = AvailabilityBlockerReasons.HAS_EXISTING_CHALLENGE;
		}
		if (PartyManager.Get().HasPendingPartyInviteOrDialog())
		{
			availabilityBlockerReasons = AvailabilityBlockerReasons.HAS_PENDING_PARTY_INVITE;
		}
		if (availabilityBlockerReasons == AvailabilityBlockerReasons.NONE)
		{
			availabilityBlockerReasons = UserAttentionManager.GetAvailabilityBlockerReason(true);
		}
		if (availabilityBlockerReasons != AvailabilityBlockerReasons.NONE)
		{
			global::Log.Presence.PrintDebug("GetAvailabilityBlockerReason: " + availabilityBlockerReasons.ToString(), Array.Empty<object>());
		}
		return availabilityBlockerReasons;
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x0002FE74 File Offset: 0x0002E074
	private void FireChangedEvent(FriendChallengeEvent challengeEvent, BnetPlayer player, FriendlyChallengeData challengeData = null)
	{
		if (challengeData == null)
		{
			challengeData = this.m_data;
		}
		FriendChallengeMgr.ChangedListener[] array = this.m_changedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(challengeEvent, player, challengeData);
		}
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x0002FEB1 File Offset: 0x0002E0B1
	private FriendlyChallengeData CleanUpChallengeData(bool updateAvailability = true)
	{
		FriendlyChallengeData data = this.m_data;
		this.m_data = new FriendlyChallengeData();
		if (updateAvailability)
		{
			this.UpdateMyAvailability();
		}
		return data;
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x0002FED0 File Offset: 0x0002E0D0
	private void StartChallengeProcess()
	{
		bool flag = (!this.DidSendChallenge() && this.m_data.m_challengeeDeckOrHeroSelected) || (this.DidSendChallenge() && this.m_data.m_challengerDeckOrHeroSelected);
		if (this.m_challengeDialog != null && !flag)
		{
			this.m_challengeDialog.Hide();
			this.m_challengeDialog = null;
		}
		GameMgr.Get().SetPendingAutoConcede(true);
		if (CollectionManager.Get().IsInEditMode())
		{
			CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
			if (editedDeck != null)
			{
				editedDeck.SendChanges();
			}
		}
		if (this.IsChallengeTavernBrawl())
		{
			TavernBrawlManager.Get().CurrentBrawlType = this.m_data.m_challengeBrawlType;
			TavernBrawlMission tavernBrawlMission = TavernBrawlManager.Get().CurrentMission();
			if (tavernBrawlMission != null)
			{
				tavernBrawlMission.SetSelectedBrawlLibraryItemId(this.m_data.m_brawlLibraryItemId);
			}
		}
		if (this.IsChallengeBacon())
		{
			this.SkipDeckSelection();
			return;
		}
		if (this.IsChallengeTavernBrawl() && !TavernBrawlManager.Get().SelectHeroBeforeMission(this.m_data.m_challengeBrawlType))
		{
			if (!TavernBrawlManager.Get().GetMission(this.m_data.m_challengeBrawlType).canCreateDeck)
			{
				this.SkipDeckSelection();
				return;
			}
			if (TavernBrawlManager.Get().HasValidDeck(this.m_data.m_challengeBrawlType, 0))
			{
				this.SelectDeck(TavernBrawlManager.Get().GetDeck(this.m_data.m_challengeBrawlType, 0).ID);
				return;
			}
			Debug.LogError("Attempting to start a Tavern Brawl challenge without a valid deck!  How did this happen?");
			return;
		}
		else
		{
			if (!this.IsChallengeTavernBrawl())
			{
				Options.SetFormatType(this.m_data.m_challengeFormatType);
			}
			bool flag2 = this.DidSendChallenge() && this.m_data.m_challengerDeckOrHeroSelected;
			if (!this.ShouldTransitionToFriendlySceneAccordingToChallengeMethod() && flag2)
			{
				return;
			}
			if (this.m_challengeDialog != null)
			{
				this.m_challengeDialog.Hide();
				this.m_challengeDialog = null;
			}
			Navigation.Clear();
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.FRIENDLY, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			return;
		}
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x0003009B File Offset: 0x0002E29B
	private bool SuppressChallengeCanceledDialogByMissionId(int missionId)
	{
		return missionId == 3459;
	}

	// Token: 0x04000563 RID: 1379
	public const int DEFAULT_SCENARIO_ID = 2;

	// Token: 0x04000564 RID: 1380
	private const bool FRIENDLY_CHALLENGE_PARTY_INVITES_USE_RESERVATIONS = true;

	// Token: 0x04000565 RID: 1381
	private static FriendChallengeMgr s_instance;

	// Token: 0x04000566 RID: 1382
	private bool m_netCacheReady;

	// Token: 0x04000567 RID: 1383
	private bool m_myPlayerReady;

	// Token: 0x04000568 RID: 1384
	private FriendlyChallengeData m_data = new FriendlyChallengeData();

	// Token: 0x04000569 RID: 1385
	private bool m_hasPreSelectedDeckOrHero;

	// Token: 0x0400056A RID: 1386
	private long m_preSelectedDeckId;

	// Token: 0x0400056B RID: 1387
	private long m_preSelectedHeroId;

	// Token: 0x0400056C RID: 1388
	private FriendChallengeMgr.ChallengeMethod m_challengeMethod;

	// Token: 0x0400056D RID: 1389
	private List<FriendChallengeMgr.ChangedListener> m_changedListeners = new List<FriendChallengeMgr.ChangedListener>();

	// Token: 0x0400056E RID: 1390
	private DialogBase m_challengeDialog;

	// Token: 0x0400056F RID: 1391
	private bool m_hasSeenDeclinedReason;

	// Token: 0x04000570 RID: 1392
	private bool m_canBeInvitedToGame;

	// Token: 0x04000571 RID: 1393
	private bool m_updateMyAvailabilityCallbackScheduledThisFrame;

	// Token: 0x04000572 RID: 1394
	private const string ATTRIBUTE_STATE_PLAYER1 = "s1";

	// Token: 0x04000573 RID: 1395
	private const string ATTRIBUTE_STATE_PLAYER2 = "s2";

	// Token: 0x04000574 RID: 1396
	private const string ATTRIBUTE_DECK_PLAYER1 = "d1";

	// Token: 0x04000575 RID: 1397
	private const string ATTRIBUTE_DECK_PLAYER2 = "d2";

	// Token: 0x04000576 RID: 1398
	private const string ATTRIBUTE_HERO_PLAYER1 = "hero1";

	// Token: 0x04000577 RID: 1399
	private const string ATTRIBUTE_HERO_PLAYER2 = "hero2";

	// Token: 0x04000578 RID: 1400
	private const string ATTRIBUTE_FSG_SHARED_SECRET_KEY_PLAYER1 = "fsg1";

	// Token: 0x04000579 RID: 1401
	private const string ATTRIBUTE_FSG_SHARED_SECRET_KEY_PLAYER2 = "fsg2";

	// Token: 0x0400057A RID: 1402
	private const string ATTRIBUTE_LEFT = "left";

	// Token: 0x0400057B RID: 1403
	private const string ATTRIBUTE_ERROR = "error";

	// Token: 0x0400057C RID: 1404
	private const string ATTRIBUTE_DECK_SHARE_STATE_PLAYER1 = "p1DeckShareState";

	// Token: 0x0400057D RID: 1405
	private const string ATTRIBUTE_DECK_SHARE_STATE_PLAYER2 = "p2DeckShareState";

	// Token: 0x0400057E RID: 1406
	private const string ATTRIBUTE_DECK_SHARE_DECKS_PLAYER1 = "p1DeckShareDecks";

	// Token: 0x0400057F RID: 1407
	private const string ATTRIBUTE_DECK_SHARE_DECKS_PLAYER2 = "p2DeckShareDecks";

	// Token: 0x04000580 RID: 1408
	private const string ATTRIBUTE_DECK_SHARE_ENABLED = "isDeckShareEnabled";

	// Token: 0x04000581 RID: 1409
	private const string ATTRIBUTE_QUEST_INFO = "quests";

	// Token: 0x04000582 RID: 1410
	private const string ATTRIBUTE_VALUE_STATE_WAIT = "wait";

	// Token: 0x04000583 RID: 1411
	private const string ATTRIBUTE_VALUE_STATE_WAIT_FOR_DECK_OR_HERO_SELECT = "deck";

	// Token: 0x04000584 RID: 1412
	private const string ATTRIBUTE_VALUE_STATE_READY = "ready";

	// Token: 0x04000585 RID: 1413
	private const string ATTRIBUTE_VALUE_STATE_GAME = "game";

	// Token: 0x04000586 RID: 1414
	private const string ATTRIBUTE_VALUE_STATE_GOTO = "goto";

	// Token: 0x04000587 RID: 1415
	private const string ATTRIBUTE_VALUE_DECK_SHARE_STATE_NONE = "none";

	// Token: 0x04000588 RID: 1416
	private const string ATTRIBUTE_VALUE_DECK_SHARE_STATE_REQUESTED = "requested";

	// Token: 0x04000589 RID: 1417
	private const string ATTRIBUTE_VALUE_DECK_SHARE_STATE_SHARING = "sharing";

	// Token: 0x0400058A RID: 1418
	private const string ATTRIBUTE_VALUE_DECK_SHARE_STATE_SHARING_UNUSED = "sharingUnused";

	// Token: 0x0400058B RID: 1419
	private const string ATTRIBUTE_VALUE_DECK_SHARE_STATE_DECLINED = "declined";

	// Token: 0x0400058C RID: 1420
	private const string ATTRIBUTE_VALUE_DECK_SHARE_STATE_ERROR = "error";

	// Token: 0x0400058D RID: 1421
	private const string ATTRIBUTE_VALUE_DECK_SHARE_ENABLED = "deckShareEnabled";

	// Token: 0x0400058E RID: 1422
	private const string ATTRIBUTE_VALUE_DECK_SHARE_DISABLED = "deckShareDisabled";

	// Token: 0x02001383 RID: 4995
	public enum ChallengeMethod
	{
		// Token: 0x0400A6EF RID: 42735
		INVALID,
		// Token: 0x0400A6F0 RID: 42736
		FROM_FRIEND_LIST,
		// Token: 0x0400A6F1 RID: 42737
		FROM_FIRESIDE_GATHERING_OPPONENT_PICKER
	}

	// Token: 0x02001384 RID: 4996
	public enum DeclineReason
	{
		// Token: 0x0400A6F3 RID: 42739
		None,
		// Token: 0x0400A6F4 RID: 42740
		UserDeclined,
		// Token: 0x0400A6F5 RID: 42741
		NoValidDeck,
		// Token: 0x0400A6F6 RID: 42742
		StandardNoValidDeck,
		// Token: 0x0400A6F7 RID: 42743
		TavernBrawlNoValidDeck,
		// Token: 0x0400A6F8 RID: 42744
		TavernBrawlNotUnlocked,
		// Token: 0x0400A6F9 RID: 42745
		UserIsBusy,
		// Token: 0x0400A6FA RID: 42746
		NotSeenWild,
		// Token: 0x0400A6FB RID: 42747
		BattlegroundsNoEarlyAccess,
		// Token: 0x0400A6FC RID: 42748
		ClassicNoValidDeck
	}

	// Token: 0x02001385 RID: 4997
	// (Invoke) Token: 0x0600D7AE RID: 55214
	public delegate void ChangedCallback(FriendChallengeEvent challengeEvent, BnetPlayer player, FriendlyChallengeData challengeData, object userData);

	// Token: 0x02001386 RID: 4998
	private class ChangedListener : global::EventListener<FriendChallengeMgr.ChangedCallback>
	{
		// Token: 0x0600D7B1 RID: 55217 RVA: 0x003EC484 File Offset: 0x003EA684
		public void Fire(FriendChallengeEvent challengeEvent, BnetPlayer player, FriendlyChallengeData challengeData)
		{
			this.m_callback(challengeEvent, player, challengeData, this.m_userData);
		}
	}
}
