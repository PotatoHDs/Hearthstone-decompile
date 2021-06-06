using System;

// Token: 0x020009FC RID: 2556
public struct WorldDimensionIndex
{
	// Token: 0x06008A41 RID: 35393 RVA: 0x002C4C10 File Offset: 0x002C2E10
	public WorldDimensionIndex(float dimension, int index)
	{
		this.Dimension = dimension;
		this.Index = index;
	}

	// Token: 0x04007387 RID: 29575
	public float Dimension;

	// Token: 0x04007388 RID: 29576
	public int Index;
}
