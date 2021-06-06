public class FiresideBrawlOkDialog : DialogBase
{
	public UIBButton m_okBrawlButton;

	public PegUIElement m_offClickCatcher;

	private void Start()
	{
		m_okBrawlButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			Hide();
		});
		m_offClickCatcher.AddEventListener(UIEventType.RELEASE, delegate
		{
			Hide();
		});
		ChatMgr.Get().OnFriendListToggled += OnFriendListToggled;
	}

	public override void Show()
	{
		base.Show();
		DoShowAnimation();
		SoundManager.Get().LoadAndPlay("friendly_challenge.prefab:649e070117bcd0d45bac691a03bf2dec");
	}

	public override void Hide()
	{
		base.Hide();
		SoundManager.Get().LoadAndPlay("banner_shrink.prefab:d9de7386a7f2017429d126e972232123");
		ChatMgr.Get().OnFriendListToggled -= OnFriendListToggled;
	}

	private void OnFriendListToggled(bool showing)
	{
		if (showing)
		{
			Hide();
		}
	}
}
