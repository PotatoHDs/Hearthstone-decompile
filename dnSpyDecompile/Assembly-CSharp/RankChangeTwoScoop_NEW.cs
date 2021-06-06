using System;
using System.Collections;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.UI;
using HutongGames.PlayMaker;
using PegasusShared;
using UnityEngine;

// Token: 0x02000645 RID: 1605
[CustomEditClass]
public class RankChangeTwoScoop_NEW : MonoBehaviour
{
	// Token: 0x06005A71 RID: 23153 RVA: 0x001D8294 File Offset: 0x001D6494
	private void Awake()
	{
		this.m_prevBannerText.Width *= this.m_bannerTextWidthMult;
		this.m_currBannerText.Width *= this.m_bannerTextWidthMult;
		this.Reset();
	}

	// Token: 0x06005A72 RID: 23154 RVA: 0x001D82E4 File Offset: 0x001D64E4
	private void Start()
	{
		this.m_prevRankedMedalWidgetReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnPrevRankedMedalWidgetReady));
		this.m_currRankedMedalWidgetReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnCurrRankedMedalWidgetReady));
		this.m_currRankedMedalLegendTextReference.RegisterReadyListener<UberText>(new Action<UberText>(this.OnCurrRankedMedalLegendTextReady));
	}

	// Token: 0x06005A73 RID: 23155 RVA: 0x001D8336 File Offset: 0x001D6536
	private void OnDestroy()
	{
		if (EndGameScreen.Get() != null)
		{
			EndGameScreen.Get().m_hitbox.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClick));
		}
	}

	// Token: 0x06005A74 RID: 23156 RVA: 0x001D8364 File Offset: 0x001D6564
	public void Initialize(MedalInfoTranslator medalInfoTranslator, FormatType formatType, Action callback)
	{
		if (medalInfoTranslator == null)
		{
			return;
		}
		this.m_closedCallback = callback;
		this.m_medalInfoTranslator = medalInfoTranslator;
		this.m_formatType = formatType;
		this.m_currMedalInfo = this.m_medalInfoTranslator.GetCurrentMedal(this.m_formatType);
		this.m_prevMedalInfo = this.m_medalInfoTranslator.GetPreviousMedal(this.m_formatType);
		RankChangeType changeType = this.m_medalInfoTranslator.GetChangeType(this.m_formatType);
		this.m_isRankChanging = (changeType == RankChangeType.RANK_UP || changeType == RankChangeType.RANK_DOWN);
		this.m_isOnWinStreak = (this.m_prevMedalInfo.RankConfig.WinStreakThreshold > 0 && this.m_currMedalInfo.winStreak >= this.m_prevMedalInfo.RankConfig.WinStreakThreshold);
	}

	// Token: 0x06005A75 RID: 23157 RVA: 0x001D8418 File Offset: 0x001D6618
	private void InitializeFromDataModels()
	{
		int maxStars = this.m_prevMedalDataModel.MaxStars;
		int starCountDarkened = maxStars - this.m_prevMedalDataModel.Stars;
		this.m_prevMedalStars.Init(maxStars, starCountDarkened);
		if (this.m_isRankChanging)
		{
			int maxStars2 = this.m_currMedalDataModel.MaxStars;
			int starCountDarkened2 = maxStars2 - this.m_currMedalDataModel.Stars;
			this.m_currMedalStars.Init(maxStars2, starCountDarkened2);
		}
		if (this.m_isRankChanging)
		{
			if (this.m_prevMedalDataModel.StarMultiplier == 1 && this.m_isOnWinStreak)
			{
				this.m_newlyEarnedStarsForRankUpRow1.Init(2, 0);
			}
			else
			{
				this.m_newlyEarnedStarsForRankUpRow1.Init(this.m_prevMedalDataModel.StarMultiplier, 0);
				if (this.m_isOnWinStreak)
				{
					this.m_newlyEarnedStarsForRankUpRow2.Init(this.m_prevMedalDataModel.StarMultiplier, 0);
				}
			}
		}
		else
		{
			int num = this.m_currMedalDataModel.Stars - this.m_prevMedalDataModel.Stars;
			if (num > 1)
			{
				this.m_newlyEarnedStars.Init(num, 0);
			}
		}
		if (this.m_prevMedalDataModel.StarMultiplier > 1)
		{
			this.m_starMultiplierText.Text = GameStrings.Format("GLOBAL_RANK_STAR_MULT", new object[]
			{
				this.m_prevMedalDataModel.StarMultiplier
			});
		}
		this.m_prevBannerText.Text = this.m_prevMedalDataModel.RankName;
		this.m_currBannerText.Text = this.m_currMedalDataModel.RankName;
	}

	// Token: 0x06005A76 RID: 23158 RVA: 0x001D8574 File Offset: 0x001D6774
	[ContextMenu("Reset")]
	private void Reset()
	{
		this.m_banner.SetActive(false);
		this.m_winStreakText.gameObject.SetActive(false);
		this.m_starMultiplierText.gameObject.SetActive(false);
		this.m_cannotLoseStarText.gameObject.SetActive(false);
		this.m_cannotLoseLevelText.gameObject.SetActive(false);
		this.m_medalGodRays.SetActive(false);
		this.m_debugClickCatcher.gameObject.SetActive(false);
		this.m_prevMedalStars.Hide();
		this.m_currMedalStars.Hide();
		this.m_newlyEarnedStars.Hide();
		this.m_newlyEarnedStarsForRankUpRow1.Hide();
		this.m_newlyEarnedStarsForRankUpRow2.Hide();
		if (this.m_currRankedMedalWidget != null)
		{
			this.m_currRankedMedalWidget.Hide();
		}
		if (this.m_prevRankedMedalWidget != null)
		{
			this.m_prevRankedMedalWidget.Hide();
		}
		if (this.m_currRankedMedalLegendText != null)
		{
			this.m_currRankedMedalLegendText.Hide();
		}
		this.m_mainFSM.SendEvent("Reset");
		this.m_starLossFSM.SendEvent("Reset");
		this.m_starGainSingleFSM.SendEvent("Reset");
		this.m_starGainMultiFSM.SendEvent("Reset");
		this.m_rankUpFSM.SendEvent("Reset");
		this.m_rankDownFSM.SendEvent("Reset");
		this.m_isPlayingAnimWithCancelPoint = false;
	}

	// Token: 0x06005A77 RID: 23159 RVA: 0x001D86D8 File Offset: 0x001D68D8
	public void Show()
	{
		Action<object> showFunc = delegate(object _)
		{
			AnimationUtil.ShowWithPunch(base.gameObject, this.m_startScale, this.m_punchScale, this.m_afterPunchScale, "OnShown", true, null, null, null);
			this.m_mainFSM.SendEvent("Birth");
		};
		base.StartCoroutine(this.ShowWhenReady(showFunc));
	}

	// Token: 0x1700054C RID: 1356
	// (get) Token: 0x06005A78 RID: 23160 RVA: 0x001D8700 File Offset: 0x001D6900
	private bool IsReady
	{
		get
		{
			return this.m_medalInfoTranslator != null && !(this.m_prevRankedMedal == null) && !(this.m_currRankedMedal == null) && this.m_currRankedMedal.IsReady && (!this.m_isRankChanging || this.m_prevRankedMedal.IsReady) && !this.m_newlyEarnedStars.IsLoading() && !this.m_newlyEarnedStarsForRankUpRow1.IsLoading() && !this.m_newlyEarnedStarsForRankUpRow2.IsLoading();
		}
	}

	// Token: 0x06005A79 RID: 23161 RVA: 0x001D8785 File Offset: 0x001D6985
	private void OnPrevRankedMedalWidgetReady(Widget widget)
	{
		this.m_prevRankedMedal = widget.GetComponentInChildren<RankedMedal>();
		this.m_prevRankedMedalWidget = widget;
		this.m_prevRankedMedalWidget.Hide();
	}

	// Token: 0x06005A7A RID: 23162 RVA: 0x001D87A5 File Offset: 0x001D69A5
	private void OnCurrRankedMedalWidgetReady(Widget widget)
	{
		this.m_currRankedMedal = widget.GetComponentInChildren<RankedMedal>();
		this.m_currRankedMedalWidget = widget;
		this.m_currRankedMedalWidget.Hide();
	}

	// Token: 0x06005A7B RID: 23163 RVA: 0x001D87C5 File Offset: 0x001D69C5
	private void OnCurrRankedMedalLegendTextReady(UberText text)
	{
		this.m_currRankedMedalLegendText = text;
	}

	// Token: 0x06005A7C RID: 23164 RVA: 0x001D87CE File Offset: 0x001D69CE
	private IEnumerator ShowWhenReady(Action<object> showFunc)
	{
		while (this.m_prevRankedMedalWidget == null || this.m_currRankedMedalWidget == null)
		{
			yield return null;
		}
		this.m_prevMedalDataModel = this.m_prevMedalInfo.CreateDataModel(RankedMedal.DisplayMode.Default, false, false, delegate(RankedPlayDataModel dm)
		{
			this.m_prevRankedMedalWidget.BindDataModel(dm, false);
		});
		this.m_currMedalDataModel = this.m_currMedalInfo.CreateDataModel(RankedMedal.DisplayMode.Default, false, false, delegate(RankedPlayDataModel dm)
		{
			this.m_currRankedMedalWidget.BindDataModel(dm, false);
		});
		this.InitializeFromDataModels();
		while (!this.IsReady)
		{
			yield return null;
		}
		this.m_banner.SetActive(true);
		this.m_prevBannerText.gameObject.SetActive(true);
		this.m_currBannerText.gameObject.SetActive(false);
		this.m_medalGodRays.SetActive(true);
		this.m_prevMedalStars.Show();
		if (this.m_isRankChanging)
		{
			this.m_prevRankedMedalWidget.Show();
		}
		else
		{
			this.m_currRankedMedalWidget.Show();
			this.m_currRankedMedalLegendText.Hide();
		}
		showFunc(this);
		yield break;
	}

	// Token: 0x06005A7D RID: 23165 RVA: 0x001D87E4 File Offset: 0x001D69E4
	private void OnShown()
	{
		this.m_clickToContinueCoroutine = base.StartCoroutine(this.EnableClickToContinueAfterDelay(this.m_maxAnimTimeBeforeClickToContinue));
		switch (this.m_medalInfoTranslator.GetChangeType(this.m_formatType))
		{
		case RankChangeType.NO_GAME_PLAYED:
			this.HandleMissingRankChange();
			return;
		case RankChangeType.RANK_UP:
			this.PlayRankUp();
			return;
		case RankChangeType.RANK_DOWN:
			this.PlayRankDown();
			return;
		case RankChangeType.RANK_SAME:
			this.PlayStarChange(this.m_prevMedalInfo.CanLoseStars(), this.m_prevMedalInfo.CanLoseLevel());
			return;
		default:
			this.EnableClickToContinue();
			return;
		}
	}

	// Token: 0x06005A7E RID: 23166 RVA: 0x001D886D File Offset: 0x001D6A6D
	private IEnumerator EnableClickToContinueAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		this.EnableClickToContinue();
		yield break;
	}

	// Token: 0x06005A7F RID: 23167 RVA: 0x001D8884 File Offset: 0x001D6A84
	private void EnableClickToContinue()
	{
		if (EndGameScreen.Get() != null)
		{
			EndGameScreen.Get().m_hitbox.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClick));
		}
		if (this.m_isRankChangeCheat)
		{
			this.m_debugClickCatcher.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClick));
		}
	}

	// Token: 0x06005A80 RID: 23168 RVA: 0x001D88DC File Offset: 0x001D6ADC
	private void OnPlayMakerCancelPointPassed()
	{
		this.m_isPlayingAnimWithCancelPoint = false;
	}

	// Token: 0x06005A81 RID: 23169 RVA: 0x001D88E5 File Offset: 0x001D6AE5
	private void OnPlayMakerFinished()
	{
		this.m_isPlayingAnimWithCancelPoint = false;
		this.EnableClickToContinue();
		if (this.m_medalInfoTranslator.GetChangeType(this.m_formatType) == RankChangeType.RANK_UP && Gameplay.Get() != null)
		{
			Gameplay.Get().UpdateFriendlySideMedalChange(this.m_medalInfoTranslator);
		}
	}

	// Token: 0x06005A82 RID: 23170 RVA: 0x001D8928 File Offset: 0x001D6B28
	private void OnClick(UIEvent e)
	{
		if (!this.m_isPlayingAnimWithCancelPoint)
		{
			this.Hide();
			return;
		}
		this.m_isPlayingAnimWithCancelPoint = false;
		RankChangeType changeType = this.m_medalInfoTranslator.GetChangeType(this.m_formatType);
		if (changeType == RankChangeType.RANK_UP)
		{
			this.m_rankUpFSM.SendEvent("Cancel");
			return;
		}
		if (changeType != RankChangeType.RANK_DOWN)
		{
			return;
		}
		this.m_rankDownFSM.SendEvent("Cancel");
	}

	// Token: 0x06005A83 RID: 23171 RVA: 0x001D8988 File Offset: 0x001D6B88
	private void Hide()
	{
		this.m_mainFSM.SendEvent("Death");
		if (this.m_clickToContinueCoroutine != null)
		{
			base.StopCoroutine(this.m_clickToContinueCoroutine);
		}
		if (EndGameScreen.Get() != null)
		{
			EndGameScreen.Get().m_hitbox.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClick));
		}
		if (base.gameObject != null)
		{
			AnimationUtil.ScaleFade(base.gameObject, new Vector3(0.1f, 0.1f, 0.1f), "DestroyRankChange");
		}
		if (this.m_isRankChangeCheat)
		{
			FullScreenFXMgr.Get().StopAllEffects(0f);
		}
	}

	// Token: 0x06005A84 RID: 23172 RVA: 0x001D8A2C File Offset: 0x001D6C2C
	private void DestroyRankChange()
	{
		if (this.m_closedCallback != null)
		{
			this.m_closedCallback();
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06005A85 RID: 23173 RVA: 0x001D8A4C File Offset: 0x001D6C4C
	private void PlayRankUp()
	{
		this.PopuplateBasicFsmVars(this.m_rankUpFSM);
		this.m_currMedalStars.PopulateFsmArrayWithStars(this.m_rankUpFSM, "NewlyEarnedMedalStars", 0, this.m_currMedalDataModel.Stars);
		this.m_newlyEarnedStarsForRankUpRow1.PopulateFsmArrayWithStars(this.m_rankUpFSM, "NewlyEarnedStarsRow1", 0, 0);
		if (this.m_isOnWinStreak)
		{
			this.m_newlyEarnedStarsForRankUpRow2.PopulateFsmArrayWithStars(this.m_rankUpFSM, "NewlyEarnedStarsRow2", 0, 0);
		}
		FsmGameObject fsmGameObject = this.m_rankUpFSM.FsmVariables.GetFsmGameObject("LegendRankText");
		if (fsmGameObject != null)
		{
			fsmGameObject.Value = this.m_currRankedMedalLegendText.gameObject;
		}
		this.m_isPlayingAnimWithCancelPoint = true;
		this.m_rankUpFSM.SendEvent("StartAnim");
	}

	// Token: 0x06005A86 RID: 23174 RVA: 0x001D8B03 File Offset: 0x001D6D03
	private void PlayRankDown()
	{
		this.m_isPlayingAnimWithCancelPoint = true;
		this.m_rankDownFSM.SendEvent("StartAnim");
	}

	// Token: 0x06005A87 RID: 23175 RVA: 0x001D8B1C File Offset: 0x001D6D1C
	private void PlayStarChange(bool canLoseStars, bool canLoseLevel)
	{
		int num = this.m_currMedalDataModel.Stars - this.m_prevMedalDataModel.Stars;
		if (num < 0)
		{
			int startIndex = this.m_prevMedalDataModel.Stars - 1;
			int count = Mathf.Abs(num);
			this.m_prevMedalStars.PopulateFsmArrayWithStars(this.m_starLossFSM, "LostMedalStars", startIndex, count);
			this.m_starLossFSM.SendEvent("StartAnim");
			return;
		}
		if (num == 0)
		{
			if (this.m_currMedalDataModel.IsLegend)
			{
				this.m_currRankedMedalLegendText.Show();
			}
			else if (!this.m_currMedalDataModel.IsNewPlayer)
			{
				if (!canLoseStars)
				{
					this.m_cannotLoseStarText.gameObject.SetActive(true);
				}
				else if (!canLoseLevel)
				{
					this.m_cannotLoseLevelText.gameObject.SetActive(true);
				}
			}
			this.EnableClickToContinue();
			return;
		}
		if (num == 1)
		{
			if (this.m_prevMedalDataModel.Stars > 0)
			{
				this.m_prevMedalStars.PopulateFsmArrayWithStars(this.m_starGainSingleFSM, "AlreadyEarnedMedalStars", 0, this.m_prevMedalDataModel.Stars);
			}
			this.m_prevMedalStars.PopulateFsmArrayWithStars(this.m_starGainSingleFSM, "NewlyEarnedMedalStars", this.m_prevMedalDataModel.Stars, num);
			this.m_starGainSingleFSM.SendEvent("StartAnim");
			return;
		}
		this.PopuplateBasicFsmVars(this.m_starGainMultiFSM);
		this.m_prevMedalStars.PopulateFsmArrayWithStars(this.m_starGainMultiFSM, "UnearnedMedalStars", this.m_prevMedalDataModel.Stars, num);
		this.m_newlyEarnedStars.PopulateFsmArrayWithStars(this.m_starGainMultiFSM, "NewlyEarnedStars", 0, 0);
		this.m_starGainMultiFSM.SendEvent("StartAnim");
	}

	// Token: 0x06005A88 RID: 23176 RVA: 0x001D8CA0 File Offset: 0x001D6EA0
	private void PopuplateBasicFsmVars(PlayMakerFSM fsm)
	{
		FsmBool fsmBool = fsm.FsmVariables.GetFsmBool("IsWinStreak");
		if (fsmBool != null)
		{
			fsmBool.Value = this.m_isOnWinStreak;
		}
		FsmInt fsmInt = fsm.FsmVariables.GetFsmInt("StarMultiplier");
		if (fsmInt != null)
		{
			fsmInt.Value = this.m_prevMedalDataModel.StarMultiplier;
		}
		FsmBool fsmBool2 = fsm.FsmVariables.GetFsmBool("IsLegend");
		if (fsmBool2 != null)
		{
			fsmBool2.Value = this.m_currMedalDataModel.IsLegend;
		}
	}

	// Token: 0x06005A89 RID: 23177 RVA: 0x001D8D17 File Offset: 0x001D6F17
	private void HandleMissingRankChange()
	{
		this.EnableClickToContinue();
	}

	// Token: 0x06005A8A RID: 23178 RVA: 0x001D8D1F File Offset: 0x001D6F1F
	public static void DebugShowFake(int leagueId, int starLevel, int stars, int starsPerWin, FormatType formatType, bool isWinStreak, bool showWin)
	{
		RankChangeTwoScoop_NEW.DebugShowHelper(MedalInfoTranslator.DebugCreateMedalInfo(leagueId, starLevel, stars, starsPerWin, formatType, isWinStreak, showWin), formatType);
	}

	// Token: 0x06005A8B RID: 23179 RVA: 0x001D8D38 File Offset: 0x001D6F38
	public static void DebugShowHelper(MedalInfoTranslator medalInfoTranslator, FormatType formatType)
	{
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			RankChangeTwoScoop_NEW component = go.GetComponent<RankChangeTwoScoop_NEW>();
			if (UniversalInputManager.UsePhoneUI)
			{
				component.transform.localPosition = new Vector3(0f, 156.5f, 1.34f);
			}
			else
			{
				component.transform.localPosition = new Vector3(0f, 292f, -9f);
			}
			component.ActivateDebugEquivalentsOfEndGameScreen();
			component.Initialize(medalInfoTranslator, formatType, null);
			component.Show();
		};
		AssetLoader.Get().InstantiatePrefab(RankMgr.RANK_CHANGE_TWO_SCOOP_PREFAB_NEW, callback, null, AssetLoadingOptions.None);
	}

	// Token: 0x06005A8C RID: 23180 RVA: 0x001D8D77 File Offset: 0x001D6F77
	private void ActivateDebugEquivalentsOfEndGameScreen()
	{
		this.m_isRankChangeCheat = true;
		FullScreenFXMgr.Get().SetBlurDesaturation(0.5f);
		FullScreenFXMgr.Get().Blur(1f, 0.5f, iTween.EaseType.easeInCirc, null);
		this.m_debugClickCatcher.gameObject.SetActive(true);
	}

	// Token: 0x06005A8D RID: 23181 RVA: 0x001D8DB8 File Offset: 0x001D6FB8
	[ContextMenu("Test StarLoss")]
	private void TestStarLoss()
	{
		this.m_isRankChanging = false;
		this.m_prevMedalDataModel = this.PrepareFakeDataModel();
		this.m_currMedalDataModel = this.PrepareFakeDataModel();
		if (this.m_currMedalDataModel.Stars == 0)
		{
			this.m_prevMedalDataModel.Stars = 1;
		}
		else
		{
			this.m_currMedalDataModel.Stars--;
		}
		this.TestShow(delegate(object _)
		{
			this.PlayStarChange(this.m_testCanLoseStars, this.m_testCanLoseLevel);
		});
	}

	// Token: 0x06005A8E RID: 23182 RVA: 0x001D8E28 File Offset: 0x001D7028
	[ContextMenu("Test StarGainSingle")]
	private void TestStarGainSingle()
	{
		this.m_isRankChanging = false;
		this.m_isOnWinStreak = this.m_testWinStreak;
		this.m_prevMedalDataModel = this.PrepareFakeDataModel();
		this.m_currMedalDataModel = this.PrepareFakeDataModel();
		this.m_currMedalDataModel.Stars++;
		this.m_currMedalDataModel.Stars = Mathf.Max(3, this.m_currMedalDataModel.Stars);
		this.TestShow(delegate(object _)
		{
			this.PlayStarChange(this.m_testCanLoseStars, this.m_testCanLoseLevel);
		});
	}

	// Token: 0x06005A8F RID: 23183 RVA: 0x001D8EA4 File Offset: 0x001D70A4
	[ContextMenu("Test StarGainMulti")]
	private void TestStarGainMulti()
	{
		this.m_isRankChanging = false;
		this.m_isOnWinStreak = this.m_testWinStreak;
		this.m_prevMedalDataModel = this.PrepareFakeDataModel();
		this.m_currMedalDataModel = this.PrepareFakeDataModel();
		int num = this.m_testStarMultiplier;
		if (this.m_testWinStreak)
		{
			num *= 2;
		}
		this.m_currMedalDataModel.Stars += num;
		this.m_currMedalDataModel.Stars = Mathf.Max(3, this.m_currMedalDataModel.Stars);
		this.TestShow(delegate(object _)
		{
			this.PlayStarChange(this.m_testCanLoseStars, this.m_testCanLoseLevel);
		});
	}

	// Token: 0x06005A90 RID: 23184 RVA: 0x001D8F34 File Offset: 0x001D7134
	[ContextMenu("Test RankUp")]
	private void TestRankUp()
	{
		this.m_isRankChanging = true;
		this.m_isOnWinStreak = this.m_testWinStreak;
		this.m_prevMedalDataModel = this.PrepareFakeDataModel();
		this.m_currMedalDataModel = this.PrepareFakeDataModel();
		int num = this.m_testStarMultiplier;
		if (this.m_testWinStreak)
		{
			num *= 2;
		}
		num -= this.m_currMedalDataModel.MaxStars - this.m_currMedalDataModel.Stars;
		num = Mathf.Max(1, num);
		int num2 = num / 3;
		int stars = num % 3;
		this.m_currMedalDataModel.StarLevel += num2;
		this.m_currMedalDataModel.Stars = stars;
		this.TestShow(delegate(object _)
		{
			this.PlayRankUp();
		});
	}

	// Token: 0x06005A91 RID: 23185 RVA: 0x001D8FDC File Offset: 0x001D71DC
	[ContextMenu("Test RankDown")]
	private void TestRankDown()
	{
		this.m_isRankChanging = true;
		this.m_prevMedalDataModel = this.PrepareFakeDataModel();
		this.m_currMedalDataModel = this.PrepareFakeDataModel();
		this.m_currMedalDataModel.StarLevel--;
		this.m_currMedalDataModel.StarLevel = Mathf.Max(1, this.m_currMedalDataModel.StarLevel);
		this.m_currMedalDataModel.Stars = 2;
		this.TestShow(delegate(object _)
		{
			this.PlayRankDown();
		});
	}

	// Token: 0x06005A92 RID: 23186 RVA: 0x001D9058 File Offset: 0x001D7258
	private bool TestShow(Action<object> showFunc)
	{
		if (this.m_medalInfoTranslator == null)
		{
			this.m_medalInfoTranslator = new MedalInfoTranslator();
		}
		this.Reset();
		if (this.m_prevRankedMedalWidget != null)
		{
			this.m_prevRankedMedalWidget.BindDataModel(this.m_prevMedalDataModel, false);
		}
		if (this.m_isRankChanging && this.m_currRankedMedalWidget != null)
		{
			this.m_currRankedMedalWidget.BindDataModel(this.m_currMedalDataModel, false);
		}
		this.InitializeFromDataModels();
		base.StartCoroutine(this.ShowWhenReady(showFunc));
		return true;
	}

	// Token: 0x06005A93 RID: 23187 RVA: 0x001D90DC File Offset: 0x001D72DC
	private RankedPlayDataModel PrepareFakeDataModel()
	{
		RankedPlayDataModel dataModel = new RankedPlayDataModel();
		dataModel.Stars = this.m_testStars;
		dataModel.MaxStars = 3;
		dataModel.StarMultiplier = this.m_testStarMultiplier;
		dataModel.StarLevel = this.m_testStarLevel;
		dataModel.MedalText = this.m_testStarLevel.ToString();
		dataModel.RankName = this.m_testStarLevel.ToString();
		dataModel.IsNewPlayer = (this.m_testLeagueType == League.LeagueType.NEW_PLAYER);
		dataModel.IsLegend = this.m_testLegend;
		dataModel.LegendRank = 1337;
		dataModel.FormatType = this.m_formatType;
		AssetLoader.ObjectCallback callback = delegate(AssetReference assetRef, UnityEngine.Object textureObj, object data)
		{
			dataModel.MedalTexture = (textureObj as Texture);
		};
		AssetLoader.Get().LoadTexture("Medal_Key_6.tif:1a7672de24da5bc4caea4d847cd690d0", callback, null, false, false);
		return dataModel;
	}

	// Token: 0x04004D47 RID: 19783
	[CustomEditField(Sections = "Playmaker Test Data")]
	public League.LeagueType m_testLeagueType = League.LeagueType.NORMAL;

	// Token: 0x04004D48 RID: 19784
	[CustomEditField(Sections = "Playmaker Test Data")]
	public int m_testStarLevel = 1;

	// Token: 0x04004D49 RID: 19785
	[CustomEditField(Sections = "Playmaker Test Data")]
	public int m_testStars;

	// Token: 0x04004D4A RID: 19786
	[CustomEditField(Sections = "Playmaker Test Data")]
	public int m_testStarMultiplier = 1;

	// Token: 0x04004D4B RID: 19787
	[CustomEditField(Sections = "Playmaker Test Data")]
	public bool m_testWinStreak;

	// Token: 0x04004D4C RID: 19788
	[CustomEditField(Sections = "Playmaker Test Data")]
	public bool m_testLegend;

	// Token: 0x04004D4D RID: 19789
	[CustomEditField(Sections = "Playmaker Test Data")]
	public bool m_testWild;

	// Token: 0x04004D4E RID: 19790
	[CustomEditField(Sections = "Playmaker Test Data")]
	public bool m_testCanLoseStars;

	// Token: 0x04004D4F RID: 19791
	[CustomEditField(Sections = "Playmaker Test Data")]
	public bool m_testCanLoseLevel;

	// Token: 0x04004D50 RID: 19792
	[CustomEditField(Sections = "Animate In")]
	public Vector3_MobileOverride m_startScale;

	// Token: 0x04004D51 RID: 19793
	[CustomEditField(Sections = "Animate In")]
	public Vector3_MobileOverride m_punchScale;

	// Token: 0x04004D52 RID: 19794
	[CustomEditField(Sections = "Animate In")]
	public Vector3_MobileOverride m_afterPunchScale;

	// Token: 0x04004D53 RID: 19795
	[CustomEditField(Sections = "Banner")]
	public Float_MobileOverride m_bannerTextWidthMult;

	// Token: 0x04004D54 RID: 19796
	[CustomEditField(Sections = "Click Blocker")]
	public float m_maxAnimTimeBeforeClickToContinue = 3f;

	// Token: 0x04004D55 RID: 19797
	public AsyncReference m_prevRankedMedalWidgetReference;

	// Token: 0x04004D56 RID: 19798
	public AsyncReference m_currRankedMedalWidgetReference;

	// Token: 0x04004D57 RID: 19799
	public AsyncReference m_currRankedMedalLegendTextReference;

	// Token: 0x04004D58 RID: 19800
	public RankedStarArray m_prevMedalStars;

	// Token: 0x04004D59 RID: 19801
	public RankedStarArray m_currMedalStars;

	// Token: 0x04004D5A RID: 19802
	public RankedStarArray m_newlyEarnedStars;

	// Token: 0x04004D5B RID: 19803
	public RankedStarArray m_newlyEarnedStarsForRankUpRow1;

	// Token: 0x04004D5C RID: 19804
	public RankedStarArray m_newlyEarnedStarsForRankUpRow2;

	// Token: 0x04004D5D RID: 19805
	public GameObject m_medalGodRays;

	// Token: 0x04004D5E RID: 19806
	public GameObject m_banner;

	// Token: 0x04004D5F RID: 19807
	public UberText m_prevBannerText;

	// Token: 0x04004D60 RID: 19808
	public UberText m_currBannerText;

	// Token: 0x04004D61 RID: 19809
	public PlayMakerFSM m_mainFSM;

	// Token: 0x04004D62 RID: 19810
	public PlayMakerFSM m_starLossFSM;

	// Token: 0x04004D63 RID: 19811
	public PlayMakerFSM m_starGainSingleFSM;

	// Token: 0x04004D64 RID: 19812
	public PlayMakerFSM m_starGainMultiFSM;

	// Token: 0x04004D65 RID: 19813
	public PlayMakerFSM m_rankUpFSM;

	// Token: 0x04004D66 RID: 19814
	public PlayMakerFSM m_rankDownFSM;

	// Token: 0x04004D67 RID: 19815
	public UberText m_winStreakText;

	// Token: 0x04004D68 RID: 19816
	public UberText m_starMultiplierText;

	// Token: 0x04004D69 RID: 19817
	public UberText m_cannotLoseStarText;

	// Token: 0x04004D6A RID: 19818
	public UberText m_cannotLoseLevelText;

	// Token: 0x04004D6B RID: 19819
	public PegUIElement m_debugClickCatcher;

	// Token: 0x04004D6C RID: 19820
	private MedalInfoTranslator m_medalInfoTranslator;

	// Token: 0x04004D6D RID: 19821
	private FormatType m_formatType = FormatType.FT_STANDARD;

	// Token: 0x04004D6E RID: 19822
	private TranslatedMedalInfo m_currMedalInfo;

	// Token: 0x04004D6F RID: 19823
	private TranslatedMedalInfo m_prevMedalInfo;

	// Token: 0x04004D70 RID: 19824
	private RankedPlayDataModel m_currMedalDataModel;

	// Token: 0x04004D71 RID: 19825
	private RankedPlayDataModel m_prevMedalDataModel;

	// Token: 0x04004D72 RID: 19826
	private RankedMedal m_currRankedMedal;

	// Token: 0x04004D73 RID: 19827
	private RankedMedal m_prevRankedMedal;

	// Token: 0x04004D74 RID: 19828
	private Widget m_currRankedMedalWidget;

	// Token: 0x04004D75 RID: 19829
	private Widget m_prevRankedMedalWidget;

	// Token: 0x04004D76 RID: 19830
	private UberText m_currRankedMedalLegendText;

	// Token: 0x04004D77 RID: 19831
	private bool m_isRankChanging;

	// Token: 0x04004D78 RID: 19832
	private bool m_isOnWinStreak;

	// Token: 0x04004D79 RID: 19833
	private Action m_closedCallback;

	// Token: 0x04004D7A RID: 19834
	private bool m_isRankChangeCheat;

	// Token: 0x04004D7B RID: 19835
	private Coroutine m_clickToContinueCoroutine;

	// Token: 0x04004D7C RID: 19836
	private bool m_isPlayingAnimWithCancelPoint;
}
