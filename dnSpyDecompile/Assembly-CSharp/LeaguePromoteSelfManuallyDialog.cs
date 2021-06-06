using System;
using Hearthstone.UI;

// Token: 0x02000B1B RID: 2843
public class LeaguePromoteSelfManuallyDialog : DialogBase
{
	// Token: 0x0600971A RID: 38682 RVA: 0x0030D76A File Offset: 0x0030B96A
	private void Start()
	{
		this.m_cancelButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnCancelButtonPress));
		this.m_confirmButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnConfirmButtonPress));
	}

	// Token: 0x0600971B RID: 38683 RVA: 0x0030D7A0 File Offset: 0x0030B9A0
	protected override void OnDestroy()
	{
		PopupRoot component = base.gameObject.GetComponent<PopupRoot>();
		UIContext.GetRoot().CleanupPopupCamera(component);
		base.OnDestroy();
	}

	// Token: 0x0600971C RID: 38684 RVA: 0x0030D7CA File Offset: 0x0030B9CA
	public override void Show()
	{
		base.Show();
		UIContext.GetRoot().ShowPopup(base.gameObject, UIContext.BlurType.Standard);
		BnetBar.Get().DisableButtonsByDialog(this);
		SoundManager.Get().LoadAndPlay("Expand_Up.prefab:775d97ea42498c044897f396362b9db3");
		this.DoShowAnimation();
	}

	// Token: 0x0600971D RID: 38685 RVA: 0x0030D809 File Offset: 0x0030BA09
	public override void Hide()
	{
		base.Hide();
		SoundManager.Get().LoadAndPlay("Shrink_Down.prefab:a6d5184049ac041418cd5896e7d9a87a");
		UIContext.GetRoot().UnregisterPopup(base.gameObject);
	}

	// Token: 0x0600971E RID: 38686 RVA: 0x0030D835 File Offset: 0x0030BA35
	public void SetInfo(LeaguePromoteSelfManuallyDialog.Info info)
	{
		this.m_responseCallback = info.m_callback;
	}

	// Token: 0x0600971F RID: 38687 RVA: 0x000C15F0 File Offset: 0x000BF7F0
	private void OnCancelButtonPress(UIEvent e)
	{
		this.Hide();
	}

	// Token: 0x06009720 RID: 38688 RVA: 0x0030D843 File Offset: 0x0030BA43
	private void OnConfirmButtonPress(UIEvent e)
	{
		this.m_responseCallback();
		this.Hide();
	}

	// Token: 0x04007E78 RID: 32376
	public UIBButton m_cancelButton;

	// Token: 0x04007E79 RID: 32377
	public UIBButton m_confirmButton;

	// Token: 0x04007E7A RID: 32378
	private LeaguePromoteSelfManuallyDialog.ResponseCallback m_responseCallback;

	// Token: 0x02002764 RID: 10084
	// (Invoke) Token: 0x060139D2 RID: 80338
	public delegate void ResponseCallback();

	// Token: 0x02002765 RID: 10085
	public class Info
	{
		// Token: 0x0400F3EF RID: 62447
		public LeaguePromoteSelfManuallyDialog.ResponseCallback m_callback;
	}
}
