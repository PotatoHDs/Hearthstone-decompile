using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

// Token: 0x02000107 RID: 263
public class CollectionDeckInfo : MonoBehaviour
{
	// Token: 0x06000FA8 RID: 4008 RVA: 0x00057898 File Offset: 0x00055A98
	private void Awake()
	{
		this.m_manaCurveTooltipText.Text = GameStrings.Get("GLUE_COLLECTION_DECK_INFO_MANA_TOOLTIP");
		foreach (DeckInfoManaBar deckInfoManaBar in this.m_manaBars)
		{
			deckInfoManaBar.m_costText.Text = this.GetTextForManaCost(deckInfoManaBar.m_manaCostID);
		}
		AssetLoader.Get().InstantiatePrefab("Card_Play_HeroPower.prefab:a3794839abb947146903a26be13e09af", new PrefabCallback<GameObject>(this.OnHeroPowerActorLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
		AssetLoader.Get().InstantiatePrefab(ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.PLAY_HERO_POWER, TAG_PREMIUM.GOLDEN), new PrefabCallback<GameObject>(this.OnGoldenHeroPowerActorLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
		this.m_wasTouchModeEnabled = true;
	}

	// Token: 0x06000FA9 RID: 4009 RVA: 0x00057960 File Offset: 0x00055B60
	private void Start()
	{
		this.m_offClicker.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClosePressed));
		this.m_offClicker.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OverOffClicker));
	}

	// Token: 0x06000FAA RID: 4010 RVA: 0x00057994 File Offset: 0x00055B94
	private void OnDestroy()
	{
		DefLoader.DisposableCardDef heroCardDef = this.m_heroCardDef;
		if (heroCardDef != null)
		{
			heroCardDef.Dispose();
		}
		this.m_heroCardDef = null;
	}

	// Token: 0x06000FAB RID: 4011 RVA: 0x000579B0 File Offset: 0x00055BB0
	private void Update()
	{
		if (this.m_wasTouchModeEnabled != UniversalInputManager.Get().IsTouchMode())
		{
			this.m_wasTouchModeEnabled = UniversalInputManager.Get().IsTouchMode();
			if (UniversalInputManager.Get().IsTouchMode())
			{
				if (this.m_heroPowerActor != null)
				{
					this.m_heroPowerActor.TurnOffCollider();
				}
				if (this.m_goldenHeroPowerActor != null)
				{
					this.m_goldenHeroPowerActor.TurnOffCollider();
				}
				this.m_offClicker.gameObject.SetActive(true);
				return;
			}
			if (this.m_heroPowerActor != null)
			{
				this.m_heroPowerActor.TurnOnCollider();
			}
			if (this.m_goldenHeroPowerActor != null)
			{
				this.m_goldenHeroPowerActor.TurnOnCollider();
			}
			this.m_offClicker.gameObject.SetActive(true);
		}
	}

	// Token: 0x06000FAC RID: 4012 RVA: 0x00057A78 File Offset: 0x00055C78
	public void Show()
	{
		if (this.m_shown)
		{
			return;
		}
		if (CollectionDeckTray.Get() == null)
		{
			this.m_visualRoot.SetActive(true);
		}
		else
		{
			CollectionDeck editingDeck = CollectionDeckTray.Get().GetCardsContent().GetEditingDeck();
			this.m_visualRoot.SetActive(!editingDeck.HasUIHeroOverride());
		}
		this.m_root.SetActive(true);
		this.m_shown = true;
		if (UniversalInputManager.Get().IsTouchMode())
		{
			Navigation.Push(new Navigation.NavigateBackHandler(this.GoBackImpl));
		}
		CollectionDeckInfo.ShowListener[] array = this.m_showListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	// Token: 0x06000FAD RID: 4013 RVA: 0x00057B1F File Offset: 0x00055D1F
	private bool GoBackImpl()
	{
		this.Hide();
		return true;
	}

	// Token: 0x06000FAE RID: 4014 RVA: 0x00057B28 File Offset: 0x00055D28
	public void Hide()
	{
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(this.GoBackImpl));
		if (!this.m_shown)
		{
			return;
		}
		this.m_root.SetActive(false);
		this.m_shown = false;
		CollectionDeckInfo.HideListener[] array = this.m_hideListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	// Token: 0x06000FAF RID: 4015 RVA: 0x00057B85 File Offset: 0x00055D85
	public void RegisterShowListener(CollectionDeckInfo.ShowListener dlg)
	{
		this.m_showListeners.Add(dlg);
	}

	// Token: 0x06000FB0 RID: 4016 RVA: 0x00057B93 File Offset: 0x00055D93
	public void UnregisterShowListener(CollectionDeckInfo.ShowListener dlg)
	{
		this.m_showListeners.Remove(dlg);
	}

	// Token: 0x06000FB1 RID: 4017 RVA: 0x00057BA2 File Offset: 0x00055DA2
	public void RegisterHideListener(CollectionDeckInfo.HideListener dlg)
	{
		this.m_hideListeners.Add(dlg);
	}

	// Token: 0x06000FB2 RID: 4018 RVA: 0x00057BB0 File Offset: 0x00055DB0
	public void UnregisterHideListener(CollectionDeckInfo.HideListener dlg)
	{
		this.m_hideListeners.Remove(dlg);
	}

	// Token: 0x06000FB3 RID: 4019 RVA: 0x00057BBF File Offset: 0x00055DBF
	public bool IsShown()
	{
		return this.m_shown;
	}

	// Token: 0x06000FB4 RID: 4020 RVA: 0x00057BC8 File Offset: 0x00055DC8
	public void UpdateManaCurve()
	{
		CollectionDeck editingDeck = CollectionDeckTray.Get().GetCardsContent().GetEditingDeck();
		this.UpdateManaCurve(editingDeck);
	}

	// Token: 0x06000FB5 RID: 4021 RVA: 0x00057BEC File Offset: 0x00055DEC
	public void UpdateManaCurve(CollectionDeck deck)
	{
		if (deck == null)
		{
			Debug.LogWarning(string.Format("CollectionDeckInfo.UpdateManaCurve(): deck is null.", Array.Empty<object>()));
			return;
		}
		string heroCardID = deck.HeroCardID;
		CardPortraitQuality quality = new CardPortraitQuality(3, TAG_PREMIUM.NORMAL);
		DefLoader.Get().LoadCardDef(heroCardID, new DefLoader.LoadDefCallback<DefLoader.DisposableCardDef>(this.OnHeroCardDefLoaded), null, quality);
		foreach (DeckInfoManaBar deckInfoManaBar in this.m_manaBars)
		{
			deckInfoManaBar.m_numCards = 0;
		}
		int num = 0;
		foreach (CollectionDeckSlot collectionDeckSlot in deck.GetSlots())
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(collectionDeckSlot.CardID);
			int manaCost = entityDef.GetCost();
			if (manaCost > 7)
			{
				manaCost = 7;
			}
			DeckInfoManaBar deckInfoManaBar2 = this.m_manaBars.Find((DeckInfoManaBar obj) => obj.m_manaCostID == manaCost);
			if (deckInfoManaBar2 == null)
			{
				Debug.LogWarning(string.Format("CollectionDeckInfo.UpdateManaCurve(): Cannot update curve. Could not find mana bar for {0} (cost {1})", entityDef, manaCost));
				return;
			}
			deckInfoManaBar2.m_numCards += collectionDeckSlot.Count;
			if (deckInfoManaBar2.m_numCards > num)
			{
				num = deckInfoManaBar2.m_numCards;
			}
		}
		foreach (DeckInfoManaBar deckInfoManaBar3 in this.m_manaBars)
		{
			deckInfoManaBar3.m_numCardsText.Text = Convert.ToString(deckInfoManaBar3.m_numCards);
			float num2 = (num == 0) ? 0f : ((float)deckInfoManaBar3.m_numCards / (float)num);
			Vector3 localPosition = deckInfoManaBar3.m_numCardsText.transform.localPosition;
			localPosition.z = Mathf.Lerp(this.MANA_COST_TEXT_MIN_LOCAL_Z, this.MANA_COST_TEXT_MAX_LOCAL_Z, num2);
			deckInfoManaBar3.m_numCardsText.transform.localPosition = localPosition;
			deckInfoManaBar3.m_barFill.GetComponent<Renderer>().GetMaterial().SetFloat("_Percent", num2);
		}
	}

	// Token: 0x06000FB6 RID: 4022 RVA: 0x00057E34 File Offset: 0x00056034
	public void SetDeck(CollectionDeck deck)
	{
		if (deck == null)
		{
			Debug.LogWarning(string.Format("CollectionDeckInfo.SetDeckID(): deck is null", Array.Empty<object>()));
			return;
		}
		this.UpdateManaCurve(deck);
		if (!string.IsNullOrEmpty(deck.HeroPowerCardID))
		{
			this.m_heroPowerID = deck.HeroPowerCardID;
		}
		else
		{
			string heroPowerCardIdFromHero = GameUtils.GetHeroPowerCardIdFromHero(deck.HeroCardID);
			if (string.IsNullOrEmpty(heroPowerCardIdFromHero))
			{
				Debug.LogWarning("CollectionDeckInfo.UpdateInfo(): invalid hero power ID");
				this.m_heroPowerID = "";
				return;
			}
			if (heroPowerCardIdFromHero.Equals(this.m_heroPowerID))
			{
				return;
			}
			this.m_heroPowerID = heroPowerCardIdFromHero;
		}
		string heroCardId = CollectionManager.GetHeroCardId(deck.GetClass(), CardHero.HeroType.VANILLA);
		TAG_PREMIUM tag_PREMIUM = CollectionManager.Get().GetBestCardPremium(heroCardId);
		if (SceneMgr.Get().IsInDuelsMode())
		{
			tag_PREMIUM = TAG_PREMIUM.NORMAL;
		}
		DefLoader.Get().LoadFullDef(this.m_heroPowerID, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnHeroPowerFullDefLoaded), tag_PREMIUM, null);
	}

	// Token: 0x06000FB7 RID: 4023 RVA: 0x00057F08 File Offset: 0x00056108
	private string GetTextForManaCost(int manaCostID)
	{
		if (manaCostID < 0 || manaCostID > 7)
		{
			Debug.LogWarning(string.Format("CollectionDeckInfo.GetTextForManaCost(): don't know how to handle mana cost ID {0}", manaCostID));
			return "";
		}
		string text = Convert.ToString(manaCostID);
		if (manaCostID == 7)
		{
			text += GameStrings.Get("GLUE_COLLECTION_PLUS");
		}
		return text;
	}

	// Token: 0x06000FB8 RID: 4024 RVA: 0x00057F58 File Offset: 0x00056158
	private void OnHeroPowerActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning(string.Format("CollectionDeckInfo.OnHeroPowerActorLoaded() - FAILED to load actor \"{0}\"", assetRef));
			return;
		}
		this.m_heroPowerActor = go.GetComponent<Actor>();
		if (this.m_heroPowerActor == null)
		{
			Debug.LogWarning(string.Format("CollectionDeckInfo.OnHeroPowerActorLoaded() - ERROR actor \"{0}\" has no Actor component", assetRef));
			return;
		}
		this.m_heroPowerActor.SetUnlit();
		this.m_heroPowerActor.transform.parent = this.m_heroPowerParent.transform;
		this.m_heroPowerActor.transform.localScale = Vector3.one;
		this.m_heroPowerActor.transform.localPosition = Vector3.zero;
		if (UniversalInputManager.Get().IsTouchMode())
		{
			this.m_heroPowerActor.TurnOffCollider();
		}
	}

	// Token: 0x06000FB9 RID: 4025 RVA: 0x00058014 File Offset: 0x00056214
	private void OnGoldenHeroPowerActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning(string.Format("CollectionDeckInfo.OnHeroPowerActorLoaded() - FAILED to load actor \"{0}\"", assetRef));
			return;
		}
		this.m_goldenHeroPowerActor = go.GetComponent<Actor>();
		if (this.m_goldenHeroPowerActor == null)
		{
			Debug.LogWarning(string.Format("CollectionDeckInfo.OnGoldenHeroPowerActorLoaded() - ERROR actor \"{0}\" has no Actor component", assetRef));
			return;
		}
		this.m_goldenHeroPowerActor.SetUnlit();
		this.m_goldenHeroPowerActor.transform.parent = this.m_heroPowerParent.transform;
		this.m_goldenHeroPowerActor.transform.localScale = Vector3.one;
		this.m_goldenHeroPowerActor.transform.localPosition = Vector3.zero;
		if (UniversalInputManager.Get().IsTouchMode())
		{
			this.m_goldenHeroPowerActor.TurnOffCollider();
		}
	}

	// Token: 0x06000FBA RID: 4026 RVA: 0x000580D0 File Offset: 0x000562D0
	private void OnHeroPowerFullDefLoaded(string cardID, DefLoader.DisposableFullDef def, object userData)
	{
		TAG_PREMIUM premium = (TAG_PREMIUM)userData;
		base.StartCoroutine(this.SetHeroPowerInfoWhenReady(cardID, def, premium));
	}

	// Token: 0x06000FBB RID: 4027 RVA: 0x000580F4 File Offset: 0x000562F4
	private IEnumerator SetHeroPowerInfoWhenReady(string heroPowerCardID, DefLoader.DisposableFullDef def, TAG_PREMIUM premium)
	{
		using (def)
		{
			while (this.m_goldenHeroPowerActor == null)
			{
				yield return null;
			}
			while (this.m_heroPowerActor == null)
			{
				yield return null;
			}
			this.SetHeroPowerInfo(heroPowerCardID, def, premium);
		}
		DefLoader.DisposableFullDef disposableFullDef = null;
		yield break;
		yield break;
	}

	// Token: 0x06000FBC RID: 4028 RVA: 0x00058118 File Offset: 0x00056318
	private void SetHeroPowerInfo(string heroPowerCardID, DefLoader.DisposableFullDef def, TAG_PREMIUM premium)
	{
		if (!heroPowerCardID.Equals(this.m_heroPowerID))
		{
			return;
		}
		if (premium == TAG_PREMIUM.GOLDEN)
		{
			if (this.m_heroPowerActor != null)
			{
				this.m_heroPowerActor.Hide();
			}
			this.m_goldenHeroPowerActor.Show();
			this.m_goldenHeroPowerActor.SetFullDef(def);
			this.m_goldenHeroPowerActor.SetUnlit();
			this.m_goldenHeroPowerActor.SetPremium(premium);
			this.m_goldenHeroPowerActor.UpdateAllComponents();
		}
		else
		{
			if (this.m_goldenHeroPowerActor != null)
			{
				this.m_goldenHeroPowerActor.Hide();
			}
			this.m_heroPowerActor.Show();
			this.m_heroPowerActor.SetFullDef(def);
			this.m_heroPowerActor.SetUnlit();
			this.m_heroPowerActor.UpdateAllComponents();
		}
		string name = def.EntityDef.GetName();
		this.m_heroPowerName.Text = name;
		string cardTextInHand = def.EntityDef.GetCardTextInHand();
		this.m_heroPowerDescription.Text = cardTextInHand;
	}

	// Token: 0x06000FBD RID: 4029 RVA: 0x00058202 File Offset: 0x00056402
	private void OnHeroCardDefLoaded(string cardId, DefLoader.DisposableCardDef def, object userData)
	{
		DefLoader.DisposableCardDef heroCardDef = this.m_heroCardDef;
		if (heroCardDef != null)
		{
			heroCardDef.Dispose();
		}
		this.m_heroCardDef = def;
	}

	// Token: 0x06000FBE RID: 4030 RVA: 0x0005821C File Offset: 0x0005641C
	private void OnClosePressed(UIEvent e)
	{
		this.Hide();
	}

	// Token: 0x06000FBF RID: 4031 RVA: 0x0005821C File Offset: 0x0005641C
	private void OverOffClicker(UIEvent e)
	{
		this.Hide();
	}

	// Token: 0x04000A93 RID: 2707
	public GameObject m_root;

	// Token: 0x04000A94 RID: 2708
	public GameObject m_visualRoot;

	// Token: 0x04000A95 RID: 2709
	public GameObject m_heroPowerParent;

	// Token: 0x04000A96 RID: 2710
	public UberText m_heroPowerName;

	// Token: 0x04000A97 RID: 2711
	public UberText m_heroPowerDescription;

	// Token: 0x04000A98 RID: 2712
	public UberText m_manaCurveTooltipText;

	// Token: 0x04000A99 RID: 2713
	public PegUIElement m_offClicker;

	// Token: 0x04000A9A RID: 2714
	public List<DeckInfoManaBar> m_manaBars;

	// Token: 0x04000A9B RID: 2715
	private readonly float MANA_COST_TEXT_MIN_LOCAL_Z;

	// Token: 0x04000A9C RID: 2716
	private readonly float MANA_COST_TEXT_MAX_LOCAL_Z = 5.167298f;

	// Token: 0x04000A9D RID: 2717
	private Actor m_heroPowerActor;

	// Token: 0x04000A9E RID: 2718
	private Actor m_goldenHeroPowerActor;

	// Token: 0x04000A9F RID: 2719
	private DefLoader.DisposableCardDef m_heroCardDef;

	// Token: 0x04000AA0 RID: 2720
	private bool m_wasTouchModeEnabled;

	// Token: 0x04000AA1 RID: 2721
	protected bool m_shown = true;

	// Token: 0x04000AA2 RID: 2722
	private string m_heroPowerID = "";

	// Token: 0x04000AA3 RID: 2723
	private List<CollectionDeckInfo.ShowListener> m_showListeners = new List<CollectionDeckInfo.ShowListener>();

	// Token: 0x04000AA4 RID: 2724
	private List<CollectionDeckInfo.HideListener> m_hideListeners = new List<CollectionDeckInfo.HideListener>();

	// Token: 0x0200142C RID: 5164
	// (Invoke) Token: 0x0600D9E8 RID: 55784
	public delegate void ShowListener();

	// Token: 0x0200142D RID: 5165
	// (Invoke) Token: 0x0600D9EC RID: 55788
	public delegate void HideListener();
}
