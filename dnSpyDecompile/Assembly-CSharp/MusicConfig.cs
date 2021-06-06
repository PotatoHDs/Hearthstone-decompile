using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200094A RID: 2378
[CustomEditClass]
public class MusicConfig : MonoBehaviour
{
	// Token: 0x06008313 RID: 33555 RVA: 0x002A80C3 File Offset: 0x002A62C3
	public MusicPlaylist GetPlaylist(MusicPlaylistType type)
	{
		return this.FindPlaylist(type) ?? new MusicPlaylist();
	}

	// Token: 0x06008314 RID: 33556 RVA: 0x002A80D8 File Offset: 0x002A62D8
	public MusicPlaylist FindPlaylist(MusicPlaylistType type)
	{
		for (int i = 0; i < this.m_playlists.Count; i++)
		{
			MusicPlaylist musicPlaylist = this.m_playlists[i];
			if (musicPlaylist.m_type == type)
			{
				return musicPlaylist;
			}
		}
		return null;
	}

	// Token: 0x06008315 RID: 33557 RVA: 0x002A7F7F File Offset: 0x002A617F
	private void Awake()
	{
		base.gameObject.AddComponent<HSDontDestroyOnLoad>();
	}

	// Token: 0x04006E56 RID: 28246
	[CustomEditField(Sections = "Playlists")]
	public List<MusicPlaylist> m_playlists = new List<MusicPlaylist>();
}
