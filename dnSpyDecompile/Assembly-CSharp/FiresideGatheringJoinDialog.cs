using System;

// Token: 0x020002E8 RID: 744
public class FiresideGatheringJoinDialog : DialogBase
{
	// Token: 0x060026D9 RID: 9945 RVA: 0x000C28A3 File Offset: 0x000C0AA3
	private void Start()
	{
		this.m_joinButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnJoinButtonPress));
		this.m_declineButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnDeclineButtonPress));
	}

	// Token: 0x060026DA RID: 9946 RVA: 0x000C28D7 File Offset: 0x000C0AD7
	public override void Show()
	{
		base.Show();
		BnetBar.Get().DisableButtonsByDialog(this);
		this.DoShowAnimation();
		SoundManager.Get().LoadAndPlay("friendly_challenge.prefab:649e070117bcd0d45bac691a03bf2dec");
	}

	// Token: 0x060026DB RID: 9947 RVA: 0x000C2904 File Offset: 0x000C0B04
	public override void Hide()
	{
		base.Hide();
		SoundManager.Get().LoadAndPlay("banner_shrink.prefab:d9de7386a7f2017429d126e972232123");
	}

	// Token: 0x060026DC RID: 9948 RVA: 0x000C2920 File Offset: 0x000C0B20
	public void SetInfo(FiresideGatheringJoinDialog.Info info)
	{
		this.m_responseCallback = info.m_callback;
	}

	// Token: 0x060026DD RID: 9949 RVA: 0x000C292E File Offset: 0x000C0B2E
	private void OnJoinButtonPress(UIEvent e)
	{
		this.m_responseCallback(true);
		this.Hide();
	}

	// Token: 0x060026DE RID: 9950 RVA: 0x000C2942 File Offset: 0x000C0B42
	private void OnDeclineButtonPress(UIEvent e)
	{
		this.m_responseCallback(false);
		this.Hide();
	}

	// Token: 0x04001615 RID: 5653
	public UIBButton m_joinButton;

	// Token: 0x04001616 RID: 5654
	public UIBButton m_declineButton;

	// Token: 0x04001617 RID: 5655
	private FiresideGatheringJoinDialog.ResponseCallback m_responseCallback;

	// Token: 0x020015FD RID: 5629
	// (Invoke) Token: 0x0600E27C RID: 57980
	public delegate void ResponseCallback(bool joinFSG);

	// Token: 0x020015FE RID: 5630
	public class Info
	{
		// Token: 0x0400AF85 RID: 44933
		public FiresideGatheringJoinDialog.ResponseCallback m_callback;
	}
}
