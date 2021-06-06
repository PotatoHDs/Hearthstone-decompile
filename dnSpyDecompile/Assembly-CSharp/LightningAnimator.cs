using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A46 RID: 2630
[CustomEditClass]
public class LightningAnimator : MonoBehaviour
{
	// Token: 0x06008E48 RID: 36424 RVA: 0x002DE520 File Offset: 0x002DC720
	private void Start()
	{
		this.m_material = base.GetComponent<Renderer>().GetMaterial();
		if (this.m_material == null)
		{
			base.enabled = false;
		}
		if (this.m_SetAlphaToZeroOnStart)
		{
			Color color = this.m_material.color;
			color.a = 0f;
			this.m_material.color = color;
		}
		if (this.m_material.HasProperty("_GlowIntensity"))
		{
			this.m_matGlowIntensity = this.m_material.GetFloat("_GlowIntensity");
		}
	}

	// Token: 0x06008E49 RID: 36425 RVA: 0x002DE5A7 File Offset: 0x002DC7A7
	private void OnEnable()
	{
		if (this.m_StartOnEnable)
		{
			this.StartAnimation();
		}
	}

	// Token: 0x06008E4A RID: 36426 RVA: 0x002DE5B7 File Offset: 0x002DC7B7
	public void StartAnimation()
	{
		base.StartCoroutine(this.AnimateMaterial());
	}

	// Token: 0x06008E4B RID: 36427 RVA: 0x002DE5C6 File Offset: 0x002DC7C6
	private IEnumerator AnimateMaterial()
	{
		this.RandomJointRotation();
		Color matColor = this.m_material.color;
		matColor.a = 0f;
		this.m_material.color = matColor;
		yield return new WaitForSeconds(UnityEngine.Random.Range(this.m_StartDelayMin, this.m_StartDelayMax));
		matColor = this.m_material.color;
		matColor.a = 1f;
		this.m_material.color = matColor;
		if (this.m_material.HasProperty("_GlowIntensity"))
		{
			this.m_material.SetFloat("_GlowIntensity", this.m_matGlowIntensity);
		}
		foreach (int num in this.m_FrameList)
		{
			this.m_material.SetFloat(this.m_MatFrameProperty, (float)num);
			yield return new WaitForSeconds(this.m_FrameTime);
		}
		List<int>.Enumerator enumerator = default(List<int>.Enumerator);
		matColor.a = 0f;
		this.m_material.color = matColor;
		if (this.m_material.HasProperty("_GlowIntensity"))
		{
			this.m_material.SetFloat("_GlowIntensity", 0f);
		}
		yield break;
		yield break;
	}

	// Token: 0x06008E4C RID: 36428 RVA: 0x002DE5D8 File Offset: 0x002DC7D8
	private void RandomJointRotation()
	{
		if (this.m_SourceJount != null)
		{
			Vector3 eulers = Vector3.Lerp(this.m_SourceMinRotation, this.m_SourceMaxRotation, UnityEngine.Random.value);
			this.m_SourceJount.Rotate(eulers);
		}
		if (this.m_TargetJoint != null)
		{
			Vector3 eulers2 = Vector3.Lerp(this.m_TargetMinRotation, this.m_TargetMaxRotation, UnityEngine.Random.value);
			this.m_TargetJoint.Rotate(eulers2);
		}
	}

	// Token: 0x04007643 RID: 30275
	public bool m_StartOnEnable;

	// Token: 0x04007644 RID: 30276
	public bool m_SetAlphaToZeroOnStart = true;

	// Token: 0x04007645 RID: 30277
	public float m_StartDelayMin;

	// Token: 0x04007646 RID: 30278
	public float m_StartDelayMax;

	// Token: 0x04007647 RID: 30279
	public string m_MatFrameProperty = "_Frame";

	// Token: 0x04007648 RID: 30280
	public float m_FrameTime = 0.01f;

	// Token: 0x04007649 RID: 30281
	public List<int> m_FrameList;

	// Token: 0x0400764A RID: 30282
	public Transform m_SourceJount;

	// Token: 0x0400764B RID: 30283
	public Vector3 m_SourceMinRotation = new Vector3(0f, -10f, 0f);

	// Token: 0x0400764C RID: 30284
	public Vector3 m_SourceMaxRotation = new Vector3(0f, 10f, 0f);

	// Token: 0x0400764D RID: 30285
	public Transform m_TargetJoint;

	// Token: 0x0400764E RID: 30286
	public Vector3 m_TargetMinRotation = new Vector3(0f, -20f, 0f);

	// Token: 0x0400764F RID: 30287
	public Vector3 m_TargetMaxRotation = new Vector3(0f, 20f, 0f);

	// Token: 0x04007650 RID: 30288
	private Material m_material;

	// Token: 0x04007651 RID: 30289
	private float m_matGlowIntensity;
}
