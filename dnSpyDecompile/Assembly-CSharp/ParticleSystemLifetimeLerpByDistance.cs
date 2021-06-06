using System;
using UnityEngine;

// Token: 0x02000811 RID: 2065
public class ParticleSystemLifetimeLerpByDistance : MonoBehaviour
{
	// Token: 0x06006F96 RID: 28566 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x06006F97 RID: 28567 RVA: 0x0023FC4C File Offset: 0x0023DE4C
	private void Update()
	{
		float value = Vector3.Distance(base.transform.position, this.targetObject.transform.position);
		foreach (ParticleSystemLifetimeLerpByDistance.ScaledObject scaledObject in this.properties)
		{
			scaledObject.component.main.startLifetime = Mathf.Lerp(scaledObject.startLifetimeMin, scaledObject.startLifetimeMax, (Mathf.Clamp(value, scaledObject.minDistance, scaledObject.maxDistance) - scaledObject.minDistance) / (scaledObject.maxDistance - scaledObject.minDistance));
		}
	}

	// Token: 0x0400597A RID: 22906
	public GameObject targetObject;

	// Token: 0x0400597B RID: 22907
	public ParticleSystemLifetimeLerpByDistance.ScaledObject[] properties;

	// Token: 0x020023CA RID: 9162
	[Serializable]
	public class ScaledObject
	{
		// Token: 0x0400E80C RID: 59404
		public ParticleSystem component;

		// Token: 0x0400E80D RID: 59405
		public float startLifetimeMin = 0.6f;

		// Token: 0x0400E80E RID: 59406
		public float startLifetimeMax = 1.2f;

		// Token: 0x0400E80F RID: 59407
		public float minDistance = 1f;

		// Token: 0x0400E810 RID: 59408
		public float maxDistance = 4f;
	}
}
