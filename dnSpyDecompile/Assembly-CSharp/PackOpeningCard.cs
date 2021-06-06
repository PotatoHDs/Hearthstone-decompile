using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000614 RID: 1556
public class PackOpeningCard : MonoBehaviour
{
	// Token: 0x06005732 RID: 22322 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Awake()
	{
	}

	// Token: 0x06005733 RID: 22323 RVA: 0x001C91BF File Offset: 0x001C73BF
	public NetCache.BoosterCard GetCard()
	{
		return this.m_boosterCard;
	}

	// Token: 0x06005734 RID: 22324 RVA: 0x001C91C7 File Offset: 0x001C73C7
	public string GetCardId()
	{
		if (this.m_boosterCard != null)
		{
			return this.m_boosterCard.Def.Name;
		}
		return null;
	}

	// Token: 0x06005735 RID: 22325 RVA: 0x001C91E3 File Offset: 0x001C73E3
	public EntityDef GetEntityDef()
	{
		DefLoader.DisposableFullDef fullDef = this.m_fullDef;
		if (fullDef == null)
		{
			return null;
		}
		return fullDef.EntityDef;
	}

	// Token: 0x06005736 RID: 22326 RVA: 0x001C91F6 File Offset: 0x001C73F6
	public Actor GetActor()
	{
		return this.m_actor;
	}

	// Token: 0x06005737 RID: 22327 RVA: 0x001C9200 File Offset: 0x001C7400
	public void AttachBoosterCard(NetCache.BoosterCard boosterCard)
	{
		if (boosterCard == null)
		{
			return;
		}
		this.m_boosterCard = boosterCard;
		this.m_premium = this.m_boosterCard.Def.Premium;
		this.Destroy();
		DefLoader.Get().LoadFullDef(this.m_boosterCard.Def.Name, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnFullDefLoaded), null, null);
	}

	// Token: 0x06005738 RID: 22328 RVA: 0x001C925C File Offset: 0x001C745C
	public bool IsReady()
	{
		return this.m_ready;
	}

	// Token: 0x06005739 RID: 22329 RVA: 0x001C9264 File Offset: 0x001C7464
	public bool IsRevealed()
	{
		return this.m_revealed;
	}

	// Token: 0x0600573A RID: 22330 RVA: 0x001C926C File Offset: 0x001C746C
	private void OnDestroy()
	{
		this.DisposeFullDef();
	}

	// Token: 0x0600573B RID: 22331 RVA: 0x001C9274 File Offset: 0x001C7474
	public void Destroy()
	{
		this.m_ready = false;
		if (this.m_actor != null)
		{
			this.m_actor.Destroy();
			this.m_actor = null;
		}
		this.m_rarityInfo = null;
		this.m_spell = null;
		this.m_revealButton = null;
		this.m_revealed = false;
	}

	// Token: 0x0600573C RID: 22332 RVA: 0x001C92C4 File Offset: 0x001C74C4
	public bool IsInputEnabled()
	{
		return this.m_inputEnabled;
	}

	// Token: 0x0600573D RID: 22333 RVA: 0x001C92CC File Offset: 0x001C74CC
	public void EnableInput(bool enable)
	{
		this.m_inputEnabled = enable;
		this.UpdateInput();
	}

	// Token: 0x0600573E RID: 22334 RVA: 0x001C92DB File Offset: 0x001C74DB
	public bool IsRevealEnabled()
	{
		return this.m_revealEnabled;
	}

	// Token: 0x0600573F RID: 22335 RVA: 0x001C92E3 File Offset: 0x001C74E3
	public void EnableReveal(bool enable)
	{
		this.m_revealEnabled = enable;
		this.UpdateActor();
	}

	// Token: 0x06005740 RID: 22336 RVA: 0x001C92F2 File Offset: 0x001C74F2
	public void AddRevealedListener(PackOpeningCard.RevealedCallback callback)
	{
		this.AddRevealedListener(callback, null);
	}

	// Token: 0x06005741 RID: 22337 RVA: 0x001C92FC File Offset: 0x001C74FC
	public void AddRevealedListener(PackOpeningCard.RevealedCallback callback, object userData)
	{
		PackOpeningCard.RevealedListener revealedListener = new PackOpeningCard.RevealedListener();
		revealedListener.SetCallback(callback);
		revealedListener.SetUserData(userData);
		this.m_revealedListeners.Add(revealedListener);
	}

	// Token: 0x06005742 RID: 22338 RVA: 0x001C9329 File Offset: 0x001C7529
	public void RemoveRevealedListener(PackOpeningCard.RevealedCallback callback)
	{
		this.RemoveRevealedListener(callback, null);
	}

	// Token: 0x06005743 RID: 22339 RVA: 0x001C9334 File Offset: 0x001C7534
	public void RemoveRevealedListener(PackOpeningCard.RevealedCallback callback, object userData)
	{
		PackOpeningCard.RevealedListener revealedListener = new PackOpeningCard.RevealedListener();
		revealedListener.SetCallback(callback);
		revealedListener.SetUserData(userData);
		this.m_revealedListeners.Remove(revealedListener);
	}

	// Token: 0x06005744 RID: 22340 RVA: 0x001C9362 File Offset: 0x001C7562
	public void RemoveOnOverWhileFlippedListeners()
	{
		this.m_revealButton.RemoveEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnOverWhileFlipped));
		this.m_revealButton.RemoveEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnOutWhileFlipped));
	}

	// Token: 0x06005745 RID: 22341 RVA: 0x001C9396 File Offset: 0x001C7596
	public void ForceReveal()
	{
		this.OnPress(null);
	}

	// Token: 0x06005746 RID: 22342 RVA: 0x001C939F File Offset: 0x001C759F
	public void ShowRarityGlow()
	{
		if (this.IsRevealed())
		{
			return;
		}
		this.OnOver(null);
	}

	// Token: 0x06005747 RID: 22343 RVA: 0x001C93B1 File Offset: 0x001C75B1
	public void HideRarityGlow()
	{
		if (this.IsRevealed())
		{
			return;
		}
		this.OnOut(null);
	}

	// Token: 0x06005748 RID: 22344 RVA: 0x001C93C4 File Offset: 0x001C75C4
	public void UseRandomCardBack()
	{
		CardBackDisplay componentInChildren = this.m_SharedHiddenCardObject.GetComponentInChildren<CardBackDisplay>();
		if (componentInChildren != null)
		{
			componentInChildren.m_CardBackSlot = CardBackManager.CardBackSlot.RANDOM;
		}
	}

	// Token: 0x06005749 RID: 22345 RVA: 0x001C93F0 File Offset: 0x001C75F0
	private void OnFullDefLoaded(string cardId, DefLoader.DisposableFullDef fullDef, object userData)
	{
		if (fullDef == null)
		{
			Debug.LogError(string.Format("PackOpeningCard.OnFullDefLoaded() - FAILED to load \"{0}\"", cardId));
			return;
		}
		this.DisposeFullDef();
		this.m_fullDef = fullDef;
		if (!this.DetermineRarityInfo())
		{
			return;
		}
		AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(this.m_fullDef.EntityDef, this.m_premium), new PrefabCallback<GameObject>(this.OnActorLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
		if (Cheats.Get().IsNewCardInPackOpeningEnabed())
		{
			CollectibleCard card = CollectionManager.Get().GetCard(this.m_fullDef.EntityDef.GetCardId(), this.m_premium);
			this.m_isNew = (card.SeenCount < 1 && card.OwnedCount < 2);
		}
	}

	// Token: 0x0600574A RID: 22346 RVA: 0x001C94A3 File Offset: 0x001C76A3
	private void DisposeFullDef()
	{
		DefLoader.DisposableFullDef fullDef = this.m_fullDef;
		if (fullDef != null)
		{
			fullDef.Dispose();
		}
		this.m_fullDef = null;
	}

	// Token: 0x0600574B RID: 22347 RVA: 0x001C94C0 File Offset: 0x001C76C0
	private void OnActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("PackOpeningCard.OnActorLoaded() - FAILED to load actor \"{0}\"", assetRef));
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogError(string.Format("PackOpeningCard.OnActorLoaded() - ERROR actor \"{0}\" has no Actor component", base.name));
			return;
		}
		this.m_actor = component;
		this.m_actor.TurnOffCollider();
		this.SetupActor();
		SceneUtils.SetLayer(component.gameObject, GameLayer.IgnoreFullScreenEffects);
		this.m_ready = true;
		this.UpdateInput();
		this.UpdateActor();
	}

	// Token: 0x0600574C RID: 22348 RVA: 0x001C9548 File Offset: 0x001C7748
	private bool DetermineRarityInfo()
	{
		PackOpeningRarity packOpeningRarity = GameUtils.GetPackOpeningRarity((this.m_fullDef.EntityDef == null) ? TAG_RARITY.COMMON : this.m_fullDef.EntityDef.GetRarity());
		if (packOpeningRarity == PackOpeningRarity.NONE)
		{
			Debug.LogError(string.Format("PackOpeningCard.DetermineRarityInfo() - FAILED to determine rarity for {0}", this.GetCardId()));
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
			Debug.LogError(string.Format("PackOpeningCard.DetermineRarityInfo() - {0} has no rarity info list. cardId={1}", this, this.GetCardId()));
			return false;
		}
		foreach (PackOpeningCardRarityInfo packOpeningCardRarityInfo in componentsInChildren)
		{
			if (packOpeningRarity == packOpeningCardRarityInfo.m_RarityType)
			{
				this.m_rarityInfo = packOpeningCardRarityInfo;
				this.SetupRarity();
				return true;
			}
		}
		Debug.LogError(string.Format("PackOpeningCard.DetermineRarityInfo() - {0} has no rarity info for {1}. cardId={2}", this, packOpeningRarity, this.GetCardId()));
		return false;
	}

	// Token: 0x0600574D RID: 22349 RVA: 0x001C9624 File Offset: 0x001C7824
	private void SetupActor()
	{
		this.m_actor.SetEntityDef(this.m_fullDef.EntityDef);
		this.m_actor.SetCardDef(this.m_fullDef.DisposableCardDef);
		this.m_actor.SetPremium(this.m_premium);
		this.m_actor.UpdateAllComponents();
	}

	// Token: 0x0600574E RID: 22350 RVA: 0x001C967C File Offset: 0x001C787C
	private void UpdateActor()
	{
		if (this.m_actor == null)
		{
			return;
		}
		if (!this.IsRevealEnabled())
		{
			this.m_actor.Hide();
			return;
		}
		if (!this.IsRevealed())
		{
			this.m_actor.Hide();
		}
		Vector3 localScale = this.m_actor.transform.localScale;
		this.m_actor.transform.parent = this.m_rarityInfo.m_RevealedCardObject.transform;
		this.m_actor.transform.localPosition = Vector3.zero;
		this.m_actor.transform.localRotation = Quaternion.identity;
		this.m_actor.transform.localScale = localScale;
		if (this.m_isNew)
		{
			this.m_actor.SetActorState(ActorStateType.CARD_RECENTLY_ACQUIRED);
		}
	}

	// Token: 0x0600574F RID: 22351 RVA: 0x001C9740 File Offset: 0x001C7940
	private void SetupRarity()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_rarityInfo.gameObject);
		if (gameObject == null)
		{
			return;
		}
		gameObject.transform.parent = this.m_CardParent.transform;
		this.m_rarityInfo = gameObject.GetComponent<PackOpeningCardRarityInfo>();
		this.m_rarityInfo.m_RarityObject.SetActive(true);
		this.m_rarityInfo.m_HiddenCardObject.SetActive(true);
		Vector3 localPosition = this.m_rarityInfo.m_HiddenCardObject.transform.localPosition;
		this.m_rarityInfo.m_HiddenCardObject.transform.parent = this.m_CardParent.transform;
		this.m_rarityInfo.m_HiddenCardObject.transform.localPosition = localPosition;
		this.m_rarityInfo.m_HiddenCardObject.transform.localRotation = Quaternion.identity;
		this.m_rarityInfo.m_HiddenCardObject.transform.localScale = new Vector3(7.646f, 7.646f, 7.646f);
		TransformUtil.AttachAndPreserveLocalTransform(this.m_rarityInfo.m_RarityObject.transform, this.m_CardParent.transform);
		this.m_spell = this.m_rarityInfo.m_RarityObject.GetComponent<Spell>();
		this.m_revealButton = this.m_rarityInfo.m_RarityObject.GetComponent<PegUIElement>();
		if (UniversalInputManager.Get().IsTouchMode())
		{
			this.m_revealButton.SetReceiveReleaseWithoutMouseDown(true);
		}
		this.m_SharedHiddenCardObject.transform.parent = this.m_rarityInfo.m_HiddenCardObject.transform;
		TransformUtil.Identity(this.m_SharedHiddenCardObject.transform);
	}

	// Token: 0x06005750 RID: 22352 RVA: 0x001C98CF File Offset: 0x001C7ACF
	private void OnOver(UIEvent e)
	{
		if (this.m_spell == null || !this.IsReady())
		{
			return;
		}
		this.m_spell.ActivateState(SpellStateType.BIRTH);
	}

	// Token: 0x06005751 RID: 22353 RVA: 0x001C98F4 File Offset: 0x001C7AF4
	private void OnOut(UIEvent e)
	{
		if (this.m_spell == null || !this.IsReady())
		{
			return;
		}
		this.m_spell.ActivateState(SpellStateType.CANCEL);
	}

	// Token: 0x06005752 RID: 22354 RVA: 0x001C991C File Offset: 0x001C7B1C
	private void OnOverWhileFlipped(UIEvent e)
	{
		if (this.m_isNew)
		{
			this.m_actor.SetActorState(ActorStateType.CARD_RECENTLY_ACQUIRED_MOUSE_OVER);
		}
		else
		{
			this.m_actor.SetActorState(ActorStateType.CARD_HISTORY);
		}
		TooltipPanelManager.Get().UpdateKeywordHelpForPackOpening(this.m_actor.GetEntityDef(), this.m_actor);
	}

	// Token: 0x06005753 RID: 22355 RVA: 0x001C9968 File Offset: 0x001C7B68
	private void OnOutWhileFlipped(UIEvent e)
	{
		if (this.m_isNew)
		{
			this.m_actor.SetActorState(ActorStateType.CARD_RECENTLY_ACQUIRED);
		}
		else
		{
			this.m_actor.SetActorState(ActorStateType.CARD_IDLE);
		}
		TooltipPanelManager.Get().HideKeywordHelp();
	}

	// Token: 0x06005754 RID: 22356 RVA: 0x001C9998 File Offset: 0x001C7B98
	private void OnPress(UIEvent e)
	{
		if (this.m_spell == null || !this.IsReady())
		{
			return;
		}
		this.m_revealed = true;
		this.UpdateInput();
		this.m_spell.AddFinishedCallback(new Spell.FinishedCallback(this.OnSpellFinished));
		this.m_spell.ActivateState(SpellStateType.ACTION);
		this.PlayCorrectSound();
	}

	// Token: 0x06005755 RID: 22357 RVA: 0x001C99F4 File Offset: 0x001C7BF4
	private void UpdateInput()
	{
		if (!this.IsReady())
		{
			return;
		}
		bool flag = !this.IsRevealed() && this.IsInputEnabled();
		if (this.m_revealButton != null && !UniversalInputManager.UsePhoneUI)
		{
			if (flag)
			{
				this.m_revealButton.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnOver));
				this.m_revealButton.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnOut));
				this.m_revealButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnPress));
				if (PegUI.Get().FindHitElement() == this.m_revealButton)
				{
					this.OnOver(null);
					return;
				}
			}
			else
			{
				this.m_revealButton.RemoveEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnOver));
				this.m_revealButton.RemoveEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnOut));
				this.m_revealButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnPress));
			}
		}
	}

	// Token: 0x06005756 RID: 22358 RVA: 0x001C9AF4 File Offset: 0x001C7CF4
	private void FireRevealedEvent()
	{
		PackOpeningCard.RevealedListener[] array = this.m_revealedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire();
		}
	}

	// Token: 0x06005757 RID: 22359 RVA: 0x001C9B24 File Offset: 0x001C7D24
	private void OnSpellFinished(Spell spell, object userData)
	{
		this.FireRevealedEvent();
		this.UpdateInput();
		this.ShowClassName();
		this.ShowIsNew();
		this.m_revealButton.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnOverWhileFlipped));
		this.m_revealButton.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnOutWhileFlipped));
	}

	// Token: 0x06005758 RID: 22360 RVA: 0x001C9B7C File Offset: 0x001C7D7C
	private void ShowClassName()
	{
		string className = this.GetClassName();
		foreach (UberText uberText in this.m_ClassNameSpell.GetComponentsInChildren<UberText>(true))
		{
			uberText.Text = className;
			if (this.m_fullDef.EntityDef.IsMultiClass())
			{
				uberText.OutlineSize = 3f;
			}
		}
		this.m_ClassNameSpell.ActivateState(SpellStateType.BIRTH);
	}

	// Token: 0x06005759 RID: 22361 RVA: 0x001C9BDF File Offset: 0x001C7DDF
	private void ShowIsNew()
	{
		if (this.m_isNew && this.m_IsNewSpell != null)
		{
			this.m_IsNewSpell.ActivateState(SpellStateType.BIRTH);
		}
	}

	// Token: 0x0600575A RID: 22362 RVA: 0x001C9C04 File Offset: 0x001C7E04
	private string GetClassName()
	{
		TAG_CLASS @class = this.m_fullDef.EntityDef.GetClass();
		if (this.m_fullDef.EntityDef.IsMultiClass())
		{
			return this.GetFamilyClassNames();
		}
		if (@class == TAG_CLASS.NEUTRAL)
		{
			return GameStrings.Get("GLUE_PACK_OPENING_ALL_CLASSES");
		}
		return GameStrings.GetClassName(@class);
	}

	// Token: 0x0600575B RID: 22363 RVA: 0x001C9C54 File Offset: 0x001C7E54
	private string GetFamilyClassNames()
	{
		if (this.m_fullDef.EntityDef.HasTag(GAME_TAG.GRIMY_GOONS))
		{
			return GameStrings.Get("GLUE_GOONS_CLASS_NAMES");
		}
		if (this.m_fullDef.EntityDef.HasTag(GAME_TAG.JADE_LOTUS))
		{
			return GameStrings.Get("GLUE_LOTUS_CLASS_NAMES");
		}
		if (this.m_fullDef.EntityDef.HasTag(GAME_TAG.KABAL))
		{
			return GameStrings.Get("GLUE_KABAL_CLASS_NAMES");
		}
		if (this.m_fullDef.EntityDef.GetClasses(null).Count<TAG_CLASS>() == 10)
		{
			return GameStrings.Get("GLUE_PACK_OPENING_ALL_CLASSES");
		}
		string text = "";
		IEnumerable<TAG_CLASS> classes = this.m_fullDef.EntityDef.GetClasses(null);
		foreach (TAG_CLASS tag_CLASS in classes)
		{
			text += GameStrings.GetClassName(tag_CLASS);
			if (tag_CLASS != classes.Last<TAG_CLASS>())
			{
				text += GameStrings.Get("GLOBAL_COMMA_SEPARATOR");
			}
		}
		return text;
	}

	// Token: 0x0600575C RID: 22364 RVA: 0x001C9D60 File Offset: 0x001C7F60
	private void PlayCorrectSound()
	{
		switch (this.m_rarityInfo.m_RarityType)
		{
		case PackOpeningRarity.COMMON:
			if (this.m_premium == TAG_PREMIUM.GOLDEN)
			{
				SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_FOIL_C_29.prefab:69820e4999e4afa439761151e057a526");
				return;
			}
			break;
		case PackOpeningRarity.UNCOMMON:
			break;
		case PackOpeningRarity.RARE:
			if (this.m_premium == TAG_PREMIUM.GOLDEN)
			{
				SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_FOIL_R_30.prefab:f5bf5bfd8e5f4d247aa8a6da966969cf");
				return;
			}
			SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_RARE_27.prefab:8ff0de7a4fd144b4b983caea4c54da4d");
			return;
		case PackOpeningRarity.EPIC:
			if (this.m_premium == TAG_PREMIUM.GOLDEN)
			{
				SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_FOIL_E_31.prefab:d419d6eca0e2a72469544bae5f11542f");
				return;
			}
			SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_EPIC_26.prefab:e76d67f55b976104794c3cf73382e82a");
			return;
		case PackOpeningRarity.LEGENDARY:
			if (this.m_premium == TAG_PREMIUM.GOLDEN)
			{
				SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_FOIL_L_32.prefab:caefd66acfc4e2b4f858035c274b257e");
				return;
			}
			SoundManager.Get().LoadAndPlay("VO_ANNOUNCER_LEGENDARY_25.prefab:e015c982aec12bc4893f36396d426750");
			break;
		default:
			return;
		}
	}

	// Token: 0x04004AF8 RID: 19192
	public GameObject m_CardParent;

	// Token: 0x04004AF9 RID: 19193
	public GameObject m_SharedHiddenCardObject;

	// Token: 0x04004AFA RID: 19194
	public Spell m_ClassNameSpell;

	// Token: 0x04004AFB RID: 19195
	public Spell m_IsNewSpell;

	// Token: 0x04004AFC RID: 19196
	private const TAG_RARITY FALLBACK_RARITY = TAG_RARITY.COMMON;

	// Token: 0x04004AFD RID: 19197
	private NetCache.BoosterCard m_boosterCard;

	// Token: 0x04004AFE RID: 19198
	private TAG_PREMIUM m_premium;

	// Token: 0x04004AFF RID: 19199
	private DefLoader.DisposableFullDef m_fullDef;

	// Token: 0x04004B00 RID: 19200
	private Actor m_actor;

	// Token: 0x04004B01 RID: 19201
	private PackOpeningCardRarityInfo m_rarityInfo;

	// Token: 0x04004B02 RID: 19202
	private Spell m_spell;

	// Token: 0x04004B03 RID: 19203
	private PegUIElement m_revealButton;

	// Token: 0x04004B04 RID: 19204
	private bool m_ready;

	// Token: 0x04004B05 RID: 19205
	private bool m_inputEnabled;

	// Token: 0x04004B06 RID: 19206
	private bool m_revealEnabled;

	// Token: 0x04004B07 RID: 19207
	private bool m_revealed;

	// Token: 0x04004B08 RID: 19208
	private bool m_isNew;

	// Token: 0x04004B09 RID: 19209
	private List<PackOpeningCard.RevealedListener> m_revealedListeners = new List<PackOpeningCard.RevealedListener>();

	// Token: 0x02002118 RID: 8472
	// (Invoke) Token: 0x06012242 RID: 74306
	public delegate void RevealedCallback(object userData);

	// Token: 0x02002119 RID: 8473
	private class RevealedListener : EventListener<PackOpeningCard.RevealedCallback>
	{
		// Token: 0x06012245 RID: 74309 RVA: 0x004FEC37 File Offset: 0x004FCE37
		public void Fire()
		{
			this.m_callback(this.m_userData);
		}
	}
}
