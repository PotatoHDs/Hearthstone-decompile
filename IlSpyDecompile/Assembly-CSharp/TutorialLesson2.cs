using UnityEngine;

public class TutorialLesson2 : MonoBehaviour
{
	public UberText m_cost;

	public UberText m_yourMana;

	private void Awake()
	{
		m_cost.SetGameStringText("GLOBAL_TUTORIAL_COST");
		m_yourMana.SetGameStringText("GLOBAL_TUTORIAL_YOUR_MANA");
	}
}
