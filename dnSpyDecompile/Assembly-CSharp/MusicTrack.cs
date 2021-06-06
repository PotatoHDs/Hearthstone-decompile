using System;

// Token: 0x02000948 RID: 2376
[Serializable]
public class MusicTrack
{
	// Token: 0x0600830D RID: 33549 RVA: 0x002A7FA0 File Offset: 0x002A61A0
	public MusicTrack Clone()
	{
		return (MusicTrack)base.MemberwiseClone();
	}

	// Token: 0x04006E4F RID: 28239
	[CustomEditField(ListSortable = true)]
	public MusicTrackType m_trackType;

	// Token: 0x04006E50 RID: 28240
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_name;

	// Token: 0x04006E51 RID: 28241
	public float m_volume = 1f;

	// Token: 0x04006E52 RID: 28242
	public bool m_shuffle = true;

	// Token: 0x04006E53 RID: 28243
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_fallback;
}
