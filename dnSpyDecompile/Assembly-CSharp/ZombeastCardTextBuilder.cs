using System;

// Token: 0x020007A8 RID: 1960
public class ZombeastCardTextBuilder : ModularEntityCardTextBuilder
{
	// Token: 0x06006CD2 RID: 27858 RVA: 0x00232B74 File Offset: 0x00230D74
	public override string GetRawCardTextInHandForCardBeingBuilt(Entity ent)
	{
		string value;
		string text;
		base.GetPowersText(ent, out value, out text);
		if (string.IsNullOrEmpty(value))
		{
			return "{1}";
		}
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand("ICC_828t");
		if (string.IsNullOrEmpty(rawCardTextInHand))
		{
			Log.All.PrintError("ZombeastCardTextBuilder.GetRawCardTextInHandForCardBeingBuilt: Could not find card text for ICC_828t.", Array.Empty<object>());
			return "{0}\n{1}";
		}
		return rawCardTextInHand;
	}
}
