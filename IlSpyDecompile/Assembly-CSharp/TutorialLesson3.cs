using UnityEngine;

public class TutorialLesson3 : MonoBehaviour
{
	public UberText m_attacker;

	public UberText m_defender;

	private void Awake()
	{
		m_attacker.SetGameStringText("GLOBAL_TUTORIAL_ATTACKER");
		m_defender.SetGameStringText("GLOBAL_TUTORIAL_DEFENDER");
	}
}
