using System;
using UnityEngine;

// Token: 0x02000A2C RID: 2604
public class floatyObj2 : MonoBehaviour
{
	// Token: 0x06008BF1 RID: 35825 RVA: 0x002CCD2F File Offset: 0x002CAF2F
	private void Start()
	{
		this.m_interval = UnityEngine.Random.Range(this.frequencyMin, this.frequencyMax);
		this.m_rotationInterval = UnityEngine.Random.Range(this.frequencyMinRot, this.frequencyMaxRot);
	}

	// Token: 0x06008BF2 RID: 35826 RVA: 0x002CCD60 File Offset: 0x002CAF60
	private void Update()
	{
		float num = Mathf.Sin(Time.time * this.m_interval) * this.magnitude;
		base.transform.position += new Vector3(num, num, num);
		float num2 = Mathf.Sin(Time.time * this.m_rotationInterval) * this.magnitudeRot;
		base.transform.eulerAngles += new Vector3(num2, num2, num2);
	}

	// Token: 0x040074DB RID: 29915
	public float frequencyMin = 0.0001f;

	// Token: 0x040074DC RID: 29916
	public float frequencyMax = 0.001f;

	// Token: 0x040074DD RID: 29917
	public float magnitude = 0.0001f;

	// Token: 0x040074DE RID: 29918
	public float frequencyMinRot = 0.0001f;

	// Token: 0x040074DF RID: 29919
	public float frequencyMaxRot = 0.001f;

	// Token: 0x040074E0 RID: 29920
	public float magnitudeRot;

	// Token: 0x040074E1 RID: 29921
	private float m_interval;

	// Token: 0x040074E2 RID: 29922
	private float m_rotationInterval;
}
