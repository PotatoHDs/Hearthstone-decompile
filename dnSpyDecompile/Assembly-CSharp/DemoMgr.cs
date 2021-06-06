using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using UnityEngine;

// Token: 0x02000897 RID: 2199
public class DemoMgr : IService
{
	// Token: 0x060078A7 RID: 30887 RVA: 0x00275FA1 File Offset: 0x002741A1
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		HearthstoneApplication.Get().WillReset += this.WillReset;
		string text = this.GetStoredGameMode();
		if (text == null)
		{
			text = Vars.Key("Demo.Mode").GetStr("NONE");
		}
		this.SetModeFromString(text);
		this.WillReset();
		yield break;
	}

	// Token: 0x060078A8 RID: 30888 RVA: 0x00090064 File Offset: 0x0008E264
	public Type[] GetDependencies()
	{
		return null;
	}

	// Token: 0x060078A9 RID: 30889 RVA: 0x00275FB0 File Offset: 0x002741B0
	public void Shutdown()
	{
		DemoMgr.s_instance = null;
	}

	// Token: 0x060078AA RID: 30890 RVA: 0x00275FB8 File Offset: 0x002741B8
	private void WillReset()
	{
		if (this.m_mode == DemoMode.BLIZZ_MUSEUM)
		{
			this.ApplyBlizzMuseumDemoDefaults();
		}
	}

	// Token: 0x060078AB RID: 30891 RVA: 0x00275FC9 File Offset: 0x002741C9
	public static DemoMgr Get()
	{
		if (DemoMgr.s_instance == null)
		{
			DemoMgr.s_instance = HearthstoneServices.Get<DemoMgr>();
		}
		return DemoMgr.s_instance;
	}

	// Token: 0x060078AC RID: 30892 RVA: 0x00090064 File Offset: 0x0008E264
	private string GetStoredGameMode()
	{
		return null;
	}

	// Token: 0x060078AD RID: 30893 RVA: 0x00275FE1 File Offset: 0x002741E1
	public bool IsDemo()
	{
		return this.m_mode > DemoMode.NONE;
	}

	// Token: 0x060078AE RID: 30894 RVA: 0x00275FEC File Offset: 0x002741EC
	public bool IsExpoDemo()
	{
		DemoMode mode = this.m_mode;
		return mode - DemoMode.PAX_EAST_2013 <= 11;
	}

	// Token: 0x060078AF RID: 30895 RVA: 0x0027600C File Offset: 0x0027420C
	public bool IsSocialEnabled()
	{
		switch (this.m_mode)
		{
		case DemoMode.BLIZZCON_2013:
		case DemoMode.BLIZZCON_2015:
		case DemoMode.BLIZZ_MUSEUM:
		case DemoMode.BLIZZCON_2017_ADVENTURE:
		case DemoMode.BLIZZCON_2017_BRAWL:
		case DemoMode.BLIZZCON_2018_BRAWL:
		case DemoMode.BLIZZCON_2019_BATTLEGROUNDS:
			return false;
		}
		return true;
	}

	// Token: 0x060078B0 RID: 30896 RVA: 0x00276058 File Offset: 0x00274258
	public bool IsCurrencyEnabled()
	{
		DemoMode mode = this.m_mode;
		return mode - DemoMode.BLIZZCON_2013 > 2 && mode - DemoMode.ANNOUNCEMENT_5_0 > 5;
	}

	// Token: 0x060078B1 RID: 30897 RVA: 0x0027607C File Offset: 0x0027427C
	public bool IsHubEscMenuEnabled(bool enabledInGameplay)
	{
		switch (this.m_mode)
		{
		case DemoMode.BLIZZCON_2013:
		case DemoMode.BLIZZCON_2014:
		case DemoMode.BLIZZCON_2015:
		case DemoMode.ANNOUNCEMENT_5_0:
		case DemoMode.BLIZZCON_2016:
		case DemoMode.BLIZZCON_2017_ADVENTURE:
		case DemoMode.BLIZZCON_2017_BRAWL:
		case DemoMode.BLIZZCON_2018_BRAWL:
		case DemoMode.BLIZZCON_2019_BATTLEGROUNDS:
			return enabledInGameplay;
		case DemoMode.BLIZZ_MUSEUM:
			return false;
		default:
			return true;
		}
	}

	// Token: 0x060078B2 RID: 30898 RVA: 0x002760C8 File Offset: 0x002742C8
	public bool CantExitArena()
	{
		DemoMode mode = this.m_mode;
		return mode == DemoMode.BLIZZCON_2013;
	}

	// Token: 0x060078B3 RID: 30899 RVA: 0x002760E4 File Offset: 0x002742E4
	public bool ArenaIs1WinMode()
	{
		DemoMode mode = this.m_mode;
		return mode == DemoMode.BLIZZCON_2013;
	}

	// Token: 0x060078B4 RID: 30900 RVA: 0x00276100 File Offset: 0x00274300
	public bool CanRestartMissions()
	{
		DemoMode mode = this.m_mode;
		return mode != DemoMode.BLIZZCON_2017_ADVENTURE;
	}

	// Token: 0x060078B5 RID: 30901 RVA: 0x0027611C File Offset: 0x0027431C
	public bool ShouldShowWelcomeQuests()
	{
		DemoMode mode = this.m_mode;
		return mode - DemoMode.BLIZZCON_2013 > 2 && mode - DemoMode.ANNOUNCEMENT_5_0 > 5;
	}

	// Token: 0x060078B6 RID: 30902 RVA: 0x00276140 File Offset: 0x00274340
	public bool IsHeroClassPlayable(TAG_CLASS heroClass)
	{
		DemoMode mode = this.m_mode;
		return mode != DemoMode.BLIZZCON_2017_ADVENTURE || heroClass == TAG_CLASS.MAGE || heroClass == TAG_CLASS.WARRIOR || heroClass == TAG_CLASS.PRIEST;
	}

	// Token: 0x060078B7 RID: 30903 RVA: 0x0027616A File Offset: 0x0027436A
	public DemoMode GetMode()
	{
		return this.m_mode;
	}

	// Token: 0x060078B8 RID: 30904 RVA: 0x00276172 File Offset: 0x00274372
	public void SetMode(DemoMode mode)
	{
		this.m_mode = mode;
	}

	// Token: 0x060078B9 RID: 30905 RVA: 0x0027617B File Offset: 0x0027437B
	public void SetModeFromString(string modeString)
	{
		this.m_mode = this.GetModeFromString(modeString);
	}

	// Token: 0x060078BA RID: 30906 RVA: 0x0027618C File Offset: 0x0027438C
	public DemoMode GetModeFromString(string modeString)
	{
		DemoMode result;
		try
		{
			result = EnumUtils.GetEnum<DemoMode>(modeString, StringComparison.OrdinalIgnoreCase);
		}
		catch (Exception)
		{
			result = DemoMode.NONE;
		}
		return result;
	}

	// Token: 0x060078BB RID: 30907 RVA: 0x002761BC File Offset: 0x002743BC
	public void CreateDemoText(string demoText)
	{
		this.CreateDemoText(demoText, false, false);
	}

	// Token: 0x060078BC RID: 30908 RVA: 0x002761C7 File Offset: 0x002743C7
	public void CreateDemoText(string demoText, bool unclickable)
	{
		this.CreateDemoText(demoText, unclickable, false);
	}

	// Token: 0x060078BD RID: 30909 RVA: 0x002761D4 File Offset: 0x002743D4
	public void CreateDemoText(string demoText, bool unclickable, bool shouldDoArenaInstruction)
	{
		if (this.m_demoText != null)
		{
			return;
		}
		this.m_shouldGiveArenaInstruction = shouldDoArenaInstruction;
		this.m_nextTipUnclickable = unclickable;
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("DemoText.prefab:5749aead2db66ce4d958e44bab4a5219", AssetLoadingOptions.None);
		OverlayUI.Get().AddGameObject(gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
		this.m_demoText = gameObject.GetComponent<Notification>();
		this.m_demoText.ChangeText(demoText);
		UniversalInputManager.Get().SetSystemDialogActive(true);
		gameObject.transform.GetComponentInChildren<PegUIElement>().AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.RemoveDemoTextDialog));
		if (this.m_nextTipUnclickable)
		{
			this.m_nextTipUnclickable = false;
			this.MakeDemoTextClickable(false);
		}
	}

	// Token: 0x060078BE RID: 30910 RVA: 0x00276279 File Offset: 0x00274479
	public void ChangeDemoText(string demoText)
	{
		this.m_demoText.ChangeText(demoText);
	}

	// Token: 0x060078BF RID: 30911 RVA: 0x00276287 File Offset: 0x00274487
	public void NextDemoTipIsNewArenaMatch()
	{
		this.m_nextDemoTipIsNewArenaMatch = true;
	}

	// Token: 0x060078C0 RID: 30912 RVA: 0x00276290 File Offset: 0x00274490
	private void DemoTextLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_demoText = go.GetComponent<Notification>();
		this.m_demoText.ChangeText((string)callbackData);
		UniversalInputManager.Get().SetSystemDialogActive(true);
		go.transform.GetComponentInChildren<PegUIElement>().AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.RemoveDemoTextDialog));
		if (this.m_nextTipUnclickable)
		{
			this.m_nextTipUnclickable = false;
			this.MakeDemoTextClickable(false);
		}
	}

	// Token: 0x060078C1 RID: 30913 RVA: 0x002762F9 File Offset: 0x002744F9
	private void RemoveDemoTextDialog(UIEvent e)
	{
		this.RemoveDemoTextDialog();
	}

	// Token: 0x060078C2 RID: 30914 RVA: 0x00276304 File Offset: 0x00274504
	public void RemoveDemoTextDialog()
	{
		UniversalInputManager.Get().SetSystemDialogActive(false);
		UnityEngine.Object.DestroyImmediate(this.m_demoText.gameObject);
		if (this.m_shouldGiveArenaInstruction)
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_FORGE_INST1_19"), "VO_INNKEEPER_FORGE_INST1_19.prefab:a0e06e90b545b274290dad8e442e83d0", 3f, null, false);
			this.m_shouldGiveArenaInstruction = false;
		}
		if (this.m_nextDemoTipIsNewArenaMatch)
		{
			this.m_nextDemoTipIsNewArenaMatch = false;
			this.CreateDemoText(GameStrings.Get("GLUE_BLIZZCON2013_ARENA"), false, true);
		}
	}

	// Token: 0x060078C3 RID: 30915 RVA: 0x00276380 File Offset: 0x00274580
	public void MakeDemoTextClickable(bool clickable)
	{
		if (!clickable)
		{
			this.m_demoText.transform.GetComponentInChildren<BoxCollider>().enabled = false;
			this.m_demoText.transform.Find("continue").gameObject.SetActive(false);
			return;
		}
		this.m_demoText.transform.GetComponentInChildren<BoxCollider>().enabled = true;
		this.m_demoText.transform.Find("continue").gameObject.SetActive(true);
	}

	// Token: 0x060078C4 RID: 30916 RVA: 0x002763FD File Offset: 0x002745FD
	public void ApplyBlizzMuseumDemoDefaults()
	{
		Options.Get().SetBool(Option.CONNECT_TO_AURORA, false);
		Options.Get().SetBool(Option.HAS_SEEN_NEW_CINEMATIC, true);
		Options.Get().SetEnum<TutorialProgress>(Option.LOCAL_TUTORIAL_PROGRESS, TutorialProgress.NOTHING_COMPLETE);
	}

	// Token: 0x060078C5 RID: 30917 RVA: 0x00276426 File Offset: 0x00274626
	public IEnumerator CompleteBlizzMuseumDemo()
	{
		yield return new WaitForSeconds(3f);
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_DEMO_COMPLETE_HEADER");
		popupInfo.m_text = GameStrings.Get("GLOBAL_DEMO_COMPLETE_BODY");
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
		{
			HearthstoneApplication.Get().Reset();
		};
		DialogManager.Get().ShowPopup(popupInfo);
		yield break;
	}

	// Token: 0x04005E27 RID: 24103
	private static DemoMgr s_instance;

	// Token: 0x04005E28 RID: 24104
	private DemoMode m_mode;

	// Token: 0x04005E29 RID: 24105
	private Notification m_demoText;

	// Token: 0x04005E2A RID: 24106
	private bool m_shouldGiveArenaInstruction;

	// Token: 0x04005E2B RID: 24107
	private bool m_nextTipUnclickable;

	// Token: 0x04005E2C RID: 24108
	private bool m_nextDemoTipIsNewArenaMatch;

	// Token: 0x04005E2D RID: 24109
	private const bool LOAD_STORED_SETTING = false;
}
