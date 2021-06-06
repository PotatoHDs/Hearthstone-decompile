using System;
using UnityEngine;

// Token: 0x0200088A RID: 2186
public class CustomVisualReward : MonoBehaviour
{
	// Token: 0x0600776B RID: 30571 RVA: 0x0026FF08 File Offset: 0x0026E108
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
		fullScreenFXMgr.Vignette(0.4f, 0.5f, iTween.EaseType.easeOutCirc, null, null);
		fullScreenFXMgr.Blur(1f, 0.5f, iTween.EaseType.easeOutCirc, null);
	}

	// Token: 0x0600776C RID: 30572 RVA: 0x0026FF66 File Offset: 0x0026E166
	public void SetCompleteCallback(Action c)
	{
		this.m_callback = c;
	}

	// Token: 0x0600776D RID: 30573 RVA: 0x0026FF6F File Offset: 0x0026E16F
	public void Complete()
	{
		if (this.m_callback != null)
		{
			this.m_callback();
		}
		FullScreenFXMgr.Get().StopBlur();
		FullScreenFXMgr.Get().StopVignette();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04005D93 RID: 23955
	private Action m_callback;
}
