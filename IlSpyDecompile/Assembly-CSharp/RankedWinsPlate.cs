public class RankedWinsPlate : PegUIElement
{
	public bool TooltipEnabled { get; set; }

	public string TooltipString { get; set; }

	protected override void OnOver(InteractionState oldState)
	{
		if (TooltipEnabled)
		{
			GetComponent<TooltipZone>().ShowTooltip(GameStrings.Get("GLUE_TOOLTIP_GOLDEN_WINS_HEADER"), TooltipString, 5f);
		}
	}

	protected override void OnOut(InteractionState oldState)
	{
		GetComponent<TooltipZone>().HideTooltip();
	}
}
