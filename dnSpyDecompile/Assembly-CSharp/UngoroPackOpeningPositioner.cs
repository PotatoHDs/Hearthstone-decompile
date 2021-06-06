using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000836 RID: 2102
public class UngoroPackOpeningPositioner : MonoBehaviour
{
	// Token: 0x0600706E RID: 28782 RVA: 0x002445F8 File Offset: 0x002427F8
	public List<Transform> GetPositioningBonesForCardCount(int cardCount)
	{
		if (cardCount <= 0)
		{
			return null;
		}
		if (cardCount - 1 >= this.m_PositioningBoneSets.Count)
		{
			return null;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			return this.m_PositioningBoneSetsMobile[cardCount - 1].m_Bones;
		}
		return this.m_PositioningBoneSets[cardCount - 1].m_Bones;
	}

	// Token: 0x04005A57 RID: 23127
	public List<UngoroPackOpeningPositioner.PositioningBoneSet> m_PositioningBoneSets;

	// Token: 0x04005A58 RID: 23128
	public List<UngoroPackOpeningPositioner.PositioningBoneSet> m_PositioningBoneSetsMobile;

	// Token: 0x04005A59 RID: 23129
	public Transform m_PackSpawningBone;

	// Token: 0x02002408 RID: 9224
	[Serializable]
	public class PositioningBoneSet
	{
		// Token: 0x0400E90E RID: 59662
		public List<Transform> m_Bones;
	}
}
