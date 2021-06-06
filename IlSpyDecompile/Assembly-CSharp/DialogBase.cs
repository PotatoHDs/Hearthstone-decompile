using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBase : MonoBehaviour
{
	public delegate void HideCallback(DialogBase dialog, object userData);

	public delegate void ReadyToDestroyCallback(DialogBase dialog);

	protected class HideListener : EventListener<HideCallback>
	{
		public void Fire(DialogBase dialog)
		{
			m_callback(dialog, m_userData);
		}
	}

	protected enum ShowAnimState
	{
		NOT_CALLED,
		IN_PROGRESS,
		FINISHED
	}

	protected readonly Vector3 START_SCALE = 0.01f * Vector3.one;

	protected Vector3 PUNCH_SCALE = 1.2f * Vector3.one;

	protected ShowAnimState m_showAnimState;

	protected bool m_shown;

	protected Vector3 m_originalPosition;

	protected Vector3 m_originalScale;

	protected ReadyToDestroyCallback m_readyToDestroyCallback;

	private List<HideListener> m_hideListeners = new List<HideListener>();

	private List<HideListener> m_hiddenOrDestroyedListeners = new List<HideListener>();

	private bool m_hiddenOrDestroyedListenersFired;

	protected virtual CanvasScaleMode ScaleMode()
	{
		return CanvasScaleMode.HEIGHT;
	}

	protected virtual void Awake()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			PUNCH_SCALE = 1.08f * Vector3.one;
		}
		if (OverlayUI.Get() != null)
		{
			OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.CENTER, destroyOnSceneLoad: false, ScaleMode());
		}
		m_originalPosition = base.transform.position;
		m_originalScale = base.transform.localScale;
		SetHiddenPosition();
	}

	protected virtual void OnDestroy()
	{
		if (!m_hiddenOrDestroyedListenersFired)
		{
			FireHiddenOrDestroyedListeners();
		}
	}

	public virtual bool HandleKeyboardInput()
	{
		return false;
	}

	public virtual void GoBack()
	{
	}

	public virtual void Show()
	{
		m_shown = true;
		SetShownPosition();
	}

	public virtual void Hide()
	{
		m_shown = false;
		StartCoroutine(HideWhenAble());
	}

	public virtual bool IsShown()
	{
		return m_shown;
	}

	public void AddHideListener(HideCallback callback)
	{
		AddHideListener(callback, null);
	}

	public void AddHideListener(HideCallback callback, object userData)
	{
		HideListener hideListener = new HideListener();
		hideListener.SetCallback(callback);
		hideListener.SetUserData(userData);
		if (!m_hideListeners.Contains(hideListener))
		{
			m_hideListeners.Add(hideListener);
		}
	}

	public void AddHiddenOrDestroyedListener(HideCallback callback)
	{
		AddHiddenOrDestroyedListener(callback, null);
	}

	public void AddHiddenOrDestroyedListener(HideCallback callback, object userData)
	{
		HideListener hideListener = new HideListener();
		hideListener.SetCallback(callback);
		hideListener.SetUserData(userData);
		if (!m_hiddenOrDestroyedListeners.Contains(hideListener))
		{
			m_hiddenOrDestroyedListeners.Add(hideListener);
		}
	}

	public void SetReadyToDestroyCallback(ReadyToDestroyCallback callback)
	{
		m_readyToDestroyCallback = callback;
	}

	protected void SetShownPosition()
	{
		base.transform.position = m_originalPosition;
	}

	protected void SetHiddenPosition(Camera referenceCamera = null)
	{
		if (referenceCamera == null)
		{
			referenceCamera = PegUI.Get().orthographicUICam;
		}
		base.transform.position = referenceCamera.transform.TransformPoint(0f, 0f, -1000f);
	}

	protected virtual void DoShowAnimation()
	{
		m_showAnimState = ShowAnimState.IN_PROGRESS;
		AnimationUtil.ShowWithPunch(base.gameObject, START_SCALE, Vector3.Scale(PUNCH_SCALE, m_originalScale), m_originalScale, "OnShowAnimFinished");
	}

	protected virtual void DoHideAnimation()
	{
		AnimationUtil.ScaleFade(base.gameObject, START_SCALE, "OnHideAnimFinished");
	}

	protected virtual void OnHideAnimFinished()
	{
		SetHiddenPosition();
		UniversalInputManager.Get().SetSystemDialogActive(active: false);
		FireHideListeners();
		FireHiddenOrDestroyedListeners();
		if (m_readyToDestroyCallback != null)
		{
			m_readyToDestroyCallback(this);
		}
	}

	private void FireHideListeners()
	{
		foreach (HideListener hideListener in m_hideListeners)
		{
			hideListener.Fire(this);
		}
	}

	private void FireHiddenOrDestroyedListeners()
	{
		foreach (HideListener hiddenOrDestroyedListener in m_hiddenOrDestroyedListeners)
		{
			hiddenOrDestroyedListener.Fire(this);
		}
		m_hiddenOrDestroyedListenersFired = true;
	}

	protected virtual void OnShowAnimFinished()
	{
		m_showAnimState = ShowAnimState.FINISHED;
	}

	private IEnumerator HideWhenAble()
	{
		while (m_showAnimState == ShowAnimState.IN_PROGRESS)
		{
			yield return null;
		}
		DoHideAnimation();
	}

	public static void DoBlur()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.SetBlurBrightness(1f);
		fullScreenFXMgr.SetBlurDesaturation(0f);
		fullScreenFXMgr.Vignette();
		fullScreenFXMgr.Blur(1f, 0.4f, iTween.EaseType.easeOutCirc);
	}

	public static void EndBlur()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.StopVignette();
		fullScreenFXMgr.StopBlur();
	}
}
