using System;
using UnityEngine;

// Token: 0x02000138 RID: 312
public class PagingArrow : MonoBehaviour
{
	// Token: 0x06001473 RID: 5235 RVA: 0x0007574E File Offset: 0x0007394E
	public void ShowHighlight()
	{
		this.m_pagingArrowHighlight.SetActive(true);
	}

	// Token: 0x06001474 RID: 5236 RVA: 0x0007575C File Offset: 0x0007395C
	public void HideHighlight()
	{
		this.m_pagingArrowHighlight.SetActive(false);
	}

	// Token: 0x04000DA7 RID: 3495
	public GameObject m_pagingArrowHighlight;
}
