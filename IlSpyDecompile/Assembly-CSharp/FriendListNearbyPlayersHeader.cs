using System;
using UnityEngine;

public class FriendListNearbyPlayersHeader : FriendListItemHeader
{
	public GameObject m_arrowRight;

	public GameObject m_disableNearbyPlayersPanel;

	public GameObject m_enableNearbyPlayersPanel;

	public UIBButton m_disableButton;

	public UIBButton m_enableButton;

	private PegUIElement m_PanelInputBlocker;

	private bool m_PanelOpen;

	private int m_StoredNearbyPlayerCount;

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

	private GameObject Panel
	{
		get
		{
			if (!NearbyPlayersEnabled)
			{
				return m_enableNearbyPlayersPanel;
			}
			return m_disableNearbyPlayersPanel;
		}
	}

	public event Action OnPanelOpened;

	protected override void Awake()
	{
		base.Awake();
		m_disableButton.AddEventListener(UIEventType.RELEASE, OnDisableRelease);
		m_enableButton.AddEventListener(UIEventType.RELEASE, OnEnableRelease);
	}

	protected override void OnDestroy()
	{
		this.OnPanelOpened = null;
		base.OnDestroy();
	}

	public void SetText(int nearbyPlayerCount)
	{
		SetText(NearbyPlayersEnabled ? GameStrings.Format("GLOBAL_FRIENDLIST_NEARBY_PLAYERS_HEADER", nearbyPlayerCount) : GameStrings.Format("GLOBAL_FRIENDLIST_NEARBY_PLAYERS_DISABLED_HEADER"));
		m_StoredNearbyPlayerCount = nearbyPlayerCount;
	}

	protected override void OnHeaderButtonReleased(UIEvent e)
	{
		if (m_PanelOpen)
		{
			ClosePanel();
		}
		else
		{
			OpenPanel();
		}
	}

	private void OpenPanel()
	{
		if (!(Panel == null) && !m_PanelOpen)
		{
			if (ChatMgr.Get() != null && ChatMgr.Get().FriendListFrame != null && ChatMgr.Get().FriendListFrame.items != null)
			{
				ChatMgr.Get().FriendListFrame.items.GetComponent<TouchList>().SetScrollingEnabled(enable: false);
			}
			m_PanelOpen = true;
			Panel.gameObject.SetActive(value: true);
			Camera camera = CameraUtils.FindFirstByLayer(Panel.layer);
			Bounds bounds = Panel.GetComponent<BoxCollider>().bounds;
			Bounds bounds2 = GetComponent<BoxCollider>().bounds;
			Vector3 vector = bounds.size / 2f - bounds2.size / 2f;
			Vector3 vector2 = bounds2.center + (Panel.transform.position - bounds.center);
			Vector3 vector3 = vector2 - vector;
			Vector3 vector4 = vector2 + vector;
			float y = ((camera.WorldToViewportPoint(new Vector3(vector3.x, (bounds2.center - bounds.size).y, 0f)).y < 0f) ? vector4.y : vector3.y);
			Panel.transform.position = new Vector3(Panel.transform.position.x, y, Panel.transform.position.z);
			UIBHighlight component = GetComponent<UIBHighlight>();
			if (component != null)
			{
				component.AlwaysOver = true;
			}
			InitPanelInputBlocker();
			if (this.OnPanelOpened != null)
			{
				this.OnPanelOpened();
			}
		}
	}

	private void ClosePanel()
	{
		if (!(Panel == null) && m_PanelOpen)
		{
			if (ChatMgr.Get() != null && ChatMgr.Get().FriendListFrame != null && ChatMgr.Get().FriendListFrame.items != null)
			{
				ChatMgr.Get().FriendListFrame.items.GetComponent<TouchList>().SetScrollingEnabled(enable: true);
			}
			m_PanelOpen = false;
			Panel.gameObject.SetActive(value: false);
			UIBHighlight component = GetComponent<UIBHighlight>();
			if (component != null)
			{
				component.AlwaysOver = false;
			}
			if (m_PanelInputBlocker != null)
			{
				UnityEngine.Object.Destroy(m_PanelInputBlocker.gameObject);
				m_PanelInputBlocker = null;
			}
		}
	}

	private void InitPanelInputBlocker()
	{
		if (m_PanelInputBlocker != null)
		{
			UnityEngine.Object.Destroy(m_PanelInputBlocker.gameObject);
			m_PanelInputBlocker = null;
		}
		GameObject gameObject = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(Panel.layer), "NearbyPlayerPanelInputBlocker");
		gameObject.transform.parent = Panel.transform;
		m_PanelInputBlocker = gameObject.AddComponent<PegUIElement>();
		m_PanelInputBlocker.AddEventListener(UIEventType.RELEASE, OnPanelInputBlockerReleased);
		TransformUtil.SetPosZ(m_PanelInputBlocker, Panel.transform.position.z + 1f);
	}

	private void OnPanelInputBlockerReleased(UIEvent e)
	{
		ClosePanel();
	}

	private void OnDisableRelease(UIEvent e)
	{
		ClosePanel();
		NearbyPlayersEnabled = false;
		SetText(m_StoredNearbyPlayerCount);
	}

	private void OnEnableRelease(UIEvent e)
	{
		ClosePanel();
		NearbyPlayersEnabled = true;
		SetText(m_StoredNearbyPlayerCount);
	}
}
