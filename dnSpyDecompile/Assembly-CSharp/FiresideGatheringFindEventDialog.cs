using System;

// Token: 0x020002E6 RID: 742
public class FiresideGatheringFindEventDialog : DialogBase
{
	// Token: 0x060026CA RID: 9930 RVA: 0x000C26DC File Offset: 0x000C08DC
	private void Start()
	{
		this.m_joinButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnJoinButtonPress));
		this.m_whereButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnWhereButtonPress));
		this.m_offClickCatcher.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.Hide();
		});
		string text = GameStrings.Get("GLOBAL_SCAN");
		this.m_joinButton.SetText(text);
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		bool flag = netObject != null && netObject.FSGShowBetaLabel;
		this.m_titleHeader.Text = GameStrings.Get(flag ? "GLUE_FIRESIDE_GATHERING_BETA_TITLE" : "GLUE_FIRESIDE_GATHERING_TITLE");
	}

	// Token: 0x060026CB RID: 9931 RVA: 0x000C2782 File Offset: 0x000C0982
	public override void Show()
	{
		base.Show();
		BnetBar.Get().DisableButtonsByDialog(this);
		SoundManager.Get().LoadAndPlay("Expand_Up.prefab:775d97ea42498c044897f396362b9db3");
		this.DoShowAnimation();
		DialogBase.DoBlur();
	}

	// Token: 0x060026CC RID: 9932 RVA: 0x000C27B4 File Offset: 0x000C09B4
	public override void Hide()
	{
		base.Hide();
		SoundManager.Get().LoadAndPlay("Shrink_Down.prefab:a6d5184049ac041418cd5896e7d9a87a");
		DialogBase.EndBlur();
	}

	// Token: 0x060026CD RID: 9933 RVA: 0x000C27D5 File Offset: 0x000C09D5
	public void SetInfo(FiresideGatheringFindEventDialog.Info info)
	{
		this.m_responseCallback = info.m_callback;
	}

	// Token: 0x060026CE RID: 9934 RVA: 0x000C27E3 File Offset: 0x000C09E3
	private void OnJoinButtonPress(UIEvent e)
	{
		this.m_responseCallback(true);
		this.Hide();
	}

	// Token: 0x060026CF RID: 9935 RVA: 0x000C27F7 File Offset: 0x000C09F7
	private void OnWhereButtonPress(UIEvent e)
	{
		this.m_responseCallback(false);
		this.Hide();
	}

	// Token: 0x0400160C RID: 5644
	public UberText m_titleHeader;

	// Token: 0x0400160D RID: 5645
	public UIBButton m_joinButton;

	// Token: 0x0400160E RID: 5646
	public UIBButton m_whereButton;

	// Token: 0x0400160F RID: 5647
	public PegUIElement m_offClickCatcher;

	// Token: 0x04001610 RID: 5648
	private FiresideGatheringFindEventDialog.ResponseCallback m_responseCallback;

	// Token: 0x020015F9 RID: 5625
	// (Invoke) Token: 0x0600E272 RID: 57970
	public delegate void ResponseCallback(bool search);

	// Token: 0x020015FA RID: 5626
	public class Info
	{
		// Token: 0x0400AF82 RID: 44930
		public FiresideGatheringFindEventDialog.ResponseCallback m_callback;
	}
}
