using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200088D RID: 2189
[CustomEditClass]
public class DeckTrayCardBackContent : DeckTrayContent
{
	// Token: 0x170006F9 RID: 1785
	// (get) Token: 0x0600779F RID: 30623 RVA: 0x00270BCF File Offset: 0x0026EDCF
	public bool WaitingForCardbackAnimation
	{
		get
		{
			return this.m_animData != null || this.m_waitingToLoadCardback;
		}
	}

	// Token: 0x060077A0 RID: 30624 RVA: 0x00270BE4 File Offset: 0x0026EDE4
	protected override void Awake()
	{
		base.Awake();
		this.m_originalLocalPosition = base.transform.localPosition;
		base.transform.localPosition = this.m_originalLocalPosition + this.m_trayHiddenOffset;
		this.m_root.SetActive(false);
	}

	// Token: 0x060077A1 RID: 30625 RVA: 0x00270C30 File Offset: 0x0026EE30
	public void AnimateInNewCardBack(CardBackManager.LoadCardBackData cardBackData, GameObject original)
	{
		GameObject gameObject = cardBackData.m_GameObject;
		gameObject.GetComponent<Actor>().GetSpell(SpellType.DEATHREVERSE).Reactivate();
		DeckTrayCardBackContent.AnimatedCardBack animatedCardBack = new DeckTrayCardBackContent.AnimatedCardBack();
		animatedCardBack.CardBackId = cardBackData.m_CardBackIndex;
		animatedCardBack.GameObject = gameObject;
		animatedCardBack.OriginalScale = gameObject.transform.localScale;
		animatedCardBack.OriginalPosition = original.transform.position;
		this.m_animData = animatedCardBack;
		gameObject.transform.position = new Vector3(original.transform.position.x, original.transform.position.y + 0.5f, original.transform.position.z);
		gameObject.transform.localScale = this.m_cardBackContainer.transform.lossyScale;
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
			"AnimateNewCardUpdate",
			"onupdatetarget",
			base.gameObject,
			"oncomplete",
			"AnimateNewCardFinished",
			"oncompleteparams",
			animatedCardBack,
			"oncompletetarget",
			base.gameObject
		});
		iTween.ValueTo(gameObject, args);
		SoundManager.Get().LoadAndPlay("collection_manager_card_add_to_deck_instant.prefab:06df359c4026d7e47b06a4174f33e3ef", base.gameObject);
	}

	// Token: 0x060077A2 RID: 30626 RVA: 0x00270DCE File Offset: 0x0026EFCE
	private void AnimateNewCardFinished(DeckTrayCardBackContent.AnimatedCardBack cardBack)
	{
		cardBack.GameObject.transform.localScale = cardBack.OriginalScale;
		this.UpdateCardBack(cardBack.CardBackId, true, cardBack.GameObject);
		this.m_animData = null;
	}

	// Token: 0x060077A3 RID: 30627 RVA: 0x00270E00 File Offset: 0x0026F000
	private void AnimateNewCardUpdate(float val)
	{
		GameObject gameObject = this.m_animData.GameObject;
		Vector3 originalPosition = this.m_animData.OriginalPosition;
		Vector3 position = this.m_cardBackContainer.transform.position;
		if (val <= 0.85f)
		{
			val /= 0.85f;
			gameObject.transform.position = new Vector3(Mathf.Lerp(originalPosition.x, position.x, val), Mathf.Lerp(originalPosition.y, position.y, val) + Mathf.Sin(val * 3.1415927f) * 15f + val * 4f, Mathf.Lerp(originalPosition.z, position.z, val));
			return;
		}
		if (this.m_currentCardBack != null)
		{
			UnityEngine.Object.Destroy(this.m_currentCardBack);
			this.m_currentCardBack = null;
		}
		val = (val - 0.85f) / 0.14999998f;
		gameObject.transform.position = new Vector3(position.x, position.y + Mathf.Lerp(4f, 0f, val), position.z);
	}

	// Token: 0x060077A4 RID: 30628 RVA: 0x00270F0C File Offset: 0x0026F10C
	public bool SetNewCardBack(int cardBackId, GameObject original)
	{
		if (this.m_animData != null || this.m_waitingToLoadCardback)
		{
			return false;
		}
		this.m_waitingToLoadCardback = true;
		if (!CardBackManager.Get().LoadCardBackByIndex(cardBackId, delegate(CardBackManager.LoadCardBackData cardBackData)
		{
			this.m_waitingToLoadCardback = false;
			this.AnimateInNewCardBack(cardBackData, original);
		}, "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", null))
		{
			this.m_waitingToLoadCardback = false;
			Debug.LogError("Could not load CardBack " + cardBackId);
			return false;
		}
		return true;
	}

	// Token: 0x060077A5 RID: 30629 RVA: 0x00270F84 File Offset: 0x0026F184
	public void UpdateCardBack(int cardBackId, bool assigning, GameObject obj = null)
	{
		CollectionDeck currentDeck = CollectionManager.Get().GetEditedDeck();
		if (currentDeck == null)
		{
			return;
		}
		if (assigning)
		{
			if (!string.IsNullOrEmpty(this.m_socketSound))
			{
				SoundManager.Get().LoadAndPlay(this.m_socketSound);
			}
			currentDeck.CardBackOverridden = true;
		}
		currentDeck.CardBackID = cardBackId;
		if (obj != null)
		{
			this.SetCardBack(obj, currentDeck.CardBackOverridden, assigning);
			return;
		}
		this.m_waitingToLoadCardback = true;
		if (!CardBackManager.Get().LoadCardBackByIndex(cardBackId, delegate(CardBackManager.LoadCardBackData cardBackData)
		{
			if (currentDeck == null)
			{
				UnityEngine.Object.Destroy(cardBackData.m_GameObject);
				this.m_waitingToLoadCardback = false;
				return;
			}
			this.m_waitingToLoadCardback = false;
			GameObject gameObject = cardBackData.m_GameObject;
			this.SetCardBack(gameObject, currentDeck.CardBackOverridden, assigning);
		}, "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", null))
		{
			this.m_waitingToLoadCardback = false;
			Debug.LogWarning(string.Format("CardBackManager was unable to load card back ID: {0}", cardBackId));
		}
	}

	// Token: 0x060077A6 RID: 30630 RVA: 0x00271064 File Offset: 0x0026F264
	private void SetCardBack(GameObject go, bool overriden, bool assigning)
	{
		GameUtils.SetParent(go, this.m_cardBackContainer, true);
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
		if (this.m_currentCardBack != null)
		{
			UnityEngine.Object.Destroy(this.m_currentCardBack);
		}
		this.m_currentCardBack = go;
		GameObject cardMesh = component.m_cardMesh;
		component.SetCardbackUpdateIgnore(true);
		component.SetUnlit();
		this.UpdateMissingEffect(component, overriden);
		if (cardMesh != null)
		{
			Material material = cardMesh.GetComponent<Renderer>().GetMaterial();
			if (material.HasProperty("_SpecularIntensity"))
			{
				material.SetFloat("_SpecularIntensity", 0f);
			}
		}
	}

	// Token: 0x060077A7 RID: 30631 RVA: 0x00271120 File Offset: 0x0026F320
	public override bool PreAnimateContentEntrance()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		this.UpdateCardBack(editedDeck.CardBackID, false, null);
		return true;
	}

	// Token: 0x060077A8 RID: 30632 RVA: 0x00271148 File Offset: 0x0026F348
	public override bool AnimateContentEntranceStart()
	{
		if (this.m_waitingToLoadCardback)
		{
			return false;
		}
		this.m_root.SetActive(true);
		base.transform.localPosition = this.m_originalLocalPosition;
		this.m_animatingTray = true;
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
				this.m_animatingTray = false;
			})
		}));
		return true;
	}

	// Token: 0x060077A9 RID: 30633 RVA: 0x00271211 File Offset: 0x0026F411
	public override bool AnimateContentEntranceEnd()
	{
		return !this.m_animatingTray;
	}

	// Token: 0x060077AA RID: 30634 RVA: 0x0027121C File Offset: 0x0026F41C
	public override bool AnimateContentExitStart()
	{
		base.transform.localPosition = this.m_originalLocalPosition;
		this.m_animatingTray = true;
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
				this.m_animatingTray = false;
				this.m_root.SetActive(false);
			})
		}));
		return true;
	}

	// Token: 0x060077AB RID: 30635 RVA: 0x00271211 File Offset: 0x0026F411
	public override bool AnimateContentExitEnd()
	{
		return !this.m_animatingTray;
	}

	// Token: 0x060077AC RID: 30636 RVA: 0x002712CF File Offset: 0x0026F4CF
	private void UpdateMissingEffect(Actor cardBackActor, bool overriden)
	{
		if (cardBackActor == null)
		{
			return;
		}
		if (overriden)
		{
			cardBackActor.DisableMissingCardEffect();
		}
		else
		{
			cardBackActor.SetMissingCardMaterial(this.m_sepiaCardMaterial);
			cardBackActor.MissingCardEffect(true);
		}
		cardBackActor.UpdateAllComponents();
	}

	// Token: 0x04005DA3 RID: 23971
	[CustomEditField(Sections = "Positioning")]
	public GameObject m_root;

	// Token: 0x04005DA4 RID: 23972
	[CustomEditField(Sections = "Positioning")]
	public Vector3 m_trayHiddenOffset;

	// Token: 0x04005DA5 RID: 23973
	[CustomEditField(Sections = "Positioning")]
	public GameObject m_cardBackContainer;

	// Token: 0x04005DA6 RID: 23974
	[CustomEditField(Sections = "Animation & Sounds")]
	public iTween.EaseType m_traySlideSlideInAnimation = iTween.EaseType.easeOutBounce;

	// Token: 0x04005DA7 RID: 23975
	[CustomEditField(Sections = "Animation & Sounds")]
	public iTween.EaseType m_traySlideSlideOutAnimation;

	// Token: 0x04005DA8 RID: 23976
	[CustomEditField(Sections = "Animation & Sounds")]
	public float m_traySlideAnimationTime = 0.25f;

	// Token: 0x04005DA9 RID: 23977
	[CustomEditField(Sections = "Animation & Sounds", T = EditType.SOUND_PREFAB)]
	public string m_socketSound;

	// Token: 0x04005DAA RID: 23978
	[CustomEditField(Sections = "Card Effects")]
	public Material m_sepiaCardMaterial;

	// Token: 0x04005DAB RID: 23979
	private const string ADD_CARD_TO_DECK_SOUND = "collection_manager_card_add_to_deck_instant.prefab:06df359c4026d7e47b06a4174f33e3ef";

	// Token: 0x04005DAC RID: 23980
	private GameObject m_currentCardBack;

	// Token: 0x04005DAD RID: 23981
	private Vector3 m_originalLocalPosition;

	// Token: 0x04005DAE RID: 23982
	private bool m_animatingTray;

	// Token: 0x04005DAF RID: 23983
	private bool m_waitingToLoadCardback;

	// Token: 0x04005DB0 RID: 23984
	private DeckTrayCardBackContent.AnimatedCardBack m_animData;

	// Token: 0x020024D2 RID: 9426
	private class AnimatedCardBack
	{
		// Token: 0x0400EBDC RID: 60380
		public int CardBackId;

		// Token: 0x0400EBDD RID: 60381
		public GameObject GameObject;

		// Token: 0x0400EBDE RID: 60382
		public Vector3 OriginalScale;

		// Token: 0x0400EBDF RID: 60383
		public Vector3 OriginalPosition;
	}
}
