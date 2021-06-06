using System;
using UnityEngine;

// Token: 0x020000D1 RID: 209
public class BoxSpinner : MonoBehaviour
{
	// Token: 0x06000C78 RID: 3192 RVA: 0x000492A8 File Offset: 0x000474A8
	private void Awake()
	{
		this.m_spinnerMat = base.GetComponent<Renderer>().GetMaterial();
	}

	// Token: 0x06000C79 RID: 3193 RVA: 0x000492BC File Offset: 0x000474BC
	private void Update()
	{
		if (!this.IsSpinning())
		{
			return;
		}
		this.m_spinnerMat.SetFloat("_RotAngle", this.m_spinY);
		this.m_spinY += this.m_info.m_DegreesPerSec * Time.deltaTime * 0.01f;
	}

	// Token: 0x06000C7A RID: 3194 RVA: 0x0004930C File Offset: 0x0004750C
	private void OnDestroy()
	{
		UnityEngine.Object.Destroy(this.m_spinnerMat);
	}

	// Token: 0x06000C7B RID: 3195 RVA: 0x00049319 File Offset: 0x00047519
	public Box GetParent()
	{
		return this.m_parent;
	}

	// Token: 0x06000C7C RID: 3196 RVA: 0x00049321 File Offset: 0x00047521
	public void SetParent(Box parent)
	{
		this.m_parent = parent;
	}

	// Token: 0x06000C7D RID: 3197 RVA: 0x0004932A File Offset: 0x0004752A
	public BoxSpinnerStateInfo GetInfo()
	{
		return this.m_info;
	}

	// Token: 0x06000C7E RID: 3198 RVA: 0x00049332 File Offset: 0x00047532
	public void SetInfo(BoxSpinnerStateInfo info)
	{
		this.m_info = info;
	}

	// Token: 0x06000C7F RID: 3199 RVA: 0x0004933B File Offset: 0x0004753B
	public void Spin()
	{
		this.m_spinning = true;
	}

	// Token: 0x06000C80 RID: 3200 RVA: 0x00049344 File Offset: 0x00047544
	public bool IsSpinning()
	{
		return this.m_spinning;
	}

	// Token: 0x06000C81 RID: 3201 RVA: 0x0004934C File Offset: 0x0004754C
	public void Stop()
	{
		this.m_spinning = false;
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x00049355 File Offset: 0x00047555
	public void Reset()
	{
		this.m_spinning = false;
		this.m_spinnerMat.SetFloat("_RotAngle", 0f);
	}

	// Token: 0x040008AF RID: 2223
	private Box m_parent;

	// Token: 0x040008B0 RID: 2224
	private BoxSpinnerStateInfo m_info;

	// Token: 0x040008B1 RID: 2225
	private bool m_spinning;

	// Token: 0x040008B2 RID: 2226
	private float m_spinY;

	// Token: 0x040008B3 RID: 2227
	private Material m_spinnerMat;
}
