using System;
using System.ComponentModel;

// Token: 0x020008F7 RID: 2295
public enum Option
{
	// Token: 0x040066D0 RID: 26320
	INVALID,
	// Token: 0x040066D1 RID: 26321
	[Description("clientOptionsVersion")]
	CLIENT_OPTIONS_VERSION,
	// Token: 0x040066D2 RID: 26322
	[Description("sound")]
	SOUND,
	// Token: 0x040066D3 RID: 26323
	[Description("music")]
	MUSIC,
	// Token: 0x040066D4 RID: 26324
	[Description("cursor")]
	CURSOR,
	// Token: 0x040066D5 RID: 26325
	[Description("hud")]
	HUD,
	// Token: 0x040066D6 RID: 26326
	[Description("streaming")]
	STREAMING,
	// Token: 0x040066D7 RID: 26327
	[Description("soundvolume")]
	SOUND_VOLUME,
	// Token: 0x040066D8 RID: 26328
	[Description("musicvolume")]
	MUSIC_VOLUME,
	// Token: 0x040066D9 RID: 26329
	[Description("graphicswidth")]
	GFX_WIDTH,
	// Token: 0x040066DA RID: 26330
	[Description("graphicsheight")]
	GFX_HEIGHT,
	// Token: 0x040066DB RID: 26331
	[Description("graphicsfullscreen")]
	GFX_FULLSCREEN,
	// Token: 0x040066DC RID: 26332
	[Description("hasseennewcinematic")]
	HAS_SEEN_NEW_CINEMATIC,
	// Token: 0x040066DD RID: 26333
	[Description("graphicsquality")]
	GFX_QUALITY,
	// Token: 0x040066DE RID: 26334
	[Description("fakepackopening")]
	FAKE_PACK_OPENING,
	// Token: 0x040066DF RID: 26335
	[Description("fakepackcount")]
	FAKE_PACK_COUNT,
	// Token: 0x040066E0 RID: 26336
	[Description("healthygamingdebug")]
	HEALTHY_GAMING_DEBUG,
	// Token: 0x040066E1 RID: 26337
	[Description("laststate")]
	LAST_SCENE_MODE,
	// Token: 0x040066E2 RID: 26338
	[Description("locale")]
	LOCALE,
	// Token: 0x040066E3 RID: 26339
	[Description("idlekicker")]
	IDLE_KICKER,
	// Token: 0x040066E4 RID: 26340
	[Description("idlekicktime")]
	IDLE_KICK_TIME,
	// Token: 0x040066E5 RID: 26341
	[Description("backgroundsound")]
	BACKGROUND_SOUND,
	// Token: 0x040066E6 RID: 26342
	[Description("preferredregion")]
	PREFERRED_REGION,
	// Token: 0x040066E7 RID: 26343
	[Description("forceShowIks")]
	FORCE_SHOW_IKS,
	// Token: 0x040066E8 RID: 26344
	[Description("peguidebug")]
	PEGUI_DEBUG,
	// Token: 0x040066E9 RID: 26345
	[Description("nearbyplayers2")]
	NEARBY_PLAYERS,
	// Token: 0x040066EA RID: 26346
	[Description("wincameraclear")]
	GFX_WIN_CAMERA_CLEAR,
	// Token: 0x040066EB RID: 26347
	[Description("msaa")]
	GFX_MSAA,
	// Token: 0x040066EC RID: 26348
	[Description("fxaa")]
	GFX_FXAA,
	// Token: 0x040066ED RID: 26349
	[Description("targetframerate")]
	GFX_TARGET_FRAME_RATE,
	// Token: 0x040066EE RID: 26350
	[Description("vsync")]
	GFX_VSYNC,
	// Token: 0x040066EF RID: 26351
	[Description("cardback")]
	CARD_BACK,
	// Token: 0x040066F0 RID: 26352
	[Description("cardback2")]
	CARD_BACK2,
	// Token: 0x040066F1 RID: 26353
	[Description("localtutorialprogress")]
	LOCAL_TUTORIAL_PROGRESS,
	// Token: 0x040066F2 RID: 26354
	[Description("connecttobnet")]
	CONNECT_TO_AURORA,
	// Token: 0x040066F3 RID: 26355
	[Description("reconnect")]
	RECONNECT,
	// Token: 0x040066F4 RID: 26356
	[Description("reconnectTimeout")]
	RECONNECT_TIMEOUT,
	// Token: 0x040066F5 RID: 26357
	[Description("reconnectRetryTime")]
	RECONNECT_RETRY_TIME,
	// Token: 0x040066F6 RID: 26358
	[Description("changedcardsdata")]
	CHANGED_CARDS_DATA,
	// Token: 0x040066F7 RID: 26359
	[Description("kelthuzadtaunts")]
	KELTHUZADTAUNTS,
	// Token: 0x040066F8 RID: 26360
	[Description("winposx")]
	GFX_WIN_POSX,
	// Token: 0x040066F9 RID: 26361
	[Description("winposy")]
	GFX_WIN_POSY,
	// Token: 0x040066FA RID: 26362
	[Description("preferredcdnindex")]
	PREFERRED_CDN_INDEX,
	// Token: 0x040066FB RID: 26363
	[Description("lastfaileddopversion")]
	LAST_FAILED_DOP_VERSION,
	// Token: 0x040066FC RID: 26364
	[Description("touchmode")]
	TOUCH_MODE,
	// Token: 0x040066FD RID: 26365
	[Description("gfxdevicewarning")]
	SHOWN_GFX_DEVICE_WARNING,
	// Token: 0x040066FE RID: 26366
	[Description("intro")]
	INTRO,
	// Token: 0x040066FF RID: 26367
	[Description("disableloginpopups")]
	DISABLE_LOGIN_POPUPS,
	// Token: 0x04006700 RID: 26368
	[Description("tutoriallostprogress")]
	TUTORIAL_LOST_PROGRESS,
	// Token: 0x04006701 RID: 26369
	[Description("errorScreen")]
	ERROR_SCREEN,
	// Token: 0x04006702 RID: 26370
	[Description("innkeepersSpecialViews")]
	IKS_VIEW_ATTEMPTS,
	// Token: 0x04006703 RID: 26371
	[Description("innkeepersSpecialLastDownloadTime")]
	IKS_LAST_DOWNLOAD_TIME,
	// Token: 0x04006704 RID: 26372
	[Description("innkeepersSpecialLastResponse")]
	IKS_LAST_DOWNLOAD_RESPONSE,
	// Token: 0x04006705 RID: 26373
	[Description("innkeepersSpecialCacheAge")]
	IKS_CACHE_AGE,
	// Token: 0x04006706 RID: 26374
	[Description("innkeepersSpecialLastStoredResponse")]
	IKS_LAST_STORED_RESPONSE,
	// Token: 0x04006707 RID: 26375
	[Description("cheatHistory")]
	CHEAT_HISTORY,
	// Token: 0x04006708 RID: 26376
	[Description("preloadCardAssets")]
	PRELOAD_CARD_ASSETS,
	// Token: 0x04006709 RID: 26377
	[Description("collectionPremiumType")]
	COLLECTION_PREMIUM_TYPE,
	// Token: 0x0400670A RID: 26378
	[Description("devTimescale")]
	DEV_TIMESCALE,
	// Token: 0x0400670B RID: 26379
	[Description("innkeepersSpecialLastShownAd")]
	IKS_LAST_SHOWN_AD,
	// Token: 0x0400670C RID: 26380
	[Description("seenPackProductList")]
	SEEN_PACK_PRODUCT_LIST,
	// Token: 0x0400670D RID: 26381
	[Description("showStandardOnly")]
	SHOW_STANDARD_ONLY,
	// Token: 0x0400670E RID: 26382
	[Description("disableSetRotationIntro")]
	DISABLE_SET_ROTATION_INTRO,
	// Token: 0x0400670F RID: 26383
	[Description("skipallmulligans")]
	SKIP_ALL_MULLIGANS,
	// Token: 0x04006710 RID: 26384
	[Description("isTemporaryAccountCheat")]
	IS_TEMPORARY_ACCOUNT_CHEAT,
	// Token: 0x04006711 RID: 26385
	[Description("temporaryAccountData")]
	TEMPORARY_ACCOUNT_DATA,
	// Token: 0x04006712 RID: 26386
	[Description("disallowedCloudStorage")]
	DISALLOWED_CLOUD_STORAGE,
	// Token: 0x04006713 RID: 26387
	[Description("createdAccount")]
	CREATED_ACCOUNT,
	// Token: 0x04006714 RID: 26388
	[Description("lastEventHealUpDate")]
	LAST_HEAL_UP_EVENT_DATE,
	// Token: 0x04006715 RID: 26389
	[Description("pushNotificationStatus")]
	PUSH_NOTIFICATION_STATUS,
	// Token: 0x04006716 RID: 26390
	[Description("dbfXmlLoading")]
	DBF_XML_LOADING,
	// Token: 0x04006717 RID: 26391
	[Description("hasShownDevicePerformanceWarning")]
	HAS_SHOWN_DEVICE_PERFORMANCE_WARNING,
	// Token: 0x04006718 RID: 26392
	[Description("screenshotDirectory")]
	SCREENSHOT_DIRECTORY,
	// Token: 0x04006719 RID: 26393
	[Description("simulatingCellular")]
	SIMULATE_CELLULAR,
	// Token: 0x0400671A RID: 26394
	[Description("assetDownloadEnabled")]
	ASSET_DOWNLOAD_ENABLED,
	// Token: 0x0400671B RID: 26395
	[Description("updateState")]
	UPDATE_STATE,
	// Token: 0x0400671C RID: 26396
	[Description("nativeUpdateState")]
	NATIVE_UPDATE_STATE,
	// Token: 0x0400671D RID: 26397
	[Description("AskUnknownApps")]
	ASK_UNKNOWN_APPS,
	// Token: 0x0400671E RID: 26398
	[Description("launchCount")]
	LAUNCH_COUNT,
	// Token: 0x0400671F RID: 26399
	[Description("isInstallReported")]
	IS_INSTALL_REPORTED,
	// Token: 0x04006720 RID: 26400
	[Description("firstInstallTime")]
	FIRST_INSTALL_TIME,
	// Token: 0x04006721 RID: 26401
	[Description("updatedClientVersion")]
	UPDATED_CLIENT_VERSION,
	// Token: 0x04006722 RID: 26402
	[Description("simulateNoInternet")]
	SIMULATE_NO_INTERNET,
	// Token: 0x04006723 RID: 26403
	[Description("updateStopLevel")]
	UPDATE_STOP_LEVEL,
	// Token: 0x04006724 RID: 26404
	[Description("maxDownloadSpeed")]
	MAX_DOWNLOAD_SPEED,
	// Token: 0x04006725 RID: 26405
	[Description("streamingSpeedInGame")]
	STREAMING_SPEED_IN_GAME,
	// Token: 0x04006726 RID: 26406
	[Description("autoConvertVirtualCurrency")]
	AUTOCONVERT_VIRTUAL_CURRENCY,
	// Token: 0x04006727 RID: 26407
	[Description("streamerMode")]
	STREAMER_MODE,
	// Token: 0x04006728 RID: 26408
	[Description("latestSeenShopProductList")]
	LATEST_SEEN_SHOP_PRODUCT_LIST,
	// Token: 0x04006729 RID: 26409
	[Description("latestDisplayedShopProductList")]
	LATEST_DISPLAYED_SHOP_PRODUCT_LIST,
	// Token: 0x0400672A RID: 26410
	[Description("rankDebug")]
	RANK_DEBUG,
	// Token: 0x0400672B RID: 26411
	[Description("debugCursor")]
	DEBUG_CURSOR,
	// Token: 0x0400672C RID: 26412
	[Description("crashCount")]
	CRASH_COUNT,
	// Token: 0x0400672D RID: 26413
	[Description("exceptionCount")]
	EXCEPTION_COUNT,
	// Token: 0x0400672E RID: 26414
	[Description("lowMemoryCount")]
	LOW_MEMORY_COUNT,
	// Token: 0x0400672F RID: 26415
	[Description("closedWithoutCrash")]
	CLOSED_WITHOUT_CRASH,
	// Token: 0x04006730 RID: 26416
	[Description("ExceptionHash")]
	EXCEPTION_HASH,
	// Token: 0x04006731 RID: 26417
	[Description("LastExceptionHash")]
	LAST_EXCEPTION_HASH,
	// Token: 0x04006732 RID: 26418
	[Description("CrashInARowCount")]
	CRASH_IN_A_ROW_COUNT,
	// Token: 0x04006733 RID: 26419
	[Description("SameExceptionCount")]
	SAME_EXCEPTION_COUNT,
	// Token: 0x04006734 RID: 26420
	[Description("CellPromptThreshold")]
	CELL_PROMPT_THRESHOLD,
	// Token: 0x04006735 RID: 26421
	[Description("DownloadAllFinished")]
	DOWNLOAD_ALL_FINISHED,
	// Token: 0x04006736 RID: 26422
	[Description("DelayedReporterStop")]
	DELAYED_REPORTER_STOP,
	// Token: 0x04006737 RID: 26423
	[Description("ScreenshakeEnabled")]
	SCREEN_SHAKE_ENABLED,
	// Token: 0x04006738 RID: 26424
	[Description("hudconfig")]
	HUD_CONFIG,
	// Token: 0x04006739 RID: 26425
	[Description("hudScale")]
	HUD_SCALE,
	// Token: 0x0400673A RID: 26426
	[Description("EnabledLogList")]
	ENABLED_LOG_LIST,
	// Token: 0x0400673B RID: 26427
	[Description("hasSeenClipboardNotification")]
	HAS_SEEN_CLIPBOARD_NOTIFICATION,
	// Token: 0x0400673C RID: 26428
	[Description("progTileDebug")]
	PROG_TILE_DEBUG,
	// Token: 0x0400673D RID: 26429
	[Description("LastLoginType")]
	LAST_LOGIN_TYPE,
	// Token: 0x0400673E RID: 26430
	[Description("TransitionToken")]
	TRANSITION_AUTH_TOKEN,
	// Token: 0x0400673F RID: 26431
	[Description("TransitionGuestId")]
	TRANSITION_GUEST_ID,
	// Token: 0x04006740 RID: 26432
	[Description("ANRThrottle")]
	ANR_THROTTLE,
	// Token: 0x04006741 RID: 26433
	[Description("ANRWaitSeconds")]
	ANR_WAIT_SECONDS,
	// Token: 0x04006742 RID: 26434
	[Description("AppRatingPopupCount")]
	APP_RATING_POPUP_COUNT,
	// Token: 0x04006743 RID: 26435
	[Description("newestRewardedDeckId")]
	NEWEST_REWARDED_DECK_ID,
	// Token: 0x04006744 RID: 26436
	[Description("showCreateSkipAcct")]
	SHOW_CREATE_SKIP_ACCT,
	// Token: 0x04006745 RID: 26437
	[Description("serverOptionsVersion")]
	SERVER_OPTIONS_VERSION,
	// Token: 0x04006746 RID: 26438
	[Description("pagemouseovers")]
	PAGE_MOUSE_OVERS,
	// Token: 0x04006747 RID: 26439
	[Description("covermouseovers")]
	COVER_MOUSE_OVERS,
	// Token: 0x04006748 RID: 26440
	[Description("aimode")]
	AI_MODE,
	// Token: 0x04006749 RID: 26441
	[Description("practicetipporgress")]
	TIP_PRACTICE_PROGRESS,
	// Token: 0x0400674A RID: 26442
	[Description("playtipprogress")]
	TIP_PLAY_PROGRESS,
	// Token: 0x0400674B RID: 26443
	[Description("forgetipprogress")]
	TIP_FORGE_PROGRESS,
	// Token: 0x0400674C RID: 26444
	[Description("lastChosenPreconHero")]
	LAST_PRECON_HERO_CHOSEN,
	// Token: 0x0400674D RID: 26445
	[Description("lastChosenCustomDeck")]
	LAST_CUSTOM_DECK_CHOSEN,
	// Token: 0x0400674E RID: 26446
	[Description("selectedAdventure")]
	SELECTED_ADVENTURE,
	// Token: 0x0400674F RID: 26447
	[Description("selectedAdventureMode")]
	SELECTED_ADVENTURE_MODE,
	// Token: 0x04006750 RID: 26448
	[Description("lastselectedbooster")]
	LAST_SELECTED_STORE_BOOSTER_ID,
	// Token: 0x04006751 RID: 26449
	[Description("lastselectedadventure")]
	LAST_SELECTED_STORE_ADVENTURE_ID,
	// Token: 0x04006752 RID: 26450
	[Description("lastselectedhero")]
	LAST_SELECTED_STORE_HERO_ID,
	// Token: 0x04006753 RID: 26451
	[Description("seenTB")]
	LATEST_SEEN_TAVERNBRAWL_SEASON,
	// Token: 0x04006754 RID: 26452
	[Description("seenTBScreen")]
	LATEST_SEEN_TAVERNBRAWL_SEASON_CHALKBOARD,
	// Token: 0x04006755 RID: 26453
	[Description("seenCrazyRulesQuote")]
	TIMES_SEEN_TAVERNBRAWL_CRAZY_RULES_QUOTE,
	// Token: 0x04006756 RID: 26454
	[Description("setRotationIntroProgress")]
	SET_ROTATION_INTRO_PROGRESS,
	// Token: 0x04006757 RID: 26455
	[Description("timesMousedOverSwitchFormatButton")]
	TIMES_MOUSED_OVER_SWITCH_FORMAT_BUTTON,
	// Token: 0x04006758 RID: 26456
	[Description("returningPlayerBannerSeen")]
	RETURNING_PLAYER_BANNER_SEEN,
	// Token: 0x04006759 RID: 26457
	[Description("seenTBSessionLimit")]
	LATEST_SEEN_TAVERNBRAWL_SESSION_LIMIT,
	// Token: 0x0400675A RID: 26458
	[Description("lastTavernJoined")]
	LAST_TAVERN_JOINED,
	// Token: 0x0400675B RID: 26459
	[Description("seenFSGB")]
	LATEST_SEEN_FIRESIDEBRAWL_SEASON,
	// Token: 0x0400675C RID: 26460
	[Description("seenFSGBScreen")]
	LATEST_SEEN_FIRESIDEBRAWL_SEASON_CHALKBOARD,
	// Token: 0x0400675D RID: 26461
	[Description("seenScheduledDoubleGoldVO")]
	LATEST_SEEN_SCHEDULED_DOUBLE_GOLD_VO,
	// Token: 0x0400675E RID: 26462
	[Description("seenScheduledAllPopupsShownVO")]
	LATEST_SEEN_SCHEDULED_ALL_POPUPS_SHOWN_VO,
	// Token: 0x0400675F RID: 26463
	[Description("seenScheduledEnteredArenaDraftVO")]
	LATEST_SEEN_SCHEDULED_ENTERED_ARENA_DRAFT,
	// Token: 0x04006760 RID: 26464
	[Description("seenScheduledLoginFlowComplete")]
	LATEST_SEEN_SCHEDULED_LOGIN_FLOW_COMPLETE,
	// Token: 0x04006761 RID: 26465
	[Description("seenWelcomeQuestDialog")]
	LATEST_SEEN_WELCOME_QUEST_DIALOG,
	// Token: 0x04006762 RID: 26466
	[Description("seenCurrencyChangeMessageForVersion")]
	LATEST_SEEN_CURRENCY_CHANGED_VERSION,
	// Token: 0x04006763 RID: 26467
	[Description("seenScheduledWelcomeQuestVO")]
	LATEST_SEEN_SCHEDULED_WELCOME_QUEST_SHOWN_VO,
	// Token: 0x04006764 RID: 26468
	[Description("seenScheduledGenericRewardShownVO")]
	LATEST_SEEN_SCHEDULED_GENERIC_REWARD_SHOWN_VO,
	// Token: 0x04006765 RID: 26469
	[Description("seenScheduledArenaRewardShownVO")]
	LATEST_SEEN_SCHEDULED_ARENA_REWARD_SHOWN_VO,
	// Token: 0x04006766 RID: 26470
	[Description("lastSelectedStorePackType")]
	LAST_SELECTED_STORE_PACK_TYPE,
	// Token: 0x04006767 RID: 26471
	[Description("whizbangPopupCounter")]
	WHIZBANG_POPUP_COUNTER,
	// Token: 0x04006768 RID: 26472
	[Description("setRotationIntroProgressNewPlayer")]
	SET_ROTATION_INTRO_PROGRESS_NEW_PLAYER,
	// Token: 0x04006769 RID: 26473
	[Description("formatType")]
	FORMAT_TYPE,
	// Token: 0x0400676A RID: 26474
	[Description("formatTypeLastPlayed")]
	FORMAT_TYPE_LAST_PLAYED,
	// Token: 0x0400676B RID: 26475
	[Description("hasclickedtournament")]
	HAS_CLICKED_TOURNAMENT,
	// Token: 0x0400676C RID: 26476
	[Description("hasopenedbooster")]
	HAS_OPENED_BOOSTER,
	// Token: 0x0400676D RID: 26477
	[Description("hasseentournament")]
	HAS_SEEN_TOURNAMENT,
	// Token: 0x0400676E RID: 26478
	[Description("hasseencollectionmanager")]
	HAS_SEEN_COLLECTIONMANAGER,
	// Token: 0x0400676F RID: 26479
	[Description("justfinishedtutorial")]
	JUST_FINISHED_TUTORIAL,
	// Token: 0x04006770 RID: 26480
	[Description("showadvancedcollectionmanager")]
	SHOW_ADVANCED_COLLECTIONMANAGER,
	// Token: 0x04006771 RID: 26481
	[Description("hasseenpracticetray")]
	HAS_SEEN_PRACTICE_TRAY,
	// Token: 0x04006772 RID: 26482
	[Description("firstHubVisitPastTutorial")]
	HAS_SEEN_HUB,
	// Token: 0x04006773 RID: 26483
	[Description("firstdeckcomplete")]
	HAS_FINISHED_A_DECK,
	// Token: 0x04006774 RID: 26484
	[Description("hasseenforge")]
	HAS_SEEN_FORGE,
	// Token: 0x04006775 RID: 26485
	[Description("hasseenforgeherochoice")]
	HAS_SEEN_FORGE_HERO_CHOICE,
	// Token: 0x04006776 RID: 26486
	[Description("hasseenforgecardchoice")]
	HAS_SEEN_FORGE_CARD_CHOICE,
	// Token: 0x04006777 RID: 26487
	[Description("hasseenforgecardchoice2")]
	HAS_SEEN_FORGE_CARD_CHOICE2,
	// Token: 0x04006778 RID: 26488
	[Description("hasseenforgeplaymode")]
	HAS_SEEN_FORGE_PLAY_MODE,
	// Token: 0x04006779 RID: 26489
	[Description("hasseenforge1win")]
	HAS_SEEN_FORGE_1WIN,
	// Token: 0x0400677A RID: 26490
	[Description("hasseenforge2loss")]
	HAS_SEEN_FORGE_2LOSS,
	// Token: 0x0400677B RID: 26491
	[Description("hasseenforgeretire")]
	HAS_SEEN_FORGE_RETIRE,
	// Token: 0x0400677C RID: 26492
	[Description("hasseenmulligan")]
	HAS_SEEN_MULLIGAN,
	// Token: 0x0400677D RID: 26493
	[Description("hasSeenExpertAI")]
	HAS_SEEN_EXPERT_AI,
	// Token: 0x0400677E RID: 26494
	[Description("hasSeenExpertAIUnlock")]
	HAS_SEEN_EXPERT_AI_UNLOCK,
	// Token: 0x0400677F RID: 26495
	[Description("hasseendeckhelper")]
	HAS_SEEN_DECK_HELPER,
	// Token: 0x04006780 RID: 26496
	[Description("hasSeenPackOpening")]
	HAS_SEEN_PACK_OPENING,
	// Token: 0x04006781 RID: 26497
	[Description("hasSeenPracticeMode")]
	HAS_SEEN_PRACTICE_MODE,
	// Token: 0x04006782 RID: 26498
	[Description("hasSeenCustomDeckPicker")]
	HAS_SEEN_CUSTOM_DECK_PICKER,
	// Token: 0x04006783 RID: 26499
	[Description("hasSeenAllBasicClassCardsComplete")]
	HAS_SEEN_ALL_BASIC_CLASS_CARDS_COMPLETE,
	// Token: 0x04006784 RID: 26500
	[Description("hasBeenNudgedToCM")]
	HAS_BEEN_NUDGED_TO_CM,
	// Token: 0x04006785 RID: 26501
	[Description("hasAddedCardsToDeck")]
	HAS_ADDED_CARDS_TO_DECK,
	// Token: 0x04006786 RID: 26502
	[Description("tipCraftingUnlocked")]
	TIP_CRAFTING_UNLOCKED,
	// Token: 0x04006787 RID: 26503
	[Description("hasPlayedExpertAI")]
	HAS_PLAYED_EXPERT_AI,
	// Token: 0x04006788 RID: 26504
	[Description("hasDisenchanted")]
	HAS_DISENCHANTED,
	// Token: 0x04006789 RID: 26505
	[Description("hasSeenShowAllCardsReminder")]
	HAS_SEEN_SHOW_ALL_CARDS_REMINDER,
	// Token: 0x0400678A RID: 26506
	[Description("hasSeenCraftingInstruction")]
	HAS_SEEN_CRAFTING_INSTRUCTION,
	// Token: 0x0400678B RID: 26507
	[Description("hasCrafted")]
	HAS_CRAFTED,
	// Token: 0x0400678C RID: 26508
	[Description("inRankedPlayMode")]
	IN_RANKED_PLAY_MODE,
	// Token: 0x0400678D RID: 26509
	[Description("hasSeenTheCoin")]
	HAS_SEEN_THE_COIN,
	// Token: 0x0400678E RID: 26510
	[Description("hasseen100goldReminder")]
	HAS_SEEN_100g_REMINDER,
	// Token: 0x0400678F RID: 26511
	[Description("hasSeenGoldQtyInstruction")]
	HAS_SEEN_GOLD_QTY_INSTRUCTION,
	// Token: 0x04006790 RID: 26512
	[Description("hasSeenLevel3")]
	HAS_SEEN_LEVEL_3,
	// Token: 0x04006791 RID: 26513
	[Description("hasLostInArena")]
	HAS_LOST_IN_ARENA,
	// Token: 0x04006792 RID: 26514
	[Description("hasRunOutOfQuests")]
	HAS_RUN_OUT_OF_QUESTS,
	// Token: 0x04006793 RID: 26515
	[Description("hasAckedArenaRewards")]
	HAS_ACKED_ARENA_REWARDS,
	// Token: 0x04006794 RID: 26516
	[Description("hasSeenStealthTaunter")]
	HAS_SEEN_STEALTH_TAUNTER,
	// Token: 0x04006795 RID: 26517
	[Description("friendslistrequestsectionhide")]
	FRIENDS_LIST_REQUEST_SECTION_HIDE,
	// Token: 0x04006796 RID: 26518
	[Description("friendslistcurrentgamesectionhide")]
	FRIENDS_LIST_CURRENTGAME_SECTION_HIDE,
	// Token: 0x04006797 RID: 26519
	[Description("friendslistfriendsectionhide")]
	FRIENDS_LIST_FRIEND_SECTION_HIDE,
	// Token: 0x04006798 RID: 26520
	[Description("friendslistnearbysectionhide")]
	FRIENDS_LIST_NEARBYPLAYER_SECTION_HIDE,
	// Token: 0x04006799 RID: 26521
	[Description("friendslistrecruitsectionhide")]
	FRIENDS_LIST_RECRUIT_SECTION_HIDE,
	// Token: 0x0400679A RID: 26522
	[Description("hasSeenHeroicWarning")]
	HAS_SEEN_HEROIC_WARNING,
	// Token: 0x0400679B RID: 26523
	[Description("hasSeenNaxx")]
	HAS_SEEN_NAXX,
	// Token: 0x0400679C RID: 26524
	[Description("hasEnteredNaxx")]
	HAS_ENTERED_NAXX,
	// Token: 0x0400679D RID: 26525
	[Description("hasSeenNaxxClassChallenge")]
	HAS_SEEN_NAXX_CLASS_CHALLENGE,
	// Token: 0x0400679E RID: 26526
	[Description("bundleJustPurchaseInHub")]
	BUNDLE_JUST_PURCHASE_IN_HUB,
	// Token: 0x0400679F RID: 26527
	[Description("hasPlayedNaxx")]
	HAS_PLAYED_NAXX,
	// Token: 0x040067A0 RID: 26528
	[Description("spectatoropenjoin")]
	SPECTATOR_OPEN_JOIN,
	// Token: 0x040067A1 RID: 26529
	[Description("hasstartedadeck")]
	HAS_STARTED_A_DECK,
	// Token: 0x040067A2 RID: 26530
	[Description("hasseencollectionmanagerafterpractice")]
	HAS_SEEN_COLLECTIONMANAGER_AFTER_PRACTICE,
	// Token: 0x040067A3 RID: 26531
	[Description("hasSeenBRM")]
	HAS_SEEN_BRM,
	// Token: 0x040067A4 RID: 26532
	[Description("hasSeenLOE")]
	HAS_SEEN_LOE,
	// Token: 0x040067A5 RID: 26533
	[Description("hasClickedManaTab")]
	HAS_CLICKED_MANA_TAB,
	// Token: 0x040067A6 RID: 26534
	[Description("hasseenforgemaxwin")]
	HAS_SEEN_FORGE_MAX_WIN,
	// Token: 0x040067A7 RID: 26535
	[Description("hasheardtgtpackvo")]
	DEPRECATED_HAS_HEARD_TGT_PACK_VO,
	// Token: 0x040067A8 RID: 26536
	[Description("hasseenloestaffdisappear")]
	HAS_SEEN_LOE_STAFF_DISAPPEAR,
	// Token: 0x040067A9 RID: 26537
	[Description("hasseenloestaffreappear")]
	HAS_SEEN_LOE_STAFF_REAPPEAR,
	// Token: 0x040067AA RID: 26538
	[Description("hasSeenUnlockAllHeroesTransition")]
	HAS_SEEN_UNLOCK_ALL_HEROES_TRANSITION,
	// Token: 0x040067AB RID: 26539
	[Description("createdFirstDeckForClass")]
	SKIP_DECK_TEMPLATE_PAGE_FOR_CLASS_FLAGS,
	// Token: 0x040067AC RID: 26540
	[Description("hasSeenDeckTemplateScreen")]
	HAS_SEEN_DECK_TEMPLATE_SCREEN,
	// Token: 0x040067AD RID: 26541
	[Description("hasClickedDeckTemplateReplace")]
	HAS_CLICKED_DECK_TEMPLATE_REPLACE,
	// Token: 0x040067AE RID: 26542
	[Description("hasSeenDeckTemplateGhostCard")]
	HAS_SEEN_DECK_TEMPLATE_GHOST_CARD,
	// Token: 0x040067AF RID: 26543
	[Description("hasRemovedCardFromDeck")]
	HAS_REMOVED_CARD_FROM_DECK,
	// Token: 0x040067B0 RID: 26544
	[Description("hasSeenDeleteDeckReminder")]
	DEPRECATED_HAS_SEEN_DELETE_DECK_REMINDER,
	// Token: 0x040067B1 RID: 26545
	[Description("hasClickedCollectionButtonForNewCard")]
	HAS_CLICKED_COLLECTION_BUTTON_FOR_NEW_CARD,
	// Token: 0x040067B2 RID: 26546
	[Description("hasClickedCollectionButtonForNewDeck")]
	HAS_CLICKED_COLLECTION_BUTTON_FOR_NEW_DECK,
	// Token: 0x040067B3 RID: 26547
	[Description("hasSeenWildModeVO")]
	HAS_SEEN_WILD_MODE_VO,
	// Token: 0x040067B4 RID: 26548
	[Description("hasSeenStandardModeTutorial")]
	HAS_SEEN_STANDARD_MODE_TUTORIAL,
	// Token: 0x040067B5 RID: 26549
	[Description("needsToMakeStandardDeck")]
	NEEDS_TO_MAKE_STANDARD_DECK,
	// Token: 0x040067B6 RID: 26550
	[Description("hasSeenInvalidRotatedCard")]
	HAS_SEEN_INVALID_ROTATED_CARD,
	// Token: 0x040067B7 RID: 26551
	[Description("showSwitchToWildOnPlayScreen")]
	SHOW_SWITCH_TO_WILD_ON_PLAY_SCREEN,
	// Token: 0x040067B8 RID: 26552
	[Description("showSwitchToWildOnCreateDeck")]
	SHOW_SWITCH_TO_WILD_ON_CREATE_DECK,
	// Token: 0x040067B9 RID: 26553
	[Description("showWildDisclaimerPopupOnCreateDeck")]
	SHOW_WILD_DISCLAIMER_POPUP_ON_CREATE_DECK,
	// Token: 0x040067BA RID: 26554
	[Description("hasSeenBasicDeckWarning")]
	HAS_SEEN_BASIC_DECK_WARNING,
	// Token: 0x040067BB RID: 26555
	[Description("glowCollectionButtonAfterSetRotation")]
	GLOW_COLLECTION_BUTTON_AFTER_SET_ROTATION,
	// Token: 0x040067BC RID: 26556
	[Description("hasSeenSetFilterTutorial")]
	HAS_SEEN_SET_FILTER_TUTORIAL,
	// Token: 0x040067BD RID: 26557
	[Description("hasSeenRAF")]
	HAS_SEEN_RAF,
	// Token: 0x040067BE RID: 26558
	[Description("hasSeenRAFRecruitURL")]
	HAS_SEEN_RAF_RECRUIT_URL,
	// Token: 0x040067BF RID: 26559
	[Description("hasSeenKara")]
	HAS_SEEN_KARA,
	// Token: 0x040067C0 RID: 26560
	[Description("hasseenheroicbrawl")]
	HAS_SEEN_HEROIC_BRAWL,
	// Token: 0x040067C1 RID: 26561
	[Description("hasHeardReturningPlayerWelcomeBackVO")]
	HAS_HEARD_RETURNING_PLAYER_WELCOME_BACK_VO,
	// Token: 0x040067C2 RID: 26562
	[Description("hadItemThatWillBeRotatedInUpcomingEvent")]
	DEPRECATED_HAD_ITEM_THAT_WILL_BE_ROTATED_IN_UPCOMING_EVENT,
	// Token: 0x040067C3 RID: 26563
	[Description("shouldAutoCheckInToFiresideGatherings")]
	SHOULD_AUTO_CHECK_IN_TO_FIRESIDE_GATHERINGS,
	// Token: 0x040067C4 RID: 26564
	[Description("hasClickedFiresideBrawlButton")]
	HAS_CLICKED_FIRESIDE_BRAWL_BUTTON,
	// Token: 0x040067C5 RID: 26565
	[Description("hasClickedFiresideGatheringsButton")]
	HAS_CLICKED_FIRESIDE_GATHERINGS_BUTTON,
	// Token: 0x040067C6 RID: 26566
	[Description("hasInitiatedFSGScan")]
	HAS_INITIATED_FIRESIDE_GATHERING_SCAN,
	// Token: 0x040067C7 RID: 26567
	[Description("hasDisabledPremiumsThisDraft")]
	HAS_DISABLED_PREMIUMS_THIS_DRAFT,
	// Token: 0x040067C8 RID: 26568
	[Description("hasSeenFreeArenaWinDialogThisDraft")]
	HAS_SEEN_FREE_ARENA_WIN_DIALOG_THIS_DRAFT,
	// Token: 0x040067C9 RID: 26569
	[Description("hasSeenICC")]
	HAS_SEEN_ICC,
	// Token: 0x040067CA RID: 26570
	[Description("seenArenaSeasonStarting")]
	LATEST_SEEN_ARENA_SEASON_STARTING,
	// Token: 0x040067CB RID: 26571
	[Description("seenArenaSeasonEnding")]
	LATEST_SEEN_ARENA_SEASON_ENDING,
	// Token: 0x040067CC RID: 26572
	[Description("hasSeenLOOT")]
	HAS_SEEN_LOOT,
	// Token: 0x040067CD RID: 26573
	[Description("hasSeenLatestDungeonRunComplete")]
	HAS_SEEN_LATEST_DUNGEON_RUN_COMPLETE,
	// Token: 0x040067CE RID: 26574
	[Description("hasSeenLOOTCharacterSelectVO")]
	HAS_SEEN_LOOT_CHARACTER_SELECT_VO,
	// Token: 0x040067CF RID: 26575
	[Description("hasSeenLOOTWelcomeBannerVO")]
	HAS_SEEN_LOOT_WELCOME_BANNER_VO,
	// Token: 0x040067D0 RID: 26576
	[Description("hasSeenLOOTBossFlip1VO")]
	HAS_SEEN_LOOT_BOSS_FLIP_1_VO,
	// Token: 0x040067D1 RID: 26577
	[Description("hasSeenLOOTBossFlip2VO")]
	HAS_SEEN_LOOT_BOSS_FLIP_2_VO,
	// Token: 0x040067D2 RID: 26578
	[Description("hasSeenLOOTBossFlip3VO")]
	HAS_SEEN_LOOT_BOSS_FLIP_3_VO,
	// Token: 0x040067D3 RID: 26579
	[Description("hasSeenLOOTOfferTreasure1VO")]
	HAS_SEEN_LOOT_OFFER_TREASURE_1_VO,
	// Token: 0x040067D4 RID: 26580
	[Description("hasSeenLOOTOfferLootPacks1VO")]
	HAS_SEEN_LOOT_OFFER_LOOT_PACKS_1_VO,
	// Token: 0x040067D5 RID: 26581
	[Description("hasSeenLOOTOfferLootPacks2VO")]
	HAS_SEEN_LOOT_OFFER_LOOT_PACKS_2_VO,
	// Token: 0x040067D6 RID: 26582
	[Description("hasJustSeenLOOTNoTakeCandleVO")]
	HAS_JUST_SEEN_LOOT_NO_TAKE_CANDLE_VO,
	// Token: 0x040067D7 RID: 26583
	[Description("hasSeenLOOTInGameWinVO")]
	HAS_SEEN_LOOT_IN_GAME_WIN_VO,
	// Token: 0x040067D8 RID: 26584
	[Description("hasSeenLOOTInGameLoseVO")]
	HAS_SEEN_LOOT_IN_GAME_LOSE_VO,
	// Token: 0x040067D9 RID: 26585
	[Description("hasSeenLOOTInGameMulligan1VO")]
	HAS_SEEN_LOOT_IN_GAME_MULLIGAN_1_VO,
	// Token: 0x040067DA RID: 26586
	[Description("hasSeenLOOTInGameMulligan2VO")]
	HAS_SEEN_LOOT_IN_GAME_MULLIGAN_2_VO,
	// Token: 0x040067DB RID: 26587
	[Description("hasSeenLOOTCompleteAllClassesVO")]
	HAS_SEEN_LOOT_COMPLETE_ALL_CLASSES_VO,
	// Token: 0x040067DC RID: 26588
	[Description("hasSeenLOOTBossHeroPower")]
	HAS_SEEN_LOOT_BOSS_HERO_POWER,
	// Token: 0x040067DD RID: 26589
	[Description("hasSeenLOOTInGameLose2VO")]
	HAS_SEEN_LOOT_IN_GAME_LOSE_2_VO,
	// Token: 0x040067DE RID: 26590
	[Description("hasSeenRankRevampEndOfGameWinsRequired")]
	HAS_SEEN_RANK_REVAMP_END_OF_GAME_WINS_REQUIRED,
	// Token: 0x040067DF RID: 26591
	[Description("hasSeenGILBonusChallenge")]
	HAS_SEEN_GIL_BONUS_CHALLENGE,
	// Token: 0x040067E0 RID: 26592
	[Description("hasSeenPlayedTess")]
	HAS_SEEN_PLAYED_TESS,
	// Token: 0x040067E1 RID: 26593
	[Description("hasSeenPlayedDarius")]
	HAS_SEEN_PLAYED_DARIUS,
	// Token: 0x040067E2 RID: 26594
	[Description("hasSeenPlayedShaw")]
	HAS_SEEN_PLAYED_SHAW,
	// Token: 0x040067E3 RID: 26595
	[Description("hasSeenPlayedToki")]
	HAS_SEEN_PLAYED_TOKI,
	// Token: 0x040067E4 RID: 26596
	[Description("hasSeenBattlegroundsBoxButton")]
	HAS_SEEN_BATTLEGROUNDS_BOX_BUTTON,
	// Token: 0x040067E5 RID: 26597
	[Description("hasSeenClassicModeVO")]
	HAS_SEEN_CLASSIC_MODE_VO,
	// Token: 0x040067E6 RID: 26598
	[Description("hasAcceptedPrivacyPolicyAndEULA")]
	HAS_ACCEPTED_PRIVACY_POLICY_AND_EULA
}
