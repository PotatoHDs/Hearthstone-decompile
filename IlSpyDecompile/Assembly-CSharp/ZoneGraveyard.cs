public class ZoneGraveyard : Zone
{
	public override bool CanAcceptTags(int controllerId, TAG_ZONE zoneTag, TAG_CARDTYPE cardType, Entity entity)
	{
		if (!base.CanAcceptTags(controllerId, zoneTag, cardType, entity))
		{
			return false;
		}
		return cardType switch
		{
			TAG_CARDTYPE.MINION => true, 
			TAG_CARDTYPE.WEAPON => true, 
			TAG_CARDTYPE.SPELL => true, 
			TAG_CARDTYPE.HERO => true, 
			_ => false, 
		};
	}

	public override void UpdateLayout()
	{
		m_updatingLayout++;
		if (IsBlockingLayout())
		{
			UpdateLayoutFinished();
			return;
		}
		for (int i = 0; i < m_cards.Count; i++)
		{
			Card card = m_cards[i];
			if (!card.IsDoNotSort())
			{
				card.HideCard();
				card.EnableTransitioningZones(enable: false);
			}
		}
		UpdateLayoutFinished();
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
}
