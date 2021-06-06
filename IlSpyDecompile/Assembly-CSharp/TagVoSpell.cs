using System.Collections.Generic;
using UnityEngine;

public class TagVoSpell : CardSoundSpell
{
	public List<TagVoData> m_TagVoDataList = new List<TagVoData>();

	public override AudioSource DetermineBestAudioSource()
	{
		for (int i = 0; i < m_TagVoDataList.Count; i++)
		{
			TagVoData tagVoData = m_TagVoDataList[i];
			if (CanPlayTagVo(tagVoData))
			{
				return tagVoData.m_AudioSource;
			}
		}
		return base.DetermineBestAudioSource();
	}

	public override string DetermineGameStringKey()
	{
		for (int i = 0; i < m_TagVoDataList.Count; i++)
		{
			TagVoData tagVoData = m_TagVoDataList[i];
			if (CanPlayTagVo(tagVoData))
			{
				return tagVoData.m_GameStringKeyOverride;
			}
		}
		return "";
	}

	private bool CanPlayTagVo(TagVoData potentialVOData)
	{
		if (potentialVOData.m_TagRequirements.Count == 0)
		{
			return false;
		}
		Card sourceCard = GetSourceCard();
		if (sourceCard == null)
		{
			return false;
		}
		Entity entity = sourceCard.GetEntity();
		foreach (TagVoRequirement tagRequirement in potentialVOData.m_TagRequirements)
		{
			if (entity.GetTag(tagRequirement.m_Tag) != tagRequirement.m_Value)
			{
				return false;
			}
		}
		return true;
	}
}
