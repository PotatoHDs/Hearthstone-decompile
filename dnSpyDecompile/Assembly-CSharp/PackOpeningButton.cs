using System;
using UnityEngine;

// Token: 0x020000D4 RID: 212
public class PackOpeningButton : BoxMenuButton
{
	// Token: 0x06000C90 RID: 3216 RVA: 0x0004961A File Offset: 0x0004781A
	public string GetGetPackCount()
	{
		return this.m_count.Text;
	}

	// Token: 0x06000C91 RID: 3217 RVA: 0x00049627 File Offset: 0x00047827
	public void SetPackCount(int packs)
	{
		if (packs < 0)
		{
			this.m_count.Text = "";
			return;
		}
		this.m_count.Text = GameStrings.Format("GLUE_PACK_OPENING_BOOSTER_COUNT", new object[]
		{
			packs
		});
	}

	// Token: 0x040008C0 RID: 2240
	public UberText m_count;

	// Token: 0x040008C1 RID: 2241
	public GameObject m_countFrame;
}
