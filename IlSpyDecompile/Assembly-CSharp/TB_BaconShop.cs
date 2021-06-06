using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.Progression;
using PegasusGame;
using UnityEngine;

public class TB_BaconShop : MissionEntity
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static Map<GameEntityOption, string> s_stringOptions = InitStringOptions();

	protected const int MISSION_EVENT_SHOP = 1;

	protected const int MISSION_EVENT_COMBAT = 2;

	protected const int MISSION_EVENT_FREEZE_PAUSE = 3;

	protected const int MISSION_EVENT_REFRESH_PAUSE = 4;

	protected const int BACON_TURNS_PER_PRIZE = 4;

	private AssetReference BACON_PHASE_POPUP = new AssetReference("BaconTurnIndicator.prefab:6342ffe02abc782459036566466d277c");

	private static readonly AssetReference Bob_BrassRing_Quote = new AssetReference("Bob_BrassRing_Quote.prefab:89385ff7d67aa1e49bcf25bc15ca61f6");

	protected const string PLAYMAKER_SHOP_STATE = "Shop";

	protected const string PLAYMAKER_COMBAT_STATE = "Combat";

	protected int m_gamePhase = 1;

	private GameObject m_phasePopup;

	private bool m_gameplaySceneLoaded;

	private Notification m_techLevelCounter;

	private int m_displayedTechLevelNumber;

	private List<BaconHeroMulliganBestPlaceVisual> m_mulliganBestPlaceVisuals = new List<BaconHeroMulliganBestPlaceVisual>();

	private readonly EmoteType[] m_gameNotificationEmotes = new EmoteType[10]
	{
		EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_01,
		EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_02,
		EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_03,
		EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_04,
		EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_05,
		EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_06,
		EmoteType.BATTLEGROUNDS_VISUAL_TRIPLE,
		EmoteType.BATTLEGROUNDS_VISUAL_HOT_STREAK,
		EmoteType.BATTLEGROUNDS_VISUAL_KNOCK_OUT,
		EmoteType.BATTLEGROUNDS_VISUAL_BANANA
	};

	private readonly EmoteType[] m_priorityEmotes = new EmoteType[1] { EmoteType.BATTLEGROUNDS_VISUAL_BANANA };

	private Map<int, bool> m_emotesAllowedForPlayer = new Map<int, bool>();

	private Map<int, QueueList<NotificationManager.SpeechBubbleOptions>> m_emotesQueuedForPlayer = new Map<int, QueueList<NotificationManager.SpeechBubbleOptions>>();

	private Map<int, LinkedList<NotificationManager.SpeechBubbleOptions>> m_gameNotificationsQueuedForPlayer = new Map<int, LinkedList<NotificationManager.SpeechBubbleOptions>>();

	private bool m_gameNotificationEmotesAllowed = true;

	private static readonly PlatformDependentValue<Vector3> BATTLEGROUNDS_MULLIGAN_ACTOR_SCALE = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(1.5f, 1.1f, 1.5f),
		Phone = new Vector3(0.9f, 1.1f, 0.9f)
	};

	protected const float DELAY_BEFORE_FREEZE_BUTTON_ACTIVE = 0.75f;

	protected const float DELAY_BEFORE_REFRESH_BUTTON_ACTIVE = 0.75f;

	private const float ZONE_PLAY_BASE_TRANSITION_TIME = 0.5f;

	protected const float DELAY_BEFORE_SHOWING_TUTORIAL_POPUPS = 3f;

	protected Notification m_buyButtonTutorialNotification;

	protected Notification m_enemyMinionTutorialNotification;

	protected Notification m_playMinionTutorialNotification;

	protected bool m_hasSeenBuyButtonTutorial;

	protected bool m_hasSeenEnemyMinionTutorial;

	protected bool m_hasSeenPlayMinionTutorial;

	protected Coroutine m_buyButtonTutorialCoroutine;

	protected Coroutine m_enemyMinionTutorialCoroutine;

	protected Coroutine m_playMinionTutorialCoroutine;

	private const float IDLE_VO_RATE = 0.05f;

	private const float SPECIAL_IDLE_VO_RATE = 0.1f;

	private const float UPGRADE_SHOP_VO_RATE = 0.25f;

	private const float RECRUIT_SMALL_VO_RATE = 0.15f;

	private const float RECRUIT_MEDIUM_VO_RATE = 0.2f;

	private const float RECRUIT_LARGE_VO_RATE = 0.25f;

	private const float TRIPLE_VO_RATE = 0.25f;

	private const float SELLING_VO_RATE = 0.15f;

	private const float FREEZING_VO_RATE = 0.1f;

	private const float REFRESHING_VO_RATE = 0.1f;

	private const float POSSIBLE_TRIPLE_VO_RATE = 0.25f;

	private const GameSaveKeyId GAME_SAVE_PARENT_KEY = GameSaveKeyId.BACON;

	private long m_hasSeenInGameWinVO;

	private long m_hasSeenInGameLoseVO;

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AFK_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AFK_01.prefab:f2f1ffe83d98b8b41b35eb26ed4d693f");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterFreezing_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterFreezing_01.prefab:4dc4f16c60d79ed40be28f898346df02");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterFreezing_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterFreezing_02.prefab:7d7ee4a2ade3b074887d5f84ba468cad");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterSelling_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterSelling_01.prefab:d71f34687d09a064bab5d202ea3fb965");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterSelling_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterSelling_02.prefab:4d26d158ea8cad747a185591e39f9b1c");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_01.prefab:d3ad51eb14e20324387e5dbbd1e82811");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_02.prefab:d46007a5929546448984c12a47029d1f");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_03.prefab:03260a54e677e4247aa19eb29662371e");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_04 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_04.prefab:ecf9f68fe25195a4a93b50d0c8e82a1a");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterTriple_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterTriple_01.prefab:dc92cab5423afa045b4ad528dd25f9d5");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterTriple_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterTriple_02.prefab:d845164292fb45a4f85eed478ad5d1c2");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterTriple_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterTriple_03.prefab:4704869d519d6e7479602dd15e12b175");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Ahead_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Ahead_01.prefab:3288c872e5b6fa94cab1cde547ffa249");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Ahead_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Ahead_02.prefab:2a404fb49fc15fe4880764fb8051dd3d");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Ahead_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Ahead_03.prefab:68a0c922c8462dc469834130d94e3fae");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_BattleEndFirstLoss_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_BattleEndFirstLoss_01.prefab:e92aab6f04ddc794fb25b092f7850cf1");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Behind_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Behind_01.prefab:9949ef92552e12d409b91401b66855f6");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Behind_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Behind_02.prefab:82611a7dc8d52934fad8a41dfa69df7e");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Behind_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Behind_03.prefab:e966c47cb01c4d8498d22e1819f56d7a");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_CombatLoss_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_CombatLoss_01.prefab:e93a9d925b5026648ac4886f50ed97b6");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_CombatLoss_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_CombatLoss_02.prefab:a928e53cb8bd8e24093e439ac8165ac3");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_CombatLoss_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_CombatLoss_03.prefab:6bc0f5c5cb0a76b438db710005f31efe");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_CombatWin_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_CombatWin_01.prefab:70e7bbcc29ab44448aeea263db38edb8");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_CombatWin_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_CombatWin_02.prefab:89433e3f8e6a7c94d873d809fdd1f174");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_CombatWin_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_CombatWin_03.prefab:8ff8566f08747ad4bb76409e6db1504b");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_FirstBattle_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_FirstBattle_01.prefab:b88923936df527147b6eda2517ce91ef");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_FirstDefeat_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_FirstDefeat_01.prefab:4ddd2298c91dc9649b98c65a0cef0760");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_FirstPlace_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_FirstPlace_01.prefab:8df89f94428cec44f95052ec460ed183");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_FirstPlace_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_FirstPlace_02.prefab:e1b62ce3616dc684a9c10326bfc91805");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_FirstPlace_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_FirstPlace_03.prefab:3334db6851a1b29408176a6da35485cc");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_FirstVictory_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_FirstVictory_01.prefab:e40b154f86185d3428ffa48867241f76");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Flavor_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Flavor_01.prefab:4c2351f6456373847a9751832273237e");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Flavor_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Flavor_02.prefab:59fd24afbf2c79e4b849aa06dfa0f887");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Flavor_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Flavor_03.prefab:35a54fd7d02b7d04da5e45553a2a15d6");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Flavor_04 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Flavor_04.prefab:b276d6b9caa6d42409fac92ca276f52a");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Flavor_05 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Flavor_05.prefab:8854bc6d0924d394f9db40a1dbf04b91");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Flavor_06 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Flavor_06.prefab:08ddc4abcd45d8041aa5b2966a154bb7");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Flavor_07 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Flavor_07.prefab:722d1d42625746a4c9e0c719a932cacd");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Flavor_08 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Flavor_08.prefab:9c55261ed5a012141ba405958a673e3b");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_General_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_General_01.prefab:4afb188175871e640abb1747e4850e07");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_General_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_General_02.prefab:d5908d1fd355b8c4b8344e300dc4fc42");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_General_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_General_03.prefab:ef1bc7fb54548df48b81a875a2936a33");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_HeroSelection_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_HeroSelection_01.prefab:93cd3efc86126de478be0e56c8e275a7");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Hire_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Hire_01.prefab:bfd9513b46b92e84da5f22e01a0387a4");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Hire_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Hire_02.prefab:eb20d844bee8bdf4f9cbb514c8ab8580");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Idle_01.prefab:a8ec2302c41e78f4e89fc7c9f8528dc7");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Idle_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Idle_02.prefab:3808bb035b74ac04f9bb4be91009e2b7");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Idle_03.prefab:34248aac29c16274c95fb999635368ff");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Idle_04 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Idle_04.prefab:511dea6cbf561ba44b20f6dc56b0a300");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Idle_05 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Idle_05.prefab:0fcad57569928814aaa426ca3b9f03f9");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_ModeSelect_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_ModeSelect_01.prefab:261a9714c4cf3ad4d8944d9127a38ddf");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_NewGame_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_NewGame_01.prefab:4395f5f45bfe3ab4ab959b3d2b7476d5");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_NewGame_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_NewGame_02.prefab:e3c7fdc1472d0284691bcea3b672a96f");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_NewGame_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_NewGame_03.prefab:f60a6cb74fe1cac488ee17d4f7b5d6f7");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_NewModeLaunch_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_NewModeLaunch_01.prefab:bfb8f00e5538f5f46a7f1d7dc786b495");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_NewModeLaunchalt_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_NewModeLaunchalt_01.prefab:d8b03d2cbf46cb54680e5503cd84f350");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_01.prefab:5192079f42e32944fb565694e6dfe411");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_02.prefab:1e21d2bfca0cbed4799ae963f7ced37b");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_03.prefab:907297373eaf42d44b8fa703fd3cce1d");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_01.prefab:9efcc8572df531d439a3153a55f56014");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_02.prefab:fc02b3772b2cc3f42882b6d210369eb0");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_03.prefab:e3cbf2a35ac2e8245b5bb3de3baa054e");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_01.prefab:347c19b9fd13bcf4db40d99a28ed0e9b");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_02.prefab:eb000d8de28cd6d478b9a718ebe1fd9e");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_03.prefab:291677e8b1c4fdc4c97cf78ecbf9f822");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitWork_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitWork_01.prefab:a5e1a6db102be6d4495aa1cd7dc7ddfc");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_ShopFirstTime_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_ShopFirstTime_01.prefab:8070938a2c3ba2f4ea92b7f0b5fdf280");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_ShopToCombat_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_ShopToCombat_01.prefab:d030c882e8a5da84e8c51af1134d9196");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_ShopToCombat_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_ShopToCombat_02.prefab:233a050c0d7df76419ac0f03f9273cde");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_ShopUpgrade_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_ShopUpgrade_01.prefab:f5019f07757dde341aae503b53a9102e");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Triple_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Triple_01.prefab:26a5500e887280c40a810c01741e2544");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Triple_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Triple_02.prefab:1aff064425948044791b8b9e3f8de61b");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Triple_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Triple_03.prefab:e14f16322b47b814d8ccb07a60ccf6d1");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_UpgradeShop_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_UpgradeShop_01.prefab:ec1459e08d9b5a04e97c6a3499505cf6");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_99h_Male_Human_Idle_01, VO_DALA_BOSS_99h_Male_Human_Idle_02, VO_DALA_BOSS_99h_Male_Human_Idle_03, VO_DALA_BOSS_99h_Male_Human_Idle_04, VO_DALA_BOSS_99h_Male_Human_Idle_05 };

	private static List<string> m_SpecialIdleLines = new List<string> { VO_DALA_BOSS_99h_Male_Human_Flavor_01, VO_DALA_BOSS_99h_Male_Human_Flavor_02, VO_DALA_BOSS_99h_Male_Human_Flavor_03, VO_DALA_BOSS_99h_Male_Human_Flavor_04, VO_DALA_BOSS_99h_Male_Human_Flavor_05, VO_DALA_BOSS_99h_Male_Human_Flavor_06, VO_DALA_BOSS_99h_Male_Human_Flavor_07, VO_DALA_BOSS_99h_Male_Human_Flavor_08 };

	private static List<string> m_ShopUpgradeLines = new List<string> { VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_01, VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_02, VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_03 };

	private static List<string> m_RecruitSmallLines = new List<string> { VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_01, VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_02, VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_03 };

	private static List<string> m_RecruitMediumLines = new List<string> { VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_01, VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_02, VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_03 };

	private static List<string> m_RecruitLargeLines = new List<string> { VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_01, VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_02, VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_03 };

	private static List<string> m_TripleLines = new List<string> { VO_DALA_BOSS_99h_Male_Human_AfterTriple_01, VO_DALA_BOSS_99h_Male_Human_AfterTriple_02, VO_DALA_BOSS_99h_Male_Human_AfterTriple_03, VO_DALA_BOSS_99h_Male_Human_Triple_03 };

	private static List<string> m_SellingLines = new List<string> { VO_DALA_BOSS_99h_Male_Human_AfterSelling_01, VO_DALA_BOSS_99h_Male_Human_AfterSelling_02 };

	private static List<string> m_FreezingLines = new List<string> { VO_DALA_BOSS_99h_Male_Human_AfterFreezing_01, VO_DALA_BOSS_99h_Male_Human_AfterFreezing_02 };

	private static List<string> m_RefreshLines = new List<string> { VO_DALA_BOSS_99h_Male_Human_Hire_01, VO_DALA_BOSS_99h_Male_Human_Hire_02 };

	private static List<string> m_PossibleTripleLines = new List<string> { VO_DALA_BOSS_99h_Male_Human_Triple_01, VO_DALA_BOSS_99h_Male_Human_Triple_02 };

	private static List<string> m_NewGameLines = new List<string> { VO_DALA_BOSS_99h_Male_Human_NewGame_01, VO_DALA_BOSS_99h_Male_Human_NewGame_02, VO_DALA_BOSS_99h_Male_Human_NewGame_03 };

	private static List<string> m_PostCombatGeneralLines = new List<string> { VO_DALA_BOSS_99h_Male_Human_General_01, VO_DALA_BOSS_99h_Male_Human_General_02, VO_DALA_BOSS_99h_Male_Human_General_03 };

	private static List<string> m_PostCombatWinLines = new List<string> { VO_DALA_BOSS_99h_Male_Human_CombatWin_01, VO_DALA_BOSS_99h_Male_Human_CombatWin_02, VO_DALA_BOSS_99h_Male_Human_CombatWin_03 };

	private static List<string> m_PostCombatLoseLines = new List<string> { VO_DALA_BOSS_99h_Male_Human_CombatLoss_01, VO_DALA_BOSS_99h_Male_Human_CombatLoss_02, VO_DALA_BOSS_99h_Male_Human_CombatLoss_03 };

	private static List<string> m_PostShopGeneralLines = new List<string> { VO_DALA_BOSS_99h_Male_Human_ShopToCombat_01, VO_DALA_BOSS_99h_Male_Human_ShopToCombat_02 };

	private static List<string> m_PostShopLoseLines = new List<string> { VO_DALA_BOSS_99h_Male_Human_Behind_01, VO_DALA_BOSS_99h_Male_Human_Behind_02, VO_DALA_BOSS_99h_Male_Human_Behind_03 };

	private static List<string> m_PostShopWinLines = new List<string> { VO_DALA_BOSS_99h_Male_Human_Ahead_01, VO_DALA_BOSS_99h_Male_Human_Ahead_02, VO_DALA_BOSS_99h_Male_Human_Ahead_03 };

	private static List<string> m_PostShopIsFirstLines = new List<string> { VO_DALA_BOSS_99h_Male_Human_FirstPlace_01, VO_DALA_BOSS_99h_Male_Human_FirstPlace_02, VO_DALA_BOSS_99h_Male_Human_FirstPlace_03 };

	public BattlegroundsRatingChange RatingChangeData { get; set; }

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.ALWAYS_SHOW_MULLIGAN_TIMER,
				true
			},
			{
				GameEntityOption.MULLIGAN_IS_CHOOSE_ONE,
				true
			},
			{
				GameEntityOption.MULLIGAN_TIMER_HAS_ALTERNATE_POSITION,
				true
			},
			{
				GameEntityOption.HERO_POWER_TOOLTIP_SHIFTED_DURING_MULLIGAN,
				true
			},
			{
				GameEntityOption.MULLIGAN_HAS_HERO_LOBBY,
				true
			},
			{
				GameEntityOption.DIM_OPPOSING_HERO_DURING_MULLIGAN,
				true
			},
			{
				GameEntityOption.HANDLE_COIN,
				false
			},
			{
				GameEntityOption.MULLIGAN_USES_ALTERNATE_ACTORS,
				true
			},
			{
				GameEntityOption.DO_OPENING_TAUNTS,
				false
			},
			{
				GameEntityOption.SUPPRESS_CLASS_NAMES,
				true
			},
			{
				GameEntityOption.ALLOW_NAME_BANNER_MODE_ICONS,
				false
			},
			{
				GameEntityOption.USE_COMPACT_ENCHANTMENT_BANNERS,
				true
			},
			{
				GameEntityOption.ALLOW_FATIGUE,
				false
			},
			{
				GameEntityOption.MOUSEOVER_DELAY_OVERRIDDEN,
				true
			},
			{
				GameEntityOption.ALLOW_ENCHANTMENT_SPARKLES,
				false
			},
			{
				GameEntityOption.ALLOW_SLEEP_FX,
				false
			},
			{
				GameEntityOption.HAS_ALTERNATE_ENEMY_EMOTE_ACTOR,
				true
			},
			{
				GameEntityOption.USES_PREMIUM_EMOTES,
				true
			},
			{
				GameEntityOption.CAN_SQUELCH_OPPONENT,
				false
			},
			{
				GameEntityOption.USES_BIG_CARDS,
				false
			},
			{
				GameEntityOption.DISPLAY_MULLIGAN_DETAIL_LABEL,
				true
			}
		};
	}

	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>
		{
			{
				GameEntityOption.ALTERNATE_MULLIGAN_ACTOR_NAME,
				"Bacon_Leaderboard_Hero.prefab:776977f5238a24647adcd67933f7d4b0"
			},
			{
				GameEntityOption.ALTERNATE_MULLIGAN_LOBBY_ACTOR_NAME,
				"Bacon_Leaderboard_Hero.prefab:776977f5238a24647adcd67933f7d4b0"
			},
			{
				GameEntityOption.VICTORY_SCREEN_PREFAB_PATH,
				"BaconTwoScoop.prefab:1e3e06c045e65674f9a8afccb8bcdec4"
			},
			{
				GameEntityOption.DEFEAT_SCREEN_PREFAB_PATH,
				"BaconTwoScoop.prefab:1e3e06c045e65674f9a8afccb8bcdec4"
			},
			{
				GameEntityOption.RULEBOOK_POPUP_PREFAB_PATH,
				"BaconInfoPopup.prefab:d5b6f1d5443d48947891de53cdd6c323"
			},
			{
				GameEntityOption.DEFEAT_AUDIO_PATH,
				null
			}
		};
	}

	public TB_BaconShop()
	{
		m_gameOptions.AddOptions(s_booleanOptions, s_stringOptions);
		HistoryManager.Get().DisableHistory();
		PlayerLeaderboardManager.Get().SetEnabled(enabled: true);
		EndTurnButton.Get().SetDisabled(disabled: true);
		SceneMgr.Get().RegisterSceneLoadedEvent(OnGameplaySceneLoaded);
		InitializePhasePopup();
		InitializeTurnTimer();
		m_gamePhase = 1;
		GameEntity.Coroutines.StartCoroutine(OnShopPhase());
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.BACON, GameSaveKeySubkeyId.BACON_HAS_SEEN_FIRST_VICTORY_TUTORIAL, out m_hasSeenInGameWinVO);
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.BACON, GameSaveKeySubkeyId.BACON_HAS_SEEN_FIRST_DEFEAT_TUTORIAL, out m_hasSeenInGameLoseVO);
		Network.Get().RequestGameRoundHistory();
		Network.Get().RequestRealtimeBattlefieldRaces();
		Network.Get().RegisterNetHandler(BattlegroundsRatingChange.PacketID.ID, OnBattlegroundsRatingChange);
		if (GameState.Get() != null)
		{
			GameState.Get().RegisterTurnChangedListener(OnTurnEnded);
		}
	}

	~TB_BaconShop()
	{
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().UnregisterSceneLoadedEvent(OnGameplaySceneLoaded);
		}
		if (Network.Get() != null)
		{
			Network.Get().RemoveNetHandler(BattlegroundsRatingChange.PacketID.ID, OnBattlegroundsRatingChange);
		}
		if (GameState.Get() != null)
		{
			GameState.Get().UnregisterTurnChangedListener(OnTurnEnded);
		}
	}

	private void OnGameplaySceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (mode == SceneMgr.Mode.GAMEPLAY)
		{
			m_gameplaySceneLoaded = true;
			SceneMgr.Get().UnregisterSceneLoadedEvent(OnGameplaySceneLoaded);
			ManaCrystalMgr.Get().SetEnemyManaCounterActive(active: false);
			OverrideZonePlayBaseTransitionTime();
			int tag = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.NEXT_OPPONENT_PLAYER_ID);
			PlayerLeaderboardManager.Get().SetNextOpponent(tag);
		}
	}

	protected bool GetEnemyDeckTooltipContent(ref string headline, ref string description, int index)
	{
		switch (index)
		{
		case 0:
		{
			TAG_RACE[] availableRacesInBattlegroundsExcludingAmalgam = GameState.Get().GetAvailableRacesInBattlegroundsExcludingAmalgam();
			if (availableRacesInBattlegroundsExcludingAmalgam.Length == 5)
			{
				headline = GameStrings.Get("GAMEPLAY_TOOLTIP_BACON_AVAILABLE_RACES_HEADLINE");
				description = GameStrings.Format("GAMEPLAY_TOOLTIP_BACON_AVAILABLE_RACES_DESC", GameStrings.GetRaceNameBattlegrounds(availableRacesInBattlegroundsExcludingAmalgam[0]), GameStrings.GetRaceNameBattlegrounds(availableRacesInBattlegroundsExcludingAmalgam[1]), GameStrings.GetRaceNameBattlegrounds(availableRacesInBattlegroundsExcludingAmalgam[2]), GameStrings.GetRaceNameBattlegrounds(availableRacesInBattlegroundsExcludingAmalgam[3]), GameStrings.GetRaceNameBattlegrounds(availableRacesInBattlegroundsExcludingAmalgam[4]));
				return true;
			}
			break;
		}
		case 1:
		{
			TAG_RACE[] missingRacesInBattlegrounds = GameState.Get().GetMissingRacesInBattlegrounds();
			if (missingRacesInBattlegrounds.Length == 3)
			{
				headline = GameStrings.Get("GAMEPLAY_TOOLTIP_BACON_UNAVAILABLE_RACES_HEADLINE");
				description = GameStrings.Format("GAMEPLAY_TOOLTIP_BACON_UNAVAILABLE_RACES_DESC", GameStrings.GetRaceNameBattlegrounds(missingRacesInBattlegrounds[0]), GameStrings.GetRaceNameBattlegrounds(missingRacesInBattlegrounds[1]), GameStrings.GetRaceNameBattlegrounds(missingRacesInBattlegrounds[2]));
				return true;
			}
			break;
		}
		}
		return false;
	}

	protected bool GetFriendlyDeckTooltipContent(ref string headline, ref string description, int index)
	{
		if (index == 0)
		{
			int count = GameState.Get().GetFriendlySidePlayer().GetDeckZone()
				.GetCards()
				.Count;
			int num = 4 - count;
			headline = GameStrings.Get("GAMEPLAY_TOOLTIP_BACON_DARKMOON_PRIZES_HEADLINE");
			description = GameStrings.Format("GAMEPLAY_TOOLTIP_BACON_DARKMOON_PRIZES_DESC", num);
			return true;
		}
		return false;
	}

	protected bool GetFriendlyManaTooltipContent(ref string headline, ref string description, int index)
	{
		if (index == 0)
		{
			headline = GameStrings.Get("GAMEPLAY_TOOLTIP_MANA_COIN_HEADLINE");
			description = GameStrings.Get("GAMEPLAY_TOOLTIP_BACON_GOLD");
			return true;
		}
		return false;
	}

	public override InputManager.ZoneTooltipSettings GetZoneTooltipSettings()
	{
		bool allowed = GameState.Get().GetGameEntity().GetTag(GAME_TAG.DARKMOON_FAIRE_PRIZES_ACTIVE) == 1;
		return new InputManager.ZoneTooltipSettings
		{
			EnemyDeck = new InputManager.TooltipSettings(allowed: true, GetEnemyDeckTooltipContent),
			EnemyHand = new InputManager.TooltipSettings(allowed: false),
			EnemyMana = new InputManager.TooltipSettings(allowed: false),
			FriendlyDeck = new InputManager.TooltipSettings(allowed, GetFriendlyDeckTooltipContent),
			FriendlyMana = new InputManager.TooltipSettings(allowed: true, GetFriendlyManaTooltipContent)
		};
	}

	public override string GetMulliganDetailText()
	{
		TAG_RACE[] availableRacesInBattlegroundsExcludingAmalgam = GameState.Get().GetAvailableRacesInBattlegroundsExcludingAmalgam();
		if (Array.Exists(availableRacesInBattlegroundsExcludingAmalgam, (TAG_RACE race) => race == TAG_RACE.INVALID))
		{
			return null;
		}
		if (availableRacesInBattlegroundsExcludingAmalgam.Length == 5)
		{
			return GameStrings.Format("GAMEPLAY_BACON_MULLIGAN_AVAILABLE_RACES", GameStrings.GetRaceNameBattlegrounds(availableRacesInBattlegroundsExcludingAmalgam[0]), GameStrings.GetRaceNameBattlegrounds(availableRacesInBattlegroundsExcludingAmalgam[1]), GameStrings.GetRaceNameBattlegrounds(availableRacesInBattlegroundsExcludingAmalgam[2]), GameStrings.GetRaceNameBattlegrounds(availableRacesInBattlegroundsExcludingAmalgam[3]), GameStrings.GetRaceNameBattlegrounds(availableRacesInBattlegroundsExcludingAmalgam[4]));
		}
		return null;
	}

	public override Vector3 NameBannerPosition(Player.Side side)
	{
		if (side == Player.Side.FRIENDLY)
		{
			return new Vector3(0f, 5f, 11f);
		}
		return base.NameBannerPosition(side);
	}

	public override Vector3 GetMulliganTimerAlternatePosition()
	{
		if (MulliganManager.Get() == null || MulliganManager.Get().GetMulliganBanner() == null)
		{
			return new Vector3(100f, 0f, 0f);
		}
		if (GameState.Get().IsInChoiceMode() && MulliganManager.Get().GetMulliganButton() != null)
		{
			return MulliganManager.Get().GetMulliganButton().transform.position;
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			return MulliganManager.Get().GetMulliganBanner().transform.position + new Vector3(-1.8f, 0f, -0.91f);
		}
		return MulliganManager.Get().GetMulliganBanner().transform.position;
	}

	protected override Spell BlowUpHero(Card card, SpellType spellType)
	{
		if (card != null && card.GetActor() != null)
		{
			PlayMakerFSM component = card.GetActor().GetComponent<PlayMakerFSM>();
			if (component != null)
			{
				component.enabled = false;
			}
		}
		if (GameState.Get().IsMulliganManagerActive())
		{
			Transform parent = card.GetActor().gameObject.transform.parent;
			parent.position = new Vector3(-7.7726f, 0.0055918f, -8.054f);
			parent.localScale = new Vector3(1.134f, 1.134f, 1.134f);
			MulliganManager.Get().StopAllCoroutines();
		}
		return base.BlowUpHero(card, spellType);
	}

	public override bool ShouldDelayShowingFakeHeroPowerTooltip()
	{
		if (GameState.Get().IsMulliganManagerActive())
		{
			return false;
		}
		return true;
	}

	public override ActorStateType GetMulliganChoiceHighlightState()
	{
		return ActorStateType.CARD_SELECTABLE;
	}

	public override bool IsHeroMulliganLobbyFinished()
	{
		if (GameState.Get().IsMulliganPhase())
		{
			return CountPlayersFinishedMulligan() == CountPlayersInGame();
		}
		return true;
	}

	private int CountPlayersFinishedMulligan()
	{
		int num = 0;
		foreach (SharedPlayerInfo value in GameState.Get().GetPlayerInfoMap().Values)
		{
			if (value.GetPlayerHero() != null)
			{
				num++;
			}
		}
		return num;
	}

	private int CountPlayersInGame()
	{
		return GameState.Get().GetPlayerInfoMap().Values.Count;
	}

	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}

	public override bool DoAlternateMulliganIntro()
	{
		if (!ShouldDoAlternateMulliganIntro())
		{
			return false;
		}
		GameEntity.Coroutines.StartCoroutine(DoBaconAlternateMulliganIntroWithTiming());
		return true;
	}

	protected override void HandleMulliganTagChange()
	{
		MulliganManager.Get().BeginMulligan();
	}

	public override Vector3 GetAlternateMulliganActorScale()
	{
		return BATTLEGROUNDS_MULLIGAN_ACTOR_SCALE;
	}

	public override int GetNumberOfFakeMulliganCardsToShowOnLeft(int numOriginalCards)
	{
		if (numOriginalCards >= 3)
		{
			return 0;
		}
		return 1;
	}

	public override int GetNumberOfFakeMulliganCardsToShowOnRight(int numOriginalCards)
	{
		if (numOriginalCards >= 4)
		{
			return 0;
		}
		return 1;
	}

	public override void ConfigureFakeMulliganCardActor(Actor actor, bool shown)
	{
		PlayerLeaderboardMainCardActor playerLeaderboardMainCardActor = actor as PlayerLeaderboardMainCardActor;
		if (!(playerLeaderboardMainCardActor == null))
		{
			playerLeaderboardMainCardActor.ToggleLockedHeroView(shown);
		}
	}

	public override bool IsGameSpeedupConditionInEffect()
	{
		if (Gameplay.Get() == null || GameState.Get() == null || GameState.Get().GetGameEntity() == null || !GameState.Get().GetGameEntity().HasTag(GAME_TAG.ALLOW_GAME_SPEEDUP))
		{
			return false;
		}
		return m_gamePhase == 2;
	}

	public override void ApplyMulliganActorStateChanges(Actor baseActor)
	{
		PlayerLeaderboardMainCardActor obj = (PlayerLeaderboardMainCardActor)baseActor;
		obj.SetAlternateNameTextActive(active: false);
		obj.m_playerNameBackground.SetActive(value: false);
		obj.m_nameTextMesh.gameObject.SetActive(value: true);
	}

	public override void ApplyMulliganActorLobbyStateChanges(Actor baseActor)
	{
		PlayerLeaderboardMainCardActor obj = (PlayerLeaderboardMainCardActor)baseActor;
		obj.SetAlternateNameTextActive(active: false);
		obj.m_nameTextMesh.gameObject.SetActive(value: false);
		obj.m_playerNameBackground.SetActive(value: true);
		obj.SetFullyHighlighted(highlighted: false);
	}

	public override void ClearMulliganActorStateChanges(Actor baseActor)
	{
		PlayerLeaderboardMainCardActor obj = (PlayerLeaderboardMainCardActor)baseActor;
		obj.SetAlternateNameTextActive(active: false);
		obj.m_nameTextMesh.gameObject.SetActive(value: false);
		obj.m_playerNameBackground.SetActive(value: false);
		obj.m_playerNameText.gameObject.SetActive(value: false);
		obj.SetFullyHighlighted(highlighted: false);
	}

	public override string GetMulliganBannerText()
	{
		return GameStrings.Get("GAMEPLAY_BACON_MULLIGAN_CHOOSE_HERO_BANNER");
	}

	public override string GetMulliganBannerSubtitleText()
	{
		return null;
	}

	public override string GetMulliganWaitingText()
	{
		return string.Format(GameStrings.Get("GAMEPLAY_BACON_MULLIGAN_WAITING_BANNER"), CountPlayersFinishedMulligan(), CountPlayersInGame());
	}

	public override string GetMulliganWaitingSubtitleText()
	{
		if (MulliganManager.Get() != null && MulliganManager.Get().IsMulliganTimerActive())
		{
			return GameStrings.Get("GAMEPLAY_BACON_MULLIGAN_WAITING_BANNER_SUBTITLE");
		}
		return null;
	}

	public override void QueueEntityForRemoval(Entity entity)
	{
		GameState.Get().QueueEntityForRemoval(entity);
	}

	protected IEnumerator DoBaconAlternateMulliganIntroWithTiming()
	{
		SceneMgr.Get().NotifySceneLoaded();
		MulliganManager.Get().LoadMulliganButton();
		while (LoadingScreen.Get().IsPreviousSceneActive() || LoadingScreen.Get().IsFadingOut())
		{
			yield return null;
		}
		GameMgr.Get().UpdatePresence();
		GameState.Get().GetGameEntity().NotifyOfHeroesFinishedAnimatingInMulligan();
		ScreenEffectsMgr.Get().SetActive(enabled: true);
	}

	public override void OnMulliganCardsDealt(List<Card> startingCards)
	{
		foreach (Card startingCard in startingCards)
		{
			AssetLoader.Get().InstantiatePrefab(new AssetReference("BaconHeroMulliganBestPlaceVisual.prefab:6e6437cf53cbc0e4fbf0b3d6ce5a6856"), OnBestPlaceVisualLoaded, startingCard);
		}
	}

	private void OnBestPlaceVisualLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		Card card = (Card)callbackData;
		int num = GameUtils.TranslateCardIdToDbId(card.GetEntity().GetCardId());
		int bestPlaceForHero = GetBestPlaceForHero(num);
		BaconHeroMulliganBestPlaceVisual component = go.GetComponent<BaconHeroMulliganBestPlaceVisual>();
		m_mulliganBestPlaceVisuals.Add(component);
		component.SetVisualActive(bestPlaceForHero, num);
		GameUtils.SetParent(go, card.gameObject);
	}

	private int GetBestPlaceForHero(int heroId)
	{
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.BACON, GameSaveKeySubkeyId.BACON_BEST_HERO_PLACE, out List<long> values);
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.BACON, GameSaveKeySubkeyId.BACON_BEST_HERO_PLACE_HERO, out List<long> values2);
		if (values == null || values2 == null)
		{
			return int.MaxValue;
		}
		if (values.Count != values2.Count)
		{
			Debug.LogError("Error in GetBestPlaceForHero: List size mismatch!");
			return int.MaxValue;
		}
		for (int i = 0; i < values2.Count; i++)
		{
			if (values2[i] == heroId && i < values.Count)
			{
				return (int)values[i];
			}
		}
		return int.MaxValue;
	}

	public override void OnMulliganBeginDealNewCards()
	{
		foreach (BaconHeroMulliganBestPlaceVisual mulliganBestPlaceVisual in m_mulliganBestPlaceVisuals)
		{
			mulliganBestPlaceVisual.Hide();
		}
	}

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	private void OverrideZonePlayBaseTransitionTime()
	{
		if (GameState.Get() != null)
		{
			ZonePlay battlefieldZone = GameState.Get().GetFriendlySidePlayer().GetBattlefieldZone();
			ZonePlay battlefieldZone2 = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone();
			battlefieldZone.OverrideBaseTransitionTime(0.5f);
			battlefieldZone.ResetTransitionTime();
			battlefieldZone2.OverrideBaseTransitionTime(0.5f);
			battlefieldZone2.ResetTransitionTime();
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 1)
		{
			m_gamePhase = 1;
			yield return OnShopPhase();
		}
		if (missionEvent == 2)
		{
			m_gamePhase = 2;
			yield return OnCombatPhase();
		}
		if (missionEvent == 3)
		{
			int tag = GetFreezeButtonCard().GetEntity().GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1);
			int tag2 = GetFreezeButtonCard().GetEntity().GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2);
			tag2--;
			if (tag >= tag2)
			{
				SetInputEnableForFrozenButton(isEnabled: false);
			}
			else
			{
				SetInputEnableForFrozenButton(isEnabled: false);
				yield return new WaitForSeconds(0.75f);
				SetInputEnableForFrozenButton(isEnabled: true);
			}
		}
		if (missionEvent == 4)
		{
			SetInputEnableForRefreshButton(isEnabled: false);
			yield return new WaitForSeconds(0.75f);
			SetInputEnableForRefreshButton(isEnabled: true);
		}
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor bobActor = GetBobActor();
		if (bobActor == null || bobActor.GetEntity() == null)
		{
			yield return null;
		}
		switch (missionEvent)
		{
		case 101:
			if (ShouldPlayRateVO(0.25f) && !m_enemySpeaking)
			{
				string voLine = GetRandomLine(m_ShopUpgradeLines);
				yield return PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 102:
			if (!m_enemySpeaking)
			{
				string voLine = VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_04;
				yield return PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 103:
			if (ShouldPlayRateVO(0.15f) && !m_enemySpeaking)
			{
				string voLine = GetRandomLine(m_RecruitSmallLines);
				yield return PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 104:
			if (ShouldPlayRateVO(0.2f) && !m_enemySpeaking)
			{
				string voLine = GetRandomLine(m_RecruitMediumLines);
				yield return PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 105:
			if (ShouldPlayRateVO(0.25f) && !m_enemySpeaking)
			{
				string voLine = GetRandomLine(m_RecruitLargeLines);
				yield return PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 106:
			if (ShouldPlayRateVO(0.25f) && !m_enemySpeaking)
			{
				string voLine = GetRandomLine(m_TripleLines);
				yield return PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 107:
			if (ShouldPlayRateVO(0.15f) && !m_enemySpeaking)
			{
				string voLine = GetRandomLine(m_SellingLines);
				yield return PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 108:
			if (ShouldPlayRateVO(0.1f) && !m_enemySpeaking)
			{
				string voLine = GetRandomLine(m_FreezingLines);
				yield return PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 109:
			if (ShouldPlayRateVO(0.1f) && !m_enemySpeaking)
			{
				string voLine = GetRandomLine(m_RefreshLines);
				yield return PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 110:
			if (ShouldPlayRateVO(0.25f) && !m_enemySpeaking)
			{
				string voLine = GetRandomLine(m_PossibleTripleLines);
				yield return PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 111:
			if (!m_enemySpeaking)
			{
				string voLine = GetRandomLine(m_NewGameLines);
				yield return PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 112:
			if (!m_enemySpeaking)
			{
				string voLine = GetRandomLine(m_PostCombatGeneralLines);
				yield return PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 113:
			if (!m_enemySpeaking)
			{
				string voLine = GetRandomLine(m_PostCombatWinLines);
				yield return PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 114:
			if (!m_enemySpeaking)
			{
				string voLine = GetRandomLine(m_PostCombatLoseLines);
				yield return PlayBobLineWithOffsetBubble(voLine);
			}
			break;
		case 115:
			GameState.Get().SetBusy(busy: true);
			if (!m_enemySpeaking)
			{
				string voLine = GetRandomLine(m_PostShopGeneralLines);
				yield return PlayBobLineWithOffsetBubble(voLine);
			}
			GameState.Get().SetBusy(busy: false);
			break;
		case 116:
			GameState.Get().SetBusy(busy: true);
			if (!m_enemySpeaking)
			{
				string voLine = GetRandomLine(m_PostShopLoseLines);
				yield return PlayBobLineWithOffsetBubble(voLine);
			}
			GameState.Get().SetBusy(busy: false);
			break;
		case 117:
			GameState.Get().SetBusy(busy: true);
			if (!m_enemySpeaking)
			{
				string voLine = GetRandomLine(m_PostShopWinLines);
				yield return PlayBobLineWithOffsetBubble(voLine);
			}
			GameState.Get().SetBusy(busy: false);
			break;
		case 118:
			GameState.Get().SetBusy(busy: true);
			if (!m_enemySpeaking)
			{
				string voLine = GetRandomLine(m_PostShopIsFirstLines);
				yield return PlayBobLineWithOffsetBubble(voLine);
			}
			GameState.Get().SetBusy(busy: false);
			break;
		case 119:
			GameState.Get().SetBusy(busy: true);
			if (!m_enemySpeaking)
			{
				string voLine = VO_DALA_BOSS_99h_Male_Human_AFK_01;
				yield return PlayBobLineWithOffsetBubble(voLine);
			}
			GameState.Get().SetBusy(busy: false);
			break;
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		AchievementManager.Get().UnpauseToastNotifications();
		PlayerLeaderboardManager.Get().UpdateLayout();
		if (gameResult == TAG_PLAYSTATE.WON && m_hasSeenInGameWinVO == 0L)
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWait(Bob_BrassRing_Quote, VO_DALA_BOSS_99h_Male_Human_FirstVictory_01));
		}
		int realTimePlayerLeaderboardPlace = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetRealTimePlayerLeaderboardPlace();
		if (gameResult == TAG_PLAYSTATE.LOST && m_hasSeenInGameLoseVO == 0L && realTimePlayerLeaderboardPlace > 4)
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWait(Bob_BrassRing_Quote, VO_DALA_BOSS_99h_Male_Human_FirstDefeat_01));
		}
	}

	protected virtual IEnumerator OnShopPhase()
	{
		AchievementManager.Get().UnpauseToastNotifications();
		yield return ShowPopup("Shop");
		PlayerLeaderboardManager.Get().UpdateLayout();
		GameState.Get().GetOpposingSidePlayer().UpdateDisplayInfo();
		UpdateNameBanner();
		ShowTechLevelDisplay(shown: true);
		yield return new WaitForSeconds(3f);
		ShowShopTutorials();
		SetGameNotificationEmotesEnabled(enabled: true);
		GameState.Get().GetTimeTracker().ResetAccruedLostTime();
	}

	protected virtual IEnumerator OnCombatPhase()
	{
		HideShopTutorials();
		yield return ShowPopup("Combat");
		ShowTechLevelDisplay(shown: false);
		GameState.Get().GetOpposingSidePlayer().UpdateDisplayInfo();
		UpdateNameBanner();
		ForceShowFriendlyHeroActor();
		InputManager.Get().HidePhoneHand();
		GameState.Get().GetTimeTracker().ResetAccruedLostTime();
	}

	public override void HandleRealTimeMissionEvent(int missionEvent)
	{
		if (missionEvent == 2)
		{
			SetGameNotificationEmotesEnabled(enabled: false);
		}
	}

	private void OnTurnEnded(int oldTurn, int newTurn, object userData)
	{
		if (!GameState.Get().IsFriendlySidePlayerTurn())
		{
			(HearthstonePerformance.Get()?.GetCurrentPerformanceFlow<FlowPerformanceBattlegrounds>())?.OnNewRoundStart();
			GameEntity.Coroutines.StartCoroutine(GameState.Get().RejectUnresolvedChangesAfterDelay());
		}
	}

	public override string GetAttackSpellControllerOverride(Entity attacker)
	{
		if (attacker == null)
		{
			return null;
		}
		if (attacker.IsHero())
		{
			return "AttackSpellController_Battlegrounds_Hero.prefab:922da2c91f4cca1458b5901204d1d26c";
		}
		return "AttackSpellController_Battlegrounds_Minion.prefab:922da2c91f4cca1458b5901204d1d26c";
	}

	public override string GetVictoryScreenBannerText()
	{
		int realTimePlayerLeaderboardPlace = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetRealTimePlayerLeaderboardPlace();
		if (realTimePlayerLeaderboardPlace == 0)
		{
			return string.Empty;
		}
		return GameStrings.Get("GAMEPLAY_END_OF_GAME_PLACE_" + realTimePlayerLeaderboardPlace);
	}

	public override string GetBestNameForPlayer(int playerId)
	{
		string text = ((GameState.Get().GetPlayerInfoMap().ContainsKey(playerId) && GameState.Get().GetPlayerInfoMap()[playerId] != null) ? GameState.Get().GetPlayerInfoMap()[playerId].GetName() : null);
		string text2 = ((GameState.Get().GetPlayerInfoMap().ContainsKey(playerId) && GameState.Get().GetPlayerInfoMap()[playerId] != null && GameState.Get().GetPlayerInfoMap()[playerId].GetHero() != null) ? GameState.Get().GetPlayerInfoMap()[playerId].GetHero().GetName() : null);
		bool num = GameState.Get().GetPlayerMap().ContainsKey(playerId) && GameState.Get().GetPlayerMap()[playerId].IsFriendlySide();
		bool @bool = Options.Get().GetBool(Option.STREAMER_MODE);
		if (text2 == null)
		{
			text2 = ((PlayerLeaderboardManager.Get() != null && PlayerLeaderboardManager.Get().GetTileForPlayerId(playerId) != null) ? PlayerLeaderboardManager.Get().GetTileForPlayerId(playerId).GetHeroName() : null);
		}
		if (num)
		{
			if (@bool || text == null)
			{
				return GameStrings.Get("GAMEPLAY_HIDDEN_PLAYER_NAME");
			}
			return text;
		}
		if (@bool)
		{
			if (text2 == null)
			{
				return GameStrings.Get("GAMEPLAY_MISSING_OPPONENT_NAME");
			}
			return text2;
		}
		if (text == null)
		{
			return GameStrings.Get("GAMEPLAY_MISSING_OPPONENT_NAME");
		}
		return text;
	}

	public override string GetNameBannerOverride(Player.Side side)
	{
		if (side != Player.Side.OPPOSING)
		{
			return null;
		}
		if (GameState.Get() == null)
		{
			return null;
		}
		if (!IsCustomGameModeAIHero())
		{
			int tag = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.NEXT_OPPONENT_PLAYER_ID);
			return GetBestNameForPlayer(tag);
		}
		if (m_gamePhase == 2)
		{
			if (PlayerLeaderboardManager.Get() == null || PlayerLeaderboardManager.Get().GetOddManOutOpponentHero() == null)
			{
				if (GameState.Get().GetOpposingSidePlayer() == null || GameState.Get().GetOpposingSidePlayer().GetHero() == null)
				{
					return null;
				}
				return GameState.Get().GetOpposingSidePlayer().GetHero()
					.GetName();
			}
			return PlayerLeaderboardManager.Get().GetOddManOutOpponentHero().GetName();
		}
		if (GameState.Get().GetOpposingSidePlayer() == null || GameState.Get().GetOpposingSidePlayer().GetHero() == null)
		{
			return null;
		}
		return GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetName();
	}

	public override void PlayAlternateEnemyEmote(int playerId, EmoteType emoteType)
	{
		string text = "";
		Actor actor = null;
		NotificationManager.VisualEmoteType visualEmoteType = NotificationManager.VisualEmoteType.NONE;
		PlayerLeaderboardCard tileForPlayerId = PlayerLeaderboardManager.Get().GetTileForPlayerId(playerId);
		if (!(tileForPlayerId == null))
		{
			actor = tileForPlayerId.m_tileActor;
			switch (emoteType)
			{
			case EmoteType.OOPS:
				text = GameStrings.Get("GAMEPLAY_BACON_TEXT_EMOTE_OOPS");
				break;
			case EmoteType.GREETINGS:
				text = GameStrings.Get("GAMEPLAY_BACON_TEXT_EMOTE_GREETINGS");
				break;
			case EmoteType.THREATEN:
				text = GameStrings.Get("GAMEPLAY_BACON_TEXT_EMOTE_THREATEN");
				break;
			case EmoteType.WELL_PLAYED:
				text = GameStrings.Get("GAMEPLAY_BACON_TEXT_EMOTE_WELL_PLAYED");
				break;
			case EmoteType.WOW:
				text = GameStrings.Get("GAMEPLAY_BACON_TEXT_EMOTE_WOW");
				break;
			case EmoteType.THANKS:
				text = GameStrings.Get("GAMEPLAY_BACON_TEXT_EMOTE_THANKS");
				break;
			case EmoteType.SORRY:
				text = GameStrings.Get("GAMEPLAY_BACON_TEXT_EMOTE_SORRY");
				break;
			case EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_01:
				visualEmoteType = NotificationManager.VisualEmoteType.TECH_UP_01;
				break;
			case EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_02:
				visualEmoteType = NotificationManager.VisualEmoteType.TECH_UP_02;
				break;
			case EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_03:
				visualEmoteType = NotificationManager.VisualEmoteType.TECH_UP_03;
				break;
			case EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_04:
				visualEmoteType = NotificationManager.VisualEmoteType.TECH_UP_04;
				break;
			case EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_05:
				visualEmoteType = NotificationManager.VisualEmoteType.TECH_UP_05;
				break;
			case EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_06:
				visualEmoteType = NotificationManager.VisualEmoteType.TECH_UP_06;
				break;
			case EmoteType.BATTLEGROUNDS_VISUAL_HOT_STREAK:
				visualEmoteType = NotificationManager.VisualEmoteType.HOT_STREAK;
				break;
			case EmoteType.BATTLEGROUNDS_VISUAL_BANANA:
				visualEmoteType = NotificationManager.VisualEmoteType.BANANA;
				break;
			case EmoteType.BATTLEGROUNDS_VISUAL_TRIPLE:
				visualEmoteType = NotificationManager.VisualEmoteType.TRIPLE;
				break;
			case EmoteType.BATTLEGROUNDS_VISUAL_ONE:
				visualEmoteType = NotificationManager.VisualEmoteType.BATTLEGROUNDS_01;
				break;
			case EmoteType.BATTLEGROUNDS_VISUAL_TWO:
				visualEmoteType = NotificationManager.VisualEmoteType.BATTLEGROUNDS_02;
				break;
			case EmoteType.BATTLEGROUNDS_VISUAL_THREE:
				visualEmoteType = NotificationManager.VisualEmoteType.BATTLEGROUNDS_03;
				break;
			case EmoteType.BATTLEGROUNDS_VISUAL_FOUR:
				visualEmoteType = NotificationManager.VisualEmoteType.BATTLEGROUNDS_04;
				break;
			case EmoteType.BATTLEGROUNDS_VISUAL_FIVE:
				visualEmoteType = NotificationManager.VisualEmoteType.BATTLEGROUNDS_05;
				break;
			case EmoteType.BATTLEGROUNDS_VISUAL_SIX:
				visualEmoteType = NotificationManager.VisualEmoteType.BATTLEGROUNDS_06;
				break;
			default:
				text = GameStrings.Get("GAMEPLAY_BACON_TEXT_EMOTE_INVALID");
				break;
			}
			if (text != null || visualEmoteType != 0)
			{
				NotificationManager.SpeechBubbleOptions options = new NotificationManager.SpeechBubbleOptions().WithActor(actor).WithBubbleScale(0.3f).WithSpeechText(text)
					.WithSpeechBubbleDirection(Notification.SpeechBubbleDirection.MiddleLeft)
					.WithParentToActor(parentToActor: false)
					.WithDestroyWhenNewCreated(destroyWhenNewCreated: true)
					.WithSpeechBubbleGroup(playerId)
					.WithVisualEmoteType(visualEmoteType)
					.WithEmoteDuration(1.5f)
					.WithFinishCallback(OnNotificationEnded);
				RequestNotification(options, emoteType);
			}
		}
	}

	private void RequestNotification(NotificationManager.SpeechBubbleOptions options, EmoteType emoteType)
	{
		int speechBubbleGroup = options.speechBubbleGroup;
		if (!m_emotesAllowedForPlayer.ContainsKey(speechBubbleGroup))
		{
			m_emotesAllowedForPlayer.Add(speechBubbleGroup, value: true);
			m_emotesQueuedForPlayer.Add(speechBubbleGroup, new QueueList<NotificationManager.SpeechBubbleOptions>());
			m_gameNotificationsQueuedForPlayer.Add(speechBubbleGroup, new LinkedList<NotificationManager.SpeechBubbleOptions>());
		}
		if (m_gameNotificationEmotes.Contains(emoteType))
		{
			if (m_priorityEmotes.Contains(emoteType))
			{
				m_gameNotificationsQueuedForPlayer[speechBubbleGroup].AddFirst(options);
			}
			else
			{
				m_gameNotificationsQueuedForPlayer[speechBubbleGroup].AddLast(options);
			}
		}
		else
		{
			m_emotesQueuedForPlayer[speechBubbleGroup].Enqueue(options);
		}
		PlayEmotesIfPossibleForPlayer(speechBubbleGroup);
	}

	private void OnNotificationEnded(int playerId)
	{
		if (m_emotesAllowedForPlayer.ContainsKey(playerId))
		{
			m_emotesAllowedForPlayer[playerId] = true;
			PlayEmotesIfPossibleForPlayer(playerId);
		}
	}

	private void PlayEmotesIfPossibleForPlayer(int playerId)
	{
		if (m_emotesAllowedForPlayer.ContainsKey(playerId) && m_emotesAllowedForPlayer[playerId])
		{
			if (m_emotesQueuedForPlayer.ContainsKey(playerId) && m_emotesQueuedForPlayer[playerId].Count > 0)
			{
				NotificationManager.Get().CreateSpeechBubble(m_emotesQueuedForPlayer[playerId].Dequeue());
				m_emotesAllowedForPlayer[playerId] = false;
			}
			else if (m_gameNotificationEmotesAllowed && m_gameNotificationsQueuedForPlayer.ContainsKey(playerId) && m_gameNotificationsQueuedForPlayer[playerId].Count > 0)
			{
				NotificationManager.Get().CreateSpeechBubble(m_gameNotificationsQueuedForPlayer[playerId].First.Value);
				m_gameNotificationsQueuedForPlayer[playerId].RemoveFirst();
				m_emotesAllowedForPlayer[playerId] = false;
			}
		}
	}

	private void SetGameNotificationEmotesEnabled(bool enabled)
	{
		m_gameNotificationEmotesAllowed = enabled;
		if (!m_gameNotificationEmotesAllowed)
		{
			return;
		}
		foreach (int item in m_emotesAllowedForPlayer.Keys.ToList())
		{
			PlayEmotesIfPossibleForPlayer(item);
		}
	}

	public override bool ShouldUseAlternateNameForPlayer(Player.Side side)
	{
		return side == Player.Side.OPPOSING;
	}

	private bool IsCustomGameModeAIHero()
	{
		if (!IsShopPhase())
		{
			return GameState.Get().GetFriendlySidePlayer().HasTag(GAME_TAG.BACON_ODD_PLAYER_OUT);
		}
		return true;
	}

	public override string GetTurnTimerCountdownText(float timeRemainingInTurn)
	{
		if (m_gamePhase == 2)
		{
			return GameStrings.Get("GAMEPLAY_BACON_COMBAT_END_TURN_BUTTON_TEXT");
		}
		if (m_gamePhase == 1)
		{
			if (timeRemainingInTurn == 0f)
			{
				if (!TurnTimer.Get().IsRopeActive())
				{
					return GameStrings.Get("GAMEPLAY_BACON_SHOP_END_TURN_BUTTON_TEXT");
				}
				return "";
			}
			AchievementManager achievementManager = AchievementManager.Get();
			if (timeRemainingInTurn < achievementManager.GetNotificationPauseBufferSeconds() && !achievementManager.ToastNotificationsPaused)
			{
				achievementManager.PauseToastNotifications();
			}
			return GameStrings.Format("GAMEPLAY_END_TURN_BUTTON_COUNTDOWN", Mathf.CeilToInt(timeRemainingInTurn));
		}
		return "";
	}

	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		base.NotifyOfGameOver(gameResult);
		HideShopTutorials();
	}

	protected void InitializePhasePopup()
	{
		AssetLoader.Get().InstantiatePrefab(BACON_PHASE_POPUP, delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			m_phasePopup = go;
			m_phasePopup.SetActive(value: false);
		});
	}

	protected IEnumerator ShowPopup(string playmakerState)
	{
		if (m_gameplaySceneLoaded)
		{
			while (m_phasePopup == null)
			{
				yield return null;
			}
			m_phasePopup.SetActive(value: true);
			m_phasePopup.GetComponent<PlayMakerFSM>().SetState(playmakerState);
		}
	}

	protected void UpdateNameBanner()
	{
		if (!(Gameplay.Get() == null))
		{
			NameBanner nameBannerForSide = Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING);
			if (!(nameBannerForSide == null))
			{
				nameBannerForSide.UpdatePlayerNameBanner();
			}
		}
	}

	protected void InitializeTurnTimer()
	{
		TurnTimer.Get().SetGameModeSettings(new TurnTimerGameModeSettings
		{
			m_RopeFuseVolume = 0.05f,
			m_EndTurnButtonExplosionVolume = 0f,
			m_RopeRolloutVolume = 0.3f,
			m_PlayMusicStinger = false,
			m_PlayTimeoutFx = false,
			m_PlayTickSound = true
		});
	}

	public bool IsShopPhase()
	{
		return m_gamePhase == 1;
	}

	private void OnBattlegroundsRatingChange()
	{
		BattlegroundsRatingChange battlegroundsRatingChange2 = (RatingChangeData = Network.Get().GetBattlegroundsRatingChange());
	}

	private int GetTechLevelInt()
	{
		if (GameState.Get() == null)
		{
			return 0;
		}
		return GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.PLAYER_TECH_LEVEL);
	}

	private void InitTurnCounter()
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("BaconTechLevelRibbon.prefab:ad60cd0fe1c8eea4bb2f12cc280acda8");
		m_techLevelCounter = gameObject.GetComponent<Notification>();
		PlayMakerFSM component = m_techLevelCounter.GetComponent<PlayMakerFSM>();
		component.FsmVariables.GetFsmInt("TechLevel").Value = GetTechLevelInt();
		component.SendEvent("Birth");
		Zone zone = ZoneMgr.Get().FindZoneOfType<ZoneHero>(Player.Side.OPPOSING);
		m_techLevelCounter.transform.localPosition = zone.transform.position + new Vector3(-1.294f, 0.21f, -0.152f);
		m_techLevelCounter.transform.localScale = Vector3.one * 0.58f;
		GameEntity.Coroutines.StartCoroutine(KeepTechLevelUpToDateCoroutine());
	}

	protected void ShowTechLevelDisplay(bool shown)
	{
		if (m_techLevelCounter == null)
		{
			InitTurnCounter();
		}
		if (m_techLevelCounter != null)
		{
			m_techLevelCounter.gameObject.SetActive(shown);
		}
	}

	private IEnumerator KeepTechLevelUpToDateCoroutine()
	{
		while (true)
		{
			if (!m_techLevelCounter.gameObject.activeInHierarchy)
			{
				yield return null;
			}
			int techLevelInt = GetTechLevelInt();
			if (techLevelInt != m_displayedTechLevelNumber)
			{
				PlayMakerFSM component = m_techLevelCounter.GetComponent<PlayMakerFSM>();
				component.FsmVariables.GetFsmInt("TechLevel").Value = techLevelInt;
				component.SendEvent("Action");
				UpdateTechLevelDisplayText(techLevelInt);
			}
			yield return null;
		}
	}

	public override void ToggleAlternateMulliganActorHighlight(Card card, bool highlighted)
	{
		PlayerLeaderboardMainCardActor playerLeaderboardMainCardActor = card.GetActor() as PlayerLeaderboardMainCardActor;
		if (playerLeaderboardMainCardActor != null)
		{
			playerLeaderboardMainCardActor.SetFullyHighlighted(highlighted);
		}
	}

	public override bool ToggleAlternateMulliganActorHighlight(Actor actor, bool? highlighted = null)
	{
		PlayerLeaderboardMainCardActor playerLeaderboardMainCardActor = actor as PlayerLeaderboardMainCardActor;
		if (playerLeaderboardMainCardActor != null)
		{
			bool flag = ((!highlighted.HasValue) ? (!playerLeaderboardMainCardActor.m_fullSelectionHighlight.activeSelf) : highlighted.Value);
			playerLeaderboardMainCardActor.SetFullyHighlighted(flag);
			return flag;
		}
		return false;
	}

	private void UpdateTechLevelDisplayText(int techLevel)
	{
		string headlineString = GameStrings.Get("GAMEPLAY_BACON_TAVERN_TIER");
		m_techLevelCounter.ChangeDialogText(headlineString, "", "", "");
		m_displayedTechLevelNumber = techLevel;
	}

	protected void ShowShopTutorials()
	{
		HideShopTutorials();
	}

	protected virtual void HideShopTutorials()
	{
		StopCoroutine(m_buyButtonTutorialCoroutine);
		StopCoroutine(m_enemyMinionTutorialCoroutine);
		StopCoroutine(m_playMinionTutorialCoroutine);
		HideBuyButtonTutorial();
		HidePlayMinionTutorial();
		HideShopMinionTutorial();
	}

	private static void StopCoroutine(Coroutine coroutine)
	{
		if (coroutine != null)
		{
			GameEntity.Coroutines.StopCoroutine(coroutine);
		}
	}

	protected virtual IEnumerator UpdateBuyButtonTutorial()
	{
		List<Zone> list = ZoneMgr.Get().FindZonesForSide(Player.Side.FRIENDLY);
		Zone buyButtonZone = null;
		foreach (Zone item in list)
		{
			if (item is ZoneGameModeButton && ((ZoneGameModeButton)item).m_ButtonSlot == 2)
			{
				buyButtonZone = item;
			}
		}
		if (buyButtonZone == null)
		{
			yield break;
		}
		Card buyCard = buyButtonZone.GetFirstCard();
		if (buyCard == null)
		{
			yield break;
		}
		while (IsPlayerOutOfMana(GameState.Get().GetFriendlySidePlayer()))
		{
			yield return null;
		}
		yield return PlayBobLineWithoutText(VO_DALA_BOSS_99h_Male_Human_ShopFirstTime_01);
		bool tutorialShown = false;
		while (!GameState.Get().IsInTargetMode())
		{
			if (buyCard.IsMousedOver() && tutorialShown)
			{
				tutorialShown = false;
				HideBuyButtonTutorial(hideImmediately: true);
			}
			else if (!buyCard.IsMousedOver() && !tutorialShown)
			{
				tutorialShown = true;
				ShowBuyButtonTutorial(buyButtonZone);
			}
			yield return null;
		}
		m_hasSeenBuyButtonTutorial = true;
		HideBuyButtonTutorial();
	}

	protected void ShowBuyButtonTutorial(Zone buyButtonZone)
	{
		Vector3 position = buyButtonZone.transform.position;
		Vector3 position2 = new Vector3(position.x, position.y, position.z + 2f);
		string key = "GAMEPLAY_BACON_BUY_TUTORIAL";
		m_buyButtonTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key));
		m_buyButtonTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
		m_buyButtonTutorialNotification.PulseReminderEveryXSeconds(2f);
	}

	protected void HideBuyButtonTutorial(bool hideImmediately = false)
	{
		if (m_buyButtonTutorialNotification != null)
		{
			if (hideImmediately)
			{
				NotificationManager.Get().DestroyNotificationNowWithNoAnim(m_buyButtonTutorialNotification);
			}
			else
			{
				NotificationManager.Get().DestroyNotification(m_buyButtonTutorialNotification, 0f);
			}
		}
	}

	protected virtual IEnumerator UpdateShopMinionTutorial()
	{
		while (!GameState.Get().IsInTargetMode() && !m_hasSeenBuyButtonTutorial)
		{
			yield return null;
		}
		yield return PlayBobLineWithoutText(VO_DALA_BOSS_99h_Male_Human_Hire_01);
		ShowShopMinionTutorial();
		while (GameState.Get().GetFriendlySidePlayer().GetHandZone()
			.GetCards()
			.Count < 2)
		{
			yield return null;
		}
		m_hasSeenEnemyMinionTutorial = true;
		HideShopMinionTutorial();
	}

	protected void ShowShopMinionTutorial()
	{
		Vector3 position = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone()
			.transform.position;
		position += new Vector3(0f, 0f, 2.25f);
		string key = "GAMEPLAY_BACON_ENEMY_MINION_TUTORIAL";
		m_enemyMinionTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, TutorialEntity.GetTextScale(), GameStrings.Get(key));
		m_enemyMinionTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.BottomThree);
		m_enemyMinionTutorialNotification.PulseReminderEveryXSeconds(2f);
	}

	protected void HideShopMinionTutorial()
	{
		if (m_enemyMinionTutorialNotification != null)
		{
			NotificationManager.Get().DestroyNotification(m_enemyMinionTutorialNotification, 0f);
		}
	}

	protected virtual IEnumerator UpdatePlayMinionTutorial()
	{
		while (!m_hasSeenEnemyMinionTutorial)
		{
			yield return null;
		}
		Card firstMinion = null;
		while (firstMinion == null)
		{
			List<Card> cards = GameState.Get().GetFriendlySidePlayer().GetHandZone()
				.GetCards();
			if (cards.Any((Card c) => c.GetEntity().IsMinion()))
			{
				firstMinion = cards.First((Card c) => c.GetEntity().IsMinion());
			}
			yield return null;
		}
		yield return PlayBobLineWithoutText(VO_DALA_BOSS_99h_Male_Human_RecruitWork_01);
		yield return new WaitForSeconds(1.5f);
		ShowPlayMinionTutorial(firstMinion);
		while (!firstMinion.IsMousedOver() && firstMinion.GetZone().m_ServerTag == TAG_ZONE.HAND)
		{
			yield return null;
		}
		m_hasSeenPlayMinionTutorial = true;
		HidePlayMinionTutorial();
	}

	protected void ShowPlayMinionTutorial(Card firstMinion)
	{
		Vector3 position = firstMinion.transform.position;
		Vector3 position2 = ((!UniversalInputManager.UsePhoneUI) ? new Vector3(position.x, position.y, position.z + 2f) : new Vector3(position.x - 0.08f, position.y + 0.2f, position.z + 1.2f));
		string key = "GAMEPLAY_BACON_PLAY_MINION_TUTORIAL";
		m_playMinionTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key));
		m_playMinionTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
		m_playMinionTutorialNotification.PulseReminderEveryXSeconds(2f);
	}

	protected void HidePlayMinionTutorial()
	{
		if (m_playMinionTutorialNotification != null)
		{
			NotificationManager.Get().DestroyNotification(m_playMinionTutorialNotification, 0f);
		}
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_DALA_BOSS_99h_Male_Human_NewModeLaunch_01, VO_DALA_BOSS_99h_Male_Human_NewModeLaunchalt_01, VO_DALA_BOSS_99h_Male_Human_ModeSelect_01, VO_DALA_BOSS_99h_Male_Human_HeroSelection_01, VO_DALA_BOSS_99h_Male_Human_ShopFirstTime_01, VO_DALA_BOSS_99h_Male_Human_FirstBattle_01, VO_DALA_BOSS_99h_Male_Human_BattleEndFirstLoss_01, VO_DALA_BOSS_99h_Male_Human_ShopUpgrade_01, VO_DALA_BOSS_99h_Male_Human_FirstDefeat_01, VO_DALA_BOSS_99h_Male_Human_FirstVictory_01,
			VO_DALA_BOSS_99h_Male_Human_UpgradeShop_01, VO_DALA_BOSS_99h_Male_Human_AFK_01, VO_DALA_BOSS_99h_Male_Human_NewGame_01, VO_DALA_BOSS_99h_Male_Human_NewGame_02, VO_DALA_BOSS_99h_Male_Human_NewGame_03, VO_DALA_BOSS_99h_Male_Human_General_01, VO_DALA_BOSS_99h_Male_Human_General_02, VO_DALA_BOSS_99h_Male_Human_General_03, VO_DALA_BOSS_99h_Male_Human_CombatWin_01, VO_DALA_BOSS_99h_Male_Human_CombatWin_02,
			VO_DALA_BOSS_99h_Male_Human_CombatWin_03, VO_DALA_BOSS_99h_Male_Human_CombatLoss_01, VO_DALA_BOSS_99h_Male_Human_CombatLoss_02, VO_DALA_BOSS_99h_Male_Human_CombatLoss_03, VO_DALA_BOSS_99h_Male_Human_ShopToCombat_01, VO_DALA_BOSS_99h_Male_Human_ShopToCombat_02, VO_DALA_BOSS_99h_Male_Human_Behind_01, VO_DALA_BOSS_99h_Male_Human_Behind_02, VO_DALA_BOSS_99h_Male_Human_Behind_03, VO_DALA_BOSS_99h_Male_Human_Ahead_01,
			VO_DALA_BOSS_99h_Male_Human_Ahead_02, VO_DALA_BOSS_99h_Male_Human_Ahead_03, VO_DALA_BOSS_99h_Male_Human_FirstPlace_01, VO_DALA_BOSS_99h_Male_Human_FirstPlace_02, VO_DALA_BOSS_99h_Male_Human_FirstPlace_03, VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_01, VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_02, VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_03, VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_04, VO_DALA_BOSS_99h_Male_Human_Idle_01,
			VO_DALA_BOSS_99h_Male_Human_Idle_02, VO_DALA_BOSS_99h_Male_Human_Idle_03, VO_DALA_BOSS_99h_Male_Human_Idle_04, VO_DALA_BOSS_99h_Male_Human_Idle_05, VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_01, VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_02, VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_03, VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_01, VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_02, VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_03,
			VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_01, VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_02, VO_DALA_BOSS_99h_Male_Human_RecruitLargeMinion_03, VO_DALA_BOSS_99h_Male_Human_AfterTriple_01, VO_DALA_BOSS_99h_Male_Human_AfterTriple_02, VO_DALA_BOSS_99h_Male_Human_AfterTriple_03, VO_DALA_BOSS_99h_Male_Human_Flavor_01, VO_DALA_BOSS_99h_Male_Human_Flavor_02, VO_DALA_BOSS_99h_Male_Human_Flavor_03, VO_DALA_BOSS_99h_Male_Human_Flavor_04,
			VO_DALA_BOSS_99h_Male_Human_Flavor_05, VO_DALA_BOSS_99h_Male_Human_Flavor_06, VO_DALA_BOSS_99h_Male_Human_Flavor_07, VO_DALA_BOSS_99h_Male_Human_Flavor_08, VO_DALA_BOSS_99h_Male_Human_AfterSelling_01, VO_DALA_BOSS_99h_Male_Human_AfterSelling_02, VO_DALA_BOSS_99h_Male_Human_AfterFreezing_01, VO_DALA_BOSS_99h_Male_Human_AfterFreezing_02, VO_DALA_BOSS_99h_Male_Human_Hire_01, VO_DALA_BOSS_99h_Male_Human_Hire_02,
			VO_DALA_BOSS_99h_Male_Human_Triple_01, VO_DALA_BOSS_99h_Male_Human_Triple_02, VO_DALA_BOSS_99h_Male_Human_Triple_03, VO_DALA_BOSS_99h_Male_Human_RecruitWork_01
		})
		{
			PreloadSound(item);
		}
	}

	public override void OnPlayThinkEmote()
	{
		if (m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide() || currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		Actor bobActor = GetBobActor();
		if (bobActor == null || bobActor.GetEntity() == null)
		{
			return;
		}
		if (IsPlayerOutOfMana(currentPlayer))
		{
			if (ShouldPlayRateVO(0.1f))
			{
				string voLine = PopRandomLine(m_SpecialIdleLines);
				GameEntity.Coroutines.StartCoroutine(PlayBobLineWithOffsetBubble(voLine));
			}
		}
		else if (ShouldPlayRateVO(0.05f))
		{
			string randomLine = GetRandomLine(m_IdleLines);
			GameEntity.Coroutines.StartCoroutine(PlayBobLineWithOffsetBubble(randomLine));
		}
	}

	protected Actor GetBobActor()
	{
		Entity hero = GameState.Get().GetOpposingSidePlayer().GetHero();
		if (hero != null && hero.GetCardId() == "TB_BaconShopBob")
		{
			return hero.GetHeroCard().GetActor();
		}
		return null;
	}

	protected string GetRandomLine(List<string> lines)
	{
		if (lines.Count == 0 || lines == null)
		{
			return null;
		}
		return lines[UnityEngine.Random.Range(0, lines.Count)];
	}

	protected string PopRandomLine(List<string> lines)
	{
		if (lines.Count == 0 || lines == null)
		{
			return null;
		}
		string randomLine = GetRandomLine(lines);
		lines.Remove(randomLine);
		return randomLine;
	}

	protected bool IsPlayerOutOfMana(Player player)
	{
		return player.GetTag(GAME_TAG.RESOURCES) - player.GetTag(GAME_TAG.RESOURCES_USED) == 0;
	}

	protected bool HasSeenAllTutorial()
	{
		if (m_hasSeenBuyButtonTutorial && m_hasSeenEnemyMinionTutorial)
		{
			return m_hasSeenPlayMinionTutorial;
		}
		return false;
	}

	protected bool ShouldPlayRateVO(float chance)
	{
		float num = UnityEngine.Random.Range(0f, 1f);
		return chance > num;
	}

	protected IEnumerator PlayBobLineWithoutText(string voLine)
	{
		Actor bobActor = GetBobActor();
		if (bobActor != null && bobActor.GetEntity() != null)
		{
			m_enemySpeaking = true;
			yield return PlaySoundAndWait(voLine, "", Notification.SpeechBubbleDirection.TopLeft, bobActor);
			m_enemySpeaking = false;
		}
	}

	protected virtual IEnumerator PlayBobLineWithOffsetBubble(string voLine)
	{
		Actor bobActor = GetBobActor();
		if (bobActor != null && bobActor.GetEntity() != null)
		{
			yield return PlayBobLineWithoutText(voLine);
		}
	}

	protected Card GetGameModeButtonBySlot(int buttonSlot)
	{
		List<Zone> list = ZoneMgr.Get().FindZonesForSide(Player.Side.FRIENDLY);
		Zone zone = null;
		foreach (Zone item in list)
		{
			if (item is ZoneGameModeButton && ((ZoneGameModeButton)item).m_ButtonSlot == buttonSlot)
			{
				zone = item;
			}
		}
		if (zone == null)
		{
			return null;
		}
		return zone.GetFirstCard();
	}

	protected Card GetBuyButtonCard()
	{
		List<Zone> list = ZoneMgr.Get().FindZonesForSide(Player.Side.FRIENDLY);
		Zone zone = null;
		foreach (Zone item in list)
		{
			if (item is ZoneMoveMinionHoverTarget && ((ZoneMoveMinionHoverTarget)item).m_Slot == 1)
			{
				zone = item;
			}
		}
		if (zone == null)
		{
			return null;
		}
		return zone.GetFirstCard();
	}

	protected Card GetFreezeButtonCard()
	{
		return GetGameModeButtonBySlot(1);
	}

	protected Card GetRefreshButtonCard()
	{
		return GetGameModeButtonBySlot(2);
	}

	protected Card GetTavernUpgradeButtonCard()
	{
		return GetGameModeButtonBySlot(3);
	}

	protected void SetInputEnableForBuy(bool isEnabled)
	{
		foreach (Card card in GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone()
			.GetCards())
		{
			card.SetInputEnabled(isEnabled);
		}
	}

	protected void SetInputEnableForRefreshButton(bool isEnabled)
	{
		Card refreshButtonCard = GetRefreshButtonCard();
		if (refreshButtonCard != null)
		{
			refreshButtonCard.SetInputEnabled(isEnabled);
		}
	}

	protected void SetInputEnableForTavernUpgradeButton(bool isEnabled)
	{
		Card tavernUpgradeButtonCard = GetTavernUpgradeButtonCard();
		if (tavernUpgradeButtonCard != null)
		{
			tavernUpgradeButtonCard.SetInputEnabled(isEnabled);
		}
	}

	protected void SetInputEnableForFrozenButton(bool isEnabled)
	{
		Card freezeButtonCard = GetFreezeButtonCard();
		if (freezeButtonCard != null)
		{
			freezeButtonCard.SetInputEnabled(isEnabled);
		}
	}

	public override bool NotifyOfPlayError(PlayErrors.ErrorType error, int? errorParam, Entity errorSource)
	{
		if (error == PlayErrors.ErrorType.REQ_ATTACK_GREATER_THAN_0)
		{
			return true;
		}
		return false;
	}

	private void ForceShowFriendlyHeroActor()
	{
		Card heroCard = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
		if ((bool)heroCard)
		{
			heroCard.ShowCard();
			if (heroCard.GetActor() != null)
			{
				heroCard.GetActor().Show();
			}
		}
	}
}
