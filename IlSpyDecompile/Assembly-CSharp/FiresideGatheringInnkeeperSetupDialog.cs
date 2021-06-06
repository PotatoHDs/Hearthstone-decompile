public class FiresideGatheringInnkeeperSetupDialog : DialogBase
{
	public delegate void ResponseCallback(bool search);

	public class Info
	{
		public string m_tavernName;

		public ResponseCallback m_callback;
	}

	public UIBButton m_cancelButton;

	public UIBButton m_confirmButton;

	public UberText m_dialogBodyText;

	private ResponseCallback m_responseCallback;

	private void Start()
	{
		m_cancelButton.AddEventListener(UIEventType.RELEASE, OnCancelButtonPress);
		m_confirmButton.AddEventListener(UIEventType.RELEASE, OnConfirmButtonPress);
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
		m_dialogBodyText.Text = GameStrings.Format("GLUE_FIRESIDE_GATHERING_INNKEEPER_SETUP_BODY", info.m_tavernName);
	}

	private void OnCancelButtonPress(UIEvent e)
	{
		m_responseCallback(search: false);
		FiresideGatheringManager.Get().ShowInnkeeperSetupTooltip();
		Hide();
	}

	private void OnConfirmButtonPress(UIEvent e)
	{
		m_responseCallback(search: true);
		Hide();
	}
}
