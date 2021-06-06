using UnityEngine;

public class GameModesPopup : BasicPopup
{
	public class Info
	{
		public UIEvent.Handler m_onArenaButtonReleased;

		public UIEvent.Handler m_onBaconButtonReleased;
	}

	public PegUIElement m_arenaButton;

	public PegUIElement m_baconButton;

	public PegUIElement m_offClickCatcher;

	public GameObject m_earlyAccessVisual;

	public GameObject m_betaFlagVisual;

	private bool m_arenaEnabled;

	private bool m_baconEnabled;

	public void SetInfo(Info info)
	{
		m_arenaButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			OnArenaButtonReleased(e, info.m_onArenaButtonReleased);
		});
		m_baconButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			OnBaconButtonReleased(e, info.m_onBaconButtonReleased);
		});
		m_offClickCatcher.AddEventListener(UIEventType.RELEASE, delegate
		{
			Hide();
		});
	}

	public override void Show()
	{
		base.Show();
		UpdateButtonStates();
		UpdateEventTimingVisuals();
		DialogBase.DoBlur();
	}

	public override void Hide()
	{
		base.Hide();
		DialogBase.EndBlur();
	}

	private void UpdateButtonStates()
	{
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		m_arenaEnabled = netObject.Games.Forge && AchieveManager.Get() != null && AchieveManager.Get().HasUnlockedArena();
		if (m_arenaEnabled)
		{
			m_arenaEnabled = HealthyGamingMgr.Get().isArenaEnabled();
		}
		bool flag = SpecialEventManager.Get().IsEventActive("battlegrounds_early_access", activeIfDoesNotExist: false) && !AccountLicenseMgr.Get().OwnsAccountLicense(NetCache.Get().GetBattlegroundsEarlyAccessLicenseId());
		m_baconEnabled = netObject.Games.Battlegrounds && !flag;
	}

	private void UpdateEventTimingVisuals()
	{
		if (m_earlyAccessVisual != null)
		{
			m_earlyAccessVisual.gameObject.SetActive(SpecialEventManager.Get().IsEventActive("battlegrounds_early_access", activeIfDoesNotExist: false));
			if (!SpecialEventManager.Get().IsEventActive("battlegrounds_early_access", activeIfDoesNotExist: false))
			{
				Options.Get().SetBool(Option.HAS_SEEN_BATTLEGROUNDS_BOX_BUTTON, val: true);
			}
		}
		if (m_betaFlagVisual != null)
		{
			m_betaFlagVisual.gameObject.SetActive(SpecialEventManager.Get().IsEventActive("battlegrounds_beta_flag", activeIfDoesNotExist: false));
		}
	}

	private void OnArenaButtonReleased(UIEvent e, UIEvent.Handler handler)
	{
		if (!m_arenaEnabled)
		{
			ShowDisabledPopup(GameStrings.Get("GLUE_TOOLTIP_BUTTON_FORGE_HEADLINE"), GetArenaDisabledPopupText());
		}
		else
		{
			OnButtonReleased(e, handler);
		}
	}

	private void OnBaconButtonReleased(UIEvent e, UIEvent.Handler handler)
	{
		if (!m_baconEnabled)
		{
			ShowDisabledPopup(GameStrings.Get("GLUE_TOOLTIP_BUTTON_BACON_HEADLINE"), GetBaconDisabledPopupText());
		}
		else
		{
			OnButtonReleased(e, handler);
		}
	}

	private void OnButtonReleased(UIEvent e, UIEvent.Handler handler)
	{
		Hide();
		handler(e);
	}

	private string GetArenaDisabledPopupText()
	{
		if (AchieveManager.Get() == null || !AchieveManager.Get().HasUnlockedVanillaHeroes())
		{
			return GameStrings.Format("GLUE_TOOLTIP_BUTTON_FORGE_NOT_UNLOCKED", 20);
		}
		return GameStrings.Get("GLUE_TOOLTIP_BUTTON_DISABLED_DESC");
	}

	private string GetBaconDisabledPopupText()
	{
		if (SpecialEventManager.Get().IsEventActive("battlegrounds_early_access", activeIfDoesNotExist: false) && !AccountLicenseMgr.Get().OwnsAccountLicense(NetCache.Get().GetBattlegroundsEarlyAccessLicenseId()))
		{
			return GameStrings.Format("GLUE_TOOLTIP_BUTTON_BACON_DESC_EARLY_ACCESS");
		}
		return GameStrings.Get("GLUE_TOOLTIP_BUTTON_DISABLED_DESC");
	}

	private void ShowDisabledPopup(string header, string description)
	{
		if (string.IsNullOrEmpty(description))
		{
			description = GameStrings.Get("GLUE_TOOLTIP_BUTTON_DISABLED_DESC");
		}
		Hide();
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = header,
			m_text = description,
			m_showAlertIcon = true,
			m_responseDisplay = AlertPopup.ResponseDisplay.OK
		};
		DialogManager.Get().ShowPopup(info);
	}
}
