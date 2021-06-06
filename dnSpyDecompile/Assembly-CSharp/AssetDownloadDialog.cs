using System;
using Hearthstone.Streaming;

// Token: 0x02000ACB RID: 2763
public class AssetDownloadDialog : DialogBase
{
	// Token: 0x17000871 RID: 2161
	// (get) Token: 0x06009368 RID: 37736 RVA: 0x000274B4 File Offset: 0x000256B4
	private IGameDownloadManager DownloadManager
	{
		get
		{
			return GameDownloadManagerProvider.Get();
		}
	}

	// Token: 0x06009369 RID: 37737 RVA: 0x002FCCB0 File Offset: 0x002FAEB0
	private void Start()
	{
		this.m_enableDownloadButton = this.EnableDownloadButton.GetComponentInChildren<UIBButton>();
		this.m_cellularDataButton = this.CellularDataButton.GetComponentInChildren<UIBButton>();
		this.m_enableDownloadButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnDownloadButtonRelease));
		this.m_cellularDataButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			DownloadPermissionManager.CellularEnabled = !DownloadPermissionManager.CellularEnabled;
		});
		this.ClickCatcher.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnInputCatcherRelease));
		SceneUtils.SetLayer(this.m_enableDownloadButton, GameLayer.HighPriorityUI);
		SceneUtils.SetLayer(this.m_cellularDataButton, GameLayer.HighPriorityUI);
	}

	// Token: 0x0600936A RID: 37738 RVA: 0x002FCD57 File Offset: 0x002FAF57
	private void OnDownloadButtonRelease(UIEvent e)
	{
		DownloadPermissionManager.DownloadEnabled = !DownloadPermissionManager.DownloadEnabled;
		if (DownloadPermissionManager.DownloadEnabled)
		{
			this.DownloadManager.StartUpdateProcessForOptional();
			return;
		}
		this.DownloadManager.StopOptionalDownloads();
	}

	// Token: 0x0600936B RID: 37739 RVA: 0x002FCD84 File Offset: 0x002FAF84
	private void OnInputCatcherRelease(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		this.Hide();
	}

	// Token: 0x0600936C RID: 37740 RVA: 0x002FCDA0 File Offset: 0x002FAFA0
	public override void Show()
	{
		base.Show();
		this.DoShowAnimation();
		DialogBase.DoBlur();
	}

	// Token: 0x0600936D RID: 37741 RVA: 0x002FCDB3 File Offset: 0x002FAFB3
	public override void Hide()
	{
		base.Hide();
		DialogBase.EndBlur();
	}

	// Token: 0x0600936E RID: 37742 RVA: 0x002FCDC0 File Offset: 0x002FAFC0
	private void Update()
	{
		if (this.DownloadManager != null)
		{
			AssetDownloadDialog.SetButtonTextForState(this.m_enableDownloadButton, DownloadPermissionManager.DownloadEnabled);
			AssetDownloadDialog.SetButtonTextForState(this.m_cellularDataButton, DownloadPermissionManager.CellularEnabled);
		}
		if (GameMenu.Get() != null && GameMenu.Get().IsShown() && this.IsShown())
		{
			this.Hide();
		}
	}

	// Token: 0x0600936F RID: 37743 RVA: 0x002FCE1C File Offset: 0x002FB01C
	private static void SetButtonTextForState(UIBButton button, bool enabled)
	{
		if (button != null)
		{
			button.SetText(GameStrings.Get(enabled ? "GLOBAL_ASSET_DOWNLOAD_ENABLED" : "GLOBAL_ASSET_DOWNLOAD_DISABLED"));
		}
	}

	// Token: 0x04007B81 RID: 31617
	public DownloadStatusView DownloadStatusView;

	// Token: 0x04007B82 RID: 31618
	public NestedPrefab EnableDownloadButton;

	// Token: 0x04007B83 RID: 31619
	public NestedPrefab CellularDataButton;

	// Token: 0x04007B84 RID: 31620
	public PegUIElement ClickCatcher;

	// Token: 0x04007B85 RID: 31621
	private PegUIElement m_blocker;

	// Token: 0x04007B86 RID: 31622
	private UIBButton m_enableDownloadButton;

	// Token: 0x04007B87 RID: 31623
	private UIBButton m_cellularDataButton;

	// Token: 0x020026FC RID: 9980
	public class Info
	{
	}
}
