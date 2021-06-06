using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200061D RID: 1565
public class PracticeAIButton : PegUIElement
{
	// Token: 0x060057B2 RID: 22450 RVA: 0x001CB12B File Offset: 0x001C932B
	public int GetMissionID()
	{
		return this.m_missionID;
	}

	// Token: 0x060057B3 RID: 22451 RVA: 0x001CB133 File Offset: 0x001C9333
	public long GetDeckID()
	{
		return this.m_deckID;
	}

	// Token: 0x060057B4 RID: 22452 RVA: 0x001CB13B File Offset: 0x001C933B
	public TAG_CLASS GetClass()
	{
		return this.m_class;
	}

	// Token: 0x060057B5 RID: 22453 RVA: 0x001CB143 File Offset: 0x001C9343
	public void PlayUnlockGlow()
	{
		this.m_unlockEffect.GetComponent<Animation>().Play("AITileGlow");
	}

	// Token: 0x060057B6 RID: 22454 RVA: 0x001CB15C File Offset: 0x001C935C
	public void Lock(bool locked)
	{
		this.m_locked = locked;
		float value = (float)(this.m_locked ? 1 : 0);
		bool enabled = !this.m_locked;
		this.SetEnabled(enabled, false);
		this.GetShowingMaterial().SetFloat("_Desaturate", value);
		this.m_rootObject.GetComponent<Renderer>().GetMaterial().SetFloat("_Desaturate", value);
	}

	// Token: 0x060057B7 RID: 22455 RVA: 0x001CB1BF File Offset: 0x001C93BF
	public void SetInfo(string name, TAG_CLASS buttonClass, DefLoader.DisposableCardDef cardDef, int missionID, bool flip)
	{
		this.SetInfo(name, buttonClass, cardDef, missionID, 0L, flip);
	}

	// Token: 0x060057B8 RID: 22456 RVA: 0x001CB1D0 File Offset: 0x001C93D0
	public void SetInfo(string name, TAG_CLASS buttonClass, DefLoader.DisposableCardDef cardDef, long deckID, bool flip)
	{
		this.SetInfo(name, buttonClass, cardDef, 0, deckID, flip);
	}

	// Token: 0x060057B9 RID: 22457 RVA: 0x001CB1E0 File Offset: 0x001C93E0
	public void CoverUp(bool flip)
	{
		this.m_covered = true;
		if (flip)
		{
			this.GetHiddenNameMesh().Text = "";
			this.GetHiddenCover().GetComponent<Renderer>().enabled = true;
			this.Flip();
		}
		else
		{
			this.GetShowingNameMesh().Text = "";
			this.GetShowingCover().GetComponent<Renderer>().enabled = true;
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.m_coveredBone.localPosition,
			"time",
			0.25f,
			"isLocal",
			true,
			"easeType",
			iTween.EaseType.linear
		});
		iTween.MoveTo(this.m_rootObject, args);
		this.SetEnabled(false, false);
	}

	// Token: 0x060057BA RID: 22458 RVA: 0x001CB2B4 File Offset: 0x001C94B4
	public void Select()
	{
		SoundManager.Get().LoadAndPlay("select_AI_opponent.prefab:a48887f01f79fa743a0c5de53a959b60", base.gameObject);
		this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		this.SetEnabled(false, false);
		this.Depress();
	}

	// Token: 0x060057BB RID: 22459 RVA: 0x001CB2EC File Offset: 0x001C94EC
	public void Deselect()
	{
		this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		if (this.m_covered)
		{
			return;
		}
		this.Raise();
		if (this.m_locked)
		{
			return;
		}
		this.SetEnabled(true, false);
	}

	// Token: 0x060057BC RID: 22460 RVA: 0x001CB31C File Offset: 0x001C951C
	public void Raise()
	{
		this.Raise(0.1f);
	}

	// Token: 0x060057BD RID: 22461 RVA: 0x001CB329 File Offset: 0x001C9529
	public void ShowQuestBang(bool shown)
	{
		this.m_questBang.SetActive(shown);
	}

	// Token: 0x060057BE RID: 22462 RVA: 0x001CB337 File Offset: 0x001C9537
	private void Flip()
	{
		base.StopCoroutine(this.FLIP_COROUTINE);
		this.m_usingBackside = !this.m_usingBackside;
		base.StartCoroutine(this.FLIP_COROUTINE, this.m_usingBackside);
	}

	// Token: 0x060057BF RID: 22463 RVA: 0x001CB36C File Offset: 0x001C956C
	private IEnumerator WaitThenFlip(bool flipToBackside)
	{
		iTween.StopByName(base.gameObject, "flip");
		yield return new WaitForEndOfFrame();
		float x = flipToBackside ? 0f : 180f;
		this.m_rootObject.transform.localEulerAngles = new Vector3(x, 0f, 0f);
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			new Vector3(180f, 0f, 0f),
			"time",
			0.25f,
			"easeType",
			iTween.EaseType.easeOutElastic,
			"space",
			Space.Self,
			"name",
			"flip"
		});
		iTween.RotateAdd(this.m_rootObject, args);
		float x2 = flipToBackside ? 180f : 0f;
		this.m_highlight.transform.localEulerAngles = new Vector3(x2, 0f, 0f);
		this.m_unlockEffect.transform.localPosition = (flipToBackside ? this.GLOW_QUAD_FLIPPED_LOCAL_POS : this.GLOW_QUAD_NORMAL_LOCAL_POS);
		yield break;
	}

	// Token: 0x060057C0 RID: 22464 RVA: 0x001CB382 File Offset: 0x001C9582
	private UberText GetShowingNameMesh()
	{
		if (!this.m_usingBackside)
		{
			return this.m_name;
		}
		return this.m_backsideName;
	}

	// Token: 0x060057C1 RID: 22465 RVA: 0x001CB399 File Offset: 0x001C9599
	private UberText GetHiddenNameMesh()
	{
		if (!this.m_usingBackside)
		{
			return this.m_backsideName;
		}
		return this.m_name;
	}

	// Token: 0x060057C2 RID: 22466 RVA: 0x001CB3B0 File Offset: 0x001C95B0
	private Material GetShowingMaterial()
	{
		int materialIndex = this.m_usingBackside ? 2 : 1;
		return this.m_rootObject.GetComponent<Renderer>().GetMaterial(materialIndex);
	}

	// Token: 0x060057C3 RID: 22467 RVA: 0x001CB3DC File Offset: 0x001C95DC
	private void SetShowingMaterial(Material mat)
	{
		int materialIndex = this.m_usingBackside ? 2 : 1;
		this.m_rootObject.GetComponent<Renderer>().SetMaterial(materialIndex, mat);
	}

	// Token: 0x060057C4 RID: 22468 RVA: 0x001CB408 File Offset: 0x001C9608
	private Material GetHiddenMaterial()
	{
		int materialIndex = this.m_usingBackside ? 1 : 2;
		return this.m_rootObject.GetComponent<Renderer>().GetMaterial(materialIndex);
	}

	// Token: 0x060057C5 RID: 22469 RVA: 0x001CB434 File Offset: 0x001C9634
	private void SetHiddenMaterial(Material mat)
	{
		int materialIndex = this.m_usingBackside ? 1 : 2;
		this.m_rootObject.GetComponent<Renderer>().SetMaterial(materialIndex, mat);
	}

	// Token: 0x060057C6 RID: 22470 RVA: 0x001CB460 File Offset: 0x001C9660
	private GameObject GetShowingCover()
	{
		if (!this.m_usingBackside)
		{
			return this.m_frontCover;
		}
		return this.m_backsideCover;
	}

	// Token: 0x060057C7 RID: 22471 RVA: 0x001CB477 File Offset: 0x001C9677
	private GameObject GetHiddenCover()
	{
		if (!this.m_usingBackside)
		{
			return this.m_backsideCover;
		}
		return this.m_frontCover;
	}

	// Token: 0x060057C8 RID: 22472 RVA: 0x001CB490 File Offset: 0x001C9690
	private void SetInfo(string name, TAG_CLASS buttonClass, DefLoader.DisposableCardDef cardDef, int missionID, long deckID, bool flip)
	{
		this.SetMissionID(missionID);
		this.SetDeckID(deckID);
		this.SetButtonClass(buttonClass);
		DefLoader.DisposableCardDef cardDef2 = this.m_cardDef;
		if (cardDef2 != null)
		{
			cardDef2.Dispose();
		}
		this.m_cardDef = cardDef;
		Material practiceAIPortrait = this.m_cardDef.CardDef.GetPracticeAIPortrait();
		if (flip)
		{
			this.GetHiddenNameMesh().Text = name;
			if (practiceAIPortrait != null)
			{
				this.SetHiddenMaterial(practiceAIPortrait);
			}
			this.Flip();
		}
		else
		{
			if (this.m_infoSet)
			{
				Debug.LogWarning("PracticeAIButton.SetInfo() - button is being re-initialized!");
			}
			this.m_infoSet = true;
			if (practiceAIPortrait != null)
			{
				this.SetShowingMaterial(practiceAIPortrait);
			}
			this.GetShowingNameMesh().Text = name;
			base.SetOriginalLocalPosition();
		}
		this.m_covered = false;
		this.GetShowingCover().GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x060057C9 RID: 22473 RVA: 0x001CB558 File Offset: 0x001C9758
	private void SetMissionID(int missionID)
	{
		this.m_missionID = missionID;
	}

	// Token: 0x060057CA RID: 22474 RVA: 0x001CB561 File Offset: 0x001C9761
	private void SetDeckID(long deckID)
	{
		this.m_deckID = deckID;
	}

	// Token: 0x060057CB RID: 22475 RVA: 0x001CB56A File Offset: 0x001C976A
	private void SetButtonClass(TAG_CLASS buttonClass)
	{
		this.m_class = buttonClass;
	}

	// Token: 0x060057CC RID: 22476 RVA: 0x001CB574 File Offset: 0x001C9774
	private void Raise(float time)
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.m_upBone.localPosition,
			"time",
			time,
			"easeType",
			iTween.EaseType.linear,
			"isLocal",
			true
		});
		iTween.MoveTo(this.m_rootObject, args);
	}

	// Token: 0x060057CD RID: 22477 RVA: 0x001CB5E8 File Offset: 0x001C97E8
	private void Depress()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.m_downBone.localPosition,
			"time",
			0.1f,
			"easeType",
			iTween.EaseType.linear,
			"isLocal",
			true
		});
		iTween.MoveTo(this.m_rootObject, args);
	}

	// Token: 0x060057CE RID: 22478 RVA: 0x001CB660 File Offset: 0x001C9860
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		SoundManager.Get().LoadAndPlay("collection_manager_hero_mouse_over.prefab:653cc8000b988cd468d2210a209adce6", base.gameObject);
		this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_MOUSE_OVER);
	}

	// Token: 0x060057CF RID: 22479 RVA: 0x001CB68A File Offset: 0x001C988A
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
	}

	// Token: 0x060057D0 RID: 22480 RVA: 0x001CB69A File Offset: 0x001C989A
	protected override void OnDestroy()
	{
		DefLoader.DisposableCardDef cardDef = this.m_cardDef;
		if (cardDef != null)
		{
			cardDef.Dispose();
		}
		this.m_cardDef = null;
		base.OnDestroy();
	}

	// Token: 0x04004B40 RID: 19264
	public UberText m_name;

	// Token: 0x04004B41 RID: 19265
	public UberText m_backsideName;

	// Token: 0x04004B42 RID: 19266
	public GameObject m_frontCover;

	// Token: 0x04004B43 RID: 19267
	public GameObject m_backsideCover;

	// Token: 0x04004B44 RID: 19268
	public HighlightState m_highlight;

	// Token: 0x04004B45 RID: 19269
	public GameObject m_unlockEffect;

	// Token: 0x04004B46 RID: 19270
	public GameObject m_questBang;

	// Token: 0x04004B47 RID: 19271
	public int m_PortraitMaterialIdx = -1;

	// Token: 0x04004B48 RID: 19272
	public GameObject m_rootObject;

	// Token: 0x04004B49 RID: 19273
	public Transform m_upBone;

	// Token: 0x04004B4A RID: 19274
	public Transform m_downBone;

	// Token: 0x04004B4B RID: 19275
	public Transform m_coveredBone;

	// Token: 0x04004B4C RID: 19276
	private int m_missionID;

	// Token: 0x04004B4D RID: 19277
	private long m_deckID;

	// Token: 0x04004B4E RID: 19278
	private bool m_covered;

	// Token: 0x04004B4F RID: 19279
	private bool m_locked;

	// Token: 0x04004B50 RID: 19280
	private bool m_infoSet;

	// Token: 0x04004B51 RID: 19281
	private bool m_usingBackside;

	// Token: 0x04004B52 RID: 19282
	private TAG_CLASS m_class;

	// Token: 0x04004B53 RID: 19283
	private DefLoader.DisposableCardDef m_cardDef;

	// Token: 0x04004B54 RID: 19284
	private const float FLIPPED_X_ROTATION = 180f;

	// Token: 0x04004B55 RID: 19285
	private const float NORMAL_X_ROTATION = 0f;

	// Token: 0x04004B56 RID: 19286
	private readonly string FLIP_COROUTINE = "WaitThenFlip";

	// Token: 0x04004B57 RID: 19287
	private readonly Vector3 GLOW_QUAD_NORMAL_LOCAL_POS = new Vector3(-0.1953466f, 1.336676f, 0.00721521f);

	// Token: 0x04004B58 RID: 19288
	private readonly Vector3 GLOW_QUAD_FLIPPED_LOCAL_POS = new Vector3(-0.1953466f, -1.336676f, 0.00721521f);
}
