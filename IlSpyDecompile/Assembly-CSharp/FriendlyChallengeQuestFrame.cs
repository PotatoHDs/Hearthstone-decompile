using UnityEngine;

[CustomEditClass]
public class FriendlyChallengeQuestFrame : MonoBehaviour
{
	[CustomEditField(Sections = "Deprecated")]
	public UberText m_questName;

	[CustomEditField(Sections = "Deprecated")]
	public UberText m_questDesc;

	[CustomEditField(Sections = "Deprecated")]
	public Transform m_rewardBone;

	[CustomEditField(Sections = "Deprecated")]
	public GameObject m_nameLine;

	[CustomEditField(Sections = "Deprecated")]
	public MeshRenderer m_rewardMesh;

	[CustomEditField(Sections = "Deprecated")]
	public UberText m_rewardAmountLabel;

	[CustomEditField(Sections = "Deprecated")]
	public UberText m_noGoldRewardText;

	[CustomEditField(Sections = "Progression", T = EditType.GAME_OBJECT, Label = "Quest Tile Bone")]
	public GameObject m_questTileBone;
}
