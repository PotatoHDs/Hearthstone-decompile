using System;
using UnityEngine;

// Token: 0x020002ED RID: 749
public class FiresideGatheringOpponentPickerFooter : MonoBehaviour
{
	// Token: 0x060027DD RID: 10205 RVA: 0x000C83A0 File Offset: 0x000C65A0
	public void SetTextOnFooter(bool listEmpty)
	{
		this.m_TitleText.Text = (listEmpty ? GameStrings.Get("GLUE_FIRESIDE_GATHERING_FOOTER_EMPTY_FIRST_LINE") : GameStrings.Get("GLUE_FIRESIDE_GATHERING_FOOTER_PARTIAL_FIRST_LINE"));
		this.m_BodyText.Text = (listEmpty ? GameStrings.Get("GLUE_FIRESIDE_GATHERING_FOOTER_EMPTY_SECOND_LINE") : GameStrings.Get("GLUE_FIRESIDE_GATHERING_FOOTER_PARTIAL_SECOND_LINE"));
	}

	// Token: 0x040016A1 RID: 5793
	public UberText m_TitleText;

	// Token: 0x040016A2 RID: 5794
	public UberText m_BodyText;

	// Token: 0x040016A3 RID: 5795
	private const string m_emptyListTitleText = "GLUE_FIRESIDE_GATHERING_FOOTER_EMPTY_FIRST_LINE";

	// Token: 0x040016A4 RID: 5796
	private const string m_emptyListBodyText = "GLUE_FIRESIDE_GATHERING_FOOTER_EMPTY_SECOND_LINE";

	// Token: 0x040016A5 RID: 5797
	private const string m_partialListTitleText = "GLUE_FIRESIDE_GATHERING_FOOTER_PARTIAL_FIRST_LINE";

	// Token: 0x040016A6 RID: 5798
	private const string m_partialListBodyText = "GLUE_FIRESIDE_GATHERING_FOOTER_PARTIAL_SECOND_LINE";
}
