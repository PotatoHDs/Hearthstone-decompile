using System;
using UnityEngine;

// Token: 0x0200089A RID: 2202
public class DOPAsset : ScriptableObject
{
	// Token: 0x0600792E RID: 31022 RVA: 0x00278237 File Offset: 0x00276437
	public static DOPAsset GenerateDOPAsset()
	{
		DOPAsset dopasset = ScriptableObject.CreateInstance<DOPAsset>();
		dopasset.DataVersion = 20400;
		return dopasset;
	}

	// Token: 0x04005E3A RID: 24122
	public int DataVersion;
}
