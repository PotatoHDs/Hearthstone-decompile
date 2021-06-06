using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000789 RID: 1929
public class TagVoSpell : CardSoundSpell
{
	// Token: 0x06006C3E RID: 27710 RVA: 0x0023088C File Offset: 0x0022EA8C
	public override AudioSource DetermineBestAudioSource()
	{
		for (int i = 0; i < this.m_TagVoDataList.Count; i++)
		{
			TagVoData tagVoData = this.m_TagVoDataList[i];
			if (this.CanPlayTagVo(tagVoData))
			{
				return tagVoData.m_AudioSource;
			}
		}
		return base.DetermineBestAudioSource();
	}

	// Token: 0x06006C3F RID: 27711 RVA: 0x002308D4 File Offset: 0x0022EAD4
	public override string DetermineGameStringKey()
	{
		for (int i = 0; i < this.m_TagVoDataList.Count; i++)
		{
			TagVoData tagVoData = this.m_TagVoDataList[i];
			if (this.CanPlayTagVo(tagVoData))
			{
				return tagVoData.m_GameStringKeyOverride;
			}
		}
		return "";
	}

	// Token: 0x06006C40 RID: 27712 RVA: 0x0023091C File Offset: 0x0022EB1C
	private bool CanPlayTagVo(TagVoData potentialVOData)
	{
		if (potentialVOData.m_TagRequirements.Count == 0)
		{
			return false;
		}
		Card sourceCard = base.GetSourceCard();
		if (sourceCard == null)
		{
			return false;
		}
		Entity entity = sourceCard.GetEntity();
		foreach (TagVoRequirement tagVoRequirement in potentialVOData.m_TagRequirements)
		{
			if (entity.GetTag(tagVoRequirement.m_Tag) != tagVoRequirement.m_Value)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x040057A5 RID: 22437
	public List<TagVoData> m_TagVoDataList = new List<TagVoData>();
}
