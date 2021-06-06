using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000720 RID: 1824
public class StoreButton : PegUIElement
{
	// Token: 0x060065AD RID: 26029 RVA: 0x002115B4 File Offset: 0x0020F7B4
	protected override void Awake()
	{
		base.Awake();
		this.m_storeText.Text = GameStrings.Get("GLUE_STORE_OPEN_BUTTON_TEXT");
		this.m_storeClosedText.Text = GameStrings.Get("GLUE_STORE_CLOSED_BUTTON_TEXT");
	}

	// Token: 0x060065AE RID: 26030 RVA: 0x002115E8 File Offset: 0x0020F7E8
	private void Start()
	{
		this.m_storeClosed.SetActive(!StoreManager.Get().IsOpen(true));
		StoreManager.Get().RegisterStatusChangedListener(new Action<bool>(this.OnStoreStatusChanged));
		this.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnButtonOver));
		this.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnButtonOut));
		if (SoundManager.Get() != null)
		{
			SoundManager.Get().Load("store_button_mouse_over.prefab:11c9392d3449f064cb60420a61398732");
			SoundManager.Get().Load("Store_window_shrink.prefab:b68247126e211224e8a904142d2a9895");
		}
		base.StartCoroutine(this.PollShopStatusForTelemetry());
	}

	// Token: 0x060065AF RID: 26031 RVA: 0x0021168A File Offset: 0x0020F88A
	public void Unload()
	{
		this.SetEnabled(false, false);
		StoreManager.Get().RemoveStatusChangedListener(new Action<bool>(this.OnStoreStatusChanged));
	}

	// Token: 0x060065B0 RID: 26032 RVA: 0x002116AA File Offset: 0x0020F8AA
	public bool IsVisualClosed()
	{
		return this.m_storeClosed != null && this.m_storeClosed.activeInHierarchy;
	}

	// Token: 0x060065B1 RID: 26033 RVA: 0x002116C8 File Offset: 0x0020F8C8
	private void OnButtonOver(UIEvent e)
	{
		if (!this.IsVisualClosed())
		{
			SoundManager.Get().LoadAndPlay("store_button_mouse_over.prefab:11c9392d3449f064cb60420a61398732", base.gameObject);
		}
		if (this.m_highlightState != null)
		{
			this.m_highlightState.ChangeState(ActorStateType.HIGHLIGHT_MOUSE_OVER);
		}
		if (this.m_highlight != null)
		{
			this.m_highlight.SetActive(true);
		}
		TooltipZone component = base.GetComponent<TooltipZone>();
		if (component != null)
		{
			component.ShowBoxTooltip(GameStrings.Get("GLUE_TOOLTIP_BUTTON_STORE_HEADLINE"), GameStrings.Get("GLUE_TOOLTIP_BUTTON_STORE_DESC"), 0);
		}
	}

	// Token: 0x060065B2 RID: 26034 RVA: 0x00211758 File Offset: 0x0020F958
	private void OnButtonOut(UIEvent e)
	{
		if (this.m_highlightState != null)
		{
			this.m_highlightState.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		}
		if (this.m_highlight != null)
		{
			this.m_highlight.SetActive(false);
		}
		TooltipZone component = base.GetComponent<TooltipZone>();
		if (component != null)
		{
			component.HideTooltip();
		}
	}

	// Token: 0x060065B3 RID: 26035 RVA: 0x002117B1 File Offset: 0x0020F9B1
	private void OnStoreStatusChanged(bool isOpen)
	{
		this.SendShopStatusTelemetry();
		if (this.m_storeClosed != null)
		{
			this.m_storeClosed.SetActive(!isOpen);
		}
	}

	// Token: 0x060065B4 RID: 26036 RVA: 0x002117D6 File Offset: 0x0020F9D6
	private IEnumerator PollShopStatusForTelemetry()
	{
		while (SceneMgr.Get().GetMode() != SceneMgr.Mode.HUB)
		{
			yield return null;
		}
		this.m_hubStartTime = DateTime.Now;
		while (this.m_lastAvailabilityError != ShopAvailabilityError.NO_ERROR)
		{
			yield return new WaitForSecondsRealtime(3f);
			this.SendShopStatusTelemetry();
		}
		yield break;
	}

	// Token: 0x060065B5 RID: 26037 RVA: 0x002117E8 File Offset: 0x0020F9E8
	private void SendShopStatusTelemetry()
	{
		double timeInHubSec = 0.0;
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.HUB)
		{
			timeInHubSec = (DateTime.Now - this.m_hubStartTime).TotalSeconds;
		}
		ShopAvailabilityError storeAvailabilityError = StoreManager.Get().GetStoreAvailabilityError();
		if (this.m_lastAvailabilityError == storeAvailabilityError)
		{
			return;
		}
		this.m_lastAvailabilityError = storeAvailabilityError;
		TelemetryManager.Client().SendShopStatus(storeAvailabilityError.ToString(), timeInHubSec);
	}

	// Token: 0x04005455 RID: 21589
	public GameObject m_storeClosed;

	// Token: 0x04005456 RID: 21590
	public UberText m_storeClosedText;

	// Token: 0x04005457 RID: 21591
	public UberText m_storeText;

	// Token: 0x04005458 RID: 21592
	public HighlightState m_highlightState;

	// Token: 0x04005459 RID: 21593
	public GameObject m_highlight;

	// Token: 0x0400545A RID: 21594
	private DateTime m_hubStartTime = DateTime.Now;

	// Token: 0x0400545B RID: 21595
	private ShopAvailabilityError m_lastAvailabilityError;

	// Token: 0x0400545C RID: 21596
	private const float SHOP_POLL_INTERVAL = 3f;
}
