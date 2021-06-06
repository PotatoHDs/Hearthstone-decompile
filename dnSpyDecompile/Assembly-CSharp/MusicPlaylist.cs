using System;
using System.Collections.Generic;

// Token: 0x02000949 RID: 2377
[Serializable]
public class MusicPlaylist
{
	// Token: 0x0600830F RID: 33551 RVA: 0x002A7FC7 File Offset: 0x002A61C7
	public List<MusicTrack> GetMusicTracks()
	{
		return this.GetRandomizedTracks(this.m_tracks, MusicTrackType.Music);
	}

	// Token: 0x06008310 RID: 33552 RVA: 0x002A7FD6 File Offset: 0x002A61D6
	public List<MusicTrack> GetAmbienceTracks()
	{
		return this.GetRandomizedTracks(this.m_tracks, MusicTrackType.Ambience);
	}

	// Token: 0x06008311 RID: 33553 RVA: 0x002A7FE8 File Offset: 0x002A61E8
	private List<MusicTrack> GetRandomizedTracks(List<MusicTrack> trackList, MusicTrackType type)
	{
		List<MusicTrack> list = new List<MusicTrack>();
		List<MusicTrack> list2 = new List<MusicTrack>();
		foreach (MusicTrack musicTrack in trackList)
		{
			if (type == musicTrack.m_trackType && !string.IsNullOrEmpty(musicTrack.m_name))
			{
				if (musicTrack.m_shuffle)
				{
					list2.Add(musicTrack.Clone());
				}
				else
				{
					list.Add(musicTrack.Clone());
				}
			}
		}
		Random random = new Random();
		while (list2.Count > 0)
		{
			int index = random.Next(0, list2.Count);
			list.Add(list2[index]);
			list2.RemoveAt(index);
		}
		return list;
	}

	// Token: 0x04006E54 RID: 28244
	[CustomEditField(ListSortable = true)]
	public MusicPlaylistType m_type;

	// Token: 0x04006E55 RID: 28245
	[CustomEditField(ListTable = true)]
	public List<MusicTrack> m_tracks = new List<MusicTrack>();
}
