using System;
using System.Collections.Generic;

[Serializable]
public class CardSpecificMultiVoData
{
	public string m_CardId;

	public SpellPlayerSide m_SideToSearch = SpellPlayerSide.TARGET;

	public List<SpellZoneTag> m_ZonesToSearch = new List<SpellZoneTag>
	{
		SpellZoneTag.PLAY,
		SpellZoneTag.HERO,
		SpellZoneTag.HERO_POWER,
		SpellZoneTag.WEAPON
	};

	public CardSpecificMultiVoLine[] m_Lines;
}
