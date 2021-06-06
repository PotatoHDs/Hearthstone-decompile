using System.Collections;
using UnityEngine;

public class CardSoundSpell : Spell
{
	public CardSoundData m_CardSoundData = new CardSoundData();

	protected AudioSource m_activeAudioSource;

	protected bool m_forceDefaultAudioSource;

	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.OnBirth(prevStateType);
		Play();
	}

	protected override void OnNone(SpellStateType prevStateType)
	{
		base.OnNone(prevStateType);
		Stop();
	}

	public AudioSource GetActiveAudioSource()
	{
		return m_activeAudioSource;
	}

	public void ForceDefaultAudioSource()
	{
		m_forceDefaultAudioSource = true;
	}

	public bool HasActiveAudioSource()
	{
		return m_activeAudioSource != null;
	}

	public virtual AudioSource DetermineBestAudioSource()
	{
		return m_CardSoundData.m_AudioSource;
	}

	public virtual string DetermineGameStringKey()
	{
		return "";
	}

	protected virtual void Play()
	{
		Stop();
		m_activeAudioSource = (m_forceDefaultAudioSource ? m_CardSoundData.m_AudioSource : DetermineBestAudioSource());
		if (m_activeAudioSource == null)
		{
			OnStateFinished();
		}
		else
		{
			StartCoroutine("DelayedPlay");
		}
	}

	protected virtual void PlayNow()
	{
		SoundManager.Get().Play(m_activeAudioSource);
		StartCoroutine("WaitForSourceThenFinishState");
	}

	protected virtual void Stop()
	{
		StopCoroutine("DelayedPlay");
		StopCoroutine("WaitForSourceThenFinishState");
		SoundManager.Get().Stop(m_activeAudioSource);
	}

	protected IEnumerator DelayedPlay()
	{
		if (m_CardSoundData.m_DelaySec > 0f)
		{
			yield return new WaitForSeconds(m_CardSoundData.m_DelaySec);
		}
		PlayNow();
	}

	protected IEnumerator WaitForSourceThenFinishState()
	{
		while (SoundManager.Get().IsActive(m_activeAudioSource))
		{
			yield return 0;
		}
		OnStateFinished();
	}
}
