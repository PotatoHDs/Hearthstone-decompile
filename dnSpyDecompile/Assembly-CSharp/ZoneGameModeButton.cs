using System;

// Token: 0x02000359 RID: 857
public class ZoneGameModeButton : Zone
{
	// Token: 0x060031D1 RID: 12753 RVA: 0x000FEE11 File Offset: 0x000FD011
	public override string ToString()
	{
		return string.Format("{0} (Game Mode Button)", base.ToString());
	}

	// Token: 0x060031D2 RID: 12754 RVA: 0x000FEE23 File Offset: 0x000FD023
	public override bool CanAcceptTags(int controllerId, TAG_ZONE zoneTag, TAG_CARDTYPE cardType, Entity entity)
	{
		return base.CanAcceptTags(controllerId, zoneTag, cardType, entity) && cardType == TAG_CARDTYPE.GAME_MODE_BUTTON && entity != null && entity.GetTag(GAME_TAG.GAME_MODE_BUTTON_SLOT) == this.m_ButtonSlot;
	}

	// Token: 0x04001BA5 RID: 7077
	public int m_ButtonSlot;
}
