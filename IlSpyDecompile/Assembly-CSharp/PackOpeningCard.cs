using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PackOpeningCard : MonoBehaviour
{
	public delegate void RevealedCallback(object userData);

	private class RevealedListener : EventListener<RevealedCallback>
	{
		public void Fire()
		{
			m_callback(m_userData);
		}
	}

	public GameObject m_CardParent;

	public GameObject m_SharedHiddenCardObject;

	public Spell m_ClassNameSpell;

	public Spell m_IsNewSpell;

	private const TAG_RARITY FALLBACK_RARITY = TAG_RARITY.COMMON;

	private NetCache.BoosterCard m_boosterCard;

	private TAG_PREMIUM m_premium;

	private DefLoader.DisposableFullDef m_fullDef;

	private Actor m_actor;

	private PackOpeningCardRarityInfo m_rarityInfo;

	private Spell m_spell;

	private PegUIElement m_revealButton;

	private bool m_ready;

	private bool m_inputEnabled;

	private bool m_revealEnabled;

	private bool m_revealed;

	private bool m_isNew;

	private List<RevealedListener> m_revealedListeners = new List<RevealedListener>();

	private void Awake()
	{
	}

	public NetCache.BoosterCard GetCard()
	{
		return m_boosterCard;
	}

	public string GetCardId()
	{
		if (m_boosterCard != null)
		{
			return m_boosterCard.Def.Name;
		}
		return null;
	}

	public EntityDef GetEntityDef()
	{
		return m_fullDef?.EntityDef;
	}

	public Actor GetActor()
	{
		return m_actor;
	}

	public void AttachBoosterCard(NetCache.BoosterCard boosterCard)
	{
		if (boosterCard != null)
		{
			m_boosterCard = boosterCard;
			m_premium = m_boosterCard.Def.Premium;
			Destroy();
			DefLoader.Get().LoadFullDef(m_boosterCard.Def.Name, OnFullDefLoaded);
		}
	}

	public bool IsReady()
	{
		return m_ready;
	}

	public bool IsRevealed()
	{
		return m_revealed;
	}

	private void OnDestroy()
	{
		DisposeFullDef();
	}

	public void Destroy()
	{
		m_ready = false;
		if (m_actor != null)
		{
			m_actor.Destroy();
			m_actor = null;
		}
		m_rarityInfo = null;
		m_spell = null;
		m_revealButton = null;
		m_revealed = false;
	}

	public bool IsInputEnabled()
	{
		return m_inputEnabled;
	}

	public void EnableInput(bool enable)
	{
		m_inputEnabled = enable;
		UpdateInput();
	}

	public bool IsRevealEnabled()
	{
		return m_revealEnabled;
	}

	public void EnableReveal(bool enable)
	{
		m_revealEnabled = enable;
		UpdateActor();
	}

	public void AddRevealedListener(RevealedCallback callback)
	{
		AddRevealedListener(callback, null);
	}

	public void AddRevealedListener(RevealedCallback callback, object userData)
	{
		RevealedListener revealedListener = new RevealedListener();
		revealedListener.SetCallback(callback);
		revealedListener.SetUserData(userData);
		m_revealedListeners.Add(revealedListener);
	}

	public void RemoveRevealedListener(RevealedCallback callback)
	{
		RemoveRevealedListener(callback, null);
	}

	public void RemoveRevealedListener(RevealedCallback callback, object userData)
	{
		RevealedListener revealedListener = new RevealedListener();
		revealedListener.SetCallback(callback);
		revealedListener.SetUserData(userData);
		m_revealedListeners.Remove(revealedListener);
	}

	public void RemoveOnOverWhileFlippedListeners()
	{
		m_revealButton.RemoveEventListener(UIEventType.ROLLOVER, OnOverWhileFlipped);
		m_revealButton.RemoveEventListener(UIEventType.ROLLOUT, OnOutWhileFlipped);
	}

	public void ForceReveal()
	{
		OnPress(null);
	}

	public void ShowRarityGlow()
	{
		if (!IsRevealed())
		{
			OnOver(null);
		}
	}

	public void HideRarityGlow()
	{
		if (!IsRevealed())
		{
			OnOut(null);
		}
	}

	public void UseRandomCardBack()
	{
		CardBackDisplay componentInChildren = m_SharedHiddenCardObject.GetComponentInChildren<CardBackDisplay>();
		if (componentInChildren != null)
		{
			componentInChildren.m_CardBackSlot = CardBackManager.CardBackSlot.RANDOM;
		}
	}

	private void OnFullDefLoaded(string cardId, DefLoader.DisposableFullDef fullDef, object userData)
	{
		if (fullDef == null)
		{
			Debug.LogError($"PackOpeningCard.OnFullDefLoaded() - FAILED to load \"{cardId}\"");
			return;
		}
		DisposeFullDef();
		m_fullDef = fullDef;
		if (DetermineRarityInfo())
		{
			AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(m_fullDef.EntityDef, m_premium), OnActorLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
			if (Cheats.Get().IsNewCardInPackOpeningEnabed())
			{
				CollectibleCard card = CollectionManager.Get().GetCard(m_fullDef.EntityDef.GetCardId(), m_premium);
				m_isNew = card.SeenCount < 1 && card.OwnedCount < 2;
			}
		}
	}

	private void DisposeFullDef()
	{
		m_fullDef?.Dispose();
		m_fullDef = null;
	}

	private void OnActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"PackOpeningCard.OnActorLoaded() - FAILED to load actor \"{assetRef}\"");
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogError($"PackOpeningCard.OnActorLoaded() - ERROR actor \"{base.name}\" has no Actor component");
			return;
		}
		m_actor = component;
		m_actor.TurnOffCollider();
		SetupActor();
		SceneUtils.SetLayer(component.gameObject, GameLayer.IgnoreFullScreenEffects);
		m_ready = true;
		UpdateInput();
		UpdateActor();
	}

	private bool DetermineRarityInfo()
	{
		PackOpeningRarity packOpeningRarity = GameUtils.GetPackOpeningRarity((m_fullDef.EntityDef == null) ? TAG_RARITY.COMMON : m_fullDef.EntityDef.GetRarity());
		if (packOpeningRarity == PackOpeningRarity.NONE)
		{
			Debug.LogError($"PackOpeningCard.DetermineRarityInfo() - FAILED to determine rarity for {GetCardId()}");
			return false;
		}
		GameObject packOpeningCardEffects = SceneUtils.FindComponentInParents<PackOpening>(this).GetPackOpeningCardEffects();
		if (packOpeningCardEffects == null)
		{
			Debug.LogError("PackOpeningCard.DetermineRarityInfo() - Fail to get card effect from PackOpening");
			return false;
		}
		PackOpeningCardRarityInfo[] componentsInChildren = packOpeningCardEffects.GetComponentsInChildren<PackOpeningCardRarityInfo>();
		if (componentsInChildren == null)
		{
			Debug.LogError($"PackOpeningCard.DetermineRarityInfo() - {this} has no rarity info list. cardId={GetCardId()}");
			return false;
		}
		foreach (PackOpeningCardRarityInfo packOpeningCardRarityInfo in componentsInChildren)
		{
			if (packOpeningRarity == packOpeningCardRarityInfo.m_RarityType)
			{
				m_rarityInfo = packOpeningCardRarityInfo;
				SetupRarity();
				return true;
			}
		}
		Debug.LogError($"PackOpeningCard.DetermineRarityInfo() - {this} has no rarity info for {packOpeningRarity}. cardId={GetCardId()}");
		return false;
	}

	private void SetupActor()
	{
		m_actor.SetEntityDef(m_fullDef.EntityDef);
		m_actor.SetCardDef(m_fullDef.DisposableCardDef);
		m_actor.SetPremium(m_premium);
		m_actor.UpdateAllComponents();
	}

	private void UpdateActor()
	{
		if (m_actor == null)
		{
			return;
		}
		if (!IsRevealEnabled())
		{
			m_actor.Hide();
			return;
		}
		if (!IsRevealed())
		{
			m_actor.Hide();
		}
		Vector3 localScale = m_actor.transform.localScale;
		m_actor.transform.parent = m_rarityInfo.m_RevealedCardObject.transform;
		m_actor.transform.localPosition = Vector3.zero;
		m_actor.transform.localRotation = Quaternion.identity;
		m_actor.transform.localScale = localScale;
		if (m_isNew)
		{
			m_actor.SetActorState(ActorStateType.CARD_RECENTLY_ACQUIRED);
		}
	}

	private void SetupRarity()
	{
		GameObject gameObject = Object.Instantiate(m_rarityInfo.gameObject);
		if (!(gameObject == null))
		{
			gameObject.transform.parent = m_CardParent.transform;
			m_rarityInfo = gameObject.GetComponent<PackOpeningCardRarityInfo>();
			m_rarityInfo.m_RarityObject.SetActive(value: true);
			m_rarityInfo.m_HiddenCardObject.SetActive(value: true);
			Vector3 localPosition = m_rarityInfo.m_HiddenCardObject.transform.localPosition;
			m_rarityInfo.m_HiddenCardObject.transform.parent = m_CardParent.transform;
			m_rarityInfo.m_HiddenCardObject.transform.localPosition = localPosition;
			m_rarityInfo.m_HiddenCardObject.transform.localRotation = Quaternion.identity;
			m_rarityInfo.m_HiddenCardObject.transform.localScale = new Vector3(7.646f, 7.646f, 7.646f);
			TransformUtil.AttachAndPreserveLocalTransform(m_rarityInfo.m_RarityObject.transform, m_CardParent.transform);
			m_spell = m_rarityInfo.m_RarityObject.GetComponent<Spell>();
			m_revealButton = m_rarityInfo.m_RarityObject.GetComponent<PegUIElement>();
			if (UniversalInputManager.Get().IsTouchMode())
			{
				m_revealButton.SetReceiveReleaseWithoutMouseDown(receiveReleaseWithoutMouseDown: true);
			}
			m_SharedHiddenCardObject.transform.parent = m_rarityInfo.m_HiddenCardObject.transform;
			TransformUtil.Identity(m_SharedHiddenCardObject.transform);
		}
	}

	private void OnOver(UIEvent e)
	{
		if (!(m_spell == null) && IsReady())
		{
			m_spell.ActivateState(SpellStateType.BIRTH);
		}
	}

	private void OnOut(UIEvent e)
	{
		if (!(m_spell == null) && IsReady())
		{
			m_spell.ActivateState(SpellStateType.CANCEL);
		}
	}

	private void OnOverWhileFlipped(UIEvent e)
	{
		if (m_isNew)
		{
			m_actor.SetActorState(ActorStateType.CARD_RECENTLY_ACQUIRED_MOUSE_OVER);
		}
		else
		{
			m_actor.SetActorState(ActorStateType.CARD_HISTORY);
		}
		TooltipPanelManager.Get().UpdateKeywordHelpForPackOpening(m_actor.GetEntityDef(), m_actor);
	}

	private void OnOutWhileFlipped(UIEvent e)
	{
		if (m_isNew)
		{
			m_actor.SetActorState(ActorStateType.CARD_RECENTLY_ACQUIRED);
		}
		else
		{
			m_actor.SetActorState(ActorStateType.CARD_IDLE);
		}
		TooltipPanelManager.Get().HideKeywordHelp();
	}

	private void OnPress(UIEvent e)
	{
		if (!(m_spell == null) && IsReady())
		{
			m_revealed = true;
			UpdateInput();
			m_spell.AddFinishedCallback(OnSpellFinished);
			m_spell.ActivateState(SpellStateType.ACTION);
			PlayCorrectSound();
		}
	}

	private void UpdateInput()
	{
		if (!IsReady())
		{
			return;
		}
		bool flag = !IsRevealed() && IsInputEnabled();
		if (!(m_revealButton != null) || (bool)UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		if (flag)
		{
			m_revealButton.AddEventListener(UIEventType.ROLLOVER, OnOver);
			m_revealButton.AddEventListener(UIEventType.ROLLOUT, OnOut);
			m_revealButton.AddEventListener(UIEventType.RELEASE, OnPress);
			if (PegUI.Get().FindHitElement() == m_revealButton)
			{
				OnOver(null);
			}
		}
		else
		{
			m_revealButton.RemoveEventListener(UIEventType.ROLLOVER, OnOver);
			m_revealButton.RemoveEventListener(UIEventType.ROLLOUT, OnOut);
			m_revealButton.RemoveEventListener(UIEventType.RELEASE, OnPress);
		}
	}

	private void FireRevealedEvent()
	{
		RevealedListener[] array = m_revealedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire();
		}
	}

	private void OnSpellFinished(Spell spell, object userData)
	{
		FireRevealedEvent();
		UpdateInput();
		ShowClassName();
		ShowIsNew();
		m_revealButton.AddEventListener(UIEventType.ROLLOVER, OnOverWhileFlipped);
		m_revealButton.AddEventListener(UIEventType.ROLLOUT, OnOutWhileFlipped);
	}

	private void ShowClassName()
	{
		string className = GetClassName();
		UberText[] componentsInChildren = m_ClassNameSpell.GetComponentsInChildren<UberText>(includeInactive: true);
		foreach (UberText uberText in componentsInChildren)
		{
			uberText.Text = className;
			if (m_fullDef.EntityDef.IsMultiClass())
			{
				uberText.OutlineSize = 3f;
			}
		}
		m_ClassNameSpell.ActivateState(SpellStateType.BIRTH);
	}

	private void ShowIsNew()
	{
		if (m_isNew && m_IsNewSpell != null)
		{
			m_IsNewSpell.ActivateState(SpellStateType.BIRTH);
		}
	}

	private string GetClassName()
	{
		TAG_CLASS @class = m_fullDef.EntityDef.GetClass();
		if (m_fullDef.EntityDef.IsMultiClass())
		{
			return GetFamilyClassNames();
		}
		if (@class == TAG_CLASS.NEUTRAL)
		{
			return GameStrings.Get("GLUE_PACK_OPENING_ALL_CLASSES");
		}
		return GameStrings.GetClassName(@class);
	}

	private string GetFamilyClassNames()
	{
		if (m_fullDef.EntityDef.HasTag(GAME_TAG.GRIMY_GOONS))
		{
			return GameStrings.Get("GLUE_GOONS_CLASS_NAMES");
		}
		if (m_fullDef.EntityDef.HasTag(GAME_TAG.JADE_LOTUS))
		{
			return GameStrings.Get("GLUE_LOTUS_CLASS_NAMES");
		}
		if (m_fullDef.EntityDef.HasTag(GAME_TAG.KABAL))
		{
			return GameStrings.Get("GLUE_KABAL_CLASS_NAMES");
		}
		if (m_fullDef.EntityDef.GetClasses().Count() == 10)
		{
			return GameStrings.Get("GLUE_PACK_OPENING_ALL_CLASSES");
		}
		string text = "";
		IEnumerable<TAG_CLASS> classes = m_fullDef.EntityDef.GetClasses();
		foreach (TAG_CLASS item in classes)
		{
			text += GameStrings.GetClassName(item);
			if (item != classes.Last())
			{
				text += GameStrings.Get("GLOBAL_COMMA_SEPARATOR");
			}
		}
		return text;
	}

	private void PlayCorrectSound()
	{
		switch (m_rarityInfo.m_RarityType)
		{
		case PackOpeningRarity.COMMON:
			if (m_premium == TAG_PREMIUM.GOLDEN)
			{
				SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_FOIL_C_29.prefab:69820e4999e4afa439761151e057a526");
			}
			break;
		case PackOpeningRarity.RARE:
			if (m_premium == TAG_PREMIUM.GOLDEN)
			{
				SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_FOIL_R_30.prefab:f5bf5bfd8e5f4d247aa8a6da966969cf");
			}
			else
			{
				SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_RARE_27.prefab:8ff0de7a4fd144b4b983caea4c54da4d");
			}
			break;
		case PackOpeningRarity.EPIC:
			if (m_premium == TAG_PREMIUM.GOLDEN)
			{
				SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_FOIL_E_31.prefab:d419d6eca0e2a72469544bae5f11542f");
			}
			else
			{
				SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_EPIC_26.prefab:e76d67f55b976104794c3cf73382e82a");
			}
			break;
		case PackOpeningRarity.LEGENDARY:
			if (m_premium == TAG_PREMIUM.GOLDEN)
			{
				SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_FOIL_L_32.prefab:caefd66acfc4e2b4f858035c274b257e");
			}
			else
			{
				SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_LEGENDARY_25.prefab:e015c982aec12bc4893f36396d426750");
			}
			break;
		case PackOpeningRarity.UNCOMMON:
			break;
		}
	}
}
