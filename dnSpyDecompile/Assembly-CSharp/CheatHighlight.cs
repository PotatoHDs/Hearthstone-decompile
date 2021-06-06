using System;
using UnityEngine;

// Token: 0x02000B40 RID: 2880
public class CheatHighlight : MonoBehaviour
{
	// Token: 0x0600989A RID: 39066 RVA: 0x00316605 File Offset: 0x00314805
	private void OnMouseEnter()
	{
		this.m_highlight.SetActive(true);
	}

	// Token: 0x0600989B RID: 39067 RVA: 0x00316613 File Offset: 0x00314813
	private void OnMouseExit()
	{
		this.m_highlight.SetActive(false);
	}

	// Token: 0x04007FA7 RID: 32679
	[SerializeField]
	public GameObject m_highlight;
}
