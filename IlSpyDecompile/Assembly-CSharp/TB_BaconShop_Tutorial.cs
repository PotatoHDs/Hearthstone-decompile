using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TB_BaconShop_Tutorial : TB_BaconShop
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static Map<GameEntityOption, string> s_stringOptions = InitStringOptions();

	private static readonly AssetReference Bob_BrassRing_Quote = new AssetReference("Bob_BrassRing_Quote.prefab:89385ff7d67aa1e49bcf25bc15ca61f6");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterFreezing_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterFreezing_01.prefab:4dc4f16c60d79ed40be28f898346df02");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterSelling_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterSelling_01.prefab:d71f34687d09a064bab5d202ea3fb965");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_01.prefab:d3ad51eb14e20324387e5dbbd1e82811");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_03.prefab:03260a54e677e4247aa19eb29662371e");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_04 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_04.prefab:ecf9f68fe25195a4a93b50d0c8e82a1a");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterTriple_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterTriple_01.prefab:dc92cab5423afa045b4ad528dd25f9d5");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterTriple_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterTriple_02.prefab:d845164292fb45a4f85eed478ad5d1c2");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterTriple_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterTriple_03.prefab:4704869d519d6e7479602dd15e12b175");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_CombatWin_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_CombatWin_03.prefab:8ff8566f08747ad4bb76409e6db1504b");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_FirstBattle_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_FirstBattle_01.prefab:b88923936df527147b6eda2517ce91ef");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_FirstVictory_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_FirstVictory_01.prefab:e40b154f86185d3428ffa48867241f76");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_General_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_General_02.prefab:d5908d1fd355b8c4b8344e300dc4fc42");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_HeroSelection_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_HeroSelection_01.prefab:93cd3efc86126de478be0e56c8e275a7");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Hire_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Hire_01.prefab:bfd9513b46b92e84da5f22e01a0387a4");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Hire_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Hire_02.prefab:eb20d844bee8bdf4f9cbb514c8ab8580");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Idle_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Idle_02.prefab:3808bb035b74ac04f9bb4be91009e2b7");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Idle_03.prefab:34248aac29c16274c95fb999635368ff");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_ModeSelect_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_ModeSelect_01.prefab:261a9714c4cf3ad4d8944d9127a38ddf");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_03.prefab:e3cbf2a35ac2e8245b5bb3de3baa054e");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_02.prefab:eb000d8de28cd6d478b9a718ebe1fd9e");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitWork_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitWork_01.prefab:a5e1a6db102be6d4495aa1cd7dc7ddfc");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_ShopFirstTime_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_ShopFirstTime_01.prefab:8070938a2c3ba2f4ea92b7f0b5fdf280");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_ShopUpgrade_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_ShopUpgrade_01.prefab:f5019f07757dde341aae503b53a9102e");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Triple_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Triple_01.prefab:26a5500e887280c40a810c01741e2544");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Triple_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Triple_02.prefab:1aff064425948044791b8b9e3f8de61b");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Triple_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Triple_03.prefab:e14f16322b47b814d8ccb07a60ccf6d1");

	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_UpgradeShop_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_UpgradeShop_01.prefab:ec1459e08d9b5a04e97c6a3499505cf6");

	protected const string TUTORIAL_MINION_ID = "CS2_065";

	private const GameSaveKeyId GAME_SAVE_PARENT_KEY = GameSaveKeyId.BACON;

	private const int MISSION_EVENT_UPDATE_NAME_BANNER = 5;

	protected Notification m_dragBuyTutorialNotification;

	protected Notification m_recruitReminderTutorialNofification;

	protected Notification m_refreshButtonTutorialNotification;

	protected Notification m_minionMoveTutorialNotification;

	protected Notification m_upgradeTavernTutorialNotification;

	protected Notification m_dragSellTutorialNotification;

	protected Notification m_freezeTutorialNotification;

	protected Notification m_popupTutorialNotification;

	protected Notification m_manaNotifier;

	protected Notification m_handBounceArrow;

	protected bool m_shouldPlayMinionMoveTutorial = true;

	protected bool m_shouldShowHandBounceArrow;

	private static readonly AssetReference BaconTutorialPopup = new AssetReference("BaconTutorialPopup.prefab:b68a7306f3300874a833909005fa797d");

	private static readonly AssetReference DRAGBUY_DIALOG_TUTORIAL_PREFAB = BaconTutorialPopup;

	protected const string DRAGBUY_TITLE_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_DRAGBUY_TITLE_TUTORIAL";

	protected const string DRAGBUY_BODY_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_DRAGBUY_BODY_TUTORIAL";

	protected const string DRAGBUY_BUTTON_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_CONFIRM_BUTTON_TUTORIAL";

	private static readonly AssetReference DRAGSELL_DIALOG_TUTORIAL_PREFAB = BaconTutorialPopup;

	protected const string DRAGSELL_TITLE_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_DRAGSELL_TITLE_TUTORIAL";

	protected const string DRAGSELL_BODY_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_DRAGSELL_BODY_TUTORIAL";

	protected const string DRAGSELL_BUTTON_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_CONFIRM_BUTTON_TUTORIAL";

	private static readonly AssetReference TRIPLE_DIALOG_TUTORIAL_PREFAB = BaconTutorialPopup;

	protected const string TRIPLE_TITLE_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_TRIPLE_TITLE_TUTORIAL";

	protected const string TRIPLE_BODY_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_TRIPLE_BODY_TUTORIAL";

	protected const string TRIPLE_BUTTON_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_CONFIRM_BUTTON_TUTORIAL";

	private static readonly AssetReference COMBAT_DIALOG_TUTORIAL_PREFAB = BaconTutorialPopup;

	protected const string COMBAT_TITLE_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_COMBAT_TITLE_TUTORIAL";

	protected const string COMBAT_BODY_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_COMBAT_BODY_TUTORIAL";

	protected const string COMBAT_BUTTON_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_CONFIRM_BUTTON_TUTORIAL";

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.MULLIGAN_HAS_HERO_LOBBY,
				false
			},
			{
				GameEntityOption.WAIT_FOR_RATING_INFO,
				false
			}
		};
	}

	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	public TB_BaconShop_Tutorial()
	{
		m_gameOptions.AddOptions(s_booleanOptions, s_stringOptions);
		HistoryManager.Get().DisableHistory();
		PlayerLeaderboardManager.Get().SetEnabled(enabled: true);
		PlayerLeaderboardManager.Get().SetAllowFakePlayers(enabled: true);
		EndTurnButton.Get().SetDisabled(disabled: true);
		SceneMgr.Get().RegisterSceneLoadedEvent(OnGameplaySceneLoaded);
		InitializeTurnTimer();
		m_gamePhase = 1;
		GameEntity.Coroutines.StartCoroutine(OnShopPhase());
	}

	~TB_BaconShop_Tutorial()
	{
		HideShopTutorials();
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().UnregisterSceneLoadedEvent(OnGameplaySceneLoaded);
		}
	}

	private void OnGameplaySceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (mode == SceneMgr.Mode.GAMEPLAY)
		{
			SceneMgr.Get().UnregisterSceneLoadedEvent(OnGameplaySceneLoaded);
			ManaCrystalMgr.Get().SetEnemyManaCounterActive(active: false);
			if (GameMgr.Get().IsSpectator())
			{
				InitializeFakeHeroLeaderboard();
			}
			HideShopTutorials();
			GameEntity.Coroutines.StartCoroutine(OnReconnect());
		}
	}

	public override InputManager.ZoneTooltipSettings GetZoneTooltipSettings()
	{
		return new InputManager.ZoneTooltipSettings
		{
			EnemyDeck = new InputManager.TooltipSettings(allowed: false),
			EnemyHand = new InputManager.TooltipSettings(allowed: false),
			EnemyMana = new InputManager.TooltipSettings(allowed: false),
			FriendlyDeck = new InputManager.TooltipSettings(allowed: false),
			FriendlyMana = new InputManager.TooltipSettings(allowed: true, base.GetFriendlyManaTooltipContent)
		};
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
		GameEntity.Coroutines.StartCoroutine(SkipStandardMulliganWithTiming());
		return true;
	}

	protected override IEnumerator OnShopPhase()
	{
		yield return ShowPopup("Shop");
		PlayerLeaderboardManager.Get().UpdateLayout();
		GameState.Get().GetOpposingSidePlayer().UpdateDisplayInfo();
		UpdateNameBanner();
		ShowTechLevelDisplay(shown: true);
		int tag = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.RESOURCES);
		TurnStartManager.Get().NotifyOfManaCrystalFilled(tag);
		yield return new WaitForSeconds(3f);
	}

	protected override IEnumerator OnCombatPhase()
	{
		HideShopTutorials();
		yield return ShowPopup("Combat");
		ShowTechLevelDisplay(shown: false);
		GameState.Get().GetOpposingSidePlayer().UpdateDisplayInfo();
		UpdateNameBanner();
	}

	protected IEnumerator OnReconnect()
	{
		if (GameState.Get().GetGameEntity().GetTag(GAME_TAG.TURN) <= 12)
		{
			SetInputEnableForAllButtons(isEnabled: false);
			SetInputEnableForAllCards(isEnabled: false);
		}
		HideShopTutorials();
		yield return new WaitForSeconds(3f);
		int tag = GameState.Get().GetGameEntity().GetTag(GAME_TAG.MISSION_EVENT);
		if (GameMgr.Get().IsSpectator())
		{
			if (tag == 1 || tag == 2 || tag == 5)
			{
				GameEntity.Coroutines.StartCoroutine(HandleMissionEventWithTiming(tag));
			}
		}
		else
		{
			GameEntity.Coroutines.StartCoroutine(HandleMissionEventWithTiming(tag));
		}
		yield return null;
	}

	protected void InitializeFakeHeroLeaderboard()
	{
		foreach (Card card in GameState.Get().GetFriendlySidePlayer().GetGraveyardZone()
			.GetCards())
		{
			Entity entity = card.GetEntity();
			if (entity.GetCardType() == TAG_CARDTYPE.HERO)
			{
				PlayerLeaderboardManager.Get().CreatePlayerTile(entity);
			}
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		switch (missionEvent)
		{
		case 1:
			m_gamePhase = 1;
			yield return OnShopPhase();
			break;
		case 2:
			m_gamePhase = 2;
			yield return OnCombatPhase();
			break;
		case 5:
			GameState.Get().GetOpposingSidePlayer().UpdateDisplayInfo();
			UpdateNameBanner();
			break;
		}
		if (GameMgr.Get().IsSpectator())
		{
			yield break;
		}
		switch (missionEvent)
		{
		case 3:
			if (GameState.Get().GetGameEntity().GetTag(GAME_TAG.TURN) != 9)
			{
				SetInputEnableForFrozenButton(isEnabled: false);
				yield return new WaitForSeconds(0.75f);
				SetInputEnableForFrozenButton(isEnabled: true);
			}
			break;
		case 4:
			SetInputEnableForRefreshButton(isEnabled: false);
			yield return new WaitForSeconds(0.75f);
			SetInputEnableForRefreshButton(isEnabled: true);
			break;
		case 10:
			GameState.Get().SetBusy(busy: true);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_ModeSelect_01);
			InitializeFakeHeroLeaderboard();
			yield return new WaitForSeconds(0.5f);
			GameState.Get().SetBusy(busy: false);
			PlayerLeaderboardManager.Get().UpdateLayout();
			break;
		case 11:
			GameState.Get().SetBusy(busy: true);
			CreateTutorialDialog(DRAGBUY_DIALOG_TUTORIAL_PREFAB, "GAMEPLAY_BACON_DRAGBUY_TITLE_TUTORIAL", "GAMEPLAY_BACON_DRAGBUY_BODY_TUTORIAL", "GAMEPLAY_BACON_CONFIRM_BUTTON_TUTORIAL", UserPressedDragBuyTutorial, new Vector2(0.5f, 0.5f));
			break;
		case 12:
			yield return new WaitForSeconds(1f);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_ShopFirstTime_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 13:
		{
			GameState.Get().SetBusy(busy: true);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_Hire_01);
			GameState.Get().SetBusy(busy: false);
			SetInputEnableForAllCards(isEnabled: true);
			Card cardInOpposingPlay = GetCardInOpposingPlay("CS2_065");
			if (cardInOpposingPlay != null)
			{
				ShowDragBuyTutorial(cardInOpposingPlay, "GAMEPLAY_BACON_DRAGBUY_TUTORIAL");
				GameEntity.Coroutines.StartCoroutine(ShowOrHideDragBuyTutorial("GAMEPLAY_BACON_DRAGBUY_TUTORIAL"));
			}
			break;
		}
		case 14:
			SetInputEnableForAllCards(isEnabled: false);
			HideNotification(m_dragBuyTutorialNotification);
			yield return ShowManaArrowWithText("GAMEPLAY_BACON_COIN_TUTORIAL_1");
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_RecruitWork_01);
			SetInputEnableForAllCards(isEnabled: true);
			yield return new WaitForSeconds(3f);
			ShowHandBounceArrow();
			break;
		case 15:
			HideHandBounceArrow();
			GameState.Get().SetBusy(busy: true);
			HideNotification(m_dragBuyTutorialNotification);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_HeroSelection_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 20:
			SetInputEnableForAllButtons(isEnabled: false);
			GameState.Get().SetBusy(busy: true);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_General_02);
			yield return new WaitForSeconds(0.25f);
			RecruitReminderTutorial();
			GameState.Get().SetBusy(busy: false);
			SetInputEnableForBuy(isEnabled: true);
			break;
		case 22:
			m_shouldShowHandBounceArrow = false;
			SetInputEnableForAllCards(isEnabled: false);
			HideNotification(m_recruitReminderTutorialNofification);
			GameState.Get().SetBusy(busy: true);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_Idle_02);
			yield return ShowManaArrowWithText("GAMEPLAY_BACON_COIN_TUTORIAL_2");
			yield return new WaitForSeconds(0.5f);
			SetInputEnableForAllCards(isEnabled: true);
			GameState.Get().SetBusy(busy: false);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_02);
			yield return new WaitForSeconds(2.5f);
			ShowHandBounceArrow();
			break;
		case 24:
			HideHandBounceArrow();
			SetInputEnableForAllCards(isEnabled: true);
			GameState.Get().SetBusy(busy: true);
			ShowMinionMoveTutorial();
			GameEntity.Coroutines.StartCoroutine(ShowOrHideMoveMinionTutorial());
			GameState.Get().SetBusy(busy: false);
			break;
		case 25:
			m_shouldPlayMinionMoveTutorial = false;
			HideNotification(m_minionMoveTutorialNotification);
			GameState.Get().SetBusy(busy: true);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_FirstBattle_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 30:
			SetInputEnableForAllButtons(isEnabled: false);
			SetInputEnableForAllCards(isEnabled: false);
			GameState.Get().SetBusy(busy: true);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_ShopUpgrade_01);
			SetInputEnableForTavernUpgradeButton(isEnabled: true);
			GameState.Get().SetBusy(busy: false);
			SetInputEnableForAllCards(isEnabled: false);
			yield return new WaitForSeconds(0.5f);
			ShowTavernUpgradeButtonTutorial();
			break;
		case 31:
			SetInputEnableForRefreshButton(isEnabled: false);
			GameState.Get().SetBusy(busy: true);
			HideNotification(m_upgradeTavernTutorialNotification);
			SetInputEnableForBuy(isEnabled: false);
			yield return new WaitForSeconds(0.5f);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_03);
			GameState.Get().SetBusy(busy: false);
			ShowRefreshButtonTutorial();
			SetInputEnableForRefreshButton(isEnabled: true);
			break;
		case 32:
			SetInputEnableForRefreshButton(isEnabled: false);
			HideNotification(m_refreshButtonTutorialNotification);
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(0.5f);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_Hire_02);
			GameState.Get().SetBusy(busy: false);
			SetInputEnableForBuy(isEnabled: true);
			SetInputEnableForAllCards(isEnabled: true);
			yield return new WaitForSeconds(5f);
			RecruitTutorialWithBoardSize(4);
			GameEntity.Coroutines.StartCoroutine(ShowOrHideRecruitTutorialWithBoardSize(4));
			break;
		case 33:
			SetInputEnableForBuy(isEnabled: true);
			SetInputEnableForAllCards(isEnabled: true);
			yield return new WaitForSeconds(5f);
			ShowHandBounceArrow();
			break;
		case 34:
			HideHandBounceArrow();
			HideNotification(m_dragBuyTutorialNotification);
			HideNotification(m_refreshButtonTutorialNotification);
			SetInputEnableForRefreshButton(isEnabled: false);
			break;
		case 40:
			SetInputEnableForAllButtons(isEnabled: false);
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(1f);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_Triple_02);
			GameState.Get().SetBusy(busy: false);
			SetInputEnableForAllCards(isEnabled: true);
			yield return new WaitForSeconds(5f);
			RecruitTutorialWithBoardSize(2);
			GameEntity.Coroutines.StartCoroutine(ShowOrHideRecruitTutorialWithBoardSize(2));
			break;
		case 41:
			SetInputEnableForAllCards(isEnabled: false);
			SetInputEnableForAllButtons(isEnabled: false);
			GameState.Get().SetBusy(busy: true);
			CreateTutorialDialog(TRIPLE_DIALOG_TUTORIAL_PREFAB, "GAMEPLAY_BACON_TRIPLE_TITLE_TUTORIAL", "GAMEPLAY_BACON_TRIPLE_BODY_TUTORIAL", "GAMEPLAY_BACON_CONFIRM_BUTTON_TUTORIAL", UserPressedTripleTutorial, new Vector2(0.5f, 0f));
			break;
		case 42:
		{
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(0.5f);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_Triple_01);
			Card cardInOpposingPlay = GetCardInOpposingPlay("CS2_065");
			if (cardInOpposingPlay != null)
			{
				cardInOpposingPlay.SetInputEnabled(enabled: true);
				ShowDragBuyTutorial(cardInOpposingPlay, "GAMEPLAY_BACON_DRAGBUY_TRIPLE_TUTORIAL");
				GameEntity.Coroutines.StartCoroutine(ShowOrHideDragBuyTutorial("GAMEPLAY_BACON_DRAGBUY_TRIPLE_TUTORIAL"));
			}
			GameState.Get().SetBusy(busy: false);
			SetInputEnableForBuy(isEnabled: true);
			break;
		}
		case 44:
			HideNotification(m_dragBuyTutorialNotification);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_AfterTriple_01);
			SetInputEnableForAllCards(isEnabled: true);
			yield return new WaitForSeconds(6f);
			ShowHandBounceArrow();
			break;
		case 45:
			HideHandBounceArrow();
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_Triple_03);
			SetInputEnableForAllCards(isEnabled: true);
			yield return new WaitForSeconds(6f);
			ShowHandBounceArrow();
			break;
		case 46:
			HideHandBounceArrow();
			SetInputEnableForAllCards(isEnabled: true);
			yield return new WaitForSeconds(6f);
			ShowHandBounceArrow();
			break;
		case 47:
			HideHandBounceArrow();
			SetInputEnableForAllButtons(isEnabled: true);
			SetInputEnableForAllCards(isEnabled: true);
			GameState.Get().SetBusy(busy: true);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_03);
			GameState.Get().SetBusy(busy: false);
			break;
		case 51:
			SetInputEnableForAllButtons(isEnabled: false);
			SetInputEnableForAllCards(isEnabled: false);
			GameState.Get().SetBusy(busy: true);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_UpgradeShop_01);
			yield return new WaitForSeconds(0.5f);
			GameState.Get().SetBusy(busy: false);
			SetInputEnableForAllCards(isEnabled: false);
			SetInputEnableForTavernUpgradeButton(isEnabled: true);
			ShowTavernUpgradeButtonTutorial();
			break;
		case 52:
			GameState.Get().SetBusy(busy: true);
			HideNotification(m_upgradeTavernTutorialNotification);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_04);
			yield return new WaitForSeconds(0.5f);
			GameState.Get().SetBusy(busy: false);
			ShowRefreshButtonTutorial("GAMEPLAY_BACON_REFRESH_UPGRADE_TUTORIAL");
			SetInputEnableForRefreshButton(isEnabled: true);
			break;
		case 53:
			SetInputEnableForRefreshButton(isEnabled: false);
			HideNotification(m_refreshButtonTutorialNotification);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_01);
			yield return new WaitForSeconds(0.5f);
			SetInputEnableForFrozenButton(isEnabled: true);
			ShowFreezeTutorial();
			break;
		case 54:
			m_shouldShowHandBounceArrow = false;
			SetInputEnableForFrozenButton(isEnabled: false);
			HideNotification(m_freezeTutorialNotification);
			GameState.Get().SetBusy(busy: true);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_AfterFreezing_01);
			GameState.Get().SetBusy(busy: false);
			SetInputEnableForAllCards(isEnabled: true);
			break;
		case 60:
			SetInputEnableForAllButtons(isEnabled: false);
			SetInputEnableForBuy(isEnabled: true);
			GameState.Get().SetBusy(busy: true);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_AfterTriple_02);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_Hire_01);
			GameState.Get().SetBusy(busy: false);
			yield return new WaitForSeconds(2f);
			RecruitTutorialWithBoardSize(4);
			GameEntity.Coroutines.StartCoroutine(ShowOrHideRecruitTutorialWithBoardSize(4));
			break;
		case 61:
			SetInputEnableForFriendlyHandCards(isEnabled: false);
			SetInputEnableForBuy(isEnabled: true);
			yield return new WaitForSeconds(6f);
			RecruitTutorialWithBoardSize(3);
			GameEntity.Coroutines.StartCoroutine(ShowOrHideRecruitTutorialWithBoardSize(3));
			break;
		case 62:
			SetInputEnableForAllButtons(isEnabled: false);
			SetInputEnableForFriendlyHandCards(isEnabled: false);
			GameState.Get().SetBusy(busy: true);
			CreateTutorialDialog(DRAGSELL_DIALOG_TUTORIAL_PREFAB, "GAMEPLAY_BACON_DRAGSELL_TITLE_TUTORIAL", "GAMEPLAY_BACON_DRAGSELL_BODY_TUTORIAL", "GAMEPLAY_BACON_CONFIRM_BUTTON_TUTORIAL", UserPressedDragSellTutorial, new Vector2(0f, 0.5f));
			break;
		case 63:
			GameState.Get().SetBusy(busy: false);
			SetInputEnableForAllCards(isEnabled: true);
			SetInputEnableForFriendlyHandCards(isEnabled: false);
			ShowDragSellTutorial();
			break;
		case 64:
			SetInputEnableForBuy(isEnabled: true);
			GameState.Get().SetBusy(busy: true);
			HideNotification(m_dragSellTutorialNotification);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_AfterSelling_01);
			GameState.Get().SetBusy(busy: false);
			yield return new WaitForSeconds(6f);
			RecruitTutorialWithBoardSize(2);
			GameEntity.Coroutines.StartCoroutine(ShowOrHideRecruitTutorialWithBoardSize(2));
			break;
		case 65:
			HideNotification(m_handBounceArrow);
			SetInputEnableForFriendlyHandCards(isEnabled: true);
			yield return new WaitForSeconds(6f);
			ShowHandBounceArrow();
			break;
		case 66:
			HideHandBounceArrow();
			SetInputEnableForFriendlyHandCards(isEnabled: true);
			yield return new WaitForSeconds(6f);
			ShowHandBounceArrow();
			break;
		case 67:
			HideHandBounceArrow();
			SetInputEnableForFriendlyHandCards(isEnabled: true);
			yield return new WaitForSeconds(6f);
			ShowHandBounceArrow();
			break;
		case 68:
			HideHandBounceArrow();
			SetInputEnableForAllButtons(isEnabled: true);
			SetInputEnableForAllCards(isEnabled: true);
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_AfterTriple_03);
			break;
		case 70:
			HideHandBounceArrow();
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_CombatWin_03);
			break;
		case 79:
			HideHandBounceArrow();
			GameState.Get().SetBusy(busy: true);
			CreateTutorialDialog(COMBAT_DIALOG_TUTORIAL_PREFAB, "GAMEPLAY_BACON_COMBAT_TITLE_TUTORIAL", "GAMEPLAY_BACON_COMBAT_BODY_TUTORIAL", "GAMEPLAY_BACON_CONFIRM_BUTTON_TUTORIAL", UserPressedCombatTutorial, Vector2.zero);
			break;
		case 80:
			HideHandBounceArrow();
			yield return PlayBobLine(VO_DALA_BOSS_99h_Male_Human_Idle_03);
			break;
		}
	}

	public override void OnPlayThinkEmote()
	{
		if (HasSeenAllTutorial() && !m_enemySpeaking)
		{
			Player currentPlayer = GameState.Get().GetCurrentPlayer();
			if (currentPlayer.IsFriendlySide())
			{
				currentPlayer.GetHeroCard().HasActiveEmoteSound();
			}
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		HideShopTutorials();
		PlayerLeaderboardManager.Get().UpdateLayout();
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			if (GameState.Get().GetBooleanGameOption(GameEntityOption.WAIT_FOR_RATING_INFO))
			{
				yield return new WaitForSeconds(5f);
			}
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWait(Bob_BrassRing_Quote, VO_DALA_BOSS_99h_Male_Human_FirstVictory_01));
		}
	}

	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		HideShopTutorials();
		GameEntity.Coroutines.StartCoroutine(HandleGameOverWithTiming(gameResult));
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			base.NotifyOfGameOver(gameResult);
		}
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			PegCursor.Get().SetMode(PegCursor.Mode.STOPWAITING);
			Network.Get().DisconnectFromGameServer();
			SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
			GameMgr.Get().PreparePostGameSceneMode(postGameSceneMode);
			SceneMgr.Get().SetNextMode(postGameSceneMode);
		}
	}

	protected IEnumerator PlayBobLine(string voLine)
	{
		Actor bobActor = GetBobActor();
		if (bobActor != null && bobActor.GetEntity() != null)
		{
			string legacyAssetName = new AssetReference(voLine).GetLegacyAssetName();
			yield return PlaySoundAndWait(voLine, legacyAssetName, Notification.SpeechBubbleDirection.TopRight, bobActor);
		}
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
		if (GameState.Get().GetOpposingSidePlayer() == null)
		{
			return null;
		}
		if (GameState.Get().GetOpposingSidePlayer().GetHero() == null)
		{
			return null;
		}
		return GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetName();
	}

	protected new void InitializeTurnTimer()
	{
		TurnTimer.Get().SetGameModeSettings(new TurnTimerGameModeSettings
		{
			m_RopeFuseVolume = 0.05f,
			m_EndTurnButtonExplosionVolume = 0f,
			m_RopeRolloutVolume = 0.3f,
			m_PlayMusicStinger = false,
			m_PlayTimeoutFx = false,
			m_PlayTickSound = false
		});
	}

	protected void UserPressedDragBuyTutorial(UIEvent e)
	{
		HandleMissionEvent(12);
	}

	protected void UserPressedTripleTutorial(UIEvent e)
	{
		HandleMissionEvent(42);
	}

	protected void UserPressedDragSellTutorial(UIEvent e)
	{
		HandleMissionEvent(63);
	}

	protected void UserPressedCombatTutorial(UIEvent e)
	{
		GameState.Get().SetBusy(busy: false);
	}

	protected override void HideShopTutorials()
	{
		HideHandBounceArrow();
		NotificationManager.Get().DestroyAllPopUps();
	}

	protected void SetInputEnableForAllButtons(bool isEnabled)
	{
		SetInputEnableForBuy(isEnabled);
		SetInputEnableForRefreshButton(isEnabled);
		SetInputEnableForTavernUpgradeButton(isEnabled);
		SetInputEnableForFrozenButton(isEnabled);
	}

	protected void SetInputEnableForAllCards(bool isEnabled)
	{
		List<Card> cards = GameState.Get().GetFriendlySidePlayer().GetBattlefieldZone()
			.GetCards();
		List<Card> cards2 = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone()
			.GetCards();
		foreach (Card item in Enumerable.Concat(second: GameState.Get().GetFriendlySidePlayer().GetHandZone()
			.GetCards(), first: cards.Concat(cards2)).ToList())
		{
			item.SetInputEnabled(isEnabled);
		}
	}

	protected void SetInputEnableForFriendlyHandCards(bool isEnabled)
	{
		foreach (Card card in GameState.Get().GetFriendlySidePlayer().GetHandZone()
			.GetCards())
		{
			card.SetInputEnabled(isEnabled);
		}
	}

	public TutorialNotification CreateTutorialDialog(AssetReference assetPrefab, string headlineGameString, string bodyTextGameString, string buttonGameString, UIEvent.Handler buttonHandler, Vector2 materialOffset)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(assetPrefab);
		if (gameObject == null)
		{
			Debug.LogError("Unable to load tutorial dialog TutorialIntroDialog prefab.");
			return null;
		}
		TutorialNotification notification = gameObject.GetComponent<TutorialNotification>();
		if (notification == null)
		{
			Debug.LogError("TutorialNotification component does not exist on TutorialIntroDialog prefab.");
			return null;
		}
		TransformUtil.AttachAndPreserveLocalTransform(gameObject.transform, OverlayUI.Get().m_heightScale.m_Center);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			gameObject.transform.localScale = 1.5f * gameObject.transform.localScale;
		}
		m_popupTutorialNotification = notification;
		notification.headlineUberText.Text = GameStrings.Get(headlineGameString);
		notification.speechUberText.Text = GameStrings.Get(bodyTextGameString);
		notification.m_ButtonStart.SetText(GameStrings.Get(buttonGameString));
		notification.artOverlay.GetMaterial().mainTextureOffset = materialOffset;
		notification.m_ButtonStart.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			if (buttonHandler != null)
			{
				buttonHandler(e);
			}
			notification.m_ButtonStart.ClearEventListeners();
			NotificationManager.Get().DestroyNotification(notification, 0f);
		});
		m_popupTutorialNotification.PlayBirth();
		UniversalInputManager.Get().SetGameDialogActive(active: true);
		return notification;
	}

	protected void HideNotification(Notification notification, bool hideImmediately = false)
	{
		if (notification != null)
		{
			if (hideImmediately)
			{
				NotificationManager.Get().DestroyNotificationNowWithNoAnim(notification);
			}
			else
			{
				NotificationManager.Get().DestroyNotification(notification, 0f);
			}
		}
	}

	protected void ShowDragBuyTutorial(Card card, string textID = "GAMEPLAY_BACON_PLAY_MINION_TUTORIAL", bool hideImmediately = false)
	{
		if (!(card == null))
		{
			Vector3 position = card.transform.position;
			Vector3 position2;
			Notification.PopUpArrowDirection direction;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				position2 = new Vector3(position.x + 0.05f, position.y, position.z + 2.9f);
				direction = Notification.PopUpArrowDirection.Down;
			}
			else
			{
				position2 = new Vector3(position.x, position.y, position.z + 2.5f);
				direction = Notification.PopUpArrowDirection.Down;
			}
			m_dragBuyTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(textID));
			m_dragBuyTutorialNotification.ShowPopUpArrow(direction);
			m_dragBuyTutorialNotification.PulseReminderEveryXSeconds(2f);
		}
	}

	private IEnumerator ShowOrHideDragBuyTutorial(string textString)
	{
		while (!InputManager.Get().GetHeldCard())
		{
			yield return null;
		}
		HideNotification(m_dragBuyTutorialNotification);
		while ((bool)InputManager.Get().GetHeldCard())
		{
			yield return null;
		}
		yield return new WaitForSeconds(2f);
		Card cardInOpposingPlay = GetCardInOpposingPlay("CS2_065");
		if (cardInOpposingPlay != null || (bool)InputManager.Get().GetHeldCard())
		{
			ShowDragBuyTutorial(cardInOpposingPlay, textString);
			GameEntity.Coroutines.StartCoroutine(ShowOrHideDragBuyTutorial(textString));
		}
		HideNotification(m_dragBuyTutorialNotification);
	}

	protected void RecruitTutorialWithBoardSize(int enemyBoardSize, string textID = "GAMEPLAY_BACON_RECRUIT_REMINDER_TUTORIAL_2", bool hideImmediately = false)
	{
		List<Card> cards = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone()
			.GetCards();
		if (cards.Count == enemyBoardSize)
		{
			Vector3 position = cards[0].transform.position;
			Vector3 position2;
			Notification.PopUpArrowDirection direction;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				position2 = new Vector3(position.x + 0.05f, position.y, position.z + 2.9f);
				direction = Notification.PopUpArrowDirection.Down;
			}
			else
			{
				position2 = new Vector3(position.x, position.y, position.z + 2.5f);
				direction = Notification.PopUpArrowDirection.Down;
			}
			m_dragBuyTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(textID));
			m_dragBuyTutorialNotification.ShowPopUpArrow(direction);
			m_dragBuyTutorialNotification.PulseReminderEveryXSeconds(2f);
		}
	}

	private IEnumerator ShowOrHideRecruitTutorialWithBoardSize(int enemyBoardSize, string textID = "GAMEPLAY_BACON_RECRUIT_REMINDER_TUTORIAL_2")
	{
		while (!InputManager.Get().GetHeldCard())
		{
			yield return null;
		}
		HideNotification(m_dragBuyTutorialNotification);
		while ((bool)InputManager.Get().GetHeldCard())
		{
			yield return null;
		}
		yield return new WaitForSeconds(2f);
		List<Card> cards = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone()
			.GetCards();
		if (cards.Count == enemyBoardSize && (cards[0] != null || (bool)InputManager.Get().GetHeldCard()))
		{
			RecruitTutorialWithBoardSize(enemyBoardSize, textID);
			GameEntity.Coroutines.StartCoroutine(ShowOrHideRecruitTutorialWithBoardSize(enemyBoardSize, textID));
		}
	}

	protected void RecruitReminderTutorial()
	{
		if (GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone()
			.GetCards()
			.Count >= 3)
		{
			Vector3 position = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone()
				.transform.position;
			position += new Vector3(0f, 0f, 2.25f);
			string key = "GAMEPLAY_BACON_RECRUIT_REMINDER_TUTORIAL";
			m_recruitReminderTutorialNofification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, TutorialEntity.GetTextScale(), GameStrings.Get(key));
			m_recruitReminderTutorialNofification.ShowPopUpArrow(Notification.PopUpArrowDirection.BottomThree);
			m_recruitReminderTutorialNofification.PulseReminderEveryXSeconds(2f);
		}
	}

	protected void ShowRefreshButtonTutorial(string textID = "GAMEPLAY_BACON_REFRESH_TUTORIAL", bool hideImmediately = false)
	{
		List<Zone> list = ZoneMgr.Get().FindZonesForSide(Player.Side.FRIENDLY);
		Zone zone = null;
		foreach (Zone item in list)
		{
			if (item is ZoneGameModeButton && ((ZoneGameModeButton)item).m_ButtonSlot == 2)
			{
				zone = item;
			}
		}
		Vector3 position = zone.transform.position;
		Vector3 position2 = ((!UniversalInputManager.UsePhoneUI) ? new Vector3(position.x, position.y, position.z - 2.25f) : new Vector3(position.x, position.y, position.z - 2.5f));
		m_refreshButtonTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(textID));
		m_refreshButtonTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Up);
		m_refreshButtonTutorialNotification.PulseReminderEveryXSeconds(2f);
	}

	protected void ShowTavernUpgradeButtonTutorial(bool hideImmediately = false)
	{
		List<Zone> list = ZoneMgr.Get().FindZonesForSide(Player.Side.FRIENDLY);
		Zone zone = null;
		foreach (Zone item in list)
		{
			if (item is ZoneGameModeButton && ((ZoneGameModeButton)item).m_ButtonSlot == 3)
			{
				zone = item;
			}
		}
		Vector3 position = zone.transform.position;
		Vector3 position2 = new Vector3(position.x, position.y, position.z - 2.25f);
		string key = "GAMEPLAY_BACON_MINION_UPGRADE_TAVERN_TUTORIAL";
		m_upgradeTavernTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key));
		m_upgradeTavernTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Up);
		m_upgradeTavernTutorialNotification.PulseReminderEveryXSeconds(2f);
	}

	protected void ShowMinionMoveTutorial()
	{
		Card leftMostMinionInFriendlyPlay = GetLeftMostMinionInFriendlyPlay();
		if (!(leftMostMinionInFriendlyPlay == null))
		{
			Vector3 position = leftMostMinionInFriendlyPlay.transform.position;
			Vector3 position2 = ((!UniversalInputManager.UsePhoneUI) ? new Vector3(position.x, position.y, position.z + 2.5f) : new Vector3(position.x + 0.05f, position.y, position.z + 2.6f));
			string key = "GAMEPLAY_BACON_MINION_MOVE_TUTORIAL";
			m_minionMoveTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key));
			m_minionMoveTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
			m_minionMoveTutorialNotification.PulseReminderEveryXSeconds(2f);
		}
	}

	private IEnumerator ShowOrHideMoveMinionTutorial()
	{
		while (!InputManager.Get().GetHeldCard())
		{
			yield return null;
		}
		HideNotification(m_minionMoveTutorialNotification);
		while ((bool)InputManager.Get().GetHeldCard())
		{
			yield return null;
		}
		yield return new WaitForSeconds(2f);
		if ((GetLeftMostMinionInFriendlyPlay() != null || (bool)InputManager.Get().GetHeldCard()) && m_shouldPlayMinionMoveTutorial)
		{
			ShowMinionMoveTutorial();
			GameEntity.Coroutines.StartCoroutine(ShowOrHideMoveMinionTutorial());
		}
	}

	protected void ShowDragSellTutorial(bool hideImmediately = false)
	{
		Card card = GetBobActor().GetCard();
		if (!(card == null))
		{
			card.GetActor().SetActorState(ActorStateType.CARD_SELECTABLE);
			Vector3 position = card.transform.position;
			Vector3 position2 = ((!UniversalInputManager.UsePhoneUI) ? new Vector3(position.x - 3.2f, position.y, position.z) : new Vector3(position.x - 3.3f, position.y, position.z - 0f));
			string key = "GAMEPLAY_BACON_DRAGSELL_TUTORIAL";
			m_dragSellTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key));
			m_dragSellTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
			m_dragSellTutorialNotification.PulseReminderEveryXSeconds(2f);
		}
	}

	protected void ShowFreezeTutorial(bool hideImmediately = false)
	{
		List<Zone> list = ZoneMgr.Get().FindZonesForSide(Player.Side.FRIENDLY);
		Zone zone = null;
		foreach (Zone item in list)
		{
			if (item is ZoneGameModeButton && ((ZoneGameModeButton)item).m_ButtonSlot == 1)
			{
				zone = item;
			}
		}
		Vector3 position = zone.transform.position;
		Vector3 position2 = ((!UniversalInputManager.UsePhoneUI) ? new Vector3(position.x, position.y, position.z - 2.1f) : new Vector3(position.x, position.y, position.z - 2.5f));
		string key = "GAMEPLAY_BACON_FREEZE_TUTORIAL";
		m_freezeTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key));
		m_freezeTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Up);
		m_freezeTutorialNotification.PulseReminderEveryXSeconds(2f);
	}

	protected IEnumerator ShowManaArrowWithText(string textID)
	{
		Vector3 manaCrystalSpawnPosition = ManaCrystalMgr.Get().GetManaCrystalSpawnPosition();
		Vector3 position;
		Notification.PopUpArrowDirection direction;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			position = new Vector3(manaCrystalSpawnPosition.x - 0.7f, manaCrystalSpawnPosition.y + 1.14f, manaCrystalSpawnPosition.z + 4.33f);
			direction = Notification.PopUpArrowDirection.RightDown;
		}
		else
		{
			position = new Vector3(manaCrystalSpawnPosition.x - 0.02f, manaCrystalSpawnPosition.y + 0.2f, manaCrystalSpawnPosition.z + 1.8f);
			direction = Notification.PopUpArrowDirection.Down;
		}
		m_manaNotifier = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, TutorialEntity.GetTextScale(), GameStrings.Get(textID));
		m_manaNotifier.ShowPopUpArrow(direction);
		yield return new WaitForSeconds(2.5f);
		if (m_manaNotifier != null)
		{
			iTween.PunchScale(m_manaNotifier.gameObject, iTween.Hash("amount", new Vector3(1f, 1f, 1f), "time", 1f));
			yield return new WaitForSeconds(2f);
		}
		if (m_manaNotifier != null)
		{
			NotificationManager.Get().DestroyNotification(m_manaNotifier, 0f);
		}
	}

	protected void ShowHandBounceArrow()
	{
		m_shouldShowHandBounceArrow = true;
		HideNotification(m_handBounceArrow);
		List<Card> cards = GameState.Get().GetFriendlySidePlayer().GetHandZone()
			.GetCards();
		if (cards.Count != 0)
		{
			Card card = cards[cards.Count - 1];
			Vector3 position = card.transform.position;
			Vector3 position2 = ((!UniversalInputManager.UsePhoneUI) ? new Vector3(position.x, position.y, position.z + 2f) : new Vector3(position.x - 0.08f, position.y + 0.2f, position.z + 1.2f));
			m_handBounceArrow = NotificationManager.Get().CreateBouncingArrow(UserAttentionBlocker.NONE, position2, new Vector3(0f, 0f, 0f));
			m_handBounceArrow.transform.parent = card.transform;
		}
	}

	public override void NotifyOfCardMousedOver(Entity mousedOverEntity)
	{
		if (mousedOverEntity.GetZone() == TAG_ZONE.HAND)
		{
			HideNotification(m_handBounceArrow);
		}
	}

	public override void NotifyOfCardMousedOff(Entity mousedOffEntity)
	{
		if (mousedOffEntity.GetZone() == TAG_ZONE.HAND && m_shouldShowHandBounceArrow)
		{
			Gameplay.Get().StartCoroutine(ShowArrowInSeconds(0.5f));
		}
	}

	protected IEnumerator ShowArrowInSeconds(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		List<Card> cards = GameState.Get().GetFriendlySidePlayer().GetHandZone()
			.GetCards();
		if (cards.Count != 0)
		{
			Card cardInHand = cards[0];
			while (iTween.Count(cardInHand.gameObject) > 0)
			{
				yield return null;
			}
			if (!cardInHand.IsMousedOver() && !(InputManager.Get().GetHeldCard() == cardInHand) && m_shouldShowHandBounceArrow)
			{
				ShowHandBounceArrow();
			}
		}
	}

	protected void HideHandBounceArrow()
	{
		m_shouldShowHandBounceArrow = false;
		HideNotification(m_handBounceArrow);
	}

	protected Card GetCardInFriendlyHand(string cardId)
	{
		foreach (Card card in GameState.Get().GetFriendlySidePlayer().GetHandZone()
			.GetCards())
		{
			if (card.GetEntity().GetCardId() == cardId)
			{
				return card;
			}
		}
		return null;
	}

	protected Card GetCardInFriendlyPlay(string cardId)
	{
		foreach (Card card in GameState.Get().GetFriendlySidePlayer().GetBattlefieldZone()
			.GetCards())
		{
			if (card.GetEntity().GetCardId() == cardId)
			{
				return card;
			}
		}
		return null;
	}

	protected Card GetLeftMostMinionInFriendlyPlay()
	{
		foreach (Card card in GameState.Get().GetFriendlySidePlayer().GetBattlefieldZone()
			.GetCards())
		{
			if (card.GetEntity().GetTag(GAME_TAG.ZONE_POSITION) == 1)
			{
				return card;
			}
		}
		return null;
	}

	protected Card GetCardInOpposingPlay(string cardId)
	{
		foreach (Card card in GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone()
			.GetCards())
		{
			if (card.GetEntity().GetCardId() == cardId)
			{
				return card;
			}
		}
		return null;
	}
}
