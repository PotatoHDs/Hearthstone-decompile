using System.Collections;
using UnityEngine;

public class DragCardSoundEffects : MonoBehaviour
{
	private const string CARD_MOTION_LOOP_AIR_SOUND = "card_motion_loop_air.prefab:d0cf08f88e847ba40ba92d3e4cc45ce5";

	private const string CARD_MOTION_LOOP_MAGICAL_SOUND = "card_motion_loop_magical.prefab:b954a7ca45bf25a49970664b7dd90c55";

	private const float MAGICAL_SOUND_VOLUME = 0.15f;

	private const float MAGICAL_SOUND_FADE_IN_TIME = 0.5f;

	private const float AIR_SOUND_MAX_VOLUME = 0.5f;

	private const float AIR_SOUND_MOVEMENT_THRESHOLD = 0.92f;

	private const float AIR_SOUND_VOLUME_SPEED = 0.4f;

	private const float AIR_SOUND_VOLUME_VELOCITY_SCALE = 0.5f;

	private const float DISABLE_VOLUME_FADE_OUT_TIME = 0.2f;

	private bool m_Disabled;

	private bool m_FadingOut;

	private Vector3 m_PreviousPosition;

	private AudioSource m_AirSoundLoop;

	private bool m_AirSoundLoading;

	private AudioSource m_MagicalSoundLoop;

	private bool m_MagicalSoundLoading;

	private float m_MagicalVolume;

	private float m_MagicalVelocity;

	private float m_AirVolume;

	private float m_AirVelocity;

	private Actor m_Actor;

	private Card m_Card;

	private void Awake()
	{
		m_PreviousPosition = base.transform.position;
	}

	private void Update()
	{
		if (m_Disabled || !base.enabled || m_AirSoundLoading || m_MagicalSoundLoading)
		{
			return;
		}
		if (m_AirSoundLoop == null)
		{
			LoadAirSound();
		}
		else if (m_MagicalSoundLoop == null)
		{
			LoadMagicalSound();
		}
		else
		{
			if (m_AirSoundLoop == null || m_MagicalSoundLoop == null)
			{
				return;
			}
			if (m_Card == null)
			{
				m_Card = GetComponent<Card>();
			}
			if (m_Card == null)
			{
				Disable();
				return;
			}
			if (m_Actor == null)
			{
				m_Actor = m_Card.GetActor();
			}
			if (m_Actor == null)
			{
				Disable();
				return;
			}
			if (!m_Actor.IsShown())
			{
				Disable();
				return;
			}
			m_MagicalSoundLoop.transform.position = base.transform.position;
			m_AirSoundLoop.transform.position = base.transform.position;
			if (m_MagicalVolume < 0.15f)
			{
				m_MagicalVolume = Mathf.SmoothDamp(m_MagicalVolume, 0.15f, ref m_MagicalVelocity, 0.5f);
				SoundManager.Get().SetVolume(m_MagicalSoundLoop, m_MagicalVolume);
			}
			else if (m_MagicalVolume > 0.15f)
			{
				m_MagicalVolume = 0.15f;
				SoundManager.Get().SetVolume(m_MagicalSoundLoop, m_MagicalVolume);
			}
			Vector3 position = base.transform.position;
			m_AirVolume = Mathf.SmoothDamp(target: Mathf.Log((position - m_PreviousPosition).magnitude * 0.5f + 0.92f), current: m_AirVolume, currentVelocity: ref m_AirVelocity, smoothTime: 0.0400000028f, maxSpeed: 1f);
			SoundManager.Get().SetVolume(m_AirSoundLoop, Mathf.Clamp(m_AirVolume, 0f, 0.5f));
			SoundManager.Get().SetVolume(m_MagicalSoundLoop, m_MagicalVolume);
			m_PreviousPosition = position;
		}
	}

	public void Restart()
	{
		base.enabled = true;
		m_Disabled = false;
	}

	public void Disable()
	{
		m_Disabled = true;
		if (base.enabled && !m_FadingOut)
		{
			StartCoroutine("FadeOutSound");
		}
	}

	private void OnDisable()
	{
		StopSound();
	}

	private void OnDestroy()
	{
		StopCoroutine("FadeOutSound");
		StopSound();
	}

	private void StopSound()
	{
		m_FadingOut = false;
		m_MagicalVolume = 0f;
		m_AirVolume = 0f;
		m_AirVelocity = 0f;
		if (m_AirSoundLoop != null)
		{
			SoundManager.Get()?.Stop(m_AirSoundLoop);
		}
		if (m_MagicalSoundLoop != null)
		{
			SoundManager.Get()?.Stop(m_MagicalSoundLoop);
		}
	}

	private IEnumerator FadeOutSound()
	{
		if (m_AirSoundLoop == null || m_MagicalSoundLoop == null)
		{
			StopSound();
			yield break;
		}
		m_FadingOut = true;
		while (m_AirVolume > 0f && m_MagicalVolume > 0f)
		{
			m_AirVolume = Mathf.SmoothDamp(m_AirVolume, 0f, ref m_AirVelocity, 0.2f);
			m_MagicalVolume = Mathf.SmoothDamp(m_MagicalVolume, 0f, ref m_MagicalVelocity, 0.2f);
			SoundManager.Get().SetVolume(m_AirSoundLoop, Mathf.Clamp(m_AirVolume, 0f, 1f));
			SoundManager.Get().SetVolume(m_MagicalSoundLoop, m_MagicalVolume);
			yield return null;
		}
		m_FadingOut = false;
		StopSound();
	}

	private void LoadAirSound()
	{
		SoundManager.LoadedCallback callback = delegate(AudioSource source, object userData)
		{
			if (!(source == null))
			{
				m_AirSoundLoading = false;
				m_AirSoundLoop = source;
				if (m_Disabled || !base.enabled)
				{
					SoundManager.Get().Stop(m_AirSoundLoop);
				}
			}
		};
		SoundManager.Get().LoadAndPlay("card_motion_loop_air.prefab:d0cf08f88e847ba40ba92d3e4cc45ce5", base.gameObject, 0f, callback);
	}

	private void LoadMagicalSound()
	{
		SoundManager.LoadedCallback callback = delegate(AudioSource source, object userData)
		{
			if (!(source == null))
			{
				m_MagicalSoundLoading = false;
				if (m_MagicalSoundLoop != null)
				{
					SoundManager.Get().Stop(m_MagicalSoundLoop);
				}
				m_MagicalSoundLoop = source;
				if (m_Disabled || !base.enabled)
				{
					SoundManager.Get().Stop(m_MagicalSoundLoop);
				}
			}
		};
		SoundManager.Get().LoadAndPlay("card_motion_loop_magical.prefab:b954a7ca45bf25a49970664b7dd90c55", base.gameObject, 0f, callback);
	}
}
