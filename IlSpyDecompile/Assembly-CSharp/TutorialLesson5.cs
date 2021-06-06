using UnityEngine;

public class TutorialLesson5 : MonoBehaviour
{
	public UberText m_heroPower;

	public UberText m_used;

	public UberText m_yourTurn;

	private void Awake()
	{
		m_heroPower.SetGameStringText("GLOBAL_TUTORIAL_HERO_POWER");
		m_used.SetGameStringText("GLOBAL_TUTORIAL_USED");
		m_yourTurn.SetGameStringText("GLOBAL_TUTORIAL_YOUR_TURN");
	}
}
