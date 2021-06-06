using System;
using UnityEngine;

// Token: 0x02000722 RID: 1826
public class StoreDisclaimer : MonoBehaviour
{
	// Token: 0x060065D1 RID: 26065 RVA: 0x00211D09 File Offset: 0x0020FF09
	private void Awake()
	{
		this.m_headlineText.Text = GameStrings.Get("GLUE_STORE_DISCLAIMER_HEADLINE");
		this.m_warningText.Text = GameStrings.Get("GLUE_STORE_DISCLAIMER_WARNING");
		this.m_detailsText.Text = "";
	}

	// Token: 0x060065D2 RID: 26066 RVA: 0x00211D45 File Offset: 0x0020FF45
	public void UpdateTextSize()
	{
		this.m_headlineText.UpdateNow(false);
		this.m_warningText.UpdateNow(false);
		this.m_detailsText.UpdateNow(false);
	}

	// Token: 0x060065D3 RID: 26067 RVA: 0x00211D6B File Offset: 0x0020FF6B
	public void SetDetailsText(string detailsText)
	{
		this.m_detailsText.Text = detailsText;
	}

	// Token: 0x0400546C RID: 21612
	public UberText m_headlineText;

	// Token: 0x0400546D RID: 21613
	public UberText m_warningText;

	// Token: 0x0400546E RID: 21614
	public UberText m_detailsText;

	// Token: 0x0400546F RID: 21615
	public GameObject m_root;
}
