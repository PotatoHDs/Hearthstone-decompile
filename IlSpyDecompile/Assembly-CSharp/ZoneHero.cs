using UnityEngine;

public class ZoneHero : Zone
{
	public override string ToString()
	{
		return $"{base.ToString()} (Hero)";
	}

	public override bool CanAcceptTags(int controllerId, TAG_ZONE zoneTag, TAG_CARDTYPE cardType, Entity entity)
	{
		if (!base.CanAcceptTags(controllerId, zoneTag, cardType, entity))
		{
			return false;
		}
		if (cardType != TAG_CARDTYPE.HERO)
		{
			return false;
		}
		return true;
	}

	public override void OnHealingDoesDamageEntityEnteredPlay()
	{
	}

	public override void OnHealingDoesDamageEntityMousedOut()
	{
	}

	public override void OnHealingDoesDamageEntityMousedOver()
	{
	}

	public override void OnLifestealDoesDamageEntityEnteredPlay()
	{
	}

	public override void OnLifestealDoesDamageEntityMousedOut()
	{
	}

	public override void OnLifestealDoesDamageEntityMousedOver()
	{
	}

	public override Transform GetZoneTransformForCard(Card card)
	{
		if (card != null && card.GetController() != null && card.GetEntity() != null && card.GetEntity().HasTag(GAME_TAG.SIDEKICK))
		{
			if (card.GetController().IsFriendlySide())
			{
				return Board.Get().FindBone("FriendlySidekickHero");
			}
			return Board.Get().FindBone("OpposingSidekickHero");
		}
		return base.transform;
	}
}
