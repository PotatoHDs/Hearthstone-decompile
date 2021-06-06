using System;
using UnityEngine;

// Token: 0x020008C6 RID: 2246
public class GameModesPopup : BasicPopup
{
	// Token: 0x06007C0E RID: 31758 RVA: 0x00284EB4 File Offset: 0x002830B4
	public void SetInfo(GameModesPopup.Info info)
	{
		this.m_arenaButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.OnArenaButtonReleased(e, info.m_onArenaButtonReleased);
		});
		this.m_baconButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.OnBaconButtonReleased(e, info.m_onBaconButtonReleased);
		});
		this.m_offClickCatcher.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.Hide();
		});
	}

	// Token: 0x06007C0F RID: 31759 RVA: 0x00284F20 File Offset: 0x00283120
	public override void Show()
	{
		base.Show();
		this.UpdateButtonStates();
		this.UpdateEventTimingVisuals();
		DialogBase.DoBlur();
	}

	// Token: 0x06007C10 RID: 31760 RVA: 0x00284F39 File Offset: 0x00283139
	public override void Hide()
	{
		base.Hide();
		DialogBase.EndBlur();
	}

	// Token: 0x06007C11 RID: 31761 RVA: 0x00284F48 File Offset: 0x00283148
	private void UpdateButtonStates()
	{
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		this.m_arenaEnabled = (netObject.Games.Forge && AchieveManager.Get() != null && AchieveManager.Get().HasUnlockedArena());
		if (this.m_arenaEnabled)
		{
			this.m_arenaEnabled = HealthyGamingMgr.Get().isArenaEnabled();
		}
		bool flag = SpecialEventManager.Get().IsEventActive("battlegrounds_early_access", false) && !AccountLicenseMgr.Get().OwnsAccountLicense(NetCache.Get().GetBattlegroundsEarlyAccessLicenseId());
		this.m_baconEnabled = (netObject.Games.Battlegrounds && !flag);
	}

	// Token: 0x06007C12 RID: 31762 RVA: 0x00284FE8 File Offset: 0x002831E8
	private void UpdateEventTimingVisuals()
	{
		if (this.m_earlyAccessVisual != null)
		{
			this.m_earlyAccessVisual.gameObject.SetActive(SpecialEventManager.Get().IsEventActive("battlegrounds_early_access", false));
			if (!SpecialEventManager.Get().IsEventActive("battlegrounds_early_access", false))
			{
				Options.Get().SetBool(Option.HAS_SEEN_BATTLEGROUNDS_BOX_BUTTON, true);
			}
		}
		if (this.m_betaFlagVisual != null)
		{
			this.m_betaFlagVisual.gameObject.SetActive(SpecialEventManager.Get().IsEventActive("battlegrounds_beta_flag", false));
		}
	}

	// Token: 0x06007C13 RID: 31763 RVA: 0x00285073 File Offset: 0x00283273
	private void OnArenaButtonReleased(UIEvent e, UIEvent.Handler handler)
	{
		if (!this.m_arenaEnabled)
		{
			this.ShowDisabledPopup(GameStrings.Get("GLUE_TOOLTIP_BUTTON_FORGE_HEADLINE"), this.GetArenaDisabledPopupText());
			return;
		}
		this.OnButtonReleased(e, handler);
	}

	// Token: 0x06007C14 RID: 31764 RVA: 0x0028509C File Offset: 0x0028329C
	private void OnBaconButtonReleased(UIEvent e, UIEvent.Handler handler)
	{
		if (!this.m_baconEnabled)
		{
			this.ShowDisabledPopup(GameStrings.Get("GLUE_TOOLTIP_BUTTON_BACON_HEADLINE"), this.GetBaconDisabledPopupText());
			return;
		}
		this.OnButtonReleased(e, handler);
	}

	// Token: 0x06007C15 RID: 31765 RVA: 0x002850C5 File Offset: 0x002832C5
	private void OnButtonReleased(UIEvent e, UIEvent.Handler handler)
	{
		this.Hide();
		handler(e);
	}

	// Token: 0x06007C16 RID: 31766 RVA: 0x002850D4 File Offset: 0x002832D4
	private string GetArenaDisabledPopupText()
	{
		if (AchieveManager.Get() == null || !AchieveManager.Get().HasUnlockedVanillaHeroes())
		{
			return GameStrings.Format("GLUE_TOOLTIP_BUTTON_FORGE_NOT_UNLOCKED", new object[]
			{
				20
			});
		}
		return GameStrings.Get("GLUE_TOOLTIP_BUTTON_DISABLED_DESC");
	}

	// Token: 0x06007C17 RID: 31767 RVA: 0x00285114 File Offset: 0x00283314
	private string GetBaconDisabledPopupText()
	{
		if (SpecialEventManager.Get().IsEventActive("battlegrounds_early_access", false) && !AccountLicenseMgr.Get().OwnsAccountLicense(NetCache.Get().GetBattlegroundsEarlyAccessLicenseId()))
		{
			return GameStrings.Format("GLUE_TOOLTIP_BUTTON_BACON_DESC_EARLY_ACCESS", Array.Empty<object>());
		}
		return GameStrings.Get("GLUE_TOOLTIP_BUTTON_DISABLED_DESC");
	}

	// Token: 0x06007C18 RID: 31768 RVA: 0x00285164 File Offset: 0x00283364
	private void ShowDisabledPopup(string header, string description)
	{
		if (string.IsNullOrEmpty(description))
		{
			description = GameStrings.Get("GLUE_TOOLTIP_BUTTON_DISABLED_DESC");
		}
		this.Hide();
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = header,
			m_text = description,
			m_showAlertIcon = true,
			m_responseDisplay = AlertPopup.ResponseDisplay.OK
		};
		DialogManager.Get().ShowPopup(info);
	}

	// Token: 0x0400652D RID: 25901
	public PegUIElement m_arenaButton;

	// Token: 0x0400652E RID: 25902
	public PegUIElement m_baconButton;

	// Token: 0x0400652F RID: 25903
	public PegUIElement m_offClickCatcher;

	// Token: 0x04006530 RID: 25904
	public GameObject m_earlyAccessVisual;

	// Token: 0x04006531 RID: 25905
	public GameObject m_betaFlagVisual;

	// Token: 0x04006532 RID: 25906
	private bool m_arenaEnabled;

	// Token: 0x04006533 RID: 25907
	private bool m_baconEnabled;

	// Token: 0x0200253E RID: 9534
	public class Info
	{
		// Token: 0x0400ED01 RID: 60673
		public UIEvent.Handler m_onArenaButtonReleased;

		// Token: 0x0400ED02 RID: 60674
		public UIEvent.Handler m_onBaconButtonReleased;
	}
}
