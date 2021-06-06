using System;
using UnityEngine;

public class CustomVisualReward : MonoBehaviour
{
	private Action m_callback;

	public virtual void Start()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr == null)
		{
			Debug.LogWarning("CustomVisualReward: FullScreenFXMgr.Get() returned null!");
			return;
		}
		fullScreenFXMgr.SetBlurBrightness(0.85f);
		fullScreenFXMgr.SetBlurDesaturation(0f);
		fullScreenFXMgr.Vignette(0.4f, 0.5f, iTween.EaseType.easeOutCirc);
		fullScreenFXMgr.Blur(1f, 0.5f, iTween.EaseType.easeOutCirc);
	}

	public void SetCompleteCallback(Action c)
	{
		m_callback = c;
	}

	public void Complete()
	{
		if (m_callback != null)
		{
			m_callback();
		}
		FullScreenFXMgr.Get().StopBlur();
		FullScreenFXMgr.Get().StopVignette();
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
