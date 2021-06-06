using Hearthstone.UI;

public class LeaguePromoteSelfManuallyDialog : DialogBase
{
	public delegate void ResponseCallback();

	public class Info
	{
		public ResponseCallback m_callback;
	}

	public UIBButton m_cancelButton;

	public UIBButton m_confirmButton;

	private ResponseCallback m_responseCallback;

	private void Start()
	{
		m_cancelButton.AddEventListener(UIEventType.RELEASE, OnCancelButtonPress);
		m_confirmButton.AddEventListener(UIEventType.RELEASE, OnConfirmButtonPress);
	}

	protected override void OnDestroy()
	{
		PopupRoot component = base.gameObject.GetComponent<PopupRoot>();
		UIContext.GetRoot().CleanupPopupCamera(component);
		base.OnDestroy();
	}

	public override void Show()
	{
		base.Show();
		UIContext.GetRoot().ShowPopup(base.gameObject);
		BnetBar.Get().DisableButtonsByDialog(this);
		SoundManager.Get().LoadAndPlay("Expand_Up.prefab:775d97ea42498c044897f396362b9db3");
		DoShowAnimation();
	}

	public override void Hide()
	{
		base.Hide();
		SoundManager.Get().LoadAndPlay("Shrink_Down.prefab:a6d5184049ac041418cd5896e7d9a87a");
		UIContext.GetRoot().UnregisterPopup(base.gameObject);
	}

	public void SetInfo(Info info)
	{
		m_responseCallback = info.m_callback;
	}

	private void OnCancelButtonPress(UIEvent e)
	{
		Hide();
	}

	private void OnConfirmButtonPress(UIEvent e)
	{
		m_responseCallback();
		Hide();
	}
}
