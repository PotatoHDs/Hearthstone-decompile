using System;
using UnityEngine;

// Token: 0x020002EF RID: 751
public class FiresideGatheringPlayButtonLantern : MonoBehaviour
{
	// Token: 0x06002806 RID: 10246 RVA: 0x000C9178 File Offset: 0x000C7378
	public void SetLanternLit(bool lit)
	{
		this.LitLantern.SetActive(lit);
		this.UnlitLantern.SetActive(!lit);
	}

	// Token: 0x040016C0 RID: 5824
	public GameObject LitLantern;

	// Token: 0x040016C1 RID: 5825
	public GameObject UnlitLantern;
}
