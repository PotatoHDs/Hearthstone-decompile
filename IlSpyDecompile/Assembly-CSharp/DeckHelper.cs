using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckHelper : MonoBehaviour
{
	public delegate void DelCompleteCallback(List<EntityDef> chosenCards);

	public UberText m_instructionText;

	public UberText m_replaceText;

	public GameObject m_rootObject;

	public UIBButton m_suggestDoneButton;

	public UIBButton m_replaceDoneButton;

	public PegUIElement m_inputBlocker;

	public Vector3 m_deckCardLocalScale = new Vector3(5.75f, 5.75f, 5.75f);

	public GameObject m_3choiceContainer;

	public GameObject m_replaceContainer;

	public GameObject m_2choiceContainer;

	public Vector3 m_cardSpacing;

	public GameObject m_suggestACardPane;

	public GameObject m_replaceACardPane;

	public string m_replaceACardSound;

	public UIBButton m_innkeeperPopup;

	private static DeckHelper s_instance;

	private Actor m_replaceCardActor;

	private List<Actor> m_choiceActors = new List<Actor>();

	private bool m_shown;

	private DeckTrayDeckTileVisual m_tileToRemove;

	private EntityDef m_cardToRemove;

	private bool m_continueAfterReplace;

	private bool m_shouldHandleDeckChanged = true;

	private bool m_ReplaceSingleTemplateCard = true;

	private Vector3 m_innkeeperFullScale;

	private bool m_innkeeperPopupShown;

	private const float INNKEEPER_POPUP_DURATION = 7f;

	private List<EntityDef> m_chosenCards = new List<EntityDef>();

	private DelCompleteCallback m_onCompleteCallback;

	private void Awake()
	{
		s_instance = this;
		m_rootObject.SetActive(value: false);
		m_replaceDoneButton.AddEventListener(UIEventType.RELEASE, EndButtonClick);
		m_suggestDoneButton.AddEventListener(UIEventType.RELEASE, EndButtonClick);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			if (m_innkeeperPopup != null)
			{
				m_innkeeperFullScale = m_innkeeperPopup.gameObject.transform.localScale;
				m_innkeeperPopup.AddEventListener(UIEventType.RELEASE, InnkeeperPopupClicked);
			}
		}
		else
		{
			m_inputBlocker.AddEventListener(UIEventType.RELEASE, EndButtonClick);
		}
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	private void EndButtonClick(UIEvent e)
	{
		Navigation.GoBack();
	}

	public static DeckHelper Get()
	{
		if (s_instance == null)
		{
			string text = (UniversalInputManager.UsePhoneUI ? "DeckHelper_phone.prefab:e2c93e38a85f44eadb1aee945b1c4636" : "DeckHelper.prefab:69e71904d55994cc28b41f5950e6608f");
			s_instance = AssetLoader.Get().InstantiatePrefab(text).GetComponent<DeckHelper>();
		}
		return s_instance;
	}

	public bool IsActive()
	{
		return m_shown;
	}

	public void OnCardAdded(CollectionDeck deck)
	{
		if (IsActive() && m_shouldHandleDeckChanged)
		{
			HandleDeckChanged(deck, m_continueAfterReplace);
		}
	}

	public static bool HasChoicesToOffer(CollectionDeck deck)
	{
		return DeckMaker.GetFillCardChoices(deck, null, 1).m_addChoices.Count > 0;
	}

	public void UpdateChoices()
	{
		CleanOldChoices();
		if (!IsActive())
		{
			return;
		}
		EntityDef entityDef = m_cardToRemove;
		m_cardToRemove = null;
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		DeckMaker.DeckChoiceFill cardsToShow = DeckMaker.GetFillCardChoices(editedDeck, entityDef, 3);
		if (entityDef == null && cardsToShow.m_removeTemplate != null)
		{
			entityDef = cardsToShow.m_removeTemplate;
		}
		string reason = cardsToShow.m_reason;
		if (cardsToShow == null || cardsToShow.m_addChoices.Count == 0)
		{
			Debug.LogError("DeckHelper.GetChoices() - Can't find choices!!!!");
			return;
		}
		if (m_instructionText != null)
		{
			bool flag = !m_instructionText.Text.Equals(reason);
			m_instructionText.Text = reason;
			if ((bool)UniversalInputManager.UsePhoneUI && flag)
			{
				if (NotificationManager.Get().IsQuotePlaying)
				{
					m_instructionText.Text = "";
				}
				else
				{
					ShowInnkeeperPopup();
				}
			}
		}
		m_replaceACardPane.SetActive(entityDef != null);
		m_suggestACardPane.SetActive(entityDef == null);
		if (entityDef != null)
		{
			if (m_tileToRemove != null)
			{
				m_tileToRemove.SetHighlight(highlight: false);
			}
			m_tileToRemove = CollectionDeckTray.Get().GetCardTileVisual(entityDef.GetCardId());
			if (m_tileToRemove == null)
			{
				Hide();
				return;
			}
			GhostCard.Type ghostTypeFromSlot = GhostCard.GetGhostTypeFromSlot(editedDeck, m_tileToRemove.GetSlot());
			m_replaceCardActor = LoadBestCardActor(entityDef, TAG_PREMIUM.NORMAL, ghostTypeFromSlot);
			if (m_replaceCardActor != null)
			{
				GameUtils.SetParent(m_replaceCardActor, m_replaceContainer);
				RenderToTexture component = m_replaceCardActor.m_ghostCardGameObject.GetComponent<RenderToTexture>();
				BoxCollider boxCollider = m_replaceCardActor.m_ghostCardGameObject.AddComponent<BoxCollider>();
				boxCollider.size = new Vector3(component.m_Width, 2f, component.m_Height);
				boxCollider.gameObject.AddComponent<PegUIElement>().AddEventListener(UIEventType.RELEASE, delegate
				{
					OnGhostCardRelease(m_replaceCardActor, cardsToShow.m_removeTemplate);
				});
			}
			if (m_replaceText != null)
			{
				if (ghostTypeFromSlot == GhostCard.Type.NOT_VALID)
				{
					if (!GameUtils.IsCardGameplayEventActive(entityDef))
					{
						m_replaceText.Text = GameStrings.Get("GLUE_COLLECTION_DECK_HELPER_REPLACE_UNPLAYABLE_CARD");
					}
					else if (CollectionManager.Get().ShouldAccountSeeStandardWild())
					{
						m_replaceText.Text = GameStrings.Get("GLUE_COLLECTION_DECK_HELPER_REPLACE_INVALID_CARD");
					}
					else
					{
						m_replaceText.Text = GameStrings.Get("GLUE_COLLECTION_DECK_HELPER_REPLACE_INVALID_CARD_NPR_NEW");
					}
				}
				else
				{
					m_replaceText.Text = GameStrings.Get("GLUE_COLLECTION_DECK_HELPER_REPLACE_CARD");
				}
			}
			if (m_tileToRemove.GetSlot().Owned && !Options.Get().GetBool(Option.HAS_SEEN_DECK_TEMPLATE_GHOST_CARD, defaultVal: false))
			{
				Options.Get().SetBool(Option.HAS_SEEN_DECK_TEMPLATE_GHOST_CARD, val: true);
			}
			if (!editedDeck.IsValidSlot(m_tileToRemove.GetSlot()) && !Options.Get().GetBool(Option.HAS_SEEN_INVALID_ROTATED_CARD, defaultVal: false))
			{
				Options.Get().SetBool(Option.HAS_SEEN_INVALID_ROTATED_CARD, val: true);
			}
			if (m_replaceACardSound != string.Empty)
			{
				SoundManager.Get().LoadAndPlay(m_replaceACardSound);
			}
		}
		bool num = entityDef != null;
		int num2 = Mathf.Min(num ? 2 : 3, cardsToShow.m_addChoices.Count);
		GameObject parent = (num ? m_2choiceContainer : m_3choiceContainer);
		for (int i = 0; i < num2; i++)
		{
			EntityDef entityDef2 = cardsToShow.m_addChoices[i];
			TAG_PREMIUM premiumToUse = TAG_PREMIUM.NORMAL;
			if (editedDeck.CanAddOwnedCard(entityDef2.GetCardId(), TAG_PREMIUM.DIAMOND))
			{
				premiumToUse = TAG_PREMIUM.DIAMOND;
			}
			else if (editedDeck.CanAddOwnedCard(entityDef2.GetCardId(), TAG_PREMIUM.GOLDEN))
			{
				premiumToUse = TAG_PREMIUM.GOLDEN;
			}
			Actor actor = LoadBestCardActor(entityDef2, premiumToUse);
			if (!(actor == null))
			{
				GameUtils.SetParent(actor, parent);
				PegUIElement pegUIElement = actor.GetCollider().gameObject.AddComponent<PegUIElement>();
				pegUIElement.AddEventListener(UIEventType.RELEASE, delegate
				{
					OnVisualRelease(actor, cardsToShow.m_removeTemplate);
				});
				pegUIElement.AddEventListener(UIEventType.ROLLOVER, delegate
				{
					OnVisualOver(actor);
				});
				pegUIElement.AddEventListener(UIEventType.ROLLOUT, delegate
				{
					OnVisualOut(actor);
				});
				m_choiceActors.Add(actor);
			}
		}
		PositionAndShowChoices();
	}

	private Actor LoadBestCardActor(EntityDef entityDef, TAG_PREMIUM premiumToUse, GhostCard.Type ghostCard = GhostCard.Type.NONE)
	{
		using DefLoader.DisposableCardDef disposableCardDef = DefLoader.Get().GetCardDef(entityDef.GetCardId(), new CardPortraitQuality(3, premiumToUse));
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(entityDef, premiumToUse), AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogWarning($"DeckHelper - FAILED to load actor \"{base.name}\"");
			return null;
		}
		Actor component = gameObject.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarning($"DeckHelper - ERROR actor \"{base.name}\" has no Actor component");
			return null;
		}
		component.transform.parent = base.transform;
		SceneUtils.SetLayer(component, base.gameObject.layer);
		component.SetEntityDef(entityDef);
		component.SetCardDef(disposableCardDef);
		component.SetPremium(premiumToUse);
		component.GhostCardEffect(ghostCard, premiumToUse);
		component.UpdateAllComponents();
		component.Hide();
		component.gameObject.name = disposableCardDef.CardDef.name + "_actor";
		return component;
	}

	private void CleanOldChoices()
	{
		foreach (Actor choiceActor in m_choiceActors)
		{
			Object.Destroy(choiceActor.gameObject);
		}
		m_choiceActors.Clear();
		if (m_replaceCardActor != null)
		{
			Object.Destroy(m_replaceCardActor.gameObject);
			m_replaceCardActor = null;
		}
	}

	private void PositionAndShowChoices()
	{
		for (int i = 0; i < m_choiceActors.Count; i++)
		{
			Actor actor = m_choiceActors[i];
			actor.transform.localPosition = m_cardSpacing * i;
			actor.Show();
			CollectionCardVisual.ShowActorShadow(actor, show: true);
		}
		if (m_replaceCardActor != null)
		{
			m_replaceCardActor.Show();
		}
		if (m_tileToRemove != null)
		{
			m_tileToRemove.SetHighlight(highlight: true);
		}
		StartCoroutine(WaitAndAnimateChoices());
	}

	private IEnumerator WaitAndAnimateChoices()
	{
		yield return new WaitForEndOfFrame();
		for (int i = 0; i < m_choiceActors.Count; i++)
		{
			if (m_choiceActors[i].isActiveAndEnabled)
			{
				m_choiceActors[i].ActivateSpellBirthState(SpellType.SUMMON_IN_FORGE);
			}
		}
		if (m_replaceCardActor != null && m_replaceContainer.activeInHierarchy)
		{
			m_replaceCardActor.ActivateSpellBirthState(SpellType.SUMMON_IN_FORGE);
		}
	}

	public void Show(DeckTrayDeckTileVisual tileToRemove, bool continueAfterReplace, bool replacingCard, DelCompleteCallback onCompleteCallback)
	{
		if (!m_shown)
		{
			Navigation.PushUnique(OnNavigateBack);
			m_shown = true;
			m_rootObject.SetActive(value: true);
			if (!Options.Get().GetBool(Option.HAS_SEEN_DECK_HELPER, defaultVal: false) && UserAttentionManager.CanShowAttentionGrabber("DeckHelper.Show:" + Option.HAS_SEEN_DECK_HELPER))
			{
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_ANNOUNCER_CM_HELP_DECK_50"), "VO_ANNOUNCER_CM_HELP_DECK_50.prefab:450881875d33d094e9a27f6260fb06d9");
				Options.Get().SetBool(Option.HAS_SEEN_DECK_HELPER, val: true);
			}
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				FullScreenFXMgr.Get().StartStandardBlurVignette(0.1f);
			}
			m_tileToRemove = tileToRemove;
			if (m_tileToRemove != null)
			{
				m_cardToRemove = tileToRemove.GetActor().GetEntityDef();
			}
			m_continueAfterReplace = continueAfterReplace;
			m_onCompleteCallback = onCompleteCallback;
			UpdateChoices();
			NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_TEMPLATE_REPLACE_1"));
			NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_TEMPLATE_REPLACE_2"));
			NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_REPLACE_WILD_CARDS"));
			NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_REPLACE_WILD_CARDS_NPR"));
		}
	}

	private bool OnNavigateBack()
	{
		Hide(popnavigation: false);
		return true;
	}

	public void Hide(bool popnavigation = true)
	{
		if (m_shown)
		{
			if (popnavigation)
			{
				Navigation.RemoveHandler(OnNavigateBack);
			}
			m_shown = false;
			m_shouldHandleDeckChanged = true;
			CleanOldChoices();
			m_rootObject.SetActive(value: false);
			if (m_tileToRemove != null)
			{
				m_tileToRemove.SetHighlight(highlight: false);
			}
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				FullScreenFXMgr.Get().EndStandardBlurVignette(0.1f);
			}
			if (m_onCompleteCallback != null)
			{
				m_onCompleteCallback(m_chosenCards);
			}
		}
	}

	private void ShowInnkeeperPopup()
	{
		if (!(m_innkeeperPopup == null))
		{
			m_innkeeperPopup.gameObject.SetActive(value: true);
			m_innkeeperPopupShown = true;
			m_innkeeperPopup.gameObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
			iTween.ScaleTo(m_innkeeperPopup.gameObject, iTween.Hash("scale", m_innkeeperFullScale, "easetype", iTween.EaseType.easeOutElastic, "time", 1f));
			StopCoroutine("WaitThenHidePopup");
			StartCoroutine("WaitThenHidePopup");
		}
	}

	private IEnumerator WaitThenHidePopup()
	{
		yield return new WaitForSeconds(7f);
		HideInnkeeperPopup();
	}

	private void InnkeeperPopupClicked(UIEvent e)
	{
		HideInnkeeperPopup();
	}

	private void HideInnkeeperPopup()
	{
		if (!(m_innkeeperPopup == null) && m_innkeeperPopupShown)
		{
			m_innkeeperPopupShown = false;
			iTween.ScaleTo(m_innkeeperPopup.gameObject, iTween.Hash("scale", new Vector3(0.01f, 0.01f, 0.01f), "easetype", iTween.EaseType.easeInExpo, "time", 0.2f, "oncomplete", "FinishHidePopup", "oncompletetarget", base.gameObject));
		}
	}

	private void FinishHidePopup()
	{
		m_innkeeperPopup.gameObject.SetActive(value: false);
	}

	public void OnVisualRelease(Actor addCardActor, EntityDef cardToRemove)
	{
		m_shouldHandleDeckChanged = false;
		TooltipPanelManager.Get().HideKeywordHelp();
		addCardActor.GetSpell(SpellType.DEATHREVERSE).ActivateState(SpellStateType.BIRTH);
		bool num = cardToRemove != null;
		bool continueAfterReplace = m_continueAfterReplace;
		int num2 = 0;
		CollectionDeckTray collectionDeckTray = CollectionDeckTray.Get();
		CollectionDeck editingDeck = collectionDeckTray.GetCardsContent().GetEditingDeck();
		EntityDef entityDef = addCardActor.GetEntityDef();
		if (num)
		{
			string cardId = entityDef.GetCardId();
			CollectionManager.Get().GetOwnedCardCount(cardId, out var normal, out var golden, out var diamond);
			int cardCountAllMatchingSlots = editingDeck.GetCardCountAllMatchingSlots(cardId, TAG_PREMIUM.NORMAL);
			int cardCountAllMatchingSlots2 = editingDeck.GetCardCountAllMatchingSlots(cardId, TAG_PREMIUM.GOLDEN);
			int cardCountAllMatchingSlots3 = editingDeck.GetCardCountAllMatchingSlots(cardId, TAG_PREMIUM.DIAMOND);
			int num3 = Mathf.Max(0, normal - cardCountAllMatchingSlots);
			int num4 = Mathf.Max(0, golden - cardCountAllMatchingSlots2);
			int num5 = Mathf.Max(0, diamond - cardCountAllMatchingSlots3);
			int num6 = (cardToRemove.IsElite() ? 1 : 2);
			int a = (m_ReplaceSingleTemplateCard ? 1 : num6);
			int invalidCardIdCount = editingDeck.GetInvalidCardIdCount(cardToRemove.GetCardId());
			Log.DeckHelper.Print("checking invalid card " + editingDeck.FormatType.ToString() + " " + invalidCardIdCount + " " + cardToRemove);
			int sameRemoveCount = Mathf.Min(a, num3 + num4);
			int num7 = collectionDeckTray.RemoveClosestInvalidCard(cardToRemove, editingDeck, sameRemoveCount);
			Log.DeckHelper.Print("removed cards " + num7);
			for (int i = 0; i < num7; i++)
			{
				TAG_PREMIUM tAG_PREMIUM = TAG_PREMIUM.NORMAL;
				if (num5 > 0)
				{
					num5--;
					tAG_PREMIUM = TAG_PREMIUM.DIAMOND;
				}
				else if (num4 > 0)
				{
					num4--;
					tAG_PREMIUM = TAG_PREMIUM.GOLDEN;
				}
				else if (num3 == 0)
				{
					break;
				}
				if (collectionDeckTray.AddCard(entityDef, tAG_PREMIUM, null, playSound: false, addCardActor))
				{
					m_chosenCards.Add(entityDef);
					num2++;
				}
				else
				{
					Log.DeckHelper.PrintError("Could not AddCard card={0} premium={1}", entityDef, tAG_PREMIUM);
				}
			}
			Log.DeckHelper.Print("did replace " + num2 + " " + invalidCardIdCount);
			if (num2 < invalidCardIdCount)
			{
				m_cardToRemove = cardToRemove;
				continueAfterReplace = true;
			}
		}
		else if (collectionDeckTray.AddCard(entityDef, addCardActor.GetPremium(), null, playSound: false, addCardActor))
		{
			m_chosenCards.Add(entityDef);
			num2++;
		}
		m_shouldHandleDeckChanged = true;
		HandleDeckChanged(editingDeck, continueAfterReplace);
	}

	private void OnGhostCardRelease(Actor addCardActor, EntityDef cardToRemove)
	{
		GhostCard ghostCard = addCardActor.m_ghostCardGameObject.GetComponent<GhostCard>();
		MeshRenderer[] componentsInChildren = ghostCard.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = false;
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			SceneUtils.SetLayer(base.gameObject, GameLayer.Default);
		}
		CraftingManager.Get().EnterCraftMode(addCardActor, delegate
		{
			if (!(addCardActor == null))
			{
				if ((bool)UniversalInputManager.UsePhoneUI)
				{
					StartCoroutine(WaitThenSetLayer(GameLayer.IgnoreFullScreenEffects));
				}
				MeshRenderer[] componentsInChildren2 = addCardActor.m_ghostCardGameObject.GetComponentsInChildren<MeshRenderer>();
				foreach (MeshRenderer obj in componentsInChildren2)
				{
					obj.enabled = obj.gameObject != ghostCard.m_GlowPlane;
				}
			}
		});
	}

	private IEnumerator WaitThenSetLayer(GameLayer layer)
	{
		yield return new WaitForSeconds(0.25f);
		SceneUtils.SetLayer(base.gameObject, layer);
	}

	private void OnVisualOver(Actor actor)
	{
		SoundManager.Get().LoadAndPlay("collection_manager_card_mouse_over.prefab:0d4e20bc78956bc48b5e2963ec39211c");
		actor.SetActorState(ActorStateType.CARD_MOUSE_OVER);
		TooltipPanelManager.Get().UpdateKeywordHelpForDeckHelper(actor.GetEntityDef(), actor);
	}

	private void OnVisualOut(Actor actor)
	{
		actor.SetActorState(ActorStateType.CARD_IDLE);
		TooltipPanelManager.Get().HideKeywordHelp();
	}

	private void HandleDeckChanged(CollectionDeck deck, bool continueAfterReplace)
	{
		if (deck.GetTotalValidCardCount() >= CollectionManager.Get().GetDeckSize())
		{
			Hide();
		}
		else if (!continueAfterReplace)
		{
			Hide();
		}
		else
		{
			UpdateChoices();
		}
	}
}
