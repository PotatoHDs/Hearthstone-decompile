using System.Collections;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

[RequireComponent(typeof(WidgetTemplate))]
public class RankedPlayDisplay : MonoBehaviour
{
	public Transform m_medalBone;

	public VisualController m_rankContainerVisualController;

	public Widget m_rewardsContainerWidget;

	public AsyncReference m_rankedMedalWidgetReference;

	public AsyncReference m_starMultiplierWidgetReference;

	public TooltipZone m_starMultiplierTooltipZone;

	public Vector3 m_rewardListPos;

	public float m_rewardListDeviceScale = 1f;

	public float m_rewardListScaleSmall = 1f;

	public float m_rewardListScaleWide = 1f;

	public float m_rewardListScaleExtraWide = 1f;

	public List<PlayMakerFSM> formatChangeGlowFSMs;

	public List<PlayMakerFSM> newDeckFormatChangeGlowFSMs;

	private bool m_inSetRotationTutorial;

	private VisualsFormatType m_currentVisualsFormatType;

	private RankedPlayDataModel m_rankedChestDataModel;

	private Widget m_starMultiplierWidget;

	private Widget m_rankedMedalWidget;

	private Widget m_widget;

	private WidgetInstance m_rankedRewardListWidget;

	private RankedRewardList m_rankedRewardList;

	private bool m_isShowingRewardsList;

	private bool m_isDesiredHidden;

	private Coroutine m_delayedVisibilityChange;

	private const string MEDAL_BUTTON_CLICKED = "MEDAL_BUTTON_CLICKED";

	private const string SHOW_MEDAL_TOOLTIP = "SHOW_MEDAL_TOOLTIP";

	private const string HIDE_MEDAL_TOOLTIP = "HIDE_MEDAL_TOOLTIP";

	private const string CHEST_BUTTON_CLICKED = "CHEST_BUTTON_CLICKED";

	private const string SHOW_CHEST_TOOLTIP = "SHOW_CHEST_TOOLTIP";

	private const string HIDE_CHEST_TOOLTIP = "HIDE_CHEST_TOOLTIP";

	private void Awake()
	{
		m_currentVisualsFormatType = VisualsFormatTypeExtensions.ToVisualsFormatType(Options.GetFormatType(), Options.GetInRankedPlayMode());
		m_widget = GetComponent<WidgetTemplate>();
		m_widget.RegisterEventListener(OnRankedPlayDisplayEvent);
	}

	private void Start()
	{
		UpdateRankContainerVisualController();
		m_rewardsContainerWidget.RegisterReadyListener(delegate
		{
			UpdateRewardsContainerWidget();
		});
		m_rankedMedalWidgetReference.RegisterReadyListener<Widget>(OnRankedMedalWidgetReady);
		m_starMultiplierWidgetReference.RegisterReadyListener<Widget>(OnStarMultiplierWidgetReady);
	}

	private void OnDestroy()
	{
		DestroyRankedRewardsList();
	}

	public void UpdateMode(VisualsFormatType newVisualsFormatType)
	{
		DeckPickerTrayDisplay.Get().UpdateRankedClassWinsPlate();
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			DeckPickerTrayDisplay.Get().ToggleRankedDetailsTray(newVisualsFormatType.IsRanked());
		}
		DeckPickerTrayDisplay.Get().SetPlayButtonText(GameStrings.Get("GLOBAL_PLAY"));
	}

	public void StartSetRotationTutorial()
	{
		m_inSetRotationTutorial = true;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			DeckPickerTrayDisplay.Get().ToggleRankedDetailsTray(shown: true);
		}
		m_currentVisualsFormatType = VisualsFormatType.VFT_STANDARD;
		Hide();
		DeckPickerTrayDisplay.Get().SetPlayButtonText(GameStrings.Get("GLOBAL_PLAY"));
		DeckPickerTrayDisplay.Get().SetPlayButtonTextAlpha(0f);
		DeckPickerTrayDisplay.Get().UpdateRankedClassWinsPlate();
		OnMedalChanged(TournamentDisplay.Get().GetCurrentMedalInfo());
	}

	public void OnMedalChanged(NetCache.NetCacheMedalInfo medalInfo)
	{
		MedalInfoTranslator medalInfoTranslator = new MedalInfoTranslator(medalInfo);
		bool isTooltipEnabled = false;
		bool hasEarnedCardBack = medalInfoTranslator.HasEarnedSeasonCardBack();
		m_rankedChestDataModel = medalInfoTranslator.CreateDataModel(m_currentVisualsFormatType.ToFormatType(), RankedMedal.DisplayMode.Chest, isTooltipEnabled, hasEarnedCardBack);
		UpdateRankContainerVisualController();
		UpdateRewardsContainerWidget();
	}

	public void UpdateRankContainerVisualController()
	{
		NetCache.NetCacheMedalInfo currentMedalInfo = TournamentDisplay.Get().GetCurrentMedalInfo();
		if (currentMedalInfo == null)
		{
			return;
		}
		MedalInfoTranslator medalInfoTranslator = new MedalInfoTranslator(currentMedalInfo);
		medalInfoTranslator.CreateDataModel(isTooltipEnabled: false, hasEarnedCardBack: medalInfoTranslator.HasEarnedSeasonCardBack(), formatType: m_currentVisualsFormatType.ToFormatType(), mode: RankedMedal.DisplayMode.Stars, dataModelReadyCallback: delegate(RankedPlayDataModel dm)
		{
			m_rankContainerVisualController.BindDataModel(dm);
			if ((bool)UniversalInputManager.UsePhoneUI && m_rankedMedalWidget != null && m_rankedMedalWidget.IsReady)
			{
				m_rankedMedalWidget.BindDataModel(dm);
			}
		});
	}

	public void UpdateRewardsContainerWidget()
	{
		if (m_rewardsContainerWidget.IsReady && m_rankedChestDataModel != null)
		{
			m_rewardsContainerWidget.BindDataModel(m_rankedChestDataModel);
			if (m_isDesiredHidden)
			{
				Hide();
			}
			else if ((bool)UniversalInputManager.UsePhoneUI)
			{
				m_rewardsContainerWidget.SetLayerOverride(GameLayer.IgnoreFullScreenEffects);
			}
		}
	}

	public void OnSwitchFormat(VisualsFormatType newVisualsFormatType)
	{
		if (!m_inSetRotationTutorial)
		{
			if (m_currentVisualsFormatType != newVisualsFormatType)
			{
				m_currentVisualsFormatType = newVisualsFormatType;
				OnMedalChanged(TournamentDisplay.Get().GetCurrentMedalInfo());
			}
			UpdateMode(newVisualsFormatType);
		}
	}

	public void Show(float delay = 0f)
	{
		if (m_isDesiredHidden)
		{
			m_isDesiredHidden = false;
			StopAndClearCoroutine(ref m_delayedVisibilityChange);
			if (delay > 0f)
			{
				m_delayedVisibilityChange = StartCoroutine(WaitThenSetVisibility(delay, visible: true));
			}
			else
			{
				SetVisibility(visible: true);
			}
		}
	}

	public void Hide(float delay = 0f)
	{
		if (!m_isDesiredHidden)
		{
			m_isDesiredHidden = true;
			StopAndClearCoroutine(ref m_delayedVisibilityChange);
			if (delay > 0f)
			{
				StartCoroutine(WaitThenSetVisibility(delay, visible: false));
			}
			else
			{
				SetVisibility(visible: false);
			}
		}
	}

	private IEnumerator WaitThenSetVisibility(float delay, bool visible)
	{
		yield return new WaitForSeconds(delay);
		SetVisibility(visible);
	}

	private void SetVisibility(bool visible)
	{
		if (visible)
		{
			m_widget.Show();
			m_rewardsContainerWidget.Show();
		}
		else
		{
			m_rewardsContainerWidget.Hide();
			m_widget.Hide();
		}
	}

	private void StopAndClearCoroutine(ref Coroutine co)
	{
		if (co != null)
		{
			StopCoroutine(co);
			co = null;
		}
	}

	public void PlayTransitionGlowBurstsForNonNewDeckFSMs(string fxEvent)
	{
		foreach (PlayMakerFSM formatChangeGlowFSM in formatChangeGlowFSMs)
		{
			if (formatChangeGlowFSM != null)
			{
				formatChangeGlowFSM.SendEvent(fxEvent);
			}
		}
	}

	public void PlayTransitionGlowBurstsForNewDeckFSMs(string fxEvent)
	{
		if (string.IsNullOrEmpty(fxEvent))
		{
			return;
		}
		foreach (PlayMakerFSM newDeckFormatChangeGlowFSM in newDeckFormatChangeGlowFSMs)
		{
			if (newDeckFormatChangeGlowFSM != null)
			{
				newDeckFormatChangeGlowFSM.SendEvent(fxEvent);
			}
		}
	}

	private void OnRankedMedalWidgetReady(Widget widget)
	{
		m_rankedMedalWidget = widget;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			widget.transform.parent = DeckPickerTrayDisplay.Get().m_medalBone_phone;
			widget.SetLayerOverride(GameLayer.IgnoreFullScreenEffects);
		}
		else
		{
			widget.transform.parent = m_medalBone;
		}
		widget.transform.localScale = Vector3.one;
		widget.transform.localPosition = Vector3.zero;
		OnMedalChanged(TournamentDisplay.Get().GetCurrentMedalInfo());
	}

	private void OnStarMultiplierWidgetReady(Widget widget)
	{
		m_starMultiplierWidget = widget;
	}

	private void OnRankedPlayDisplayEvent(string eventName)
	{
		if (!RankMgr.Get().IsNewPlayer())
		{
			switch (eventName)
			{
			case "MEDAL_BUTTON_CLICKED":
				DialogManager.Get().ShowRankedIntroPopUp(null);
				break;
			case "SHOW_MEDAL_TOOLTIP":
				ShowMedalTooltip();
				break;
			case "HIDE_MEDAL_TOOLTIP":
				HideMedalTooltip();
				break;
			}
			switch (eventName)
			{
			case "CHEST_BUTTON_CLICKED":
				StartCoroutine(ShowRankedRewardList());
				break;
			case "SHOW_CHEST_TOOLTIP":
				ShowChestTooltip();
				break;
			case "HIDE_CHEST_TOOLTIP":
				HideChestTooltip();
				break;
			}
		}
	}

	private void ShowMedalTooltip()
	{
		FormatType formatType = Options.GetFormatType();
		string value;
		string bodytext = (m_rankedChestDataModel.IsLegend ? GameStrings.Format("GLOBAL_MEDAL_TOOLTIP_BODY_LEGEND") : ((!new Map<FormatType, string>
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
		}.TryGetValue(formatType, out value)) ? ("UNKNOWN FORMAT TYPE " + formatType) : GameStrings.Format(value)));
		string value2;
		string headline = ((!new Map<FormatType, string>
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
		}.TryGetValue(formatType, out value2)) ? ("UNKNOWN FORMAT TYPE " + m_rankedChestDataModel.FormatType) : GameStrings.Format(value2, m_rankedChestDataModel.RankName));
		TooltipZone component = m_rankContainerVisualController.GetComponent<TooltipZone>();
		component.ShowTooltip(headline, bodytext, 5f);
		if (m_starMultiplierWidget != null && m_starMultiplierWidget.gameObject.activeInHierarchy && m_starMultiplierTooltipZone != null)
		{
			int starsPerWin = RankMgr.Get().GetLocalPlayerMedalInfo().GetCurrentMedal(formatType)
				.starsPerWin;
			string headline2 = GameStrings.Format("GLUE_TOURNAMENT_STAR_MULT_HEAD", starsPerWin);
			string bodytext2 = GameStrings.Format("GLUE_TOURNAMENT_STAR_MULT_BODY", starsPerWin);
			m_starMultiplierTooltipZone.ShowTooltip(headline2, bodytext2, 5f);
			m_starMultiplierTooltipZone.AnchorTooltipTo(component.GetTooltipObject(), Anchor.BOTTOM_XZ, Anchor.TOP_XZ);
		}
	}

	private void HideMedalTooltip()
	{
		m_rankContainerVisualController.GetComponent<TooltipZone>().HideTooltip();
		if (m_starMultiplierTooltipZone != null)
		{
			m_starMultiplierTooltipZone.HideTooltip();
		}
	}

	private IEnumerator ShowRankedRewardList()
	{
		if (m_isShowingRewardsList)
		{
			yield break;
		}
		m_isShowingRewardsList = true;
		if (m_rankedRewardListWidget == null)
		{
			m_rankedRewardListWidget = WidgetInstance.Create(RankMgr.RANKED_REWARD_LIST_POPUP);
			m_rankedRewardListWidget.RegisterReadyListener(delegate
			{
				OnRankedRewardListPopupWidgetReady();
			});
			m_rankedRewardListWidget.WillLoadSynchronously = true;
			m_rankedRewardListWidget.Initialize();
		}
		while (m_rankedRewardList == null || m_rankedRewardListWidget.IsChangingStates)
		{
			yield return null;
		}
		UIContext.GetRoot().RegisterPopup(m_rankedRewardListWidget.gameObject, UIContext.RenderCameraType.OrthographicUI);
		m_rankedRewardListWidget.Show();
		m_rankedRewardListWidget.TriggerEvent("SHOW");
		yield return new WaitForSeconds(0.25f);
	}

	private void OnRankedRewardListPopupWidgetReady()
	{
		OverlayUI.Get().AddGameObject(m_rankedRewardListWidget.gameObject);
		m_rankedRewardListWidget.transform.localPosition = m_rewardListPos;
		float aspectRatioDependentValue = TransformUtil.GetAspectRatioDependentValue(m_rewardListScaleSmall, m_rewardListScaleWide, m_rewardListScaleExtraWide);
		m_rankedRewardListWidget.transform.localScale = Vector3.one * aspectRatioDependentValue * m_rewardListDeviceScale;
		m_rankedRewardListWidget.RegisterEventListener(WidgetEventListener_RewardsList);
		m_rankedRewardList = m_rankedRewardListWidget.GetComponentInChildren<RankedRewardList>();
		m_rankedRewardListWidget.Hide();
		UpdateRankedRewardList();
	}

	private void WidgetEventListener_RewardsList(string eventName)
	{
		if (eventName.Equals("HIDE"))
		{
			HideRankedRewardsList();
		}
	}

	private void HideRankedRewardsList()
	{
		UIContext.GetRoot().UnregisterPopup(m_rankedRewardListWidget.gameObject);
		m_isShowingRewardsList = false;
	}

	private void UpdateRankedRewardList()
	{
		if (m_rankedRewardList != null)
		{
			m_rankedRewardList.Initialize(new MedalInfoTranslator(TournamentDisplay.Get().GetCurrentMedalInfo()));
		}
	}

	private void DestroyRankedRewardsList()
	{
		if (m_rankedRewardListWidget != null)
		{
			Object.Destroy(m_rankedRewardListWidget.gameObject);
		}
		m_isShowingRewardsList = false;
	}

	private void ShowChestTooltip()
	{
		m_rewardsContainerWidget.GetComponent<TooltipZone>().ShowTooltip(GameStrings.Get("GLOBAL_PROGRESSION_RANKED_REWARDS_TOOLTIP_TITLE"), GameStrings.Get("GLOBAL_PROGRESSION_RANKED_REWARDS_TOOLTIP"), 5f);
	}

	private void HideChestTooltip()
	{
		m_rewardsContainerWidget.GetComponent<TooltipZone>().HideTooltip();
	}
}
