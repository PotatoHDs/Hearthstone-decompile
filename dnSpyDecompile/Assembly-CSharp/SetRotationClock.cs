using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200069E RID: 1694
public class SetRotationClock : MonoBehaviour
{
	// Token: 0x06005E81 RID: 24193 RVA: 0x001EB4BC File Offset: 0x001E96BC
	private void Awake()
	{
		SetRotationClock.s_instance = this;
		if (UniversalInputManager.UsePhoneUI)
		{
			base.transform.position = new Vector3(-60.7f, -18.939f, -43f);
			base.transform.localScale = new Vector3(9.043651f, 9.043651f, 9.043651f);
		}
		else
		{
			base.transform.position = new Vector3(-47.234f, -18.939f, -31.837f);
			base.transform.localScale = new Vector3(6.970411f, 6.970411f, 6.970411f);
		}
		this.m_overlayText.HideImmediate();
		this.m_clickCatcher.gameObject.SetActive(false);
		this.m_clickCatcher.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClick));
		this.m_buttonBannerScale = this.m_ButtonBanner.transform.localScale;
		this.m_ButtonBannerStandard.TextColor = this.m_ButtonBannerTextColor;
		this.m_ButtonBannerClassic.TextColor = this.m_ButtonBannerTextColor;
		this.m_ButtonBanner.SetActive(false);
		this.m_ButtonBannerStandard.gameObject.SetActive(false);
		this.m_ButtonBannerClassic.gameObject.SetActive(false);
		Material sharedMaterial = this.m_GlassPanel.GetSharedMaterial();
		sharedMaterial.SetTexture("_BlendImage1", this.m_PreviousIcon);
		sharedMaterial.SetTexture("_BlendImage2", this.m_PreviousIconBlur);
		sharedMaterial.SetFloat("_BlendTransparency", 1f);
		sharedMaterial.SetFloat("_DistortionAmountX", 0f);
		sharedMaterial.SetFloat("_DistortionAmountY", 0f);
		sharedMaterial.SetFloat("_BlendImageSizeX", 6.5f);
		sharedMaterial.SetFloat("_BlendImageSizeY", 6.5f);
	}

	// Token: 0x06005E82 RID: 24194 RVA: 0x001EB670 File Offset: 0x001E9870
	public static SetRotationClock Get()
	{
		return SetRotationClock.s_instance;
	}

	// Token: 0x06005E83 RID: 24195 RVA: 0x001EB677 File Offset: 0x001E9877
	public void StartTheClock()
	{
		this.m_SetRotationButton.SetActive(true);
		base.StartCoroutine(this.ClockAnimation());
	}

	// Token: 0x06005E84 RID: 24196 RVA: 0x001EB692 File Offset: 0x001E9892
	public void ShakeCamera()
	{
		CameraShakeMgr.Shake(Camera.main, new Vector3(0.1f, 0.1f, 0.1f), 0.4f);
	}

	// Token: 0x06005E85 RID: 24197 RVA: 0x001EB6B7 File Offset: 0x001E98B7
	public void SwapSetIcons()
	{
		Material sharedMaterial = this.m_GlassPanel.GetSharedMaterial();
		sharedMaterial.SetTexture("_BlendImage1", this.m_NewIcon);
		sharedMaterial.SetTexture("_BlendImage2", this.m_NewIconBlur);
	}

	// Token: 0x06005E86 RID: 24198 RVA: 0x001EB6E5 File Offset: 0x001E98E5
	public IEnumerator ClockAnimation()
	{
		bool veteranFlow = SetRotationManager.HasSeenStandardModeTutorial();
		AudioSource clickSound = null;
		if (this.m_ClickSound != null)
		{
			clickSound = UnityEngine.Object.Instantiate<AudioSource>(this.m_ClickSound);
		}
		while (!DeckPickerTrayDisplay.Get().IsLoaded())
		{
			yield return null;
		}
		DeckPickerTrayDisplay.Get().InitSetRotationTutorial(veteranFlow);
		if (!veteranFlow)
		{
			this.PlayClockAnimation();
			if (this.m_Stage1Sound != null)
			{
				SoundManager.Get().Play(UnityEngine.Object.Instantiate<AudioSource>(this.m_Stage1Sound), null, null, null);
			}
			if (this.m_TheClockAmbientSound != null)
			{
				this.FadeInAmbientSound();
			}
			yield return new WaitForSeconds(this.m_AnimationWaitTime);
			this.VignetteBackground(0.5f);
			this.m_clickCatcher.gameObject.SetActive(true);
			this.m_clickCaptured = false;
			this.m_overlayText.UpdateText(0);
			this.m_overlayText.Show();
			yield return new WaitForSeconds(this.m_TextDelayTime);
			while (!this.m_clickCaptured)
			{
				yield return null;
			}
			if (this.m_Stage2Sound != null)
			{
				SoundManager.Get().Play(UnityEngine.Object.Instantiate<AudioSource>(this.m_Stage2Sound), null, null, null);
			}
			if (clickSound != null)
			{
				SoundManager.Get().Play(clickSound, null, null, null);
			}
			this.StopVignetteBackground(0.5f);
			this.m_clickCatcher.gameObject.SetActive(false);
			this.m_overlayText.Hide();
			yield return new WaitForSeconds(this.m_TextDelayTime);
		}
		else
		{
			if (this.m_TheClockAmbientSound != null)
			{
				this.FadeInAmbientSound();
			}
			yield return new WaitForSeconds(this.m_VeteranGhostedIconDelayTime);
			if (this.m_Stage2Sound_Veteran != null)
			{
				SoundManager.Get().Play(UnityEngine.Object.Instantiate<AudioSource>(this.m_Stage2Sound_Veteran), null, null, null);
			}
		}
		this.FlipCenterPanelButton();
		yield return new WaitForSeconds(this.m_ButtonRotationHoldTime);
		this.RaiseButton();
		yield return new WaitForSeconds(this.m_BlurScreenDelay);
		this.BlurBackground(this.m_BlurScreenTime);
		yield return new WaitForSeconds(this.m_BlurScreenTime);
		this.m_clickCatcher.gameObject.SetActive(true);
		this.m_clickCaptured = false;
		this.m_overlayText.UpdateText(1);
		this.m_overlayText.Show();
		yield return new WaitForSeconds(this.m_TextDelayTime);
		while (!this.m_clickCaptured)
		{
			yield return null;
		}
		if (clickSound != null)
		{
			SoundManager.Get().Play(clickSound, null, null, null);
		}
		this.m_clickCatcher.gameObject.SetActive(false);
		this.m_overlayText.Hide();
		if (this.m_Stage3Sound != null)
		{
			SoundManager.Get().Play(UnityEngine.Object.Instantiate<AudioSource>(this.m_Stage3Sound), null, null, null);
		}
		this.MoveButtonUp();
		yield return new WaitForSeconds(this.m_TextDelayTime);
		this.m_clickCatcher.gameObject.SetActive(true);
		this.m_clickCaptured = false;
		this.ShowButtonBanner();
		this.ShowButtonYellowGlow();
		TournamentDisplay.Get().SetRotationSlideIn();
		this.FadeOutAmbientSound();
		while (!this.m_clickCaptured)
		{
			yield return null;
		}
		if (clickSound != null)
		{
			SoundManager.Get().Play(clickSound, null, null, null);
		}
		this.m_clickCatcher.gameObject.SetActive(false);
		if (this.m_Stage4Sound != null)
		{
			SoundManager.Get().Play(UnityEngine.Object.Instantiate<AudioSource>(this.m_Stage4Sound), null, null, null);
		}
		while (TournamentDisplay.Get().SlidingInForSetRotation)
		{
			yield return null;
		}
		if (clickSound != null)
		{
			SoundManager.Get().Play(clickSound, null, null, null);
		}
		this.m_clickCatcher.gameObject.SetActive(false);
		this.HideButtonBanner();
		this.StopBlurBackground(this.m_EndBlurScreenTime);
		this.StopButtonDrift();
		this.EndClockStartTutorial();
		yield break;
	}

	// Token: 0x06005E87 RID: 24199 RVA: 0x001EB6F4 File Offset: 0x001E98F4
	private void FadeInAmbientSound()
	{
		if (this.m_TheClockAmbientSound == null)
		{
			return;
		}
		this.m_ambientSound = UnityEngine.Object.Instantiate<AudioSource>(this.m_TheClockAmbientSound);
		SoundManager.Get().SetVolume(this.m_ambientSound, 0.01f);
		Action<object> action = delegate(object amount)
		{
			SoundManager.Get().SetVolume(this.m_ambientSound, (float)amount);
		};
		iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
		{
			"name",
			"TheClockAmbientSound",
			"from",
			0.01f,
			"to",
			this.m_TheClockAmbientSoundVolume,
			"time",
			this.m_TheClockAmbientSoundFadeInTime,
			"easetype",
			iTween.EaseType.linear,
			"onupdate",
			action,
			"onupdatetarget",
			base.gameObject
		}));
		SoundManager.Get().Play(this.m_ambientSound, null, null, null);
	}

	// Token: 0x06005E88 RID: 24200 RVA: 0x001EB7F4 File Offset: 0x001E99F4
	private void FadeOutAmbientSound()
	{
		if (this.m_ambientSound == null)
		{
			return;
		}
		Action<object> action = delegate(object amount)
		{
			SoundManager.Get().SetVolume(this.m_ambientSound, (float)amount);
		};
		iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
		{
			"name",
			"TheClockAmbientSound",
			"from",
			this.m_TheClockAmbientSoundVolume,
			"to",
			0f,
			"time",
			this.m_TheClockAmbientSoundFadeOutTime,
			"easetype",
			iTween.EaseType.linear,
			"onupdate",
			action,
			"onupdatetarget",
			base.gameObject,
			"oncompletetarget",
			base.gameObject,
			"oncomplete",
			"StopAmbientSound"
		}));
	}

	// Token: 0x06005E89 RID: 24201 RVA: 0x001EB8DE File Offset: 0x001E9ADE
	private void StopAmbientSound()
	{
		if (this.m_ambientSound == null)
		{
			return;
		}
		SoundManager.Get().Stop(this.m_ambientSound);
	}

	// Token: 0x06005E8A RID: 24202 RVA: 0x001EB900 File Offset: 0x001E9B00
	private void PlayClockAnimation()
	{
		Animator component = base.GetComponent<Animator>();
		if (component == null)
		{
			return;
		}
		component.SetTrigger("StartClock");
	}

	// Token: 0x06005E8B RID: 24203 RVA: 0x001EB929 File Offset: 0x001E9B29
	private void AnimateButtonToTournamentTray()
	{
		TournamentDisplay.Get().SetRotationSlideIn();
	}

	// Token: 0x06005E8C RID: 24204 RVA: 0x001EB938 File Offset: 0x001E9B38
	private void FlipCenterPanelButton()
	{
		iTween.RotateTo(this.m_CenterPanel, iTween.Hash(new object[]
		{
			"z",
			180f,
			"time",
			this.m_CenterPanelFlipTime,
			"islocal",
			true,
			"easetype",
			iTween.EaseType.easeOutBounce
		}));
		this.m_SetRotationButton.transform.localEulerAngles = new Vector3(0f, 0f, -10f);
		iTween.RotateTo(this.m_SetRotationButton, iTween.Hash(new object[]
		{
			"z",
			0f,
			"delay",
			this.m_SetRotationButtonDelay,
			"time",
			this.m_SetRotationButtonWobbleTime,
			"islocal",
			true,
			"easetype",
			iTween.EaseType.easeOutBounce
		}));
	}

	// Token: 0x06005E8D RID: 24205 RVA: 0x001EBA4C File Offset: 0x001E9C4C
	private void RaiseButton()
	{
		base.GetComponent<Animator>().SetTrigger("RaiseButton");
		SceneUtils.SetLayer(this.m_SetRotationButton, GameLayer.IgnoreFullScreenEffects);
		iTween.MoveTo(this.m_SetRotationButton, iTween.Hash(new object[]
		{
			"position",
			this.m_ButtonRiseBone.transform.position,
			"delay",
			0f,
			"time",
			this.m_ButtonRiseTime,
			"islocal",
			false,
			"easetype",
			iTween.EaseType.easeInOutQuint,
			"oncompletetarget",
			base.gameObject,
			"oncomplete",
			"RaiseButtonComplete"
		}));
	}

	// Token: 0x06005E8E RID: 24206 RVA: 0x001EBB24 File Offset: 0x001E9D24
	private void RaiseButtonComplete()
	{
		TokyoDrift componentInChildren = this.m_SetRotationButton.GetComponentInChildren<TokyoDrift>();
		if (componentInChildren == null)
		{
			return;
		}
		componentInChildren.enabled = true;
	}

	// Token: 0x06005E8F RID: 24207 RVA: 0x001EBB50 File Offset: 0x001E9D50
	private void StopButtonDrift()
	{
		this.m_ButtonBanner.SetActive(false);
		this.m_ButtonBannerStandard.gameObject.SetActive(false);
		this.m_ButtonBannerClassic.gameObject.SetActive(false);
		TokyoDrift componentInChildren = this.m_SetRotationButton.GetComponentInChildren<TokyoDrift>();
		if (componentInChildren == null)
		{
			return;
		}
		componentInChildren.enabled = false;
	}

	// Token: 0x06005E90 RID: 24208 RVA: 0x001EBBA8 File Offset: 0x001E9DA8
	private void ShowButtonBanner()
	{
		this.m_ButtonBanner.SetActive(true);
		this.m_ButtonBannerStandard.gameObject.SetActive(true);
		this.m_ButtonBanner.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		iTween.ScaleTo(this.m_ButtonBanner, iTween.Hash(new object[]
		{
			"scale",
			this.m_buttonBannerScale,
			"time",
			0.15f,
			"easetype",
			iTween.EaseType.easeOutQuad
		}));
	}

	// Token: 0x06005E91 RID: 24209 RVA: 0x001EBC48 File Offset: 0x001E9E48
	private void ShowButtonYellowGlow()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"islocal",
			true,
			"from",
			0f,
			"to",
			1f,
			"time",
			0.3f,
			"easeType",
			iTween.EaseType.easeOutExpo,
			"onupdate",
			new Action<object>(delegate(object value)
			{
				this.m_ButtonGlowPlaneYellow.GetComponent<Renderer>().GetMaterial().SetFloat("_Intensity", (float)value);
			}),
			"onupdatetarget",
			base.gameObject
		});
		iTween.ValueTo(base.gameObject, args);
	}

	// Token: 0x06005E92 RID: 24210 RVA: 0x001EBD00 File Offset: 0x001E9F00
	private void CrossFadeToGreenGlow()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"islocal",
			true,
			"from",
			1f,
			"to",
			0f,
			"time",
			0.3f,
			"easeType",
			iTween.EaseType.easeOutExpo,
			"onupdate",
			new Action<object>(delegate(object value)
			{
				this.m_ButtonGlowPlaneYellow.GetComponent<Renderer>().GetMaterial().SetFloat("_Intensity", (float)value);
			}),
			"onupdatetarget",
			this.m_ButtonGlowPlaneYellow
		});
		iTween.ValueTo(base.gameObject, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"islocal",
			true,
			"from",
			0f,
			"to",
			1f,
			"time",
			0.3f,
			"easeType",
			iTween.EaseType.easeOutExpo,
			"onupdate",
			new Action<object>(delegate(object value)
			{
				this.m_ButtonGlowPlaneGreen.GetComponent<Renderer>().GetMaterial().SetFloat("_Intensity", (float)value);
			}),
			"onupdatetarget",
			base.gameObject
		});
		iTween.ValueTo(this.m_ButtonGlowPlaneGreen, args2);
	}

	// Token: 0x06005E93 RID: 24211 RVA: 0x001EBE60 File Offset: 0x001EA060
	private void ButtonBannerCrossFadeText()
	{
		this.m_ButtonBannerStandard.gameObject.SetActive(true);
		this.m_ButtonBannerClassic.gameObject.SetActive(true);
		Color textColor = this.m_ButtonBannerClassic.TextColor;
		textColor.a = 0f;
		this.m_ButtonBannerClassic.TextColor = textColor;
		iTween.FadeTo(this.m_ButtonBannerStandard.gameObject, 0f, this.m_ButtonFlipTime * 0.1f);
		iTween.FadeTo(this.m_ButtonBannerClassic.gameObject, 1f, this.m_ButtonFlipTime * 0.1f);
	}

	// Token: 0x06005E94 RID: 24212 RVA: 0x001EBEF8 File Offset: 0x001EA0F8
	private void ButtonBannerPunch()
	{
		Vector3 localScale = this.m_ButtonBanner.transform.localScale;
		iTween.ScaleTo(this.m_ButtonBanner, iTween.Hash(new object[]
		{
			"scale",
			localScale * 1.5f,
			"time",
			0.075f,
			"delay",
			this.m_ButtonFlipTime * 0.25f,
			"easetype",
			iTween.EaseType.easeOutQuad,
			"onupdatetarget",
			base.gameObject
		}));
		iTween.ScaleTo(this.m_ButtonBanner, iTween.Hash(new object[]
		{
			"scale",
			localScale,
			"time",
			0.25f,
			"delay",
			this.m_ButtonFlipTime * 0.25f + 0.075f,
			"easetype",
			iTween.EaseType.easeInOutQuad,
			"onupdatetarget",
			base.gameObject
		}));
	}

	// Token: 0x06005E95 RID: 24213 RVA: 0x001EC020 File Offset: 0x001EA220
	private void HideButtonBanner()
	{
		iTween.ScaleTo(this.m_ButtonBanner, iTween.Hash(new object[]
		{
			"scale",
			Vector3.zero,
			"time",
			0.25f,
			"easetype",
			iTween.EaseType.easeInQuad,
			"oncompletetarget",
			base.gameObject,
			"oncomplete",
			"HideButtonBannerComplete"
		}));
	}

	// Token: 0x06005E96 RID: 24214 RVA: 0x001EC0A1 File Offset: 0x001EA2A1
	private void HideButtonBannerComplete()
	{
		this.m_ButtonBanner.SetActive(false);
	}

	// Token: 0x06005E97 RID: 24215 RVA: 0x001EC0B0 File Offset: 0x001EA2B0
	private void FlipButton()
	{
		iTween.RotateTo(this.m_SetRotationButtonMesh, iTween.Hash(new object[]
		{
			"z",
			0f,
			"time",
			this.m_ButtonFlipTime,
			"islocal",
			true,
			"easetype",
			iTween.EaseType.easeOutElastic
		}));
	}

	// Token: 0x06005E98 RID: 24216 RVA: 0x001EC124 File Offset: 0x001EA324
	private void MoveButtonUp()
	{
		float num = this.m_MoveButtonUpZ;
		if (UniversalInputManager.UsePhoneUI)
		{
			num = this.m_MoveButtonUpZphone;
		}
		iTween.MoveTo(this.m_SetRotationButton, iTween.Hash(new object[]
		{
			"z",
			num,
			"delay",
			0f,
			"time",
			this.m_MoveButtonUpTime,
			"islocal",
			true,
			"easetype",
			iTween.EaseType.easeInOutQuint
		}));
	}

	// Token: 0x06005E99 RID: 24217 RVA: 0x001EC1C4 File Offset: 0x001EA3C4
	private void VignetteBackground(float time)
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr == null)
		{
			Debug.LogWarning("FullScreenFXMgr is NULL!");
			return;
		}
		fullScreenFXMgr.Vignette(0.99f, time, iTween.EaseType.easeOutCubic, null, null);
	}

	// Token: 0x06005E9A RID: 24218 RVA: 0x001EC1F4 File Offset: 0x001EA3F4
	private void StopVignetteBackground(float time)
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr == null)
		{
			Debug.LogWarning("FullScreenFXMgr is NULL!");
			return;
		}
		fullScreenFXMgr.Vignette(0f, time, iTween.EaseType.easeInCubic, null, null);
	}

	// Token: 0x06005E9B RID: 24219 RVA: 0x001EC224 File Offset: 0x001EA424
	private void BlurBackground(float time)
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr == null)
		{
			Debug.LogWarning("FullScreenFXMgr is NULL!");
			return;
		}
		fullScreenFXMgr.StartStandardBlurVignette(time);
	}

	// Token: 0x06005E9C RID: 24220 RVA: 0x001EC24C File Offset: 0x001EA44C
	private void StopBlurBackground(float time)
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr == null)
		{
			Debug.LogWarning("FullScreenFXMgr is NULL!");
			return;
		}
		fullScreenFXMgr.EndStandardBlurVignette(time, null);
	}

	// Token: 0x06005E9D RID: 24221 RVA: 0x001EC278 File Offset: 0x001EA478
	private void MoveButtonToDeckPickerTray(bool socketAsClassic)
	{
		this.StopButtonDrift();
		Vector3 vector = Vector3.zero;
		Vector3 vector2 = Vector3.one;
		GameObject theClockButtonBone = DeckPickerTrayDisplay.Get().m_TheClockButtonBone;
		if (theClockButtonBone != null)
		{
			vector = theClockButtonBone.transform.position;
			vector2 = theClockButtonBone.transform.localScale;
		}
		Vector3 vector3 = Vector3.Lerp(this.m_SetRotationButton.transform.position, vector, 0.75f);
		vector3 = new Vector3(vector3.x + 7f, vector3.y, vector3.z);
		Vector3[] array = new Vector3[]
		{
			this.m_SetRotationButton.transform.position,
			vector3,
			vector
		};
		base.GetComponent<Animator>().SetTrigger("SocketButton");
		Hashtable args = iTween.Hash(new object[]
		{
			"path",
			array,
			"delay",
			0f,
			"time",
			this.m_ButtonToTrayAnimTime,
			"islocal",
			false,
			"easetype",
			iTween.EaseType.easeInOutQuint,
			"oncompletetarget",
			base.gameObject,
			"oncomplete",
			"ButtonImpactAndShutdownTheClock"
		});
		iTween.MoveTo(this.m_SetRotationButton, args);
		iTween.RotateTo(this.m_SetRotationButtonMesh, iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, 0f, socketAsClassic ? 0f : 180f),
			"time",
			this.m_ButtonToTrayAnimTime,
			"islocal",
			true,
			"easetype",
			iTween.EaseType.easeInOutQuint
		}));
		iTween.RotateTo(this.m_SetRotationButton, iTween.Hash(new object[]
		{
			"rotation",
			Vector3.zero,
			"time",
			this.m_ButtonToTrayAnimTime,
			"islocal",
			true,
			"easetype",
			iTween.EaseType.easeInOutQuint
		}));
		iTween.ScaleTo(this.m_SetRotationButton, iTween.Hash(new object[]
		{
			"scale",
			vector2,
			"delay",
			0f,
			"time",
			this.m_ButtonToTrayAnimTime,
			"easetype",
			iTween.EaseType.easeInOutQuint
		}));
	}

	// Token: 0x06005E9E RID: 24222 RVA: 0x001EC521 File Offset: 0x001EA721
	private void ButtonImpactAndShutdownTheClock()
	{
		this.ShakeCamera();
		this.m_ImpactParticles.Play();
		base.StartCoroutine(this.FinalGlowAndDisableTheClock());
	}

	// Token: 0x06005E9F RID: 24223 RVA: 0x001EC541 File Offset: 0x001EA741
	private IEnumerator FinalGlowAndDisableTheClock()
	{
		this.EndClockStartTutorial();
		Renderer renderer = SetRotationManager.HasSeenStandardModeTutorial() ? this.m_ButtonGlowPlaneYellow.GetComponent<Renderer>() : this.m_ButtonGlowPlaneGreen.GetComponent<Renderer>();
		Material glowMat = renderer.GetMaterial();
		float animLength = this.m_ButtonGlowAnimation.keys[this.m_ButtonGlowAnimation.length - 1].time;
		float animTime = 0f;
		while (animTime < animLength)
		{
			animTime += Time.deltaTime;
			glowMat.SetFloat("_Intensity", this.m_ButtonGlowAnimation.Evaluate(animTime));
			yield return null;
		}
		yield return new WaitForSeconds(3f);
		base.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x06005EA0 RID: 24224 RVA: 0x00028167 File Offset: 0x00026367
	private void DisableTheClock()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06005EA1 RID: 24225 RVA: 0x001EC550 File Offset: 0x001EA750
	private void EndClockStartTutorial()
	{
		SetRotationClock.DisableTheClockCallback callback = new SetRotationClock.DisableTheClockCallback(this.DisableTheClock);
		base.GetComponent<Animator>().StopPlayback();
		DeckPickerTrayDisplay.Get().StartSetRotationTutorial(callback);
	}

	// Token: 0x06005EA2 RID: 24226 RVA: 0x001EC580 File Offset: 0x001EA780
	private void OnClick(UIEvent e)
	{
		this.m_clickCaptured = true;
	}

	// Token: 0x04004F96 RID: 20374
	public Texture2D m_PreviousIcon;

	// Token: 0x04004F97 RID: 20375
	public Texture2D m_PreviousIconBlur;

	// Token: 0x04004F98 RID: 20376
	public Texture2D m_NewIcon;

	// Token: 0x04004F99 RID: 20377
	public Texture2D m_NewIconBlur;

	// Token: 0x04004F9A RID: 20378
	public Renderer m_GlassPanel;

	// Token: 0x04004F9B RID: 20379
	public float m_AnimationWaitTime = 5.5f;

	// Token: 0x04004F9C RID: 20380
	public GameObject m_CenterPanel;

	// Token: 0x04004F9D RID: 20381
	public float m_CenterPanelFlipTime = 1f;

	// Token: 0x04004F9E RID: 20382
	public GameObject m_SetRotationButton;

	// Token: 0x04004F9F RID: 20383
	public GameObject m_SetRotationButtonMesh;

	// Token: 0x04004FA0 RID: 20384
	public GameObject m_SetRotationIconWidget;

	// Token: 0x04004FA1 RID: 20385
	public float m_SetRotationButtonDelay = 0.75f;

	// Token: 0x04004FA2 RID: 20386
	public float m_SetRotationButtonWobbleTime = 0.5f;

	// Token: 0x04004FA3 RID: 20387
	public float m_ButtonRotationHoldTime = 1.5f;

	// Token: 0x04004FA4 RID: 20388
	public GameObject m_ButtonRiseBone;

	// Token: 0x04004FA5 RID: 20389
	public GameObject m_ButtonBanner;

	// Token: 0x04004FA6 RID: 20390
	public UberText m_ButtonBannerStandard;

	// Token: 0x04004FA7 RID: 20391
	public UberText m_ButtonBannerClassic;

	// Token: 0x04004FA8 RID: 20392
	public Color m_ButtonBannerTextColor = Color.white;

	// Token: 0x04004FA9 RID: 20393
	public float m_ButtonRiseTime = 1.75f;

	// Token: 0x04004FAA RID: 20394
	public float m_BlurScreenDelay = 0.5f;

	// Token: 0x04004FAB RID: 20395
	public float m_BlurScreenTime = 1f;

	// Token: 0x04004FAC RID: 20396
	public float m_MoveButtonUpZ = -0.1f;

	// Token: 0x04004FAD RID: 20397
	public float m_MoveButtonUpZphone = -0.3f;

	// Token: 0x04004FAE RID: 20398
	public float m_MoveButtonUpTime = 1f;

	// Token: 0x04004FAF RID: 20399
	public float m_ButtonFlipTime = 0.5f;

	// Token: 0x04004FB0 RID: 20400
	public float m_ButtonToTrayAnimTime = 0.5f;

	// Token: 0x04004FB1 RID: 20401
	public float m_EndBlurScreenDelay = 0.5f;

	// Token: 0x04004FB2 RID: 20402
	public float m_EndBlurScreenTime = 1f;

	// Token: 0x04004FB3 RID: 20403
	public float m_MoveButtonToTrayDelay = 1.5f;

	// Token: 0x04004FB4 RID: 20404
	public float m_TextDelayTime = 1f;

	// Token: 0x04004FB5 RID: 20405
	public float m_VeteranGhostedIconDelayTime = 3f;

	// Token: 0x04004FB6 RID: 20406
	public ClockOverlayText m_overlayText;

	// Token: 0x04004FB7 RID: 20407
	public GameObject m_ButtonGlowPlaneYellow;

	// Token: 0x04004FB8 RID: 20408
	public GameObject m_ButtonGlowPlaneGreen;

	// Token: 0x04004FB9 RID: 20409
	public ParticleSystem m_ImpactParticles;

	// Token: 0x04004FBA RID: 20410
	public AnimationCurve m_ButtonGlowAnimation;

	// Token: 0x04004FBB RID: 20411
	public PegUIElement m_clickCatcher;

	// Token: 0x04004FBC RID: 20412
	public AudioSource m_TheClockAmbientSound;

	// Token: 0x04004FBD RID: 20413
	public float m_TheClockAmbientSoundVolume = 1f;

	// Token: 0x04004FBE RID: 20414
	public float m_TheClockAmbientSoundFadeInTime = 2f;

	// Token: 0x04004FBF RID: 20415
	public float m_TheClockAmbientSoundFadeOutTime = 1f;

	// Token: 0x04004FC0 RID: 20416
	public AudioSource m_ClickSound;

	// Token: 0x04004FC1 RID: 20417
	public AudioSource m_Stage1Sound;

	// Token: 0x04004FC2 RID: 20418
	public AudioSource m_Stage1Sound_Veteran;

	// Token: 0x04004FC3 RID: 20419
	public AudioSource m_Stage2Sound;

	// Token: 0x04004FC4 RID: 20420
	public AudioSource m_Stage2Sound_Veteran;

	// Token: 0x04004FC5 RID: 20421
	public AudioSource m_Stage3Sound;

	// Token: 0x04004FC6 RID: 20422
	public AudioSource m_Stage4Sound;

	// Token: 0x04004FC7 RID: 20423
	public AudioSource m_Stage5Sound;

	// Token: 0x04004FC8 RID: 20424
	public AudioSource m_Stage5Sound_Veteran;

	// Token: 0x04004FC9 RID: 20425
	private bool m_clickCaptured;

	// Token: 0x04004FCA RID: 20426
	private Vector3 m_buttonBannerScale;

	// Token: 0x04004FCB RID: 20427
	private AudioSource m_ambientSound;

	// Token: 0x04004FCC RID: 20428
	private const float BUTTON_MESH_Z_ROTATION_FOR_CLASSIC = 0f;

	// Token: 0x04004FCD RID: 20429
	private const float BUTTON_MESH_Z_ROTATION_FOR_STANDARD = 180f;

	// Token: 0x04004FCE RID: 20430
	private static SetRotationClock s_instance;

	// Token: 0x020021C3 RID: 8643
	// (Invoke) Token: 0x060124B9 RID: 74937
	public delegate void DisableTheClockCallback();
}
