using System;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000626 RID: 1574
[RequireComponent(typeof(WidgetTemplate))]
public class RewardTrackResetPopup : MonoBehaviour
{
	// Token: 0x06005824 RID: 22564 RVA: 0x001CCF1C File Offset: 0x001CB11C
	private void Awake()
	{
		this.m_widget = base.GetComponent<WidgetTemplate>();
		this.m_headerText.Text = GameStrings.Format("GLUE_PROGRESSION_REWARD_TRACK_POPUP_NEW_TRACK_TITLE", Array.Empty<object>());
	}

	// Token: 0x04004B97 RID: 19351
	public UberText m_headerText;

	// Token: 0x04004B98 RID: 19352
	private Widget m_widget;
}
