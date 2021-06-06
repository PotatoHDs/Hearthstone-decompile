using System;
using UnityEngine;

// Token: 0x02000636 RID: 1590
public class QuestProgressToast : GameToast
{
	// Token: 0x06005988 RID: 22920 RVA: 0x001D2F85 File Offset: 0x001D1185
	private void Awake()
	{
		this.m_intensityMaterials.Add(this.m_questProgressCountBg.GetComponent<Renderer>().GetMaterial());
		this.m_intensityMaterials.Add(this.m_background.GetComponent<Renderer>().GetMaterial());
	}

	// Token: 0x06005989 RID: 22921 RVA: 0x001D2FC0 File Offset: 0x001D11C0
	public void UpdateDisplay(string title, string description, int progress, int maxProgress)
	{
		if (maxProgress > 1)
		{
			this.m_questProgressCountBg.SetActive(true);
			this.m_questProgressCount.Text = GameStrings.Format("GLOBAL_QUEST_PROGRESS_COUNT", new object[]
			{
				progress,
				maxProgress
			});
		}
		else
		{
			this.m_questProgressCountBg.SetActive(false);
		}
		this.m_questTitle.Text = title;
		this.m_questDescription.Text = description;
	}

	// Token: 0x04004C8C RID: 19596
	public UberText m_questTitle;

	// Token: 0x04004C8D RID: 19597
	public UberText m_questDescription;

	// Token: 0x04004C8E RID: 19598
	public UberText m_questProgressCount;

	// Token: 0x04004C8F RID: 19599
	public GameObject m_questProgressCountBg;

	// Token: 0x04004C90 RID: 19600
	public GameObject m_background;
}
