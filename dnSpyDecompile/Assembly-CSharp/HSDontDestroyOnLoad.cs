using System;
using UnityEngine;

// Token: 0x02000686 RID: 1670
public class HSDontDestroyOnLoad : MonoBehaviour
{
	// Token: 0x06005D7A RID: 23930 RVA: 0x001E68E4 File Offset: 0x001E4AE4
	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}
}
