using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.UI;
using HutongGames.PlayMaker;
using PegasusShared;
using UnityEngine;

// Token: 0x02000650 RID: 1616
public class SeasonEndDialog : DialogBase
{
	// Token: 0x06005B30 RID: 23344 RVA: 0x001DB9A8 File Offset: 0x001D9BA8
	protected override void Awake()
	{
		base.Awake();
		SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
		if (UniversalInputManager.Get().IsTouchMode())
		{
			this.m_rewardChestInstructions.Text = GameStrings.Format("GLOBAL_SEASON_END_CHEST_INSTRUCTIONS_TOUCH", Array.Empty<object>());
		}
		this.m_okayButton.SetText(GameStrings.Get("GLOBAL_BUTTON_NEXT"));
		this.m_okayButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OkayButtonReleased));
		NetCache.Get().RegisterUpdatedListener(typeof(NetCache.NetCacheRewardProgress), new Action(this.OnNetCacheRewardProgressUpdated));
		NetCache.Get().ReloadNetObject<NetCache.NetCacheRewardProgress>();
	}

	// Token: 0x06005B31 RID: 23345 RVA: 0x001DBA50 File Offset: 0x001D9C50
	private void Start()
	{
		this.m_rankedMedalWidgetReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnRankedMedalWidgetReady));
		this.m_starMultiplierWidgetReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnStarMultiplierWidgetReady));
		this.m_rankedCardBackProgressWidgetReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnRankedCardBackProgressWidgetReady));
		this.m_rankedIntroPopUpWidgetReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnRankedIntroPopUpWidgetReady));
		this.m_rankedRewardChestWidgetReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnRankedRewardChestWidgetReady));
	}

	// Token: 0x06005B32 RID: 23346 RVA: 0x001DBAD0 File Offset: 0x001D9CD0
	public void Init(SeasonEndDialog.SeasonEndInfo info)
	{
		this.m_seasonEndInfo = info;
		this.m_header.Text = this.GetSeasonName(info.m_seasonID);
		this.m_earnedRewardChest = (info.m_rankedRewards != null && info.m_rankedRewards.Count > 0);
		this.m_seasonEndMedalInfo = MedalInfoTranslator.CreateTranslatedMedalInfo(info.m_formatType, info.m_leagueId, info.m_starLevelAtEndOfSeason, info.m_legendIndex);
		this.m_seasonBestMedalInfo = MedalInfoTranslator.CreateTranslatedMedalInfo(info.m_formatType, info.m_leagueId, info.m_bestStarLevelAtEndOfSeason, info.m_legendIndex);
		this.m_currentMedalInfo = RankMgr.Get().GetLocalPlayerMedalInfo().GetCurrentMedal(info.m_formatType);
		this.m_wasPrevSeasonLegacy = RankMgr.Get().UseLegacyRankedPlay(this.m_seasonEndInfo.m_leagueId);
		this.m_isNewSeasonLegacy = RankMgr.Get().UseLegacyRankedPlay(this.m_currentMedalInfo.leagueId);
		this.m_seasonBestRankedDataModel = this.m_seasonBestMedalInfo.CreateDataModel(RankedMedal.DisplayMode.Default, false, false, null);
		this.m_rankedChestDataModel = this.m_seasonBestMedalInfo.CreateDataModel(RankedMedal.DisplayMode.Chest, false, false, null);
		this.m_currentRankedDataModel = this.m_currentMedalInfo.CreateDataModel(RankedMedal.DisplayMode.Default, false, false, null);
		this.m_showMedal = true;
		this.m_rankName.Text = this.m_seasonBestMedalInfo.GetRankName();
		this.m_cardBackReminderDetails.Text = GameStrings.Format("GLOBAL_REMINDER_CARDBACK_SEASON_END_DIALOG", new object[]
		{
			RankMgr.Get().GetLocalPlayerMedalInfo().GetSeasonCardBackMinWins()
		});
		foreach (PegUIElement pegUIElement in this.m_rewardChests)
		{
			pegUIElement.gameObject.SetActive(false);
		}
		if (this.m_earnedRewardChest && this.m_wasPrevSeasonLegacy)
		{
			this.InitLegacyChest();
		}
		this.m_progressBar.SetProgressBar(0f);
	}

	// Token: 0x06005B33 RID: 23347 RVA: 0x001DBCB0 File Offset: 0x001D9EB0
	private void InitLegacyChest()
	{
		int rewardChestVisualIndex = this.m_seasonBestMedalInfo.RankConfig.RewardChestVisualIndex;
		this.m_rewardChestLegacy = this.m_rewardChests[rewardChestVisualIndex];
		this.m_rewardChestLegacy.gameObject.SetActive(true);
		this.m_rewardChestLegacy.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.LegacyChestButtonReleased));
		this.m_medalPlayMaker.FsmVariables.GetFsmGameObject("RankChest").Value = this.m_rewardChestLegacy.gameObject;
		UberText[] componentsInChildren = this.m_rewardChestLegacy.GetComponentsInChildren<UberText>(true);
		if (componentsInChildren.Length != 0)
		{
			componentsInChildren[0].Text = this.m_seasonBestMedalInfo.GetMedalText();
		}
		this.m_rewardChestHeader.Text = this.GetChestEarnedText();
	}

	// Token: 0x06005B34 RID: 23348 RVA: 0x001DBD64 File Offset: 0x001D9F64
	private void InitNewChest()
	{
		PlayMakerFSM componentInChildren = this.m_rankedRewardChestWidget.GetComponentInChildren<PlayMakerFSM>();
		if (componentInChildren != null)
		{
			this.m_medalPlayMaker.FsmVariables.GetFsmGameObject("RankChest").Value = componentInChildren.gameObject;
		}
		this.m_rewardChestHeader.Text = "GLOBAL_REWARD_CHEST_HEADER";
	}

	// Token: 0x06005B35 RID: 23349 RVA: 0x001DBDB8 File Offset: 0x001D9FB8
	protected override void OnDestroy()
	{
		SceneMgr sceneMgr;
		if (HearthstoneServices.TryGet<SceneMgr>(out sceneMgr))
		{
			sceneMgr.UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
		}
	}

	// Token: 0x06005B36 RID: 23350 RVA: 0x001DBDE1 File Offset: 0x001D9FE1
	public void ShowMedal()
	{
		this.m_showMedal = true;
		this.UpdateRankedMedalWidget();
	}

	// Token: 0x06005B37 RID: 23351 RVA: 0x001DBDF0 File Offset: 0x001D9FF0
	public void HideMedal()
	{
		this.m_showMedal = false;
		this.UpdateRankedMedalWidget();
	}

	// Token: 0x06005B38 RID: 23352 RVA: 0x001DBDFF File Offset: 0x001D9FFF
	private void ShowNewRewardChestWidget()
	{
		if (!this.m_wasPrevSeasonLegacy)
		{
			this.m_rankedRewardChestWidget.Show();
		}
	}

	// Token: 0x06005B39 RID: 23353 RVA: 0x001DBE14 File Offset: 0x001DA014
	public void ShowRewardChestPage()
	{
		this.m_rewardChestPage.SetActive(true);
		this.m_leftFiligree.transform.position = this.m_rewardChestLeftFiligreeBone.transform.position;
		this.m_rightFiligree.transform.position = this.m_rewardChestRightFiligreeBone.transform.position;
		iTween.FadeTo(this.m_leftFiligree.gameObject, 1f, 0.5f);
		iTween.FadeTo(this.m_rightFiligree.gameObject, 1f, 0.5f);
		if (this.m_wasPrevSeasonLegacy && this.m_seasonBestMedalInfo.IsLegendRank())
		{
			this.m_legendaryGem.SetActive(true);
		}
	}

	// Token: 0x06005B3A RID: 23354 RVA: 0x001DBEC2 File Offset: 0x001DA0C2
	public void HideRewardChestPage()
	{
		this.m_rewardChestPage.SetActive(false);
		if (!this.m_wasPrevSeasonLegacy)
		{
			this.m_rankedRewardChestWidget.Hide();
		}
	}

	// Token: 0x06005B3B RID: 23355 RVA: 0x001DBEE3 File Offset: 0x001DA0E3
	private void DisableOkayButton(bool hideButton)
	{
		if (hideButton && !this.m_isOkayButtonHidden)
		{
			this.m_okayButton.Flip(false, false);
			this.m_isOkayButtonHidden = true;
		}
		this.m_okayButton.SetEnabled(false, false);
		this.m_okayButton.GetComponent<UIBHighlight>().Reset();
	}

	// Token: 0x06005B3C RID: 23356 RVA: 0x001DBF21 File Offset: 0x001DA121
	private void EnableOkayButton()
	{
		if (this.m_isOkayButtonHidden)
		{
			this.m_okayButton.Flip(true, false);
			this.m_isOkayButtonHidden = false;
		}
		this.m_okayButton.SetEnabled(true, false);
	}

	// Token: 0x06005B3D RID: 23357 RVA: 0x001DBF4C File Offset: 0x001DA14C
	public void MedalAnimationFinished()
	{
		if (this.m_currentMode == SeasonEndDialog.MODE.REDUCED_WELCOME)
		{
			if (this.m_isNewSeasonLegacy)
			{
				this.GotoChestReminder();
				return;
			}
			this.GoToCardBackReminder();
			return;
		}
		else
		{
			if (this.m_earnedRewardChest)
			{
				this.DisableOkayButton(true);
				this.m_currentMode = SeasonEndDialog.MODE.CHEST_EARNED;
				this.m_medalPlayMaker.SendEvent("RevealRewardChest");
				iTween.FadeTo(this.m_rankAchieved.gameObject, 0f, 0.5f);
				return;
			}
			this.GotoBonusStarsOrWelcome();
			return;
		}
	}

	// Token: 0x06005B3E RID: 23358 RVA: 0x001DBFC0 File Offset: 0x001DA1C0
	public void GotoBonusStarsOrWelcome()
	{
		if (!this.m_isNewSeasonLegacy && this.m_seasonEndMedalInfo.LeagueConfig.LeagueType != League.LeagueType.NEW_PLAYER)
		{
			long num = 0L;
			int rankedIntroSeenRequirement = this.m_currentMedalInfo.LeagueConfig.RankedIntroSeenRequirement;
			GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_INTRO_SEEN_COUNT, out num);
			if (num < (long)rankedIntroSeenRequirement)
			{
				this.GoToRankedIntroPopUp();
			}
			else
			{
				this.m_skipRankedIntroPopup = true;
			}
		}
		string seasonName = this.GetSeasonName(this.m_rewardProgress.Season);
		this.m_header.Text = seasonName;
		if (this.m_currentMedalInfo.starsPerWin > 1 && !this.m_isNewSeasonLegacy)
		{
			this.GoToStarMultiplier();
			return;
		}
		if (this.m_currentMedalInfo.starLevel < this.m_seasonEndMedalInfo.starLevel && this.m_wasPrevSeasonLegacy)
		{
			this.GotoReducedMedal();
			return;
		}
		if (!this.m_earnedRewardChest)
		{
			this.GotoSeasonWelcome(seasonName);
			return;
		}
		if (this.m_isNewSeasonLegacy)
		{
			this.GotoChestReminder();
			return;
		}
		this.GoToCardBackReminder();
	}

	// Token: 0x06005B3F RID: 23359 RVA: 0x001DC0B0 File Offset: 0x001DA2B0
	public void GoToStarMultiplier()
	{
		this.m_currentMode = SeasonEndDialog.MODE.STAR_MULTIPLIER;
		this.m_welcomeItems.SetActive(false);
		if (this.m_skipRankedIntroPopup)
		{
			base.StartCoroutine(this.DoPageTear());
			return;
		}
		this.HideRewardChestPage();
		this.m_bonusStarItems.SetActive(true);
		this.m_bonusStarTitle.Text = GameStrings.Get("GLOBAL_SEASON_END_STAR_MULTIPLIER_TITLE");
		this.m_bonusStarLabel.Text = GameStrings.Get("GLOBAL_SEASON_END_STAR_MULTIPLIER_LABEL");
		base.StartCoroutine(this.FadeWidgetIn(this.m_starMultiplierWidget, 0f));
		iTween.FadeTo(this.m_bonusStarItems, 1f, 0f);
		this.EnableOkayButton();
	}

	// Token: 0x06005B40 RID: 23360 RVA: 0x001DC158 File Offset: 0x001DA358
	public void GotoReducedMedal()
	{
		this.m_currentMode = SeasonEndDialog.MODE.REDUCED_WELCOME;
		base.StartCoroutine(this.DoPageTear());
		this.HideRewardChestPage();
		this.m_welcomeItems.SetActive(false);
		this.m_bonusStarItems.SetActive(true);
		this.UpdateRankedMedalWidget();
		this.m_bonusStarLabel.Text = this.m_currentMedalInfo.GetRankName();
		this.m_bonusStarTitle.Text = GameStrings.Get("GLOBAL_SEASON_END_BONUS_STAR_TITLE");
		this.UpdateBonusStarFinePrint();
	}

	// Token: 0x06005B41 RID: 23361 RVA: 0x001DC1D0 File Offset: 0x001DA3D0
	public void GotoChestReminder()
	{
		this.m_currentMode = SeasonEndDialog.MODE.REMINDER_CHEST;
		this.HideRewardChestPage();
		this.m_welcomeItems.SetActive(false);
		this.m_bonusStarItems.SetActive(false);
		base.StartCoroutine(this.DoPageTear());
		int seasonRollRewardMinWins = RankMgr.Get().GetLeagueRecord(this.m_seasonEndInfo.m_leagueId).SeasonRollRewardMinWins;
		this.m_progressBar.SetLabel(GameStrings.Format("GLOBAL_REWARD_PROGRESS", new object[]
		{
			0,
			seasonRollRewardMinWins
		}));
	}

	// Token: 0x06005B42 RID: 23362 RVA: 0x001DC257 File Offset: 0x001DA457
	public void GoToCardBackReminder()
	{
		this.m_currentMode = SeasonEndDialog.MODE.REMINDER_CARDBACK;
		this.HideRewardChestPage();
		this.m_welcomeItems.SetActive(false);
		this.m_bonusStarItems.SetActive(false);
		base.StartCoroutine(this.DoPageTear());
	}

	// Token: 0x06005B43 RID: 23363 RVA: 0x001DC28C File Offset: 0x001DA48C
	public void GoToRankedIntroPopUp()
	{
		iTween.ScaleTo(this.m_root, new Vector3(0f, 0f, 0f), 0.5f);
		this.m_rankedIntroPopUpWidget.TriggerEvent("CODE_DIALOGMANAGER_SHOW", default(Widget.TriggerEventParameters));
	}

	// Token: 0x06005B44 RID: 23364 RVA: 0x001DC2D7 File Offset: 0x001DA4D7
	private void ReminderChestSummonOutFinished()
	{
		this.Finish();
	}

	// Token: 0x06005B45 RID: 23365 RVA: 0x001DC2E0 File Offset: 0x001DA4E0
	public void GotoSeasonWelcome(string newSeasonName)
	{
		this.m_currentMode = SeasonEndDialog.MODE.SEASON_WELCOME;
		base.StartCoroutine(this.DoPageTear());
		this.m_welcomeItems.SetActive(true);
		this.HideRewardChestPage();
		this.m_bonusStarItems.SetActive(false);
		this.m_welcomeDetails.Text = GameStrings.Format("GLOBAL_SEASON_END_NEW_SEASON", new object[]
		{
			newSeasonName
		});
	}

	// Token: 0x06005B46 RID: 23366 RVA: 0x001DC33E File Offset: 0x001DA53E
	public IEnumerator DoPageTear()
	{
		this.m_medalPlayMaker.SendEvent("PageTear");
		yield return new WaitForSeconds(0.69f);
		bool flag = false;
		if (this.m_currentMode == SeasonEndDialog.MODE.REMINDER_CHEST)
		{
			this.m_leftFiligree.transform.position = this.m_reminderChestLeftFiligreeBone.transform.position;
			this.m_rightFiligree.transform.position = this.m_reminderChestRightFiligreeBone.transform.position;
			iTween.FadeTo(this.m_leftFiligree.gameObject, 1f, 0.5f);
			iTween.FadeTo(this.m_rightFiligree.gameObject, 1f, 0.5f);
			this.m_reminderRewardsChest.SetActive(true);
			this.m_reminderRewardsChest.GetComponent<PlayMakerFSM>().SendEvent("SummonIn");
			this.EnableOkayButton();
			this.m_okayButton.SetText("GLOBAL_DONE");
		}
		else if (this.m_currentMode == SeasonEndDialog.MODE.REDUCED_WELCOME)
		{
			this.m_leftFiligree.transform.position = this.m_boostedMedalLeftFiligreeBone.transform.position;
			this.m_rightFiligree.transform.position = this.m_boostedMedalRightFiligreeBone.transform.position;
			if (this.m_seasonBestMedalInfo.IsLegendRank())
			{
				this.m_medalPlayMaker.SendEvent("JustMedalIn");
			}
			else
			{
				this.m_medalPlayMaker.SendEvent("MedalBannerIn");
			}
			flag = true;
		}
		else if (this.m_currentMode == SeasonEndDialog.MODE.STAR_MULTIPLIER)
		{
			this.HideRewardChestPage();
			this.m_bonusStarItems.SetActive(true);
			this.m_bonusStarTitle.Text = GameStrings.Get("GLOBAL_SEASON_END_STAR_MULTIPLIER_TITLE");
			this.m_bonusStarLabel.Text = GameStrings.Get("GLOBAL_SEASON_END_STAR_MULTIPLIER_LABEL");
			base.StartCoroutine(this.FadeWidgetIn(this.m_starMultiplierWidget, 0.5f));
			iTween.FadeTo(this.m_bonusStarItems, 1f, 0.5f);
			this.EnableOkayButton();
		}
		else if (this.m_currentMode == SeasonEndDialog.MODE.REMINDER_CARDBACK)
		{
			this.m_rankedCardBackProgressWidget.Show();
			this.m_cardBackReminderDetails.Show();
			this.m_okayButton.SetText("GLOBAL_DONE");
		}
		if (!flag)
		{
			this.EnableOkayButton();
		}
		yield break;
	}

	// Token: 0x06005B47 RID: 23367 RVA: 0x001DC34D File Offset: 0x001DA54D
	public void MedalInFinished()
	{
		this.EnableOkayButton();
	}

	// Token: 0x06005B48 RID: 23368 RVA: 0x001DC355 File Offset: 0x001DA555
	public override void Show()
	{
		base.StartCoroutine(this.ShowWhenReady());
	}

	// Token: 0x06005B49 RID: 23369 RVA: 0x001DC364 File Offset: 0x001DA564
	private IEnumerator ShowWhenReady()
	{
		while (this.m_rewardProgress == null || this.m_rankedMedal == null || this.m_rankedMedalWidget.IsChangingStates || this.m_starMultiplierWidget == null || this.m_starMultiplierWidget.IsChangingStates || this.m_rankedCardBackProgressWidget == null || this.m_rankedCardBackProgressWidget.IsChangingStates || this.m_rankedRewardChestWidget == null || this.m_rankedRewardChestWidget.IsChangingStates)
		{
			yield return null;
		}
		if (this.m_earnedRewardChest && !this.m_wasPrevSeasonLegacy)
		{
			this.InitNewChest();
		}
		this.FadeEffectsIn();
		base.Show();
		this.DoShowAnimation();
		UniversalInputManager.Get().SetGameDialogActive(true);
		SoundManager.Get().LoadAndPlay("rank_window_expand.prefab:9f3f1c260a5d8b34f9705caf4925f5cb");
		yield break;
	}

	// Token: 0x06005B4A RID: 23370 RVA: 0x001DC373 File Offset: 0x001DA573
	public override void Hide()
	{
		this.m_seasonFramePage.SetActive(false);
		base.Hide();
		this.FadeEffectsOut();
		SoundManager.Get().LoadAndPlay("rank_window_shrink.prefab:9c6393a1d207a07439c22f31ef405a7c");
	}

	// Token: 0x06005B4B RID: 23371 RVA: 0x001DC3A1 File Offset: 0x001DA5A1
	protected override void OnHideAnimFinished()
	{
		UniversalInputManager.Get().SetGameDialogActive(false);
		base.OnHideAnimFinished();
	}

	// Token: 0x06005B4C RID: 23372 RVA: 0x001DC3B4 File Offset: 0x001DA5B4
	private void Finish()
	{
		this.DisableOkayButton(false);
		this.Hide();
		foreach (long id in this.m_seasonEndInfo.m_noticesToAck)
		{
			Network.Get().AckNotice(id);
		}
	}

	// Token: 0x06005B4D RID: 23373 RVA: 0x001DC420 File Offset: 0x001DA620
	private void OkayButtonReleased(UIEvent e)
	{
		this.DisableOkayButton(false);
		if (this.m_currentMode == SeasonEndDialog.MODE.REMINDER_CHEST)
		{
			this.m_reminderRewardsChest.GetComponent<PlayMakerFSM>().SendEvent("SummonOut");
			return;
		}
		if (this.m_currentMode == SeasonEndDialog.MODE.SEASON_WELCOME || this.m_currentMode == SeasonEndDialog.MODE.REDUCED_WELCOME)
		{
			this.m_boostedFlourish.GetComponent<Renderer>().SetMaterial(this.m_transparentMaterial);
			iTween.FadeTo(this.m_bonusStarItems.gameObject, 0f, 0.5f);
			iTween.FadeTo(this.m_boostedFlourish.gameObject, 0f, 0.5f);
			iTween.FadeTo(this.m_leftFiligree.gameObject, 0f, 0.5f);
			iTween.FadeTo(this.m_rightFiligree.gameObject, 0f, 0.5f);
			if (this.m_currentMode != SeasonEndDialog.MODE.SEASON_WELCOME)
			{
				this.m_medalPlayMaker.SendEvent("JustMedalNoRibbon");
				return;
			}
			this.m_welcomeItems.SetActive(false);
			if (this.m_isNewSeasonLegacy)
			{
				this.GotoChestReminder();
				return;
			}
			this.GoToCardBackReminder();
			return;
		}
		else if (this.m_currentMode == SeasonEndDialog.MODE.RANK_EARNED)
		{
			this.m_ribbon.GetComponent<Renderer>().SetMaterial(this.m_transparentMaterial);
			this.m_nameFlourish.GetComponent<Renderer>().SetMaterial(this.m_transparentMaterial);
			iTween.FadeTo(this.m_nameFlourish.gameObject, 0f, 0.5f);
			iTween.FadeTo(this.m_rankName.gameObject, iTween.Hash(new object[]
			{
				"alpha",
				0,
				"time",
				0.5f,
				"oncomplete",
				"OnRankNameHidden",
				"oncompletetarget",
				base.gameObject
			}));
			iTween.FadeTo(this.m_rankAchieved.gameObject, 0f, 0.5f);
			iTween.FadeTo(this.m_leftFiligree.gameObject, 0f, 0.5f);
			iTween.FadeTo(this.m_rightFiligree.gameObject, 0f, 0.5f);
			if (this.m_seasonBestMedalInfo.IsLegendRank())
			{
				this.m_medalPlayMaker.SendEvent("JustMedal");
				return;
			}
			this.m_medalPlayMaker.SendEvent("MedalBanner");
			return;
		}
		else
		{
			if (this.m_currentMode == SeasonEndDialog.MODE.STAR_MULTIPLIER)
			{
				base.StartCoroutine(this.FadeWidgetOut(this.m_starMultiplierWidget, 0.5f));
				this.GoToCardBackReminder();
				return;
			}
			if (this.m_currentMode == SeasonEndDialog.MODE.REMINDER_CARDBACK)
			{
				this.m_rankedCardBackProgressWidget.Hide();
				this.Finish();
			}
			return;
		}
	}

	// Token: 0x06005B4E RID: 23374 RVA: 0x001DC694 File Offset: 0x001DA894
	private void LegacyChestButtonReleased(UIEvent e)
	{
		if (this.m_chestOpened)
		{
			return;
		}
		this.m_chestOpened = true;
		this.m_rewardChestLegacy.GetComponent<PlayMakerFSM>().SendEvent("StartAnim");
	}

	// Token: 0x06005B4F RID: 23375 RVA: 0x001DC6BC File Offset: 0x001DA8BC
	private void OpenRewards()
	{
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			if (SoundManager.Get() != null)
			{
				SoundManager.Get().LoadAndPlay("card_turn_over_legendary.prefab:a8140f686bff601459e954bc23de35e0");
			}
			RewardBoxesDisplay component = go.GetComponent<RewardBoxesDisplay>();
			component.SetRewards(this.m_seasonEndInfo.m_rankedRewards);
			component.m_playBoxFlyoutSound = false;
			component.SetLayer(GameLayer.PerspectiveUI);
			component.UseDarkeningClickCatcher(true);
			component.RegisterDoneCallback(delegate
			{
				if (this.m_wasPrevSeasonLegacy)
				{
					this.m_rewardChestLegacy.GetComponent<PlayMakerFSM>().SendEvent("SummonOut");
					return;
				}
				PlayMakerFSM componentInChildren = this.m_rankedRewardChestWidget.GetComponentInChildren<PlayMakerFSM>();
				FsmGameObject fsmGameObject = componentInChildren.FsmVariables.GetFsmGameObject("OwnerObject");
				if (fsmGameObject != null)
				{
					fsmGameObject.Value = base.gameObject;
				}
				componentInChildren.SendEvent("SummonOut");
			});
			component.transform.localPosition = this.m_rewardBoxesBone.transform.localPosition;
			component.transform.localRotation = this.m_rewardBoxesBone.transform.localRotation;
			component.transform.localScale = this.m_rewardBoxesBone.transform.localScale;
			component.AnimateRewards();
		};
		AssetLoader.Get().InstantiatePrefab("RewardBoxes.prefab:f136fead3d6a148c6801f1e3bd2e8267", callback, null, AssetLoadingOptions.None);
		iTween.FadeTo(this.m_rewardChestInstructions.gameObject, 0f, 0.5f);
	}

	// Token: 0x06005B50 RID: 23376 RVA: 0x0006BF0E File Offset: 0x0006A10E
	private void FadeEffectsIn()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.SetBlurBrightness(1f);
		fullScreenFXMgr.SetBlurDesaturation(0f);
		fullScreenFXMgr.Vignette();
		fullScreenFXMgr.Blur(1f, 0.4f, iTween.EaseType.easeOutCirc, null);
	}

	// Token: 0x06005B51 RID: 23377 RVA: 0x001A2E08 File Offset: 0x001A1008
	private void FadeEffectsOut()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.StopVignette();
		fullScreenFXMgr.StopBlur();
	}

	// Token: 0x06005B52 RID: 23378 RVA: 0x001DC708 File Offset: 0x001DA908
	private string GetSeasonName(int seasonId)
	{
		string key = string.Format("GLUE_RANKED_SEASON_NAME_{0}", seasonId);
		if (seasonId < 6)
		{
			Debug.LogFormat("GetSeasonName called with invalid seasonId {0}. Launch season is 6.", Array.Empty<object>());
			return string.Empty;
		}
		int monthDigits = (seasonId + 9) % 12 + 1;
		int num = 2014 + Mathf.FloorToInt(((float)seasonId - 3f) / 12f);
		string monthFromDigits = GameStrings.GetMonthFromDigits(monthDigits);
		if (GameStrings.HasKey(key))
		{
			return GameStrings.Format(key, new object[]
			{
				monthFromDigits,
				num,
				seasonId
			});
		}
		return GameStrings.Format("GLUE_RANKED_SEASON_NAME_GENERIC", new object[]
		{
			monthFromDigits,
			num,
			seasonId
		});
	}

	// Token: 0x06005B53 RID: 23379 RVA: 0x001DC7B9 File Offset: 0x001DA9B9
	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (mode != SceneMgr.Mode.HUB)
		{
			this.Hide();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06005B54 RID: 23380 RVA: 0x001DC7D0 File Offset: 0x001DA9D0
	private void UpdateBonusStarFinePrint()
	{
		if (this.m_seasonEndInfo.m_wasLimitedByBestEverStarLevel)
		{
			this.m_bonusStarFinePrint.Text = GameStrings.Format("GLOBAL_SEASON_END_BEST_EVER_ABOVE_MAX", new object[]
			{
				this.m_currentMedalInfo.GetMedalText()
			});
			this.m_bonusStarFinePrint.Show();
			return;
		}
		this.m_bonusStarFinePrint.Hide();
	}

	// Token: 0x06005B55 RID: 23381 RVA: 0x001DC82A File Offset: 0x001DAA2A
	private void OnRankNameHidden()
	{
		this.m_rankName.gameObject.SetActive(false);
	}

	// Token: 0x06005B56 RID: 23382 RVA: 0x001DC83D File Offset: 0x001DAA3D
	private void OnRankedMedalWidgetReady(Widget widget)
	{
		this.m_rankedMedalWidget = widget;
		this.m_rankedMedal = widget.GetComponentInChildren<RankedMedalWrapper>();
		this.UpdateRankedMedalWidget();
	}

	// Token: 0x06005B57 RID: 23383 RVA: 0x001DC858 File Offset: 0x001DAA58
	private void OnStarMultiplierWidgetReady(Widget widget)
	{
		this.m_starMultiplierWidget = widget;
		IDataModel dataModel;
		if (!this.m_starMultiplierWidget.GetDataModel(123, out dataModel))
		{
			dataModel = new RankedPlayDataModel();
			this.m_starMultiplierWidget.BindDataModel(dataModel, false);
		}
		RankedPlayDataModel rankedPlayDataModel = dataModel as RankedPlayDataModel;
		if (rankedPlayDataModel != null)
		{
			rankedPlayDataModel.StarMultiplier = this.m_currentMedalInfo.starsPerWin;
		}
		base.StartCoroutine(this.FadeWidgetOut(this.m_starMultiplierWidget, 0f));
	}

	// Token: 0x06005B58 RID: 23384 RVA: 0x001DC8C3 File Offset: 0x001DAAC3
	private void OnRankedCardBackProgressWidgetReady(Widget widget)
	{
		this.m_rankedCardBackProgressWidget = widget;
		this.UpdateRankedCardBackWidget();
		this.m_cardBackReminderDetails.Hide();
		this.m_rankedCardBackProgressWidget.Hide();
	}

	// Token: 0x06005B59 RID: 23385 RVA: 0x001DC8E8 File Offset: 0x001DAAE8
	private void OnRankedIntroPopUpWidgetReady(Widget widget)
	{
		this.m_rankedIntroPopUpWidget = widget;
		widget.RegisterEventListener(new Widget.EventListenerDelegate(this.RankedIntroPopUpEventListener));
	}

	// Token: 0x06005B5A RID: 23386 RVA: 0x001DC903 File Offset: 0x001DAB03
	private void RankedIntroPopUpEventListener(string eventName)
	{
		if (eventName.Equals("HIDE"))
		{
			iTween.ScaleTo(this.m_root, new Vector3(1f, 1f, 1f), 0.5f);
		}
	}

	// Token: 0x06005B5B RID: 23387 RVA: 0x001DC938 File Offset: 0x001DAB38
	private void OnRankedRewardChestWidgetReady(Widget widget)
	{
		this.m_rankedRewardChestWidget = widget;
		if (this.m_wasPrevSeasonLegacy)
		{
			this.m_rankedRewardChestWidget.gameObject.SetActive(false);
			return;
		}
		this.m_rankedRewardChestWidget.Hide();
		this.m_rankedRewardChestWidget.RegisterEventListener(new Widget.EventListenerDelegate(this.RankedChestEventListener));
		this.m_rankedRewardChestWidget.BindDataModel(this.m_rankedChestDataModel, false);
	}

	// Token: 0x06005B5C RID: 23388 RVA: 0x001DC99C File Offset: 0x001DAB9C
	private void RankedChestEventListener(string eventName)
	{
		if (eventName.Equals("CLICKED"))
		{
			if (this.m_chestOpened)
			{
				return;
			}
			this.m_chestOpened = true;
			PlayMakerFSM componentInChildren = this.m_rankedRewardChestWidget.GetComponentInChildren<PlayMakerFSM>();
			FsmGameObject fsmGameObject = componentInChildren.FsmVariables.GetFsmGameObject("OwnerObject");
			if (fsmGameObject != null)
			{
				fsmGameObject.Value = base.gameObject;
			}
			componentInChildren.SendEvent("StartAnim");
		}
	}

	// Token: 0x06005B5D RID: 23389 RVA: 0x001DC9FC File Offset: 0x001DABFC
	private void UpdateRankedCardBackWidget()
	{
		if (this.m_rankedCardBackProgressWidget == null || this.m_rewardProgress == null)
		{
			return;
		}
		IDataModel dataModel;
		if (!this.m_rankedCardBackProgressWidget.GetDataModel(26, out dataModel))
		{
			dataModel = new CardBackDataModel();
			this.m_rankedCardBackProgressWidget.BindDataModel(dataModel, false);
		}
		CardBackDataModel cardBackDataModel;
		if ((cardBackDataModel = (dataModel as CardBackDataModel)) != null)
		{
			cardBackDataModel.CardBackId = RankMgr.Get().GetRankedCardBackIdForSeasonId(this.m_rewardProgress.Season);
		}
		ProgressBar componentInChildren = this.m_rankedCardBackProgressWidget.GetComponentInChildren<ProgressBar>();
		if (componentInChildren != null)
		{
			int seasonCardBackMinWins = RankMgr.Get().GetLocalPlayerMedalInfo().GetSeasonCardBackMinWins();
			componentInChildren.SetLabel(GameStrings.Format("GLOBAL_REWARD_PROGRESS", new object[]
			{
				0,
				seasonCardBackMinWins
			}));
			componentInChildren.SetProgressBar(0f);
		}
	}

	// Token: 0x06005B5E RID: 23390 RVA: 0x001DCAC4 File Offset: 0x001DACC4
	private void UpdateRankedMedalWidget()
	{
		if (this.m_rankedMedal == null)
		{
			return;
		}
		if (this.m_showMedal)
		{
			this.m_rankedMedal.gameObject.SetActive(true);
			RankedPlayDataModel dataModel;
			if (this.m_currentMode == SeasonEndDialog.MODE.REDUCED_WELCOME)
			{
				this.m_rankedMedal.transform.position = this.m_boostedMedalBone.transform.position;
				dataModel = this.m_currentRankedDataModel;
			}
			else
			{
				dataModel = this.m_seasonBestRankedDataModel;
			}
			this.m_rankedMedal.BindRankedPlayDataModel(dataModel);
			this.m_rankedMedal.Show(this.m_wasPrevSeasonLegacy);
			return;
		}
		this.m_rankedMedal.gameObject.SetActive(false);
	}

	// Token: 0x06005B5F RID: 23391 RVA: 0x001DCB61 File Offset: 0x001DAD61
	private IEnumerator FadeWidgetIn(Widget widget, float time)
	{
		while (!widget.IsReady || widget.IsChangingStates)
		{
			yield return null;
		}
		iTween.FadeTo(widget.gameObject, 1f, time);
		yield break;
	}

	// Token: 0x06005B60 RID: 23392 RVA: 0x001DCB77 File Offset: 0x001DAD77
	private IEnumerator FadeWidgetOut(Widget widget, float time)
	{
		while (!widget.IsReady || widget.IsChangingStates)
		{
			yield return null;
		}
		iTween.FadeTo(widget.gameObject, 0f, time);
		yield break;
	}

	// Token: 0x06005B61 RID: 23393 RVA: 0x001DCB8D File Offset: 0x001DAD8D
	private void OnNetCacheRewardProgressUpdated()
	{
		this.m_rewardProgress = NetCache.Get().GetNetObject<NetCache.NetCacheRewardProgress>();
		this.UpdateRankedCardBackWidget();
	}

	// Token: 0x06005B62 RID: 23394 RVA: 0x001DCBA8 File Offset: 0x001DADA8
	private int GetChestRewardTier()
	{
		int maxRewardChestVisualIndex = RankMgr.Get().GetMaxRewardChestVisualIndex();
		int rewardChestVisualIndex = this.m_seasonBestMedalInfo.RankConfig.RewardChestVisualIndex;
		return 1 + (maxRewardChestVisualIndex - rewardChestVisualIndex);
	}

	// Token: 0x06005B63 RID: 23395 RVA: 0x001DCBD8 File Offset: 0x001DADD8
	public string GetChestName()
	{
		int chestRewardTier = this.GetChestRewardTier();
		return GameStrings.Get(string.Format("GLOBAL_REWARD_CHEST_TIER{0}", chestRewardTier));
	}

	// Token: 0x06005B64 RID: 23396 RVA: 0x001DCC04 File Offset: 0x001DAE04
	public string GetChestEarnedText()
	{
		int chestRewardTier = this.GetChestRewardTier();
		return GameStrings.Get(string.Format("GLOBAL_REWARD_CHEST_TIER{0}_EARNED", chestRewardTier));
	}

	// Token: 0x06005B65 RID: 23397 RVA: 0x001DCC30 File Offset: 0x001DAE30
	protected override void DoShowAnimation()
	{
		this.m_showAnimState = DialogBase.ShowAnimState.IN_PROGRESS;
		AnimationUtil.ShowWithPunch(base.gameObject, this.START_SCALE, Vector3.Scale(this.PUNCH_SCALE, this.m_originalScale), this.m_originalScale, "OnShowAnimFinished", true, null, null, null);
	}

	// Token: 0x04004DDA RID: 19930
	public GameObject m_root;

	// Token: 0x04004DDB RID: 19931
	public UIBButton m_okayButton;

	// Token: 0x04004DDC RID: 19932
	public GameObject m_boostedMedalBone;

	// Token: 0x04004DDD RID: 19933
	public GameObject m_boostedMedalLeftFiligreeBone;

	// Token: 0x04004DDE RID: 19934
	public GameObject m_boostedMedalRightFiligreeBone;

	// Token: 0x04004DDF RID: 19935
	public GameObject m_rewardChestPage;

	// Token: 0x04004DE0 RID: 19936
	public PegUIElement m_rewardChestLegacy;

	// Token: 0x04004DE1 RID: 19937
	public UberText m_rewardChestHeader;

	// Token: 0x04004DE2 RID: 19938
	public UberText m_rewardChestInstructions;

	// Token: 0x04004DE3 RID: 19939
	public GameObject m_rewardChestLeftFiligreeBone;

	// Token: 0x04004DE4 RID: 19940
	public GameObject m_rewardChestRightFiligreeBone;

	// Token: 0x04004DE5 RID: 19941
	public GameObject m_rewardBoxesBone;

	// Token: 0x04004DE6 RID: 19942
	public AsyncReference m_rankedMedalWidgetReference;

	// Token: 0x04004DE7 RID: 19943
	public AsyncReference m_starMultiplierWidgetReference;

	// Token: 0x04004DE8 RID: 19944
	public AsyncReference m_rankedRewardChestWidgetReference;

	// Token: 0x04004DE9 RID: 19945
	public UberText m_header;

	// Token: 0x04004DEA RID: 19946
	public UberText m_rankAchieved;

	// Token: 0x04004DEB RID: 19947
	public UberText m_rankName;

	// Token: 0x04004DEC RID: 19948
	public GameObject m_ribbon;

	// Token: 0x04004DED RID: 19949
	public GameObject m_nameFlourish;

	// Token: 0x04004DEE RID: 19950
	public GameObject m_boostedFlourish;

	// Token: 0x04004DEF RID: 19951
	public GameObject m_welcomeItems;

	// Token: 0x04004DF0 RID: 19952
	public GameObject m_leftFiligree;

	// Token: 0x04004DF1 RID: 19953
	public GameObject m_rightFiligree;

	// Token: 0x04004DF2 RID: 19954
	public UberText m_welcomeDetails;

	// Token: 0x04004DF3 RID: 19955
	public UberText m_welcomeTitle;

	// Token: 0x04004DF4 RID: 19956
	public GameObject m_shieldIcon;

	// Token: 0x04004DF5 RID: 19957
	public GameObject m_bonusStarItems;

	// Token: 0x04004DF6 RID: 19958
	public UberText m_bonusStarTitle;

	// Token: 0x04004DF7 RID: 19959
	public UberText m_bonusStarLabel;

	// Token: 0x04004DF8 RID: 19960
	public UberText m_bonusStarFinePrint;

	// Token: 0x04004DF9 RID: 19961
	public GameObject m_bonusStarFlourish;

	// Token: 0x04004DFA RID: 19962
	public Material m_transparentMaterial;

	// Token: 0x04004DFB RID: 19963
	public PlayMakerFSM m_medalPlayMaker;

	// Token: 0x04004DFC RID: 19964
	public GameObject m_seasonFramePage;

	// Token: 0x04004DFD RID: 19965
	public GameObject m_legendaryGem;

	// Token: 0x04004DFE RID: 19966
	public List<PegUIElement> m_rewardChests;

	// Token: 0x04004DFF RID: 19967
	public GameObject m_reminderChestRightFiligreeBone;

	// Token: 0x04004E00 RID: 19968
	public GameObject m_reminderChestLeftFiligreeBone;

	// Token: 0x04004E01 RID: 19969
	public GameObject m_reminderRewardsChest;

	// Token: 0x04004E02 RID: 19970
	public ProgressBar m_progressBar;

	// Token: 0x04004E03 RID: 19971
	public AsyncReference m_rankedCardBackProgressWidgetReference;

	// Token: 0x04004E04 RID: 19972
	public UberText m_cardBackReminderDetails;

	// Token: 0x04004E05 RID: 19973
	public AsyncReference m_rankedIntroPopUpWidgetReference;

	// Token: 0x04004E06 RID: 19974
	private SeasonEndDialog.SeasonEndInfo m_seasonEndInfo;

	// Token: 0x04004E07 RID: 19975
	private TranslatedMedalInfo m_seasonBestMedalInfo;

	// Token: 0x04004E08 RID: 19976
	private TranslatedMedalInfo m_seasonEndMedalInfo;

	// Token: 0x04004E09 RID: 19977
	private TranslatedMedalInfo m_currentMedalInfo;

	// Token: 0x04004E0A RID: 19978
	private bool m_earnedRewardChest;

	// Token: 0x04004E0B RID: 19979
	private bool m_wasPrevSeasonLegacy;

	// Token: 0x04004E0C RID: 19980
	private bool m_isNewSeasonLegacy;

	// Token: 0x04004E0D RID: 19981
	private SeasonEndDialog.MODE m_currentMode;

	// Token: 0x04004E0E RID: 19982
	private RankedMedalWrapper m_rankedMedal;

	// Token: 0x04004E0F RID: 19983
	private RankedPlayDataModel m_seasonBestRankedDataModel;

	// Token: 0x04004E10 RID: 19984
	private RankedPlayDataModel m_currentRankedDataModel;

	// Token: 0x04004E11 RID: 19985
	private RankedPlayDataModel m_rankedChestDataModel;

	// Token: 0x04004E12 RID: 19986
	private Widget m_rankedMedalWidget;

	// Token: 0x04004E13 RID: 19987
	private bool m_showMedal;

	// Token: 0x04004E14 RID: 19988
	private bool m_chestOpened;

	// Token: 0x04004E15 RID: 19989
	private Widget m_starMultiplierWidget;

	// Token: 0x04004E16 RID: 19990
	private Widget m_rankedCardBackProgressWidget;

	// Token: 0x04004E17 RID: 19991
	private Widget m_rankedIntroPopUpWidget;

	// Token: 0x04004E18 RID: 19992
	private bool m_skipRankedIntroPopup;

	// Token: 0x04004E19 RID: 19993
	private Widget m_rankedRewardChestWidget;

	// Token: 0x04004E1A RID: 19994
	private NetCache.NetCacheRewardProgress m_rewardProgress;

	// Token: 0x04004E1B RID: 19995
	private bool m_isOkayButtonHidden;

	// Token: 0x04004E1C RID: 19996
	private const string REWARD_CHEST_NAME_STRING_FORMAT = "GLOBAL_REWARD_CHEST_TIER{0}";

	// Token: 0x04004E1D RID: 19997
	private const string REWARD_CHEST_EARNED_STRING_FORMAT = "GLOBAL_REWARD_CHEST_TIER{0}_EARNED";

	// Token: 0x02002168 RID: 8552
	public class SeasonEndInfo
	{
		// Token: 0x0400E024 RID: 57380
		public int m_seasonID;

		// Token: 0x0400E025 RID: 57381
		public int m_leagueId;

		// Token: 0x0400E026 RID: 57382
		public int m_starLevelAtEndOfSeason;

		// Token: 0x0400E027 RID: 57383
		public int m_bestStarLevelAtEndOfSeason;

		// Token: 0x0400E028 RID: 57384
		public int m_legendIndex;

		// Token: 0x0400E029 RID: 57385
		public List<RewardData> m_rankedRewards;

		// Token: 0x0400E02A RID: 57386
		public List<long> m_noticesToAck = new List<long>();

		// Token: 0x0400E02B RID: 57387
		public FormatType m_formatType;

		// Token: 0x0400E02C RID: 57388
		public bool m_wasLimitedByBestEverStarLevel;
	}

	// Token: 0x02002169 RID: 8553
	private enum MODE
	{
		// Token: 0x0400E02E RID: 57390
		RANK_EARNED,
		// Token: 0x0400E02F RID: 57391
		CHEST_EARNED,
		// Token: 0x0400E030 RID: 57392
		SEASON_WELCOME,
		// Token: 0x0400E031 RID: 57393
		REDUCED_WELCOME,
		// Token: 0x0400E032 RID: 57394
		REMINDER_CHEST,
		// Token: 0x0400E033 RID: 57395
		STAR_MULTIPLIER,
		// Token: 0x0400E034 RID: 57396
		REMINDER_CARDBACK
	}
}
