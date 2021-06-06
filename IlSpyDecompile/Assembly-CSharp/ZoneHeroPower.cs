using UnityEngine;

public class ZoneHeroPower : Zone
{
	private void Awake()
	{
	}

	public override string ToString()
	{
		return $"{base.ToString()} (Hero Power)";
	}

	public override bool CanAcceptTags(int controllerId, TAG_ZONE zoneTag, TAG_CARDTYPE cardType, Entity entity)
	{
		if (!base.CanAcceptTags(controllerId, zoneTag, cardType, entity))
		{
			return false;
		}
		if (cardType != TAG_CARDTYPE.HERO_POWER)
		{
			return false;
		}
		return true;
	}

	public override Transform GetZoneTransformForCard(Card card)
	{
		if (card != null && card.GetController() != null && card.GetEntity() != null && card.GetEntity().HasTag(GAME_TAG.SIDEKICK_HERO_POWER))
		{
			if (card.GetController().IsFriendlySide())
			{
				return ZoneMgr.Get().FindZoneOfType<ZoneWeapon>(Player.Side.FRIENDLY).transform;
			}
			return ZoneMgr.Get().FindZoneOfType<ZoneWeapon>(Player.Side.OPPOSING).transform;
		}
		return base.transform;
	}
}
