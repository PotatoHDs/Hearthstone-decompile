using System;

[Serializable]
public class MusicTrack
{
	[CustomEditField(ListSortable = true)]
	public MusicTrackType m_trackType;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_name;

	public float m_volume = 1f;

	public bool m_shuffle = true;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_fallback;

	public MusicTrack Clone()
	{
		return (MusicTrack)MemberwiseClone();
	}
}
