using System;

// Token: 0x020008C1 RID: 2241
public static class GameLayerExtensions
{
	// Token: 0x06007B73 RID: 31603 RVA: 0x00280C2A File Offset: 0x0027EE2A
	public static int LayerBit(this GameLayer gameLayer)
	{
		return 1 << (int)gameLayer;
	}
}
