using UnityEngine;

public class TutorialLesson4 : MonoBehaviour
{
	public UberText m_tauntDescriptionTitle;

	public UberText m_tauntDescription;

	public UberText m_taunt;

	private void Awake()
	{
		m_tauntDescriptionTitle.SetGameStringText("GLOBAL_TUTORIAL_TAUNT");
		m_tauntDescription.SetGameStringText("GLOBAL_TUTORIAL_TAUNT_DESCRIPTION");
		m_taunt.SetGameStringText("GLOBAL_TUTORIAL_TAUNT");
	}
}
