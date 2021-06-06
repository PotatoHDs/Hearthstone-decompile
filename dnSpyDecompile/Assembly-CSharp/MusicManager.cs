using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

// Token: 0x0200094C RID: 2380
public class MusicManager : IService
{
	// Token: 0x1700077C RID: 1916
	// (get) Token: 0x06008318 RID: 33560 RVA: 0x002A8127 File Offset: 0x002A6327
	// (set) Token: 0x06008319 RID: 33561 RVA: 0x002A812F File Offset: 0x002A632F
	public MusicConfig Config { get; private set; }

	// Token: 0x0600831A RID: 33562 RVA: 0x002A8138 File Offset: 0x002A6338
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		InstantiatePrefab loadMusicConfig = new InstantiatePrefab("MusicConfig.prefab:0af92217368c85f42ae37bec9a4e3625");
		yield return loadMusicConfig;
		this.Config = loadMusicConfig.InstantiatedPrefab.GetComponent<MusicConfig>();
		if (HearthstoneApplication.Get() != null)
		{
			HearthstoneApplication.Get().WillReset += this.WillReset;
		}
		yield break;
	}

	// Token: 0x0600831B RID: 33563 RVA: 0x002450CA File Offset: 0x002432CA
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(IAssetLoader)
		};
	}

	// Token: 0x0600831C RID: 33564 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x0600831D RID: 33565 RVA: 0x002A8147 File Offset: 0x002A6347
	public static MusicManager Get()
	{
		return HearthstoneServices.Get<MusicManager>();
	}

	// Token: 0x0600831E RID: 33566 RVA: 0x002A8150 File Offset: 0x002A6350
	public bool StartPlaylist(MusicPlaylistType type)
	{
		if (this.m_currentPlaylist == type)
		{
			return true;
		}
		SoundManager soundManager;
		if (!HearthstoneServices.TryGet<SoundManager>(out soundManager))
		{
			Debug.LogError("MusicManager.StartPlaylist() - SoundManager does not exist.");
			return false;
		}
		MusicPlaylist musicPlaylist = this.FindPlaylist(type);
		if (musicPlaylist == null)
		{
			Debug.LogWarning(string.Format("MusicManager.StartPlaylist() - failed to find playlist for type {0}", type));
			return false;
		}
		List<MusicTrack> musicTracks = musicPlaylist.GetMusicTracks();
		List<MusicTrack> currentMusicTracks = soundManager.GetCurrentMusicTracks();
		if (!this.AreTracksEqual(musicTracks, currentMusicTracks))
		{
			soundManager.NukeMusicAndStopPlayingCurrentTrack();
			if (musicTracks != null && musicTracks.Count > 0)
			{
				soundManager.AddMusicTracks(musicTracks);
			}
		}
		List<MusicTrack> ambienceTracks = musicPlaylist.GetAmbienceTracks();
		List<MusicTrack> currentAmbienceTracks = soundManager.GetCurrentAmbienceTracks();
		if (!this.AreTracksEqual(ambienceTracks, currentAmbienceTracks))
		{
			soundManager.NukeAmbienceAndStopPlayingCurrentTrack();
			if (ambienceTracks != null && ambienceTracks.Count > 0)
			{
				soundManager.AddAmbienceTracks(ambienceTracks);
			}
		}
		this.m_currentPlaylist = musicPlaylist.m_type;
		return true;
	}

	// Token: 0x0600831F RID: 33567 RVA: 0x002A8218 File Offset: 0x002A6418
	public bool StopPlaylist()
	{
		SoundManager soundManager = SoundManager.Get();
		if (soundManager == null)
		{
			Debug.LogError("MusicManager.StopPlaylist() - SoundManager does not exist.");
			return false;
		}
		if (this.m_currentPlaylist == MusicPlaylistType.Invalid)
		{
			return false;
		}
		this.m_currentPlaylist = MusicPlaylistType.Invalid;
		soundManager.NukePlaylistsAndStopPlayingCurrentTracks();
		return true;
	}

	// Token: 0x06008320 RID: 33568 RVA: 0x002A8254 File Offset: 0x002A6454
	public MusicPlaylistBookmark CreateBookmarkOfCurrentPlaylist()
	{
		SoundManager soundManager = SoundManager.Get();
		if (soundManager == null)
		{
			Debug.LogError("MusicManager.CreateBookmarkOfCurrentPlaylist() - SoundManager does not exist.");
			return new MusicPlaylistBookmark();
		}
		MusicPlaylistBookmark musicPlaylistBookmark = new MusicPlaylistBookmark();
		musicPlaylistBookmark.m_playListType = this.m_currentPlaylist;
		musicPlaylistBookmark.m_playListIndex = soundManager.GetCurrentMusicTrackIndex();
		musicPlaylistBookmark.m_timeStamp = Time.unscaledTime;
		AudioSource currentMusicTrack = soundManager.GetCurrentMusicTrack();
		if (currentMusicTrack)
		{
			musicPlaylistBookmark.m_trackTime = currentMusicTrack.time;
			musicPlaylistBookmark.m_currentTrack = currentMusicTrack;
		}
		return musicPlaylistBookmark;
	}

	// Token: 0x06008321 RID: 33569 RVA: 0x002A82C8 File Offset: 0x002A64C8
	public bool PlayFromBookmark(MusicPlaylistBookmark bookmark)
	{
		if (bookmark == null || bookmark.m_playListType == MusicPlaylistType.Invalid)
		{
			return false;
		}
		SoundManager sndMgr = SoundManager.Get();
		if (sndMgr == null)
		{
			Debug.LogError("MusicManager.PlayFromBookmark() - SoundManager does not exist.");
			return false;
		}
		Action syncMusic = null;
		syncMusic = delegate()
		{
			SoundManager sndMgr2 = sndMgr;
			sndMgr2.OnMusicStarted = (Action)Delegate.Remove(sndMgr2.OnMusicStarted, syncMusic);
			if (this.m_currentPlaylist != bookmark.m_playListType || sndMgr.GetCurrentMusicTrackIndex() != bookmark.m_playListIndex)
			{
				return;
			}
			if (bookmark.m_currentTrack != null)
			{
				sndMgr.SetCurrentMusicTrackTime(bookmark.m_currentTrack.time);
				return;
			}
			sndMgr.SetCurrentMusicTrackTime(bookmark.m_trackTime);
		};
		SoundManager sndMgr3 = sndMgr;
		sndMgr3.OnMusicStarted = (Action)Delegate.Combine(sndMgr3.OnMusicStarted, syncMusic);
		this.StartPlaylist(bookmark.m_playListType);
		sndMgr.SetCurrentMusicTrackIndex(bookmark.m_playListIndex);
		return true;
	}

	// Token: 0x06008322 RID: 33570 RVA: 0x002A8382 File Offset: 0x002A6582
	public MusicPlaylistType GetCurrentPlaylist()
	{
		return this.m_currentPlaylist;
	}

	// Token: 0x06008323 RID: 33571 RVA: 0x002A838C File Offset: 0x002A658C
	private void WillReset()
	{
		SoundManager soundManager = SoundManager.Get();
		if (soundManager == null)
		{
			Debug.LogError("MusicManager.WillReset() - SoundManager does not exist.");
			return;
		}
		this.m_currentPlaylist = MusicPlaylistType.Invalid;
		soundManager.ImmediatelyKillMusicAndAmbience();
	}

	// Token: 0x06008324 RID: 33572 RVA: 0x002A83BC File Offset: 0x002A65BC
	private MusicPlaylist FindPlaylist(MusicPlaylistType type)
	{
		if (this.Config == null)
		{
			Debug.LogError("MusicManager.FindPlaylist() - MusicConfig does not exist.");
			return null;
		}
		MusicPlaylist musicPlaylist = this.Config.FindPlaylist(type);
		if (musicPlaylist == null)
		{
			Debug.LogWarning(string.Format("MusicManager.FindPlaylist() - {0} playlist is not defined.", type));
			return null;
		}
		return musicPlaylist;
	}

	// Token: 0x06008325 RID: 33573 RVA: 0x002A840C File Offset: 0x002A660C
	private bool AreTracksEqual(List<MusicTrack> newTracks, List<MusicTrack> curTracks)
	{
		if (newTracks.Count != curTracks.Count)
		{
			return false;
		}
		using (List<MusicTrack>.Enumerator enumerator = newTracks.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				MusicTrack newT = enumerator.Current;
				if (curTracks.Find(delegate(MusicTrack curT)
				{
					string a = AssetLoader.Get().IsAssetAvailable(curT.m_name) ? curT.m_name : curT.m_fallback;
					string b = AssetLoader.Get().IsAssetAvailable(newT.m_name) ? newT.m_name : newT.m_fallback;
					return a == b;
				}) == null)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x04006E5C RID: 28252
	private MusicPlaylistType m_currentPlaylist;
}
