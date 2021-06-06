using System;
using UnityEngine;

// Token: 0x02000B05 RID: 2821
[ExecuteInEditMode]
public class QuadUVSetter : MonoBehaviour
{
	// Token: 0x06009634 RID: 38452 RVA: 0x0030A3A3 File Offset: 0x003085A3
	private void Start()
	{
		this.SetUVs();
	}

	// Token: 0x06009635 RID: 38453 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void SetUVs()
	{
	}

	// Token: 0x06009636 RID: 38454 RVA: 0x0030A3A3 File Offset: 0x003085A3
	private void Update()
	{
		this.SetUVs();
	}

	// Token: 0x04007DE5 RID: 32229
	public Vector2 Origin;

	// Token: 0x04007DE6 RID: 32230
	public float Width;

	// Token: 0x04007DE7 RID: 32231
	public float Height;

	// Token: 0x04007DE8 RID: 32232
	public Vector2 ImageSize;
}
