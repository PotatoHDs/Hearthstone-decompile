using UnityEngine;

public class TutorialLesson1 : MonoBehaviour
{
	public UberText m_health;

	public UberText m_attack;

	public UberText m_minion;

	private void Awake()
	{
		m_health.SetGameStringText("GLOBAL_TUTORIAL_HEALTH");
		m_attack.SetGameStringText("GLOBAL_TUTORIAL_ATTACK");
		m_minion.SetGameStringText("GLOBAL_TUTORIAL_MINION");
	}
}
