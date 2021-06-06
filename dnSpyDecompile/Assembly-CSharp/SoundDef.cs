using System;
using System.Collections.Generic;
using Assets;
using UnityEngine;

// Token: 0x02000953 RID: 2387
[CustomEditClass]
public class SoundDef : MonoBehaviour
{
	// Token: 0x06008330 RID: 33584 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Awake()
	{
	}

	// Token: 0x04006E6C RID: 28268
	[CustomEditField(T = EditType.AUDIO_CLIP)]
	public string m_AudioClip;

	// Token: 0x04006E6D RID: 28269
	public Global.SoundCategory m_Category = Global.SoundCategory.FX;

	// Token: 0x04006E6E RID: 28270
	public List<RandomAudioClip> m_RandomClips;

	// Token: 0x04006E6F RID: 28271
	public float m_RandomPitchMin = 1f;

	// Token: 0x04006E70 RID: 28272
	public float m_RandomPitchMax = 1f;

	// Token: 0x04006E71 RID: 28273
	public float m_RandomVolumeMin = 1f;

	// Token: 0x04006E72 RID: 28274
	public float m_RandomVolumeMax = 1f;

	// Token: 0x04006E73 RID: 28275
	public bool m_IgnoreDucking;

	// Token: 0x04006E74 RID: 28276
	public bool m_persistPastGameEnd;
}
