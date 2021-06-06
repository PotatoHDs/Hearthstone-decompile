using System;
using System.Collections.Generic;
using bgs;
using Hearthstone.Login;
using UnityEngine;

// Token: 0x02000372 RID: 882
public class RegionSwitchMenuController
{
	// Token: 0x060033E0 RID: 13280 RVA: 0x0010A308 File Offset: 0x00108508
	public void ShowRegionMenuWithDefaultSettings()
	{
		if (this.ShouldSkipRegionSwitch())
		{
			GameUtils.LogoutConfirmation();
			return;
		}
		if (PlatformSettings.LocaleVariant == LocaleVariant.China)
		{
			this.SwitchRegion(constants.BnetRegion.REGION_CN, true);
			return;
		}
		RegionSwitchMenuController.RegionMenuSettings settings = RegionSwitchMenuController.CreateDefaultSettings();
		this.ShowRegionMenu(settings);
	}

	// Token: 0x060033E1 RID: 13281 RVA: 0x0010A344 File Offset: 0x00108544
	private bool ShouldSkipRegionSwitch()
	{
		ILoginService loginService = HearthstoneServices.Get<ILoginService>();
		return loginService == null || !loginService.RequireRegionSwitchOnSwitchAccount();
	}

	// Token: 0x060033E2 RID: 13282 RVA: 0x0010A368 File Offset: 0x00108568
	private static RegionSwitchMenuController.RegionMenuSettings CreateDefaultSettings()
	{
		return new RegionSwitchMenuController.RegionMenuSettings
		{
			CurrentRegion = BattleNet.GetCurrentRegion(),
			Buttons = RegionSwitchMenuController.CreateDefaultRegionButtons()
		};
	}

	// Token: 0x060033E3 RID: 13283 RVA: 0x0010A398 File Offset: 0x00108598
	private static List<RegionSwitchMenuController.RegionMenuSettings.RegionButtonSetting> CreateDefaultRegionButtons()
	{
		return new List<RegionSwitchMenuController.RegionMenuSettings.RegionButtonSetting>(3)
		{
			new RegionSwitchMenuController.RegionMenuSettings.RegionButtonSetting
			{
				Region = constants.BnetRegion.REGION_US,
				ButtonLabel = "GLOBAL_REGION_AMERICAS"
			},
			new RegionSwitchMenuController.RegionMenuSettings.RegionButtonSetting
			{
				Region = constants.BnetRegion.REGION_EU,
				ButtonLabel = "GLOBAL_REGION_EUROPE"
			},
			new RegionSwitchMenuController.RegionMenuSettings.RegionButtonSetting
			{
				Region = constants.BnetRegion.REGION_KR,
				ButtonLabel = "GLOBAL_REGION_ASIA"
			}
		};
	}

	// Token: 0x060033E4 RID: 13284 RVA: 0x0010A414 File Offset: 0x00108614
	public void ShowRegionMenu(RegionSwitchMenuController.RegionMenuSettings settings)
	{
		if (this.m_regionMenu != null && this.m_regionMenu.IsShown())
		{
			return;
		}
		AssetLoader.Get().InstantiatePrefab("RegionMenu.prefab:81394e6ea3adb1140a29ff4b44744891", new PrefabCallback<GameObject>(this.OnMenuLoaded), settings, AssetLoadingOptions.None);
	}

	// Token: 0x060033E5 RID: 13285 RVA: 0x0010A468 File Offset: 0x00108668
	private void OnMenuLoaded(AssetReference assetRef, GameObject instance, object callbackData)
	{
		this.m_regionMenu = instance.GetComponent<RegionMenu>();
		if (this.m_regionMenu == null)
		{
			global::Log.Login.PrintError("Could not load Region Menu game object", Array.Empty<object>());
			UnityEngine.Object.Destroy(instance);
			return;
		}
		if (callbackData == null || !(callbackData is RegionSwitchMenuController.RegionMenuSettings))
		{
			global::Log.Login.PrintError("No region menu settings found", Array.Empty<object>());
			UnityEngine.Object.Destroy(instance);
			return;
		}
		RegionSwitchMenuController.RegionMenuSettings menuButtonsAndShow = (RegionSwitchMenuController.RegionMenuSettings)callbackData;
		this.SetMenuButtonsAndShow(menuButtonsAndShow);
	}

	// Token: 0x060033E6 RID: 13286 RVA: 0x0010A4E0 File Offset: 0x001086E0
	private void SetMenuButtonsAndShow(RegionSwitchMenuController.RegionMenuSettings settings)
	{
		List<UIBButton> list = new List<UIBButton>(3);
		using (List<RegionSwitchMenuController.RegionMenuSettings.RegionButtonSetting>.Enumerator enumerator = settings.Buttons.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				RegionSwitchMenuController.RegionMenuSettings.RegionButtonSetting buttonSettings = enumerator.Current;
				list.Add(this.m_regionMenu.CreateMenuButton(null, buttonSettings.ButtonLabel, delegate(UIEvent _)
				{
					this.OnRegionButtonPressed(buttonSettings.Region, settings.CurrentRegion);
				}));
			}
		}
		this.m_regionMenu.SetButtons(list);
		this.m_regionMenu.Show();
	}

	// Token: 0x060033E7 RID: 13287 RVA: 0x0010A5A0 File Offset: 0x001087A0
	private void OnRegionButtonPressed(constants.BnetRegion selectedRegion, constants.BnetRegion currentRegion)
	{
		this.m_regionMenu.Hide();
		if (selectedRegion != currentRegion)
		{
			this.ShowRegionWarningDialog(selectedRegion);
			return;
		}
		this.SwitchRegion(selectedRegion, true);
	}

	// Token: 0x060033E8 RID: 13288 RVA: 0x0010A5C4 File Offset: 0x001087C4
	private void ShowRegionWarningDialog(constants.BnetRegion region)
	{
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_MOBILE_REGION_SELECT_WARNING_HEADER"),
			m_text = GameStrings.Get("GLUE_MOBILE_REGION_SELECT_WARNING"),
			m_showAlertIcon = false,
			m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
			m_responseCallback = new AlertPopup.ResponseCallback(this.OnRegionWarningResponse),
			m_padding = RegionSwitchMenuController.WARNING_PADDING,
			m_responseUserData = region
		};
		DialogManager.Get().ShowPopup(info, new DialogManager.DialogProcessCallback(this.OnDialogProcess));
	}

	// Token: 0x060033E9 RID: 13289 RVA: 0x0010A64A File Offset: 0x0010884A
	private bool OnDialogProcess(DialogBase dialog, object userData)
	{
		((GameObject)GameUtils.InstantiateGameObject("RegionSelect.prefab:a29650226d94fae408628b0c5aad1348", dialog.gameObject, false)).SetActive(true);
		return true;
	}

	// Token: 0x060033EA RID: 13290 RVA: 0x0010A66C File Offset: 0x0010886C
	private void OnRegionWarningResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CONFIRM)
		{
			constants.BnetRegion region = (constants.BnetRegion)userData;
			this.SwitchRegion(region, false);
			return;
		}
	}

	// Token: 0x060033EB RID: 13291 RVA: 0x0010A68D File Offset: 0x0010888D
	private void SwitchRegion(constants.BnetRegion region, bool requestConfirmation)
	{
		Options.Get().SetInt(Option.PREFERRED_REGION, (int)region);
		if (requestConfirmation)
		{
			GameUtils.LogoutConfirmation();
			return;
		}
		GameUtils.Logout();
	}

	// Token: 0x04001C6B RID: 7275
	private RegionMenu m_regionMenu;

	// Token: 0x04001C6C RID: 7276
	private const int BUTTON_COUNT = 3;

	// Token: 0x04001C6D RID: 7277
	private const string WARNING_PREFAB = "RegionSelect.prefab:a29650226d94fae408628b0c5aad1348";

	// Token: 0x04001C6E RID: 7278
	private const string REGION_MENU_PREFAB = "RegionMenu.prefab:81394e6ea3adb1140a29ff4b44744891";

	// Token: 0x04001C6F RID: 7279
	private static readonly PlatformDependentValue<float> WARNING_PADDING = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 60f,
		Phone = 80f
	};

	// Token: 0x02001728 RID: 5928
	public struct RegionMenuSettings
	{
		// Token: 0x170014EE RID: 5358
		// (get) Token: 0x0600E748 RID: 59208 RVA: 0x00413E70 File Offset: 0x00412070
		// (set) Token: 0x0600E749 RID: 59209 RVA: 0x00413E78 File Offset: 0x00412078
		public List<RegionSwitchMenuController.RegionMenuSettings.RegionButtonSetting> Buttons { get; set; }

		// Token: 0x170014EF RID: 5359
		// (get) Token: 0x0600E74A RID: 59210 RVA: 0x00413E81 File Offset: 0x00412081
		// (set) Token: 0x0600E74B RID: 59211 RVA: 0x00413E89 File Offset: 0x00412089
		public constants.BnetRegion CurrentRegion { get; set; }

		// Token: 0x02002983 RID: 10627
		public struct RegionButtonSetting
		{
			// Token: 0x17002D88 RID: 11656
			// (get) Token: 0x06013F03 RID: 81667 RVA: 0x005416E1 File Offset: 0x0053F8E1
			// (set) Token: 0x06013F04 RID: 81668 RVA: 0x005416E9 File Offset: 0x0053F8E9
			public string ButtonLabel { get; set; }

			// Token: 0x17002D89 RID: 11657
			// (get) Token: 0x06013F05 RID: 81669 RVA: 0x005416F2 File Offset: 0x0053F8F2
			// (set) Token: 0x06013F06 RID: 81670 RVA: 0x005416FA File Offset: 0x0053F8FA
			public constants.BnetRegion Region { get; set; }
		}
	}
}
