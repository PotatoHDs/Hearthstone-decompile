using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

public class MusicManager : IService
{
	private MusicPlaylistType m_currentPlaylist;

	public MusicConfig Config { get; private set; }

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		InstantiatePrefab loadMusicConfig = new InstantiatePrefab("MusicConfig.prefab:0af92217368c85f42ae37bec9a4e3625");
		yield return loadMusicConfig;
		Config = loadMusicConfig.InstantiatedPrefab.GetComponent<MusicConfig>();
		if (HearthstoneApplication.Get() != null)
		{
			HearthstoneApplication.Get().WillReset += WillReset;
		}
	}

	public Type[] GetDependencies()
	{
		return new Type[1] { typeof(IAssetLoader) };
	}

	public void Shutdown()
	{
	}

	public static MusicManager Get()
	{
		return HearthstoneServices.Get<MusicManager>();
	}

	public bool StartPlaylist(MusicPlaylistType type)
	{
		if (m_currentPlaylist == type)
		{
			return true;
		}
		if (!HearthstoneServices.TryGet<SoundManager>(out var service))
		{
			Debug.LogError("MusicManager.StartPlaylist() - SoundManager does not exist.");
			return false;
		}
		MusicPlaylist musicPlaylist = FindPlaylist(type);
		if (musicPlaylist == null)
		{
			Debug.LogWarning($"MusicManager.StartPlaylist() - failed to find playlist for type {type}");
			return false;
		}
		List<MusicTrack> musicTracks = musicPlaylist.GetMusicTracks();
		List<MusicTrack> currentMusicTracks = service.GetCurrentMusicTracks();
		if (!AreTracksEqual(musicTracks, currentMusicTracks))
		{
			service.NukeMusicAndStopPlayingCurrentTrack();
			if (musicTracks != null && musicTracks.Count > 0)
			{
				service.AddMusicTracks(musicTracks);
			}
		}
		List<MusicTrack> ambienceTracks = musicPlaylist.GetAmbienceTracks();
		List<MusicTrack> currentAmbienceTracks = service.GetCurrentAmbienceTracks();
		if (!AreTracksEqual(ambienceTracks, currentAmbienceTracks))
		{
			service.NukeAmbienceAndStopPlayingCurrentTrack();
			if (ambienceTracks != null && ambienceTracks.Count > 0)
			{
				service.AddAmbienceTracks(ambienceTracks);
			}
		}
		m_currentPlaylist = musicPlaylist.m_type;
		return true;
	}

	public bool StopPlaylist()
	{
		SoundManager soundManager = SoundManager.Get();
		if (soundManager == null)
		{
			Debug.LogError("MusicManager.StopPlaylist() - SoundManager does not exist.");
			return false;
		}
		if (m_currentPlaylist == MusicPlaylistType.Invalid)
		{
			return false;
		}
		m_currentPlaylist = MusicPlaylistType.Invalid;
		soundManager.NukePlaylistsAndStopPlayingCurrentTracks();
		return true;
	}

	public MusicPlaylistBookmark CreateBookmarkOfCurrentPlaylist()
	{
		SoundManager soundManager = SoundManager.Get();
		if (soundManager == null)
		{
			Debug.LogError("MusicManager.CreateBookmarkOfCurrentPlaylist() - SoundManager does not exist.");
			return new MusicPlaylistBookmark();
		}
		MusicPlaylistBookmark musicPlaylistBookmark = new MusicPlaylistBookmark();
		musicPlaylistBookmark.m_playListType = m_currentPlaylist;
		musicPlaylistBookmark.m_playListIndex = soundManager.GetCurrentMusicTrackIndex();
		musicPlaylistBookmark.m_timeStamp = Time.unscaledTime;
		AudioSource currentMusicTrack = soundManager.GetCurrentMusicTrack();
		if ((bool)currentMusicTrack)
		{
			musicPlaylistBookmark.m_trackTime = currentMusicTrack.time;
			musicPlaylistBookmark.m_currentTrack = currentMusicTrack;
		}
		return musicPlaylistBookmark;
	}

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
		syncMusic = delegate
		{
			SoundManager soundManager2 = sndMgr;
			soundManager2.OnMusicStarted = (Action)Delegate.Remove(soundManager2.OnMusicStarted, syncMusic);
			if (m_currentPlaylist == bookmark.m_playListType && sndMgr.GetCurrentMusicTrackIndex() == bookmark.m_playListIndex)
			{
				if (bookmark.m_currentTrack != null)
				{
					sndMgr.SetCurrentMusicTrackTime(bookmark.m_currentTrack.time);
				}
				else
				{
					sndMgr.SetCurrentMusicTrackTime(bookmark.m_trackTime);
				}
			}
		};
		SoundManager soundManager = sndMgr;
		soundManager.OnMusicStarted = (Action)Delegate.Combine(soundManager.OnMusicStarted, syncMusic);
		StartPlaylist(bookmark.m_playListType);
		sndMgr.SetCurrentMusicTrackIndex(bookmark.m_playListIndex);
		return true;
	}

	public MusicPlaylistType GetCurrentPlaylist()
	{
		return m_currentPlaylist;
	}

	private void WillReset()
	{
		SoundManager soundManager = SoundManager.Get();
		if (soundManager == null)
		{
			Debug.LogError("MusicManager.WillReset() - SoundManager does not exist.");
			return;
		}
		m_currentPlaylist = MusicPlaylistType.Invalid;
		soundManager.ImmediatelyKillMusicAndAmbience();
	}

	private MusicPlaylist FindPlaylist(MusicPlaylistType type)
	{
		if (Config == null)
		{
			Debug.LogError("MusicManager.FindPlaylist() - MusicConfig does not exist.");
			return null;
		}
		MusicPlaylist musicPlaylist = Config.FindPlaylist(type);
		if (musicPlaylist == null)
		{
			Debug.LogWarning($"MusicManager.FindPlaylist() - {type} playlist is not defined.");
			return null;
		}
		return musicPlaylist;
	}

	private bool AreTracksEqual(List<MusicTrack> newTracks, List<MusicTrack> curTracks)
	{
		if (newTracks.Count != curTracks.Count)
		{
			return false;
		}
		foreach (MusicTrack newT in newTracks)
		{
			if (curTracks.Find(delegate(MusicTrack curT)
			{
				string obj = (AssetLoader.Get().IsAssetAvailable(curT.m_name) ? curT.m_name : curT.m_fallback);
				string text = (AssetLoader.Get().IsAssetAvailable(newT.m_name) ? newT.m_name : newT.m_fallback);
				return obj == text;
			}) == null)
			{
				return false;
			}
		}
		return true;
	}
}
