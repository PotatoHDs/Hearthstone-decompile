using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class DeckTrayHeroSkinContent : DeckTrayContent
{
	public delegate void HeroAssigned(string cardId);

	private class AnimatedHeroSkin
	{
		public Actor Actor;

		public GameObject GameObject;

		public Vector3 OriginalScale;

		public Vector3 OriginalPosition;
	}

	[CustomEditField(Sections = "Positioning")]
	public GameObject m_root;

	[CustomEditField(Sections = "Positioning")]
	public Vector3 m_trayHiddenOffset;

	[CustomEditField(Sections = "Positioning")]
	public GameObject m_heroSkinContainer;

	[CustomEditField(Sections = "Positioning")]
	public Vector3 m_missingCardEffectScale;

	[CustomEditField(Sections = "Animation & Sounds")]
	public iTween.EaseType m_traySlideSlideInAnimation = iTween.EaseType.easeOutBounce;

	[CustomEditField(Sections = "Animation & Sounds")]
	public iTween.EaseType m_traySlideSlideOutAnimation;

	[CustomEditField(Sections = "Animation & Sounds")]
	public float m_traySlideAnimationTime = 0.25f;

	[CustomEditField(Sections = "Animation & Sounds", T = EditType.SOUND_PREFAB)]
	public string m_socketSound;

	public UberText m_currentHeroSkinName;

	[CustomEditField(Sections = "Card Effects")]
	public Material m_sepiaCardMaterial;

	private const string ADD_CARD_TO_DECK_SOUND = "collection_manager_card_add_to_deck_instant.prefab:06df359c4026d7e47b06a4174f33e3ef";

	private string m_currentHeroCardId;

	private Actor m_heroSkinObject;

	private Vector3 m_originalLocalPosition;

	private bool m_animating;

	private bool m_waitingToLoadHeroDef;

	private List<HeroAssigned> m_heroAssignedListeners = new List<HeroAssigned>();

	private AnimatedHeroSkin m_animData;

	protected override void Awake()
	{
		base.Awake();
		m_originalLocalPosition = base.transform.localPosition;
		base.transform.localPosition = m_originalLocalPosition + m_trayHiddenOffset;
		m_root.SetActive(value: false);
		LoadHeroSkinActor();
	}

	public void UpdateHeroSkin(EntityDef entityDef, TAG_PREMIUM premium, bool assigning)
	{
		UpdateHeroSkin(entityDef.GetCardId(), premium, assigning);
	}

	public void UpdateHeroSkin(string cardId, TAG_PREMIUM premium, bool assigning, Actor baseActor = null)
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (assigning)
		{
			if (!string.IsNullOrEmpty(m_socketSound))
			{
				SoundManager.Get().LoadAndPlay(m_socketSound);
			}
			editedDeck.HeroOverridden = true;
		}
		UpdateMissingEffect(editedDeck.HeroOverridden);
		if (m_currentHeroCardId == cardId)
		{
			ShowSocketFX();
			return;
		}
		m_currentHeroCardId = cardId;
		editedDeck.HeroCardID = cardId;
		editedDeck.HeroPremium = premium;
		if (baseActor != null)
		{
			using (DefLoader.DisposableCardDef cardDef = baseActor.ShareDisposableCardDef())
			{
				UpdateHeroSkinVisual(baseActor.GetEntityDef(), cardDef, baseActor.GetPremium(), assigning);
			}
			return;
		}
		m_waitingToLoadHeroDef = true;
		DefLoader.Get().LoadFullDef(cardId, delegate(string cardID, DefLoader.DisposableFullDef fullDef, object callbackData)
		{
			using (fullDef)
			{
				m_waitingToLoadHeroDef = false;
				UpdateHeroSkinVisual(fullDef.EntityDef, fullDef.DisposableCardDef, premium, assigning);
			}
		});
	}

	public void AnimateInNewHeroSkin(Actor actor)
	{
		GameObject gameObject = actor.gameObject;
		AnimatedHeroSkin animatedHeroSkin = new AnimatedHeroSkin();
		animatedHeroSkin.Actor = actor;
		animatedHeroSkin.GameObject = gameObject;
		animatedHeroSkin.OriginalScale = gameObject.transform.localScale;
		animatedHeroSkin.OriginalPosition = gameObject.transform.position;
		m_animData = animatedHeroSkin;
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);
		gameObject.transform.localScale = m_heroSkinContainer.transform.lossyScale;
		Hashtable args = iTween.Hash("from", 0f, "to", 1f, "time", 0.6f, "easetype", iTween.EaseType.easeOutCubic, "onupdate", "AnimateNewHeroSkinUpdate", "onupdatetarget", base.gameObject, "oncomplete", "AnimateNewHeroSkinFinished", "oncompleteparams", animatedHeroSkin, "oncompletetarget", base.gameObject);
		iTween.ValueTo(gameObject, args);
		CollectionHeroSkin component = actor.gameObject.GetComponent<CollectionHeroSkin>();
		if (component != null)
		{
			component.ShowSocketFX();
		}
		SoundManager.Get().LoadAndPlay("collection_manager_card_add_to_deck_instant.prefab:06df359c4026d7e47b06a4174f33e3ef", base.gameObject);
	}

	private void AnimateNewHeroSkinFinished()
	{
		m_heroSkinObject.gameObject.SetActive(value: true);
		Actor actor = m_animData.Actor;
		UpdateHeroSkin(actor.GetEntityDef().GetCardId(), actor.GetPremium(), assigning: true, actor);
		UnityEngine.Object.Destroy(m_animData.GameObject);
		m_animData = null;
	}

	private void AnimateNewHeroSkinUpdate(float val)
	{
		GameObject gameObject = m_animData.GameObject;
		Vector3 originalPosition = m_animData.OriginalPosition;
		Vector3 position = m_heroSkinContainer.transform.position;
		if (val <= 0.85f)
		{
			val /= 0.85f;
			gameObject.transform.position = new Vector3(Mathf.Lerp(originalPosition.x, position.x, val), Mathf.Lerp(originalPosition.y, position.y, val) + Mathf.Sin(val * (float)Math.PI) * 15f + val * 4f, Mathf.Lerp(originalPosition.z, position.z, val));
		}
		else
		{
			m_heroSkinObject.gameObject.SetActive(value: false);
			val = (val - 0.85f) / 0.149999976f;
			gameObject.transform.position = new Vector3(position.x, position.y + Mathf.Lerp(4f, 0f, val), position.z);
		}
	}

	public void SetNewHeroSkin(Actor actor)
	{
		if (m_animData == null)
		{
			Actor actor2 = actor.Clone();
			actor2.SetCardDefFromActor(actor);
			CollectionHeroSkin component = actor2.GetComponent<CollectionHeroSkin>();
			if (component != null)
			{
				component.ShowFavoriteBanner(show: false);
			}
			AnimateInNewHeroSkin(actor2);
		}
	}

	public override bool PreAnimateContentEntrance()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (editedDeck == null)
		{
			return true;
		}
		UpdateHeroSkin(editedDeck.HeroCardID, editedDeck.HeroPremium, assigning: false);
		return true;
	}

	public override bool AnimateContentEntranceStart()
	{
		if (m_waitingToLoadHeroDef)
		{
			return false;
		}
		m_root.SetActive(value: true);
		base.transform.localPosition = m_originalLocalPosition;
		m_animating = true;
		iTween.MoveFrom(base.gameObject, iTween.Hash("position", m_originalLocalPosition + m_trayHiddenOffset, "islocal", true, "time", m_traySlideAnimationTime, "easetype", m_traySlideSlideInAnimation, "oncomplete", (Action<object>)delegate
		{
			m_animating = false;
		}));
		return true;
	}

	public override bool AnimateContentEntranceEnd()
	{
		return !m_animating;
	}

	public override bool AnimateContentExitStart()
	{
		base.transform.localPosition = m_originalLocalPosition;
		m_animating = true;
		iTween.MoveTo(base.gameObject, iTween.Hash("position", m_originalLocalPosition + m_trayHiddenOffset, "islocal", true, "time", m_traySlideAnimationTime, "easetype", m_traySlideSlideOutAnimation, "oncomplete", (Action<object>)delegate
		{
			m_animating = false;
			m_root.SetActive(value: false);
		}));
		return true;
	}

	public override bool AnimateContentExitEnd()
	{
		return !m_animating;
	}

	public void RegisterHeroAssignedListener(HeroAssigned dlg)
	{
		m_heroAssignedListeners.Add(dlg);
	}

	public void UnregisterHeroAssignedListener(HeroAssigned dlg)
	{
		m_heroAssignedListeners.Remove(dlg);
	}

	private void LoadHeroSkinActor()
	{
		string heroSkinOrHandActor = ActorNames.GetHeroSkinOrHandActor(TAG_CARDTYPE.HERO, TAG_PREMIUM.NORMAL);
		AssetLoader.Get().InstantiatePrefab(heroSkinOrHandActor, delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			if (go == null)
			{
				Debug.LogWarning($"DeckTrayHeroSkinContent.LoadHeroSkinActor - FAILED to load \"{assetRef}\"");
			}
			else
			{
				Actor component = go.GetComponent<Actor>();
				if (component == null)
				{
					Debug.LogWarning($"HandActorCache.OnActorLoaded() - ERROR \"{assetRef}\" has no Actor component");
				}
				else
				{
					GameUtils.SetParent(component, m_heroSkinContainer);
					m_heroSkinObject = component;
					m_heroSkinObject.SetUseShortName(useShortName: true);
				}
			}
		}, null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	private void UpdateHeroSkinVisual(EntityDef entityDef, DefLoader.DisposableCardDef cardDef, TAG_PREMIUM premium, bool assigning)
	{
		if (m_heroSkinObject == null)
		{
			Debug.LogError("Hero skin object not loaded yet! Cannot set portrait!");
			return;
		}
		m_heroSkinObject.SetEntityDef(entityDef);
		m_heroSkinObject.SetCardDef(cardDef);
		m_heroSkinObject.SetPremium(premium);
		m_heroSkinObject.UpdateAllComponents();
		CollectionHeroSkin component = m_heroSkinObject.GetComponent<CollectionHeroSkin>();
		if (component != null)
		{
			component.SetClass(entityDef.GetClass());
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				component.ShowName = false;
			}
		}
		HeroAssigned[] array = m_heroAssignedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](entityDef.GetCardId());
		}
		if (assigning && cardDef?.CardDef != null)
		{
			GameUtils.LoadCardDefEmoteSound(cardDef.CardDef.m_EmoteDefs, EmoteType.PICKED, OnPickEmoteLoaded);
		}
		if (m_currentHeroSkinName != null)
		{
			m_currentHeroSkinName.Text = entityDef.GetName();
		}
		ShowSocketFX();
	}

	private void OnPickEmoteLoaded(CardSoundSpell spell)
	{
		if (!(spell == null))
		{
			spell.AddFinishedCallback(OnPickEmoteFinished);
			spell.Reactivate();
		}
	}

	private void OnPickEmoteFinished(Spell spell, object userData)
	{
		UnityEngine.Object.Destroy(spell.gameObject);
	}

	private void ShowSocketFX()
	{
		CollectionHeroSkin component = m_heroSkinObject.GetComponent<CollectionHeroSkin>();
		if (component != null)
		{
			component.ShowSocketFX();
		}
	}

	private void UpdateMissingEffect(bool overriden)
	{
		if (overriden)
		{
			m_heroSkinObject.DisableMissingCardEffect();
		}
		else
		{
			m_heroSkinObject.SetMissingCardMaterial(m_sepiaCardMaterial);
			m_heroSkinObject.MissingCardEffect();
			m_heroSkinObject.m_missingCardEffect.transform.localScale = m_missingCardEffectScale;
		}
		m_heroSkinObject.UpdateAllComponents();
	}
}
