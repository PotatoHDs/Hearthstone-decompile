using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000A24 RID: 2596
public class DragCardSoundEffects : MonoBehaviour
{
	// Token: 0x06008BCC RID: 35788 RVA: 0x002CC07A File Offset: 0x002CA27A
	private void Awake()
	{
		this.m_PreviousPosition = base.transform.position;
	}

	// Token: 0x06008BCD RID: 35789 RVA: 0x002CC090 File Offset: 0x002CA290
	private void Update()
	{
		if (this.m_Disabled || !base.enabled)
		{
			return;
		}
		if (this.m_AirSoundLoading || this.m_MagicalSoundLoading)
		{
			return;
		}
		if (this.m_AirSoundLoop == null)
		{
			this.LoadAirSound();
			return;
		}
		if (this.m_MagicalSoundLoop == null)
		{
			this.LoadMagicalSound();
			return;
		}
		if (this.m_AirSoundLoop == null || this.m_MagicalSoundLoop == null)
		{
			return;
		}
		if (this.m_Card == null)
		{
			this.m_Card = base.GetComponent<Card>();
		}
		if (this.m_Card == null)
		{
			this.Disable();
			return;
		}
		if (this.m_Actor == null)
		{
			this.m_Actor = this.m_Card.GetActor();
		}
		if (this.m_Actor == null)
		{
			this.Disable();
			return;
		}
		if (!this.m_Actor.IsShown())
		{
			this.Disable();
			return;
		}
		this.m_MagicalSoundLoop.transform.position = base.transform.position;
		this.m_AirSoundLoop.transform.position = base.transform.position;
		if (this.m_MagicalVolume < 0.15f)
		{
			this.m_MagicalVolume = Mathf.SmoothDamp(this.m_MagicalVolume, 0.15f, ref this.m_MagicalVelocity, 0.5f);
			SoundManager.Get().SetVolume(this.m_MagicalSoundLoop, this.m_MagicalVolume);
		}
		else if (this.m_MagicalVolume > 0.15f)
		{
			this.m_MagicalVolume = 0.15f;
			SoundManager.Get().SetVolume(this.m_MagicalSoundLoop, this.m_MagicalVolume);
		}
		Vector3 position = base.transform.position;
		Vector3 vector = position - this.m_PreviousPosition;
		this.m_AirVolume = Mathf.SmoothDamp(this.m_AirVolume, Mathf.Log(vector.magnitude * 0.5f + 0.92f), ref this.m_AirVelocity, 0.040000003f, 1f);
		SoundManager.Get().SetVolume(this.m_AirSoundLoop, Mathf.Clamp(this.m_AirVolume, 0f, 0.5f));
		SoundManager.Get().SetVolume(this.m_MagicalSoundLoop, this.m_MagicalVolume);
		this.m_PreviousPosition = position;
	}

	// Token: 0x06008BCE RID: 35790 RVA: 0x002CC2BB File Offset: 0x002CA4BB
	public void Restart()
	{
		base.enabled = true;
		this.m_Disabled = false;
	}

	// Token: 0x06008BCF RID: 35791 RVA: 0x002CC2CB File Offset: 0x002CA4CB
	public void Disable()
	{
		this.m_Disabled = true;
		if (base.enabled && !this.m_FadingOut)
		{
			base.StartCoroutine("FadeOutSound");
		}
	}

	// Token: 0x06008BD0 RID: 35792 RVA: 0x002CC2F0 File Offset: 0x002CA4F0
	private void OnDisable()
	{
		this.StopSound();
	}

	// Token: 0x06008BD1 RID: 35793 RVA: 0x002CC2F8 File Offset: 0x002CA4F8
	private void OnDestroy()
	{
		base.StopCoroutine("FadeOutSound");
		this.StopSound();
	}

	// Token: 0x06008BD2 RID: 35794 RVA: 0x002CC30C File Offset: 0x002CA50C
	private void StopSound()
	{
		this.m_FadingOut = false;
		this.m_MagicalVolume = 0f;
		this.m_AirVolume = 0f;
		this.m_AirVelocity = 0f;
		if (this.m_AirSoundLoop != null)
		{
			SoundManager soundManager = SoundManager.Get();
			if (soundManager != null)
			{
				soundManager.Stop(this.m_AirSoundLoop);
			}
		}
		if (this.m_MagicalSoundLoop != null)
		{
			SoundManager soundManager2 = SoundManager.Get();
			if (soundManager2 == null)
			{
				return;
			}
			soundManager2.Stop(this.m_MagicalSoundLoop);
		}
	}

	// Token: 0x06008BD3 RID: 35795 RVA: 0x002CC38A File Offset: 0x002CA58A
	private IEnumerator FadeOutSound()
	{
		if (this.m_AirSoundLoop == null || this.m_MagicalSoundLoop == null)
		{
			this.StopSound();
			yield break;
		}
		this.m_FadingOut = true;
		while (this.m_AirVolume > 0f && this.m_MagicalVolume > 0f)
		{
			this.m_AirVolume = Mathf.SmoothDamp(this.m_AirVolume, 0f, ref this.m_AirVelocity, 0.2f);
			this.m_MagicalVolume = Mathf.SmoothDamp(this.m_MagicalVolume, 0f, ref this.m_MagicalVelocity, 0.2f);
			SoundManager.Get().SetVolume(this.m_AirSoundLoop, Mathf.Clamp(this.m_AirVolume, 0f, 1f));
			SoundManager.Get().SetVolume(this.m_MagicalSoundLoop, this.m_MagicalVolume);
			yield return null;
		}
		this.m_FadingOut = false;
		this.StopSound();
		yield break;
	}

	// Token: 0x06008BD4 RID: 35796 RVA: 0x002CC39C File Offset: 0x002CA59C
	private void LoadAirSound()
	{
		SoundManager.LoadedCallback callback = delegate(AudioSource source, object userData)
		{
			if (source == null)
			{
				return;
			}
			this.m_AirSoundLoading = false;
			this.m_AirSoundLoop = source;
			if (this.m_Disabled || !base.enabled)
			{
				SoundManager.Get().Stop(this.m_AirSoundLoop);
			}
		};
		SoundManager.Get().LoadAndPlay("card_motion_loop_air.prefab:d0cf08f88e847ba40ba92d3e4cc45ce5", base.gameObject, 0f, callback);
	}

	// Token: 0x06008BD5 RID: 35797 RVA: 0x002CC3D8 File Offset: 0x002CA5D8
	private void LoadMagicalSound()
	{
		SoundManager.LoadedCallback callback = delegate(AudioSource source, object userData)
		{
			if (source == null)
			{
				return;
			}
			this.m_MagicalSoundLoading = false;
			if (this.m_MagicalSoundLoop != null)
			{
				SoundManager.Get().Stop(this.m_MagicalSoundLoop);
			}
			this.m_MagicalSoundLoop = source;
			if (this.m_Disabled || !base.enabled)
			{
				SoundManager.Get().Stop(this.m_MagicalSoundLoop);
			}
		};
		SoundManager.Get().LoadAndPlay("card_motion_loop_magical.prefab:b954a7ca45bf25a49970664b7dd90c55", base.gameObject, 0f, callback);
	}

	// Token: 0x040074A5 RID: 29861
	private const string CARD_MOTION_LOOP_AIR_SOUND = "card_motion_loop_air.prefab:d0cf08f88e847ba40ba92d3e4cc45ce5";

	// Token: 0x040074A6 RID: 29862
	private const string CARD_MOTION_LOOP_MAGICAL_SOUND = "card_motion_loop_magical.prefab:b954a7ca45bf25a49970664b7dd90c55";

	// Token: 0x040074A7 RID: 29863
	private const float MAGICAL_SOUND_VOLUME = 0.15f;

	// Token: 0x040074A8 RID: 29864
	private const float MAGICAL_SOUND_FADE_IN_TIME = 0.5f;

	// Token: 0x040074A9 RID: 29865
	private const float AIR_SOUND_MAX_VOLUME = 0.5f;

	// Token: 0x040074AA RID: 29866
	private const float AIR_SOUND_MOVEMENT_THRESHOLD = 0.92f;

	// Token: 0x040074AB RID: 29867
	private const float AIR_SOUND_VOLUME_SPEED = 0.4f;

	// Token: 0x040074AC RID: 29868
	private const float AIR_SOUND_VOLUME_VELOCITY_SCALE = 0.5f;

	// Token: 0x040074AD RID: 29869
	private const float DISABLE_VOLUME_FADE_OUT_TIME = 0.2f;

	// Token: 0x040074AE RID: 29870
	private bool m_Disabled;

	// Token: 0x040074AF RID: 29871
	private bool m_FadingOut;

	// Token: 0x040074B0 RID: 29872
	private Vector3 m_PreviousPosition;

	// Token: 0x040074B1 RID: 29873
	private AudioSource m_AirSoundLoop;

	// Token: 0x040074B2 RID: 29874
	private bool m_AirSoundLoading;

	// Token: 0x040074B3 RID: 29875
	private AudioSource m_MagicalSoundLoop;

	// Token: 0x040074B4 RID: 29876
	private bool m_MagicalSoundLoading;

	// Token: 0x040074B5 RID: 29877
	private float m_MagicalVolume;

	// Token: 0x040074B6 RID: 29878
	private float m_MagicalVelocity;

	// Token: 0x040074B7 RID: 29879
	private float m_AirVolume;

	// Token: 0x040074B8 RID: 29880
	private float m_AirVelocity;

	// Token: 0x040074B9 RID: 29881
	private Actor m_Actor;

	// Token: 0x040074BA RID: 29882
	private Card m_Card;
}
