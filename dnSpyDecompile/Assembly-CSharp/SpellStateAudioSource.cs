using System;
using System.Collections;
using Assets;
using UnityEngine;

// Token: 0x02000976 RID: 2422
[Serializable]
public class SpellStateAudioSource
{
	// Token: 0x06008576 RID: 34166 RVA: 0x002B182F File Offset: 0x002AFA2F
	public void Init()
	{
		if (this.m_AudioSource == null)
		{
			return;
		}
		this.m_AudioSource.playOnAwake = false;
	}

	// Token: 0x06008577 RID: 34167 RVA: 0x002B184C File Offset: 0x002AFA4C
	public void Play(SpellState parent)
	{
		if (!this.m_Enabled)
		{
			return;
		}
		if (Mathf.Approximately(this.m_StartDelaySec, 0f))
		{
			this.PlayNow();
			return;
		}
		parent.StartCoroutine(this.DelayedPlay());
	}

	// Token: 0x06008578 RID: 34168 RVA: 0x002B187D File Offset: 0x002AFA7D
	public void Stop()
	{
		if (!this.m_Enabled)
		{
			return;
		}
		if (this.m_AudioSource == null)
		{
			return;
		}
		if (this.m_PlayGlobally)
		{
			return;
		}
		if (this.m_StopOnStateChange)
		{
			this.m_AudioSource.Stop();
		}
	}

	// Token: 0x06008579 RID: 34169 RVA: 0x002B18B3 File Offset: 0x002AFAB3
	private IEnumerator DelayedPlay()
	{
		yield return new WaitForSeconds(this.m_StartDelaySec);
		this.PlayNow();
		yield break;
	}

	// Token: 0x0600857A RID: 34170 RVA: 0x002B18C4 File Offset: 0x002AFAC4
	private void PlayNow()
	{
		if (this.m_AudioSource == null)
		{
			return;
		}
		if (this.m_PlayGlobally)
		{
			SoundPlayClipArgs soundPlayClipArgs = new SoundPlayClipArgs();
			soundPlayClipArgs.m_def = SoundManager.Get().GetSoundDef(this.m_AudioSource);
			soundPlayClipArgs.m_volume = new float?(this.m_AudioSource.volume);
			soundPlayClipArgs.m_pitch = new float?(this.m_AudioSource.pitch);
			soundPlayClipArgs.m_category = new Global.SoundCategory?(SoundManager.Get().GetCategory(this.m_AudioSource));
			soundPlayClipArgs.m_parentObject = this.m_AudioSource.gameObject;
			SoundManager.Get().PlayClip(soundPlayClipArgs, true, null);
			return;
		}
		SoundManager.Get().Play(this.m_AudioSource, null, null, null);
	}

	// Token: 0x04006FE1 RID: 28641
	public AudioSource m_AudioSource;

	// Token: 0x04006FE2 RID: 28642
	public float m_StartDelaySec;

	// Token: 0x04006FE3 RID: 28643
	public bool m_PlayGlobally;

	// Token: 0x04006FE4 RID: 28644
	public bool m_StopOnStateChange;

	// Token: 0x04006FE5 RID: 28645
	public string m_Comment;

	// Token: 0x04006FE6 RID: 28646
	public bool m_Enabled = true;
}
