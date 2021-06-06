using System;
using System.Collections;
using UnityEngine;

// Token: 0x020006D9 RID: 1753
public class DamageSplatSpell : Spell
{
	// Token: 0x0600620B RID: 25099 RVA: 0x00200215 File Offset: 0x001FE415
	protected override void Awake()
	{
		base.Awake();
		this.EnableAllRenderers(false);
	}

	// Token: 0x0600620C RID: 25100 RVA: 0x00200224 File Offset: 0x001FE424
	public float GetDamage()
	{
		return (float)this.m_damage;
	}

	// Token: 0x0600620D RID: 25101 RVA: 0x0020022D File Offset: 0x001FE42D
	public void SetDamage(int damage)
	{
		this.m_damage = damage;
	}

	// Token: 0x0600620E RID: 25102 RVA: 0x00200236 File Offset: 0x001FE436
	public void SetPoisonous(bool isPoisonous)
	{
		this.m_poison = isPoisonous;
		this.m_DamageTextMesh.gameObject.SetActive(!this.m_poison);
	}

	// Token: 0x0600620F RID: 25103 RVA: 0x00200258 File Offset: 0x001FE458
	public bool IsPoisonous()
	{
		return this.m_poison;
	}

	// Token: 0x06006210 RID: 25104 RVA: 0x00200260 File Offset: 0x001FE460
	public void DoSplatAnims()
	{
		base.StopAllCoroutines();
		iTween.Stop(base.gameObject);
		base.StartCoroutine(this.SplatAnimCoroutine());
	}

	// Token: 0x06006211 RID: 25105 RVA: 0x00200280 File Offset: 0x001FE480
	private IEnumerator SplatAnimCoroutine()
	{
		this.UpdateElements();
		base.transform.localScale = Vector3.zero;
		yield return null;
		this.OnSpellFinished();
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			Vector3.one,
			"time",
			1f,
			"easetype",
			iTween.EaseType.easeOutElastic
		}));
		float seconds = 2f;
		if (this.IsPoisonous())
		{
			seconds = 0.8f;
		}
		yield return new WaitForSeconds(seconds);
		iTween.FadeTo(base.gameObject, 0f, 1f);
		yield return new WaitForSeconds(1.1f);
		this.EnableAllRenderers(false);
		this.OnStateFinished();
		yield break;
	}

	// Token: 0x06006212 RID: 25106 RVA: 0x0020028F File Offset: 0x001FE48F
	protected override void OnIdle(SpellStateType prevStateType)
	{
		base.StopAllCoroutines();
		this.UpdateElements();
		base.OnIdle(prevStateType);
	}

	// Token: 0x06006213 RID: 25107 RVA: 0x002002A4 File Offset: 0x001FE4A4
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.UpdateElements();
		base.OnAction(prevStateType);
		this.DoSplatAnims();
	}

	// Token: 0x06006214 RID: 25108 RVA: 0x002002B9 File Offset: 0x001FE4B9
	protected override void OnNone(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		this.m_activeSplat = null;
	}

	// Token: 0x06006215 RID: 25109 RVA: 0x002002CC File Offset: 0x001FE4CC
	protected override void ShowImpl()
	{
		base.ShowImpl();
		if (this.m_activeSplat == null)
		{
			return;
		}
		SceneUtils.EnableRenderers(this.m_activeSplat.gameObject, true);
		this.m_DamageTextMesh.gameObject.SetActive(!this.m_poison);
	}

	// Token: 0x06006216 RID: 25110 RVA: 0x00200318 File Offset: 0x001FE518
	protected override void HideImpl()
	{
		base.HideImpl();
		base.StopAllCoroutines();
		iTween.Stop(base.gameObject);
		this.EnableAllRenderers(false);
	}

	// Token: 0x06006217 RID: 25111 RVA: 0x00200338 File Offset: 0x001FE538
	private void UpdateElements()
	{
		iTween.Stop(base.gameObject);
		iTween.FadeTo(base.gameObject, 1f, 0f);
		if (this.m_damage < 0)
		{
			this.m_DamageTextMesh.Text = string.Format("+{0}", Mathf.Abs(this.m_damage));
			this.m_activeSplat = this.m_HealSplat;
			SceneUtils.EnableRenderers(this.m_BloodSplat.gameObject, false);
			if (this.m_PoisonSplat != null)
			{
				SceneUtils.EnableRenderers(this.m_PoisonSplat.gameObject, false);
			}
			SceneUtils.EnableRenderers(this.m_HealSplat.gameObject, true);
			this.m_DamageTextMesh.gameObject.SetActive(true);
			return;
		}
		if (this.m_poison && this.m_PoisonSplat != null)
		{
			this.m_DamageTextMesh.Text = string.Format("-{0}", 0);
			this.m_activeSplat = this.m_PoisonSplat;
			SceneUtils.EnableRenderers(this.m_BloodSplat.gameObject, false);
			SceneUtils.EnableRenderers(this.m_PoisonSplat.gameObject, true);
			SceneUtils.EnableRenderers(this.m_HealSplat.gameObject, false);
			this.m_DamageTextMesh.gameObject.SetActive(false);
			return;
		}
		this.m_DamageTextMesh.Text = string.Format("-{0}", this.m_damage);
		this.m_activeSplat = this.m_BloodSplat;
		SceneUtils.EnableRenderers(this.m_BloodSplat.gameObject, true);
		if (this.m_PoisonSplat != null)
		{
			SceneUtils.EnableRenderers(this.m_PoisonSplat.gameObject, false);
		}
		SceneUtils.EnableRenderers(this.m_HealSplat.gameObject, false);
		this.m_DamageTextMesh.gameObject.SetActive(true);
	}

	// Token: 0x06006218 RID: 25112 RVA: 0x002004F8 File Offset: 0x001FE6F8
	private void EnableAllRenderers(bool enabled)
	{
		SceneUtils.EnableRenderers(this.m_BloodSplat.gameObject, enabled);
		SceneUtils.EnableRenderers(this.m_HealSplat.gameObject, enabled);
		if (this.m_PoisonSplat != null)
		{
			SceneUtils.EnableRenderers(this.m_PoisonSplat.gameObject, enabled);
		}
		this.m_DamageTextMesh.gameObject.SetActive(enabled);
	}

	// Token: 0x04005198 RID: 20888
	public GameObject m_BloodSplat;

	// Token: 0x04005199 RID: 20889
	public GameObject m_PoisonSplat;

	// Token: 0x0400519A RID: 20890
	public GameObject m_HealSplat;

	// Token: 0x0400519B RID: 20891
	public UberText m_DamageTextMesh;

	// Token: 0x0400519C RID: 20892
	private GameObject m_activeSplat;

	// Token: 0x0400519D RID: 20893
	private int m_damage;

	// Token: 0x0400519E RID: 20894
	private bool m_poison;

	// Token: 0x0400519F RID: 20895
	private const float SCALE_IN_TIME = 1f;
}
