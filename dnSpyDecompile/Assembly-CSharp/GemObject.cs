using System;
using UnityEngine;

// Token: 0x020008CE RID: 2254
public class GemObject : MonoBehaviour
{
	// Token: 0x06007CB8 RID: 31928 RVA: 0x002892D9 File Offset: 0x002874D9
	private void Awake()
	{
		this.startingScale = base.transform.localScale;
	}

	// Token: 0x06007CB9 RID: 31929 RVA: 0x002892EC File Offset: 0x002874EC
	public void Enlarge(float scaleFactor)
	{
		iTween.Stop(base.gameObject);
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			new Vector3(this.startingScale.x * scaleFactor, this.startingScale.y * scaleFactor, this.startingScale.z * scaleFactor),
			"time",
			0.5f,
			"easetype",
			iTween.EaseType.easeOutElastic
		}));
	}

	// Token: 0x06007CBA RID: 31930 RVA: 0x0028937D File Offset: 0x0028757D
	public void Shrink()
	{
		iTween.ScaleTo(base.gameObject, this.startingScale, 0.5f);
	}

	// Token: 0x06007CBB RID: 31931 RVA: 0x00289395 File Offset: 0x00287595
	public void ImmediatelyScaleToZero()
	{
		base.transform.localScale = Vector3.zero;
	}

	// Token: 0x06007CBC RID: 31932 RVA: 0x002893A7 File Offset: 0x002875A7
	public void ScaleToZero()
	{
		iTween.Stop(base.gameObject);
		iTween.ScaleTo(base.gameObject, Vector3.zero, 0.5f);
	}

	// Token: 0x06007CBD RID: 31933 RVA: 0x002893C9 File Offset: 0x002875C9
	public void SetToZeroThenEnlarge()
	{
		base.transform.localScale = Vector3.zero;
		this.Enlarge(1f);
	}

	// Token: 0x06007CBE RID: 31934 RVA: 0x00028167 File Offset: 0x00026367
	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06007CBF RID: 31935 RVA: 0x00028159 File Offset: 0x00026359
	public void Show()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06007CC0 RID: 31936 RVA: 0x002893E6 File Offset: 0x002875E6
	public void Initialize()
	{
		this.initialized = true;
	}

	// Token: 0x06007CC1 RID: 31937 RVA: 0x002893EF File Offset: 0x002875EF
	public void SetHideNumberFlag(bool enable)
	{
		this.m_hiddenFlag = enable;
	}

	// Token: 0x06007CC2 RID: 31938 RVA: 0x002893F8 File Offset: 0x002875F8
	public bool IsNumberHidden()
	{
		return this.m_hiddenFlag;
	}

	// Token: 0x06007CC3 RID: 31939 RVA: 0x00289400 File Offset: 0x00287600
	public void Jiggle()
	{
		if (!this.initialized)
		{
			this.initialized = true;
			return;
		}
		iTween.Stop(base.gameObject);
		base.transform.localScale = this.startingScale;
		iTween.PunchScale(base.gameObject, new Vector3(this.jiggleAmount, this.jiggleAmount, this.jiggleAmount), 1f);
	}

	// Token: 0x0400656E RID: 25966
	public Vector3 startingScale;

	// Token: 0x0400656F RID: 25967
	public float jiggleAmount;

	// Token: 0x04006570 RID: 25968
	private bool initialized;

	// Token: 0x04006571 RID: 25969
	private bool m_hiddenFlag;
}
