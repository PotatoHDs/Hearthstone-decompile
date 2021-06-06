using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Core;
using Hearthstone.Core;
using PegasusShared;
using UnityEngine;

// Token: 0x02000127 RID: 295
public class DeckTemplatePicker : MonoBehaviour
{
	// Token: 0x0600138E RID: 5006 RVA: 0x000706AC File Offset: 0x0006E8AC
	private void Awake()
	{
		this.m_currentSelectedDeck = this.m_customDeck;
		for (int i = 0; i < 3; i++)
		{
			int idx = i;
			DeckTemplatePickerButton deckTemplatePickerButton = (DeckTemplatePickerButton)GameUtils.Instantiate(this.m_pickerButtonTpl, this.m_pickerButtonRoot.gameObject, true);
			Vector3 zero = Vector3.zero;
			if (UniversalInputManager.UsePhoneUI)
			{
				zero.x = 0.75f;
			}
			this.m_pickerButtonRoot.AddObject(deckTemplatePickerButton, zero, true);
			deckTemplatePickerButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.SelectButtonWithIndex(idx);
			});
			deckTemplatePickerButton.gameObject.SetActive(true);
			this.m_pickerButtons.Add(deckTemplatePickerButton);
		}
		if (this.m_pickerButtons.Count > 0)
		{
			this.m_pickerButtons[0].SetIsCoreDeck(true);
		}
		this.m_pickerButtonRoot.UpdatePositions();
		this.m_pickerButtonTpl.gameObject.SetActive(false);
		if (this.m_customDeckButton != null)
		{
			this.m_customDeckButton.gameObject.SetActive(true);
			this.m_customDeckButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.SelectCustomDeckButton(false);
			});
		}
		if (this.m_chooseButton != null)
		{
			this.m_chooseButton.Disable(false);
			this.m_chooseButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.ChooseRecipeAndFillInCards();
			});
		}
		if (this.m_phoneTray != null)
		{
			this.m_phoneTray.m_scrollbar.SaveScroll("start");
			this.m_phoneTray.gameObject.SetActive(false);
		}
		if (this.m_bottomPanel != null)
		{
			this.m_origBottomPanelPos = this.m_bottomPanel.transform.localPosition;
		}
		if (this.m_phoneBackButton != null)
		{
			this.m_phoneBackButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.OnBackButtonPressed(e);
			});
		}
		TransformUtil.CopyLocal(this.m_customDeckInitialPosition, this.m_customDeckButton.transform);
	}

	// Token: 0x0600138F RID: 5007 RVA: 0x00004EB5 File Offset: 0x000030B5
	private void OnBackButtonPressed(UIEvent e)
	{
		Navigation.GoBack();
	}

	// Token: 0x06001390 RID: 5008 RVA: 0x0007089C File Offset: 0x0006EA9C
	private IEnumerator BackOut()
	{
		CollectionManager.Get().GetCollectibleDisplay().EnableInput(false);
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(CollectionDeckTray.Get().OnBackOutOfDeckContents));
		yield return base.StartCoroutine(this.ShowPacks(false));
		CollectionDeckTray deckTray = CollectionDeckTray.Get();
		deckTray.OnBackOutOfDeckContentsImpl(true);
		while (!deckTray.m_cardsContent.HasFinishedExiting())
		{
			yield return null;
		}
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.EnterSelectNewDeckHeroMode();
			HeroPickerDisplay heroPickerDisplay = collectionManagerDisplay.GetHeroPickerDisplay();
			while (heroPickerDisplay != null && !heroPickerDisplay.IsShown())
			{
				yield return null;
			}
			heroPickerDisplay = null;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			base.StartCoroutine(this.HideTrays());
		}
		CollectionManager.Get().GetCollectibleDisplay().EnableInput(true);
		yield break;
	}

	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x06001391 RID: 5009 RVA: 0x000708AB File Offset: 0x0006EAAB
	public FormatType CurrentSelectedFormat
	{
		get
		{
			return this.m_currentSelectedFormat;
		}
	}

	// Token: 0x06001392 RID: 5010 RVA: 0x000708B3 File Offset: 0x0006EAB3
	public bool OnNavigateBack()
	{
		base.StartCoroutine(this.BackOut());
		return true;
	}

	// Token: 0x06001393 RID: 5011 RVA: 0x000708C3 File Offset: 0x0006EAC3
	public void RegisterOnTemplateDeckChosen(DeckTemplatePicker.OnTemplateDeckChosen dlg)
	{
		this.m_templateDeckChosenListeners.Add(dlg);
	}

	// Token: 0x06001394 RID: 5012 RVA: 0x000708D1 File Offset: 0x0006EAD1
	public void UnregisterOnTemplateDeckChosen(DeckTemplatePicker.OnTemplateDeckChosen dlg)
	{
		this.m_templateDeckChosenListeners.Remove(dlg);
	}

	// Token: 0x06001395 RID: 5013 RVA: 0x000708E0 File Offset: 0x0006EAE0
	public bool IsShowingBottomPanel()
	{
		return this.m_showingBottomPanel;
	}

	// Token: 0x06001396 RID: 5014 RVA: 0x000708E8 File Offset: 0x0006EAE8
	public bool IsShowingPacks()
	{
		return this.m_packsShown;
	}

	// Token: 0x06001397 RID: 5015 RVA: 0x000708F0 File Offset: 0x0006EAF0
	public IEnumerator Show(bool show)
	{
		if (show)
		{
			this.m_root.SetActive(true);
			this.m_showingBottomPanel = false;
			this.m_packsShown = false;
			this.m_pickerButtonRoot.UpdatePositions();
			TransformUtil.CopyLocal(this.m_customDeckButton.transform, this.m_customDeckInitialPosition);
			this.m_customDeckButton.GetComponentInChildren<UberText>().Text = GameStrings.Get(GameStrings.Get("GLUE_DECK_TEMPLATE_CUSTOM_DECK"));
			if (CollectionManager.Get().GetEditedDeck() == null)
			{
				yield break;
			}
			this.SetupTemplateButtons(this.m_customDeck);
			this.m_chooseButton.Disable(false);
			if (this.m_deckTemplateDescription != null)
			{
				this.m_deckTemplateDescription.Text = GameStrings.Get("GLUE_COLLECTION_DECK_TEMPLATE_SELECT_A_DECK");
			}
			CollectionDeckTray.Get().GetCardsContent().ResetFakeDeck();
			if (this.m_phoneTray != null)
			{
				this.m_phoneTray.m_cardsContent.ResetFakeDeck();
			}
			this.FillWithCustomDeck();
			this.m_currentSelectedDeck = this.m_customDeck;
			if (!UniversalInputManager.UsePhoneUI)
			{
				this.OnTrayToggled(true);
			}
			Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
			if (!CollectionManager.Get().ShouldShowDeckTemplatePageForClass(this.m_currentSelectedClass) && !UniversalInputManager.UsePhoneUI)
			{
				this.SelectCustomDeckButton(true);
			}
			this.ShowBottomPanel(true);
			yield return base.StartCoroutine(this.ShowPacks(true));
			while (CollectionDeckTray.Get() == null || CollectionDeckTray.Get().GetCurrentContentType() != DeckTray.DeckContentTypes.Cards)
			{
				yield return null;
			}
		}
		else if (this.m_root.activeSelf)
		{
			yield return base.StartCoroutine(this.ShowPacks(false));
			CollectionDeckTray.Get().GetCardsContent().ResetFakeDeck();
			this.ShowBottomPanel(true);
			this.m_root.SetActive(false);
		}
		yield break;
	}

	// Token: 0x06001398 RID: 5016 RVA: 0x00070908 File Offset: 0x0006EB08
	private void SetupTemplateButtons(CollectionManager.TemplateDeck refDeck)
	{
		List<CollectionManager.TemplateDeck> nonStarterTemplateDecks = CollectionManager.Get().GetNonStarterTemplateDecks(this.m_currentSelectedFormat, this.m_currentSelectedClass);
		if (nonStarterTemplateDecks == null)
		{
			Log.Decks.PrintWarning("SetupTemplateButtons with class {0} which had no template decks", new object[]
			{
				this.m_currentSelectedClass
			});
			return;
		}
		int num = 0;
		while (num < this.m_pickerButtons.Count && num < nonStarterTemplateDecks.Count)
		{
			CollectionManager.TemplateDeck templateDeck = nonStarterTemplateDecks[num];
			bool flag = refDeck == templateDeck;
			if (flag)
			{
				this.m_currentSelectedDeck = templateDeck;
			}
			this.m_pickerButtons[num].SetSelected(false);
			if (flag && this.m_deckTemplateDescription != null)
			{
				this.m_deckTemplateDescription.Text = templateDeck.m_description;
			}
			if (flag && this.m_deckTemplatePhoneName != null)
			{
				this.m_deckTemplatePhoneName.Text = templateDeck.m_title;
			}
			this.m_pickerButtons[num].transform.localEulerAngles = Vector3.zero;
			this.m_pickerButtons[num].GetComponent<RandomTransform>().Apply();
			this.m_pickerButtons[num].GetComponent<AnimatedLowPolyPack>().Init(0, this.m_pickerButtons[num].transform.localPosition, this.m_pickerButtons[num].transform.localPosition + this.m_offscreenPackOffset, false, false);
			this.m_pickerButtons[num].GetComponent<AnimatedLowPolyPack>().SetFlyingLocalRotations(this.m_pickerButtons[num].transform.localEulerAngles, this.m_pickerButtons[num].transform.localEulerAngles);
			num++;
		}
		if (this.m_customDeckButton != null)
		{
			this.m_customDeckButton.SetSelected(false);
			this.m_customDeckButton.transform.localEulerAngles = Vector3.zero;
			this.m_customDeckButton.GetComponent<AnimatedLowPolyPack>().Init(0, this.m_customDeckButton.transform.localPosition, this.m_customDeckButton.transform.localPosition + this.m_offscreenPackOffset, false, false);
			this.m_customDeckButton.GetComponent<AnimatedLowPolyPack>().SetFlyingLocalRotations(this.m_customDeckButton.transform.localEulerAngles, this.m_customDeckButton.transform.localEulerAngles);
		}
	}

	// Token: 0x06001399 RID: 5017 RVA: 0x00070B46 File Offset: 0x0006ED46
	public IEnumerator ShowPacks(bool show)
	{
		float delay = 0f;
		if (show)
		{
			CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
			if (collectionManagerDisplay != null)
			{
				HeroPickerDisplay heroPickerDisplay = collectionManagerDisplay.GetHeroPickerDisplay();
				while (heroPickerDisplay != null && !heroPickerDisplay.IsHidden())
				{
					yield return new WaitForEndOfFrame();
				}
				heroPickerDisplay = null;
			}
		}
		DeckTemplatePickerButton[] array = this.m_pickerButtons.ToArray();
		GeneralUtils.Shuffle<DeckTemplatePickerButton>(array);
		foreach (DeckTemplatePickerButton deckTemplatePickerButton in array)
		{
			if (show)
			{
				deckTemplatePickerButton.GetComponent<AnimatedLowPolyPack>().FlyIn(this.m_packAnimInTime, delay);
			}
			else
			{
				deckTemplatePickerButton.GetComponent<AnimatedLowPolyPack>().FlyOut(this.m_packAnimOutTime, delay);
			}
			yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f * this.m_packAnimInTime, 0.4f * this.m_packAnimInTime));
		}
		DeckTemplatePickerButton[] array2 = null;
		if (show)
		{
			this.m_customDeckButton.GetComponent<AnimatedLowPolyPack>().FlyIn(this.m_packAnimInTime, delay);
			yield return new WaitForSeconds(this.m_packAnimInTime + delay);
		}
		else
		{
			this.m_customDeckButton.GetComponent<AnimatedLowPolyPack>().FlyOut(this.m_packAnimOutTime, delay);
			yield return new WaitForSeconds(this.m_packAnimOutTime + delay);
		}
		this.m_packsShown = show;
		yield break;
	}

	// Token: 0x0600139A RID: 5018 RVA: 0x00070B5C File Offset: 0x0006ED5C
	public void ShowBottomPanel(bool show)
	{
		if (this.m_bottomPanel != null)
		{
			Vector3 vector = this.m_origBottomPanelPos;
			Vector3 vector2 = this.m_origBottomPanelPos;
			float num = 0f;
			if (show)
			{
				vector2 += this.m_bottomPanelHideOffset;
				num = this.m_bottomPanelSlideInWaitDelay;
				this.m_showingBottomPanel = true;
			}
			else
			{
				vector += this.m_bottomPanelHideOffset;
				Processor.ScheduleCallback(this.m_bottomPanelAnimateTime, false, delegate(object o)
				{
					this.m_showingBottomPanel = show;
				}, null);
			}
			iTween.Stop(this.m_bottomPanel);
			this.m_bottomPanel.transform.localPosition = vector2;
			iTween.MoveTo(this.m_bottomPanel, iTween.Hash(new object[]
			{
				"position",
				vector,
				"isLocal",
				true,
				"time",
				this.m_bottomPanelAnimateTime,
				"delay",
				num
			}));
		}
	}

	// Token: 0x0600139B RID: 5019 RVA: 0x00070C69 File Offset: 0x0006EE69
	public void OnTrayToggled(bool shown)
	{
		if (shown)
		{
			base.StartCoroutine(this.ShowTutorialPopup());
			return;
		}
		CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.CARDS, true, null);
	}

	// Token: 0x0600139C RID: 5020 RVA: 0x00070C8E File Offset: 0x0006EE8E
	private IEnumerator ShowTutorialPopup()
	{
		yield return new WaitForSeconds(0.5f);
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null && !Options.Get().GetBool(Option.HAS_SEEN_DECK_TEMPLATE_SCREEN, false) && UserAttentionManager.CanShowAttentionGrabber("DeckTemplatePicker.ShowTutorialPopup:" + Option.HAS_SEEN_DECK_TEMPLATE_SCREEN))
		{
			Transform deckTemplateTutorialWelcomeBone = collectionManagerDisplay.m_deckTemplateTutorialWelcomeBone;
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, deckTemplateTutorialWelcomeBone.localPosition, GameStrings.Get("GLUE_COLLECTION_TUTORIAL_TEMPLATE_WELCOME"), "VO_INNKEEPER_Male_Dwarf_RECIPE1_01.prefab:0261ef622a5e2b945a8f89e87cbe01a7", 3f, null, false);
			Options.Get().SetBool(Option.HAS_SEEN_DECK_TEMPLATE_SCREEN, true);
		}
		yield break;
	}

	// Token: 0x0600139D RID: 5021 RVA: 0x00070C98 File Offset: 0x0006EE98
	public void SetDeckFormatAndClass(FormatType deckFormat, TAG_CLASS deckClass)
	{
		this.m_currentSelectedFormat = deckFormat;
		this.m_currentSelectedClass = deckClass;
		List<CollectionManager.TemplateDeck> nonStarterTemplateDecks = CollectionManager.Get().GetNonStarterTemplateDecks(this.m_currentSelectedFormat, this.m_currentSelectedClass);
		int num = (nonStarterTemplateDecks != null) ? nonStarterTemplateDecks.Count : 0;
		Color color = CollectionPageManager.ColorForClass(deckClass);
		this.m_pageHeaderText.Text = GameStrings.Format("GLUE_DECK_TEMPLATE_CHOOSE_DECK", new object[]
		{
			GameStrings.GetClassName(deckClass)
		});
		CollectionPageDisplay.SetPageFlavorTextures(this.m_pageHeader, CollectionPageDisplay.TagClassToHeaderClass(deckClass));
		for (int i = 0; i < this.m_pickerButtons.Count; i++)
		{
			DeckTemplatePickerButton deckTemplatePickerButton = this.m_pickerButtons[i];
			bool flag = i < num;
			deckTemplatePickerButton.gameObject.SetActive(flag);
			if (flag)
			{
				CollectionManager.TemplateDeck templateDeck = nonStarterTemplateDecks[i];
				deckTemplatePickerButton.SetTitleText(templateDeck.m_title);
				int num2 = 0;
				foreach (KeyValuePair<string, int> keyValuePair in templateDeck.m_cardIds)
				{
					int num3;
					int num4;
					int num5;
					CollectionManager.Get().GetOwnedCardCount(keyValuePair.Key, out num3, out num4, out num5);
					int num6 = Mathf.Min(num3 + num4 + num5, keyValuePair.Value);
					num2 += num6;
				}
				deckTemplatePickerButton.SetCardCountText(num2);
				deckTemplatePickerButton.SetDeckRecipeArt(templateDeck.m_displayTexture);
				deckTemplatePickerButton.m_packRibbon.GetMaterial().color = color;
			}
		}
		if (this.m_customDeckButton != null)
		{
			this.m_customDeckButton.m_deckTexture.GetMaterial().mainTextureOffset = CollectionPageManager.s_classTextureOffsets[deckClass];
			this.m_customDeckButton.m_packRibbon.GetMaterial().color = color;
		}
	}

	// Token: 0x0600139E RID: 5022 RVA: 0x00070E58 File Offset: 0x0006F058
	private void SelectButtonWithIndex(int index)
	{
		Action action = delegate()
		{
			if (this.m_chooseButton != null)
			{
				this.m_chooseButton.Enable();
			}
			List<CollectionManager.TemplateDeck> nonStarterTemplateDecks = CollectionManager.Get().GetNonStarterTemplateDecks(this.m_currentSelectedFormat, this.m_currentSelectedClass);
			CollectionManager.TemplateDeck templateDeck = this.m_customDeck;
			if (nonStarterTemplateDecks != null && index < nonStarterTemplateDecks.Count)
			{
				templateDeck = nonStarterTemplateDecks[index];
			}
			for (int i = 0; i < this.m_pickerButtons.Count; i++)
			{
				this.m_pickerButtons[i].SetSelected(i == index);
			}
			if (this.m_deckTemplateDescription != null)
			{
				this.m_deckTemplateDescription.Text = templateDeck.m_description;
			}
			if (this.m_deckTemplatePhoneName != null)
			{
				this.m_deckTemplatePhoneName.Text = templateDeck.m_title;
			}
			if (this.m_customDeckButton != null)
			{
				this.m_customDeckButton.SetSelected(false);
			}
			this.m_currentSelectedDeck = templateDeck;
			if (UniversalInputManager.UsePhoneUI)
			{
				if (this.m_phoneTray.GetComponent<SlidingTray>().TraySliderIsAnimating())
				{
					return;
				}
				this.m_phoneTray.gameObject.SetActive(true);
				this.m_phoneTray.GetComponent<SlidingTray>().ShowTray();
				this.m_phoneTray.m_scrollbar.LoadScroll("start", false);
				this.m_phoneTray.FlashDeckTemplateHighlight();
			}
			else
			{
				CollectionDeckTray collectionDeckTray = CollectionDeckTray.Get();
				if (collectionDeckTray != null)
				{
					collectionDeckTray.FlashDeckTemplateHighlight();
				}
			}
			this.FillDeckWithTemplate(this.m_currentSelectedDeck);
			this.StartCoroutine(this.ShowTips());
		};
		if (index < this.m_pickerButtons.Count && this.m_pickerButtons[index].GetOwnedCardCount() < 10 && !this.m_pickerButtons[index].IsCoreDeck())
		{
			this.ShowLowCardsPopup(action, null);
			return;
		}
		action();
	}

	// Token: 0x0600139F RID: 5023 RVA: 0x00070EDA File Offset: 0x0006F0DA
	public IEnumerator ShowTips()
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			while (this.m_phoneTray.GetComponent<SlidingTray>().TraySliderIsAnimating())
			{
				yield return null;
			}
		}
		yield break;
	}

	// Token: 0x060013A0 RID: 5024 RVA: 0x00070EEC File Offset: 0x0006F0EC
	private void FillDeckWithTemplate(CollectionManager.TemplateDeck tplDeck)
	{
		CollectionDeck editingDeck = CollectionDeckTray.Get().GetCardsContent().GetEditingDeck();
		if (editingDeck == null)
		{
			return;
		}
		if (tplDeck == null)
		{
			CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
			editingDeck.CopyFrom(editedDeck);
		}
		else
		{
			editingDeck.FillFromTemplateDeck(tplDeck);
		}
		CollectionDeckTray.Get().m_cardsContent.UpdateCardList();
		CollectionDeckTray.Get().m_decksContent.UpdateDeckName(null);
		if (this.m_phoneTray != null)
		{
			CollectionDeck editingDeck2 = this.m_phoneTray.m_cardsContent.GetEditingDeck();
			if (tplDeck == null)
			{
				CollectionDeck editedDeck2 = CollectionManager.Get().GetEditedDeck();
				editingDeck2.CopyFrom(editedDeck2);
			}
			else
			{
				editingDeck2.FillFromTemplateDeck(tplDeck);
			}
			this.m_phoneTray.m_cardsContent.UpdateCardList();
			SceneUtils.SetLayer(this.m_phoneTray, GameLayer.IgnoreFullScreenEffects);
		}
	}

	// Token: 0x060013A1 RID: 5025 RVA: 0x00070FA4 File Offset: 0x0006F1A4
	private void FillWithCustomDeck()
	{
		this.FillDeckWithTemplate(null);
	}

	// Token: 0x060013A2 RID: 5026 RVA: 0x00070FB0 File Offset: 0x0006F1B0
	private void FireOnTemplateDeckChosenEvent()
	{
		DeckTemplatePicker.OnTemplateDeckChosen[] array = this.m_templateDeckChosenListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	// Token: 0x060013A3 RID: 5027 RVA: 0x00070FDF File Offset: 0x0006F1DF
	private IEnumerator HideTrays()
	{
		SlidingTray phoneTray = this.m_phoneTray.GetComponent<SlidingTray>();
		phoneTray.HideTray();
		while (phoneTray.isActiveAndEnabled && !phoneTray.IsTrayInShownPosition())
		{
			yield return new WaitForEndOfFrame();
		}
		base.GetComponent<SlidingTray>().HideTray();
		yield break;
	}

	// Token: 0x060013A4 RID: 5028 RVA: 0x00070FF0 File Offset: 0x0006F1F0
	private void ChooseRecipeAndFillInCards()
	{
		CollectionManager.Get().SetShowDeckTemplatePageForClass(this.m_currentSelectedClass, this.m_currentSelectedDeck != this.m_customDeck);
		CollectionDeckTray.Get().GetCardsContent().CommitFakeDeckChanges();
		this.FireOnTemplateDeckChosenEvent();
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (this.m_currentSelectedDeck != this.m_customDeck)
		{
			editedDeck.SourceType = DeckSourceType.DECK_SOURCE_TYPE_TEMPLATE;
			Network.Get().SetDeckTemplateSource(editedDeck.ID, this.m_currentSelectedDeck.m_id);
		}
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		if (UniversalInputManager.UsePhoneUI)
		{
			base.StartCoroutine(this.EnterDeckPhone());
		}
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null && CollectionManager.Get().ShouldShowWildToStandardTutorial(true) && editedDeck.FormatType == FormatType.FT_STANDARD)
		{
			collectionManagerDisplay.ShowStandardInfoTutorial(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS);
		}
	}

	// Token: 0x060013A5 RID: 5029 RVA: 0x000710D0 File Offset: 0x0006F2D0
	private void ShowLowCardsPopup(Action confirmAction, Action cancelAction = null)
	{
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_COLLECTION_DECK_TEMPLATE_LOW_CARD_WARNING"),
			m_text = GameStrings.Get("GLUE_COLLECTION_DECK_TEMPLATE_LOW_CARD_WARNING_MESSAGE"),
			m_cancelText = GameStrings.Get("GLUE_COLLECTION_DECK_TEMPLATE_LOW_CARD_WARNING_CANCEL"),
			m_confirmText = GameStrings.Get("GLUE_COLLECTION_DECK_TEMPLATE_LOW_CARD_WARNING_CONFIRM"),
			m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
			m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				if (response == AlertPopup.Response.CONFIRM)
				{
					confirmAction();
					return;
				}
				if (response == AlertPopup.Response.CANCEL && cancelAction != null)
				{
					cancelAction();
				}
			}
		};
		DialogManager.Get().ShowPopup(info);
	}

	// Token: 0x060013A6 RID: 5030 RVA: 0x0007115C File Offset: 0x0006F35C
	private void SelectCustomDeckButton(bool preselect = false)
	{
		CollectionDeckTray collectionDeckTray = CollectionDeckTray.Get();
		if (collectionDeckTray != null && !preselect)
		{
			collectionDeckTray.FlashDeckTemplateHighlight();
		}
		if (this.m_chooseButton != null)
		{
			this.m_chooseButton.Enable();
		}
		for (int i = 0; i < this.m_pickerButtons.Count; i++)
		{
			this.m_pickerButtons[i].SetSelected(false);
		}
		if (this.m_customDeckButton != null)
		{
			this.m_customDeckButton.SetSelected(true);
		}
		if (this.m_deckTemplateDescription != null)
		{
			this.m_deckTemplateDescription.Text = GameStrings.Get("GLUE_DECK_TEMPLATE_CUSTOM_DECK_DESCRIPTION");
		}
		this.FillWithCustomDeck();
		this.m_currentSelectedDeck = this.m_customDeck;
		if (UniversalInputManager.UsePhoneUI && !preselect)
		{
			this.ChooseRecipeAndFillInCards();
		}
	}

	// Token: 0x060013A7 RID: 5031 RVA: 0x00071226 File Offset: 0x0006F426
	public IEnumerator EnterDeckPhone()
	{
		yield return base.StartCoroutine(this.ShowPacks(false));
		yield return base.StartCoroutine(this.HideTrays());
		yield break;
	}

	// Token: 0x04000CC2 RID: 3266
	public GameObject m_root;

	// Token: 0x04000CC3 RID: 3267
	public GameObject m_pageHeader;

	// Token: 0x04000CC4 RID: 3268
	public UberText m_pageHeaderText;

	// Token: 0x04000CC5 RID: 3269
	public UIBObjectSpacing m_pickerButtonRoot;

	// Token: 0x04000CC6 RID: 3270
	public DeckTemplatePickerButton m_pickerButtonTpl;

	// Token: 0x04000CC7 RID: 3271
	public DeckTemplatePickerButton m_customDeckButton;

	// Token: 0x04000CC8 RID: 3272
	public UberText m_deckTemplateDescription;

	// Token: 0x04000CC9 RID: 3273
	public UberText m_deckTemplatePhoneName;

	// Token: 0x04000CCA RID: 3274
	public PlayButton m_chooseButton;

	// Token: 0x04000CCB RID: 3275
	public GameObject m_bottomPanel;

	// Token: 0x04000CCC RID: 3276
	public DeckTemplatePhoneTray m_phoneTray;

	// Token: 0x04000CCD RID: 3277
	public UIBButton m_phoneBackButton;

	// Token: 0x04000CCE RID: 3278
	public Vector3 m_bottomPanelHideOffset = new Vector3(0f, 0f, 25f);

	// Token: 0x04000CCF RID: 3279
	public float m_bottomPanelSlideInWaitDelay = 0.25f;

	// Token: 0x04000CD0 RID: 3280
	public float m_bottomPanelAnimateTime = 0.25f;

	// Token: 0x04000CD1 RID: 3281
	public float m_packAnimInTime = 0.25f;

	// Token: 0x04000CD2 RID: 3282
	public float m_packAnimOutTime = 0.2f;

	// Token: 0x04000CD3 RID: 3283
	public Vector3 m_offscreenPackOffset;

	// Token: 0x04000CD4 RID: 3284
	public Transform m_ghostCardTipBone;

	// Token: 0x04000CD5 RID: 3285
	private List<DeckTemplatePickerButton> m_pickerButtons = new List<DeckTemplatePickerButton>();

	// Token: 0x04000CD6 RID: 3286
	private CollectionManager.TemplateDeck m_customDeck = new CollectionManager.TemplateDeck();

	// Token: 0x04000CD7 RID: 3287
	private TAG_CLASS m_currentSelectedClass;

	// Token: 0x04000CD8 RID: 3288
	private FormatType m_currentSelectedFormat;

	// Token: 0x04000CD9 RID: 3289
	private CollectionManager.TemplateDeck m_currentSelectedDeck;

	// Token: 0x04000CDA RID: 3290
	private List<DeckTemplatePicker.OnTemplateDeckChosen> m_templateDeckChosenListeners = new List<DeckTemplatePicker.OnTemplateDeckChosen>();

	// Token: 0x04000CDB RID: 3291
	private Vector3 m_origBottomPanelPos;

	// Token: 0x04000CDC RID: 3292
	private bool m_showingBottomPanel;

	// Token: 0x04000CDD RID: 3293
	private TransformProps m_customDeckInitialPosition = new TransformProps();

	// Token: 0x04000CDE RID: 3294
	private bool m_packsShown;

	// Token: 0x020014B9 RID: 5305
	// (Invoke) Token: 0x0600DBFC RID: 56316
	public delegate void OnTemplateDeckChosen();
}
