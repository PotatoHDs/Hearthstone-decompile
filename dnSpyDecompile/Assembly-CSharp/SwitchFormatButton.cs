using System;
using System.Collections;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x020002AE RID: 686
public class SwitchFormatButton : UIBButton
{
	// Token: 0x060023AB RID: 9131 RVA: 0x000B2048 File Offset: 0x000B0248
	private void UpdateIcon()
	{
		if (this.m_isCovered)
		{
			this.m_visualController.SetState("COVERED");
			return;
		}
		switch (this.m_visualsFormatType)
		{
		case VisualsFormatType.VFT_WILD:
			this.m_visualController.SetState("WILD");
			return;
		case VisualsFormatType.VFT_STANDARD:
			this.m_visualController.SetState("STANDARD");
			return;
		case VisualsFormatType.VFT_CLASSIC:
			this.m_visualController.SetState("CLASSIC");
			return;
		case VisualsFormatType.VFT_CASUAL:
			this.m_visualController.SetState("CASUAL");
			return;
		default:
			return;
		}
	}

	// Token: 0x060023AC RID: 9132 RVA: 0x000B20D6 File Offset: 0x000B02D6
	public void SetVisualsFormatType(VisualsFormatType newVisualsFormatType)
	{
		if (this.m_visualsFormatType != newVisualsFormatType)
		{
			this.m_visualsFormatType = newVisualsFormatType;
			this.UpdateIcon();
		}
	}

	// Token: 0x060023AD RID: 9133 RVA: 0x000B20EE File Offset: 0x000B02EE
	public void Disable()
	{
		this.m_uibHighlight.Reset();
		this.SetEnabled(false, false);
	}

	// Token: 0x060023AE RID: 9134 RVA: 0x000B2103 File Offset: 0x000B0303
	public void Enable()
	{
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		this.SetEnabled(true, false);
		this.UpdateIcon();
	}

	// Token: 0x060023AF RID: 9135 RVA: 0x000B212C File Offset: 0x000B032C
	public IEnumerator EnableWithDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		this.SetEnabled(true, false);
		this.UpdateIcon();
		yield break;
	}

	// Token: 0x060023B0 RID: 9136 RVA: 0x000B2142 File Offset: 0x000B0342
	public void Cover()
	{
		this.m_isCovered = true;
		this.UpdateIcon();
	}

	// Token: 0x060023B1 RID: 9137 RVA: 0x000B2151 File Offset: 0x000B0351
	public void Uncover()
	{
		this.m_isCovered = false;
		this.UpdateIcon();
	}

	// Token: 0x060023B2 RID: 9138 RVA: 0x000B2160 File Offset: 0x000B0360
	public bool IsCovered()
	{
		return this.m_isCovered;
	}

	// Token: 0x060023B3 RID: 9139 RVA: 0x000B2168 File Offset: 0x000B0368
	public void EnableHighlight(bool enabled)
	{
		this.EnableHighlightImpl(enabled);
	}

	// Token: 0x060023B4 RID: 9140 RVA: 0x000B2171 File Offset: 0x000B0371
	private void EnableHighlightImpl(bool enabled)
	{
		if (enabled)
		{
			this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			return;
		}
		this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
	}

	// Token: 0x040013CA RID: 5066
	public MeshRenderer m_buttonRenderer;

	// Token: 0x040013CB RID: 5067
	public HighlightState m_highlight;

	// Token: 0x040013CC RID: 5068
	public GameObject m_coverObject;

	// Token: 0x040013CD RID: 5069
	public UIBHighlight m_uibHighlight;

	// Token: 0x040013CE RID: 5070
	public VisualController m_visualController;

	// Token: 0x040013CF RID: 5071
	private VisualsFormatType m_visualsFormatType = VisualsFormatType.VFT_STANDARD;

	// Token: 0x040013D0 RID: 5072
	private bool m_isCovered;

	// Token: 0x040013D1 RID: 5073
	private const string STANDARD_STATE = "STANDARD";

	// Token: 0x040013D2 RID: 5074
	private const string CLASSIC_STATE = "CLASSIC";

	// Token: 0x040013D3 RID: 5075
	private const string WILD_STATE = "WILD";

	// Token: 0x040013D4 RID: 5076
	private const string CASUAL_STATE = "CASUAL";

	// Token: 0x040013D5 RID: 5077
	private const string COVERED_STATE = "COVERED";
}
