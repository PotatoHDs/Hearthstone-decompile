using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200031B RID: 795
public class HistoryItem : MonoBehaviour
{
	// Token: 0x06002C80 RID: 11392 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void Awake()
	{
	}

	// Token: 0x06002C81 RID: 11393 RVA: 0x000E000E File Offset: 0x000DE20E
	protected virtual void OnDestroy()
	{
		DefLoader.DisposableCardDef cardDef = this.m_cardDef;
		if (cardDef != null)
		{
			cardDef.Dispose();
		}
		this.m_cardDef = null;
	}

	// Token: 0x06002C82 RID: 11394 RVA: 0x000E0028 File Offset: 0x000DE228
	public Entity GetEntity()
	{
		return this.m_entity;
	}

	// Token: 0x06002C83 RID: 11395 RVA: 0x000E0030 File Offset: 0x000DE230
	public Texture GetPortraitTexture()
	{
		return this.m_portraitTexture;
	}

	// Token: 0x06002C84 RID: 11396 RVA: 0x000E0038 File Offset: 0x000DE238
	public Material GetPortraitGoldenMaterial()
	{
		return this.m_portraitGoldenMaterial;
	}

	// Token: 0x06002C85 RID: 11397 RVA: 0x000E0040 File Offset: 0x000DE240
	public Collider GetTileCollider()
	{
		if (this.m_tileActor == null)
		{
			return null;
		}
		if (this.m_tileActor.GetMeshRenderer(false) == null)
		{
			return null;
		}
		Transform transform = this.m_tileActor.GetMeshRenderer(false).transform.Find("Collider");
		if (transform == null)
		{
			return null;
		}
		return transform.GetComponent<Collider>();
	}

	// Token: 0x06002C86 RID: 11398 RVA: 0x000E00A0 File Offset: 0x000DE2A0
	public bool IsMainCardActorInitialized()
	{
		return this.m_mainCardActorInitialized;
	}

	// Token: 0x06002C87 RID: 11399 RVA: 0x000E00A8 File Offset: 0x000DE2A8
	public void InitializeMainCardActor()
	{
		if (this.m_mainCardActorInitialized)
		{
			return;
		}
		this.m_mainCardActor.TurnOffCollider();
		this.m_mainCardActor.SetActorState(ActorStateType.CARD_HISTORY);
		this.m_mainCardActorInitialized = true;
	}

	// Token: 0x06002C88 RID: 11400 RVA: 0x000E00D4 File Offset: 0x000DE2D4
	public void DisplaySpells()
	{
		if (this.m_fatigue)
		{
			return;
		}
		if (this.m_burned)
		{
			this.DisplayFlameOnActor(this.m_mainCardActor);
			return;
		}
		if (!this.m_entity.IsCharacter() && !this.m_entity.IsWeapon())
		{
			return;
		}
		if (this.m_dead && !this.m_isPoisonous)
		{
			this.DisplaySkullOnActor(this.m_mainCardActor);
			return;
		}
		if (this.m_splatAmount != 0 || this.m_isPoisonous)
		{
			this.DisplaySplatOnActor(this.m_mainCardActor, this.m_splatAmount, this.m_isPoisonous);
		}
	}

	// Token: 0x06002C89 RID: 11401 RVA: 0x000E0160 File Offset: 0x000DE360
	private void DisplaySplatOnActor(Actor actor, int damage, bool isPoisonous)
	{
		Spell spell = actor.GetSpell(SpellType.DAMAGE);
		if (spell == null)
		{
			return;
		}
		DamageSplatSpell damageSplatSpell = (DamageSplatSpell)spell;
		damageSplatSpell.SetDamage(damage);
		damageSplatSpell.SetPoisonous(isPoisonous);
		damageSplatSpell.ActivateState(SpellStateType.IDLE);
		this.FadeHistoryOverlay(spell.gameObject);
	}

	// Token: 0x06002C8A RID: 11402 RVA: 0x000E01A8 File Offset: 0x000DE3A8
	private void DisplaySkullOnActor(Actor actor)
	{
		Spell spell = actor.GetSpell(SpellType.SKULL);
		if (spell == null)
		{
			return;
		}
		spell.ActivateState(SpellStateType.IDLE);
		this.FadeHistoryOverlay(spell.gameObject);
	}

	// Token: 0x06002C8B RID: 11403 RVA: 0x000E01DC File Offset: 0x000DE3DC
	private void DisplayFlameOnActor(Actor actor)
	{
		Spell spell = actor.GetSpell(SpellType.FLAME_SYMBOL);
		if (spell == null)
		{
			return;
		}
		spell.ActivateState(SpellStateType.IDLE);
		this.FadeHistoryOverlay(spell.gameObject);
	}

	// Token: 0x06002C8C RID: 11404 RVA: 0x000E020F File Offset: 0x000DE40F
	private void FadeHistoryOverlay(GameObject gameObject)
	{
		base.StopAllCoroutines();
		iTween.Stop(gameObject);
		base.StartCoroutine(this.FadeHistoryOverlayCoroutine(gameObject));
	}

	// Token: 0x06002C8D RID: 11405 RVA: 0x000E022B File Offset: 0x000DE42B
	private IEnumerator FadeHistoryOverlayCoroutine(GameObject gameObject)
	{
		iTween.FadeTo(gameObject, 1f, 0f);
		yield return new WaitForSeconds(1.5f);
		iTween.FadeTo(gameObject, 0f, 0.5f);
		yield break;
	}

	// Token: 0x06002C8E RID: 11406 RVA: 0x000E023A File Offset: 0x000DE43A
	protected void SetCardDef(DefLoader.DisposableCardDef cardDef)
	{
		DefLoader.DisposableCardDef cardDef2 = this.m_cardDef;
		if (cardDef2 != null)
		{
			cardDef2.Dispose();
		}
		this.m_cardDef = ((cardDef != null) ? cardDef.Share() : null);
	}

	// Token: 0x040018A9 RID: 6313
	public Actor m_tileActor;

	// Token: 0x040018AA RID: 6314
	public Actor m_mainCardActor;

	// Token: 0x040018AB RID: 6315
	protected bool m_dead;

	// Token: 0x040018AC RID: 6316
	protected bool m_burned;

	// Token: 0x040018AD RID: 6317
	protected bool m_isPoisonous;

	// Token: 0x040018AE RID: 6318
	protected int m_splatAmount;

	// Token: 0x040018AF RID: 6319
	protected Entity m_entity;

	// Token: 0x040018B0 RID: 6320
	protected Texture m_portraitTexture;

	// Token: 0x040018B1 RID: 6321
	protected Material m_portraitGoldenMaterial;

	// Token: 0x040018B2 RID: 6322
	protected DefLoader.DisposableCardDef m_cardDef;

	// Token: 0x040018B3 RID: 6323
	protected bool m_mainCardActorInitialized;

	// Token: 0x040018B4 RID: 6324
	protected bool m_fatigue;
}
