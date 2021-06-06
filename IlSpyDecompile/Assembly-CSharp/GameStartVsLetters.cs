using UnityEngine;

public class GameStartVsLetters : MonoBehaviour
{
	public string m_fadeAnimName;

	private Animation m_anim;

	private void Awake()
	{
		m_anim = GetComponentInChildren<Animation>();
		if (m_anim == null)
		{
			Log.All.PrintError("GameStartVsLetters.Awake(): No Animator component found in children.");
		}
	}

	public void FadeIn()
	{
		if (m_anim != null)
		{
			m_anim[m_fadeAnimName].speed = -1f;
			m_anim[m_fadeAnimName].time = 1f;
			m_anim.Play(m_fadeAnimName);
		}
	}

	public void FadeOut()
	{
		if (m_anim != null)
		{
			m_anim[m_fadeAnimName].speed = 1f;
			m_anim[m_fadeAnimName].time = 0f;
			m_anim.Play(m_fadeAnimName);
		}
	}
}
