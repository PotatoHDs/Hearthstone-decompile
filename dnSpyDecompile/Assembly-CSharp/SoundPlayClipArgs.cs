using System;
using Assets;
using UnityEngine;

// Token: 0x0200095C RID: 2396
public class SoundPlayClipArgs
{
	// Token: 0x04006EAC RID: 28332
	public AudioSource m_templateSource;

	// Token: 0x04006EAD RID: 28333
	public SoundDef m_def;

	// Token: 0x04006EAE RID: 28334
	public AudioClip m_forcedAudioClip;

	// Token: 0x04006EAF RID: 28335
	public float? m_volume;

	// Token: 0x04006EB0 RID: 28336
	public float? m_pitch;

	// Token: 0x04006EB1 RID: 28337
	public float? m_spatialBlend;

	// Token: 0x04006EB2 RID: 28338
	public Global.SoundCategory? m_category;

	// Token: 0x04006EB3 RID: 28339
	public GameObject m_parentObject;
}
