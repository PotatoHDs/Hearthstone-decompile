using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

// Token: 0x020002AC RID: 684
[RequireComponent(typeof(WidgetTemplate))]
public class RankedPlayDisplay : MonoBehaviour
{
	// Token: 0x06002383 RID: 9091 RVA: 0x000B1695 File Offset: 0x000AF895
	private void Awake()
	{
		this.m_currentVisualsFormatType = VisualsFormatTypeExtensions.ToVisualsFormatType(Options.GetFormatType(), Options.GetInRankedPlayMode());
		this.m_widget = base.GetComponent<WidgetTemplate>();
		this.m_widget.RegisterEventListener(new Widget.EventListenerDelegate(this.OnRankedPlayDisplayEvent));
	}

	// Token: 0x06002384 RID: 9092 RVA: 0x000B16D0 File Offset: 0x000AF8D0
	private void Start()
	{
		this.UpdateRankContainerVisualController();
		this.m_rewardsContainerWidget.RegisterReadyListener(delegate(object _)
		{
			this.UpdateRewardsContainerWidget();
		}, null, true);
		this.m_rankedMedalWidgetReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnRankedMedalWidgetReady));
		this.m_starMultiplierWidgetReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnStarMultiplierWidgetReady));
	}

	// Token: 0x06002385 RID: 9093 RVA: 0x000B172A File Offset: 0x000AF92A
	private void OnDestroy()
	{
		this.DestroyRankedRewardsList();
	}

	// Token: 0x06002386 RID: 9094 RVA: 0x000B1732 File Offset: 0x000AF932
	public void UpdateMode(VisualsFormatType newVisualsFormatType)
	{
		DeckPickerTrayDisplay.Get().UpdateRankedClassWinsPlate();
		if (UniversalInputManager.UsePhoneUI)
		{
			DeckPickerTrayDisplay.Get().ToggleRankedDetailsTray(newVisualsFormatType.IsRanked());
		}
		DeckPickerTrayDisplay.Get().SetPlayButtonText(GameStrings.Get("GLOBAL_PLAY"));
	}

	// Token: 0x06002387 RID: 9095 RVA: 0x000B1770 File Offset: 0x000AF970
	public void StartSetRotationTutorial()
	{
		this.m_inSetRotationTutorial = true;
		if (UniversalInputManager.UsePhoneUI)
		{
			DeckPickerTrayDisplay.Get().ToggleRankedDetailsTray(true);
		}
		this.m_currentVisualsFormatType = VisualsFormatType.VFT_STANDARD;
		this.Hide(0f);
		DeckPickerTrayDisplay.Get().SetPlayButtonText(GameStrings.Get("GLOBAL_PLAY"));
		DeckPickerTrayDisplay.Get().SetPlayButtonTextAlpha(0f);
		DeckPickerTrayDisplay.Get().UpdateRankedClassWinsPlate();
		this.OnMedalChanged(TournamentDisplay.Get().GetCurrentMedalInfo());
	}

	// Token: 0x06002388 RID: 9096 RVA: 0x000B17EC File Offset: 0x000AF9EC
	public void OnMedalChanged(NetCache.NetCacheMedalInfo medalInfo)
	{
		MedalInfoTranslator medalInfoTranslator = new MedalInfoTranslator(medalInfo, null);
		bool isTooltipEnabled = false;
		bool hasEarnedCardBack = medalInfoTranslator.HasEarnedSeasonCardBack();
		this.m_rankedChestDataModel = medalInfoTranslator.CreateDataModel(this.m_currentVisualsFormatType.ToFormatType(), RankedMedal.DisplayMode.Chest, isTooltipEnabled, hasEarnedCardBack, null);
		this.UpdateRankContainerVisualController();
		this.UpdateRewardsContainerWidget();
	}

	// Token: 0x06002389 RID: 9097 RVA: 0x000B1834 File Offset: 0x000AFA34
	public void UpdateRankContainerVisualController()
	{
		NetCache.NetCacheMedalInfo currentMedalInfo = TournamentDisplay.Get().GetCurrentMedalInfo();
		if (currentMedalInfo == null)
		{
			return;
		}
		MedalInfoTranslator medalInfoTranslator = new MedalInfoTranslator(currentMedalInfo, null);
		bool isTooltipEnabled = false;
		bool hasEarnedCardBack = medalInfoTranslator.HasEarnedSeasonCardBack();
		medalInfoTranslator.CreateDataModel(this.m_currentVisualsFormatType.ToFormatType(), RankedMedal.DisplayMode.Stars, isTooltipEnabled, hasEarnedCardBack, delegate(RankedPlayDataModel dm)
		{
			this.m_rankContainerVisualController.BindDataModel(dm, true, false);
			if (UniversalInputManager.UsePhoneUI && this.m_rankedMedalWidget != null && this.m_rankedMedalWidget.IsReady)
			{
				this.m_rankedMedalWidget.BindDataModel(dm, false);
			}
		});
	}

	// Token: 0x0600238A RID: 9098 RVA: 0x000B1880 File Offset: 0x000AFA80
	public void UpdateRewardsContainerWidget()
	{
		if (!this.m_rewardsContainerWidget.IsReady)
		{
			return;
		}
		if (this.m_rankedChestDataModel == null)
		{
			return;
		}
		this.m_rewardsContainerWidget.BindDataModel(this.m_rankedChestDataModel, false);
		if (this.m_isDesiredHidden)
		{
			this.Hide(0f);
			return;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_rewardsContainerWidget.SetLayerOverride(GameLayer.IgnoreFullScreenEffects);
		}
	}

	// Token: 0x0600238B RID: 9099 RVA: 0x000B18E3 File Offset: 0x000AFAE3
	public void OnSwitchFormat(VisualsFormatType newVisualsFormatType)
	{
		if (!this.m_inSetRotationTutorial)
		{
			if (this.m_currentVisualsFormatType != newVisualsFormatType)
			{
				this.m_currentVisualsFormatType = newVisualsFormatType;
				this.OnMedalChanged(TournamentDisplay.Get().GetCurrentMedalInfo());
			}
			this.UpdateMode(newVisualsFormatType);
		}
	}

	// Token: 0x0600238C RID: 9100 RVA: 0x000B1914 File Offset: 0x000AFB14
	public void Show(float delay = 0f)
	{
		if (!this.m_isDesiredHidden)
		{
			return;
		}
		this.m_isDesiredHidden = false;
		this.StopAndClearCoroutine(ref this.m_delayedVisibilityChange);
		if (delay > 0f)
		{
			this.m_delayedVisibilityChange = base.StartCoroutine(this.WaitThenSetVisibility(delay, true));
			return;
		}
		this.SetVisibility(true);
	}

	// Token: 0x0600238D RID: 9101 RVA: 0x000B1961 File Offset: 0x000AFB61
	public void Hide(float delay = 0f)
	{
		if (this.m_isDesiredHidden)
		{
			return;
		}
		this.m_isDesiredHidden = true;
		this.StopAndClearCoroutine(ref this.m_delayedVisibilityChange);
		if (delay > 0f)
		{
			base.StartCoroutine(this.WaitThenSetVisibility(delay, false));
			return;
		}
		this.SetVisibility(false);
	}

	// Token: 0x0600238E RID: 9102 RVA: 0x000B199E File Offset: 0x000AFB9E
	private IEnumerator WaitThenSetVisibility(float delay, bool visible)
	{
		yield return new WaitForSeconds(delay);
		this.SetVisibility(visible);
		yield break;
	}

	// Token: 0x0600238F RID: 9103 RVA: 0x000B19BB File Offset: 0x000AFBBB
	private void SetVisibility(bool visible)
	{
		if (visible)
		{
			this.m_widget.Show();
			this.m_rewardsContainerWidget.Show();
			return;
		}
		this.m_rewardsContainerWidget.Hide();
		this.m_widget.Hide();
	}

	// Token: 0x06002390 RID: 9104 RVA: 0x0007852C File Offset: 0x0007672C
	private void StopAndClearCoroutine(ref Coroutine co)
	{
		if (co != null)
		{
			base.StopCoroutine(co);
			co = null;
		}
	}

	// Token: 0x06002391 RID: 9105 RVA: 0x000B19F0 File Offset: 0x000AFBF0
	public void PlayTransitionGlowBurstsForNonNewDeckFSMs(string fxEvent)
	{
		foreach (PlayMakerFSM playMakerFSM in this.formatChangeGlowFSMs)
		{
			if (playMakerFSM != null)
			{
				playMakerFSM.SendEvent(fxEvent);
			}
		}
	}

	// Token: 0x06002392 RID: 9106 RVA: 0x000B1A4C File Offset: 0x000AFC4C
	public void PlayTransitionGlowBurstsForNewDeckFSMs(string fxEvent)
	{
		if (string.IsNullOrEmpty(fxEvent))
		{
			return;
		}
		foreach (PlayMakerFSM playMakerFSM in this.newDeckFormatChangeGlowFSMs)
		{
			if (playMakerFSM != null)
			{
				playMakerFSM.SendEvent(fxEvent);
			}
		}
	}

	// Token: 0x06002393 RID: 9107 RVA: 0x000B1AB4 File Offset: 0x000AFCB4
	private void OnRankedMedalWidgetReady(Widget widget)
	{
		this.m_rankedMedalWidget = widget;
		if (UniversalInputManager.UsePhoneUI)
		{
			widget.transform.parent = DeckPickerTrayDisplay.Get().m_medalBone_phone;
			widget.SetLayerOverride(GameLayer.IgnoreFullScreenEffects);
		}
		else
		{
			widget.transform.parent = this.m_medalBone;
		}
		widget.transform.localScale = Vector3.one;
		widget.transform.localPosition = Vector3.zero;
		this.OnMedalChanged(TournamentDisplay.Get().GetCurrentMedalInfo());
	}

	// Token: 0x06002394 RID: 9108 RVA: 0x000B1B34 File Offset: 0x000AFD34
	private void OnStarMultiplierWidgetReady(Widget widget)
	{
		this.m_starMultiplierWidget = widget;
	}

	// Token: 0x06002395 RID: 9109 RVA: 0x000B1B40 File Offset: 0x000AFD40
	private void OnRankedPlayDisplayEvent(string eventName)
	{
		if (!RankMgr.Get().IsNewPlayer())
		{
			if (eventName == "MEDAL_BUTTON_CLICKED")
			{
				DialogManager.Get().ShowRankedIntroPopUp(null);
			}
			else if (eventName == "SHOW_MEDAL_TOOLTIP")
			{
				this.ShowMedalTooltip();
			}
			else if (eventName == "HIDE_MEDAL_TOOLTIP")
			{
				this.HideMedalTooltip();
			}
			if (eventName == "CHEST_BUTTON_CLICKED")
			{
				base.StartCoroutine(this.ShowRankedRewardList());
				return;
			}
			if (eventName == "SHOW_CHEST_TOOLTIP")
			{
				this.ShowChestTooltip();
				return;
			}
			if (eventName == "HIDE_CHEST_TOOLTIP")
			{
				this.HideChestTooltip();
			}
		}
	}

	// Token: 0x06002396 RID: 9110 RVA: 0x000B1BE0 File Offset: 0x000AFDE0
	private void ShowMedalTooltip()
	{
		FormatType formatType = Options.GetFormatType();
		string bodytext;
		string key;
		if (this.m_rankedChestDataModel.IsLegend)
		{
			bodytext = GameStrings.Format("GLOBAL_MEDAL_TOOLTIP_BODY_LEGEND", Array.Empty<object>());
		}
		else if (new Map<FormatType, string>
		{
			{
				FormatType.FT_STANDARD,
				"GLOBAL_MEDAL_TOOLTIP_BODY_STANDARD"
			},
			{
				FormatType.FT_WILD,
				"GLOBAL_MEDAL_TOOLTIP_BODY_WILD"
			},
			{
				FormatType.FT_CLASSIC,
				"GLOBAL_MEDAL_TOOLTIP_BODY_CLASSIC"
			}
		}.TryGetValue(formatType, out key))
		{
			bodytext = GameStrings.Format(key, Array.Empty<object>());
		}
		else
		{
			bodytext = "UNKNOWN FORMAT TYPE " + formatType.ToString();
		}
		string key2;
		string headline;
		if (new Map<FormatType, string>
		{
			{
				FormatType.FT_STANDARD,
				"GLOBAL_MEDAL_TOOLTIP_BEST_RANK_STANDARD"
			},
			{
				FormatType.FT_WILD,
				"GLOBAL_MEDAL_TOOLTIP_BEST_RANK_WILD"
			},
			{
				FormatType.FT_CLASSIC,
				"GLOBAL_MEDAL_TOOLTIP_BEST_RANK_CLASSIC"
			}
		}.TryGetValue(formatType, out key2))
		{
			headline = GameStrings.Format(key2, new object[]
			{
				this.m_rankedChestDataModel.RankName
			});
		}
		else
		{
			headline = "UNKNOWN FORMAT TYPE " + this.m_rankedChestDataModel.FormatType.ToString();
		}
		TooltipZone component = this.m_rankContainerVisualController.GetComponent<TooltipZone>();
		component.ShowTooltip(headline, bodytext, 5f, 0);
		if (this.m_starMultiplierWidget != null && this.m_starMultiplierWidget.gameObject.activeInHierarchy && this.m_starMultiplierTooltipZone != null)
		{
			int starsPerWin = RankMgr.Get().GetLocalPlayerMedalInfo().GetCurrentMedal(formatType).starsPerWin;
			string headline2 = GameStrings.Format("GLUE_TOURNAMENT_STAR_MULT_HEAD", new object[]
			{
				starsPerWin
			});
			string bodytext2 = GameStrings.Format("GLUE_TOURNAMENT_STAR_MULT_BODY", new object[]
			{
				starsPerWin
			});
			this.m_starMultiplierTooltipZone.ShowTooltip(headline2, bodytext2, 5f, 0);
			this.m_starMultiplierTooltipZone.AnchorTooltipTo(component.GetTooltipObject(0), Anchor.BOTTOM_XZ, Anchor.TOP_XZ, 0);
		}
	}

	// Token: 0x06002397 RID: 9111 RVA: 0x000B1DAF File Offset: 0x000AFFAF
	private void HideMedalTooltip()
	{
		this.m_rankContainerVisualController.GetComponent<TooltipZone>().HideTooltip();
		if (this.m_starMultiplierTooltipZone != null)
		{
			this.m_starMultiplierTooltipZone.HideTooltip();
		}
	}

	// Token: 0x06002398 RID: 9112 RVA: 0x000B1DDA File Offset: 0x000AFFDA
	private IEnumerator ShowRankedRewardList()
	{
		if (this.m_isShowingRewardsList)
		{
			yield break;
		}
		this.m_isShowingRewardsList = true;
		if (this.m_rankedRewardListWidget == null)
		{
			this.m_rankedRewardListWidget = WidgetInstance.Create(RankMgr.RANKED_REWARD_LIST_POPUP, false);
			this.m_rankedRewardListWidget.RegisterReadyListener(delegate(object _)
			{
				this.OnRankedRewardListPopupWidgetReady();
			}, null, true);
			this.m_rankedRewardListWidget.WillLoadSynchronously = true;
			this.m_rankedRewardListWidget.Initialize();
		}
		while (this.m_rankedRewardList == null || this.m_rankedRewardListWidget.IsChangingStates)
		{
			yield return null;
		}
		UIContext.GetRoot().RegisterPopup(this.m_rankedRewardListWidget.gameObject, UIContext.RenderCameraType.OrthographicUI, UIContext.BlurType.Standard);
		this.m_rankedRewardListWidget.Show();
		this.m_rankedRewardListWidget.TriggerEvent("SHOW", default(Widget.TriggerEventParameters));
		yield return new WaitForSeconds(0.25f);
		yield break;
	}

	// Token: 0x06002399 RID: 9113 RVA: 0x000B1DEC File Offset: 0x000AFFEC
	private void OnRankedRewardListPopupWidgetReady()
	{
		OverlayUI.Get().AddGameObject(this.m_rankedRewardListWidget.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
		this.m_rankedRewardListWidget.transform.localPosition = this.m_rewardListPos;
		float aspectRatioDependentValue = TransformUtil.GetAspectRatioDependentValue(this.m_rewardListScaleSmall, this.m_rewardListScaleWide, this.m_rewardListScaleExtraWide);
		this.m_rankedRewardListWidget.transform.localScale = Vector3.one * aspectRatioDependentValue * this.m_rewardListDeviceScale;
		this.m_rankedRewardListWidget.RegisterEventListener(new Widget.EventListenerDelegate(this.WidgetEventListener_RewardsList));
		this.m_rankedRewardList = this.m_rankedRewardListWidget.GetComponentInChildren<RankedRewardList>();
		this.m_rankedRewardListWidget.Hide();
		this.UpdateRankedRewardList();
	}

	// Token: 0x0600239A RID: 9114 RVA: 0x000B1E9E File Offset: 0x000B009E
	private void WidgetEventListener_RewardsList(string eventName)
	{
		if (eventName.Equals("HIDE"))
		{
			this.HideRankedRewardsList();
		}
	}

	// Token: 0x0600239B RID: 9115 RVA: 0x000B1EB3 File Offset: 0x000B00B3
	private void HideRankedRewardsList()
	{
		UIContext.GetRoot().UnregisterPopup(this.m_rankedRewardListWidget.gameObject);
		this.m_isShowingRewardsList = false;
	}

	// Token: 0x0600239C RID: 9116 RVA: 0x000B1ED1 File Offset: 0x000B00D1
	private void UpdateRankedRewardList()
	{
		if (this.m_rankedRewardList != null)
		{
			this.m_rankedRewardList.Initialize(new MedalInfoTranslator(TournamentDisplay.Get().GetCurrentMedalInfo(), null));
		}
	}

	// Token: 0x0600239D RID: 9117 RVA: 0x000B1EFC File Offset: 0x000B00FC
	private void DestroyRankedRewardsList()
	{
		if (this.m_rankedRewardListWidget != null)
		{
			UnityEngine.Object.Destroy(this.m_rankedRewardListWidget.gameObject);
		}
		this.m_isShowingRewardsList = false;
	}

	// Token: 0x0600239E RID: 9118 RVA: 0x000B1F23 File Offset: 0x000B0123
	private void ShowChestTooltip()
	{
		this.m_rewardsContainerWidget.GetComponent<TooltipZone>().ShowTooltip(GameStrings.Get("GLOBAL_PROGRESSION_RANKED_REWARDS_TOOLTIP_TITLE"), GameStrings.Get("GLOBAL_PROGRESSION_RANKED_REWARDS_TOOLTIP"), 5f, 0);
	}

	// Token: 0x0600239F RID: 9119 RVA: 0x000B1F50 File Offset: 0x000B0150
	private void HideChestTooltip()
	{
		this.m_rewardsContainerWidget.GetComponent<TooltipZone>().HideTooltip();
	}

	// Token: 0x040013AA RID: 5034
	public Transform m_medalBone;

	// Token: 0x040013AB RID: 5035
	public VisualController m_rankContainerVisualController;

	// Token: 0x040013AC RID: 5036
	public Widget m_rewardsContainerWidget;

	// Token: 0x040013AD RID: 5037
	public AsyncReference m_rankedMedalWidgetReference;

	// Token: 0x040013AE RID: 5038
	public AsyncReference m_starMultiplierWidgetReference;

	// Token: 0x040013AF RID: 5039
	public TooltipZone m_starMultiplierTooltipZone;

	// Token: 0x040013B0 RID: 5040
	public Vector3 m_rewardListPos;

	// Token: 0x040013B1 RID: 5041
	public float m_rewardListDeviceScale = 1f;

	// Token: 0x040013B2 RID: 5042
	public float m_rewardListScaleSmall = 1f;

	// Token: 0x040013B3 RID: 5043
	public float m_rewardListScaleWide = 1f;

	// Token: 0x040013B4 RID: 5044
	public float m_rewardListScaleExtraWide = 1f;

	// Token: 0x040013B5 RID: 5045
	public List<PlayMakerFSM> formatChangeGlowFSMs;

	// Token: 0x040013B6 RID: 5046
	public List<PlayMakerFSM> newDeckFormatChangeGlowFSMs;

	// Token: 0x040013B7 RID: 5047
	private bool m_inSetRotationTutorial;

	// Token: 0x040013B8 RID: 5048
	private VisualsFormatType m_currentVisualsFormatType;

	// Token: 0x040013B9 RID: 5049
	private RankedPlayDataModel m_rankedChestDataModel;

	// Token: 0x040013BA RID: 5050
	private Widget m_starMultiplierWidget;

	// Token: 0x040013BB RID: 5051
	private Widget m_rankedMedalWidget;

	// Token: 0x040013BC RID: 5052
	private Widget m_widget;

	// Token: 0x040013BD RID: 5053
	private WidgetInstance m_rankedRewardListWidget;

	// Token: 0x040013BE RID: 5054
	private RankedRewardList m_rankedRewardList;

	// Token: 0x040013BF RID: 5055
	private bool m_isShowingRewardsList;

	// Token: 0x040013C0 RID: 5056
	private bool m_isDesiredHidden;

	// Token: 0x040013C1 RID: 5057
	private Coroutine m_delayedVisibilityChange;

	// Token: 0x040013C2 RID: 5058
	private const string MEDAL_BUTTON_CLICKED = "MEDAL_BUTTON_CLICKED";

	// Token: 0x040013C3 RID: 5059
	private const string SHOW_MEDAL_TOOLTIP = "SHOW_MEDAL_TOOLTIP";

	// Token: 0x040013C4 RID: 5060
	private const string HIDE_MEDAL_TOOLTIP = "HIDE_MEDAL_TOOLTIP";

	// Token: 0x040013C5 RID: 5061
	private const string CHEST_BUTTON_CLICKED = "CHEST_BUTTON_CLICKED";

	// Token: 0x040013C6 RID: 5062
	private const string SHOW_CHEST_TOOLTIP = "SHOW_CHEST_TOOLTIP";

	// Token: 0x040013C7 RID: 5063
	private const string HIDE_CHEST_TOOLTIP = "HIDE_CHEST_TOOLTIP";
}
