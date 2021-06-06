using System;
using System.Collections.Generic;

// Token: 0x0200077E RID: 1918
[Serializable]
public class CardSpecificMultiVoData
{
	// Token: 0x0400578C RID: 22412
	public string m_CardId;

	// Token: 0x0400578D RID: 22413
	public SpellPlayerSide m_SideToSearch = SpellPlayerSide.TARGET;

	// Token: 0x0400578E RID: 22414
	public List<SpellZoneTag> m_ZonesToSearch = new List<SpellZoneTag>
	{
		SpellZoneTag.PLAY,
		SpellZoneTag.HERO,
		SpellZoneTag.HERO_POWER,
		SpellZoneTag.WEAPON
	};

	// Token: 0x0400578F RID: 22415
	public CardSpecificMultiVoLine[] m_Lines;
}
