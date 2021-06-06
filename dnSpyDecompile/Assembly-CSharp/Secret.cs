using System;
using UnityEngine;

// Token: 0x020006DD RID: 1757
public class Secret : MonoBehaviour
{
	// Token: 0x06006222 RID: 25122 RVA: 0x0020069D File Offset: 0x001FE89D
	private void Start()
	{
		this.secretLabelTop.SetGameStringText("GAMEPLAY_SECRET_BANNER_TITLE");
		this.secretLabelMiddle.SetGameStringText("GAMEPLAY_SECRET_BANNER_TITLE");
		this.secretLabelBottom.SetGameStringText("GAMEPLAY_SECRET_BANNER_TITLE");
	}

	// Token: 0x040051A1 RID: 20897
	public UberText secretLabelTop;

	// Token: 0x040051A2 RID: 20898
	public UberText secretLabelMiddle;

	// Token: 0x040051A3 RID: 20899
	public UberText secretLabelBottom;
}
