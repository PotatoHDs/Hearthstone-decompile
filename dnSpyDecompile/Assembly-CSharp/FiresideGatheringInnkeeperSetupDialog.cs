using System;

// Token: 0x020002E7 RID: 743
public class FiresideGatheringInnkeeperSetupDialog : DialogBase
{
	// Token: 0x060026D2 RID: 9938 RVA: 0x000C280B File Offset: 0x000C0A0B
	private void Start()
	{
		this.m_cancelButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnCancelButtonPress));
		this.m_confirmButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnConfirmButtonPress));
	}

	// Token: 0x060026D3 RID: 9939 RVA: 0x000C2782 File Offset: 0x000C0982
	public override void Show()
	{
		base.Show();
		BnetBar.Get().DisableButtonsByDialog(this);
		SoundManager.Get().LoadAndPlay("Expand_Up.prefab:775d97ea42498c044897f396362b9db3");
		this.DoShowAnimation();
		DialogBase.DoBlur();
	}

	// Token: 0x060026D4 RID: 9940 RVA: 0x000C27B4 File Offset: 0x000C09B4
	public override void Hide()
	{
		base.Hide();
		SoundManager.Get().LoadAndPlay("Shrink_Down.prefab:a6d5184049ac041418cd5896e7d9a87a");
		DialogBase.EndBlur();
	}

	// Token: 0x060026D5 RID: 9941 RVA: 0x000C283F File Offset: 0x000C0A3F
	public void SetInfo(FiresideGatheringInnkeeperSetupDialog.Info info)
	{
		this.m_responseCallback = info.m_callback;
		this.m_dialogBodyText.Text = GameStrings.Format("GLUE_FIRESIDE_GATHERING_INNKEEPER_SETUP_BODY", new object[]
		{
			info.m_tavernName
		});
	}

	// Token: 0x060026D6 RID: 9942 RVA: 0x000C2871 File Offset: 0x000C0A71
	private void OnCancelButtonPress(UIEvent e)
	{
		this.m_responseCallback(false);
		FiresideGatheringManager.Get().ShowInnkeeperSetupTooltip();
		this.Hide();
	}

	// Token: 0x060026D7 RID: 9943 RVA: 0x000C288F File Offset: 0x000C0A8F
	private void OnConfirmButtonPress(UIEvent e)
	{
		this.m_responseCallback(true);
		this.Hide();
	}

	// Token: 0x04001611 RID: 5649
	public UIBButton m_cancelButton;

	// Token: 0x04001612 RID: 5650
	public UIBButton m_confirmButton;

	// Token: 0x04001613 RID: 5651
	public UberText m_dialogBodyText;

	// Token: 0x04001614 RID: 5652
	private FiresideGatheringInnkeeperSetupDialog.ResponseCallback m_responseCallback;

	// Token: 0x020015FB RID: 5627
	// (Invoke) Token: 0x0600E277 RID: 57975
	public delegate void ResponseCallback(bool search);

	// Token: 0x020015FC RID: 5628
	public class Info
	{
		// Token: 0x0400AF83 RID: 44931
		public string m_tavernName;

		// Token: 0x0400AF84 RID: 44932
		public FiresideGatheringInnkeeperSetupDialog.ResponseCallback m_callback;
	}
}
