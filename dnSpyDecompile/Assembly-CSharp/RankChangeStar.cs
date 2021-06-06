using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000644 RID: 1604
public class RankChangeStar : MonoBehaviour
{
	// Token: 0x06005A63 RID: 23139 RVA: 0x001D81A7 File Offset: 0x001D63A7
	public void BlackOut()
	{
		this.m_starMeshRenderer.enabled = false;
	}

	// Token: 0x06005A64 RID: 23140 RVA: 0x001D81B5 File Offset: 0x001D63B5
	public void UnBlackOut()
	{
		this.m_starMeshRenderer.enabled = true;
	}

	// Token: 0x06005A65 RID: 23141 RVA: 0x001D81C3 File Offset: 0x001D63C3
	public void FadeIn()
	{
		base.GetComponent<PlayMakerFSM>().SendEvent("FadeIn");
	}

	// Token: 0x06005A66 RID: 23142 RVA: 0x001D81D5 File Offset: 0x001D63D5
	public void Spawn()
	{
		base.GetComponent<PlayMakerFSM>().SendEvent("Spawn");
	}

	// Token: 0x06005A67 RID: 23143 RVA: 0x001D81E7 File Offset: 0x001D63E7
	public void Reset()
	{
		base.GetComponent<PlayMakerFSM>().SendEvent("Reset");
	}

	// Token: 0x06005A68 RID: 23144 RVA: 0x001D81F9 File Offset: 0x001D63F9
	public void Blink(float delay)
	{
		base.StartCoroutine(this.DelayedBlink(delay));
	}

	// Token: 0x06005A69 RID: 23145 RVA: 0x001D8209 File Offset: 0x001D6409
	public IEnumerator DelayedBlink(float delay)
	{
		yield return new WaitForSeconds(delay);
		base.GetComponent<PlayMakerFSM>().SendEvent("Blink");
		yield break;
	}

	// Token: 0x06005A6A RID: 23146 RVA: 0x001D821F File Offset: 0x001D641F
	public void Burst(float delay)
	{
		base.StartCoroutine(this.DelayedBurst(delay));
	}

	// Token: 0x06005A6B RID: 23147 RVA: 0x001D822F File Offset: 0x001D642F
	public IEnumerator DelayedBurst(float delay)
	{
		yield return new WaitForSeconds(delay);
		this.UnBlackOut();
		base.GetComponent<PlayMakerFSM>().SendEvent("Burst");
		yield break;
	}

	// Token: 0x06005A6C RID: 23148 RVA: 0x001D8245 File Offset: 0x001D6445
	public IEnumerator DelayedDespawn(float delay)
	{
		yield return new WaitForSeconds(delay);
		base.GetComponent<PlayMakerFSM>().SendEvent("DeSpawn");
		yield break;
	}

	// Token: 0x06005A6D RID: 23149 RVA: 0x001D825B File Offset: 0x001D645B
	public void Despawn()
	{
		base.GetComponent<PlayMakerFSM>().SendEvent("DeSpawn");
	}

	// Token: 0x06005A6E RID: 23150 RVA: 0x001D826D File Offset: 0x001D646D
	public void Wipe(float delay)
	{
		base.StartCoroutine(this.DelayedWipe(delay));
	}

	// Token: 0x06005A6F RID: 23151 RVA: 0x001D827D File Offset: 0x001D647D
	public IEnumerator DelayedWipe(float delay)
	{
		yield return new WaitForSeconds(delay);
		base.GetComponent<PlayMakerFSM>().SendEvent("Wipe");
		yield break;
	}

	// Token: 0x04004D44 RID: 19780
	public MeshRenderer m_starMeshRenderer;

	// Token: 0x04004D45 RID: 19781
	public MeshRenderer m_bottomGlowRenderer;

	// Token: 0x04004D46 RID: 19782
	public MeshRenderer m_topGlowRenderer;
}
