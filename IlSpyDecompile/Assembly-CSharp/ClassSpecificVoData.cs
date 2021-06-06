using System;
using System.Collections.Generic;

[Serializable]
public class ClassSpecificVoData
{
	public List<ClassSpecificVoLine> m_Lines = new List<ClassSpecificVoLine>();

	public SpellPlayerSide m_SideToSearch = SpellPlayerSide.TARGET;

	public List<SpellZoneTag> m_ZonesToSearch = new List<SpellZoneTag> { SpellZoneTag.HERO };
}
