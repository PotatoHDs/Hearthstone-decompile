using System;
using UnityEngine;

// Token: 0x02000668 RID: 1640
public class CardRewardCount : MonoBehaviour
{
	// Token: 0x06005C38 RID: 23608 RVA: 0x001DFADF File Offset: 0x001DDCDF
	private void Awake()
	{
		this.m_multiplierText.Text = GameStrings.Get("GLOBAL_REWARD_CARD_COUNT_MULTIPLIER");
	}

	// Token: 0x06005C39 RID: 23609 RVA: 0x00028159 File Offset: 0x00026359
	public void Show()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06005C3A RID: 23610 RVA: 0x00028167 File Offset: 0x00026367
	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06005C3B RID: 23611 RVA: 0x001DFAF6 File Offset: 0x001DDCF6
	public void SetCount(int count)
	{
		this.m_countText.Text = count.ToString();
	}

	// Token: 0x04004E73 RID: 20083
	public UberText m_countText;

	// Token: 0x04004E74 RID: 20084
	public UberText m_multiplierText;
}
