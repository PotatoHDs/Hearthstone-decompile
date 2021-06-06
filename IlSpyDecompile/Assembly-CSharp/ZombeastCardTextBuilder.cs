public class ZombeastCardTextBuilder : ModularEntityCardTextBuilder
{
	public override string GetRawCardTextInHandForCardBeingBuilt(Entity ent)
	{
		GetPowersText(ent, out var power, out var _);
		if (string.IsNullOrEmpty(power))
		{
			return "{1}";
		}
		string rawCardTextInHand = CardTextBuilder.GetRawCardTextInHand("ICC_828t");
		if (string.IsNullOrEmpty(rawCardTextInHand))
		{
			Log.All.PrintError("ZombeastCardTextBuilder.GetRawCardTextInHandForCardBeingBuilt: Could not find card text for ICC_828t.");
			return "{0}\n{1}";
		}
		return rawCardTextInHand;
	}
}
