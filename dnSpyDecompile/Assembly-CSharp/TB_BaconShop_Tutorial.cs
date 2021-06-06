using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020005A5 RID: 1445
public class TB_BaconShop_Tutorial : TB_BaconShop
{
	// Token: 0x0600503F RID: 20543 RVA: 0x001A59D0 File Offset: 0x001A3BD0
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

	// Token: 0x06005040 RID: 20544 RVA: 0x0010C860 File Offset: 0x0010AA60
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	// Token: 0x06005041 RID: 20545 RVA: 0x001A59E8 File Offset: 0x001A3BE8
	public TB_BaconShop_Tutorial()
	{
		this.m_gameOptions.AddOptions(TB_BaconShop_Tutorial.s_booleanOptions, TB_BaconShop_Tutorial.s_stringOptions);
		HistoryManager.Get().DisableHistory();
		PlayerLeaderboardManager.Get().SetEnabled(true);
		PlayerLeaderboardManager.Get().SetAllowFakePlayers(true);
		EndTurnButton.Get().SetDisabled(true);
		SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnGameplaySceneLoaded));
		this.InitializeTurnTimer();
		this.m_gamePhase = 1;
		GameEntity.Coroutines.StartCoroutine(this.OnShopPhase());
	}

	// Token: 0x06005042 RID: 20546 RVA: 0x001A5A78 File Offset: 0x001A3C78
	~TB_BaconShop_Tutorial()
	{
		this.HideShopTutorials();
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnGameplaySceneLoaded));
		}
	}

	// Token: 0x06005043 RID: 20547 RVA: 0x001A5AC4 File Offset: 0x001A3CC4
	private void OnGameplaySceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (mode != SceneMgr.Mode.GAMEPLAY)
		{
			return;
		}
		SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnGameplaySceneLoaded));
		ManaCrystalMgr.Get().SetEnemyManaCounterActive(false);
		if (GameMgr.Get().IsSpectator())
		{
			this.InitializeFakeHeroLeaderboard();
		}
		this.HideShopTutorials();
		GameEntity.Coroutines.StartCoroutine(this.OnReconnect());
	}

	// Token: 0x06005044 RID: 20548 RVA: 0x001A5B24 File Offset: 0x001A3D24
	public override InputManager.ZoneTooltipSettings GetZoneTooltipSettings()
	{
		return new InputManager.ZoneTooltipSettings
		{
			EnemyDeck = new InputManager.TooltipSettings(false),
			EnemyHand = new InputManager.TooltipSettings(false),
			EnemyMana = new InputManager.TooltipSettings(false),
			FriendlyDeck = new InputManager.TooltipSettings(false),
			FriendlyMana = new InputManager.TooltipSettings(true, new InputManager.TooltipContentDelegate(base.GetFriendlyManaTooltipContent))
		};
	}

	// Token: 0x06005045 RID: 20549 RVA: 0x000052EC File Offset: 0x000034EC
	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}

	// Token: 0x06005046 RID: 20550 RVA: 0x0019E141 File Offset: 0x0019C341
	public override bool DoAlternateMulliganIntro()
	{
		if (!this.ShouldDoAlternateMulliganIntro())
		{
			return false;
		}
		GameEntity.Coroutines.StartCoroutine(base.SkipStandardMulliganWithTiming());
		return true;
	}

	// Token: 0x06005047 RID: 20551 RVA: 0x001A5B7E File Offset: 0x001A3D7E
	protected override IEnumerator OnShopPhase()
	{
		yield return base.ShowPopup("Shop");
		PlayerLeaderboardManager.Get().UpdateLayout(true);
		GameState.Get().GetOpposingSidePlayer().UpdateDisplayInfo();
		base.UpdateNameBanner();
		base.ShowTechLevelDisplay(true);
		int tag = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.RESOURCES);
		TurnStartManager.Get().NotifyOfManaCrystalFilled(tag);
		yield return new WaitForSeconds(3f);
		yield break;
	}

	// Token: 0x06005048 RID: 20552 RVA: 0x001A5B8D File Offset: 0x001A3D8D
	protected override IEnumerator OnCombatPhase()
	{
		this.HideShopTutorials();
		yield return base.ShowPopup("Combat");
		base.ShowTechLevelDisplay(false);
		GameState.Get().GetOpposingSidePlayer().UpdateDisplayInfo();
		base.UpdateNameBanner();
		yield break;
	}

	// Token: 0x06005049 RID: 20553 RVA: 0x001A5B9C File Offset: 0x001A3D9C
	protected IEnumerator OnReconnect()
	{
		if (GameState.Get().GetGameEntity().GetTag(GAME_TAG.TURN) <= 12)
		{
			this.SetInputEnableForAllButtons(false);
			this.SetInputEnableForAllCards(false);
		}
		this.HideShopTutorials();
		yield return new WaitForSeconds(3f);
		int tag = GameState.Get().GetGameEntity().GetTag(GAME_TAG.MISSION_EVENT);
		if (GameMgr.Get().IsSpectator())
		{
			if (tag == 1 || tag == 2 || tag == 5)
			{
				GameEntity.Coroutines.StartCoroutine(this.HandleMissionEventWithTiming(tag));
			}
		}
		else
		{
			GameEntity.Coroutines.StartCoroutine(this.HandleMissionEventWithTiming(tag));
		}
		yield return null;
		yield break;
	}

	// Token: 0x0600504A RID: 20554 RVA: 0x001A5BAC File Offset: 0x001A3DAC
	protected void InitializeFakeHeroLeaderboard()
	{
		foreach (Card card in GameState.Get().GetFriendlySidePlayer().GetGraveyardZone().GetCards())
		{
			Entity entity = card.GetEntity();
			if (entity.GetCardType() == TAG_CARDTYPE.HERO)
			{
				PlayerLeaderboardManager.Get().CreatePlayerTile(entity);
			}
		}
	}

	// Token: 0x0600504B RID: 20555 RVA: 0x001A5C24 File Offset: 0x001A3E24
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		switch (missionEvent)
		{
		case 1:
			this.m_gamePhase = 1;
			yield return this.OnShopPhase();
			break;
		case 2:
			this.m_gamePhase = 2;
			yield return this.OnCombatPhase();
			break;
		case 5:
			GameState.Get().GetOpposingSidePlayer().UpdateDisplayInfo();
			base.UpdateNameBanner();
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
				base.SetInputEnableForFrozenButton(false);
				yield return new WaitForSeconds(0.75f);
				base.SetInputEnableForFrozenButton(true);
			}
			break;
		case 4:
			base.SetInputEnableForRefreshButton(false);
			yield return new WaitForSeconds(0.75f);
			base.SetInputEnableForRefreshButton(true);
			break;
		case 10:
			GameState.Get().SetBusy(true);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_ModeSelect_01);
			this.InitializeFakeHeroLeaderboard();
			yield return new WaitForSeconds(0.5f);
			GameState.Get().SetBusy(false);
			PlayerLeaderboardManager.Get().UpdateLayout(true);
			break;
		case 11:
			GameState.Get().SetBusy(true);
			this.CreateTutorialDialog(TB_BaconShop_Tutorial.DRAGBUY_DIALOG_TUTORIAL_PREFAB, "GAMEPLAY_BACON_DRAGBUY_TITLE_TUTORIAL", "GAMEPLAY_BACON_DRAGBUY_BODY_TUTORIAL", "GAMEPLAY_BACON_CONFIRM_BUTTON_TUTORIAL", new UIEvent.Handler(this.UserPressedDragBuyTutorial), new Vector2(0.5f, 0.5f));
			break;
		case 12:
			yield return new WaitForSeconds(1f);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_ShopFirstTime_01);
			GameState.Get().SetBusy(false);
			break;
		case 13:
		{
			GameState.Get().SetBusy(true);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_Hire_01);
			GameState.Get().SetBusy(false);
			this.SetInputEnableForAllCards(true);
			Card cardInOpposingPlay = this.GetCardInOpposingPlay("CS2_065");
			if (cardInOpposingPlay != null)
			{
				this.ShowDragBuyTutorial(cardInOpposingPlay, "GAMEPLAY_BACON_DRAGBUY_TUTORIAL", false);
				GameEntity.Coroutines.StartCoroutine(this.ShowOrHideDragBuyTutorial("GAMEPLAY_BACON_DRAGBUY_TUTORIAL"));
			}
			break;
		}
		case 14:
			this.SetInputEnableForAllCards(false);
			this.HideNotification(this.m_dragBuyTutorialNotification, false);
			yield return this.ShowManaArrowWithText("GAMEPLAY_BACON_COIN_TUTORIAL_1");
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_RecruitWork_01);
			this.SetInputEnableForAllCards(true);
			yield return new WaitForSeconds(3f);
			this.ShowHandBounceArrow();
			break;
		case 15:
			this.HideHandBounceArrow();
			GameState.Get().SetBusy(true);
			this.HideNotification(this.m_dragBuyTutorialNotification, false);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_HeroSelection_01);
			GameState.Get().SetBusy(false);
			break;
		case 20:
			this.SetInputEnableForAllButtons(false);
			GameState.Get().SetBusy(true);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_General_02);
			yield return new WaitForSeconds(0.25f);
			this.RecruitReminderTutorial();
			GameState.Get().SetBusy(false);
			base.SetInputEnableForBuy(true);
			break;
		case 22:
			this.m_shouldShowHandBounceArrow = false;
			this.SetInputEnableForAllCards(false);
			this.HideNotification(this.m_recruitReminderTutorialNofification, false);
			GameState.Get().SetBusy(true);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_Idle_02);
			yield return this.ShowManaArrowWithText("GAMEPLAY_BACON_COIN_TUTORIAL_2");
			yield return new WaitForSeconds(0.5f);
			this.SetInputEnableForAllCards(true);
			GameState.Get().SetBusy(false);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_02);
			yield return new WaitForSeconds(2.5f);
			this.ShowHandBounceArrow();
			break;
		case 24:
			this.HideHandBounceArrow();
			this.SetInputEnableForAllCards(true);
			GameState.Get().SetBusy(true);
			this.ShowMinionMoveTutorial();
			GameEntity.Coroutines.StartCoroutine(this.ShowOrHideMoveMinionTutorial());
			GameState.Get().SetBusy(false);
			break;
		case 25:
			this.m_shouldPlayMinionMoveTutorial = false;
			this.HideNotification(this.m_minionMoveTutorialNotification, false);
			GameState.Get().SetBusy(true);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_FirstBattle_01);
			GameState.Get().SetBusy(false);
			break;
		case 30:
			this.SetInputEnableForAllButtons(false);
			this.SetInputEnableForAllCards(false);
			GameState.Get().SetBusy(true);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_ShopUpgrade_01);
			base.SetInputEnableForTavernUpgradeButton(true);
			GameState.Get().SetBusy(false);
			this.SetInputEnableForAllCards(false);
			yield return new WaitForSeconds(0.5f);
			this.ShowTavernUpgradeButtonTutorial(false);
			break;
		case 31:
			base.SetInputEnableForRefreshButton(false);
			GameState.Get().SetBusy(true);
			this.HideNotification(this.m_upgradeTavernTutorialNotification, false);
			base.SetInputEnableForBuy(false);
			yield return new WaitForSeconds(0.5f);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_03);
			GameState.Get().SetBusy(false);
			this.ShowRefreshButtonTutorial("GAMEPLAY_BACON_REFRESH_TUTORIAL", false);
			base.SetInputEnableForRefreshButton(true);
			break;
		case 32:
			base.SetInputEnableForRefreshButton(false);
			this.HideNotification(this.m_refreshButtonTutorialNotification, false);
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(0.5f);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_Hire_02);
			GameState.Get().SetBusy(false);
			base.SetInputEnableForBuy(true);
			this.SetInputEnableForAllCards(true);
			yield return new WaitForSeconds(5f);
			this.RecruitTutorialWithBoardSize(4, "GAMEPLAY_BACON_RECRUIT_REMINDER_TUTORIAL_2", false);
			GameEntity.Coroutines.StartCoroutine(this.ShowOrHideRecruitTutorialWithBoardSize(4, "GAMEPLAY_BACON_RECRUIT_REMINDER_TUTORIAL_2"));
			break;
		case 33:
			base.SetInputEnableForBuy(true);
			this.SetInputEnableForAllCards(true);
			yield return new WaitForSeconds(5f);
			this.ShowHandBounceArrow();
			break;
		case 34:
			this.HideHandBounceArrow();
			this.HideNotification(this.m_dragBuyTutorialNotification, false);
			this.HideNotification(this.m_refreshButtonTutorialNotification, false);
			base.SetInputEnableForRefreshButton(false);
			break;
		case 40:
			this.SetInputEnableForAllButtons(false);
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(1f);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_Triple_02);
			GameState.Get().SetBusy(false);
			this.SetInputEnableForAllCards(true);
			yield return new WaitForSeconds(5f);
			this.RecruitTutorialWithBoardSize(2, "GAMEPLAY_BACON_RECRUIT_REMINDER_TUTORIAL_2", false);
			GameEntity.Coroutines.StartCoroutine(this.ShowOrHideRecruitTutorialWithBoardSize(2, "GAMEPLAY_BACON_RECRUIT_REMINDER_TUTORIAL_2"));
			break;
		case 41:
			this.SetInputEnableForAllCards(false);
			this.SetInputEnableForAllButtons(false);
			GameState.Get().SetBusy(true);
			this.CreateTutorialDialog(TB_BaconShop_Tutorial.TRIPLE_DIALOG_TUTORIAL_PREFAB, "GAMEPLAY_BACON_TRIPLE_TITLE_TUTORIAL", "GAMEPLAY_BACON_TRIPLE_BODY_TUTORIAL", "GAMEPLAY_BACON_CONFIRM_BUTTON_TUTORIAL", new UIEvent.Handler(this.UserPressedTripleTutorial), new Vector2(0.5f, 0f));
			break;
		case 42:
		{
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(0.5f);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_Triple_01);
			Card cardInOpposingPlay = this.GetCardInOpposingPlay("CS2_065");
			if (cardInOpposingPlay != null)
			{
				cardInOpposingPlay.SetInputEnabled(true);
				this.ShowDragBuyTutorial(cardInOpposingPlay, "GAMEPLAY_BACON_DRAGBUY_TRIPLE_TUTORIAL", false);
				GameEntity.Coroutines.StartCoroutine(this.ShowOrHideDragBuyTutorial("GAMEPLAY_BACON_DRAGBUY_TRIPLE_TUTORIAL"));
			}
			GameState.Get().SetBusy(false);
			base.SetInputEnableForBuy(true);
			break;
		}
		case 44:
			this.HideNotification(this.m_dragBuyTutorialNotification, false);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_AfterTriple_01);
			this.SetInputEnableForAllCards(true);
			yield return new WaitForSeconds(6f);
			this.ShowHandBounceArrow();
			break;
		case 45:
			this.HideHandBounceArrow();
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_Triple_03);
			this.SetInputEnableForAllCards(true);
			yield return new WaitForSeconds(6f);
			this.ShowHandBounceArrow();
			break;
		case 46:
			this.HideHandBounceArrow();
			this.SetInputEnableForAllCards(true);
			yield return new WaitForSeconds(6f);
			this.ShowHandBounceArrow();
			break;
		case 47:
			this.HideHandBounceArrow();
			this.SetInputEnableForAllButtons(true);
			this.SetInputEnableForAllCards(true);
			GameState.Get().SetBusy(true);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_03);
			GameState.Get().SetBusy(false);
			break;
		case 51:
			this.SetInputEnableForAllButtons(false);
			this.SetInputEnableForAllCards(false);
			GameState.Get().SetBusy(true);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_UpgradeShop_01);
			yield return new WaitForSeconds(0.5f);
			GameState.Get().SetBusy(false);
			this.SetInputEnableForAllCards(false);
			base.SetInputEnableForTavernUpgradeButton(true);
			this.ShowTavernUpgradeButtonTutorial(false);
			break;
		case 52:
			GameState.Get().SetBusy(true);
			this.HideNotification(this.m_upgradeTavernTutorialNotification, false);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_04);
			yield return new WaitForSeconds(0.5f);
			GameState.Get().SetBusy(false);
			this.ShowRefreshButtonTutorial("GAMEPLAY_BACON_REFRESH_UPGRADE_TUTORIAL", false);
			base.SetInputEnableForRefreshButton(true);
			break;
		case 53:
			base.SetInputEnableForRefreshButton(false);
			this.HideNotification(this.m_refreshButtonTutorialNotification, false);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_01);
			yield return new WaitForSeconds(0.5f);
			base.SetInputEnableForFrozenButton(true);
			this.ShowFreezeTutorial(false);
			break;
		case 54:
			this.m_shouldShowHandBounceArrow = false;
			base.SetInputEnableForFrozenButton(false);
			this.HideNotification(this.m_freezeTutorialNotification, false);
			GameState.Get().SetBusy(true);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_AfterFreezing_01);
			GameState.Get().SetBusy(false);
			this.SetInputEnableForAllCards(true);
			break;
		case 60:
			this.SetInputEnableForAllButtons(false);
			base.SetInputEnableForBuy(true);
			GameState.Get().SetBusy(true);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_AfterTriple_02);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_Hire_01);
			GameState.Get().SetBusy(false);
			yield return new WaitForSeconds(2f);
			this.RecruitTutorialWithBoardSize(4, "GAMEPLAY_BACON_RECRUIT_REMINDER_TUTORIAL_2", false);
			GameEntity.Coroutines.StartCoroutine(this.ShowOrHideRecruitTutorialWithBoardSize(4, "GAMEPLAY_BACON_RECRUIT_REMINDER_TUTORIAL_2"));
			break;
		case 61:
			this.SetInputEnableForFriendlyHandCards(false);
			base.SetInputEnableForBuy(true);
			yield return new WaitForSeconds(6f);
			this.RecruitTutorialWithBoardSize(3, "GAMEPLAY_BACON_RECRUIT_REMINDER_TUTORIAL_2", false);
			GameEntity.Coroutines.StartCoroutine(this.ShowOrHideRecruitTutorialWithBoardSize(3, "GAMEPLAY_BACON_RECRUIT_REMINDER_TUTORIAL_2"));
			break;
		case 62:
			this.SetInputEnableForAllButtons(false);
			this.SetInputEnableForFriendlyHandCards(false);
			GameState.Get().SetBusy(true);
			this.CreateTutorialDialog(TB_BaconShop_Tutorial.DRAGSELL_DIALOG_TUTORIAL_PREFAB, "GAMEPLAY_BACON_DRAGSELL_TITLE_TUTORIAL", "GAMEPLAY_BACON_DRAGSELL_BODY_TUTORIAL", "GAMEPLAY_BACON_CONFIRM_BUTTON_TUTORIAL", new UIEvent.Handler(this.UserPressedDragSellTutorial), new Vector2(0f, 0.5f));
			break;
		case 63:
			GameState.Get().SetBusy(false);
			this.SetInputEnableForAllCards(true);
			this.SetInputEnableForFriendlyHandCards(false);
			this.ShowDragSellTutorial(false);
			break;
		case 64:
			base.SetInputEnableForBuy(true);
			GameState.Get().SetBusy(true);
			this.HideNotification(this.m_dragSellTutorialNotification, false);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_AfterSelling_01);
			GameState.Get().SetBusy(false);
			yield return new WaitForSeconds(6f);
			this.RecruitTutorialWithBoardSize(2, "GAMEPLAY_BACON_RECRUIT_REMINDER_TUTORIAL_2", false);
			GameEntity.Coroutines.StartCoroutine(this.ShowOrHideRecruitTutorialWithBoardSize(2, "GAMEPLAY_BACON_RECRUIT_REMINDER_TUTORIAL_2"));
			break;
		case 65:
			this.HideNotification(this.m_handBounceArrow, false);
			this.SetInputEnableForFriendlyHandCards(true);
			yield return new WaitForSeconds(6f);
			this.ShowHandBounceArrow();
			break;
		case 66:
			this.HideHandBounceArrow();
			this.SetInputEnableForFriendlyHandCards(true);
			yield return new WaitForSeconds(6f);
			this.ShowHandBounceArrow();
			break;
		case 67:
			this.HideHandBounceArrow();
			this.SetInputEnableForFriendlyHandCards(true);
			yield return new WaitForSeconds(6f);
			this.ShowHandBounceArrow();
			break;
		case 68:
			this.HideHandBounceArrow();
			this.SetInputEnableForAllButtons(true);
			this.SetInputEnableForAllCards(true);
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_AfterTriple_03);
			break;
		case 70:
			this.HideHandBounceArrow();
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_CombatWin_03);
			break;
		case 79:
			this.HideHandBounceArrow();
			GameState.Get().SetBusy(true);
			this.CreateTutorialDialog(TB_BaconShop_Tutorial.COMBAT_DIALOG_TUTORIAL_PREFAB, "GAMEPLAY_BACON_COMBAT_TITLE_TUTORIAL", "GAMEPLAY_BACON_COMBAT_BODY_TUTORIAL", "GAMEPLAY_BACON_CONFIRM_BUTTON_TUTORIAL", new UIEvent.Handler(this.UserPressedCombatTutorial), Vector2.zero);
			break;
		case 80:
			this.HideHandBounceArrow();
			yield return this.PlayBobLine(TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_Idle_03);
			break;
		}
		yield break;
	}

	// Token: 0x0600504C RID: 20556 RVA: 0x001A5C3C File Offset: 0x001A3E3C
	public override void OnPlayThinkEmote()
	{
		if (!base.HasSeenAllTutorial())
		{
			return;
		}
		if (this.m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide())
		{
			return;
		}
		currentPlayer.GetHeroCard().HasActiveEmoteSound();
	}

	// Token: 0x0600504D RID: 20557 RVA: 0x001A5C7B File Offset: 0x001A3E7B
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		this.HideShopTutorials();
		PlayerLeaderboardManager.Get().UpdateLayout(true);
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			if (GameState.Get().GetBooleanGameOption(GameEntityOption.WAIT_FOR_RATING_INFO))
			{
				yield return new WaitForSeconds(5f);
			}
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait(TB_BaconShop_Tutorial.Bob_BrassRing_Quote, TB_BaconShop_Tutorial.VO_DALA_BOSS_99h_Male_Human_FirstVictory_01, 3f, 1f, true, false));
		}
		yield break;
	}

	// Token: 0x0600504E RID: 20558 RVA: 0x001A5C94 File Offset: 0x001A3E94
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		this.HideShopTutorials();
		GameEntity.Coroutines.StartCoroutine(this.HandleGameOverWithTiming(gameResult));
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
			SceneMgr.Get().SetNextMode(postGameSceneMode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		}
	}

	// Token: 0x0600504F RID: 20559 RVA: 0x001A5D00 File Offset: 0x001A3F00
	protected IEnumerator PlayBobLine(string voLine)
	{
		Actor bobActor = base.GetBobActor();
		if (bobActor != null && bobActor.GetEntity() != null)
		{
			string legacyAssetName = new AssetReference(voLine).GetLegacyAssetName();
			yield return base.PlaySoundAndWait(voLine, legacyAssetName, Notification.SpeechBubbleDirection.TopRight, bobActor, 1f, true, false, 3f, 0f);
		}
		yield break;
	}

	// Token: 0x06005050 RID: 20560 RVA: 0x001A5D18 File Offset: 0x001A3F18
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
		return GameState.Get().GetOpposingSidePlayer().GetHero().GetName();
	}

	// Token: 0x06005051 RID: 20561 RVA: 0x001A5D6C File Offset: 0x001A3F6C
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

	// Token: 0x06005052 RID: 20562 RVA: 0x001A5DBE File Offset: 0x001A3FBE
	protected void UserPressedDragBuyTutorial(UIEvent e)
	{
		base.HandleMissionEvent(12);
	}

	// Token: 0x06005053 RID: 20563 RVA: 0x001A5DC8 File Offset: 0x001A3FC8
	protected void UserPressedTripleTutorial(UIEvent e)
	{
		base.HandleMissionEvent(42);
	}

	// Token: 0x06005054 RID: 20564 RVA: 0x001A5DD2 File Offset: 0x001A3FD2
	protected void UserPressedDragSellTutorial(UIEvent e)
	{
		base.HandleMissionEvent(63);
	}

	// Token: 0x06005055 RID: 20565 RVA: 0x001A5DDC File Offset: 0x001A3FDC
	protected void UserPressedCombatTutorial(UIEvent e)
	{
		GameState.Get().SetBusy(false);
	}

	// Token: 0x06005056 RID: 20566 RVA: 0x001A5DE9 File Offset: 0x001A3FE9
	protected override void HideShopTutorials()
	{
		this.HideHandBounceArrow();
		NotificationManager.Get().DestroyAllPopUps();
	}

	// Token: 0x06005057 RID: 20567 RVA: 0x001A5DFB File Offset: 0x001A3FFB
	protected void SetInputEnableForAllButtons(bool isEnabled)
	{
		base.SetInputEnableForBuy(isEnabled);
		base.SetInputEnableForRefreshButton(isEnabled);
		base.SetInputEnableForTavernUpgradeButton(isEnabled);
		base.SetInputEnableForFrozenButton(isEnabled);
	}

	// Token: 0x06005058 RID: 20568 RVA: 0x001A5E1C File Offset: 0x001A401C
	protected void SetInputEnableForAllCards(bool isEnabled)
	{
		IEnumerable<Card> cards = GameState.Get().GetFriendlySidePlayer().GetBattlefieldZone().GetCards();
		List<Card> cards2 = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone().GetCards();
		List<Card> cards3 = GameState.Get().GetFriendlySidePlayer().GetHandZone().GetCards();
		foreach (Card card in cards.Concat(cards2).Concat(cards3).ToList<Card>())
		{
			card.SetInputEnabled(isEnabled);
		}
	}

	// Token: 0x06005059 RID: 20569 RVA: 0x001A5EB8 File Offset: 0x001A40B8
	protected void SetInputEnableForFriendlyHandCards(bool isEnabled)
	{
		foreach (Card card in GameState.Get().GetFriendlySidePlayer().GetHandZone().GetCards())
		{
			card.SetInputEnabled(isEnabled);
		}
	}

	// Token: 0x0600505A RID: 20570 RVA: 0x001A5F18 File Offset: 0x001A4118
	public TutorialNotification CreateTutorialDialog(AssetReference assetPrefab, string headlineGameString, string bodyTextGameString, string buttonGameString, UIEvent.Handler buttonHandler, Vector2 materialOffset)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(assetPrefab, AssetLoadingOptions.None);
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
		if (UniversalInputManager.UsePhoneUI)
		{
			gameObject.transform.localScale = 1.5f * gameObject.transform.localScale;
		}
		this.m_popupTutorialNotification = notification;
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
		this.m_popupTutorialNotification.PlayBirth();
		UniversalInputManager.Get().SetGameDialogActive(true);
		return notification;
	}

	// Token: 0x0600505B RID: 20571 RVA: 0x001784A8 File Offset: 0x001766A8
	protected void HideNotification(Notification notification, bool hideImmediately = false)
	{
		if (notification != null)
		{
			if (hideImmediately)
			{
				NotificationManager.Get().DestroyNotificationNowWithNoAnim(notification);
				return;
			}
			NotificationManager.Get().DestroyNotification(notification, 0f);
		}
	}

	// Token: 0x0600505C RID: 20572 RVA: 0x001A6064 File Offset: 0x001A4264
	protected void ShowDragBuyTutorial(Card card, string textID = "GAMEPLAY_BACON_PLAY_MINION_TUTORIAL", bool hideImmediately = false)
	{
		if (card == null)
		{
			return;
		}
		Vector3 position = card.transform.position;
		Vector3 position2;
		Notification.PopUpArrowDirection direction;
		if (UniversalInputManager.UsePhoneUI)
		{
			position2 = new Vector3(position.x + 0.05f, position.y, position.z + 2.9f);
			direction = Notification.PopUpArrowDirection.Down;
		}
		else
		{
			position2 = new Vector3(position.x, position.y, position.z + 2.5f);
			direction = Notification.PopUpArrowDirection.Down;
		}
		this.m_dragBuyTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(textID), true, NotificationManager.PopupTextType.BASIC);
		this.m_dragBuyTutorialNotification.ShowPopUpArrow(direction);
		this.m_dragBuyTutorialNotification.PulseReminderEveryXSeconds(2f);
	}

	// Token: 0x0600505D RID: 20573 RVA: 0x001A6118 File Offset: 0x001A4318
	private IEnumerator ShowOrHideDragBuyTutorial(string textString)
	{
		while (!InputManager.Get().GetHeldCard())
		{
			yield return null;
		}
		this.HideNotification(this.m_dragBuyTutorialNotification, false);
		while (InputManager.Get().GetHeldCard())
		{
			yield return null;
		}
		yield return new WaitForSeconds(2f);
		Card cardInOpposingPlay = this.GetCardInOpposingPlay("CS2_065");
		if (cardInOpposingPlay != null || InputManager.Get().GetHeldCard())
		{
			this.ShowDragBuyTutorial(cardInOpposingPlay, textString, false);
			GameEntity.Coroutines.StartCoroutine(this.ShowOrHideDragBuyTutorial(textString));
		}
		this.HideNotification(this.m_dragBuyTutorialNotification, false);
		yield break;
	}

	// Token: 0x0600505E RID: 20574 RVA: 0x001A6130 File Offset: 0x001A4330
	protected void RecruitTutorialWithBoardSize(int enemyBoardSize, string textID = "GAMEPLAY_BACON_RECRUIT_REMINDER_TUTORIAL_2", bool hideImmediately = false)
	{
		List<Card> cards = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone().GetCards();
		if (cards.Count != enemyBoardSize)
		{
			return;
		}
		Vector3 position = cards[0].transform.position;
		Vector3 position2;
		Notification.PopUpArrowDirection direction;
		if (UniversalInputManager.UsePhoneUI)
		{
			position2 = new Vector3(position.x + 0.05f, position.y, position.z + 2.9f);
			direction = Notification.PopUpArrowDirection.Down;
		}
		else
		{
			position2 = new Vector3(position.x, position.y, position.z + 2.5f);
			direction = Notification.PopUpArrowDirection.Down;
		}
		this.m_dragBuyTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(textID), true, NotificationManager.PopupTextType.BASIC);
		this.m_dragBuyTutorialNotification.ShowPopUpArrow(direction);
		this.m_dragBuyTutorialNotification.PulseReminderEveryXSeconds(2f);
	}

	// Token: 0x0600505F RID: 20575 RVA: 0x001A61FF File Offset: 0x001A43FF
	private IEnumerator ShowOrHideRecruitTutorialWithBoardSize(int enemyBoardSize, string textID = "GAMEPLAY_BACON_RECRUIT_REMINDER_TUTORIAL_2")
	{
		while (!InputManager.Get().GetHeldCard())
		{
			yield return null;
		}
		this.HideNotification(this.m_dragBuyTutorialNotification, false);
		while (InputManager.Get().GetHeldCard())
		{
			yield return null;
		}
		yield return new WaitForSeconds(2f);
		List<Card> cards = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone().GetCards();
		if (cards.Count == enemyBoardSize && (cards[0] != null || InputManager.Get().GetHeldCard()))
		{
			this.RecruitTutorialWithBoardSize(enemyBoardSize, textID, false);
			GameEntity.Coroutines.StartCoroutine(this.ShowOrHideRecruitTutorialWithBoardSize(enemyBoardSize, textID));
		}
		yield break;
	}

	// Token: 0x06005060 RID: 20576 RVA: 0x001A621C File Offset: 0x001A441C
	protected void RecruitReminderTutorial()
	{
		if (GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone().GetCards().Count < 3)
		{
			return;
		}
		Vector3 vector = GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone().transform.position;
		vector += new Vector3(0f, 0f, 2.25f);
		string key = "GAMEPLAY_BACON_RECRUIT_REMINDER_TUTORIAL";
		this.m_recruitReminderTutorialNofification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, vector, TutorialEntity.GetTextScale(), GameStrings.Get(key), true, NotificationManager.PopupTextType.BASIC);
		this.m_recruitReminderTutorialNofification.ShowPopUpArrow(Notification.PopUpArrowDirection.BottomThree);
		this.m_recruitReminderTutorialNofification.PulseReminderEveryXSeconds(2f);
	}

	// Token: 0x06005061 RID: 20577 RVA: 0x001A62BC File Offset: 0x001A44BC
	protected void ShowRefreshButtonTutorial(string textID = "GAMEPLAY_BACON_REFRESH_TUTORIAL", bool hideImmediately = false)
	{
		List<Zone> list = ZoneMgr.Get().FindZonesForSide(Player.Side.FRIENDLY);
		Zone zone = null;
		foreach (Zone zone2 in list)
		{
			if (zone2 is ZoneGameModeButton && ((ZoneGameModeButton)zone2).m_ButtonSlot == 2)
			{
				zone = zone2;
			}
		}
		Vector3 position = zone.transform.position;
		Vector3 position2;
		if (UniversalInputManager.UsePhoneUI)
		{
			position2 = new Vector3(position.x, position.y, position.z - 2.5f);
		}
		else
		{
			position2 = new Vector3(position.x, position.y, position.z - 2.25f);
		}
		this.m_refreshButtonTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(textID), true, NotificationManager.PopupTextType.BASIC);
		this.m_refreshButtonTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Up);
		this.m_refreshButtonTutorialNotification.PulseReminderEveryXSeconds(2f);
	}

	// Token: 0x06005062 RID: 20578 RVA: 0x001A63C0 File Offset: 0x001A45C0
	protected void ShowTavernUpgradeButtonTutorial(bool hideImmediately = false)
	{
		List<Zone> list = ZoneMgr.Get().FindZonesForSide(Player.Side.FRIENDLY);
		Zone zone = null;
		foreach (Zone zone2 in list)
		{
			if (zone2 is ZoneGameModeButton && ((ZoneGameModeButton)zone2).m_ButtonSlot == 3)
			{
				zone = zone2;
			}
		}
		Vector3 position = zone.transform.position;
		Vector3 position2 = new Vector3(position.x, position.y, position.z - 2.25f);
		string key = "GAMEPLAY_BACON_MINION_UPGRADE_TAVERN_TUTORIAL";
		this.m_upgradeTavernTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key), true, NotificationManager.PopupTextType.BASIC);
		this.m_upgradeTavernTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Up);
		this.m_upgradeTavernTutorialNotification.PulseReminderEveryXSeconds(2f);
	}

	// Token: 0x06005063 RID: 20579 RVA: 0x001A649C File Offset: 0x001A469C
	protected void ShowMinionMoveTutorial()
	{
		Card leftMostMinionInFriendlyPlay = this.GetLeftMostMinionInFriendlyPlay();
		if (leftMostMinionInFriendlyPlay == null)
		{
			return;
		}
		Vector3 position = leftMostMinionInFriendlyPlay.transform.position;
		Vector3 position2;
		if (UniversalInputManager.UsePhoneUI)
		{
			position2 = new Vector3(position.x + 0.05f, position.y, position.z + 2.6f);
		}
		else
		{
			position2 = new Vector3(position.x, position.y, position.z + 2.5f);
		}
		string key = "GAMEPLAY_BACON_MINION_MOVE_TUTORIAL";
		this.m_minionMoveTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key), true, NotificationManager.PopupTextType.BASIC);
		this.m_minionMoveTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
		this.m_minionMoveTutorialNotification.PulseReminderEveryXSeconds(2f);
	}

	// Token: 0x06005064 RID: 20580 RVA: 0x001A6559 File Offset: 0x001A4759
	private IEnumerator ShowOrHideMoveMinionTutorial()
	{
		while (!InputManager.Get().GetHeldCard())
		{
			yield return null;
		}
		this.HideNotification(this.m_minionMoveTutorialNotification, false);
		while (InputManager.Get().GetHeldCard())
		{
			yield return null;
		}
		yield return new WaitForSeconds(2f);
		if ((this.GetLeftMostMinionInFriendlyPlay() != null || InputManager.Get().GetHeldCard()) && this.m_shouldPlayMinionMoveTutorial)
		{
			this.ShowMinionMoveTutorial();
			GameEntity.Coroutines.StartCoroutine(this.ShowOrHideMoveMinionTutorial());
		}
		yield break;
	}

	// Token: 0x06005065 RID: 20581 RVA: 0x001A6568 File Offset: 0x001A4768
	protected void ShowDragSellTutorial(bool hideImmediately = false)
	{
		Card card = base.GetBobActor().GetCard();
		if (card == null)
		{
			return;
		}
		card.GetActor().SetActorState(ActorStateType.CARD_SELECTABLE);
		Vector3 position = card.transform.position;
		Vector3 position2;
		if (UniversalInputManager.UsePhoneUI)
		{
			position2 = new Vector3(position.x - 3.3f, position.y, position.z - 0f);
		}
		else
		{
			position2 = new Vector3(position.x - 3.2f, position.y, position.z);
		}
		string key = "GAMEPLAY_BACON_DRAGSELL_TUTORIAL";
		this.m_dragSellTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key), true, NotificationManager.PopupTextType.BASIC);
		this.m_dragSellTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
		this.m_dragSellTutorialNotification.PulseReminderEveryXSeconds(2f);
	}

	// Token: 0x06005066 RID: 20582 RVA: 0x001A6638 File Offset: 0x001A4838
	protected void ShowFreezeTutorial(bool hideImmediately = false)
	{
		List<Zone> list = ZoneMgr.Get().FindZonesForSide(Player.Side.FRIENDLY);
		Zone zone = null;
		foreach (Zone zone2 in list)
		{
			if (zone2 is ZoneGameModeButton && ((ZoneGameModeButton)zone2).m_ButtonSlot == 1)
			{
				zone = zone2;
			}
		}
		Vector3 position = zone.transform.position;
		Vector3 position2;
		if (UniversalInputManager.UsePhoneUI)
		{
			position2 = new Vector3(position.x, position.y, position.z - 2.5f);
		}
		else
		{
			position2 = new Vector3(position.x, position.y, position.z - 2.1f);
		}
		string key = "GAMEPLAY_BACON_FREEZE_TUTORIAL";
		this.m_freezeTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key), true, NotificationManager.PopupTextType.BASIC);
		this.m_freezeTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Up);
		this.m_freezeTutorialNotification.PulseReminderEveryXSeconds(2f);
	}

	// Token: 0x06005067 RID: 20583 RVA: 0x001A6744 File Offset: 0x001A4944
	protected IEnumerator ShowManaArrowWithText(string textID)
	{
		Vector3 manaCrystalSpawnPosition = ManaCrystalMgr.Get().GetManaCrystalSpawnPosition();
		Vector3 position;
		Notification.PopUpArrowDirection direction;
		if (UniversalInputManager.UsePhoneUI)
		{
			position = new Vector3(manaCrystalSpawnPosition.x - 0.7f, manaCrystalSpawnPosition.y + 1.14f, manaCrystalSpawnPosition.z + 4.33f);
			direction = Notification.PopUpArrowDirection.RightDown;
		}
		else
		{
			position = new Vector3(manaCrystalSpawnPosition.x - 0.02f, manaCrystalSpawnPosition.y + 0.2f, manaCrystalSpawnPosition.z + 1.8f);
			direction = Notification.PopUpArrowDirection.Down;
		}
		this.m_manaNotifier = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position, TutorialEntity.GetTextScale(), GameStrings.Get(textID), true, NotificationManager.PopupTextType.BASIC);
		this.m_manaNotifier.ShowPopUpArrow(direction);
		yield return new WaitForSeconds(2.5f);
		if (this.m_manaNotifier != null)
		{
			iTween.PunchScale(this.m_manaNotifier.gameObject, iTween.Hash(new object[]
			{
				"amount",
				new Vector3(1f, 1f, 1f),
				"time",
				1f
			}));
			yield return new WaitForSeconds(2f);
		}
		if (this.m_manaNotifier != null)
		{
			NotificationManager.Get().DestroyNotification(this.m_manaNotifier, 0f);
		}
		yield break;
	}

	// Token: 0x06005068 RID: 20584 RVA: 0x001A675C File Offset: 0x001A495C
	protected void ShowHandBounceArrow()
	{
		this.m_shouldShowHandBounceArrow = true;
		this.HideNotification(this.m_handBounceArrow, false);
		List<Card> cards = GameState.Get().GetFriendlySidePlayer().GetHandZone().GetCards();
		if (cards.Count == 0)
		{
			return;
		}
		Card card = cards[cards.Count - 1];
		Vector3 position = card.transform.position;
		Vector3 position2;
		if (UniversalInputManager.UsePhoneUI)
		{
			position2 = new Vector3(position.x - 0.08f, position.y + 0.2f, position.z + 1.2f);
		}
		else
		{
			position2 = new Vector3(position.x, position.y, position.z + 2f);
		}
		this.m_handBounceArrow = NotificationManager.Get().CreateBouncingArrow(UserAttentionBlocker.NONE, position2, new Vector3(0f, 0f, 0f));
		this.m_handBounceArrow.transform.parent = card.transform;
	}

	// Token: 0x06005069 RID: 20585 RVA: 0x001A684A File Offset: 0x001A4A4A
	public override void NotifyOfCardMousedOver(Entity mousedOverEntity)
	{
		if (mousedOverEntity.GetZone() == TAG_ZONE.HAND)
		{
			this.HideNotification(this.m_handBounceArrow, false);
		}
	}

	// Token: 0x0600506A RID: 20586 RVA: 0x001A6862 File Offset: 0x001A4A62
	public override void NotifyOfCardMousedOff(Entity mousedOffEntity)
	{
		if (mousedOffEntity.GetZone() == TAG_ZONE.HAND && this.m_shouldShowHandBounceArrow)
		{
			Gameplay.Get().StartCoroutine(this.ShowArrowInSeconds(0.5f));
		}
	}

	// Token: 0x0600506B RID: 20587 RVA: 0x001A688B File Offset: 0x001A4A8B
	protected IEnumerator ShowArrowInSeconds(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		List<Card> cards = GameState.Get().GetFriendlySidePlayer().GetHandZone().GetCards();
		if (cards.Count == 0)
		{
			yield break;
		}
		Card cardInHand = cards[0];
		while (iTween.Count(cardInHand.gameObject) > 0)
		{
			yield return null;
		}
		if (cardInHand.IsMousedOver())
		{
			yield break;
		}
		if (InputManager.Get().GetHeldCard() == cardInHand)
		{
			yield break;
		}
		if (!this.m_shouldShowHandBounceArrow)
		{
			yield break;
		}
		this.ShowHandBounceArrow();
		yield break;
	}

	// Token: 0x0600506C RID: 20588 RVA: 0x001A68A1 File Offset: 0x001A4AA1
	protected void HideHandBounceArrow()
	{
		this.m_shouldShowHandBounceArrow = false;
		this.HideNotification(this.m_handBounceArrow, false);
	}

	// Token: 0x0600506D RID: 20589 RVA: 0x001A68B8 File Offset: 0x001A4AB8
	protected Card GetCardInFriendlyHand(string cardId)
	{
		foreach (Card card in GameState.Get().GetFriendlySidePlayer().GetHandZone().GetCards())
		{
			if (card.GetEntity().GetCardId() == cardId)
			{
				return card;
			}
		}
		return null;
	}

	// Token: 0x0600506E RID: 20590 RVA: 0x001A692C File Offset: 0x001A4B2C
	protected Card GetCardInFriendlyPlay(string cardId)
	{
		foreach (Card card in GameState.Get().GetFriendlySidePlayer().GetBattlefieldZone().GetCards())
		{
			if (card.GetEntity().GetCardId() == cardId)
			{
				return card;
			}
		}
		return null;
	}

	// Token: 0x0600506F RID: 20591 RVA: 0x001A69A0 File Offset: 0x001A4BA0
	protected Card GetLeftMostMinionInFriendlyPlay()
	{
		foreach (Card card in GameState.Get().GetFriendlySidePlayer().GetBattlefieldZone().GetCards())
		{
			if (card.GetEntity().GetTag(GAME_TAG.ZONE_POSITION) == 1)
			{
				return card;
			}
		}
		return null;
	}

	// Token: 0x06005070 RID: 20592 RVA: 0x001A6A14 File Offset: 0x001A4C14
	protected Card GetCardInOpposingPlay(string cardId)
	{
		foreach (Card card in GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone().GetCards())
		{
			if (card.GetEntity().GetCardId() == cardId)
			{
				return card;
			}
		}
		return null;
	}

	// Token: 0x04004672 RID: 18034
	private static Map<GameEntityOption, bool> s_booleanOptions = TB_BaconShop_Tutorial.InitBooleanOptions();

	// Token: 0x04004673 RID: 18035
	private static Map<GameEntityOption, string> s_stringOptions = TB_BaconShop_Tutorial.InitStringOptions();

	// Token: 0x04004674 RID: 18036
	private static readonly AssetReference Bob_BrassRing_Quote = new AssetReference("Bob_BrassRing_Quote.prefab:89385ff7d67aa1e49bcf25bc15ca61f6");

	// Token: 0x04004675 RID: 18037
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterFreezing_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterFreezing_01.prefab:4dc4f16c60d79ed40be28f898346df02");

	// Token: 0x04004676 RID: 18038
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterSelling_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterSelling_01.prefab:d71f34687d09a064bab5d202ea3fb965");

	// Token: 0x04004677 RID: 18039
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_01.prefab:d3ad51eb14e20324387e5dbbd1e82811");

	// Token: 0x04004678 RID: 18040
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_03.prefab:03260a54e677e4247aa19eb29662371e");

	// Token: 0x04004679 RID: 18041
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_04 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterShopUpgrade_04.prefab:ecf9f68fe25195a4a93b50d0c8e82a1a");

	// Token: 0x0400467A RID: 18042
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterTriple_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterTriple_01.prefab:dc92cab5423afa045b4ad528dd25f9d5");

	// Token: 0x0400467B RID: 18043
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterTriple_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterTriple_02.prefab:d845164292fb45a4f85eed478ad5d1c2");

	// Token: 0x0400467C RID: 18044
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_AfterTriple_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_AfterTriple_03.prefab:4704869d519d6e7479602dd15e12b175");

	// Token: 0x0400467D RID: 18045
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_CombatWin_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_CombatWin_03.prefab:8ff8566f08747ad4bb76409e6db1504b");

	// Token: 0x0400467E RID: 18046
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_FirstBattle_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_FirstBattle_01.prefab:b88923936df527147b6eda2517ce91ef");

	// Token: 0x0400467F RID: 18047
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_FirstVictory_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_FirstVictory_01.prefab:e40b154f86185d3428ffa48867241f76");

	// Token: 0x04004680 RID: 18048
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_General_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_General_02.prefab:d5908d1fd355b8c4b8344e300dc4fc42");

	// Token: 0x04004681 RID: 18049
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_HeroSelection_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_HeroSelection_01.prefab:93cd3efc86126de478be0e56c8e275a7");

	// Token: 0x04004682 RID: 18050
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Hire_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Hire_01.prefab:bfd9513b46b92e84da5f22e01a0387a4");

	// Token: 0x04004683 RID: 18051
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Hire_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Hire_02.prefab:eb20d844bee8bdf4f9cbb514c8ab8580");

	// Token: 0x04004684 RID: 18052
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Idle_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Idle_02.prefab:3808bb035b74ac04f9bb4be91009e2b7");

	// Token: 0x04004685 RID: 18053
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Idle_03.prefab:34248aac29c16274c95fb999635368ff");

	// Token: 0x04004686 RID: 18054
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_ModeSelect_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_ModeSelect_01.prefab:261a9714c4cf3ad4d8944d9127a38ddf");

	// Token: 0x04004687 RID: 18055
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitMediumMinion_03.prefab:e3cbf2a35ac2e8245b5bb3de3baa054e");

	// Token: 0x04004688 RID: 18056
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitSmallMinion_02.prefab:eb000d8de28cd6d478b9a718ebe1fd9e");

	// Token: 0x04004689 RID: 18057
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_RecruitWork_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_RecruitWork_01.prefab:a5e1a6db102be6d4495aa1cd7dc7ddfc");

	// Token: 0x0400468A RID: 18058
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_ShopFirstTime_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_ShopFirstTime_01.prefab:8070938a2c3ba2f4ea92b7f0b5fdf280");

	// Token: 0x0400468B RID: 18059
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_ShopUpgrade_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_ShopUpgrade_01.prefab:f5019f07757dde341aae503b53a9102e");

	// Token: 0x0400468C RID: 18060
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Triple_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Triple_01.prefab:26a5500e887280c40a810c01741e2544");

	// Token: 0x0400468D RID: 18061
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Triple_02 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Triple_02.prefab:1aff064425948044791b8b9e3f8de61b");

	// Token: 0x0400468E RID: 18062
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_Triple_03 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_Triple_03.prefab:e14f16322b47b814d8ccb07a60ccf6d1");

	// Token: 0x0400468F RID: 18063
	private static readonly AssetReference VO_DALA_BOSS_99h_Male_Human_UpgradeShop_01 = new AssetReference("VO_DALA_BOSS_99h_Male_Human_UpgradeShop_01.prefab:ec1459e08d9b5a04e97c6a3499505cf6");

	// Token: 0x04004690 RID: 18064
	protected const string TUTORIAL_MINION_ID = "CS2_065";

	// Token: 0x04004691 RID: 18065
	private const GameSaveKeyId GAME_SAVE_PARENT_KEY = GameSaveKeyId.BACON;

	// Token: 0x04004692 RID: 18066
	private const int MISSION_EVENT_UPDATE_NAME_BANNER = 5;

	// Token: 0x04004693 RID: 18067
	protected Notification m_dragBuyTutorialNotification;

	// Token: 0x04004694 RID: 18068
	protected Notification m_recruitReminderTutorialNofification;

	// Token: 0x04004695 RID: 18069
	protected Notification m_refreshButtonTutorialNotification;

	// Token: 0x04004696 RID: 18070
	protected Notification m_minionMoveTutorialNotification;

	// Token: 0x04004697 RID: 18071
	protected Notification m_upgradeTavernTutorialNotification;

	// Token: 0x04004698 RID: 18072
	protected Notification m_dragSellTutorialNotification;

	// Token: 0x04004699 RID: 18073
	protected Notification m_freezeTutorialNotification;

	// Token: 0x0400469A RID: 18074
	protected Notification m_popupTutorialNotification;

	// Token: 0x0400469B RID: 18075
	protected Notification m_manaNotifier;

	// Token: 0x0400469C RID: 18076
	protected Notification m_handBounceArrow;

	// Token: 0x0400469D RID: 18077
	protected bool m_shouldPlayMinionMoveTutorial = true;

	// Token: 0x0400469E RID: 18078
	protected bool m_shouldShowHandBounceArrow;

	// Token: 0x0400469F RID: 18079
	private static readonly AssetReference BaconTutorialPopup = new AssetReference("BaconTutorialPopup.prefab:b68a7306f3300874a833909005fa797d");

	// Token: 0x040046A0 RID: 18080
	private static readonly AssetReference DRAGBUY_DIALOG_TUTORIAL_PREFAB = TB_BaconShop_Tutorial.BaconTutorialPopup;

	// Token: 0x040046A1 RID: 18081
	protected const string DRAGBUY_TITLE_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_DRAGBUY_TITLE_TUTORIAL";

	// Token: 0x040046A2 RID: 18082
	protected const string DRAGBUY_BODY_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_DRAGBUY_BODY_TUTORIAL";

	// Token: 0x040046A3 RID: 18083
	protected const string DRAGBUY_BUTTON_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_CONFIRM_BUTTON_TUTORIAL";

	// Token: 0x040046A4 RID: 18084
	private static readonly AssetReference DRAGSELL_DIALOG_TUTORIAL_PREFAB = TB_BaconShop_Tutorial.BaconTutorialPopup;

	// Token: 0x040046A5 RID: 18085
	protected const string DRAGSELL_TITLE_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_DRAGSELL_TITLE_TUTORIAL";

	// Token: 0x040046A6 RID: 18086
	protected const string DRAGSELL_BODY_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_DRAGSELL_BODY_TUTORIAL";

	// Token: 0x040046A7 RID: 18087
	protected const string DRAGSELL_BUTTON_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_CONFIRM_BUTTON_TUTORIAL";

	// Token: 0x040046A8 RID: 18088
	private static readonly AssetReference TRIPLE_DIALOG_TUTORIAL_PREFAB = TB_BaconShop_Tutorial.BaconTutorialPopup;

	// Token: 0x040046A9 RID: 18089
	protected const string TRIPLE_TITLE_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_TRIPLE_TITLE_TUTORIAL";

	// Token: 0x040046AA RID: 18090
	protected const string TRIPLE_BODY_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_TRIPLE_BODY_TUTORIAL";

	// Token: 0x040046AB RID: 18091
	protected const string TRIPLE_BUTTON_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_CONFIRM_BUTTON_TUTORIAL";

	// Token: 0x040046AC RID: 18092
	private static readonly AssetReference COMBAT_DIALOG_TUTORIAL_PREFAB = TB_BaconShop_Tutorial.BaconTutorialPopup;

	// Token: 0x040046AD RID: 18093
	protected const string COMBAT_TITLE_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_COMBAT_TITLE_TUTORIAL";

	// Token: 0x040046AE RID: 18094
	protected const string COMBAT_BODY_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_COMBAT_BODY_TUTORIAL";

	// Token: 0x040046AF RID: 18095
	protected const string COMBAT_BUTTON_TUTORIAL_GAMESTRING = "GAMEPLAY_BACON_CONFIRM_BUTTON_TUTORIAL";
}
