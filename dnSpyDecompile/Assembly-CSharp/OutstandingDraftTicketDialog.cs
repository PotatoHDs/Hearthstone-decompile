using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002BE RID: 702
public class OutstandingDraftTicketDialog : DialogBase
{
	// Token: 0x060024DD RID: 9437 RVA: 0x000B97C0 File Offset: 0x000B79C0
	protected override void Awake()
	{
		base.Awake();
		this.m_enterButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.HandleDraftTicketResponse(true);
		});
		this.m_cancelButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.HandleDraftTicketResponse(false);
		});
		this.m_plusSign.SetActive(false);
		base.AddHideListener(new DialogBase.HideCallback(this.OnHideComplete));
	}

	// Token: 0x060024DE RID: 9438 RVA: 0x000B9823 File Offset: 0x000B7A23
	public void SetInfo(OutstandingDraftTicketDialog.Info info)
	{
		this.m_info = info;
	}

	// Token: 0x060024DF RID: 9439 RVA: 0x000B982C File Offset: 0x000B7A2C
	public override void Show()
	{
		Vector3 localScale = base.transform.localScale;
		base.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		this.EnableFullScreenEffects(true);
		base.Show();
		int outstandingTicketCount = this.m_info.m_outstandingTicketCount;
		bool active;
		if (outstandingTicketCount > 9)
		{
			this.m_ticketCount.SetGameStringText("9");
			active = true;
		}
		else
		{
			this.m_ticketCount.SetGameStringText(outstandingTicketCount.ToString());
			active = false;
		}
		GameStrings.PluralNumber[] pluralNumbers = new GameStrings.PluralNumber[]
		{
			new GameStrings.PluralNumber
			{
				m_index = 0,
				m_number = outstandingTicketCount
			}
		};
		this.m_description.Text = GameStrings.FormatPlurals("GLUE_OUTSTANDING_DRAFT_TICKET_DIALOG_DESC", pluralNumbers, Array.Empty<object>());
		if (this.m_plusSign != null)
		{
			this.m_plusSign.SetActive(active);
		}
		if (!string.IsNullOrEmpty(this.m_showAnimationSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_showAnimationSound);
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			localScale,
			"time",
			0.3f,
			"easetype",
			iTween.EaseType.easeOutBack
		});
		iTween.ScaleTo(base.gameObject, args);
		UniversalInputManager.Get().SetSystemDialogActive(true);
	}

	// Token: 0x060024E0 RID: 9440 RVA: 0x000B9731 File Offset: 0x000B7931
	protected void EnableFullScreenEffects(bool enable)
	{
		if (FullScreenFXMgr.Get() == null)
		{
			return;
		}
		if (enable)
		{
			FullScreenFXMgr.Get().StartStandardBlurVignette(1f);
			return;
		}
		FullScreenFXMgr.Get().EndStandardBlurVignette(1f, null);
	}

	// Token: 0x060024E1 RID: 9441 RVA: 0x000B997D File Offset: 0x000B7B7D
	protected override void DoHideAnimation()
	{
		if (!string.IsNullOrEmpty(this.m_hideAnimationSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_hideAnimationSound);
		}
		base.DoHideAnimation();
	}

	// Token: 0x060024E2 RID: 9442 RVA: 0x000B99A7 File Offset: 0x000B7BA7
	private void HandleDraftTicketResponse(bool isConfirmed)
	{
		this.m_isConfirmed = isConfirmed;
		this.EnableFullScreenEffects(false);
		this.Hide();
	}

	// Token: 0x060024E3 RID: 9443 RVA: 0x000B99BD File Offset: 0x000B7BBD
	private void OnHideComplete(DialogBase dialog, object userdata)
	{
		if (this.m_isConfirmed)
		{
			OutstandingDraftTicketDialog.Info info = this.m_info;
			if (info == null)
			{
				return;
			}
			Action callbackOnEnter = info.m_callbackOnEnter;
			if (callbackOnEnter == null)
			{
				return;
			}
			callbackOnEnter();
			return;
		}
		else
		{
			OutstandingDraftTicketDialog.Info info2 = this.m_info;
			if (info2 == null)
			{
				return;
			}
			Action callbackOnCancel = info2.m_callbackOnCancel;
			if (callbackOnCancel == null)
			{
				return;
			}
			callbackOnCancel();
			return;
		}
	}

	// Token: 0x04001490 RID: 5264
	[CustomEditField(Sections = "Object Links")]
	public UIBButton m_enterButton;

	// Token: 0x04001491 RID: 5265
	[CustomEditField(Sections = "Object Links")]
	public UIBButton m_cancelButton;

	// Token: 0x04001492 RID: 5266
	public UberText m_ticketCount;

	// Token: 0x04001493 RID: 5267
	public UberText m_description;

	// Token: 0x04001494 RID: 5268
	public GameObject m_plusSign;

	// Token: 0x04001495 RID: 5269
	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_showAnimationSound = "Expand_Up.prefab:775d97ea42498c044897f396362b9db3";

	// Token: 0x04001496 RID: 5270
	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_hideAnimationSound = "Shrink_Down_Quicker.prefab:2fe963b171811ca4b8d544fa53e3330c";

	// Token: 0x04001497 RID: 5271
	private OutstandingDraftTicketDialog.Info m_info;

	// Token: 0x04001498 RID: 5272
	private bool m_isConfirmed;

	// Token: 0x020015CB RID: 5579
	public class Info
	{
		// Token: 0x0400AF02 RID: 44802
		public Action m_callbackOnEnter;

		// Token: 0x0400AF03 RID: 44803
		public Action m_callbackOnCancel;

		// Token: 0x0400AF04 RID: 44804
		public int m_outstandingTicketCount;
	}
}
