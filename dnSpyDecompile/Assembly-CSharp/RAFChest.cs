using System;
using UnityEngine;

// Token: 0x0200063B RID: 1595
public class RAFChest : PegUIElement
{
	// Token: 0x060059DF RID: 23007 RVA: 0x001D571C File Offset: 0x001D391C
	public void SetOpen(bool isChestOpen)
	{
		if (this.m_isChestOpen != isChestOpen)
		{
			this.m_isChestOpen = isChestOpen;
			this.m_chestQuad.GetMaterial().SetTextureOffset("_MainTex", new Vector2(this.m_isChestOpen ? 0.5f : 0f, 0.5f));
			base.gameObject.GetComponent<UIBHighlight>().EnableResponse = !this.m_isChestOpen;
		}
	}

	// Token: 0x060059E0 RID: 23008 RVA: 0x001D5785 File Offset: 0x001D3985
	public bool IsOpen()
	{
		return this.m_isChestOpen;
	}

	// Token: 0x04004CEB RID: 19691
	public Renderer m_chestQuad;

	// Token: 0x04004CEC RID: 19692
	public GameObject m_tooltipBone;

	// Token: 0x04004CED RID: 19693
	private bool m_isChestOpen;
}
