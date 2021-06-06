using System;

// Token: 0x0200035A RID: 858
public class ZoneGraveyard : Zone
{
	// Token: 0x060031D4 RID: 12756 RVA: 0x000FEE58 File Offset: 0x000FD058
	public override bool CanAcceptTags(int controllerId, TAG_ZONE zoneTag, TAG_CARDTYPE cardType, Entity entity)
	{
		return base.CanAcceptTags(controllerId, zoneTag, cardType, entity) && (cardType == TAG_CARDTYPE.MINION || cardType == TAG_CARDTYPE.WEAPON || cardType == TAG_CARDTYPE.SPELL || cardType == TAG_CARDTYPE.HERO);
	}

	// Token: 0x060031D5 RID: 12757 RVA: 0x000FEE84 File Offset: 0x000FD084
	public override void UpdateLayout()
	{
		this.m_updatingLayout++;
		if (base.IsBlockingLayout())
		{
			base.UpdateLayoutFinished();
			return;
		}
		for (int i = 0; i < this.m_cards.Count; i++)
		{
			Card card = this.m_cards[i];
			if (!card.IsDoNotSort())
			{
				card.HideCard();
				card.EnableTransitioningZones(false);
			}
		}
		base.UpdateLayoutFinished();
	}

	// Token: 0x060031D6 RID: 12758 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnHealingDoesDamageEntityEnteredPlay()
	{
	}

	// Token: 0x060031D7 RID: 12759 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnHealingDoesDamageEntityMousedOut()
	{
	}

	// Token: 0x060031D8 RID: 12760 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnHealingDoesDamageEntityMousedOver()
	{
	}

	// Token: 0x060031D9 RID: 12761 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnLifestealDoesDamageEntityEnteredPlay()
	{
	}

	// Token: 0x060031DA RID: 12762 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnLifestealDoesDamageEntityMousedOut()
	{
	}

	// Token: 0x060031DB RID: 12763 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnLifestealDoesDamageEntityMousedOver()
	{
	}
}
