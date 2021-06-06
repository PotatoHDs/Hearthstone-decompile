using System;
using UnityEngine;

// Token: 0x02000335 RID: 821
public class PlayerLeaderboardInformationPanel : MonoBehaviour
{
	// Token: 0x06002F1A RID: 12058 RVA: 0x000F015C File Offset: 0x000EE35C
	public void SetTitle(string text)
	{
		this.m_panelLabel.Text = text;
	}

	// Token: 0x04001A46 RID: 6726
	public UberText m_panelLabel;
}
