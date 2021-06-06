using System;
using System.Collections;
using UnityEngine;

[CustomEditClass]
public class CraftingManager : MonoBehaviour
{
	public Transform m_floatingCardBone;

	public Transform m_faceDownCardBone;

	public Transform m_cardInfoPaneBone;

	public Transform m_cardCounterBone;

	public Transform m_showCraftingUIBone;

	public Transform m_hideCraftingUIBone;

	public BoxCollider m_offClickCatcher;

	public CraftCardCountTab m_cardCountTab;

	public Vector3 m_cardCountTabShowScale = Vector3.one;

	public Vector3 m_cardCountTabHideScale = new Vector3(1f, 1f, 0f);

	public PegUIElement m_switchPremiumButton;

	public QuestCardRewardOverlay m_questCardRewardOverlay;

	public float m_timeForCardToFlipUp;

	public float m_timeForBackCardToMoveUp;

	public float m_delayBeforeBackCardMovesUp;

	public iTween.EaseType m_easeTypeForCardFlip;

	public iTween.EaseType m_easeTypeForCardMoveUp;

	private static CraftingManager s_instance;

	public CraftingUI m_craftingUI;

	private Actor m_currentBigActor;

	private bool m_isCurrentActorAGhost;

	private Actor m_upsideDownActor;

	private Actor m_currentRelatedCardActor;

	private Actor m_ghostWeaponActor;

	private Actor m_ghostMinionActor;

	private Actor m_ghostSpellActor;

	private Actor m_ghostHeroActor;

	private Actor m_ghostHeroPowerActor;

	private Actor m_templateWeaponActor;

	private Actor m_templateSpellActor;

	private Actor m_templateMinionActor;

	private Actor m_templateHeroActor;

	private Actor m_templateHeroPowerActor;

	private Actor m_hiddenActor;

	private CardInfoPane m_cardInfoPane;

	private Actor m_templateGoldenWeaponActor;

	private Actor m_templateGoldenSpellActor;

	private Actor m_templateGoldenMinionActor;

	private Actor m_templateGoldenHeroActor;

	private Actor m_templateGoldenHeroPowerActor;

	private Actor m_templateDiamondMinionActor;

	private Actor m_ghostGoldenWeaponActor;

	private Actor m_ghostGoldenSpellActor;

	private Actor m_ghostGoldenMinionActor;

	private Actor m_ghostGoldenHeroActor;

	private Actor m_ghostGoldenHeroPowerActor;

	private Actor m_ghostDiamondMinionActor;

	private bool m_cancellingCraftMode;

	private long m_unCommitedArcaneDustAdjustments;

	private PendingTransaction m_pendingClientTransaction;

	private PendingTransaction m_pendingServerTransaction;

	private Vector3 m_craftSourcePosition;

	private Vector3 m_craftSourceScale;

	private CollectionCardActors m_cardActors;

	private Actor m_collectionCardActor;

	private bool m_elementsLoaded;

	private static readonly PlatformDependentValue<Vector3> HERO_POWER_START_POSITION = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(0f, -0.5f, 0f),
		Phone = new Vector3(0f, -0.5f, 0f)
	};

	private static readonly PlatformDependentValue<Vector3> HERO_POWER_START_SCALE = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(0.1f, 0.1f, 0.1f),
		Phone = new Vector3(0.1f, 0.1f, 0.1f)
	};

	private static readonly PlatformDependentValue<Vector3> HERO_POWER_POSITION = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(-2.11f, -0.010312f, -0.06f),
		Phone = new Vector3(-1.97f, -0.0006f, -0.033f)
	};

	private static readonly PlatformDependentValue<Vector3> HERO_POWER_SCALE = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(0.85f, 0.85f, 0.85f),
		Phone = new Vector3(0.76637f, 0.76637f, 0.76637f)
	};

	private static readonly float HERO_POWER_TWEEN_TIME = 0.5f;

	public static bool IsInitialized => s_instance != null;

	private bool IsInCraftingMode { get; set; }

	private void Awake()
	{
		CollectionManager.Get().RegisterMassDisenchantListener(OnMassDisenchant);
	}

	private void OnDestroy()
	{
		if (CollectionManager.Get() != null)
		{
			CollectionManager.Get().RemoveMassDisenchantListener(OnMassDisenchant);
		}
		s_instance = null;
	}

	private void Start()
	{
		LoadElements();
	}

	private void LoadElements()
	{
		if (!m_elementsLoaded)
		{
			LoadActor("Card_Hand_Weapon.prefab:30888a1fdca5c6c43abcc5d9dca55783", ref m_ghostWeaponActor, ref m_templateWeaponActor);
			LoadActor(ActorNames.GetHandActor(TAG_CARDTYPE.WEAPON, TAG_PREMIUM.GOLDEN), ref m_ghostGoldenWeaponActor, ref m_templateGoldenWeaponActor);
			LoadActor("Card_Hand_Ally.prefab:d00eb0f79080e0749993fe4619e9143d", ref m_ghostMinionActor, ref m_templateMinionActor);
			LoadActor(ActorNames.GetHandActor(TAG_CARDTYPE.MINION, TAG_PREMIUM.GOLDEN), ref m_ghostGoldenMinionActor, ref m_templateGoldenMinionActor);
			LoadActor(ActorNames.GetHandActor(TAG_CARDTYPE.MINION, TAG_PREMIUM.DIAMOND), ref m_ghostDiamondMinionActor, ref m_templateDiamondMinionActor);
			LoadActor("Card_Hand_Ability.prefab:3c3f5189f0d0b3745a1c1ca21d41efe0", ref m_ghostSpellActor, ref m_templateSpellActor);
			LoadActor(ActorNames.GetHandActor(TAG_CARDTYPE.SPELL, TAG_PREMIUM.GOLDEN), ref m_ghostGoldenSpellActor, ref m_templateGoldenSpellActor);
			LoadActor("Card_Hand_Hero.prefab:a977c49edb5fb5d4c8dee4d2344d1395", ref m_ghostHeroActor, ref m_templateHeroActor);
			LoadActor(ActorNames.GetHandActor(TAG_CARDTYPE.HERO, TAG_PREMIUM.GOLDEN), ref m_ghostGoldenHeroActor, ref m_templateGoldenHeroActor);
			LoadActor("History_HeroPower.prefab:e73edf8ccea2b11429093f7a448eef53", ref m_ghostHeroPowerActor, ref m_templateHeroPowerActor);
			LoadActor(ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.HISTORY_HERO_POWER, TAG_PREMIUM.GOLDEN), ref m_ghostGoldenHeroPowerActor, ref m_templateGoldenHeroPowerActor);
			LoadActor("Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", ref m_hiddenActor);
			m_hiddenActor.GetMeshRenderer().transform.localEulerAngles = new Vector3(0f, 180f, 180f);
			SceneUtils.SetLayer(m_hiddenActor.gameObject, GameLayer.IgnoreFullScreenEffects);
			SoundManager.Get().Load("Card_Transition_Out.prefab:aecf5b5837772844b9d2db995744df82");
			SoundManager.Get().Load("Card_Transition_In.prefab:3f3fbe896b8b260448e8c7e5d028d971");
			LoadRandomCardBack();
			m_elementsLoaded = true;
		}
	}

	private void SwitchPremiumView(UIEvent e)
	{
		TAG_PREMIUM premium = TAG_PREMIUM.GOLDEN;
		switch (m_currentBigActor.GetPremium())
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
		TellServerAboutWhatUserDid();
		SetupActor(m_collectionCardActor, premium);
	}

	public static CraftingManager Get()
	{
		if (s_instance == null)
		{
			string text = (UniversalInputManager.UsePhoneUI ? "CraftingManager_phone.prefab:d28ac29ae64f14e649186d0d1fe5f7e8" : "CraftingManager.prefab:9dc2dd187dd914959b311d326c3fd5b2");
			s_instance = AssetLoader.Get().InstantiatePrefab(text).GetComponent<CraftingManager>();
			s_instance.LoadElements();
		}
		return s_instance;
	}

	public NetCache.CardValue GetCardValue(string cardID, TAG_PREMIUM premium)
	{
		NetCache.NetCacheCardValues netObject = NetCache.Get().GetNetObject<NetCache.NetCacheCardValues>();
		NetCache.CardDefinition key = new NetCache.CardDefinition
		{
			Name = cardID,
			Premium = premium
		};
		if (netObject == null || !netObject.Values.TryGetValue(key, out var value))
		{
			return null;
		}
		return value;
	}

	public bool IsCardShowing()
	{
		return m_currentBigActor != null;
	}

	public static bool GetIsInCraftingMode()
	{
		if (s_instance != null)
		{
			return s_instance.IsInCraftingMode;
		}
		return false;
	}

	public bool GetShownCardInfo(out EntityDef entityDef, out TAG_PREMIUM premium)
	{
		entityDef = null;
		premium = TAG_PREMIUM.NORMAL;
		if (m_currentBigActor == null)
		{
			return false;
		}
		entityDef = m_currentBigActor.GetEntityDef();
		premium = m_currentBigActor.GetPremium();
		if (entityDef == null)
		{
			return false;
		}
		return true;
	}

	public Actor GetShownActor()
	{
		return m_currentBigActor;
	}

	public void OnMassDisenchant(int amount)
	{
		if (!MassDisenchant.Get())
		{
			m_craftingUI.UpdateBankText();
		}
	}

	public long GetUnCommitedArcaneDustChanges()
	{
		return m_unCommitedArcaneDustAdjustments;
	}

	public void AdjustUnCommitedArcaneDustChanges(int amount)
	{
		m_unCommitedArcaneDustAdjustments += amount;
	}

	public void ResetUnCommitedArcaneDustChanges()
	{
		m_unCommitedArcaneDustAdjustments = 0L;
	}

	public int GetNumClientTransactions()
	{
		if (m_pendingClientTransaction == null)
		{
			return 0;
		}
		return m_pendingClientTransaction.TransactionAmt;
	}

	public void NotifyOfTransaction(int amt)
	{
		if (m_pendingClientTransaction != null)
		{
			m_pendingClientTransaction.TransactionAmt += amt;
		}
	}

	public bool IsCancelling()
	{
		return m_cancellingCraftMode;
	}

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

	public void EnterCraftMode(Actor collectionCardActor, Action callback = null)
	{
		m_collectionCardActor = collectionCardActor;
		if (m_collectionCardActor == null)
		{
			return;
		}
		m_cardActors = new CollectionCardActors();
		if (collectionCardActor.GetPremium() == TAG_PREMIUM.DIAMOND)
		{
			m_cardActors.AddCardActor(CreateActorCopy(collectionCardActor, TAG_PREMIUM.DIAMOND));
		}
		else
		{
			m_cardActors.AddCardActor(CreateActorCopy(collectionCardActor, TAG_PREMIUM.NORMAL));
			m_cardActors.AddCardActor(CreateActorCopy(collectionCardActor, TAG_PREMIUM.GOLDEN));
		}
		if (m_cancellingCraftMode || CollectionDeckTray.Get().IsWaitingToDeleteDeck())
		{
			return;
		}
		CollectionManager.Get().GetCollectibleDisplay().HideAllTips();
		m_offClickCatcher.enabled = true;
		TooltipPanelManager.Get().HideKeywordHelp();
		SetupActor(m_collectionCardActor, m_collectionCardActor.GetPremium());
		if (m_cardInfoPane == null && !UniversalInputManager.UsePhoneUI)
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab("CardInfoPane.prefab:b9220edd61d504be38fab162c18e56f1");
			m_cardInfoPane = gameObject.GetComponent<CardInfoPane>();
		}
		if (m_cardInfoPane != null)
		{
			m_cardInfoPane.UpdateContent();
		}
		if (m_craftingUI == null)
		{
			string text = (UniversalInputManager.UsePhoneUI ? "CraftingUI_Phone.prefab:3119329ada4ac4a8888187b5b2d373f5" : "CraftingUI.prefab:ef05b5bf5ebb14a22919f0095d75f0b2");
			GameObject gameObject2 = AssetLoader.Get().InstantiatePrefab(text);
			m_craftingUI = gameObject2.GetComponent<CraftingUI>();
			m_craftingUI.SetStartingActive();
			GameUtils.SetParent(m_craftingUI, m_showCraftingUIBone.gameObject);
		}
		m_craftingUI.gameObject.SetActive(value: true);
		m_switchPremiumButton.gameObject.SetActive(value: false);
		m_craftingUI.Enable(m_showCraftingUIBone.position, m_hideCraftingUIBone.position);
		FadeEffectsIn();
		UpdateCardInfoPane();
		ShowLeagueLockedCardPopup();
		IsInCraftingMode = true;
		Navigation.Push(delegate
		{
			bool result = CancelCraftMode();
			if (callback != null)
			{
				callback();
			}
			return result;
		});
	}

	private void SetupActor(Actor collectionCardActor, TAG_PREMIUM premium)
	{
		if (m_upsideDownActor != null)
		{
			UnityEngine.Object.Destroy(m_upsideDownActor.gameObject);
		}
		if (m_currentBigActor != null)
		{
			UnityEngine.Object.Destroy(m_currentBigActor.gameObject);
		}
		Debug.Log(string.Concat("setting up actor ", collectionCardActor.GetEntityDef(), " ", premium));
		MoveCardToBigSpot(collectionCardActor, premium);
		string cardId = collectionCardActor.GetEntityDef().GetCardId();
		m_pendingClientTransaction = new PendingTransaction();
		if (GameUtils.IsClassicCard(cardId))
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(cardId);
			m_pendingClientTransaction.CardID = GameUtils.TranslateDbIdToCardId(entityDef.GetTag(GAME_TAG.DECK_RULE_COUNT_AS_COPY_OF_CARD_ID));
		}
		else
		{
			m_pendingClientTransaction.CardID = cardId;
		}
		m_pendingClientTransaction.Premium = premium;
		m_pendingClientTransaction.TransactionAmt = 0;
		NetCache.CardValue cardValue = GetCardValue(m_pendingClientTransaction.CardID, premium);
		if (cardValue != null)
		{
			m_pendingClientTransaction.CardValueOverridden = cardValue.IsOverrideActive();
		}
		if (m_craftingUI != null)
		{
			m_craftingUI.Enable(m_showCraftingUIBone.position, m_hideCraftingUIBone.position);
		}
	}

	public bool CancelCraftMode()
	{
		StopAllCoroutines();
		m_offClickCatcher.enabled = false;
		m_cancellingCraftMode = true;
		Actor actor = m_upsideDownActor;
		Actor actor2 = m_currentBigActor;
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
			actor2.ToggleForceIdle(bOn: false);
			if (actor != null)
			{
				iTween.Stop(actor.gameObject);
				actor.transform.parent = actor2.transform;
			}
			SoundManager.Get().LoadAndPlay("Card_Transition_In.prefab:3f3fbe896b8b260448e8c7e5d028d971");
			iTween.MoveTo(actor2.gameObject, iTween.Hash("name", "CancelCraftMode", "position", m_craftSourcePosition, "time", num, "oncomplete", "FinishActorMoveAway", "oncompletetarget", base.gameObject, "easetype", iTween.EaseType.linear));
			iTween.ScaleTo(actor2.gameObject, iTween.Hash("scale", m_craftSourceScale, "time", num, "easetype", iTween.EaseType.linear));
		}
		iTween.Stop(m_cardCountTab.gameObject);
		if (GetNumOwnedIncludePending() > 0)
		{
			iTween.MoveTo(m_cardCountTab.gameObject, iTween.Hash("position", m_craftSourcePosition - new Vector3(0f, 12f, 0f), "time", 3f * num, "oncomplete", iTween.EaseType.easeInQuad));
			iTween.ScaleTo(m_cardCountTab.gameObject, iTween.Hash("scale", 0.1f * Vector3.one, "time", 3f * num, "oncomplete", iTween.EaseType.easeInQuad));
		}
		if (actor != null)
		{
			iTween.RotateTo(actor.gameObject, new Vector3(0f, 359f, 180f), num);
			iTween.MoveTo(actor.gameObject, iTween.Hash("name", "CancelCraftMode2", "position", new Vector3(0f, -1f, 0f), "time", num, "islocal", true));
			iTween.ScaleTo(actor.gameObject, new Vector3(actor.transform.localScale.x * 0.8f, actor.transform.localScale.y * 0.8f, actor.transform.localScale.z * 0.8f), num);
		}
		HideAndDestroyRelatedBigCard();
		if (m_craftingUI != null && m_craftingUI.IsEnabled())
		{
			m_craftingUI.Disable(m_hideCraftingUIBone.position);
		}
		m_cardCountTab.m_shadow.GetComponent<Animation>().Play("Crafting2ndCardShadowOff");
		FadeEffectsOut();
		if (m_cardInfoPane != null)
		{
			iTween.Stop(m_cardInfoPane.gameObject);
			m_cardInfoPane.gameObject.SetActive(value: false);
		}
		iTween.ScaleTo(m_switchPremiumButton.gameObject, m_cardCountTabHideScale, 0.4f);
		TellServerAboutWhatUserDid();
		IsInCraftingMode = false;
		return true;
	}

	public void CreateButtonPressed()
	{
		HideAndDestroyRelatedBigCard();
		m_craftingUI.DoCreate();
	}

	public void DisenchantButtonPressed()
	{
		HideAndDestroyRelatedBigCard();
		m_craftingUI.DoDisenchant();
	}

	public void UpdateBankText()
	{
		if (m_craftingUI != null)
		{
			m_craftingUI.UpdateBankText();
		}
	}

	private void TellServerAboutWhatUserDid()
	{
		if (GetCurrentActor() == null)
		{
			return;
		}
		string cardID = m_pendingClientTransaction.CardID;
		TAG_PREMIUM premium = m_pendingClientTransaction.Premium;
		int assetId = GameUtils.TranslateCardIdToDbId(cardID);
		if (m_pendingClientTransaction.TransactionAmt != 0)
		{
			m_pendingServerTransaction = new PendingTransaction();
			m_pendingServerTransaction.CardID = m_pendingClientTransaction.CardID;
			m_pendingServerTransaction.TransactionAmt = m_pendingClientTransaction.TransactionAmt;
			m_pendingServerTransaction.Premium = m_pendingClientTransaction.Premium;
		}
		int numCopiesInCollection = CollectionManager.Get().GetNumCopiesInCollection(cardID, premium);
		NetCache.CardValue cardValue = GetCardValue(cardID, premium);
		if (cardValue == null)
		{
			return;
		}
		if (cardValue.IsOverrideActive() == m_pendingClientTransaction.CardValueOverridden)
		{
			if (m_pendingClientTransaction.TransactionAmt < 0)
			{
				Log.Crafting.Print("Selling card: cardId={0} count={1} owned={2} premium={3}", cardID, m_pendingClientTransaction.TransactionAmt, numCopiesInCollection, premium);
				Network.Get().SellCard(assetId, premium, -m_pendingClientTransaction.TransactionAmt, cardValue.GetSellValue(), numCopiesInCollection);
			}
			else if (m_pendingClientTransaction.TransactionAmt > 0)
			{
				Log.Crafting.Print("Buying card: cardId={0} count={1} owned={2} premium={3}", cardID, m_pendingClientTransaction.TransactionAmt, numCopiesInCollection, premium);
				Network.Get().BuyCard(assetId, premium, m_pendingClientTransaction.TransactionAmt, cardValue.GetBuyValue(), numCopiesInCollection);
			}
		}
		else
		{
			OnCardValueChangedError(null);
		}
		m_pendingClientTransaction = null;
		ResetUnCommitedArcaneDustChanges();
		BnetBar.Get().RefreshCurrency();
	}

	public void OnCardGenericError(Network.CardSaleResult sale)
	{
		m_pendingServerTransaction = null;
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_ERROR_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_COLLECTION_GENERIC_ERROR");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	public void OnCardPermissionError(Network.CardSaleResult sale)
	{
		m_pendingServerTransaction = null;
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_ERROR_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_COLLECTION_CARD_PERMISSION_ERROR");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	public void OnCardDisenchantSoulboundError(Network.CardSaleResult sale)
	{
		m_pendingServerTransaction = null;
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_ERROR_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_COLLECTION_CARD_SOULBOUND");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	public void OnCardCountError(Network.CardSaleResult sale)
	{
		m_pendingServerTransaction = null;
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_ERROR_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_COLLECTION_GENERIC_ERROR");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	public void OnCardCraftingEventNotActiveError(Network.CardSaleResult sale)
	{
		m_pendingServerTransaction = null;
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_ERROR_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_COLLECTION_CARD_CRAFTING_EVENT_NOT_ACTIVE");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	public void OnCardUnknownError(Network.CardSaleResult sale)
	{
		m_pendingServerTransaction = null;
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_ERROR_HEADER");
		popupInfo.m_text = GameStrings.Format("GLUE_COLLECTION_CARD_UNKNOWN_ERROR", sale.Action);
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	public void OnCardValueChangedError(Network.CardSaleResult sale)
	{
		m_pendingServerTransaction = null;
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_ERROR_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_COLLECTION_CARD_VALUE_CHANGED_ERROR");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	public void OnCardDisenchanted(Network.CardSaleResult sale)
	{
		m_pendingServerTransaction = null;
		CollectionCardVisual cardVisual = CollectionManager.Get().GetCollectibleDisplay().m_pageManager.GetCardVisual(sale.AssetName, sale.Premium);
		if (cardVisual != null && cardVisual.IsShown())
		{
			cardVisual.OnDoneCrafting();
		}
	}

	public void OnCardCreated(Network.CardSaleResult sale)
	{
		m_pendingServerTransaction = null;
		CollectionCardVisual cardVisual = CollectionManager.Get().GetCollectibleDisplay().m_pageManager.GetCardVisual(sale.AssetName, sale.Premium);
		if (!(cardVisual != null) || !cardVisual.IsShown())
		{
			return;
		}
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

	public void LoadGhostActorIfNecessary()
	{
		if (m_cancellingCraftMode)
		{
			return;
		}
		iTween.ScaleTo(m_cardCountTab.gameObject, m_cardCountTabHideScale, 0.4f);
		if (GetNumOwnedIncludePending() > 0)
		{
			if (m_upsideDownActor == null)
			{
				m_currentBigActor = GetAndPositionNewActor(m_currentBigActor, 1);
				m_currentBigActor.name = "CurrentBigActor";
				m_currentBigActor.transform.position = m_floatingCardBone.position;
				m_currentBigActor.transform.localScale = m_floatingCardBone.localScale;
				m_cardCountTab.transform.position = new Vector3(0f, 307f, -10f);
				SetBigActorLayer(inCraftingMode: true);
			}
			else
			{
				m_upsideDownActor.transform.parent = null;
				m_currentBigActor = m_upsideDownActor;
				m_currentBigActor.name = "CurrentBigActor";
				m_upsideDownActor = null;
			}
		}
		else
		{
			if (m_upsideDownActor != null)
			{
				Log.Crafting.Print("Deleting rogue m_upsideDownActor!");
				UnityEngine.Object.Destroy(m_upsideDownActor.gameObject);
			}
			m_currentBigActor = GetAndPositionNewActor(m_currentBigActor, 0);
			m_currentBigActor.name = "CurrentBigActor";
			m_currentBigActor.transform.position = m_floatingCardBone.position;
			m_currentBigActor.transform.localScale = m_floatingCardBone.localScale;
			m_cardCountTab.transform.position = new Vector3(0f, 307f, -10f);
			SetBigActorLayer(inCraftingMode: true);
		}
	}

	public Actor LoadNewActorAndConstructIt()
	{
		if (m_cancellingCraftMode)
		{
			return null;
		}
		if (!m_isCurrentActorAGhost)
		{
			if (m_currentBigActor == null)
			{
				m_currentBigActor = GetAndPositionNewActor(m_upsideDownActor, 0);
			}
			else
			{
				Actor currentBigActor = m_currentBigActor;
				m_currentBigActor = GetAndPositionNewActor(m_currentBigActor, 0);
				Debug.LogWarning("Destroying unexpected m_currentBigActor to prevent a lost reference");
				UnityEngine.Object.Destroy(currentBigActor.gameObject);
			}
			m_isCurrentActorAGhost = false;
			m_currentBigActor.name = "CurrentBigActor";
			m_currentBigActor.transform.position = m_floatingCardBone.position;
			m_currentBigActor.transform.localScale = m_floatingCardBone.localScale;
			SetBigActorLayer(inCraftingMode: true);
		}
		m_currentBigActor.ActivateSpellBirthState(SpellType.CONSTRUCT);
		return m_currentBigActor;
	}

	public void ForceNonGhostFlagOn()
	{
		m_isCurrentActorAGhost = false;
	}

	public void FinishCreateAnims()
	{
		if (!(m_currentBigActor == null) && !m_cancellingCraftMode)
		{
			iTween.ScaleTo(m_cardCountTab.gameObject, m_cardCountTabShowScale, 0.4f);
			m_currentBigActor.GetSpell(SpellType.GHOSTMODE).GetComponent<PlayMakerFSM>().SendEvent("Cancel");
			m_isCurrentActorAGhost = false;
			int numOwnedIncludePending = GetNumOwnedIncludePending();
			m_cardCountTab.UpdateText(numOwnedIncludePending, m_currentBigActor.GetPremium());
			m_cardCountTab.transform.position = m_cardCounterBone.position;
			ShowRelatedBigCard(m_currentBigActor.GetPremium());
		}
	}

	public void FlipCurrentActor()
	{
		if (!(m_currentBigActor == null) && !m_isCurrentActorAGhost)
		{
			m_cardCountTab.transform.localScale = m_cardCountTabHideScale;
			if (m_upsideDownActor != null)
			{
				Debug.LogError("m_upsideDownActor was not null, destroying object to prevent lost reference");
				UnityEngine.Object.Destroy(m_upsideDownActor.gameObject);
				m_upsideDownActor = null;
			}
			m_upsideDownActor = m_currentBigActor;
			m_upsideDownActor.name = "UpsideDownActor";
			m_upsideDownActor.GetSpell(SpellType.GHOSTMODE).GetComponent<PlayMakerFSM>().SendEvent("Cancel");
			m_currentBigActor = null;
			iTween.Stop(m_upsideDownActor.gameObject);
			iTween.RotateTo(m_upsideDownActor.gameObject, iTween.Hash("rotation", new Vector3(0f, 350f, 180f), "time", 1f));
			iTween.MoveTo(m_upsideDownActor.gameObject, iTween.Hash("name", "FlipCurrentActor", "position", m_faceDownCardBone.position, "time", 1f));
			StartCoroutine(ReplaceFaceDownActorWithHiddenCard());
		}
	}

	public void FinishFlipCurrentActorEarly()
	{
		StopAllCoroutines();
		if (m_currentBigActor != null)
		{
			iTween.Stop(m_currentBigActor.gameObject);
		}
		if (m_upsideDownActor != null)
		{
			iTween.Stop(m_upsideDownActor.gameObject);
		}
		m_currentBigActor.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		m_currentBigActor.transform.position = m_floatingCardBone.position;
		m_currentBigActor.Show();
		GameObject hiddenStandIn = m_currentBigActor.GetHiddenStandIn();
		if (!(hiddenStandIn == null))
		{
			hiddenStandIn.SetActive(value: false);
			UnityEngine.Object.Destroy(hiddenStandIn);
		}
	}

	public void FlipUpsideDownCard(Actor oldActor)
	{
		if (!m_cancellingCraftMode)
		{
			int numOwnedIncludePending = GetNumOwnedIncludePending();
			if (numOwnedIncludePending > 1)
			{
				m_upsideDownActor = GetAndPositionNewUpsideDownActor(m_currentBigActor, fromPage: false);
				m_upsideDownActor.name = "UpsideDownActor";
				StartCoroutine(ReplaceFaceDownActorWithHiddenCard());
			}
			if (numOwnedIncludePending >= 1)
			{
				iTween.ScaleTo(m_cardCountTab.gameObject, iTween.Hash("scale", m_cardCountTabShowScale, "time", 0.4f, "delay", m_timeForCardToFlipUp));
				m_cardCountTab.UpdateText(numOwnedIncludePending, m_currentBigActor.GetPremium());
			}
			if (m_isCurrentActorAGhost)
			{
				m_currentBigActor.gameObject.transform.position = m_floatingCardBone.position;
			}
			else
			{
				iTween.MoveTo(m_currentBigActor.gameObject, iTween.Hash("name", "FlipUpsideDownCard", "position", m_floatingCardBone.position, "time", m_timeForCardToFlipUp, "easetype", m_easeTypeForCardFlip));
			}
			iTween.RotateTo(m_currentBigActor.gameObject, iTween.Hash("rotation", new Vector3(0f, 0f, 0f), "time", m_timeForCardToFlipUp, "easetype", m_easeTypeForCardFlip, "oncomplete", "OnCardFlipComplete", "oncompletetarget", base.gameObject));
			StartCoroutine(ReplaceHiddenCardwithRealActor(m_currentBigActor));
		}
	}

	private IEnumerator ReplaceFaceDownActorWithHiddenCard()
	{
		while (m_upsideDownActor != null && m_upsideDownActor.transform.localEulerAngles.z < 90f)
		{
			yield return null;
		}
		if (!(m_upsideDownActor == null))
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(m_hiddenActor.gameObject);
			if (CardBackManager.Get().IsFavoriteCardBackRandomAndEnabled())
			{
				Actor component = gameObject.GetComponent<Actor>();
				component.SetCardBackSlotOverride(CardBackManager.CardBackSlot.RANDOM);
				component.UpdateCardBack();
			}
			gameObject.transform.parent = m_upsideDownActor.transform;
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
			gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			m_upsideDownActor.Hide();
			m_upsideDownActor.SetHiddenStandIn(gameObject);
		}
	}

	private IEnumerator ReplaceHiddenCardwithRealActor(Actor actor)
	{
		while (actor != null && actor.transform.localEulerAngles.z > 90f && actor.transform.localEulerAngles.z < 270f)
		{
			yield return null;
		}
		if (!(actor == null))
		{
			actor.Show();
			GameObject hiddenStandIn = actor.GetHiddenStandIn();
			if (!(hiddenStandIn == null))
			{
				hiddenStandIn.SetActive(value: false);
				UnityEngine.Object.Destroy(hiddenStandIn);
			}
		}
	}

	private void OnCardFlipComplete()
	{
		ShowRelatedBigCard(m_currentBigActor.GetPremium());
	}

	public PendingTransaction GetPendingClientTransaction()
	{
		return m_pendingClientTransaction;
	}

	public PendingTransaction GetPendingServerTransaction()
	{
		return m_pendingServerTransaction;
	}

	public void ShowCraftingUI(UIEvent e)
	{
		if (m_craftingUI.IsEnabled())
		{
			m_craftingUI.Disable(m_hideCraftingUIBone.position);
		}
		else
		{
			m_craftingUI.Enable(m_showCraftingUIBone.position, m_hideCraftingUIBone.position);
		}
	}

	private Actor GetCurrentActor()
	{
		if (m_currentBigActor != null)
		{
			return m_currentBigActor;
		}
		if (m_upsideDownActor != null)
		{
			return m_upsideDownActor;
		}
		return null;
	}

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
		if (m_currentBigActor != null)
		{
			Debug.LogError("m_currentBigActor was not null, destroying object before we lose the reference");
			UnityEngine.Object.Destroy(m_currentBigActor.gameObject);
			m_currentBigActor = null;
		}
		m_currentBigActor = GetAndPositionNewActor(m_cardActors.GetActor(premium), numCopiesInCollection);
		if (m_currentBigActor == null)
		{
			Debug.LogError("CraftingManager.MoveCardToBigSpot - GetAndPositionNewActor returned null");
			return;
		}
		m_currentBigActor.name = "CurrentBigActor";
		m_craftSourcePosition = collectionCardActor.transform.position;
		m_craftSourceScale = collectionCardActor.transform.lossyScale;
		m_craftSourceScale = Vector3.one * Mathf.Min(m_craftSourceScale.x, m_craftSourceScale.y, m_craftSourceScale.z);
		m_currentBigActor.transform.position = m_craftSourcePosition;
		TransformUtil.SetWorldScale(m_currentBigActor, m_craftSourceScale);
		SetBigActorLayer(inCraftingMode: true);
		m_currentBigActor.ToggleForceIdle(bOn: true);
		m_currentBigActor.SetActorState(ActorStateType.CARD_IDLE);
		if (entityDef.IsHeroSkin())
		{
			m_cardCountTab.gameObject.SetActive(value: false);
		}
		else
		{
			m_cardCountTab.gameObject.SetActive(value: true);
			if (numCopiesInCollection > 1)
			{
				if (m_upsideDownActor != null)
				{
					Debug.LogError("m_upsideDownActor was not null, destroying object before we lose the reference");
					UnityEngine.Object.Destroy(m_upsideDownActor.gameObject);
					m_upsideDownActor = null;
				}
				m_upsideDownActor = GetAndPositionNewUpsideDownActor(collectionCardActor, fromPage: true);
				m_upsideDownActor.name = "UpsideDownActor";
				StartCoroutine(ReplaceFaceDownActorWithHiddenCard());
			}
			if (numCopiesInCollection > 0)
			{
				m_cardCountTab.UpdateText(numCopiesInCollection, premium);
				m_cardCountTab.transform.position = new Vector3(collectionCardActor.transform.position.x, collectionCardActor.transform.position.y - 2f, collectionCardActor.transform.position.z);
			}
		}
		FinishBigCardMove();
	}

	private string GetRelatedCardId(EntityDef def)
	{
		int id = def.GetTag(GAME_TAG.COLLECTION_RELATED_CARD_DATABASE_ID);
		CardDbfRecord record = GameDbf.Card.GetRecord(id);
		if (record != null)
		{
			return record.NoteMiniGuid;
		}
		if (def.IsHero())
		{
			return GameUtils.GetHeroPowerCardIdFromHero(def.GetCardId());
		}
		if (def.IsQuest())
		{
			int id2 = def.GetTag(GAME_TAG.QUEST_REWARD_DATABASE_ID);
			return GameDbf.Card.GetRecord(id2)?.NoteMiniGuid;
		}
		return null;
	}

	private void ShowRelatedBigCard(TAG_PREMIUM premium)
	{
		if (m_currentBigActor == null)
		{
			Debug.LogError("Unexpected error in ShowRelatedBigCard. Current big actor was null");
			return;
		}
		EntityDef entityDef = m_currentBigActor.GetEntityDef();
		if (entityDef == null)
		{
			Debug.LogError("Unexpected error in ShowRelatedBigCard. Current big actor's entity def was null");
			return;
		}
		string relatedCardId = GetRelatedCardId(entityDef);
		if (string.IsNullOrEmpty(relatedCardId))
		{
			return;
		}
		int numOwnedIncludePending = GetNumOwnedIncludePending();
		Actor templateActorForType = GetTemplateActorForType(entityDef.GetCardType(), premium);
		if (templateActorForType.GetEntityDef() == null || templateActorForType.GetEntityDef().GetCardId() != relatedCardId || templateActorForType.GetPremium() != premium)
		{
			using DefLoader.DisposableFullDef disposableFullDef = DefLoader.Get().GetFullDef(relatedCardId, m_currentBigActor.CardPortraitQuality);
			templateActorForType.SetCardDef(disposableFullDef.DisposableCardDef);
			templateActorForType.SetEntityDef(disposableFullDef.EntityDef);
			templateActorForType.SetPremium(premium);
		}
		if (m_currentRelatedCardActor != null)
		{
			Debug.LogWarning("Current related card actor was not new when creating a new one. Ensure we cleanup this actor");
			HideAndDestroyRelatedBigCard();
		}
		m_currentRelatedCardActor = GetAndPositionNewActor(templateActorForType, numOwnedIncludePending);
		if (entityDef.IsQuest())
		{
			EntityDef entityDef2 = DefLoader.Get().GetEntityDef(relatedCardId);
			if (entityDef2 != null)
			{
				bool isPremium = m_currentRelatedCardActor.GetPremium() == TAG_PREMIUM.GOLDEN;
				AddQuestOverlay(entityDef2, isPremium, m_currentRelatedCardActor.gameObject);
			}
		}
		SceneUtils.SetLayer(m_currentRelatedCardActor.gameObject, GameLayer.IgnoreFullScreenEffects);
		m_currentRelatedCardActor.gameObject.transform.parent = m_currentBigActor.transform;
		StartCoroutine(RevealRelatedCard(m_currentRelatedCardActor));
	}

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
		if (!(actor.gameObject == null))
		{
			actor.Show();
			GameObject gameObject = actor.gameObject;
			Transform obj = gameObject.transform;
			obj.localPosition = HERO_POWER_START_POSITION;
			obj.localScale = HERO_POWER_START_SCALE;
			iTween.MoveTo(gameObject, iTween.Hash("position", HERO_POWER_POSITION.Value, "isLocal", true, "time", HERO_POWER_TWEEN_TIME));
			iTween.ScaleTo(gameObject, iTween.Hash("scale", HERO_POWER_SCALE.Value, "isLocal", true, "time", HERO_POWER_TWEEN_TIME));
		}
	}

	private void HideAndDestroyRelatedBigCard()
	{
		if (!(m_currentRelatedCardActor == null))
		{
			UnityEngine.Object.Destroy(m_currentRelatedCardActor.gameObject);
			m_currentRelatedCardActor = null;
		}
	}

	private void FinishBigCardMove()
	{
		if (!(m_currentBigActor == null))
		{
			int numOwnedIncludePending = GetNumOwnedIncludePending();
			SoundManager.Get().LoadAndPlay("Card_Transition_Out.prefab:aecf5b5837772844b9d2db995744df82");
			iTween.MoveTo(m_currentBigActor.gameObject, iTween.Hash("name", "FinishBigCardMove", "position", m_floatingCardBone.position, "time", 0.4f, "oncomplete", "FinishActorMoveTowardsScreen", "oncompletetarget", base.gameObject));
			iTween.ScaleTo(m_currentBigActor.gameObject, iTween.Hash("scale", m_floatingCardBone.localScale, "time", 0.4f, "easetype", iTween.EaseType.easeOutQuad));
			if (numOwnedIncludePending > 0)
			{
				m_cardCountTab.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
				iTween.MoveTo(m_cardCountTab.gameObject, m_cardCounterBone.position, 0.4f);
				iTween.ScaleTo(m_cardCountTab.gameObject, m_cardCountTabShowScale, 0.4f);
			}
		}
	}

	private void UpdateCardInfoPane()
	{
		if (m_cardInfoPane == null || m_currentBigActor == null)
		{
			Debug.LogError("CraftingManager.UpdateCardInfoPane - m_cardInfoPane or m_currentBigActor are null");
			return;
		}
		m_cardInfoPane.gameObject.SetActive(value: true);
		m_cardInfoPane.UpdateContent();
		m_cardInfoPane.transform.position = m_currentBigActor.transform.position - new Vector3(0f, 1f, 0f);
		Vector3 localScale = m_cardInfoPaneBone.localScale;
		m_cardInfoPane.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		iTween.MoveTo(m_cardInfoPane.gameObject, m_cardInfoPaneBone.position, 0.5f);
		iTween.ScaleTo(m_cardInfoPane.gameObject, localScale, 0.5f);
	}

	private void FinishActorMoveTowardsScreen()
	{
		ShowRelatedBigCard(m_currentBigActor.GetPremium());
	}

	private void FinishActorMoveAway()
	{
		m_cancellingCraftMode = false;
		iTween.Stop(m_cardCountTab.gameObject);
		m_cardCountTab.transform.position = new Vector3(0f, 307f, -10f);
		if (m_upsideDownActor != null)
		{
			UnityEngine.Object.Destroy(m_upsideDownActor.gameObject);
		}
		if (m_currentBigActor != null)
		{
			UnityEngine.Object.Destroy(m_currentBigActor.gameObject);
		}
		LoadRandomCardBack();
	}

	private void FadeEffectsIn()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.SetBlurBrightness(1f);
		fullScreenFXMgr.SetBlurDesaturation(0f);
		fullScreenFXMgr.Vignette();
		fullScreenFXMgr.Blur(1f, 0.4f, iTween.EaseType.easeOutCirc);
	}

	private void FadeEffectsOut()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.StopVignette(0.2f, iTween.EaseType.easeOutCirc, OnVignetteFinished);
		fullScreenFXMgr.StopBlur();
	}

	private void OnVignetteFinished()
	{
		SetBigActorLayer(inCraftingMode: false);
		if (GetCurrentCardVisual() != null)
		{
			GetCurrentCardVisual().OnDoneCrafting();
		}
		if (m_currentBigActor != null)
		{
			m_currentBigActor.name = "USED_TO_BE_CurrentBigActor";
			StartCoroutine(MakeSureActorIsCleanedUp(m_currentBigActor));
		}
		m_currentBigActor = null;
		m_craftingUI.gameObject.SetActive(value: false);
	}

	private IEnumerator MakeSureActorIsCleanedUp(Actor oldActor)
	{
		yield return new WaitForSeconds(1f);
		if (!(oldActor == null))
		{
			UnityEngine.Object.DestroyImmediate(oldActor);
		}
	}

	private Actor GetAndPositionNewUpsideDownActor(Actor oldActor, bool fromPage)
	{
		Actor andPositionNewActor = GetAndPositionNewActor(oldActor, 1);
		SceneUtils.SetLayer(andPositionNewActor.gameObject, GameLayer.IgnoreFullScreenEffects);
		if (fromPage)
		{
			andPositionNewActor.transform.position = oldActor.transform.position + new Vector3(0f, -2f, 0f);
			andPositionNewActor.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
			iTween.RotateTo(andPositionNewActor.gameObject, new Vector3(0f, 350f, 180f), 0.4f);
			iTween.MoveTo(andPositionNewActor.gameObject, iTween.Hash("name", "GetAndPositionNewUpsideDownActor", "position", m_faceDownCardBone.position, "time", 0.4f));
			iTween.ScaleTo(andPositionNewActor.gameObject, m_faceDownCardBone.localScale, 0.4f);
		}
		else
		{
			andPositionNewActor.transform.localEulerAngles = new Vector3(0f, 350f, 180f);
			andPositionNewActor.transform.position = m_faceDownCardBone.position + new Vector3(0f, -6f, 0f);
			andPositionNewActor.transform.localScale = m_faceDownCardBone.localScale;
			iTween.MoveTo(andPositionNewActor.gameObject, iTween.Hash("name", "GetAndPositionNewUpsideDownActor", "position", m_faceDownCardBone.position, "time", m_timeForBackCardToMoveUp, "easetype", m_easeTypeForCardMoveUp, "delay", m_delayBeforeBackCardMovesUp));
		}
		return andPositionNewActor;
	}

	private Actor GetAndPositionNewActor(Actor oldActor, int numCopies)
	{
		Actor actor = ((numCopies != 0) ? GetNonGhostActor(oldActor) : GetGhostActor(oldActor));
		if (actor != null)
		{
			actor.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		}
		return actor;
	}

	private Actor GetGhostActor(Actor actor)
	{
		m_isCurrentActorAGhost = true;
		bool flag = actor.GetPremium() == TAG_PREMIUM.GOLDEN;
		bool flag2 = actor.GetPremium() == TAG_PREMIUM.DIAMOND;
		Actor templateActor = m_ghostMinionActor;
		switch (actor.GetEntityDef().GetCardType())
		{
		case TAG_CARDTYPE.SPELL:
			templateActor = ((!flag) ? m_ghostSpellActor : m_ghostGoldenSpellActor);
			break;
		case TAG_CARDTYPE.WEAPON:
			templateActor = ((!flag) ? m_ghostWeaponActor : m_ghostGoldenWeaponActor);
			break;
		case TAG_CARDTYPE.MINION:
			templateActor = ((!flag) ? ((!flag2) ? m_ghostMinionActor : m_ghostDiamondMinionActor) : m_ghostGoldenMinionActor);
			break;
		case TAG_CARDTYPE.HERO:
			templateActor = ((!flag) ? m_ghostHeroActor : m_ghostGoldenHeroActor);
			break;
		case TAG_CARDTYPE.HERO_POWER:
			templateActor = ((!flag) ? m_ghostHeroPowerActor : m_ghostGoldenHeroPowerActor);
			break;
		default:
			Debug.LogError("CraftingManager.GetGhostActor() - tried to get a ghost actor for a cardtype that we haven't anticipated!!");
			break;
		}
		return SetUpGhostActor(templateActor, actor);
	}

	private Actor GetNonGhostActor(Actor actor)
	{
		m_isCurrentActorAGhost = false;
		return SetUpNonGhostActor(GetTemplateActor(actor), actor);
	}

	private Actor GetTemplateActorForType(TAG_CARDTYPE type, TAG_PREMIUM premium)
	{
		bool flag = premium == TAG_PREMIUM.GOLDEN;
		bool flag2 = premium == TAG_PREMIUM.DIAMOND;
		switch (type)
		{
		case TAG_CARDTYPE.SPELL:
			if (flag)
			{
				return m_templateGoldenSpellActor;
			}
			return m_templateSpellActor;
		case TAG_CARDTYPE.WEAPON:
			if (flag)
			{
				return m_templateGoldenWeaponActor;
			}
			return m_templateWeaponActor;
		case TAG_CARDTYPE.MINION:
			if (flag)
			{
				return m_templateGoldenMinionActor;
			}
			if (flag2)
			{
				return m_templateDiamondMinionActor;
			}
			return m_templateMinionActor;
		case TAG_CARDTYPE.HERO:
			if (flag)
			{
				return m_templateGoldenHeroActor;
			}
			return m_templateHeroActor;
		case TAG_CARDTYPE.HERO_POWER:
			if (flag)
			{
				return m_templateGoldenHeroPowerActor;
			}
			return m_templateHeroPowerActor;
		default:
			Debug.LogError("CraftingManager.GetTemplateActorForType() - tried to get a actor for a cardtype that we haven't anticipated!!");
			return m_templateMinionActor;
		}
	}

	private Actor GetTemplateActor(Actor actor)
	{
		return GetTemplateActorForType(actor.GetEntityDef().GetCardType(), actor.GetPremium());
	}

	private Actor SetUpNonGhostActor(Actor templateActor, Actor actor)
	{
		Actor actor2 = UnityEngine.Object.Instantiate(templateActor);
		actor2.SetFullDefFromActor(actor);
		actor2.SetPremium(actor.GetPremium());
		if (CardBackManager.Get().IsFavoriteCardBackRandomAndEnabled())
		{
			actor2.SetCardBackSlotOverride(CardBackManager.CardBackSlot.RANDOM);
		}
		actor2.SetUnlit();
		actor2.UpdateAllComponents();
		return actor2;
	}

	private Actor SetUpGhostActor(Actor templateActor, Actor actor)
	{
		if (templateActor == null || actor == null)
		{
			Debug.LogError("CraftingManager.SetUpGhostActor - passed arguments are null");
			return null;
		}
		Actor actor2 = UnityEngine.Object.Instantiate(templateActor);
		actor2.SetFullDefFromActor(actor);
		actor2.SetPremium(actor.GetPremium());
		if (CardBackManager.Get().IsFavoriteCardBackRandomAndEnabled())
		{
			actor2.SetCardBackSlotOverride(CardBackManager.CardBackSlot.RANDOM);
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
		StartCoroutine(ShowAfterTwoFrames(actor2));
		return actor2;
	}

	private IEnumerator ShowAfterTwoFrames(Actor actorToShow)
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		if (!(actorToShow != m_currentBigActor))
		{
			actorToShow.Show();
		}
	}

	private void SetBigActorLayer(bool inCraftingMode)
	{
		if (!(m_currentBigActor == null))
		{
			GameLayer layer = (inCraftingMode ? GameLayer.IgnoreFullScreenEffects : GameLayer.CardRaycast);
			SceneUtils.SetLayer(m_currentBigActor.gameObject, layer);
		}
	}

	private CollectionCardVisual GetCurrentCardVisual()
	{
		if (!GetShownCardInfo(out var entityDef, out var premium))
		{
			return null;
		}
		return CollectionManager.Get().GetCollectibleDisplay().m_pageManager.GetCardVisual(entityDef.GetCardId(), premium);
	}

	public int GetNumOwnedIncludePending(TAG_PREMIUM premium)
	{
		EntityDef entityDef = m_collectionCardActor.GetEntityDef();
		string cardId = entityDef.GetCardId();
		int numCopiesInCollection = CollectionManager.Get().GetNumCopiesInCollection(cardId, premium);
		if (m_pendingClientTransaction != null && (m_pendingClientTransaction.CardID == cardId || (GameUtils.IsClassicCard(cardId) && m_pendingClientTransaction.CardID == GameUtils.TranslateDbIdToCardId(entityDef.GetTag(GAME_TAG.DECK_RULE_COUNT_AS_COPY_OF_CARD_ID)))) && m_pendingClientTransaction.Premium == premium)
		{
			return numCopiesInCollection + m_pendingClientTransaction.TransactionAmt;
		}
		return numCopiesInCollection;
	}

	public int GetNumOwnedIncludePending()
	{
		TAG_PREMIUM premium = m_collectionCardActor.GetPremium();
		return GetNumOwnedIncludePending(premium);
	}

	private void AddQuestOverlay(EntityDef def, bool isPremium, GameObject parent)
	{
		if (m_questCardRewardOverlay == null)
		{
			Debug.LogWarning("Attempted to add quest overlay to a card, but no prefab was set on CraftinManager");
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(m_questCardRewardOverlay.gameObject, parent.transform);
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
		}
		else
		{
			component.SetEntityType(def, isPremium);
		}
	}

	private void LoadActor(string actorPath, ref Actor actor)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(actorPath, AssetLoadingOptions.IgnorePrefabPosition);
		gameObject.transform.position = new Vector3(-99999f, 99999f, 99999f);
		actor = gameObject.GetComponent<Actor>();
		actor.TurnOffCollider();
	}

	private void LoadActor(string actorPath, ref Actor actor, ref Actor actorCopy)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(actorPath, AssetLoadingOptions.IgnorePrefabPosition);
		gameObject.transform.position = new Vector3(-99999f, 99999f, 99999f);
		actor = gameObject.GetComponent<Actor>();
		actorCopy = UnityEngine.Object.Instantiate(actor);
		actor.TurnOffCollider();
		actorCopy.TurnOffCollider();
	}

	private void ShowLeagueLockedCardPopup()
	{
		if (GetShownCardInfo(out var entityDef, out var _) && RankMgr.Get().IsCardLockedInCurrentLeague(entityDef))
		{
			LeagueDbfRecord localPlayerStandardLeagueConfig = RankMgr.Get().GetLocalPlayerStandardLeagueConfig();
			if (!string.IsNullOrEmpty(localPlayerStandardLeagueConfig.LockedCardPopupTitleText) && !string.IsNullOrEmpty(localPlayerStandardLeagueConfig.LockedCardPopupBodyText))
			{
				AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
				popupInfo.m_headerText = localPlayerStandardLeagueConfig.LockedCardPopupTitleText;
				popupInfo.m_text = localPlayerStandardLeagueConfig.LockedCardPopupBodyText;
				popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
				popupInfo.m_layerToUse = GameLayer.UI;
				popupInfo.m_showAlertIcon = false;
				DialogManager.Get().ShowPopup(popupInfo);
			}
		}
	}

	private void LoadRandomCardBack()
	{
		if (CardBackManager.Get().IsFavoriteCardBackRandomAndEnabled())
		{
			CardBackManager.Get().LoadRandomCardBackOwnedByPlayer();
		}
	}
}
