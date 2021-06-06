using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000125 RID: 293
public class DeckHelper : MonoBehaviour
{
	// Token: 0x0600134F RID: 4943 RVA: 0x0006EB68 File Offset: 0x0006CD68
	private void Awake()
	{
		DeckHelper.s_instance = this;
		this.m_rootObject.SetActive(false);
		this.m_replaceDoneButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.EndButtonClick));
		this.m_suggestDoneButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.EndButtonClick));
		if (UniversalInputManager.UsePhoneUI)
		{
			if (this.m_innkeeperPopup != null)
			{
				this.m_innkeeperFullScale = this.m_innkeeperPopup.gameObject.transform.localScale;
				this.m_innkeeperPopup.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.InnkeeperPopupClicked));
				return;
			}
		}
		else
		{
			this.m_inputBlocker.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.EndButtonClick));
		}
	}

	// Token: 0x06001350 RID: 4944 RVA: 0x0006EC21 File Offset: 0x0006CE21
	private void OnDestroy()
	{
		DeckHelper.s_instance = null;
	}

	// Token: 0x06001351 RID: 4945 RVA: 0x00004EB5 File Offset: 0x000030B5
	private void EndButtonClick(UIEvent e)
	{
		Navigation.GoBack();
	}

	// Token: 0x06001352 RID: 4946 RVA: 0x0006EC2C File Offset: 0x0006CE2C
	public static DeckHelper Get()
	{
		if (DeckHelper.s_instance == null)
		{
			string input = UniversalInputManager.UsePhoneUI ? "DeckHelper_phone.prefab:e2c93e38a85f44eadb1aee945b1c4636" : "DeckHelper.prefab:69e71904d55994cc28b41f5950e6608f";
			DeckHelper.s_instance = AssetLoader.Get().InstantiatePrefab(input, AssetLoadingOptions.None).GetComponent<DeckHelper>();
		}
		return DeckHelper.s_instance;
	}

	// Token: 0x06001353 RID: 4947 RVA: 0x0006EC7F File Offset: 0x0006CE7F
	public bool IsActive()
	{
		return this.m_shown;
	}

	// Token: 0x06001354 RID: 4948 RVA: 0x0006EC87 File Offset: 0x0006CE87
	public void OnCardAdded(CollectionDeck deck)
	{
		if (!this.IsActive())
		{
			return;
		}
		if (this.m_shouldHandleDeckChanged)
		{
			this.HandleDeckChanged(deck, this.m_continueAfterReplace);
		}
	}

	// Token: 0x06001355 RID: 4949 RVA: 0x0006ECA7 File Offset: 0x0006CEA7
	public static bool HasChoicesToOffer(CollectionDeck deck)
	{
		return DeckMaker.GetFillCardChoices(deck, null, 1, null).m_addChoices.Count > 0;
	}

	// Token: 0x06001356 RID: 4950 RVA: 0x0006ECC0 File Offset: 0x0006CEC0
	public void UpdateChoices()
	{
		this.CleanOldChoices();
		if (!this.IsActive())
		{
			return;
		}
		EntityDef entityDef = this.m_cardToRemove;
		this.m_cardToRemove = null;
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		DeckMaker.DeckChoiceFill cardsToShow = DeckMaker.GetFillCardChoices(editedDeck, entityDef, 3, null);
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
		if (this.m_instructionText != null)
		{
			bool flag = !this.m_instructionText.Text.Equals(reason);
			this.m_instructionText.Text = reason;
			if (UniversalInputManager.UsePhoneUI && flag)
			{
				if (NotificationManager.Get().IsQuotePlaying)
				{
					this.m_instructionText.Text = "";
				}
				else
				{
					this.ShowInnkeeperPopup();
				}
			}
		}
		this.m_replaceACardPane.SetActive(entityDef != null);
		this.m_suggestACardPane.SetActive(entityDef == null);
		if (entityDef != null)
		{
			if (this.m_tileToRemove != null)
			{
				this.m_tileToRemove.SetHighlight(false);
			}
			this.m_tileToRemove = CollectionDeckTray.Get().GetCardTileVisual(entityDef.GetCardId());
			if (this.m_tileToRemove == null)
			{
				this.Hide(true);
				return;
			}
			GhostCard.Type ghostTypeFromSlot = GhostCard.GetGhostTypeFromSlot(editedDeck, this.m_tileToRemove.GetSlot());
			this.m_replaceCardActor = this.LoadBestCardActor(entityDef, TAG_PREMIUM.NORMAL, ghostTypeFromSlot);
			if (this.m_replaceCardActor != null)
			{
				GameUtils.SetParent(this.m_replaceCardActor, this.m_replaceContainer, false);
				RenderToTexture component = this.m_replaceCardActor.m_ghostCardGameObject.GetComponent<RenderToTexture>();
				BoxCollider boxCollider = this.m_replaceCardActor.m_ghostCardGameObject.AddComponent<BoxCollider>();
				boxCollider.size = new Vector3(component.m_Width, 2f, component.m_Height);
				boxCollider.gameObject.AddComponent<PegUIElement>().AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
				{
					this.OnGhostCardRelease(this.m_replaceCardActor, cardsToShow.m_removeTemplate);
				});
			}
			if (this.m_replaceText != null)
			{
				if (ghostTypeFromSlot == GhostCard.Type.NOT_VALID)
				{
					if (!GameUtils.IsCardGameplayEventActive(entityDef))
					{
						this.m_replaceText.Text = GameStrings.Get("GLUE_COLLECTION_DECK_HELPER_REPLACE_UNPLAYABLE_CARD");
					}
					else if (CollectionManager.Get().ShouldAccountSeeStandardWild())
					{
						this.m_replaceText.Text = GameStrings.Get("GLUE_COLLECTION_DECK_HELPER_REPLACE_INVALID_CARD");
					}
					else
					{
						this.m_replaceText.Text = GameStrings.Get("GLUE_COLLECTION_DECK_HELPER_REPLACE_INVALID_CARD_NPR_NEW");
					}
				}
				else
				{
					this.m_replaceText.Text = GameStrings.Get("GLUE_COLLECTION_DECK_HELPER_REPLACE_CARD");
				}
			}
			if (this.m_tileToRemove.GetSlot().Owned && !Options.Get().GetBool(Option.HAS_SEEN_DECK_TEMPLATE_GHOST_CARD, false))
			{
				Options.Get().SetBool(Option.HAS_SEEN_DECK_TEMPLATE_GHOST_CARD, true);
			}
			if (!editedDeck.IsValidSlot(this.m_tileToRemove.GetSlot(), false, false, false) && !Options.Get().GetBool(Option.HAS_SEEN_INVALID_ROTATED_CARD, false))
			{
				Options.Get().SetBool(Option.HAS_SEEN_INVALID_ROTATED_CARD, true);
			}
			if (this.m_replaceACardSound != string.Empty)
			{
				SoundManager.Get().LoadAndPlay(this.m_replaceACardSound);
			}
		}
		bool flag2 = entityDef != null;
		int num = Mathf.Min(flag2 ? 2 : 3, cardsToShow.m_addChoices.Count);
		GameObject parent = flag2 ? this.m_2choiceContainer : this.m_3choiceContainer;
		for (int i = 0; i < num; i++)
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
			Actor actor = this.LoadBestCardActor(entityDef2, premiumToUse, GhostCard.Type.NONE);
			if (!(actor == null))
			{
				GameUtils.SetParent(actor, parent, false);
				PegUIElement pegUIElement = actor.GetCollider().gameObject.AddComponent<PegUIElement>();
				pegUIElement.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
				{
					this.OnVisualRelease(actor, cardsToShow.m_removeTemplate);
				});
				pegUIElement.AddEventListener(UIEventType.ROLLOVER, delegate(UIEvent e)
				{
					this.OnVisualOver(actor);
				});
				pegUIElement.AddEventListener(UIEventType.ROLLOUT, delegate(UIEvent e)
				{
					this.OnVisualOut(actor);
				});
				this.m_choiceActors.Add(actor);
			}
		}
		this.PositionAndShowChoices();
	}

	// Token: 0x06001357 RID: 4951 RVA: 0x0006F11C File Offset: 0x0006D31C
	private Actor LoadBestCardActor(EntityDef entityDef, TAG_PREMIUM premiumToUse, GhostCard.Type ghostCard = GhostCard.Type.NONE)
	{
		Actor result;
		using (DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(entityDef.GetCardId(), new CardPortraitQuality(3, premiumToUse)))
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(entityDef, premiumToUse), AssetLoadingOptions.IgnorePrefabPosition);
			if (gameObject == null)
			{
				Debug.LogWarning(string.Format("DeckHelper - FAILED to load actor \"{0}\"", base.name));
				result = null;
			}
			else
			{
				Actor component = gameObject.GetComponent<Actor>();
				if (component == null)
				{
					Debug.LogWarning(string.Format("DeckHelper - ERROR actor \"{0}\" has no Actor component", base.name));
					result = null;
				}
				else
				{
					component.transform.parent = base.transform;
					SceneUtils.SetLayer(component, base.gameObject.layer);
					component.SetEntityDef(entityDef);
					component.SetCardDef(cardDef);
					component.SetPremium(premiumToUse);
					component.GhostCardEffect(ghostCard, premiumToUse, true);
					component.UpdateAllComponents();
					component.Hide();
					component.gameObject.name = cardDef.CardDef.name + "_actor";
					result = component;
				}
			}
		}
		return result;
	}

	// Token: 0x06001358 RID: 4952 RVA: 0x0006F234 File Offset: 0x0006D434
	private void CleanOldChoices()
	{
		foreach (Actor actor in this.m_choiceActors)
		{
			UnityEngine.Object.Destroy(actor.gameObject);
		}
		this.m_choiceActors.Clear();
		if (this.m_replaceCardActor != null)
		{
			UnityEngine.Object.Destroy(this.m_replaceCardActor.gameObject);
			this.m_replaceCardActor = null;
		}
	}

	// Token: 0x06001359 RID: 4953 RVA: 0x0006F2BC File Offset: 0x0006D4BC
	private void PositionAndShowChoices()
	{
		for (int i = 0; i < this.m_choiceActors.Count; i++)
		{
			Actor actor = this.m_choiceActors[i];
			actor.transform.localPosition = this.m_cardSpacing * (float)i;
			actor.Show();
			CollectionCardVisual.ShowActorShadow(actor, true);
		}
		if (this.m_replaceCardActor != null)
		{
			this.m_replaceCardActor.Show();
		}
		if (this.m_tileToRemove != null)
		{
			this.m_tileToRemove.SetHighlight(true);
		}
		base.StartCoroutine(this.WaitAndAnimateChoices());
	}

	// Token: 0x0600135A RID: 4954 RVA: 0x0006F34F File Offset: 0x0006D54F
	private IEnumerator WaitAndAnimateChoices()
	{
		yield return new WaitForEndOfFrame();
		for (int i = 0; i < this.m_choiceActors.Count; i++)
		{
			if (this.m_choiceActors[i].isActiveAndEnabled)
			{
				this.m_choiceActors[i].ActivateSpellBirthState(SpellType.SUMMON_IN_FORGE);
			}
		}
		if (this.m_replaceCardActor != null && this.m_replaceContainer.activeInHierarchy)
		{
			this.m_replaceCardActor.ActivateSpellBirthState(SpellType.SUMMON_IN_FORGE);
		}
		yield break;
	}

	// Token: 0x0600135B RID: 4955 RVA: 0x0006F360 File Offset: 0x0006D560
	public void Show(DeckTrayDeckTileVisual tileToRemove, bool continueAfterReplace, bool replacingCard, DeckHelper.DelCompleteCallback onCompleteCallback)
	{
		if (this.m_shown)
		{
			return;
		}
		Navigation.PushUnique(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		this.m_shown = true;
		this.m_rootObject.SetActive(true);
		if (!Options.Get().GetBool(Option.HAS_SEEN_DECK_HELPER, false) && UserAttentionManager.CanShowAttentionGrabber("DeckHelper.Show:" + Option.HAS_SEEN_DECK_HELPER))
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_ANNOUNCER_CM_HELP_DECK_50"), "VO_ANNOUNCER_CM_HELP_DECK_50.prefab:450881875d33d094e9a27f6260fb06d9", 0f, null, false);
			Options.Get().SetBool(Option.HAS_SEEN_DECK_HELPER, true);
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			FullScreenFXMgr.Get().StartStandardBlurVignette(0.1f);
		}
		this.m_tileToRemove = tileToRemove;
		if (this.m_tileToRemove != null)
		{
			this.m_cardToRemove = tileToRemove.GetActor().GetEntityDef();
		}
		this.m_continueAfterReplace = continueAfterReplace;
		this.m_onCompleteCallback = onCompleteCallback;
		this.UpdateChoices();
		NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_TEMPLATE_REPLACE_1"), 0f);
		NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_TEMPLATE_REPLACE_2"), 0f);
		NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_REPLACE_WILD_CARDS"), 0f);
		NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_REPLACE_WILD_CARDS_NPR"), 0f);
	}

	// Token: 0x0600135C RID: 4956 RVA: 0x0006F4B3 File Offset: 0x0006D6B3
	private bool OnNavigateBack()
	{
		this.Hide(false);
		return true;
	}

	// Token: 0x0600135D RID: 4957 RVA: 0x0006F4C0 File Offset: 0x0006D6C0
	public void Hide(bool popnavigation = true)
	{
		if (!this.m_shown)
		{
			return;
		}
		if (popnavigation)
		{
			Navigation.RemoveHandler(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		}
		this.m_shown = false;
		this.m_shouldHandleDeckChanged = true;
		this.CleanOldChoices();
		this.m_rootObject.SetActive(false);
		if (this.m_tileToRemove != null)
		{
			this.m_tileToRemove.SetHighlight(false);
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			FullScreenFXMgr.Get().EndStandardBlurVignette(0.1f, null);
		}
		if (this.m_onCompleteCallback != null)
		{
			this.m_onCompleteCallback(this.m_chosenCards);
		}
	}

	// Token: 0x0600135E RID: 4958 RVA: 0x0006F55C File Offset: 0x0006D75C
	private void ShowInnkeeperPopup()
	{
		if (this.m_innkeeperPopup == null)
		{
			return;
		}
		this.m_innkeeperPopup.gameObject.SetActive(true);
		this.m_innkeeperPopupShown = true;
		this.m_innkeeperPopup.gameObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		iTween.ScaleTo(this.m_innkeeperPopup.gameObject, iTween.Hash(new object[]
		{
			"scale",
			this.m_innkeeperFullScale,
			"easetype",
			iTween.EaseType.easeOutElastic,
			"time",
			1f
		}));
		base.StopCoroutine("WaitThenHidePopup");
		base.StartCoroutine("WaitThenHidePopup");
	}

	// Token: 0x0600135F RID: 4959 RVA: 0x0006F628 File Offset: 0x0006D828
	private IEnumerator WaitThenHidePopup()
	{
		yield return new WaitForSeconds(7f);
		this.HideInnkeeperPopup();
		yield break;
	}

	// Token: 0x06001360 RID: 4960 RVA: 0x0006F637 File Offset: 0x0006D837
	private void InnkeeperPopupClicked(UIEvent e)
	{
		this.HideInnkeeperPopup();
	}

	// Token: 0x06001361 RID: 4961 RVA: 0x0006F640 File Offset: 0x0006D840
	private void HideInnkeeperPopup()
	{
		if (this.m_innkeeperPopup == null || !this.m_innkeeperPopupShown)
		{
			return;
		}
		this.m_innkeeperPopupShown = false;
		iTween.ScaleTo(this.m_innkeeperPopup.gameObject, iTween.Hash(new object[]
		{
			"scale",
			new Vector3(0.01f, 0.01f, 0.01f),
			"easetype",
			iTween.EaseType.easeInExpo,
			"time",
			0.2f,
			"oncomplete",
			"FinishHidePopup",
			"oncompletetarget",
			base.gameObject
		}));
	}

	// Token: 0x06001362 RID: 4962 RVA: 0x0006F6F4 File Offset: 0x0006D8F4
	private void FinishHidePopup()
	{
		this.m_innkeeperPopup.gameObject.SetActive(false);
	}

	// Token: 0x06001363 RID: 4963 RVA: 0x0006F708 File Offset: 0x0006D908
	public void OnVisualRelease(Actor addCardActor, EntityDef cardToRemove)
	{
		this.m_shouldHandleDeckChanged = false;
		TooltipPanelManager.Get().HideKeywordHelp();
		addCardActor.GetSpell(SpellType.DEATHREVERSE).ActivateState(SpellStateType.BIRTH);
		bool flag = cardToRemove != null;
		bool continueAfterReplace = this.m_continueAfterReplace;
		int num = 0;
		CollectionDeckTray collectionDeckTray = CollectionDeckTray.Get();
		CollectionDeck editingDeck = collectionDeckTray.GetCardsContent().GetEditingDeck();
		EntityDef entityDef = addCardActor.GetEntityDef();
		if (flag)
		{
			string cardId = entityDef.GetCardId();
			int num2;
			int num3;
			int num4;
			CollectionManager.Get().GetOwnedCardCount(cardId, out num2, out num3, out num4);
			int cardCountAllMatchingSlots = editingDeck.GetCardCountAllMatchingSlots(cardId, TAG_PREMIUM.NORMAL);
			int cardCountAllMatchingSlots2 = editingDeck.GetCardCountAllMatchingSlots(cardId, TAG_PREMIUM.GOLDEN);
			int cardCountAllMatchingSlots3 = editingDeck.GetCardCountAllMatchingSlots(cardId, TAG_PREMIUM.DIAMOND);
			int num5 = Mathf.Max(0, num2 - cardCountAllMatchingSlots);
			int num6 = Mathf.Max(0, num3 - cardCountAllMatchingSlots2);
			int num7 = Mathf.Max(0, num4 - cardCountAllMatchingSlots3);
			int num8 = cardToRemove.IsElite() ? 1 : 2;
			int a = this.m_ReplaceSingleTemplateCard ? 1 : num8;
			int invalidCardIdCount = editingDeck.GetInvalidCardIdCount(cardToRemove.GetCardId());
			Log.DeckHelper.Print(string.Concat(new object[]
			{
				"checking invalid card ",
				editingDeck.FormatType.ToString(),
				" ",
				invalidCardIdCount,
				" ",
				cardToRemove
			}), Array.Empty<object>());
			int sameRemoveCount = Mathf.Min(a, num5 + num6);
			int num9 = collectionDeckTray.RemoveClosestInvalidCard(cardToRemove, editingDeck, sameRemoveCount);
			Log.DeckHelper.Print("removed cards " + num9, Array.Empty<object>());
			for (int i = 0; i < num9; i++)
			{
				TAG_PREMIUM tag_PREMIUM = TAG_PREMIUM.NORMAL;
				if (num7 > 0)
				{
					num7--;
					tag_PREMIUM = TAG_PREMIUM.DIAMOND;
				}
				else if (num6 > 0)
				{
					num6--;
					tag_PREMIUM = TAG_PREMIUM.GOLDEN;
				}
				else if (num5 == 0)
				{
					break;
				}
				if (collectionDeckTray.AddCard(entityDef, tag_PREMIUM, null, false, addCardActor, false))
				{
					this.m_chosenCards.Add(entityDef);
					num++;
				}
				else
				{
					Log.DeckHelper.PrintError("Could not AddCard card={0} premium={1}", new object[]
					{
						entityDef,
						tag_PREMIUM
					});
				}
			}
			Log.DeckHelper.Print(string.Concat(new object[]
			{
				"did replace ",
				num,
				" ",
				invalidCardIdCount
			}), Array.Empty<object>());
			if (num < invalidCardIdCount)
			{
				this.m_cardToRemove = cardToRemove;
				continueAfterReplace = true;
			}
		}
		else if (collectionDeckTray.AddCard(entityDef, addCardActor.GetPremium(), null, false, addCardActor, false))
		{
			this.m_chosenCards.Add(entityDef);
			num++;
		}
		this.m_shouldHandleDeckChanged = true;
		this.HandleDeckChanged(editingDeck, continueAfterReplace);
	}

	// Token: 0x06001364 RID: 4964 RVA: 0x0006F980 File Offset: 0x0006DB80
	private void OnGhostCardRelease(Actor addCardActor, EntityDef cardToRemove)
	{
		GhostCard ghostCard = addCardActor.m_ghostCardGameObject.GetComponent<GhostCard>();
		MeshRenderer[] componentsInChildren = ghostCard.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = false;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			SceneUtils.SetLayer(base.gameObject, GameLayer.Default);
		}
		CraftingManager.Get().EnterCraftMode(addCardActor, delegate
		{
			if (addCardActor == null)
			{
				return;
			}
			if (UniversalInputManager.UsePhoneUI)
			{
				this.StartCoroutine(this.WaitThenSetLayer(GameLayer.IgnoreFullScreenEffects));
			}
			foreach (MeshRenderer meshRenderer in addCardActor.m_ghostCardGameObject.GetComponentsInChildren<MeshRenderer>())
			{
				meshRenderer.enabled = (meshRenderer.gameObject != ghostCard.m_GlowPlane);
			}
		});
	}

	// Token: 0x06001365 RID: 4965 RVA: 0x0006FA0E File Offset: 0x0006DC0E
	private IEnumerator WaitThenSetLayer(GameLayer layer)
	{
		yield return new WaitForSeconds(0.25f);
		SceneUtils.SetLayer(base.gameObject, layer);
		yield break;
	}

	// Token: 0x06001366 RID: 4966 RVA: 0x0006FA24 File Offset: 0x0006DC24
	private void OnVisualOver(Actor actor)
	{
		SoundManager.Get().LoadAndPlay("collection_manager_card_mouse_over.prefab:0d4e20bc78956bc48b5e2963ec39211c");
		actor.SetActorState(ActorStateType.CARD_MOUSE_OVER);
		TooltipPanelManager.Get().UpdateKeywordHelpForDeckHelper(actor.GetEntityDef(), actor);
	}

	// Token: 0x06001367 RID: 4967 RVA: 0x0006FA52 File Offset: 0x0006DC52
	private void OnVisualOut(Actor actor)
	{
		actor.SetActorState(ActorStateType.CARD_IDLE);
		TooltipPanelManager.Get().HideKeywordHelp();
	}

	// Token: 0x06001368 RID: 4968 RVA: 0x0006FA65 File Offset: 0x0006DC65
	private void HandleDeckChanged(CollectionDeck deck, bool continueAfterReplace)
	{
		if (deck.GetTotalValidCardCount() >= CollectionManager.Get().GetDeckSize())
		{
			this.Hide(true);
			return;
		}
		if (!continueAfterReplace)
		{
			this.Hide(true);
			return;
		}
		this.UpdateChoices();
	}

	// Token: 0x04000C90 RID: 3216
	public UberText m_instructionText;

	// Token: 0x04000C91 RID: 3217
	public UberText m_replaceText;

	// Token: 0x04000C92 RID: 3218
	public GameObject m_rootObject;

	// Token: 0x04000C93 RID: 3219
	public UIBButton m_suggestDoneButton;

	// Token: 0x04000C94 RID: 3220
	public UIBButton m_replaceDoneButton;

	// Token: 0x04000C95 RID: 3221
	public PegUIElement m_inputBlocker;

	// Token: 0x04000C96 RID: 3222
	public Vector3 m_deckCardLocalScale = new Vector3(5.75f, 5.75f, 5.75f);

	// Token: 0x04000C97 RID: 3223
	public GameObject m_3choiceContainer;

	// Token: 0x04000C98 RID: 3224
	public GameObject m_replaceContainer;

	// Token: 0x04000C99 RID: 3225
	public GameObject m_2choiceContainer;

	// Token: 0x04000C9A RID: 3226
	public Vector3 m_cardSpacing;

	// Token: 0x04000C9B RID: 3227
	public GameObject m_suggestACardPane;

	// Token: 0x04000C9C RID: 3228
	public GameObject m_replaceACardPane;

	// Token: 0x04000C9D RID: 3229
	public string m_replaceACardSound;

	// Token: 0x04000C9E RID: 3230
	public UIBButton m_innkeeperPopup;

	// Token: 0x04000C9F RID: 3231
	private static DeckHelper s_instance;

	// Token: 0x04000CA0 RID: 3232
	private Actor m_replaceCardActor;

	// Token: 0x04000CA1 RID: 3233
	private List<Actor> m_choiceActors = new List<Actor>();

	// Token: 0x04000CA2 RID: 3234
	private bool m_shown;

	// Token: 0x04000CA3 RID: 3235
	private DeckTrayDeckTileVisual m_tileToRemove;

	// Token: 0x04000CA4 RID: 3236
	private EntityDef m_cardToRemove;

	// Token: 0x04000CA5 RID: 3237
	private bool m_continueAfterReplace;

	// Token: 0x04000CA6 RID: 3238
	private bool m_shouldHandleDeckChanged = true;

	// Token: 0x04000CA7 RID: 3239
	private bool m_ReplaceSingleTemplateCard = true;

	// Token: 0x04000CA8 RID: 3240
	private Vector3 m_innkeeperFullScale;

	// Token: 0x04000CA9 RID: 3241
	private bool m_innkeeperPopupShown;

	// Token: 0x04000CAA RID: 3242
	private const float INNKEEPER_POPUP_DURATION = 7f;

	// Token: 0x04000CAB RID: 3243
	private List<EntityDef> m_chosenCards = new List<EntityDef>();

	// Token: 0x04000CAC RID: 3244
	private DeckHelper.DelCompleteCallback m_onCompleteCallback;

	// Token: 0x020014B1 RID: 5297
	// (Invoke) Token: 0x0600DBD8 RID: 56280
	public delegate void DelCompleteCallback(List<EntityDef> chosenCards);
}
