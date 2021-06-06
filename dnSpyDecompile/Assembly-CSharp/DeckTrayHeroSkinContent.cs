using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000892 RID: 2194
[CustomEditClass]
public class DeckTrayHeroSkinContent : DeckTrayContent
{
	// Token: 0x06007864 RID: 30820 RVA: 0x00274688 File Offset: 0x00272888
	protected override void Awake()
	{
		base.Awake();
		this.m_originalLocalPosition = base.transform.localPosition;
		base.transform.localPosition = this.m_originalLocalPosition + this.m_trayHiddenOffset;
		this.m_root.SetActive(false);
		this.LoadHeroSkinActor();
	}

	// Token: 0x06007865 RID: 30821 RVA: 0x002746DA File Offset: 0x002728DA
	public void UpdateHeroSkin(EntityDef entityDef, TAG_PREMIUM premium, bool assigning)
	{
		this.UpdateHeroSkin(entityDef.GetCardId(), premium, assigning, null);
	}

	// Token: 0x06007866 RID: 30822 RVA: 0x002746EC File Offset: 0x002728EC
	public void UpdateHeroSkin(string cardId, TAG_PREMIUM premium, bool assigning, Actor baseActor = null)
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (assigning)
		{
			if (!string.IsNullOrEmpty(this.m_socketSound))
			{
				SoundManager.Get().LoadAndPlay(this.m_socketSound);
			}
			editedDeck.HeroOverridden = true;
		}
		this.UpdateMissingEffect(editedDeck.HeroOverridden);
		if (this.m_currentHeroCardId == cardId)
		{
			this.ShowSocketFX();
			return;
		}
		this.m_currentHeroCardId = cardId;
		editedDeck.HeroCardID = cardId;
		editedDeck.HeroPremium = premium;
		if (baseActor != null)
		{
			using (DefLoader.DisposableCardDef disposableCardDef = baseActor.ShareDisposableCardDef())
			{
				this.UpdateHeroSkinVisual(baseActor.GetEntityDef(), disposableCardDef, baseActor.GetPremium(), assigning);
			}
			return;
		}
		this.m_waitingToLoadHeroDef = true;
		DefLoader.Get().LoadFullDef(cardId, delegate(string cardID, DefLoader.DisposableFullDef fullDef, object callbackData)
		{
			try
			{
				this.m_waitingToLoadHeroDef = false;
				this.UpdateHeroSkinVisual(fullDef.EntityDef, fullDef.DisposableCardDef, premium, assigning);
			}
			finally
			{
				if (fullDef != null)
				{
					((IDisposable)fullDef).Dispose();
				}
			}
		}, null, null);
	}

	// Token: 0x06007867 RID: 30823 RVA: 0x002747F8 File Offset: 0x002729F8
	public void AnimateInNewHeroSkin(Actor actor)
	{
		GameObject gameObject = actor.gameObject;
		DeckTrayHeroSkinContent.AnimatedHeroSkin animatedHeroSkin = new DeckTrayHeroSkinContent.AnimatedHeroSkin();
		animatedHeroSkin.Actor = actor;
		animatedHeroSkin.GameObject = gameObject;
		animatedHeroSkin.OriginalScale = gameObject.transform.localScale;
		animatedHeroSkin.OriginalPosition = gameObject.transform.position;
		this.m_animData = animatedHeroSkin;
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);
		gameObject.transform.localScale = this.m_heroSkinContainer.transform.lossyScale;
		Hashtable args = iTween.Hash(new object[]
		{
			"from",
			0f,
			"to",
			1f,
			"time",
			0.6f,
			"easetype",
			iTween.EaseType.easeOutCubic,
			"onupdate",
			"AnimateNewHeroSkinUpdate",
			"onupdatetarget",
			base.gameObject,
			"oncomplete",
			"AnimateNewHeroSkinFinished",
			"oncompleteparams",
			animatedHeroSkin,
			"oncompletetarget",
			base.gameObject
		});
		iTween.ValueTo(gameObject, args);
		CollectionHeroSkin component = actor.gameObject.GetComponent<CollectionHeroSkin>();
		if (component != null)
		{
			component.ShowSocketFX();
		}
		SoundManager.Get().LoadAndPlay("collection_manager_card_add_to_deck_instant.prefab:06df359c4026d7e47b06a4174f33e3ef", base.gameObject);
	}

	// Token: 0x06007868 RID: 30824 RVA: 0x0027499C File Offset: 0x00272B9C
	private void AnimateNewHeroSkinFinished()
	{
		this.m_heroSkinObject.gameObject.SetActive(true);
		Actor actor = this.m_animData.Actor;
		this.UpdateHeroSkin(actor.GetEntityDef().GetCardId(), actor.GetPremium(), true, actor);
		UnityEngine.Object.Destroy(this.m_animData.GameObject);
		this.m_animData = null;
	}

	// Token: 0x06007869 RID: 30825 RVA: 0x002749F8 File Offset: 0x00272BF8
	private void AnimateNewHeroSkinUpdate(float val)
	{
		GameObject gameObject = this.m_animData.GameObject;
		Vector3 originalPosition = this.m_animData.OriginalPosition;
		Vector3 position = this.m_heroSkinContainer.transform.position;
		if (val <= 0.85f)
		{
			val /= 0.85f;
			gameObject.transform.position = new Vector3(Mathf.Lerp(originalPosition.x, position.x, val), Mathf.Lerp(originalPosition.y, position.y, val) + Mathf.Sin(val * 3.1415927f) * 15f + val * 4f, Mathf.Lerp(originalPosition.z, position.z, val));
			return;
		}
		this.m_heroSkinObject.gameObject.SetActive(false);
		val = (val - 0.85f) / 0.14999998f;
		gameObject.transform.position = new Vector3(position.x, position.y + Mathf.Lerp(4f, 0f, val), position.z);
	}

	// Token: 0x0600786A RID: 30826 RVA: 0x00274AF4 File Offset: 0x00272CF4
	public void SetNewHeroSkin(Actor actor)
	{
		if (this.m_animData != null)
		{
			return;
		}
		Actor actor2 = actor.Clone();
		actor2.SetCardDefFromActor(actor);
		CollectionHeroSkin component = actor2.GetComponent<CollectionHeroSkin>();
		if (component != null)
		{
			component.ShowFavoriteBanner(false);
		}
		this.AnimateInNewHeroSkin(actor2);
	}

	// Token: 0x0600786B RID: 30827 RVA: 0x00274B38 File Offset: 0x00272D38
	public override bool PreAnimateContentEntrance()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (editedDeck == null)
		{
			return true;
		}
		this.UpdateHeroSkin(editedDeck.HeroCardID, editedDeck.HeroPremium, false, null);
		return true;
	}

	// Token: 0x0600786C RID: 30828 RVA: 0x00274B6C File Offset: 0x00272D6C
	public override bool AnimateContentEntranceStart()
	{
		if (this.m_waitingToLoadHeroDef)
		{
			return false;
		}
		this.m_root.SetActive(true);
		base.transform.localPosition = this.m_originalLocalPosition;
		this.m_animating = true;
		iTween.MoveFrom(base.gameObject, iTween.Hash(new object[]
		{
			"position",
			this.m_originalLocalPosition + this.m_trayHiddenOffset,
			"islocal",
			true,
			"time",
			this.m_traySlideAnimationTime,
			"easetype",
			this.m_traySlideSlideInAnimation,
			"oncomplete",
			new Action<object>(delegate(object o)
			{
				this.m_animating = false;
			})
		}));
		return true;
	}

	// Token: 0x0600786D RID: 30829 RVA: 0x00274C35 File Offset: 0x00272E35
	public override bool AnimateContentEntranceEnd()
	{
		return !this.m_animating;
	}

	// Token: 0x0600786E RID: 30830 RVA: 0x00274C40 File Offset: 0x00272E40
	public override bool AnimateContentExitStart()
	{
		base.transform.localPosition = this.m_originalLocalPosition;
		this.m_animating = true;
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"position",
			this.m_originalLocalPosition + this.m_trayHiddenOffset,
			"islocal",
			true,
			"time",
			this.m_traySlideAnimationTime,
			"easetype",
			this.m_traySlideSlideOutAnimation,
			"oncomplete",
			new Action<object>(delegate(object o)
			{
				this.m_animating = false;
				this.m_root.SetActive(false);
			})
		}));
		return true;
	}

	// Token: 0x0600786F RID: 30831 RVA: 0x00274C35 File Offset: 0x00272E35
	public override bool AnimateContentExitEnd()
	{
		return !this.m_animating;
	}

	// Token: 0x06007870 RID: 30832 RVA: 0x00274CF3 File Offset: 0x00272EF3
	public void RegisterHeroAssignedListener(DeckTrayHeroSkinContent.HeroAssigned dlg)
	{
		this.m_heroAssignedListeners.Add(dlg);
	}

	// Token: 0x06007871 RID: 30833 RVA: 0x00274D01 File Offset: 0x00272F01
	public void UnregisterHeroAssignedListener(DeckTrayHeroSkinContent.HeroAssigned dlg)
	{
		this.m_heroAssignedListeners.Remove(dlg);
	}

	// Token: 0x06007872 RID: 30834 RVA: 0x00274D10 File Offset: 0x00272F10
	private void LoadHeroSkinActor()
	{
		string heroSkinOrHandActor = ActorNames.GetHeroSkinOrHandActor(TAG_CARDTYPE.HERO, TAG_PREMIUM.NORMAL);
		AssetLoader.Get().InstantiatePrefab(heroSkinOrHandActor, delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			if (go == null)
			{
				Debug.LogWarning(string.Format("DeckTrayHeroSkinContent.LoadHeroSkinActor - FAILED to load \"{0}\"", assetRef));
				return;
			}
			Actor component = go.GetComponent<Actor>();
			if (component == null)
			{
				Debug.LogWarning(string.Format("HandActorCache.OnActorLoaded() - ERROR \"{0}\" has no Actor component", assetRef));
				return;
			}
			GameUtils.SetParent(component, this.m_heroSkinContainer, false);
			this.m_heroSkinObject = component;
			this.m_heroSkinObject.SetUseShortName(true);
		}, null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x06007873 RID: 30835 RVA: 0x00274D44 File Offset: 0x00272F44
	private void UpdateHeroSkinVisual(EntityDef entityDef, DefLoader.DisposableCardDef cardDef, TAG_PREMIUM premium, bool assigning)
	{
		if (this.m_heroSkinObject == null)
		{
			Debug.LogError("Hero skin object not loaded yet! Cannot set portrait!");
			return;
		}
		this.m_heroSkinObject.SetEntityDef(entityDef);
		this.m_heroSkinObject.SetCardDef(cardDef);
		this.m_heroSkinObject.SetPremium(premium);
		this.m_heroSkinObject.UpdateAllComponents();
		CollectionHeroSkin component = this.m_heroSkinObject.GetComponent<CollectionHeroSkin>();
		if (component != null)
		{
			component.SetClass(entityDef.GetClass());
			if (UniversalInputManager.UsePhoneUI)
			{
				component.ShowName = false;
			}
		}
		DeckTrayHeroSkinContent.HeroAssigned[] array = this.m_heroAssignedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](entityDef.GetCardId());
		}
		if (assigning && ((cardDef != null) ? cardDef.CardDef : null) != null)
		{
			GameUtils.LoadCardDefEmoteSound(cardDef.CardDef.m_EmoteDefs, EmoteType.PICKED, new GameUtils.EmoteSoundLoaded(this.OnPickEmoteLoaded));
		}
		if (this.m_currentHeroSkinName != null)
		{
			this.m_currentHeroSkinName.Text = entityDef.GetName();
		}
		this.ShowSocketFX();
	}

	// Token: 0x06007874 RID: 30836 RVA: 0x00274E51 File Offset: 0x00273051
	private void OnPickEmoteLoaded(CardSoundSpell spell)
	{
		if (spell == null)
		{
			return;
		}
		spell.AddFinishedCallback(new Spell.FinishedCallback(this.OnPickEmoteFinished));
		spell.Reactivate();
	}

	// Token: 0x06007875 RID: 30837 RVA: 0x000CDD07 File Offset: 0x000CBF07
	private void OnPickEmoteFinished(Spell spell, object userData)
	{
		UnityEngine.Object.Destroy(spell.gameObject);
	}

	// Token: 0x06007876 RID: 30838 RVA: 0x00274E78 File Offset: 0x00273078
	private void ShowSocketFX()
	{
		CollectionHeroSkin component = this.m_heroSkinObject.GetComponent<CollectionHeroSkin>();
		if (component != null)
		{
			component.ShowSocketFX();
		}
	}

	// Token: 0x06007877 RID: 30839 RVA: 0x00274EA0 File Offset: 0x002730A0
	private void UpdateMissingEffect(bool overriden)
	{
		if (overriden)
		{
			this.m_heroSkinObject.DisableMissingCardEffect();
		}
		else
		{
			this.m_heroSkinObject.SetMissingCardMaterial(this.m_sepiaCardMaterial);
			this.m_heroSkinObject.MissingCardEffect(true);
			this.m_heroSkinObject.m_missingCardEffect.transform.localScale = this.m_missingCardEffectScale;
		}
		this.m_heroSkinObject.UpdateAllComponents();
	}

	// Token: 0x04005DFB RID: 24059
	[CustomEditField(Sections = "Positioning")]
	public GameObject m_root;

	// Token: 0x04005DFC RID: 24060
	[CustomEditField(Sections = "Positioning")]
	public Vector3 m_trayHiddenOffset;

	// Token: 0x04005DFD RID: 24061
	[CustomEditField(Sections = "Positioning")]
	public GameObject m_heroSkinContainer;

	// Token: 0x04005DFE RID: 24062
	[CustomEditField(Sections = "Positioning")]
	public Vector3 m_missingCardEffectScale;

	// Token: 0x04005DFF RID: 24063
	[CustomEditField(Sections = "Animation & Sounds")]
	public iTween.EaseType m_traySlideSlideInAnimation = iTween.EaseType.easeOutBounce;

	// Token: 0x04005E00 RID: 24064
	[CustomEditField(Sections = "Animation & Sounds")]
	public iTween.EaseType m_traySlideSlideOutAnimation;

	// Token: 0x04005E01 RID: 24065
	[CustomEditField(Sections = "Animation & Sounds")]
	public float m_traySlideAnimationTime = 0.25f;

	// Token: 0x04005E02 RID: 24066
	[CustomEditField(Sections = "Animation & Sounds", T = EditType.SOUND_PREFAB)]
	public string m_socketSound;

	// Token: 0x04005E03 RID: 24067
	public UberText m_currentHeroSkinName;

	// Token: 0x04005E04 RID: 24068
	[CustomEditField(Sections = "Card Effects")]
	public Material m_sepiaCardMaterial;

	// Token: 0x04005E05 RID: 24069
	private const string ADD_CARD_TO_DECK_SOUND = "collection_manager_card_add_to_deck_instant.prefab:06df359c4026d7e47b06a4174f33e3ef";

	// Token: 0x04005E06 RID: 24070
	private string m_currentHeroCardId;

	// Token: 0x04005E07 RID: 24071
	private Actor m_heroSkinObject;

	// Token: 0x04005E08 RID: 24072
	private Vector3 m_originalLocalPosition;

	// Token: 0x04005E09 RID: 24073
	private bool m_animating;

	// Token: 0x04005E0A RID: 24074
	private bool m_waitingToLoadHeroDef;

	// Token: 0x04005E0B RID: 24075
	private List<DeckTrayHeroSkinContent.HeroAssigned> m_heroAssignedListeners = new List<DeckTrayHeroSkinContent.HeroAssigned>();

	// Token: 0x04005E0C RID: 24076
	private DeckTrayHeroSkinContent.AnimatedHeroSkin m_animData;

	// Token: 0x020024EB RID: 9451
	// (Invoke) Token: 0x0601316D RID: 78189
	public delegate void HeroAssigned(string cardId);

	// Token: 0x020024EC RID: 9452
	private class AnimatedHeroSkin
	{
		// Token: 0x0400EC16 RID: 60438
		public Actor Actor;

		// Token: 0x0400EC17 RID: 60439
		public GameObject GameObject;

		// Token: 0x0400EC18 RID: 60440
		public Vector3 OriginalScale;

		// Token: 0x0400EC19 RID: 60441
		public Vector3 OriginalPosition;
	}
}
