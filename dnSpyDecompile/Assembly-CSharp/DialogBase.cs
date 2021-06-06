using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B0B RID: 2827
public class DialogBase : MonoBehaviour
{
	// Token: 0x0600964C RID: 38476 RVA: 0x000052EC File Offset: 0x000034EC
	protected virtual CanvasScaleMode ScaleMode()
	{
		return CanvasScaleMode.HEIGHT;
	}

	// Token: 0x0600964D RID: 38477 RVA: 0x0030ABBC File Offset: 0x00308DBC
	protected virtual void Awake()
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			this.PUNCH_SCALE = 1.08f * Vector3.one;
		}
		if (OverlayUI.Get() != null)
		{
			OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.CENTER, false, this.ScaleMode());
		}
		this.m_originalPosition = base.transform.position;
		this.m_originalScale = base.transform.localScale;
		this.SetHiddenPosition(null);
	}

	// Token: 0x0600964E RID: 38478 RVA: 0x0030AC38 File Offset: 0x00308E38
	protected virtual void OnDestroy()
	{
		if (!this.m_hiddenOrDestroyedListenersFired)
		{
			this.FireHiddenOrDestroyedListeners();
		}
	}

	// Token: 0x0600964F RID: 38479 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public virtual bool HandleKeyboardInput()
	{
		return false;
	}

	// Token: 0x06009650 RID: 38480 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void GoBack()
	{
	}

	// Token: 0x06009651 RID: 38481 RVA: 0x0030AC48 File Offset: 0x00308E48
	public virtual void Show()
	{
		this.m_shown = true;
		this.SetShownPosition();
	}

	// Token: 0x06009652 RID: 38482 RVA: 0x0030AC57 File Offset: 0x00308E57
	public virtual void Hide()
	{
		this.m_shown = false;
		base.StartCoroutine(this.HideWhenAble());
	}

	// Token: 0x06009653 RID: 38483 RVA: 0x0030AC6D File Offset: 0x00308E6D
	public virtual bool IsShown()
	{
		return this.m_shown;
	}

	// Token: 0x06009654 RID: 38484 RVA: 0x0030AC75 File Offset: 0x00308E75
	public void AddHideListener(DialogBase.HideCallback callback)
	{
		this.AddHideListener(callback, null);
	}

	// Token: 0x06009655 RID: 38485 RVA: 0x0030AC80 File Offset: 0x00308E80
	public void AddHideListener(DialogBase.HideCallback callback, object userData)
	{
		DialogBase.HideListener hideListener = new DialogBase.HideListener();
		hideListener.SetCallback(callback);
		hideListener.SetUserData(userData);
		if (!this.m_hideListeners.Contains(hideListener))
		{
			this.m_hideListeners.Add(hideListener);
		}
	}

	// Token: 0x06009656 RID: 38486 RVA: 0x0030ACBB File Offset: 0x00308EBB
	public void AddHiddenOrDestroyedListener(DialogBase.HideCallback callback)
	{
		this.AddHiddenOrDestroyedListener(callback, null);
	}

	// Token: 0x06009657 RID: 38487 RVA: 0x0030ACC8 File Offset: 0x00308EC8
	public void AddHiddenOrDestroyedListener(DialogBase.HideCallback callback, object userData)
	{
		DialogBase.HideListener hideListener = new DialogBase.HideListener();
		hideListener.SetCallback(callback);
		hideListener.SetUserData(userData);
		if (!this.m_hiddenOrDestroyedListeners.Contains(hideListener))
		{
			this.m_hiddenOrDestroyedListeners.Add(hideListener);
		}
	}

	// Token: 0x06009658 RID: 38488 RVA: 0x0030AD03 File Offset: 0x00308F03
	public void SetReadyToDestroyCallback(DialogBase.ReadyToDestroyCallback callback)
	{
		this.m_readyToDestroyCallback = callback;
	}

	// Token: 0x06009659 RID: 38489 RVA: 0x0030AD0C File Offset: 0x00308F0C
	protected void SetShownPosition()
	{
		base.transform.position = this.m_originalPosition;
	}

	// Token: 0x0600965A RID: 38490 RVA: 0x0030AD1F File Offset: 0x00308F1F
	protected void SetHiddenPosition(Camera referenceCamera = null)
	{
		if (referenceCamera == null)
		{
			referenceCamera = PegUI.Get().orthographicUICam;
		}
		base.transform.position = referenceCamera.transform.TransformPoint(0f, 0f, -1000f);
	}

	// Token: 0x0600965B RID: 38491 RVA: 0x0030AD5C File Offset: 0x00308F5C
	protected virtual void DoShowAnimation()
	{
		this.m_showAnimState = DialogBase.ShowAnimState.IN_PROGRESS;
		AnimationUtil.ShowWithPunch(base.gameObject, this.START_SCALE, Vector3.Scale(this.PUNCH_SCALE, this.m_originalScale), this.m_originalScale, "OnShowAnimFinished", false, null, null, null);
	}

	// Token: 0x0600965C RID: 38492 RVA: 0x0030ADA1 File Offset: 0x00308FA1
	protected virtual void DoHideAnimation()
	{
		AnimationUtil.ScaleFade(base.gameObject, this.START_SCALE, "OnHideAnimFinished");
	}

	// Token: 0x0600965D RID: 38493 RVA: 0x0030ADB9 File Offset: 0x00308FB9
	protected virtual void OnHideAnimFinished()
	{
		this.SetHiddenPosition(null);
		UniversalInputManager.Get().SetSystemDialogActive(false);
		this.FireHideListeners();
		this.FireHiddenOrDestroyedListeners();
		if (this.m_readyToDestroyCallback != null)
		{
			this.m_readyToDestroyCallback(this);
		}
	}

	// Token: 0x0600965E RID: 38494 RVA: 0x0030ADF0 File Offset: 0x00308FF0
	private void FireHideListeners()
	{
		foreach (DialogBase.HideListener hideListener in this.m_hideListeners)
		{
			hideListener.Fire(this);
		}
	}

	// Token: 0x0600965F RID: 38495 RVA: 0x0030AE44 File Offset: 0x00309044
	private void FireHiddenOrDestroyedListeners()
	{
		foreach (DialogBase.HideListener hideListener in this.m_hiddenOrDestroyedListeners)
		{
			hideListener.Fire(this);
		}
		this.m_hiddenOrDestroyedListenersFired = true;
	}

	// Token: 0x06009660 RID: 38496 RVA: 0x0030AE9C File Offset: 0x0030909C
	protected virtual void OnShowAnimFinished()
	{
		this.m_showAnimState = DialogBase.ShowAnimState.FINISHED;
	}

	// Token: 0x06009661 RID: 38497 RVA: 0x0030AEA5 File Offset: 0x003090A5
	private IEnumerator HideWhenAble()
	{
		while (this.m_showAnimState == DialogBase.ShowAnimState.IN_PROGRESS)
		{
			yield return null;
		}
		this.DoHideAnimation();
		yield break;
	}

	// Token: 0x06009662 RID: 38498 RVA: 0x0006BF0E File Offset: 0x0006A10E
	public static void DoBlur()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.SetBlurBrightness(1f);
		fullScreenFXMgr.SetBlurDesaturation(0f);
		fullScreenFXMgr.Vignette();
		fullScreenFXMgr.Blur(1f, 0.4f, iTween.EaseType.easeOutCirc, null);
	}

	// Token: 0x06009663 RID: 38499 RVA: 0x001A2E08 File Offset: 0x001A1008
	public static void EndBlur()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.StopVignette();
		fullScreenFXMgr.StopBlur();
	}

	// Token: 0x04007E01 RID: 32257
	protected readonly Vector3 START_SCALE = 0.01f * Vector3.one;

	// Token: 0x04007E02 RID: 32258
	protected Vector3 PUNCH_SCALE = 1.2f * Vector3.one;

	// Token: 0x04007E03 RID: 32259
	protected DialogBase.ShowAnimState m_showAnimState;

	// Token: 0x04007E04 RID: 32260
	protected bool m_shown;

	// Token: 0x04007E05 RID: 32261
	protected Vector3 m_originalPosition;

	// Token: 0x04007E06 RID: 32262
	protected Vector3 m_originalScale;

	// Token: 0x04007E07 RID: 32263
	protected DialogBase.ReadyToDestroyCallback m_readyToDestroyCallback;

	// Token: 0x04007E08 RID: 32264
	private List<DialogBase.HideListener> m_hideListeners = new List<DialogBase.HideListener>();

	// Token: 0x04007E09 RID: 32265
	private List<DialogBase.HideListener> m_hiddenOrDestroyedListeners = new List<DialogBase.HideListener>();

	// Token: 0x04007E0A RID: 32266
	private bool m_hiddenOrDestroyedListenersFired;

	// Token: 0x02002753 RID: 10067
	// (Invoke) Token: 0x06013992 RID: 80274
	public delegate void HideCallback(DialogBase dialog, object userData);

	// Token: 0x02002754 RID: 10068
	// (Invoke) Token: 0x06013996 RID: 80278
	public delegate void ReadyToDestroyCallback(DialogBase dialog);

	// Token: 0x02002755 RID: 10069
	protected class HideListener : EventListener<DialogBase.HideCallback>
	{
		// Token: 0x06013999 RID: 80281 RVA: 0x00538676 File Offset: 0x00536876
		public void Fire(DialogBase dialog)
		{
			this.m_callback(dialog, this.m_userData);
		}
	}

	// Token: 0x02002756 RID: 10070
	protected enum ShowAnimState
	{
		// Token: 0x0400F3D7 RID: 62423
		NOT_CALLED,
		// Token: 0x0400F3D8 RID: 62424
		IN_PROGRESS,
		// Token: 0x0400F3D9 RID: 62425
		FINISHED
	}
}
