using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000780 RID: 1920
[Serializable]
public class CardSpecificVoData
{
	// Token: 0x04005793 RID: 22419
	public string m_CardId;

	// Token: 0x04005794 RID: 22420
	public AudioSource m_AudioSource;

	// Token: 0x04005795 RID: 22421
	public SpellPlayerSide m_SideToSearch = SpellPlayerSide.TARGET;

	// Token: 0x04005796 RID: 22422
	public List<SpellZoneTag> m_ZonesToSearch = new List<SpellZoneTag>
	{
		SpellZoneTag.PLAY,
		SpellZoneTag.HERO,
		SpellZoneTag.HERO_POWER,
		SpellZoneTag.WEAPON
	};

	// Token: 0x04005797 RID: 22423
	public string m_GameStringKey;
}
