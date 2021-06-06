using System.Collections;
using UnityEngine;

[CustomEditClass]
public class BannerPopup : MonoBehaviour
{
	public GameObject m_root;

	public UberText m_header;

	public UberText m_text;

	public UIBButton m_dismissButton;

	public Spell m_ShowSpell;

	public Spell m_LoopingSpell;

	public Spell m_HideSpell;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_showSound;

	private BannerManager.DelOnCloseBanner m_onCloseBannerPopup;

	private PegUIElement m_inputBlocker;

	private bool m_showSpellComplete = true;

	private bool m_onCloseCallbackCalled;

	private void Awake()
	{
		base.gameObject.SetActive(value: false);
	}

	private void Start()
	{
		if (m_ShowSpell == null)
		{
			OnShowSpellFinished(null, null);
			return;
		}
		m_ShowSpell.AddFinishedCallback(OnShowSpellFinished);
		m_ShowSpell.Activate();
	}

	private void OnDestroy()
	{
		if (!m_onCloseCallbackCalled)
		{
			m_onCloseCallbackCalled = true;
			if (m_onCloseBannerPopup != null)
			{
				m_onCloseBannerPopup();
			}
		}
	}

	public void Show(string headerText, string bannerText, BannerManager.DelOnCloseBanner onCloseCallback = null)
	{
		OverlayUI.Get().AddGameObject(base.gameObject);
		if (m_header != null && headerText != null)
		{
			m_header.Text = headerText;
		}
		if (m_text != null && bannerText != null)
		{
			m_text.Text = bannerText;
		}
		m_onCloseBannerPopup = onCloseCallback;
		base.gameObject.SetActive(value: true);
		Animation animation = ((m_root == null) ? null : m_root.GetComponent<Animation>());
		if (animation != null)
		{
			animation.Play();
		}
		if (!string.IsNullOrEmpty(m_showSound))
		{
			SoundManager.Get().LoadAndPlay(m_showSound);
		}
		GameObject gameObject = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(base.gameObject.layer), "ClosedSignInputBlocker", this);
		SceneUtils.SetLayer(gameObject, base.gameObject.layer);
		m_inputBlocker = gameObject.AddComponent<PegUIElement>();
		iTween.ScaleFrom(base.gameObject, iTween.Hash("scale", new Vector3(0.01f, 0.01f, 0.01f), "time", 0.25f, "oncompletetarget", base.gameObject, "oncomplete", "EnableClickHandler"));
		FadeEffectsIn();
		if (m_dismissButton != null)
		{
			m_dismissButton.AddEventListener(UIEventType.RELEASE, CloseBannerPopup);
		}
		m_showSpellComplete = false;
	}

	private void FadeEffectsIn()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.SetBlurBrightness(1f);
		fullScreenFXMgr.SetBlurDesaturation(0f);
		fullScreenFXMgr.Vignette(0.4f, 0.2f, iTween.EaseType.easeOutCirc);
		fullScreenFXMgr.Blur(1f, 0.2f, iTween.EaseType.easeOutCirc);
	}

	private void FadeEffectsOut()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.StopVignette();
		fullScreenFXMgr.StopBlur();
	}

	private void CloseBannerPopup(UIEvent e)
	{
		m_inputBlocker.RemoveEventListener(UIEventType.RELEASE, CloseBannerPopup);
		Close();
	}

	public void Close()
	{
		FadeEffectsOut();
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", Vector3.zero, "time", 0.5f, "oncompletetarget", base.gameObject, "oncomplete", "DestroyBannerPopup"));
		SoundManager.Get().LoadAndPlay("new_quest_click_and_shrink.prefab:601ba6676276eab43947e38f110f7b99");
		ParticleSystem[] componentsInChildren = base.gameObject.GetComponentsInChildren<ParticleSystem>();
		if (componentsInChildren != null)
		{
			ParticleSystem[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].gameObject.SetActive(value: false);
			}
		}
		if (m_LoopingSpell != null)
		{
			m_LoopingSpell.AddFinishedCallback(OnLoopingSpellFinished);
			m_LoopingSpell.ActivateState(SpellStateType.DEATH);
		}
		else if (m_HideSpell != null)
		{
			m_HideSpell.Activate();
		}
	}

	private void EnableClickHandler()
	{
		if (m_dismissButton == null)
		{
			m_inputBlocker.AddEventListener(UIEventType.RELEASE, CloseBannerPopup);
		}
	}

	private void DestroyBannerPopup()
	{
		m_onCloseCallbackCalled = true;
		if (m_onCloseBannerPopup != null)
		{
			m_onCloseBannerPopup();
		}
		StartCoroutine(DestroyPopupObject());
	}

	private IEnumerator DestroyPopupObject()
	{
		while (!m_showSpellComplete)
		{
			yield return null;
		}
		Object.Destroy(base.gameObject);
	}

	private void OnShowSpellFinished(Spell spell, object userData)
	{
		m_showSpellComplete = true;
		if (m_LoopingSpell == null)
		{
			OnLoopingSpellFinished(null, null);
		}
		else
		{
			m_LoopingSpell.ActivateState(SpellStateType.ACTION);
		}
	}

	private void OnLoopingSpellFinished(Spell spell, object userData)
	{
		if (m_HideSpell != null)
		{
			m_HideSpell.Activate();
		}
	}
}
