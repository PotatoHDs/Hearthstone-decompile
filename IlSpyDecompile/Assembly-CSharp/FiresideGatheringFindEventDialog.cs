public class FiresideGatheringFindEventDialog : DialogBase
{
	public delegate void ResponseCallback(bool search);

	public class Info
	{
		public ResponseCallback m_callback;
	}

	public UberText m_titleHeader;

	public UIBButton m_joinButton;

	public UIBButton m_whereButton;

	public PegUIElement m_offClickCatcher;

	private ResponseCallback m_responseCallback;

	private void Start()
	{
		m_joinButton.AddEventListener(UIEventType.RELEASE, OnJoinButtonPress);
		m_whereButton.AddEventListener(UIEventType.RELEASE, OnWhereButtonPress);
		m_offClickCatcher.AddEventListener(UIEventType.RELEASE, delegate
		{
			Hide();
		});
		string text = GameStrings.Get("GLOBAL_SCAN");
		m_joinButton.SetText(text);
		bool flag = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>()?.FSGShowBetaLabel ?? false;
		m_titleHeader.Text = GameStrings.Get(flag ? "GLUE_FIRESIDE_GATHERING_BETA_TITLE" : "GLUE_FIRESIDE_GATHERING_TITLE");
	}

	public override void Show()
	{
		base.Show();
		BnetBar.Get().DisableButtonsByDialog(this);
		SoundManager.Get().LoadAndPlay("Expand_Up.prefab:775d97ea42498c044897f396362b9db3");
		DoShowAnimation();
		DialogBase.DoBlur();
	}

	public override void Hide()
	{
		base.Hide();
		SoundManager.Get().LoadAndPlay("Shrink_Down.prefab:a6d5184049ac041418cd5896e7d9a87a");
		DialogBase.EndBlur();
	}

	public void SetInfo(Info info)
	{
		m_responseCallback = info.m_callback;
	}

	private void OnJoinButtonPress(UIEvent e)
	{
		m_responseCallback(search: true);
		Hide();
	}

	private void OnWhereButtonPress(UIEvent e)
	{
		m_responseCallback(search: false);
		Hide();
	}
}
