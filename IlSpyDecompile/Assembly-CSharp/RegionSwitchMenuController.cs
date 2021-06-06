using System.Collections.Generic;
using bgs;
using Hearthstone.Login;
using UnityEngine;

public class RegionSwitchMenuController
{
	public struct RegionMenuSettings
	{
		public struct RegionButtonSetting
		{
			public string ButtonLabel { get; set; }

			public constants.BnetRegion Region { get; set; }
		}

		public List<RegionButtonSetting> Buttons { get; set; }

		public constants.BnetRegion CurrentRegion { get; set; }
	}

	private RegionMenu m_regionMenu;

	private const int BUTTON_COUNT = 3;

	private const string WARNING_PREFAB = "RegionSelect.prefab:a29650226d94fae408628b0c5aad1348";

	private const string REGION_MENU_PREFAB = "RegionMenu.prefab:81394e6ea3adb1140a29ff4b44744891";

	private static readonly PlatformDependentValue<float> WARNING_PADDING = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 60f,
		Phone = 80f
	};

	public void ShowRegionMenuWithDefaultSettings()
	{
		if (ShouldSkipRegionSwitch())
		{
			GameUtils.LogoutConfirmation();
			return;
		}
		if (PlatformSettings.LocaleVariant == LocaleVariant.China)
		{
			SwitchRegion(constants.BnetRegion.REGION_CN, requestConfirmation: true);
			return;
		}
		RegionMenuSettings settings = CreateDefaultSettings();
		ShowRegionMenu(settings);
	}

	private bool ShouldSkipRegionSwitch()
	{
		ILoginService loginService = HearthstoneServices.Get<ILoginService>();
		if (loginService != null)
		{
			return !loginService.RequireRegionSwitchOnSwitchAccount();
		}
		return true;
	}

	private static RegionMenuSettings CreateDefaultSettings()
	{
		RegionMenuSettings result = default(RegionMenuSettings);
		result.CurrentRegion = BattleNet.GetCurrentRegion();
		result.Buttons = CreateDefaultRegionButtons();
		return result;
	}

	private static List<RegionMenuSettings.RegionButtonSetting> CreateDefaultRegionButtons()
	{
		return new List<RegionMenuSettings.RegionButtonSetting>(3)
		{
			new RegionMenuSettings.RegionButtonSetting
			{
				Region = constants.BnetRegion.REGION_US,
				ButtonLabel = "GLOBAL_REGION_AMERICAS"
			},
			new RegionMenuSettings.RegionButtonSetting
			{
				Region = constants.BnetRegion.REGION_EU,
				ButtonLabel = "GLOBAL_REGION_EUROPE"
			},
			new RegionMenuSettings.RegionButtonSetting
			{
				Region = constants.BnetRegion.REGION_KR,
				ButtonLabel = "GLOBAL_REGION_ASIA"
			}
		};
	}

	public void ShowRegionMenu(RegionMenuSettings settings)
	{
		if (!(m_regionMenu != null) || !m_regionMenu.IsShown())
		{
			AssetLoader.Get().InstantiatePrefab("RegionMenu.prefab:81394e6ea3adb1140a29ff4b44744891", OnMenuLoaded, settings);
		}
	}

	private void OnMenuLoaded(AssetReference assetRef, GameObject instance, object callbackData)
	{
		m_regionMenu = instance.GetComponent<RegionMenu>();
		if (m_regionMenu == null)
		{
			Log.Login.PrintError("Could not load Region Menu game object");
			Object.Destroy(instance);
		}
		else if (callbackData == null || !(callbackData is RegionMenuSettings))
		{
			Log.Login.PrintError("No region menu settings found");
			Object.Destroy(instance);
		}
		else
		{
			RegionMenuSettings menuButtonsAndShow = (RegionMenuSettings)callbackData;
			SetMenuButtonsAndShow(menuButtonsAndShow);
		}
	}

	private void SetMenuButtonsAndShow(RegionMenuSettings settings)
	{
		List<UIBButton> list = new List<UIBButton>(3);
		foreach (RegionMenuSettings.RegionButtonSetting buttonSettings in settings.Buttons)
		{
			list.Add(m_regionMenu.CreateMenuButton(null, buttonSettings.ButtonLabel, delegate
			{
				OnRegionButtonPressed(buttonSettings.Region, settings.CurrentRegion);
			}));
		}
		m_regionMenu.SetButtons(list);
		m_regionMenu.Show();
	}

	private void OnRegionButtonPressed(constants.BnetRegion selectedRegion, constants.BnetRegion currentRegion)
	{
		m_regionMenu.Hide();
		if (selectedRegion != currentRegion)
		{
			ShowRegionWarningDialog(selectedRegion);
		}
		else
		{
			SwitchRegion(selectedRegion, requestConfirmation: true);
		}
	}

	private void ShowRegionWarningDialog(constants.BnetRegion region)
	{
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_MOBILE_REGION_SELECT_WARNING_HEADER"),
			m_text = GameStrings.Get("GLUE_MOBILE_REGION_SELECT_WARNING"),
			m_showAlertIcon = false,
			m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
			m_responseCallback = OnRegionWarningResponse,
			m_padding = WARNING_PADDING,
			m_responseUserData = region
		};
		DialogManager.Get().ShowPopup(info, OnDialogProcess);
	}

	private bool OnDialogProcess(DialogBase dialog, object userData)
	{
		((GameObject)GameUtils.InstantiateGameObject("RegionSelect.prefab:a29650226d94fae408628b0c5aad1348", dialog.gameObject)).SetActive(value: true);
		return true;
	}

	private void OnRegionWarningResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CONFIRM)
		{
			constants.BnetRegion region = (constants.BnetRegion)userData;
			SwitchRegion(region, requestConfirmation: false);
		}
	}

	private void SwitchRegion(constants.BnetRegion region, bool requestConfirmation)
	{
		Options.Get().SetInt(Option.PREFERRED_REGION, (int)region);
		if (requestConfirmation)
		{
			GameUtils.LogoutConfirmation();
		}
		else
		{
			GameUtils.Logout();
		}
	}
}
