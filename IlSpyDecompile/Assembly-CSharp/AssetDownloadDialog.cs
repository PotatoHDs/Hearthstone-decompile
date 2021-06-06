using Hearthstone.Streaming;

public class AssetDownloadDialog : DialogBase
{
	public class Info
	{
	}

	public DownloadStatusView DownloadStatusView;

	public NestedPrefab EnableDownloadButton;

	public NestedPrefab CellularDataButton;

	public PegUIElement ClickCatcher;

	private PegUIElement m_blocker;

	private UIBButton m_enableDownloadButton;

	private UIBButton m_cellularDataButton;

	private IGameDownloadManager DownloadManager => GameDownloadManagerProvider.Get();

	private void Start()
	{
		m_enableDownloadButton = EnableDownloadButton.GetComponentInChildren<UIBButton>();
		m_cellularDataButton = CellularDataButton.GetComponentInChildren<UIBButton>();
		m_enableDownloadButton.AddEventListener(UIEventType.RELEASE, OnDownloadButtonRelease);
		m_cellularDataButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			DownloadPermissionManager.CellularEnabled = !DownloadPermissionManager.CellularEnabled;
		});
		ClickCatcher.AddEventListener(UIEventType.RELEASE, OnInputCatcherRelease);
		SceneUtils.SetLayer(m_enableDownloadButton, GameLayer.HighPriorityUI);
		SceneUtils.SetLayer(m_cellularDataButton, GameLayer.HighPriorityUI);
	}

	private void OnDownloadButtonRelease(UIEvent e)
	{
		DownloadPermissionManager.DownloadEnabled = !DownloadPermissionManager.DownloadEnabled;
		if (DownloadPermissionManager.DownloadEnabled)
		{
			DownloadManager.StartUpdateProcessForOptional();
		}
		else
		{
			DownloadManager.StopOptionalDownloads();
		}
	}

	private void OnInputCatcherRelease(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		Hide();
	}

	public override void Show()
	{
		base.Show();
		DoShowAnimation();
		DialogBase.DoBlur();
	}

	public override void Hide()
	{
		base.Hide();
		DialogBase.EndBlur();
	}

	private void Update()
	{
		if (DownloadManager != null)
		{
			SetButtonTextForState(m_enableDownloadButton, DownloadPermissionManager.DownloadEnabled);
			SetButtonTextForState(m_cellularDataButton, DownloadPermissionManager.CellularEnabled);
		}
		if (GameMenu.Get() != null && GameMenu.Get().IsShown() && IsShown())
		{
			Hide();
		}
	}

	private static void SetButtonTextForState(UIBButton button, bool enabled)
	{
		if (button != null)
		{
			button.SetText(GameStrings.Get(enabled ? "GLOBAL_ASSET_DOWNLOAD_ENABLED" : "GLOBAL_ASSET_DOWNLOAD_DISABLED"));
		}
	}
}
