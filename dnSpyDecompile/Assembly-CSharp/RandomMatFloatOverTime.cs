using System;
using UnityEngine;

// Token: 0x02000A67 RID: 2663
public class RandomMatFloatOverTime : MonoBehaviour
{
	// Token: 0x06008EFD RID: 36605 RVA: 0x002E3797 File Offset: 0x002E1997
	private void Start()
	{
		if (this.m_sync)
		{
			this.random = this.m_syncSeed;
		}
		else
		{
			this.random = UnityEngine.Random.Range(0f, 65535f);
		}
		this.m_renderer = base.GetComponent<Renderer>();
	}

	// Token: 0x06008EFE RID: 36606 RVA: 0x002E37D0 File Offset: 0x002E19D0
	private void Update()
	{
		float t = Mathf.PerlinNoise(this.random, Time.time * this.m_timeScale);
		this.m_renderer.GetMaterial(this.m_matIndex).SetFloat(this.m_property, Mathf.Lerp(this.minIntensity, this.maxIntensity, t));
	}

	// Token: 0x04007774 RID: 30580
	public float minIntensity = 0.25f;

	// Token: 0x04007775 RID: 30581
	public float maxIntensity = 0.5f;

	// Token: 0x04007776 RID: 30582
	public float m_timeScale = 1f;

	// Token: 0x04007777 RID: 30583
	public string m_property;

	// Token: 0x04007778 RID: 30584
	public int m_matIndex;

	// Token: 0x04007779 RID: 30585
	public bool m_sync;

	// Token: 0x0400777A RID: 30586
	public float m_syncSeed;

	// Token: 0x0400777B RID: 30587
	private float random;

	// Token: 0x0400777C RID: 30588
	private Renderer m_renderer;
}
