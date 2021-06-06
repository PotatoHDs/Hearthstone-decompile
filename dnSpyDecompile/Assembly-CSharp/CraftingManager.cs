using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200011C RID: 284
[CustomEditClass]
public class CraftingManager : MonoBehaviour
{
	// Token: 0x0600129C RID: 4764 RVA: 0x00069E76 File Offset: 0x00068076
	private void Awake()
	{
		CollectionManager.Get().RegisterMassDisenchantListener(new CollectionManager.OnMassDisenchant(this.OnMassDisenchant));
	}

	// Token: 0x0600129D RID: 4765 RVA: 0x00069E8E File Offset: 0x0006808E
	private void OnDestroy()
	{
		if (CollectionManager.Get() != null)
		{
			CollectionManager.Get().RemoveMassDisenchantListener(new CollectionManager.OnMassDisenchant(this.OnMassDisenchant));
		}
		CraftingManager.s_instance = null;
	}

	// Token: 0x0600129E RID: 4766 RVA: 0x00069EB3 File Offset: 0x000680B3
	private void Start()
	{
		this.LoadElements();
	}

	// Token: 0x0600129F RID: 4767 RVA: 0x00069EBC File Offset: 0x000680BC
	private void LoadElements()
	{
		if (this.m_elementsLoaded)
		{
			return;
		}
		this.LoadActor("Card_Hand_Weapon.prefab:30888a1fdca5c6c43abcc5d9dca55783", ref this.m_ghostWeaponActor, ref this.m_templateWeaponActor);
		this.LoadActor(ActorNames.GetHandActor(TAG_CARDTYPE.WEAPON, TAG_PREMIUM.GOLDEN, false), ref this.m_ghostGoldenWeaponActor, ref this.m_templateGoldenWeaponActor);
		this.LoadActor("Card_Hand_Ally.prefab:d00eb0f79080e0749993fe4619e9143d", ref this.m_ghostMinionActor, ref this.m_templateMinionActor);
		this.LoadActor(ActorNames.GetHandActor(TAG_CARDTYPE.MINION, TAG_PREMIUM.GOLDEN, false), ref this.m_ghostGoldenMinionActor, ref this.m_templateGoldenMinionActor);
		this.LoadActor(ActorNames.GetHandActor(TAG_CARDTYPE.MINION, TAG_PREMIUM.DIAMOND, false), ref this.m_ghostDiamondMinionActor, ref this.m_templateDiamondMinionActor);
		this.LoadActor("Card_Hand_Ability.prefab:3c3f5189f0d0b3745a1c1ca21d41efe0", ref this.m_ghostSpellActor, ref this.m_templateSpellActor);
		this.LoadActor(ActorNames.GetHandActor(TAG_CARDTYPE.SPELL, TAG_PREMIUM.GOLDEN, false), ref this.m_ghostGoldenSpellActor, ref this.m_templateGoldenSpellActor);
		this.LoadActor("Card_Hand_Hero.prefab:a977c49edb5fb5d4c8dee4d2344d1395", ref this.m_ghostHeroActor, ref this.m_templateHeroActor);
		this.LoadActor(ActorNames.GetHandActor(TAG_CARDTYPE.HERO, TAG_PREMIUM.GOLDEN, false), ref this.m_ghostGoldenHeroActor, ref this.m_templateGoldenHeroActor);
		this.LoadActor("History_HeroPower.prefab:e73edf8ccea2b11429093f7a448eef53", ref this.m_ghostHeroPowerActor, ref this.m_templateHeroPowerActor);
		this.LoadActor(ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.HISTORY_HERO_POWER, TAG_PREMIUM.GOLDEN), ref this.m_ghostGoldenHeroPowerActor, ref this.m_templateGoldenHeroPowerActor);
		this.LoadActor("Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", ref this.m_hiddenActor);
		this.m_hiddenActor.GetMeshRenderer(false).transform.localEulerAngles = new Vector3(0f, 180f, 180f);
		SceneUtils.SetLayer(this.m_hiddenActor.gameObject, GameLayer.IgnoreFullScreenEffects);
		SoundManager.Get().Load("Card_Transition_Out.prefab:aecf5b5837772844b9d2db995744df82");
		SoundManager.Get().Load("Card_Transition_In.prefab:3f3fbe896b8b260448e8c7e5d028d971");
		this.LoadRandomCardBack();
		this.m_elementsLoaded = true;
	}

	// Token: 0x060012A0 RID: 4768 RVA: 0x0006A068 File Offset: 0x00068268
	private void SwitchPremiumView(UIEvent e)
	{
		TAG_PREMIUM premium = TAG_PREMIUM.GOLDEN;
		switch (this.m_currentBigActor.GetPremium())
		{
		case TAG_PREMIUM.NORMAL:
			premium = TAG_PREMIUM.GOLDEN;
			break;
		case TAG_PREMIUM.GOLDEN:
			premium = TAG_PREMIUM.DIAMOND;
			break;
		case TAG_PREMIUM.DIAMOND:
			premium = TAG_PREMIUM.NORMAL;
			break;
		}
		this.TellServerAboutWhatUserDid();
		this.SetupActor(this.m_collectionCardActor, premium);
	}

	// Token: 0x170000DB RID: 219
	// (get) Token: 0x060012A1 RID: 4769 RVA: 0x0006A0B4 File Offset: 0x000682B4
	public static bool IsInitialized
	{
		get
		{
			return CraftingManager.s_instance != null;
		}
	}

	// Token: 0x060012A2 RID: 4770 RVA: 0x0006A0C4 File Offset: 0x000682C4
	public static CraftingManager Get()
	{
		if (CraftingManager.s_instance == null)
		{
			string input = UniversalInputManager.UsePhoneUI ? "CraftingManager_phone.prefab:d28ac29ae64f14e649186d0d1fe5f7e8" : "CraftingManager.prefab:9dc2dd187dd914959b311d326c3fd5b2";
			CraftingManager.s_instance = AssetLoader.Get().InstantiatePrefab(input, AssetLoadingOptions.None).GetComponent<CraftingManager>();
			CraftingManager.s_instance.LoadElements();
		}
		return CraftingManager.s_instance;
	}

	// Token: 0x060012A3 RID: 4771 RVA: 0x0006A124 File Offset: 0x00068324
	public NetCache.CardValue GetCardValue(string cardID, TAG_PREMIUM premium)
	{
		NetCache.NetCacheCardValues netObject = NetCache.Get().GetNetObject<NetCache.NetCacheCardValues>();
		NetCache.CardDefinition key = new NetCache.CardDefinition
		{
			Name = cardID,
			Premium = premium
		};
		NetCache.CardValue result;
		if (netObject == null || !netObject.Values.TryGetValue(key, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x060012A4 RID: 4772 RVA: 0x0006A166 File Offset: 0x00068366
	public bool IsCardShowing()
	{
		return this.m_currentBigActor != null;
	}

	// Token: 0x060012A5 RID: 4773 RVA: 0x0006A174 File Offset: 0x00068374
	public static bool GetIsInCraftingMode()
	{
		return CraftingManager.s_instance != null && CraftingManager.s_instance.IsInCraftingMode;
	}

	// Token: 0x170000DC RID: 220
	// (get) Token: 0x060012A6 RID: 4774 RVA: 0x0006A18F File Offset: 0x0006838F
	// (set) Token: 0x060012A7 RID: 4775 RVA: 0x0006A197 File Offset: 0x00068397
	private bool IsInCraftingMode { get; set; }

	// Token: 0x060012A8 RID: 4776 RVA: 0x0006A1A0 File Offset: 0x000683A0
	public bool GetShownCardInfo(out EntityDef entityDef, out TAG_PREMIUM premium)
	{
		entityDef = null;
		premium = TAG_PREMIUM.NORMAL;
		if (this.m_currentBigActor == null)
		{
			return false;
		}
		entityDef = this.m_currentBigActor.GetEntityDef();
		premium = this.m_currentBigActor.GetPremium();
		return entityDef != null;
	}

	// Token: 0x060012A9 RID: 4777 RVA: 0x0006A1D9 File Offset: 0x000683D9
	public Actor GetShownActor()
	{
		return this.m_currentBigActor;
	}

	// Token: 0x060012AA RID: 4778 RVA: 0x0006A1E1 File Offset: 0x000683E1
	public void OnMassDisenchant(int amount)
	{
		if (!MassDisenchant.Get())
		{
			this.m_craftingUI.UpdateBankText();
		}
	}

	// Token: 0x060012AB RID: 4779 RVA: 0x0006A1FA File Offset: 0x000683FA
	public long GetUnCommitedArcaneDustChanges()
	{
		return this.m_unCommitedArcaneDustAdjustments;
	}

	// Token: 0x060012AC RID: 4780 RVA: 0x0006A202 File Offset: 0x00068402
	public void AdjustUnCommitedArcaneDustChanges(int amount)
	{
		this.m_unCommitedArcaneDustAdjustments += (long)amount;
	}

	// Token: 0x060012AD RID: 4781 RVA: 0x0006A213 File Offset: 0x00068413
	public void ResetUnCommitedArcaneDustChanges()
	{
		this.m_unCommitedArcaneDustAdjustments = 0L;
	}

	// Token: 0x060012AE RID: 4782 RVA: 0x0006A21D File Offset: 0x0006841D
	public int GetNumClientTransactions()
	{
		if (this.m_pendingClientTransaction == null)
		{
			return 0;
		}
		return this.m_pendingClientTransaction.TransactionAmt;
	}

	// Token: 0x060012AF RID: 4783 RVA: 0x0006A234 File Offset: 0x00068434
	public void NotifyOfTransaction(int amt)
	{
		if (this.m_pendingClientTransaction != null)
		{
			this.m_pendingClientTransaction.TransactionAmt += amt;
		}
	}

	// Token: 0x060012B0 RID: 4784 RVA: 0x0006A251 File Offset: 0x00068451
	public bool IsCancelling()
	{
		return this.m_cancellingCraftMode;
	}

	// Token: 0x060012B1 RID: 4785 RVA: 0x0006A25C File Offset: 0x0006845C
	private Actor CreateActorCopy(Actor actor, TAG_PREMIUM premium)
	{
		string heroSkinOrHandActor = ActorNames.GetHeroSkinOrHandActor(actor.GetEntityDef().GetCardType(), premium);
		Actor component = AssetLoader.Get().InstantiatePrefab(heroSkinOrHandActor, AssetLoadingOptions.IgnorePrefabPosition).GetComponent<Actor>();
		component.SetFullDefFromActor(actor);
		component.SetEntity(actor.GetEntity());
		component.SetPremium(premium);
		component.UpdateAllComponents();
		return component;
	}

	// Token: 0x060012B2 RID: 4786 RVA: 0x0006A2B4 File Offset: 0x000684B4
	public void EnterCraftMode(Actor collectionCardActor, Action callback = null)
	{
		this.m_collectionCardActor = collectionCardActor;
		if (this.m_collectionCardActor == null)
		{
			return;
		}
		this.m_cardActors = new CollectionCardActors();
		if (collectionCardActor.GetPremium() == TAG_PREMIUM.DIAMOND)
		{
			this.m_cardActors.AddCardActor(this.CreateActorCopy(collectionCardActor, TAG_PREMIUM.DIAMOND));
		}
		else
		{
			this.m_cardActors.AddCardActor(this.CreateActorCopy(collectionCardActor, TAG_PREMIUM.NORMAL));
			this.m_cardActors.AddCardActor(this.CreateActorCopy(collectionCardActor, TAG_PREMIUM.GOLDEN));
		}
		if (this.m_cancellingCraftMode || CollectionDeckTray.Get().IsWaitingToDeleteDeck())
		{
			return;
		}
		CollectionManager.Get().GetCollectibleDisplay().HideAllTips();
		this.m_offClickCatcher.enabled = true;
		TooltipPanelManager.Get().HideKeywordHelp();
		this.SetupActor(this.m_collectionCardActor, this.m_collectionCardActor.GetPremium());
		if (this.m_cardInfoPane == null && !UniversalInputManager.UsePhoneUI)
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab("CardInfoPane.prefab:b9220edd61d504be38fab162c18e56f1", AssetLoadingOptions.None);
			this.m_cardInfoPane = gameObject.GetComponent<CardInfoPane>();
		}
		if (this.m_cardInfoPane != null)
		{
			this.m_cardInfoPane.UpdateContent();
		}
		if (this.m_craftingUI == null)
		{
			string input = UniversalInputManager.UsePhoneUI ? "CraftingUI_Phone.prefab:3119329ada4ac4a8888187b5b2d373f5" : "CraftingUI.prefab:ef05b5bf5ebb14a22919f0095d75f0b2";
			GameObject gameObject2 = AssetLoader.Get().InstantiatePrefab(input, AssetLoadingOptions.None);
			this.m_craftingUI = gameObject2.GetComponent<CraftingUI>();
			this.m_craftingUI.SetStartingActive();
			GameUtils.SetParent(this.m_craftingUI, this.m_showCraftingUIBone.gameObject, false);
		}
		this.m_craftingUI.gameObject.SetActive(true);
		this.m_switchPremiumButton.gameObject.SetActive(false);
		this.m_craftingUI.Enable(this.m_showCraftingUIBone.position, this.m_hideCraftingUIBone.position);
		this.FadeEffectsIn();
		this.UpdateCardInfoPane();
		this.ShowLeagueLockedCardPopup();
		this.IsInCraftingMode = true;
		Navigation.Push(delegate
		{
			bool result = this.CancelCraftMode();
			if (callback != null)
			{
				callback();
			}
			return result;
		});
	}

	// Token: 0x060012B3 RID: 4787 RVA: 0x0006A4B4 File Offset: 0x000686B4
	private void SetupActor(Actor collectionCardActor, TAG_PREMIUM premium)
	{
		if (this.m_upsideDownActor != null)
		{
			UnityEngine.Object.Destroy(this.m_upsideDownActor.gameObject);
		}
		if (this.m_currentBigActor != null)
		{
			UnityEngine.Object.Destroy(this.m_currentBigActor.gameObject);
		}
		Debug.Log(string.Concat(new object[]
		{
			"setting up actor ",
			collectionCardActor.GetEntityDef(),
			" ",
			premium
		}));
		this.MoveCardToBigSpot(collectionCardActor, premium);
		string cardId = collectionCardActor.GetEntityDef().GetCardId();
		this.m_pendingClientTransaction = new PendingTransaction();
		if (GameUtils.IsClassicCard(cardId))
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(cardId);
			this.m_pendingClientTransaction.CardID = GameUtils.TranslateDbIdToCardId(entityDef.GetTag(GAME_TAG.DECK_RULE_COUNT_AS_COPY_OF_CARD_ID), false);
		}
		else
		{
			this.m_pendingClientTransaction.CardID = cardId;
		}
		this.m_pendingClientTransaction.Premium = premium;
		this.m_pendingClientTransaction.TransactionAmt = 0;
		NetCache.CardValue cardValue = this.GetCardValue(this.m_pendingClientTransaction.CardID, premium);
		if (cardValue != null)
		{
			this.m_pendingClientTransaction.CardValueOverridden = cardValue.IsOverrideActive();
		}
		if (this.m_craftingUI != null)
		{
			this.m_craftingUI.Enable(this.m_showCraftingUIBone.position, this.m_hideCraftingUIBone.position);
		}
	}

	// Token: 0x060012B4 RID: 4788 RVA: 0x0006A5FC File Offset: 0x000687FC
	public bool CancelCraftMode()
	{
		base.StopAllCoroutines();
		this.m_offClickCatcher.enabled = false;
		this.m_cancellingCraftMode = true;
		Actor actor = this.m_upsideDownActor;
		Actor actor2 = this.m_currentBigActor;
		if (actor2 == null && actor != null)
		{
			actor2 = actor;
			actor = null;
		}
		float num = 0.2f;
		if (actor2 != null)
		{
			iTween.Stop(actor2.gameObject);
			iTween.RotateTo(actor2.gameObject, Vector3.zero, num);
			actor2.ToggleForceIdle(false);
			if (actor != null)
			{
				iTween.Stop(actor.gameObject);
				actor.transform.parent = actor2.transform;
			}
			SoundManager.Get().LoadAndPlay("Card_Transition_In.prefab:3f3fbe896b8b260448e8c7e5d028d971");
			iTween.MoveTo(actor2.gameObject, iTween.Hash(new object[]
			{
				"name",
				"CancelCraftMode",
				"position",
				this.m_craftSourcePosition,
				"time",
				num,
				"oncomplete",
				"FinishActorMoveAway",
				"oncompletetarget",
				base.gameObject,
				"easetype",
				iTween.EaseType.linear
			}));
			iTween.ScaleTo(actor2.gameObject, iTween.Hash(new object[]
			{
				"scale",
				this.m_craftSourceScale,
				"time",
				num,
				"easetype",
				iTween.EaseType.linear
			}));
		}
		iTween.Stop(this.m_cardCountTab.gameObject);
		if (this.GetNumOwnedIncludePending() > 0)
		{
			iTween.MoveTo(this.m_cardCountTab.gameObject, iTween.Hash(new object[]
			{
				"position",
				this.m_craftSourcePosition - new Vector3(0f, 12f, 0f),
				"time",
				3f * num,
				"oncomplete",
				iTween.EaseType.easeInQuad
			}));
			iTween.ScaleTo(this.m_cardCountTab.gameObject, iTween.Hash(new object[]
			{
				"scale",
				0.1f * Vector3.one,
				"time",
				3f * num,
				"oncomplete",
				iTween.EaseType.easeInQuad
			}));
		}
		if (actor != null)
		{
			iTween.RotateTo(actor.gameObject, new Vector3(0f, 359f, 180f), num);
			iTween.MoveTo(actor.gameObject, iTween.Hash(new object[]
			{
				"name",
				"CancelCraftMode2",
				"position",
				new Vector3(0f, -1f, 0f),
				"time",
				num,
				"islocal",
				true
			}));
			iTween.ScaleTo(actor.gameObject, new Vector3(actor.transform.localScale.x * 0.8f, actor.transform.localScale.y * 0.8f, actor.transform.localScale.z * 0.8f), num);
		}
		this.HideAndDestroyRelatedBigCard();
		if (this.m_craftingUI != null && this.m_craftingUI.IsEnabled())
		{
			this.m_craftingUI.Disable(this.m_hideCraftingUIBone.position);
		}
		this.m_cardCountTab.m_shadow.GetComponent<Animation>().Play("Crafting2ndCardShadowOff");
		this.FadeEffectsOut();
		if (this.m_cardInfoPane != null)
		{
			iTween.Stop(this.m_cardInfoPane.gameObject);
			this.m_cardInfoPane.gameObject.SetActive(false);
		}
		iTween.ScaleTo(this.m_switchPremiumButton.gameObject, this.m_cardCountTabHideScale, 0.4f);
		this.TellServerAboutWhatUserDid();
		this.IsInCraftingMode = false;
		return true;
	}

	// Token: 0x060012B5 RID: 4789 RVA: 0x0006AA12 File Offset: 0x00068C12
	public void CreateButtonPressed()
	{
		this.HideAndDestroyRelatedBigCard();
		this.m_craftingUI.DoCreate();
	}

	// Token: 0x060012B6 RID: 4790 RVA: 0x0006AA25 File Offset: 0x00068C25
	public void DisenchantButtonPressed()
	{
		this.HideAndDestroyRelatedBigCard();
		this.m_craftingUI.DoDisenchant();
	}

	// Token: 0x060012B7 RID: 4791 RVA: 0x0006AA38 File Offset: 0x00068C38
	public void UpdateBankText()
	{
		if (this.m_craftingUI != null)
		{
			this.m_craftingUI.UpdateBankText();
		}
	}

	// Token: 0x060012B8 RID: 4792 RVA: 0x0006AA54 File Offset: 0x00068C54
	private void TellServerAboutWhatUserDid()
	{
		if (this.GetCurrentActor() == null)
		{
			return;
		}
		string cardID = this.m_pendingClientTransaction.CardID;
		TAG_PREMIUM premium = this.m_pendingClientTransaction.Premium;
		int assetId = GameUtils.TranslateCardIdToDbId(cardID, false);
		if (this.m_pendingClientTransaction.TransactionAmt != 0)
		{
			this.m_pendingServerTransaction = new PendingTransaction();
			this.m_pendingServerTransaction.CardID = this.m_pendingClientTransaction.CardID;
			this.m_pendingServerTransaction.TransactionAmt = this.m_pendingClientTransaction.TransactionAmt;
			this.m_pendingServerTransaction.Premium = this.m_pendingClientTransaction.Premium;
		}
		int numCopiesInCollection = CollectionManager.Get().GetNumCopiesInCollection(cardID, premium);
		NetCache.CardValue cardValue = this.GetCardValue(cardID, premium);
		if (cardValue == null)
		{
			return;
		}
		if (cardValue.IsOverrideActive() == this.m_pendingClientTransaction.CardValueOverridden)
		{
			if (this.m_pendingClientTransaction.TransactionAmt < 0)
			{
				Log.Crafting.Print("Selling card: cardId={0} count={1} owned={2} premium={3}", new object[]
				{
					cardID,
					this.m_pendingClientTransaction.TransactionAmt,
					numCopiesInCollection,
					premium
				});
				Network.Get().SellCard(assetId, premium, -this.m_pendingClientTransaction.TransactionAmt, cardValue.GetSellValue(), numCopiesInCollection);
			}
			else if (this.m_pendingClientTransaction.TransactionAmt > 0)
			{
				Log.Crafting.Print("Buying card: cardId={0} count={1} owned={2} premium={3}", new object[]
				{
					cardID,
					this.m_pendingClientTransaction.TransactionAmt,
					numCopiesInCollection,
					premium
				});
				Network.Get().BuyCard(assetId, premium, this.m_pendingClientTransaction.TransactionAmt, cardValue.GetBuyValue(), numCopiesInCollection);
			}
		}
		else
		{
			this.OnCardValueChangedError(null);
		}
		this.m_pendingClientTransaction = null;
		this.ResetUnCommitedArcaneDustChanges();
		BnetBar.Get().RefreshCurrency();
	}

	// Token: 0x060012B9 RID: 4793 RVA: 0x0006AC18 File Offset: 0x00068E18
	public void OnCardGenericError(Network.CardSaleResult sale)
	{
		this.m_pendingServerTransaction = null;
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_ERROR_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_COLLECTION_GENERIC_ERROR");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x060012BA RID: 4794 RVA: 0x0006AC6C File Offset: 0x00068E6C
	public void OnCardPermissionError(Network.CardSaleResult sale)
	{
		this.m_pendingServerTransaction = null;
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_ERROR_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_COLLECTION_CARD_PERMISSION_ERROR");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x060012BB RID: 4795 RVA: 0x0006ACC0 File Offset: 0x00068EC0
	public void OnCardDisenchantSoulboundError(Network.CardSaleResult sale)
	{
		this.m_pendingServerTransaction = null;
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_ERROR_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_COLLECTION_CARD_SOULBOUND");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x060012BC RID: 4796 RVA: 0x0006AD14 File Offset: 0x00068F14
	public void OnCardCountError(Network.CardSaleResult sale)
	{
		this.m_pendingServerTransaction = null;
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_ERROR_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_COLLECTION_GENERIC_ERROR");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x060012BD RID: 4797 RVA: 0x0006AD68 File Offset: 0x00068F68
	public void OnCardCraftingEventNotActiveError(Network.CardSaleResult sale)
	{
		this.m_pendingServerTransaction = null;
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_ERROR_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_COLLECTION_CARD_CRAFTING_EVENT_NOT_ACTIVE");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x060012BE RID: 4798 RVA: 0x0006ADBC File Offset: 0x00068FBC
	public void OnCardUnknownError(Network.CardSaleResult sale)
	{
		this.m_pendingServerTransaction = null;
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_ERROR_HEADER");
		popupInfo.m_text = GameStrings.Format("GLUE_COLLECTION_CARD_UNKNOWN_ERROR", new object[]
		{
			sale.Action
		});
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x060012BF RID: 4799 RVA: 0x0006AE24 File Offset: 0x00069024
	public void OnCardValueChangedError(Network.CardSaleResult sale)
	{
		this.m_pendingServerTransaction = null;
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_ERROR_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_COLLECTION_CARD_VALUE_CHANGED_ERROR");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x060012C0 RID: 4800 RVA: 0x0006AE78 File Offset: 0x00069078
	public void OnCardDisenchanted(Network.CardSaleResult sale)
	{
		this.m_pendingServerTransaction = null;
		CollectionCardVisual cardVisual = CollectionManager.Get().GetCollectibleDisplay().m_pageManager.GetCardVisual(sale.AssetName, sale.Premium);
		if (cardVisual != null && cardVisual.IsShown())
		{
			cardVisual.OnDoneCrafting();
		}
	}

	// Token: 0x060012C1 RID: 4801 RVA: 0x0006AEC4 File Offset: 0x000690C4
	public void OnCardCreated(Network.CardSaleResult sale)
	{
		this.m_pendingServerTransaction = null;
		CollectionCardVisual cardVisual = CollectionManager.Get().GetCollectibleDisplay().m_pageManager.GetCardVisual(sale.AssetName, sale.Premium);
		if (cardVisual != null && cardVisual.IsShown())
		{
			cardVisual.OnDoneCrafting();
			if (TemporaryAccountManager.IsTemporaryAccount() && cardVisual.GetActor() != null && sale.Action == Network.CardSaleResult.SaleResult.CARD_WAS_BOUGHT)
			{
				EntityDef entityDef = cardVisual.GetActor().GetEntityDef();
				if (entityDef != null && (entityDef.GetRarity() == TAG_RARITY.EPIC || entityDef.GetRarity() == TAG_RARITY.LEGENDARY))
				{
					TemporaryAccountManager.Get().ShowEarnCardEventHealUpDialog(TemporaryAccountManager.HealUpReason.CRAFT_CARD);
				}
			}
		}
	}

	// Token: 0x060012C2 RID: 4802 RVA: 0x0006AF5C File Offset: 0x0006915C
	public void LoadGhostActorIfNecessary()
	{
		if (this.m_cancellingCraftMode)
		{
			return;
		}
		iTween.ScaleTo(this.m_cardCountTab.gameObject, this.m_cardCountTabHideScale, 0.4f);
		if (this.GetNumOwnedIncludePending() <= 0)
		{
			if (this.m_upsideDownActor != null)
			{
				Log.Crafting.Print("Deleting rogue m_upsideDownActor!", Array.Empty<object>());
				UnityEngine.Object.Destroy(this.m_upsideDownActor.gameObject);
			}
			this.m_currentBigActor = this.GetAndPositionNewActor(this.m_currentBigActor, 0);
			this.m_currentBigActor.name = "CurrentBigActor";
			this.m_currentBigActor.transform.position = this.m_floatingCardBone.position;
			this.m_currentBigActor.transform.localScale = this.m_floatingCardBone.localScale;
			this.m_cardCountTab.transform.position = new Vector3(0f, 307f, -10f);
			this.SetBigActorLayer(true);
			return;
		}
		if (this.m_upsideDownActor == null)
		{
			this.m_currentBigActor = this.GetAndPositionNewActor(this.m_currentBigActor, 1);
			this.m_currentBigActor.name = "CurrentBigActor";
			this.m_currentBigActor.transform.position = this.m_floatingCardBone.position;
			this.m_currentBigActor.transform.localScale = this.m_floatingCardBone.localScale;
			this.m_cardCountTab.transform.position = new Vector3(0f, 307f, -10f);
			this.SetBigActorLayer(true);
			return;
		}
		this.m_upsideDownActor.transform.parent = null;
		this.m_currentBigActor = this.m_upsideDownActor;
		this.m_currentBigActor.name = "CurrentBigActor";
		this.m_upsideDownActor = null;
	}

	// Token: 0x060012C3 RID: 4803 RVA: 0x0006B11C File Offset: 0x0006931C
	public Actor LoadNewActorAndConstructIt()
	{
		if (this.m_cancellingCraftMode)
		{
			return null;
		}
		if (!this.m_isCurrentActorAGhost)
		{
			if (this.m_currentBigActor == null)
			{
				this.m_currentBigActor = this.GetAndPositionNewActor(this.m_upsideDownActor, 0);
			}
			else
			{
				Component currentBigActor = this.m_currentBigActor;
				this.m_currentBigActor = this.GetAndPositionNewActor(this.m_currentBigActor, 0);
				Debug.LogWarning("Destroying unexpected m_currentBigActor to prevent a lost reference");
				UnityEngine.Object.Destroy(currentBigActor.gameObject);
			}
			this.m_isCurrentActorAGhost = false;
			this.m_currentBigActor.name = "CurrentBigActor";
			this.m_currentBigActor.transform.position = this.m_floatingCardBone.position;
			this.m_currentBigActor.transform.localScale = this.m_floatingCardBone.localScale;
			this.SetBigActorLayer(true);
		}
		this.m_currentBigActor.ActivateSpellBirthState(SpellType.CONSTRUCT);
		return this.m_currentBigActor;
	}

	// Token: 0x060012C4 RID: 4804 RVA: 0x0006B1F6 File Offset: 0x000693F6
	public void ForceNonGhostFlagOn()
	{
		this.m_isCurrentActorAGhost = false;
	}

	// Token: 0x060012C5 RID: 4805 RVA: 0x0006B200 File Offset: 0x00069400
	public void FinishCreateAnims()
	{
		if (this.m_currentBigActor == null || this.m_cancellingCraftMode)
		{
			return;
		}
		iTween.ScaleTo(this.m_cardCountTab.gameObject, this.m_cardCountTabShowScale, 0.4f);
		this.m_currentBigActor.GetSpell(SpellType.GHOSTMODE).GetComponent<PlayMakerFSM>().SendEvent("Cancel");
		this.m_isCurrentActorAGhost = false;
		int numOwnedIncludePending = this.GetNumOwnedIncludePending();
		this.m_cardCountTab.UpdateText(numOwnedIncludePending, this.m_currentBigActor.GetPremium());
		this.m_cardCountTab.transform.position = this.m_cardCounterBone.position;
		this.ShowRelatedBigCard(this.m_currentBigActor.GetPremium());
	}

	// Token: 0x060012C6 RID: 4806 RVA: 0x0006B2AC File Offset: 0x000694AC
	public void FlipCurrentActor()
	{
		if (this.m_currentBigActor == null || this.m_isCurrentActorAGhost)
		{
			return;
		}
		this.m_cardCountTab.transform.localScale = this.m_cardCountTabHideScale;
		if (this.m_upsideDownActor != null)
		{
			Debug.LogError("m_upsideDownActor was not null, destroying object to prevent lost reference");
			UnityEngine.Object.Destroy(this.m_upsideDownActor.gameObject);
			this.m_upsideDownActor = null;
		}
		this.m_upsideDownActor = this.m_currentBigActor;
		this.m_upsideDownActor.name = "UpsideDownActor";
		this.m_upsideDownActor.GetSpell(SpellType.GHOSTMODE).GetComponent<PlayMakerFSM>().SendEvent("Cancel");
		this.m_currentBigActor = null;
		iTween.Stop(this.m_upsideDownActor.gameObject);
		iTween.RotateTo(this.m_upsideDownActor.gameObject, iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, 350f, 180f),
			"time",
			1f
		}));
		iTween.MoveTo(this.m_upsideDownActor.gameObject, iTween.Hash(new object[]
		{
			"name",
			"FlipCurrentActor",
			"position",
			this.m_faceDownCardBone.position,
			"time",
			1f
		}));
		base.StartCoroutine(this.ReplaceFaceDownActorWithHiddenCard());
	}

	// Token: 0x060012C7 RID: 4807 RVA: 0x0006B420 File Offset: 0x00069620
	public void FinishFlipCurrentActorEarly()
	{
		base.StopAllCoroutines();
		if (this.m_currentBigActor != null)
		{
			iTween.Stop(this.m_currentBigActor.gameObject);
		}
		if (this.m_upsideDownActor != null)
		{
			iTween.Stop(this.m_upsideDownActor.gameObject);
		}
		this.m_currentBigActor.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		this.m_currentBigActor.transform.position = this.m_floatingCardBone.position;
		this.m_currentBigActor.Show();
		GameObject hiddenStandIn = this.m_currentBigActor.GetHiddenStandIn();
		if (hiddenStandIn == null)
		{
			return;
		}
		hiddenStandIn.SetActive(false);
		UnityEngine.Object.Destroy(hiddenStandIn);
	}

	// Token: 0x060012C8 RID: 4808 RVA: 0x0006B4DC File Offset: 0x000696DC
	public void FlipUpsideDownCard(Actor oldActor)
	{
		if (this.m_cancellingCraftMode)
		{
			return;
		}
		int numOwnedIncludePending = this.GetNumOwnedIncludePending();
		if (numOwnedIncludePending > 1)
		{
			this.m_upsideDownActor = this.GetAndPositionNewUpsideDownActor(this.m_currentBigActor, false);
			this.m_upsideDownActor.name = "UpsideDownActor";
			base.StartCoroutine(this.ReplaceFaceDownActorWithHiddenCard());
		}
		if (numOwnedIncludePending >= 1)
		{
			iTween.ScaleTo(this.m_cardCountTab.gameObject, iTween.Hash(new object[]
			{
				"scale",
				this.m_cardCountTabShowScale,
				"time",
				0.4f,
				"delay",
				this.m_timeForCardToFlipUp
			}));
			this.m_cardCountTab.UpdateText(numOwnedIncludePending, this.m_currentBigActor.GetPremium());
		}
		if (this.m_isCurrentActorAGhost)
		{
			this.m_currentBigActor.gameObject.transform.position = this.m_floatingCardBone.position;
		}
		else
		{
			iTween.MoveTo(this.m_currentBigActor.gameObject, iTween.Hash(new object[]
			{
				"name",
				"FlipUpsideDownCard",
				"position",
				this.m_floatingCardBone.position,
				"time",
				this.m_timeForCardToFlipUp,
				"easetype",
				this.m_easeTypeForCardFlip
			}));
		}
		iTween.RotateTo(this.m_currentBigActor.gameObject, iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, 0f, 0f),
			"time",
			this.m_timeForCardToFlipUp,
			"easetype",
			this.m_easeTypeForCardFlip,
			"oncomplete",
			"OnCardFlipComplete",
			"oncompletetarget",
			base.gameObject
		}));
		base.StartCoroutine(this.ReplaceHiddenCardwithRealActor(this.m_currentBigActor));
	}

	// Token: 0x060012C9 RID: 4809 RVA: 0x0006B6E1 File Offset: 0x000698E1
	private IEnumerator ReplaceFaceDownActorWithHiddenCard()
	{
		while (this.m_upsideDownActor != null && this.m_upsideDownActor.transform.localEulerAngles.z < 90f)
		{
			yield return null;
		}
		if (this.m_upsideDownActor == null)
		{
			yield break;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_hiddenActor.gameObject);
		if (CardBackManager.Get().IsFavoriteCardBackRandomAndEnabled())
		{
			Actor component = gameObject.GetComponent<Actor>();
			component.SetCardBackSlotOverride(new CardBackManager.CardBackSlot?(CardBackManager.CardBackSlot.RANDOM));
			component.UpdateCardBack();
		}
		gameObject.transform.parent = this.m_upsideDownActor.transform;
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
		gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		this.m_upsideDownActor.Hide();
		this.m_upsideDownActor.SetHiddenStandIn(gameObject);
		yield break;
	}

	// Token: 0x060012CA RID: 4810 RVA: 0x0006B6F0 File Offset: 0x000698F0
	private IEnumerator ReplaceHiddenCardwithRealActor(Actor actor)
	{
		while (actor != null && actor.transform.localEulerAngles.z > 90f && actor.transform.localEulerAngles.z < 270f)
		{
			yield return null;
		}
		if (actor == null)
		{
			yield break;
		}
		actor.Show();
		GameObject hiddenStandIn = actor.GetHiddenStandIn();
		if (hiddenStandIn == null)
		{
			yield break;
		}
		hiddenStandIn.SetActive(false);
		UnityEngine.Object.Destroy(hiddenStandIn);
		yield break;
	}

	// Token: 0x060012CB RID: 4811 RVA: 0x0006B6FF File Offset: 0x000698FF
	private void OnCardFlipComplete()
	{
		this.ShowRelatedBigCard(this.m_currentBigActor.GetPremium());
	}

	// Token: 0x060012CC RID: 4812 RVA: 0x0006B712 File Offset: 0x00069912
	public PendingTransaction GetPendingClientTransaction()
	{
		return this.m_pendingClientTransaction;
	}

	// Token: 0x060012CD RID: 4813 RVA: 0x0006B71A File Offset: 0x0006991A
	public PendingTransaction GetPendingServerTransaction()
	{
		return this.m_pendingServerTransaction;
	}

	// Token: 0x060012CE RID: 4814 RVA: 0x0006B724 File Offset: 0x00069924
	public void ShowCraftingUI(UIEvent e)
	{
		if (this.m_craftingUI.IsEnabled())
		{
			this.m_craftingUI.Disable(this.m_hideCraftingUIBone.position);
			return;
		}
		this.m_craftingUI.Enable(this.m_showCraftingUIBone.position, this.m_hideCraftingUIBone.position);
	}

	// Token: 0x060012CF RID: 4815 RVA: 0x0006B776 File Offset: 0x00069976
	private Actor GetCurrentActor()
	{
		if (this.m_currentBigActor != null)
		{
			return this.m_currentBigActor;
		}
		if (this.m_upsideDownActor != null)
		{
			return this.m_upsideDownActor;
		}
		return null;
	}

	// Token: 0x060012D0 RID: 4816 RVA: 0x0006B7A4 File Offset: 0x000699A4
	private void MoveCardToBigSpot(Actor collectionCardActor, TAG_PREMIUM premium)
	{
		if (collectionCardActor == null)
		{
			return;
		}
		EntityDef entityDef = collectionCardActor.GetEntityDef();
		if (entityDef == null)
		{
			return;
		}
		int numCopiesInCollection = CollectionManager.Get().GetNumCopiesInCollection(entityDef.GetCardId(), premium);
		if (this.m_currentBigActor != null)
		{
			Debug.LogError("m_currentBigActor was not null, destroying object before we lose the reference");
			UnityEngine.Object.Destroy(this.m_currentBigActor.gameObject);
			this.m_currentBigActor = null;
		}
		this.m_currentBigActor = this.GetAndPositionNewActor(this.m_cardActors.GetActor(premium), numCopiesInCollection);
		if (this.m_currentBigActor == null)
		{
			Debug.LogError("CraftingManager.MoveCardToBigSpot - GetAndPositionNewActor returned null");
			return;
		}
		this.m_currentBigActor.name = "CurrentBigActor";
		this.m_craftSourcePosition = collectionCardActor.transform.position;
		this.m_craftSourceScale = collectionCardActor.transform.lossyScale;
		this.m_craftSourceScale = Vector3.one * Mathf.Min(new float[]
		{
			this.m_craftSourceScale.x,
			this.m_craftSourceScale.y,
			this.m_craftSourceScale.z
		});
		this.m_currentBigActor.transform.position = this.m_craftSourcePosition;
		TransformUtil.SetWorldScale(this.m_currentBigActor, this.m_craftSourceScale);
		this.SetBigActorLayer(true);
		this.m_currentBigActor.ToggleForceIdle(true);
		this.m_currentBigActor.SetActorState(ActorStateType.CARD_IDLE);
		if (entityDef.IsHeroSkin())
		{
			this.m_cardCountTab.gameObject.SetActive(false);
		}
		else
		{
			this.m_cardCountTab.gameObject.SetActive(true);
			if (numCopiesInCollection > 1)
			{
				if (this.m_upsideDownActor != null)
				{
					Debug.LogError("m_upsideDownActor was not null, destroying object before we lose the reference");
					UnityEngine.Object.Destroy(this.m_upsideDownActor.gameObject);
					this.m_upsideDownActor = null;
				}
				this.m_upsideDownActor = this.GetAndPositionNewUpsideDownActor(collectionCardActor, true);
				this.m_upsideDownActor.name = "UpsideDownActor";
				base.StartCoroutine(this.ReplaceFaceDownActorWithHiddenCard());
			}
			if (numCopiesInCollection > 0)
			{
				this.m_cardCountTab.UpdateText(numCopiesInCollection, premium);
				this.m_cardCountTab.transform.position = new Vector3(collectionCardActor.transform.position.x, collectionCardActor.transform.position.y - 2f, collectionCardActor.transform.position.z);
			}
		}
		this.FinishBigCardMove();
	}

	// Token: 0x060012D1 RID: 4817 RVA: 0x0006B9E8 File Offset: 0x00069BE8
	private string GetRelatedCardId(EntityDef def)
	{
		int tag = def.GetTag(GAME_TAG.COLLECTION_RELATED_CARD_DATABASE_ID);
		CardDbfRecord record = GameDbf.Card.GetRecord(tag);
		if (record != null)
		{
			return record.NoteMiniGuid;
		}
		if (def.IsHero())
		{
			return GameUtils.GetHeroPowerCardIdFromHero(def.GetCardId());
		}
		if (!def.IsQuest())
		{
			return null;
		}
		int tag2 = def.GetTag(GAME_TAG.QUEST_REWARD_DATABASE_ID);
		CardDbfRecord record2 = GameDbf.Card.GetRecord(tag2);
		if (record2 == null)
		{
			return null;
		}
		return record2.NoteMiniGuid;
	}

	// Token: 0x060012D2 RID: 4818 RVA: 0x0006BA58 File Offset: 0x00069C58
	private void ShowRelatedBigCard(TAG_PREMIUM premium)
	{
		if (this.m_currentBigActor == null)
		{
			Debug.LogError("Unexpected error in ShowRelatedBigCard. Current big actor was null");
			return;
		}
		EntityDef entityDef = this.m_currentBigActor.GetEntityDef();
		if (entityDef == null)
		{
			Debug.LogError("Unexpected error in ShowRelatedBigCard. Current big actor's entity def was null");
			return;
		}
		string relatedCardId = this.GetRelatedCardId(entityDef);
		if (string.IsNullOrEmpty(relatedCardId))
		{
			return;
		}
		int numOwnedIncludePending = this.GetNumOwnedIncludePending();
		Actor templateActorForType = this.GetTemplateActorForType(entityDef.GetCardType(), premium);
		if (templateActorForType.GetEntityDef() == null || templateActorForType.GetEntityDef().GetCardId() != relatedCardId || templateActorForType.GetPremium() != premium)
		{
			using (DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(relatedCardId, this.m_currentBigActor.CardPortraitQuality))
			{
				templateActorForType.SetCardDef(fullDef.DisposableCardDef);
				templateActorForType.SetEntityDef(fullDef.EntityDef);
				templateActorForType.SetPremium(premium);
			}
		}
		if (this.m_currentRelatedCardActor != null)
		{
			Debug.LogWarning("Current related card actor was not new when creating a new one. Ensure we cleanup this actor");
			this.HideAndDestroyRelatedBigCard();
		}
		this.m_currentRelatedCardActor = this.GetAndPositionNewActor(templateActorForType, numOwnedIncludePending);
		if (entityDef.IsQuest())
		{
			EntityDef entityDef2 = DefLoader.Get().GetEntityDef(relatedCardId);
			if (entityDef2 != null)
			{
				bool isPremium = this.m_currentRelatedCardActor.GetPremium() == TAG_PREMIUM.GOLDEN;
				this.AddQuestOverlay(entityDef2, isPremium, this.m_currentRelatedCardActor.gameObject);
			}
		}
		SceneUtils.SetLayer(this.m_currentRelatedCardActor.gameObject, GameLayer.IgnoreFullScreenEffects);
		this.m_currentRelatedCardActor.gameObject.transform.parent = this.m_currentBigActor.transform;
		base.StartCoroutine(this.RevealRelatedCard(this.m_currentRelatedCardActor));
	}

	// Token: 0x060012D3 RID: 4819 RVA: 0x0006BBE8 File Offset: 0x00069DE8
	private IEnumerator RevealRelatedCard(Actor actor)
	{
		if (actor == null)
		{
			yield break;
		}
		Spell ghostSpell = actor.GetSpellIfLoaded(SpellType.GHOSTMODE);
		if (ghostSpell != null)
		{
			while (!ghostSpell.IsFinished())
			{
				yield return null;
			}
		}
		if (actor.gameObject == null)
		{
			yield break;
		}
		actor.Show();
		GameObject gameObject = actor.gameObject;
		Transform transform = gameObject.transform;
		transform.localPosition = CraftingManager.HERO_POWER_START_POSITION;
		transform.localScale = CraftingManager.HERO_POWER_START_SCALE;
		iTween.MoveTo(gameObject, iTween.Hash(new object[]
		{
			"position",
			CraftingManager.HERO_POWER_POSITION.Value,
			"isLocal",
			true,
			"time",
			CraftingManager.HERO_POWER_TWEEN_TIME
		}));
		iTween.ScaleTo(gameObject, iTween.Hash(new object[]
		{
			"scale",
			CraftingManager.HERO_POWER_SCALE.Value,
			"isLocal",
			true,
			"time",
			CraftingManager.HERO_POWER_TWEEN_TIME
		}));
		yield break;
	}

	// Token: 0x060012D4 RID: 4820 RVA: 0x0006BBF7 File Offset: 0x00069DF7
	private void HideAndDestroyRelatedBigCard()
	{
		if (this.m_currentRelatedCardActor == null)
		{
			return;
		}
		UnityEngine.Object.Destroy(this.m_currentRelatedCardActor.gameObject);
		this.m_currentRelatedCardActor = null;
	}

	// Token: 0x060012D5 RID: 4821 RVA: 0x0006BC20 File Offset: 0x00069E20
	private void FinishBigCardMove()
	{
		if (this.m_currentBigActor == null)
		{
			return;
		}
		int numOwnedIncludePending = this.GetNumOwnedIncludePending();
		SoundManager.Get().LoadAndPlay("Card_Transition_Out.prefab:aecf5b5837772844b9d2db995744df82");
		iTween.MoveTo(this.m_currentBigActor.gameObject, iTween.Hash(new object[]
		{
			"name",
			"FinishBigCardMove",
			"position",
			this.m_floatingCardBone.position,
			"time",
			0.4f,
			"oncomplete",
			"FinishActorMoveTowardsScreen",
			"oncompletetarget",
			base.gameObject
		}));
		iTween.ScaleTo(this.m_currentBigActor.gameObject, iTween.Hash(new object[]
		{
			"scale",
			this.m_floatingCardBone.localScale,
			"time",
			0.4f,
			"easetype",
			iTween.EaseType.easeOutQuad
		}));
		if (numOwnedIncludePending > 0)
		{
			this.m_cardCountTab.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			iTween.MoveTo(this.m_cardCountTab.gameObject, this.m_cardCounterBone.position, 0.4f);
			iTween.ScaleTo(this.m_cardCountTab.gameObject, this.m_cardCountTabShowScale, 0.4f);
		}
	}

	// Token: 0x060012D6 RID: 4822 RVA: 0x0006BD94 File Offset: 0x00069F94
	private void UpdateCardInfoPane()
	{
		if (this.m_cardInfoPane == null || this.m_currentBigActor == null)
		{
			Debug.LogError("CraftingManager.UpdateCardInfoPane - m_cardInfoPane or m_currentBigActor are null");
			return;
		}
		this.m_cardInfoPane.gameObject.SetActive(true);
		this.m_cardInfoPane.UpdateContent();
		this.m_cardInfoPane.transform.position = this.m_currentBigActor.transform.position - new Vector3(0f, 1f, 0f);
		Vector3 localScale = this.m_cardInfoPaneBone.localScale;
		this.m_cardInfoPane.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		iTween.MoveTo(this.m_cardInfoPane.gameObject, this.m_cardInfoPaneBone.position, 0.5f);
		iTween.ScaleTo(this.m_cardInfoPane.gameObject, localScale, 0.5f);
	}

	// Token: 0x060012D7 RID: 4823 RVA: 0x0006B6FF File Offset: 0x000698FF
	private void FinishActorMoveTowardsScreen()
	{
		this.ShowRelatedBigCard(this.m_currentBigActor.GetPremium());
	}

	// Token: 0x060012D8 RID: 4824 RVA: 0x0006BE84 File Offset: 0x0006A084
	private void FinishActorMoveAway()
	{
		this.m_cancellingCraftMode = false;
		iTween.Stop(this.m_cardCountTab.gameObject);
		this.m_cardCountTab.transform.position = new Vector3(0f, 307f, -10f);
		if (this.m_upsideDownActor != null)
		{
			UnityEngine.Object.Destroy(this.m_upsideDownActor.gameObject);
		}
		if (this.m_currentBigActor != null)
		{
			UnityEngine.Object.Destroy(this.m_currentBigActor.gameObject);
		}
		this.LoadRandomCardBack();
	}

	// Token: 0x060012D9 RID: 4825 RVA: 0x0006BF0E File Offset: 0x0006A10E
	private void FadeEffectsIn()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.SetBlurBrightness(1f);
		fullScreenFXMgr.SetBlurDesaturation(0f);
		fullScreenFXMgr.Vignette();
		fullScreenFXMgr.Blur(1f, 0.4f, iTween.EaseType.easeOutCirc, null);
	}

	// Token: 0x060012DA RID: 4826 RVA: 0x0006BF43 File Offset: 0x0006A143
	private void FadeEffectsOut()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.StopVignette(0.2f, iTween.EaseType.easeOutCirc, new Action(this.OnVignetteFinished), null);
		fullScreenFXMgr.StopBlur();
	}

	// Token: 0x060012DB RID: 4827 RVA: 0x0006BF6C File Offset: 0x0006A16C
	private void OnVignetteFinished()
	{
		this.SetBigActorLayer(false);
		if (this.GetCurrentCardVisual() != null)
		{
			this.GetCurrentCardVisual().OnDoneCrafting();
		}
		if (this.m_currentBigActor != null)
		{
			this.m_currentBigActor.name = "USED_TO_BE_CurrentBigActor";
			base.StartCoroutine(this.MakeSureActorIsCleanedUp(this.m_currentBigActor));
		}
		this.m_currentBigActor = null;
		this.m_craftingUI.gameObject.SetActive(false);
	}

	// Token: 0x060012DC RID: 4828 RVA: 0x0006BFE2 File Offset: 0x0006A1E2
	private IEnumerator MakeSureActorIsCleanedUp(Actor oldActor)
	{
		yield return new WaitForSeconds(1f);
		if (oldActor == null)
		{
			yield break;
		}
		UnityEngine.Object.DestroyImmediate(oldActor);
		yield break;
	}

	// Token: 0x060012DD RID: 4829 RVA: 0x0006BFF4 File Offset: 0x0006A1F4
	private Actor GetAndPositionNewUpsideDownActor(Actor oldActor, bool fromPage)
	{
		Actor andPositionNewActor = this.GetAndPositionNewActor(oldActor, 1);
		SceneUtils.SetLayer(andPositionNewActor.gameObject, GameLayer.IgnoreFullScreenEffects);
		if (fromPage)
		{
			andPositionNewActor.transform.position = oldActor.transform.position + new Vector3(0f, -2f, 0f);
			andPositionNewActor.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
			iTween.RotateTo(andPositionNewActor.gameObject, new Vector3(0f, 350f, 180f), 0.4f);
			iTween.MoveTo(andPositionNewActor.gameObject, iTween.Hash(new object[]
			{
				"name",
				"GetAndPositionNewUpsideDownActor",
				"position",
				this.m_faceDownCardBone.position,
				"time",
				0.4f
			}));
			iTween.ScaleTo(andPositionNewActor.gameObject, this.m_faceDownCardBone.localScale, 0.4f);
		}
		else
		{
			andPositionNewActor.transform.localEulerAngles = new Vector3(0f, 350f, 180f);
			andPositionNewActor.transform.position = this.m_faceDownCardBone.position + new Vector3(0f, -6f, 0f);
			andPositionNewActor.transform.localScale = this.m_faceDownCardBone.localScale;
			iTween.MoveTo(andPositionNewActor.gameObject, iTween.Hash(new object[]
			{
				"name",
				"GetAndPositionNewUpsideDownActor",
				"position",
				this.m_faceDownCardBone.position,
				"time",
				this.m_timeForBackCardToMoveUp,
				"easetype",
				this.m_easeTypeForCardMoveUp,
				"delay",
				this.m_delayBeforeBackCardMovesUp
			}));
		}
		return andPositionNewActor;
	}

	// Token: 0x060012DE RID: 4830 RVA: 0x0006C1F0 File Offset: 0x0006A3F0
	private Actor GetAndPositionNewActor(Actor oldActor, int numCopies)
	{
		Actor actor;
		if (numCopies == 0)
		{
			actor = this.GetGhostActor(oldActor);
		}
		else
		{
			actor = this.GetNonGhostActor(oldActor);
		}
		if (actor != null)
		{
			actor.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		}
		return actor;
	}

	// Token: 0x060012DF RID: 4831 RVA: 0x0006C23C File Offset: 0x0006A43C
	private Actor GetGhostActor(Actor actor)
	{
		this.m_isCurrentActorAGhost = true;
		bool flag = actor.GetPremium() == TAG_PREMIUM.GOLDEN;
		bool flag2 = actor.GetPremium() == TAG_PREMIUM.DIAMOND;
		Actor templateActor = this.m_ghostMinionActor;
		switch (actor.GetEntityDef().GetCardType())
		{
		case TAG_CARDTYPE.HERO:
			if (flag)
			{
				templateActor = this.m_ghostGoldenHeroActor;
				goto IL_D7;
			}
			templateActor = this.m_ghostHeroActor;
			goto IL_D7;
		case TAG_CARDTYPE.MINION:
			if (flag)
			{
				templateActor = this.m_ghostGoldenMinionActor;
				goto IL_D7;
			}
			if (flag2)
			{
				templateActor = this.m_ghostDiamondMinionActor;
				goto IL_D7;
			}
			templateActor = this.m_ghostMinionActor;
			goto IL_D7;
		case TAG_CARDTYPE.SPELL:
			if (flag)
			{
				templateActor = this.m_ghostGoldenSpellActor;
				goto IL_D7;
			}
			templateActor = this.m_ghostSpellActor;
			goto IL_D7;
		case TAG_CARDTYPE.WEAPON:
			if (flag)
			{
				templateActor = this.m_ghostGoldenWeaponActor;
				goto IL_D7;
			}
			templateActor = this.m_ghostWeaponActor;
			goto IL_D7;
		case TAG_CARDTYPE.HERO_POWER:
			if (flag)
			{
				templateActor = this.m_ghostGoldenHeroPowerActor;
				goto IL_D7;
			}
			templateActor = this.m_ghostHeroPowerActor;
			goto IL_D7;
		}
		Debug.LogError("CraftingManager.GetGhostActor() - tried to get a ghost actor for a cardtype that we haven't anticipated!!");
		IL_D7:
		return this.SetUpGhostActor(templateActor, actor);
	}

	// Token: 0x060012E0 RID: 4832 RVA: 0x0006C328 File Offset: 0x0006A528
	private Actor GetNonGhostActor(Actor actor)
	{
		this.m_isCurrentActorAGhost = false;
		return this.SetUpNonGhostActor(this.GetTemplateActor(actor), actor);
	}

	// Token: 0x060012E1 RID: 4833 RVA: 0x0006C340 File Offset: 0x0006A540
	private Actor GetTemplateActorForType(TAG_CARDTYPE type, TAG_PREMIUM premium)
	{
		bool flag = premium == TAG_PREMIUM.GOLDEN;
		bool flag2 = premium == TAG_PREMIUM.DIAMOND;
		switch (type)
		{
		case TAG_CARDTYPE.HERO:
			if (flag)
			{
				return this.m_templateGoldenHeroActor;
			}
			return this.m_templateHeroActor;
		case TAG_CARDTYPE.MINION:
			if (flag)
			{
				return this.m_templateGoldenMinionActor;
			}
			if (flag2)
			{
				return this.m_templateDiamondMinionActor;
			}
			return this.m_templateMinionActor;
		case TAG_CARDTYPE.SPELL:
			if (flag)
			{
				return this.m_templateGoldenSpellActor;
			}
			return this.m_templateSpellActor;
		case TAG_CARDTYPE.WEAPON:
			if (flag)
			{
				return this.m_templateGoldenWeaponActor;
			}
			return this.m_templateWeaponActor;
		case TAG_CARDTYPE.HERO_POWER:
			if (flag)
			{
				return this.m_templateGoldenHeroPowerActor;
			}
			return this.m_templateHeroPowerActor;
		}
		Debug.LogError("CraftingManager.GetTemplateActorForType() - tried to get a actor for a cardtype that we haven't anticipated!!");
		return this.m_templateMinionActor;
	}

	// Token: 0x060012E2 RID: 4834 RVA: 0x0006C3F0 File Offset: 0x0006A5F0
	private Actor GetTemplateActor(Actor actor)
	{
		return this.GetTemplateActorForType(actor.GetEntityDef().GetCardType(), actor.GetPremium());
	}

	// Token: 0x060012E3 RID: 4835 RVA: 0x0006C40C File Offset: 0x0006A60C
	private Actor SetUpNonGhostActor(Actor templateActor, Actor actor)
	{
		Actor actor2 = UnityEngine.Object.Instantiate<Actor>(templateActor);
		actor2.SetFullDefFromActor(actor);
		actor2.SetPremium(actor.GetPremium());
		if (CardBackManager.Get().IsFavoriteCardBackRandomAndEnabled())
		{
			actor2.SetCardBackSlotOverride(new CardBackManager.CardBackSlot?(CardBackManager.CardBackSlot.RANDOM));
		}
		actor2.SetUnlit();
		actor2.UpdateAllComponents();
		return actor2;
	}

	// Token: 0x060012E4 RID: 4836 RVA: 0x0006C458 File Offset: 0x0006A658
	private Actor SetUpGhostActor(Actor templateActor, Actor actor)
	{
		if (templateActor == null || actor == null)
		{
			Debug.LogError("CraftingManager.SetUpGhostActor - passed arguments are null");
			return null;
		}
		Actor actor2 = UnityEngine.Object.Instantiate<Actor>(templateActor);
		actor2.SetFullDefFromActor(actor);
		actor2.SetPremium(actor.GetPremium());
		if (CardBackManager.Get().IsFavoriteCardBackRandomAndEnabled())
		{
			actor2.SetCardBackSlotOverride(new CardBackManager.CardBackSlot?(CardBackManager.CardBackSlot.RANDOM));
		}
		actor2.UpdateAllComponents();
		actor2.UpdatePortraitTexture();
		actor2.UpdateCardColor();
		actor2.SetUnlit();
		actor2.Hide();
		if (actor.isMissingCard())
		{
			actor2.ActivateSpellBirthState(SpellType.MISSING_BIGCARD);
		}
		else
		{
			actor2.ActivateSpellBirthState(SpellType.GHOSTMODE);
		}
		base.StartCoroutine(this.ShowAfterTwoFrames(actor2));
		return actor2;
	}

	// Token: 0x060012E5 RID: 4837 RVA: 0x0006C4FE File Offset: 0x0006A6FE
	private IEnumerator ShowAfterTwoFrames(Actor actorToShow)
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		if (actorToShow != this.m_currentBigActor)
		{
			yield break;
		}
		actorToShow.Show();
		yield break;
	}

	// Token: 0x060012E6 RID: 4838 RVA: 0x0006C514 File Offset: 0x0006A714
	private void SetBigActorLayer(bool inCraftingMode)
	{
		if (this.m_currentBigActor == null)
		{
			return;
		}
		GameLayer layer = inCraftingMode ? GameLayer.IgnoreFullScreenEffects : GameLayer.CardRaycast;
		SceneUtils.SetLayer(this.m_currentBigActor.gameObject, layer);
	}

	// Token: 0x060012E7 RID: 4839 RVA: 0x0006C54C File Offset: 0x0006A74C
	private CollectionCardVisual GetCurrentCardVisual()
	{
		EntityDef entityDef;
		TAG_PREMIUM premium;
		if (!this.GetShownCardInfo(out entityDef, out premium))
		{
			return null;
		}
		return CollectionManager.Get().GetCollectibleDisplay().m_pageManager.GetCardVisual(entityDef.GetCardId(), premium);
	}

	// Token: 0x060012E8 RID: 4840 RVA: 0x0006C584 File Offset: 0x0006A784
	public int GetNumOwnedIncludePending(TAG_PREMIUM premium)
	{
		EntityDef entityDef = this.m_collectionCardActor.GetEntityDef();
		string cardId = entityDef.GetCardId();
		int numCopiesInCollection = CollectionManager.Get().GetNumCopiesInCollection(cardId, premium);
		if (this.m_pendingClientTransaction != null && (this.m_pendingClientTransaction.CardID == cardId || (GameUtils.IsClassicCard(cardId) && this.m_pendingClientTransaction.CardID == GameUtils.TranslateDbIdToCardId(entityDef.GetTag(GAME_TAG.DECK_RULE_COUNT_AS_COPY_OF_CARD_ID), false))) && this.m_pendingClientTransaction.Premium == premium)
		{
			return numCopiesInCollection + this.m_pendingClientTransaction.TransactionAmt;
		}
		return numCopiesInCollection;
	}

	// Token: 0x060012E9 RID: 4841 RVA: 0x0006C614 File Offset: 0x0006A814
	public int GetNumOwnedIncludePending()
	{
		TAG_PREMIUM premium = this.m_collectionCardActor.GetPremium();
		return this.GetNumOwnedIncludePending(premium);
	}

	// Token: 0x060012EA RID: 4842 RVA: 0x0006C634 File Offset: 0x0006A834
	private void AddQuestOverlay(EntityDef def, bool isPremium, GameObject parent)
	{
		if (this.m_questCardRewardOverlay == null)
		{
			Debug.LogWarning("Attempted to add quest overlay to a card, but no prefab was set on CraftinManager");
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_questCardRewardOverlay.gameObject, parent.transform);
		if (gameObject == null)
		{
			Debug.LogError("Could not instantiate a new quest reward overlay from prefab");
			return;
		}
		QuestCardRewardOverlay component = gameObject.GetComponent<QuestCardRewardOverlay>();
		if (component == null)
		{
			Debug.LogError("Newly instantiated quest reward overlay game object does not contain a QuestCardRewardOverlay component.");
			UnityEngine.Object.Destroy(gameObject);
			return;
		}
		component.SetEntityType(def, isPremium);
	}

	// Token: 0x060012EB RID: 4843 RVA: 0x0006C6B0 File Offset: 0x0006A8B0
	private void LoadActor(string actorPath, ref Actor actor)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(actorPath, AssetLoadingOptions.IgnorePrefabPosition);
		gameObject.transform.position = new Vector3(-99999f, 99999f, 99999f);
		actor = gameObject.GetComponent<Actor>();
		actor.TurnOffCollider();
	}

	// Token: 0x060012EC RID: 4844 RVA: 0x0006C700 File Offset: 0x0006A900
	private void LoadActor(string actorPath, ref Actor actor, ref Actor actorCopy)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(actorPath, AssetLoadingOptions.IgnorePrefabPosition);
		gameObject.transform.position = new Vector3(-99999f, 99999f, 99999f);
		actor = gameObject.GetComponent<Actor>();
		actorCopy = UnityEngine.Object.Instantiate<Actor>(actor);
		actor.TurnOffCollider();
		actorCopy.TurnOffCollider();
	}

	// Token: 0x060012ED RID: 4845 RVA: 0x0006C760 File Offset: 0x0006A960
	private void ShowLeagueLockedCardPopup()
	{
		EntityDef entityDef;
		TAG_PREMIUM tag_PREMIUM;
		if (!this.GetShownCardInfo(out entityDef, out tag_PREMIUM))
		{
			return;
		}
		if (RankMgr.Get().IsCardLockedInCurrentLeague(entityDef))
		{
			LeagueDbfRecord localPlayerStandardLeagueConfig = RankMgr.Get().GetLocalPlayerStandardLeagueConfig();
			if (!string.IsNullOrEmpty(localPlayerStandardLeagueConfig.LockedCardPopupTitleText) && !string.IsNullOrEmpty(localPlayerStandardLeagueConfig.LockedCardPopupBodyText))
			{
				AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
				popupInfo.m_headerText = localPlayerStandardLeagueConfig.LockedCardPopupTitleText;
				popupInfo.m_text = localPlayerStandardLeagueConfig.LockedCardPopupBodyText;
				popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
				popupInfo.m_layerToUse = new GameLayer?(GameLayer.UI);
				popupInfo.m_showAlertIcon = false;
				DialogManager.Get().ShowPopup(popupInfo);
				return;
			}
		}
	}

	// Token: 0x060012EE RID: 4846 RVA: 0x0006C804 File Offset: 0x0006AA04
	private void LoadRandomCardBack()
	{
		if (CardBackManager.Get().IsFavoriteCardBackRandomAndEnabled())
		{
			CardBackManager.Get().LoadRandomCardBackOwnedByPlayer();
		}
	}

	// Token: 0x04000BF2 RID: 3058
	public Transform m_floatingCardBone;

	// Token: 0x04000BF3 RID: 3059
	public Transform m_faceDownCardBone;

	// Token: 0x04000BF4 RID: 3060
	public Transform m_cardInfoPaneBone;

	// Token: 0x04000BF5 RID: 3061
	public Transform m_cardCounterBone;

	// Token: 0x04000BF6 RID: 3062
	public Transform m_showCraftingUIBone;

	// Token: 0x04000BF7 RID: 3063
	public Transform m_hideCraftingUIBone;

	// Token: 0x04000BF8 RID: 3064
	public BoxCollider m_offClickCatcher;

	// Token: 0x04000BF9 RID: 3065
	public CraftCardCountTab m_cardCountTab;

	// Token: 0x04000BFA RID: 3066
	public Vector3 m_cardCountTabShowScale = Vector3.one;

	// Token: 0x04000BFB RID: 3067
	public Vector3 m_cardCountTabHideScale = new Vector3(1f, 1f, 0f);

	// Token: 0x04000BFC RID: 3068
	public PegUIElement m_switchPremiumButton;

	// Token: 0x04000BFD RID: 3069
	public QuestCardRewardOverlay m_questCardRewardOverlay;

	// Token: 0x04000BFE RID: 3070
	public float m_timeForCardToFlipUp;

	// Token: 0x04000BFF RID: 3071
	public float m_timeForBackCardToMoveUp;

	// Token: 0x04000C00 RID: 3072
	public float m_delayBeforeBackCardMovesUp;

	// Token: 0x04000C01 RID: 3073
	public iTween.EaseType m_easeTypeForCardFlip;

	// Token: 0x04000C02 RID: 3074
	public iTween.EaseType m_easeTypeForCardMoveUp;

	// Token: 0x04000C03 RID: 3075
	private static CraftingManager s_instance;

	// Token: 0x04000C04 RID: 3076
	public CraftingUI m_craftingUI;

	// Token: 0x04000C05 RID: 3077
	private Actor m_currentBigActor;

	// Token: 0x04000C06 RID: 3078
	private bool m_isCurrentActorAGhost;

	// Token: 0x04000C07 RID: 3079
	private Actor m_upsideDownActor;

	// Token: 0x04000C08 RID: 3080
	private Actor m_currentRelatedCardActor;

	// Token: 0x04000C09 RID: 3081
	private Actor m_ghostWeaponActor;

	// Token: 0x04000C0A RID: 3082
	private Actor m_ghostMinionActor;

	// Token: 0x04000C0B RID: 3083
	private Actor m_ghostSpellActor;

	// Token: 0x04000C0C RID: 3084
	private Actor m_ghostHeroActor;

	// Token: 0x04000C0D RID: 3085
	private Actor m_ghostHeroPowerActor;

	// Token: 0x04000C0E RID: 3086
	private Actor m_templateWeaponActor;

	// Token: 0x04000C0F RID: 3087
	private Actor m_templateSpellActor;

	// Token: 0x04000C10 RID: 3088
	private Actor m_templateMinionActor;

	// Token: 0x04000C11 RID: 3089
	private Actor m_templateHeroActor;

	// Token: 0x04000C12 RID: 3090
	private Actor m_templateHeroPowerActor;

	// Token: 0x04000C13 RID: 3091
	private Actor m_hiddenActor;

	// Token: 0x04000C14 RID: 3092
	private CardInfoPane m_cardInfoPane;

	// Token: 0x04000C15 RID: 3093
	private Actor m_templateGoldenWeaponActor;

	// Token: 0x04000C16 RID: 3094
	private Actor m_templateGoldenSpellActor;

	// Token: 0x04000C17 RID: 3095
	private Actor m_templateGoldenMinionActor;

	// Token: 0x04000C18 RID: 3096
	private Actor m_templateGoldenHeroActor;

	// Token: 0x04000C19 RID: 3097
	private Actor m_templateGoldenHeroPowerActor;

	// Token: 0x04000C1A RID: 3098
	private Actor m_templateDiamondMinionActor;

	// Token: 0x04000C1B RID: 3099
	private Actor m_ghostGoldenWeaponActor;

	// Token: 0x04000C1C RID: 3100
	private Actor m_ghostGoldenSpellActor;

	// Token: 0x04000C1D RID: 3101
	private Actor m_ghostGoldenMinionActor;

	// Token: 0x04000C1E RID: 3102
	private Actor m_ghostGoldenHeroActor;

	// Token: 0x04000C1F RID: 3103
	private Actor m_ghostGoldenHeroPowerActor;

	// Token: 0x04000C20 RID: 3104
	private Actor m_ghostDiamondMinionActor;

	// Token: 0x04000C21 RID: 3105
	private bool m_cancellingCraftMode;

	// Token: 0x04000C22 RID: 3106
	private long m_unCommitedArcaneDustAdjustments;

	// Token: 0x04000C23 RID: 3107
	private PendingTransaction m_pendingClientTransaction;

	// Token: 0x04000C24 RID: 3108
	private PendingTransaction m_pendingServerTransaction;

	// Token: 0x04000C25 RID: 3109
	private Vector3 m_craftSourcePosition;

	// Token: 0x04000C26 RID: 3110
	private Vector3 m_craftSourceScale;

	// Token: 0x04000C27 RID: 3111
	private CollectionCardActors m_cardActors;

	// Token: 0x04000C28 RID: 3112
	private Actor m_collectionCardActor;

	// Token: 0x04000C29 RID: 3113
	private bool m_elementsLoaded;

	// Token: 0x04000C2A RID: 3114
	private static readonly PlatformDependentValue<Vector3> HERO_POWER_START_POSITION = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(0f, -0.5f, 0f),
		Phone = new Vector3(0f, -0.5f, 0f)
	};

	// Token: 0x04000C2B RID: 3115
	private static readonly PlatformDependentValue<Vector3> HERO_POWER_START_SCALE = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(0.1f, 0.1f, 0.1f),
		Phone = new Vector3(0.1f, 0.1f, 0.1f)
	};

	// Token: 0x04000C2C RID: 3116
	private static readonly PlatformDependentValue<Vector3> HERO_POWER_POSITION = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(-2.11f, -0.010312f, -0.06f),
		Phone = new Vector3(-1.97f, -0.0006f, -0.033f)
	};

	// Token: 0x04000C2D RID: 3117
	private static readonly PlatformDependentValue<Vector3> HERO_POWER_SCALE = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(0.85f, 0.85f, 0.85f),
		Phone = new Vector3(0.76637f, 0.76637f, 0.76637f)
	};

	// Token: 0x04000C2E RID: 3118
	private static readonly float HERO_POWER_TWEEN_TIME = 0.5f;
}
