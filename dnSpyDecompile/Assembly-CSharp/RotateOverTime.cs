using System;
using UnityEngine;

// Token: 0x02000A7F RID: 2687
public class RotateOverTime : MonoBehaviour
{
	// Token: 0x06009022 RID: 36898 RVA: 0x002ECC68 File Offset: 0x002EAE68
	private void Start()
	{
		if (this.RandomStartX)
		{
			base.transform.Rotate(Vector3.left, (float)UnityEngine.Random.Range(0, 360));
		}
		if (this.RandomStartY)
		{
			base.transform.Rotate(Vector3.up, (float)UnityEngine.Random.Range(0, 360));
		}
		if (this.RandomStartZ)
		{
			base.transform.Rotate(Vector3.forward, (float)UnityEngine.Random.Range(0, 360));
		}
	}

	// Token: 0x06009023 RID: 36899 RVA: 0x002ECCE4 File Offset: 0x002EAEE4
	private void Update()
	{
		base.transform.Rotate(Vector3.left, Time.deltaTime * this.RotateSpeedX, Space.Self);
		base.transform.Rotate(Vector3.up, Time.deltaTime * this.RotateSpeedY, Space.Self);
		base.transform.Rotate(Vector3.forward, Time.deltaTime * this.RotateSpeedZ, Space.Self);
	}

	// Token: 0x04007905 RID: 30981
	public float RotateSpeedX;

	// Token: 0x04007906 RID: 30982
	public float RotateSpeedY;

	// Token: 0x04007907 RID: 30983
	public float RotateSpeedZ;

	// Token: 0x04007908 RID: 30984
	public bool RandomStartX;

	// Token: 0x04007909 RID: 30985
	public bool RandomStartY;

	// Token: 0x0400790A RID: 30986
	public bool RandomStartZ;
}
