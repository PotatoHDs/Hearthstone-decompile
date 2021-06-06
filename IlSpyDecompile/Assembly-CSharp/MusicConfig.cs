using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class MusicConfig : MonoBehaviour
{
	[CustomEditField(Sections = "Playlists")]
	public List<MusicPlaylist> m_playlists = new List<MusicPlaylist>();

	public MusicPlaylist GetPlaylist(MusicPlaylistType type)
	{
		return FindPlaylist(type) ?? new MusicPlaylist();
	}

	public MusicPlaylist FindPlaylist(MusicPlaylistType type)
	{
		for (int i = 0; i < m_playlists.Count; i++)
		{
			MusicPlaylist musicPlaylist = m_playlists[i];
			if (musicPlaylist.m_type == type)
			{
				return musicPlaylist;
			}
		}
		return null;
	}

	private void Awake()
	{
		base.gameObject.AddComponent<HSDontDestroyOnLoad>();
	}
}
