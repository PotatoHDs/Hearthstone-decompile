using System;
using UnityEngine;

// Token: 0x02000A7D RID: 2685
public class RotateAnim : MonoBehaviour
{
	// Token: 0x0600901C RID: 36892 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x0600901D RID: 36893 RVA: 0x002ECA90 File Offset: 0x002EAC90
	private void Update()
	{
		if (!this.gogogo)
		{
			return;
		}
		this.timePassed += Time.deltaTime;
		float num = this.timePassed;
		float num2 = this.startingAngle;
		float num3 = num2 - Quaternion.Angle(base.transform.rotation, this.targetRotation);
		float num4 = this.timeValue;
		float num5 = num3 * (-Mathf.Pow(2f, -10f * num / num4) + 1f) + num2;
		base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, this.targetRotation, num5 * Time.deltaTime);
		if (Quaternion.Angle(base.transform.rotation, this.targetRotation) <= Mathf.Epsilon)
		{
			this.gogogo = false;
			UnityEngine.Object.Destroy(this);
		}
	}

	// Token: 0x0600901E RID: 36894 RVA: 0x002ECB55 File Offset: 0x002EAD55
	public void SetTargetRotation(Vector3 target, float timeValueInput)
	{
		this.targetRotation = Quaternion.Euler(target);
		this.gogogo = true;
		this.timeValue = timeValueInput;
		this.startingAngle = Quaternion.Angle(base.transform.rotation, this.targetRotation);
	}

	// Token: 0x040078FE RID: 30974
	private Quaternion targetRotation;

	// Token: 0x040078FF RID: 30975
	private bool gogogo;

	// Token: 0x04007900 RID: 30976
	private float timeValue;

	// Token: 0x04007901 RID: 30977
	private float timePassed;

	// Token: 0x04007902 RID: 30978
	private float startingAngle;
}
