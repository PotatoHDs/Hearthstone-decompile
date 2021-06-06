using System;
using System.Collections;
using UnityEngine;

public class StoreButton : PegUIElement
{
	public GameObject m_storeClosed;

	public UberText m_storeClosedText;

	public UberText m_storeText;

	public HighlightState m_highlightState;

	public GameObject m_highlight;

	private DateTime m_hubStartTime = DateTime.Now;

	private ShopAvailabilityError m_lastAvailabilityError;

	private const float SHOP_POLL_INTERVAL = 3f;

	protected override void Awake()
	{
		base.Awake();
		m_storeText.Text = GameStrings.Get("GLUE_STORE_OPEN_BUTTON_TEXT");
		m_storeClosedText.Text = GameStrings.Get("GLUE_STORE_CLOSED_BUTTON_TEXT");
	}

	private void Start()
	{
		m_storeClosed.SetActive(!StoreManager.Get().IsOpen());
		StoreManager.Get().RegisterStatusChangedListener(OnStoreStatusChanged);
		AddEventListener(UIEventType.ROLLOVER, OnButtonOver);
		AddEventListener(UIEventType.ROLLOUT, OnButtonOut);
		if (SoundManager.Get() != null)
		{
			SoundManager.Get().Load("store_button_mouse_over.prefab:11c9392d3449f064cb60420a61398732");
			SoundManager.Get().Load("Store_window_shrink.prefab:b68247126e211224e8a904142d2a9895");
		}
		StartCoroutine(PollShopStatusForTelemetry());
	}

	public void Unload()
	{
		SetEnabled(enabled: false);
		StoreManager.Get().RemoveStatusChangedListener(OnStoreStatusChanged);
	}

	public bool IsVisualClosed()
	{
		if (m_storeClosed != null)
		{
			return m_storeClosed.activeInHierarchy;
		}
		return false;
	}

	private void OnButtonOver(UIEvent e)
	{
		if (!IsVisualClosed())
		{
			SoundManager.Get().LoadAndPlay("store_button_mouse_over.prefab:11c9392d3449f064cb60420a61398732", base.gameObject);
		}
		if (m_highlightState != null)
		{
			m_highlightState.ChangeState(ActorStateType.HIGHLIGHT_MOUSE_OVER);
		}
		if (m_highlight != null)
		{
			m_highlight.SetActive(value: true);
		}
		TooltipZone component = GetComponent<TooltipZone>();
		if (component != null)
		{
			component.ShowBoxTooltip(GameStrings.Get("GLUE_TOOLTIP_BUTTON_STORE_HEADLINE"), GameStrings.Get("GLUE_TOOLTIP_BUTTON_STORE_DESC"));
		}
	}

	private void OnButtonOut(UIEvent e)
	{
		if (m_highlightState != null)
		{
			m_highlightState.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		}
		if (m_highlight != null)
		{
			m_highlight.SetActive(value: false);
		}
		TooltipZone component = GetComponent<TooltipZone>();
		if (component != null)
		{
			component.HideTooltip();
		}
	}

	private void OnStoreStatusChanged(bool isOpen)
	{
		SendShopStatusTelemetry();
		if (m_storeClosed != null)
		{
			m_storeClosed.SetActive(!isOpen);
		}
	}

	private IEnumerator PollShopStatusForTelemetry()
	{
		while (SceneMgr.Get().GetMode() != SceneMgr.Mode.HUB)
		{
			yield return null;
		}
		m_hubStartTime = DateTime.Now;
		while (m_lastAvailabilityError != ShopAvailabilityError.NO_ERROR)
		{
			yield return new WaitForSecondsRealtime(3f);
			SendShopStatusTelemetry();
		}
	}

	private void SendShopStatusTelemetry()
	{
		double timeInHubSec = 0.0;
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.HUB)
		{
			timeInHubSec = (DateTime.Now - m_hubStartTime).TotalSeconds;
		}
		ShopAvailabilityError storeAvailabilityError = StoreManager.Get().GetStoreAvailabilityError();
		if (m_lastAvailabilityError != storeAvailabilityError)
		{
			m_lastAvailabilityError = storeAvailabilityError;
			TelemetryManager.Client().SendShopStatus(storeAvailabilityError.ToString(), timeInHubSec);
		}
	}
}
