using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellState : MonoBehaviour
{
	public SpellStateType m_StateType;

	public float m_StartDelaySec;

	public List<SpellStateAnimObject> m_ExternalAnimatedObjects;

	public List<SpellStateAudioSource> m_AudioSources;

	private Spell m_spell;

	private bool m_playing;

	private bool m_initialized;

	private bool m_shown = true;

	private void Start()
	{
		m_spell = SceneUtils.FindComponentInParents<Spell>(base.gameObject);
		for (int i = 0; i < m_ExternalAnimatedObjects.Count; i++)
		{
			m_ExternalAnimatedObjects[i].Init();
		}
		for (int j = 0; j < m_AudioSources.Count; j++)
		{
			m_AudioSources[j].Init();
		}
		m_initialized = true;
		if (m_shown && m_playing)
		{
			PlayImpl();
		}
		else
		{
			StopImpl(null);
		}
	}

	public void Play()
	{
		if (!m_playing && m_shown)
		{
			m_playing = true;
			if (m_initialized)
			{
				PlayImpl();
			}
		}
	}

	public void Stop(List<SpellState> nextStateList)
	{
		if (m_playing)
		{
			m_playing = false;
			if (m_initialized)
			{
				StopImpl(nextStateList);
			}
		}
	}

	public void ShowState()
	{
		if (!m_shown)
		{
			m_shown = true;
			if (m_initialized && m_playing)
			{
				PlayImpl();
			}
		}
	}

	public void HideState()
	{
		if (m_shown)
		{
			m_shown = false;
			if (m_initialized && m_playing)
			{
				StopImpl(null);
			}
		}
	}

	public void OnLoad()
	{
		base.gameObject.SetActive(value: true);
		foreach (SpellStateAnimObject externalAnimatedObject in m_ExternalAnimatedObjects)
		{
			externalAnimatedObject.OnLoad(this);
		}
	}

	private void OnStateFinished()
	{
		m_spell.OnStateFinished();
	}

	private void OnSpellFinished()
	{
		m_spell.OnSpellFinished();
	}

	private void OnChangeState(SpellStateType stateType)
	{
		m_spell.ChangeState(stateType);
	}

	private IEnumerator DelayedPlay()
	{
		yield return new WaitForSeconds(m_StartDelaySec);
		PlayNow();
	}

	private void PlayImpl()
	{
		base.gameObject.SetActive(value: true);
		if (Mathf.Approximately(m_StartDelaySec, 0f))
		{
			PlayNow();
		}
		else
		{
			StartCoroutine(DelayedPlay());
		}
	}

	private void StopImpl(List<SpellState> nextStateList)
	{
		if (nextStateList == null)
		{
			foreach (SpellStateAnimObject externalAnimatedObject in m_ExternalAnimatedObjects)
			{
				externalAnimatedObject.Stop();
			}
		}
		else
		{
			foreach (SpellStateAnimObject externalAnimatedObject2 in m_ExternalAnimatedObjects)
			{
				externalAnimatedObject2.Stop(nextStateList);
			}
		}
		foreach (SpellStateAudioSource audioSource in m_AudioSources)
		{
			audioSource.Stop();
		}
		base.gameObject.SetActive(value: false);
	}

	private void PlayNow()
	{
		foreach (SpellStateAnimObject externalAnimatedObject in m_ExternalAnimatedObjects)
		{
			externalAnimatedObject.Play();
		}
		foreach (SpellStateAudioSource audioSource in m_AudioSources)
		{
			audioSource.Play(this);
		}
	}
}
