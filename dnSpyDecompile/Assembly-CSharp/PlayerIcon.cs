using System;
using bgs;
using UnityEngine;

// Token: 0x020000A1 RID: 161
public class PlayerIcon : PegUIElement
{
	// Token: 0x06000A3C RID: 2620 RVA: 0x00039E60 File Offset: 0x00038060
	public void Hide()
	{
		this.m_hidden = true;
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x00039E75 File Offset: 0x00038075
	public void Show()
	{
		this.m_hidden = false;
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x00039E8A File Offset: 0x0003808A
	public BnetPlayer GetPlayer()
	{
		return this.m_player;
	}

	// Token: 0x06000A3F RID: 2623 RVA: 0x00039E92 File Offset: 0x00038092
	public bool SetPlayer(BnetPlayer player)
	{
		if (this.m_player == player)
		{
			return false;
		}
		this.m_player = player;
		this.UpdateIcon();
		return true;
	}

	// Token: 0x06000A40 RID: 2624 RVA: 0x00039EB0 File Offset: 0x000380B0
	public void UpdateIcon()
	{
		if (this.m_player == null)
		{
			return;
		}
		BnetProgramId bestProgramId = this.m_player.GetBestProgramId();
		bool flag = false;
		if (bestProgramId != null)
		{
			flag = bestProgramId.IsGame();
		}
		if (this.m_player.IsOnline() && flag)
		{
			if (!this.m_hidden)
			{
				base.gameObject.SetActive(true);
			}
			this.m_OnlinePortrait.SetProgramId(bestProgramId);
			return;
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400068E RID: 1678
	public GameObject m_OfflineIcon;

	// Token: 0x0400068F RID: 1679
	public GameObject m_OnlineIcon;

	// Token: 0x04000690 RID: 1680
	public PlayerPortrait m_OnlinePortrait;

	// Token: 0x04000691 RID: 1681
	private bool m_hidden;

	// Token: 0x04000692 RID: 1682
	private BnetPlayer m_player;
}
