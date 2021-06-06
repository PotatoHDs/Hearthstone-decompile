using System;
using bgs;
using Hearthstone;
using UnityEngine;

// Token: 0x02000B14 RID: 2836
public class GameMenuBase
{
	// Token: 0x060096D1 RID: 38609 RVA: 0x0030CCB0 File Offset: 0x0030AEB0
	public void ShowOptionsMenu()
	{
		if (this.m_hideCallback != null)
		{
			this.m_hideCallback();
		}
		if (this.m_optionsMenu == null)
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab("OptionsMenu.prefab:a6e5621068fd7c8429475b3e1a1aa991", AssetLoadingOptions.None);
			this.m_optionsMenu = gameObject.GetComponent<OptionsMenu>();
			if (this.m_optionsMenu != null)
			{
				this.SwitchToOptionsMenu();
				return;
			}
		}
		else
		{
			this.SwitchToOptionsMenu();
		}
	}

	// Token: 0x060096D2 RID: 38610 RVA: 0x0030CD1B File Offset: 0x0030AF1B
	public void DestroyOptionsMenu()
	{
		if (this.m_optionsMenu != null)
		{
			this.m_optionsMenu.RemoveHideHandler(new OptionsMenu.hideHandler(this.OnOptionsMenuHidden));
		}
	}

	// Token: 0x060096D3 RID: 38611 RVA: 0x0030CD44 File Offset: 0x0030AF44
	public bool UseKoreanRating()
	{
		if (SceneMgr.Get().IsInGame())
		{
			return false;
		}
		bool flag = BattleNet.GetAccountCountry() == "KOR";
		if (PlatformSettings.IsMobile() && !flag)
		{
			flag = (MobileDeviceLocale.GetCountryCode() == "KR");
		}
		return flag;
	}

	// Token: 0x060096D4 RID: 38612 RVA: 0x0030CD8A File Offset: 0x0030AF8A
	private void SwitchToOptionsMenu()
	{
		this.m_optionsMenu.SetHideHandler(new OptionsMenu.hideHandler(this.OnOptionsMenuHidden));
		this.m_optionsMenu.Show();
	}

	// Token: 0x060096D5 RID: 38613 RVA: 0x0030CDB0 File Offset: 0x0030AFB0
	private void OnOptionsMenuHidden()
	{
		UnityEngine.Object.Destroy(this.m_optionsMenu.gameObject);
		this.m_optionsMenu = null;
		if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.FATAL_ERROR) && !HearthstoneApplication.Get().IsResetting() && BnetBar.Get().AreButtonsEnabled() && this.m_showCallback != null)
		{
			this.m_showCallback();
		}
	}

	// Token: 0x04007E51 RID: 32337
	public GameMenuBase.ShowCallback m_showCallback;

	// Token: 0x04007E52 RID: 32338
	public GameMenuBase.HideCallback m_hideCallback;

	// Token: 0x04007E53 RID: 32339
	private const string OPTIONS_MENU_NAME = "OptionsMenu.prefab:a6e5621068fd7c8429475b3e1a1aa991";

	// Token: 0x04007E54 RID: 32340
	private OptionsMenu m_optionsMenu;

	// Token: 0x02002761 RID: 10081
	// (Invoke) Token: 0x060139C6 RID: 80326
	public delegate void ShowCallback();

	// Token: 0x02002762 RID: 10082
	// (Invoke) Token: 0x060139CA RID: 80330
	public delegate void HideCallback();
}
