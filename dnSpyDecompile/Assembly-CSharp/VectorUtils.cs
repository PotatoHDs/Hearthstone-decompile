using System;
using UnityEngine;

// Token: 0x02000A02 RID: 2562
public static class VectorUtils
{
	// Token: 0x06008B01 RID: 35585 RVA: 0x002C769F File Offset: 0x002C589F
	public static Vector2 Abs(Vector2 vector)
	{
		return new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
	}

	// Token: 0x06008B02 RID: 35586 RVA: 0x002C76BC File Offset: 0x002C58BC
	public static Vector2 CreateFromAngle(float degrees)
	{
		float f = 0.017453292f * degrees;
		return new Vector2(Mathf.Cos(f), Mathf.Sin(f));
	}

	// Token: 0x06008B03 RID: 35587 RVA: 0x002C76E2 File Offset: 0x002C58E2
	public static Vector3 Abs(Vector3 vector)
	{
		return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
	}
}
