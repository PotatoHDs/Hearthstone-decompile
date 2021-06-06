using System;
using UnityEngine;

// Token: 0x02000A2B RID: 2603
public class floatyObj : MonoBehaviour
{
	// Token: 0x06008BEE RID: 35822 RVA: 0x002CCC8C File Offset: 0x002CAE8C
	private void Start()
	{
		this.m_interval = UnityEngine.Random.Range(this.frequencyMin, this.frequencyMax);
	}

	// Token: 0x06008BEF RID: 35823 RVA: 0x002CCCA8 File Offset: 0x002CAEA8
	private void Update()
	{
		float num = Mathf.Sin(Time.time * this.m_interval) * this.magnitude;
		Vector3 b = new Vector3(num, num, num);
		base.transform.position += b;
		base.transform.eulerAngles += b;
	}

	// Token: 0x040074D7 RID: 29911
	public float frequencyMin = 0.0001f;

	// Token: 0x040074D8 RID: 29912
	public float frequencyMax = 0.001f;

	// Token: 0x040074D9 RID: 29913
	public float magnitude = 0.0001f;

	// Token: 0x040074DA RID: 29914
	private float m_interval;
}
