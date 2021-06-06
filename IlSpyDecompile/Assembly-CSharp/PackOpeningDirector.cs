using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.Progression;
using UnityEngine;

[CustomEditClass]
public class PackOpeningDirector : MonoBehaviour
{
	public delegate void FinishedCallback(object userData);

	private class FinishedListener : EventListener<FinishedCallback>
	{
		public void Fire()
		{
			m_callback(m_userData);
		}
	}

	private readonly Vector3 PACK_OPENING_FX_POSITION = Vector3.zero;

	public PackOpeningCard m_HiddenCard;

	public GameObject m_CardsInsidePack;

	public GameObject m_ClassName;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_DoneButtonPrefab;

	public Carousel m_Carousel;

	private List<PackOpeningCard> m_hiddenCards = new List<PackOpeningCard>();

	private NormalButton m_doneButton;

	private bool m_loadingDoneButton;

	private bool m_playing;

	private Map<int, Spell> m_packFxSpells = new Map<int, Spell>();

	private Spell m_activePackFxSpell;

	private int m_cardsPendingReveal;

	private int m_effectsPendingFinish;

	private int m_effectsPendingDestroy;

	private List<FinishedListener> m_finishedListeners = new List<FinishedListener>();

	private int m_centerCardIndex;

	private bool m_doneButtonShown;

	private PackOpeningCard m_clickedCard;

	private int m_clickedPosition;

	private PackOpeningCard m_glowingCard;

	public static bool QuickPackOpeningAllowed => NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>()?.QuickPackOpeningAllowed ?? false;

	public event Action OnDoneOpeningPack;

	private void Awake()
	{
		InitializeCards();
		InitializeUI();
	}

	private void Update()
	{
		if ((bool)m_Carousel)
		{
			m_Carousel.UpdateUI(UniversalInputManager.Get().GetMouseButtonDown(0));
		}
	}

	public void Play(int boosterId)
	{
		if (!m_playing)
		{
			m_playing = true;
			EnableCardInput(enable: false);
			StartCoroutine(PlayWhenReady(boosterId));
		}
	}

	public bool IsPlaying()
	{
		return m_playing;
	}

	public void OnBoosterOpened(List<NetCache.BoosterCard> cards)
	{
		if (cards.Count > m_hiddenCards.Count)
		{
			Debug.LogError($"PackOpeningDirector.OnBoosterOpened() - Not enough PackOpeningCards! Received {cards.Count} cards. There are only {m_hiddenCards.Count} hidden cards.");
			return;
		}
		int num = (m_cardsPendingReveal = Mathf.Min(cards.Count, m_hiddenCards.Count));
		StartCoroutine(AttachBoosterCards(cards));
	}

	public void AddFinishedListener(FinishedCallback callback)
	{
		AddFinishedListener(callback, null);
	}

	public void AddFinishedListener(FinishedCallback callback, object userData)
	{
		FinishedListener finishedListener = new FinishedListener();
		finishedListener.SetCallback(callback);
		finishedListener.SetUserData(userData);
		m_finishedListeners.Add(finishedListener);
	}

	public void RemoveFinishedListener(FinishedCallback callback)
	{
		RemoveFinishedListener(callback, null);
	}

	public void RemoveFinishedListener(FinishedCallback callback, object userData)
	{
		FinishedListener finishedListener = new FinishedListener();
		finishedListener.SetCallback(callback);
		finishedListener.SetUserData(userData);
		m_finishedListeners.Remove(finishedListener);
	}

	public bool IsDoneButtonShown()
	{
		return m_doneButtonShown;
	}

	public List<PackOpeningCard> GetHiddenCards()
	{
		return m_hiddenCards;
	}

	public void HideCardsAndDoneButton()
	{
		foreach (PackOpeningCard hiddenCard in m_hiddenCards)
		{
			hiddenCard.gameObject.SetActive(value: false);
		}
		if (IsDoneButtonShown())
		{
			HideDoneButton();
		}
	}

	public bool FinishPackOpen()
	{
		if (!m_doneButtonShown)
		{
			return false;
		}
		m_activePackFxSpell.ActivateState(SpellStateType.DEATH);
		Box.Get().GetBoxCamera().GetEventTable()
			.m_BlurSpell.ActivateState(SpellStateType.DEATH);
		m_effectsPendingFinish = 1 + 2 * m_hiddenCards.Count;
		m_effectsPendingDestroy = m_effectsPendingFinish;
		HideDoneButton();
		foreach (PackOpeningCard hiddenCard in m_hiddenCards)
		{
			CardBackDisplay componentInChildren = hiddenCard.GetComponentInChildren<CardBackDisplay>();
			if (componentInChildren != null)
			{
				componentInChildren.gameObject.GetComponent<Renderer>().enabled = false;
			}
			Spell classNameSpell = hiddenCard.m_ClassNameSpell;
			classNameSpell.AddFinishedCallback(OnHiddenCardSpellFinished);
			classNameSpell.AddStateFinishedCallback(OnHiddenCardSpellStateFinished);
			classNameSpell.ActivateState(SpellStateType.DEATH);
			Spell isNewSpell = hiddenCard.m_IsNewSpell;
			if (isNewSpell != null)
			{
				isNewSpell.ActivateState(SpellStateType.DEATH);
			}
			Spell spell = hiddenCard.GetActor().GetSpell(SpellType.DEATH);
			spell.AddFinishedCallback(OnHiddenCardSpellFinished);
			spell.AddStateFinishedCallback(OnHiddenCardSpellStateFinished);
			spell.Activate();
			if (TemporaryAccountManager.IsTemporaryAccount())
			{
				EntityDef entityDef = hiddenCard.GetEntityDef();
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
		HideKeywordTooltips();
		return true;
	}

	public void ForceRevealRandomCard()
	{
		PackOpeningCard[] array = m_hiddenCards.Where(CardCanBeRevealed).ToArray();
		if (array.Length != 0)
		{
			int num = UnityEngine.Random.Range(0, array.Length);
			array[num].ForceReveal();
		}
	}

	private static bool CardCanBeRevealed(PackOpeningCard card)
	{
		if (card.IsReady() && card.IsRevealEnabled())
		{
			return !card.IsRevealed();
		}
		return false;
	}

	private IEnumerator PlayWhenReady(int boosterId)
	{
		while (m_loadingDoneButton)
		{
			yield return null;
		}
		if (m_doneButton == null)
		{
			FireFinishedEvent();
			yield break;
		}
		if (!m_packFxSpells.TryGetValue(boosterId, out var spell))
		{
			BoosterDbfRecord record = GameDbf.Booster.GetRecord(boosterId);
			bool loading = true;
			PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
			{
				loading = false;
				m_packFxSpells[boosterId] = spell;
				if (go == null)
				{
					Error.AddDevFatal("PackOpeningDirector.PlayWhenReady() - Error loading {0} for booster id {1}", assetRef, boosterId);
				}
				else
				{
					spell = go.GetComponent<Spell>();
					go.transform.parent = base.transform;
					go.transform.localPosition = PACK_OPENING_FX_POSITION;
				}
			};
			AssetLoader.Get().InstantiatePrefab(new AssetReference(record.PackOpeningFxPrefab), callback);
			while (loading)
			{
				yield return null;
			}
		}
		if (!spell)
		{
			FireFinishedEvent();
			yield break;
		}
		m_activePackFxSpell = spell;
		PlayMakerFSM component = spell.GetComponent<PlayMakerFSM>();
		if (component != null)
		{
			component.FsmVariables.GetFsmGameObject("CardsInsidePack").Value = m_CardsInsidePack;
			component.FsmVariables.GetFsmGameObject("ClassName").Value = m_ClassName;
			component.FsmVariables.GetFsmGameObject("PackOpeningDirector").Value = base.gameObject;
		}
		m_activePackFxSpell.AddFinishedCallback(OnSpellFinished);
		m_activePackFxSpell.ActivateState(SpellStateType.ACTION);
	}

	private void OnSpellFinished(Spell spell, object userData)
	{
		foreach (PackOpeningCard hiddenCard in m_hiddenCards)
		{
			hiddenCard.EnableInput(enable: true);
			hiddenCard.EnableReveal(enable: true);
		}
		AttachCardsToCarousel();
	}

	private void CameraBlurOn()
	{
		Box.Get().GetBoxCamera().GetEventTable()
			.m_BlurSpell.ActivateState(SpellStateType.BIRTH);
	}

	private void AttachCardsToCarousel()
	{
		if (!m_Carousel)
		{
			return;
		}
		List<PackOpeningCardCarouselItem> list = new List<PackOpeningCardCarouselItem>();
		for (int i = 0; i < m_hiddenCards.Count; i++)
		{
			PackOpeningCard packOpeningCard = m_hiddenCards[i];
			packOpeningCard.GetComponent<Collider>().enabled = true;
			PackOpeningCardCarouselItem item = new PackOpeningCardCarouselItem(packOpeningCard);
			list.Add(item);
			if (QuickPackOpeningAllowed && (bool)UniversalInputManager.UsePhoneUI)
			{
				packOpeningCard.ShowRarityGlow();
			}
		}
		Carousel carousel = m_Carousel;
		CarouselItem[] items = list.ToArray();
		carousel.Initialize(items);
		m_Carousel.SetListeners(null, CarouselItemClicked, CarouselItemReleased, CarouselSettled, CarouselStartedScrolling, CarouselItemCrossedCenterPosition);
		CarouselSettled();
		CarouselItemCrossedCenterPosition(m_Carousel.GetCurrentItem(), 0);
	}

	private void CarouselItemCrossedCenterPosition(CarouselItem item, int index)
	{
		if ((bool)UniversalInputManager.UsePhoneUI && item != null && QuickPackOpeningAllowed)
		{
			PackOpeningCard component = ((PackOpeningCardCarouselItem)item).GetGameObject().GetComponent<PackOpeningCard>();
			if (!component.IsRevealed())
			{
				component.ForceReveal();
			}
		}
	}

	private void CarouselItemClicked(CarouselItem item, int index)
	{
		m_clickedCard = item.GetGameObject().GetComponent<PackOpeningCard>();
		m_clickedPosition = index;
	}

	private void CarouselItemReleased()
	{
		if (m_Carousel.IsScrolling())
		{
			return;
		}
		bool flag = !UniversalInputManager.UsePhoneUI || !QuickPackOpeningAllowed;
		if (m_clickedPosition == m_Carousel.GetCurrentIndex())
		{
			if (m_clickedCard.IsRevealed())
			{
				if (flag && m_clickedPosition < 4)
				{
					m_Carousel.SetPosition(m_clickedPosition + 1, animate: true);
				}
			}
			else
			{
				m_clickedCard.ForceReveal();
			}
		}
		else if (flag)
		{
			m_Carousel.SetPosition(m_clickedPosition, animate: true);
		}
	}

	private void CarouselSettled()
	{
		(m_glowingCard = ((PackOpeningCardCarouselItem)m_Carousel.GetCurrentItem()).GetGameObject().GetComponent<PackOpeningCard>()).ShowRarityGlow();
	}

	private void CarouselStartedScrolling()
	{
		if (m_glowingCard != null && m_glowingCard.GetEntityDef() != null && m_glowingCard.GetEntityDef().GetRarity() != TAG_RARITY.COMMON)
		{
			m_glowingCard.HideRarityGlow();
		}
	}

	private void InitializeUI()
	{
		m_loadingDoneButton = true;
		AssetLoader.Get().InstantiatePrefab(m_DoneButtonPrefab, OnDoneButtonLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	private void OnDoneButtonLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_loadingDoneButton = false;
		if (go == null)
		{
			Debug.LogError($"PackOpeningDirector.OnDoneButtonLoaded() - FAILED to load \"{assetRef}\"");
			return;
		}
		m_doneButton = go.GetComponent<NormalButton>();
		if (m_doneButton == null)
		{
			Debug.LogError($"PackOpeningDirector.OnDoneButtonLoaded() - ERROR \"{assetRef}\" has no {typeof(NormalButton)} component");
			return;
		}
		SceneUtils.SetLayer(m_doneButton.gameObject, GameLayer.IgnoreFullScreenEffects);
		m_doneButton.transform.parent = base.transform;
		TransformUtil.CopyWorld(m_doneButton, PackOpening.Get().m_Bones.m_DoneButton);
		SceneUtils.EnableRenderersAndColliders(m_doneButton.gameObject, enable: false);
	}

	private void ShowDoneButton()
	{
		m_doneButtonShown = true;
		SceneUtils.EnableRenderersAndColliders(m_doneButton.gameObject, enable: true);
		Spell component = m_doneButton.m_button.GetComponent<Spell>();
		component.AddFinishedCallback(OnDoneButtonShown);
		component.ActivateState(SpellStateType.BIRTH);
	}

	private void OnDoneButtonShown(Spell spell, object userData)
	{
		m_doneButton.AddEventListener(UIEventType.RELEASE, OnDoneButtonPressed);
	}

	private void HideDoneButton()
	{
		m_doneButtonShown = false;
		SceneUtils.EnableColliders(m_doneButton.gameObject, enable: false);
		m_doneButton.RemoveEventListener(UIEventType.RELEASE, OnDoneButtonPressed);
		Spell component = m_doneButton.m_button.GetComponent<Spell>();
		component.AddFinishedCallback(OnDoneButtonHidden);
		component.ActivateState(SpellStateType.DEATH);
	}

	private void OnDoneButtonHidden(Spell spell, object userData)
	{
		OnEffectFinished();
		OnEffectDone();
	}

	private void OnDoneButtonPressed(UIEvent e)
	{
		HideKeywordTooltips();
		FinishPackOpen();
	}

	private void HideKeywordTooltips()
	{
		foreach (PackOpeningCard hiddenCard in m_hiddenCards)
		{
			hiddenCard.RemoveOnOverWhileFlippedListeners();
		}
		TooltipPanelManager.Get().HideKeywordHelp();
	}

	private void InitializeCards()
	{
		bool flag = CardBackManager.Get().IsFavoriteCardBackRandomAndEnabled();
		if (flag)
		{
			CardBackManager.Get().LoadRandomCardBackOwnedByPlayer();
		}
		m_hiddenCards.Add(m_HiddenCard);
		for (int i = 1; i < 5; i++)
		{
			PackOpeningCard component = UnityEngine.Object.Instantiate(m_HiddenCard.gameObject).GetComponent<PackOpeningCard>();
			component.transform.parent = m_HiddenCard.transform.parent;
			TransformUtil.CopyLocal(component, m_HiddenCard);
			m_hiddenCards.Add(component);
		}
		for (int j = 0; j < m_hiddenCards.Count; j++)
		{
			PackOpeningCard packOpeningCard = m_hiddenCards[j];
			packOpeningCard.name = $"Card_Hidden{j + 1}";
			packOpeningCard.EnableInput(enable: false);
			packOpeningCard.AddRevealedListener(OnCardRevealed, packOpeningCard);
			if (flag)
			{
				packOpeningCard.UseRandomCardBack();
			}
		}
	}

	private void EnableCardInput(bool enable)
	{
		foreach (PackOpeningCard hiddenCard in m_hiddenCards)
		{
			hiddenCard.EnableInput(enable);
		}
	}

	private void OnCardRevealed(object userData)
	{
		PackOpeningCard packOpeningCard = (PackOpeningCard)userData;
		if (packOpeningCard.GetEntityDef().GetRarity() == TAG_RARITY.LEGENDARY)
		{
			if (packOpeningCard.GetActor().GetPremium() == TAG_PREMIUM.GOLDEN)
			{
				BnetPresenceMgr.Get().SetGameField(4u, packOpeningCard.GetCardId() + ",1");
			}
			else
			{
				BnetPresenceMgr.Get().SetGameField(4u, packOpeningCard.GetCardId() + ",0");
			}
		}
		m_cardsPendingReveal--;
		if (m_cardsPendingReveal <= 0)
		{
			AchievementManager.Get().UnpauseToastNotifications();
			ShowDoneButton();
		}
	}

	private void OnHiddenCardSpellFinished(Spell spell, object userData)
	{
		OnEffectFinished();
	}

	private void OnHiddenCardSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			OnEffectDone();
		}
	}

	private IEnumerator AttachBoosterCards(List<NetCache.BoosterCard> cards)
	{
		int minCardCount = Mathf.Min(cards.Count, m_hiddenCards.Count);
		int i = 0;
		while (i < minCardCount)
		{
			yield return null;
			NetCache.BoosterCard boosterCard = cards[i];
			m_hiddenCards[i].AttachBoosterCard(boosterCard);
			int num = i + 1;
			i = num;
		}
	}

	private void OnEffectFinished()
	{
		m_effectsPendingFinish--;
		if (m_effectsPendingFinish <= 0)
		{
			FireFinishedEvent();
		}
	}

	private void OnEffectDone()
	{
		m_effectsPendingDestroy--;
		if (m_effectsPendingDestroy <= 0)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void FireFinishedEvent()
	{
		FinishedListener[] array = m_finishedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire();
		}
	}
}
