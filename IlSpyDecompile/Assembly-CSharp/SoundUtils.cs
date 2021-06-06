using System.Collections.Generic;
using Assets;
using UnityEngine;

public class SoundUtils
{
	public static readonly AssetReference SquarePanelSlideOnSFX = new AssetReference("UI_SquarePanel_slide_on.prefab:777a4a40258158040ad5bc27596ba51e");

	public static readonly AssetReference SquarePanelSlideOffSFX = new AssetReference("UI_SquarePanel_slide_off.prefab:9e10f244ba0586e44beca5b547684d3f");

	public static PlatformDependentValue<bool> PlATFORM_CAN_DETECT_VOLUME = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		PC = true,
		Mac = true,
		iOS = false,
		Android = false
	};

	public static bool IsDeviceBackgroundMusicPlaying()
	{
		return false;
	}

	public static Option GetCategoryEnabledOption(Global.SoundCategory cat)
	{
		Option value = Option.INVALID;
		SoundDataTables.s_categoryEnabledOptionMap.TryGetValue(cat, out value);
		return value;
	}

	public static Option GetCategoryVolumeOption(Global.SoundCategory cat)
	{
		Option value = Option.INVALID;
		SoundDataTables.s_categoryVolumeOptionMap.TryGetValue(cat, out value);
		return value;
	}

	public static float GetOptionVolume(Option option)
	{
		float num = Mathf.Clamp01(Options.Get().GetFloat(option));
		float num2 = (SoundDataTables.s_optionVolumeMaxMap.ContainsKey(option) ? SoundDataTables.s_optionVolumeMaxMap[option] : 1f);
		return num * num2;
	}

	public static float GetCategoryVolume(Global.SoundCategory cat)
	{
		Cheats cheats = Cheats.Get();
		if (cheats != null && !cheats.IsSoundCategoryEnabled(cat))
		{
			return 0f;
		}
		Option categoryVolumeOption = GetCategoryVolumeOption(cat);
		if (categoryVolumeOption == Option.INVALID)
		{
			return 1f;
		}
		return GetOptionVolume(categoryVolumeOption);
	}

	public static bool IsMusicCategory(Global.SoundCategory cat)
	{
		return cat switch
		{
			Global.SoundCategory.MUSIC => true, 
			Global.SoundCategory.SPECIAL_MUSIC => true, 
			_ => false, 
		};
	}

	public static bool IsVoiceCategory(Global.SoundCategory cat)
	{
		return cat switch
		{
			Global.SoundCategory.VO => true, 
			Global.SoundCategory.SPECIAL_VO => true, 
			_ => false, 
		};
	}

	public static Global.SoundCategory GetCategoryFromSource(AudioSource source)
	{
		SoundDef component = source.GetComponent<SoundDef>();
		if (component == null)
		{
			return Global.SoundCategory.NONE;
		}
		return component.m_Category;
	}

	public static bool CanDetectVolume()
	{
		return PlATFORM_CAN_DETECT_VOLUME;
	}

	public static void SetVolumes(Component c, float volume, bool includeInactive = false)
	{
		if ((bool)c)
		{
			SetVolumes(c.gameObject, volume);
		}
	}

	public static void SetVolumes(GameObject go, float volume, bool includeInactive = false)
	{
		if ((bool)go)
		{
			AudioSource[] componentsInChildren = go.GetComponentsInChildren<AudioSource>(includeInactive);
			foreach (AudioSource source in componentsInChildren)
			{
				SoundManager.Get().SetVolume(source, volume);
			}
		}
	}

	public static void SetSourceVolumes(Component c, float volume, bool includeInactive = false)
	{
		if ((bool)c)
		{
			SetSourceVolumes(c.gameObject, volume);
		}
	}

	public static void SetSourceVolumes(GameObject go, float volume, bool includeInactive = false)
	{
		if ((bool)go)
		{
			AudioSource[] componentsInChildren = go.GetComponentsInChildren<AudioSource>(includeInactive);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].volume = volume;
			}
		}
	}

	public static string GetRandomClipFromDef(SoundDef def)
	{
		if (def == null)
		{
			return null;
		}
		List<RandomAudioClip> list = def.m_RandomClips;
		if (def is IMultipleRandomClipSoundDef)
		{
			list = ((IMultipleRandomClipSoundDef)def).GetRandomAudioClips();
		}
		if (list == null)
		{
			return null;
		}
		if (list.Count == 0)
		{
			return null;
		}
		float num = 0f;
		foreach (RandomAudioClip item in list)
		{
			num += item.m_Weight;
		}
		float num2 = Random.Range(0f, num);
		float num3 = 0f;
		int num4 = list.Count - 1;
		for (int i = 0; i < num4; i++)
		{
			RandomAudioClip randomAudioClip = list[i];
			num3 += randomAudioClip.m_Weight;
			if (num2 <= num3)
			{
				return randomAudioClip.m_Clip;
			}
		}
		return list[num4].m_Clip;
	}

	public static float GetRandomVolumeFromDef(SoundDef def)
	{
		if (def == null)
		{
			return 1f;
		}
		return Random.Range(def.m_RandomVolumeMin, def.m_RandomVolumeMax);
	}

	public static float GetRandomPitchFromDef(SoundDef def)
	{
		if (def == null)
		{
			return 1f;
		}
		return Random.Range(def.m_RandomPitchMin, def.m_RandomPitchMax);
	}

	public static void CopyDuckedCategoryDef(SoundDuckedCategoryDef src, SoundDuckedCategoryDef dst)
	{
		dst.m_Category = src.m_Category;
		dst.m_Volume = src.m_Volume;
		dst.m_BeginSec = src.m_BeginSec;
		dst.m_BeginEaseType = src.m_BeginEaseType;
		dst.m_RestoreSec = src.m_RestoreSec;
		dst.m_RestoreEaseType = src.m_RestoreEaseType;
	}

	public static void CopyAudioSource(AudioSource src, AudioSource dst)
	{
		dst.clip = src.clip;
		dst.bypassEffects = src.bypassEffects;
		dst.loop = src.loop;
		dst.priority = src.priority;
		dst.volume = src.volume;
		dst.pitch = src.pitch;
		dst.panStereo = src.panStereo;
		dst.spatialBlend = src.spatialBlend;
		dst.reverbZoneMix = src.reverbZoneMix;
		dst.rolloffMode = src.rolloffMode;
		dst.dopplerLevel = src.dopplerLevel;
		dst.minDistance = src.minDistance;
		dst.maxDistance = src.maxDistance;
		dst.spread = src.spread;
		SoundDef component = src.GetComponent<SoundDef>();
		if (component == null)
		{
			SoundDef component2 = dst.GetComponent<SoundDef>();
			if (component2 != null)
			{
				Object.DestroyImmediate(component2);
			}
			return;
		}
		SoundDef soundDef = dst.GetComponent<SoundDef>();
		if (soundDef == null)
		{
			soundDef = dst.gameObject.AddComponent<SoundDef>();
		}
		CopySoundDef(component, soundDef);
	}

	public static void CopySoundDef(SoundDef src, SoundDef dst)
	{
		dst.m_Category = src.m_Category;
		dst.m_RandomClips = new List<RandomAudioClip>();
		if (src.m_RandomClips != null)
		{
			for (int i = 0; i < src.m_RandomClips.Count; i++)
			{
				dst.m_RandomClips.Add(src.m_RandomClips[i]);
			}
		}
		dst.m_RandomPitchMin = src.m_RandomPitchMin;
		dst.m_RandomPitchMax = src.m_RandomPitchMax;
		dst.m_RandomVolumeMin = src.m_RandomVolumeMin;
		dst.m_RandomVolumeMax = src.m_RandomVolumeMax;
		dst.m_IgnoreDucking = src.m_IgnoreDucking;
	}

	public static bool ChangeAudioSourceSettings(AudioSource source, AudioSourceSettings settings)
	{
		bool result = false;
		if (source.bypassEffects != settings.m_bypassEffects)
		{
			source.bypassEffects = settings.m_bypassEffects;
			result = true;
		}
		if (source.loop != settings.m_loop)
		{
			source.loop = settings.m_loop;
			result = true;
		}
		if (source.priority != settings.m_priority)
		{
			source.priority = settings.m_priority;
			result = true;
		}
		if (!Mathf.Approximately(source.volume, settings.m_volume))
		{
			source.volume = settings.m_volume;
			result = true;
		}
		if (!Mathf.Approximately(source.pitch, settings.m_pitch))
		{
			source.pitch = settings.m_pitch;
			result = true;
		}
		if (!Mathf.Approximately(source.panStereo, settings.m_stereoPan))
		{
			source.panStereo = settings.m_stereoPan;
			result = true;
		}
		if (!Mathf.Approximately(source.spatialBlend, settings.m_spatialBlend))
		{
			source.spatialBlend = settings.m_spatialBlend;
			result = true;
		}
		if (!Mathf.Approximately(source.reverbZoneMix, settings.m_reverbZoneMix))
		{
			source.reverbZoneMix = settings.m_reverbZoneMix;
			result = true;
		}
		if (source.rolloffMode != settings.m_rolloffMode)
		{
			source.rolloffMode = settings.m_rolloffMode;
			result = true;
		}
		if (!Mathf.Approximately(source.dopplerLevel, settings.m_dopplerLevel))
		{
			source.dopplerLevel = settings.m_dopplerLevel;
			result = true;
		}
		if (!Mathf.Approximately(source.minDistance, settings.m_minDistance))
		{
			source.minDistance = settings.m_minDistance;
			result = true;
		}
		if (!Mathf.Approximately(source.maxDistance, settings.m_maxDistance))
		{
			source.maxDistance = settings.m_maxDistance;
			result = true;
		}
		if (!Mathf.Approximately(source.spread, settings.m_spread))
		{
			source.spread = settings.m_spread;
			result = true;
		}
		return result;
	}

	public static bool AddAudioSourceComponents(GameObject go)
	{
		bool result = false;
		AudioSource audioSource = go.GetComponent<AudioSource>();
		if (audioSource == null)
		{
			audioSource = go.AddComponent<AudioSource>();
			ChangeAudioSourceSettings(audioSource, new AudioSourceSettings());
			result = true;
		}
		if (audioSource.playOnAwake)
		{
			audioSource.playOnAwake = false;
			result = true;
		}
		if (go.GetComponent<SoundDef>() == null)
		{
			result = true;
			go.AddComponent<SoundDef>();
		}
		return result;
	}
}
