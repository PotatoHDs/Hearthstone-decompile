using System;
using UnityEngine;

// Token: 0x02000A47 RID: 2631
public class LightPhase : MonoBehaviour
{
	// Token: 0x06008E4E RID: 36430 RVA: 0x002DE6E0 File Offset: 0x002DC8E0
	public void Update()
	{
		float time = Time.time;
		if (time - this.lastTargetTimestamp > this.timeToWaitForNewTarget)
		{
			this.targetIntensity = UnityEngine.Random.Range(this.minPower, this.maxPower);
			this.lastTargetTimestamp = time;
		}
		float num = this.targetIntensity - base.GetComponent<Light>().intensity;
		float num2 = num / Mathf.Abs(num);
		if (base.GetComponent<Light>().intensity != this.targetIntensity)
		{
			base.GetComponent<Light>().intensity = base.GetComponent<Light>().intensity + num2 * this.speed;
		}
	}

	// Token: 0x04007652 RID: 30290
	public float duration = 1f;

	// Token: 0x04007653 RID: 30291
	public float minPower = 3f;

	// Token: 0x04007654 RID: 30292
	public float maxPower = 8f;

	// Token: 0x04007655 RID: 30293
	public float speed = 0.01f;

	// Token: 0x04007656 RID: 30294
	private float targetIntensity;

	// Token: 0x04007657 RID: 30295
	private float lastTargetTimestamp;

	// Token: 0x04007658 RID: 30296
	private float timeToWaitForNewTarget = 1f;
}
