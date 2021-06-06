using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000804 RID: 2052
public class MagtheridonLinkToHellfireWardersSpell : MouseOverLinkSpell
{
	// Token: 0x06006F34 RID: 28468 RVA: 0x0023CE64 File Offset: 0x0023B064
	protected override void GetAllTargets(Entity source, List<GameObject> targets)
	{
		if (source == null || targets == null)
		{
			return;
		}
		ZoneMgr zoneMgr = ZoneMgr.Get();
		if (zoneMgr == null)
		{
			return;
		}
		bool flag = false;
		bool flag2 = source.IsControlledByFriendlySidePlayer();
		Player.Side side;
		Player.Side side2;
		if (source.GetCardId() == MagtheridonLinkToHellfireWardersSpell.MagtheridonId)
		{
			side = (flag2 ? Player.Side.OPPOSING : Player.Side.FRIENDLY);
			side2 = (flag2 ? Player.Side.FRIENDLY : Player.Side.OPPOSING);
			flag = true;
		}
		else
		{
			if (!(source.GetCardId() == MagtheridonLinkToHellfireWardersSpell.HellfireWarderId))
			{
				return;
			}
			side = (flag2 ? Player.Side.FRIENDLY : Player.Side.OPPOSING);
			side2 = (flag2 ? Player.Side.OPPOSING : Player.Side.FRIENDLY);
		}
		ZonePlay zonePlay = zoneMgr.FindZoneOfType<ZonePlay>(side);
		Zone zone = zoneMgr.FindZoneOfType<ZonePlay>(side2);
		int num = 0;
		int num2 = 0;
		foreach (Card card in zone.GetCards())
		{
			Entity entity = card.GetEntity();
			if (entity.GetCardId() == MagtheridonLinkToHellfireWardersSpell.MagtheridonId && entity.IsDormant())
			{
				if (flag)
				{
					if (card.gameObject == this.m_source)
					{
						targets.Add(card.gameObject);
						num++;
					}
				}
				else
				{
					targets.Add(card.gameObject);
					num++;
				}
			}
		}
		foreach (Card card2 in zonePlay.GetCards())
		{
			if (card2.GetEntity().GetCardId() == MagtheridonLinkToHellfireWardersSpell.HellfireWarderId)
			{
				targets.Add(card2.gameObject);
				num2++;
			}
		}
		if (num == 0 || num2 == 0)
		{
			targets.Clear();
		}
	}

	// Token: 0x04005927 RID: 22823
	public static readonly string MagtheridonId = "BT_850";

	// Token: 0x04005928 RID: 22824
	public static readonly string HellfireWarderId = "BT_850t";
}
