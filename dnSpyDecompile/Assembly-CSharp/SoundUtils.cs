using System;
using System.Collections.Generic;
using Assets;
using UnityEngine;

// Token: 0x0200095E RID: 2398
public class SoundUtils
{
	// Token: 0x060083FA RID: 33786 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public static bool IsDeviceBackgroundMusicPlaying()
	{
		return false;
	}

	// Token: 0x060083FB RID: 33787 RVA: 0x002AB93C File Offset: 0x002A9B3C
	public static Option GetCategoryEnabledOption(Global.SoundCategory cat)
	{
		Option result = Option.INVALID;
		SoundDataTables.s_categoryEnabledOptionMap.TryGetValue(cat, out result);
		return result;
	}

	// Token: 0x060083FC RID: 33788 RVA: 0x002AB95C File Offset: 0x002A9B5C
	public static Option GetCategoryVolumeOption(Global.SoundCategory cat)
	{
		Option result = Option.INVALID;
		SoundDataTables.s_categoryVolumeOptionMap.TryGetValue(cat, out result);
		return result;
	}

	// Token: 0x060083FD RID: 33789 RVA: 0x002AB97C File Offset: 0x002A9B7C
	public static float GetOptionVolume(Option option)
	{
		float num = Mathf.Clamp01(Options.Get().GetFloat(option));
		float num2 = SoundDataTables.s_optionVolumeMaxMap.ContainsKey(option) ? SoundDataTables.s_optionVolumeMaxMap[option] : 1f;
		return num * num2;
	}

	// Token: 0x060083FE RID: 33790 RVA: 0x002AB9BC File Offset: 0x002A9BBC
	public static float GetCategoryVolume(Global.SoundCategory cat)
	{
		Cheats cheats = Cheats.Get();
		if (cheats != null && !cheats.IsSoundCategoryEnabled(cat))
		{
			return 0f;
		}
		Option categoryVolumeOption = SoundUtils.GetCategoryVolumeOption(cat);
		if (categoryVolumeOption == Option.INVALID)
		{
			return 1f;
		}
		return SoundUtils.GetOptionVolume(categoryVolumeOption);
	}

	// Token: 0x060083FF RID: 33791 RVA: 0x002AB9F7 File Offset: 0x002A9BF7
	public static bool IsMusicCategory(Global.SoundCategory cat)
	{
		return cat == Global.SoundCategory.MUSIC || cat == Global.SoundCategory.SPECIAL_MUSIC;
	}

	// Token: 0x06008400 RID: 33792 RVA: 0x002ABA06 File Offset: 0x002A9C06
	public static bool IsVoiceCategory(Global.SoundCategory cat)
	{
		return cat == Global.SoundCategory.VO || cat == Global.SoundCategory.SPECIAL_VO;
	}

	// Token: 0x06008401 RID: 33793 RVA: 0x002ABA18 File Offset: 0x002A9C18
	public static Global.SoundCategory GetCategoryFromSource(AudioSource source)
	{
		SoundDef component = source.GetComponent<SoundDef>();
		if (component == null)
		{
			return Global.SoundCategory.NONE;
		}
		return component.m_Category;
	}

	// Token: 0x06008402 RID: 33794 RVA: 0x002ABA3D File Offset: 0x002A9C3D
	public static bool CanDetectVolume()
	{
		return SoundUtils.PlATFORM_CAN_DETECT_VOLUME;
	}

	// Token: 0x06008403 RID: 33795 RVA: 0x002ABA49 File Offset: 0x002A9C49
	public static void SetVolumes(Component c, float volume, bool includeInactive = false)
	{
		if (!c)
		{
			return;
		}
		SoundUtils.SetVolumes(c.gameObject, volume, false);
	}

	// Token: 0x06008404 RID: 33796 RVA: 0x002ABA64 File Offset: 0x002A9C64
	public static void SetVolumes(GameObject go, float volume, bool includeInactive = false)
	{
		if (!go)
		{
			return;
		}
		foreach (AudioSource source in go.GetComponentsInChildren<AudioSource>(includeInactive))
		{
			SoundManager.Get().SetVolume(source, volume);
		}
	}

	// Token: 0x06008405 RID: 33797 RVA: 0x002ABAA0 File Offset: 0x002A9CA0
	public static void SetSourceVolumes(Component c, float volume, bool includeInactive = false)
	{
		if (!c)
		{
			return;
		}
		SoundUtils.SetSourceVolumes(c.gameObject, volume, false);
	}

	// Token: 0x06008406 RID: 33798 RVA: 0x002ABAB8 File Offset: 0x002A9CB8
	public static void SetSourceVolumes(GameObject go, float volume, bool includeInactive = false)
	{
		if (!go)
		{
			return;
		}
		AudioSource[] componentsInChildren = go.GetComponentsInChildren<AudioSource>(includeInactive);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].volume = volume;
		}
	}

	// Token: 0x06008407 RID: 33799 RVA: 0x002ABAF0 File Offset: 0x002A9CF0
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
		foreach (RandomAudioClip randomAudioClip in list)
		{
			num += randomAudioClip.m_Weight;
		}
		float num2 = UnityEngine.Random.Range(0f, num);
		float num3 = 0f;
		int num4 = list.Count - 1;
		for (int i = 0; i < num4; i++)
		{
			RandomAudioClip randomAudioClip2 = list[i];
			num3 += randomAudioClip2.m_Weight;
			if (num2 <= num3)
			{
				return randomAudioClip2.m_Clip;
			}
		}
		return list[num4].m_Clip;
	}

	// Token: 0x06008408 RID: 33800 RVA: 0x002ABBD8 File Offset: 0x002A9DD8
	public static float GetRandomVolumeFromDef(SoundDef def)
	{
		if (def == null)
		{
			return 1f;
		}
		return UnityEngine.Random.Range(def.m_RandomVolumeMin, def.m_RandomVolumeMax);
	}

	// Token: 0x06008409 RID: 33801 RVA: 0x002ABBFA File Offset: 0x002A9DFA
	public static float GetRandomPitchFromDef(SoundDef def)
	{
		if (def == null)
		{
			return 1f;
		}
		return UnityEngine.Random.Range(def.m_RandomPitchMin, def.m_RandomPitchMax);
	}

	// Token: 0x0600840A RID: 33802 RVA: 0x002ABC1C File Offset: 0x002A9E1C
	public static void CopyDuckedCategoryDef(SoundDuckedCategoryDef src, SoundDuckedCategoryDef dst)
	{
		dst.m_Category = src.m_Category;
		dst.m_Volume = src.m_Volume;
		dst.m_BeginSec = src.m_BeginSec;
		dst.m_BeginEaseType = src.m_BeginEaseType;
		dst.m_RestoreSec = src.m_RestoreSec;
		dst.m_RestoreEaseType = src.m_RestoreEaseType;
	}

	// Token: 0x0600840B RID: 33803 RVA: 0x002ABC74 File Offset: 0x002A9E74
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
				UnityEngine.Object.DestroyImmediate(component2);
				return;
			}
		}
		else
		{
			SoundDef soundDef = dst.GetComponent<SoundDef>();
			if (soundDef == null)
			{
				soundDef = dst.gameObject.AddComponent<SoundDef>();
			}
			SoundUtils.CopySoundDef(component, soundDef);
		}
	}

	// Token: 0x0600840C RID: 33804 RVA: 0x002ABD74 File Offset: 0x002A9F74
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

	// Token: 0x0600840D RID: 33805 RVA: 0x002ABE0C File Offset: 0x002AA00C
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

	// Token: 0x0600840E RID: 33806 RVA: 0x002ABFB8 File Offset: 0x002AA1B8
	public static bool AddAudioSourceComponents(GameObject go)
	{
		bool result = false;
		AudioSource audioSource = go.GetComponent<AudioSource>();
		if (audioSource == null)
		{
			audioSource = go.AddComponent<AudioSource>();
			SoundUtils.ChangeAudioSourceSettings(audioSource, new AudioSourceSettings());
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

	// Token: 0x04006EDE RID: 28382
	public static readonly AssetReference SquarePanelSlideOnSFX = new AssetReference("UI_SquarePanel_slide_on.prefab:777a4a40258158040ad5bc27596ba51e");

	// Token: 0x04006EDF RID: 28383
	public static readonly AssetReference SquarePanelSlideOffSFX = new AssetReference("UI_SquarePanel_slide_off.prefab:9e10f244ba0586e44beca5b547684d3f");

	// Token: 0x04006EE0 RID: 28384
	public static PlatformDependentValue<bool> PlATFORM_CAN_DETECT_VOLUME = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		PC = true,
		Mac = true,
		iOS = false,
		Android = false
	};
}
