using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000300 RID: 768
public class CardTileListDisplay : MonoBehaviour
{
	// Token: 0x060028DA RID: 10458 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void Awake()
	{
	}

	// Token: 0x060028DB RID: 10459 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void Start()
	{
	}

	// Token: 0x060028DC RID: 10460 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnDestroy()
	{
	}

	// Token: 0x060028DD RID: 10461 RVA: 0x000CF034 File Offset: 0x000CD234
	protected void AnimateVignetteIn()
	{
		FullScreenEffects activeCameraFullScreenEffects = FullScreenFXMgr.Get().ActiveCameraFullScreenEffects;
		this.m_animatingVignette = activeCameraFullScreenEffects.VignettingEnable;
		if (this.m_animatingVignette)
		{
			Hashtable args = iTween.Hash(new object[]
			{
				"from",
				activeCameraFullScreenEffects.VignettingIntensity,
				"to",
				0.6f,
				"time",
				0.4f,
				"easetype",
				iTween.EaseType.easeInOutQuad,
				"onupdate",
				"OnUpdateVignetteVal",
				"onupdatetarget",
				base.gameObject,
				"name",
				"historyVig",
				"oncomplete",
				"OnVignetteInFinished",
				"oncompletetarget",
				base.gameObject
			});
			iTween.StopByName(Camera.main.gameObject, "historyVig");
			iTween.ValueTo(Camera.main.gameObject, args);
		}
		this.m_animatingDesat = activeCameraFullScreenEffects.DesaturationEnabled;
		if (this.m_animatingDesat)
		{
			Hashtable args2 = iTween.Hash(new object[]
			{
				"from",
				activeCameraFullScreenEffects.Desaturation,
				"to",
				1f,
				"time",
				0.4f,
				"easetype",
				iTween.EaseType.easeInOutQuad,
				"onupdate",
				"OnUpdateDesatVal",
				"onupdatetarget",
				base.gameObject,
				"name",
				"historyDesat",
				"oncomplete",
				"OnDesatInFinished",
				"oncompletetarget",
				base.gameObject
			});
			iTween.StopByName(Camera.main.gameObject, "historyDesat");
			iTween.ValueTo(Camera.main.gameObject, args2);
		}
	}

	// Token: 0x060028DE RID: 10462 RVA: 0x000CF234 File Offset: 0x000CD434
	protected void AnimateVignetteOut()
	{
		FullScreenEffects activeCameraFullScreenEffects = FullScreenFXMgr.Get().ActiveCameraFullScreenEffects;
		this.m_animatingVignette = activeCameraFullScreenEffects.VignettingEnable;
		if (this.m_animatingVignette)
		{
			Hashtable args = iTween.Hash(new object[]
			{
				"from",
				activeCameraFullScreenEffects.VignettingIntensity,
				"to",
				0f,
				"time",
				0.4f,
				"easetype",
				iTween.EaseType.easeInOutQuad,
				"onupdate",
				"OnUpdateVignetteVal",
				"onupdatetarget",
				base.gameObject,
				"name",
				"historyVig",
				"oncomplete",
				"OnVignetteOutFinished",
				"oncompletetarget",
				base.gameObject
			});
			iTween.StopByName(Camera.main.gameObject, "historyVig");
			iTween.ValueTo(Camera.main.gameObject, args);
		}
		this.m_animatingDesat = activeCameraFullScreenEffects.DesaturationEnabled;
		if (this.m_animatingDesat)
		{
			Hashtable args2 = iTween.Hash(new object[]
			{
				"from",
				activeCameraFullScreenEffects.Desaturation,
				"to",
				0f,
				"time",
				0.4f,
				"easetype",
				iTween.EaseType.easeInOutQuad,
				"onupdate",
				"OnUpdateDesatVal",
				"onupdatetarget",
				base.gameObject,
				"name",
				"historyDesat",
				"oncomplete",
				"OnDesatOutFinished",
				"oncompletetarget",
				base.gameObject
			});
			iTween.StopByName(Camera.main.gameObject, "historyDesat");
			iTween.ValueTo(Camera.main.gameObject, args2);
		}
	}

	// Token: 0x060028DF RID: 10463 RVA: 0x000CF434 File Offset: 0x000CD634
	protected void AnimateBlurVignetteIn()
	{
		FullScreenEffects activeCameraFullScreenEffects = FullScreenFXMgr.Get().ActiveCameraFullScreenEffects;
		this.m_animatingBlur = activeCameraFullScreenEffects.BlurEnabled;
		if (this.m_animatingBlur)
		{
			Hashtable args = iTween.Hash(new object[]
			{
				"from",
				activeCameraFullScreenEffects.BlurAmount,
				"to",
				0.6f,
				"time",
				0.4f,
				"easetype",
				iTween.EaseType.easeInOutQuad,
				"onupdate",
				"OnUpdateBlurVal",
				"onupdatetarget",
				base.gameObject,
				"name",
				"historyBlur",
				"oncomplete",
				"OnBlurInFinished",
				"oncompletetarget",
				base.gameObject
			});
			iTween.StopByName(Camera.main.gameObject, "historyBlur");
			iTween.ValueTo(Camera.main.gameObject, args);
		}
		this.m_animatingVignette = activeCameraFullScreenEffects.VignettingEnable;
		if (this.m_animatingVignette)
		{
			Hashtable args2 = iTween.Hash(new object[]
			{
				"from",
				activeCameraFullScreenEffects.VignettingIntensity,
				"to",
				0.6f,
				"time",
				0.4f,
				"easetype",
				iTween.EaseType.easeInOutQuad,
				"onupdate",
				"OnUpdateVignetteVal",
				"onupdatetarget",
				base.gameObject,
				"name",
				"historyVig",
				"oncomplete",
				"OnVignetteInFinished",
				"oncompletetarget",
				base.gameObject
			});
			iTween.StopByName(Camera.main.gameObject, "historyVig");
			iTween.ValueTo(Camera.main.gameObject, args2);
		}
		this.m_animatingDesat = activeCameraFullScreenEffects.BlurEnabled;
		if (this.m_animatingDesat)
		{
			Hashtable args3 = iTween.Hash(new object[]
			{
				"from",
				activeCameraFullScreenEffects.BlurDesaturation,
				"to",
				1f,
				"time",
				0.4f,
				"easetype",
				iTween.EaseType.easeInOutQuad,
				"onupdate",
				"OnUpdateBlurDesatVal",
				"onupdatetarget",
				base.gameObject,
				"name",
				"historyBlurDesat",
				"oncomplete",
				"OnBlurDesatInFinished",
				"oncompletetarget",
				base.gameObject
			});
			iTween.StopByName(Camera.main.gameObject, "historyBlurDesat");
			iTween.ValueTo(Camera.main.gameObject, args3);
		}
	}

	// Token: 0x060028E0 RID: 10464 RVA: 0x000CF728 File Offset: 0x000CD928
	protected void AnimateBlurVignetteOut()
	{
		FullScreenEffects activeCameraFullScreenEffects = FullScreenFXMgr.Get().ActiveCameraFullScreenEffects;
		this.m_animatingBlur = activeCameraFullScreenEffects.BlurEnabled;
		if (this.m_animatingBlur)
		{
			Hashtable args = iTween.Hash(new object[]
			{
				"from",
				activeCameraFullScreenEffects.BlurAmount,
				"to",
				0f,
				"time",
				0.1f,
				"easetype",
				iTween.EaseType.easeInOutQuad,
				"onupdate",
				"OnUpdateBlurVal",
				"onupdatetarget",
				base.gameObject,
				"name",
				"historyBlur",
				"oncomplete",
				"OnBlurOutFinished",
				"oncompletetarget",
				base.gameObject
			});
			iTween.StopByName(Camera.main.gameObject, "historyBlur");
			iTween.ValueTo(Camera.main.gameObject, args);
		}
		this.m_animatingVignette = activeCameraFullScreenEffects.VignettingEnable;
		if (this.m_animatingVignette)
		{
			Hashtable args2 = iTween.Hash(new object[]
			{
				"from",
				activeCameraFullScreenEffects.VignettingIntensity,
				"to",
				0f,
				"time",
				0.1f,
				"easetype",
				iTween.EaseType.easeInOutQuad,
				"onupdate",
				"OnUpdateVignetteVal",
				"onupdatetarget",
				base.gameObject,
				"name",
				"historyVig",
				"oncomplete",
				"OnVignetteOutFinished",
				"oncompletetarget",
				base.gameObject
			});
			iTween.StopByName(Camera.main.gameObject, "historyVig");
			iTween.ValueTo(Camera.main.gameObject, args2);
		}
		this.m_animatingDesat = activeCameraFullScreenEffects.BlurEnabled;
		if (this.m_animatingDesat)
		{
			Hashtable args3 = iTween.Hash(new object[]
			{
				"from",
				activeCameraFullScreenEffects.BlurDesaturation,
				"to",
				0f,
				"time",
				0.1f,
				"easetype",
				iTween.EaseType.easeInOutQuad,
				"onupdate",
				"OnUpdateBlurDesatVal",
				"onupdatetarget",
				base.gameObject,
				"name",
				"historyBlurDesat",
				"oncomplete",
				"OnBlurDesatOutFinished",
				"oncompletetarget",
				base.gameObject
			});
			iTween.StopByName(Camera.main.gameObject, "historyBlurDesat");
			iTween.ValueTo(Camera.main.gameObject, args3);
		}
	}

	// Token: 0x060028E1 RID: 10465 RVA: 0x000CFA1C File Offset: 0x000CDC1C
	private void OnUpdateVignetteVal(float val)
	{
		FullScreenFXMgr.Get().ActiveCameraFullScreenEffects.VignettingIntensity = val;
	}

	// Token: 0x060028E2 RID: 10466 RVA: 0x000CFA2E File Offset: 0x000CDC2E
	private void OnUpdateDesatVal(float val)
	{
		FullScreenFXMgr.Get().ActiveCameraFullScreenEffects.Desaturation = val;
	}

	// Token: 0x060028E3 RID: 10467 RVA: 0x000CFA40 File Offset: 0x000CDC40
	private void OnUpdateBlurDesatVal(float val)
	{
		FullScreenFXMgr.Get().ActiveCameraFullScreenEffects.BlurDesaturation = val;
	}

	// Token: 0x060028E4 RID: 10468 RVA: 0x000CFA52 File Offset: 0x000CDC52
	private void OnVignetteInFinished()
	{
		this.m_animatingVignette = false;
	}

	// Token: 0x060028E5 RID: 10469 RVA: 0x000CFA5B File Offset: 0x000CDC5B
	private void OnDesatInFinished()
	{
		this.m_animatingDesat = false;
	}

	// Token: 0x060028E6 RID: 10470 RVA: 0x000CFA5B File Offset: 0x000CDC5B
	private void OnBlurDesatInFinished()
	{
		this.m_animatingDesat = false;
	}

	// Token: 0x060028E7 RID: 10471 RVA: 0x000CFA64 File Offset: 0x000CDC64
	private void OnVignetteOutFinished()
	{
		this.m_animatingVignette = false;
		FullScreenFXMgr.Get().ActiveCameraFullScreenEffects.VignettingEnable = false;
		this.OnFullScreenEffectOutFinished();
	}

	// Token: 0x060028E8 RID: 10472 RVA: 0x000CFA83 File Offset: 0x000CDC83
	private void OnDesatOutFinished()
	{
		this.m_animatingDesat = false;
		FullScreenFXMgr.Get().ActiveCameraFullScreenEffects.DesaturationEnabled = false;
		this.OnFullScreenEffectOutFinished();
	}

	// Token: 0x060028E9 RID: 10473 RVA: 0x000CFAA2 File Offset: 0x000CDCA2
	private void OnBlurDesatOutFinished()
	{
		this.m_animatingDesat = false;
		this.OnFullScreenEffectOutFinished();
	}

	// Token: 0x060028EA RID: 10474 RVA: 0x000CFAB1 File Offset: 0x000CDCB1
	private void OnUpdateBlurVal(float val)
	{
		FullScreenFXMgr.Get().ActiveCameraFullScreenEffects.BlurAmount = val;
	}

	// Token: 0x060028EB RID: 10475 RVA: 0x000CFAC3 File Offset: 0x000CDCC3
	private void OnBlurInFinished()
	{
		this.m_animatingBlur = false;
	}

	// Token: 0x060028EC RID: 10476 RVA: 0x000CFACC File Offset: 0x000CDCCC
	private void OnBlurOutFinished()
	{
		this.m_animatingBlur = false;
		FullScreenFXMgr.Get().ActiveCameraFullScreenEffects.BlurEnabled = false;
		this.OnFullScreenEffectOutFinished();
	}

	// Token: 0x060028ED RID: 10477 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnFullScreenEffectOutFinished()
	{
	}

	// Token: 0x0400174F RID: 5967
	public SoundDucker m_SoundDucker;

	// Token: 0x04001750 RID: 5968
	protected bool m_animatingVignette;

	// Token: 0x04001751 RID: 5969
	protected bool m_animatingDesat;

	// Token: 0x04001752 RID: 5970
	protected bool m_animatingBlur;
}
