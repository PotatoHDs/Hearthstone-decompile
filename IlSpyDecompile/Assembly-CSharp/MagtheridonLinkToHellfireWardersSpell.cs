using System.Collections.Generic;
using UnityEngine;

public class MagtheridonLinkToHellfireWardersSpell : MouseOverLinkSpell
{
	public static readonly string MagtheridonId = "BT_850";

	public static readonly string HellfireWarderId = "BT_850t";

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
		if (source.GetCardId() == MagtheridonId)
		{
			side = ((!flag2) ? Player.Side.FRIENDLY : Player.Side.OPPOSING);
			side2 = (flag2 ? Player.Side.FRIENDLY : Player.Side.OPPOSING);
			flag = true;
		}
		else
		{
			if (!(source.GetCardId() == HellfireWarderId))
			{
				return;
			}
			side = (flag2 ? Player.Side.FRIENDLY : Player.Side.OPPOSING);
			side2 = ((!flag2) ? Player.Side.FRIENDLY : Player.Side.OPPOSING);
		}
		ZonePlay zonePlay = zoneMgr.FindZoneOfType<ZonePlay>(side);
		ZonePlay zonePlay2 = zoneMgr.FindZoneOfType<ZonePlay>(side2);
		int num = 0;
		int num2 = 0;
		foreach (Card card in zonePlay2.GetCards())
		{
			Entity entity = card.GetEntity();
			if (!(entity.GetCardId() == MagtheridonId) || !entity.IsDormant())
			{
				continue;
			}
			if (flag)
			{
				if (card.gameObject == m_source)
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
		foreach (Card card2 in zonePlay.GetCards())
		{
			if (card2.GetEntity().GetCardId() == HellfireWarderId)
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
}
