using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets;
using PegasusShared;
using UnityEngine;

[CustomEditClass]
public class CollectionDeckBoxVisual : PegUIElement
{
	[Serializable]
	public class FormatElements
	{
		[SerializeField]
		public FormatType formatType;

		[SerializeField]
		public Texture2D highlight;

		[SerializeField]
		public GameObject portraitObject;

		[SerializeField]
		public int portraitMaterialIndex;

		[SerializeField]
		public GameObject classObject;

		[SerializeField]
		public int classIconMaterialIndex;

		[SerializeField]
		public int classBannerMaterialIndex;

		[SerializeField]
		public MeshRenderer topBannerRenderer;

		[SerializeField]
		public Material xButtonMaterial;

		[CustomEditField(T = EditType.SOUND_PREFAB)]
		public string deckSelectSound;

		[SerializeField]
		public GameObject disabledMeshObject;
	}

	public delegate void DelOnAnimationFinished(object callbackData);

	private class OnPopAnimationFinishedCallbackData
	{
		public string m_animationName;

		public DelOnAnimationFinished m_callback;

		public object m_callbackData;
	}

	private class OnScaleFinishedCallbackData
	{
		public DelOnAnimationFinished m_callback;

		public object m_callbackData;
	}

	public UberText m_deckName;

	public UberText m_deckDesc;

	public GameObject m_labelGradient;

	public PegUIElement m_deleteButton;

	public GameObject m_highlight;

	public List<FormatElements> m_formatElements;

	public GameObject m_missingCardsIndicator;

	public UberText m_missingCardsIndicatorText;

	public int m_topBannerMaterialIndex;

	public GameObject m_pressedBone;

	public CustomDeckBones m_bones;

	public GameObject m_normalDeckVisuals;

	public GameObject m_lockedDeckVisuals;

	public TooltipZone m_tooltipZone;

	public GameObject m_renameVisuals;

	public bool m_neverUseGoldenPortraits;

	public static readonly float POPPED_UP_LOCAL_Z = 0f;

	public static readonly Vector3 POPPED_DOWN_LOCAL_POS = new Vector3(0f, -0.8598533f, 0f);

	public const float DECKBOX_SCALE = 0.95f;

	public static readonly Vector3 SCALED_DOWN_LOCAL_SCALE = new Vector3(0.95f, 0.95f, 0.95f);

	public const float SCALED_UP_LOCAL_Y_OFFSET = 3.238702f;

	public const float SCALED_DOWN_LOCAL_Y_OFFSET = 1.273138f;

	private const float BUTTON_POP_SPEED = 6f;

	private const string DECKBOX_POPUP_ANIM_NAME = "Deck_PopUp";

	private const string DECKBOX_POPDOWN_ANIM_NAME = "Deck_PopDown";

	private const string DECKBOX_DESATURATION_ANIM_NAME = "CustomDeck_Desat";

	private Vector3 SCALED_UP_DECK_OFFSET = new Vector3(0f, 0f, 0f);

	private const float SCALE_TIME = 0.2f;

	private const float ADJUST_Y_OFFSET_ANIM_TIME = 0.05f;

	private static readonly Color DECK_DESC_ENABLED_COLOR = new Color(0.97f, 0.82f, 0.22f);

	private static readonly Color DECK_NAME_ENABLED_COLOR = Color.white;

	private long m_deckID = -1L;

	private bool m_isPoppedUp;

	private bool m_isShown;

	private DefLoader.DisposableFullDef m_fullDef;

	private bool m_isShared;

	private HighlightState m_highlightState;

	private string m_heroCardID = "";

	private TAG_PREMIUM? m_heroCardPremiumOverride;

	private FormatType m_formatType = FormatType.FT_WILD;

	private Vector3 m_originalButtonPosition;

	private Quaternion m_originalButtonRotation;

	private bool m_animateButtonPress = true;

	private bool m_wasTouchModeEnabled;

	private int m_positionIndex;

	private bool m_showGlow;

	private bool m_isLocked;

	private bool m_forceSingleLineDeckName;

	private bool m_isSelected;

	private float m_wiggleIntensity;

	private bool m_showBanner = true;

	private Transform m_customDeckTransform;

	public static Vector3 SCALED_UP_LOCAL_SCALE { get; private set; }

	public bool IsMissingCards { get; private set; }

	private GameObject ButtonGameObject => GetActiveFormatElements().portraitObject;

	private static CollectionDeckTrayDeckListContent DecksContent
	{
		get
		{
			CollectionDeckTray collectionDeckTray = CollectionDeckTray.Get();
			if (!(collectionDeckTray != null))
			{
				return null;
			}
			return collectionDeckTray.GetDecksContent();
		}
	}

	protected override void Awake()
	{
		base.Awake();
		SetEnabled(enabled: false);
		m_deleteButton.AddEventListener(UIEventType.RELEASE, OnDeleteButtonPressed);
		m_deleteButton.AddEventListener(UIEventType.ROLLOVER, OnDeleteButtonOver);
		m_deleteButton.AddEventListener(UIEventType.ROLLOUT, OnDeleteButtonRollout);
		ShowDeleteButton(show: false);
		UpdateMissingCardsIndicator();
		m_deckName.RichText = false;
		m_deckName.TextColor = DECK_NAME_ENABLED_COLOR;
		m_deckDesc.TextColor = DECK_DESC_ENABLED_COLOR;
		SoundManager.Get().Load("tiny_button_press_1.prefab:44fc68b7418870b4797b85f0ca88a8db");
		SoundManager.Get().Load("tiny_button_mouseover_1.prefab:0ab88a13f5168ed43a3b53275114a842");
		m_customDeckTransform = base.transform.Find("CustomDeck");
		SetHighlightRoot();
		SCALED_UP_LOCAL_SCALE = new Vector3(1.126f, 1.126f, 1.126f);
		if (PlatformSettings.s_screen == ScreenCategory.Phone)
		{
			SCALED_UP_LOCAL_SCALE = new Vector3(1.1f, 1.1f, 1.1f);
			SCALED_UP_DECK_OFFSET = new Vector3(0f, -0.2f, 0f);
		}
		m_wasTouchModeEnabled = UniversalInputManager.Get().IsTouchMode();
	}

	protected override void OnDestroy()
	{
		m_fullDef?.Dispose();
		m_fullDef = null;
		base.OnDestroy();
	}

	private void Update()
	{
		if (m_wasTouchModeEnabled != UniversalInputManager.Get().IsTouchMode())
		{
			InteractionState interactionState = GetInteractionState();
			if (m_wasTouchModeEnabled)
			{
				switch (interactionState)
				{
				case InteractionState.Down:
					OnPressEvent();
					break;
				case InteractionState.Over:
					OnOverEvent();
					break;
				}
			}
			else
			{
				switch (interactionState)
				{
				case InteractionState.Down:
					OnReleaseEvent();
					break;
				case InteractionState.Over:
					OnOutEvent();
					break;
				}
				ShowDeleteButton(show: false);
			}
			m_wasTouchModeEnabled = UniversalInputManager.Get().IsTouchMode();
		}
		GameObject buttonGameObject = ButtonGameObject;
		CollectionDeckTrayDeckListContent decksContent = DecksContent;
		if (buttonGameObject != null && decksContent != null)
		{
			float rearrangeStartStopTweenDuration = decksContent.m_rearrangeStartStopTweenDuration;
			float rearrangeStartStopTweenDuration2 = decksContent.m_rearrangeStartStopTweenDuration;
			float rearrangeWiggleFrequency = decksContent.m_rearrangeWiggleFrequency;
			float rearrangeWiggleAmplitude = decksContent.m_rearrangeWiggleAmplitude;
			Vector3 rearrangeWiggleAxis = decksContent.m_rearrangeWiggleAxis;
			bool num = decksContent.DraggingDeckBox != null && decksContent.DraggingDeckBox != this;
			bool flag = m_wiggleIntensity > 0f;
			if (num)
			{
				m_wiggleIntensity = Mathf.Clamp01(m_wiggleIntensity + Time.deltaTime / rearrangeStartStopTweenDuration);
			}
			else
			{
				m_wiggleIntensity = Mathf.Clamp01(m_wiggleIntensity - Time.deltaTime / rearrangeStartStopTweenDuration2);
			}
			bool flag2 = m_wiggleIntensity > 0f;
			if (flag || flag2)
			{
				float angle = rearrangeWiggleAmplitude * m_wiggleIntensity * Mathf.Cos((float)m_positionIndex + Time.time * rearrangeWiggleFrequency);
				buttonGameObject.transform.localRotation = Quaternion.AngleAxis(angle, rearrangeWiggleAxis) * m_originalButtonRotation;
			}
		}
	}

	public void Show()
	{
		base.gameObject.SetActive(value: true);
		m_isShown = true;
	}

	public void Hide()
	{
		base.gameObject.SetActive(value: false);
		m_isShown = false;
	}

	public bool IsShown()
	{
		return m_isShown;
	}

	public bool IsDeckSelectableForCurrentMode()
	{
		FormatType formatType = Options.GetFormatType();
		switch (SceneMgr.Get().GetMode())
		{
		case SceneMgr.Mode.TOURNAMENT:
			if (Options.GetInRankedPlayMode() && formatType != m_formatType && (formatType != FormatType.FT_WILD || m_formatType != FormatType.FT_STANDARD))
			{
				if (formatType == FormatType.FT_STANDARD)
				{
					return m_formatType == FormatType.FT_WILD;
				}
				return false;
			}
			return true;
		case SceneMgr.Mode.ADVENTURE:
			return m_formatType != FormatType.FT_CLASSIC;
		default:
			return true;
		}
	}

	public void SetDeckName(string deckName)
	{
		m_deckName.Text = deckName;
	}

	public UberText GetDeckNameText()
	{
		return m_deckName;
	}

	public void HideDeckName()
	{
		m_deckName.gameObject.SetActive(value: false);
	}

	public void ShowDeckName()
	{
		m_deckName.gameObject.SetActive(value: true);
	}

	public void HideRenameVisuals()
	{
		if (m_renameVisuals != null)
		{
			m_renameVisuals.SetActive(value: false);
		}
	}

	public void ShowRenameVisuals()
	{
		if (!CollectionManagerDisplay.IsSpecialOneDeckMode() && m_renameVisuals != null)
		{
			m_renameVisuals.SetActive(value: true);
		}
	}

	public void SetDeckID(long id)
	{
		m_deckID = id;
	}

	public long GetDeckID()
	{
		return m_deckID;
	}

	public CollectionDeck GetCollectionDeck()
	{
		return CollectionManager.Get().GetDeck(m_deckID);
	}

	public Texture GetHeroPortraitTexture()
	{
		CardDef cardDef = m_fullDef?.CardDef;
		if (!(cardDef == null))
		{
			return cardDef.GetPortraitTexture();
		}
		return null;
	}

	public DefLoader.DisposableFullDef SharedDisposableFullDef()
	{
		return m_fullDef?.Share();
	}

	public bool HasFullDef()
	{
		return m_fullDef != null;
	}

	public TAG_CLASS GetClass()
	{
		return (m_fullDef?.EntityDef)?.GetClass() ?? TAG_CLASS.INVALID;
	}

	public string GetHeroCardID()
	{
		return m_heroCardID;
	}

	public void SetHeroCardID(string heroCardID)
	{
		m_heroCardID = heroCardID;
		DefLoader.Get().LoadFullDef(heroCardID, OnHeroFullDefLoaded);
	}

	public void SetHeroCardPremiumOverride(TAG_PREMIUM? premium)
	{
		m_heroCardPremiumOverride = premium;
	}

	public TAG_PREMIUM GetHeroCardPremium()
	{
		if (m_heroCardPremiumOverride.HasValue)
		{
			return m_heroCardPremiumOverride.Value;
		}
		EntityDef entityDef = DefLoader.Get().GetEntityDef(m_heroCardID);
		TAG_CARD_SET cardSet = entityDef.GetCardSet();
		string cardID = m_heroCardID;
		if (TAG_CARD_SET.HERO_SKINS == cardSet)
		{
			cardID = CollectionManager.GetHeroCardId(entityDef.GetClass(), CardHero.HeroType.VANILLA);
		}
		return CollectionManager.Get().GetBestCardPremium(cardID);
	}

	public void SetShowGlow(bool showGlow)
	{
		m_showGlow = showGlow;
		if (m_showGlow)
		{
			SetHighlightState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		}
	}

	public FormatType GetFormatType()
	{
		return m_formatType;
	}

	public void PlayGlowAnim(bool setFormatToStandard)
	{
		if (setFormatToStandard)
		{
			SetFormatType(FormatType.FT_STANDARD);
		}
		Animator component = GetComponent<Animator>();
		if (component != null)
		{
			component.enabled = true;
			component.Play("CustomDeck_GlowOut", 0, 0f);
		}
	}

	public void OnGlowAnimPeak()
	{
		CollectionDeck collectionDeck = GetCollectionDeck();
		if (collectionDeck == null || collectionDeck.FormatType != FormatType.FT_WILD)
		{
			return;
		}
		m_formatType = collectionDeck.FormatType;
		ReparentElements(m_formatType);
		FormatElements activeFormatElements = GetActiveFormatElements();
		FormatElements[] inactiveFormatElements = GetInactiveFormatElements();
		m_highlightState.m_StaticSilouetteTexture = activeFormatElements.highlight;
		FormatElements[] array = inactiveFormatElements;
		foreach (FormatElements formatElements in array)
		{
			if (formatElements.portraitObject != null)
			{
				formatElements.portraitObject.SetActive(value: false);
			}
		}
		if (activeFormatElements.portraitObject != null)
		{
			activeFormatElements.portraitObject.SetActive(value: true);
			if (!UniversalInputManager.UsePhoneUI)
			{
				activeFormatElements.portraitObject.GetComponent<Animator>().Play("Wild_RolldownActivate", 0, 1f);
			}
			else
			{
				activeFormatElements.portraitObject.GetComponent<Animator>().Play("WildActivate", 0, 1f);
			}
		}
	}

	public void SetFormatType(FormatType formatType)
	{
		m_formatType = formatType;
		ReparentElements(formatType);
		UpdateVisualBannerState();
		FormatElements activeFormatElements = GetActiveFormatElements();
		m_deleteButton.GetComponent<Renderer>().SetMaterial(activeFormatElements.xButtonMaterial);
		m_highlightState.m_StaticSilouetteTexture = activeFormatElements.highlight;
		foreach (FormatElements formatElement in m_formatElements)
		{
			if (formatElement.portraitObject != null)
			{
				formatElement.portraitObject.SetActive(formatElement.formatType == formatType);
			}
		}
	}

	public void SetPositionIndex(int idx)
	{
		m_positionIndex = idx;
	}

	public int GetPositionIndex()
	{
		return m_positionIndex;
	}

	public void UpdateDeckLabel()
	{
		bool flag = false;
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER || IsShared() || m_heroCardPremiumOverride.HasValue || m_forceSingleLineDeckName || !IsDeckSelectableForCurrentMode())
		{
			flag = true;
		}
		else if (CanClickToConvertToStandard())
		{
			string key = (UniversalInputManager.Get().IsTouchMode() ? "GLUE_COLLECTION_DECK_WILD_LABEL_TOUCH" : "GLUE_COLLECTION_DECK_WILD_LABEL");
			m_deckDesc.Text = GameStrings.Get(key);
		}
		else if (IsMissingCards)
		{
			string key2 = (UniversalInputManager.Get().IsTouchMode() ? "GLUE_COLLECTION_DECK_INCOMPLETE_LABEL_TOUCH" : "GLUE_COLLECTION_DECK_INCOMPLETE_LABEL");
			m_deckDesc.Text = GameStrings.Get(key2);
		}
		else if (m_fullDef?.EntityDef != null)
		{
			flag = true;
		}
		if (flag)
		{
			SetDeckNameAsSingleLine(forceSingleLine: false);
			return;
		}
		m_deckName.transform.position = m_bones.m_deckLabelTwoLine.position;
		m_labelGradient.transform.parent = m_bones.m_gradientTwoLine;
		m_labelGradient.transform.localPosition = Vector3.zero;
		m_labelGradient.transform.localScale = Vector3.one;
		m_deckDesc.gameObject.SetActive(value: true);
	}

	public void SetDeckNameAsSingleLine(bool forceSingleLine)
	{
		if (forceSingleLine)
		{
			m_forceSingleLineDeckName = true;
		}
		m_deckName.transform.position = m_bones.m_deckLabelOneLine.position;
		m_labelGradient.transform.parent = m_bones.m_gradientOneLine;
		m_labelGradient.transform.localPosition = Vector3.zero;
		m_labelGradient.transform.localScale = Vector3.one;
		m_deckDesc.gameObject.SetActive(value: false);
	}

	protected bool FormatMustMatchMode()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		bool flag = mode == SceneMgr.Mode.FIRESIDE_GATHERING && FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.FRIENDLY_CHALLENGE;
		return mode == SceneMgr.Mode.TOURNAMENT || mode == SceneMgr.Mode.FRIENDLY || flag;
	}

	public bool IsValidForCurrentMode()
	{
		CollectionDeck collectionDeck = GetCollectionDeck();
		if (collectionDeck == null)
		{
			return false;
		}
		if (!collectionDeck.IsValidForRuleset)
		{
			return false;
		}
		if (FormatMustMatchMode() && !collectionDeck.IsValidForFormat(Options.GetFormatType()))
		{
			return false;
		}
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.ADVENTURE && collectionDeck.FormatType == FormatType.FT_CLASSIC)
		{
			return false;
		}
		return true;
	}

	public bool CanClickToConvertToStandard()
	{
		CollectionDeck collectionDeck = GetCollectionDeck();
		if (collectionDeck == null)
		{
			return false;
		}
		if (FormatMustMatchMode() && collectionDeck.FormatType == FormatType.FT_WILD)
		{
			return Options.GetFormatType() == FormatType.FT_STANDARD;
		}
		return false;
	}

	public void UpdateMissingCardsIndicator()
	{
		IsMissingCards = false;
		CollectionDeck collectionDeck = GetCollectionDeck();
		if (collectionDeck != null && collectionDeck.NetworkContentsLoaded() && !collectionDeck.IsBeingEdited() && GameUtils.IsCardGameplayEventActive(collectionDeck.HeroCardID))
		{
			DeckRuleset ruleset = collectionDeck.GetRuleset();
			if (ruleset == null || !ruleset.EntityInDeckIgnoresRuleset(collectionDeck))
			{
				int maxCardCount = collectionDeck.GetMaxCardCount();
				int totalValidCardCount = collectionDeck.GetTotalValidCardCount();
				if (totalValidCardCount < maxCardCount)
				{
					IsMissingCards = true;
					m_missingCardsIndicatorText.Text = GameStrings.Format("GLUE_COLLECTION_DECK_MISSING_CARDS_INDICATOR", totalValidCardCount, maxCardCount);
				}
			}
		}
		m_missingCardsIndicator.SetActive(IsMissingCards);
		UpdateDeckLabel();
	}

	public bool IsShared()
	{
		return m_isShared;
	}

	public void SetIsShared(bool isShared)
	{
		if (m_isShared != isShared)
		{
			m_isShared = isShared;
			UpdateDeckLabel();
		}
	}

	public bool IsLocked()
	{
		return m_isLocked;
	}

	public void SetIsLocked(bool isLocked)
	{
		if (m_isLocked != isLocked)
		{
			m_isLocked = isLocked;
			m_normalDeckVisuals.SetActive(!m_isLocked);
			m_lockedDeckVisuals.SetActive(m_isLocked);
			SetHighlightRoot();
		}
	}

	public void SetHighlightRoot()
	{
		if (m_isLocked)
		{
			m_highlightState = m_lockedDeckVisuals.GetComponentInChildren<HighlightState>();
		}
		else
		{
			m_highlightState = m_normalDeckVisuals.GetComponentInChildren<HighlightState>();
		}
	}

	public bool IsSelected()
	{
		return m_isSelected;
	}

	public void SetIsSelected(bool isSelected)
	{
		if (m_isSelected != isSelected)
		{
			m_isSelected = isSelected;
			if (!m_isSelected && m_tooltipZone != null)
			{
				m_tooltipZone.HideTooltip();
			}
		}
	}

	public void EnableButtonAnimation()
	{
		m_animateButtonPress = true;
	}

	public void DisableButtonAnimation()
	{
		m_animateButtonPress = false;
	}

	public void PlayScaleUpAnimation()
	{
		PlayScaleUpAnimation(null);
	}

	public void PlayScaleUpAnimation(DelOnAnimationFinished callback)
	{
		PlayScaleUpAnimation(callback, null);
	}

	public void PlayScaleUpAnimation(DelOnAnimationFinished callback, object callbackData)
	{
		OnScaleFinishedCallbackData onScaleFinishedCallbackData = new OnScaleFinishedCallbackData
		{
			m_callback = callback,
			m_callbackData = callbackData
		};
		Vector3 localPosition = base.transform.localPosition;
		localPosition.y = 3.238702f;
		Hashtable args = iTween.Hash("position", localPosition, "isLocal", true, "time", 0.05f, "easetype", iTween.EaseType.linear, "oncomplete", "ScaleUpNow", "oncompletetarget", base.gameObject, "oncompleteparams", onScaleFinishedCallbackData);
		iTween.MoveTo(base.gameObject, args);
	}

	private void ScaleUpNow(OnScaleFinishedCallbackData readyToScaleUpData)
	{
		ScaleDeckBox(scaleUp: true, readyToScaleUpData.m_callback, readyToScaleUpData.m_callbackData);
	}

	public void PlayScaleDownAnimation()
	{
		PlayScaleDownAnimation(null);
	}

	public void PlayScaleDownAnimation(DelOnAnimationFinished callback)
	{
		PlayScaleDownAnimation(callback, null);
	}

	public void PlayScaleDownAnimation(DelOnAnimationFinished callback, object callbackData)
	{
		OnScaleFinishedCallbackData callbackData2 = new OnScaleFinishedCallbackData
		{
			m_callback = callback,
			m_callbackData = callbackData
		};
		ScaleDeckBox(scaleUp: false, OnScaledDown, callbackData2);
	}

	private void OnScaledDown(object callbackData)
	{
		OnScaleFinishedCallbackData onScaleFinishedCallbackData = callbackData as OnScaleFinishedCallbackData;
		Vector3 localPosition = base.transform.localPosition;
		localPosition.y = 1.273138f;
		Hashtable args = iTween.Hash("position", localPosition, "isLocal", true, "time", 0.05f, "easetype", iTween.EaseType.linear, "oncomplete", "ScaleDownComplete", "oncompletetarget", base.gameObject, "oncompleteparams", onScaleFinishedCallbackData);
		iTween.MoveTo(base.gameObject, args);
	}

	private void ScaleDownComplete(OnScaleFinishedCallbackData onScaledDownData)
	{
		if (onScaledDownData.m_callback != null)
		{
			onScaledDownData.m_callback(onScaledDownData.m_callbackData);
		}
	}

	public void PlayPopUpAnimation()
	{
		PlayPopUpAnimation(null);
	}

	public void PlayPopUpAnimation(DelOnAnimationFinished callback)
	{
		PlayPopUpAnimation(callback, null);
	}

	public void PlayPopUpAnimation(DelOnAnimationFinished callback, object callbackData)
	{
		if (m_isPoppedUp)
		{
			callback?.Invoke(callbackData);
			return;
		}
		m_isPoppedUp = true;
		if (m_customDeckTransform != null)
		{
			m_customDeckTransform.localPosition += SCALED_UP_DECK_OFFSET;
		}
		GetComponent<Animation>()["Deck_PopUp"].time = 0f;
		GetComponent<Animation>()["Deck_PopUp"].speed = 6f;
		PlayPopAnimation("Deck_PopUp", callback, callbackData);
	}

	public void PlayDesaturationAnimation()
	{
		Animator component = GetComponent<Animator>();
		if (component != null)
		{
			component.enabled = true;
			component.Play("CustomDeck_Desat", 0, 0f);
		}
	}

	public void PlayPopDownAnimation()
	{
		PlayPopDownAnimation(null);
	}

	public void PlayPopDownAnimation(DelOnAnimationFinished callback)
	{
		PlayPopDownAnimation(callback, null);
	}

	public void PlayPopDownAnimation(DelOnAnimationFinished callback, object callbackData)
	{
		if (!m_isPoppedUp)
		{
			callback?.Invoke(callbackData);
			return;
		}
		m_isPoppedUp = false;
		if (m_customDeckTransform != null)
		{
			m_customDeckTransform.localPosition -= SCALED_UP_DECK_OFFSET;
		}
		GetComponent<Animation>()["Deck_PopDown"].time = 0f;
		GetComponent<Animation>()["Deck_PopDown"].speed = 6f;
		PlayPopAnimation("Deck_PopDown", callback, callbackData);
	}

	public void PlayPopDownAnimationImmediately()
	{
		PlayPopDownAnimationImmediately(null);
	}

	public void PlayPopDownAnimationImmediately(DelOnAnimationFinished callback)
	{
		PlayPopDownAnimationImmediately(callback, null);
	}

	public void PlayPopDownAnimationImmediately(DelOnAnimationFinished callback, object callbackData)
	{
		if (!m_isPoppedUp)
		{
			callback?.Invoke(callbackData);
			return;
		}
		m_isPoppedUp = false;
		GetComponent<Animation>()["Deck_PopDown"].time = GetComponent<Animation>()["Deck_PopDown"].length;
		GetComponent<Animation>()["Deck_PopDown"].speed = 1f;
		PlayPopAnimation("Deck_PopDown", callback, callbackData);
	}

	public void SetHighlightMaterialForState(Material mat, ActorStateType stateType)
	{
		bool flag = false;
		foreach (HighlightRenderState highlightState in m_highlightState.m_HighlightStates)
		{
			if (highlightState.m_StateType == stateType)
			{
				flag = true;
				highlightState.m_Material = mat;
			}
		}
		if (!flag)
		{
			Log.All.PrintWarning("CollectionDeckBoxVisual - Attempting to set new material for state {0}, but no HighlightRenderState object found for that state type!", stateType);
		}
	}

	public void SetHighlightState(ActorStateType stateType)
	{
		if (m_highlightState != null)
		{
			if (!m_highlightState.IsReady())
			{
				StartCoroutine(ChangeHighlightStateWhenReady(stateType));
			}
			else
			{
				m_highlightState.ChangeState(stateType);
			}
		}
	}

	private IEnumerator ChangeHighlightStateWhenReady(ActorStateType stateType)
	{
		while (!m_highlightState.IsReady())
		{
			yield return null;
		}
		m_highlightState.ChangeState(stateType);
	}

	public void ShowDeleteButton(bool show)
	{
		m_deleteButton.gameObject.SetActive(show);
		if (IsMissingCards)
		{
			m_missingCardsIndicator.SetActive(!show);
		}
	}

	public void StoreOriginalButtonPositionAndRotation()
	{
		if (ButtonGameObject != null)
		{
			m_originalButtonPosition = ButtonGameObject.transform.localPosition;
			m_originalButtonRotation = ButtonGameObject.transform.localRotation;
		}
	}

	public void HideBanner()
	{
		ShowBannerInternal(show: false);
	}

	public void ShowBanner()
	{
		ShowBannerInternal(show: true);
	}

	public void AssignFromCollectionDeck(CollectionDeck deck)
	{
		if (deck != null)
		{
			SetDeckName(deck.Name);
			SetDeckID(deck.ID);
			SetHeroCardPremiumOverride(deck.GetDisplayHeroPremiumOverride());
			SetHeroCardID(deck.GetDisplayHeroCardID());
			SetShowGlow(ShouldHighlightDeck(deck));
			SetFormatType(CollectionManager.Get().GetThemeShowing(deck));
			SetIsShared(deck.IsShared);
			UpdateMissingCardsIndicator();
		}
	}

	private FormatElements GetFormatElements(FormatType formatType)
	{
		if (m_deckID == 0L && formatType == FormatType.FT_UNKNOWN)
		{
			return GetStandardFormatElements();
		}
		FormatElements formatElements = m_formatElements.Where((FormatElements x) => x.formatType == formatType).FirstOrDefault();
		if (formatElements == null)
		{
			Debug.LogError("Unsupported format type in CollectionDeckBoxVisual.GetFormatElements: " + formatType.ToString() + ". Will use standard formatting.");
			return GetStandardFormatElements();
		}
		return formatElements;
	}

	private FormatElements GetStandardFormatElements()
	{
		return m_formatElements.Where((FormatElements x) => x.formatType == FormatType.FT_STANDARD).FirstOrDefault();
	}

	private FormatElements GetActiveFormatElements()
	{
		return GetFormatElements(m_formatType);
	}

	private FormatElements[] GetInactiveFormatElements()
	{
		return m_formatElements.Where((FormatElements x) => x.formatType != 0 && x.formatType != m_formatType).ToArray();
	}

	private void ShowBannerInternal(bool show)
	{
		m_showBanner = show;
		UpdateVisualBannerState();
	}

	private void UpdateVisualBannerState()
	{
		FormatElements activeFormatElements = GetActiveFormatElements();
		bool flag = IsDeckSelectableForCurrentMode();
		if (activeFormatElements.disabledMeshObject != null)
		{
			activeFormatElements.disabledMeshObject.SetActive(!flag);
		}
		if (activeFormatElements.classObject != null)
		{
			activeFormatElements.classObject.SetActive((flag || (bool)UniversalInputManager.UsePhoneUI) && m_showBanner);
		}
		if (activeFormatElements.topBannerRenderer != null)
		{
			activeFormatElements.topBannerRenderer.gameObject.SetActive(flag && m_showBanner);
		}
	}

	private void OnDeleteButtonRollout(UIEvent e)
	{
		ShowDeleteButton(show: false);
	}

	private void OnDeleteButtonOver(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("tiny_button_mouseover_1.prefab:0ab88a13f5168ed43a3b53275114a842", base.gameObject);
	}

	private void OnDeleteButtonPressed(UIEvent e)
	{
		if (!CollectionDeckTray.Get().IsShowingDeckContents())
		{
			SoundManager.Get().LoadAndPlay("tiny_button_press_1.prefab:44fc68b7418870b4797b85f0ca88a8db", base.gameObject);
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_DELETE_CONFIRM_HEADER");
			popupInfo.m_showAlertIcon = false;
			popupInfo.m_text = GameStrings.Get("GLUE_COLLECTION_DELETE_CONFIRM_DESC");
			popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
			popupInfo.m_responseCallback = OnDeleteButtonConfirmationResponse;
			DialogManager.Get().ShowPopup(popupInfo);
		}
	}

	private void OnDeleteButtonConfirmationResponse(AlertPopup.Response response, object userData)
	{
		if (response != AlertPopup.Response.CANCEL)
		{
			SetEnabled(enabled: false);
			DecksContent.DeleteDeck(GetDeckID());
		}
	}

	private void PlayPopAnimation(string animationName)
	{
		PlayPopAnimation(animationName, null, null);
	}

	private void PlayPopAnimation(string animationName, DelOnAnimationFinished callback, object callbackData)
	{
		GetComponent<Animation>().Play(animationName);
		OnPopAnimationFinishedCallbackData value = new OnPopAnimationFinishedCallbackData
		{
			m_callback = callback,
			m_callbackData = callbackData,
			m_animationName = animationName
		};
		StopCoroutine("WaitThenCallAnimationCallback");
		StartCoroutine("WaitThenCallAnimationCallback", value);
	}

	private IEnumerator WaitThenCallAnimationCallback(OnPopAnimationFinishedCallbackData callbackData)
	{
		yield return new WaitForSeconds(GetComponent<Animation>()[callbackData.m_animationName].length / GetComponent<Animation>()[callbackData.m_animationName].speed);
		bool flag = callbackData.m_animationName.Equals("Deck_PopUp");
		SetEnabled(flag);
		if (callbackData.m_callback != null)
		{
			callbackData.m_callback(callbackData.m_callbackData);
		}
	}

	private void ScaleDeckBox(bool scaleUp, DelOnAnimationFinished callback, object callbackData)
	{
		OnScaleFinishedCallbackData onScaleFinishedCallbackData = new OnScaleFinishedCallbackData
		{
			m_callback = callback,
			m_callbackData = callbackData
		};
		Vector3 vector = (scaleUp ? SCALED_UP_LOCAL_SCALE : SCALED_DOWN_LOCAL_SCALE);
		Hashtable args = iTween.Hash("scale", vector, "isLocal", true, "time", 0.2f, "easetype", iTween.EaseType.linear, "oncomplete", "OnScaleComplete", "oncompletetarget", base.gameObject, "oncompleteparams", onScaleFinishedCallbackData, "name", "scale");
		iTween.StopByName(base.gameObject, "scale");
		iTween.ScaleTo(base.gameObject, args);
	}

	private void OnScaleComplete(OnScaleFinishedCallbackData callbackData)
	{
		if (callbackData.m_callback != null)
		{
			callbackData.m_callback(callbackData.m_callbackData);
		}
	}

	private void OnHeroFullDefLoaded(string cardID, DefLoader.DisposableFullDef def, object userData)
	{
		Log.CollectionDeckBox.Print("OnHeroFullDefLoaded cardID: {0},  m_heroCardID: {1}", cardID, m_heroCardID);
		if (cardID != null && cardID.Equals(m_heroCardID))
		{
			m_fullDef?.Dispose();
			m_fullDef = def;
			if (m_fullDef != null && m_fullDef.CardDef != null)
			{
				SetPortrait(m_fullDef.CardDef.GetCustomDeckPortrait());
			}
			if (m_fullDef != null && m_fullDef.EntityDef != null)
			{
				TAG_CLASS @class = m_fullDef.EntityDef.GetClass();
				SetClassDisplay(@class);
			}
			UpdateDeckLabel();
		}
	}

	private void UpdatePortraitMaterial(GameObject portraitObject, Material portraitMaterial, int portraitMaterialIndex)
	{
		if (portraitMaterial == null)
		{
			Debug.LogError("Custom Deck Portrait Material is null!");
			return;
		}
		portraitObject.GetComponent<Renderer>().SetSharedMaterial(portraitMaterialIndex, portraitMaterial);
		if (m_fullDef?.CardDef == null || m_neverUseGoldenPortraits || (GetHeroCardPremium() != TAG_PREMIUM.GOLDEN && GameUtils.IsVanillaHero(m_fullDef.EntityDef.GetCardId())) || GraphicsManager.Get().isVeryLowQualityDevice())
		{
			return;
		}
		Material material = m_fullDef?.CardDef.GetPremiumPortraitMaterial();
		if (material != null)
		{
			Renderer component = portraitObject.GetComponent<Renderer>();
			Material material2 = component.GetMaterial(portraitMaterialIndex);
			Texture value = null;
			if (material2.HasProperty("_ShadowTex"))
			{
				value = material2.GetTexture("_ShadowTex");
			}
			component.SetMaterial(portraitMaterialIndex, material);
			component.GetMaterial(portraitMaterialIndex).SetTexture("_ShadowTex", value);
			Material material3 = component.GetMaterial(portraitMaterialIndex);
			material3.mainTextureOffset = material2.mainTextureOffset;
			material3.mainTextureScale = material2.mainTextureScale;
		}
		UberShaderAnimation uberShaderAnimation = m_fullDef?.CardDef.GetPremiumPortraitAnimation();
		if (uberShaderAnimation != null)
		{
			UberShaderController uberShaderController = portraitObject.GetComponent<UberShaderController>();
			if (uberShaderController == null)
			{
				uberShaderController = portraitObject.AddComponent<UberShaderController>();
			}
			uberShaderController.UberShaderAnimation = UnityEngine.Object.Instantiate(uberShaderAnimation);
			uberShaderController.m_MaterialIndex = portraitMaterialIndex;
		}
	}

	private void SetPortrait(Material portraitMaterial)
	{
		foreach (FormatElements formatElement in m_formatElements)
		{
			UpdatePortraitMaterial(formatElement.portraitObject, portraitMaterial, formatElement.portraitMaterialIndex);
		}
	}

	private void SetClassDisplay(TAG_CLASS classTag)
	{
		foreach (FormatElements formatElement in m_formatElements)
		{
			if (formatElement.classObject == null)
			{
				continue;
			}
			MeshRenderer component = formatElement.classObject.GetComponent<MeshRenderer>();
			if (!(component != null))
			{
				continue;
			}
			Material material = component.GetMaterial(formatElement.classIconMaterialIndex);
			Material material2 = component.GetMaterial(formatElement.classBannerMaterialIndex);
			if (!(material == null) && !(material2 == null))
			{
				material.mainTextureOffset = CollectionPageManager.s_classTextureOffsets[classTag];
				material2.color = CollectionPageManager.ColorForClass(classTag);
				if (formatElement.topBannerRenderer != null)
				{
					formatElement.topBannerRenderer.GetMaterial(m_topBannerMaterialIndex).color = CollectionPageManager.ColorForClass(classTag);
				}
			}
		}
	}

	private void MarkRewardedDeckAsSeen(long deckId)
	{
		if (RewardUtils.HasNewRewardedDeck(out var collectionDeckId) && deckId == collectionDeckId)
		{
			RewardUtils.MarkNewestRewardedDeckAsSeen();
		}
	}

	private void MarkDeckAsSeen()
	{
		SetHighlightState(ActorStateType.HIGHLIGHT_PRIMARY_MOUSE_OVER);
		CollectionDeck collectionDeck = GetCollectionDeck();
		if (collectionDeck != null && collectionDeck.NeedsName)
		{
			Log.CollectionDeckBox.Print($"Sending deck changes for deck {m_deckID}, to clear the NEEDS_NAME flag.");
			collectionDeck.SendChanges();
			collectionDeck.NeedsName = false;
		}
		MarkRewardedDeckAsSeen(m_deckID);
		m_showGlow = false;
	}

	protected override void OnPress()
	{
		if (m_animateButtonPress && !m_isLocked && !m_isSelected && IsDeckSelectableForCurrentMode())
		{
			OnPressEvent();
		}
	}

	protected override void OnHold()
	{
		CollectionDeckTray collectionDeckTray = CollectionDeckTray.Get();
		if (collectionDeckTray == null || collectionDeckTray.GetCurrentContentType() != 0)
		{
			return;
		}
		CollectionDeckTrayDeckListContent decksContent = collectionDeckTray.GetDecksContent();
		if (!(decksContent == null) && !decksContent.IsTouchDragging)
		{
			if (m_tooltipZone != null)
			{
				m_tooltipZone.HideTooltip();
			}
			SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
			if (ButtonGameObject != null)
			{
				Hashtable args = iTween.Hash("scale", decksContent.m_rearrangeEnlargeScale * Vector3.one, "isLocal", true, "time", decksContent.m_rearrangeStartStopTweenDuration, "easeType", iTween.EaseType.linear);
				iTween.ScaleTo(ButtonGameObject, args);
			}
			decksContent.StartDragToReorder(this);
		}
	}

	protected override void OnRelease()
	{
		CollectionDeckTrayDeckListContent decksContent = DecksContent;
		if (decksContent != null)
		{
			decksContent.StopDragToReorder();
		}
		if (m_isLocked || m_isSelected || !IsDeckSelectableForCurrentMode())
		{
			return;
		}
		if (!SceneMgr.Get().IsInTavernBrawlMode() || UniversalInputManager.Get().IsTouchMode())
		{
			string deckSelectSound = GetActiveFormatElements().deckSelectSound;
			if (!string.IsNullOrEmpty(deckSelectSound))
			{
				SoundManager.Get().LoadAndPlay(deckSelectSound, base.gameObject);
			}
		}
		OnReleaseEvent();
	}

	public void OnStopDragToReorder()
	{
		if (ButtonGameObject != null)
		{
			float num = 0.1f;
			CollectionDeckTrayDeckListContent decksContent = DecksContent;
			if (decksContent != null)
			{
				num = decksContent.m_rearrangeStartStopTweenDuration;
			}
			Hashtable args = iTween.Hash("scale", Vector3.one, "isLocal", true, "time", num, "easeType", iTween.EaseType.linear);
			iTween.ScaleTo(ButtonGameObject, args);
		}
		OnOutEvent();
	}

	protected override void OnOut(InteractionState oldState)
	{
		if (m_tooltipZone != null)
		{
			m_tooltipZone.HideTooltip();
		}
		OnOutEvent();
	}

	protected override void OnOver(InteractionState oldState)
	{
		if (m_tooltipZone != null)
		{
			if (SceneMgr.Get().GetMode() != SceneMgr.Mode.ADVENTURE)
			{
				if (((m_formatType == FormatType.FT_WILD && Options.GetFormatType() != FormatType.FT_WILD) || (m_formatType == FormatType.FT_CLASSIC && Options.GetFormatType() != FormatType.FT_CLASSIC) || (m_formatType == FormatType.FT_STANDARD && Options.GetFormatType() == FormatType.FT_CLASSIC)) && Options.GetInRankedPlayMode())
				{
					m_tooltipZone.ShowTooltip(GameStrings.Format("GLUE_DISABLED_DECK_HEADER", GameStrings.GetFormatName(m_formatType)), GameStrings.Format("GLUE_DISABLED_DECK_DESC", GameStrings.GetFormatName(Options.GetFormatType())), 4f);
				}
			}
			else if (!IsDeckSelectableForCurrentMode())
			{
				m_tooltipZone.ShowTooltip(GameStrings.Format("GLUE_DISABLED_DECK_HEADER", GameStrings.GetFormatName(m_formatType)), GameStrings.Get("GLUE_DISABLED_DECK_IN_CURRENT_MODE_DESC"), 4f);
			}
			else if (IsMissingCards)
			{
				m_tooltipZone.ShowTooltip(GameStrings.Get("GLUE_INCOMPLETE_DECK_HEADER"), GameStrings.Get("GLUE_INCOMPLETE_DECK_DESC"), 4f);
			}
		}
		OnOverEvent();
	}

	public override void SetEnabled(bool enabled, bool isInternal = false)
	{
		base.SetEnabled(enabled, isInternal);
		if (!enabled && m_tooltipZone != null)
		{
			m_tooltipZone.HideTooltip();
		}
	}

	private void OnPressEvent()
	{
		ShowDeleteButton(show: false);
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.COLLECTIONMANAGER)
		{
			Hashtable args = iTween.Hash("position", m_pressedBone.transform.localPosition, "isLocal", true, "time", 0.1, "easeType", iTween.EaseType.linear);
			iTween.MoveTo(ButtonGameObject, args);
		}
	}

	private void OnReleaseEvent()
	{
		if (UniversalInputManager.Get().IsTouchMode() && m_showGlow)
		{
			MarkDeckAsSeen();
		}
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.COLLECTIONMANAGER)
		{
			Hashtable args = iTween.Hash("position", m_originalButtonPosition, "isLocal", true, "time", 0.1, "easeType", iTween.EaseType.linear);
			iTween.MoveTo(ButtonGameObject, args);
		}
	}

	private void OnOutEvent()
	{
		if (!m_isSelected)
		{
			SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
		}
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.COLLECTIONMANAGER)
		{
			Hashtable args = iTween.Hash("position", m_originalButtonPosition, "isLocal", true, "time", 0.1, "easeType", iTween.EaseType.linear);
			iTween.MoveTo(ButtonGameObject, args);
		}
	}

	private void OnOverEvent()
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			return;
		}
		CollectionDeckTrayDeckListContent decksContent = DecksContent;
		if (decksContent != null && decksContent.DraggingDeckBox != null)
		{
			return;
		}
		SoundManager.Get().LoadAndPlay("collection_manager_hero_mouse_over.prefab:653cc8000b988cd468d2210a209adce6", base.gameObject);
		if (!m_isSelected)
		{
			if (m_showGlow)
			{
				MarkDeckAsSeen();
			}
			else if (IsDeckSelectableForCurrentMode())
			{
				SetHighlightState(ActorStateType.HIGHLIGHT_MOUSE_OVER);
			}
		}
	}

	private void ReparentElements(FormatType formatType)
	{
		FormatElements formatElements = GetFormatElements(formatType);
		Transform parent = formatElements.portraitObject.transform;
		m_highlight.transform.parent = parent;
		m_deckName.gameObject.transform.parent = parent;
		m_deckDesc.gameObject.transform.parent = parent;
		m_missingCardsIndicator.gameObject.transform.parent = parent;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			formatElements.classObject.transform.parent = parent;
		}
		m_bones.m_gradientOneLine.parent = parent;
		m_bones.m_gradientTwoLine.parent = parent;
	}

	private static bool ShouldHighlightDeck(CollectionDeck deck)
	{
		if (!deck.NeedsName)
		{
			if (RewardUtils.HasNewRewardedDeck(out var collectionDeckId))
			{
				return deck.ID == collectionDeckId;
			}
			return false;
		}
		return true;
	}
}
