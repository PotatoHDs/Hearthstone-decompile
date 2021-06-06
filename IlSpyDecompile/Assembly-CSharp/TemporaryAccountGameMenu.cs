using UnityEngine;

public class TemporaryAccountGameMenu : UIBPopup, GameMenuInterface
{
	public UIBButton m_signUpButton;

	public UIBButton m_optionsButton;

	public GameObject m_koreanRatings;

	private static readonly Vector3 SHOW_POS_PHONE = new Vector3(0f, 0f, 0f);

	private static readonly Vector3 SHOW_SCALE_PHONE = new Vector3(75f, 75f, 75f);

	private static readonly Vector3 SHOW_POS_PHONE_KR = new Vector3(0f, 0f, 15f);

	private static readonly Vector3 SHOW_SCALE_PHONE_KR = new Vector3(65f, 65f, 65f);

	private GameMenuBase m_gameMenuBase;

	private PegUIElement m_inputBlockerPegUIElement;

	protected override void Awake()
	{
		base.Awake();
		m_destroyOnSceneLoad = false;
		m_gameMenuBase = new GameMenuBase();
		m_gameMenuBase.m_showCallback = Show;
		m_gameMenuBase.m_hideCallback = Hide;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_showPosition = (m_gameMenuBase.UseKoreanRating() ? SHOW_POS_PHONE_KR : SHOW_POS_PHONE);
			m_showScale = (m_gameMenuBase.UseKoreanRating() ? SHOW_SCALE_PHONE_KR : SHOW_SCALE_PHONE);
		}
		m_signUpButton.AddEventListener(UIEventType.RELEASE, OnSignUpPressed);
		m_optionsButton.AddEventListener(UIEventType.RELEASE, OnOptionsPressed);
	}

	private void OnDestroy()
	{
		m_gameMenuBase.DestroyOptionsMenu();
	}

	public bool GameMenuIsShown()
	{
		return IsShown();
	}

	public void GameMenuShow()
	{
		Show();
	}

	public void GameMenuHide()
	{
		Hide();
	}

	public void GameMenuShowOptionsMenu()
	{
		ShowOptionsMenu();
	}

	public GameObject GameMenuGetGameObject()
	{
		return base.gameObject;
	}

	public override void Show()
	{
		if (!base.IsShown())
		{
			Navigation.Push(OnNavigateBack);
			base.Show(useOverlayUI: true);
			if (m_gameMenuBase.UseKoreanRating())
			{
				m_koreanRatings.SetActive(value: true);
			}
			base.gameObject.SetActive(value: true);
			if (m_inputBlockerPegUIElement != null)
			{
				Object.Destroy(m_inputBlockerPegUIElement.gameObject);
				m_inputBlockerPegUIElement = null;
			}
			GameObject gameObject = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(base.gameObject.layer), "TemporaryAccountGameMenuInputBlocker");
			SceneUtils.SetLayer(gameObject, base.gameObject.layer);
			m_inputBlockerPegUIElement = gameObject.AddComponent<PegUIElement>();
			m_inputBlockerPegUIElement.AddEventListener(UIEventType.RELEASE, OnInputBlockerRelease);
			TransformUtil.SetPosY(m_inputBlockerPegUIElement, base.gameObject.transform.position.y - 5f);
			BnetBar.Get().m_menuButton.SetSelected(enable: true);
		}
	}

	public override void Hide()
	{
		Hide(animate: false);
		BnetBar.Get().m_menuButton.SetSelected(enable: false);
	}

	protected override void Hide(bool animate)
	{
		if (base.IsShown())
		{
			Navigation.RemoveHandler(OnNavigateBack);
			if (m_inputBlockerPegUIElement != null)
			{
				Object.Destroy(m_inputBlockerPegUIElement.gameObject);
				m_inputBlockerPegUIElement = null;
			}
			if (base.gameObject != null)
			{
				base.gameObject.SetActive(value: false);
			}
			base.Hide(animate);
		}
	}

	public void ShowOptionsMenu()
	{
		if (m_gameMenuBase != null)
		{
			m_gameMenuBase.ShowOptionsMenu();
		}
	}

	private void OnSignUpPressed(UIEvent e)
	{
		Hide();
		TemporaryAccountManager.Get().ShowHealUpPage(TemporaryAccountManager.HealUpReason.GAME_MENU);
	}

	private void OnOptionsPressed(UIEvent e)
	{
		ShowOptionsMenu();
	}

	private bool OnNavigateBack()
	{
		Hide();
		return true;
	}

	private void OnInputBlockerRelease(UIEvent e)
	{
		Hide();
	}
}
