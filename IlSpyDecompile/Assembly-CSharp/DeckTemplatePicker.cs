using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Core;
using Hearthstone.Core;
using PegasusShared;
using UnityEngine;

public class DeckTemplatePicker : MonoBehaviour
{
	public delegate void OnTemplateDeckChosen();

	public GameObject m_root;

	public GameObject m_pageHeader;

	public UberText m_pageHeaderText;

	public UIBObjectSpacing m_pickerButtonRoot;

	public DeckTemplatePickerButton m_pickerButtonTpl;

	public DeckTemplatePickerButton m_customDeckButton;

	public UberText m_deckTemplateDescription;

	public UberText m_deckTemplatePhoneName;

	public PlayButton m_chooseButton;

	public GameObject m_bottomPanel;

	public DeckTemplatePhoneTray m_phoneTray;

	public UIBButton m_phoneBackButton;

	public Vector3 m_bottomPanelHideOffset = new Vector3(0f, 0f, 25f);

	public float m_bottomPanelSlideInWaitDelay = 0.25f;

	public float m_bottomPanelAnimateTime = 0.25f;

	public float m_packAnimInTime = 0.25f;

	public float m_packAnimOutTime = 0.2f;

	public Vector3 m_offscreenPackOffset;

	public Transform m_ghostCardTipBone;

	private List<DeckTemplatePickerButton> m_pickerButtons = new List<DeckTemplatePickerButton>();

	private CollectionManager.TemplateDeck m_customDeck = new CollectionManager.TemplateDeck();

	private TAG_CLASS m_currentSelectedClass;

	private FormatType m_currentSelectedFormat;

	private CollectionManager.TemplateDeck m_currentSelectedDeck;

	private List<OnTemplateDeckChosen> m_templateDeckChosenListeners = new List<OnTemplateDeckChosen>();

	private Vector3 m_origBottomPanelPos;

	private bool m_showingBottomPanel;

	private TransformProps m_customDeckInitialPosition = new TransformProps();

	private bool m_packsShown;

	public FormatType CurrentSelectedFormat => m_currentSelectedFormat;

	private void Awake()
	{
		m_currentSelectedDeck = m_customDeck;
		for (int i = 0; i < 3; i++)
		{
			int idx = i;
			DeckTemplatePickerButton deckTemplatePickerButton = (DeckTemplatePickerButton)GameUtils.Instantiate(m_pickerButtonTpl, m_pickerButtonRoot.gameObject, withRotation: true);
			Vector3 zero = Vector3.zero;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				zero.x = 0.75f;
			}
			m_pickerButtonRoot.AddObject(deckTemplatePickerButton, zero);
			deckTemplatePickerButton.AddEventListener(UIEventType.RELEASE, delegate
			{
				SelectButtonWithIndex(idx);
			});
			deckTemplatePickerButton.gameObject.SetActive(value: true);
			m_pickerButtons.Add(deckTemplatePickerButton);
		}
		if (m_pickerButtons.Count > 0)
		{
			m_pickerButtons[0].SetIsCoreDeck(isCore: true);
		}
		m_pickerButtonRoot.UpdatePositions();
		m_pickerButtonTpl.gameObject.SetActive(value: false);
		if (m_customDeckButton != null)
		{
			m_customDeckButton.gameObject.SetActive(value: true);
			m_customDeckButton.AddEventListener(UIEventType.RELEASE, delegate
			{
				SelectCustomDeckButton();
			});
		}
		if (m_chooseButton != null)
		{
			m_chooseButton.Disable();
			m_chooseButton.AddEventListener(UIEventType.RELEASE, delegate
			{
				ChooseRecipeAndFillInCards();
			});
		}
		if (m_phoneTray != null)
		{
			m_phoneTray.m_scrollbar.SaveScroll("start");
			m_phoneTray.gameObject.SetActive(value: false);
		}
		if (m_bottomPanel != null)
		{
			m_origBottomPanelPos = m_bottomPanel.transform.localPosition;
		}
		if (m_phoneBackButton != null)
		{
			m_phoneBackButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				OnBackButtonPressed(e);
			});
		}
		TransformUtil.CopyLocal(m_customDeckInitialPosition, m_customDeckButton.transform);
	}

	private void OnBackButtonPressed(UIEvent e)
	{
		Navigation.GoBack();
	}

	private IEnumerator BackOut()
	{
		CollectionManager.Get().GetCollectibleDisplay().EnableInput(enable: false);
		Navigation.RemoveHandler(CollectionDeckTray.Get().OnBackOutOfDeckContents);
		yield return StartCoroutine(ShowPacks(show: false));
		CollectionDeckTray deckTray = CollectionDeckTray.Get();
		deckTray.OnBackOutOfDeckContentsImpl(deleteDeck: true);
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
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			StartCoroutine(HideTrays());
		}
		CollectionManager.Get().GetCollectibleDisplay().EnableInput(enable: true);
	}

	public bool OnNavigateBack()
	{
		StartCoroutine(BackOut());
		return true;
	}

	public void RegisterOnTemplateDeckChosen(OnTemplateDeckChosen dlg)
	{
		m_templateDeckChosenListeners.Add(dlg);
	}

	public void UnregisterOnTemplateDeckChosen(OnTemplateDeckChosen dlg)
	{
		m_templateDeckChosenListeners.Remove(dlg);
	}

	public bool IsShowingBottomPanel()
	{
		return m_showingBottomPanel;
	}

	public bool IsShowingPacks()
	{
		return m_packsShown;
	}

	public IEnumerator Show(bool show)
	{
		if (show)
		{
			m_root.SetActive(value: true);
			m_showingBottomPanel = false;
			m_packsShown = false;
			m_pickerButtonRoot.UpdatePositions();
			TransformUtil.CopyLocal(m_customDeckButton.transform, m_customDeckInitialPosition);
			m_customDeckButton.GetComponentInChildren<UberText>().Text = GameStrings.Get(GameStrings.Get("GLUE_DECK_TEMPLATE_CUSTOM_DECK"));
			if (CollectionManager.Get().GetEditedDeck() != null)
			{
				SetupTemplateButtons(m_customDeck);
				m_chooseButton.Disable();
				if (m_deckTemplateDescription != null)
				{
					m_deckTemplateDescription.Text = GameStrings.Get("GLUE_COLLECTION_DECK_TEMPLATE_SELECT_A_DECK");
				}
				CollectionDeckTray.Get().GetCardsContent().ResetFakeDeck();
				if (m_phoneTray != null)
				{
					m_phoneTray.m_cardsContent.ResetFakeDeck();
				}
				FillWithCustomDeck();
				m_currentSelectedDeck = m_customDeck;
				if (!UniversalInputManager.UsePhoneUI)
				{
					OnTrayToggled(shown: true);
				}
				Navigation.Push(OnNavigateBack);
				if (!CollectionManager.Get().ShouldShowDeckTemplatePageForClass(m_currentSelectedClass) && !UniversalInputManager.UsePhoneUI)
				{
					SelectCustomDeckButton(preselect: true);
				}
				ShowBottomPanel(show: true);
				yield return StartCoroutine(ShowPacks(show: true));
				while (CollectionDeckTray.Get() == null || CollectionDeckTray.Get().GetCurrentContentType() != DeckTray.DeckContentTypes.Cards)
				{
					yield return null;
				}
			}
		}
		else if (m_root.activeSelf)
		{
			yield return StartCoroutine(ShowPacks(show: false));
			CollectionDeckTray.Get().GetCardsContent().ResetFakeDeck();
			ShowBottomPanel(show: true);
			m_root.SetActive(value: false);
		}
	}

	private void SetupTemplateButtons(CollectionManager.TemplateDeck refDeck)
	{
		List<CollectionManager.TemplateDeck> nonStarterTemplateDecks = CollectionManager.Get().GetNonStarterTemplateDecks(m_currentSelectedFormat, m_currentSelectedClass);
		if (nonStarterTemplateDecks == null)
		{
			Log.Decks.PrintWarning("SetupTemplateButtons with class {0} which had no template decks", m_currentSelectedClass);
			return;
		}
		for (int i = 0; i < m_pickerButtons.Count && i < nonStarterTemplateDecks.Count; i++)
		{
			CollectionManager.TemplateDeck templateDeck = nonStarterTemplateDecks[i];
			bool num = refDeck == templateDeck;
			if (num)
			{
				m_currentSelectedDeck = templateDeck;
			}
			m_pickerButtons[i].SetSelected(selected: false);
			if (num && m_deckTemplateDescription != null)
			{
				m_deckTemplateDescription.Text = templateDeck.m_description;
			}
			if (num && m_deckTemplatePhoneName != null)
			{
				m_deckTemplatePhoneName.Text = templateDeck.m_title;
			}
			m_pickerButtons[i].transform.localEulerAngles = Vector3.zero;
			m_pickerButtons[i].GetComponent<RandomTransform>().Apply();
			m_pickerButtons[i].GetComponent<AnimatedLowPolyPack>().Init(0, m_pickerButtons[i].transform.localPosition, m_pickerButtons[i].transform.localPosition + m_offscreenPackOffset, ignoreFullscreenEffects: false, changeActivation: false);
			m_pickerButtons[i].GetComponent<AnimatedLowPolyPack>().SetFlyingLocalRotations(m_pickerButtons[i].transform.localEulerAngles, m_pickerButtons[i].transform.localEulerAngles);
		}
		if (m_customDeckButton != null)
		{
			m_customDeckButton.SetSelected(selected: false);
			m_customDeckButton.transform.localEulerAngles = Vector3.zero;
			m_customDeckButton.GetComponent<AnimatedLowPolyPack>().Init(0, m_customDeckButton.transform.localPosition, m_customDeckButton.transform.localPosition + m_offscreenPackOffset, ignoreFullscreenEffects: false, changeActivation: false);
			m_customDeckButton.GetComponent<AnimatedLowPolyPack>().SetFlyingLocalRotations(m_customDeckButton.transform.localEulerAngles, m_customDeckButton.transform.localEulerAngles);
		}
	}

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
			}
		}
		DeckTemplatePickerButton[] array = m_pickerButtons.ToArray();
		GeneralUtils.Shuffle(array);
		DeckTemplatePickerButton[] array2 = array;
		foreach (DeckTemplatePickerButton deckTemplatePickerButton in array2)
		{
			if (show)
			{
				deckTemplatePickerButton.GetComponent<AnimatedLowPolyPack>().FlyIn(m_packAnimInTime, delay);
			}
			else
			{
				deckTemplatePickerButton.GetComponent<AnimatedLowPolyPack>().FlyOut(m_packAnimOutTime, delay);
			}
			yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f * m_packAnimInTime, 0.4f * m_packAnimInTime));
		}
		if (show)
		{
			m_customDeckButton.GetComponent<AnimatedLowPolyPack>().FlyIn(m_packAnimInTime, delay);
			yield return new WaitForSeconds(m_packAnimInTime + delay);
		}
		else
		{
			m_customDeckButton.GetComponent<AnimatedLowPolyPack>().FlyOut(m_packAnimOutTime, delay);
			yield return new WaitForSeconds(m_packAnimOutTime + delay);
		}
		m_packsShown = show;
	}

	public void ShowBottomPanel(bool show)
	{
		if (!(m_bottomPanel != null))
		{
			return;
		}
		Vector3 origBottomPanelPos = m_origBottomPanelPos;
		Vector3 origBottomPanelPos2 = m_origBottomPanelPos;
		float num = 0f;
		if (show)
		{
			origBottomPanelPos2 += m_bottomPanelHideOffset;
			num = m_bottomPanelSlideInWaitDelay;
			m_showingBottomPanel = true;
		}
		else
		{
			origBottomPanelPos += m_bottomPanelHideOffset;
			Processor.ScheduleCallback(m_bottomPanelAnimateTime, realTime: false, delegate
			{
				m_showingBottomPanel = show;
			});
		}
		iTween.Stop(m_bottomPanel);
		m_bottomPanel.transform.localPosition = origBottomPanelPos2;
		iTween.MoveTo(m_bottomPanel, iTween.Hash("position", origBottomPanelPos, "isLocal", true, "time", m_bottomPanelAnimateTime, "delay", num));
	}

	public void OnTrayToggled(bool shown)
	{
		if (shown)
		{
			StartCoroutine(ShowTutorialPopup());
		}
		else
		{
			CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.CARDS, triggerResponse: true);
		}
	}

	private IEnumerator ShowTutorialPopup()
	{
		yield return new WaitForSeconds(0.5f);
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null && !Options.Get().GetBool(Option.HAS_SEEN_DECK_TEMPLATE_SCREEN, defaultVal: false) && UserAttentionManager.CanShowAttentionGrabber("DeckTemplatePicker.ShowTutorialPopup:" + Option.HAS_SEEN_DECK_TEMPLATE_SCREEN))
		{
			Transform deckTemplateTutorialWelcomeBone = collectionManagerDisplay.m_deckTemplateTutorialWelcomeBone;
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, deckTemplateTutorialWelcomeBone.localPosition, GameStrings.Get("GLUE_COLLECTION_TUTORIAL_TEMPLATE_WELCOME"), "VO_INNKEEPER_Male_Dwarf_RECIPE1_01.prefab:0261ef622a5e2b945a8f89e87cbe01a7", 3f);
			Options.Get().SetBool(Option.HAS_SEEN_DECK_TEMPLATE_SCREEN, val: true);
		}
	}

	public void SetDeckFormatAndClass(FormatType deckFormat, TAG_CLASS deckClass)
	{
		m_currentSelectedFormat = deckFormat;
		m_currentSelectedClass = deckClass;
		List<CollectionManager.TemplateDeck> nonStarterTemplateDecks = CollectionManager.Get().GetNonStarterTemplateDecks(m_currentSelectedFormat, m_currentSelectedClass);
		int num = nonStarterTemplateDecks?.Count ?? 0;
		Color color = CollectionPageManager.ColorForClass(deckClass);
		m_pageHeaderText.Text = GameStrings.Format("GLUE_DECK_TEMPLATE_CHOOSE_DECK", GameStrings.GetClassName(deckClass));
		CollectionPageDisplay.SetPageFlavorTextures(m_pageHeader, CollectionPageDisplay.TagClassToHeaderClass(deckClass));
		for (int i = 0; i < m_pickerButtons.Count; i++)
		{
			DeckTemplatePickerButton deckTemplatePickerButton = m_pickerButtons[i];
			bool flag = i < num;
			deckTemplatePickerButton.gameObject.SetActive(flag);
			if (!flag)
			{
				continue;
			}
			CollectionManager.TemplateDeck templateDeck = nonStarterTemplateDecks[i];
			deckTemplatePickerButton.SetTitleText(templateDeck.m_title);
			int num2 = 0;
			foreach (KeyValuePair<string, int> cardId in templateDeck.m_cardIds)
			{
				CollectionManager.Get().GetOwnedCardCount(cardId.Key, out var normal, out var golden, out var diamond);
				int num3 = Mathf.Min(normal + golden + diamond, cardId.Value);
				num2 += num3;
			}
			deckTemplatePickerButton.SetCardCountText(num2);
			deckTemplatePickerButton.SetDeckRecipeArt(templateDeck.m_displayTexture);
			deckTemplatePickerButton.m_packRibbon.GetMaterial().color = color;
		}
		if (m_customDeckButton != null)
		{
			m_customDeckButton.m_deckTexture.GetMaterial().mainTextureOffset = CollectionPageManager.s_classTextureOffsets[deckClass];
			m_customDeckButton.m_packRibbon.GetMaterial().color = color;
		}
	}

	private void SelectButtonWithIndex(int index)
	{
		Action action = delegate
		{
			if (m_chooseButton != null)
			{
				m_chooseButton.Enable();
			}
			List<CollectionManager.TemplateDeck> nonStarterTemplateDecks = CollectionManager.Get().GetNonStarterTemplateDecks(m_currentSelectedFormat, m_currentSelectedClass);
			CollectionManager.TemplateDeck templateDeck = m_customDeck;
			if (nonStarterTemplateDecks != null && index < nonStarterTemplateDecks.Count)
			{
				templateDeck = nonStarterTemplateDecks[index];
			}
			for (int i = 0; i < m_pickerButtons.Count; i++)
			{
				m_pickerButtons[i].SetSelected(i == index);
			}
			if (m_deckTemplateDescription != null)
			{
				m_deckTemplateDescription.Text = templateDeck.m_description;
			}
			if (m_deckTemplatePhoneName != null)
			{
				m_deckTemplatePhoneName.Text = templateDeck.m_title;
			}
			if (m_customDeckButton != null)
			{
				m_customDeckButton.SetSelected(selected: false);
			}
			m_currentSelectedDeck = templateDeck;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				if (m_phoneTray.GetComponent<SlidingTray>().TraySliderIsAnimating())
				{
					return;
				}
				m_phoneTray.gameObject.SetActive(value: true);
				m_phoneTray.GetComponent<SlidingTray>().ShowTray();
				m_phoneTray.m_scrollbar.LoadScroll("start", snap: false);
				m_phoneTray.FlashDeckTemplateHighlight();
			}
			else
			{
				CollectionDeckTray collectionDeckTray = CollectionDeckTray.Get();
				if (collectionDeckTray != null)
				{
					collectionDeckTray.FlashDeckTemplateHighlight();
				}
			}
			FillDeckWithTemplate(m_currentSelectedDeck);
			StartCoroutine(ShowTips());
		};
		if (index < m_pickerButtons.Count && m_pickerButtons[index].GetOwnedCardCount() < 10 && !m_pickerButtons[index].IsCoreDeck())
		{
			ShowLowCardsPopup(action);
		}
		else
		{
			action();
		}
	}

	public IEnumerator ShowTips()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			while (m_phoneTray.GetComponent<SlidingTray>().TraySliderIsAnimating())
			{
				yield return null;
			}
		}
	}

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
		CollectionDeckTray.Get().m_decksContent.UpdateDeckName();
		if (m_phoneTray != null)
		{
			CollectionDeck editingDeck2 = m_phoneTray.m_cardsContent.GetEditingDeck();
			if (tplDeck == null)
			{
				CollectionDeck editedDeck2 = CollectionManager.Get().GetEditedDeck();
				editingDeck2.CopyFrom(editedDeck2);
			}
			else
			{
				editingDeck2.FillFromTemplateDeck(tplDeck);
			}
			m_phoneTray.m_cardsContent.UpdateCardList();
			SceneUtils.SetLayer(m_phoneTray, GameLayer.IgnoreFullScreenEffects);
		}
	}

	private void FillWithCustomDeck()
	{
		FillDeckWithTemplate(null);
	}

	private void FireOnTemplateDeckChosenEvent()
	{
		OnTemplateDeckChosen[] array = m_templateDeckChosenListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	private IEnumerator HideTrays()
	{
		SlidingTray phoneTray = m_phoneTray.GetComponent<SlidingTray>();
		phoneTray.HideTray();
		while (phoneTray.isActiveAndEnabled && !phoneTray.IsTrayInShownPosition())
		{
			yield return new WaitForEndOfFrame();
		}
		GetComponent<SlidingTray>().HideTray();
	}

	private void ChooseRecipeAndFillInCards()
	{
		CollectionManager.Get().SetShowDeckTemplatePageForClass(m_currentSelectedClass, m_currentSelectedDeck != m_customDeck);
		CollectionDeckTray.Get().GetCardsContent().CommitFakeDeckChanges();
		FireOnTemplateDeckChosenEvent();
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (m_currentSelectedDeck != m_customDeck)
		{
			editedDeck.SourceType = DeckSourceType.DECK_SOURCE_TYPE_TEMPLATE;
			Network.Get().SetDeckTemplateSource(editedDeck.ID, m_currentSelectedDeck.m_id);
		}
		Navigation.RemoveHandler(OnNavigateBack);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			StartCoroutine(EnterDeckPhone());
		}
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null && CollectionManager.Get().ShouldShowWildToStandardTutorial() && editedDeck.FormatType == FormatType.FT_STANDARD)
		{
			collectionManagerDisplay.ShowStandardInfoTutorial(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS);
		}
	}

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
				switch (response)
				{
				case AlertPopup.Response.CONFIRM:
					confirmAction();
					break;
				case AlertPopup.Response.CANCEL:
					if (cancelAction != null)
					{
						cancelAction();
					}
					break;
				}
			}
		};
		DialogManager.Get().ShowPopup(info);
	}

	private void SelectCustomDeckButton(bool preselect = false)
	{
		CollectionDeckTray collectionDeckTray = CollectionDeckTray.Get();
		if (collectionDeckTray != null && !preselect)
		{
			collectionDeckTray.FlashDeckTemplateHighlight();
		}
		if (m_chooseButton != null)
		{
			m_chooseButton.Enable();
		}
		for (int i = 0; i < m_pickerButtons.Count; i++)
		{
			m_pickerButtons[i].SetSelected(selected: false);
		}
		if (m_customDeckButton != null)
		{
			m_customDeckButton.SetSelected(selected: true);
		}
		if (m_deckTemplateDescription != null)
		{
			m_deckTemplateDescription.Text = GameStrings.Get("GLUE_DECK_TEMPLATE_CUSTOM_DECK_DESCRIPTION");
		}
		FillWithCustomDeck();
		m_currentSelectedDeck = m_customDeck;
		if ((bool)UniversalInputManager.UsePhoneUI && !preselect)
		{
			ChooseRecipeAndFillInCards();
		}
	}

	public IEnumerator EnterDeckPhone()
	{
		yield return StartCoroutine(ShowPacks(show: false));
		yield return StartCoroutine(HideTrays());
	}
}
