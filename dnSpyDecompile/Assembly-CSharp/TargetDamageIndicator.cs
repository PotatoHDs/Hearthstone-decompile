using System;
using UnityEngine;

// Token: 0x02000347 RID: 839
public class TargetDamageIndicator : MonoBehaviour
{
	// Token: 0x060030A7 RID: 12455 RVA: 0x000FA371 File Offset: 0x000F8571
	public void SetText(string newText)
	{
		this.m_indicatorText.Text = (newText ?? string.Empty);
	}

	// Token: 0x060030A8 RID: 12456 RVA: 0x000FA388 File Offset: 0x000F8588
	public void Show(bool active)
	{
		this.m_indicatorText.gameObject.SetActive(active);
		this.m_targetArrowBang.SetActive(active);
	}

	// Token: 0x04001B02 RID: 6914
	public UberText m_indicatorText;

	// Token: 0x04001B03 RID: 6915
	public GameObject m_targetArrowBang;
}
