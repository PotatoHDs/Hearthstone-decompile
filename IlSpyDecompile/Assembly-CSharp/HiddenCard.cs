public class HiddenCard : CardDef
{
	public override string DetermineActorPathForZone(Entity entity, TAG_ZONE zoneTag)
	{
		switch (zoneTag)
		{
		case TAG_ZONE.DECK:
		case TAG_ZONE.GRAVEYARD:
		case TAG_ZONE.REMOVEDFROMGAME:
		case TAG_ZONE.SETASIDE:
			return "Card_Invisible.prefab:579b3b9a80234754593f24582f9cb93b";
		case TAG_ZONE.SECRET:
			return base.DetermineActorPathForZone(entity, zoneTag);
		default:
			return "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9";
		}
	}
}
