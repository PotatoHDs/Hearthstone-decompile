using System;
using UnityEngine;

// Token: 0x020002F8 RID: 760
[CustomEditClass]
public class FriendlyChallengeQuestFrame : MonoBehaviour
{
	// Token: 0x040016EF RID: 5871
	[CustomEditField(Sections = "Deprecated")]
	public UberText m_questName;

	// Token: 0x040016F0 RID: 5872
	[CustomEditField(Sections = "Deprecated")]
	public UberText m_questDesc;

	// Token: 0x040016F1 RID: 5873
	[CustomEditField(Sections = "Deprecated")]
	public Transform m_rewardBone;

	// Token: 0x040016F2 RID: 5874
	[CustomEditField(Sections = "Deprecated")]
	public GameObject m_nameLine;

	// Token: 0x040016F3 RID: 5875
	[CustomEditField(Sections = "Deprecated")]
	public MeshRenderer m_rewardMesh;

	// Token: 0x040016F4 RID: 5876
	[CustomEditField(Sections = "Deprecated")]
	public UberText m_rewardAmountLabel;

	// Token: 0x040016F5 RID: 5877
	[CustomEditField(Sections = "Deprecated")]
	public UberText m_noGoldRewardText;

	// Token: 0x040016F6 RID: 5878
	[CustomEditField(Sections = "Progression", T = EditType.GAME_OBJECT, Label = "Quest Tile Bone")]
	public GameObject m_questTileBone;
}
