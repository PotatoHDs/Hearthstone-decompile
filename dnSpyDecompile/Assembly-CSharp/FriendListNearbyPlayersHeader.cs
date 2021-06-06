using System;
using UnityEngine;

// Token: 0x02000093 RID: 147
public class FriendListNearbyPlayersHeader : FriendListItemHeader
{
	// Token: 0x1400000D RID: 13
	// (add) Token: 0x0600094A RID: 2378 RVA: 0x00036A28 File Offset: 0x00034C28
	// (remove) Token: 0x0600094B RID: 2379 RVA: 0x00036A60 File Offset: 0x00034C60
	public event Action OnPanelOpened;

	// Token: 0x17000080 RID: 128
	// (get) Token: 0x0600094C RID: 2380 RVA: 0x00036A95 File Offset: 0x00034C95
	// (set) Token: 0x0600094D RID: 2381 RVA: 0x00036AA3 File Offset: 0x00034CA3
	private bool NearbyPlayersEnabled
	{
		get
		{
			return Options.Get().GetBool(Option.NEARBY_PLAYERS);
		}
		set
		{
			Options.Get().SetBool(Option.NEARBY_PLAYERS, value);
		}
	}

	// Token: 0x17000081 RID: 129
	// (get) Token: 0x0600094E RID: 2382 RVA: 0x00036AB2 File Offset: 0x00034CB2
	private GameObject Panel
	{
		get
		{
			if (!this.NearbyPlayersEnabled)
			{
				return this.m_enableNearbyPlayersPanel;
			}
			return this.m_disableNearbyPlayersPanel;
		}
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x00036AC9 File Offset: 0x00034CC9
	protected override void Awake()
	{
		base.Awake();
		this.m_disableButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnDisableRelease));
		this.m_enableButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnEnableRelease));
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x00036B03 File Offset: 0x00034D03
	protected override void OnDestroy()
	{
		this.OnPanelOpened = null;
		base.OnDestroy();
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x00036B14 File Offset: 0x00034D14
	public void SetText(int nearbyPlayerCount)
	{
		base.SetText(this.NearbyPlayersEnabled ? GameStrings.Format("GLOBAL_FRIENDLIST_NEARBY_PLAYERS_HEADER", new object[]
		{
			nearbyPlayerCount
		}) : GameStrings.Format("GLOBAL_FRIENDLIST_NEARBY_PLAYERS_DISABLED_HEADER", Array.Empty<object>()));
		this.m_StoredNearbyPlayerCount = nearbyPlayerCount;
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x00036B60 File Offset: 0x00034D60
	protected override void OnHeaderButtonReleased(UIEvent e)
	{
		if (this.m_PanelOpen)
		{
			this.ClosePanel();
			return;
		}
		this.OpenPanel();
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x00036B78 File Offset: 0x00034D78
	private void OpenPanel()
	{
		if (this.Panel == null || this.m_PanelOpen)
		{
			return;
		}
		if (ChatMgr.Get() != null && ChatMgr.Get().FriendListFrame != null && ChatMgr.Get().FriendListFrame.items != null)
		{
			ChatMgr.Get().FriendListFrame.items.GetComponent<TouchList>().SetScrollingEnabled(false);
		}
		this.m_PanelOpen = true;
		this.Panel.gameObject.SetActive(true);
		Camera camera = CameraUtils.FindFirstByLayer(this.Panel.layer);
		Bounds bounds = this.Panel.GetComponent<BoxCollider>().bounds;
		Bounds bounds2 = base.GetComponent<BoxCollider>().bounds;
		Vector3 b = bounds.size / 2f - bounds2.size / 2f;
		Vector3 a = bounds2.center + (this.Panel.transform.position - bounds.center);
		Vector3 vector = a - b;
		Vector3 vector2 = a + b;
		float y = (camera.WorldToViewportPoint(new Vector3(vector.x, (bounds2.center - bounds.size).y, 0f)).y < 0f) ? vector2.y : vector.y;
		this.Panel.transform.position = new Vector3(this.Panel.transform.position.x, y, this.Panel.transform.position.z);
		UIBHighlight component = base.GetComponent<UIBHighlight>();
		if (component != null)
		{
			component.AlwaysOver = true;
		}
		this.InitPanelInputBlocker();
		if (this.OnPanelOpened != null)
		{
			this.OnPanelOpened();
		}
	}

	// Token: 0x06000954 RID: 2388 RVA: 0x00036D50 File Offset: 0x00034F50
	private void ClosePanel()
	{
		if (this.Panel == null || !this.m_PanelOpen)
		{
			return;
		}
		if (ChatMgr.Get() != null && ChatMgr.Get().FriendListFrame != null && ChatMgr.Get().FriendListFrame.items != null)
		{
			ChatMgr.Get().FriendListFrame.items.GetComponent<TouchList>().SetScrollingEnabled(true);
		}
		this.m_PanelOpen = false;
		this.Panel.gameObject.SetActive(false);
		UIBHighlight component = base.GetComponent<UIBHighlight>();
		if (component != null)
		{
			component.AlwaysOver = false;
		}
		if (this.m_PanelInputBlocker != null)
		{
			UnityEngine.Object.Destroy(this.m_PanelInputBlocker.gameObject);
			this.m_PanelInputBlocker = null;
		}
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x00036E18 File Offset: 0x00035018
	private void InitPanelInputBlocker()
	{
		if (this.m_PanelInputBlocker != null)
		{
			UnityEngine.Object.Destroy(this.m_PanelInputBlocker.gameObject);
			this.m_PanelInputBlocker = null;
		}
		GameObject gameObject = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(this.Panel.layer), "NearbyPlayerPanelInputBlocker");
		gameObject.transform.parent = this.Panel.transform;
		this.m_PanelInputBlocker = gameObject.AddComponent<PegUIElement>();
		this.m_PanelInputBlocker.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnPanelInputBlockerReleased));
		TransformUtil.SetPosZ(this.m_PanelInputBlocker, this.Panel.transform.position.z + 1f);
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x00036EC6 File Offset: 0x000350C6
	private void OnPanelInputBlockerReleased(UIEvent e)
	{
		this.ClosePanel();
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x00036ECE File Offset: 0x000350CE
	private void OnDisableRelease(UIEvent e)
	{
		this.ClosePanel();
		this.NearbyPlayersEnabled = false;
		this.SetText(this.m_StoredNearbyPlayerCount);
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x00036EE9 File Offset: 0x000350E9
	private void OnEnableRelease(UIEvent e)
	{
		this.ClosePanel();
		this.NearbyPlayersEnabled = true;
		this.SetText(this.m_StoredNearbyPlayerCount);
	}

	// Token: 0x04000643 RID: 1603
	public GameObject m_arrowRight;

	// Token: 0x04000644 RID: 1604
	public GameObject m_disableNearbyPlayersPanel;

	// Token: 0x04000645 RID: 1605
	public GameObject m_enableNearbyPlayersPanel;

	// Token: 0x04000646 RID: 1606
	public UIBButton m_disableButton;

	// Token: 0x04000647 RID: 1607
	public UIBButton m_enableButton;

	// Token: 0x04000649 RID: 1609
	private PegUIElement m_PanelInputBlocker;

	// Token: 0x0400064A RID: 1610
	private bool m_PanelOpen;

	// Token: 0x0400064B RID: 1611
	private int m_StoredNearbyPlayerCount;
}
