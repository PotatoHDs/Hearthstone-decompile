using System;
using UnityEngine;

// Token: 0x02000A91 RID: 2705
public class ShakeObject : MonoBehaviour
{
	// Token: 0x060090A3 RID: 37027 RVA: 0x002EEA52 File Offset: 0x002ECC52
	private void Start()
	{
		this.orgPos = base.transform.position;
	}

	// Token: 0x060090A4 RID: 37028 RVA: 0x002EEA68 File Offset: 0x002ECC68
	private void Update()
	{
		float num = UnityEngine.Random.value * this.amount;
		float num2 = UnityEngine.Random.value * this.amount;
		float num3 = UnityEngine.Random.value * this.amount;
		num *= this.amount;
		num2 *= this.amount;
		num3 *= this.amount;
		base.transform.position = this.orgPos + new Vector3(num, num2, num3);
	}

	// Token: 0x0400796A RID: 31082
	public float amount = 1f;

	// Token: 0x0400796B RID: 31083
	private Vector3 orgPos;
}
