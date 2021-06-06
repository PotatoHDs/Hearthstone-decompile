using System;
using System.Collections.Generic;

// Token: 0x02000783 RID: 1923
[Serializable]
public class ClassSpecificVoData
{
	// Token: 0x0400579B RID: 22427
	public List<ClassSpecificVoLine> m_Lines = new List<ClassSpecificVoLine>();

	// Token: 0x0400579C RID: 22428
	public SpellPlayerSide m_SideToSearch = SpellPlayerSide.TARGET;

	// Token: 0x0400579D RID: 22429
	public List<SpellZoneTag> m_ZonesToSearch = new List<SpellZoneTag>
	{
		SpellZoneTag.HERO
	};
}
