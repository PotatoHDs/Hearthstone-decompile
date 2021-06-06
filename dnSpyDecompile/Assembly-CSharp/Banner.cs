using System;
using UnityEngine;

// Token: 0x0200085F RID: 2143
public class Banner : MonoBehaviour
{
	// Token: 0x060073B2 RID: 29618 RVA: 0x0025224D File Offset: 0x0025044D
	public void SetText(string headline)
	{
		this.m_headline.Text = headline;
		this.m_caption.gameObject.SetActive(false);
	}

	// Token: 0x060073B3 RID: 29619 RVA: 0x0025226C File Offset: 0x0025046C
	public void SetText(string headline, string caption)
	{
		this.m_headline.Text = headline;
		this.m_caption.gameObject.SetActive(true);
		this.m_caption.Text = caption;
	}

	// Token: 0x060073B4 RID: 29620 RVA: 0x00252298 File Offset: 0x00250498
	public void MoveGlowForBottomPlacement()
	{
		this.m_glowObject.transform.localPosition = new Vector3(this.m_glowObject.transform.localPosition.x, this.m_glowObject.transform.localPosition.y, 0f);
	}

	// Token: 0x04005BEC RID: 23532
	public UberText m_headline;

	// Token: 0x04005BED RID: 23533
	public UberText m_caption;

	// Token: 0x04005BEE RID: 23534
	public GameObject m_glowObject;
}
