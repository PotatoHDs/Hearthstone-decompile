using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000704 RID: 1796
[CustomEditClass]
public class GeneralStorePhoneCover : MonoBehaviour
{
	// Token: 0x060064A1 RID: 25761 RVA: 0x0020E344 File Offset: 0x0020C544
	private void Awake()
	{
		GeneralStorePhoneCover.s_instance = this;
		this.m_parentStore.RegisterModeChangedListener(new GeneralStore.ModeChanged(this.OnGeneralStoreModeChanged));
		this.m_backToCoverButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			Navigation.GoBack();
		});
	}

	// Token: 0x060064A2 RID: 25762 RVA: 0x0020E39A File Offset: 0x0020C59A
	private void OnDestroy()
	{
		GeneralStorePhoneCover.s_instance = null;
	}

	// Token: 0x060064A3 RID: 25763 RVA: 0x0020E3A2 File Offset: 0x0020C5A2
	private void Start()
	{
		this.ShowCover();
	}

	// Token: 0x060064A4 RID: 25764 RVA: 0x0020E3AA File Offset: 0x0020C5AA
	public void ShowCover()
	{
		this.UpdateCoverScale();
		base.StopCoroutine("PlayAndWaitForAnimation");
		base.StartCoroutine("PlayAndWaitForAnimation", this.m_buttonEnterAnimation);
		this.m_coverClickArea.SetActive(true);
	}

	// Token: 0x060064A5 RID: 25765 RVA: 0x0020E3DC File Offset: 0x0020C5DC
	public void HideCover(GeneralStoreMode selectedMode)
	{
		base.StartCoroutine(this.PushBackMethodWhenShown());
		GeneralStorePhoneCover.ModeAnimation modeAnimation = this.m_buttonExitAnimations.Find((GeneralStorePhoneCover.ModeAnimation o) => o.m_mode == selectedMode);
		if (modeAnimation == null)
		{
			Debug.LogError(string.Format("Unable to find animation for {0} mode.", selectedMode));
			return;
		}
		if (string.IsNullOrEmpty(modeAnimation.m_playAnimationName))
		{
			Debug.LogError(string.Format("Animation name not defined for {0} mode.", selectedMode));
			return;
		}
		base.StopCoroutine("PlayAndWaitForAnimation");
		base.StartCoroutine("PlayAndWaitForAnimation", modeAnimation.m_playAnimationName);
		this.m_coverClickArea.SetActive(false);
	}

	// Token: 0x060064A6 RID: 25766 RVA: 0x0020E48A File Offset: 0x0020C68A
	private IEnumerator PushBackMethodWhenShown()
	{
		while (!this.m_parentStore.IsShown())
		{
			yield return null;
		}
		Navigation.Push(new Navigation.NavigateBackHandler(GeneralStorePhoneCover.OnNavigateBack));
		yield break;
	}

	// Token: 0x060064A7 RID: 25767 RVA: 0x0020E499 File Offset: 0x0020C699
	private void OnGeneralStoreModeChanged(GeneralStoreMode oldMode, GeneralStoreMode newMode)
	{
		if (newMode != GeneralStoreMode.NONE)
		{
			this.HideCover(newMode);
			return;
		}
		this.ShowCover();
	}

	// Token: 0x060064A8 RID: 25768 RVA: 0x0020E4AC File Offset: 0x0020C6AC
	private IEnumerator PlayAndWaitForAnimation(string animationName)
	{
		this.m_animationController.enabled = true;
		this.m_animationController.StopPlayback();
		this.m_animationClickBlocker.SetActive(true);
		yield return new WaitForEndOfFrame();
		this.m_animationController.Play(animationName);
		while (this.m_animationController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
		{
			yield return null;
		}
		this.m_animationClickBlocker.SetActive(false);
		yield break;
	}

	// Token: 0x060064A9 RID: 25769 RVA: 0x0020E4C4 File Offset: 0x0020C6C4
	private void UpdateCoverScale()
	{
		float num;
		if (TransformUtil.IsExtraWideAspectRatio())
		{
			num = this.m_scaleExtraWide_XZ;
		}
		else
		{
			num = TransformUtil.GetAspectRatioDependentValue(this.m_scale3to2_XZ, this.m_scale16to9_XZ, this.m_scale16to9_XZ);
		}
		base.transform.localScale = new Vector3(num, this.m_scaleY, num);
	}

	// Token: 0x060064AA RID: 25770 RVA: 0x0020E511 File Offset: 0x0020C711
	public static bool OnNavigateBack()
	{
		if (GeneralStorePhoneCover.s_instance == null)
		{
			return false;
		}
		GeneralStorePhoneCover.s_instance.m_parentStore.SetMode(GeneralStoreMode.NONE);
		return true;
	}

	// Token: 0x040053A5 RID: 21413
	[CustomEditField(Sections = "General UI")]
	public GeneralStore m_parentStore;

	// Token: 0x040053A6 RID: 21414
	[CustomEditField(Sections = "General UI")]
	public PegUIElement m_backToCoverButton;

	// Token: 0x040053A7 RID: 21415
	[CustomEditField(Sections = "Animation")]
	public Animator m_animationController;

	// Token: 0x040053A8 RID: 21416
	[CustomEditField(Sections = "Animation")]
	public string m_buttonEnterAnimation = "";

	// Token: 0x040053A9 RID: 21417
	[CustomEditField(Sections = "Animation")]
	public List<GeneralStorePhoneCover.ModeAnimation> m_buttonExitAnimations = new List<GeneralStorePhoneCover.ModeAnimation>();

	// Token: 0x040053AA RID: 21418
	[CustomEditField(Sections = "UI Blockers")]
	public GameObject m_coverClickArea;

	// Token: 0x040053AB RID: 21419
	[CustomEditField(Sections = "UI Blockers")]
	public GameObject m_animationClickBlocker;

	// Token: 0x040053AC RID: 21420
	[CustomEditField(Sections = "Aspect Ratio Scaling")]
	public float m_scale3to2_XZ = 0.39f;

	// Token: 0x040053AD RID: 21421
	[CustomEditField(Sections = "Aspect Ratio Scaling")]
	public float m_scale16to9_XZ = 0.37f;

	// Token: 0x040053AE RID: 21422
	[CustomEditField(Sections = "Aspect Ratio Scaling")]
	public float m_scaleExtraWide_XZ = 0.79f;

	// Token: 0x040053AF RID: 21423
	[CustomEditField(Sections = "Aspect Ratio Scaling")]
	public float m_scaleY = 0.35f;

	// Token: 0x040053B0 RID: 21424
	private static GeneralStorePhoneCover s_instance;

	// Token: 0x040053B1 RID: 21425
	private const string s_coverAnimationCoroutine = "PlayAndWaitForAnimation";

	// Token: 0x02002296 RID: 8854
	// (Invoke) Token: 0x060127BD RID: 75709
	public delegate void AnimationCallback();

	// Token: 0x02002297 RID: 8855
	[Serializable]
	public class ModeAnimation
	{
		// Token: 0x0400E40E RID: 58382
		public GeneralStoreMode m_mode;

		// Token: 0x0400E40F RID: 58383
		public string m_playAnimationName;
	}
}
