public class ZoneGameModeButton : Zone
{
	public int m_ButtonSlot;

	public override string ToString()
	{
		return $"{base.ToString()} (Game Mode Button)";
	}

	public override bool CanAcceptTags(int controllerId, TAG_ZONE zoneTag, TAG_CARDTYPE cardType, Entity entity)
	{
		if (!base.CanAcceptTags(controllerId, zoneTag, cardType, entity))
		{
			return false;
		}
		if (cardType != TAG_CARDTYPE.GAME_MODE_BUTTON)
		{
			return false;
		}
		if (entity == null)
		{
			return false;
		}
		if (entity.GetTag(GAME_TAG.GAME_MODE_BUTTON_SLOT) != m_ButtonSlot)
		{
			return false;
		}
		return true;
	}
}
