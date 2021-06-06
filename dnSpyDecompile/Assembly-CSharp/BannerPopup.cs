using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000860 RID: 2144
[CustomEditClass]
public class BannerPopup : MonoBehaviour
{
	// Token: 0x060073B6 RID: 29622 RVA: 0x00028167 File Offset: 0x00026367
	private void Awake()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x060073B7 RID: 29623 RVA: 0x002522E9 File Offset: 0x002504E9
	private void Start()
	{
		if (this.m_ShowSpell == null)
		{
			this.OnShowSpellFinished(null, null);
			return;
		}
		this.m_ShowSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnShowSpellFinished));
		this.m_ShowSpell.Activate();
	}

	// Token: 0x060073B8 RID: 29624 RVA: 0x00252324 File Offset: 0x00250524
	private void OnDestroy()
	{
		if (!this.m_onCloseCallbackCalled)
		{
			this.m_onCloseCallbackCalled = true;
			if (this.m_onCloseBannerPopup != null)
			{
				this.m_onCloseBannerPopup();
			}
		}
	}

	// Token: 0x060073B9 RID: 29625 RVA: 0x00252348 File Offset: 0x00250548
	public void Show(string headerText, string bannerText, BannerManager.DelOnCloseBanner onCloseCallback = null)
	{
		OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
		if (this.m_header != null && headerText != null)
		{
			this.m_header.Text = headerText;
		}
		if (this.m_text != null && bannerText != null)
		{
			this.m_text.Text = bannerText;
		}
		this.m_onCloseBannerPopup = onCloseCallback;
		base.gameObject.SetActive(true);
		Animation animation = (this.m_root == null) ? null : this.m_root.GetComponent<Animation>();
		if (animation != null)
		{
			animation.Play();
		}
		if (!string.IsNullOrEmpty(this.m_showSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_showSound);
		}
		GameObject gameObject = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(base.gameObject.layer), "ClosedSignInputBlocker", this);
		SceneUtils.SetLayer(gameObject, base.gameObject.layer, null);
		this.m_inputBlocker = gameObject.AddComponent<PegUIElement>();
		iTween.ScaleFrom(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			new Vector3(0.01f, 0.01f, 0.01f),
			"time",
			0.25f,
			"oncompletetarget",
			base.gameObject,
			"oncomplete",
			"EnableClickHandler"
		}));
		this.FadeEffectsIn();
		if (this.m_dismissButton != null)
		{
			this.m_dismissButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.CloseBannerPopup));
		}
		this.m_showSpellComplete = false;
	}

	// Token: 0x060073BA RID: 29626 RVA: 0x002524EC File Offset: 0x002506EC
	private void FadeEffectsIn()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.SetBlurBrightness(1f);
		fullScreenFXMgr.SetBlurDesaturation(0f);
		fullScreenFXMgr.Vignette(0.4f, 0.2f, iTween.EaseType.easeOutCirc, null, null);
		fullScreenFXMgr.Blur(1f, 0.2f, iTween.EaseType.easeOutCirc, null);
	}

	// Token: 0x060073BB RID: 29627 RVA: 0x001A2E08 File Offset: 0x001A1008
	private void FadeEffectsOut()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.StopVignette();
		fullScreenFXMgr.StopBlur();
	}

	// Token: 0x060073BC RID: 29628 RVA: 0x0025253A File Offset: 0x0025073A
	private void CloseBannerPopup(UIEvent e)
	{
		this.m_inputBlocker.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.CloseBannerPopup));
		this.Close();
	}

	// Token: 0x060073BD RID: 29629 RVA: 0x0025255C File Offset: 0x0025075C
	public void Close()
	{
		this.FadeEffectsOut();
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			Vector3.zero,
			"time",
			0.5f,
			"oncompletetarget",
			base.gameObject,
			"oncomplete",
			"DestroyBannerPopup"
		}));
		SoundManager.Get().LoadAndPlay("new_quest_click_and_shrink.prefab:601ba6676276eab43947e38f110f7b99");
		ParticleSystem[] componentsInChildren = base.gameObject.GetComponentsInChildren<ParticleSystem>();
		if (componentsInChildren != null)
		{
			ParticleSystem[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].gameObject.SetActive(false);
			}
		}
		if (this.m_LoopingSpell != null)
		{
			this.m_LoopingSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnLoopingSpellFinished));
			this.m_LoopingSpell.ActivateState(SpellStateType.DEATH);
			return;
		}
		if (this.m_HideSpell != null)
		{
			this.m_HideSpell.Activate();
		}
	}

	// Token: 0x060073BE RID: 29630 RVA: 0x0025265C File Offset: 0x0025085C
	private void EnableClickHandler()
	{
		if (this.m_dismissButton == null)
		{
			this.m_inputBlocker.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.CloseBannerPopup));
		}
	}

	// Token: 0x060073BF RID: 29631 RVA: 0x00252685 File Offset: 0x00250885
	private void DestroyBannerPopup()
	{
		this.m_onCloseCallbackCalled = true;
		if (this.m_onCloseBannerPopup != null)
		{
			this.m_onCloseBannerPopup();
		}
		base.StartCoroutine(this.DestroyPopupObject());
	}

	// Token: 0x060073C0 RID: 29632 RVA: 0x002526AE File Offset: 0x002508AE
	private IEnumerator DestroyPopupObject()
	{
		while (!this.m_showSpellComplete)
		{
			yield return null;
		}
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x060073C1 RID: 29633 RVA: 0x002526BD File Offset: 0x002508BD
	private void OnShowSpellFinished(Spell spell, object userData)
	{
		this.m_showSpellComplete = true;
		if (this.m_LoopingSpell == null)
		{
			this.OnLoopingSpellFinished(null, null);
			return;
		}
		this.m_LoopingSpell.ActivateState(SpellStateType.ACTION);
	}

	// Token: 0x060073C2 RID: 29634 RVA: 0x002526E9 File Offset: 0x002508E9
	private void OnLoopingSpellFinished(Spell spell, object userData)
	{
		if (this.m_HideSpell != null)
		{
			this.m_HideSpell.Activate();
		}
	}

	// Token: 0x04005BEF RID: 23535
	public GameObject m_root;

	// Token: 0x04005BF0 RID: 23536
	public UberText m_header;

	// Token: 0x04005BF1 RID: 23537
	public UberText m_text;

	// Token: 0x04005BF2 RID: 23538
	public UIBButton m_dismissButton;

	// Token: 0x04005BF3 RID: 23539
	public Spell m_ShowSpell;

	// Token: 0x04005BF4 RID: 23540
	public Spell m_LoopingSpell;

	// Token: 0x04005BF5 RID: 23541
	public Spell m_HideSpell;

	// Token: 0x04005BF6 RID: 23542
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_showSound;

	// Token: 0x04005BF7 RID: 23543
	private BannerManager.DelOnCloseBanner m_onCloseBannerPopup;

	// Token: 0x04005BF8 RID: 23544
	private PegUIElement m_inputBlocker;

	// Token: 0x04005BF9 RID: 23545
	private bool m_showSpellComplete = true;

	// Token: 0x04005BFA RID: 23546
	private bool m_onCloseCallbackCalled;
}
