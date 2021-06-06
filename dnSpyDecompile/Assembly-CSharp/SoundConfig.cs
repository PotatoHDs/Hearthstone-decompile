using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000951 RID: 2385
[CustomEditClass]
public class SoundConfig : MonoBehaviour
{
	// Token: 0x0600832D RID: 33581 RVA: 0x002A7F7F File Offset: 0x002A617F
	private void Awake()
	{
		base.gameObject.AddComponent<HSDontDestroyOnLoad>();
	}

	// Token: 0x04006E65 RID: 28261
	[CustomEditField(Sections = "Music")]
	public float m_SecondsBetweenMusicTracks = 10f;

	// Token: 0x04006E66 RID: 28262
	[CustomEditField(Sections = "System Audio Sources")]
	public AudioSource m_PlayClipTemplate;

	// Token: 0x04006E67 RID: 28263
	[CustomEditField(Sections = "System Audio Sources")]
	public AudioSource m_PlaceholderSound;

	// Token: 0x04006E68 RID: 28264
	[CustomEditField(Sections = "Playback Limiting")]
	public List<SoundPlaybackLimitDef> m_PlaybackLimitDefs = new List<SoundPlaybackLimitDef>();

	// Token: 0x04006E69 RID: 28265
	[CustomEditField(Sections = "Ducking")]
	public List<SoundDuckingDef> m_DuckingDefs = new List<SoundDuckingDef>();
}
