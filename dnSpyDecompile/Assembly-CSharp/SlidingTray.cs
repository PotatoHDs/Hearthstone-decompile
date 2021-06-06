using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005F9 RID: 1529
[CustomEditClass]
public class SlidingTray : MonoBehaviour
{
	// Token: 0x1400002D RID: 45
	// (add) Token: 0x0600532A RID: 21290 RVA: 0x001B297C File Offset: 0x001B0B7C
	// (remove) Token: 0x0600532B RID: 21291 RVA: 0x001B29B4 File Offset: 0x001B0BB4
	public event Action OnTransitionComplete;

	// Token: 0x0600532C RID: 21292 RVA: 0x001B29EC File Offset: 0x001B0BEC
	private void Awake()
	{
		UniversalInputManager.UsePhoneUI;
		if (this.m_traySliderButton != null)
		{
			this.m_traySliderButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnTraySliderPressed));
		}
		if (this.m_offClickCatcher != null)
		{
			this.m_offClickCatcher.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClickCatcherPressed));
		}
		if (this.m_darkenQuad != null)
		{
			this.m_darkenQuad.gameObject.SetActive(false);
			this.m_darkenQuad.GetMaterial().color = this.m_quadHiddenColor;
		}
		if (this.m_invisibleOnHide)
		{
			base.transform.localPosition = this.INVISIBLE_POSITION;
		}
		if (SceneMgr.Get() != null)
		{
			this.m_sceneContext = SceneMgr.Get().GetMode();
		}
	}

	// Token: 0x0600532D RID: 21293 RVA: 0x001B2AB8 File Offset: 0x001B0CB8
	private void Start()
	{
		if (!this.m_startingPositionSet)
		{
			if (this.m_invisibleOnHide)
			{
				base.transform.localPosition = this.INVISIBLE_POSITION;
			}
			else
			{
				base.transform.localPosition = this.m_trayHiddenBone.localPosition;
			}
			this.m_trayShown = false;
			if (this.m_inactivateOnHide)
			{
				base.gameObject.SetActive(false);
			}
			this.m_startingPositionSet = true;
		}
	}

	// Token: 0x0600532E RID: 21294 RVA: 0x001B2B20 File Offset: 0x001B0D20
	private void OnDestroy()
	{
		if (this.m_offClickCatcher != null)
		{
			this.m_offClickCatcher.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClickCatcherPressed));
		}
		if (this.m_traySliderButton != null)
		{
			this.m_traySliderButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnTraySliderPressed));
		}
		if (FullScreenFXMgr.Get() != null && this.m_sceneContext != SceneMgr.Mode.GAME_MODE)
		{
			FullScreenFXMgr.Get().EndStandardBlurVignette(0f, null);
		}
	}

	// Token: 0x0600532F RID: 21295 RVA: 0x001B2B9C File Offset: 0x001B0D9C
	[ContextMenu("Show")]
	public void ShowTray()
	{
		this.ToggleTraySlider(true, null, true);
	}

	// Token: 0x06005330 RID: 21296 RVA: 0x001B2BA7 File Offset: 0x001B0DA7
	[ContextMenu("Hide")]
	public void HideTray()
	{
		this.ToggleTraySlider(false, null, true);
	}

	// Token: 0x06005331 RID: 21297 RVA: 0x001B2BB4 File Offset: 0x001B0DB4
	public void ToggleTraySlider(bool show, Transform target = null, bool animate = true)
	{
		if (this.m_trayShown == show)
		{
			return;
		}
		if (show && target != null)
		{
			this.m_trayShownBone = target;
		}
		this.m_trayShown = show;
		if (show)
		{
			this.DoShowLogic(animate);
		}
		else
		{
			this.DoHideLogic(animate);
		}
		this.m_startingPositionSet = true;
		if (this.m_trayToggledListener != null)
		{
			this.m_trayToggledListener(show);
		}
	}

	// Token: 0x06005332 RID: 21298 RVA: 0x001B2C13 File Offset: 0x001B0E13
	public bool TraySliderIsAnimating()
	{
		return this.m_traySliderAnimating;
	}

	// Token: 0x06005333 RID: 21299 RVA: 0x001B2C1B File Offset: 0x001B0E1B
	public bool IsAnimatingToShow()
	{
		return this.m_traySliderAnimating && this.m_trayShown;
	}

	// Token: 0x06005334 RID: 21300 RVA: 0x001B2C2D File Offset: 0x001B0E2D
	public bool IsAnimatingToHide()
	{
		return this.m_traySliderAnimating && !this.m_trayShown;
	}

	// Token: 0x06005335 RID: 21301 RVA: 0x001B2C42 File Offset: 0x001B0E42
	public bool IsTrayInShownPosition()
	{
		return base.gameObject.transform.localPosition == this.m_trayShownBone.localPosition;
	}

	// Token: 0x06005336 RID: 21302 RVA: 0x001B2C64 File Offset: 0x001B0E64
	public bool IsShown()
	{
		return this.m_trayShown;
	}

	// Token: 0x06005337 RID: 21303 RVA: 0x001B2C6C File Offset: 0x001B0E6C
	public void RegisterTrayToggleListener(SlidingTray.TrayToggledListener listener)
	{
		this.m_trayToggledListener = listener;
	}

	// Token: 0x06005338 RID: 21304 RVA: 0x001B2C75 File Offset: 0x001B0E75
	public void UnregisterTrayToggleListener(SlidingTray.TrayToggledListener listener)
	{
		if (this.m_trayToggledListener == listener)
		{
			this.m_trayToggledListener = null;
			return;
		}
		Log.All.Print("Attempting to unregister a TrayToggleListener that has not been registered!", Array.Empty<object>());
	}

	// Token: 0x06005339 RID: 21305 RVA: 0x001B2CA1 File Offset: 0x001B0EA1
	public void SetLayers(GameLayer visible, GameLayer hidden)
	{
		this.m_shownLayer = visible;
		this.m_hiddenLayer = hidden;
	}

	// Token: 0x0600533A RID: 21306 RVA: 0x001B2CB4 File Offset: 0x001B0EB4
	private void DoShowLogic(bool animate)
	{
		if (this.m_useNavigationBack)
		{
			Navigation.Push(new Navigation.NavigateBackHandler(this.BackPressed));
		}
		base.gameObject.SetActive(true);
		if (base.gameObject.activeInHierarchy && animate)
		{
			base.transform.localPosition = this.m_trayHiddenBone.localPosition;
			Hashtable args = iTween.Hash(new object[]
			{
				"position",
				this.m_trayShownBone.localPosition,
				"isLocal",
				true,
				"time",
				this.m_traySlideDuration,
				"oncomplete",
				"OnTraySliderAnimFinished",
				"oncompletetarget",
				base.gameObject,
				"easetype",
				this.m_animateBounce ? iTween.EaseType.easeOutBounce : iTween.Defaults.easeType
			});
			iTween.MoveTo(base.gameObject, args);
			this.m_traySliderAnimating = true;
			if (this.m_offClickCatcher != null)
			{
				if (this.m_darkenQuad != null)
				{
					this.m_darkenQuad.gameObject.SetActive(true);
					iTween.Stop(this.m_darkenQuad.gameObject);
					Hashtable args2 = iTween.Hash(new object[]
					{
						"from",
						this.m_currentQuadFade,
						"to",
						1f,
						"time",
						this.m_animateBlurInTime,
						"onupdate",
						"DarkenQuadFade_Update",
						"onupdatetarget",
						base.gameObject
					});
					iTween.ValueTo(this.m_darkenQuad.gameObject, args2);
				}
				else
				{
					this.FadeEffectsIn(this.m_animateBlurInTime);
				}
				this.m_offClickCatcher.gameObject.SetActive(true);
			}
			if (this.m_playAudioOnSlide)
			{
				SoundManager.Get().LoadAndPlay(this.m_SlideOnSFXAssetString, base.gameObject);
				return;
			}
		}
		else
		{
			base.gameObject.transform.localPosition = this.m_trayShownBone.localPosition;
			if (this.m_darkenQuad != null)
			{
				iTween.Stop(this.m_darkenQuad.gameObject);
				this.m_currentQuadFade = 1f;
				this.m_darkenQuad.GetMaterial().color = this.m_quadShownColor;
			}
			this.OnTraySliderAnimFinished();
		}
	}

	// Token: 0x0600533B RID: 21307 RVA: 0x001B2F1C File Offset: 0x001B111C
	private void DoHideLogic(bool animate)
	{
		if (this.m_useNavigationBack)
		{
			Navigation.RemoveHandler(new Navigation.NavigateBackHandler(this.BackPressed));
		}
		if (base.gameObject.activeInHierarchy && animate)
		{
			Hashtable args = iTween.Hash(new object[]
			{
				"position",
				this.m_trayHiddenBone.localPosition,
				"isLocal",
				true,
				"oncomplete",
				"OnTraySliderAnimFinished",
				"oncompletetarget",
				base.gameObject,
				"time",
				this.m_animateBounce ? this.m_traySlideDuration : (this.m_traySlideDuration / 2f),
				"easetype",
				this.m_animateBounce ? iTween.EaseType.easeOutBounce : iTween.EaseType.linear
			});
			iTween.MoveTo(base.gameObject, args);
			this.m_traySliderAnimating = true;
			if (this.m_offClickCatcher != null)
			{
				if (this.m_darkenQuad != null)
				{
					iTween.Stop(this.m_darkenQuad.gameObject);
					Hashtable args2 = iTween.Hash(new object[]
					{
						"from",
						this.m_currentQuadFade,
						"to",
						0f,
						"time",
						this.m_animateBlurOutTime,
						"onupdate",
						"DarkenQuadFade_Update",
						"onupdatetarget",
						base.gameObject
					});
					iTween.ValueTo(this.m_darkenQuad.gameObject, args2);
				}
				else
				{
					this.FadeEffectsOut(this.m_animateBlurOutTime);
				}
				this.m_offClickCatcher.gameObject.SetActive(false);
			}
			if (this.m_playAudioOnSlide)
			{
				SoundManager.Get().LoadAndPlay(this.m_SlideOffSFXAssetString, base.gameObject);
				return;
			}
		}
		else
		{
			base.gameObject.transform.localPosition = this.m_trayHiddenBone.localPosition;
			if (this.m_darkenQuad != null)
			{
				iTween.Stop(this.m_darkenQuad.gameObject);
				this.m_currentQuadFade = 0f;
				this.m_darkenQuad.GetMaterial().color = this.m_quadHiddenColor;
			}
			this.OnTraySliderAnimFinished();
		}
	}

	// Token: 0x0600533C RID: 21308 RVA: 0x001B3165 File Offset: 0x001B1365
	private bool BackPressed()
	{
		this.ToggleTraySlider(false, null, true);
		return true;
	}

	// Token: 0x0600533D RID: 21309 RVA: 0x001B3174 File Offset: 0x001B1374
	private void OnTraySliderAnimFinished()
	{
		this.m_traySliderAnimating = false;
		if (!this.m_trayShown)
		{
			if (this.m_inactivateOnHide)
			{
				base.gameObject.SetActive(false);
			}
			if (this.m_invisibleOnHide)
			{
				base.transform.localPosition = new Vector3(0f, 0f, -500f);
			}
			if (this.m_darkenQuad != null)
			{
				this.m_darkenQuad.gameObject.SetActive(false);
			}
			if (this.m_offClickCatcher != null)
			{
				this.m_offClickCatcher.gameObject.SetActive(false);
			}
		}
		if (this.OnTransitionComplete != null)
		{
			this.OnTransitionComplete();
		}
	}

	// Token: 0x0600533E RID: 21310 RVA: 0x001B321C File Offset: 0x001B141C
	private void OnTraySliderPressed(UIEvent e)
	{
		if (this.m_useNavigationBack && this.m_trayShown)
		{
			return;
		}
		this.ToggleTraySlider(!this.m_trayShown, null, true);
	}

	// Token: 0x0600533F RID: 21311 RVA: 0x001B2BA7 File Offset: 0x001B0DA7
	private void OnClickCatcherPressed(UIEvent e)
	{
		this.ToggleTraySlider(false, null, true);
	}

	// Token: 0x06005340 RID: 21312 RVA: 0x001B3240 File Offset: 0x001B1440
	private void FadeEffectsIn(float time)
	{
		SceneUtils.SetLayer(base.gameObject, this.m_shownLayer);
		if (this.m_shownLayer == GameLayer.IgnoreFullScreenEffects)
		{
			SceneUtils.SetLayer(Box.Get().m_letterboxingContainer, this.m_shownLayer);
		}
		FullScreenFXMgr.Get().StartStandardBlurVignette(time);
	}

	// Token: 0x06005341 RID: 21313 RVA: 0x001B327D File Offset: 0x001B147D
	private void FadeEffectsOut(float time)
	{
		FullScreenFXMgr.Get().EndStandardBlurVignette(time, new Action(this.OnFadeFinished));
	}

	// Token: 0x06005342 RID: 21314 RVA: 0x001B3296 File Offset: 0x001B1496
	private void OnFadeFinished()
	{
		if (base.gameObject == null)
		{
			return;
		}
		SceneUtils.SetLayer(base.gameObject, this.m_shownLayer);
		if (this.m_hiddenLayer == GameLayer.Default)
		{
			SceneUtils.SetLayer(Box.Get().m_letterboxingContainer, this.m_hiddenLayer);
		}
	}

	// Token: 0x06005343 RID: 21315 RVA: 0x001B32D8 File Offset: 0x001B14D8
	private void DarkenQuadFade_Update(float fade)
	{
		this.m_currentQuadFade = fade;
		Color color = Color.Lerp(this.m_quadHiddenColor, this.m_quadShownColor, this.m_currentQuadFade);
		this.m_darkenQuad.GetMaterial().color = color;
	}

	// Token: 0x040049D0 RID: 18896
	[CustomEditField(Sections = "Bones")]
	public Transform m_trayHiddenBone;

	// Token: 0x040049D1 RID: 18897
	[CustomEditField(Sections = "Bones")]
	public Transform m_trayShownBone;

	// Token: 0x040049D2 RID: 18898
	[CustomEditField(Sections = "Parameters")]
	public bool m_inactivateOnHide = true;

	// Token: 0x040049D3 RID: 18899
	[CustomEditField(Sections = "Parameters")]
	[Tooltip("Useful to use (instead of 'inactivate On Hide') when the SlidingTray has Widgets on it that you want to load before it gets shown.")]
	public bool m_invisibleOnHide;

	// Token: 0x040049D4 RID: 18900
	[CustomEditField(Sections = "Parameters")]
	public bool m_useNavigationBack;

	// Token: 0x040049D5 RID: 18901
	[CustomEditField(Sections = "Parameters")]
	public bool m_playAudioOnSlide = true;

	// Token: 0x040049D6 RID: 18902
	[CustomEditField(Sections = "Parameters")]
	public string m_SlideOnSFXAssetString = "choose_opponent_panel_slide_on.prefab:66491d3d01ed663429ab80daf6a5e880";

	// Token: 0x040049D7 RID: 18903
	[CustomEditField(Sections = "Parameters")]
	public string m_SlideOffSFXAssetString = "choose_opponent_panel_slide_off.prefab:3139d09eb94899d41b9bf612649f47bf";

	// Token: 0x040049D8 RID: 18904
	[CustomEditField(Sections = "Parameters")]
	public float m_traySlideDuration = 0.5f;

	// Token: 0x040049D9 RID: 18905
	[CustomEditField(Sections = "Parameters")]
	public bool m_animateBounce;

	// Token: 0x040049DA RID: 18906
	[CustomEditField(Sections = "Parameters")]
	public float m_animateBlurInTime = 0.4f;

	// Token: 0x040049DB RID: 18907
	[CustomEditField(Sections = "Parameters")]
	public float m_animateBlurOutTime = 0.2f;

	// Token: 0x040049DC RID: 18908
	[CustomEditField(Sections = "Optional Features")]
	public PegUIElement m_offClickCatcher;

	// Token: 0x040049DD RID: 18909
	[CustomEditField(Sections = "Optional Features")]
	public MeshRenderer m_darkenQuad;

	// Token: 0x040049DE RID: 18910
	[CustomEditField(Sections = "Optional Features")]
	public PegUIElement m_traySliderButton;

	// Token: 0x040049E0 RID: 18912
	private bool m_trayShown;

	// Token: 0x040049E1 RID: 18913
	private bool m_traySliderAnimating;

	// Token: 0x040049E2 RID: 18914
	private SlidingTray.TrayToggledListener m_trayToggledListener;

	// Token: 0x040049E3 RID: 18915
	private bool m_startingPositionSet;

	// Token: 0x040049E4 RID: 18916
	private GameLayer m_hiddenLayer;

	// Token: 0x040049E5 RID: 18917
	private GameLayer m_shownLayer = GameLayer.IgnoreFullScreenEffects;

	// Token: 0x040049E6 RID: 18918
	private Color m_quadHiddenColor = Color.white;

	// Token: 0x040049E7 RID: 18919
	private Color m_quadShownColor = new Color(0.53f, 0.53f, 0.53f, 1f);

	// Token: 0x040049E8 RID: 18920
	private float m_currentQuadFade;

	// Token: 0x040049E9 RID: 18921
	private readonly Vector3 INVISIBLE_POSITION = new Vector3(0f, 0f, -500f);

	// Token: 0x040049EA RID: 18922
	private SceneMgr.Mode m_sceneContext;

	// Token: 0x0200202A RID: 8234
	// (Invoke) Token: 0x06011C4E RID: 72782
	public delegate void TrayToggledListener(bool shown);
}
