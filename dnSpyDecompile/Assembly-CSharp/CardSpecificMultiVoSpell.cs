using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200077F RID: 1919
public class CardSpecificMultiVoSpell : CardSoundSpell
{
	// Token: 0x06006C24 RID: 27684 RVA: 0x0023021C File Offset: 0x0022E41C
	protected override void Play()
	{
		if (!this.m_forceDefaultAudioSource)
		{
			this.m_SpecificCardFound = this.SearchForCard();
		}
		if (!this.m_SpecificCardFound)
		{
			base.Play();
			return;
		}
		this.Stop();
		this.m_ActiveAudioIndex = 0;
		this.m_activeAudioSource = (this.m_forceDefaultAudioSource ? this.m_CardSoundData.m_AudioSource : this.DetermineBestAudioSource());
		if (this.m_activeAudioSource == null)
		{
			this.OnStateFinished();
			return;
		}
		base.StartCoroutine("DelayedPlayMulti");
	}

	// Token: 0x06006C25 RID: 27685 RVA: 0x0023029B File Offset: 0x0022E49B
	protected virtual void PlayNowMulti()
	{
		SoundManager.Get().Play(this.m_activeAudioSource, null, null, null);
		base.StartCoroutine("WaitForSourceThenContinue");
	}

	// Token: 0x06006C26 RID: 27686 RVA: 0x002302BD File Offset: 0x0022E4BD
	protected override void Stop()
	{
		base.StopCoroutine("WaitForSourceThenContinue");
		base.Stop();
	}

	// Token: 0x06006C27 RID: 27687 RVA: 0x002302D0 File Offset: 0x0022E4D0
	public override AudioSource DetermineBestAudioSource()
	{
		if (!this.m_SpecificCardFound)
		{
			return base.DetermineBestAudioSource();
		}
		if (this.m_ActiveAudioIndex < this.m_CardSpecificVoData.m_Lines.Length)
		{
			return this.m_CardSpecificVoData.m_Lines[this.m_ActiveAudioIndex].m_AudioSource;
		}
		return null;
	}

	// Token: 0x06006C28 RID: 27688 RVA: 0x00230310 File Offset: 0x0022E510
	private bool SearchForCard()
	{
		if (string.IsNullOrEmpty(this.m_CardSpecificVoData.m_CardId))
		{
			return false;
		}
		foreach (SpellZoneTag zoneTag in this.m_CardSpecificVoData.m_ZonesToSearch)
		{
			List<Zone> zones = SpellUtils.FindZonesFromTag(this, zoneTag, this.m_CardSpecificVoData.m_SideToSearch);
			if (this.IsCardInZones(zones))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06006C29 RID: 27689 RVA: 0x00230398 File Offset: 0x0022E598
	private bool IsCardInZones(List<Zone> zones)
	{
		if (zones == null)
		{
			return false;
		}
		foreach (Zone zone in zones)
		{
			using (List<Card>.Enumerator enumerator2 = zone.GetCards().GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.GetEntity().GetCardId() == this.m_CardSpecificVoData.m_CardId)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06006C2A RID: 27690 RVA: 0x00230440 File Offset: 0x0022E640
	protected IEnumerator DelayedPlayMulti()
	{
		float delaySec = this.m_CardSpecificVoData.m_Lines[this.m_ActiveAudioIndex].m_DelaySec;
		if (delaySec > 0f)
		{
			yield return new WaitForSeconds(delaySec);
		}
		this.PlayNowMulti();
		yield break;
	}

	// Token: 0x06006C2B RID: 27691 RVA: 0x0023044F File Offset: 0x0022E64F
	protected IEnumerator WaitForSourceThenContinue()
	{
		while (SoundManager.Get().IsActive(this.m_activeAudioSource))
		{
			yield return 0;
		}
		this.m_ActiveAudioIndex++;
		this.m_activeAudioSource = this.DetermineBestAudioSource();
		if (this.m_activeAudioSource != null)
		{
			base.StartCoroutine("DelayedPlayMulti");
		}
		else
		{
			this.OnStateFinished();
		}
		yield break;
	}

	// Token: 0x04005790 RID: 22416
	public CardSpecificMultiVoData m_CardSpecificVoData = new CardSpecificMultiVoData();

	// Token: 0x04005791 RID: 22417
	private int m_ActiveAudioIndex;

	// Token: 0x04005792 RID: 22418
	private bool m_SpecificCardFound;
}
