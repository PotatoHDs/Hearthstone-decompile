using UnityEngine;

public class AudioSourceSettings
{
	public const bool DEFAULT_BYPASS_EFFECTS = false;

	public const bool DEFAULT_LOOP = false;

	public const float MIN_VOLUME = 0f;

	public const float MAX_VOLUME = 1f;

	public const float DEFAULT_VOLUME = 1f;

	public const int MIN_PRIORITY = 0;

	public const int MAX_PRIORITY = 256;

	public const int DEFAULT_PRIORITY = 128;

	public const float MIN_PITCH = -3f;

	public const float MAX_PITCH = 3f;

	public const float DEFAULT_PITCH = 1f;

	public const float MIN_STEREO_PAN = -1f;

	public const float MAX_STEREO_PAN = 1f;

	public const float DEFAULT_STEREO_PAN = 0f;

	public const float MIN_SPATIAL_BLEND = 0f;

	public const float MAX_SPATIAL_BLEND = 1f;

	public const float DEFAULT_SPATIAL_BLEND = 1f;

	public const float MIN_REVERB_ZONE_MIX = 0f;

	public const float MAX_REVERB_ZONE_MIX = 1.1f;

	public const float DEFAULT_REVERB_ZONE_MIX = 1f;

	public const AudioRolloffMode DEFAULT_ROLLOFF_MODE = AudioRolloffMode.Linear;

	public const float MIN_DOPPLER_LEVEL = 0f;

	public const float MAX_DOPPLER_LEVEL = 5f;

	public const float DEFAULT_DOPPLER_LEVEL = 1f;

	public const float DEFAULT_MIN_DISTANCE = 100f;

	public const float DEFAULT_MAX_DISTANCE = 500f;

	public const float MIN_SPREAD = 0f;

	public const float MAX_SPREAD = 360f;

	public const float DEFAULT_SPREAD = 0f;

	public bool m_bypassEffects;

	public bool m_loop;

	public int m_priority;

	public float m_volume;

	public float m_pitch;

	public float m_stereoPan;

	public float m_spatialBlend;

	public float m_reverbZoneMix;

	public AudioRolloffMode m_rolloffMode;

	public float m_dopplerLevel;

	public float m_minDistance;

	public float m_maxDistance;

	public float m_spread;

	public AudioSourceSettings()
	{
		LoadDefaults();
	}

	public void LoadDefaults()
	{
		m_bypassEffects = false;
		m_loop = false;
		m_priority = 128;
		m_volume = 1f;
		m_pitch = 1f;
		m_stereoPan = 0f;
		m_spatialBlend = 1f;
		m_reverbZoneMix = 1f;
		m_rolloffMode = AudioRolloffMode.Linear;
		m_dopplerLevel = 1f;
		m_minDistance = 100f;
		m_maxDistance = 500f;
		m_spread = 0f;
	}
}
