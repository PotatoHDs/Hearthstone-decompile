using System;
using UnityEngine;

// Token: 0x02000A80 RID: 2688
public class RotateOverTimePingPong : MonoBehaviour
{
	// Token: 0x06009025 RID: 36901 RVA: 0x002ECD48 File Offset: 0x002EAF48
	private void Start()
	{
		if (this.RandomStartX)
		{
			base.transform.Rotate(Vector3.left, UnityEngine.Random.Range(this.RotateRangeXmin, this.RotateRangeXmax));
		}
		if (this.RandomStartY)
		{
			base.transform.Rotate(Vector3.up, UnityEngine.Random.Range(this.RotateRangeYmin, this.RotateRangeYmax));
		}
		if (this.RandomStartZ)
		{
			base.transform.Rotate(Vector3.forward, UnityEngine.Random.Range(this.RotateRangeZmin, this.RotateRangeZmax));
		}
	}

	// Token: 0x06009026 RID: 36902 RVA: 0x002ECDD0 File Offset: 0x002EAFD0
	private void Update()
	{
		float z = Mathf.Sin(Time.time) * this.RotateRangeZmax;
		float y = base.gameObject.transform.localRotation.y;
		iTween.RotateUpdate(base.gameObject, iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, y, z),
			"isLocal",
			true,
			"time",
			0
		}));
	}

	// Token: 0x0400790B RID: 30987
	public float RotateSpeedX;

	// Token: 0x0400790C RID: 30988
	public float RotateSpeedY;

	// Token: 0x0400790D RID: 30989
	public float RotateSpeedZ;

	// Token: 0x0400790E RID: 30990
	public bool RandomStartX = true;

	// Token: 0x0400790F RID: 30991
	public bool RandomStartY = true;

	// Token: 0x04007910 RID: 30992
	public bool RandomStartZ = true;

	// Token: 0x04007911 RID: 30993
	public float RotateRangeXmin;

	// Token: 0x04007912 RID: 30994
	public float RotateRangeXmax = 10f;

	// Token: 0x04007913 RID: 30995
	public float RotateRangeYmin;

	// Token: 0x04007914 RID: 30996
	public float RotateRangeYmax = 10f;

	// Token: 0x04007915 RID: 30997
	public float RotateRangeZmin;

	// Token: 0x04007916 RID: 30998
	public float RotateRangeZmax = 10f;
}
