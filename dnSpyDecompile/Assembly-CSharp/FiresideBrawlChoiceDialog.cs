using System;
using PegasusShared;

// Token: 0x020002E0 RID: 736
public class FiresideBrawlChoiceDialog : DialogBase
{
	// Token: 0x06002678 RID: 9848 RVA: 0x000C1418 File Offset: 0x000BF618
	private void Start()
	{
		this.m_regularBrawlButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnRegularButtonPress));
		bool flag = TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
		bool flag2 = TavernBrawlManager.Get().HasUnlockedTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
		flag = (flag && flag2);
		this.m_regularBrawlButton.SetEnabled(flag, false);
		this.m_regularBrawlButton.Flip(flag, false);
		if (!flag2)
		{
			this.m_regularBrawlText.Text = GameStrings.Get("GLUE_TOOLTIP_BUTTON_TAVERN_BRAWL_NOT_UNLOCKED");
		}
		else if (!flag)
		{
			this.m_regularBrawlText.Text = TavernBrawlManager.Get().GetStartingTimeText(true);
		}
		else
		{
			this.m_regularBrawlText.Text = GameStrings.Get("GLUE_FIRESIDE_GATHERING_PLAY_REGULAR_BRAWL");
		}
		bool flag3 = TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING);
		this.m_fsgBrawlButton.SetEnabled(flag3, false);
		this.m_fsgBrawlButton.Flip(flag3, false);
		if (flag3)
		{
			this.m_fsgBrawlText.Text = GameStrings.Get("GLUE_FIRESIDE_GATHERING_PLAY_FSG_BRAWL");
		}
		else
		{
			this.m_fsgBrawlText.Text = GameStrings.Get("GLUE_FIRESIDE_GATHERING_BRAWL_UNAVAILABLE");
		}
		this.m_fsgBrawlButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnFSGButtonPress));
		this.m_offClickCatcher.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.Hide();
		});
	}

	// Token: 0x06002679 RID: 9849 RVA: 0x000C1545 File Offset: 0x000BF745
	public override void Show()
	{
		base.Show();
		BnetBar.Get().DisableButtonsByDialog(this);
		this.DoShowAnimation();
		SoundManager.Get().LoadAndPlay("friendly_challenge.prefab:649e070117bcd0d45bac691a03bf2dec");
		DialogBase.DoBlur();
	}

	// Token: 0x0600267A RID: 9850 RVA: 0x000C1577 File Offset: 0x000BF777
	public override void Hide()
	{
		base.Hide();
		SoundManager.Get().LoadAndPlay("banner_shrink.prefab:d9de7386a7f2017429d126e972232123");
		DialogBase.EndBlur();
	}

	// Token: 0x0600267B RID: 9851 RVA: 0x000C1598 File Offset: 0x000BF798
	public void SetInfo(FiresideBrawlChoiceDialog.Info info)
	{
		this.m_responseCallback = info.m_callback;
	}

	// Token: 0x0600267C RID: 9852 RVA: 0x000C15A6 File Offset: 0x000BF7A6
	private void OnRegularButtonPress(UIEvent e)
	{
		this.ChooseTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
	}

	// Token: 0x0600267D RID: 9853 RVA: 0x000C15AF File Offset: 0x000BF7AF
	private void OnFSGButtonPress(UIEvent e)
	{
		this.ChooseTavernBrawl(BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING);
	}

	// Token: 0x0600267E RID: 9854 RVA: 0x000C15B8 File Offset: 0x000BF7B8
	private void ChooseTavernBrawl(BrawlType source)
	{
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		if (this.m_responseCallback != null)
		{
			this.m_responseCallback(source);
		}
		this.Hide();
	}

	// Token: 0x040015D7 RID: 5591
	public UIBButton m_regularBrawlButton;

	// Token: 0x040015D8 RID: 5592
	public UIBButton m_fsgBrawlButton;

	// Token: 0x040015D9 RID: 5593
	public PegUIElement m_offClickCatcher;

	// Token: 0x040015DA RID: 5594
	public UberText m_regularBrawlText;

	// Token: 0x040015DB RID: 5595
	public UberText m_fsgBrawlText;

	// Token: 0x040015DC RID: 5596
	private FiresideBrawlChoiceDialog.ResponseCallback m_responseCallback;

	// Token: 0x020015F2 RID: 5618
	// (Invoke) Token: 0x0600E256 RID: 57942
	public delegate void ResponseCallback(BrawlType choice);

	// Token: 0x020015F3 RID: 5619
	public class Info
	{
		// Token: 0x0400AF73 RID: 44915
		public FiresideBrawlChoiceDialog.ResponseCallback m_callback;
	}
}
