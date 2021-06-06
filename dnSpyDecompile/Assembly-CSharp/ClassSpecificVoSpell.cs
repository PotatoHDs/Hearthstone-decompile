using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000784 RID: 1924
public class ClassSpecificVoSpell : CardSoundSpell
{
	// Token: 0x06006C35 RID: 27701 RVA: 0x00230688 File Offset: 0x0022E888
	public override AudioSource DetermineBestAudioSource()
	{
		AudioSource audioSource = this.SearchForClassSpecificVo();
		if (audioSource)
		{
			return audioSource;
		}
		return base.DetermineBestAudioSource();
	}

	// Token: 0x06006C36 RID: 27702 RVA: 0x002306AC File Offset: 0x0022E8AC
	private AudioSource SearchForClassSpecificVo()
	{
		foreach (SpellZoneTag zoneTag in this.m_ClassSpecificVoData.m_ZonesToSearch)
		{
			List<Zone> zones = SpellUtils.FindZonesFromTag(this, zoneTag, this.m_ClassSpecificVoData.m_SideToSearch);
			AudioSource audioSource = this.SearchForClassSpecificVo(zones);
			if (audioSource)
			{
				return audioSource;
			}
		}
		return null;
	}

	// Token: 0x06006C37 RID: 27703 RVA: 0x0023072C File Offset: 0x0022E92C
	private AudioSource SearchForClassSpecificVo(List<Zone> zones)
	{
		if (zones == null)
		{
			return null;
		}
		foreach (Zone zone in zones)
		{
			foreach (Card card in zone.GetCards())
			{
				SpellClassTag spellClassTag = SpellUtils.ConvertClassTagToSpellEnum(card.GetEntity().GetClass());
				if (spellClassTag != SpellClassTag.NONE)
				{
					foreach (ClassSpecificVoLine classSpecificVoLine in this.m_ClassSpecificVoData.m_Lines)
					{
						if (classSpecificVoLine.m_Class == spellClassTag)
						{
							return classSpecificVoLine.m_AudioSource;
						}
					}
				}
			}
		}
		return null;
	}

	// Token: 0x0400579E RID: 22430
	public ClassSpecificVoData m_ClassSpecificVoData = new ClassSpecificVoData();
}
