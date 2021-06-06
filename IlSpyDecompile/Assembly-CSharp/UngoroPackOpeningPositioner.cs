using System;
using System.Collections.Generic;
using UnityEngine;

public class UngoroPackOpeningPositioner : MonoBehaviour
{
	[Serializable]
	public class PositioningBoneSet
	{
		public List<Transform> m_Bones;
	}

	public List<PositioningBoneSet> m_PositioningBoneSets;

	public List<PositioningBoneSet> m_PositioningBoneSetsMobile;

	public Transform m_PackSpawningBone;

	public List<Transform> GetPositioningBonesForCardCount(int cardCount)
	{
		if (cardCount <= 0)
		{
			return null;
		}
		if (cardCount - 1 >= m_PositioningBoneSets.Count)
		{
			return null;
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			return m_PositioningBoneSetsMobile[cardCount - 1].m_Bones;
		}
		return m_PositioningBoneSets[cardCount - 1].m_Bones;
	}
}
