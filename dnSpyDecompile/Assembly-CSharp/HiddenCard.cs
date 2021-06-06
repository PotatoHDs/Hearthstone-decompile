using System;

// Token: 0x020000E5 RID: 229
public class HiddenCard : CardDef
{
	// Token: 0x06000D3F RID: 3391 RVA: 0x0004C2F0 File Offset: 0x0004A4F0
	public override string DetermineActorPathForZone(Entity entity, TAG_ZONE zoneTag)
	{
		if (zoneTag == TAG_ZONE.DECK || zoneTag == TAG_ZONE.GRAVEYARD || zoneTag == TAG_ZONE.REMOVEDFROMGAME || zoneTag == TAG_ZONE.SETASIDE)
		{
			return "Card_Invisible.prefab:579b3b9a80234754593f24582f9cb93b";
		}
		if (zoneTag == TAG_ZONE.SECRET)
		{
			return base.DetermineActorPathForZone(entity, zoneTag);
		}
		return "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9";
	}
}
