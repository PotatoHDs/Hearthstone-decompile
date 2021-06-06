using bgs;
using Hearthstone;
using UnityEngine;

public class GameMenuBase
{
	public delegate void ShowCallback();

	public delegate void HideCallback();

	public ShowCallback m_showCallback;

	public HideCallback m_hideCallback;

	private const string OPTIONS_MENU_NAME = "OptionsMenu.prefab:a6e5621068fd7c8429475b3e1a1aa991";

	private OptionsMenu m_optionsMenu;

	public void ShowOptionsMenu()
	{
		if (m_hideCallback != null)
		{
			m_hideCallback();
		}
		if (m_optionsMenu == null)
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab("OptionsMenu.prefab:a6e5621068fd7c8429475b3e1a1aa991");
			m_optionsMenu = gameObject.GetComponent<OptionsMenu>();
			if (m_optionsMenu != null)
			{
				SwitchToOptionsMenu();
			}
		}
		else
		{
			SwitchToOptionsMenu();
		}
	}

	public void DestroyOptionsMenu()
	{
		if (m_optionsMenu != null)
		{
			m_optionsMenu.RemoveHideHandler(OnOptionsMenuHidden);
		}
	}

	public bool UseKoreanRating()
	{
		if (SceneMgr.Get().IsInGame())
		{
			return false;
		}
		bool flag = BattleNet.GetAccountCountry() == "KOR";
		if (PlatformSettings.IsMobile() && !flag)
		{
			flag = MobileDeviceLocale.GetCountryCode() == "KR";
		}
		return flag;
	}

	private void SwitchToOptionsMenu()
	{
		m_optionsMenu.SetHideHandler(OnOptionsMenuHidden);
		m_optionsMenu.Show();
	}

	private void OnOptionsMenuHidden()
	{
		Object.Destroy(m_optionsMenu.gameObject);
		m_optionsMenu = null;
		if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.FATAL_ERROR) && !HearthstoneApplication.Get().IsResetting() && BnetBar.Get().AreButtonsEnabled() && m_showCallback != null)
		{
			m_showCallback();
		}
	}
}
