using System;
using UnityEngine;

// Token: 0x0200035D RID: 861
public class ZoneHeroPower : Zone
{
	// Token: 0x06003224 RID: 12836 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Awake()
	{
	}

	// Token: 0x06003225 RID: 12837 RVA: 0x00100B63 File Offset: 0x000FED63
	public override string ToString()
	{
		return string.Format("{0} (Hero Power)", base.ToString());
	}

	// Token: 0x06003226 RID: 12838 RVA: 0x00100B75 File Offset: 0x000FED75
	public override bool CanAcceptTags(int controllerId, TAG_ZONE zoneTag, TAG_CARDTYPE cardType, Entity entity)
	{
		return base.CanAcceptTags(controllerId, zoneTag, cardType, entity) && cardType == TAG_CARDTYPE.HERO_POWER;
	}

	// Token: 0x06003227 RID: 12839 RVA: 0x00100B90 File Offset: 0x000FED90
	public override Transform GetZoneTransformForCard(Card card)
	{
		if (!(card != null) || card.GetController() == null || card.GetEntity() == null || !card.GetEntity().HasTag(GAME_TAG.SIDEKICK_HERO_POWER))
		{
			return base.transform;
		}
		if (card.GetController().IsFriendlySide())
		{
			return ZoneMgr.Get().FindZoneOfType<ZoneWeapon>(Player.Side.FRIENDLY).transform;
		}
		return ZoneMgr.Get().FindZoneOfType<ZoneWeapon>(Player.Side.OPPOSING).transform;
	}
}
