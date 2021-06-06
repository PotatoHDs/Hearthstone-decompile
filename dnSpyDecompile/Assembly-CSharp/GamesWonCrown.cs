using System;
using UnityEngine;

// Token: 0x020002CA RID: 714
[Serializable]
public class GamesWonCrown
{
	// Token: 0x060025BE RID: 9662 RVA: 0x000BDF3D File Offset: 0x000BC13D
	public void Show()
	{
		this.m_crown.SetActive(true);
	}

	// Token: 0x060025BF RID: 9663 RVA: 0x000BDF4B File Offset: 0x000BC14B
	public void Hide()
	{
		this.m_crown.SetActive(false);
	}

	// Token: 0x060025C0 RID: 9664 RVA: 0x000BDF59 File Offset: 0x000BC159
	public void Animate()
	{
		this.Show();
		this.m_crown.GetComponent<PlayMakerFSM>().SendEvent("Birth");
	}

	// Token: 0x04001524 RID: 5412
	public GameObject m_crown;
}
