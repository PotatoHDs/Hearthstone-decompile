using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FCA RID: 4042
	public static class Global
	{
		// Token: 0x0600B01C RID: 45084 RVA: 0x00366D5C File Offset: 0x00364F5C
		public static Global.AssetFlags ParseAssetFlagsValue(string value)
		{
			Global.AssetFlags result;
			EnumUtils.TryGetEnum<Global.AssetFlags>(value, out result);
			return result;
		}

		// Token: 0x0600B01D RID: 45085 RVA: 0x00366D74 File Offset: 0x00364F74
		public static Global.PresenceStatus ParsePresenceStatusValue(string value)
		{
			Global.PresenceStatus result;
			EnumUtils.TryGetEnum<Global.PresenceStatus>(value, out result);
			return result;
		}

		// Token: 0x0600B01E RID: 45086 RVA: 0x00366D8C File Offset: 0x00364F8C
		public static Global.Region ParseRegionValue(string value)
		{
			Global.Region result;
			EnumUtils.TryGetEnum<Global.Region>(value, out result);
			return result;
		}

		// Token: 0x0600B01F RID: 45087 RVA: 0x00366DA4 File Offset: 0x00364FA4
		public static Global.FormatType ParseFormatTypeValue(string value)
		{
			Global.FormatType result;
			EnumUtils.TryGetEnum<Global.FormatType>(value, out result);
			return result;
		}

		// Token: 0x0600B020 RID: 45088 RVA: 0x00366DBC File Offset: 0x00364FBC
		public static Global.RewardType ParseRewardTypeValue(string value)
		{
			Global.RewardType result;
			EnumUtils.TryGetEnum<Global.RewardType>(value, out result);
			return result;
		}

		// Token: 0x0600B021 RID: 45089 RVA: 0x00366DD4 File Offset: 0x00364FD4
		public static Global.CardPremiumLevel ParseCardPremiumLevelValue(string value)
		{
			Global.CardPremiumLevel result;
			EnumUtils.TryGetEnum<Global.CardPremiumLevel>(value, out result);
			return result;
		}

		// Token: 0x0600B022 RID: 45090 RVA: 0x00366DEC File Offset: 0x00364FEC
		public static Global.BnetGameType ParseBnetGameTypeValue(string value)
		{
			Global.BnetGameType result;
			EnumUtils.TryGetEnum<Global.BnetGameType>(value, out result);
			return result;
		}

		// Token: 0x0600B023 RID: 45091 RVA: 0x00366E04 File Offset: 0x00365004
		public static Global.SoundCategory ParseSoundCategoryValue(string value)
		{
			Global.SoundCategory result;
			EnumUtils.TryGetEnum<Global.SoundCategory>(value, out result);
			return result;
		}

		// Token: 0x0600B024 RID: 45092 RVA: 0x00366E1C File Offset: 0x0036501C
		public static Global.GameStringCategory ParseGameStringCategoryValue(string value)
		{
			Global.GameStringCategory result;
			EnumUtils.TryGetEnum<Global.GameStringCategory>(value, out result);
			return result;
		}

		// Token: 0x020027E7 RID: 10215
		[Flags]
		public enum AssetFlags
		{
			// Token: 0x0400F6F5 RID: 63221
			[Description("none")]
			NONE = 0,
			// Token: 0x0400F6F6 RID: 63222
			[Description("dev_only")]
			DEV_ONLY = 1,
			// Token: 0x0400F6F7 RID: 63223
			[Description("not_packaged_in_client")]
			NOT_PACKAGED_IN_CLIENT = 2,
			// Token: 0x0400F6F8 RID: 63224
			[Description("force_do_not_localize")]
			FORCE_DO_NOT_LOCALIZE = 4
		}

		// Token: 0x020027E8 RID: 10216
		public enum PresenceStatus
		{
			// Token: 0x0400F6FA RID: 63226
			UNKNOWN = -1,
			// Token: 0x0400F6FB RID: 63227
			LOGIN,
			// Token: 0x0400F6FC RID: 63228
			TUTORIAL_PREGAME,
			// Token: 0x0400F6FD RID: 63229
			TUTORIAL_GAME,
			// Token: 0x0400F6FE RID: 63230
			WELCOMEQUESTS,
			// Token: 0x0400F6FF RID: 63231
			HUB,
			// Token: 0x0400F700 RID: 63232
			STORE,
			// Token: 0x0400F701 RID: 63233
			QUESTLOG,
			// Token: 0x0400F702 RID: 63234
			PACKOPENING,
			// Token: 0x0400F703 RID: 63235
			COLLECTION,
			// Token: 0x0400F704 RID: 63236
			DECKEDITOR,
			// Token: 0x0400F705 RID: 63237
			CRAFTING,
			// Token: 0x0400F706 RID: 63238
			PLAY_DECKPICKER,
			// Token: 0x0400F707 RID: 63239
			PLAY_QUEUE,
			// Token: 0x0400F708 RID: 63240
			PLAY_GAME,
			// Token: 0x0400F709 RID: 63241
			PRACTICE_DECKPICKER,
			// Token: 0x0400F70A RID: 63242
			PRACTICE_GAME,
			// Token: 0x0400F70B RID: 63243
			ARENA_PURCHASE,
			// Token: 0x0400F70C RID: 63244
			ARENA_FORGE,
			// Token: 0x0400F70D RID: 63245
			ARENA_IDLE,
			// Token: 0x0400F70E RID: 63246
			ARENA_QUEUE,
			// Token: 0x0400F70F RID: 63247
			ARENA_GAME,
			// Token: 0x0400F710 RID: 63248
			ARENA_REWARD,
			// Token: 0x0400F711 RID: 63249
			FRIENDLY_DECKPICKER,
			// Token: 0x0400F712 RID: 63250
			FRIENDLY_GAME,
			// Token: 0x0400F713 RID: 63251
			ADVENTURE_CHOOSING_MODE,
			// Token: 0x0400F714 RID: 63252
			ADVENTURE_SCENARIO_SELECT,
			// Token: 0x0400F715 RID: 63253
			ADVENTURE_SCENARIO_PLAYING_GAME,
			// Token: 0x0400F716 RID: 63254
			SPECTATING_GAME_TUTORIAL,
			// Token: 0x0400F717 RID: 63255
			SPECTATING_GAME_PRACTICE,
			// Token: 0x0400F718 RID: 63256
			SPECTATING_GAME_PLAY,
			// Token: 0x0400F719 RID: 63257
			SPECTATING_GAME_ARENA,
			// Token: 0x0400F71A RID: 63258
			SPECTATING_GAME_FRIENDLY,
			// Token: 0x0400F71B RID: 63259
			SPECTATING_GAME_ADVENTURE_NAXX_NORMAL,
			// Token: 0x0400F71C RID: 63260
			SPECTATING_GAME_ADVENTURE_NAXX_HEROIC,
			// Token: 0x0400F71D RID: 63261
			SPECTATING_GAME_ADVENTURE_NAXX_CLASS_CHALLENGE,
			// Token: 0x0400F71E RID: 63262
			SPECTATING_GAME_ADVENTURE_BRM_NORMAL,
			// Token: 0x0400F71F RID: 63263
			SPECTATING_GAME_ADVENTURE_BRM_HEROIC,
			// Token: 0x0400F720 RID: 63264
			SPECTATING_GAME_ADVENTURE_BRM_CLASS_CHALLENGE,
			// Token: 0x0400F721 RID: 63265
			TAVERN_BRAWL_SCREEN,
			// Token: 0x0400F722 RID: 63266
			TAVERN_BRAWL_DECKEDITOR,
			// Token: 0x0400F723 RID: 63267
			TAVERN_BRAWL_QUEUE,
			// Token: 0x0400F724 RID: 63268
			TAVERN_BRAWL_GAME,
			// Token: 0x0400F725 RID: 63269
			TAVERN_BRAWL_FRIENDLY_WAITING,
			// Token: 0x0400F726 RID: 63270
			TAVERN_BRAWL_FRIENDLY_GAME,
			// Token: 0x0400F727 RID: 63271
			SPECTATING_GAME_TAVERN_BRAWL,
			// Token: 0x0400F728 RID: 63272
			SPECTATING_GAME_ADVENTURE_LOE_NORMAL,
			// Token: 0x0400F729 RID: 63273
			SPECTATING_GAME_ADVENTURE_LOE_HEROIC,
			// Token: 0x0400F72A RID: 63274
			SPECTATING_GAME_ADVENTURE_LOE_CLASS_CHALLENGE,
			// Token: 0x0400F72B RID: 63275
			SPECTATING_GAME_ADVENTURE_KAR_NORMAL,
			// Token: 0x0400F72C RID: 63276
			SPECTATING_GAME_ADVENTURE_KAR_HEROIC,
			// Token: 0x0400F72D RID: 63277
			SPECTATING_GAME_ADVENTURE_KAR_CLASS_CHALLENGE,
			// Token: 0x0400F72E RID: 63278
			SPECTATING_GAME_RETURNING_PLAYER_CHALLENGE,
			// Token: 0x0400F72F RID: 63279
			FIRESIDE_BRAWL_SCREEN,
			// Token: 0x0400F730 RID: 63280
			SPECTATING_GAME_ADVENTURE_ICC_NORMAL,
			// Token: 0x0400F731 RID: 63281
			SPECTATING_GAME_ADVENTURE_LOOT,
			// Token: 0x0400F732 RID: 63282
			WAIT_FOR_OPPONENT_RECONNECT,
			// Token: 0x0400F733 RID: 63283
			SPECTATING_GAME_ADVENTURE_GIL,
			// Token: 0x0400F734 RID: 63284
			SPECTATING_GAME_ADVENTURE_GIL_BONUS_CHALLENGE,
			// Token: 0x0400F735 RID: 63285
			SPECTATING_GAME_ADVENTURE_BOT,
			// Token: 0x0400F736 RID: 63286
			SPECTATING_GAME_ADVENTURE_TRL,
			// Token: 0x0400F737 RID: 63287
			SPECTATING_GAME_ADVENTURE_DAL,
			// Token: 0x0400F738 RID: 63288
			SPECTATING_GAME_ADVENTURE_DAL_HEROIC,
			// Token: 0x0400F739 RID: 63289
			SPECTATING_GAME_ADVENTURE_ULD,
			// Token: 0x0400F73A RID: 63290
			SPECTATING_GAME_ADVENTURE_ULD_HEROIC,
			// Token: 0x0400F73B RID: 63291
			BATTLEGROUNDS_QUEUE,
			// Token: 0x0400F73C RID: 63292
			BATTLEGROUNDS_GAME,
			// Token: 0x0400F73D RID: 63293
			SPECTATING_GAME_BATTLEGROUNDS,
			// Token: 0x0400F73E RID: 63294
			BATTLEGROUNDS_SCREEN,
			// Token: 0x0400F73F RID: 63295
			SPECTATING_GAME_ADVENTURE_DRG,
			// Token: 0x0400F740 RID: 63296
			SPECTATING_GAME_ADVENTURE_DRG_HEROIC,
			// Token: 0x0400F741 RID: 63297
			SPECTATING_GAME_ADVENTURE_BTP,
			// Token: 0x0400F742 RID: 63298
			SPECTATING_GAME_ADVENTURE_BTA,
			// Token: 0x0400F743 RID: 63299
			SPECTATING_GAME_ADVENTURE_BTA_HEROIC,
			// Token: 0x0400F744 RID: 63300
			SPECTATING_GAME_ADVENTURE_BOH,
			// Token: 0x0400F745 RID: 63301
			DUELS_QUEUE,
			// Token: 0x0400F746 RID: 63302
			DUELS_GAME,
			// Token: 0x0400F747 RID: 63303
			DUELS_BUILDING_DECK,
			// Token: 0x0400F748 RID: 63304
			DUELS_IDLE,
			// Token: 0x0400F749 RID: 63305
			SPECTATING_GAME_DUELS,
			// Token: 0x0400F74A RID: 63306
			DUELS_REWARD,
			// Token: 0x0400F74B RID: 63307
			DUELS_PURCHASE,
			// Token: 0x0400F74C RID: 63308
			VIEWING_JOURNAL,
			// Token: 0x0400F74D RID: 63309
			SPECTATING_GAME_ADVENTURE_BOM,
			// Token: 0x0400F74E RID: 63310
			PLAY_RANKED_STANDARD,
			// Token: 0x0400F74F RID: 63311
			PLAY_RANKED_WILD,
			// Token: 0x0400F750 RID: 63312
			PLAY_RANKED_CLASSIC,
			// Token: 0x0400F751 RID: 63313
			PLAY_CASUAL_STANDARD,
			// Token: 0x0400F752 RID: 63314
			PLAY_CASUAL_WILD,
			// Token: 0x0400F753 RID: 63315
			PLAY_CASUAL_CLASSIC
		}

		// Token: 0x020027E9 RID: 10217
		public enum Region
		{
			// Token: 0x0400F755 RID: 63317
			[Description("region_unknown")]
			REGION_UNKNOWN,
			// Token: 0x0400F756 RID: 63318
			[Description("region_us")]
			REGION_US,
			// Token: 0x0400F757 RID: 63319
			[Description("region_eu")]
			REGION_EU,
			// Token: 0x0400F758 RID: 63320
			[Description("region_kr")]
			REGION_KR,
			// Token: 0x0400F759 RID: 63321
			[Description("region_tw")]
			REGION_TW,
			// Token: 0x0400F75A RID: 63322
			[Description("region_cn")]
			REGION_CN,
			// Token: 0x0400F75B RID: 63323
			[Description("region_sg")]
			REGION_SG,
			// Token: 0x0400F75C RID: 63324
			[Description("region_ptr")]
			REGION_PTR = 98
		}

		// Token: 0x020027EA RID: 10218
		public enum FormatType
		{
			// Token: 0x0400F75E RID: 63326
			[Description("ft_unknown")]
			FT_UNKNOWN,
			// Token: 0x0400F75F RID: 63327
			[Description("ft_wild")]
			FT_WILD,
			// Token: 0x0400F760 RID: 63328
			[Description("ft_standard")]
			FT_STANDARD,
			// Token: 0x0400F761 RID: 63329
			[Description("ft_classic")]
			FT_CLASSIC
		}

		// Token: 0x020027EB RID: 10219
		public enum RewardType
		{
			// Token: 0x0400F763 RID: 63331
			NONE,
			// Token: 0x0400F764 RID: 63332
			GOLD,
			// Token: 0x0400F765 RID: 63333
			DUST,
			// Token: 0x0400F766 RID: 63334
			ARCANE_ORBS,
			// Token: 0x0400F767 RID: 63335
			BOOSTER,
			// Token: 0x0400F768 RID: 63336
			CARD = 6,
			// Token: 0x0400F769 RID: 63337
			RANDOM_CARD,
			// Token: 0x0400F76A RID: 63338
			TAVERN_TICKET,
			// Token: 0x0400F76B RID: 63339
			CARD_BACK,
			// Token: 0x0400F76C RID: 63340
			HERO_SKIN,
			// Token: 0x0400F76D RID: 63341
			CUSTOM_COIN,
			// Token: 0x0400F76E RID: 63342
			REWARD_TRACK_XP_BOOST,
			// Token: 0x0400F76F RID: 63343
			CARD_SUBSET
		}

		// Token: 0x020027EC RID: 10220
		public enum CardPremiumLevel
		{
			// Token: 0x0400F771 RID: 63345
			NORMAL,
			// Token: 0x0400F772 RID: 63346
			GOLDEN,
			// Token: 0x0400F773 RID: 63347
			DIAMOND
		}

		// Token: 0x020027ED RID: 10221
		public enum BnetGameType
		{
			// Token: 0x0400F775 RID: 63349
			[Description("bgt_unknown")]
			BGT_UNKNOWN,
			// Token: 0x0400F776 RID: 63350
			[Description("bgt_friends")]
			BGT_FRIENDS,
			// Token: 0x0400F777 RID: 63351
			[Description("bgt_ranked_standard")]
			BGT_RANKED_STANDARD,
			// Token: 0x0400F778 RID: 63352
			[Description("bgt_arena")]
			BGT_ARENA,
			// Token: 0x0400F779 RID: 63353
			[Description("bgt_vs_ai")]
			BGT_VS_AI,
			// Token: 0x0400F77A RID: 63354
			[Description("bgt_tutorial")]
			BGT_TUTORIAL,
			// Token: 0x0400F77B RID: 63355
			[Description("bgt_async")]
			BGT_ASYNC,
			// Token: 0x0400F77C RID: 63356
			[Description("bgt_casual_standard")]
			BGT_CASUAL_STANDARD = 10,
			// Token: 0x0400F77D RID: 63357
			[Description("bgt_test1")]
			BGT_TEST1,
			// Token: 0x0400F77E RID: 63358
			[Description("bgt_test2")]
			BGT_TEST2,
			// Token: 0x0400F77F RID: 63359
			[Description("bgt_test3")]
			BGT_TEST3,
			// Token: 0x0400F780 RID: 63360
			[Description("bgt_tavernbrawl_pvp")]
			BGT_TAVERNBRAWL_PVP = 16,
			// Token: 0x0400F781 RID: 63361
			[Description("bgt_tavernbrawl_1p_versus_ai")]
			BGT_TAVERNBRAWL_1P_VERSUS_AI,
			// Token: 0x0400F782 RID: 63362
			[Description("bgt_tavernbrawl_2p_coop")]
			BGT_TAVERNBRAWL_2P_COOP,
			// Token: 0x0400F783 RID: 63363
			[Description("bgt_ranked_wild")]
			BGT_RANKED_WILD = 30,
			// Token: 0x0400F784 RID: 63364
			[Description("bgt_casual_wild")]
			BGT_CASUAL_WILD,
			// Token: 0x0400F785 RID: 63365
			[Description("bgt_fsg_brawl_vs_friend")]
			BGT_FSG_BRAWL_VS_FRIEND = 40,
			// Token: 0x0400F786 RID: 63366
			[Description("bgt_fsg_brawl_pvp")]
			BGT_FSG_BRAWL_PVP,
			// Token: 0x0400F787 RID: 63367
			[Description("bgt_fsg_brawl_1p_versus_ai")]
			BGT_FSG_BRAWL_1P_VERSUS_AI,
			// Token: 0x0400F788 RID: 63368
			[Description("bgt_fsg_brawl_2p_coop")]
			BGT_FSG_BRAWL_2P_COOP,
			// Token: 0x0400F789 RID: 63369
			[Description("bgt_ranked_standard_new_player")]
			BGT_RANKED_STANDARD_NEW_PLAYER = 45,
			// Token: 0x0400F78A RID: 63370
			[Description("bgt_battlegrounds")]
			BGT_BATTLEGROUNDS = 50,
			// Token: 0x0400F78B RID: 63371
			[Description("bgt_battlegrounds_friendly")]
			BGT_BATTLEGROUNDS_FRIENDLY,
			// Token: 0x0400F78C RID: 63372
			[Description("bgt_pvpdr_paid")]
			BGT_PVPDR_PAID = 54,
			// Token: 0x0400F78D RID: 63373
			[Description("bgt_pvpdr")]
			BGT_PVPDR,
			// Token: 0x0400F78E RID: 63374
			[Description("bgt_reserved_18_22")]
			BGT_RESERVED_18_22,
			// Token: 0x0400F78F RID: 63375
			[Description("bgt_reserved_18_23")]
			BGT_RESERVED_18_23,
			// Token: 0x0400F790 RID: 63376
			[Description("bgt_ranked_classic")]
			BGT_RANKED_CLASSIC,
			// Token: 0x0400F791 RID: 63377
			[Description("bgt_casual_classic")]
			BGT_CASUAL_CLASSIC,
			// Token: 0x0400F792 RID: 63378
			[Description("bgt_last")]
			BGT_LAST
		}

		// Token: 0x020027EE RID: 10222
		public enum SoundCategory
		{
			// Token: 0x0400F794 RID: 63380
			NONE,
			// Token: 0x0400F795 RID: 63381
			FX,
			// Token: 0x0400F796 RID: 63382
			MUSIC,
			// Token: 0x0400F797 RID: 63383
			VO,
			// Token: 0x0400F798 RID: 63384
			SPECIAL_VO,
			// Token: 0x0400F799 RID: 63385
			SPECIAL_CARD,
			// Token: 0x0400F79A RID: 63386
			AMBIENCE,
			// Token: 0x0400F79B RID: 63387
			SPECIAL_MUSIC,
			// Token: 0x0400F79C RID: 63388
			TRIGGER_VO,
			// Token: 0x0400F79D RID: 63389
			HERO_MUSIC,
			// Token: 0x0400F79E RID: 63390
			BOSS_VO,
			// Token: 0x0400F79F RID: 63391
			RESET_GAME
		}

		// Token: 0x020027EF RID: 10223
		public enum GameStringCategory
		{
			// Token: 0x0400F7A1 RID: 63393
			INVALID,
			// Token: 0x0400F7A2 RID: 63394
			GLOBAL,
			// Token: 0x0400F7A3 RID: 63395
			GLUE,
			// Token: 0x0400F7A4 RID: 63396
			GAMEPLAY,
			// Token: 0x0400F7A5 RID: 63397
			TUTORIAL,
			// Token: 0x0400F7A6 RID: 63398
			PRESENCE,
			// Token: 0x0400F7A7 RID: 63399
			MISSION
		}
	}
}
