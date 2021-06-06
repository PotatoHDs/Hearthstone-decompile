using PegasusShared;

public class FiresideBrawlChoiceDialog : DialogBase
{
	public delegate void ResponseCallback(BrawlType choice);

	public class Info
	{
		public ResponseCallback m_callback;
	}

	public UIBButton m_regularBrawlButton;

	public UIBButton m_fsgBrawlButton;

	public PegUIElement m_offClickCatcher;

	public UberText m_regularBrawlText;

	public UberText m_fsgBrawlText;

	private ResponseCallback m_responseCallback;

	private void Start()
	{
		m_regularBrawlButton.AddEventListener(UIEventType.RELEASE, OnRegularButtonPress);
		bool flag = TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
		bool flag2 = TavernBrawlManager.Get().HasUnlockedTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
		flag = flag && flag2;
		m_regularBrawlButton.SetEnabled(flag);
		m_regularBrawlButton.Flip(flag);
		if (!flag2)
		{
			m_regularBrawlText.Text = GameStrings.Get("GLUE_TOOLTIP_BUTTON_TAVERN_BRAWL_NOT_UNLOCKED");
		}
		else if (!flag)
		{
			m_regularBrawlText.Text = TavernBrawlManager.Get().GetStartingTimeText(singleLine: true);
		}
		else
		{
			m_regularBrawlText.Text = GameStrings.Get("GLUE_FIRESIDE_GATHERING_PLAY_REGULAR_BRAWL");
		}
		bool flag3 = TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING);
		m_fsgBrawlButton.SetEnabled(flag3);
		m_fsgBrawlButton.Flip(flag3);
		if (flag3)
		{
			m_fsgBrawlText.Text = GameStrings.Get("GLUE_FIRESIDE_GATHERING_PLAY_FSG_BRAWL");
		}
		else
		{
			m_fsgBrawlText.Text = GameStrings.Get("GLUE_FIRESIDE_GATHERING_BRAWL_UNAVAILABLE");
		}
		m_fsgBrawlButton.AddEventListener(UIEventType.RELEASE, OnFSGButtonPress);
		m_offClickCatcher.AddEventListener(UIEventType.RELEASE, delegate
		{
			Hide();
		});
	}

	public override void Show()
	{
		base.Show();
		BnetBar.Get().DisableButtonsByDialog(this);
		DoShowAnimation();
		SoundManager.Get().LoadAndPlay("friendly_challenge.prefab:649e070117bcd0d45bac691a03bf2dec");
		DialogBase.DoBlur();
	}

	public override void Hide()
	{
		base.Hide();
		SoundManager.Get().LoadAndPlay("banner_shrink.prefab:d9de7386a7f2017429d126e972232123");
		DialogBase.EndBlur();
	}

	public void SetInfo(Info info)
	{
		m_responseCallback = info.m_callback;
	}

	private void OnRegularButtonPress(UIEvent e)
	{
		ChooseTavernBrawl(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
	}

	private void OnFSGButtonPress(UIEvent e)
	{
		ChooseTavernBrawl(BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING);
	}

	private void ChooseTavernBrawl(BrawlType source)
	{
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		if (m_responseCallback != null)
		{
			m_responseCallback(source);
		}
		Hide();
	}
}
