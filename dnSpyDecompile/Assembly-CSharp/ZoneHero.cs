using System;
using UnityEngine;

// Token: 0x0200035C RID: 860
public class ZoneHero : Zone
{
	// Token: 0x0600321A RID: 12826 RVA: 0x00100ACA File Offset: 0x000FECCA
	public override string ToString()
	{
		return string.Format("{0} (Hero)", base.ToString());
	}

	// Token: 0x0600321B RID: 12827 RVA: 0x00100ADC File Offset: 0x000FECDC
	public override bool CanAcceptTags(int controllerId, TAG_ZONE zoneTag, TAG_CARDTYPE cardType, Entity entity)
	{
		return base.CanAcceptTags(controllerId, zoneTag, cardType, entity) && cardType == TAG_CARDTYPE.HERO;
	}

	// Token: 0x0600321C RID: 12828 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnHealingDoesDamageEntityEnteredPlay()
	{
	}

	// Token: 0x0600321D RID: 12829 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnHealingDoesDamageEntityMousedOut()
	{
	}

	// Token: 0x0600321E RID: 12830 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnHealingDoesDamageEntityMousedOver()
	{
	}

	// Token: 0x0600321F RID: 12831 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnLifestealDoesDamageEntityEnteredPlay()
	{
	}

	// Token: 0x06003220 RID: 12832 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnLifestealDoesDamageEntityMousedOut()
	{
	}

	// Token: 0x06003221 RID: 12833 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void OnLifestealDoesDamageEntityMousedOver()
	{
	}

	// Token: 0x06003222 RID: 12834 RVA: 0x00100AF4 File Offset: 0x000FECF4
	public override Transform GetZoneTransformForCard(Card card)
	{
		if (card != null && card.GetController() != null && card.GetEntity() != null && card.GetEntity().HasTag(GAME_TAG.SIDEKICK))
		{
			Transform result;
			if (card.GetController().IsFriendlySide())
			{
				result = Board.Get().FindBone("FriendlySidekickHero");
			}
			else
			{
				result = Board.Get().FindBone("OpposingSidekickHero");
			}
			return result;
		}
		return base.transform;
	}
}
