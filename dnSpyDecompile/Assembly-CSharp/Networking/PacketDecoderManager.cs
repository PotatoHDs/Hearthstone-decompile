using System;
using BobNetProto;
using PegasusFSG;
using PegasusGame;
using PegasusUtil;

namespace Networking
{
	// Token: 0x02000FB8 RID: 4024
	public class PacketDecoderManager : IPacketDecoderManager
	{
		// Token: 0x0600AFFA RID: 45050 RVA: 0x003662B6 File Offset: 0x003644B6
		public PacketDecoderManager(Map<int, IPacketDecoder> decoders)
		{
			this.packetDecoders = decoders;
		}

		// Token: 0x0600AFFB RID: 45051 RVA: 0x003662C8 File Offset: 0x003644C8
		public PacketDecoderManager(bool registerDebugDecoders)
		{
			this.packetDecoders = new Map<int, IPacketDecoder>
			{
				{
					116,
					new PongPacketDecoder()
				},
				{
					169,
					new DefaultProtobufPacketDecoder<Deadend>()
				},
				{
					167,
					new DefaultProtobufPacketDecoder<DeadendUtil>()
				},
				{
					14,
					new DefaultProtobufPacketDecoder<AllOptions>()
				},
				{
					5,
					new DefaultProtobufPacketDecoder<DebugMessage>()
				},
				{
					17,
					new DefaultProtobufPacketDecoder<EntityChoices>()
				},
				{
					13,
					new DefaultProtobufPacketDecoder<EntitiesChosen>()
				},
				{
					16,
					new DefaultProtobufPacketDecoder<GameSetup>()
				},
				{
					19,
					new DefaultProtobufPacketDecoder<PowerHistory>()
				},
				{
					15,
					new DefaultProtobufPacketDecoder<UserUI>()
				},
				{
					9,
					new DefaultProtobufPacketDecoder<PegasusGame.TurnTimer>()
				},
				{
					10,
					new DefaultProtobufPacketDecoder<NAckOption>()
				},
				{
					12,
					new DefaultProtobufPacketDecoder<GameCanceled>()
				},
				{
					23,
					new DefaultProtobufPacketDecoder<ServerResult>()
				},
				{
					24,
					new DefaultProtobufPacketDecoder<SpectatorNotify>()
				},
				{
					202,
					new DefaultProtobufPacketDecoder<DeckList>()
				},
				{
					207,
					new DefaultProtobufPacketDecoder<InitialClientState>()
				},
				{
					215,
					new DefaultProtobufPacketDecoder<GetDeckContentsResponse>()
				},
				{
					216,
					new DefaultProtobufPacketDecoder<DBAction>()
				},
				{
					217,
					new DefaultProtobufPacketDecoder<DeckCreated>()
				},
				{
					218,
					new DefaultProtobufPacketDecoder<DeckDeleted>()
				},
				{
					219,
					new DefaultProtobufPacketDecoder<DeckRenamed>()
				},
				{
					226,
					new DefaultProtobufPacketDecoder<BoosterContent>()
				},
				{
					208,
					new DefaultProtobufPacketDecoder<GamesInfo>()
				},
				{
					231,
					new DefaultProtobufPacketDecoder<ProfileDeckLimit>()
				},
				{
					233,
					new DefaultProtobufPacketDecoder<ProfileProgress>()
				},
				{
					270,
					new DefaultProtobufPacketDecoder<PlayerRecords>()
				},
				{
					271,
					new DefaultProtobufPacketDecoder<RewardProgress>()
				},
				{
					232,
					new DefaultProtobufPacketDecoder<MedalInfo>()
				},
				{
					246,
					new DefaultProtobufPacketDecoder<DraftBeginning>()
				},
				{
					247,
					new DefaultProtobufPacketDecoder<DraftRetired>()
				},
				{
					248,
					new DefaultProtobufPacketDecoder<DraftChoicesAndContents>()
				},
				{
					249,
					new DefaultProtobufPacketDecoder<DraftChosen>()
				},
				{
					288,
					new DefaultProtobufPacketDecoder<DraftRewardsAcked>()
				},
				{
					251,
					new DefaultProtobufPacketDecoder<DraftError>()
				},
				{
					285,
					new DefaultProtobufPacketDecoder<ValidateAchieveResponse>()
				},
				{
					282,
					new DefaultProtobufPacketDecoder<CancelQuestResponse>()
				},
				{
					264,
					new DefaultProtobufPacketDecoder<GuardianVars>()
				},
				{
					260,
					new DefaultProtobufPacketDecoder<CardValues>()
				},
				{
					258,
					new DefaultProtobufPacketDecoder<BoughtSoldCard>()
				},
				{
					269,
					new DefaultProtobufPacketDecoder<MassDisenchantResponse>()
				},
				{
					265,
					new DefaultProtobufPacketDecoder<BattlePayStatusResponse>()
				},
				{
					295,
					new DefaultProtobufPacketDecoder<ThirdPartyPurchaseStatusResponse>()
				},
				{
					272,
					new DefaultProtobufPacketDecoder<PurchaseMethod>()
				},
				{
					275,
					new DefaultProtobufPacketDecoder<CancelPurchaseResponse>()
				},
				{
					256,
					new DefaultProtobufPacketDecoder<PurchaseResponse>()
				},
				{
					238,
					new DefaultProtobufPacketDecoder<BattlePayConfigResponse>()
				},
				{
					280,
					new DefaultProtobufPacketDecoder<PurchaseWithGoldResponse>()
				},
				{
					283,
					new DefaultProtobufPacketDecoder<HeroXP>()
				},
				{
					254,
					new NoOpPacketDecoder()
				},
				{
					331,
					new DefaultProtobufPacketDecoder<UpdateAccountLicensesResponse>()
				},
				{
					236,
					new DefaultProtobufPacketDecoder<CardBacks>()
				},
				{
					292,
					new DefaultProtobufPacketDecoder<SetFavoriteCardBackResponse>()
				},
				{
					296,
					new DefaultProtobufPacketDecoder<SetProgressResponse>()
				},
				{
					299,
					new DefaultProtobufPacketDecoder<TriggerEventResponse>()
				},
				{
					304,
					new DefaultProtobufPacketDecoder<AssetsVersionResponse>()
				},
				{
					306,
					new DefaultProtobufPacketDecoder<AdventureProgressResponse>()
				},
				{
					336,
					new DefaultProtobufPacketDecoder<RecruitAFriendURLResponse>()
				},
				{
					338,
					new DefaultProtobufPacketDecoder<RecruitAFriendDataResponse>()
				},
				{
					307,
					new DefaultProtobufPacketDecoder<UpdateLoginComplete>()
				},
				{
					311,
					new DefaultProtobufPacketDecoder<AccountLicenseAchieveResponse>()
				},
				{
					315,
					new DefaultProtobufPacketDecoder<SubscribeResponse>()
				},
				{
					316,
					new DefaultProtobufPacketDecoder<TavernBrawlInfo>()
				},
				{
					317,
					new DefaultProtobufPacketDecoder<TavernBrawlPlayerRecordResponse>()
				},
				{
					318,
					new DefaultProtobufPacketDecoder<FavoriteHeroesResponse>()
				},
				{
					320,
					new DefaultProtobufPacketDecoder<SetFavoriteHeroResponse>()
				},
				{
					324,
					new DefaultProtobufPacketDecoder<DebugCommandResponse>()
				},
				{
					325,
					new DefaultProtobufPacketDecoder<AccountLicensesInfoResponse>()
				},
				{
					326,
					new DefaultProtobufPacketDecoder<GenericResponse>()
				},
				{
					328,
					new DefaultProtobufPacketDecoder<ClientRequestResponse>()
				},
				{
					322,
					new DefaultProtobufPacketDecoder<GetAssetResponse>()
				},
				{
					341,
					new DefaultProtobufPacketDecoder<ClientStaticAssetsResponse>()
				},
				{
					333,
					new DefaultProtobufPacketDecoder<ClientStateNotification>()
				},
				{
					347,
					new DefaultProtobufPacketDecoder<TavernBrawlRequestSessionBeginResponse>()
				},
				{
					348,
					new DefaultProtobufPacketDecoder<TavernBrawlRequestSessionRetireResponse>()
				},
				{
					349,
					new DefaultProtobufPacketDecoder<TavernBrawlSessionAckRewardsResponse>()
				},
				{
					351,
					new DefaultProtobufPacketDecoder<ArenaSessionResponse>()
				},
				{
					504,
					new DefaultProtobufPacketDecoder<RequestNearbyFSGsResponse>()
				},
				{
					505,
					new DefaultProtobufPacketDecoder<CheckInToFSGResponse>()
				},
				{
					506,
					new DefaultProtobufPacketDecoder<CheckOutOfFSGResponse>()
				},
				{
					508,
					new DefaultProtobufPacketDecoder<InnkeeperSetupGatheringResponse>()
				},
				{
					509,
					new DefaultProtobufPacketDecoder<PatronCheckedInToFSG>()
				},
				{
					510,
					new DefaultProtobufPacketDecoder<PatronCheckedOutOfFSG>()
				},
				{
					511,
					new DefaultProtobufPacketDecoder<FSGFeatureConfig>()
				},
				{
					512,
					new DefaultProtobufPacketDecoder<FSGPatronListUpdate>()
				},
				{
					355,
					new DefaultProtobufPacketDecoder<DraftRemovePremiumsResponse>()
				},
				{
					368,
					new DefaultProtobufPacketDecoder<LeaguePromoteSelfResponse>()
				},
				{
					358,
					new DefaultProtobufPacketDecoder<GameSaveDataResponse>()
				},
				{
					360,
					new DefaultProtobufPacketDecoder<SetGameSaveDataResponse>()
				},
				{
					362,
					new DefaultProtobufPacketDecoder<LocateCheatServerResponse>()
				},
				{
					365,
					new DefaultProtobufPacketDecoder<GetServerTimeResponse>()
				},
				{
					363,
					new DefaultProtobufPacketDecoder<GameToConnectNotification>()
				},
				{
					6,
					new DefaultProtobufPacketDecoder<AIDebugInformation>()
				},
				{
					8,
					new DefaultProtobufPacketDecoder<RopeTimerDebugInformation>()
				},
				{
					370,
					new DefaultProtobufPacketDecoder<SmartDeckResponse>()
				},
				{
					373,
					new DefaultProtobufPacketDecoder<BattlegroundsRatingInfoResponse>()
				},
				{
					7,
					new DefaultProtobufPacketDecoder<ScriptDebugInformation>()
				},
				{
					30,
					new DefaultProtobufPacketDecoder<GameRoundHistory>()
				},
				{
					375,
					new DefaultProtobufPacketDecoder<BattlegroundsPremiumStatusResponse>()
				},
				{
					31,
					new DefaultProtobufPacketDecoder<GameRealTimeBattlefieldRaces>()
				},
				{
					34,
					new DefaultProtobufPacketDecoder<BattlegroundsRatingChange>()
				},
				{
					35,
					new DefaultProtobufPacketDecoder<GameGuardianVars>()
				},
				{
					50,
					new DefaultProtobufPacketDecoder<ScriptLogMessage>()
				},
				{
					334,
					new DefaultProtobufPacketDecoder<FreeDeckChoiceResponse>()
				},
				{
					390,
					new DefaultProtobufPacketDecoder<UtilLogRelay>()
				},
				{
					51,
					new DefaultProtobufPacketDecoder<GameLogRelay>()
				},
				{
					52,
					new DefaultProtobufPacketDecoder<AchievementProgress>()
				},
				{
					618,
					new DefaultProtobufPacketDecoder<AchievementComplete>()
				},
				{
					383,
					new DefaultProtobufPacketDecoder<PVPDRSessionStartResponse>()
				},
				{
					389,
					new DefaultProtobufPacketDecoder<PVPDRSessionEndResponse>()
				},
				{
					377,
					new DefaultProtobufPacketDecoder<PVPDRSessionInfoResponse>()
				},
				{
					379,
					new DefaultProtobufPacketDecoder<PVPDRStatsInfoResponse>()
				},
				{
					381,
					new DefaultProtobufPacketDecoder<PVPDRRetireResponse>()
				},
				{
					601,
					new DefaultProtobufPacketDecoder<PlayerQuestStateUpdate>()
				},
				{
					602,
					new DefaultProtobufPacketDecoder<PlayerQuestPoolStateUpdate>()
				},
				{
					603,
					new DefaultProtobufPacketDecoder<PlayerAchievementStateUpdate>()
				},
				{
					614,
					new DefaultProtobufPacketDecoder<PlayerRewardTrackStateUpdate>()
				},
				{
					617,
					new DefaultProtobufPacketDecoder<RewardTrackXpNotification>()
				},
				{
					619,
					new DefaultProtobufPacketDecoder<RewardTrackUnclaimedNotification>()
				},
				{
					607,
					new DefaultProtobufPacketDecoder<RerollQuestResponse>()
				},
				{
					608,
					new DefaultProtobufPacketDecoder<Coins>()
				},
				{
					610,
					new DefaultProtobufPacketDecoder<SetFavoriteCoinResponse>()
				},
				{
					611,
					new DefaultProtobufPacketDecoder<CoinUpdate>()
				},
				{
					53,
					new DefaultProtobufPacketDecoder<UpdateBattlegroundInfo>()
				}
			};
			if (registerDebugDecoders)
			{
				this.packetDecoders.Add(123, new DefaultProtobufPacketDecoder<DebugConsoleCommand>());
				this.packetDecoders.Add(124, new DefaultProtobufPacketDecoder<DebugConsoleResponse>());
			}
		}

		// Token: 0x0600AFFC RID: 45052 RVA: 0x00366A81 File Offset: 0x00364C81
		public bool CanDecodePacket(int packetId)
		{
			return this.packetDecoders.ContainsKey(packetId);
		}

		// Token: 0x0600AFFD RID: 45053 RVA: 0x00366A90 File Offset: 0x00364C90
		public PegasusPacket DecodePacket(PegasusPacket packet)
		{
			IPacketDecoder packetDecoder;
			if (!this.packetDecoders.TryGetValue(packet.Type, out packetDecoder))
			{
				return null;
			}
			return packetDecoder.DecodePacket(packet);
		}

		// Token: 0x04009514 RID: 38164
		private readonly Map<int, IPacketDecoder> packetDecoders;
	}
}
