using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bgs;
using bgs.types;
using Blizzard.T5.Jobs;
using Hearthstone;
using Hearthstone.Core;
using Hearthstone.Login;
using PegasusShared;
using UnityEngine;

// Token: 0x0200073E RID: 1854
public class TemporaryAccountManager
{
	// Token: 0x060068ED RID: 26861 RVA: 0x00222E18 File Offset: 0x00221018
	public static bool IsTemporaryAccount()
	{
		bool result;
		if (HearthstoneApplication.IsInternal() && Options.Get().HasOption(Option.IS_TEMPORARY_ACCOUNT_CHEAT))
		{
			result = Options.Get().GetBool(Option.IS_TEMPORARY_ACCOUNT_CHEAT);
		}
		else
		{
			result = BattleNet.IsHeadlessAccount();
		}
		return result;
	}

	// Token: 0x060068EE RID: 26862 RVA: 0x00222E50 File Offset: 0x00221050
	public static TemporaryAccountManager Get()
	{
		if (TemporaryAccountManager.s_Instance == null)
		{
			TemporaryAccountManager.s_Instance = new TemporaryAccountManager();
		}
		return TemporaryAccountManager.s_Instance;
	}

	// Token: 0x060068EF RID: 26863 RVA: 0x00222E68 File Offset: 0x00221068
	public void Initialize()
	{
		HearthstoneApplication.Get().WillReset += this.WillReset;
		if (TemporaryAccountManager.IsTemporaryAccount())
		{
			Processor.QueueJob("TemporaryAccountManager.AddFakeBooster", this.Job_AddFakeBooster(), JobFlags.StartImmediately, Array.Empty<IJobDependency>());
		}
	}

	// Token: 0x060068F0 RID: 26864 RVA: 0x00222E9E File Offset: 0x0022109E
	private IEnumerator<IAsyncJobResult> Job_AddFakeBooster()
	{
		yield return new WaitForNetCacheObject<NetCache.NetCacheBoosters>();
		NetCache.NetCacheBoosters netObject = NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>();
		int id = 18;
		NetCache.BoosterStack item = new NetCache.BoosterStack
		{
			Id = id,
			Count = 1
		};
		netObject.BoosterStacks.Add(item);
		yield break;
	}

	// Token: 0x060068F1 RID: 26865 RVA: 0x00222EA6 File Offset: 0x002210A6
	public void WillReset()
	{
		HearthstoneApplication.Get().WillReset -= this.WillReset;
		BnetPresenceMgr.Get().OnGameAccountPresenceChange -= this.OnPresenceChanged;
	}

	// Token: 0x060068F2 RID: 26866 RVA: 0x00222ED4 File Offset: 0x002210D4
	public void LoadTemporaryAccountData()
	{
		if (this.m_isTemporaryAccountDataLoaded)
		{
			return;
		}
		this.m_temporaryAccountData = null;
		this.m_isTemporaryAccountDataLoaded = true;
	}

	// Token: 0x060068F3 RID: 26867 RVA: 0x00222EED File Offset: 0x002210ED
	public TemporaryAccountManager.TemporaryAccountData GetTemporaryAccountData()
	{
		this.LoadTemporaryAccountData();
		return this.m_temporaryAccountData;
	}

	// Token: 0x060068F4 RID: 26868 RVA: 0x00222EFC File Offset: 0x002210FC
	public string GetSelectedTemporaryAccountId()
	{
		global::Log.TemporaryAccount.Print("Get selected Temporary Account Id", Array.Empty<object>());
		if (!this.m_isTemporaryAccountDataLoaded)
		{
			this.LoadTemporaryAccountData();
		}
		if (this.m_temporaryAccountData == null)
		{
			global::Log.TemporaryAccount.PrintWarning("Unable to load temporary account data!", Array.Empty<object>());
			return null;
		}
		if (this.m_temporaryAccountData.m_selectedTemporaryAccountIndex == -1)
		{
			global::Log.TemporaryAccount.PrintWarning("No selected temporary account!", Array.Empty<object>());
			return null;
		}
		string temporaryAccountId = this.m_temporaryAccountData.m_temporaryAccounts[this.m_temporaryAccountData.m_selectedTemporaryAccountIndex].m_temporaryAccountId;
		if (!string.IsNullOrEmpty(temporaryAccountId))
		{
			return temporaryAccountId;
		}
		return null;
	}

	// Token: 0x060068F5 RID: 26869 RVA: 0x00222F9C File Offset: 0x0022119C
	public void CreatedTemporaryAccount(string temporaryAccountId)
	{
		global::Log.TemporaryAccount.Print("Created Temporary Account with Id: " + temporaryAccountId, Array.Empty<object>());
		if (!string.IsNullOrEmpty(this.m_createdTemporaryAccountId))
		{
			global::Log.TemporaryAccount.PrintWarning("Replacing previous created Temporary Account Id! Previous Id = " + this.m_createdTemporaryAccountId, Array.Empty<object>());
		}
		this.m_createdTemporaryAccountId = temporaryAccountId;
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		if (myPlayer != null)
		{
			this.AddCreatedTemporaryAccount(myPlayer.GetBattleTag().GetName());
			return;
		}
		BnetPresenceMgr.Get().OnGameAccountPresenceChange -= this.OnPresenceChanged;
		BnetPresenceMgr.Get().OnGameAccountPresenceChange += this.OnPresenceChanged;
	}

	// Token: 0x060068F6 RID: 26870 RVA: 0x00223042 File Offset: 0x00221242
	public void UpdateTemporaryAccountData()
	{
		if (!TemporaryAccountManager.IsTemporaryAccount())
		{
			return;
		}
		CloudStorageManager.Get().StartInitialize(new CloudStorageManager.OnInitializedFinished(this.OnCloudStorageInitializedUpdateTemporaryAccountData), GameStrings.Get("GLUE_CLOUD_STORAGE_CONTEXT_BODY_01"));
	}

	// Token: 0x060068F7 RID: 26871 RVA: 0x0022306C File Offset: 0x0022126C
	public void HealUpSelectedTemporaryAccount()
	{
		global::Log.TemporaryAccount.Print("Heal Up Selected Temporary Account", Array.Empty<object>());
		InGameBrowserManager.Get().HideAllButtons();
		if (!this.m_isTemporaryAccountDataLoaded)
		{
			this.LoadTemporaryAccountData();
		}
		if (this.m_temporaryAccountData == null)
		{
			global::Log.TemporaryAccount.PrintWarning("Unable to load temporary account data!", Array.Empty<object>());
			return;
		}
		if (this.m_temporaryAccountData.m_selectedTemporaryAccountIndex == -1)
		{
			global::Log.TemporaryAccount.PrintWarning("No selected temporary account!", Array.Empty<object>());
			return;
		}
		this.m_temporaryAccountData.m_temporaryAccounts[this.m_temporaryAccountData.m_selectedTemporaryAccountIndex].m_isHealedUp = true;
		this.m_temporaryAccountData.m_temporaryAccounts[this.m_temporaryAccountData.m_selectedTemporaryAccountIndex].m_isMinor = false;
		this.m_temporaryAccountData.m_selectedTemporaryAccountIndex = -1;
		this.SaveTemporaryAccountData();
		Options.Get().SetBool(Option.CREATED_ACCOUNT, true);
		Options.Get().DeleteOption(Option.LAST_HEAL_UP_EVENT_DATE);
	}

	// Token: 0x060068F8 RID: 26872 RVA: 0x00223154 File Offset: 0x00221354
	public void SetSelectedTemporaryAccountAsMinor()
	{
		global::Log.TemporaryAccount.Print("Set Temporary Account as Minor", Array.Empty<object>());
		if (!this.m_isTemporaryAccountDataLoaded)
		{
			this.LoadTemporaryAccountData();
		}
		if (this.m_temporaryAccountData == null)
		{
			global::Log.TemporaryAccount.PrintWarning("Unable to load temporary account data!", Array.Empty<object>());
			return;
		}
		if (this.m_temporaryAccountData.m_selectedTemporaryAccountIndex == -1)
		{
			global::Log.TemporaryAccount.PrintWarning("No selected temporary account!", Array.Empty<object>());
			return;
		}
		this.m_temporaryAccountData.m_temporaryAccounts[this.m_temporaryAccountData.m_selectedTemporaryAccountIndex].m_isMinor = true;
		this.SaveTemporaryAccountData();
	}

	// Token: 0x060068F9 RID: 26873 RVA: 0x002231EA File Offset: 0x002213EA
	public void DeleteTemporaryAccountData()
	{
		global::Log.TemporaryAccount.PrintWarning("Deleting Temporary Account Data!", Array.Empty<object>());
		this.m_temporaryAccountData = new TemporaryAccountManager.TemporaryAccountData();
		Options.Get().DeleteOption(Option.TEMPORARY_ACCOUNT_DATA);
		CloudStorageManager.Get().RemoveObject("TEMPORARY_ACCOUNT_DATA");
	}

	// Token: 0x060068FA RID: 26874 RVA: 0x00223228 File Offset: 0x00221428
	public void SetSelectedTemporaryAccount(string temporaryAccountId)
	{
		global::Log.TemporaryAccount.Print("Set Selected Temporary Account", Array.Empty<object>());
		if (!this.m_isTemporaryAccountDataLoaded)
		{
			this.LoadTemporaryAccountData();
		}
		if (this.m_temporaryAccountData == null)
		{
			global::Log.TemporaryAccount.PrintWarning("Unable to load temporary account data!", Array.Empty<object>());
			return;
		}
		for (int i = 0; i < this.m_temporaryAccountData.m_temporaryAccounts.Count; i++)
		{
			if (this.m_temporaryAccountData.m_temporaryAccounts[i].m_temporaryAccountId == temporaryAccountId)
			{
				this.m_temporaryAccountData.m_selectedTemporaryAccountIndex = i;
				break;
			}
		}
		if (this.m_temporaryAccountData.m_selectedTemporaryAccountIndex == this.m_temporaryAccountData.m_temporaryAccounts.Count)
		{
			global::Log.TemporaryAccount.PrintError("Unable to find temporary account Id!", Array.Empty<object>());
			this.m_temporaryAccountData.m_selectedTemporaryAccountIndex = -1;
		}
		this.SaveTemporaryAccountData();
	}

	// Token: 0x060068FB RID: 26875 RVA: 0x00223300 File Offset: 0x00221500
	public void UnselectTemporaryAccount()
	{
		global::Log.TemporaryAccount.Print("Unselect Selected Temporary Account (if any)", Array.Empty<object>());
		if (!this.m_isTemporaryAccountDataLoaded)
		{
			this.LoadTemporaryAccountData();
		}
		if (this.m_temporaryAccountData == null)
		{
			global::Log.TemporaryAccount.PrintWarning("Unable to load temporary account data!", Array.Empty<object>());
			return;
		}
		this.m_lastLoginSelectedTemporaryAccountIndex = this.m_temporaryAccountData.m_selectedTemporaryAccountIndex;
		this.m_temporaryAccountData.m_selectedTemporaryAccountIndex = -1;
		this.SaveTemporaryAccountData();
	}

	// Token: 0x060068FC RID: 26876 RVA: 0x00223370 File Offset: 0x00221570
	public bool IsSelectedTemporaryAccountMinor()
	{
		global::Log.TemporaryAccount.Print("Is Selected Temporary Account a Minor", Array.Empty<object>());
		if (!this.m_isTemporaryAccountDataLoaded)
		{
			this.LoadTemporaryAccountData();
		}
		if (this.m_temporaryAccountData == null)
		{
			global::Log.TemporaryAccount.PrintWarning("Unable to load temporary account data!", Array.Empty<object>());
			return false;
		}
		if (this.m_temporaryAccountData.m_selectedTemporaryAccountIndex == -1)
		{
			global::Log.TemporaryAccount.PrintWarning("No selected temporary account!", Array.Empty<object>());
			return false;
		}
		return this.m_temporaryAccountData.m_temporaryAccounts[this.m_temporaryAccountData.m_selectedTemporaryAccountIndex].m_isMinor;
	}

	// Token: 0x060068FD RID: 26877 RVA: 0x00223404 File Offset: 0x00221604
	public bool ShowHealUpDialog(string header, string body, TemporaryAccountManager.HealUpReason reason, bool userTriggered, TemporaryAccountManager.OnHealUpDialogDismissed onSignUpHandler)
	{
		TemporaryAccountSignUpPopUp.PopupTextParameters popupArgs = new TemporaryAccountSignUpPopUp.PopupTextParameters
		{
			Header = header,
			Body = body
		};
		return this.ShowHealUpDialog(popupArgs, reason, userTriggered, onSignUpHandler);
	}

	// Token: 0x060068FE RID: 26878 RVA: 0x00223438 File Offset: 0x00221638
	public bool ShowHealUpDialog(TemporaryAccountSignUpPopUp.PopupTextParameters popupArgs, TemporaryAccountManager.HealUpReason reason, bool userTriggered, TemporaryAccountManager.OnHealUpDialogDismissed onSignUpHandler)
	{
		if (!TemporaryAccountManager.IsTemporaryAccount())
		{
			return false;
		}
		if (!userTriggered)
		{
			long @long = Options.Get().GetLong(Option.LAST_HEAL_UP_EVENT_DATE);
			DateTime now = DateTime.Now;
			if (@long != 0L)
			{
				int totalWins = this.GetTotalWins();
				DateTime d = new DateTime(@long);
				TimeSpan timeSpan = now - d;
				int num = 1;
				foreach (int num2 in TemporaryAccountManager.HEAL_UP_FREQUENCY.Keys)
				{
					if (totalWins >= num2)
					{
						num = TemporaryAccountManager.HEAL_UP_FREQUENCY[num2];
					}
				}
				if (timeSpan.TotalHours < (double)num)
				{
					return false;
				}
			}
			Options.Get().SetLong(Option.LAST_HEAL_UP_EVENT_DATE, now.Ticks);
		}
		this.m_signUpReason = reason;
		this.m_onSignUpDismissedHandler = onSignUpHandler;
		if (this.m_signUpPopUp == null)
		{
			if (!this.m_isSignUpPopUpLoading)
			{
				this.m_isSignUpPopUpLoading = true;
				AssetLoader.Get().InstantiatePrefab("TemporaryAccountSignUp.prefab:14791f0c7af5c6f4480fc78ab36c81bc", new PrefabCallback<GameObject>(this.ShowSignUpPopUp), null, AssetLoadingOptions.None);
			}
			this.m_popupArgs = popupArgs;
			return true;
		}
		this.m_signUpPopUp.Show(popupArgs, new TemporaryAccountSignUpPopUp.OnSignUpPopUpBack(this.OnHealUpProcessCancelled));
		return true;
	}

	// Token: 0x060068FF RID: 26879 RVA: 0x00223570 File Offset: 0x00221770
	public bool ShowEarnCardEventHealUpDialog(TemporaryAccountManager.HealUpReason reason)
	{
		return this.ShowHealUpDialog(GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_HEADER_03"), GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_BODY_01"), reason, false, null);
	}

	// Token: 0x06006900 RID: 26880 RVA: 0x0022358F File Offset: 0x0022178F
	public void ShowHealUpPage(TemporaryAccountManager.HealUpReason reason, Action<bool> onDismissed = null)
	{
		this.m_signUpReason = reason;
		this.ShowHealUpPage(onDismissed);
	}

	// Token: 0x06006901 RID: 26881 RVA: 0x002235A0 File Offset: 0x002217A0
	public void ShowHealUpPage(Action<bool> onDismissed = null)
	{
		ILoginService loginService = HearthstoneServices.Get<ILoginService>();
		if (loginService != null && loginService.SupportsAccountHealup())
		{
			global::Log.TemporaryAccount.PrintDebug("Using Login Service for account heal up", Array.Empty<object>());
			loginService.HealupCurrentTemporaryAccount(onDismissed);
			return;
		}
		this.m_healUpTemporaryAccountGameAccountId = BattleNet.GetMyGameAccountId().lo;
		this.m_handledDisconnect = false;
		this.m_handledHealUpError = false;
		Network.Get().AddBnetErrorListener(new Network.BnetErrorCallback(this.OnBnetError));
		NetCache.Get().DispatchClientOptionsToServer();
		string accountHealUpLink;
		if (this.m_signUpReason == TemporaryAccountManager.HealUpReason.WIN_GAME)
		{
			accountHealUpLink = ExternalUrlService.Get().GetAccountHealUpLink(this.m_signUpReason, this.GetTotalWins());
		}
		else
		{
			accountHealUpLink = ExternalUrlService.Get().GetAccountHealUpLink(this.m_signUpReason, 0);
		}
		global::Log.TemporaryAccount.Print("Opening URL: " + accountHealUpLink, Array.Empty<object>());
		InGameBrowserManager.Get().SetUrl(accountHealUpLink);
		InGameBrowserManager.Get().Show(new InGameBrowserManager.BrowserClosedHandler(this.OnHealUpPageClosed));
		this.m_signUpReason = TemporaryAccountManager.HealUpReason.UNKNOWN;
	}

	// Token: 0x06006902 RID: 26882 RVA: 0x00223690 File Offset: 0x00221890
	public void OnHealUpPageClosed()
	{
		global::Log.TemporaryAccount.Print("Heal Up Page Closed", Array.Empty<object>());
		this.m_healUpTemporaryAccountGameAccountId = 0UL;
		this.m_handledDisconnect = false;
		this.m_handledHealUpError = false;
		Network.Get().RemoveBnetErrorListener(new Network.BnetErrorCallback(this.OnBnetError));
		this.OnHealUpProcessCancelled();
	}

	// Token: 0x06006903 RID: 26883 RVA: 0x002236E4 File Offset: 0x002218E4
	public void HealUpComplete()
	{
		global::Log.TemporaryAccount.Print(string.Concat(new string[]
		{
			"Heal Up Complete - m_handledDisconnect = ",
			this.m_handledDisconnect.ToString(),
			" m_handledHealUpError = ",
			this.m_handledHealUpError.ToString(),
			" Heal Up Already Complete:",
			this.m_healUpComplete.ToString()
		}), Array.Empty<object>());
		if (this.m_healUpComplete)
		{
			return;
		}
		this.m_healUpTemporaryAccountGameAccountId = 0UL;
		this.m_healUpComplete = true;
		InGameBrowserManager.Get().Hide();
		BattleNet.RequestCloseAurora();
		HearthstoneApplication.Get().ResetAndForceLogin();
	}

	// Token: 0x06006904 RID: 26884 RVA: 0x0022377E File Offset: 0x0022197E
	public void StartShowSwitchAccountMenu(SwitchAccountMenu.OnSwitchAccountLogInPressed handler, bool disableInputBlocker)
	{
		this.m_onSwithAccountLogInPressedHandler = handler;
		this.m_disableSwitchAccountMenuInputBlocker = disableInputBlocker;
		CloudStorageManager.Get().StartInitialize(new CloudStorageManager.OnInitializedFinished(this.OnCloudStorageInitializedStartShowSwitchAccountMenu), GameStrings.Get("GLUE_CLOUD_STORAGE_CONTEXT_BODY_02"));
	}

	// Token: 0x06006905 RID: 26885 RVA: 0x002237AE File Offset: 0x002219AE
	public void ShowSwitchAccountMenu(SwitchAccountMenu.OnSwitchAccountLogInPressed handler, bool disableInputBlocker)
	{
		this.m_onSwithAccountLogInPressedHandler = handler;
		this.m_disableSwitchAccountMenuInputBlocker = disableInputBlocker;
		this.ShowSwitchAccountMenu();
	}

	// Token: 0x06006906 RID: 26886 RVA: 0x002237C4 File Offset: 0x002219C4
	public void ShowSwitchAccountMenu()
	{
		if (this.m_switchAccountMenu)
		{
			if (!this.m_switchAccountMenu.IsShown())
			{
				this.m_switchAccountMenu.Show(this.m_onSwithAccountLogInPressedHandler);
				if (this.m_disableSwitchAccountMenuInputBlocker)
				{
					this.m_switchAccountMenu.DisableInputBlocker();
				}
			}
			this.m_onSwithAccountLogInPressedHandler = null;
			this.m_disableSwitchAccountMenuInputBlocker = false;
			return;
		}
		if (this.m_isSwitchAccountMenuLoading)
		{
			return;
		}
		this.m_isSwitchAccountMenuLoading = true;
		AssetLoader.Get().InstantiatePrefab("SwitchAccountMenu.prefab:bca3c7466980f484fbf25690f6cef4bf", new PrefabCallback<GameObject>(this.OnSwitchAccountMenuLoaded), null, AssetLoadingOptions.None);
	}

	// Token: 0x06006907 RID: 26887 RVA: 0x00223851 File Offset: 0x00221A51
	public ulong GetHealUpTemporaryAccountGameAccountId()
	{
		return this.m_healUpTemporaryAccountGameAccountId;
	}

	// Token: 0x06006908 RID: 26888 RVA: 0x00223859 File Offset: 0x00221A59
	public int GetLastLoginSelectedTemporaryAccountIndex()
	{
		return this.m_lastLoginSelectedTemporaryAccountIndex;
	}

	// Token: 0x06006909 RID: 26889 RVA: 0x00223861 File Offset: 0x00221A61
	public void LoginTemporaryAccount(TemporaryAccountManager.TemporaryAccountData.TemporaryAccount temporaryAccount)
	{
		global::Log.TemporaryAccount.Print("Login Temporary Account", Array.Empty<object>());
		TemporaryAccountManager.Get().SetSelectedTemporaryAccount(temporaryAccount.m_temporaryAccountId);
		Options.Get().SetInt(Option.PREFERRED_REGION, temporaryAccount.m_regionId);
		GameUtils.Logout();
	}

	// Token: 0x0600690A RID: 26890 RVA: 0x002238A0 File Offset: 0x00221AA0
	public void LoginTemporaryAccount(int selectedTemporaryAccountIndex)
	{
		if (!this.m_isTemporaryAccountDataLoaded)
		{
			this.LoadTemporaryAccountData();
		}
		if (this.m_temporaryAccountData == null)
		{
			global::Log.TemporaryAccount.PrintWarning("Unable to load temporary account data!", Array.Empty<object>());
			return;
		}
		if (selectedTemporaryAccountIndex == -1 || selectedTemporaryAccountIndex >= this.m_temporaryAccountData.m_temporaryAccounts.Count)
		{
			global::Log.TemporaryAccount.PrintError("Invalid selected Temporary Account index! Index = " + selectedTemporaryAccountIndex, Array.Empty<object>());
			return;
		}
		TemporaryAccountManager.TemporaryAccountData.TemporaryAccount temporaryAccount = this.m_temporaryAccountData.m_temporaryAccounts[selectedTemporaryAccountIndex];
		this.LoginTemporaryAccount(temporaryAccount);
	}

	// Token: 0x0600690B RID: 26891 RVA: 0x00223928 File Offset: 0x00221B28
	public void PrintTemporaryAccountData()
	{
		if (!this.m_isTemporaryAccountDataLoaded)
		{
			this.LoadTemporaryAccountData();
		}
		if (this.m_temporaryAccountData == null)
		{
			global::Log.TemporaryAccount.Print("m_temporaryAccountData == null", Array.Empty<object>());
			return;
		}
		global::Log.TemporaryAccount.Print(string.Concat(new object[]
		{
			"Selected Account = ",
			this.m_temporaryAccountData.m_selectedTemporaryAccountIndex,
			", m_lastUpdate = ",
			Convert.ToDateTime(this.m_temporaryAccountData.m_lastUpdated)
		}), Array.Empty<object>());
		foreach (TemporaryAccountManager.TemporaryAccountData.TemporaryAccount temporaryAccount in this.m_temporaryAccountData.m_temporaryAccounts)
		{
			global::Log.TemporaryAccount.Print(string.Concat(new object[]
			{
				"[m_temporaryAccountId = ",
				temporaryAccount.m_temporaryAccountId,
				", m_battleTag = ",
				temporaryAccount.m_battleTag,
				", m_regionId = ",
				temporaryAccount.m_regionId,
				", m_lastLogin = ",
				Convert.ToDateTime(temporaryAccount.m_lastLogin),
				", m_isHealedUp = ",
				temporaryAccount.m_isHealedUp.ToString(),
				", m_isMinor = ",
				temporaryAccount.m_isMinor.ToString(),
				"]"
			}), Array.Empty<object>());
		}
		this.SortPrint();
	}

	// Token: 0x0600690C RID: 26892 RVA: 0x00223AAC File Offset: 0x00221CAC
	public void Test()
	{
		if (!this.m_isTemporaryAccountDataLoaded)
		{
			this.LoadTemporaryAccountData();
		}
		if (this.m_temporaryAccountData == null)
		{
			global::Log.TemporaryAccount.Print("m_temporaryAccountData == null", Array.Empty<object>());
			return;
		}
		this.m_temporaryAccountData = new TemporaryAccountManager.TemporaryAccountData();
		TemporaryAccountManager.TemporaryAccountData.TemporaryAccount temporaryAccount = new TemporaryAccountManager.TemporaryAccountData.TemporaryAccount();
		temporaryAccount.m_temporaryAccountId = "BLAH BLAH BLAH";
		temporaryAccount.m_battleTag = "BATTLETAG";
		temporaryAccount.m_lastLogin = DateTime.UtcNow.ToString();
		this.m_temporaryAccountData.m_temporaryAccounts.Add(temporaryAccount);
		temporaryAccount = new TemporaryAccountManager.TemporaryAccountData.TemporaryAccount();
		temporaryAccount.m_temporaryAccountId = "HEHEHEHEHEHE";
		temporaryAccount.m_battleTag = "ARGAREOJ";
		temporaryAccount.m_lastLogin = DateTime.UtcNow.ToString();
		this.m_temporaryAccountData.m_temporaryAccounts.Add(temporaryAccount);
		temporaryAccount = new TemporaryAccountManager.TemporaryAccountData.TemporaryAccount();
		temporaryAccount.m_temporaryAccountId = "Wha?";
		temporaryAccount.m_battleTag = "YE";
		temporaryAccount.m_lastLogin = DateTime.UtcNow.ToString();
		this.m_temporaryAccountData.m_temporaryAccounts.Add(temporaryAccount);
		temporaryAccount = new TemporaryAccountManager.TemporaryAccountData.TemporaryAccount();
		temporaryAccount.m_temporaryAccountId = "SUPER_SECRET_ACCOUNT_ID";
		temporaryAccount.m_battleTag = "GoodKnight";
		temporaryAccount.m_lastLogin = DateTime.UtcNow.ToString();
		this.m_temporaryAccountData.m_temporaryAccounts.Add(temporaryAccount);
		temporaryAccount = new TemporaryAccountManager.TemporaryAccountData.TemporaryAccount();
		temporaryAccount.m_temporaryAccountId = "SUPER_SECRET_ACCOUNT_ID";
		temporaryAccount.m_battleTag = "GoodKnight";
		temporaryAccount.m_lastLogin = DateTime.UtcNow.ToString();
		this.m_temporaryAccountData.m_temporaryAccounts.Add(temporaryAccount);
		this.SaveTemporaryAccountData();
	}

	// Token: 0x0600690D RID: 26893 RVA: 0x00223C38 File Offset: 0x00221E38
	public void SortPrint()
	{
		IEnumerable<TemporaryAccountManager.TemporaryAccountData.TemporaryAccount> sortedTemporaryAccounts = this.GetSortedTemporaryAccounts();
		global::Log.TemporaryAccount.Print("Sorted!", Array.Empty<object>());
		global::Log.TemporaryAccount.Print(string.Concat(new object[]
		{
			"Selected Account = ",
			this.m_temporaryAccountData.m_selectedTemporaryAccountIndex,
			", m_lastUpdate = ",
			Convert.ToDateTime(this.m_temporaryAccountData.m_lastUpdated)
		}), Array.Empty<object>());
		foreach (TemporaryAccountManager.TemporaryAccountData.TemporaryAccount temporaryAccount in sortedTemporaryAccounts)
		{
			global::Log.TemporaryAccount.Print(string.Concat(new object[]
			{
				"[m_temporaryAccountId = ",
				temporaryAccount.m_temporaryAccountId,
				", m_battleTag = ",
				temporaryAccount.m_battleTag,
				", m_regionId = ",
				temporaryAccount.m_regionId,
				", m_lastLogin = ",
				Convert.ToDateTime(temporaryAccount.m_lastLogin),
				", m_isHealedUp = ",
				temporaryAccount.m_isHealedUp.ToString(),
				", m_isMinor = ",
				temporaryAccount.m_isMinor.ToString(),
				"]"
			}), Array.Empty<object>());
		}
	}

	// Token: 0x0600690E RID: 26894 RVA: 0x00223D98 File Offset: 0x00221F98
	public void HealUpCompleteTest()
	{
		Options.Get().SetBool(Option.CREATED_ACCOUNT, true);
		InGameBrowserManager.Get().Hide();
		HearthstoneApplication.Get().Reset();
	}

	// Token: 0x0600690F RID: 26895 RVA: 0x00223DBC File Offset: 0x00221FBC
	public string NagTimeDebugLog()
	{
		long @long = Options.Get().GetLong(Option.LAST_HEAL_UP_EVENT_DATE);
		DateTime now = DateTime.Now;
		string text;
		if (@long != 0L)
		{
			int totalWins = this.GetTotalWins();
			DateTime dateTime = new DateTime(@long);
			TimeSpan timeSpan = now - dateTime;
			int num = 1;
			foreach (int num2 in TemporaryAccountManager.HEAL_UP_FREQUENCY.Keys)
			{
				if (totalWins >= num2)
				{
					num = TemporaryAccountManager.HEAL_UP_FREQUENCY[num2];
				}
			}
			text = "Last frequency time: " + dateTime;
			if (timeSpan.TotalHours > (double)num)
			{
				text += " Next event will trigger nag";
			}
			else
			{
				text = string.Concat(new object[]
				{
					text,
					" Next nag in ",
					(double)num - timeSpan.TotalHours,
					" hours"
				});
			}
		}
		else
		{
			text = "No frequency time saved!";
		}
		return text;
	}

	// Token: 0x06006910 RID: 26896 RVA: 0x00223EC0 File Offset: 0x002220C0
	private void ShowSignUpPopUp(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_signUpPopUp = go.GetComponent<TemporaryAccountSignUpPopUp>();
		this.m_signUpPopUp.Show(this.m_popupArgs, new TemporaryAccountSignUpPopUp.OnSignUpPopUpBack(this.OnHealUpProcessCancelled));
		this.m_isSignUpPopUpLoading = false;
	}

	// Token: 0x06006911 RID: 26897 RVA: 0x00223EF2 File Offset: 0x002220F2
	private void OnHealUpProcessCancelled()
	{
		if (this.m_onSignUpDismissedHandler != null)
		{
			this.m_onSignUpDismissedHandler();
			this.m_onSignUpDismissedHandler = null;
		}
	}

	// Token: 0x06006912 RID: 26898 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void SaveTemporaryAccountData()
	{
	}

	// Token: 0x06006913 RID: 26899 RVA: 0x00223F10 File Offset: 0x00222110
	private void AddCreatedTemporaryAccount(string battleTag)
	{
		if (this.m_createdTemporaryAccountId == null)
		{
			global::Log.TemporaryAccount.PrintError("Attempting to add new temporary account without ID!", Array.Empty<object>());
			return;
		}
		AdTrackingManager.Get().TrackHeadlessAccountCreated();
		global::Log.TemporaryAccount.Print("Adding Created Temporary Account, updating data...", Array.Empty<object>());
		this.m_createdTemporaryAccount = new TemporaryAccountManager.TemporaryAccountData.TemporaryAccount();
		this.m_createdTemporaryAccount.m_temporaryAccountId = this.m_createdTemporaryAccountId;
		this.m_createdTemporaryAccount.m_battleTag = battleTag;
		this.m_createdTemporaryAccount.m_regionId = (int)MobileDeviceLocale.GetCurrentRegionId();
		this.m_createdTemporaryAccount.m_lastLogin = DateTime.UtcNow.ToString();
		this.m_createdTemporaryAccountId = null;
		CloudStorageManager.Get().StartInitialize(new CloudStorageManager.OnInitializedFinished(this.OnCloudStorageInitializedAddCreatedTemporaryAccount), GameStrings.Get("GLUE_CLOUD_STORAGE_CONTEXT_BODY_01"));
	}

	// Token: 0x06006914 RID: 26900 RVA: 0x00223FD0 File Offset: 0x002221D0
	private void OnCloudStorageInitializedAddCreatedTemporaryAccount()
	{
		if (!this.m_isTemporaryAccountDataLoaded)
		{
			this.LoadTemporaryAccountData();
		}
		if (this.m_temporaryAccountData == null)
		{
			global::Log.TemporaryAccount.PrintWarning("Unable to load temporary account data!", Array.Empty<object>());
			return;
		}
		if (!this.m_temporaryAccountData.m_temporaryAccounts.Exists((TemporaryAccountManager.TemporaryAccountData.TemporaryAccount account) => account.m_temporaryAccountId == this.m_createdTemporaryAccount.m_temporaryAccountId))
		{
			this.m_temporaryAccountData.m_temporaryAccounts.Add(this.m_createdTemporaryAccount);
			this.m_temporaryAccountData.m_selectedTemporaryAccountIndex = this.m_temporaryAccountData.m_temporaryAccounts.Count - 1;
			this.SaveTemporaryAccountData();
		}
		else
		{
			global::Log.TemporaryAccount.PrintInfo("Did not add temporary account to cloud storage as it was already saved", Array.Empty<object>());
		}
		this.m_createdTemporaryAccount = null;
	}

	// Token: 0x06006915 RID: 26901 RVA: 0x0022407C File Offset: 0x0022227C
	private void OnCloudStorageInitializedUpdateTemporaryAccountData()
	{
		if (!this.m_isTemporaryAccountDataLoaded)
		{
			this.LoadTemporaryAccountData();
		}
		if (this.m_temporaryAccountData == null)
		{
			global::Log.TemporaryAccount.PrintWarning("Unable to load temporary account data!", Array.Empty<object>());
			return;
		}
		if (string.IsNullOrEmpty(this.m_createdTemporaryAccountId) && this.m_temporaryAccountData.m_selectedTemporaryAccountIndex != -1)
		{
			this.m_temporaryAccountData.m_temporaryAccounts[this.m_temporaryAccountData.m_selectedTemporaryAccountIndex].m_lastLogin = DateTime.UtcNow.ToString();
			this.SaveTemporaryAccountData();
		}
	}

	// Token: 0x06006916 RID: 26902 RVA: 0x00224104 File Offset: 0x00222304
	private void OnCloudStorageInitializedStartShowSwitchAccountMenu()
	{
		if (!this.m_isTemporaryAccountDataLoaded)
		{
			this.LoadTemporaryAccountData();
		}
		if (this.m_temporaryAccountData == null || this.ValidTemporaryAccountCount() == 0 || this.m_temporaryAccountData.m_selectedTemporaryAccountIndex != -1)
		{
			if (this.m_onSwithAccountLogInPressedHandler != null)
			{
				this.m_onSwithAccountLogInPressedHandler(null);
				this.m_onSwithAccountLogInPressedHandler = null;
			}
			this.m_disableSwitchAccountMenuInputBlocker = false;
			return;
		}
		this.ShowSwitchAccountMenu();
	}

	// Token: 0x06006917 RID: 26903 RVA: 0x00224168 File Offset: 0x00222368
	private int ValidTemporaryAccountCount()
	{
		if (!this.m_isTemporaryAccountDataLoaded)
		{
			this.LoadTemporaryAccountData();
		}
		if (this.m_temporaryAccountData == null)
		{
			global::Log.TemporaryAccount.Print("m_temporaryAccountData == null", Array.Empty<object>());
			return 0;
		}
		int num = 0;
		using (List<TemporaryAccountManager.TemporaryAccountData.TemporaryAccount>.Enumerator enumerator = this.m_temporaryAccountData.m_temporaryAccounts.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.m_isHealedUp)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x06006918 RID: 26904 RVA: 0x002241F4 File Offset: 0x002223F4
	private IEnumerable<TemporaryAccountManager.TemporaryAccountData.TemporaryAccount> GetSortedTemporaryAccounts()
	{
		return this.m_temporaryAccountData.m_temporaryAccounts.OrderByDescending(delegate(TemporaryAccountManager.TemporaryAccountData.TemporaryAccount temporaryAccount)
		{
			DateTime result;
			DateTime.TryParse(temporaryAccount.m_lastLogin, out result);
			return result;
		});
	}

	// Token: 0x06006919 RID: 26905 RVA: 0x00224228 File Offset: 0x00222428
	private void OnPresenceChanged(PresenceUpdate[] updates)
	{
		if (string.IsNullOrEmpty(this.m_createdTemporaryAccountId))
		{
			BnetPresenceMgr.Get().OnGameAccountPresenceChange -= this.OnPresenceChanged;
			return;
		}
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		if (myPlayer != null)
		{
			this.AddCreatedTemporaryAccount(myPlayer.GetBattleTag().GetName());
			BnetPresenceMgr.Get().OnGameAccountPresenceChange -= this.OnPresenceChanged;
		}
	}

	// Token: 0x0600691A RID: 26906 RVA: 0x00224290 File Offset: 0x00222490
	private void OnSwitchAccountMenuLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_switchAccountMenu = go.GetComponent<SwitchAccountMenu>();
		this.m_switchAccountMenu.AddTemporaryAccountButtons(this.GetSortedTemporaryAccounts(), this.GetSelectedTemporaryAccountId());
		this.m_switchAccountMenu.Show(this.m_onSwithAccountLogInPressedHandler);
		if (this.m_disableSwitchAccountMenuInputBlocker)
		{
			this.m_switchAccountMenu.DisableInputBlocker();
			this.m_disableSwitchAccountMenuInputBlocker = false;
		}
		this.m_onSwithAccountLogInPressedHandler = null;
		this.m_isSwitchAccountMenuLoading = false;
	}

	// Token: 0x0600691B RID: 26907 RVA: 0x002242FC File Offset: 0x002224FC
	private bool OnBnetError(BnetErrorInfo info, object userData)
	{
		if (this.m_handledDisconnect && this.m_handledHealUpError)
		{
			return false;
		}
		BattleNetErrors error = info.GetError();
		global::Log.TemporaryAccount.Print("OnBnetError: " + error, Array.Empty<object>());
		if (error != BattleNetErrors.ERROR_SESSION_DATA_CHANGED)
		{
			if (error == BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED)
			{
				global::Log.TemporaryAccount.Print("Handled Disconnect", Array.Empty<object>());
				this.m_handledDisconnect = true;
				return true;
			}
			if (error != BattleNetErrors.ERROR_SESSION_ADMIN_KICK)
			{
				return false;
			}
		}
		global::Log.TemporaryAccount.Print("Handled Heal Up Error", Array.Empty<object>());
		this.m_handledHealUpError = true;
		return true;
	}

	// Token: 0x0600691C RID: 26908 RVA: 0x00224394 File Offset: 0x00222594
	private int GetTotalWins()
	{
		int num = 0;
		if (NetCache.Get() == null || NetCache.Get().GetNetObject<NetCache.NetCachePlayerRecords>() == null || NetCache.Get().GetNetObject<NetCache.NetCachePlayerRecords>().Records == null)
		{
			return num;
		}
		foreach (NetCache.PlayerRecord playerRecord in NetCache.Get().GetNetObject<NetCache.NetCachePlayerRecords>().Records)
		{
			if (playerRecord.Data == 0)
			{
				GameType recordType = playerRecord.RecordType;
				if (recordType <= GameType.GT_CASUAL)
				{
					if (recordType != GameType.GT_VS_AI && recordType != GameType.GT_ARENA && recordType - GameType.GT_RANKED > 1)
					{
						continue;
					}
				}
				else if (recordType != GameType.GT_TAVERNBRAWL && recordType != GameType.GT_FSG_BRAWL && recordType != GameType.GT_FSG_BRAWL_2P_COOP)
				{
					continue;
				}
				num += playerRecord.Wins;
			}
		}
		return num;
	}

	// Token: 0x0600691D RID: 26909 RVA: 0x00224450 File Offset: 0x00222650
	private string Encrypt(string toEncrypt)
	{
		byte[] bytes = Encoding.UTF8.GetBytes("v9OJ4mkM9Za*g8gQdw#WA12KA1DGA&Q7");
		byte[] array = Crypto.Rijndael.Encrypt(Encoding.UTF8.GetBytes(toEncrypt), bytes);
		return Convert.ToBase64String(array, 0, array.Length);
	}

	// Token: 0x0600691E RID: 26910 RVA: 0x0022448C File Offset: 0x0022268C
	private string Decrypt(string toDecrypt)
	{
		try
		{
			byte[] bytes = Encoding.UTF8.GetBytes("v9OJ4mkM9Za*g8gQdw#WA12KA1DGA&Q7");
			byte[] bytes2 = Crypto.Rijndael.Decrypt(Convert.FromBase64String(toDecrypt), bytes);
			return Encoding.UTF8.GetString(bytes2);
		}
		catch (Exception ex)
		{
			global::Log.TemporaryAccount.Print("Decrypt: {0} {1}", new object[]
			{
				toDecrypt,
				ex.Message
			});
		}
		return null;
	}

	// Token: 0x0600691F RID: 26911 RVA: 0x002244FC File Offset: 0x002226FC
	private bool IsValid(TemporaryAccountManager.TemporaryAccountData data)
	{
		if (data == null || data.m_temporaryAccounts == null || string.IsNullOrEmpty(data.m_lastUpdated))
		{
			global::Log.TemporaryAccount.Print("Invalid data found: null", Array.Empty<object>());
			return false;
		}
		DateTime date;
		if (!DateTime.TryParse(data.m_lastUpdated, out date))
		{
			global::Log.TemporaryAccount.Print("Invalid date format in m_lastUpdated: {0}", new object[]
			{
				data.m_lastUpdated
			});
			return false;
		}
		return !data.m_temporaryAccounts.Any(delegate(TemporaryAccountManager.TemporaryAccountData.TemporaryAccount a)
		{
			bool flag = DateTime.TryParse(a.m_lastLogin, out date);
			if (!flag)
			{
				global::Log.TemporaryAccount.Print("Invalid date format in m_lastLogin: {0}", new object[]
				{
					a.m_lastLogin
				});
			}
			return !flag;
		});
	}

	// Token: 0x040055EE RID: 21998
	public const int NO_TEMPORARY_ACCOUNT_SELECTED = -1;

	// Token: 0x040055EF RID: 21999
	private const string TEMPORARY_ACCOUNT_DATA_CRYPTO_KEY = "v9OJ4mkM9Za*g8gQdw#WA12KA1DGA&Q7";

	// Token: 0x040055F0 RID: 22000
	private const string TEMPORARY_ACCOUNT_DATA_CLOUD_KEY = "TEMPORARY_ACCOUNT_DATA";

	// Token: 0x040055F1 RID: 22001
	private static global::Map<int, int> HEAL_UP_FREQUENCY = new global::Map<int, int>
	{
		{
			20,
			48
		},
		{
			10,
			3
		},
		{
			0,
			1
		}
	};

	// Token: 0x040055F2 RID: 22002
	private static TemporaryAccountManager s_Instance;

	// Token: 0x040055F3 RID: 22003
	private TemporaryAccountManager.TemporaryAccountData m_temporaryAccountData;

	// Token: 0x040055F4 RID: 22004
	private bool m_isTemporaryAccountDataLoaded;

	// Token: 0x040055F5 RID: 22005
	private string m_createdTemporaryAccountId;

	// Token: 0x040055F6 RID: 22006
	private TemporaryAccountManager.TemporaryAccountData.TemporaryAccount m_createdTemporaryAccount;

	// Token: 0x040055F7 RID: 22007
	private TemporaryAccountSignUpPopUp m_signUpPopUp;

	// Token: 0x040055F8 RID: 22008
	private bool m_isSignUpPopUpLoading;

	// Token: 0x040055F9 RID: 22009
	private TemporaryAccountSignUpPopUp.PopupTextParameters m_popupArgs;

	// Token: 0x040055FA RID: 22010
	private TemporaryAccountManager.OnHealUpDialogDismissed m_onSignUpDismissedHandler;

	// Token: 0x040055FB RID: 22011
	private TemporaryAccountManager.HealUpReason m_signUpReason;

	// Token: 0x040055FC RID: 22012
	private SwitchAccountMenu m_switchAccountMenu;

	// Token: 0x040055FD RID: 22013
	private bool m_isSwitchAccountMenuLoading;

	// Token: 0x040055FE RID: 22014
	private bool m_disableSwitchAccountMenuInputBlocker;

	// Token: 0x040055FF RID: 22015
	private SwitchAccountMenu.OnSwitchAccountLogInPressed m_onSwithAccountLogInPressedHandler;

	// Token: 0x04005600 RID: 22016
	private bool m_handledDisconnect;

	// Token: 0x04005601 RID: 22017
	private bool m_handledHealUpError;

	// Token: 0x04005602 RID: 22018
	private ulong m_healUpTemporaryAccountGameAccountId;

	// Token: 0x04005603 RID: 22019
	private int m_lastLoginSelectedTemporaryAccountIndex = -1;

	// Token: 0x04005604 RID: 22020
	private bool m_healUpComplete;

	// Token: 0x02002312 RID: 8978
	[Serializable]
	public class TemporaryAccountData
	{
		// Token: 0x0400E5B4 RID: 58804
		public int m_selectedTemporaryAccountIndex = -1;

		// Token: 0x0400E5B5 RID: 58805
		public List<TemporaryAccountManager.TemporaryAccountData.TemporaryAccount> m_temporaryAccounts = new List<TemporaryAccountManager.TemporaryAccountData.TemporaryAccount>();

		// Token: 0x0400E5B6 RID: 58806
		public string m_lastUpdated;

		// Token: 0x020029A3 RID: 10659
		[Serializable]
		public class TemporaryAccount
		{
			// Token: 0x0400FDE8 RID: 65000
			public string m_temporaryAccountId;

			// Token: 0x0400FDE9 RID: 65001
			public string m_battleTag;

			// Token: 0x0400FDEA RID: 65002
			public int m_regionId = -1;

			// Token: 0x0400FDEB RID: 65003
			public string m_lastLogin;

			// Token: 0x0400FDEC RID: 65004
			public bool m_isHealedUp;

			// Token: 0x0400FDED RID: 65005
			public bool m_isMinor;
		}
	}

	// Token: 0x02002313 RID: 8979
	// (Invoke) Token: 0x060129B7 RID: 76215
	public delegate void OnHealUpDialogDismissed();

	// Token: 0x02002314 RID: 8980
	public enum HealUpReason
	{
		// Token: 0x0400E5B8 RID: 58808
		UNKNOWN,
		// Token: 0x0400E5B9 RID: 58809
		FRIENDS_LIST,
		// Token: 0x0400E5BA RID: 58810
		GAME_MENU,
		// Token: 0x0400E5BB RID: 58811
		REAL_MONEY,
		// Token: 0x0400E5BC RID: 58812
		LOCKED_PACK,
		// Token: 0x0400E5BD RID: 58813
		WIN_GAME,
		// Token: 0x0400E5BE RID: 58814
		CRAFT_CARD,
		// Token: 0x0400E5BF RID: 58815
		OPEN_PACK
	}
}
