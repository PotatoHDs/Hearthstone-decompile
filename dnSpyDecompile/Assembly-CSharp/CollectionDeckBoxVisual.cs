using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets;
using PegasusShared;
using UnityEngine;

// Token: 0x02000105 RID: 261
[CustomEditClass]
public class CollectionDeckBoxVisual : PegUIElement
{
	// Token: 0x170000CD RID: 205
	// (get) Token: 0x06000F33 RID: 3891 RVA: 0x00055792 File Offset: 0x00053992
	// (set) Token: 0x06000F34 RID: 3892 RVA: 0x00055799 File Offset: 0x00053999
	public static Vector3 SCALED_UP_LOCAL_SCALE { get; private set; }

	// Token: 0x06000F35 RID: 3893 RVA: 0x000557A4 File Offset: 0x000539A4
	protected override void Awake()
	{
		base.Awake();
		this.SetEnabled(false, false);
		this.m_deleteButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnDeleteButtonPressed));
		this.m_deleteButton.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnDeleteButtonOver));
		this.m_deleteButton.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnDeleteButtonRollout));
		this.ShowDeleteButton(false);
		this.UpdateMissingCardsIndicator();
		this.m_deckName.RichText = false;
		this.m_deckName.TextColor = CollectionDeckBoxVisual.DECK_NAME_ENABLED_COLOR;
		this.m_deckDesc.TextColor = CollectionDeckBoxVisual.DECK_DESC_ENABLED_COLOR;
		SoundManager.Get().Load("tiny_button_press_1.prefab:44fc68b7418870b4797b85f0ca88a8db");
		SoundManager.Get().Load("tiny_button_mouseover_1.prefab:0ab88a13f5168ed43a3b53275114a842");
		this.m_customDeckTransform = base.transform.Find("CustomDeck");
		this.SetHighlightRoot();
		CollectionDeckBoxVisual.SCALED_UP_LOCAL_SCALE = new Vector3(1.126f, 1.126f, 1.126f);
		if (PlatformSettings.s_screen == ScreenCategory.Phone)
		{
			CollectionDeckBoxVisual.SCALED_UP_LOCAL_SCALE = new Vector3(1.1f, 1.1f, 1.1f);
			this.SCALED_UP_DECK_OFFSET = new Vector3(0f, -0.2f, 0f);
		}
		this.m_wasTouchModeEnabled = UniversalInputManager.Get().IsTouchMode();
	}

	// Token: 0x06000F36 RID: 3894 RVA: 0x000558ED File Offset: 0x00053AED
	protected override void OnDestroy()
	{
		DefLoader.DisposableFullDef fullDef = this.m_fullDef;
		if (fullDef != null)
		{
			fullDef.Dispose();
		}
		this.m_fullDef = null;
		base.OnDestroy();
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x00055910 File Offset: 0x00053B10
	private void Update()
	{
		if (this.m_wasTouchModeEnabled != UniversalInputManager.Get().IsTouchMode())
		{
			PegUIElement.InteractionState interactionState = base.GetInteractionState();
			if (this.m_wasTouchModeEnabled)
			{
				if (interactionState == PegUIElement.InteractionState.Down)
				{
					this.OnPressEvent();
				}
				else if (interactionState == PegUIElement.InteractionState.Over)
				{
					this.OnOverEvent();
				}
			}
			else
			{
				if (interactionState == PegUIElement.InteractionState.Down)
				{
					this.OnReleaseEvent();
				}
				else if (interactionState == PegUIElement.InteractionState.Over)
				{
					this.OnOutEvent();
				}
				this.ShowDeleteButton(false);
			}
			this.m_wasTouchModeEnabled = UniversalInputManager.Get().IsTouchMode();
		}
		GameObject buttonGameObject = this.ButtonGameObject;
		CollectionDeckTrayDeckListContent decksContent = CollectionDeckBoxVisual.DecksContent;
		if (buttonGameObject != null && decksContent != null)
		{
			float rearrangeStartStopTweenDuration = decksContent.m_rearrangeStartStopTweenDuration;
			float rearrangeStartStopTweenDuration2 = decksContent.m_rearrangeStartStopTweenDuration;
			float rearrangeWiggleFrequency = decksContent.m_rearrangeWiggleFrequency;
			float rearrangeWiggleAmplitude = decksContent.m_rearrangeWiggleAmplitude;
			Vector3 rearrangeWiggleAxis = decksContent.m_rearrangeWiggleAxis;
			bool flag = decksContent.DraggingDeckBox != null && decksContent.DraggingDeckBox != this;
			bool flag2 = this.m_wiggleIntensity > 0f;
			if (flag)
			{
				this.m_wiggleIntensity = Mathf.Clamp01(this.m_wiggleIntensity + Time.deltaTime / rearrangeStartStopTweenDuration);
			}
			else
			{
				this.m_wiggleIntensity = Mathf.Clamp01(this.m_wiggleIntensity - Time.deltaTime / rearrangeStartStopTweenDuration2);
			}
			bool flag3 = this.m_wiggleIntensity > 0f;
			if (flag2 || flag3)
			{
				float angle = rearrangeWiggleAmplitude * this.m_wiggleIntensity * Mathf.Cos((float)this.m_positionIndex + Time.time * rearrangeWiggleFrequency);
				buttonGameObject.transform.localRotation = Quaternion.AngleAxis(angle, rearrangeWiggleAxis) * this.m_originalButtonRotation;
			}
		}
	}

	// Token: 0x06000F38 RID: 3896 RVA: 0x00055A88 File Offset: 0x00053C88
	public void Show()
	{
		base.gameObject.SetActive(true);
		this.m_isShown = true;
	}

	// Token: 0x06000F39 RID: 3897 RVA: 0x00055A9D File Offset: 0x00053C9D
	public void Hide()
	{
		base.gameObject.SetActive(false);
		this.m_isShown = false;
	}

	// Token: 0x06000F3A RID: 3898 RVA: 0x00055AB2 File Offset: 0x00053CB2
	public bool IsShown()
	{
		return this.m_isShown;
	}

	// Token: 0x06000F3B RID: 3899 RVA: 0x00055ABC File Offset: 0x00053CBC
	public bool IsDeckSelectableForCurrentMode()
	{
		FormatType formatType = Options.GetFormatType();
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode != SceneMgr.Mode.TOURNAMENT)
		{
			return mode != SceneMgr.Mode.ADVENTURE || this.m_formatType != FormatType.FT_CLASSIC;
		}
		return !Options.GetInRankedPlayMode() || formatType == this.m_formatType || (formatType == FormatType.FT_WILD && this.m_formatType == FormatType.FT_STANDARD) || (formatType == FormatType.FT_STANDARD && this.m_formatType == FormatType.FT_WILD);
	}

	// Token: 0x06000F3C RID: 3900 RVA: 0x00055B22 File Offset: 0x00053D22
	public void SetDeckName(string deckName)
	{
		this.m_deckName.Text = deckName;
	}

	// Token: 0x06000F3D RID: 3901 RVA: 0x00055B30 File Offset: 0x00053D30
	public UberText GetDeckNameText()
	{
		return this.m_deckName;
	}

	// Token: 0x06000F3E RID: 3902 RVA: 0x00055B38 File Offset: 0x00053D38
	public void HideDeckName()
	{
		this.m_deckName.gameObject.SetActive(false);
	}

	// Token: 0x06000F3F RID: 3903 RVA: 0x00055B4B File Offset: 0x00053D4B
	public void ShowDeckName()
	{
		this.m_deckName.gameObject.SetActive(true);
	}

	// Token: 0x06000F40 RID: 3904 RVA: 0x00055B5E File Offset: 0x00053D5E
	public void HideRenameVisuals()
	{
		if (this.m_renameVisuals != null)
		{
			this.m_renameVisuals.SetActive(false);
		}
	}

	// Token: 0x06000F41 RID: 3905 RVA: 0x00055B7A File Offset: 0x00053D7A
	public void ShowRenameVisuals()
	{
		if (CollectionManagerDisplay.IsSpecialOneDeckMode())
		{
			return;
		}
		if (this.m_renameVisuals != null)
		{
			this.m_renameVisuals.SetActive(true);
		}
	}

	// Token: 0x06000F42 RID: 3906 RVA: 0x00055B9E File Offset: 0x00053D9E
	public void SetDeckID(long id)
	{
		this.m_deckID = id;
	}

	// Token: 0x06000F43 RID: 3907 RVA: 0x00055BA7 File Offset: 0x00053DA7
	public long GetDeckID()
	{
		return this.m_deckID;
	}

	// Token: 0x06000F44 RID: 3908 RVA: 0x00055BAF File Offset: 0x00053DAF
	public CollectionDeck GetCollectionDeck()
	{
		return CollectionManager.Get().GetDeck(this.m_deckID);
	}

	// Token: 0x06000F45 RID: 3909 RVA: 0x00055BC4 File Offset: 0x00053DC4
	public Texture GetHeroPortraitTexture()
	{
		DefLoader.DisposableFullDef fullDef = this.m_fullDef;
		global::CardDef cardDef = (fullDef != null) ? fullDef.CardDef : null;
		if (!(cardDef == null))
		{
			return cardDef.GetPortraitTexture();
		}
		return null;
	}

	// Token: 0x06000F46 RID: 3910 RVA: 0x00055BF5 File Offset: 0x00053DF5
	public DefLoader.DisposableFullDef SharedDisposableFullDef()
	{
		DefLoader.DisposableFullDef fullDef = this.m_fullDef;
		if (fullDef == null)
		{
			return null;
		}
		return fullDef.Share();
	}

	// Token: 0x06000F47 RID: 3911 RVA: 0x00055C08 File Offset: 0x00053E08
	public bool HasFullDef()
	{
		return this.m_fullDef != null;
	}

	// Token: 0x06000F48 RID: 3912 RVA: 0x00055C14 File Offset: 0x00053E14
	public TAG_CLASS GetClass()
	{
		DefLoader.DisposableFullDef fullDef = this.m_fullDef;
		EntityDef entityDef = (fullDef != null) ? fullDef.EntityDef : null;
		if (entityDef != null)
		{
			return entityDef.GetClass();
		}
		return TAG_CLASS.INVALID;
	}

	// Token: 0x06000F49 RID: 3913 RVA: 0x00055C3F File Offset: 0x00053E3F
	public string GetHeroCardID()
	{
		return this.m_heroCardID;
	}

	// Token: 0x06000F4A RID: 3914 RVA: 0x00055C47 File Offset: 0x00053E47
	public void SetHeroCardID(string heroCardID)
	{
		this.m_heroCardID = heroCardID;
		DefLoader.Get().LoadFullDef(heroCardID, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnHeroFullDefLoaded), null, null);
	}

	// Token: 0x06000F4B RID: 3915 RVA: 0x00055C69 File Offset: 0x00053E69
	public void SetHeroCardPremiumOverride(TAG_PREMIUM? premium)
	{
		this.m_heroCardPremiumOverride = premium;
	}

	// Token: 0x06000F4C RID: 3916 RVA: 0x00055C74 File Offset: 0x00053E74
	public TAG_PREMIUM GetHeroCardPremium()
	{
		if (this.m_heroCardPremiumOverride != null)
		{
			return this.m_heroCardPremiumOverride.Value;
		}
		EntityDef entityDef = DefLoader.Get().GetEntityDef(this.m_heroCardID);
		TAG_CARD_SET cardSet = entityDef.GetCardSet();
		string cardID = this.m_heroCardID;
		if (TAG_CARD_SET.HERO_SKINS == cardSet)
		{
			cardID = CollectionManager.GetHeroCardId(entityDef.GetClass(), CardHero.HeroType.VANILLA);
		}
		return CollectionManager.Get().GetBestCardPremium(cardID);
	}

	// Token: 0x06000F4D RID: 3917 RVA: 0x00055CD6 File Offset: 0x00053ED6
	public void SetShowGlow(bool showGlow)
	{
		this.m_showGlow = showGlow;
		if (this.m_showGlow)
		{
			this.SetHighlightState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		}
	}

	// Token: 0x06000F4E RID: 3918 RVA: 0x00055CEF File Offset: 0x00053EEF
	public FormatType GetFormatType()
	{
		return this.m_formatType;
	}

	// Token: 0x06000F4F RID: 3919 RVA: 0x00055CF8 File Offset: 0x00053EF8
	public void PlayGlowAnim(bool setFormatToStandard)
	{
		if (setFormatToStandard)
		{
			this.SetFormatType(FormatType.FT_STANDARD);
		}
		Animator component = base.GetComponent<Animator>();
		if (component != null)
		{
			component.enabled = true;
			component.Play("CustomDeck_GlowOut", 0, 0f);
		}
	}

	// Token: 0x06000F50 RID: 3920 RVA: 0x00055D38 File Offset: 0x00053F38
	public void OnGlowAnimPeak()
	{
		CollectionDeck collectionDeck = this.GetCollectionDeck();
		if (collectionDeck == null)
		{
			return;
		}
		if (collectionDeck.FormatType == FormatType.FT_WILD)
		{
			this.m_formatType = collectionDeck.FormatType;
			this.ReparentElements(this.m_formatType);
			CollectionDeckBoxVisual.FormatElements activeFormatElements = this.GetActiveFormatElements();
			CollectionDeckBoxVisual.FormatElements[] inactiveFormatElements = this.GetInactiveFormatElements();
			this.m_highlightState.m_StaticSilouetteTexture = activeFormatElements.highlight;
			foreach (CollectionDeckBoxVisual.FormatElements formatElements in inactiveFormatElements)
			{
				if (formatElements.portraitObject != null)
				{
					formatElements.portraitObject.SetActive(false);
				}
			}
			if (activeFormatElements.portraitObject != null)
			{
				activeFormatElements.portraitObject.SetActive(true);
				if (!UniversalInputManager.UsePhoneUI)
				{
					activeFormatElements.portraitObject.GetComponent<Animator>().Play("Wild_RolldownActivate", 0, 1f);
					return;
				}
				activeFormatElements.portraitObject.GetComponent<Animator>().Play("WildActivate", 0, 1f);
			}
		}
	}

	// Token: 0x06000F51 RID: 3921 RVA: 0x00055E20 File Offset: 0x00054020
	public void SetFormatType(FormatType formatType)
	{
		this.m_formatType = formatType;
		this.ReparentElements(formatType);
		this.UpdateVisualBannerState();
		CollectionDeckBoxVisual.FormatElements activeFormatElements = this.GetActiveFormatElements();
		this.m_deleteButton.GetComponent<Renderer>().SetMaterial(activeFormatElements.xButtonMaterial);
		this.m_highlightState.m_StaticSilouetteTexture = activeFormatElements.highlight;
		foreach (CollectionDeckBoxVisual.FormatElements formatElements in this.m_formatElements)
		{
			if (formatElements.portraitObject != null)
			{
				formatElements.portraitObject.SetActive(formatElements.formatType == formatType);
			}
		}
	}

	// Token: 0x06000F52 RID: 3922 RVA: 0x00055ED0 File Offset: 0x000540D0
	public void SetPositionIndex(int idx)
	{
		this.m_positionIndex = idx;
	}

	// Token: 0x06000F53 RID: 3923 RVA: 0x00055ED9 File Offset: 0x000540D9
	public int GetPositionIndex()
	{
		return this.m_positionIndex;
	}

	// Token: 0x06000F54 RID: 3924 RVA: 0x00055EE4 File Offset: 0x000540E4
	public void UpdateDeckLabel()
	{
		bool flag = false;
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER || this.IsShared() || this.m_heroCardPremiumOverride != null || this.m_forceSingleLineDeckName || !this.IsDeckSelectableForCurrentMode())
		{
			flag = true;
		}
		else if (this.CanClickToConvertToStandard())
		{
			string key = UniversalInputManager.Get().IsTouchMode() ? "GLUE_COLLECTION_DECK_WILD_LABEL_TOUCH" : "GLUE_COLLECTION_DECK_WILD_LABEL";
			this.m_deckDesc.Text = GameStrings.Get(key);
		}
		else if (this.IsMissingCards)
		{
			string key2 = UniversalInputManager.Get().IsTouchMode() ? "GLUE_COLLECTION_DECK_INCOMPLETE_LABEL_TOUCH" : "GLUE_COLLECTION_DECK_INCOMPLETE_LABEL";
			this.m_deckDesc.Text = GameStrings.Get(key2);
		}
		else
		{
			DefLoader.DisposableFullDef fullDef = this.m_fullDef;
			if (((fullDef != null) ? fullDef.EntityDef : null) != null)
			{
				flag = true;
			}
		}
		if (flag)
		{
			this.SetDeckNameAsSingleLine(false);
			return;
		}
		this.m_deckName.transform.position = this.m_bones.m_deckLabelTwoLine.position;
		this.m_labelGradient.transform.parent = this.m_bones.m_gradientTwoLine;
		this.m_labelGradient.transform.localPosition = Vector3.zero;
		this.m_labelGradient.transform.localScale = Vector3.one;
		this.m_deckDesc.gameObject.SetActive(true);
	}

	// Token: 0x06000F55 RID: 3925 RVA: 0x00056028 File Offset: 0x00054228
	public void SetDeckNameAsSingleLine(bool forceSingleLine)
	{
		if (forceSingleLine)
		{
			this.m_forceSingleLineDeckName = true;
		}
		this.m_deckName.transform.position = this.m_bones.m_deckLabelOneLine.position;
		this.m_labelGradient.transform.parent = this.m_bones.m_gradientOneLine;
		this.m_labelGradient.transform.localPosition = Vector3.zero;
		this.m_labelGradient.transform.localScale = Vector3.one;
		this.m_deckDesc.gameObject.SetActive(false);
	}

	// Token: 0x06000F56 RID: 3926 RVA: 0x000560B8 File Offset: 0x000542B8
	protected bool FormatMustMatchMode()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		bool flag = mode == SceneMgr.Mode.FIRESIDE_GATHERING && FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.FRIENDLY_CHALLENGE;
		return mode == SceneMgr.Mode.TOURNAMENT || mode == SceneMgr.Mode.FRIENDLY || flag;
	}

	// Token: 0x06000F57 RID: 3927 RVA: 0x000560F4 File Offset: 0x000542F4
	public bool IsValidForCurrentMode()
	{
		CollectionDeck collectionDeck = this.GetCollectionDeck();
		return collectionDeck != null && collectionDeck.IsValidForRuleset && (!this.FormatMustMatchMode() || collectionDeck.IsValidForFormat(Options.GetFormatType())) && (SceneMgr.Get().GetMode() != SceneMgr.Mode.ADVENTURE || collectionDeck.FormatType != FormatType.FT_CLASSIC);
	}

	// Token: 0x06000F58 RID: 3928 RVA: 0x00056148 File Offset: 0x00054348
	public bool CanClickToConvertToStandard()
	{
		CollectionDeck collectionDeck = this.GetCollectionDeck();
		return collectionDeck != null && (this.FormatMustMatchMode() && collectionDeck.FormatType == FormatType.FT_WILD) && Options.GetFormatType() == FormatType.FT_STANDARD;
	}

	// Token: 0x170000CE RID: 206
	// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0005617C File Offset: 0x0005437C
	// (set) Token: 0x06000F5A RID: 3930 RVA: 0x00056184 File Offset: 0x00054384
	public bool IsMissingCards { get; private set; }

	// Token: 0x06000F5B RID: 3931 RVA: 0x00056190 File Offset: 0x00054390
	public void UpdateMissingCardsIndicator()
	{
		this.IsMissingCards = false;
		CollectionDeck collectionDeck = this.GetCollectionDeck();
		if (collectionDeck != null && collectionDeck.NetworkContentsLoaded() && !collectionDeck.IsBeingEdited() && GameUtils.IsCardGameplayEventActive(collectionDeck.HeroCardID))
		{
			global::DeckRuleset ruleset = collectionDeck.GetRuleset();
			if (ruleset == null || !ruleset.EntityInDeckIgnoresRuleset(collectionDeck))
			{
				int maxCardCount = collectionDeck.GetMaxCardCount();
				int totalValidCardCount = collectionDeck.GetTotalValidCardCount();
				if (totalValidCardCount < maxCardCount)
				{
					this.IsMissingCards = true;
					this.m_missingCardsIndicatorText.Text = GameStrings.Format("GLUE_COLLECTION_DECK_MISSING_CARDS_INDICATOR", new object[]
					{
						totalValidCardCount,
						maxCardCount
					});
				}
			}
		}
		this.m_missingCardsIndicator.SetActive(this.IsMissingCards);
		this.UpdateDeckLabel();
	}

	// Token: 0x06000F5C RID: 3932 RVA: 0x0005623B File Offset: 0x0005443B
	public bool IsShared()
	{
		return this.m_isShared;
	}

	// Token: 0x06000F5D RID: 3933 RVA: 0x00056243 File Offset: 0x00054443
	public void SetIsShared(bool isShared)
	{
		if (this.m_isShared != isShared)
		{
			this.m_isShared = isShared;
			this.UpdateDeckLabel();
		}
	}

	// Token: 0x06000F5E RID: 3934 RVA: 0x0005625B File Offset: 0x0005445B
	public bool IsLocked()
	{
		return this.m_isLocked;
	}

	// Token: 0x06000F5F RID: 3935 RVA: 0x00056263 File Offset: 0x00054463
	public void SetIsLocked(bool isLocked)
	{
		if (this.m_isLocked == isLocked)
		{
			return;
		}
		this.m_isLocked = isLocked;
		this.m_normalDeckVisuals.SetActive(!this.m_isLocked);
		this.m_lockedDeckVisuals.SetActive(this.m_isLocked);
		this.SetHighlightRoot();
	}

	// Token: 0x06000F60 RID: 3936 RVA: 0x000562A1 File Offset: 0x000544A1
	public void SetHighlightRoot()
	{
		if (this.m_isLocked)
		{
			this.m_highlightState = this.m_lockedDeckVisuals.GetComponentInChildren<HighlightState>();
			return;
		}
		this.m_highlightState = this.m_normalDeckVisuals.GetComponentInChildren<HighlightState>();
	}

	// Token: 0x06000F61 RID: 3937 RVA: 0x000562CE File Offset: 0x000544CE
	public bool IsSelected()
	{
		return this.m_isSelected;
	}

	// Token: 0x06000F62 RID: 3938 RVA: 0x000562D6 File Offset: 0x000544D6
	public void SetIsSelected(bool isSelected)
	{
		if (this.m_isSelected == isSelected)
		{
			return;
		}
		this.m_isSelected = isSelected;
		if (!this.m_isSelected && this.m_tooltipZone != null)
		{
			this.m_tooltipZone.HideTooltip();
		}
	}

	// Token: 0x06000F63 RID: 3939 RVA: 0x0005630A File Offset: 0x0005450A
	public void EnableButtonAnimation()
	{
		this.m_animateButtonPress = true;
	}

	// Token: 0x06000F64 RID: 3940 RVA: 0x00056313 File Offset: 0x00054513
	public void DisableButtonAnimation()
	{
		this.m_animateButtonPress = false;
	}

	// Token: 0x06000F65 RID: 3941 RVA: 0x0005631C File Offset: 0x0005451C
	public void PlayScaleUpAnimation()
	{
		this.PlayScaleUpAnimation(null);
	}

	// Token: 0x06000F66 RID: 3942 RVA: 0x00056325 File Offset: 0x00054525
	public void PlayScaleUpAnimation(CollectionDeckBoxVisual.DelOnAnimationFinished callback)
	{
		this.PlayScaleUpAnimation(callback, null);
	}

	// Token: 0x06000F67 RID: 3943 RVA: 0x00056330 File Offset: 0x00054530
	public void PlayScaleUpAnimation(CollectionDeckBoxVisual.DelOnAnimationFinished callback, object callbackData)
	{
		CollectionDeckBoxVisual.OnScaleFinishedCallbackData onScaleFinishedCallbackData = new CollectionDeckBoxVisual.OnScaleFinishedCallbackData
		{
			m_callback = callback,
			m_callbackData = callbackData
		};
		Vector3 localPosition = base.transform.localPosition;
		localPosition.y = 3.238702f;
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			localPosition,
			"isLocal",
			true,
			"time",
			0.05f,
			"easetype",
			iTween.EaseType.linear,
			"oncomplete",
			"ScaleUpNow",
			"oncompletetarget",
			base.gameObject,
			"oncompleteparams",
			onScaleFinishedCallbackData
		});
		iTween.MoveTo(base.gameObject, args);
	}

	// Token: 0x06000F68 RID: 3944 RVA: 0x000563FD File Offset: 0x000545FD
	private void ScaleUpNow(CollectionDeckBoxVisual.OnScaleFinishedCallbackData readyToScaleUpData)
	{
		this.ScaleDeckBox(true, readyToScaleUpData.m_callback, readyToScaleUpData.m_callbackData);
	}

	// Token: 0x06000F69 RID: 3945 RVA: 0x00056412 File Offset: 0x00054612
	public void PlayScaleDownAnimation()
	{
		this.PlayScaleDownAnimation(null);
	}

	// Token: 0x06000F6A RID: 3946 RVA: 0x0005641B File Offset: 0x0005461B
	public void PlayScaleDownAnimation(CollectionDeckBoxVisual.DelOnAnimationFinished callback)
	{
		this.PlayScaleDownAnimation(callback, null);
	}

	// Token: 0x06000F6B RID: 3947 RVA: 0x00056428 File Offset: 0x00054628
	public void PlayScaleDownAnimation(CollectionDeckBoxVisual.DelOnAnimationFinished callback, object callbackData)
	{
		CollectionDeckBoxVisual.OnScaleFinishedCallbackData callbackData2 = new CollectionDeckBoxVisual.OnScaleFinishedCallbackData
		{
			m_callback = callback,
			m_callbackData = callbackData
		};
		this.ScaleDeckBox(false, new CollectionDeckBoxVisual.DelOnAnimationFinished(this.OnScaledDown), callbackData2);
	}

	// Token: 0x06000F6C RID: 3948 RVA: 0x00056460 File Offset: 0x00054660
	private void OnScaledDown(object callbackData)
	{
		CollectionDeckBoxVisual.OnScaleFinishedCallbackData onScaleFinishedCallbackData = callbackData as CollectionDeckBoxVisual.OnScaleFinishedCallbackData;
		Vector3 localPosition = base.transform.localPosition;
		localPosition.y = 1.273138f;
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			localPosition,
			"isLocal",
			true,
			"time",
			0.05f,
			"easetype",
			iTween.EaseType.linear,
			"oncomplete",
			"ScaleDownComplete",
			"oncompletetarget",
			base.gameObject,
			"oncompleteparams",
			onScaleFinishedCallbackData
		});
		iTween.MoveTo(base.gameObject, args);
	}

	// Token: 0x06000F6D RID: 3949 RVA: 0x00056520 File Offset: 0x00054720
	private void ScaleDownComplete(CollectionDeckBoxVisual.OnScaleFinishedCallbackData onScaledDownData)
	{
		if (onScaledDownData.m_callback == null)
		{
			return;
		}
		onScaledDownData.m_callback(onScaledDownData.m_callbackData);
	}

	// Token: 0x06000F6E RID: 3950 RVA: 0x0005653C File Offset: 0x0005473C
	public void PlayPopUpAnimation()
	{
		this.PlayPopUpAnimation(null);
	}

	// Token: 0x06000F6F RID: 3951 RVA: 0x00056545 File Offset: 0x00054745
	public void PlayPopUpAnimation(CollectionDeckBoxVisual.DelOnAnimationFinished callback)
	{
		this.PlayPopUpAnimation(callback, null);
	}

	// Token: 0x06000F70 RID: 3952 RVA: 0x00056550 File Offset: 0x00054750
	public void PlayPopUpAnimation(CollectionDeckBoxVisual.DelOnAnimationFinished callback, object callbackData)
	{
		if (this.m_isPoppedUp)
		{
			if (callback != null)
			{
				callback(callbackData);
			}
			return;
		}
		this.m_isPoppedUp = true;
		if (this.m_customDeckTransform != null)
		{
			this.m_customDeckTransform.localPosition += this.SCALED_UP_DECK_OFFSET;
		}
		base.GetComponent<Animation>()["Deck_PopUp"].time = 0f;
		base.GetComponent<Animation>()["Deck_PopUp"].speed = 6f;
		this.PlayPopAnimation("Deck_PopUp", callback, callbackData);
	}

	// Token: 0x06000F71 RID: 3953 RVA: 0x000565E4 File Offset: 0x000547E4
	public void PlayDesaturationAnimation()
	{
		Animator component = base.GetComponent<Animator>();
		if (component != null)
		{
			component.enabled = true;
			component.Play("CustomDeck_Desat", 0, 0f);
		}
	}

	// Token: 0x06000F72 RID: 3954 RVA: 0x00056619 File Offset: 0x00054819
	public void PlayPopDownAnimation()
	{
		this.PlayPopDownAnimation(null);
	}

	// Token: 0x06000F73 RID: 3955 RVA: 0x00056622 File Offset: 0x00054822
	public void PlayPopDownAnimation(CollectionDeckBoxVisual.DelOnAnimationFinished callback)
	{
		this.PlayPopDownAnimation(callback, null);
	}

	// Token: 0x06000F74 RID: 3956 RVA: 0x0005662C File Offset: 0x0005482C
	public void PlayPopDownAnimation(CollectionDeckBoxVisual.DelOnAnimationFinished callback, object callbackData)
	{
		if (!this.m_isPoppedUp)
		{
			if (callback != null)
			{
				callback(callbackData);
			}
			return;
		}
		this.m_isPoppedUp = false;
		if (this.m_customDeckTransform != null)
		{
			this.m_customDeckTransform.localPosition -= this.SCALED_UP_DECK_OFFSET;
		}
		base.GetComponent<Animation>()["Deck_PopDown"].time = 0f;
		base.GetComponent<Animation>()["Deck_PopDown"].speed = 6f;
		this.PlayPopAnimation("Deck_PopDown", callback, callbackData);
	}

	// Token: 0x06000F75 RID: 3957 RVA: 0x000566BE File Offset: 0x000548BE
	public void PlayPopDownAnimationImmediately()
	{
		this.PlayPopDownAnimationImmediately(null);
	}

	// Token: 0x06000F76 RID: 3958 RVA: 0x000566C7 File Offset: 0x000548C7
	public void PlayPopDownAnimationImmediately(CollectionDeckBoxVisual.DelOnAnimationFinished callback)
	{
		this.PlayPopDownAnimationImmediately(callback, null);
	}

	// Token: 0x06000F77 RID: 3959 RVA: 0x000566D4 File Offset: 0x000548D4
	public void PlayPopDownAnimationImmediately(CollectionDeckBoxVisual.DelOnAnimationFinished callback, object callbackData)
	{
		if (!this.m_isPoppedUp)
		{
			if (callback != null)
			{
				callback(callbackData);
			}
			return;
		}
		this.m_isPoppedUp = false;
		base.GetComponent<Animation>()["Deck_PopDown"].time = base.GetComponent<Animation>()["Deck_PopDown"].length;
		base.GetComponent<Animation>()["Deck_PopDown"].speed = 1f;
		this.PlayPopAnimation("Deck_PopDown", callback, callbackData);
	}

	// Token: 0x06000F78 RID: 3960 RVA: 0x0005674C File Offset: 0x0005494C
	public void SetHighlightMaterialForState(Material mat, ActorStateType stateType)
	{
		bool flag = false;
		foreach (HighlightRenderState highlightRenderState in this.m_highlightState.m_HighlightStates)
		{
			if (highlightRenderState.m_StateType == stateType)
			{
				flag = true;
				highlightRenderState.m_Material = mat;
			}
		}
		if (!flag)
		{
			Log.All.PrintWarning("CollectionDeckBoxVisual - Attempting to set new material for state {0}, but no HighlightRenderState object found for that state type!", new object[]
			{
				stateType
			});
		}
	}

	// Token: 0x06000F79 RID: 3961 RVA: 0x000567D4 File Offset: 0x000549D4
	public void SetHighlightState(ActorStateType stateType)
	{
		if (this.m_highlightState != null)
		{
			if (!this.m_highlightState.IsReady())
			{
				base.StartCoroutine(this.ChangeHighlightStateWhenReady(stateType));
				return;
			}
			this.m_highlightState.ChangeState(stateType);
		}
	}

	// Token: 0x06000F7A RID: 3962 RVA: 0x0005680D File Offset: 0x00054A0D
	private IEnumerator ChangeHighlightStateWhenReady(ActorStateType stateType)
	{
		while (!this.m_highlightState.IsReady())
		{
			yield return null;
		}
		this.m_highlightState.ChangeState(stateType);
		yield break;
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x00056823 File Offset: 0x00054A23
	public void ShowDeleteButton(bool show)
	{
		this.m_deleteButton.gameObject.SetActive(show);
		if (this.IsMissingCards)
		{
			this.m_missingCardsIndicator.SetActive(!show);
		}
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x0005684D File Offset: 0x00054A4D
	public void StoreOriginalButtonPositionAndRotation()
	{
		if (this.ButtonGameObject != null)
		{
			this.m_originalButtonPosition = this.ButtonGameObject.transform.localPosition;
			this.m_originalButtonRotation = this.ButtonGameObject.transform.localRotation;
		}
	}

	// Token: 0x06000F7D RID: 3965 RVA: 0x00056889 File Offset: 0x00054A89
	public void HideBanner()
	{
		this.ShowBannerInternal(false);
	}

	// Token: 0x06000F7E RID: 3966 RVA: 0x00056892 File Offset: 0x00054A92
	public void ShowBanner()
	{
		this.ShowBannerInternal(true);
	}

	// Token: 0x06000F7F RID: 3967 RVA: 0x0005689C File Offset: 0x00054A9C
	public void AssignFromCollectionDeck(CollectionDeck deck)
	{
		if (deck == null)
		{
			return;
		}
		this.SetDeckName(deck.Name);
		this.SetDeckID(deck.ID);
		this.SetHeroCardPremiumOverride(deck.GetDisplayHeroPremiumOverride());
		this.SetHeroCardID(deck.GetDisplayHeroCardID());
		this.SetShowGlow(CollectionDeckBoxVisual.ShouldHighlightDeck(deck));
		this.SetFormatType(CollectionManager.Get().GetThemeShowing(deck));
		this.SetIsShared(deck.IsShared);
		this.UpdateMissingCardsIndicator();
	}

	// Token: 0x06000F80 RID: 3968 RVA: 0x0005690C File Offset: 0x00054B0C
	private CollectionDeckBoxVisual.FormatElements GetFormatElements(FormatType formatType)
	{
		if (this.m_deckID == 0L && formatType == FormatType.FT_UNKNOWN)
		{
			return this.GetStandardFormatElements();
		}
		CollectionDeckBoxVisual.FormatElements formatElements = (from x in this.m_formatElements
		where x.formatType == formatType
		select x).FirstOrDefault<CollectionDeckBoxVisual.FormatElements>();
		if (formatElements == null)
		{
			Debug.LogError("Unsupported format type in CollectionDeckBoxVisual.GetFormatElements: " + formatType.ToString() + ". Will use standard formatting.");
			return this.GetStandardFormatElements();
		}
		return formatElements;
	}

	// Token: 0x06000F81 RID: 3969 RVA: 0x0005698A File Offset: 0x00054B8A
	private CollectionDeckBoxVisual.FormatElements GetStandardFormatElements()
	{
		return (from x in this.m_formatElements
		where x.formatType == FormatType.FT_STANDARD
		select x).FirstOrDefault<CollectionDeckBoxVisual.FormatElements>();
	}

	// Token: 0x06000F82 RID: 3970 RVA: 0x000569BB File Offset: 0x00054BBB
	private CollectionDeckBoxVisual.FormatElements GetActiveFormatElements()
	{
		return this.GetFormatElements(this.m_formatType);
	}

	// Token: 0x06000F83 RID: 3971 RVA: 0x000569C9 File Offset: 0x00054BC9
	private CollectionDeckBoxVisual.FormatElements[] GetInactiveFormatElements()
	{
		return (from x in this.m_formatElements
		where x.formatType != FormatType.FT_UNKNOWN && x.formatType != this.m_formatType
		select x).ToArray<CollectionDeckBoxVisual.FormatElements>();
	}

	// Token: 0x06000F84 RID: 3972 RVA: 0x000569E7 File Offset: 0x00054BE7
	private void ShowBannerInternal(bool show)
	{
		this.m_showBanner = show;
		this.UpdateVisualBannerState();
	}

	// Token: 0x06000F85 RID: 3973 RVA: 0x000569F8 File Offset: 0x00054BF8
	private void UpdateVisualBannerState()
	{
		CollectionDeckBoxVisual.FormatElements activeFormatElements = this.GetActiveFormatElements();
		bool flag = this.IsDeckSelectableForCurrentMode();
		if (activeFormatElements.disabledMeshObject != null)
		{
			activeFormatElements.disabledMeshObject.SetActive(!flag);
		}
		if (activeFormatElements.classObject != null)
		{
			activeFormatElements.classObject.SetActive((flag || UniversalInputManager.UsePhoneUI) && this.m_showBanner);
		}
		if (activeFormatElements.topBannerRenderer != null)
		{
			activeFormatElements.topBannerRenderer.gameObject.SetActive(flag && this.m_showBanner);
		}
	}

	// Token: 0x170000CF RID: 207
	// (get) Token: 0x06000F86 RID: 3974 RVA: 0x00056A8B File Offset: 0x00054C8B
	private GameObject ButtonGameObject
	{
		get
		{
			return this.GetActiveFormatElements().portraitObject;
		}
	}

	// Token: 0x06000F87 RID: 3975 RVA: 0x00056A98 File Offset: 0x00054C98
	private void OnDeleteButtonRollout(UIEvent e)
	{
		this.ShowDeleteButton(false);
	}

	// Token: 0x06000F88 RID: 3976 RVA: 0x00056AA1 File Offset: 0x00054CA1
	private void OnDeleteButtonOver(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("tiny_button_mouseover_1.prefab:0ab88a13f5168ed43a3b53275114a842", base.gameObject);
	}

	// Token: 0x06000F89 RID: 3977 RVA: 0x00056AC0 File Offset: 0x00054CC0
	private void OnDeleteButtonPressed(UIEvent e)
	{
		if (CollectionDeckTray.Get().IsShowingDeckContents())
		{
			return;
		}
		SoundManager.Get().LoadAndPlay("tiny_button_press_1.prefab:44fc68b7418870b4797b85f0ca88a8db", base.gameObject);
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_DELETE_CONFIRM_HEADER");
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_text = GameStrings.Get("GLUE_COLLECTION_DELETE_CONFIRM_DESC");
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnDeleteButtonConfirmationResponse);
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x06000F8A RID: 3978 RVA: 0x00056B4C File Offset: 0x00054D4C
	private void OnDeleteButtonConfirmationResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL)
		{
			return;
		}
		this.SetEnabled(false, false);
		CollectionDeckBoxVisual.DecksContent.DeleteDeck(this.GetDeckID());
	}

	// Token: 0x06000F8B RID: 3979 RVA: 0x00056B6B File Offset: 0x00054D6B
	private void PlayPopAnimation(string animationName)
	{
		this.PlayPopAnimation(animationName, null, null);
	}

	// Token: 0x06000F8C RID: 3980 RVA: 0x00056B78 File Offset: 0x00054D78
	private void PlayPopAnimation(string animationName, CollectionDeckBoxVisual.DelOnAnimationFinished callback, object callbackData)
	{
		base.GetComponent<Animation>().Play(animationName);
		CollectionDeckBoxVisual.OnPopAnimationFinishedCallbackData value = new CollectionDeckBoxVisual.OnPopAnimationFinishedCallbackData
		{
			m_callback = callback,
			m_callbackData = callbackData,
			m_animationName = animationName
		};
		base.StopCoroutine("WaitThenCallAnimationCallback");
		base.StartCoroutine("WaitThenCallAnimationCallback", value);
	}

	// Token: 0x06000F8D RID: 3981 RVA: 0x00056BC5 File Offset: 0x00054DC5
	private IEnumerator WaitThenCallAnimationCallback(CollectionDeckBoxVisual.OnPopAnimationFinishedCallbackData callbackData)
	{
		yield return new WaitForSeconds(base.GetComponent<Animation>()[callbackData.m_animationName].length / base.GetComponent<Animation>()[callbackData.m_animationName].speed);
		bool enabled = callbackData.m_animationName.Equals("Deck_PopUp");
		this.SetEnabled(enabled, false);
		if (callbackData.m_callback == null)
		{
			yield break;
		}
		callbackData.m_callback(callbackData.m_callbackData);
		yield break;
	}

	// Token: 0x06000F8E RID: 3982 RVA: 0x00056BDC File Offset: 0x00054DDC
	private void ScaleDeckBox(bool scaleUp, CollectionDeckBoxVisual.DelOnAnimationFinished callback, object callbackData)
	{
		CollectionDeckBoxVisual.OnScaleFinishedCallbackData onScaleFinishedCallbackData = new CollectionDeckBoxVisual.OnScaleFinishedCallbackData
		{
			m_callback = callback,
			m_callbackData = callbackData
		};
		Vector3 vector = scaleUp ? CollectionDeckBoxVisual.SCALED_UP_LOCAL_SCALE : CollectionDeckBoxVisual.SCALED_DOWN_LOCAL_SCALE;
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			vector,
			"isLocal",
			true,
			"time",
			0.2f,
			"easetype",
			iTween.EaseType.linear,
			"oncomplete",
			"OnScaleComplete",
			"oncompletetarget",
			base.gameObject,
			"oncompleteparams",
			onScaleFinishedCallbackData,
			"name",
			"scale"
		});
		iTween.StopByName(base.gameObject, "scale");
		iTween.ScaleTo(base.gameObject, args);
	}

	// Token: 0x06000F8F RID: 3983 RVA: 0x00056520 File Offset: 0x00054720
	private void OnScaleComplete(CollectionDeckBoxVisual.OnScaleFinishedCallbackData callbackData)
	{
		if (callbackData.m_callback == null)
		{
			return;
		}
		callbackData.m_callback(callbackData.m_callbackData);
	}

	// Token: 0x06000F90 RID: 3984 RVA: 0x00056CC4 File Offset: 0x00054EC4
	private void OnHeroFullDefLoaded(string cardID, DefLoader.DisposableFullDef def, object userData)
	{
		Log.CollectionDeckBox.Print("OnHeroFullDefLoaded cardID: {0},  m_heroCardID: {1}", new object[]
		{
			cardID,
			this.m_heroCardID
		});
		if (cardID == null || !cardID.Equals(this.m_heroCardID))
		{
			return;
		}
		DefLoader.DisposableFullDef fullDef = this.m_fullDef;
		if (fullDef != null)
		{
			fullDef.Dispose();
		}
		this.m_fullDef = def;
		if (this.m_fullDef != null && this.m_fullDef.CardDef != null)
		{
			this.SetPortrait(this.m_fullDef.CardDef.GetCustomDeckPortrait());
		}
		if (this.m_fullDef != null && this.m_fullDef.EntityDef != null)
		{
			TAG_CLASS @class = this.m_fullDef.EntityDef.GetClass();
			this.SetClassDisplay(@class);
		}
		this.UpdateDeckLabel();
	}

	// Token: 0x06000F91 RID: 3985 RVA: 0x00056D84 File Offset: 0x00054F84
	private void UpdatePortraitMaterial(GameObject portraitObject, Material portraitMaterial, int portraitMaterialIndex)
	{
		if (portraitMaterial == null)
		{
			Debug.LogError("Custom Deck Portrait Material is null!");
			return;
		}
		portraitObject.GetComponent<Renderer>().SetSharedMaterial(portraitMaterialIndex, portraitMaterial);
		DefLoader.DisposableFullDef fullDef = this.m_fullDef;
		if (((fullDef != null) ? fullDef.CardDef : null) == null)
		{
			return;
		}
		if (this.m_neverUseGoldenPortraits)
		{
			return;
		}
		if (this.GetHeroCardPremium() != TAG_PREMIUM.GOLDEN && GameUtils.IsVanillaHero(this.m_fullDef.EntityDef.GetCardId()))
		{
			return;
		}
		if (GraphicsManager.Get().isVeryLowQualityDevice())
		{
			return;
		}
		DefLoader.DisposableFullDef fullDef2 = this.m_fullDef;
		Material material = (fullDef2 != null) ? fullDef2.CardDef.GetPremiumPortraitMaterial() : null;
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
		DefLoader.DisposableFullDef fullDef3 = this.m_fullDef;
		UberShaderAnimation uberShaderAnimation = (fullDef3 != null) ? fullDef3.CardDef.GetPremiumPortraitAnimation() : null;
		if (uberShaderAnimation != null)
		{
			UberShaderController uberShaderController = portraitObject.GetComponent<UberShaderController>();
			if (uberShaderController == null)
			{
				uberShaderController = portraitObject.AddComponent<UberShaderController>();
			}
			uberShaderController.UberShaderAnimation = UnityEngine.Object.Instantiate<UberShaderAnimation>(uberShaderAnimation);
			uberShaderController.m_MaterialIndex = portraitMaterialIndex;
		}
	}

	// Token: 0x06000F92 RID: 3986 RVA: 0x00056ED8 File Offset: 0x000550D8
	private void SetPortrait(Material portraitMaterial)
	{
		foreach (CollectionDeckBoxVisual.FormatElements formatElements in this.m_formatElements)
		{
			this.UpdatePortraitMaterial(formatElements.portraitObject, portraitMaterial, formatElements.portraitMaterialIndex);
		}
	}

	// Token: 0x06000F93 RID: 3987 RVA: 0x00056F38 File Offset: 0x00055138
	private void SetClassDisplay(TAG_CLASS classTag)
	{
		foreach (CollectionDeckBoxVisual.FormatElements formatElements in this.m_formatElements)
		{
			if (!(formatElements.classObject == null))
			{
				MeshRenderer component = formatElements.classObject.GetComponent<MeshRenderer>();
				if (component != null)
				{
					Material material = component.GetMaterial(formatElements.classIconMaterialIndex);
					Material material2 = component.GetMaterial(formatElements.classBannerMaterialIndex);
					if (!(material == null) && !(material2 == null))
					{
						material.mainTextureOffset = CollectionPageManager.s_classTextureOffsets[classTag];
						material2.color = CollectionPageManager.ColorForClass(classTag);
						if (formatElements.topBannerRenderer != null)
						{
							formatElements.topBannerRenderer.GetMaterial(this.m_topBannerMaterialIndex).color = CollectionPageManager.ColorForClass(classTag);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000F94 RID: 3988 RVA: 0x00057028 File Offset: 0x00055228
	private void MarkRewardedDeckAsSeen(long deckId)
	{
		long num;
		if (RewardUtils.HasNewRewardedDeck(out num) && deckId == num)
		{
			RewardUtils.MarkNewestRewardedDeckAsSeen();
		}
	}

	// Token: 0x06000F95 RID: 3989 RVA: 0x00057048 File Offset: 0x00055248
	private void MarkDeckAsSeen()
	{
		this.SetHighlightState(ActorStateType.HIGHLIGHT_PRIMARY_MOUSE_OVER);
		CollectionDeck collectionDeck = this.GetCollectionDeck();
		if (collectionDeck != null && collectionDeck.NeedsName)
		{
			Log.CollectionDeckBox.Print(string.Format("Sending deck changes for deck {0}, to clear the NEEDS_NAME flag.", this.m_deckID), Array.Empty<object>());
			collectionDeck.SendChanges();
			collectionDeck.NeedsName = false;
		}
		this.MarkRewardedDeckAsSeen(this.m_deckID);
		this.m_showGlow = false;
	}

	// Token: 0x06000F96 RID: 3990 RVA: 0x000570B3 File Offset: 0x000552B3
	protected override void OnPress()
	{
		if (!this.m_animateButtonPress || this.m_isLocked || this.m_isSelected || !this.IsDeckSelectableForCurrentMode())
		{
			return;
		}
		this.OnPressEvent();
	}

	// Token: 0x06000F97 RID: 3991 RVA: 0x000570DC File Offset: 0x000552DC
	protected override void OnHold()
	{
		CollectionDeckTray collectionDeckTray = CollectionDeckTray.Get();
		if (collectionDeckTray == null)
		{
			return;
		}
		if (collectionDeckTray.GetCurrentContentType() != DeckTray.DeckContentTypes.Decks)
		{
			return;
		}
		CollectionDeckTrayDeckListContent decksContent = collectionDeckTray.GetDecksContent();
		if (decksContent == null)
		{
			return;
		}
		if (decksContent.IsTouchDragging)
		{
			return;
		}
		if (this.m_tooltipZone != null)
		{
			this.m_tooltipZone.HideTooltip();
		}
		this.SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
		if (this.ButtonGameObject != null)
		{
			Hashtable args = iTween.Hash(new object[]
			{
				"scale",
				decksContent.m_rearrangeEnlargeScale * Vector3.one,
				"isLocal",
				true,
				"time",
				decksContent.m_rearrangeStartStopTweenDuration,
				"easeType",
				iTween.EaseType.linear
			});
			iTween.ScaleTo(this.ButtonGameObject, args);
		}
		decksContent.StartDragToReorder(this);
	}

	// Token: 0x06000F98 RID: 3992 RVA: 0x000571C4 File Offset: 0x000553C4
	protected override void OnRelease()
	{
		CollectionDeckTrayDeckListContent decksContent = CollectionDeckBoxVisual.DecksContent;
		if (decksContent != null)
		{
			decksContent.StopDragToReorder();
		}
		if (this.m_isLocked || this.m_isSelected || !this.IsDeckSelectableForCurrentMode())
		{
			return;
		}
		if (!SceneMgr.Get().IsInTavernBrawlMode() || UniversalInputManager.Get().IsTouchMode())
		{
			string deckSelectSound = this.GetActiveFormatElements().deckSelectSound;
			if (!string.IsNullOrEmpty(deckSelectSound))
			{
				SoundManager.Get().LoadAndPlay(deckSelectSound, base.gameObject);
			}
		}
		this.OnReleaseEvent();
	}

	// Token: 0x06000F99 RID: 3993 RVA: 0x00057248 File Offset: 0x00055448
	public void OnStopDragToReorder()
	{
		if (this.ButtonGameObject != null)
		{
			float num = 0.1f;
			CollectionDeckTrayDeckListContent decksContent = CollectionDeckBoxVisual.DecksContent;
			if (decksContent != null)
			{
				num = decksContent.m_rearrangeStartStopTweenDuration;
			}
			Hashtable args = iTween.Hash(new object[]
			{
				"scale",
				Vector3.one,
				"isLocal",
				true,
				"time",
				num,
				"easeType",
				iTween.EaseType.linear
			});
			iTween.ScaleTo(this.ButtonGameObject, args);
		}
		this.OnOutEvent();
	}

	// Token: 0x06000F9A RID: 3994 RVA: 0x000572E6 File Offset: 0x000554E6
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		if (this.m_tooltipZone != null)
		{
			this.m_tooltipZone.HideTooltip();
		}
		this.OnOutEvent();
	}

	// Token: 0x06000F9B RID: 3995 RVA: 0x00057308 File Offset: 0x00055508
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		if (this.m_tooltipZone != null)
		{
			if (SceneMgr.Get().GetMode() != SceneMgr.Mode.ADVENTURE)
			{
				if (((this.m_formatType == FormatType.FT_WILD && Options.GetFormatType() != FormatType.FT_WILD) || (this.m_formatType == FormatType.FT_CLASSIC && Options.GetFormatType() != FormatType.FT_CLASSIC) || (this.m_formatType == FormatType.FT_STANDARD && Options.GetFormatType() == FormatType.FT_CLASSIC)) && Options.GetInRankedPlayMode())
				{
					this.m_tooltipZone.ShowTooltip(GameStrings.Format("GLUE_DISABLED_DECK_HEADER", new object[]
					{
						GameStrings.GetFormatName(this.m_formatType)
					}), GameStrings.Format("GLUE_DISABLED_DECK_DESC", new object[]
					{
						GameStrings.GetFormatName(Options.GetFormatType())
					}), 4f, 0);
				}
			}
			else if (!this.IsDeckSelectableForCurrentMode())
			{
				this.m_tooltipZone.ShowTooltip(GameStrings.Format("GLUE_DISABLED_DECK_HEADER", new object[]
				{
					GameStrings.GetFormatName(this.m_formatType)
				}), GameStrings.Get("GLUE_DISABLED_DECK_IN_CURRENT_MODE_DESC"), 4f, 0);
			}
			else if (this.IsMissingCards)
			{
				this.m_tooltipZone.ShowTooltip(GameStrings.Get("GLUE_INCOMPLETE_DECK_HEADER"), GameStrings.Get("GLUE_INCOMPLETE_DECK_DESC"), 4f, 0);
			}
		}
		this.OnOverEvent();
	}

	// Token: 0x06000F9C RID: 3996 RVA: 0x00057441 File Offset: 0x00055641
	public override void SetEnabled(bool enabled, bool isInternal = false)
	{
		base.SetEnabled(enabled, isInternal);
		if (!enabled && this.m_tooltipZone != null)
		{
			this.m_tooltipZone.HideTooltip();
		}
	}

	// Token: 0x06000F9D RID: 3997 RVA: 0x00057468 File Offset: 0x00055668
	private void OnPressEvent()
	{
		this.ShowDeleteButton(false);
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.COLLECTIONMANAGER)
		{
			Hashtable args = iTween.Hash(new object[]
			{
				"position",
				this.m_pressedBone.transform.localPosition,
				"isLocal",
				true,
				"time",
				0.1,
				"easeType",
				iTween.EaseType.linear
			});
			iTween.MoveTo(this.ButtonGameObject, args);
		}
	}

	// Token: 0x06000F9E RID: 3998 RVA: 0x00057500 File Offset: 0x00055700
	private void OnReleaseEvent()
	{
		if (UniversalInputManager.Get().IsTouchMode() && this.m_showGlow)
		{
			this.MarkDeckAsSeen();
		}
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.COLLECTIONMANAGER)
		{
			Hashtable args = iTween.Hash(new object[]
			{
				"position",
				this.m_originalButtonPosition,
				"isLocal",
				true,
				"time",
				0.1,
				"easeType",
				iTween.EaseType.linear
			});
			iTween.MoveTo(this.ButtonGameObject, args);
		}
	}

	// Token: 0x06000F9F RID: 3999 RVA: 0x000575A0 File Offset: 0x000557A0
	private void OnOutEvent()
	{
		if (!this.m_isSelected)
		{
			this.SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
		}
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.COLLECTIONMANAGER)
		{
			Hashtable args = iTween.Hash(new object[]
			{
				"position",
				this.m_originalButtonPosition,
				"isLocal",
				true,
				"time",
				0.1,
				"easeType",
				iTween.EaseType.linear
			});
			iTween.MoveTo(this.ButtonGameObject, args);
		}
	}

	// Token: 0x06000FA0 RID: 4000 RVA: 0x00057634 File Offset: 0x00055834
	private void OnOverEvent()
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			return;
		}
		CollectionDeckTrayDeckListContent decksContent = CollectionDeckBoxVisual.DecksContent;
		if (decksContent != null && decksContent.DraggingDeckBox != null)
		{
			return;
		}
		SoundManager.Get().LoadAndPlay("collection_manager_hero_mouse_over.prefab:653cc8000b988cd468d2210a209adce6", base.gameObject);
		if (this.m_isSelected)
		{
			return;
		}
		if (this.m_showGlow)
		{
			this.MarkDeckAsSeen();
			return;
		}
		if (this.IsDeckSelectableForCurrentMode())
		{
			this.SetHighlightState(ActorStateType.HIGHLIGHT_MOUSE_OVER);
		}
	}

	// Token: 0x06000FA1 RID: 4001 RVA: 0x000576B0 File Offset: 0x000558B0
	private void ReparentElements(FormatType formatType)
	{
		CollectionDeckBoxVisual.FormatElements formatElements = this.GetFormatElements(formatType);
		Transform transform = formatElements.portraitObject.transform;
		this.m_highlight.transform.parent = transform;
		this.m_deckName.gameObject.transform.parent = transform;
		this.m_deckDesc.gameObject.transform.parent = transform;
		this.m_missingCardsIndicator.gameObject.transform.parent = transform;
		if (UniversalInputManager.UsePhoneUI)
		{
			formatElements.classObject.transform.parent = transform;
		}
		this.m_bones.m_gradientOneLine.parent = transform;
		this.m_bones.m_gradientTwoLine.parent = transform;
	}

	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x00057764 File Offset: 0x00055964
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

	// Token: 0x06000FA3 RID: 4003 RVA: 0x00057788 File Offset: 0x00055988
	private static bool ShouldHighlightDeck(CollectionDeck deck)
	{
		long num;
		return deck.NeedsName || (RewardUtils.HasNewRewardedDeck(out num) && deck.ID == num);
	}

	// Token: 0x04000A58 RID: 2648
	public UberText m_deckName;

	// Token: 0x04000A59 RID: 2649
	public UberText m_deckDesc;

	// Token: 0x04000A5A RID: 2650
	public GameObject m_labelGradient;

	// Token: 0x04000A5B RID: 2651
	public PegUIElement m_deleteButton;

	// Token: 0x04000A5C RID: 2652
	public GameObject m_highlight;

	// Token: 0x04000A5D RID: 2653
	public List<CollectionDeckBoxVisual.FormatElements> m_formatElements;

	// Token: 0x04000A5E RID: 2654
	public GameObject m_missingCardsIndicator;

	// Token: 0x04000A5F RID: 2655
	public UberText m_missingCardsIndicatorText;

	// Token: 0x04000A60 RID: 2656
	public int m_topBannerMaterialIndex;

	// Token: 0x04000A61 RID: 2657
	public GameObject m_pressedBone;

	// Token: 0x04000A62 RID: 2658
	public CustomDeckBones m_bones;

	// Token: 0x04000A63 RID: 2659
	public GameObject m_normalDeckVisuals;

	// Token: 0x04000A64 RID: 2660
	public GameObject m_lockedDeckVisuals;

	// Token: 0x04000A65 RID: 2661
	public TooltipZone m_tooltipZone;

	// Token: 0x04000A66 RID: 2662
	public GameObject m_renameVisuals;

	// Token: 0x04000A67 RID: 2663
	public bool m_neverUseGoldenPortraits;

	// Token: 0x04000A69 RID: 2665
	public static readonly float POPPED_UP_LOCAL_Z = 0f;

	// Token: 0x04000A6A RID: 2666
	public static readonly Vector3 POPPED_DOWN_LOCAL_POS = new Vector3(0f, -0.8598533f, 0f);

	// Token: 0x04000A6B RID: 2667
	public const float DECKBOX_SCALE = 0.95f;

	// Token: 0x04000A6C RID: 2668
	public static readonly Vector3 SCALED_DOWN_LOCAL_SCALE = new Vector3(0.95f, 0.95f, 0.95f);

	// Token: 0x04000A6D RID: 2669
	public const float SCALED_UP_LOCAL_Y_OFFSET = 3.238702f;

	// Token: 0x04000A6E RID: 2670
	public const float SCALED_DOWN_LOCAL_Y_OFFSET = 1.273138f;

	// Token: 0x04000A6F RID: 2671
	private const float BUTTON_POP_SPEED = 6f;

	// Token: 0x04000A70 RID: 2672
	private const string DECKBOX_POPUP_ANIM_NAME = "Deck_PopUp";

	// Token: 0x04000A71 RID: 2673
	private const string DECKBOX_POPDOWN_ANIM_NAME = "Deck_PopDown";

	// Token: 0x04000A72 RID: 2674
	private const string DECKBOX_DESATURATION_ANIM_NAME = "CustomDeck_Desat";

	// Token: 0x04000A73 RID: 2675
	private Vector3 SCALED_UP_DECK_OFFSET = new Vector3(0f, 0f, 0f);

	// Token: 0x04000A74 RID: 2676
	private const float SCALE_TIME = 0.2f;

	// Token: 0x04000A75 RID: 2677
	private const float ADJUST_Y_OFFSET_ANIM_TIME = 0.05f;

	// Token: 0x04000A76 RID: 2678
	private static readonly Color DECK_DESC_ENABLED_COLOR = new Color(0.97f, 0.82f, 0.22f);

	// Token: 0x04000A77 RID: 2679
	private static readonly Color DECK_NAME_ENABLED_COLOR = Color.white;

	// Token: 0x04000A78 RID: 2680
	private long m_deckID = -1L;

	// Token: 0x04000A79 RID: 2681
	private bool m_isPoppedUp;

	// Token: 0x04000A7A RID: 2682
	private bool m_isShown;

	// Token: 0x04000A7B RID: 2683
	private DefLoader.DisposableFullDef m_fullDef;

	// Token: 0x04000A7C RID: 2684
	private bool m_isShared;

	// Token: 0x04000A7D RID: 2685
	private HighlightState m_highlightState;

	// Token: 0x04000A7E RID: 2686
	private string m_heroCardID = "";

	// Token: 0x04000A7F RID: 2687
	private TAG_PREMIUM? m_heroCardPremiumOverride;

	// Token: 0x04000A80 RID: 2688
	private FormatType m_formatType = FormatType.FT_WILD;

	// Token: 0x04000A81 RID: 2689
	private Vector3 m_originalButtonPosition;

	// Token: 0x04000A82 RID: 2690
	private Quaternion m_originalButtonRotation;

	// Token: 0x04000A83 RID: 2691
	private bool m_animateButtonPress = true;

	// Token: 0x04000A84 RID: 2692
	private bool m_wasTouchModeEnabled;

	// Token: 0x04000A85 RID: 2693
	private int m_positionIndex;

	// Token: 0x04000A86 RID: 2694
	private bool m_showGlow;

	// Token: 0x04000A87 RID: 2695
	private bool m_isLocked;

	// Token: 0x04000A88 RID: 2696
	private bool m_forceSingleLineDeckName;

	// Token: 0x04000A89 RID: 2697
	private bool m_isSelected;

	// Token: 0x04000A8A RID: 2698
	private float m_wiggleIntensity;

	// Token: 0x04000A8B RID: 2699
	private bool m_showBanner = true;

	// Token: 0x04000A8C RID: 2700
	private Transform m_customDeckTransform;

	// Token: 0x02001424 RID: 5156
	[Serializable]
	public class FormatElements
	{
		// Token: 0x0400A913 RID: 43283
		[SerializeField]
		public FormatType formatType;

		// Token: 0x0400A914 RID: 43284
		[SerializeField]
		public Texture2D highlight;

		// Token: 0x0400A915 RID: 43285
		[SerializeField]
		public GameObject portraitObject;

		// Token: 0x0400A916 RID: 43286
		[SerializeField]
		public int portraitMaterialIndex;

		// Token: 0x0400A917 RID: 43287
		[SerializeField]
		public GameObject classObject;

		// Token: 0x0400A918 RID: 43288
		[SerializeField]
		public int classIconMaterialIndex;

		// Token: 0x0400A919 RID: 43289
		[SerializeField]
		public int classBannerMaterialIndex;

		// Token: 0x0400A91A RID: 43290
		[SerializeField]
		public MeshRenderer topBannerRenderer;

		// Token: 0x0400A91B RID: 43291
		[SerializeField]
		public Material xButtonMaterial;

		// Token: 0x0400A91C RID: 43292
		[CustomEditField(T = EditType.SOUND_PREFAB)]
		public string deckSelectSound;

		// Token: 0x0400A91D RID: 43293
		[SerializeField]
		public GameObject disabledMeshObject;
	}

	// Token: 0x02001425 RID: 5157
	// (Invoke) Token: 0x0600D9D1 RID: 55761
	public delegate void DelOnAnimationFinished(object callbackData);

	// Token: 0x02001426 RID: 5158
	private class OnPopAnimationFinishedCallbackData
	{
		// Token: 0x0400A91E RID: 43294
		public string m_animationName;

		// Token: 0x0400A91F RID: 43295
		public CollectionDeckBoxVisual.DelOnAnimationFinished m_callback;

		// Token: 0x0400A920 RID: 43296
		public object m_callbackData;
	}

	// Token: 0x02001427 RID: 5159
	private class OnScaleFinishedCallbackData
	{
		// Token: 0x0400A921 RID: 43297
		public CollectionDeckBoxVisual.DelOnAnimationFinished m_callback;

		// Token: 0x0400A922 RID: 43298
		public object m_callbackData;
	}
}
