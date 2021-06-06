using System;
using UnityEngine;

// Token: 0x02000A4D RID: 2637
[RequireComponent(typeof(Material))]
public class MaterialSoftFlicker : MonoBehaviour
{
	// Token: 0x06008E66 RID: 36454 RVA: 0x002DF208 File Offset: 0x002DD408
	private void Start()
	{
		this.random = UnityEngine.Random.Range(0f, 65535f);
		this.m_renderer = base.gameObject.GetComponent<Renderer>();
	}

	// Token: 0x06008E67 RID: 36455 RVA: 0x002DF230 File Offset: 0x002DD430
	private void Update()
	{
		float t = Mathf.PerlinNoise(this.random, Time.time * this.m_timeScale);
		this.m_renderer.GetMaterial().SetColor("_TintColor", new Color(this.m_color.r, this.m_color.g, this.m_color.b, Mathf.Lerp(this.minIntensity, this.maxIntensity, t)));
	}

	// Token: 0x04007682 RID: 30338
	public float minIntensity = 0.25f;

	// Token: 0x04007683 RID: 30339
	public float maxIntensity = 0.5f;

	// Token: 0x04007684 RID: 30340
	public float m_timeScale = 1f;

	// Token: 0x04007685 RID: 30341
	public Color m_color = new Color(1f, 1f, 1f, 1f);

	// Token: 0x04007686 RID: 30342
	private float random;

	// Token: 0x04007687 RID: 30343
	private Renderer m_renderer;
}
