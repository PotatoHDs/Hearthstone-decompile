public class FiresideGatheringJoinDialog : DialogBase
{
	public delegate void ResponseCallback(bool joinFSG);

	public class Info
	{
		public ResponseCallback m_callback;
	}

	public UIBButton m_joinButton;

	public UIBButton m_declineButton;

	private ResponseCallback m_responseCallback;

	private void Start()
	{
		m_joinButton.AddEventListener(UIEventType.RELEASE, OnJoinButtonPress);
		m_declineButton.AddEventListener(UIEventType.RELEASE, OnDeclineButtonPress);
	}

	public override void Show()
	{
		base.Show();
		BnetBar.Get().DisableButtonsByDialog(this);
		DoShowAnimation();
		SoundManager.Get().LoadAndPlay("friendly_challenge.prefab:649e070117bcd0d45bac691a03bf2dec");
	}

	public override void Hide()
	{
		base.Hide();
		SoundManager.Get().LoadAndPlay("banner_shrink.prefab:d9de7386a7f2017429d126e972232123");
	}

	public void SetInfo(Info info)
	{
		m_responseCallback = info.m_callback;
	}

	private void OnJoinButtonPress(UIEvent e)
	{
		m_responseCallback(joinFSG: true);
		Hide();
	}

	private void OnDeclineButtonPress(UIEvent e)
	{
		m_responseCallback(joinFSG: false);
		Hide();
	}
}
