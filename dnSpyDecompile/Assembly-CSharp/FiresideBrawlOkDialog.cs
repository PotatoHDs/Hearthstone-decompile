using System;

// Token: 0x020002E1 RID: 737
public class FiresideBrawlOkDialog : DialogBase
{
	// Token: 0x06002681 RID: 9857 RVA: 0x000C15F8 File Offset: 0x000BF7F8
	private void Start()
	{
		this.m_okBrawlButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.Hide();
		});
		this.m_offClickCatcher.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.Hide();
		});
		ChatMgr.Get().OnFriendListToggled += this.OnFriendListToggled;
	}

	// Token: 0x06002682 RID: 9858 RVA: 0x000C164D File Offset: 0x000BF84D
	public override void Show()
	{
		base.Show();
		this.DoShowAnimation();
		SoundManager.Get().LoadAndPlay("friendly_challenge.prefab:649e070117bcd0d45bac691a03bf2dec");
	}

	// Token: 0x06002683 RID: 9859 RVA: 0x000C166F File Offset: 0x000BF86F
	public override void Hide()
	{
		base.Hide();
		SoundManager.Get().LoadAndPlay("banner_shrink.prefab:d9de7386a7f2017429d126e972232123");
		ChatMgr.Get().OnFriendListToggled -= this.OnFriendListToggled;
	}

	// Token: 0x06002684 RID: 9860 RVA: 0x000C16A1 File Offset: 0x000BF8A1
	private void OnFriendListToggled(bool showing)
	{
		if (showing)
		{
			this.Hide();
		}
	}

	// Token: 0x040015DD RID: 5597
	public UIBButton m_okBrawlButton;

	// Token: 0x040015DE RID: 5598
	public PegUIElement m_offClickCatcher;
}
