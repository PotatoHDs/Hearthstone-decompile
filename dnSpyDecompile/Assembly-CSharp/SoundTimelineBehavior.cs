using System;
using UnityEngine;
using UnityEngine.Playables;

// Token: 0x02000957 RID: 2391
public class SoundTimelineBehavior : PlayableBehaviour
{
	// Token: 0x060083ED RID: 33773 RVA: 0x002AB761 File Offset: 0x002A9961
	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		if (this.m_AudioSource == null)
		{
			this.m_AudioSource = (playerData as AudioSource);
		}
	}

	// Token: 0x060083EE RID: 33774 RVA: 0x002AB77D File Offset: 0x002A997D
	public override void OnBehaviourPlay(Playable playable, FrameData info)
	{
		SoundManager.Get().Play(this.m_AudioSource, null, null, null);
	}

	// Token: 0x060083EF RID: 33775 RVA: 0x002AB793 File Offset: 0x002A9993
	public override void OnBehaviourPause(Playable playable, FrameData info)
	{
		SoundManager.Get().Pause(this.m_AudioSource);
	}

	// Token: 0x04006E9B RID: 28315
	public AudioSource m_AudioSource;

	// Token: 0x04006E9C RID: 28316
	public float m_RandomPitchMin = 1f;

	// Token: 0x04006E9D RID: 28317
	public float m_RandomPitchMax = 1f;

	// Token: 0x04006E9E RID: 28318
	public float m_RandomVolumeMin = 1f;

	// Token: 0x04006E9F RID: 28319
	public float m_RandomVolumeMax = 1f;

	// Token: 0x04006EA0 RID: 28320
	public bool m_IgnoreDucking;
}
