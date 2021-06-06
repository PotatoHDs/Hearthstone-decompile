using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class CollectionDeckInfo : MonoBehaviour
{
	public delegate void ShowListener();

	public delegate void HideListener();

	public GameObject m_root;

	public GameObject m_visualRoot;

	public GameObject m_heroPowerParent;

	public UberText m_heroPowerName;

	public UberText m_heroPowerDescription;

	public UberText m_manaCurveTooltipText;

	public PegUIElement m_offClicker;

	public List<DeckInfoManaBar> m_manaBars;

	private readonly float MANA_COST_TEXT_MIN_LOCAL_Z;

	private readonly float MANA_COST_TEXT_MAX_LOCAL_Z = 5.167298f;

	private Actor m_heroPowerActor;

	private Actor m_goldenHeroPowerActor;

	private DefLoader.DisposableCardDef m_heroCardDef;

	private bool m_wasTouchModeEnabled;

	protected bool m_shown = true;

	private string m_heroPowerID = "";

	private List<ShowListener> m_showListeners = new List<ShowListener>();

	private List<HideListener> m_hideListeners = new List<HideListener>();

	private void Awake()
	{
		m_manaCurveTooltipText.Text = GameStrings.Get("GLUE_COLLECTION_DECK_INFO_MANA_TOOLTIP");
		foreach (DeckInfoManaBar manaBar in m_manaBars)
		{
			manaBar.m_costText.Text = GetTextForManaCost(manaBar.m_manaCostID);
		}
		AssetLoader.Get().InstantiatePrefab("Card_Play_HeroPower.prefab:a3794839abb947146903a26be13e09af", OnHeroPowerActorLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
		AssetLoader.Get().InstantiatePrefab(ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.PLAY_HERO_POWER, TAG_PREMIUM.GOLDEN), OnGoldenHeroPowerActorLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
		m_wasTouchModeEnabled = true;
	}

	private void Start()
	{
		m_offClicker.AddEventListener(UIEventType.RELEASE, OnClosePressed);
		m_offClicker.AddEventListener(UIEventType.ROLLOVER, OverOffClicker);
	}

	private void OnDestroy()
	{
		m_heroCardDef?.Dispose();
		m_heroCardDef = null;
	}

	private void Update()
	{
		if (m_wasTouchModeEnabled == UniversalInputManager.Get().IsTouchMode())
		{
			return;
		}
		m_wasTouchModeEnabled = UniversalInputManager.Get().IsTouchMode();
		if (UniversalInputManager.Get().IsTouchMode())
		{
			if (m_heroPowerActor != null)
			{
				m_heroPowerActor.TurnOffCollider();
			}
			if (m_goldenHeroPowerActor != null)
			{
				m_goldenHeroPowerActor.TurnOffCollider();
			}
			m_offClicker.gameObject.SetActive(value: true);
		}
		else
		{
			if (m_heroPowerActor != null)
			{
				m_heroPowerActor.TurnOnCollider();
			}
			if (m_goldenHeroPowerActor != null)
			{
				m_goldenHeroPowerActor.TurnOnCollider();
			}
			m_offClicker.gameObject.SetActive(value: true);
		}
	}

	public void Show()
	{
		if (!m_shown)
		{
			if (CollectionDeckTray.Get() == null)
			{
				m_visualRoot.SetActive(value: true);
			}
			else
			{
				CollectionDeck editingDeck = CollectionDeckTray.Get().GetCardsContent().GetEditingDeck();
				m_visualRoot.SetActive(!editingDeck.HasUIHeroOverride());
			}
			m_root.SetActive(value: true);
			m_shown = true;
			if (UniversalInputManager.Get().IsTouchMode())
			{
				Navigation.Push(GoBackImpl);
			}
			ShowListener[] array = m_showListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i]();
			}
		}
	}

	private bool GoBackImpl()
	{
		Hide();
		return true;
	}

	public void Hide()
	{
		Navigation.RemoveHandler(GoBackImpl);
		if (m_shown)
		{
			m_root.SetActive(value: false);
			m_shown = false;
			HideListener[] array = m_hideListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i]();
			}
		}
	}

	public void RegisterShowListener(ShowListener dlg)
	{
		m_showListeners.Add(dlg);
	}

	public void UnregisterShowListener(ShowListener dlg)
	{
		m_showListeners.Remove(dlg);
	}

	public void RegisterHideListener(HideListener dlg)
	{
		m_hideListeners.Add(dlg);
	}

	public void UnregisterHideListener(HideListener dlg)
	{
		m_hideListeners.Remove(dlg);
	}

	public bool IsShown()
	{
		return m_shown;
	}

	public void UpdateManaCurve()
	{
		CollectionDeck editingDeck = CollectionDeckTray.Get().GetCardsContent().GetEditingDeck();
		UpdateManaCurve(editingDeck);
	}

	public void UpdateManaCurve(CollectionDeck deck)
	{
		if (deck == null)
		{
			Debug.LogWarning($"CollectionDeckInfo.UpdateManaCurve(): deck is null.");
			return;
		}
		string heroCardID = deck.HeroCardID;
		CardPortraitQuality quality = new CardPortraitQuality(3, TAG_PREMIUM.NORMAL);
		DefLoader.Get().LoadCardDef(heroCardID, OnHeroCardDefLoaded, null, quality);
		foreach (DeckInfoManaBar manaBar in m_manaBars)
		{
			manaBar.m_numCards = 0;
		}
		int num = 0;
		foreach (CollectionDeckSlot slot in deck.GetSlots())
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(slot.CardID);
			int manaCost = entityDef.GetCost();
			if (manaCost > 7)
			{
				manaCost = 7;
			}
			DeckInfoManaBar deckInfoManaBar = m_manaBars.Find((DeckInfoManaBar obj) => obj.m_manaCostID == manaCost);
			if (deckInfoManaBar == null)
			{
				Debug.LogWarning($"CollectionDeckInfo.UpdateManaCurve(): Cannot update curve. Could not find mana bar for {entityDef} (cost {manaCost})");
				return;
			}
			deckInfoManaBar.m_numCards += slot.Count;
			if (deckInfoManaBar.m_numCards > num)
			{
				num = deckInfoManaBar.m_numCards;
			}
		}
		foreach (DeckInfoManaBar manaBar2 in m_manaBars)
		{
			manaBar2.m_numCardsText.Text = Convert.ToString(manaBar2.m_numCards);
			float num2 = ((num == 0) ? 0f : ((float)manaBar2.m_numCards / (float)num));
			Vector3 localPosition = manaBar2.m_numCardsText.transform.localPosition;
			localPosition.z = Mathf.Lerp(MANA_COST_TEXT_MIN_LOCAL_Z, MANA_COST_TEXT_MAX_LOCAL_Z, num2);
			manaBar2.m_numCardsText.transform.localPosition = localPosition;
			manaBar2.m_barFill.GetComponent<Renderer>().GetMaterial().SetFloat("_Percent", num2);
		}
	}

	public void SetDeck(CollectionDeck deck)
	{
		if (deck == null)
		{
			Debug.LogWarning($"CollectionDeckInfo.SetDeckID(): deck is null");
			return;
		}
		UpdateManaCurve(deck);
		if (!string.IsNullOrEmpty(deck.HeroPowerCardID))
		{
			m_heroPowerID = deck.HeroPowerCardID;
		}
		else
		{
			string heroPowerCardIdFromHero = GameUtils.GetHeroPowerCardIdFromHero(deck.HeroCardID);
			if (string.IsNullOrEmpty(heroPowerCardIdFromHero))
			{
				Debug.LogWarning("CollectionDeckInfo.UpdateInfo(): invalid hero power ID");
				m_heroPowerID = "";
				return;
			}
			if (heroPowerCardIdFromHero.Equals(m_heroPowerID))
			{
				return;
			}
			m_heroPowerID = heroPowerCardIdFromHero;
		}
		string heroCardId = CollectionManager.GetHeroCardId(deck.GetClass(), CardHero.HeroType.VANILLA);
		TAG_PREMIUM tAG_PREMIUM = CollectionManager.Get().GetBestCardPremium(heroCardId);
		if (SceneMgr.Get().IsInDuelsMode())
		{
			tAG_PREMIUM = TAG_PREMIUM.NORMAL;
		}
		DefLoader.Get().LoadFullDef(m_heroPowerID, OnHeroPowerFullDefLoaded, tAG_PREMIUM);
	}

	private string GetTextForManaCost(int manaCostID)
	{
		if (manaCostID < 0 || manaCostID > 7)
		{
			Debug.LogWarning($"CollectionDeckInfo.GetTextForManaCost(): don't know how to handle mana cost ID {manaCostID}");
			return "";
		}
		string text = Convert.ToString(manaCostID);
		if (manaCostID == 7)
		{
			text += GameStrings.Get("GLUE_COLLECTION_PLUS");
		}
		return text;
	}

	private void OnHeroPowerActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning($"CollectionDeckInfo.OnHeroPowerActorLoaded() - FAILED to load actor \"{assetRef}\"");
			return;
		}
		m_heroPowerActor = go.GetComponent<Actor>();
		if (m_heroPowerActor == null)
		{
			Debug.LogWarning($"CollectionDeckInfo.OnHeroPowerActorLoaded() - ERROR actor \"{assetRef}\" has no Actor component");
			return;
		}
		m_heroPowerActor.SetUnlit();
		m_heroPowerActor.transform.parent = m_heroPowerParent.transform;
		m_heroPowerActor.transform.localScale = Vector3.one;
		m_heroPowerActor.transform.localPosition = Vector3.zero;
		if (UniversalInputManager.Get().IsTouchMode())
		{
			m_heroPowerActor.TurnOffCollider();
		}
	}

	private void OnGoldenHeroPowerActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning($"CollectionDeckInfo.OnHeroPowerActorLoaded() - FAILED to load actor \"{assetRef}\"");
			return;
		}
		m_goldenHeroPowerActor = go.GetComponent<Actor>();
		if (m_goldenHeroPowerActor == null)
		{
			Debug.LogWarning($"CollectionDeckInfo.OnGoldenHeroPowerActorLoaded() - ERROR actor \"{assetRef}\" has no Actor component");
			return;
		}
		m_goldenHeroPowerActor.SetUnlit();
		m_goldenHeroPowerActor.transform.parent = m_heroPowerParent.transform;
		m_goldenHeroPowerActor.transform.localScale = Vector3.one;
		m_goldenHeroPowerActor.transform.localPosition = Vector3.zero;
		if (UniversalInputManager.Get().IsTouchMode())
		{
			m_goldenHeroPowerActor.TurnOffCollider();
		}
	}

	private void OnHeroPowerFullDefLoaded(string cardID, DefLoader.DisposableFullDef def, object userData)
	{
		TAG_PREMIUM premium = (TAG_PREMIUM)userData;
		StartCoroutine(SetHeroPowerInfoWhenReady(cardID, def, premium));
	}

	private IEnumerator SetHeroPowerInfoWhenReady(string heroPowerCardID, DefLoader.DisposableFullDef def, TAG_PREMIUM premium)
	{
		using (def)
		{
			while (m_goldenHeroPowerActor == null)
			{
				yield return null;
			}
			while (m_heroPowerActor == null)
			{
				yield return null;
			}
			SetHeroPowerInfo(heroPowerCardID, def, premium);
		}
	}

	private void SetHeroPowerInfo(string heroPowerCardID, DefLoader.DisposableFullDef def, TAG_PREMIUM premium)
	{
		if (!heroPowerCardID.Equals(m_heroPowerID))
		{
			return;
		}
		if (premium == TAG_PREMIUM.GOLDEN)
		{
			if (m_heroPowerActor != null)
			{
				m_heroPowerActor.Hide();
			}
			m_goldenHeroPowerActor.Show();
			m_goldenHeroPowerActor.SetFullDef(def);
			m_goldenHeroPowerActor.SetUnlit();
			m_goldenHeroPowerActor.SetPremium(premium);
			m_goldenHeroPowerActor.UpdateAllComponents();
		}
		else
		{
			if (m_goldenHeroPowerActor != null)
			{
				m_goldenHeroPowerActor.Hide();
			}
			m_heroPowerActor.Show();
			m_heroPowerActor.SetFullDef(def);
			m_heroPowerActor.SetUnlit();
			m_heroPowerActor.UpdateAllComponents();
		}
		string text = def.EntityDef.GetName();
		m_heroPowerName.Text = text;
		string cardTextInHand = def.EntityDef.GetCardTextInHand();
		m_heroPowerDescription.Text = cardTextInHand;
	}

	private void OnHeroCardDefLoaded(string cardId, DefLoader.DisposableCardDef def, object userData)
	{
		m_heroCardDef?.Dispose();
		m_heroCardDef = def;
	}

	private void OnClosePressed(UIEvent e)
	{
		Hide();
	}

	private void OverOffClicker(UIEvent e)
	{
		Hide();
	}
}
