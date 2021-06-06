using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class GeneralStorePhoneCover : MonoBehaviour
{
	public delegate void AnimationCallback();

	[Serializable]
	public class ModeAnimation
	{
		public GeneralStoreMode m_mode;

		public string m_playAnimationName;
	}

	[CustomEditField(Sections = "General UI")]
	public GeneralStore m_parentStore;

	[CustomEditField(Sections = "General UI")]
	public PegUIElement m_backToCoverButton;

	[CustomEditField(Sections = "Animation")]
	public Animator m_animationController;

	[CustomEditField(Sections = "Animation")]
	public string m_buttonEnterAnimation = "";

	[CustomEditField(Sections = "Animation")]
	public List<ModeAnimation> m_buttonExitAnimations = new List<ModeAnimation>();

	[CustomEditField(Sections = "UI Blockers")]
	public GameObject m_coverClickArea;

	[CustomEditField(Sections = "UI Blockers")]
	public GameObject m_animationClickBlocker;

	[CustomEditField(Sections = "Aspect Ratio Scaling")]
	public float m_scale3to2_XZ = 0.39f;

	[CustomEditField(Sections = "Aspect Ratio Scaling")]
	public float m_scale16to9_XZ = 0.37f;

	[CustomEditField(Sections = "Aspect Ratio Scaling")]
	public float m_scaleExtraWide_XZ = 0.79f;

	[CustomEditField(Sections = "Aspect Ratio Scaling")]
	public float m_scaleY = 0.35f;

	private static GeneralStorePhoneCover s_instance;

	private const string s_coverAnimationCoroutine = "PlayAndWaitForAnimation";

	private void Awake()
	{
		s_instance = this;
		m_parentStore.RegisterModeChangedListener(OnGeneralStoreModeChanged);
		m_backToCoverButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			Navigation.GoBack();
		});
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	private void Start()
	{
		ShowCover();
	}

	public void ShowCover()
	{
		UpdateCoverScale();
		StopCoroutine("PlayAndWaitForAnimation");
		StartCoroutine("PlayAndWaitForAnimation", m_buttonEnterAnimation);
		m_coverClickArea.SetActive(value: true);
	}

	public void HideCover(GeneralStoreMode selectedMode)
	{
		StartCoroutine(PushBackMethodWhenShown());
		ModeAnimation modeAnimation = m_buttonExitAnimations.Find((ModeAnimation o) => o.m_mode == selectedMode);
		if (modeAnimation == null)
		{
			Debug.LogError($"Unable to find animation for {selectedMode} mode.");
			return;
		}
		if (string.IsNullOrEmpty(modeAnimation.m_playAnimationName))
		{
			Debug.LogError($"Animation name not defined for {selectedMode} mode.");
			return;
		}
		StopCoroutine("PlayAndWaitForAnimation");
		StartCoroutine("PlayAndWaitForAnimation", modeAnimation.m_playAnimationName);
		m_coverClickArea.SetActive(value: false);
	}

	private IEnumerator PushBackMethodWhenShown()
	{
		while (!m_parentStore.IsShown())
		{
			yield return null;
		}
		Navigation.Push(OnNavigateBack);
	}

	private void OnGeneralStoreModeChanged(GeneralStoreMode oldMode, GeneralStoreMode newMode)
	{
		if (newMode != 0)
		{
			HideCover(newMode);
		}
		else
		{
			ShowCover();
		}
	}

	private IEnumerator PlayAndWaitForAnimation(string animationName)
	{
		m_animationController.enabled = true;
		m_animationController.StopPlayback();
		m_animationClickBlocker.SetActive(value: true);
		yield return new WaitForEndOfFrame();
		m_animationController.Play(animationName);
		while (m_animationController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
		{
			yield return null;
		}
		m_animationClickBlocker.SetActive(value: false);
	}

	private void UpdateCoverScale()
	{
		float num = ((!TransformUtil.IsExtraWideAspectRatio()) ? TransformUtil.GetAspectRatioDependentValue(m_scale3to2_XZ, m_scale16to9_XZ, m_scale16to9_XZ) : m_scaleExtraWide_XZ);
		base.transform.localScale = new Vector3(num, m_scaleY, num);
	}

	public static bool OnNavigateBack()
	{
		if (s_instance == null)
		{
			return false;
		}
		s_instance.m_parentStore.SetMode(GeneralStoreMode.NONE);
		return true;
	}
}
