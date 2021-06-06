using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AEC RID: 2796
[CustomEditClass]
public class BasicPopup : DialogBase
{
	// Token: 0x060094B2 RID: 38066 RVA: 0x00302DAC File Offset: 0x00300FAC
	protected override void Awake()
	{
		base.Awake();
		if (this.m_cancelButton != null)
		{
			this.m_cancelButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.ButtonPress(BasicPopup.Response.CANCEL);
			});
		}
		if (this.m_customButton != null)
		{
			this.m_customButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.ButtonPress(BasicPopup.Response.CUSTOM_RESPONSE);
			});
		}
	}

	// Token: 0x060094B3 RID: 38067 RVA: 0x00302E0D File Offset: 0x0030100D
	public override bool HandleKeyboardInput()
	{
		if (InputCollection.GetKeyUp(KeyCode.Escape))
		{
			this.GoBack();
			return true;
		}
		return false;
	}

	// Token: 0x060094B4 RID: 38068 RVA: 0x00302E21 File Offset: 0x00301021
	public override void GoBack()
	{
		this.ButtonPress(BasicPopup.Response.CANCEL);
	}

	// Token: 0x060094B5 RID: 38069 RVA: 0x00302E2A File Offset: 0x0030102A
	public void SetInfo(BasicPopup.PopupInfo info)
	{
		this.m_popupInfo = info;
	}

	// Token: 0x060094B6 RID: 38070 RVA: 0x00302E34 File Offset: 0x00301034
	public override void Show()
	{
		base.Show();
		this.InitInfo();
		if (this.m_popupInfo.m_disableBnetBar)
		{
			BnetBar.Get().DisableButtonsByDialog(this);
		}
		if (this.m_popupInfo.m_blurWhenShown)
		{
			DialogBase.DoBlur();
		}
		this.DoShowAnimation();
		if (!string.IsNullOrEmpty(this.m_showAnimationSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_showAnimationSound);
		}
	}

	// Token: 0x060094B7 RID: 38071 RVA: 0x00302E9F File Offset: 0x0030109F
	public override void Hide()
	{
		base.Hide();
		if (this.m_popupInfo.m_blurWhenShown)
		{
			DialogBase.EndBlur();
		}
	}

	// Token: 0x060094B8 RID: 38072 RVA: 0x00302EB9 File Offset: 0x003010B9
	protected override void OnHideAnimFinished()
	{
		UniversalInputManager.Get().SetSystemDialogActive(false);
		base.OnHideAnimFinished();
		if (!string.IsNullOrEmpty(this.m_hideAnimationSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_hideAnimationSound);
		}
	}

	// Token: 0x060094B9 RID: 38073 RVA: 0x00302EF0 File Offset: 0x003010F0
	private void InitInfo()
	{
		if (this.m_popupInfo == null)
		{
			this.m_popupInfo = new BasicPopup.PopupInfo();
		}
		if (this.m_headerText != null && this.m_popupInfo.m_headerText != null)
		{
			this.m_headerText.Text = this.m_popupInfo.m_headerText;
		}
		if (this.m_bodyText != null && this.m_popupInfo.m_bodyText != null)
		{
			this.m_bodyText.Text = this.m_popupInfo.m_bodyText;
		}
	}

	// Token: 0x060094BA RID: 38074 RVA: 0x00302F72 File Offset: 0x00301172
	private void ButtonPress(BasicPopup.Response response)
	{
		if (this.m_popupInfo.m_responseCallback != null)
		{
			this.m_popupInfo.m_responseCallback(response, this.m_popupInfo.m_responseUserData);
		}
		this.Hide();
	}

	// Token: 0x04007CA7 RID: 31911
	public UIBButton m_cancelButton;

	// Token: 0x04007CA8 RID: 31912
	public UIBButton m_customButton;

	// Token: 0x04007CA9 RID: 31913
	public UberText m_headerText;

	// Token: 0x04007CAA RID: 31914
	public UberText m_bodyText;

	// Token: 0x04007CAB RID: 31915
	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_showAnimationSound = "Expand_Up.prefab:775d97ea42498c044897f396362b9db3";

	// Token: 0x04007CAC RID: 31916
	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_hideAnimationSound = "Shrink_Down_Quicker.prefab:2fe963b171811ca4b8d544fa53e3330c";

	// Token: 0x04007CAD RID: 31917
	protected BasicPopup.PopupInfo m_popupInfo;

	// Token: 0x02002722 RID: 10018
	public enum Response
	{
		// Token: 0x0400F381 RID: 62337
		CANCEL,
		// Token: 0x0400F382 RID: 62338
		CUSTOM_RESPONSE
	}

	// Token: 0x02002723 RID: 10019
	// (Invoke) Token: 0x060138FE RID: 80126
	public delegate void ResponseCallback(BasicPopup.Response response, object userData);

	// Token: 0x02002724 RID: 10020
	public class PopupInfo
	{
		// Token: 0x0400F383 RID: 62339
		public readonly List<string> m_prefabAssetRefs = new List<string>();

		// Token: 0x0400F384 RID: 62340
		public BasicPopup.ResponseCallback m_responseCallback;

		// Token: 0x0400F385 RID: 62341
		public object m_responseUserData;

		// Token: 0x0400F386 RID: 62342
		public string m_headerText;

		// Token: 0x0400F387 RID: 62343
		public string m_bodyText;

		// Token: 0x0400F388 RID: 62344
		public bool m_disableBnetBar;

		// Token: 0x0400F389 RID: 62345
		public bool m_blurWhenShown;
	}
}
