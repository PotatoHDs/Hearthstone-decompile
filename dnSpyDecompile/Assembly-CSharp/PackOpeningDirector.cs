using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.Progression;
using UnityEngine;

// Token: 0x02000616 RID: 1558
[CustomEditClass]
public class PackOpeningDirector : MonoBehaviour
{
	// Token: 0x14000034 RID: 52
	// (add) Token: 0x06005760 RID: 22368 RVA: 0x001C9E64 File Offset: 0x001C8064
	// (remove) Token: 0x06005761 RID: 22369 RVA: 0x001C9E9C File Offset: 0x001C809C
	public event Action OnDoneOpeningPack;

	// Token: 0x06005762 RID: 22370 RVA: 0x001C9ED1 File Offset: 0x001C80D1
	private void Awake()
	{
		this.InitializeCards();
		this.InitializeUI();
	}

	// Token: 0x06005763 RID: 22371 RVA: 0x001C9EDF File Offset: 0x001C80DF
	private void Update()
	{
		if (this.m_Carousel)
		{
			this.m_Carousel.UpdateUI(UniversalInputManager.Get().GetMouseButtonDown(0));
		}
	}

	// Token: 0x06005764 RID: 22372 RVA: 0x001C9F04 File Offset: 0x001C8104
	public void Play(int boosterId)
	{
		if (this.m_playing)
		{
			return;
		}
		this.m_playing = true;
		this.EnableCardInput(false);
		base.StartCoroutine(this.PlayWhenReady(boosterId));
	}

	// Token: 0x06005765 RID: 22373 RVA: 0x001C9F2B File Offset: 0x001C812B
	public bool IsPlaying()
	{
		return this.m_playing;
	}

	// Token: 0x06005766 RID: 22374 RVA: 0x001C9F34 File Offset: 0x001C8134
	public void OnBoosterOpened(List<NetCache.BoosterCard> cards)
	{
		if (cards.Count > this.m_hiddenCards.Count)
		{
			Debug.LogError(string.Format("PackOpeningDirector.OnBoosterOpened() - Not enough PackOpeningCards! Received {0} cards. There are only {1} hidden cards.", cards.Count, this.m_hiddenCards.Count));
			return;
		}
		int cardsPendingReveal = Mathf.Min(cards.Count, this.m_hiddenCards.Count);
		this.m_cardsPendingReveal = cardsPendingReveal;
		base.StartCoroutine(this.AttachBoosterCards(cards));
	}

	// Token: 0x06005767 RID: 22375 RVA: 0x001C9FAB File Offset: 0x001C81AB
	public void AddFinishedListener(PackOpeningDirector.FinishedCallback callback)
	{
		this.AddFinishedListener(callback, null);
	}

	// Token: 0x06005768 RID: 22376 RVA: 0x001C9FB8 File Offset: 0x001C81B8
	public void AddFinishedListener(PackOpeningDirector.FinishedCallback callback, object userData)
	{
		PackOpeningDirector.FinishedListener finishedListener = new PackOpeningDirector.FinishedListener();
		finishedListener.SetCallback(callback);
		finishedListener.SetUserData(userData);
		this.m_finishedListeners.Add(finishedListener);
	}

	// Token: 0x06005769 RID: 22377 RVA: 0x001C9FE5 File Offset: 0x001C81E5
	public void RemoveFinishedListener(PackOpeningDirector.FinishedCallback callback)
	{
		this.RemoveFinishedListener(callback, null);
	}

	// Token: 0x0600576A RID: 22378 RVA: 0x001C9FF0 File Offset: 0x001C81F0
	public void RemoveFinishedListener(PackOpeningDirector.FinishedCallback callback, object userData)
	{
		PackOpeningDirector.FinishedListener finishedListener = new PackOpeningDirector.FinishedListener();
		finishedListener.SetCallback(callback);
		finishedListener.SetUserData(userData);
		this.m_finishedListeners.Remove(finishedListener);
	}

	// Token: 0x0600576B RID: 22379 RVA: 0x001CA01E File Offset: 0x001C821E
	public bool IsDoneButtonShown()
	{
		return this.m_doneButtonShown;
	}

	// Token: 0x0600576C RID: 22380 RVA: 0x001CA026 File Offset: 0x001C8226
	public List<PackOpeningCard> GetHiddenCards()
	{
		return this.m_hiddenCards;
	}

	// Token: 0x0600576D RID: 22381 RVA: 0x001CA030 File Offset: 0x001C8230
	public void HideCardsAndDoneButton()
	{
		foreach (PackOpeningCard packOpeningCard in this.m_hiddenCards)
		{
			packOpeningCard.gameObject.SetActive(false);
		}
		if (this.IsDoneButtonShown())
		{
			this.HideDoneButton();
		}
	}

	// Token: 0x0600576E RID: 22382 RVA: 0x001CA094 File Offset: 0x001C8294
	public bool FinishPackOpen()
	{
		if (!this.m_doneButtonShown)
		{
			return false;
		}
		this.m_activePackFxSpell.ActivateState(SpellStateType.DEATH);
		Box.Get().GetBoxCamera().GetEventTable().m_BlurSpell.ActivateState(SpellStateType.DEATH);
		this.m_effectsPendingFinish = 1 + 2 * this.m_hiddenCards.Count;
		this.m_effectsPendingDestroy = this.m_effectsPendingFinish;
		this.HideDoneButton();
		foreach (PackOpeningCard packOpeningCard in this.m_hiddenCards)
		{
			CardBackDisplay componentInChildren = packOpeningCard.GetComponentInChildren<CardBackDisplay>();
			if (componentInChildren != null)
			{
				componentInChildren.gameObject.GetComponent<Renderer>().enabled = false;
			}
			Spell classNameSpell = packOpeningCard.m_ClassNameSpell;
			classNameSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnHiddenCardSpellFinished));
			classNameSpell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnHiddenCardSpellStateFinished));
			classNameSpell.ActivateState(SpellStateType.DEATH);
			Spell isNewSpell = packOpeningCard.m_IsNewSpell;
			if (isNewSpell != null)
			{
				isNewSpell.ActivateState(SpellStateType.DEATH);
			}
			Spell spell = packOpeningCard.GetActor().GetSpell(SpellType.DEATH);
			spell.AddFinishedCallback(new Spell.FinishedCallback(this.OnHiddenCardSpellFinished));
			spell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnHiddenCardSpellStateFinished));
			spell.Activate();
			if (TemporaryAccountManager.IsTemporaryAccount())
			{
				EntityDef entityDef = packOpeningCard.GetEntityDef();
				if (entityDef != null && (entityDef.GetRarity() == TAG_RARITY.EPIC || entityDef.GetRarity() == TAG_RARITY.LEGENDARY))
				{
					TemporaryAccountManager.Get().ShowEarnCardEventHealUpDialog(TemporaryAccountManager.HealUpReason.OPEN_PACK);
				}
			}
		}
		if (this.OnDoneOpeningPack != null)
		{
			this.OnDoneOpeningPack();
		}
		this.HideKeywordTooltips();
		return true;
	}

	// Token: 0x0600576F RID: 22383 RVA: 0x001CA228 File Offset: 0x001C8428
	public void ForceRevealRandomCard()
	{
		PackOpeningCard[] array = this.m_hiddenCards.Where(new Func<PackOpeningCard, bool>(PackOpeningDirector.CardCanBeRevealed)).ToArray<PackOpeningCard>();
		if (array.Length == 0)
		{
			return;
		}
		int num = UnityEngine.Random.Range(0, array.Length);
		array[num].ForceReveal();
	}

	// Token: 0x06005770 RID: 22384 RVA: 0x001CA269 File Offset: 0x001C8469
	private static bool CardCanBeRevealed(PackOpeningCard card)
	{
		return card.IsReady() && card.IsRevealEnabled() && !card.IsRevealed();
	}

	// Token: 0x17000522 RID: 1314
	// (get) Token: 0x06005771 RID: 22385 RVA: 0x001CA288 File Offset: 0x001C8488
	public static bool QuickPackOpeningAllowed
	{
		get
		{
			NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
			return netObject != null && netObject.QuickPackOpeningAllowed;
		}
	}

	// Token: 0x06005772 RID: 22386 RVA: 0x001CA2AB File Offset: 0x001C84AB
	private IEnumerator PlayWhenReady(int boosterId)
	{
		PackOpeningDirector.<>c__DisplayClass43_0 CS$<>8__locals1 = new PackOpeningDirector.<>c__DisplayClass43_0();
		CS$<>8__locals1.<>4__this = this;
		CS$<>8__locals1.boosterId = boosterId;
		while (this.m_loadingDoneButton)
		{
			yield return null;
		}
		if (this.m_doneButton == null)
		{
			this.FireFinishedEvent();
			yield break;
		}
		if (!this.m_packFxSpells.TryGetValue(CS$<>8__locals1.boosterId, out CS$<>8__locals1.spell))
		{
			PackOpeningDirector.<>c__DisplayClass43_1 CS$<>8__locals2 = new PackOpeningDirector.<>c__DisplayClass43_1();
			CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
			BoosterDbfRecord record = GameDbf.Booster.GetRecord(CS$<>8__locals2.CS$<>8__locals1.boosterId);
			CS$<>8__locals2.loading = true;
			PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
			{
				CS$<>8__locals2.loading = false;
				CS$<>8__locals2.CS$<>8__locals1.<>4__this.m_packFxSpells[CS$<>8__locals2.CS$<>8__locals1.boosterId] = CS$<>8__locals2.CS$<>8__locals1.spell;
				if (go == null)
				{
					Error.AddDevFatal("PackOpeningDirector.PlayWhenReady() - Error loading {0} for booster id {1}", new object[]
					{
						assetRef,
						CS$<>8__locals2.CS$<>8__locals1.boosterId
					});
					return;
				}
				CS$<>8__locals2.CS$<>8__locals1.spell = go.GetComponent<Spell>();
				go.transform.parent = CS$<>8__locals2.CS$<>8__locals1.<>4__this.transform;
				go.transform.localPosition = CS$<>8__locals2.CS$<>8__locals1.<>4__this.PACK_OPENING_FX_POSITION;
			};
			AssetLoader.Get().InstantiatePrefab(new AssetReference(record.PackOpeningFxPrefab), callback, null, AssetLoadingOptions.None);
			while (CS$<>8__locals2.loading)
			{
				yield return null;
			}
			CS$<>8__locals2 = null;
		}
		if (!CS$<>8__locals1.spell)
		{
			this.FireFinishedEvent();
			yield break;
		}
		this.m_activePackFxSpell = CS$<>8__locals1.spell;
		PlayMakerFSM component = CS$<>8__locals1.spell.GetComponent<PlayMakerFSM>();
		if (component != null)
		{
			component.FsmVariables.GetFsmGameObject("CardsInsidePack").Value = this.m_CardsInsidePack;
			component.FsmVariables.GetFsmGameObject("ClassName").Value = this.m_ClassName;
			component.FsmVariables.GetFsmGameObject("PackOpeningDirector").Value = base.gameObject;
		}
		this.m_activePackFxSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnSpellFinished));
		this.m_activePackFxSpell.ActivateState(SpellStateType.ACTION);
		yield break;
	}

	// Token: 0x06005773 RID: 22387 RVA: 0x001CA2C4 File Offset: 0x001C84C4
	private void OnSpellFinished(Spell spell, object userData)
	{
		foreach (PackOpeningCard packOpeningCard in this.m_hiddenCards)
		{
			packOpeningCard.EnableInput(true);
			packOpeningCard.EnableReveal(true);
		}
		this.AttachCardsToCarousel();
	}

	// Token: 0x06005774 RID: 22388 RVA: 0x001CA324 File Offset: 0x001C8524
	private void CameraBlurOn()
	{
		Box.Get().GetBoxCamera().GetEventTable().m_BlurSpell.ActivateState(SpellStateType.BIRTH);
	}

	// Token: 0x06005775 RID: 22389 RVA: 0x001CA340 File Offset: 0x001C8540
	private void AttachCardsToCarousel()
	{
		if (!this.m_Carousel)
		{
			return;
		}
		List<PackOpeningCardCarouselItem> list = new List<PackOpeningCardCarouselItem>();
		for (int i = 0; i < this.m_hiddenCards.Count; i++)
		{
			PackOpeningCard packOpeningCard = this.m_hiddenCards[i];
			packOpeningCard.GetComponent<Collider>().enabled = true;
			PackOpeningCardCarouselItem item = new PackOpeningCardCarouselItem(packOpeningCard);
			list.Add(item);
			if (PackOpeningDirector.QuickPackOpeningAllowed && UniversalInputManager.UsePhoneUI)
			{
				packOpeningCard.ShowRarityGlow();
			}
		}
		Carousel carousel = this.m_Carousel;
		CarouselItem[] items = list.ToArray();
		carousel.Initialize(items, 0);
		this.m_Carousel.SetListeners(null, new Carousel.ItemClicked(this.CarouselItemClicked), new Carousel.ItemReleased(this.CarouselItemReleased), new Carousel.CarouselSettled(this.CarouselSettled), new Carousel.CarouselStartedScrolling(this.CarouselStartedScrolling), new Carousel.ItemPulled(this.CarouselItemCrossedCenterPosition));
		this.CarouselSettled();
		this.CarouselItemCrossedCenterPosition(this.m_Carousel.GetCurrentItem(), 0);
	}

	// Token: 0x06005776 RID: 22390 RVA: 0x001CA430 File Offset: 0x001C8630
	private void CarouselItemCrossedCenterPosition(CarouselItem item, int index)
	{
		if (!UniversalInputManager.UsePhoneUI || item == null || !PackOpeningDirector.QuickPackOpeningAllowed)
		{
			return;
		}
		PackOpeningCard component = ((PackOpeningCardCarouselItem)item).GetGameObject().GetComponent<PackOpeningCard>();
		if (!component.IsRevealed())
		{
			component.ForceReveal();
		}
	}

	// Token: 0x06005777 RID: 22391 RVA: 0x001CA473 File Offset: 0x001C8673
	private void CarouselItemClicked(CarouselItem item, int index)
	{
		this.m_clickedCard = item.GetGameObject().GetComponent<PackOpeningCard>();
		this.m_clickedPosition = index;
	}

	// Token: 0x06005778 RID: 22392 RVA: 0x001CA490 File Offset: 0x001C8690
	private void CarouselItemReleased()
	{
		if (!this.m_Carousel.IsScrolling())
		{
			bool flag = !UniversalInputManager.UsePhoneUI || !PackOpeningDirector.QuickPackOpeningAllowed;
			if (this.m_clickedPosition == this.m_Carousel.GetCurrentIndex())
			{
				if (!this.m_clickedCard.IsRevealed())
				{
					this.m_clickedCard.ForceReveal();
					return;
				}
				if (flag && this.m_clickedPosition < 4)
				{
					this.m_Carousel.SetPosition(this.m_clickedPosition + 1, true);
					return;
				}
			}
			else if (flag)
			{
				this.m_Carousel.SetPosition(this.m_clickedPosition, true);
			}
		}
	}

	// Token: 0x06005779 RID: 22393 RVA: 0x001CA524 File Offset: 0x001C8724
	private void CarouselSettled()
	{
		PackOpeningCard component = ((PackOpeningCardCarouselItem)this.m_Carousel.GetCurrentItem()).GetGameObject().GetComponent<PackOpeningCard>();
		this.m_glowingCard = component;
		component.ShowRarityGlow();
	}

	// Token: 0x0600577A RID: 22394 RVA: 0x001CA559 File Offset: 0x001C8759
	private void CarouselStartedScrolling()
	{
		if (this.m_glowingCard != null && this.m_glowingCard.GetEntityDef() != null && this.m_glowingCard.GetEntityDef().GetRarity() != TAG_RARITY.COMMON)
		{
			this.m_glowingCard.HideRarityGlow();
		}
	}

	// Token: 0x0600577B RID: 22395 RVA: 0x001CA594 File Offset: 0x001C8794
	private void InitializeUI()
	{
		this.m_loadingDoneButton = true;
		AssetLoader.Get().InstantiatePrefab(this.m_DoneButtonPrefab, new PrefabCallback<GameObject>(this.OnDoneButtonLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x0600577C RID: 22396 RVA: 0x001CA5C4 File Offset: 0x001C87C4
	private void OnDoneButtonLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_loadingDoneButton = false;
		if (go == null)
		{
			Debug.LogError(string.Format("PackOpeningDirector.OnDoneButtonLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		this.m_doneButton = go.GetComponent<NormalButton>();
		if (this.m_doneButton == null)
		{
			Debug.LogError(string.Format("PackOpeningDirector.OnDoneButtonLoaded() - ERROR \"{0}\" has no {1} component", assetRef, typeof(NormalButton)));
			return;
		}
		SceneUtils.SetLayer(this.m_doneButton.gameObject, GameLayer.IgnoreFullScreenEffects);
		this.m_doneButton.transform.parent = base.transform;
		TransformUtil.CopyWorld(this.m_doneButton, PackOpening.Get().m_Bones.m_DoneButton);
		SceneUtils.EnableRenderersAndColliders(this.m_doneButton.gameObject, false);
	}

	// Token: 0x0600577D RID: 22397 RVA: 0x001CA67C File Offset: 0x001C887C
	private void ShowDoneButton()
	{
		this.m_doneButtonShown = true;
		SceneUtils.EnableRenderersAndColliders(this.m_doneButton.gameObject, true);
		Spell component = this.m_doneButton.m_button.GetComponent<Spell>();
		component.AddFinishedCallback(new Spell.FinishedCallback(this.OnDoneButtonShown));
		component.ActivateState(SpellStateType.BIRTH);
	}

	// Token: 0x0600577E RID: 22398 RVA: 0x001CA6C9 File Offset: 0x001C88C9
	private void OnDoneButtonShown(Spell spell, object userData)
	{
		this.m_doneButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnDoneButtonPressed));
	}

	// Token: 0x0600577F RID: 22399 RVA: 0x001CA6E4 File Offset: 0x001C88E4
	private void HideDoneButton()
	{
		this.m_doneButtonShown = false;
		SceneUtils.EnableColliders(this.m_doneButton.gameObject, false);
		this.m_doneButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnDoneButtonPressed));
		Spell component = this.m_doneButton.m_button.GetComponent<Spell>();
		component.AddFinishedCallback(new Spell.FinishedCallback(this.OnDoneButtonHidden));
		component.ActivateState(SpellStateType.DEATH);
	}

	// Token: 0x06005780 RID: 22400 RVA: 0x001CA74A File Offset: 0x001C894A
	private void OnDoneButtonHidden(Spell spell, object userData)
	{
		this.OnEffectFinished();
		this.OnEffectDone();
	}

	// Token: 0x06005781 RID: 22401 RVA: 0x001CA758 File Offset: 0x001C8958
	private void OnDoneButtonPressed(UIEvent e)
	{
		this.HideKeywordTooltips();
		this.FinishPackOpen();
	}

	// Token: 0x06005782 RID: 22402 RVA: 0x001CA768 File Offset: 0x001C8968
	private void HideKeywordTooltips()
	{
		foreach (PackOpeningCard packOpeningCard in this.m_hiddenCards)
		{
			packOpeningCard.RemoveOnOverWhileFlippedListeners();
		}
		TooltipPanelManager.Get().HideKeywordHelp();
	}

	// Token: 0x06005783 RID: 22403 RVA: 0x001CA7C4 File Offset: 0x001C89C4
	private void InitializeCards()
	{
		bool flag = CardBackManager.Get().IsFavoriteCardBackRandomAndEnabled();
		if (flag)
		{
			CardBackManager.Get().LoadRandomCardBackOwnedByPlayer();
		}
		this.m_hiddenCards.Add(this.m_HiddenCard);
		for (int i = 1; i < 5; i++)
		{
			PackOpeningCard component = UnityEngine.Object.Instantiate<GameObject>(this.m_HiddenCard.gameObject).GetComponent<PackOpeningCard>();
			component.transform.parent = this.m_HiddenCard.transform.parent;
			TransformUtil.CopyLocal(component, this.m_HiddenCard);
			this.m_hiddenCards.Add(component);
		}
		for (int j = 0; j < this.m_hiddenCards.Count; j++)
		{
			PackOpeningCard packOpeningCard = this.m_hiddenCards[j];
			packOpeningCard.name = string.Format("Card_Hidden{0}", j + 1);
			packOpeningCard.EnableInput(false);
			packOpeningCard.AddRevealedListener(new PackOpeningCard.RevealedCallback(this.OnCardRevealed), packOpeningCard);
			if (flag)
			{
				packOpeningCard.UseRandomCardBack();
			}
		}
	}

	// Token: 0x06005784 RID: 22404 RVA: 0x001CA8B4 File Offset: 0x001C8AB4
	private void EnableCardInput(bool enable)
	{
		foreach (PackOpeningCard packOpeningCard in this.m_hiddenCards)
		{
			packOpeningCard.EnableInput(enable);
		}
	}

	// Token: 0x06005785 RID: 22405 RVA: 0x001CA908 File Offset: 0x001C8B08
	private void OnCardRevealed(object userData)
	{
		PackOpeningCard packOpeningCard = (PackOpeningCard)userData;
		if (packOpeningCard.GetEntityDef().GetRarity() == TAG_RARITY.LEGENDARY)
		{
			if (packOpeningCard.GetActor().GetPremium() == TAG_PREMIUM.GOLDEN)
			{
				BnetPresenceMgr.Get().SetGameField(4U, packOpeningCard.GetCardId() + ",1");
			}
			else
			{
				BnetPresenceMgr.Get().SetGameField(4U, packOpeningCard.GetCardId() + ",0");
			}
		}
		this.m_cardsPendingReveal--;
		if (this.m_cardsPendingReveal > 0)
		{
			return;
		}
		AchievementManager.Get().UnpauseToastNotifications();
		this.ShowDoneButton();
	}

	// Token: 0x06005786 RID: 22406 RVA: 0x001CA99A File Offset: 0x001C8B9A
	private void OnHiddenCardSpellFinished(Spell spell, object userData)
	{
		this.OnEffectFinished();
	}

	// Token: 0x06005787 RID: 22407 RVA: 0x001CA9A2 File Offset: 0x001C8BA2
	private void OnHiddenCardSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		this.OnEffectDone();
	}

	// Token: 0x06005788 RID: 22408 RVA: 0x001CA9B3 File Offset: 0x001C8BB3
	private IEnumerator AttachBoosterCards(List<NetCache.BoosterCard> cards)
	{
		int minCardCount = Mathf.Min(cards.Count, this.m_hiddenCards.Count);
		int num;
		for (int i = 0; i < minCardCount; i = num)
		{
			yield return null;
			NetCache.BoosterCard boosterCard = cards[i];
			this.m_hiddenCards[i].AttachBoosterCard(boosterCard);
			num = i + 1;
		}
		yield break;
	}

	// Token: 0x06005789 RID: 22409 RVA: 0x001CA9C9 File Offset: 0x001C8BC9
	private void OnEffectFinished()
	{
		this.m_effectsPendingFinish--;
		if (this.m_effectsPendingFinish > 0)
		{
			return;
		}
		this.FireFinishedEvent();
	}

	// Token: 0x0600578A RID: 22410 RVA: 0x001CA9E9 File Offset: 0x001C8BE9
	private void OnEffectDone()
	{
		this.m_effectsPendingDestroy--;
		if (this.m_effectsPendingDestroy > 0)
		{
			return;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600578B RID: 22411 RVA: 0x001CAA10 File Offset: 0x001C8C10
	private void FireFinishedEvent()
	{
		PackOpeningDirector.FinishedListener[] array = this.m_finishedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire();
		}
	}

	// Token: 0x04004B0E RID: 19214
	private readonly Vector3 PACK_OPENING_FX_POSITION = Vector3.zero;

	// Token: 0x04004B0F RID: 19215
	public PackOpeningCard m_HiddenCard;

	// Token: 0x04004B10 RID: 19216
	public GameObject m_CardsInsidePack;

	// Token: 0x04004B11 RID: 19217
	public GameObject m_ClassName;

	// Token: 0x04004B12 RID: 19218
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_DoneButtonPrefab;

	// Token: 0x04004B13 RID: 19219
	public Carousel m_Carousel;

	// Token: 0x04004B15 RID: 19221
	private List<PackOpeningCard> m_hiddenCards = new List<PackOpeningCard>();

	// Token: 0x04004B16 RID: 19222
	private NormalButton m_doneButton;

	// Token: 0x04004B17 RID: 19223
	private bool m_loadingDoneButton;

	// Token: 0x04004B18 RID: 19224
	private bool m_playing;

	// Token: 0x04004B19 RID: 19225
	private Map<int, Spell> m_packFxSpells = new Map<int, Spell>();

	// Token: 0x04004B1A RID: 19226
	private Spell m_activePackFxSpell;

	// Token: 0x04004B1B RID: 19227
	private int m_cardsPendingReveal;

	// Token: 0x04004B1C RID: 19228
	private int m_effectsPendingFinish;

	// Token: 0x04004B1D RID: 19229
	private int m_effectsPendingDestroy;

	// Token: 0x04004B1E RID: 19230
	private List<PackOpeningDirector.FinishedListener> m_finishedListeners = new List<PackOpeningDirector.FinishedListener>();

	// Token: 0x04004B1F RID: 19231
	private int m_centerCardIndex;

	// Token: 0x04004B20 RID: 19232
	private bool m_doneButtonShown;

	// Token: 0x04004B21 RID: 19233
	private PackOpeningCard m_clickedCard;

	// Token: 0x04004B22 RID: 19234
	private int m_clickedPosition;

	// Token: 0x04004B23 RID: 19235
	private PackOpeningCard m_glowingCard;

	// Token: 0x0200211A RID: 8474
	// (Invoke) Token: 0x06012248 RID: 74312
	public delegate void FinishedCallback(object userData);

	// Token: 0x0200211B RID: 8475
	private class FinishedListener : EventListener<PackOpeningDirector.FinishedCallback>
	{
		// Token: 0x0601224B RID: 74315 RVA: 0x004FEC52 File Offset: 0x004FCE52
		public void Fire()
		{
			this.m_callback(this.m_userData);
		}
	}
}
