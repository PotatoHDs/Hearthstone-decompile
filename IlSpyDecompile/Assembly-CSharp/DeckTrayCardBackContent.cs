using System;
using System.Collections;
using UnityEngine;

[CustomEditClass]
public class DeckTrayCardBackContent : DeckTrayContent
{
	private class AnimatedCardBack
	{
		public int CardBackId;

		public GameObject GameObject;

		public Vector3 OriginalScale;

		public Vector3 OriginalPosition;
	}

	[CustomEditField(Sections = "Positioning")]
	public GameObject m_root;

	[CustomEditField(Sections = "Positioning")]
	public Vector3 m_trayHiddenOffset;

	[CustomEditField(Sections = "Positioning")]
	public GameObject m_cardBackContainer;

	[CustomEditField(Sections = "Animation & Sounds")]
	public iTween.EaseType m_traySlideSlideInAnimation = iTween.EaseType.easeOutBounce;

	[CustomEditField(Sections = "Animation & Sounds")]
	public iTween.EaseType m_traySlideSlideOutAnimation;

	[CustomEditField(Sections = "Animation & Sounds")]
	public float m_traySlideAnimationTime = 0.25f;

	[CustomEditField(Sections = "Animation & Sounds", T = EditType.SOUND_PREFAB)]
	public string m_socketSound;

	[CustomEditField(Sections = "Card Effects")]
	public Material m_sepiaCardMaterial;

	private const string ADD_CARD_TO_DECK_SOUND = "collection_manager_card_add_to_deck_instant.prefab:06df359c4026d7e47b06a4174f33e3ef";

	private GameObject m_currentCardBack;

	private Vector3 m_originalLocalPosition;

	private bool m_animatingTray;

	private bool m_waitingToLoadCardback;

	private AnimatedCardBack m_animData;

	public bool WaitingForCardbackAnimation
	{
		get
		{
			if (m_animData == null)
			{
				return m_waitingToLoadCardback;
			}
			return true;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		m_originalLocalPosition = base.transform.localPosition;
		base.transform.localPosition = m_originalLocalPosition + m_trayHiddenOffset;
		m_root.SetActive(value: false);
	}

	public void AnimateInNewCardBack(CardBackManager.LoadCardBackData cardBackData, GameObject original)
	{
		GameObject gameObject = cardBackData.m_GameObject;
		gameObject.GetComponent<Actor>().GetSpell(SpellType.DEATHREVERSE).Reactivate();
		AnimatedCardBack animatedCardBack = new AnimatedCardBack();
		animatedCardBack.CardBackId = cardBackData.m_CardBackIndex;
		animatedCardBack.GameObject = gameObject;
		animatedCardBack.OriginalScale = gameObject.transform.localScale;
		animatedCardBack.OriginalPosition = original.transform.position;
		m_animData = animatedCardBack;
		gameObject.transform.position = new Vector3(original.transform.position.x, original.transform.position.y + 0.5f, original.transform.position.z);
		gameObject.transform.localScale = m_cardBackContainer.transform.lossyScale;
		Hashtable args = iTween.Hash("from", 0f, "to", 1f, "time", 0.6f, "easetype", iTween.EaseType.easeOutCubic, "onupdate", "AnimateNewCardUpdate", "onupdatetarget", base.gameObject, "oncomplete", "AnimateNewCardFinished", "oncompleteparams", animatedCardBack, "oncompletetarget", base.gameObject);
		iTween.ValueTo(gameObject, args);
		SoundManager.Get().LoadAndPlay("collection_manager_card_add_to_deck_instant.prefab:06df359c4026d7e47b06a4174f33e3ef", base.gameObject);
	}

	private void AnimateNewCardFinished(AnimatedCardBack cardBack)
	{
		cardBack.GameObject.transform.localScale = cardBack.OriginalScale;
		UpdateCardBack(cardBack.CardBackId, assigning: true, cardBack.GameObject);
		m_animData = null;
	}

	private void AnimateNewCardUpdate(float val)
	{
		GameObject gameObject = m_animData.GameObject;
		Vector3 originalPosition = m_animData.OriginalPosition;
		Vector3 position = m_cardBackContainer.transform.position;
		if (val <= 0.85f)
		{
			val /= 0.85f;
			gameObject.transform.position = new Vector3(Mathf.Lerp(originalPosition.x, position.x, val), Mathf.Lerp(originalPosition.y, position.y, val) + Mathf.Sin(val * (float)Math.PI) * 15f + val * 4f, Mathf.Lerp(originalPosition.z, position.z, val));
			return;
		}
		if (m_currentCardBack != null)
		{
			UnityEngine.Object.Destroy(m_currentCardBack);
			m_currentCardBack = null;
		}
		val = (val - 0.85f) / 0.149999976f;
		gameObject.transform.position = new Vector3(position.x, position.y + Mathf.Lerp(4f, 0f, val), position.z);
	}

	public bool SetNewCardBack(int cardBackId, GameObject original)
	{
		if (m_animData != null || m_waitingToLoadCardback)
		{
			return false;
		}
		m_waitingToLoadCardback = true;
		if (!CardBackManager.Get().LoadCardBackByIndex(cardBackId, delegate(CardBackManager.LoadCardBackData cardBackData)
		{
			m_waitingToLoadCardback = false;
			AnimateInNewCardBack(cardBackData, original);
		}, "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9"))
		{
			m_waitingToLoadCardback = false;
			Debug.LogError("Could not load CardBack " + cardBackId);
			return false;
		}
		return true;
	}

	public void UpdateCardBack(int cardBackId, bool assigning, GameObject obj = null)
	{
		CollectionDeck currentDeck = CollectionManager.Get().GetEditedDeck();
		if (currentDeck == null)
		{
			return;
		}
		if (assigning)
		{
			if (!string.IsNullOrEmpty(m_socketSound))
			{
				SoundManager.Get().LoadAndPlay(m_socketSound);
			}
			currentDeck.CardBackOverridden = true;
		}
		currentDeck.CardBackID = cardBackId;
		if (obj != null)
		{
			SetCardBack(obj, currentDeck.CardBackOverridden, assigning);
			return;
		}
		m_waitingToLoadCardback = true;
		if (!CardBackManager.Get().LoadCardBackByIndex(cardBackId, delegate(CardBackManager.LoadCardBackData cardBackData)
		{
			if (currentDeck == null)
			{
				UnityEngine.Object.Destroy(cardBackData.m_GameObject);
				m_waitingToLoadCardback = false;
			}
			else
			{
				m_waitingToLoadCardback = false;
				GameObject go = cardBackData.m_GameObject;
				SetCardBack(go, currentDeck.CardBackOverridden, assigning);
			}
		}, "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9"))
		{
			m_waitingToLoadCardback = false;
			Debug.LogWarning($"CardBackManager was unable to load card back ID: {cardBackId}");
		}
	}

	private void SetCardBack(GameObject go, bool overriden, bool assigning)
	{
		GameUtils.SetParent(go, m_cardBackContainer, withRotation: true);
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		if (assigning)
		{
			Spell spell = component.GetSpell(SpellType.DEATHREVERSE);
			if (spell != null)
			{
				spell.ActivateState(SpellStateType.BIRTH);
			}
		}
		if (m_currentCardBack != null)
		{
			UnityEngine.Object.Destroy(m_currentCardBack);
		}
		m_currentCardBack = go;
		GameObject cardMesh = component.m_cardMesh;
		component.SetCardbackUpdateIgnore(ignoreUpdate: true);
		component.SetUnlit();
		UpdateMissingEffect(component, overriden);
		if (cardMesh != null)
		{
			Material material = cardMesh.GetComponent<Renderer>().GetMaterial();
			if (material.HasProperty("_SpecularIntensity"))
			{
				material.SetFloat("_SpecularIntensity", 0f);
			}
		}
	}

	public override bool PreAnimateContentEntrance()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		UpdateCardBack(editedDeck.CardBackID, assigning: false);
		return true;
	}

	public override bool AnimateContentEntranceStart()
	{
		if (m_waitingToLoadCardback)
		{
			return false;
		}
		m_root.SetActive(value: true);
		base.transform.localPosition = m_originalLocalPosition;
		m_animatingTray = true;
		iTween.MoveFrom(base.gameObject, iTween.Hash("position", m_originalLocalPosition + m_trayHiddenOffset, "islocal", true, "time", m_traySlideAnimationTime, "easetype", m_traySlideSlideInAnimation, "oncomplete", (Action<object>)delegate
		{
			m_animatingTray = false;
		}));
		return true;
	}

	public override bool AnimateContentEntranceEnd()
	{
		return !m_animatingTray;
	}

	public override bool AnimateContentExitStart()
	{
		base.transform.localPosition = m_originalLocalPosition;
		m_animatingTray = true;
		iTween.MoveTo(base.gameObject, iTween.Hash("position", m_originalLocalPosition + m_trayHiddenOffset, "islocal", true, "time", m_traySlideAnimationTime, "easetype", m_traySlideSlideOutAnimation, "oncomplete", (Action<object>)delegate
		{
			m_animatingTray = false;
			m_root.SetActive(value: false);
		}));
		return true;
	}

	public override bool AnimateContentExitEnd()
	{
		return !m_animatingTray;
	}

	private void UpdateMissingEffect(Actor cardBackActor, bool overriden)
	{
		if (!(cardBackActor == null))
		{
			if (overriden)
			{
				cardBackActor.DisableMissingCardEffect();
			}
			else
			{
				cardBackActor.SetMissingCardMaterial(m_sepiaCardMaterial);
				cardBackActor.MissingCardEffect();
			}
			cardBackActor.UpdateAllComponents();
		}
	}
}
