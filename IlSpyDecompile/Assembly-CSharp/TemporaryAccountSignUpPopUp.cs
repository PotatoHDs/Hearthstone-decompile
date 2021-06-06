using UnityEngine;

public class TemporaryAccountSignUpPopUp : UIBPopup
{
	public delegate void OnSignUpPopUpBack();

	public struct PopupTextParameters
	{
		public string Header { get; set; }

		public string Body { get; set; }

		public string SignUpButton { get; set; }

		public string CancelButton { get; set; }
	}

	public UIBButton m_backButton;

	public UIBButton m_signUpButton;

	public UberText m_headlineText;

	public UberText m_messageText;

	public UberText m_backButtonText;

	public UberText m_signUpButtonText;

	public GameObject m_inputBlockerRenderer;

	private static readonly Vector3 SHOW_POS_PHONE = new Vector3(0f, 15f, -2f);

	private static readonly Vector3 SHOW_SCALE_PHONE = new Vector3(85f, 85f, 85f);

	private static readonly PopupTextParameters DEFAULT_STRINGS = new PopupTextParameters
	{
		Header = "GLUE_TEMPORARY_ACCOUNT_DIALOG_HEADER_01",
		Body = "GLUE_TEMPORARY_ACCOUNT_DIALOG_BODY_01",
		CancelButton = "GLOBAL_BACK",
		SignUpButton = "GLUE_TEMPORARY_ACCOUNT_SIGN_UP"
	};

	private PegUIElement m_inputBlockerPegUIElement;

	private OnSignUpPopUpBack m_signUpPopUpBackHandler;

	protected override void Awake()
	{
		base.Awake();
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_showPosition = SHOW_POS_PHONE;
			m_showScale = SHOW_SCALE_PHONE;
		}
		m_backButton.AddEventListener(UIEventType.RELEASE, OnBackPressed);
		m_signUpButton.AddEventListener(UIEventType.RELEASE, OnSignUpPressed);
	}

	private void OnDestroy()
	{
		m_signUpPopUpBackHandler = null;
		Hide(animate: false);
	}

	public void Show(PopupTextParameters textArgs, OnSignUpPopUpBack onSignUpPopUpBack)
	{
		SetTextStrings(textArgs);
		m_signUpPopUpBackHandler = onSignUpPopUpBack;
		Show();
	}

	private void SetTextStrings(PopupTextParameters textArgs)
	{
		m_headlineText.Text = ((!string.IsNullOrEmpty(textArgs.Header)) ? textArgs.Header : DEFAULT_STRINGS.Header);
		m_messageText.Text = ((!string.IsNullOrEmpty(textArgs.Body)) ? textArgs.Body : DEFAULT_STRINGS.Body);
		m_backButtonText.Text = ((!string.IsNullOrEmpty(textArgs.CancelButton)) ? textArgs.CancelButton : DEFAULT_STRINGS.CancelButton);
		m_signUpButtonText.Text = ((!string.IsNullOrEmpty(textArgs.SignUpButton)) ? textArgs.SignUpButton : DEFAULT_STRINGS.SignUpButton);
	}

	public override void Show()
	{
		if (!base.IsShown())
		{
			Navigation.Push(OnNavigateBack);
			base.Show(useOverlayUI: true);
			base.gameObject.SetActive(value: true);
			if (m_inputBlockerPegUIElement != null)
			{
				Object.Destroy(m_inputBlockerPegUIElement.gameObject);
				m_inputBlockerPegUIElement = null;
			}
			GameObject gameObject = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(base.gameObject.layer), "TemporaryAccountSignUpPopUpInputBlocker");
			SceneUtils.SetLayer(gameObject, base.gameObject.layer);
			m_inputBlockerPegUIElement = gameObject.AddComponent<PegUIElement>();
			m_inputBlockerPegUIElement.AddEventListener(UIEventType.RELEASE, OnInputBlockerRelease);
			TransformUtil.SetPosY(m_inputBlockerPegUIElement, base.gameObject.transform.position.y - 5f);
			DarkenInputBlocker(gameObject, 0.5f);
		}
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
			base.gameObject.SetActive(value: false);
			base.Hide(animate);
		}
	}

	private void OnBackPressed(UIEvent e)
	{
		if (m_signUpPopUpBackHandler != null)
		{
			m_signUpPopUpBackHandler();
			m_signUpPopUpBackHandler = null;
		}
		Hide(animate: true);
	}

	private void OnSignUpPressed(UIEvent e)
	{
		Hide(animate: false);
		TemporaryAccountManager.Get().ShowHealUpPage(delegate(bool isResetting)
		{
			if (!isResetting)
			{
				m_signUpPopUpBackHandler?.Invoke();
			}
		});
	}

	private bool OnNavigateBack()
	{
		if (m_signUpPopUpBackHandler != null)
		{
			m_signUpPopUpBackHandler();
			m_signUpPopUpBackHandler = null;
		}
		Hide(animate: true);
		return true;
	}

	private void OnInputBlockerRelease(UIEvent e)
	{
		if (m_signUpPopUpBackHandler != null)
		{
			m_signUpPopUpBackHandler();
			m_signUpPopUpBackHandler = null;
		}
		Hide(animate: true);
	}

	private void DarkenInputBlocker(GameObject inputBlockerObject, float alpha)
	{
		inputBlockerObject.AddComponent<MeshRenderer>().SetMaterial(m_inputBlockerRenderer.GetComponent<MeshRenderer>().GetMaterial());
		inputBlockerObject.AddComponent<MeshFilter>().SetMesh(m_inputBlockerRenderer.GetComponent<MeshFilter>().GetMesh());
		BoxCollider component = inputBlockerObject.GetComponent<BoxCollider>();
		TransformUtil.SetLocalScaleXY(inputBlockerObject, component.size.x, component.size.y);
		component.size = new Vector3(1f, 1f, 0f);
		TransformUtil.SetLocalEulerAngleX(inputBlockerObject, 90f);
		RenderUtils.SetAlpha(inputBlockerObject, alpha);
	}
}
