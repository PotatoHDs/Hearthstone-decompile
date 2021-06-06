using System.Collections.Generic;
using UnityEngine;

public class ClassSpecificVoSpell : CardSoundSpell
{
	public ClassSpecificVoData m_ClassSpecificVoData = new ClassSpecificVoData();

	public override AudioSource DetermineBestAudioSource()
	{
		AudioSource audioSource = SearchForClassSpecificVo();
		if ((bool)audioSource)
		{
			return audioSource;
		}
		return base.DetermineBestAudioSource();
	}

	private AudioSource SearchForClassSpecificVo()
	{
		foreach (SpellZoneTag item in m_ClassSpecificVoData.m_ZonesToSearch)
		{
			List<Zone> zones = SpellUtils.FindZonesFromTag(this, item, m_ClassSpecificVoData.m_SideToSearch);
			AudioSource audioSource = SearchForClassSpecificVo(zones);
			if ((bool)audioSource)
			{
				return audioSource;
			}
		}
		return null;
	}

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
				if (spellClassTag == SpellClassTag.NONE)
				{
					continue;
				}
				foreach (ClassSpecificVoLine line in m_ClassSpecificVoData.m_Lines)
				{
					if (line.m_Class == spellClassTag)
					{
						return line.m_AudioSource;
					}
				}
			}
		}
		return null;
	}
}
