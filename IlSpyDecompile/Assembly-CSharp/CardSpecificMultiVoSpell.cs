using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpecificMultiVoSpell : CardSoundSpell
{
	public CardSpecificMultiVoData m_CardSpecificVoData = new CardSpecificMultiVoData();

	private int m_ActiveAudioIndex;

	private bool m_SpecificCardFound;

	protected override void Play()
	{
		if (!m_forceDefaultAudioSource)
		{
			m_SpecificCardFound = SearchForCard();
		}
		if (m_SpecificCardFound)
		{
			Stop();
			m_ActiveAudioIndex = 0;
			m_activeAudioSource = (m_forceDefaultAudioSource ? m_CardSoundData.m_AudioSource : DetermineBestAudioSource());
			if (m_activeAudioSource == null)
			{
				OnStateFinished();
			}
			else
			{
				StartCoroutine("DelayedPlayMulti");
			}
		}
		else
		{
			base.Play();
		}
	}

	protected virtual void PlayNowMulti()
	{
		SoundManager.Get().Play(m_activeAudioSource);
		StartCoroutine("WaitForSourceThenContinue");
	}

	protected override void Stop()
	{
		StopCoroutine("WaitForSourceThenContinue");
		base.Stop();
	}

	public override AudioSource DetermineBestAudioSource()
	{
		if (m_SpecificCardFound)
		{
			if (m_ActiveAudioIndex < m_CardSpecificVoData.m_Lines.Length)
			{
				return m_CardSpecificVoData.m_Lines[m_ActiveAudioIndex].m_AudioSource;
			}
			return null;
		}
		return base.DetermineBestAudioSource();
	}

	private bool SearchForCard()
	{
		if (string.IsNullOrEmpty(m_CardSpecificVoData.m_CardId))
		{
			return false;
		}
		foreach (SpellZoneTag item in m_CardSpecificVoData.m_ZonesToSearch)
		{
			List<Zone> zones = SpellUtils.FindZonesFromTag(this, item, m_CardSpecificVoData.m_SideToSearch);
			if (IsCardInZones(zones))
			{
				return true;
			}
		}
		return false;
	}

	private bool IsCardInZones(List<Zone> zones)
	{
		if (zones == null)
		{
			return false;
		}
		foreach (Zone zone in zones)
		{
			foreach (Card card in zone.GetCards())
			{
				if (card.GetEntity().GetCardId() == m_CardSpecificVoData.m_CardId)
				{
					return true;
				}
			}
		}
		return false;
	}

	protected IEnumerator DelayedPlayMulti()
	{
		float delaySec = m_CardSpecificVoData.m_Lines[m_ActiveAudioIndex].m_DelaySec;
		if (delaySec > 0f)
		{
			yield return new WaitForSeconds(delaySec);
		}
		PlayNowMulti();
	}

	protected IEnumerator WaitForSourceThenContinue()
	{
		while (SoundManager.Get().IsActive(m_activeAudioSource))
		{
			yield return 0;
		}
		m_ActiveAudioIndex++;
		m_activeAudioSource = DetermineBestAudioSource();
		if (m_activeAudioSource != null)
		{
			StartCoroutine("DelayedPlayMulti");
		}
		else
		{
			OnStateFinished();
		}
	}
}
