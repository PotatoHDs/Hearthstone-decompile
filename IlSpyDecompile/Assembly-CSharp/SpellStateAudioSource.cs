using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class SpellStateAudioSource
{
	public AudioSource m_AudioSource;

	public float m_StartDelaySec;

	public bool m_PlayGlobally;

	public bool m_StopOnStateChange;

	public string m_Comment;

	public bool m_Enabled = true;

	public void Init()
	{
		if (!(m_AudioSource == null))
		{
			m_AudioSource.playOnAwake = false;
		}
	}

	public void Play(SpellState parent)
	{
		if (m_Enabled)
		{
			if (Mathf.Approximately(m_StartDelaySec, 0f))
			{
				PlayNow();
			}
			else
			{
				parent.StartCoroutine(DelayedPlay());
			}
		}
	}

	public void Stop()
	{
		if (m_Enabled && !(m_AudioSource == null) && !m_PlayGlobally && m_StopOnStateChange)
		{
			m_AudioSource.Stop();
		}
	}

	private IEnumerator DelayedPlay()
	{
		yield return new WaitForSeconds(m_StartDelaySec);
		PlayNow();
	}

	private void PlayNow()
	{
		if (!(m_AudioSource == null))
		{
			if (m_PlayGlobally)
			{
				SoundPlayClipArgs soundPlayClipArgs = new SoundPlayClipArgs();
				soundPlayClipArgs.m_def = SoundManager.Get().GetSoundDef(m_AudioSource);
				soundPlayClipArgs.m_volume = m_AudioSource.volume;
				soundPlayClipArgs.m_pitch = m_AudioSource.pitch;
				soundPlayClipArgs.m_category = SoundManager.Get().GetCategory(m_AudioSource);
				soundPlayClipArgs.m_parentObject = m_AudioSource.gameObject;
				SoundManager.Get().PlayClip(soundPlayClipArgs);
			}
			else
			{
				SoundManager.Get().Play(m_AudioSource);
			}
		}
	}
}
