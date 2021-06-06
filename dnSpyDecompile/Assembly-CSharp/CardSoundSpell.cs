using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200077C RID: 1916
public class CardSoundSpell : Spell
{
	// Token: 0x06006C15 RID: 27669 RVA: 0x002300C7 File Offset: 0x0022E2C7
	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.OnBirth(prevStateType);
		this.Play();
	}

	// Token: 0x06006C16 RID: 27670 RVA: 0x002300D6 File Offset: 0x0022E2D6
	protected override void OnNone(SpellStateType prevStateType)
	{
		base.OnNone(prevStateType);
		this.Stop();
	}

	// Token: 0x06006C17 RID: 27671 RVA: 0x002300E5 File Offset: 0x0022E2E5
	public AudioSource GetActiveAudioSource()
	{
		return this.m_activeAudioSource;
	}

	// Token: 0x06006C18 RID: 27672 RVA: 0x002300ED File Offset: 0x0022E2ED
	public void ForceDefaultAudioSource()
	{
		this.m_forceDefaultAudioSource = true;
	}

	// Token: 0x06006C19 RID: 27673 RVA: 0x002300F6 File Offset: 0x0022E2F6
	public bool HasActiveAudioSource()
	{
		return this.m_activeAudioSource != null;
	}

	// Token: 0x06006C1A RID: 27674 RVA: 0x00230104 File Offset: 0x0022E304
	public virtual AudioSource DetermineBestAudioSource()
	{
		return this.m_CardSoundData.m_AudioSource;
	}

	// Token: 0x06006C1B RID: 27675 RVA: 0x000D5239 File Offset: 0x000D3439
	public virtual string DetermineGameStringKey()
	{
		return "";
	}

	// Token: 0x06006C1C RID: 27676 RVA: 0x00230114 File Offset: 0x0022E314
	protected virtual void Play()
	{
		this.Stop();
		this.m_activeAudioSource = (this.m_forceDefaultAudioSource ? this.m_CardSoundData.m_AudioSource : this.DetermineBestAudioSource());
		if (this.m_activeAudioSource == null)
		{
			this.OnStateFinished();
			return;
		}
		base.StartCoroutine("DelayedPlay");
	}

	// Token: 0x06006C1D RID: 27677 RVA: 0x00230169 File Offset: 0x0022E369
	protected virtual void PlayNow()
	{
		SoundManager.Get().Play(this.m_activeAudioSource, null, null, null);
		base.StartCoroutine("WaitForSourceThenFinishState");
	}

	// Token: 0x06006C1E RID: 27678 RVA: 0x0023018B File Offset: 0x0022E38B
	protected virtual void Stop()
	{
		base.StopCoroutine("DelayedPlay");
		base.StopCoroutine("WaitForSourceThenFinishState");
		SoundManager.Get().Stop(this.m_activeAudioSource);
	}

	// Token: 0x06006C1F RID: 27679 RVA: 0x002301B4 File Offset: 0x0022E3B4
	protected IEnumerator DelayedPlay()
	{
		if (this.m_CardSoundData.m_DelaySec > 0f)
		{
			yield return new WaitForSeconds(this.m_CardSoundData.m_DelaySec);
		}
		this.PlayNow();
		yield break;
	}

	// Token: 0x06006C20 RID: 27680 RVA: 0x002301C3 File Offset: 0x0022E3C3
	protected IEnumerator WaitForSourceThenFinishState()
	{
		while (SoundManager.Get().IsActive(this.m_activeAudioSource))
		{
			yield return 0;
		}
		this.OnStateFinished();
		yield break;
	}

	// Token: 0x04005787 RID: 22407
	public CardSoundData m_CardSoundData = new CardSoundData();

	// Token: 0x04005788 RID: 22408
	protected AudioSource m_activeAudioSource;

	// Token: 0x04005789 RID: 22409
	protected bool m_forceDefaultAudioSource;
}
