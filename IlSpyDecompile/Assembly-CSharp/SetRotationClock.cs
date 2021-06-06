using System;
using System.Collections;
using UnityEngine;

public class SetRotationClock : MonoBehaviour
{
	public delegate void DisableTheClockCallback();

	public Texture2D m_PreviousIcon;

	public Texture2D m_PreviousIconBlur;

	public Texture2D m_NewIcon;

	public Texture2D m_NewIconBlur;

	public Renderer m_GlassPanel;

	public float m_AnimationWaitTime = 5.5f;

	public GameObject m_CenterPanel;

	public float m_CenterPanelFlipTime = 1f;

	public GameObject m_SetRotationButton;

	public GameObject m_SetRotationButtonMesh;

	public GameObject m_SetRotationIconWidget;

	public float m_SetRotationButtonDelay = 0.75f;

	public float m_SetRotationButtonWobbleTime = 0.5f;

	public float m_ButtonRotationHoldTime = 1.5f;

	public GameObject m_ButtonRiseBone;

	public GameObject m_ButtonBanner;

	public UberText m_ButtonBannerStandard;

	public UberText m_ButtonBannerClassic;

	public Color m_ButtonBannerTextColor = Color.white;

	public float m_ButtonRiseTime = 1.75f;

	public float m_BlurScreenDelay = 0.5f;

	public float m_BlurScreenTime = 1f;

	public float m_MoveButtonUpZ = -0.1f;

	public float m_MoveButtonUpZphone = -0.3f;

	public float m_MoveButtonUpTime = 1f;

	public float m_ButtonFlipTime = 0.5f;

	public float m_ButtonToTrayAnimTime = 0.5f;

	public float m_EndBlurScreenDelay = 0.5f;

	public float m_EndBlurScreenTime = 1f;

	public float m_MoveButtonToTrayDelay = 1.5f;

	public float m_TextDelayTime = 1f;

	public float m_VeteranGhostedIconDelayTime = 3f;

	public ClockOverlayText m_overlayText;

	public GameObject m_ButtonGlowPlaneYellow;

	public GameObject m_ButtonGlowPlaneGreen;

	public ParticleSystem m_ImpactParticles;

	public AnimationCurve m_ButtonGlowAnimation;

	public PegUIElement m_clickCatcher;

	public AudioSource m_TheClockAmbientSound;

	public float m_TheClockAmbientSoundVolume = 1f;

	public float m_TheClockAmbientSoundFadeInTime = 2f;

	public float m_TheClockAmbientSoundFadeOutTime = 1f;

	public AudioSource m_ClickSound;

	public AudioSource m_Stage1Sound;

	public AudioSource m_Stage1Sound_Veteran;

	public AudioSource m_Stage2Sound;

	public AudioSource m_Stage2Sound_Veteran;

	public AudioSource m_Stage3Sound;

	public AudioSource m_Stage4Sound;

	public AudioSource m_Stage5Sound;

	public AudioSource m_Stage5Sound_Veteran;

	private bool m_clickCaptured;

	private Vector3 m_buttonBannerScale;

	private AudioSource m_ambientSound;

	private const float BUTTON_MESH_Z_ROTATION_FOR_CLASSIC = 0f;

	private const float BUTTON_MESH_Z_ROTATION_FOR_STANDARD = 180f;

	private static SetRotationClock s_instance;

	private void Awake()
	{
		s_instance = this;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			base.transform.position = new Vector3(-60.7f, -18.939f, -43f);
			base.transform.localScale = new Vector3(9.043651f, 9.043651f, 9.043651f);
		}
		else
		{
			base.transform.position = new Vector3(-47.234f, -18.939f, -31.837f);
			base.transform.localScale = new Vector3(6.970411f, 6.970411f, 6.970411f);
		}
		m_overlayText.HideImmediate();
		m_clickCatcher.gameObject.SetActive(value: false);
		m_clickCatcher.AddEventListener(UIEventType.RELEASE, OnClick);
		m_buttonBannerScale = m_ButtonBanner.transform.localScale;
		m_ButtonBannerStandard.TextColor = m_ButtonBannerTextColor;
		m_ButtonBannerClassic.TextColor = m_ButtonBannerTextColor;
		m_ButtonBanner.SetActive(value: false);
		m_ButtonBannerStandard.gameObject.SetActive(value: false);
		m_ButtonBannerClassic.gameObject.SetActive(value: false);
		Material sharedMaterial = m_GlassPanel.GetSharedMaterial();
		sharedMaterial.SetTexture("_BlendImage1", m_PreviousIcon);
		sharedMaterial.SetTexture("_BlendImage2", m_PreviousIconBlur);
		sharedMaterial.SetFloat("_BlendTransparency", 1f);
		sharedMaterial.SetFloat("_DistortionAmountX", 0f);
		sharedMaterial.SetFloat("_DistortionAmountY", 0f);
		sharedMaterial.SetFloat("_BlendImageSizeX", 6.5f);
		sharedMaterial.SetFloat("_BlendImageSizeY", 6.5f);
	}

	public static SetRotationClock Get()
	{
		return s_instance;
	}

	public void StartTheClock()
	{
		m_SetRotationButton.SetActive(value: true);
		StartCoroutine(ClockAnimation());
	}

	public void ShakeCamera()
	{
		CameraShakeMgr.Shake(Camera.main, new Vector3(0.1f, 0.1f, 0.1f), 0.4f);
	}

	public void SwapSetIcons()
	{
		Material sharedMaterial = m_GlassPanel.GetSharedMaterial();
		sharedMaterial.SetTexture("_BlendImage1", m_NewIcon);
		sharedMaterial.SetTexture("_BlendImage2", m_NewIconBlur);
	}

	public IEnumerator ClockAnimation()
	{
		bool veteranFlow = SetRotationManager.HasSeenStandardModeTutorial();
		AudioSource clickSound = null;
		if (m_ClickSound != null)
		{
			clickSound = UnityEngine.Object.Instantiate(m_ClickSound);
		}
		while (!DeckPickerTrayDisplay.Get().IsLoaded())
		{
			yield return null;
		}
		DeckPickerTrayDisplay.Get().InitSetRotationTutorial(veteranFlow);
		if (!veteranFlow)
		{
			PlayClockAnimation();
			if (m_Stage1Sound != null)
			{
				SoundManager.Get().Play(UnityEngine.Object.Instantiate(m_Stage1Sound));
			}
			if (m_TheClockAmbientSound != null)
			{
				FadeInAmbientSound();
			}
			yield return new WaitForSeconds(m_AnimationWaitTime);
			VignetteBackground(0.5f);
			m_clickCatcher.gameObject.SetActive(value: true);
			m_clickCaptured = false;
			m_overlayText.UpdateText(0);
			m_overlayText.Show();
			yield return new WaitForSeconds(m_TextDelayTime);
			while (!m_clickCaptured)
			{
				yield return null;
			}
			if (m_Stage2Sound != null)
			{
				SoundManager.Get().Play(UnityEngine.Object.Instantiate(m_Stage2Sound));
			}
			if (clickSound != null)
			{
				SoundManager.Get().Play(clickSound);
			}
			StopVignetteBackground(0.5f);
			m_clickCatcher.gameObject.SetActive(value: false);
			m_overlayText.Hide();
			yield return new WaitForSeconds(m_TextDelayTime);
		}
		else
		{
			if (m_TheClockAmbientSound != null)
			{
				FadeInAmbientSound();
			}
			yield return new WaitForSeconds(m_VeteranGhostedIconDelayTime);
			if (m_Stage2Sound_Veteran != null)
			{
				SoundManager.Get().Play(UnityEngine.Object.Instantiate(m_Stage2Sound_Veteran));
			}
		}
		FlipCenterPanelButton();
		yield return new WaitForSeconds(m_ButtonRotationHoldTime);
		RaiseButton();
		yield return new WaitForSeconds(m_BlurScreenDelay);
		BlurBackground(m_BlurScreenTime);
		yield return new WaitForSeconds(m_BlurScreenTime);
		m_clickCatcher.gameObject.SetActive(value: true);
		m_clickCaptured = false;
		m_overlayText.UpdateText(1);
		m_overlayText.Show();
		yield return new WaitForSeconds(m_TextDelayTime);
		while (!m_clickCaptured)
		{
			yield return null;
		}
		if (clickSound != null)
		{
			SoundManager.Get().Play(clickSound);
		}
		m_clickCatcher.gameObject.SetActive(value: false);
		m_overlayText.Hide();
		if (m_Stage3Sound != null)
		{
			SoundManager.Get().Play(UnityEngine.Object.Instantiate(m_Stage3Sound));
		}
		MoveButtonUp();
		yield return new WaitForSeconds(m_TextDelayTime);
		m_clickCatcher.gameObject.SetActive(value: true);
		m_clickCaptured = false;
		ShowButtonBanner();
		ShowButtonYellowGlow();
		TournamentDisplay.Get().SetRotationSlideIn();
		FadeOutAmbientSound();
		while (!m_clickCaptured)
		{
			yield return null;
		}
		if (clickSound != null)
		{
			SoundManager.Get().Play(clickSound);
		}
		m_clickCatcher.gameObject.SetActive(value: false);
		if (m_Stage4Sound != null)
		{
			SoundManager.Get().Play(UnityEngine.Object.Instantiate(m_Stage4Sound));
		}
		while (TournamentDisplay.Get().SlidingInForSetRotation)
		{
			yield return null;
		}
		if (clickSound != null)
		{
			SoundManager.Get().Play(clickSound);
		}
		m_clickCatcher.gameObject.SetActive(value: false);
		HideButtonBanner();
		StopBlurBackground(m_EndBlurScreenTime);
		StopButtonDrift();
		EndClockStartTutorial();
	}

	private void FadeInAmbientSound()
	{
		if (!(m_TheClockAmbientSound == null))
		{
			m_ambientSound = UnityEngine.Object.Instantiate(m_TheClockAmbientSound);
			SoundManager.Get().SetVolume(m_ambientSound, 0.01f);
			Action<object> action = delegate(object amount)
			{
				SoundManager.Get().SetVolume(m_ambientSound, (float)amount);
			};
			iTween.ValueTo(base.gameObject, iTween.Hash("name", "TheClockAmbientSound", "from", 0.01f, "to", m_TheClockAmbientSoundVolume, "time", m_TheClockAmbientSoundFadeInTime, "easetype", iTween.EaseType.linear, "onupdate", action, "onupdatetarget", base.gameObject));
			SoundManager.Get().Play(m_ambientSound);
		}
	}

	private void FadeOutAmbientSound()
	{
		if (!(m_ambientSound == null))
		{
			Action<object> action = delegate(object amount)
			{
				SoundManager.Get().SetVolume(m_ambientSound, (float)amount);
			};
			iTween.ValueTo(base.gameObject, iTween.Hash("name", "TheClockAmbientSound", "from", m_TheClockAmbientSoundVolume, "to", 0f, "time", m_TheClockAmbientSoundFadeOutTime, "easetype", iTween.EaseType.linear, "onupdate", action, "onupdatetarget", base.gameObject, "oncompletetarget", base.gameObject, "oncomplete", "StopAmbientSound"));
		}
	}

	private void StopAmbientSound()
	{
		if (!(m_ambientSound == null))
		{
			SoundManager.Get().Stop(m_ambientSound);
		}
	}

	private void PlayClockAnimation()
	{
		Animator component = GetComponent<Animator>();
		if (!(component == null))
		{
			component.SetTrigger("StartClock");
		}
	}

	private void AnimateButtonToTournamentTray()
	{
		TournamentDisplay.Get().SetRotationSlideIn();
	}

	private void FlipCenterPanelButton()
	{
		iTween.RotateTo(m_CenterPanel, iTween.Hash("z", 180f, "time", m_CenterPanelFlipTime, "islocal", true, "easetype", iTween.EaseType.easeOutBounce));
		m_SetRotationButton.transform.localEulerAngles = new Vector3(0f, 0f, -10f);
		iTween.RotateTo(m_SetRotationButton, iTween.Hash("z", 0f, "delay", m_SetRotationButtonDelay, "time", m_SetRotationButtonWobbleTime, "islocal", true, "easetype", iTween.EaseType.easeOutBounce));
	}

	private void RaiseButton()
	{
		GetComponent<Animator>().SetTrigger("RaiseButton");
		SceneUtils.SetLayer(m_SetRotationButton, GameLayer.IgnoreFullScreenEffects);
		iTween.MoveTo(m_SetRotationButton, iTween.Hash("position", m_ButtonRiseBone.transform.position, "delay", 0f, "time", m_ButtonRiseTime, "islocal", false, "easetype", iTween.EaseType.easeInOutQuint, "oncompletetarget", base.gameObject, "oncomplete", "RaiseButtonComplete"));
	}

	private void RaiseButtonComplete()
	{
		TokyoDrift componentInChildren = m_SetRotationButton.GetComponentInChildren<TokyoDrift>();
		if (!(componentInChildren == null))
		{
			componentInChildren.enabled = true;
		}
	}

	private void StopButtonDrift()
	{
		m_ButtonBanner.SetActive(value: false);
		m_ButtonBannerStandard.gameObject.SetActive(value: false);
		m_ButtonBannerClassic.gameObject.SetActive(value: false);
		TokyoDrift componentInChildren = m_SetRotationButton.GetComponentInChildren<TokyoDrift>();
		if (!(componentInChildren == null))
		{
			componentInChildren.enabled = false;
		}
	}

	private void ShowButtonBanner()
	{
		m_ButtonBanner.SetActive(value: true);
		m_ButtonBannerStandard.gameObject.SetActive(value: true);
		m_ButtonBanner.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		iTween.ScaleTo(m_ButtonBanner, iTween.Hash("scale", m_buttonBannerScale, "time", 0.15f, "easetype", iTween.EaseType.easeOutQuad));
	}

	private void ShowButtonYellowGlow()
	{
		Hashtable args = iTween.Hash("islocal", true, "from", 0f, "to", 1f, "time", 0.3f, "easeType", iTween.EaseType.easeOutExpo, "onupdate", (Action<object>)delegate(object value)
		{
			m_ButtonGlowPlaneYellow.GetComponent<Renderer>().GetMaterial().SetFloat("_Intensity", (float)value);
		}, "onupdatetarget", base.gameObject);
		iTween.ValueTo(base.gameObject, args);
	}

	private void CrossFadeToGreenGlow()
	{
		Hashtable args = iTween.Hash("islocal", true, "from", 1f, "to", 0f, "time", 0.3f, "easeType", iTween.EaseType.easeOutExpo, "onupdate", (Action<object>)delegate(object value)
		{
			m_ButtonGlowPlaneYellow.GetComponent<Renderer>().GetMaterial().SetFloat("_Intensity", (float)value);
		}, "onupdatetarget", m_ButtonGlowPlaneYellow);
		iTween.ValueTo(base.gameObject, args);
		Hashtable args2 = iTween.Hash("islocal", true, "from", 0f, "to", 1f, "time", 0.3f, "easeType", iTween.EaseType.easeOutExpo, "onupdate", (Action<object>)delegate(object value)
		{
			m_ButtonGlowPlaneGreen.GetComponent<Renderer>().GetMaterial().SetFloat("_Intensity", (float)value);
		}, "onupdatetarget", base.gameObject);
		iTween.ValueTo(m_ButtonGlowPlaneGreen, args2);
	}

	private void ButtonBannerCrossFadeText()
	{
		m_ButtonBannerStandard.gameObject.SetActive(value: true);
		m_ButtonBannerClassic.gameObject.SetActive(value: true);
		Color textColor = m_ButtonBannerClassic.TextColor;
		textColor.a = 0f;
		m_ButtonBannerClassic.TextColor = textColor;
		iTween.FadeTo(m_ButtonBannerStandard.gameObject, 0f, m_ButtonFlipTime * 0.1f);
		iTween.FadeTo(m_ButtonBannerClassic.gameObject, 1f, m_ButtonFlipTime * 0.1f);
	}

	private void ButtonBannerPunch()
	{
		Vector3 localScale = m_ButtonBanner.transform.localScale;
		iTween.ScaleTo(m_ButtonBanner, iTween.Hash("scale", localScale * 1.5f, "time", 0.075f, "delay", m_ButtonFlipTime * 0.25f, "easetype", iTween.EaseType.easeOutQuad, "onupdatetarget", base.gameObject));
		iTween.ScaleTo(m_ButtonBanner, iTween.Hash("scale", localScale, "time", 0.25f, "delay", m_ButtonFlipTime * 0.25f + 0.075f, "easetype", iTween.EaseType.easeInOutQuad, "onupdatetarget", base.gameObject));
	}

	private void HideButtonBanner()
	{
		iTween.ScaleTo(m_ButtonBanner, iTween.Hash("scale", Vector3.zero, "time", 0.25f, "easetype", iTween.EaseType.easeInQuad, "oncompletetarget", base.gameObject, "oncomplete", "HideButtonBannerComplete"));
	}

	private void HideButtonBannerComplete()
	{
		m_ButtonBanner.SetActive(value: false);
	}

	private void FlipButton()
	{
		iTween.RotateTo(m_SetRotationButtonMesh, iTween.Hash("z", 0f, "time", m_ButtonFlipTime, "islocal", true, "easetype", iTween.EaseType.easeOutElastic));
	}

	private void MoveButtonUp()
	{
		float num = m_MoveButtonUpZ;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			num = m_MoveButtonUpZphone;
		}
		iTween.MoveTo(m_SetRotationButton, iTween.Hash("z", num, "delay", 0f, "time", m_MoveButtonUpTime, "islocal", true, "easetype", iTween.EaseType.easeInOutQuint));
	}

	private void VignetteBackground(float time)
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr == null)
		{
			Debug.LogWarning("FullScreenFXMgr is NULL!");
		}
		else
		{
			fullScreenFXMgr.Vignette(0.99f, time, iTween.EaseType.easeOutCubic);
		}
	}

	private void StopVignetteBackground(float time)
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr == null)
		{
			Debug.LogWarning("FullScreenFXMgr is NULL!");
		}
		else
		{
			fullScreenFXMgr.Vignette(0f, time, iTween.EaseType.easeInCubic);
		}
	}

	private void BlurBackground(float time)
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr == null)
		{
			Debug.LogWarning("FullScreenFXMgr is NULL!");
		}
		else
		{
			fullScreenFXMgr.StartStandardBlurVignette(time);
		}
	}

	private void StopBlurBackground(float time)
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr == null)
		{
			Debug.LogWarning("FullScreenFXMgr is NULL!");
		}
		else
		{
			fullScreenFXMgr.EndStandardBlurVignette(time);
		}
	}

	private void MoveButtonToDeckPickerTray(bool socketAsClassic)
	{
		StopButtonDrift();
		Vector3 vector = Vector3.zero;
		Vector3 vector2 = Vector3.one;
		GameObject theClockButtonBone = DeckPickerTrayDisplay.Get().m_TheClockButtonBone;
		if (theClockButtonBone != null)
		{
			vector = theClockButtonBone.transform.position;
			vector2 = theClockButtonBone.transform.localScale;
		}
		Vector3 vector3 = Vector3.Lerp(m_SetRotationButton.transform.position, vector, 0.75f);
		vector3 = new Vector3(vector3.x + 7f, vector3.y, vector3.z);
		Vector3[] array = new Vector3[3]
		{
			m_SetRotationButton.transform.position,
			vector3,
			vector
		};
		GetComponent<Animator>().SetTrigger("SocketButton");
		Hashtable args = iTween.Hash("path", array, "delay", 0f, "time", m_ButtonToTrayAnimTime, "islocal", false, "easetype", iTween.EaseType.easeInOutQuint, "oncompletetarget", base.gameObject, "oncomplete", "ButtonImpactAndShutdownTheClock");
		iTween.MoveTo(m_SetRotationButton, args);
		iTween.RotateTo(m_SetRotationButtonMesh, iTween.Hash("rotation", new Vector3(0f, 0f, socketAsClassic ? 0f : 180f), "time", m_ButtonToTrayAnimTime, "islocal", true, "easetype", iTween.EaseType.easeInOutQuint));
		iTween.RotateTo(m_SetRotationButton, iTween.Hash("rotation", Vector3.zero, "time", m_ButtonToTrayAnimTime, "islocal", true, "easetype", iTween.EaseType.easeInOutQuint));
		iTween.ScaleTo(m_SetRotationButton, iTween.Hash("scale", vector2, "delay", 0f, "time", m_ButtonToTrayAnimTime, "easetype", iTween.EaseType.easeInOutQuint));
	}

	private void ButtonImpactAndShutdownTheClock()
	{
		ShakeCamera();
		m_ImpactParticles.Play();
		StartCoroutine(FinalGlowAndDisableTheClock());
	}

	private IEnumerator FinalGlowAndDisableTheClock()
	{
		EndClockStartTutorial();
		Renderer renderer = (SetRotationManager.HasSeenStandardModeTutorial() ? m_ButtonGlowPlaneYellow.GetComponent<Renderer>() : m_ButtonGlowPlaneGreen.GetComponent<Renderer>());
		Material glowMat = renderer.GetMaterial();
		float animLength = m_ButtonGlowAnimation.keys[m_ButtonGlowAnimation.length - 1].time;
		float animTime = 0f;
		while (animTime < animLength)
		{
			animTime += Time.deltaTime;
			glowMat.SetFloat("_Intensity", m_ButtonGlowAnimation.Evaluate(animTime));
			yield return null;
		}
		yield return new WaitForSeconds(3f);
		base.gameObject.SetActive(value: false);
	}

	private void DisableTheClock()
	{
		base.gameObject.SetActive(value: false);
	}

	private void EndClockStartTutorial()
	{
		DisableTheClockCallback callback = DisableTheClock;
		GetComponent<Animator>().StopPlayback();
		DeckPickerTrayDisplay.Get().StartSetRotationTutorial(callback);
	}

	private void OnClick(UIEvent e)
	{
		m_clickCaptured = true;
	}
}
