using System;
using System.Collections.Generic;
using Blizzard.T5.AssetManager;
using UnityEngine;

// Token: 0x02000AC8 RID: 2760
[CustomEditClass]
public class AlertPopup : DialogBase
{
	// Token: 0x0600933A RID: 37690 RVA: 0x002FBCFC File Offset: 0x002F9EFC
	protected override void Awake()
	{
		this.m_alertTextInitialWidth = this.m_alertText.Width;
		base.Awake();
		this.m_okayButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.ButtonPress(AlertPopup.Response.OK);
		});
		this.m_confirmButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.ButtonPress(AlertPopup.Response.CONFIRM);
		});
		this.m_cancelButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.ButtonPress(AlertPopup.Response.CANCEL);
		});
	}

	// Token: 0x0600933B RID: 37691 RVA: 0x002FBD6B File Offset: 0x002F9F6B
	private void Start()
	{
		if (string.IsNullOrEmpty(this.m_alertText.Text))
		{
			this.m_alertText.Text = GameStrings.Get("GLOBAL_OKAY");
		}
	}

	// Token: 0x0600933C RID: 37692 RVA: 0x002FBD94 File Offset: 0x002F9F94
	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (UniversalInputManager.Get() != null)
		{
			UniversalInputManager.Get().SetSystemDialogActive(false);
		}
		AssetHandle.SafeDispose<Texture>(ref this.m_loadedIconTexture);
	}

	// Token: 0x0600933D RID: 37693 RVA: 0x002FBDBC File Offset: 0x002F9FBC
	public override bool HandleKeyboardInput()
	{
		if (InputCollection.GetKeyUp(KeyCode.Escape) && this.m_popupInfo != null && this.m_popupInfo.m_keyboardEscIsCancel && this.m_cancelButton.enabled && this.m_cancelButton.gameObject.activeSelf)
		{
			this.GoBack();
			return true;
		}
		return false;
	}

	// Token: 0x0600933E RID: 37694 RVA: 0x002FBE0F File Offset: 0x002FA00F
	public override void GoBack()
	{
		this.ButtonPress(AlertPopup.Response.CANCEL);
	}

	// Token: 0x0600933F RID: 37695 RVA: 0x002FBE18 File Offset: 0x002FA018
	public void SetInfo(AlertPopup.PopupInfo info)
	{
		this.m_popupInfo = info;
	}

	// Token: 0x06009340 RID: 37696 RVA: 0x002FBE21 File Offset: 0x002FA021
	public AlertPopup.PopupInfo GetInfo()
	{
		return this.m_popupInfo;
	}

	// Token: 0x06009341 RID: 37697 RVA: 0x002FBE2C File Offset: 0x002FA02C
	public override void Show()
	{
		base.Show();
		this.InitInfo();
		this.UpdateAll(this.m_popupInfo);
		base.transform.localPosition += this.m_popupInfo.m_offset;
		if (this.m_popupInfo.m_layerToUse != null)
		{
			SceneUtils.SetLayer(this, this.m_popupInfo.m_layerToUse.Value);
		}
		if (this.m_popupInfo.m_disableBlocker)
		{
			this.m_clickCatcher.SetActive(false);
		}
		if (this.m_popupInfo.m_disableBnetBar)
		{
			BnetBar.Get().DisableButtonsByDialog(this);
		}
		if (this.m_popupInfo.m_blurWhenShown)
		{
			DialogBase.DoBlur();
		}
		this.DoShowAnimation();
		bool systemDialogActive = this.m_popupInfo == null || this.m_popupInfo.m_layerToUse == null || this.m_popupInfo.m_layerToUse.Value == GameLayer.UI || this.m_popupInfo.m_layerToUse.Value == GameLayer.HighPriorityUI;
		UniversalInputManager.Get().SetSystemDialogActive(systemDialogActive);
	}

	// Token: 0x06009342 RID: 37698 RVA: 0x002FBF33 File Offset: 0x002FA133
	public override void Hide()
	{
		base.Hide();
		if (this.m_popupInfo.m_blurWhenShown)
		{
			DialogBase.EndBlur();
		}
	}

	// Token: 0x06009343 RID: 37699 RVA: 0x002FBF4D File Offset: 0x002FA14D
	public void UpdateInfo(AlertPopup.PopupInfo info)
	{
		this.m_updateInfo = info;
		this.UpdateButtons(this.m_updateInfo.m_responseDisplay);
		if (this.m_showAnimState != DialogBase.ShowAnimState.IN_PROGRESS)
		{
			this.UpdateInfoAfterAnim();
		}
	}

	// Token: 0x17000870 RID: 2160
	// (get) Token: 0x06009344 RID: 37700 RVA: 0x002FBF76 File Offset: 0x002FA176
	// (set) Token: 0x06009345 RID: 37701 RVA: 0x002FBF83 File Offset: 0x002FA183
	public string BodyText
	{
		get
		{
			return this.m_alertText.Text;
		}
		set
		{
			this.m_alertText.Text = value;
			if (this.m_popupInfo != null)
			{
				this.UpdateLayout();
			}
		}
	}

	// Token: 0x06009346 RID: 37702 RVA: 0x002FBF9F File Offset: 0x002FA19F
	protected override void OnHideAnimFinished()
	{
		UniversalInputManager.Get().SetSystemDialogActive(false);
		base.OnHideAnimFinished();
	}

	// Token: 0x06009347 RID: 37703 RVA: 0x002FBFB2 File Offset: 0x002FA1B2
	protected override void OnShowAnimFinished()
	{
		base.OnShowAnimFinished();
		if (this.m_updateInfo != null)
		{
			this.UpdateInfoAfterAnim();
		}
	}

	// Token: 0x06009348 RID: 37704 RVA: 0x002FBFC8 File Offset: 0x002FA1C8
	private void InitInfo()
	{
		if (this.m_popupInfo == null)
		{
			this.m_popupInfo = new AlertPopup.PopupInfo();
		}
		if (this.m_popupInfo.m_headerText == null)
		{
			this.m_popupInfo.m_headerText = GameStrings.Get("GLOBAL_DEFAULT_ALERT_HEADER");
		}
	}

	// Token: 0x06009349 RID: 37705 RVA: 0x002FC000 File Offset: 0x002FA200
	private void UpdateButtons(AlertPopup.ResponseDisplay displayType)
	{
		this.m_confirmButton.gameObject.SetActive(false);
		this.m_cancelButton.gameObject.SetActive(false);
		this.m_okayButton.gameObject.SetActive(false);
		switch (displayType)
		{
		case AlertPopup.ResponseDisplay.OK:
			this.m_okayButton.gameObject.SetActive(true);
			break;
		case AlertPopup.ResponseDisplay.CONFIRM:
			this.m_confirmButton.gameObject.SetActive(true);
			break;
		case AlertPopup.ResponseDisplay.CANCEL:
			this.m_cancelButton.gameObject.SetActive(true);
			break;
		case AlertPopup.ResponseDisplay.CONFIRM_CANCEL:
			this.m_confirmButton.gameObject.SetActive(true);
			this.m_cancelButton.gameObject.SetActive(true);
			break;
		}
		this.m_buttonContainer.UpdateSlices();
	}

	// Token: 0x0600934A RID: 37706 RVA: 0x002FC0C0 File Offset: 0x002FA2C0
	private void UpdateTexts(AlertPopup.PopupInfo popupInfo)
	{
		this.m_alertText.RichText = this.m_popupInfo.m_richTextEnabled;
		this.m_alertText.Alignment = this.m_popupInfo.m_alertTextAlignment;
		this.m_alertText.Anchor = this.m_popupInfo.m_alertTextAlignmentAnchor;
		if (popupInfo.m_headerText == null)
		{
			popupInfo.m_headerText = GameStrings.Get("GLOBAL_DEFAULT_ALERT_HEADER");
		}
		this.m_alertText.Text = popupInfo.m_text;
		this.m_okayButton.SetText((popupInfo.m_okText == null) ? GameStrings.Get("GLOBAL_OKAY") : popupInfo.m_okText);
		this.m_confirmButton.SetText((popupInfo.m_confirmText == null) ? GameStrings.Get("GLOBAL_CONFIRM") : popupInfo.m_confirmText);
		this.m_cancelButton.SetText((popupInfo.m_cancelText == null) ? GameStrings.Get("GLOBAL_CANCEL") : popupInfo.m_cancelText);
	}

	// Token: 0x0600934B RID: 37707 RVA: 0x002FC1A8 File Offset: 0x002FA3A8
	private void UpdateIcons(AlertPopup.PopupInfo popupInfo)
	{
		this.m_alertIcon.SetActive(popupInfo.m_showAlertIcon);
		if (AssetLoader.Get() != null && !string.IsNullOrEmpty(popupInfo.m_iconTexture))
		{
			MeshRenderer component = this.m_alertIcon.GetComponent<MeshRenderer>();
			if (component != null)
			{
				AssetLoader.Get().LoadAsset<Texture>(ref this.m_loadedIconTexture, popupInfo.m_iconTexture, AssetLoadingOptions.None);
				if (this.m_loadedIconTexture != null)
				{
					component.GetMaterial().SetTexture("_MainTex", this.m_loadedIconTexture);
				}
			}
		}
		bool active = popupInfo.m_iconSet == AlertPopup.PopupInfo.IconSet.Default;
		bool active2 = popupInfo.m_iconSet == AlertPopup.PopupInfo.IconSet.Alternate;
		for (int i = 0; i < this.m_buttonIconsSet1.Count; i++)
		{
			this.m_buttonIconsSet1[i].SetActive(active);
		}
		for (int j = 0; j < this.m_buttonIconsSet2.Count; j++)
		{
			this.m_buttonIconsSet2[j].SetActive(active2);
		}
	}

	// Token: 0x0600934C RID: 37708 RVA: 0x002FC298 File Offset: 0x002FA498
	private void UpdateInfoAfterAnim()
	{
		this.m_popupInfo = this.m_updateInfo;
		this.m_updateInfo = null;
		this.UpdateAll(this.m_popupInfo);
	}

	// Token: 0x0600934D RID: 37709 RVA: 0x002FC2B9 File Offset: 0x002FA4B9
	private void UpdateAll(AlertPopup.PopupInfo popupInfo)
	{
		this.UpdateIcons(popupInfo);
		this.UpdateHeaderText(popupInfo.m_headerText);
		this.UpdateTexts(popupInfo);
		this.UpdateLayout();
	}

	// Token: 0x0600934E RID: 37710 RVA: 0x002FC2DC File Offset: 0x002FA4DC
	private void UpdateLayout()
	{
		bool activeSelf = this.m_alertIcon.activeSelf;
		Bounds textBounds = this.m_alertText.GetTextBounds();
		float num = textBounds.size.x;
		float a = textBounds.size.y + this.m_padding + this.m_popupInfo.m_padding;
		float num2 = 0f;
		float b = 0f;
		if (activeSelf)
		{
			OrientedBounds orientedBounds = TransformUtil.ComputeOrientedWorldBounds(this.m_alertIcon, true);
			num2 = orientedBounds.Extents[0].magnitude * 2f;
			b = orientedBounds.Extents[1].magnitude * 2f;
		}
		this.UpdateButtons(this.m_popupInfo.m_responseDisplay);
		num = Mathf.Max(TransformUtil.GetBoundsOfChildren(this.m_confirmButton).size.x * 2f, num);
		this.m_body.SetSize(num + num2, Mathf.Max(a, b));
		Vector3 offset = new Vector3(0f, 0.01f, 0f);
		TransformUtil.SetPoint(this.m_alertIcon, Anchor.TOP_LEFT_XZ, this.m_body.m_middle, Anchor.TOP_LEFT_XZ, offset);
		this.m_alertIcon.transform.localPosition += this.m_alertIconOffset;
		Anchor anchor = Anchor.TOP_LEFT_XZ;
		if (this.m_popupInfo.m_alertTextAlignment == UberText.AlignmentOptions.Center)
		{
			anchor = Anchor.TOP_XZ;
			if (this.m_popupInfo.m_showAlertIcon)
			{
				anchor = Anchor.TOP_LEFT_XZ;
			}
		}
		if (this.m_alertText.Anchor == UberText.AnchorOptions.Middle)
		{
			switch (anchor)
			{
			case Anchor.TOP_LEFT_XZ:
				anchor = Anchor.LEFT_XZ;
				break;
			case Anchor.TOP_XZ:
				anchor = Anchor.CENTER_XZ;
				break;
			case Anchor.TOP_RIGHT_XZ:
				anchor = Anchor.RIGHT_XZ;
				break;
			}
		}
		TransformUtil.SetPoint(this.m_alertText, anchor, this.m_body.m_middle, anchor, offset);
		Vector3 position = this.m_alertText.transform.position;
		position.x += num2 + this.m_alertIconOffset.x;
		position.y += 1f;
		this.m_alertText.transform.position = position;
		if (this.m_popupInfo.m_alertTextAlignment == UberText.AlignmentOptions.Center)
		{
			this.m_alertText.Width = this.m_alertTextInitialWidth - num2 * this.m_alertText.transform.localScale.x;
		}
		this.m_header.m_container.transform.position = this.m_body.m_top.m_slice.transform.position;
		this.m_buttonContainer.transform.position = this.m_body.m_bottom.m_slice.transform.position;
		if (this.m_popupInfo.m_scaleOverride != null)
		{
			this.m_originalScale = this.m_popupInfo.m_scaleOverride.Value;
		}
	}

	// Token: 0x0600934F RID: 37711 RVA: 0x002FC5A0 File Offset: 0x002FA7A0
	private void ButtonPress(AlertPopup.Response response)
	{
		if (this.m_wasPressed)
		{
			return;
		}
		this.m_wasPressed = true;
		if (this.m_popupInfo.m_responseCallback != null)
		{
			this.m_popupInfo.m_responseCallback(response, this.m_popupInfo.m_responseUserData);
		}
		this.Hide();
	}

	// Token: 0x06009350 RID: 37712 RVA: 0x002FC5EC File Offset: 0x002FA7EC
	private void UpdateHeaderText(string text)
	{
		bool flag = string.IsNullOrEmpty(text);
		this.m_header.m_container.gameObject.SetActive(!flag);
		if (flag)
		{
			return;
		}
		this.m_header.m_text.ResizeToFit = false;
		this.m_header.m_text.Text = text;
		this.m_header.m_text.UpdateNow(false);
		MeshRenderer component = this.m_body.m_middle.m_slice.GetComponent<MeshRenderer>();
		float x = this.m_header.m_text.GetTextBounds().size.x;
		float x2 = this.m_header.m_text.transform.worldToLocalMatrix.MultiplyVector(this.m_header.m_text.GetTextBounds().size).x;
		float num = 0.8f * this.m_header.m_text.transform.worldToLocalMatrix.MultiplyVector(component.GetComponent<Renderer>().bounds.size).x;
		if (x2 > num)
		{
			this.m_header.m_text.Width = num;
			this.m_header.m_text.ResizeToFit = true;
			this.m_header.m_text.UpdateNow(false);
			x = this.m_header.m_text.GetTextBounds().size.x;
		}
		else
		{
			this.m_header.m_text.Width = x2;
		}
		TransformUtil.SetLocalScaleToWorldDimension(this.m_header.m_middle, new WorldDimensionIndex[]
		{
			new WorldDimensionIndex(x, 0)
		});
		this.m_header.m_container.UpdateSlices();
	}

	// Token: 0x04007B64 RID: 31588
	public AlertPopup.Header m_header;

	// Token: 0x04007B65 RID: 31589
	public NineSliceElement m_body;

	// Token: 0x04007B66 RID: 31590
	public GameObject m_alertIcon;

	// Token: 0x04007B67 RID: 31591
	public MultiSliceElement m_buttonContainer;

	// Token: 0x04007B68 RID: 31592
	public UIBButton m_okayButton;

	// Token: 0x04007B69 RID: 31593
	public UIBButton m_confirmButton;

	// Token: 0x04007B6A RID: 31594
	public UIBButton m_cancelButton;

	// Token: 0x04007B6B RID: 31595
	public GameObject m_clickCatcher;

	// Token: 0x04007B6C RID: 31596
	public UberText m_alertText;

	// Token: 0x04007B6D RID: 31597
	public Vector3 m_alertIconOffset;

	// Token: 0x04007B6E RID: 31598
	public float m_padding;

	// Token: 0x04007B6F RID: 31599
	public Vector3 m_loadPosition;

	// Token: 0x04007B70 RID: 31600
	public Vector3 m_showPosition;

	// Token: 0x04007B71 RID: 31601
	public List<GameObject> m_buttonIconsSet1 = new List<GameObject>();

	// Token: 0x04007B72 RID: 31602
	public List<GameObject> m_buttonIconsSet2 = new List<GameObject>();

	// Token: 0x04007B73 RID: 31603
	private const float BUTTON_FRAME_WIDTH = 80f;

	// Token: 0x04007B74 RID: 31604
	private AlertPopup.PopupInfo m_popupInfo;

	// Token: 0x04007B75 RID: 31605
	private AlertPopup.PopupInfo m_updateInfo;

	// Token: 0x04007B76 RID: 31606
	private float m_alertTextInitialWidth;

	// Token: 0x04007B77 RID: 31607
	private bool m_wasPressed;

	// Token: 0x04007B78 RID: 31608
	private AssetHandle<Texture> m_loadedIconTexture;

	// Token: 0x020026F7 RID: 9975
	[Serializable]
	public class Header
	{
		// Token: 0x0400F2CF RID: 62159
		public MultiSliceElement m_container;

		// Token: 0x0400F2D0 RID: 62160
		public GameObject m_middle;

		// Token: 0x0400F2D1 RID: 62161
		public UberText m_text;
	}

	// Token: 0x020026F8 RID: 9976
	public enum Response
	{
		// Token: 0x0400F2D3 RID: 62163
		OK,
		// Token: 0x0400F2D4 RID: 62164
		CONFIRM,
		// Token: 0x0400F2D5 RID: 62165
		CANCEL
	}

	// Token: 0x020026F9 RID: 9977
	public enum ResponseDisplay
	{
		// Token: 0x0400F2D7 RID: 62167
		NONE,
		// Token: 0x0400F2D8 RID: 62168
		OK,
		// Token: 0x0400F2D9 RID: 62169
		CONFIRM,
		// Token: 0x0400F2DA RID: 62170
		CANCEL,
		// Token: 0x0400F2DB RID: 62171
		CONFIRM_CANCEL
	}

	// Token: 0x020026FA RID: 9978
	// (Invoke) Token: 0x060138B3 RID: 80051
	public delegate void ResponseCallback(AlertPopup.Response response, object userData);

	// Token: 0x020026FB RID: 9979
	public class PopupInfo
	{
		// Token: 0x0400F2DC RID: 62172
		public UserAttentionBlocker m_attentionCategory = UserAttentionBlocker.ALL_EXCEPT_FATAL_ERROR_SCENE;

		// Token: 0x0400F2DD RID: 62173
		public string m_id;

		// Token: 0x0400F2DE RID: 62174
		public string m_headerText;

		// Token: 0x0400F2DF RID: 62175
		public string m_text;

		// Token: 0x0400F2E0 RID: 62176
		public string m_okText;

		// Token: 0x0400F2E1 RID: 62177
		public string m_confirmText;

		// Token: 0x0400F2E2 RID: 62178
		public string m_cancelText;

		// Token: 0x0400F2E3 RID: 62179
		public bool m_showAlertIcon = true;

		// Token: 0x0400F2E4 RID: 62180
		public AssetReference m_iconTexture;

		// Token: 0x0400F2E5 RID: 62181
		public AlertPopup.ResponseDisplay m_responseDisplay = AlertPopup.ResponseDisplay.OK;

		// Token: 0x0400F2E6 RID: 62182
		public AlertPopup.ResponseCallback m_responseCallback;

		// Token: 0x0400F2E7 RID: 62183
		public object m_responseUserData;

		// Token: 0x0400F2E8 RID: 62184
		public Vector3 m_offset = Vector3.zero;

		// Token: 0x0400F2E9 RID: 62185
		public float m_padding;

		// Token: 0x0400F2EA RID: 62186
		public Vector3? m_scaleOverride;

		// Token: 0x0400F2EB RID: 62187
		public bool m_richTextEnabled = true;

		// Token: 0x0400F2EC RID: 62188
		public bool m_disableBlocker;

		// Token: 0x0400F2ED RID: 62189
		public AlertPopup.PopupInfo.IconSet m_iconSet;

		// Token: 0x0400F2EE RID: 62190
		public UberText.AlignmentOptions m_alertTextAlignment;

		// Token: 0x0400F2EF RID: 62191
		public UberText.AnchorOptions m_alertTextAlignmentAnchor;

		// Token: 0x0400F2F0 RID: 62192
		public GameLayer? m_layerToUse;

		// Token: 0x0400F2F1 RID: 62193
		public bool m_keyboardEscIsCancel = true;

		// Token: 0x0400F2F2 RID: 62194
		public bool m_disableBnetBar;

		// Token: 0x0400F2F3 RID: 62195
		public bool m_blurWhenShown;

		// Token: 0x020029A7 RID: 10663
		public enum IconSet
		{
			// Token: 0x0400FE06 RID: 65030
			Default,
			// Token: 0x0400FE07 RID: 65031
			Alternate,
			// Token: 0x0400FE08 RID: 65032
			None
		}
	}
}
