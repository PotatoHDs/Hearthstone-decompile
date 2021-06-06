using System;
using System.Collections;
using UnityEngine;

[CustomEditClass]
public class SlidingTray : MonoBehaviour
{
	public delegate void TrayToggledListener(bool shown);

	[CustomEditField(Sections = "Bones")]
	public Transform m_trayHiddenBone;

	[CustomEditField(Sections = "Bones")]
	public Transform m_trayShownBone;

	[CustomEditField(Sections = "Parameters")]
	public bool m_inactivateOnHide = true;

	[CustomEditField(Sections = "Parameters")]
	[Tooltip("Useful to use (instead of 'inactivate On Hide') when the SlidingTray has Widgets on it that you want to load before it gets shown.")]
	public bool m_invisibleOnHide;

	[CustomEditField(Sections = "Parameters")]
	public bool m_useNavigationBack;

	[CustomEditField(Sections = "Parameters")]
	public bool m_playAudioOnSlide = true;

	[CustomEditField(Sections = "Parameters")]
	public string m_SlideOnSFXAssetString = "choose_opponent_panel_slide_on.prefab:66491d3d01ed663429ab80daf6a5e880";

	[CustomEditField(Sections = "Parameters")]
	public string m_SlideOffSFXAssetString = "choose_opponent_panel_slide_off.prefab:3139d09eb94899d41b9bf612649f47bf";

	[CustomEditField(Sections = "Parameters")]
	public float m_traySlideDuration = 0.5f;

	[CustomEditField(Sections = "Parameters")]
	public bool m_animateBounce;

	[CustomEditField(Sections = "Parameters")]
	public float m_animateBlurInTime = 0.4f;

	[CustomEditField(Sections = "Parameters")]
	public float m_animateBlurOutTime = 0.2f;

	[CustomEditField(Sections = "Optional Features")]
	public PegUIElement m_offClickCatcher;

	[CustomEditField(Sections = "Optional Features")]
	public MeshRenderer m_darkenQuad;

	[CustomEditField(Sections = "Optional Features")]
	public PegUIElement m_traySliderButton;

	private bool m_trayShown;

	private bool m_traySliderAnimating;

	private TrayToggledListener m_trayToggledListener;

	private bool m_startingPositionSet;

	private GameLayer m_hiddenLayer;

	private GameLayer m_shownLayer = GameLayer.IgnoreFullScreenEffects;

	private Color m_quadHiddenColor = Color.white;

	private Color m_quadShownColor = new Color(0.53f, 0.53f, 0.53f, 1f);

	private float m_currentQuadFade;

	private readonly Vector3 INVISIBLE_POSITION = new Vector3(0f, 0f, -500f);

	private SceneMgr.Mode m_sceneContext;

	public event Action OnTransitionComplete;

	private void Awake()
	{
		_ = (bool)UniversalInputManager.UsePhoneUI;
		if (m_traySliderButton != null)
		{
			m_traySliderButton.AddEventListener(UIEventType.RELEASE, OnTraySliderPressed);
		}
		if (m_offClickCatcher != null)
		{
			m_offClickCatcher.AddEventListener(UIEventType.RELEASE, OnClickCatcherPressed);
		}
		if (m_darkenQuad != null)
		{
			m_darkenQuad.gameObject.SetActive(value: false);
			m_darkenQuad.GetMaterial().color = m_quadHiddenColor;
		}
		if (m_invisibleOnHide)
		{
			base.transform.localPosition = INVISIBLE_POSITION;
		}
		if (SceneMgr.Get() != null)
		{
			m_sceneContext = SceneMgr.Get().GetMode();
		}
	}

	private void Start()
	{
		if (!m_startingPositionSet)
		{
			if (m_invisibleOnHide)
			{
				base.transform.localPosition = INVISIBLE_POSITION;
			}
			else
			{
				base.transform.localPosition = m_trayHiddenBone.localPosition;
			}
			m_trayShown = false;
			if (m_inactivateOnHide)
			{
				base.gameObject.SetActive(value: false);
			}
			m_startingPositionSet = true;
		}
	}

	private void OnDestroy()
	{
		if (m_offClickCatcher != null)
		{
			m_offClickCatcher.RemoveEventListener(UIEventType.RELEASE, OnClickCatcherPressed);
		}
		if (m_traySliderButton != null)
		{
			m_traySliderButton.RemoveEventListener(UIEventType.RELEASE, OnTraySliderPressed);
		}
		if (FullScreenFXMgr.Get() != null && m_sceneContext != SceneMgr.Mode.GAME_MODE)
		{
			FullScreenFXMgr.Get().EndStandardBlurVignette(0f);
		}
	}

	[ContextMenu("Show")]
	public void ShowTray()
	{
		ToggleTraySlider(show: true);
	}

	[ContextMenu("Hide")]
	public void HideTray()
	{
		ToggleTraySlider(show: false);
	}

	public void ToggleTraySlider(bool show, Transform target = null, bool animate = true)
	{
		if (m_trayShown != show)
		{
			if (show && target != null)
			{
				m_trayShownBone = target;
			}
			m_trayShown = show;
			if (show)
			{
				DoShowLogic(animate);
			}
			else
			{
				DoHideLogic(animate);
			}
			m_startingPositionSet = true;
			if (m_trayToggledListener != null)
			{
				m_trayToggledListener(show);
			}
		}
	}

	public bool TraySliderIsAnimating()
	{
		return m_traySliderAnimating;
	}

	public bool IsAnimatingToShow()
	{
		if (m_traySliderAnimating)
		{
			return m_trayShown;
		}
		return false;
	}

	public bool IsAnimatingToHide()
	{
		if (m_traySliderAnimating)
		{
			return !m_trayShown;
		}
		return false;
	}

	public bool IsTrayInShownPosition()
	{
		return base.gameObject.transform.localPosition == m_trayShownBone.localPosition;
	}

	public bool IsShown()
	{
		return m_trayShown;
	}

	public void RegisterTrayToggleListener(TrayToggledListener listener)
	{
		m_trayToggledListener = listener;
	}

	public void UnregisterTrayToggleListener(TrayToggledListener listener)
	{
		if (m_trayToggledListener == listener)
		{
			m_trayToggledListener = null;
		}
		else
		{
			Log.All.Print("Attempting to unregister a TrayToggleListener that has not been registered!");
		}
	}

	public void SetLayers(GameLayer visible, GameLayer hidden)
	{
		m_shownLayer = visible;
		m_hiddenLayer = hidden;
	}

	private void DoShowLogic(bool animate)
	{
		if (m_useNavigationBack)
		{
			Navigation.Push(BackPressed);
		}
		base.gameObject.SetActive(value: true);
		if (base.gameObject.activeInHierarchy && animate)
		{
			base.transform.localPosition = m_trayHiddenBone.localPosition;
			Hashtable args = iTween.Hash("position", m_trayShownBone.localPosition, "isLocal", true, "time", m_traySlideDuration, "oncomplete", "OnTraySliderAnimFinished", "oncompletetarget", base.gameObject, "easetype", m_animateBounce ? iTween.EaseType.easeOutBounce : iTween.Defaults.easeType);
			iTween.MoveTo(base.gameObject, args);
			m_traySliderAnimating = true;
			if (m_offClickCatcher != null)
			{
				if (m_darkenQuad != null)
				{
					m_darkenQuad.gameObject.SetActive(value: true);
					iTween.Stop(m_darkenQuad.gameObject);
					Hashtable args2 = iTween.Hash("from", m_currentQuadFade, "to", 1f, "time", m_animateBlurInTime, "onupdate", "DarkenQuadFade_Update", "onupdatetarget", base.gameObject);
					iTween.ValueTo(m_darkenQuad.gameObject, args2);
				}
				else
				{
					FadeEffectsIn(m_animateBlurInTime);
				}
				m_offClickCatcher.gameObject.SetActive(value: true);
			}
			if (m_playAudioOnSlide)
			{
				SoundManager.Get().LoadAndPlay(m_SlideOnSFXAssetString, base.gameObject);
			}
		}
		else
		{
			base.gameObject.transform.localPosition = m_trayShownBone.localPosition;
			if (m_darkenQuad != null)
			{
				iTween.Stop(m_darkenQuad.gameObject);
				m_currentQuadFade = 1f;
				m_darkenQuad.GetMaterial().color = m_quadShownColor;
			}
			OnTraySliderAnimFinished();
		}
	}

	private void DoHideLogic(bool animate)
	{
		if (m_useNavigationBack)
		{
			Navigation.RemoveHandler(BackPressed);
		}
		if (base.gameObject.activeInHierarchy && animate)
		{
			Hashtable args = iTween.Hash("position", m_trayHiddenBone.localPosition, "isLocal", true, "oncomplete", "OnTraySliderAnimFinished", "oncompletetarget", base.gameObject, "time", m_animateBounce ? m_traySlideDuration : (m_traySlideDuration / 2f), "easetype", m_animateBounce ? iTween.EaseType.easeOutBounce : iTween.EaseType.linear);
			iTween.MoveTo(base.gameObject, args);
			m_traySliderAnimating = true;
			if (m_offClickCatcher != null)
			{
				if (m_darkenQuad != null)
				{
					iTween.Stop(m_darkenQuad.gameObject);
					Hashtable args2 = iTween.Hash("from", m_currentQuadFade, "to", 0f, "time", m_animateBlurOutTime, "onupdate", "DarkenQuadFade_Update", "onupdatetarget", base.gameObject);
					iTween.ValueTo(m_darkenQuad.gameObject, args2);
				}
				else
				{
					FadeEffectsOut(m_animateBlurOutTime);
				}
				m_offClickCatcher.gameObject.SetActive(value: false);
			}
			if (m_playAudioOnSlide)
			{
				SoundManager.Get().LoadAndPlay(m_SlideOffSFXAssetString, base.gameObject);
			}
		}
		else
		{
			base.gameObject.transform.localPosition = m_trayHiddenBone.localPosition;
			if (m_darkenQuad != null)
			{
				iTween.Stop(m_darkenQuad.gameObject);
				m_currentQuadFade = 0f;
				m_darkenQuad.GetMaterial().color = m_quadHiddenColor;
			}
			OnTraySliderAnimFinished();
		}
	}

	private bool BackPressed()
	{
		ToggleTraySlider(show: false);
		return true;
	}

	private void OnTraySliderAnimFinished()
	{
		m_traySliderAnimating = false;
		if (!m_trayShown)
		{
			if (m_inactivateOnHide)
			{
				base.gameObject.SetActive(value: false);
			}
			if (m_invisibleOnHide)
			{
				base.transform.localPosition = new Vector3(0f, 0f, -500f);
			}
			if (m_darkenQuad != null)
			{
				m_darkenQuad.gameObject.SetActive(value: false);
			}
			if (m_offClickCatcher != null)
			{
				m_offClickCatcher.gameObject.SetActive(value: false);
			}
		}
		if (this.OnTransitionComplete != null)
		{
			this.OnTransitionComplete();
		}
	}

	private void OnTraySliderPressed(UIEvent e)
	{
		if (!m_useNavigationBack || !m_trayShown)
		{
			ToggleTraySlider(!m_trayShown);
		}
	}

	private void OnClickCatcherPressed(UIEvent e)
	{
		ToggleTraySlider(show: false);
	}

	private void FadeEffectsIn(float time)
	{
		SceneUtils.SetLayer(base.gameObject, m_shownLayer);
		if (m_shownLayer == GameLayer.IgnoreFullScreenEffects)
		{
			SceneUtils.SetLayer(Box.Get().m_letterboxingContainer, m_shownLayer);
		}
		FullScreenFXMgr.Get().StartStandardBlurVignette(time);
	}

	private void FadeEffectsOut(float time)
	{
		FullScreenFXMgr.Get().EndStandardBlurVignette(time, OnFadeFinished);
	}

	private void OnFadeFinished()
	{
		if (!(base.gameObject == null))
		{
			SceneUtils.SetLayer(base.gameObject, m_shownLayer);
			if (m_hiddenLayer == GameLayer.Default)
			{
				SceneUtils.SetLayer(Box.Get().m_letterboxingContainer, m_hiddenLayer);
			}
		}
	}

	private void DarkenQuadFade_Update(float fade)
	{
		m_currentQuadFade = fade;
		Color color = Color.Lerp(m_quadHiddenColor, m_quadShownColor, m_currentQuadFade);
		m_darkenQuad.GetMaterial().color = color;
	}
}
