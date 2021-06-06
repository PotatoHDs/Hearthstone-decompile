using System;
using UnityEngine;

// Token: 0x0200095D RID: 2397
public class AudioSourceSettings
{
	// Token: 0x060083F8 RID: 33784 RVA: 0x002AB89D File Offset: 0x002A9A9D
	public AudioSourceSettings()
	{
		this.LoadDefaults();
	}

	// Token: 0x060083F9 RID: 33785 RVA: 0x002AB8AC File Offset: 0x002A9AAC
	public void LoadDefaults()
	{
		this.m_bypassEffects = false;
		this.m_loop = false;
		this.m_priority = 128;
		this.m_volume = 1f;
		this.m_pitch = 1f;
		this.m_stereoPan = 0f;
		this.m_spatialBlend = 1f;
		this.m_reverbZoneMix = 1f;
		this.m_rolloffMode = AudioRolloffMode.Linear;
		this.m_dopplerLevel = 1f;
		this.m_minDistance = 100f;
		this.m_maxDistance = 500f;
		this.m_spread = 0f;
	}

	// Token: 0x04006EB4 RID: 28340
	public const bool DEFAULT_BYPASS_EFFECTS = false;

	// Token: 0x04006EB5 RID: 28341
	public const bool DEFAULT_LOOP = false;

	// Token: 0x04006EB6 RID: 28342
	public const float MIN_VOLUME = 0f;

	// Token: 0x04006EB7 RID: 28343
	public const float MAX_VOLUME = 1f;

	// Token: 0x04006EB8 RID: 28344
	public const float DEFAULT_VOLUME = 1f;

	// Token: 0x04006EB9 RID: 28345
	public const int MIN_PRIORITY = 0;

	// Token: 0x04006EBA RID: 28346
	public const int MAX_PRIORITY = 256;

	// Token: 0x04006EBB RID: 28347
	public const int DEFAULT_PRIORITY = 128;

	// Token: 0x04006EBC RID: 28348
	public const float MIN_PITCH = -3f;

	// Token: 0x04006EBD RID: 28349
	public const float MAX_PITCH = 3f;

	// Token: 0x04006EBE RID: 28350
	public const float DEFAULT_PITCH = 1f;

	// Token: 0x04006EBF RID: 28351
	public const float MIN_STEREO_PAN = -1f;

	// Token: 0x04006EC0 RID: 28352
	public const float MAX_STEREO_PAN = 1f;

	// Token: 0x04006EC1 RID: 28353
	public const float DEFAULT_STEREO_PAN = 0f;

	// Token: 0x04006EC2 RID: 28354
	public const float MIN_SPATIAL_BLEND = 0f;

	// Token: 0x04006EC3 RID: 28355
	public const float MAX_SPATIAL_BLEND = 1f;

	// Token: 0x04006EC4 RID: 28356
	public const float DEFAULT_SPATIAL_BLEND = 1f;

	// Token: 0x04006EC5 RID: 28357
	public const float MIN_REVERB_ZONE_MIX = 0f;

	// Token: 0x04006EC6 RID: 28358
	public const float MAX_REVERB_ZONE_MIX = 1.1f;

	// Token: 0x04006EC7 RID: 28359
	public const float DEFAULT_REVERB_ZONE_MIX = 1f;

	// Token: 0x04006EC8 RID: 28360
	public const AudioRolloffMode DEFAULT_ROLLOFF_MODE = AudioRolloffMode.Linear;

	// Token: 0x04006EC9 RID: 28361
	public const float MIN_DOPPLER_LEVEL = 0f;

	// Token: 0x04006ECA RID: 28362
	public const float MAX_DOPPLER_LEVEL = 5f;

	// Token: 0x04006ECB RID: 28363
	public const float DEFAULT_DOPPLER_LEVEL = 1f;

	// Token: 0x04006ECC RID: 28364
	public const float DEFAULT_MIN_DISTANCE = 100f;

	// Token: 0x04006ECD RID: 28365
	public const float DEFAULT_MAX_DISTANCE = 500f;

	// Token: 0x04006ECE RID: 28366
	public const float MIN_SPREAD = 0f;

	// Token: 0x04006ECF RID: 28367
	public const float MAX_SPREAD = 360f;

	// Token: 0x04006ED0 RID: 28368
	public const float DEFAULT_SPREAD = 0f;

	// Token: 0x04006ED1 RID: 28369
	public bool m_bypassEffects;

	// Token: 0x04006ED2 RID: 28370
	public bool m_loop;

	// Token: 0x04006ED3 RID: 28371
	public int m_priority;

	// Token: 0x04006ED4 RID: 28372
	public float m_volume;

	// Token: 0x04006ED5 RID: 28373
	public float m_pitch;

	// Token: 0x04006ED6 RID: 28374
	public float m_stereoPan;

	// Token: 0x04006ED7 RID: 28375
	public float m_spatialBlend;

	// Token: 0x04006ED8 RID: 28376
	public float m_reverbZoneMix;

	// Token: 0x04006ED9 RID: 28377
	public AudioRolloffMode m_rolloffMode;

	// Token: 0x04006EDA RID: 28378
	public float m_dopplerLevel;

	// Token: 0x04006EDB RID: 28379
	public float m_minDistance;

	// Token: 0x04006EDC RID: 28380
	public float m_maxDistance;

	// Token: 0x04006EDD RID: 28381
	public float m_spread;
}
