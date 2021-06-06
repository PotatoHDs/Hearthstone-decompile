using System;
using System.Collections.Generic;
using bgs;
using bgs.types;
using BobNetProto;
using PegasusFSG;
using PegasusGame;
using PegasusShared;
using PegasusUtil;

namespace Networking
{
	// Token: 0x02000FAA RID: 4010
	public class ConnectAPI
	{
		// Token: 0x0600AE45 RID: 44613 RVA: 0x00363D0F File Offset: 0x00361F0F
		public ConnectAPI(IDispatcher dispatcher)
		{
			this.dispatcherImpl = dispatcher;
		}

		// Token: 0x0600AE46 RID: 44614 RVA: 0x00363D20 File Offset: 0x00361F20
		public void AbortBlizzardPurchase(string deviceId, bool isAutoCanceled, CancelPurchase.CancelReason? reason, string error)
		{
			CancelPurchase cancelPurchase = new CancelPurchase
			{
				IsAutoCancel = isAutoCanceled,
				DeviceId = deviceId,
				ErrorMessage = error
			};
			if (reason != null)
			{
				cancelPurchase.Reason = reason.Value;
			}
			this.SendUtilPacket(274, UtilSystemId.BATTLEPAY, cancelPurchase, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE47 RID: 44615 RVA: 0x00363D6E File Offset: 0x00361F6E
		public void AbortThirdPartyPurchase(string deviceId, CancelPurchase.CancelReason reason, string error)
		{
			this.SendUtilPacket(274, UtilSystemId.BATTLEPAY, new CancelPurchase
			{
				IsAutoCancel = false,
				Reason = reason,
				DeviceId = deviceId,
				ErrorMessage = error
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE48 RID: 44616 RVA: 0x00363D9F File Offset: 0x00361F9F
		public void AckAchieveProgress(int achievementId, int ackProgress)
		{
			this.SendUtilPacket(243, UtilSystemId.CLIENT, new AckAchieveProgress
			{
				Id = achievementId,
				AckProgress = ackProgress
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE49 RID: 44617 RVA: 0x00363DC2 File Offset: 0x00361FC2
		public void AckQuest(int questId)
		{
			this.SendUtilPacket(604, UtilSystemId.CLIENT, new AckQuest
			{
				QuestId = questId
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE4A RID: 44618 RVA: 0x00363DDE File Offset: 0x00361FDE
		public void CheckForNewQuests()
		{
			this.SendUtilPacket(605, UtilSystemId.CLIENT, new CheckForNewQuests(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE4B RID: 44619 RVA: 0x00363DF3 File Offset: 0x00361FF3
		public void RerollQuest(int questId)
		{
			this.SendUtilPacket(606, UtilSystemId.CLIENT, new RerollQuest
			{
				QuestId = questId
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE4C RID: 44620 RVA: 0x00363E0F File Offset: 0x0036200F
		public void AckAchievement(int achievementId)
		{
			this.SendUtilPacket(612, UtilSystemId.CLIENT, new AckAchievement
			{
				AchievementId = achievementId
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE4D RID: 44621 RVA: 0x00363E2B File Offset: 0x0036202B
		public void ClaimAchievementReward(int achievementId, int chooseOneRewardId = 0)
		{
			this.SendUtilPacket(613, UtilSystemId.CLIENT, new ClaimAchievementReward
			{
				AchievementId = achievementId,
				ChooseOneRewardItemId = chooseOneRewardId
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE4E RID: 44622 RVA: 0x00363E4E File Offset: 0x0036204E
		public void AckRewardTrackReward(int rewardTrackId, int level, bool forPaidTrack)
		{
			this.SendUtilPacket(616, UtilSystemId.CLIENT, new AckRewardTrackReward
			{
				RewardTrackId = rewardTrackId,
				Level = level,
				ForPaidTrack = forPaidTrack
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE4F RID: 44623 RVA: 0x00363E78 File Offset: 0x00362078
		public void ClaimRewardTrackReward(int rewardTrackId, int level, bool forPaidTrack, int chooseOneRewardItemId)
		{
			this.SendUtilPacket(615, UtilSystemId.CLIENT, new ClaimRewardTrackReward
			{
				RewardTrackId = rewardTrackId,
				Level = level,
				ForPaidTrack = forPaidTrack,
				ChooseOneRewardItemId = chooseOneRewardItemId
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE50 RID: 44624 RVA: 0x00363EAA File Offset: 0x003620AA
		public void AckCardSeen(AckCardSeen ackCardSeenPacket)
		{
			this.SendUtilPacket(223, UtilSystemId.CLIENT, ackCardSeenPacket, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE51 RID: 44625 RVA: 0x00363EBB File Offset: 0x003620BB
		public void AckNotice(long noticeId)
		{
			this.SendUtilPacket(213, UtilSystemId.CLIENT, new AckNotice
			{
				Entry = noticeId
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE52 RID: 44626 RVA: 0x00363ED7 File Offset: 0x003620D7
		public void AcknowledgeBanner(int bannerId)
		{
			this.SendUtilPacket(309, UtilSystemId.CLIENT, new AcknowledgeBanner
			{
				Banner = bannerId
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE53 RID: 44627 RVA: 0x00363EF3 File Offset: 0x003620F3
		public void AckWingProgress(int wing, int ackProgress)
		{
			this.SendUtilPacket(308, UtilSystemId.CLIENT, new AckWingProgress
			{
				Wing = wing,
				Ack = ackProgress
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE54 RID: 44628 RVA: 0x00363F16 File Offset: 0x00362116
		public void BeginThirdPartyPurchase(string deviceId, BattlePayProvider provider, string productId, int quantity)
		{
			this.SendUtilPacket(312, UtilSystemId.BATTLEPAY, new StartThirdPartyPurchase
			{
				Provider = provider,
				ProductId = productId,
				Quantity = quantity,
				DeviceId = deviceId
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE55 RID: 44629 RVA: 0x00363F48 File Offset: 0x00362148
		public void BeginThirdPartyPurchaseWithReceipt(string deviceId, BattlePayProvider provider, string productId, int quantity, string thirdPartyId, string base64Receipt, string thirdPartyUserId)
		{
			ThirdPartyReceiptData danglingReceiptData = new ThirdPartyReceiptData
			{
				ThirdPartyId = thirdPartyId,
				Receipt = base64Receipt,
				ThirdPartyUserId = thirdPartyUserId
			};
			this.SendUtilPacket(312, UtilSystemId.BATTLEPAY, new StartThirdPartyPurchase
			{
				Provider = provider,
				ProductId = productId,
				Quantity = quantity,
				DeviceId = deviceId,
				DanglingReceiptData = danglingReceiptData
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE56 RID: 44630 RVA: 0x00363FAC File Offset: 0x003621AC
		public void BuyCard(PegasusShared.CardDef cardDef, int count, int unitBuyPrice, int currentCollectionCount)
		{
			BuySellCard buySellCard = new BuySellCard
			{
				Def = cardDef,
				Buying = true,
				UnitBuyPrice = unitBuyPrice,
				CurrentCollectionCount = currentCollectionCount
			};
			if (count != 1)
			{
				buySellCard.Count = count;
			}
			this.SendUtilPacket(257, UtilSystemId.CLIENT, buySellCard, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE57 RID: 44631 RVA: 0x00363FF6 File Offset: 0x003621F6
		public void CheckAccountLicenseAchieve(int achieveId)
		{
			this.SendUtilPacket(297, UtilSystemId.BATTLEPAY, new CheckAccountLicenseAchieve
			{
				Achieve = achieveId
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE58 RID: 44632 RVA: 0x00364012 File Offset: 0x00362212
		public void Close()
		{
			this.dispatcherImpl.Close();
			this.dispatcherImpl.DebugConnectionManager.Shutdown();
		}

		// Token: 0x0600AE59 RID: 44633 RVA: 0x0036402F File Offset: 0x0036222F
		public void Concede()
		{
			this.SendGamePacket(11, new Concede());
		}

		// Token: 0x0600AE5A RID: 44634 RVA: 0x0036403E File Offset: 0x0036223E
		public void ConfirmPurchase()
		{
			this.SendUtilPacket(273, UtilSystemId.BATTLEPAY, new DoPurchase(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE5B RID: 44635 RVA: 0x00364054 File Offset: 0x00362254
		public void CreateDeck(DeckType deckType, string name, int heroId, TAG_PREMIUM heroIsPremium, FormatType formatType, long sortOrder, DeckSourceType sourceType, string pastedDeckHash, long? fsgId, byte[] fsgSharedSecretKey, int brawlLibraryItemId, int? requestId)
		{
			CreateDeck createDeck = new CreateDeck
			{
				Name = name,
				Hero = heroId,
				HeroPremium = (int)heroIsPremium,
				DeckType = deckType,
				TaggedStandard = (formatType == FormatType.FT_STANDARD),
				SortOrder = sortOrder,
				SourceType = sourceType,
				PastedDeckHash = pastedDeckHash,
				FsgSharedSecretKey = fsgSharedSecretKey,
				BrawlLibraryItemId = brawlLibraryItemId,
				FormatType = formatType
			};
			if (fsgId != null)
			{
				createDeck.FsgId = fsgId.Value;
			}
			if (requestId != null)
			{
				createDeck.RequestId = requestId.Value;
			}
			this.SendUtilPacket(209, UtilSystemId.CLIENT, createDeck, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE5C RID: 44636 RVA: 0x003640FA File Offset: 0x003622FA
		public DeckCreated DeckCreated()
		{
			return this.UnpackNextUtilPacket<DeckCreated>(217);
		}

		// Token: 0x0600AE5D RID: 44637 RVA: 0x00364107 File Offset: 0x00362307
		public DeckDeleted DeckDeleted()
		{
			return this.UnpackNextUtilPacket<DeckDeleted>(218);
		}

		// Token: 0x0600AE5E RID: 44638 RVA: 0x00364114 File Offset: 0x00362314
		public DeckRenamed DeckRenamed()
		{
			return this.UnpackNextUtilPacket<DeckRenamed>(219);
		}

		// Token: 0x0600AE5F RID: 44639 RVA: 0x00364121 File Offset: 0x00362321
		public void DecodeAndProcessPacket(PegasusPacket packet)
		{
			if (this.dispatcherImpl.DecodePacket(packet) == null)
			{
				return;
			}
			this.dispatcherImpl.NotifyUtilResponseReceived(packet);
		}

		// Token: 0x0600AE60 RID: 44640 RVA: 0x0036413E File Offset: 0x0036233E
		public void DeleteDeck(long deckId, DeckType deckType)
		{
			this.SendUtilPacket(210, UtilSystemId.CLIENT, new DeleteDeck
			{
				Deck = deckId,
				DeckType = deckType
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE61 RID: 44641 RVA: 0x00364161 File Offset: 0x00362361
		public void DisconnectFromGameServer()
		{
			this.dispatcherImpl.DisconnectFromGameServer();
		}

		// Token: 0x0600AE62 RID: 44642 RVA: 0x0036416E File Offset: 0x0036236E
		public void DoLoginUpdate(string referralSource)
		{
			this.SendUtilPacket(205, UtilSystemId.CLIENT, new UpdateLogin
			{
				Referral = referralSource
			}, RequestPhase.STARTUP, 0);
		}

		// Token: 0x0600AE63 RID: 44643 RVA: 0x0036418A File Offset: 0x0036238A
		public void DraftAckRewards(long deckId, int slot)
		{
			this.SendUtilPacket(287, UtilSystemId.CLIENT, new DraftAckRewards
			{
				DeckId = deckId,
				Slot = slot
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE64 RID: 44644 RVA: 0x003641AD File Offset: 0x003623AD
		public void DraftBegin()
		{
			this.SendUtilPacket(235, UtilSystemId.CLIENT, new DraftBegin(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE65 RID: 44645 RVA: 0x003641C2 File Offset: 0x003623C2
		public DraftChosen GetDraftChosen()
		{
			return this.UnpackNextUtilPacket<DraftChosen>(249);
		}

		// Token: 0x0600AE66 RID: 44646 RVA: 0x003641CF File Offset: 0x003623CF
		public DraftBeginning GetDraftBeginning()
		{
			return this.UnpackNextUtilPacket<DraftBeginning>(246);
		}

		// Token: 0x0600AE67 RID: 44647 RVA: 0x003641DC File Offset: 0x003623DC
		public DraftChoicesAndContents GetDraftChoicesAndContents()
		{
			return this.UnpackNextUtilPacket<DraftChoicesAndContents>(248);
		}

		// Token: 0x0600AE68 RID: 44648 RVA: 0x003641E9 File Offset: 0x003623E9
		public DraftError DraftGetError()
		{
			return this.UnpackNextUtilPacket<DraftError>(251);
		}

		// Token: 0x0600AE69 RID: 44649 RVA: 0x003641F6 File Offset: 0x003623F6
		public void RequestDraftChoicesAndContents()
		{
			this.SendUtilPacket(244, UtilSystemId.CLIENT, new DraftGetChoicesAndContents(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE6A RID: 44650 RVA: 0x0036420B File Offset: 0x0036240B
		public void SendArenaSessionRequest()
		{
			this.SendUtilPacket(346, UtilSystemId.CLIENT, new ArenaSessionRequest(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE6B RID: 44651 RVA: 0x00364220 File Offset: 0x00362420
		public ArenaSessionResponse GetArenaSessionResponse()
		{
			return this.UnpackNextUtilPacket<ArenaSessionResponse>(351);
		}

		// Token: 0x0600AE6C RID: 44652 RVA: 0x0036422D File Offset: 0x0036242D
		public DraftRewardsAcked DraftRewardsAcked()
		{
			return this.UnpackNextUtilPacket<DraftRewardsAcked>(288);
		}

		// Token: 0x0600AE6D RID: 44653 RVA: 0x0036423A File Offset: 0x0036243A
		public void DraftMakePick(long deckId, int slot, int index, int premium)
		{
			this.SendUtilPacket(245, UtilSystemId.CLIENT, new DraftMakePick
			{
				DeckId = deckId,
				Slot = slot,
				Index = index,
				Premium = premium
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE6E RID: 44654 RVA: 0x0036426C File Offset: 0x0036246C
		public void DraftRetire(long deckId, int slot, int seasonId)
		{
			this.SendUtilPacket(242, UtilSystemId.CLIENT, new DraftRetire
			{
				DeckId = deckId,
				Slot = slot,
				SeasonId = seasonId
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE6F RID: 44655 RVA: 0x00364296 File Offset: 0x00362496
		public int DropAllDebugPackets()
		{
			return this.dispatcherImpl.DebugConnectionManager.DropAllPackets();
		}

		// Token: 0x0600AE70 RID: 44656 RVA: 0x003642A8 File Offset: 0x003624A8
		public int DropAllGamePackets()
		{
			return this.dispatcherImpl.DropAllGamePackets();
		}

		// Token: 0x0600AE71 RID: 44657 RVA: 0x003642B5 File Offset: 0x003624B5
		public int DropAllUtilPackets()
		{
			return this.dispatcherImpl.DropAllUtilPackets();
		}

		// Token: 0x0600AE72 RID: 44658 RVA: 0x003642C2 File Offset: 0x003624C2
		public void DropDebugPacket()
		{
			this.dispatcherImpl.DebugConnectionManager.DropPacket();
		}

		// Token: 0x0600AE73 RID: 44659 RVA: 0x003642D4 File Offset: 0x003624D4
		public void DropGamePacket()
		{
			this.dispatcherImpl.DropGamePacket();
		}

		// Token: 0x0600AE74 RID: 44660 RVA: 0x003642E1 File Offset: 0x003624E1
		public void DropUtilPacket()
		{
			this.dispatcherImpl.DropUtilPacket();
		}

		// Token: 0x0600AE75 RID: 44661 RVA: 0x003642EE File Offset: 0x003624EE
		public bool GameServerHasEvents()
		{
			return this.dispatcherImpl.GameServerHasEvents();
		}

		// Token: 0x0600AE76 RID: 44662 RVA: 0x003642FB File Offset: 0x003624FB
		public AccountLicenseAchieveResponse GetAccountLicenseAchieveResponse()
		{
			return this.UnpackNextUtilPacket<AccountLicenseAchieveResponse>(311);
		}

		// Token: 0x0600AE77 RID: 44663 RVA: 0x00364308 File Offset: 0x00362508
		public AccountLicensesInfoResponse GetAccountLicensesInfoResponse()
		{
			return this.UnpackNextUtilPacket<AccountLicensesInfoResponse>(325);
		}

		// Token: 0x0600AE78 RID: 44664 RVA: 0x00364315 File Offset: 0x00362515
		public AdventureProgressResponse GetAdventureProgressResponse()
		{
			return this.UnpackNextUtilPacket<AdventureProgressResponse>(306);
		}

		// Token: 0x0600AE79 RID: 44665 RVA: 0x00364322 File Offset: 0x00362522
		public void GetAllClientOptions()
		{
			this.SendUtilPacket(240, UtilSystemId.CLIENT, new GetOptions(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AE7A RID: 44666 RVA: 0x00364337 File Offset: 0x00362537
		public HeroXP GetHeroXP()
		{
			return this.UnpackNextUtilPacket<HeroXP>(283);
		}

		// Token: 0x0600AE7B RID: 44667 RVA: 0x00364344 File Offset: 0x00362544
		public GetAssetResponse GetAssetResponse()
		{
			return this.UnpackNextUtilPacket<GetAssetResponse>(322);
		}

		// Token: 0x0600AE7C RID: 44668 RVA: 0x00364351 File Offset: 0x00362551
		public AssetsVersionResponse GetAssetsVersionResponse()
		{
			return this.UnpackNextUtilPacket<AssetsVersionResponse>(304);
		}

		// Token: 0x0600AE7D RID: 44669 RVA: 0x0036435E File Offset: 0x0036255E
		public BattlePayConfigResponse GetBattlePayConfigResponse()
		{
			return this.UnpackNextUtilPacket<BattlePayConfigResponse>(238);
		}

		// Token: 0x0600AE7E RID: 44670 RVA: 0x0036436B File Offset: 0x0036256B
		public BattlePayStatusResponse GetBattlePayStatusResponse()
		{
			return this.UnpackNextUtilPacket<BattlePayStatusResponse>(265);
		}

		// Token: 0x0600AE7F RID: 44671 RVA: 0x00364378 File Offset: 0x00362578
		public CancelQuestResponse GetCanceledQuestResponse()
		{
			return this.UnpackNextUtilPacket<CancelQuestResponse>(282);
		}

		// Token: 0x0600AE80 RID: 44672 RVA: 0x00364385 File Offset: 0x00362585
		public SetFavoriteCardBackResponse GetSetFavoriteCardBackResponse()
		{
			return this.UnpackNextUtilPacket<SetFavoriteCardBackResponse>(292);
		}

		// Token: 0x0600AE81 RID: 44673 RVA: 0x00364392 File Offset: 0x00362592
		public CardBacks GetCardBacks()
		{
			return this.UnpackNextUtilPacket<CardBacks>(236);
		}

		// Token: 0x0600AE82 RID: 44674 RVA: 0x0036439F File Offset: 0x0036259F
		public BoughtSoldCard GetCardSaleResult()
		{
			return this.UnpackNextUtilPacket<BoughtSoldCard>(258);
		}

		// Token: 0x0600AE83 RID: 44675 RVA: 0x003643AC File Offset: 0x003625AC
		public CardValues GetCardValues()
		{
			return this.UnpackNextUtilPacket<CardValues>(260);
		}

		// Token: 0x0600AE84 RID: 44676 RVA: 0x003643B9 File Offset: 0x003625B9
		public ClientStaticAssetsResponse GetClientStaticAssetsResponse()
		{
			return this.UnpackNextUtilPacket<ClientStaticAssetsResponse>(341);
		}

		// Token: 0x0600AE85 RID: 44677 RVA: 0x003643C6 File Offset: 0x003625C6
		public InitialClientState GetInitialClientState()
		{
			return this.UnpackNextUtilPacket<InitialClientState>(207);
		}

		// Token: 0x0600AE86 RID: 44678 RVA: 0x003643D3 File Offset: 0x003625D3
		public DBAction GetDbAction()
		{
			return this.UnpackNextUtilPacket<DBAction>(216);
		}

		// Token: 0x0600AE87 RID: 44679 RVA: 0x003643E0 File Offset: 0x003625E0
		public Deadend GetDeadendGame()
		{
			return this.UnpackNextGamePacket<Deadend>(169);
		}

		// Token: 0x0600AE88 RID: 44680 RVA: 0x003643ED File Offset: 0x003625ED
		public DeadendUtil GetDeadendUtil()
		{
			return this.UnpackNextUtilPacket<DeadendUtil>(167);
		}

		// Token: 0x0600AE89 RID: 44681 RVA: 0x003643FA File Offset: 0x003625FA
		public DebugCommandResponse GetDebugCommandResponse()
		{
			return this.UnpackNextUtilPacket<DebugCommandResponse>(324);
		}

		// Token: 0x0600AE8A RID: 44682 RVA: 0x00364407 File Offset: 0x00362607
		public DebugConsoleCommand GetDebugConsoleCommand()
		{
			if (!this.dispatcherImpl.DebugConnectionManager.AllowDebugConnections())
			{
				return null;
			}
			return this.UnpackNextDebugPacket<DebugConsoleCommand>(123);
		}

		// Token: 0x0600AE8B RID: 44683 RVA: 0x00364425 File Offset: 0x00362625
		public DebugConsoleResponse GetDebugConsoleResponse()
		{
			if (!this.dispatcherImpl.DebugConnectionManager.AllowDebugConnections())
			{
				return null;
			}
			return this.UnpackNextGamePacket<DebugConsoleResponse>(124);
		}

		// Token: 0x0600AE8C RID: 44684 RVA: 0x00364443 File Offset: 0x00362643
		public GetDeckContentsResponse GetDeckContentsResponse()
		{
			return this.UnpackNextUtilPacket<GetDeckContentsResponse>(215);
		}

		// Token: 0x0600AE8D RID: 44685 RVA: 0x00364450 File Offset: 0x00362650
		public FreeDeckChoiceResponse GetFreeDeckChoiceResponse()
		{
			return this.UnpackNextUtilPacket<FreeDeckChoiceResponse>(334);
		}

		// Token: 0x0600AE8E RID: 44686 RVA: 0x0036445D File Offset: 0x0036265D
		public DeckList GetDeckHeaders()
		{
			return this.UnpackNextUtilPacket<DeckList>(202);
		}

		// Token: 0x0600AE8F RID: 44687 RVA: 0x0036446A File Offset: 0x0036266A
		public ProfileDeckLimit GetDeckLimit()
		{
			return this.UnpackNextUtilPacket<ProfileDeckLimit>(231);
		}

		// Token: 0x0600AE90 RID: 44688 RVA: 0x00364477 File Offset: 0x00362677
		public DraftRetired GetDraftRetired()
		{
			return this.UnpackNextUtilPacket<DraftRetired>(247);
		}

		// Token: 0x0600AE91 RID: 44689 RVA: 0x00364484 File Offset: 0x00362684
		public DraftRemovePremiumsResponse GetDraftDisablePremiumsResponse()
		{
			return this.UnpackNextUtilPacket<DraftRemovePremiumsResponse>(355);
		}

		// Token: 0x0600AE92 RID: 44690 RVA: 0x00364491 File Offset: 0x00362691
		public EntitiesChosen GetEntitiesChosen()
		{
			return this.UnpackNextGamePacket<EntitiesChosen>(13);
		}

		// Token: 0x0600AE93 RID: 44691 RVA: 0x0036449B File Offset: 0x0036269B
		public EntityChoices GetEntityChoices()
		{
			return this.UnpackNextGamePacket<EntityChoices>(17);
		}

		// Token: 0x0600AE94 RID: 44692 RVA: 0x003644A5 File Offset: 0x003626A5
		public FavoriteHeroesResponse GetFavoriteHeroesResponse()
		{
			return this.UnpackNextUtilPacket<FavoriteHeroesResponse>(318);
		}

		// Token: 0x0600AE95 RID: 44693 RVA: 0x003644B2 File Offset: 0x003626B2
		public GuardianVars GetGuardianVars()
		{
			return this.UnpackNextUtilPacket<GuardianVars>(264);
		}

		// Token: 0x0600AE96 RID: 44694 RVA: 0x003644BF File Offset: 0x003626BF
		public GameCanceled GetGameCancelInfo()
		{
			return this.UnpackNextGamePacket<GameCanceled>(12);
		}

		// Token: 0x0600AE97 RID: 44695 RVA: 0x003644C9 File Offset: 0x003626C9
		public GameSetup GetGameSetup()
		{
			return this.UnpackNextGamePacket<GameSetup>(16);
		}

		// Token: 0x0600AE98 RID: 44696 RVA: 0x003644D3 File Offset: 0x003626D3
		public GamesInfo GetGamesInfo()
		{
			return this.UnpackNextUtilPacket<GamesInfo>(208);
		}

		// Token: 0x0600AE99 RID: 44697 RVA: 0x003644E0 File Offset: 0x003626E0
		public GameStartState GetGameStartState()
		{
			return this.dispatcherImpl.GameStartState;
		}

		// Token: 0x0600AE9A RID: 44698 RVA: 0x003644ED File Offset: 0x003626ED
		public void SetGameStartState(GameStartState state)
		{
			this.dispatcherImpl.GameStartState = state;
		}

		// Token: 0x0600AE9B RID: 44699 RVA: 0x003644FB File Offset: 0x003626FB
		public void GetGameState()
		{
			this.SendGamePacket(1, new GetGameState());
		}

		// Token: 0x0600AE9C RID: 44700 RVA: 0x00364509 File Offset: 0x00362709
		public void UpdateBattlegroundInfo()
		{
			this.SendGamePacket(53, new UpdateBattlegroundInfo());
		}

		// Token: 0x0600AE9D RID: 44701 RVA: 0x00364518 File Offset: 0x00362718
		public void RequestGameRoundHistory()
		{
			this.SendGamePacket(32, new GetGameRoundHistory());
		}

		// Token: 0x0600AE9E RID: 44702 RVA: 0x00364527 File Offset: 0x00362727
		public void RequestRealtimeBattlefieldRaces()
		{
			this.SendGamePacket(33, new GetGameRealTimeBattlefieldRaces());
		}

		// Token: 0x0600AE9F RID: 44703 RVA: 0x00364536 File Offset: 0x00362736
		public GenericResponse GetGenericResponse()
		{
			return this.UnpackNextUtilPacket<GenericResponse>(326);
		}

		// Token: 0x0600AEA0 RID: 44704 RVA: 0x00364543 File Offset: 0x00362743
		public MassDisenchantResponse GetMassDisenchantResponse()
		{
			return this.UnpackNextUtilPacket<MassDisenchantResponse>(269);
		}

		// Token: 0x0600AEA1 RID: 44705 RVA: 0x00364550 File Offset: 0x00362750
		public MedalInfo GetMedalInfo()
		{
			return this.UnpackNextUtilPacket<MedalInfo>(232);
		}

		// Token: 0x0600AEA2 RID: 44706 RVA: 0x0036455D File Offset: 0x0036275D
		public NAckOption GetNAckOption()
		{
			return this.UnpackNextGamePacket<NAckOption>(10);
		}

		// Token: 0x0600AEA3 RID: 44707 RVA: 0x00364567 File Offset: 0x00362767
		public ClientStateNotification GetClientStateNotification()
		{
			return this.UnpackNextUtilPacket<ClientStateNotification>(333);
		}

		// Token: 0x0600AEA4 RID: 44708 RVA: 0x00364574 File Offset: 0x00362774
		public BoosterContent GetOpenedBooster()
		{
			return this.UnpackNextUtilPacket<BoosterContent>(226);
		}

		// Token: 0x0600AEA5 RID: 44709 RVA: 0x00364581 File Offset: 0x00362781
		public AllOptions GetAllOptions()
		{
			return this.UnpackNextGamePacket<AllOptions>(14);
		}

		// Token: 0x0600AEA6 RID: 44710 RVA: 0x0036458B File Offset: 0x0036278B
		public PlayerQuestStateUpdate GetPlayerQuestStateUpdate()
		{
			return this.UnpackNextUtilPacket<PlayerQuestStateUpdate>(601);
		}

		// Token: 0x0600AEA7 RID: 44711 RVA: 0x00364598 File Offset: 0x00362798
		public PlayerQuestPoolStateUpdate GetPlayerQuestPoolStateUpdate()
		{
			return this.UnpackNextUtilPacket<PlayerQuestPoolStateUpdate>(602);
		}

		// Token: 0x0600AEA8 RID: 44712 RVA: 0x003645A5 File Offset: 0x003627A5
		public PlayerAchievementStateUpdate GetPlayerAchievementStateUpdate()
		{
			return this.UnpackNextUtilPacket<PlayerAchievementStateUpdate>(603);
		}

		// Token: 0x0600AEA9 RID: 44713 RVA: 0x003645B2 File Offset: 0x003627B2
		public PlayerRewardTrackStateUpdate GetPlayerRewardTrackStateUpdate()
		{
			return this.UnpackNextUtilPacket<PlayerRewardTrackStateUpdate>(614);
		}

		// Token: 0x0600AEAA RID: 44714 RVA: 0x003645BF File Offset: 0x003627BF
		public RewardTrackXpNotification GetRewardTrackXpNotification()
		{
			return this.UnpackNextUtilPacket<RewardTrackXpNotification>(617);
		}

		// Token: 0x0600AEAB RID: 44715 RVA: 0x003645CC File Offset: 0x003627CC
		public RewardTrackUnclaimedNotification GetRewardTrackUnclaimedNotification()
		{
			return this.UnpackNextUtilPacket<RewardTrackUnclaimedNotification>(619);
		}

		// Token: 0x0600AEAC RID: 44716 RVA: 0x003645D9 File Offset: 0x003627D9
		public RerollQuestResponse GetRerollQuestResponse()
		{
			return this.UnpackNextUtilPacket<RerollQuestResponse>(607);
		}

		// Token: 0x0600AEAD RID: 44717 RVA: 0x003645E6 File Offset: 0x003627E6
		public PlayerRecords GetPlayerRecords()
		{
			return this.UnpackNextUtilPacket<PlayerRecords>(270);
		}

		// Token: 0x0600AEAE RID: 44718 RVA: 0x003645F3 File Offset: 0x003627F3
		public PowerHistory GetPowerHistory()
		{
			return this.UnpackNextGamePacket<PowerHistory>(19);
		}

		// Token: 0x0600AEAF RID: 44719 RVA: 0x003645FD File Offset: 0x003627FD
		public ProcessRecruitAFriendResponse GetProcessRecruitAFriendResponse()
		{
			return this.UnpackNextUtilPacket<ProcessRecruitAFriendResponse>(342);
		}

		// Token: 0x0600AEB0 RID: 44720 RVA: 0x0036460A File Offset: 0x0036280A
		public ProfileProgress GetProfileProgress()
		{
			return this.UnpackNextUtilPacket<ProfileProgress>(233);
		}

		// Token: 0x0600AEB1 RID: 44721 RVA: 0x00364617 File Offset: 0x00362817
		public CancelPurchaseResponse GetCancelPurchaseResponse()
		{
			return this.UnpackNextUtilPacket<CancelPurchaseResponse>(275);
		}

		// Token: 0x0600AEB2 RID: 44722 RVA: 0x00364624 File Offset: 0x00362824
		public PurchaseMethod GetPurchaseMethodResponse()
		{
			return this.UnpackNextUtilPacket<PurchaseMethod>(272);
		}

		// Token: 0x0600AEB3 RID: 44723 RVA: 0x00364631 File Offset: 0x00362831
		public PurchaseResponse GetPurchaseResponse()
		{
			return this.UnpackNextUtilPacket<PurchaseResponse>(256);
		}

		// Token: 0x0600AEB4 RID: 44724 RVA: 0x0036463E File Offset: 0x0036283E
		public PurchaseWithGoldResponse GetPurchaseWithGoldResponse()
		{
			return this.UnpackNextUtilPacket<PurchaseWithGoldResponse>(280);
		}

		// Token: 0x0600AEB5 RID: 44725 RVA: 0x0036464B File Offset: 0x0036284B
		public RecruitAFriendDataResponse GetRecruitAFriendDataResponse()
		{
			return this.UnpackNextUtilPacket<RecruitAFriendDataResponse>(338);
		}

		// Token: 0x0600AEB6 RID: 44726 RVA: 0x00364658 File Offset: 0x00362858
		public RecruitAFriendURLResponse GetRecruitAFriendUrlResponse()
		{
			return this.UnpackNextUtilPacket<RecruitAFriendURLResponse>(336);
		}

		// Token: 0x0600AEB7 RID: 44727 RVA: 0x00364665 File Offset: 0x00362865
		public RewardProgress GetRewardProgress()
		{
			return this.UnpackNextUtilPacket<RewardProgress>(271);
		}

		// Token: 0x0600AEB8 RID: 44728 RVA: 0x00364672 File Offset: 0x00362872
		public ServerResult GetServerResult()
		{
			return this.UnpackNextGamePacket<ServerResult>(23);
		}

		// Token: 0x0600AEB9 RID: 44729 RVA: 0x0036467C File Offset: 0x0036287C
		public SetFavoriteHeroResponse GetSetFavoriteHeroResponse()
		{
			return this.UnpackNextUtilPacket<SetFavoriteHeroResponse>(320);
		}

		// Token: 0x0600AEBA RID: 44730 RVA: 0x00364689 File Offset: 0x00362889
		public SetProgressResponse GetSetProgressResponse()
		{
			return this.UnpackNextUtilPacket<SetProgressResponse>(296);
		}

		// Token: 0x0600AEBB RID: 44731 RVA: 0x00364696 File Offset: 0x00362896
		public SpectatorNotify GetSpectatorNotify()
		{
			return this.UnpackNextGamePacket<SpectatorNotify>(24);
		}

		// Token: 0x0600AEBC RID: 44732 RVA: 0x003646A0 File Offset: 0x003628A0
		public AIDebugInformation GetAIDebugInformation()
		{
			return this.UnpackNextGamePacket<AIDebugInformation>(6);
		}

		// Token: 0x0600AEBD RID: 44733 RVA: 0x003646A9 File Offset: 0x003628A9
		public RopeTimerDebugInformation GetRopeTimerDebugInformation()
		{
			return this.UnpackNextGamePacket<RopeTimerDebugInformation>(8);
		}

		// Token: 0x0600AEBE RID: 44734 RVA: 0x003646B2 File Offset: 0x003628B2
		public ScriptDebugInformation GetScriptDebugInformation()
		{
			return this.UnpackNextGamePacket<ScriptDebugInformation>(7);
		}

		// Token: 0x0600AEBF RID: 44735 RVA: 0x003646BB File Offset: 0x003628BB
		public GameRoundHistory GetGameRoundHistory()
		{
			return this.UnpackNextGamePacket<GameRoundHistory>(30);
		}

		// Token: 0x0600AEC0 RID: 44736 RVA: 0x003646C5 File Offset: 0x003628C5
		public GameRealTimeBattlefieldRaces GetGameRealTimeBattlefieldRaces()
		{
			return this.UnpackNextGamePacket<GameRealTimeBattlefieldRaces>(31);
		}

		// Token: 0x0600AEC1 RID: 44737 RVA: 0x003646CF File Offset: 0x003628CF
		public BattlegroundsRatingChange GetBattlegroundsRatingChange()
		{
			return this.UnpackNextGamePacket<BattlegroundsRatingChange>(34);
		}

		// Token: 0x0600AEC2 RID: 44738 RVA: 0x003646DC File Offset: 0x003628DC
		public void SendPVPDRSessionStartRequest(bool paidEntry)
		{
			this.SendUtilPacket(382, UtilSystemId.CLIENT, new PVPDRSessionStartRequest
			{
				PaidEntry = paidEntry
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AEC3 RID: 44739 RVA: 0x00364705 File Offset: 0x00362905
		public PVPDRSessionStartResponse GetPVPDRSessionStartResponse()
		{
			return this.UnpackNextUtilPacket<PVPDRSessionStartResponse>(383);
		}

		// Token: 0x0600AEC4 RID: 44740 RVA: 0x00364712 File Offset: 0x00362912
		public void SendPVPDRSessionEndRequest()
		{
			this.SendUtilPacket(388, UtilSystemId.CLIENT, new PVPDRSessionEndRequest(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AEC5 RID: 44741 RVA: 0x00364727 File Offset: 0x00362927
		public PVPDRSessionEndResponse GetPVPDRSessionEndResponse()
		{
			return this.UnpackNextUtilPacket<PVPDRSessionEndResponse>(389);
		}

		// Token: 0x0600AEC6 RID: 44742 RVA: 0x00364734 File Offset: 0x00362934
		public void SendPVPDRSessionInfoRequest()
		{
			this.SendUtilPacket(376, UtilSystemId.CLIENT, new PVPDRSessionInfoRequest(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AEC7 RID: 44743 RVA: 0x00364749 File Offset: 0x00362949
		public PVPDRSessionInfoResponse GetPVPDRSessionInfoResponse()
		{
			return this.UnpackNextUtilPacket<PVPDRSessionInfoResponse>(377);
		}

		// Token: 0x0600AEC8 RID: 44744 RVA: 0x00364756 File Offset: 0x00362956
		public void SendPVPDRRetireRequest()
		{
			this.SendUtilPacket(380, UtilSystemId.CLIENT, new PVPDRRetireRequest(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AEC9 RID: 44745 RVA: 0x0036476B File Offset: 0x0036296B
		public PVPDRRetireResponse GetPVPDRRetireResponse()
		{
			return this.UnpackNextUtilPacket<PVPDRRetireResponse>(381);
		}

		// Token: 0x0600AECA RID: 44746 RVA: 0x00364778 File Offset: 0x00362978
		public GameGuardianVars GetGameGuardianVars()
		{
			return this.UnpackNextGamePacket<GameGuardianVars>(35);
		}

		// Token: 0x0600AECB RID: 44747 RVA: 0x00364782 File Offset: 0x00362982
		public UpdateBattlegroundInfo GetBattlegroundInfo()
		{
			return this.UnpackNextGamePacket<UpdateBattlegroundInfo>(53);
		}

		// Token: 0x0600AECC RID: 44748 RVA: 0x0036478C File Offset: 0x0036298C
		public DebugMessage GetDebugMessage()
		{
			return this.UnpackNextGamePacket<DebugMessage>(5);
		}

		// Token: 0x0600AECD RID: 44749 RVA: 0x00364795 File Offset: 0x00362995
		public ScriptLogMessage GetScriptLogMessage()
		{
			return this.UnpackNextGamePacket<ScriptLogMessage>(50);
		}

		// Token: 0x0600AECE RID: 44750 RVA: 0x0036479F File Offset: 0x0036299F
		public SubscribeResponse GetSubscribeResponse()
		{
			return this.UnpackNextUtilPacket<SubscribeResponse>(315);
		}

		// Token: 0x0600AECF RID: 44751 RVA: 0x003647AC File Offset: 0x003629AC
		public TavernBrawlInfo GetTavernBrawlInfo()
		{
			return this.UnpackNextUtilPacket<TavernBrawlInfo>(316);
		}

		// Token: 0x0600AED0 RID: 44752 RVA: 0x003647B9 File Offset: 0x003629B9
		public TavernBrawlPlayerRecordResponse GeTavernBrawlPlayerRecordResponse()
		{
			return this.UnpackNextUtilPacket<TavernBrawlPlayerRecordResponse>(317);
		}

		// Token: 0x0600AED1 RID: 44753 RVA: 0x003647C6 File Offset: 0x003629C6
		public void GetThirdPartyPurchaseStatus(string transactionId)
		{
			this.SendUtilPacket(294, UtilSystemId.BATTLEPAY, new GetThirdPartyPurchaseStatus
			{
				ThirdPartyId = transactionId
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AED2 RID: 44754 RVA: 0x003647E2 File Offset: 0x003629E2
		public ThirdPartyPurchaseStatusResponse GetThirdPartyPurchaseStatusResponse()
		{
			return this.UnpackNextUtilPacket<ThirdPartyPurchaseStatusResponse>(295);
		}

		// Token: 0x0600AED3 RID: 44755 RVA: 0x003647EF File Offset: 0x003629EF
		public TriggerEventResponse GetTriggerEventResponse()
		{
			return this.UnpackNextUtilPacket<TriggerEventResponse>(299);
		}

		// Token: 0x0600AED4 RID: 44756 RVA: 0x003647FC File Offset: 0x003629FC
		public PegasusGame.TurnTimer GetTurnTimerInfo()
		{
			return this.UnpackNextGamePacket<PegasusGame.TurnTimer>(9);
		}

		// Token: 0x0600AED5 RID: 44757 RVA: 0x00364806 File Offset: 0x00362A06
		public UpdateAccountLicensesResponse GetUpdateAccountLicensesResponse()
		{
			return this.UnpackNextUtilPacket<UpdateAccountLicensesResponse>(331);
		}

		// Token: 0x0600AED6 RID: 44758 RVA: 0x00364813 File Offset: 0x00362A13
		public UpdateLoginComplete GetUpdateLoginComplete()
		{
			return this.UnpackNextUtilPacket<UpdateLoginComplete>(307);
		}

		// Token: 0x0600AED7 RID: 44759 RVA: 0x00364820 File Offset: 0x00362A20
		public UserUI GetUserUi()
		{
			return this.UnpackNextGamePacket<UserUI>(15);
		}

		// Token: 0x0600AED8 RID: 44760 RVA: 0x0036482A File Offset: 0x00362A2A
		public ValidateAchieveResponse GetValidateAchieveResponse()
		{
			return this.UnpackNextUtilPacket<ValidateAchieveResponse>(285);
		}

		// Token: 0x0600AED9 RID: 44761 RVA: 0x00364837 File Offset: 0x00362A37
		public Coins GetCoins()
		{
			return this.UnpackNextUtilPacket<Coins>(608);
		}

		// Token: 0x0600AEDA RID: 44762 RVA: 0x00364844 File Offset: 0x00362A44
		public void SetFavoriteCoin(int coin)
		{
			this.SendUtilPacket(609, UtilSystemId.CLIENT, new SetFavoriteCoin
			{
				Coin = coin
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AEDB RID: 44763 RVA: 0x00364860 File Offset: 0x00362A60
		public UtilLogRelay GetUtilLogRelay()
		{
			return this.UnpackNextUtilPacket<UtilLogRelay>(390);
		}

		// Token: 0x0600AEDC RID: 44764 RVA: 0x0036486D File Offset: 0x00362A6D
		public GameLogRelay GetGameLogRelay()
		{
			return this.UnpackNextGamePacket<GameLogRelay>(51);
		}

		// Token: 0x0600AEDD RID: 44765 RVA: 0x00364877 File Offset: 0x00362A77
		public AchievementProgress GetAchievementInGameProgress()
		{
			return this.UnpackNextGamePacket<AchievementProgress>(52);
		}

		// Token: 0x0600AEDE RID: 44766 RVA: 0x00364881 File Offset: 0x00362A81
		public AchievementComplete GetAchievementComplete()
		{
			return this.UnpackNextUtilPacket<AchievementComplete>(618);
		}

		// Token: 0x0600AEDF RID: 44767 RVA: 0x0036488E File Offset: 0x00362A8E
		public SetFavoriteCoinResponse GetSetFavoriteCoinResponse()
		{
			return this.UnpackNextUtilPacket<SetFavoriteCoinResponse>(610);
		}

		// Token: 0x0600AEE0 RID: 44768 RVA: 0x0036489B File Offset: 0x00362A9B
		public CoinUpdate GetCoinUpdate()
		{
			return this.UnpackNextUtilPacket<CoinUpdate>(611);
		}

		// Token: 0x0600AEE1 RID: 44769 RVA: 0x003648A8 File Offset: 0x00362AA8
		public bool GotoGameServer(string address, uint port)
		{
			return this.dispatcherImpl.ConnectToGameServer(address, port);
		}

		// Token: 0x0600AEE2 RID: 44770 RVA: 0x003648B7 File Offset: 0x00362AB7
		public void RegisterGameServerConnectEventListener(Action<BattleNetErrors> listener)
		{
			this.dispatcherImpl.OnGameServerConnectEvent += listener;
		}

		// Token: 0x0600AEE3 RID: 44771 RVA: 0x003648C5 File Offset: 0x00362AC5
		public void RemoveGameServerConnectEventListener(Action<BattleNetErrors> listener)
		{
			this.dispatcherImpl.OnGameServerConnectEvent -= listener;
		}

		// Token: 0x0600AEE4 RID: 44772 RVA: 0x003648D3 File Offset: 0x00362AD3
		public void RegisterGameServerDisconnectEventListener(Action<BattleNetErrors> listener)
		{
			this.dispatcherImpl.OnGameServerDisconnectEvent += listener;
		}

		// Token: 0x0600AEE5 RID: 44773 RVA: 0x003648E1 File Offset: 0x00362AE1
		public void RemoveGameServerDisconnectEventListener(Action<BattleNetErrors> listener)
		{
			this.dispatcherImpl.OnGameServerDisconnectEvent -= listener;
		}

		// Token: 0x0600AEE6 RID: 44774 RVA: 0x003648EF File Offset: 0x00362AEF
		public void SendSpectatorGameHandshake(string version, Platform platform, GameServerInfo info, BnetId bnetId)
		{
			this.SendGamePacket(22, new SpectatorHandshake
			{
				GameHandle = (int)info.GameHandle,
				Password = info.SpectatorPassword,
				Version = version,
				Platform = platform,
				GameAccountId = bnetId
			});
		}

		// Token: 0x0600AEE7 RID: 44775 RVA: 0x0036492C File Offset: 0x00362B2C
		public void SendGameHandshake(GameServerInfo info, Platform platform)
		{
			this.SendGamePacket(168, new Handshake
			{
				Password = info.AuroraPassword,
				GameHandle = (int)info.GameHandle,
				ClientHandle = (long)((int)info.ClientHandle),
				Mission = info.Mission,
				Version = info.Version,
				Platform = platform
			});
		}

		// Token: 0x0600AEE8 RID: 44776 RVA: 0x0036498E File Offset: 0x00362B8E
		public bool HasErrors()
		{
			return this.dispatcherImpl.HasUtilErrors();
		}

		// Token: 0x0600AEE9 RID: 44777 RVA: 0x0036499B File Offset: 0x00362B9B
		public bool HasDebugPackets()
		{
			return this.dispatcherImpl.DebugConnectionManager.HaveDebugPackets();
		}

		// Token: 0x0600AEEA RID: 44778 RVA: 0x003649AD File Offset: 0x00362BAD
		public bool HasGamePackets()
		{
			return this.dispatcherImpl.HasGamePackets();
		}

		// Token: 0x0600AEEB RID: 44779 RVA: 0x003649BA File Offset: 0x00362BBA
		public bool HasGameServerConnection()
		{
			return this.dispatcherImpl.HasGameServerConnection();
		}

		// Token: 0x0600AEEC RID: 44780 RVA: 0x003649C7 File Offset: 0x00362BC7
		public bool HasUtilPackets()
		{
			return this.dispatcherImpl.HasUtilPackets();
		}

		// Token: 0x0600AEED RID: 44781 RVA: 0x003649D4 File Offset: 0x00362BD4
		public bool IsConnectedToGameServer()
		{
			return this.dispatcherImpl.IsConnectedToGameServer();
		}

		// Token: 0x0600AEEE RID: 44782 RVA: 0x003649E1 File Offset: 0x00362BE1
		public void UpdateGameServerConnection()
		{
			this.dispatcherImpl.ProcessGamePackets();
		}

		// Token: 0x0600AEEF RID: 44783 RVA: 0x003649EE File Offset: 0x00362BEE
		public void ProcessUtilPackets()
		{
			this.dispatcherImpl.ProcessUtilPackets();
		}

		// Token: 0x0600AEF0 RID: 44784 RVA: 0x003649FB File Offset: 0x00362BFB
		public bool TryConnectDebugConsole()
		{
			return this.dispatcherImpl.DebugConnectionManager.TryConnectDebugConsole();
		}

		// Token: 0x0600AEF1 RID: 44785 RVA: 0x00364A0D File Offset: 0x00362C0D
		public void UpdateDebugConsole()
		{
			this.dispatcherImpl.DebugConnectionManager.Update();
		}

		// Token: 0x0600AEF2 RID: 44786 RVA: 0x00364A1F File Offset: 0x00362C1F
		public void MassDisenchant()
		{
			this.SendUtilPacket(268, UtilSystemId.CLIENT, new MassDisenchantRequest(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AEF3 RID: 44787 RVA: 0x00364A34 File Offset: 0x00362C34
		public PegasusPacket NextDebugPacket()
		{
			return this.dispatcherImpl.DebugConnectionManager.NextDebugPacket();
		}

		// Token: 0x0600AEF4 RID: 44788 RVA: 0x00364A46 File Offset: 0x00362C46
		public int NextDebugPacketType()
		{
			return this.dispatcherImpl.DebugConnectionManager.NextDebugConsoleType();
		}

		// Token: 0x0600AEF5 RID: 44789 RVA: 0x00364A58 File Offset: 0x00362C58
		public PegasusPacket NextGamePacket()
		{
			return this.dispatcherImpl.NextGamePacket();
		}

		// Token: 0x0600AEF6 RID: 44790 RVA: 0x00364A65 File Offset: 0x00362C65
		public int NextGamePacketType()
		{
			return this.dispatcherImpl.NextGameType();
		}

		// Token: 0x0600AEF7 RID: 44791 RVA: 0x00364A72 File Offset: 0x00362C72
		public ResponseWithRequest NextUtilPacket()
		{
			return this.dispatcherImpl.NextUtilPacket();
		}

		// Token: 0x0600AEF8 RID: 44792 RVA: 0x00364A7F File Offset: 0x00362C7F
		public int NextUtilPacketType()
		{
			return this.dispatcherImpl.NextUtilType();
		}

		// Token: 0x0600AEF9 RID: 44793 RVA: 0x00364A8C File Offset: 0x00362C8C
		public void OnDebugPacketReceived(PegasusPacket packet)
		{
			this.dispatcherImpl.DebugConnectionManager.OnPacketReceived(packet);
		}

		// Token: 0x0600AEFA RID: 44794 RVA: 0x00364A9F File Offset: 0x00362C9F
		public void PushReceivedGamePacket(PegasusPacket packet)
		{
			this.dispatcherImpl.OnGamePacketReceived(packet, packet.Type);
		}

		// Token: 0x0600AEFB RID: 44795 RVA: 0x00364AB3 File Offset: 0x00362CB3
		public void OnGamePacketReceived(PegasusPacket packet, int packetTypeId)
		{
			this.dispatcherImpl.OnGamePacketReceived(packet, packetTypeId);
		}

		// Token: 0x0600AEFC RID: 44796 RVA: 0x00364AC2 File Offset: 0x00362CC2
		public void OnLoginComplete()
		{
			this.dispatcherImpl.OnLoginComplete();
		}

		// Token: 0x0600AEFD RID: 44797 RVA: 0x00364ACF File Offset: 0x00362CCF
		public void OnLoginStarted()
		{
			this.dispatcherImpl.DebugConnectionManager.OnLoginStarted();
		}

		// Token: 0x0600AEFE RID: 44798 RVA: 0x00364AE1 File Offset: 0x00362CE1
		public void OnUtilPacketReceived(PegasusPacket packet, int packetTypeId)
		{
			this.dispatcherImpl.OnUtilPacketReceived(packet, packetTypeId);
		}

		// Token: 0x0600AEFF RID: 44799 RVA: 0x00364AF0 File Offset: 0x00362CF0
		public void OnStartupPacketSequenceComplete()
		{
			this.dispatcherImpl.OnStartupPacketSequenceComplete();
		}

		// Token: 0x0600AF00 RID: 44800 RVA: 0x00364AFD File Offset: 0x00362CFD
		public void OpenBooster(int boosterTypeId, long fsgId)
		{
			this.SendUtilPacket(225, UtilSystemId.CLIENT, new OpenBooster
			{
				BoosterType = boosterTypeId,
				FsgId = fsgId
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF01 RID: 44801 RVA: 0x00364B20 File Offset: 0x00362D20
		public void PurchaseViaGold(int quantity, ProductType product, int data)
		{
			this.SendUtilPacket(279, UtilSystemId.CLIENT, new PurchaseWithGold
			{
				Product = product,
				Quantity = quantity,
				Data = data
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF02 RID: 44802 RVA: 0x00364B4A File Offset: 0x00362D4A
		public void RenameDeck(long deckId, string name)
		{
			this.SendUtilPacket(211, UtilSystemId.CLIENT, new RenameDeck
			{
				Deck = deckId,
				Name = name
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF03 RID: 44803 RVA: 0x00364B6D File Offset: 0x00362D6D
		public void RequestAccountLicensesUpdate()
		{
			this.SendUtilPacket(276, UtilSystemId.BATTLEPAY, new UpdateAccountLicenses(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF04 RID: 44804 RVA: 0x00364B82 File Offset: 0x00362D82
		public void RequestAdventureProgress()
		{
			this.SendUtilPacket(305, UtilSystemId.CLIENT, new GetAdventureProgress(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF05 RID: 44805 RVA: 0x00364B97 File Offset: 0x00362D97
		public void RequestAssetsVersion(Platform platform, long cachedCollectionVersion, List<GetAssetsVersion.DeckModificationTimes> deckTimes, long cachedCollectionVersionLastModified)
		{
			this.SendUtilPacket(303, UtilSystemId.CLIENT, new GetAssetsVersion
			{
				Platform = platform,
				ClientCollectionVersion = cachedCollectionVersion,
				CachedDeckModificationTimes = deckTimes,
				CollectionVersionLastModified = cachedCollectionVersionLastModified
			}, RequestPhase.STARTUP, 0);
		}

		// Token: 0x0600AF06 RID: 44806 RVA: 0x00364BC9 File Offset: 0x00362DC9
		public void RequestCancelQuest(int questId)
		{
			this.SendUtilPacket(281, UtilSystemId.CLIENT, new CancelQuest
			{
				QuestId = questId
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF07 RID: 44807 RVA: 0x00364BE8 File Offset: 0x00362DE8
		public void RequestDeckContents(long[] deckIds)
		{
			GetDeckContents getDeckContents = new GetDeckContents();
			getDeckContents.DeckId.AddRange(deckIds);
			this.SendUtilPacket(214, UtilSystemId.CLIENT, getDeckContents, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF08 RID: 44808 RVA: 0x00364C16 File Offset: 0x00362E16
		public void RequestAccountInfoNetCacheObject(GetAccountInfo.Request request)
		{
			this.SendUtilPacket(201, UtilSystemId.CLIENT, new GetAccountInfo
			{
				Request_ = request
			}, RequestPhase.RUNNING, (int)request);
		}

		// Token: 0x0600AF09 RID: 44809 RVA: 0x00364C34 File Offset: 0x00362E34
		public void RequestNetCacheObjectList(List<GetAccountInfo.Request> requests, List<GenericRequest> genericRequests)
		{
			GenericRequestList genericRequestList = new GenericRequestList();
			foreach (GetAccountInfo.Request requestSubId in requests)
			{
				genericRequestList.Requests.Add(new GenericRequest
				{
					RequestId = 201,
					RequestSubId = (int)requestSubId
				});
			}
			if (genericRequests != null)
			{
				foreach (GenericRequest item in genericRequests)
				{
					genericRequestList.Requests.Add(item);
				}
			}
			this.SendUtilPacket(327, UtilSystemId.CLIENT, genericRequestList, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF0A RID: 44810 RVA: 0x00364CFC File Offset: 0x00362EFC
		public void RequestProcessRecruitAFriend()
		{
			this.SendUtilPacket(339, UtilSystemId.RECRUIT_A_FRIEND, new ProcessRecruitAFriend(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF0B RID: 44811 RVA: 0x00364D14 File Offset: 0x00362F14
		public void RequestPurchaseMethod(long? pmtProductId, int quantity, PegasusShared.Currency currency, string deviceId, Platform platform)
		{
			this.SendUtilPacket(250, UtilSystemId.BATTLEPAY, new GetPurchaseMethod
			{
				PmtProductId = pmtProductId.GetValueOrDefault(),
				Quantity = quantity,
				CurrencyCode = currency.Code,
				DeviceId = deviceId,
				Platform = platform
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF0C RID: 44812 RVA: 0x00364D64 File Offset: 0x00362F64
		public void RequestRecruitAFriendData()
		{
			this.SendUtilPacket(337, UtilSystemId.RECRUIT_A_FRIEND, new GetRecruitAFriendData(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF0D RID: 44813 RVA: 0x00364D79 File Offset: 0x00362F79
		public void RequestRecruitAFriendUrl(Platform platform)
		{
			this.SendUtilPacket(335, UtilSystemId.RECRUIT_A_FRIEND, new GetRecruitAFriendURL
			{
				Platform = platform
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF0E RID: 44814 RVA: 0x00364D98 File Offset: 0x00362F98
		public void SellCard(PegasusShared.CardDef cardDef, int count, int unitSellPrice, int currentCollectionCount)
		{
			BuySellCard buySellCard = new BuySellCard
			{
				Def = cardDef,
				Buying = false,
				UnitSellPrice = unitSellPrice,
				CurrentCollectionCount = currentCollectionCount
			};
			if (count != 1)
			{
				buySellCard.Count = count;
			}
			this.SendUtilPacket(257, UtilSystemId.CLIENT, buySellCard, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF0F RID: 44815 RVA: 0x00364DE4 File Offset: 0x00362FE4
		public void SendAssetRequest(int clientToken, List<AssetKey> requestKeys, long? fsgId, byte[] fsgSharedSecretKey)
		{
			while (requestKeys.Count > 0)
			{
				int count = Math.Min(requestKeys.Count, 100);
				List<AssetKey> range = requestKeys.GetRange(0, count);
				requestKeys.RemoveRange(0, count);
				GetAssetRequest getAssetRequest = new GetAssetRequest
				{
					ClientToken = clientToken,
					Requests = range,
					FsgSharedSecretKey = fsgSharedSecretKey
				};
				if (fsgId != null)
				{
					getAssetRequest.FsgId = fsgId.Value;
				}
				this.SendUtilPacket(321, UtilSystemId.CLIENT, getAssetRequest, RequestPhase.RUNNING, 0);
			}
		}

		// Token: 0x0600AF10 RID: 44816 RVA: 0x00364E5C File Offset: 0x0036305C
		public void SendChoices(int choicesId, List<int> picks)
		{
			this.SendGamePacket(3, new ChooseEntities
			{
				Id = choicesId,
				Entities = picks
			});
		}

		// Token: 0x0600AF11 RID: 44817 RVA: 0x00364E78 File Offset: 0x00363078
		public void SendDebugCommandRequest(DebugCommandRequest packet)
		{
			this.SendUtilPacket(323, UtilSystemId.CLIENT, packet, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF12 RID: 44818 RVA: 0x00364E89 File Offset: 0x00363089
		public void SendDebugConsoleResponse(int responseType, string message)
		{
			if (!this.dispatcherImpl.DebugConnectionManager.IsActive())
			{
				return;
			}
			this.dispatcherImpl.DebugConnectionManager.SendDebugConsoleResponse(responseType, message);
		}

		// Token: 0x0600AF13 RID: 44819 RVA: 0x00364EB0 File Offset: 0x003630B0
		public void SendDeckData(DeckSetData packet)
		{
			this.SendUtilPacket(222, UtilSystemId.CLIENT, packet, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF14 RID: 44820 RVA: 0x00364EC1 File Offset: 0x003630C1
		public void SendDeckTemplateSource(long deckId, int templateId)
		{
			this.SendUtilPacket(332, UtilSystemId.CLIENT, new DeckSetTemplateSource
			{
				Deck = deckId,
				TemplateId = templateId
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF15 RID: 44821 RVA: 0x00364EE4 File Offset: 0x003630E4
		public void SendFreeDeckChoice(int classId, long noticeId)
		{
			this.SendUtilPacket(333, UtilSystemId.CLIENT, new FreeDeckChoice
			{
				ClassId = classId,
				NoticeId = noticeId
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF16 RID: 44822 RVA: 0x00364F07 File Offset: 0x00363107
		public void SendSmartDeckRequest(SmartDeckRequest packet)
		{
			this.SendUtilPacket(369, UtilSystemId.COLLECTION, packet, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF17 RID: 44823 RVA: 0x00364F18 File Offset: 0x00363118
		public void SendOfflineDeckContentsRequest()
		{
			this.SendUtilPacket(371, UtilSystemId.CLIENT, new OfflineDeckContentsRequest(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF18 RID: 44824 RVA: 0x00364F2D File Offset: 0x0036312D
		public void SendEmote(int emoteId)
		{
			this.SendGamePacket(15, new UserUI
			{
				Emote = emoteId
			});
		}

		// Token: 0x0600AF19 RID: 44825 RVA: 0x00364F43 File Offset: 0x00363143
		public bool AllowDebugConnections()
		{
			return this.dispatcherImpl.DebugConnectionManager.AllowDebugConnections();
		}

		// Token: 0x0600AF1A RID: 44826 RVA: 0x00364F55 File Offset: 0x00363155
		public void SendDebugConsoleCommand(string command)
		{
			this.SendGamePacket(123, new DebugConsoleCommand
			{
				Command = command
			});
		}

		// Token: 0x0600AF1B RID: 44827 RVA: 0x00364F6B File Offset: 0x0036316B
		public void SendOption(int choiceId, int index, int target, int subOption, int position)
		{
			this.SendGamePacket(2, new ChooseOption
			{
				Id = choiceId,
				Index = index,
				Target = target,
				SubOption = subOption,
				Position = position
			});
		}

		// Token: 0x0600AF1C RID: 44828 RVA: 0x00364F9E File Offset: 0x0036319E
		public void SendPing()
		{
			this.SendGamePacket(115, new Ping());
		}

		// Token: 0x0600AF1D RID: 44829 RVA: 0x00364FAD File Offset: 0x003631AD
		public void SendRemoveAllSpectators(bool regeneratePassword)
		{
			this.SendGamePacket(26, new RemoveSpectators
			{
				KickAllSpectators = true,
				RegenerateSpectatorPassword = regeneratePassword
			});
		}

		// Token: 0x0600AF1E RID: 44830 RVA: 0x00364FCA File Offset: 0x003631CA
		public void SendRemoveSpectators(bool regeneratePassword, List<BnetId> spectators)
		{
			this.SendGamePacket(26, new RemoveSpectators
			{
				RegenerateSpectatorPassword = regeneratePassword,
				TargetGameaccountIds = spectators
			});
		}

		// Token: 0x0600AF1F RID: 44831 RVA: 0x00364FE7 File Offset: 0x003631E7
		public void SendSpectatorInvite(BnetId targetBnetId, BnetId targetGameAccountId)
		{
			this.SendGamePacket(25, new InviteToSpectate
			{
				TargetBnetAccountId = targetBnetId,
				TargetGameAccountId = targetGameAccountId
			});
		}

		// Token: 0x0600AF20 RID: 44832 RVA: 0x00365004 File Offset: 0x00363204
		public void EnsureSubscribedTo(UtilSystemId systemChannel)
		{
			this.dispatcherImpl.EnsureSubscribedTo(systemChannel);
		}

		// Token: 0x0600AF21 RID: 44833 RVA: 0x00365012 File Offset: 0x00363212
		public void SendUnsubscribeRequest(Unsubscribe packet, UtilSystemId systemChannel)
		{
			this.SendUtilPacket(329, systemChannel, packet, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF22 RID: 44834 RVA: 0x00365023 File Offset: 0x00363223
		public void SendUserUi(int overCard, int heldCard, int arrowOrigin, int x, int y)
		{
			this.SendGamePacket(15, new UserUI
			{
				MouseInfo = new MouseInfo
				{
					ArrowOrigin = arrowOrigin,
					OverCard = overCard,
					HeldCard = heldCard,
					X = x,
					Y = y
				}
			});
		}

		// Token: 0x0600AF23 RID: 44835 RVA: 0x00365062 File Offset: 0x00363262
		public void SetClientOptions(SetOptions packet)
		{
			this.SendUtilPacket(239, UtilSystemId.CLIENT, packet, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF24 RID: 44836 RVA: 0x00365073 File Offset: 0x00363273
		public void SetFavoriteCardBack(int cardBack)
		{
			this.SendUtilPacket(291, UtilSystemId.CLIENT, new SetFavoriteCardBack
			{
				CardBack = cardBack
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF25 RID: 44837 RVA: 0x0036508F File Offset: 0x0036328F
		public void SetDisconnectedFromBattleNet()
		{
			this.dispatcherImpl.SetDisconnectedFromBattleNet();
		}

		// Token: 0x0600AF26 RID: 44838 RVA: 0x0036509C File Offset: 0x0036329C
		public void SetFavoriteHero(int classId, PegasusShared.CardDef heroCardDef)
		{
			this.SendUtilPacket(319, UtilSystemId.CLIENT, new SetFavoriteHero
			{
				FavoriteHero = new FavoriteHero
				{
					ClassId = classId,
					Hero = heroCardDef
				}
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF27 RID: 44839 RVA: 0x003650CA File Offset: 0x003632CA
		public void SetProgress(long value)
		{
			this.SendUtilPacket(230, UtilSystemId.CLIENT, new SetProgress
			{
				Value = value
			}, RequestPhase.STARTUP, 0);
		}

		// Token: 0x0600AF28 RID: 44840 RVA: 0x003650E6 File Offset: 0x003632E6
		public bool ShouldIgnoreError(BnetErrorInfo errorInfo)
		{
			return this.dispatcherImpl.ShouldIgnoreError(errorInfo);
		}

		// Token: 0x0600AF29 RID: 44841 RVA: 0x003650F4 File Offset: 0x003632F4
		public void SubmitThirdPartyPurchaseReceipt(long bpayId, BattlePayProvider provider, string productId, string deviceId, int quantity, string thirdPartyId, string base64Receipt, string thirdPartyUserId)
		{
			ThirdPartyReceiptData thirdPartyReceiptData = new ThirdPartyReceiptData
			{
				ThirdPartyId = thirdPartyId,
				Receipt = base64Receipt
			};
			if (!string.IsNullOrEmpty(thirdPartyUserId))
			{
				thirdPartyReceiptData.ThirdPartyUserId = thirdPartyUserId;
			}
			this.SendUtilPacket(293, UtilSystemId.BATTLEPAY, new SubmitThirdPartyReceipt
			{
				Provider = provider,
				ProductId = productId,
				Quantity = quantity,
				TransactionId = bpayId,
				ReceiptData = thirdPartyReceiptData,
				DeviceId = deviceId
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF2A RID: 44842 RVA: 0x00365167 File Offset: 0x00363367
		public double GetTimeLastPingReceieved()
		{
			return this.dispatcherImpl.TimeLastPingReceived;
		}

		// Token: 0x0600AF2B RID: 44843 RVA: 0x00365174 File Offset: 0x00363374
		public void SetTimeLastPingReceived(double time)
		{
			this.dispatcherImpl.TimeLastPingReceived = time;
		}

		// Token: 0x0600AF2C RID: 44844 RVA: 0x00365182 File Offset: 0x00363382
		public double GetTimeLastPingSent()
		{
			return this.dispatcherImpl.TimeLastPingSent;
		}

		// Token: 0x0600AF2D RID: 44845 RVA: 0x0036518F File Offset: 0x0036338F
		public void SetTimeLastPingSent(double time)
		{
			this.dispatcherImpl.TimeLastPingSent = time;
		}

		// Token: 0x0600AF2E RID: 44846 RVA: 0x003651A0 File Offset: 0x003633A0
		public void TriggerPlayedNearbyPlayerOnSubnet(ulong lastPlayedBnetHi, ulong lastPlayedBnetLo, ulong lastPlayedStartTime, ulong otherPlayerBnetHi, ulong otherPlayerBnetLo, ulong otherPlayerStartTime)
		{
			this.SendUtilPacket(298, UtilSystemId.CLIENT, new TriggerPlayedNearbyPlayerOnSubnet
			{
				LastPlayed = new NearbyPlayer
				{
					BnetIdHi = lastPlayedBnetHi,
					BnetIdLo = lastPlayedBnetLo,
					SessionStartTime = lastPlayedStartTime
				},
				OtherPlayer = new NearbyPlayer
				{
					BnetIdHi = otherPlayerBnetHi,
					BnetIdLo = otherPlayerBnetLo,
					SessionStartTime = otherPlayerStartTime
				}
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF2F RID: 44847 RVA: 0x00365203 File Offset: 0x00363403
		public void SetShouldIgnorePong(bool value)
		{
			this.dispatcherImpl.ShouldIgnorePong = value;
		}

		// Token: 0x0600AF30 RID: 44848 RVA: 0x00365211 File Offset: 0x00363411
		public void SetSpoofDisconnected(bool value)
		{
			this.dispatcherImpl.SpoofDisconnected = value;
		}

		// Token: 0x0600AF31 RID: 44849 RVA: 0x0036521F File Offset: 0x0036341F
		public void SetPingsSinceLastPong(int value)
		{
			this.dispatcherImpl.PingsSinceLastPong = value;
		}

		// Token: 0x0600AF32 RID: 44850 RVA: 0x0036522D File Offset: 0x0036342D
		public int GetPingsSinceLastPong()
		{
			return this.dispatcherImpl.PingsSinceLastPong;
		}

		// Token: 0x0600AF33 RID: 44851 RVA: 0x0036523A File Offset: 0x0036343A
		public void ValidateAchieve(int achieveId)
		{
			this.SendUtilPacket(284, UtilSystemId.CLIENT, new ValidateAchieve
			{
				Achieve = achieveId
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF34 RID: 44852 RVA: 0x00365256 File Offset: 0x00363456
		public void RequestTavernBrawlSessionBegin()
		{
			this.SendUtilPacket(343, UtilSystemId.CLIENT, new TavernBrawlRequestSessionBegin(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF35 RID: 44853 RVA: 0x0036526B File Offset: 0x0036346B
		public void TavernBrawlRetire()
		{
			this.SendUtilPacket(344, UtilSystemId.CLIENT, new TavernBrawlRequestSessionRetire(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF36 RID: 44854 RVA: 0x00365280 File Offset: 0x00363480
		public void AckTavernBrawlSessionRewards()
		{
			this.SendUtilPacket(345, UtilSystemId.CLIENT, new TavernBrawlAckSessionRewards(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF37 RID: 44855 RVA: 0x00365295 File Offset: 0x00363495
		public TavernBrawlRequestSessionBeginResponse GetTavernBrawlSessionBeginResponse()
		{
			return this.UnpackNextUtilPacket<TavernBrawlRequestSessionBeginResponse>(347);
		}

		// Token: 0x0600AF38 RID: 44856 RVA: 0x003652A2 File Offset: 0x003634A2
		public TavernBrawlRequestSessionRetireResponse GetTavernBrawlSessionRetired()
		{
			return this.UnpackNextUtilPacket<TavernBrawlRequestSessionRetireResponse>(348);
		}

		// Token: 0x0600AF39 RID: 44857 RVA: 0x003652B0 File Offset: 0x003634B0
		public void RequestTavernBrawlInfo(BrawlType brawlType, long? fsgId, byte[] fsgSharedSecretKey)
		{
			RequestTavernBrawlInfo requestTavernBrawlInfo = new RequestTavernBrawlInfo
			{
				BrawlType = brawlType,
				FsgSharedSecretKey = fsgSharedSecretKey
			};
			if (fsgId != null)
			{
				requestTavernBrawlInfo.FsgId = fsgId.Value;
			}
			this.SendUtilPacket(352, UtilSystemId.CLIENT, requestTavernBrawlInfo, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF3A RID: 44858 RVA: 0x003652F8 File Offset: 0x003634F8
		public void RequestTavernBrawlPlayerRecord(BrawlType brawlType, long? fsgId, byte[] fsgSharedSecretKey)
		{
			RequestTavernBrawlPlayerRecord requestTavernBrawlPlayerRecord = new RequestTavernBrawlPlayerRecord
			{
				BrawlType = brawlType,
				FsgSharedSecretKey = fsgSharedSecretKey
			};
			if (fsgId != null)
			{
				requestTavernBrawlPlayerRecord.FsgId = fsgId.Value;
			}
			this.SendUtilPacket(353, UtilSystemId.CLIENT, requestTavernBrawlPlayerRecord, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF3B RID: 44859 RVA: 0x00365340 File Offset: 0x00363540
		public void RequestNearbyFSGs(double latitude, double longitude, double accuracy, List<string> bssids, Platform platform)
		{
			this.SendUtilPacket(501, UtilSystemId.FIRESIDE_GATHERINGS, new RequestNearbyFSGs
			{
				Location = new GPSCoords
				{
					Latitude = latitude,
					Longitude = longitude,
					Accuracy = accuracy
				},
				Bssids = (bssids ?? new List<string>()),
				Platform = platform
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF3C RID: 44860 RVA: 0x00365399 File Offset: 0x00363599
		public void RequestNearbyFSGs(List<string> bssids, Platform platform)
		{
			this.SendUtilPacket(501, UtilSystemId.FIRESIDE_GATHERINGS, new RequestNearbyFSGs
			{
				Location = null,
				Bssids = (bssids ?? new List<string>()),
				Platform = platform
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF3D RID: 44861 RVA: 0x003653CC File Offset: 0x003635CC
		public void CheckInToFSG(long fsgId, double latitude, double longitude, double accuracy, List<string> bssids, Platform platform)
		{
			this.SendUtilPacket(502, UtilSystemId.FIRESIDE_GATHERINGS, new CheckInToFSG
			{
				FsgId = fsgId,
				Location = new GPSCoords
				{
					Latitude = latitude,
					Longitude = longitude,
					Accuracy = accuracy
				},
				Bssids = (bssids ?? new List<string>()),
				Platform = platform
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF3E RID: 44862 RVA: 0x0036542D File Offset: 0x0036362D
		public void CheckInToFSG(long fsgId, List<string> bssids, Platform platform)
		{
			this.SendUtilPacket(502, UtilSystemId.FIRESIDE_GATHERINGS, new CheckInToFSG
			{
				FsgId = fsgId,
				Location = null,
				Bssids = (bssids ?? new List<string>()),
				Platform = platform
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF3F RID: 44863 RVA: 0x00365467 File Offset: 0x00363667
		public void CheckOutOfFSG(long fsgId, Platform platform)
		{
			this.SendUtilPacket(503, UtilSystemId.FIRESIDE_GATHERINGS, new CheckOutOfFSG
			{
				FsgId = fsgId,
				Platform = platform
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF40 RID: 44864 RVA: 0x0036548A File Offset: 0x0036368A
		public void InnkeeperSetupFSG(List<string> bssids, long fsgId, Platform platform)
		{
			this.InnkeeperSetupFSG(bssids, fsgId, null, platform);
		}

		// Token: 0x0600AF41 RID: 44865 RVA: 0x00365496 File Offset: 0x00363696
		public void InnkeeperSetupFSG(List<string> bssids, long fsgId, GPSCoords location, Platform platform)
		{
			this.SendUtilPacket(507, UtilSystemId.FIRESIDE_GATHERINGS, new InnkeeperSetupGathering
			{
				Location = location,
				Bssids = (bssids ?? new List<string>()),
				FsgId = fsgId,
				Platform = platform
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF42 RID: 44866 RVA: 0x003654D1 File Offset: 0x003636D1
		public void RequestFSGPatronListUpdate()
		{
			this.SendUtilPacket(512, UtilSystemId.FIRESIDE_GATHERINGS, new FSGPatronListUpdate(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF43 RID: 44867 RVA: 0x003654E6 File Offset: 0x003636E6
		public void DraftRequestDisablePremiums()
		{
			this.SendUtilPacket(354, UtilSystemId.CLIENT, new DraftRequestRemovePremiums(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF44 RID: 44868 RVA: 0x003654FB File Offset: 0x003636FB
		public void RequestLeaguePromoteSelf()
		{
			this.SendUtilPacket(367, UtilSystemId.CLIENT, new LeaguePromoteSelfRequest(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF45 RID: 44869 RVA: 0x00365510 File Offset: 0x00363710
		public RequestNearbyFSGsResponse GetRequestNearbyFSGsResponse()
		{
			return this.UnpackNextUtilPacket<RequestNearbyFSGsResponse>(504);
		}

		// Token: 0x0600AF46 RID: 44870 RVA: 0x0036551D File Offset: 0x0036371D
		public CheckInToFSGResponse GetCheckInToFSGResponse()
		{
			return this.UnpackNextUtilPacket<CheckInToFSGResponse>(505);
		}

		// Token: 0x0600AF47 RID: 44871 RVA: 0x0036552A File Offset: 0x0036372A
		public CheckOutOfFSGResponse GetCheckOutOfFSGResponse()
		{
			return this.UnpackNextUtilPacket<CheckOutOfFSGResponse>(506);
		}

		// Token: 0x0600AF48 RID: 44872 RVA: 0x00365537 File Offset: 0x00363737
		public InnkeeperSetupGatheringResponse GetInnkeeperSetupGatheringResponse()
		{
			return this.UnpackNextUtilPacket<InnkeeperSetupGatheringResponse>(508);
		}

		// Token: 0x0600AF49 RID: 44873 RVA: 0x00365544 File Offset: 0x00363744
		public PatronCheckedInToFSG GetPatronCheckedInToFSG()
		{
			return this.UnpackNextUtilPacket<PatronCheckedInToFSG>(509);
		}

		// Token: 0x0600AF4A RID: 44874 RVA: 0x00365551 File Offset: 0x00363751
		public PatronCheckedOutOfFSG GetPatronCheckedOutOfFSG()
		{
			return this.UnpackNextUtilPacket<PatronCheckedOutOfFSG>(510);
		}

		// Token: 0x0600AF4B RID: 44875 RVA: 0x0036555E File Offset: 0x0036375E
		public FSGPatronListUpdate GetFSGPatronListUpdate()
		{
			return this.UnpackNextUtilPacket<FSGPatronListUpdate>(512);
		}

		// Token: 0x0600AF4C RID: 44876 RVA: 0x0036556B File Offset: 0x0036376B
		public FSGFeatureConfig GetFSGFeatureConfig()
		{
			return this.UnpackNextUtilPacket<FSGFeatureConfig>(511);
		}

		// Token: 0x0600AF4D RID: 44877 RVA: 0x00365578 File Offset: 0x00363778
		public LeaguePromoteSelfResponse GetLeaguePromoteSelfResponse()
		{
			return this.UnpackNextUtilPacket<LeaguePromoteSelfResponse>(368);
		}

		// Token: 0x0600AF4E RID: 44878 RVA: 0x00365585 File Offset: 0x00363785
		public SmartDeckResponse GetSmartDeckResponse()
		{
			return this.UnpackNextUtilPacket<SmartDeckResponse>(370);
		}

		// Token: 0x0600AF4F RID: 44879 RVA: 0x00365592 File Offset: 0x00363792
		public void RequestGameSaveData(List<long> keys, int clientToken)
		{
			this.SendUtilPacket(357, UtilSystemId.CLIENT, new GameSaveDataRequest
			{
				KeyIds = keys,
				ClientToken = clientToken
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF50 RID: 44880 RVA: 0x003655B5 File Offset: 0x003637B5
		public GameSaveDataResponse GetGameSaveDataResponse()
		{
			return this.UnpackNextUtilPacket<GameSaveDataResponse>(358);
		}

		// Token: 0x0600AF51 RID: 44881 RVA: 0x003655C2 File Offset: 0x003637C2
		public void SetGameSaveData(List<GameSaveDataUpdate> dataUpdates, int clientToken)
		{
			this.SendUtilPacket(359, UtilSystemId.CLIENT, new SetGameSaveData
			{
				Data = dataUpdates,
				ClientToken = clientToken
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF52 RID: 44882 RVA: 0x003655E5 File Offset: 0x003637E5
		public SetGameSaveDataResponse GetSetGameSaveDataResponse()
		{
			return this.UnpackNextUtilPacket<SetGameSaveDataResponse>(360);
		}

		// Token: 0x0600AF53 RID: 44883 RVA: 0x003655F2 File Offset: 0x003637F2
		public void SendLocateCheatServerRequest()
		{
			this.SendUtilPacket(361, UtilSystemId.CHEAT, new LocateCheatServerRequest(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF54 RID: 44884 RVA: 0x00365607 File Offset: 0x00363807
		public LocateCheatServerResponse GetLocateCheatServerResponse()
		{
			return this.UnpackNextUtilPacket<LocateCheatServerResponse>(362);
		}

		// Token: 0x0600AF55 RID: 44885 RVA: 0x00365614 File Offset: 0x00363814
		public GameToConnectNotification GetGameToConnectNotification()
		{
			return this.UnpackNextUtilPacket<GameToConnectNotification>(363);
		}

		// Token: 0x0600AF56 RID: 44886 RVA: 0x00365621 File Offset: 0x00363821
		public void GetServerTimeRequest(long now)
		{
			this.SendUtilPacket(364, UtilSystemId.CLIENT, new GetServerTimeRequest
			{
				ClientUnixTime = now
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF57 RID: 44887 RVA: 0x0036563D File Offset: 0x0036383D
		public ResponseWithRequest<GetServerTimeResponse, GetServerTimeRequest> GetServerTimeResponse()
		{
			return this.UnpackNextUtilPacketWithRequest<GetServerTimeResponse, GetServerTimeRequest>(365);
		}

		// Token: 0x0600AF58 RID: 44888 RVA: 0x0036564A File Offset: 0x0036384A
		public void RequestBaconRatingInfo()
		{
			this.SendUtilPacket(372, UtilSystemId.CLIENT, new BattlegroundsRatingInfoRequest(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF59 RID: 44889 RVA: 0x0036565F File Offset: 0x0036385F
		public ResponseWithRequest<BattlegroundsRatingInfoResponse, BattlegroundsRatingInfoRequest> BattlegroundsRatingInfoResponse()
		{
			return this.UnpackNextUtilPacketWithRequest<BattlegroundsRatingInfoResponse, BattlegroundsRatingInfoRequest>(373);
		}

		// Token: 0x0600AF5A RID: 44890 RVA: 0x0036566C File Offset: 0x0036386C
		public void RequestBattlegroundsPremiumStatus()
		{
			this.SendUtilPacket(374, UtilSystemId.CLIENT, new BattlegroundsPremiumStatusRequest(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF5B RID: 44891 RVA: 0x00365681 File Offset: 0x00363881
		public ResponseWithRequest<BattlegroundsPremiumStatusResponse, BattlegroundsPremiumStatusRequest> GetBattlegroundsPremiumStatus()
		{
			return this.UnpackNextUtilPacketWithRequest<BattlegroundsPremiumStatusResponse, BattlegroundsPremiumStatusRequest>(375);
		}

		// Token: 0x0600AF5C RID: 44892 RVA: 0x0036568E File Offset: 0x0036388E
		public void RequestPVPDRStatsInfo()
		{
			this.SendUtilPacket(378, UtilSystemId.CLIENT, new PVPDRStatsInfoRequest(), RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF5D RID: 44893 RVA: 0x003656A3 File Offset: 0x003638A3
		public ResponseWithRequest<PVPDRStatsInfoResponse, PVPDRStatsInfoRequest> PVPDRStatsInfoResponse()
		{
			return this.UnpackNextUtilPacketWithRequest<PVPDRStatsInfoResponse, PVPDRStatsInfoRequest>(379);
		}

		// Token: 0x0600AF5E RID: 44894 RVA: 0x003656B0 File Offset: 0x003638B0
		public void ReportBlizzardCheckoutStatus(BlizzardCheckoutStatus status, HearthstoneCheckoutTransactionData data, long now)
		{
			string transactionId = "";
			string productId = "";
			string currency = "";
			if (data != null)
			{
				transactionId = data.TransactionID;
				productId = data.ProductID.ToString();
				currency = data.CurrencyCode;
			}
			this.SendUtilPacket(366, UtilSystemId.BATTLEPAY, new ReportBlizzardCheckoutStatus
			{
				Status = status,
				TransactionId = transactionId,
				ProductId = productId,
				Currency = currency,
				ClientUnixTime = now
			}, RequestPhase.RUNNING, 0);
		}

		// Token: 0x0600AF5F RID: 44895 RVA: 0x00365725 File Offset: 0x00363925
		private void SendGamePacket(int packetId, IProtoBuf body)
		{
			this.dispatcherImpl.SendGamePacket(packetId, body);
		}

		// Token: 0x0600AF60 RID: 44896 RVA: 0x00365734 File Offset: 0x00363934
		private void SendUtilPacket(int type, UtilSystemId system, IProtoBuf body, RequestPhase requestPhase = RequestPhase.RUNNING, int subId = 0)
		{
			this.dispatcherImpl.SendUtilPacket(type, system, body, requestPhase, subId);
		}

		// Token: 0x0600AF61 RID: 44897 RVA: 0x00365748 File Offset: 0x00363948
		private T UnpackNextDebugPacket<T>(int packetId) where T : IProtoBuf
		{
			return this.Unpack<T>(this.dispatcherImpl.DebugConnectionManager.NextDebugPacket(), packetId);
		}

		// Token: 0x0600AF62 RID: 44898 RVA: 0x00365761 File Offset: 0x00363961
		private T UnpackNextGamePacket<T>(int packetId) where T : IProtoBuf
		{
			return this.Unpack<T>(this.dispatcherImpl.NextGamePacket(), packetId);
		}

		// Token: 0x0600AF63 RID: 44899 RVA: 0x00365775 File Offset: 0x00363975
		private T UnpackNextUtilPacket<T>(int packetId) where T : IProtoBuf
		{
			return this.Unpack<T>(this.dispatcherImpl.NextUtilPacket().Response, packetId);
		}

		// Token: 0x0600AF64 RID: 44900 RVA: 0x00365790 File Offset: 0x00363990
		private ResponseWithRequest<T, U> UnpackNextUtilPacketWithRequest<T, U>(int packetId) where T : IProtoBuf where U : IProtoBuf, new()
		{
			ResponseWithRequest<T, U> responseWithRequest = new ResponseWithRequest<T, U>();
			ResponseWithRequest responseWithRequest2 = this.dispatcherImpl.NextUtilPacket();
			if (responseWithRequest2.Request != null && responseWithRequest2.Request.Body is byte[])
			{
				responseWithRequest.Request = (U)((object)ProtobufUtil.ParseFromGeneric<U>((byte[])responseWithRequest2.Request.Body));
			}
			responseWithRequest.Response = this.Unpack<T>(responseWithRequest2.Response, packetId);
			return responseWithRequest;
		}

		// Token: 0x0600AF65 RID: 44901 RVA: 0x00365800 File Offset: 0x00363A00
		private T Unpack<T>(PegasusPacket p, int packetId) where T : IProtoBuf
		{
			if (p == null || p.Type != packetId || !(p.Body is T))
			{
				return default(T);
			}
			return (T)((object)p.Body);
		}

		// Token: 0x040094F6 RID: 38134
		private readonly IDispatcher dispatcherImpl;
	}
}
