using System;
using UnityEngine;

// Token: 0x020009FB RID: 2555
public class OrientedBounds
{
	// Token: 0x06008A3F RID: 35391 RVA: 0x002C4BFD File Offset: 0x002C2DFD
	public Vector3 GetTrueCenterPosition()
	{
		return this.Origin + this.CenterOffset;
	}

	// Token: 0x04007384 RID: 29572
	public Vector3[] Extents;

	// Token: 0x04007385 RID: 29573
	public Vector3 Origin;

	// Token: 0x04007386 RID: 29574
	public Vector3 CenterOffset;
}
