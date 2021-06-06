using System;
using UnityEngine;

// Token: 0x02000ADA RID: 2778
public class PegUIContainer : MonoBehaviour
{
	// Token: 0x06009406 RID: 37894 RVA: 0x00300F58 File Offset: 0x002FF158
	public void SetActive(bool a)
	{
		if (a != base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(a);
		}
	}

	// Token: 0x04007C38 RID: 31800
	public bool isActive = true;
}
