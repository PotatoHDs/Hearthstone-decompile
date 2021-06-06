using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200011E RID: 286
public class CraftingModeButton : UIBButton
{
	// Token: 0x060012F2 RID: 4850 RVA: 0x0006C95F File Offset: 0x0006AB5F
	public void ShowActiveGlow(bool show)
	{
		this.m_isGlowEnabled = show;
		this.m_activeGlow.SetActive(show);
	}

	// Token: 0x060012F3 RID: 4851 RVA: 0x0006C974 File Offset: 0x0006AB74
	public void ShowDustBottle(bool show)
	{
		this.m_showDustBottle = show;
		this.m_dustBottle.SetActive(show);
		if (show)
		{
			this.StartBottleJiggle();
		}
	}

	// Token: 0x060012F4 RID: 4852 RVA: 0x0006C992 File Offset: 0x0006AB92
	private void StartBottleJiggle()
	{
		if (this.m_jiggleCoroutine != null)
		{
			base.StopCoroutine(this.m_jiggleCoroutine);
			iTween.Stop(this.m_dustBottle.gameObject);
		}
		this.BottleJiggle();
	}

	// Token: 0x060012F5 RID: 4853 RVA: 0x0006C9BE File Offset: 0x0006ABBE
	private void BottleJiggle()
	{
		this.m_jiggleCoroutine = base.StartCoroutine(this.Jiggle());
	}

	// Token: 0x060012F6 RID: 4854 RVA: 0x0006C9D2 File Offset: 0x0006ABD2
	private IEnumerator Jiggle()
	{
		yield return new WaitForSeconds(1f);
		this.m_dustShower.Play();
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			this.m_jarJiggleRotation,
			"time",
			0.5f,
			"oncomplete",
			"BottleJiggle",
			"oncompletetarget",
			base.gameObject
		});
		iTween.PunchRotation(this.m_dustBottle.gameObject, args);
		yield break;
	}

	// Token: 0x060012F7 RID: 4855 RVA: 0x0006C9E4 File Offset: 0x0006ABE4
	public void Enable(bool enabled)
	{
		this.SetEnabled(enabled, false);
		this.m_activeGlow.SetActive(enabled && this.m_isGlowEnabled);
		this.m_textObject.SetActive(enabled);
		this.m_dustShower.gameObject.SetActive(enabled);
		if (enabled)
		{
			this.m_dustBottle.SetActive(this.m_showDustBottle);
		}
		else
		{
			this.m_dustBottle.SetActive(false);
		}
		this.m_mainMesh.SetSharedMaterial(enabled ? this.m_enabledMaterial : this.m_disabledMaterial);
	}

	// Token: 0x04000C34 RID: 3124
	public GameObject m_dustBottle;

	// Token: 0x04000C35 RID: 3125
	public GameObject m_activeGlow;

	// Token: 0x04000C36 RID: 3126
	public ParticleSystem m_dustShower;

	// Token: 0x04000C37 RID: 3127
	public Vector3 m_jarJiggleRotation = new Vector3(0f, 30f, 0f);

	// Token: 0x04000C38 RID: 3128
	public GameObject m_textObject;

	// Token: 0x04000C39 RID: 3129
	public MeshRenderer m_mainMesh;

	// Token: 0x04000C3A RID: 3130
	public Material m_enabledMaterial;

	// Token: 0x04000C3B RID: 3131
	public Material m_disabledMaterial;

	// Token: 0x04000C3C RID: 3132
	private bool m_isGlowEnabled;

	// Token: 0x04000C3D RID: 3133
	private bool m_showDustBottle;

	// Token: 0x04000C3E RID: 3134
	private bool m_isJiggling;

	// Token: 0x04000C3F RID: 3135
	private Coroutine m_jiggleCoroutine;
}
