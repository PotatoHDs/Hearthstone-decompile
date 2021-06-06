using System;

// Token: 0x02000363 RID: 867
public class ZoneMoveMinionHoverTarget : Zone
{
	// Token: 0x060032E5 RID: 13029 RVA: 0x00104D90 File Offset: 0x00102F90
	public override string ToString()
	{
		return string.Format("{0} (Move Minion Hover Target)", base.ToString());
	}

	// Token: 0x060032E6 RID: 13030 RVA: 0x00104DA2 File Offset: 0x00102FA2
	public override bool CanAcceptTags(int controllerId, TAG_ZONE zoneTag, TAG_CARDTYPE cardType, Entity entity)
	{
		return base.CanAcceptTags(controllerId, zoneTag, cardType, entity) && cardType == TAG_CARDTYPE.MOVE_MINION_HOVER_TARGET && entity != null && entity.GetTag(GAME_TAG.MOVE_MINION_HOVER_TARGET_SLOT) == this.m_Slot;
	}

	// Token: 0x04001C00 RID: 7168
	public int m_Slot;
}
