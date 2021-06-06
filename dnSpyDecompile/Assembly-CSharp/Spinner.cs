using System;
using UnityEngine;

// Token: 0x02000A94 RID: 2708
public class Spinner : MonoBehaviour
{
	// Token: 0x060090B3 RID: 37043 RVA: 0x002EEDCB File Offset: 0x002ECFCB
	public void Update()
	{
		base.transform.Rotate(Vector3.right, Time.deltaTime * this.SpeedX);
		base.transform.Rotate(Vector3.up, Time.deltaTime * this.SpeedY, Space.World);
	}

	// Token: 0x0400797C RID: 31100
	public float SpeedX;

	// Token: 0x0400797D RID: 31101
	public float SpeedY;
}
