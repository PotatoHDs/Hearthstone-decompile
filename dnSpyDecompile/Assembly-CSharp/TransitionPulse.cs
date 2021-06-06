using System;
using UnityEngine;

// Token: 0x02000AA2 RID: 2722
public class TransitionPulse : MonoBehaviour
{
	// Token: 0x0600911F RID: 37151 RVA: 0x002F1110 File Offset: 0x002EF310
	private void Start()
	{
		this.m_interval = UnityEngine.Random.Range(this.frequencyMin, this.frequencyMax);
	}

	// Token: 0x06009120 RID: 37152 RVA: 0x002F112C File Offset: 0x002EF32C
	private void Update()
	{
		float value = Mathf.Sin(Time.time * this.m_interval) * this.magnitude;
		base.gameObject.GetComponent<Renderer>().GetMaterial().SetFloat("_Transistion", value);
	}

	// Token: 0x040079F3 RID: 31219
	public float frequencyMin = 0.0001f;

	// Token: 0x040079F4 RID: 31220
	public float frequencyMax = 1f;

	// Token: 0x040079F5 RID: 31221
	public float magnitude = 0.0001f;

	// Token: 0x040079F6 RID: 31222
	private float m_interval;
}
