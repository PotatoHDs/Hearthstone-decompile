using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000A32 RID: 2610
public class FullScreenFXMgrCallbacks : MonoBehaviour
{
	// Token: 0x170007EA RID: 2026
	// (get) Token: 0x06008C7A RID: 35962 RVA: 0x002CF45F File Offset: 0x002CD65F
	public FullScreenEffects ScreenEffect
	{
		get
		{
			if (this.m_screenEffect == null && FullScreenFXMgr.Get() != null)
			{
				return FullScreenFXMgr.Get().ActiveCameraFullScreenEffects;
			}
			return this.m_screenEffect;
		}
	}

	// Token: 0x06008C7B RID: 35963 RVA: 0x002CF487 File Offset: 0x002CD687
	public void SetEffectsComponent(FullScreenEffects screenEffect)
	{
		this.m_screenEffect = screenEffect;
	}

	// Token: 0x06008C7C RID: 35964 RVA: 0x002CF490 File Offset: 0x002CD690
	public void BeginEffect(string name, string onUpdate, string onComplete, float start, float end, float time, iTween.EaseType easeType)
	{
		Log.FullScreenFX.Print(string.Concat(new object[]
		{
			"BeginEffect ",
			name,
			" ",
			start,
			" => ",
			end
		}), Array.Empty<object>());
		Hashtable hashtable = new Hashtable();
		hashtable["name"] = name;
		hashtable["onupdate"] = onUpdate;
		hashtable["onupdatetarget"] = base.gameObject;
		hashtable["from"] = start;
		if (!string.IsNullOrEmpty(onComplete))
		{
			hashtable["oncomplete"] = onComplete;
			hashtable["oncompletetarget"] = base.gameObject;
		}
		hashtable["to"] = end;
		hashtable["time"] = time;
		hashtable["easetype"] = easeType;
		iTween.StopByName(base.gameObject, name);
		iTween.ValueTo(base.gameObject, hashtable);
	}

	// Token: 0x06008C7D RID: 35965 RVA: 0x002CF59B File Offset: 0x002CD79B
	public void OnVignette(float val)
	{
		if (this.ScreenEffect == null)
		{
			return;
		}
		this.ScreenEffect.VignettingIntensity = val;
	}

	// Token: 0x06008C7E RID: 35966 RVA: 0x002CF5B8 File Offset: 0x002CD7B8
	public void OnVignetteComplete()
	{
		Action vignetteComplete = this.VignetteComplete;
		if (vignetteComplete == null)
		{
			return;
		}
		vignetteComplete();
	}

	// Token: 0x06008C7F RID: 35967 RVA: 0x002CF5CA File Offset: 0x002CD7CA
	public void OnVignetteClear()
	{
		if (this.ScreenEffect == null)
		{
			return;
		}
		this.ScreenEffect.VignettingEnable = false;
		this.OnVignetteComplete();
	}

	// Token: 0x06008C80 RID: 35968 RVA: 0x002CF5ED File Offset: 0x002CD7ED
	public void OnDesat(float val)
	{
		if (this.ScreenEffect == null)
		{
			return;
		}
		this.ScreenEffect.Desaturation = val;
	}

	// Token: 0x06008C81 RID: 35969 RVA: 0x002CF60A File Offset: 0x002CD80A
	public void OnDesatComplete()
	{
		Action desatComplete = this.DesatComplete;
		if (desatComplete == null)
		{
			return;
		}
		desatComplete();
	}

	// Token: 0x06008C82 RID: 35970 RVA: 0x002CF61C File Offset: 0x002CD81C
	public void OnDesatClear()
	{
		if (this.ScreenEffect == null)
		{
			return;
		}
		this.ScreenEffect.DesaturationEnabled = false;
		this.OnDesatComplete();
	}

	// Token: 0x06008C83 RID: 35971 RVA: 0x002CF63F File Offset: 0x002CD83F
	public void OnBlur(float val)
	{
		if (this.ScreenEffect == null)
		{
			return;
		}
		this.ScreenEffect.BlurBlend = val;
	}

	// Token: 0x06008C84 RID: 35972 RVA: 0x002CF65C File Offset: 0x002CD85C
	public void OnBlurComplete()
	{
		Action blurComplete = this.BlurComplete;
		if (blurComplete == null)
		{
			return;
		}
		blurComplete();
	}

	// Token: 0x06008C85 RID: 35973 RVA: 0x002CF66E File Offset: 0x002CD86E
	public void OnBlurClear()
	{
		if (this.ScreenEffect == null)
		{
			return;
		}
		this.ScreenEffect.BlurEnabled = false;
		this.OnBlurComplete();
	}

	// Token: 0x06008C86 RID: 35974 RVA: 0x002CF691 File Offset: 0x002CD891
	public void OnBlendToColor(float val)
	{
		if (this.ScreenEffect == null)
		{
			return;
		}
		this.ScreenEffect.BlendToColorAmount = val;
	}

	// Token: 0x06008C87 RID: 35975 RVA: 0x002CF6AE File Offset: 0x002CD8AE
	public void OnBlendToColorComplete()
	{
		Action blendToColorComplete = this.BlendToColorComplete;
		if (blendToColorComplete == null)
		{
			return;
		}
		blendToColorComplete();
	}

	// Token: 0x06008C88 RID: 35976 RVA: 0x002CF6C0 File Offset: 0x002CD8C0
	public void OnBlendToColorClear()
	{
		if (this.ScreenEffect == null)
		{
			return;
		}
		this.ScreenEffect.BlendToColorEnable = false;
		this.OnBlendToColorComplete();
	}

	// Token: 0x06008C89 RID: 35977 RVA: 0x002CF6E3 File Offset: 0x002CD8E3
	private void OnDestroy()
	{
		this.m_screenEffect = null;
	}

	// Token: 0x0400753B RID: 30011
	private FullScreenEffects m_screenEffect;

	// Token: 0x0400753C RID: 30012
	public Action VignetteComplete;

	// Token: 0x0400753D RID: 30013
	public Action BlurComplete;

	// Token: 0x0400753E RID: 30014
	public Action DesatComplete;

	// Token: 0x0400753F RID: 30015
	public Action BlendToColorComplete;
}
