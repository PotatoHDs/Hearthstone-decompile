using System;
using UnityEngine;

// Token: 0x0200073F RID: 1855
public class TemporaryAccountSignUpPopUp : UIBPopup
{
	// Token: 0x06006923 RID: 26915 RVA: 0x002245DC File Offset: 0x002227DC
	protected override void Awake()
	{
		base.Awake();
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_showPosition = TemporaryAccountSignUpPopUp.SHOW_POS_PHONE;
			this.m_showScale = TemporaryAccountSignUpPopUp.SHOW_SCALE_PHONE;
		}
		this.m_backButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBackPressed));
		this.m_signUpButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnSignUpPressed));
	}

	// Token: 0x06006924 RID: 26916 RVA: 0x00224643 File Offset: 0x00222843
	private void OnDestroy()
	{
		this.m_signUpPopUpBackHandler = null;
		this.Hide(false);
	}

	// Token: 0x06006925 RID: 26917 RVA: 0x00224653 File Offset: 0x00222853
	public void Show(TemporaryAccountSignUpPopUp.PopupTextParameters textArgs, TemporaryAccountSignUpPopUp.OnSignUpPopUpBack onSignUpPopUpBack)
	{
		this.SetTextStrings(textArgs);
		this.m_signUpPopUpBackHandler = onSignUpPopUpBack;
		this.Show();
	}

	// Token: 0x06006926 RID: 26918 RVA: 0x0022466C File Offset: 0x0022286C
	private void SetTextStrings(TemporaryAccountSignUpPopUp.PopupTextParameters textArgs)
	{
		this.m_headlineText.Text = ((!string.IsNullOrEmpty(textArgs.Header)) ? textArgs.Header : TemporaryAccountSignUpPopUp.DEFAULT_STRINGS.Header);
		this.m_messageText.Text = ((!string.IsNullOrEmpty(textArgs.Body)) ? textArgs.Body : TemporaryAccountSignUpPopUp.DEFAULT_STRINGS.Body);
		this.m_backButtonText.Text = ((!string.IsNullOrEmpty(textArgs.CancelButton)) ? textArgs.CancelButton : TemporaryAccountSignUpPopUp.DEFAULT_STRINGS.CancelButton);
		this.m_signUpButtonText.Text = ((!string.IsNullOrEmpty(textArgs.SignUpButton)) ? textArgs.SignUpButton : TemporaryAccountSignUpPopUp.DEFAULT_STRINGS.SignUpButton);
	}

	// Token: 0x06006927 RID: 26919 RVA: 0x00224738 File Offset: 0x00222938
	public override void Show()
	{
		if (base.IsShown())
		{
			return;
		}
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		base.Show(true);
		base.gameObject.SetActive(true);
		if (this.m_inputBlockerPegUIElement != null)
		{
			UnityEngine.Object.Destroy(this.m_inputBlockerPegUIElement.gameObject);
			this.m_inputBlockerPegUIElement = null;
		}
		GameObject gameObject = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(base.gameObject.layer), "TemporaryAccountSignUpPopUpInputBlocker");
		SceneUtils.SetLayer(gameObject, base.gameObject.layer, null);
		this.m_inputBlockerPegUIElement = gameObject.AddComponent<PegUIElement>();
		this.m_inputBlockerPegUIElement.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnInputBlockerRelease));
		TransformUtil.SetPosY(this.m_inputBlockerPegUIElement, base.gameObject.transform.position.y - 5f);
		this.DarkenInputBlocker(gameObject, 0.5f);
	}

	// Token: 0x06006928 RID: 26920 RVA: 0x00224824 File Offset: 0x00222A24
	protected override void Hide(bool animate)
	{
		if (!base.IsShown())
		{
			return;
		}
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		if (this.m_inputBlockerPegUIElement != null)
		{
			UnityEngine.Object.Destroy(this.m_inputBlockerPegUIElement.gameObject);
			this.m_inputBlockerPegUIElement = null;
		}
		base.gameObject.SetActive(false);
		base.Hide(animate);
	}

	// Token: 0x06006929 RID: 26921 RVA: 0x00224884 File Offset: 0x00222A84
	private void OnBackPressed(UIEvent e)
	{
		if (this.m_signUpPopUpBackHandler != null)
		{
			this.m_signUpPopUpBackHandler();
			this.m_signUpPopUpBackHandler = null;
		}
		this.Hide(true);
	}

	// Token: 0x0600692A RID: 26922 RVA: 0x002248A7 File Offset: 0x00222AA7
	private void OnSignUpPressed(UIEvent e)
	{
		this.Hide(false);
		TemporaryAccountManager.Get().ShowHealUpPage(delegate(bool isResetting)
		{
			if (!isResetting)
			{
				TemporaryAccountSignUpPopUp.OnSignUpPopUpBack signUpPopUpBackHandler = this.m_signUpPopUpBackHandler;
				if (signUpPopUpBackHandler == null)
				{
					return;
				}
				signUpPopUpBackHandler();
			}
		});
	}

	// Token: 0x0600692B RID: 26923 RVA: 0x002248C6 File Offset: 0x00222AC6
	private bool OnNavigateBack()
	{
		if (this.m_signUpPopUpBackHandler != null)
		{
			this.m_signUpPopUpBackHandler();
			this.m_signUpPopUpBackHandler = null;
		}
		this.Hide(true);
		return true;
	}

	// Token: 0x0600692C RID: 26924 RVA: 0x00224884 File Offset: 0x00222A84
	private void OnInputBlockerRelease(UIEvent e)
	{
		if (this.m_signUpPopUpBackHandler != null)
		{
			this.m_signUpPopUpBackHandler();
			this.m_signUpPopUpBackHandler = null;
		}
		this.Hide(true);
	}

	// Token: 0x0600692D RID: 26925 RVA: 0x002248EC File Offset: 0x00222AEC
	private void DarkenInputBlocker(GameObject inputBlockerObject, float alpha)
	{
		inputBlockerObject.AddComponent<MeshRenderer>().SetMaterial(this.m_inputBlockerRenderer.GetComponent<MeshRenderer>().GetMaterial());
		inputBlockerObject.AddComponent<MeshFilter>().SetMesh(this.m_inputBlockerRenderer.GetComponent<MeshFilter>().GetMesh());
		BoxCollider component = inputBlockerObject.GetComponent<BoxCollider>();
		TransformUtil.SetLocalScaleXY(inputBlockerObject, component.size.x, component.size.y);
		component.size = new Vector3(1f, 1f, 0f);
		TransformUtil.SetLocalEulerAngleX(inputBlockerObject, 90f);
		RenderUtils.SetAlpha(inputBlockerObject, alpha);
	}

	// Token: 0x04005605 RID: 22021
	public UIBButton m_backButton;

	// Token: 0x04005606 RID: 22022
	public UIBButton m_signUpButton;

	// Token: 0x04005607 RID: 22023
	public UberText m_headlineText;

	// Token: 0x04005608 RID: 22024
	public UberText m_messageText;

	// Token: 0x04005609 RID: 22025
	public UberText m_backButtonText;

	// Token: 0x0400560A RID: 22026
	public UberText m_signUpButtonText;

	// Token: 0x0400560B RID: 22027
	public GameObject m_inputBlockerRenderer;

	// Token: 0x0400560C RID: 22028
	private static readonly Vector3 SHOW_POS_PHONE = new Vector3(0f, 15f, -2f);

	// Token: 0x0400560D RID: 22029
	private static readonly Vector3 SHOW_SCALE_PHONE = new Vector3(85f, 85f, 85f);

	// Token: 0x0400560E RID: 22030
	private static readonly TemporaryAccountSignUpPopUp.PopupTextParameters DEFAULT_STRINGS = new TemporaryAccountSignUpPopUp.PopupTextParameters
	{
		Header = "GLUE_TEMPORARY_ACCOUNT_DIALOG_HEADER_01",
		Body = "GLUE_TEMPORARY_ACCOUNT_DIALOG_BODY_01",
		CancelButton = "GLOBAL_BACK",
		SignUpButton = "GLUE_TEMPORARY_ACCOUNT_SIGN_UP"
	};

	// Token: 0x0400560F RID: 22031
	private PegUIElement m_inputBlockerPegUIElement;

	// Token: 0x04005610 RID: 22032
	private TemporaryAccountSignUpPopUp.OnSignUpPopUpBack m_signUpPopUpBackHandler;

	// Token: 0x02002318 RID: 8984
	// (Invoke) Token: 0x060129C6 RID: 76230
	public delegate void OnSignUpPopUpBack();

	// Token: 0x02002319 RID: 8985
	public struct PopupTextParameters
	{
		// Token: 0x170029C4 RID: 10692
		// (get) Token: 0x060129C9 RID: 76233 RVA: 0x00511218 File Offset: 0x0050F418
		// (set) Token: 0x060129CA RID: 76234 RVA: 0x00511220 File Offset: 0x0050F420
		public string Header { get; set; }

		// Token: 0x170029C5 RID: 10693
		// (get) Token: 0x060129CB RID: 76235 RVA: 0x00511229 File Offset: 0x0050F429
		// (set) Token: 0x060129CC RID: 76236 RVA: 0x00511231 File Offset: 0x0050F431
		public string Body { get; set; }

		// Token: 0x170029C6 RID: 10694
		// (get) Token: 0x060129CD RID: 76237 RVA: 0x0051123A File Offset: 0x0050F43A
		// (set) Token: 0x060129CE RID: 76238 RVA: 0x00511242 File Offset: 0x0050F442
		public string SignUpButton { get; set; }

		// Token: 0x170029C7 RID: 10695
		// (get) Token: 0x060129CF RID: 76239 RVA: 0x0051124B File Offset: 0x0050F44B
		// (set) Token: 0x060129D0 RID: 76240 RVA: 0x00511253 File Offset: 0x0050F453
		public string CancelButton { get; set; }
	}
}
