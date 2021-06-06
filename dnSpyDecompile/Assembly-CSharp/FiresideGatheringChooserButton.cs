using System;
using UnityEngine;

// Token: 0x020002E3 RID: 739
[CustomEditClass]
public class FiresideGatheringChooserButton : ChooserButton
{
	// Token: 0x060026A0 RID: 9888 RVA: 0x000C1E97 File Offset: 0x000C0097
	public new FiresideGatheringChooserSubButton CreateSubButton(string subButtonPrefab, bool useAsLastSelected)
	{
		return (FiresideGatheringChooserSubButton)base.CreateSubButton(subButtonPrefab, useAsLastSelected);
	}

	// Token: 0x060026A1 RID: 9889 RVA: 0x000C1EA6 File Offset: 0x000C00A6
	public void ShowLantern()
	{
		this.LanternMesh.SetActive(true);
	}

	// Token: 0x060026A2 RID: 9890 RVA: 0x000C1EB4 File Offset: 0x000C00B4
	public void ShowSwords()
	{
		this.SwordMesh.SetActive(true);
	}

	// Token: 0x040015EC RID: 5612
	public GameObject LanternMesh;

	// Token: 0x040015ED RID: 5613
	public GameObject SwordMesh;
}
