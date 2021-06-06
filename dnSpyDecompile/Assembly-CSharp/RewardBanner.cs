using System;
using UnityEngine;

// Token: 0x0200067B RID: 1659
public class RewardBanner : MonoBehaviour
{
	// Token: 0x06005CE4 RID: 23780 RVA: 0x001E167C File Offset: 0x001DF87C
	private void Awake()
	{
		if (UniversalInputManager.UsePhoneUI && this.m_sourceText != null)
		{
			this.m_sourceText.gameObject.SetActive(false);
		}
		this.m_headlineHeight = this.m_headlineText.Height;
	}

	// Token: 0x17000585 RID: 1413
	// (get) Token: 0x06005CE5 RID: 23781 RVA: 0x001E16BA File Offset: 0x001DF8BA
	public string HeadlineText
	{
		get
		{
			return this.m_headlineText.Text;
		}
	}

	// Token: 0x17000586 RID: 1414
	// (get) Token: 0x06005CE6 RID: 23782 RVA: 0x001E16C7 File Offset: 0x001DF8C7
	public string DetailsText
	{
		get
		{
			return this.m_detailsText.Text;
		}
	}

	// Token: 0x17000587 RID: 1415
	// (get) Token: 0x06005CE7 RID: 23783 RVA: 0x001E16D4 File Offset: 0x001DF8D4
	public string SourceText
	{
		get
		{
			return this.m_sourceText.Text;
		}
	}

	// Token: 0x06005CE8 RID: 23784 RVA: 0x001E16E4 File Offset: 0x001DF8E4
	public void SetText(string headline, string details, string source)
	{
		this.m_headlineText.Text = headline;
		this.m_detailsText.Text = details;
		this.m_sourceText.Text = source;
		if (details == "")
		{
			this.AlignHeadlineToCenterBone();
			this.m_headlineText.Height = this.m_headlineHeight * 1.5f;
		}
	}

	// Token: 0x06005CE9 RID: 23785 RVA: 0x001E173F File Offset: 0x001DF93F
	public void AlignHeadlineToCenterBone()
	{
		if (this.m_headlineCenterBone != null)
		{
			this.m_headlineText.transform.localPosition = this.m_headlineCenterBone.transform.localPosition;
		}
	}

	// Token: 0x04004EBC RID: 20156
	public UberText m_headlineText;

	// Token: 0x04004EBD RID: 20157
	public UberText m_detailsText;

	// Token: 0x04004EBE RID: 20158
	public UberText m_sourceText;

	// Token: 0x04004EBF RID: 20159
	public GameObject m_headlineCenterBone;

	// Token: 0x04004EC0 RID: 20160
	private float m_headlineHeight;
}
