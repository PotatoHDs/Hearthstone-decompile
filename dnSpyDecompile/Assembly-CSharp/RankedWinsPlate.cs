using System;

// Token: 0x020002AD RID: 685
public class RankedWinsPlate : PegUIElement
{
	// Token: 0x170004BE RID: 1214
	// (get) Token: 0x060023A4 RID: 9124 RVA: 0x000B1FF7 File Offset: 0x000B01F7
	// (set) Token: 0x060023A5 RID: 9125 RVA: 0x000B1FFF File Offset: 0x000B01FF
	public bool TooltipEnabled { get; set; }

	// Token: 0x170004BF RID: 1215
	// (get) Token: 0x060023A6 RID: 9126 RVA: 0x000B2008 File Offset: 0x000B0208
	// (set) Token: 0x060023A7 RID: 9127 RVA: 0x000B2010 File Offset: 0x000B0210
	public string TooltipString { get; set; }

	// Token: 0x060023A8 RID: 9128 RVA: 0x000B2019 File Offset: 0x000B0219
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		if (!this.TooltipEnabled)
		{
			return;
		}
		base.GetComponent<TooltipZone>().ShowTooltip(GameStrings.Get("GLUE_TOOLTIP_GOLDEN_WINS_HEADER"), this.TooltipString, 5f, 0);
	}

	// Token: 0x060023A9 RID: 9129 RVA: 0x0002B336 File Offset: 0x00029536
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		base.GetComponent<TooltipZone>().HideTooltip();
	}
}
