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
	public class ConnectAPI
	{
		private readonly IDispatcher dispatcherImpl;

		public ConnectAPI(IDispatcher dispatcher)
		{
			dispatcherImpl = dispatcher;
		}

		public void AbortBlizzardPurchase(string deviceId, bool isAutoCanceled, CancelPurchase.CancelReason? reason, string error)
		{
			CancelPurchase cancelPurchase = new CancelPurchase
			{
				IsAutoCancel = isAutoCanceled,
				DeviceId = deviceId,
				ErrorMessage = error
			};
			if (reason.HasValue)
			{
				cancelPurchase.Reason = reason.Value;
			}
			SendUtilPacket(274, UtilSystemId.BATTLEPAY, cancelPurchase);
		}

		public void AbortThirdPartyPurchase(string deviceId, CancelPurchase.CancelReason reason, string error)
		{
			SendUtilPacket(274, UtilSystemId.BATTLEPAY, new CancelPurchase
			{
				IsAutoCancel = false,
				Reason = reason,
				DeviceId = deviceId,
				ErrorMessage = error
			});
		}

		public void AckAchieveProgress(int achievementId, int ackProgress)
		{
			SendUtilPacket(243, UtilSystemId.CLIENT, new AckAchieveProgress
			{
				Id = achievementId,
				AckProgress = ackProgress
			});
		}

		public void AckQuest(int questId)
		{
			SendUtilPacket(604, UtilSystemId.CLIENT, new AckQuest
			{
				QuestId = questId
			});
		}

		public void CheckForNewQuests()
		{
			SendUtilPacket(605, UtilSystemId.CLIENT, new CheckForNewQuests());
		}

		public void RerollQuest(int questId)
		{
			SendUtilPacket(606, UtilSystemId.CLIENT, new RerollQuest
			{
				QuestId = questId
			});
		}

		public void AckAchievement(int achievementId)
		{
			SendUtilPacket(612, UtilSystemId.CLIENT, new AckAchievement
			{
				AchievementId = achievementId
			});
		}

		public void ClaimAchievementReward(int achievementId, int chooseOneRewardId = 0)
		{
			SendUtilPacket(613, UtilSystemId.CLIENT, new ClaimAchievementReward
			{
				AchievementId = achievementId,
				ChooseOneRewardItemId = chooseOneRewardId
			});
		}

		public void AckRewardTrackReward(int rewardTrackId, int level, bool forPaidTrack)
		{
			SendUtilPacket(616, UtilSystemId.CLIENT, new AckRewardTrackReward
			{
				RewardTrackId = rewardTrackId,
				Level = level,
				ForPaidTrack = forPaidTrack
			});
		}

		public void ClaimRewardTrackReward(int rewardTrackId, int level, bool forPaidTrack, int chooseOneRewardItemId)
		{
			SendUtilPacket(615, UtilSystemId.CLIENT, new ClaimRewardTrackReward
			{
				RewardTrackId = rewardTrackId,
				Level = level,
				ForPaidTrack = forPaidTrack,
				ChooseOneRewardItemId = chooseOneRewardItemId
			});
		}

		public void AckCardSeen(AckCardSeen ackCardSeenPacket)
		{
			SendUtilPacket(223, UtilSystemId.CLIENT, ackCardSeenPacket);
		}

		public void AckNotice(long noticeId)
		{
			SendUtilPacket(213, UtilSystemId.CLIENT, new AckNotice
			{
				Entry = noticeId
			});
		}

		public void AcknowledgeBanner(int bannerId)
		{
			SendUtilPacket(309, UtilSystemId.CLIENT, new AcknowledgeBanner
			{
				Banner = bannerId
			});
		}

		public void AckWingProgress(int wing, int ackProgress)
		{
			SendUtilPacket(308, UtilSystemId.CLIENT, new AckWingProgress
			{
				Wing = wing,
				Ack = ackProgress
			});
		}

		public void BeginThirdPartyPurchase(string deviceId, BattlePayProvider provider, string productId, int quantity)
		{
			SendUtilPacket(312, UtilSystemId.BATTLEPAY, new StartThirdPartyPurchase
			{
				Provider = provider,
				ProductId = productId,
				Quantity = quantity,
				DeviceId = deviceId
			});
		}

		public void BeginThirdPartyPurchaseWithReceipt(string deviceId, BattlePayProvider provider, string productId, int quantity, string thirdPartyId, string base64Receipt, string thirdPartyUserId)
		{
			ThirdPartyReceiptData danglingReceiptData = new ThirdPartyReceiptData
			{
				ThirdPartyId = thirdPartyId,
				Receipt = base64Receipt,
				ThirdPartyUserId = thirdPartyUserId
			};
			SendUtilPacket(312, UtilSystemId.BATTLEPAY, new StartThirdPartyPurchase
			{
				Provider = provider,
				ProductId = productId,
				Quantity = quantity,
				DeviceId = deviceId,
				DanglingReceiptData = danglingReceiptData
			});
		}

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
			SendUtilPacket(257, UtilSystemId.CLIENT, buySellCard);
		}

		public void CheckAccountLicenseAchieve(int achieveId)
		{
			SendUtilPacket(297, UtilSystemId.BATTLEPAY, new CheckAccountLicenseAchieve
			{
				Achieve = achieveId
			});
		}

		public void Close()
		{
			dispatcherImpl.Close();
			dispatcherImpl.DebugConnectionManager.Shutdown();
		}

		public void Concede()
		{
			SendGamePacket(11, new Concede());
		}

		public void ConfirmPurchase()
		{
			SendUtilPacket(273, UtilSystemId.BATTLEPAY, new DoPurchase());
		}

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
			if (fsgId.HasValue)
			{
				createDeck.FsgId = fsgId.Value;
			}
			if (requestId.HasValue)
			{
				createDeck.RequestId = requestId.Value;
			}
			SendUtilPacket(209, UtilSystemId.CLIENT, createDeck);
		}

		public DeckCreated DeckCreated()
		{
			return UnpackNextUtilPacket<DeckCreated>(217);
		}

		public DeckDeleted DeckDeleted()
		{
			return UnpackNextUtilPacket<DeckDeleted>(218);
		}

		public DeckRenamed DeckRenamed()
		{
			return UnpackNextUtilPacket<DeckRenamed>(219);
		}

		public void DecodeAndProcessPacket(PegasusPacket packet)
		{
			if (dispatcherImpl.DecodePacket(packet) != null)
			{
				dispatcherImpl.NotifyUtilResponseReceived(packet);
			}
		}

		public void DeleteDeck(long deckId, DeckType deckType)
		{
			SendUtilPacket(210, UtilSystemId.CLIENT, new DeleteDeck
			{
				Deck = deckId,
				DeckType = deckType
			});
		}

		public void DisconnectFromGameServer()
		{
			dispatcherImpl.DisconnectFromGameServer();
		}

		public void DoLoginUpdate(string referralSource)
		{
			SendUtilPacket(205, UtilSystemId.CLIENT, new UpdateLogin
			{
				Referral = referralSource
			}, RequestPhase.STARTUP);
		}

		public void DraftAckRewards(long deckId, int slot)
		{
			SendUtilPacket(287, UtilSystemId.CLIENT, new DraftAckRewards
			{
				DeckId = deckId,
				Slot = slot
			});
		}

		public void DraftBegin()
		{
			SendUtilPacket(235, UtilSystemId.CLIENT, new DraftBegin());
		}

		public DraftChosen GetDraftChosen()
		{
			return UnpackNextUtilPacket<DraftChosen>(249);
		}

		public DraftBeginning GetDraftBeginning()
		{
			return UnpackNextUtilPacket<DraftBeginning>(246);
		}

		public DraftChoicesAndContents GetDraftChoicesAndContents()
		{
			return UnpackNextUtilPacket<DraftChoicesAndContents>(248);
		}

		public DraftError DraftGetError()
		{
			return UnpackNextUtilPacket<DraftError>(251);
		}

		public void RequestDraftChoicesAndContents()
		{
			SendUtilPacket(244, UtilSystemId.CLIENT, new DraftGetChoicesAndContents());
		}

		public void SendArenaSessionRequest()
		{
			SendUtilPacket(346, UtilSystemId.CLIENT, new ArenaSessionRequest());
		}

		public ArenaSessionResponse GetArenaSessionResponse()
		{
			return UnpackNextUtilPacket<ArenaSessionResponse>(351);
		}

		public DraftRewardsAcked DraftRewardsAcked()
		{
			return UnpackNextUtilPacket<DraftRewardsAcked>(288);
		}

		public void DraftMakePick(long deckId, int slot, int index, int premium)
		{
			SendUtilPacket(245, UtilSystemId.CLIENT, new DraftMakePick
			{
				DeckId = deckId,
				Slot = slot,
				Index = index,
				Premium = premium
			});
		}

		public void DraftRetire(long deckId, int slot, int seasonId)
		{
			SendUtilPacket(242, UtilSystemId.CLIENT, new DraftRetire
			{
				DeckId = deckId,
				Slot = slot,
				SeasonId = seasonId
			});
		}

		public int DropAllDebugPackets()
		{
			return dispatcherImpl.DebugConnectionManager.DropAllPackets();
		}

		public int DropAllGamePackets()
		{
			return dispatcherImpl.DropAllGamePackets();
		}

		public int DropAllUtilPackets()
		{
			return dispatcherImpl.DropAllUtilPackets();
		}

		public void DropDebugPacket()
		{
			dispatcherImpl.DebugConnectionManager.DropPacket();
		}

		public void DropGamePacket()
		{
			dispatcherImpl.DropGamePacket();
		}

		public void DropUtilPacket()
		{
			dispatcherImpl.DropUtilPacket();
		}

		public bool GameServerHasEvents()
		{
			return dispatcherImpl.GameServerHasEvents();
		}

		public AccountLicenseAchieveResponse GetAccountLicenseAchieveResponse()
		{
			return UnpackNextUtilPacket<AccountLicenseAchieveResponse>(311);
		}

		public AccountLicensesInfoResponse GetAccountLicensesInfoResponse()
		{
			return UnpackNextUtilPacket<AccountLicensesInfoResponse>(325);
		}

		public AdventureProgressResponse GetAdventureProgressResponse()
		{
			return UnpackNextUtilPacket<AdventureProgressResponse>(306);
		}

		public void GetAllClientOptions()
		{
			SendUtilPacket(240, UtilSystemId.CLIENT, new GetOptions());
		}

		public HeroXP GetHeroXP()
		{
			return UnpackNextUtilPacket<HeroXP>(283);
		}

		public GetAssetResponse GetAssetResponse()
		{
			return UnpackNextUtilPacket<GetAssetResponse>(322);
		}

		public AssetsVersionResponse GetAssetsVersionResponse()
		{
			return UnpackNextUtilPacket<AssetsVersionResponse>(304);
		}

		public BattlePayConfigResponse GetBattlePayConfigResponse()
		{
			return UnpackNextUtilPacket<BattlePayConfigResponse>(238);
		}

		public BattlePayStatusResponse GetBattlePayStatusResponse()
		{
			return UnpackNextUtilPacket<BattlePayStatusResponse>(265);
		}

		public CancelQuestResponse GetCanceledQuestResponse()
		{
			return UnpackNextUtilPacket<CancelQuestResponse>(282);
		}

		public SetFavoriteCardBackResponse GetSetFavoriteCardBackResponse()
		{
			return UnpackNextUtilPacket<SetFavoriteCardBackResponse>(292);
		}

		public CardBacks GetCardBacks()
		{
			return UnpackNextUtilPacket<CardBacks>(236);
		}

		public BoughtSoldCard GetCardSaleResult()
		{
			return UnpackNextUtilPacket<BoughtSoldCard>(258);
		}

		public CardValues GetCardValues()
		{
			return UnpackNextUtilPacket<CardValues>(260);
		}

		public ClientStaticAssetsResponse GetClientStaticAssetsResponse()
		{
			return UnpackNextUtilPacket<ClientStaticAssetsResponse>(341);
		}

		public InitialClientState GetInitialClientState()
		{
			return UnpackNextUtilPacket<InitialClientState>(207);
		}

		public DBAction GetDbAction()
		{
			return UnpackNextUtilPacket<DBAction>(216);
		}

		public Deadend GetDeadendGame()
		{
			return UnpackNextGamePacket<Deadend>(169);
		}

		public DeadendUtil GetDeadendUtil()
		{
			return UnpackNextUtilPacket<DeadendUtil>(167);
		}

		public DebugCommandResponse GetDebugCommandResponse()
		{
			return UnpackNextUtilPacket<DebugCommandResponse>(324);
		}

		public DebugConsoleCommand GetDebugConsoleCommand()
		{
			if (!dispatcherImpl.DebugConnectionManager.AllowDebugConnections())
			{
				return null;
			}
			return UnpackNextDebugPacket<DebugConsoleCommand>(123);
		}

		public DebugConsoleResponse GetDebugConsoleResponse()
		{
			if (!dispatcherImpl.DebugConnectionManager.AllowDebugConnections())
			{
				return null;
			}
			return UnpackNextGamePacket<DebugConsoleResponse>(124);
		}

		public GetDeckContentsResponse GetDeckContentsResponse()
		{
			return UnpackNextUtilPacket<GetDeckContentsResponse>(215);
		}

		public FreeDeckChoiceResponse GetFreeDeckChoiceResponse()
		{
			return UnpackNextUtilPacket<FreeDeckChoiceResponse>(334);
		}

		public DeckList GetDeckHeaders()
		{
			return UnpackNextUtilPacket<DeckList>(202);
		}

		public ProfileDeckLimit GetDeckLimit()
		{
			return UnpackNextUtilPacket<ProfileDeckLimit>(231);
		}

		public DraftRetired GetDraftRetired()
		{
			return UnpackNextUtilPacket<DraftRetired>(247);
		}

		public DraftRemovePremiumsResponse GetDraftDisablePremiumsResponse()
		{
			return UnpackNextUtilPacket<DraftRemovePremiumsResponse>(355);
		}

		public EntitiesChosen GetEntitiesChosen()
		{
			return UnpackNextGamePacket<EntitiesChosen>(13);
		}

		public EntityChoices GetEntityChoices()
		{
			return UnpackNextGamePacket<EntityChoices>(17);
		}

		public FavoriteHeroesResponse GetFavoriteHeroesResponse()
		{
			return UnpackNextUtilPacket<FavoriteHeroesResponse>(318);
		}

		public GuardianVars GetGuardianVars()
		{
			return UnpackNextUtilPacket<GuardianVars>(264);
		}

		public GameCanceled GetGameCancelInfo()
		{
			return UnpackNextGamePacket<GameCanceled>(12);
		}

		public GameSetup GetGameSetup()
		{
			return UnpackNextGamePacket<GameSetup>(16);
		}

		public GamesInfo GetGamesInfo()
		{
			return UnpackNextUtilPacket<GamesInfo>(208);
		}

		public GameStartState GetGameStartState()
		{
			return dispatcherImpl.GameStartState;
		}

		public void SetGameStartState(GameStartState state)
		{
			dispatcherImpl.GameStartState = state;
		}

		public void GetGameState()
		{
			SendGamePacket(1, new GetGameState());
		}

		public void UpdateBattlegroundInfo()
		{
			SendGamePacket(53, new UpdateBattlegroundInfo());
		}

		public void RequestGameRoundHistory()
		{
			SendGamePacket(32, new GetGameRoundHistory());
		}

		public void RequestRealtimeBattlefieldRaces()
		{
			SendGamePacket(33, new GetGameRealTimeBattlefieldRaces());
		}

		public GenericResponse GetGenericResponse()
		{
			return UnpackNextUtilPacket<GenericResponse>(326);
		}

		public MassDisenchantResponse GetMassDisenchantResponse()
		{
			return UnpackNextUtilPacket<MassDisenchantResponse>(269);
		}

		public MedalInfo GetMedalInfo()
		{
			return UnpackNextUtilPacket<MedalInfo>(232);
		}

		public NAckOption GetNAckOption()
		{
			return UnpackNextGamePacket<NAckOption>(10);
		}

		public ClientStateNotification GetClientStateNotification()
		{
			return UnpackNextUtilPacket<ClientStateNotification>(333);
		}

		public BoosterContent GetOpenedBooster()
		{
			return UnpackNextUtilPacket<BoosterContent>(226);
		}

		public AllOptions GetAllOptions()
		{
			return UnpackNextGamePacket<AllOptions>(14);
		}

		public PlayerQuestStateUpdate GetPlayerQuestStateUpdate()
		{
			return UnpackNextUtilPacket<PlayerQuestStateUpdate>(601);
		}

		public PlayerQuestPoolStateUpdate GetPlayerQuestPoolStateUpdate()
		{
			return UnpackNextUtilPacket<PlayerQuestPoolStateUpdate>(602);
		}

		public PlayerAchievementStateUpdate GetPlayerAchievementStateUpdate()
		{
			return UnpackNextUtilPacket<PlayerAchievementStateUpdate>(603);
		}

		public PlayerRewardTrackStateUpdate GetPlayerRewardTrackStateUpdate()
		{
			return UnpackNextUtilPacket<PlayerRewardTrackStateUpdate>(614);
		}

		public RewardTrackXpNotification GetRewardTrackXpNotification()
		{
			return UnpackNextUtilPacket<RewardTrackXpNotification>(617);
		}

		public RewardTrackUnclaimedNotification GetRewardTrackUnclaimedNotification()
		{
			return UnpackNextUtilPacket<RewardTrackUnclaimedNotification>(619);
		}

		public RerollQuestResponse GetRerollQuestResponse()
		{
			return UnpackNextUtilPacket<RerollQuestResponse>(607);
		}

		public PlayerRecords GetPlayerRecords()
		{
			return UnpackNextUtilPacket<PlayerRecords>(270);
		}

		public PowerHistory GetPowerHistory()
		{
			return UnpackNextGamePacket<PowerHistory>(19);
		}

		public ProcessRecruitAFriendResponse GetProcessRecruitAFriendResponse()
		{
			return UnpackNextUtilPacket<ProcessRecruitAFriendResponse>(342);
		}

		public ProfileProgress GetProfileProgress()
		{
			return UnpackNextUtilPacket<ProfileProgress>(233);
		}

		public CancelPurchaseResponse GetCancelPurchaseResponse()
		{
			return UnpackNextUtilPacket<CancelPurchaseResponse>(275);
		}

		public PurchaseMethod GetPurchaseMethodResponse()
		{
			return UnpackNextUtilPacket<PurchaseMethod>(272);
		}

		public PurchaseResponse GetPurchaseResponse()
		{
			return UnpackNextUtilPacket<PurchaseResponse>(256);
		}

		public PurchaseWithGoldResponse GetPurchaseWithGoldResponse()
		{
			return UnpackNextUtilPacket<PurchaseWithGoldResponse>(280);
		}

		public RecruitAFriendDataResponse GetRecruitAFriendDataResponse()
		{
			return UnpackNextUtilPacket<RecruitAFriendDataResponse>(338);
		}

		public RecruitAFriendURLResponse GetRecruitAFriendUrlResponse()
		{
			return UnpackNextUtilPacket<RecruitAFriendURLResponse>(336);
		}

		public RewardProgress GetRewardProgress()
		{
			return UnpackNextUtilPacket<RewardProgress>(271);
		}

		public ServerResult GetServerResult()
		{
			return UnpackNextGamePacket<ServerResult>(23);
		}

		public SetFavoriteHeroResponse GetSetFavoriteHeroResponse()
		{
			return UnpackNextUtilPacket<SetFavoriteHeroResponse>(320);
		}

		public SetProgressResponse GetSetProgressResponse()
		{
			return UnpackNextUtilPacket<SetProgressResponse>(296);
		}

		public SpectatorNotify GetSpectatorNotify()
		{
			return UnpackNextGamePacket<SpectatorNotify>(24);
		}

		public AIDebugInformation GetAIDebugInformation()
		{
			return UnpackNextGamePacket<AIDebugInformation>(6);
		}

		public RopeTimerDebugInformation GetRopeTimerDebugInformation()
		{
			return UnpackNextGamePacket<RopeTimerDebugInformation>(8);
		}

		public ScriptDebugInformation GetScriptDebugInformation()
		{
			return UnpackNextGamePacket<ScriptDebugInformation>(7);
		}

		public GameRoundHistory GetGameRoundHistory()
		{
			return UnpackNextGamePacket<GameRoundHistory>(30);
		}

		public GameRealTimeBattlefieldRaces GetGameRealTimeBattlefieldRaces()
		{
			return UnpackNextGamePacket<GameRealTimeBattlefieldRaces>(31);
		}

		public BattlegroundsRatingChange GetBattlegroundsRatingChange()
		{
			return UnpackNextGamePacket<BattlegroundsRatingChange>(34);
		}

		public void SendPVPDRSessionStartRequest(bool paidEntry)
		{
			PVPDRSessionStartRequest pVPDRSessionStartRequest = new PVPDRSessionStartRequest();
			pVPDRSessionStartRequest.PaidEntry = paidEntry;
			SendUtilPacket(382, UtilSystemId.CLIENT, pVPDRSessionStartRequest);
		}

		public PVPDRSessionStartResponse GetPVPDRSessionStartResponse()
		{
			return UnpackNextUtilPacket<PVPDRSessionStartResponse>(383);
		}

		public void SendPVPDRSessionEndRequest()
		{
			SendUtilPacket(388, UtilSystemId.CLIENT, new PVPDRSessionEndRequest());
		}

		public PVPDRSessionEndResponse GetPVPDRSessionEndResponse()
		{
			return UnpackNextUtilPacket<PVPDRSessionEndResponse>(389);
		}

		public void SendPVPDRSessionInfoRequest()
		{
			SendUtilPacket(376, UtilSystemId.CLIENT, new PVPDRSessionInfoRequest());
		}

		public PVPDRSessionInfoResponse GetPVPDRSessionInfoResponse()
		{
			return UnpackNextUtilPacket<PVPDRSessionInfoResponse>(377);
		}

		public void SendPVPDRRetireRequest()
		{
			SendUtilPacket(380, UtilSystemId.CLIENT, new PVPDRRetireRequest());
		}

		public PVPDRRetireResponse GetPVPDRRetireResponse()
		{
			return UnpackNextUtilPacket<PVPDRRetireResponse>(381);
		}

		public GameGuardianVars GetGameGuardianVars()
		{
			return UnpackNextGamePacket<GameGuardianVars>(35);
		}

		public UpdateBattlegroundInfo GetBattlegroundInfo()
		{
			return UnpackNextGamePacket<UpdateBattlegroundInfo>(53);
		}

		public DebugMessage GetDebugMessage()
		{
			return UnpackNextGamePacket<DebugMessage>(5);
		}

		public ScriptLogMessage GetScriptLogMessage()
		{
			return UnpackNextGamePacket<ScriptLogMessage>(50);
		}

		public SubscribeResponse GetSubscribeResponse()
		{
			return UnpackNextUtilPacket<SubscribeResponse>(315);
		}

		public TavernBrawlInfo GetTavernBrawlInfo()
		{
			return UnpackNextUtilPacket<TavernBrawlInfo>(316);
		}

		public TavernBrawlPlayerRecordResponse GeTavernBrawlPlayerRecordResponse()
		{
			return UnpackNextUtilPacket<TavernBrawlPlayerRecordResponse>(317);
		}

		public void GetThirdPartyPurchaseStatus(string transactionId)
		{
			SendUtilPacket(294, UtilSystemId.BATTLEPAY, new GetThirdPartyPurchaseStatus
			{
				ThirdPartyId = transactionId
			});
		}

		public ThirdPartyPurchaseStatusResponse GetThirdPartyPurchaseStatusResponse()
		{
			return UnpackNextUtilPacket<ThirdPartyPurchaseStatusResponse>(295);
		}

		public TriggerEventResponse GetTriggerEventResponse()
		{
			return UnpackNextUtilPacket<TriggerEventResponse>(299);
		}

		public PegasusGame.TurnTimer GetTurnTimerInfo()
		{
			return UnpackNextGamePacket<PegasusGame.TurnTimer>(9);
		}

		public UpdateAccountLicensesResponse GetUpdateAccountLicensesResponse()
		{
			return UnpackNextUtilPacket<UpdateAccountLicensesResponse>(331);
		}

		public UpdateLoginComplete GetUpdateLoginComplete()
		{
			return UnpackNextUtilPacket<UpdateLoginComplete>(307);
		}

		public UserUI GetUserUi()
		{
			return UnpackNextGamePacket<UserUI>(15);
		}

		public ValidateAchieveResponse GetValidateAchieveResponse()
		{
			return UnpackNextUtilPacket<ValidateAchieveResponse>(285);
		}

		public Coins GetCoins()
		{
			return UnpackNextUtilPacket<Coins>(608);
		}

		public void SetFavoriteCoin(int coin)
		{
			SendUtilPacket(609, UtilSystemId.CLIENT, new SetFavoriteCoin
			{
				Coin = coin
			});
		}

		public UtilLogRelay GetUtilLogRelay()
		{
			return UnpackNextUtilPacket<UtilLogRelay>(390);
		}

		public GameLogRelay GetGameLogRelay()
		{
			return UnpackNextGamePacket<GameLogRelay>(51);
		}

		public AchievementProgress GetAchievementInGameProgress()
		{
			return UnpackNextGamePacket<AchievementProgress>(52);
		}

		public AchievementComplete GetAchievementComplete()
		{
			return UnpackNextUtilPacket<AchievementComplete>(618);
		}

		public SetFavoriteCoinResponse GetSetFavoriteCoinResponse()
		{
			return UnpackNextUtilPacket<SetFavoriteCoinResponse>(610);
		}

		public CoinUpdate GetCoinUpdate()
		{
			return UnpackNextUtilPacket<CoinUpdate>(611);
		}

		public bool GotoGameServer(string address, uint port)
		{
			return dispatcherImpl.ConnectToGameServer(address, port);
		}

		public void RegisterGameServerConnectEventListener(Action<BattleNetErrors> listener)
		{
			dispatcherImpl.OnGameServerConnectEvent += listener;
		}

		public void RemoveGameServerConnectEventListener(Action<BattleNetErrors> listener)
		{
			dispatcherImpl.OnGameServerConnectEvent -= listener;
		}

		public void RegisterGameServerDisconnectEventListener(Action<BattleNetErrors> listener)
		{
			dispatcherImpl.OnGameServerDisconnectEvent += listener;
		}

		public void RemoveGameServerDisconnectEventListener(Action<BattleNetErrors> listener)
		{
			dispatcherImpl.OnGameServerDisconnectEvent -= listener;
		}

		public void SendSpectatorGameHandshake(string version, Platform platform, GameServerInfo info, BnetId bnetId)
		{
			SendGamePacket(22, new SpectatorHandshake
			{
				GameHandle = (int)info.GameHandle,
				Password = info.SpectatorPassword,
				Version = version,
				Platform = platform,
				GameAccountId = bnetId
			});
		}

		public void SendGameHandshake(GameServerInfo info, Platform platform)
		{
			SendGamePacket(168, new Handshake
			{
				Password = info.AuroraPassword,
				GameHandle = (int)info.GameHandle,
				ClientHandle = (int)info.ClientHandle,
				Mission = info.Mission,
				Version = info.Version,
				Platform = platform
			});
		}

		public bool HasErrors()
		{
			return dispatcherImpl.HasUtilErrors();
		}

		public bool HasDebugPackets()
		{
			return dispatcherImpl.DebugConnectionManager.HaveDebugPackets();
		}

		public bool HasGamePackets()
		{
			return dispatcherImpl.HasGamePackets();
		}

		public bool HasGameServerConnection()
		{
			return dispatcherImpl.HasGameServerConnection();
		}

		public bool HasUtilPackets()
		{
			return dispatcherImpl.HasUtilPackets();
		}

		public bool IsConnectedToGameServer()
		{
			return dispatcherImpl.IsConnectedToGameServer();
		}

		public void UpdateGameServerConnection()
		{
			dispatcherImpl.ProcessGamePackets();
		}

		public void ProcessUtilPackets()
		{
			dispatcherImpl.ProcessUtilPackets();
		}

		public bool TryConnectDebugConsole()
		{
			return dispatcherImpl.DebugConnectionManager.TryConnectDebugConsole();
		}

		public void UpdateDebugConsole()
		{
			dispatcherImpl.DebugConnectionManager.Update();
		}

		public void MassDisenchant()
		{
			SendUtilPacket(268, UtilSystemId.CLIENT, new MassDisenchantRequest());
		}

		public PegasusPacket NextDebugPacket()
		{
			return dispatcherImpl.DebugConnectionManager.NextDebugPacket();
		}

		public int NextDebugPacketType()
		{
			return dispatcherImpl.DebugConnectionManager.NextDebugConsoleType();
		}

		public PegasusPacket NextGamePacket()
		{
			return dispatcherImpl.NextGamePacket();
		}

		public int NextGamePacketType()
		{
			return dispatcherImpl.NextGameType();
		}

		public ResponseWithRequest NextUtilPacket()
		{
			return dispatcherImpl.NextUtilPacket();
		}

		public int NextUtilPacketType()
		{
			return dispatcherImpl.NextUtilType();
		}

		public void OnDebugPacketReceived(PegasusPacket packet)
		{
			dispatcherImpl.DebugConnectionManager.OnPacketReceived(packet);
		}

		public void PushReceivedGamePacket(PegasusPacket packet)
		{
			dispatcherImpl.OnGamePacketReceived(packet, packet.Type);
		}

		public void OnGamePacketReceived(PegasusPacket packet, int packetTypeId)
		{
			dispatcherImpl.OnGamePacketReceived(packet, packetTypeId);
		}

		public void OnLoginComplete()
		{
			dispatcherImpl.OnLoginComplete();
		}

		public void OnLoginStarted()
		{
			dispatcherImpl.DebugConnectionManager.OnLoginStarted();
		}

		public void OnUtilPacketReceived(PegasusPacket packet, int packetTypeId)
		{
			dispatcherImpl.OnUtilPacketReceived(packet, packetTypeId);
		}

		public void OnStartupPacketSequenceComplete()
		{
			dispatcherImpl.OnStartupPacketSequenceComplete();
		}

		public void OpenBooster(int boosterTypeId, long fsgId)
		{
			SendUtilPacket(225, UtilSystemId.CLIENT, new OpenBooster
			{
				BoosterType = boosterTypeId,
				FsgId = fsgId
			});
		}

		public void PurchaseViaGold(int quantity, ProductType product, int data)
		{
			SendUtilPacket(279, UtilSystemId.CLIENT, new PurchaseWithGold
			{
				Product = product,
				Quantity = quantity,
				Data = data
			});
		}

		public void RenameDeck(long deckId, string name)
		{
			SendUtilPacket(211, UtilSystemId.CLIENT, new RenameDeck
			{
				Deck = deckId,
				Name = name
			});
		}

		public void RequestAccountLicensesUpdate()
		{
			SendUtilPacket(276, UtilSystemId.BATTLEPAY, new UpdateAccountLicenses());
		}

		public void RequestAdventureProgress()
		{
			SendUtilPacket(305, UtilSystemId.CLIENT, new GetAdventureProgress());
		}

		public void RequestAssetsVersion(Platform platform, long cachedCollectionVersion, List<GetAssetsVersion.DeckModificationTimes> deckTimes, long cachedCollectionVersionLastModified)
		{
			SendUtilPacket(303, UtilSystemId.CLIENT, new GetAssetsVersion
			{
				Platform = platform,
				ClientCollectionVersion = cachedCollectionVersion,
				CachedDeckModificationTimes = deckTimes,
				CollectionVersionLastModified = cachedCollectionVersionLastModified
			}, RequestPhase.STARTUP);
		}

		public void RequestCancelQuest(int questId)
		{
			SendUtilPacket(281, UtilSystemId.CLIENT, new CancelQuest
			{
				QuestId = questId
			});
		}

		public void RequestDeckContents(long[] deckIds)
		{
			GetDeckContents getDeckContents = new GetDeckContents();
			getDeckContents.DeckId.AddRange(deckIds);
			SendUtilPacket(214, UtilSystemId.CLIENT, getDeckContents);
		}

		public void RequestAccountInfoNetCacheObject(GetAccountInfo.Request request)
		{
			SendUtilPacket(201, UtilSystemId.CLIENT, new GetAccountInfo
			{
				Request_ = request
			}, RequestPhase.RUNNING, (int)request);
		}

		public void RequestNetCacheObjectList(List<GetAccountInfo.Request> requests, List<GenericRequest> genericRequests)
		{
			GenericRequestList genericRequestList = new GenericRequestList();
			foreach (GetAccountInfo.Request request in requests)
			{
				genericRequestList.Requests.Add(new GenericRequest
				{
					RequestId = 201,
					RequestSubId = (int)request
				});
			}
			if (genericRequests != null)
			{
				foreach (GenericRequest genericRequest in genericRequests)
				{
					genericRequestList.Requests.Add(genericRequest);
				}
			}
			SendUtilPacket(327, UtilSystemId.CLIENT, genericRequestList);
		}

		public void RequestProcessRecruitAFriend()
		{
			SendUtilPacket(339, UtilSystemId.RECRUIT_A_FRIEND, new ProcessRecruitAFriend());
		}

		public void RequestPurchaseMethod(long? pmtProductId, int quantity, PegasusShared.Currency currency, string deviceId, Platform platform)
		{
			SendUtilPacket(250, UtilSystemId.BATTLEPAY, new GetPurchaseMethod
			{
				PmtProductId = pmtProductId.GetValueOrDefault(),
				Quantity = quantity,
				CurrencyCode = currency.Code,
				DeviceId = deviceId,
				Platform = platform
			});
		}

		public void RequestRecruitAFriendData()
		{
			SendUtilPacket(337, UtilSystemId.RECRUIT_A_FRIEND, new GetRecruitAFriendData());
		}

		public void RequestRecruitAFriendUrl(Platform platform)
		{
			SendUtilPacket(335, UtilSystemId.RECRUIT_A_FRIEND, new GetRecruitAFriendURL
			{
				Platform = platform
			});
		}

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
			SendUtilPacket(257, UtilSystemId.CLIENT, buySellCard);
		}

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
				if (fsgId.HasValue)
				{
					getAssetRequest.FsgId = fsgId.Value;
				}
				SendUtilPacket(321, UtilSystemId.CLIENT, getAssetRequest);
			}
		}

		public void SendChoices(int choicesId, List<int> picks)
		{
			SendGamePacket(3, new ChooseEntities
			{
				Id = choicesId,
				Entities = picks
			});
		}

		public void SendDebugCommandRequest(DebugCommandRequest packet)
		{
			SendUtilPacket(323, UtilSystemId.CLIENT, packet);
		}

		public void SendDebugConsoleResponse(int responseType, string message)
		{
			if (dispatcherImpl.DebugConnectionManager.IsActive())
			{
				dispatcherImpl.DebugConnectionManager.SendDebugConsoleResponse(responseType, message);
			}
		}

		public void SendDeckData(DeckSetData packet)
		{
			SendUtilPacket(222, UtilSystemId.CLIENT, packet);
		}

		public void SendDeckTemplateSource(long deckId, int templateId)
		{
			SendUtilPacket(332, UtilSystemId.CLIENT, new DeckSetTemplateSource
			{
				Deck = deckId,
				TemplateId = templateId
			});
		}

		public void SendFreeDeckChoice(int classId, long noticeId)
		{
			SendUtilPacket(333, UtilSystemId.CLIENT, new FreeDeckChoice
			{
				ClassId = classId,
				NoticeId = noticeId
			});
		}

		public void SendSmartDeckRequest(SmartDeckRequest packet)
		{
			SendUtilPacket(369, UtilSystemId.COLLECTION, packet);
		}

		public void SendOfflineDeckContentsRequest()
		{
			SendUtilPacket(371, UtilSystemId.CLIENT, new OfflineDeckContentsRequest());
		}

		public void SendEmote(int emoteId)
		{
			SendGamePacket(15, new UserUI
			{
				Emote = emoteId
			});
		}

		public bool AllowDebugConnections()
		{
			return dispatcherImpl.DebugConnectionManager.AllowDebugConnections();
		}

		public void SendDebugConsoleCommand(string command)
		{
			SendGamePacket(123, new DebugConsoleCommand
			{
				Command = command
			});
		}

		public void SendOption(int choiceId, int index, int target, int subOption, int position)
		{
			SendGamePacket(2, new ChooseOption
			{
				Id = choiceId,
				Index = index,
				Target = target,
				SubOption = subOption,
				Position = position
			});
		}

		public void SendPing()
		{
			SendGamePacket(115, new Ping());
		}

		public void SendRemoveAllSpectators(bool regeneratePassword)
		{
			SendGamePacket(26, new RemoveSpectators
			{
				KickAllSpectators = true,
				RegenerateSpectatorPassword = regeneratePassword
			});
		}

		public void SendRemoveSpectators(bool regeneratePassword, List<BnetId> spectators)
		{
			SendGamePacket(26, new RemoveSpectators
			{
				RegenerateSpectatorPassword = regeneratePassword,
				TargetGameaccountIds = spectators
			});
		}

		public void SendSpectatorInvite(BnetId targetBnetId, BnetId targetGameAccountId)
		{
			SendGamePacket(25, new InviteToSpectate
			{
				TargetBnetAccountId = targetBnetId,
				TargetGameAccountId = targetGameAccountId
			});
		}

		public void EnsureSubscribedTo(UtilSystemId systemChannel)
		{
			dispatcherImpl.EnsureSubscribedTo(systemChannel);
		}

		public void SendUnsubscribeRequest(Unsubscribe packet, UtilSystemId systemChannel)
		{
			SendUtilPacket(329, systemChannel, packet);
		}

		public void SendUserUi(int overCard, int heldCard, int arrowOrigin, int x, int y)
		{
			SendGamePacket(15, new UserUI
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

		public void SetClientOptions(SetOptions packet)
		{
			SendUtilPacket(239, UtilSystemId.CLIENT, packet);
		}

		public void SetFavoriteCardBack(int cardBack)
		{
			SendUtilPacket(291, UtilSystemId.CLIENT, new SetFavoriteCardBack
			{
				CardBack = cardBack
			});
		}

		public void SetDisconnectedFromBattleNet()
		{
			dispatcherImpl.SetDisconnectedFromBattleNet();
		}

		public void SetFavoriteHero(int classId, PegasusShared.CardDef heroCardDef)
		{
			SendUtilPacket(319, UtilSystemId.CLIENT, new SetFavoriteHero
			{
				FavoriteHero = new FavoriteHero
				{
					ClassId = classId,
					Hero = heroCardDef
				}
			});
		}

		public void SetProgress(long value)
		{
			SendUtilPacket(230, UtilSystemId.CLIENT, new SetProgress
			{
				Value = value
			}, RequestPhase.STARTUP);
		}

		public bool ShouldIgnoreError(BnetErrorInfo errorInfo)
		{
			return dispatcherImpl.ShouldIgnoreError(errorInfo);
		}

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
			SendUtilPacket(293, UtilSystemId.BATTLEPAY, new SubmitThirdPartyReceipt
			{
				Provider = provider,
				ProductId = productId,
				Quantity = quantity,
				TransactionId = bpayId,
				ReceiptData = thirdPartyReceiptData,
				DeviceId = deviceId
			});
		}

		public double GetTimeLastPingReceieved()
		{
			return dispatcherImpl.TimeLastPingReceived;
		}

		public void SetTimeLastPingReceived(double time)
		{
			dispatcherImpl.TimeLastPingReceived = time;
		}

		public double GetTimeLastPingSent()
		{
			return dispatcherImpl.TimeLastPingSent;
		}

		public void SetTimeLastPingSent(double time)
		{
			dispatcherImpl.TimeLastPingSent = time;
		}

		public void TriggerPlayedNearbyPlayerOnSubnet(ulong lastPlayedBnetHi, ulong lastPlayedBnetLo, ulong lastPlayedStartTime, ulong otherPlayerBnetHi, ulong otherPlayerBnetLo, ulong otherPlayerStartTime)
		{
			SendUtilPacket(298, UtilSystemId.CLIENT, new TriggerPlayedNearbyPlayerOnSubnet
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
			});
		}

		public void SetShouldIgnorePong(bool value)
		{
			dispatcherImpl.ShouldIgnorePong = value;
		}

		public void SetSpoofDisconnected(bool value)
		{
			dispatcherImpl.SpoofDisconnected = value;
		}

		public void SetPingsSinceLastPong(int value)
		{
			dispatcherImpl.PingsSinceLastPong = value;
		}

		public int GetPingsSinceLastPong()
		{
			return dispatcherImpl.PingsSinceLastPong;
		}

		public void ValidateAchieve(int achieveId)
		{
			SendUtilPacket(284, UtilSystemId.CLIENT, new ValidateAchieve
			{
				Achieve = achieveId
			});
		}

		public void RequestTavernBrawlSessionBegin()
		{
			SendUtilPacket(343, UtilSystemId.CLIENT, new TavernBrawlRequestSessionBegin());
		}

		public void TavernBrawlRetire()
		{
			SendUtilPacket(344, UtilSystemId.CLIENT, new TavernBrawlRequestSessionRetire());
		}

		public void AckTavernBrawlSessionRewards()
		{
			SendUtilPacket(345, UtilSystemId.CLIENT, new TavernBrawlAckSessionRewards());
		}

		public TavernBrawlRequestSessionBeginResponse GetTavernBrawlSessionBeginResponse()
		{
			return UnpackNextUtilPacket<TavernBrawlRequestSessionBeginResponse>(347);
		}

		public TavernBrawlRequestSessionRetireResponse GetTavernBrawlSessionRetired()
		{
			return UnpackNextUtilPacket<TavernBrawlRequestSessionRetireResponse>(348);
		}

		public void RequestTavernBrawlInfo(BrawlType brawlType, long? fsgId, byte[] fsgSharedSecretKey)
		{
			RequestTavernBrawlInfo requestTavernBrawlInfo = new RequestTavernBrawlInfo
			{
				BrawlType = brawlType,
				FsgSharedSecretKey = fsgSharedSecretKey
			};
			if (fsgId.HasValue)
			{
				requestTavernBrawlInfo.FsgId = fsgId.Value;
			}
			SendUtilPacket(352, UtilSystemId.CLIENT, requestTavernBrawlInfo);
		}

		public void RequestTavernBrawlPlayerRecord(BrawlType brawlType, long? fsgId, byte[] fsgSharedSecretKey)
		{
			RequestTavernBrawlPlayerRecord requestTavernBrawlPlayerRecord = new RequestTavernBrawlPlayerRecord
			{
				BrawlType = brawlType,
				FsgSharedSecretKey = fsgSharedSecretKey
			};
			if (fsgId.HasValue)
			{
				requestTavernBrawlPlayerRecord.FsgId = fsgId.Value;
			}
			SendUtilPacket(353, UtilSystemId.CLIENT, requestTavernBrawlPlayerRecord);
		}

		public void RequestNearbyFSGs(double latitude, double longitude, double accuracy, List<string> bssids, Platform platform)
		{
			SendUtilPacket(501, UtilSystemId.FIRESIDE_GATHERINGS, new RequestNearbyFSGs
			{
				Location = new GPSCoords
				{
					Latitude = latitude,
					Longitude = longitude,
					Accuracy = accuracy
				},
				Bssids = (bssids ?? new List<string>()),
				Platform = platform
			});
		}

		public void RequestNearbyFSGs(List<string> bssids, Platform platform)
		{
			SendUtilPacket(501, UtilSystemId.FIRESIDE_GATHERINGS, new RequestNearbyFSGs
			{
				Location = null,
				Bssids = (bssids ?? new List<string>()),
				Platform = platform
			});
		}

		public void CheckInToFSG(long fsgId, double latitude, double longitude, double accuracy, List<string> bssids, Platform platform)
		{
			SendUtilPacket(502, UtilSystemId.FIRESIDE_GATHERINGS, new CheckInToFSG
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
			});
		}

		public void CheckInToFSG(long fsgId, List<string> bssids, Platform platform)
		{
			SendUtilPacket(502, UtilSystemId.FIRESIDE_GATHERINGS, new CheckInToFSG
			{
				FsgId = fsgId,
				Location = null,
				Bssids = (bssids ?? new List<string>()),
				Platform = platform
			});
		}

		public void CheckOutOfFSG(long fsgId, Platform platform)
		{
			SendUtilPacket(503, UtilSystemId.FIRESIDE_GATHERINGS, new CheckOutOfFSG
			{
				FsgId = fsgId,
				Platform = platform
			});
		}

		public void InnkeeperSetupFSG(List<string> bssids, long fsgId, Platform platform)
		{
			InnkeeperSetupFSG(bssids, fsgId, null, platform);
		}

		public void InnkeeperSetupFSG(List<string> bssids, long fsgId, GPSCoords location, Platform platform)
		{
			SendUtilPacket(507, UtilSystemId.FIRESIDE_GATHERINGS, new InnkeeperSetupGathering
			{
				Location = location,
				Bssids = (bssids ?? new List<string>()),
				FsgId = fsgId,
				Platform = platform
			});
		}

		public void RequestFSGPatronListUpdate()
		{
			SendUtilPacket(512, UtilSystemId.FIRESIDE_GATHERINGS, new FSGPatronListUpdate());
		}

		public void DraftRequestDisablePremiums()
		{
			SendUtilPacket(354, UtilSystemId.CLIENT, new DraftRequestRemovePremiums());
		}

		public void RequestLeaguePromoteSelf()
		{
			SendUtilPacket(367, UtilSystemId.CLIENT, new LeaguePromoteSelfRequest());
		}

		public RequestNearbyFSGsResponse GetRequestNearbyFSGsResponse()
		{
			return UnpackNextUtilPacket<RequestNearbyFSGsResponse>(504);
		}

		public CheckInToFSGResponse GetCheckInToFSGResponse()
		{
			return UnpackNextUtilPacket<CheckInToFSGResponse>(505);
		}

		public CheckOutOfFSGResponse GetCheckOutOfFSGResponse()
		{
			return UnpackNextUtilPacket<CheckOutOfFSGResponse>(506);
		}

		public InnkeeperSetupGatheringResponse GetInnkeeperSetupGatheringResponse()
		{
			return UnpackNextUtilPacket<InnkeeperSetupGatheringResponse>(508);
		}

		public PatronCheckedInToFSG GetPatronCheckedInToFSG()
		{
			return UnpackNextUtilPacket<PatronCheckedInToFSG>(509);
		}

		public PatronCheckedOutOfFSG GetPatronCheckedOutOfFSG()
		{
			return UnpackNextUtilPacket<PatronCheckedOutOfFSG>(510);
		}

		public FSGPatronListUpdate GetFSGPatronListUpdate()
		{
			return UnpackNextUtilPacket<FSGPatronListUpdate>(512);
		}

		public FSGFeatureConfig GetFSGFeatureConfig()
		{
			return UnpackNextUtilPacket<FSGFeatureConfig>(511);
		}

		public LeaguePromoteSelfResponse GetLeaguePromoteSelfResponse()
		{
			return UnpackNextUtilPacket<LeaguePromoteSelfResponse>(368);
		}

		public SmartDeckResponse GetSmartDeckResponse()
		{
			return UnpackNextUtilPacket<SmartDeckResponse>(370);
		}

		public void RequestGameSaveData(List<long> keys, int clientToken)
		{
			SendUtilPacket(357, UtilSystemId.CLIENT, new GameSaveDataRequest
			{
				KeyIds = keys,
				ClientToken = clientToken
			});
		}

		public GameSaveDataResponse GetGameSaveDataResponse()
		{
			return UnpackNextUtilPacket<GameSaveDataResponse>(358);
		}

		public void SetGameSaveData(List<GameSaveDataUpdate> dataUpdates, int clientToken)
		{
			SendUtilPacket(359, UtilSystemId.CLIENT, new SetGameSaveData
			{
				Data = dataUpdates,
				ClientToken = clientToken
			});
		}

		public SetGameSaveDataResponse GetSetGameSaveDataResponse()
		{
			return UnpackNextUtilPacket<SetGameSaveDataResponse>(360);
		}

		public void SendLocateCheatServerRequest()
		{
			SendUtilPacket(361, UtilSystemId.CHEAT, new LocateCheatServerRequest());
		}

		public LocateCheatServerResponse GetLocateCheatServerResponse()
		{
			return UnpackNextUtilPacket<LocateCheatServerResponse>(362);
		}

		public GameToConnectNotification GetGameToConnectNotification()
		{
			return UnpackNextUtilPacket<GameToConnectNotification>(363);
		}

		public void GetServerTimeRequest(long now)
		{
			SendUtilPacket(364, UtilSystemId.CLIENT, new GetServerTimeRequest
			{
				ClientUnixTime = now
			});
		}

		public ResponseWithRequest<GetServerTimeResponse, GetServerTimeRequest> GetServerTimeResponse()
		{
			return UnpackNextUtilPacketWithRequest<GetServerTimeResponse, GetServerTimeRequest>(365);
		}

		public void RequestBaconRatingInfo()
		{
			SendUtilPacket(372, UtilSystemId.CLIENT, new BattlegroundsRatingInfoRequest());
		}

		public ResponseWithRequest<BattlegroundsRatingInfoResponse, BattlegroundsRatingInfoRequest> BattlegroundsRatingInfoResponse()
		{
			return UnpackNextUtilPacketWithRequest<BattlegroundsRatingInfoResponse, BattlegroundsRatingInfoRequest>(373);
		}

		public void RequestBattlegroundsPremiumStatus()
		{
			SendUtilPacket(374, UtilSystemId.CLIENT, new BattlegroundsPremiumStatusRequest());
		}

		public ResponseWithRequest<BattlegroundsPremiumStatusResponse, BattlegroundsPremiumStatusRequest> GetBattlegroundsPremiumStatus()
		{
			return UnpackNextUtilPacketWithRequest<BattlegroundsPremiumStatusResponse, BattlegroundsPremiumStatusRequest>(375);
		}

		public void RequestPVPDRStatsInfo()
		{
			SendUtilPacket(378, UtilSystemId.CLIENT, new PVPDRStatsInfoRequest());
		}

		public ResponseWithRequest<PVPDRStatsInfoResponse, PVPDRStatsInfoRequest> PVPDRStatsInfoResponse()
		{
			return UnpackNextUtilPacketWithRequest<PVPDRStatsInfoResponse, PVPDRStatsInfoRequest>(379);
		}

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
			SendUtilPacket(366, UtilSystemId.BATTLEPAY, new ReportBlizzardCheckoutStatus
			{
				Status = status,
				TransactionId = transactionId,
				ProductId = productId,
				Currency = currency,
				ClientUnixTime = now
			});
		}

		private void SendGamePacket(int packetId, IProtoBuf body)
		{
			dispatcherImpl.SendGamePacket(packetId, body);
		}

		private void SendUtilPacket(int type, UtilSystemId system, IProtoBuf body, RequestPhase requestPhase = RequestPhase.RUNNING, int subId = 0)
		{
			dispatcherImpl.SendUtilPacket(type, system, body, requestPhase, subId);
		}

		private T UnpackNextDebugPacket<T>(int packetId) where T : IProtoBuf
		{
			return Unpack<T>(dispatcherImpl.DebugConnectionManager.NextDebugPacket(), packetId);
		}

		private T UnpackNextGamePacket<T>(int packetId) where T : IProtoBuf
		{
			return Unpack<T>(dispatcherImpl.NextGamePacket(), packetId);
		}

		private T UnpackNextUtilPacket<T>(int packetId) where T : IProtoBuf
		{
			return Unpack<T>(dispatcherImpl.NextUtilPacket().Response, packetId);
		}

		private ResponseWithRequest<T, U> UnpackNextUtilPacketWithRequest<T, U>(int packetId) where T : IProtoBuf where U : IProtoBuf, new()
		{
			ResponseWithRequest<T, U> responseWithRequest = new ResponseWithRequest<T, U>();
			ResponseWithRequest responseWithRequest2 = dispatcherImpl.NextUtilPacket();
			if (responseWithRequest2.Request != null && responseWithRequest2.Request.Body is byte[])
			{
				responseWithRequest.Request = (U)ProtobufUtil.ParseFromGeneric<U>((byte[])responseWithRequest2.Request.Body);
			}
			responseWithRequest.Response = Unpack<T>(responseWithRequest2.Response, packetId);
			return responseWithRequest;
		}

		private T Unpack<T>(PegasusPacket p, int packetId) where T : IProtoBuf
		{
			if (p == null || p.Type != packetId || !(p.Body is T))
			{
				return default(T);
			}
			return (T)p.Body;
		}
	}
}
