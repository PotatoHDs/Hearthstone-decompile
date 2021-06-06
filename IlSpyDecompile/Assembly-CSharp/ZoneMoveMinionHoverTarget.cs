public class ZoneMoveMinionHoverTarget : Zone
{
	public int m_Slot;

	public override string ToString()
	{
		return $"{base.ToString()} (Move Minion Hover Target)";
	}

	public override bool CanAcceptTags(int controllerId, TAG_ZONE zoneTag, TAG_CARDTYPE cardType, Entity entity)
	{
		if (!base.CanAcceptTags(controllerId, zoneTag, cardType, entity))
		{
			return false;
		}
		if (cardType != TAG_CARDTYPE.MOVE_MINION_HOVER_TARGET)
		{
			return false;
		}
		if (entity == null)
		{
			return false;
		}
		if (entity.GetTag(GAME_TAG.MOVE_MINION_HOVER_TARGET_SLOT) != m_Slot)
		{
			return false;
		}
		return true;
	}
}
