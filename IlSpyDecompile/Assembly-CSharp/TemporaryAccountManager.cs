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

public class TemporaryAccountManager
{
	[Serializable]
	public class TemporaryAccountData
	{
		[Serializable]
		public class TemporaryAccount
		{
			public string m_temporaryAccountId;

			public string m_battleTag;

			public int m_regionId = -1;

			public string m_lastLogin;

			public bool m_isHealedUp;

			public bool m_isMinor;
		}

		public int m_selectedTemporaryAccountIndex = -1;

		public List<TemporaryAccount> m_temporaryAccounts = new List<TemporaryAccount>();

		public string m_lastUpdated;
	}

	public delegate void OnHealUpDialogDismissed();

	public enum HealUpReason
	{
		UNKNOWN,
		FRIENDS_LIST,
		GAME_MENU,
		REAL_MONEY,
		LOCKED_PACK,
		WIN_GAME,
		CRAFT_CARD,
		OPEN_PACK
	}

	public const int NO_TEMPORARY_ACCOUNT_SELECTED = -1;

	private const string TEMPORARY_ACCOUNT_DATA_CRYPTO_KEY = "v9OJ4mkM9Za*g8gQdw#WA12KA1DGA&Q7";

	private const string TEMPORARY_ACCOUNT_DATA_CLOUD_KEY = "TEMPORARY_ACCOUNT_DATA";

	private static Map<int, int> HEAL_UP_FREQUENCY = new Map<int, int>
	{
		{ 20, 48 },
		{ 10, 3 },
		{ 0, 1 }
	};

	private static TemporaryAccountManager s_Instance;

	private TemporaryAccountData m_temporaryAccountData;

	private bool m_isTemporaryAccountDataLoaded;

	private string m_createdTemporaryAccountId;

	private TemporaryAccountData.TemporaryAccount m_createdTemporaryAccount;

	private TemporaryAccountSignUpPopUp m_signUpPopUp;

	private bool m_isSignUpPopUpLoading;

	private TemporaryAccountSignUpPopUp.PopupTextParameters m_popupArgs;

	private OnHealUpDialogDismissed m_onSignUpDismissedHandler;

	private HealUpReason m_signUpReason;

	private SwitchAccountMenu m_switchAccountMenu;

	private bool m_isSwitchAccountMenuLoading;

	private bool m_disableSwitchAccountMenuInputBlocker;

	private SwitchAccountMenu.OnSwitchAccountLogInPressed m_onSwithAccountLogInPressedHandler;

	private bool m_handledDisconnect;

	private bool m_handledHealUpError;

	private ulong m_healUpTemporaryAccountGameAccountId;

	private int m_lastLoginSelectedTemporaryAccountIndex = -1;

	private bool m_healUpComplete;

	public static bool IsTemporaryAccount()
	{
		if (HearthstoneApplication.IsInternal() && Options.Get().HasOption(Option.IS_TEMPORARY_ACCOUNT_CHEAT))
		{
			return Options.Get().GetBool(Option.IS_TEMPORARY_ACCOUNT_CHEAT);
		}
		return BattleNet.IsHeadlessAccount();
	}

	public static TemporaryAccountManager Get()
	{
		if (s_Instance == null)
		{
			s_Instance = new TemporaryAccountManager();
		}
		return s_Instance;
	}

	public void Initialize()
	{
		HearthstoneApplication.Get().WillReset += WillReset;
		if (IsTemporaryAccount())
		{
			Processor.QueueJob("TemporaryAccountManager.AddFakeBooster", Job_AddFakeBooster(), JobFlags.StartImmediately);
		}
	}

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
	}

	public void WillReset()
	{
		HearthstoneApplication.Get().WillReset -= WillReset;
		BnetPresenceMgr.Get().OnGameAccountPresenceChange -= OnPresenceChanged;
	}

	public void LoadTemporaryAccountData()
	{
		if (!m_isTemporaryAccountDataLoaded)
		{
			m_temporaryAccountData = null;
			m_isTemporaryAccountDataLoaded = true;
		}
	}

	public TemporaryAccountData GetTemporaryAccountData()
	{
		LoadTemporaryAccountData();
		return m_temporaryAccountData;
	}

	public string GetSelectedTemporaryAccountId()
	{
		Log.TemporaryAccount.Print("Get selected Temporary Account Id");
		if (!m_isTemporaryAccountDataLoaded)
		{
			LoadTemporaryAccountData();
		}
		if (m_temporaryAccountData == null)
		{
			Log.TemporaryAccount.PrintWarning("Unable to load temporary account data!");
			return null;
		}
		if (m_temporaryAccountData.m_selectedTemporaryAccountIndex == -1)
		{
			Log.TemporaryAccount.PrintWarning("No selected temporary account!");
			return null;
		}
		string temporaryAccountId = m_temporaryAccountData.m_temporaryAccounts[m_temporaryAccountData.m_selectedTemporaryAccountIndex].m_temporaryAccountId;
		if (!string.IsNullOrEmpty(temporaryAccountId))
		{
			return temporaryAccountId;
		}
		return null;
	}

	public void CreatedTemporaryAccount(string temporaryAccountId)
	{
		Log.TemporaryAccount.Print("Created Temporary Account with Id: " + temporaryAccountId);
		if (!string.IsNullOrEmpty(m_createdTemporaryAccountId))
		{
			Log.TemporaryAccount.PrintWarning("Replacing previous created Temporary Account Id! Previous Id = " + m_createdTemporaryAccountId);
		}
		m_createdTemporaryAccountId = temporaryAccountId;
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		if (myPlayer != null)
		{
			AddCreatedTemporaryAccount(myPlayer.GetBattleTag().GetName());
			return;
		}
		BnetPresenceMgr.Get().OnGameAccountPresenceChange -= OnPresenceChanged;
		BnetPresenceMgr.Get().OnGameAccountPresenceChange += OnPresenceChanged;
	}

	public void UpdateTemporaryAccountData()
	{
		if (IsTemporaryAccount())
		{
			CloudStorageManager.Get().StartInitialize(OnCloudStorageInitializedUpdateTemporaryAccountData, GameStrings.Get("GLUE_CLOUD_STORAGE_CONTEXT_BODY_01"));
		}
	}

	public void HealUpSelectedTemporaryAccount()
	{
		Log.TemporaryAccount.Print("Heal Up Selected Temporary Account");
		InGameBrowserManager.Get().HideAllButtons();
		if (!m_isTemporaryAccountDataLoaded)
		{
			LoadTemporaryAccountData();
		}
		if (m_temporaryAccountData == null)
		{
			Log.TemporaryAccount.PrintWarning("Unable to load temporary account data!");
			return;
		}
		if (m_temporaryAccountData.m_selectedTemporaryAccountIndex == -1)
		{
			Log.TemporaryAccount.PrintWarning("No selected temporary account!");
			return;
		}
		m_temporaryAccountData.m_temporaryAccounts[m_temporaryAccountData.m_selectedTemporaryAccountIndex].m_isHealedUp = true;
		m_temporaryAccountData.m_temporaryAccounts[m_temporaryAccountData.m_selectedTemporaryAccountIndex].m_isMinor = false;
		m_temporaryAccountData.m_selectedTemporaryAccountIndex = -1;
		SaveTemporaryAccountData();
		Options.Get().SetBool(Option.CREATED_ACCOUNT, val: true);
		Options.Get().DeleteOption(Option.LAST_HEAL_UP_EVENT_DATE);
	}

	public void SetSelectedTemporaryAccountAsMinor()
	{
		Log.TemporaryAccount.Print("Set Temporary Account as Minor");
		if (!m_isTemporaryAccountDataLoaded)
		{
			LoadTemporaryAccountData();
		}
		if (m_temporaryAccountData == null)
		{
			Log.TemporaryAccount.PrintWarning("Unable to load temporary account data!");
			return;
		}
		if (m_temporaryAccountData.m_selectedTemporaryAccountIndex == -1)
		{
			Log.TemporaryAccount.PrintWarning("No selected temporary account!");
			return;
		}
		m_temporaryAccountData.m_temporaryAccounts[m_temporaryAccountData.m_selectedTemporaryAccountIndex].m_isMinor = true;
		SaveTemporaryAccountData();
	}

	public void DeleteTemporaryAccountData()
	{
		Log.TemporaryAccount.PrintWarning("Deleting Temporary Account Data!");
		m_temporaryAccountData = new TemporaryAccountData();
		Options.Get().DeleteOption(Option.TEMPORARY_ACCOUNT_DATA);
		CloudStorageManager.Get().RemoveObject("TEMPORARY_ACCOUNT_DATA");
	}

	public void SetSelectedTemporaryAccount(string temporaryAccountId)
	{
		Log.TemporaryAccount.Print("Set Selected Temporary Account");
		if (!m_isTemporaryAccountDataLoaded)
		{
			LoadTemporaryAccountData();
		}
		if (m_temporaryAccountData == null)
		{
			Log.TemporaryAccount.PrintWarning("Unable to load temporary account data!");
			return;
		}
		for (int i = 0; i < m_temporaryAccountData.m_temporaryAccounts.Count; i++)
		{
			if (m_temporaryAccountData.m_temporaryAccounts[i].m_temporaryAccountId == temporaryAccountId)
			{
				m_temporaryAccountData.m_selectedTemporaryAccountIndex = i;
				break;
			}
		}
		if (m_temporaryAccountData.m_selectedTemporaryAccountIndex == m_temporaryAccountData.m_temporaryAccounts.Count)
		{
			Log.TemporaryAccount.PrintError("Unable to find temporary account Id!");
			m_temporaryAccountData.m_selectedTemporaryAccountIndex = -1;
		}
		SaveTemporaryAccountData();
	}

	public void UnselectTemporaryAccount()
	{
		Log.TemporaryAccount.Print("Unselect Selected Temporary Account (if any)");
		if (!m_isTemporaryAccountDataLoaded)
		{
			LoadTemporaryAccountData();
		}
		if (m_temporaryAccountData == null)
		{
			Log.TemporaryAccount.PrintWarning("Unable to load temporary account data!");
			return;
		}
		m_lastLoginSelectedTemporaryAccountIndex = m_temporaryAccountData.m_selectedTemporaryAccountIndex;
		m_temporaryAccountData.m_selectedTemporaryAccountIndex = -1;
		SaveTemporaryAccountData();
	}

	public bool IsSelectedTemporaryAccountMinor()
	{
		Log.TemporaryAccount.Print("Is Selected Temporary Account a Minor");
		if (!m_isTemporaryAccountDataLoaded)
		{
			LoadTemporaryAccountData();
		}
		if (m_temporaryAccountData == null)
		{
			Log.TemporaryAccount.PrintWarning("Unable to load temporary account data!");
			return false;
		}
		if (m_temporaryAccountData.m_selectedTemporaryAccountIndex == -1)
		{
			Log.TemporaryAccount.PrintWarning("No selected temporary account!");
			return false;
		}
		return m_temporaryAccountData.m_temporaryAccounts[m_temporaryAccountData.m_selectedTemporaryAccountIndex].m_isMinor;
	}

	public bool ShowHealUpDialog(string header, string body, HealUpReason reason, bool userTriggered, OnHealUpDialogDismissed onSignUpHandler)
	{
		TemporaryAccountSignUpPopUp.PopupTextParameters popupTextParameters = default(TemporaryAccountSignUpPopUp.PopupTextParameters);
		popupTextParameters.Header = header;
		popupTextParameters.Body = body;
		TemporaryAccountSignUpPopUp.PopupTextParameters popupArgs = popupTextParameters;
		return ShowHealUpDialog(popupArgs, reason, userTriggered, onSignUpHandler);
	}

	public bool ShowHealUpDialog(TemporaryAccountSignUpPopUp.PopupTextParameters popupArgs, HealUpReason reason, bool userTriggered, OnHealUpDialogDismissed onSignUpHandler)
	{
		if (!IsTemporaryAccount())
		{
			return false;
		}
		if (!userTriggered)
		{
			long @long = Options.Get().GetLong(Option.LAST_HEAL_UP_EVENT_DATE);
			DateTime now = DateTime.Now;
			if (@long != 0L)
			{
				int totalWins = GetTotalWins();
				DateTime dateTime = new DateTime(@long);
				TimeSpan timeSpan = now - dateTime;
				int num = 1;
				foreach (int key in HEAL_UP_FREQUENCY.Keys)
				{
					if (totalWins >= key)
					{
						num = HEAL_UP_FREQUENCY[key];
					}
				}
				if (timeSpan.TotalHours < (double)num)
				{
					return false;
				}
			}
			Options.Get().SetLong(Option.LAST_HEAL_UP_EVENT_DATE, now.Ticks);
		}
		m_signUpReason = reason;
		m_onSignUpDismissedHandler = onSignUpHandler;
		if (m_signUpPopUp == null)
		{
			if (!m_isSignUpPopUpLoading)
			{
				m_isSignUpPopUpLoading = true;
				AssetLoader.Get().InstantiatePrefab("TemporaryAccountSignUp.prefab:14791f0c7af5c6f4480fc78ab36c81bc", ShowSignUpPopUp);
			}
			m_popupArgs = popupArgs;
			return true;
		}
		m_signUpPopUp.Show(popupArgs, OnHealUpProcessCancelled);
		return true;
	}

	public bool ShowEarnCardEventHealUpDialog(HealUpReason reason)
	{
		return ShowHealUpDialog(GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_HEADER_03"), GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_BODY_01"), reason, userTriggered: false, null);
	}

	public void ShowHealUpPage(HealUpReason reason, Action<bool> onDismissed = null)
	{
		m_signUpReason = reason;
		ShowHealUpPage(onDismissed);
	}

	public void ShowHealUpPage(Action<bool> onDismissed = null)
	{
		ILoginService loginService = HearthstoneServices.Get<ILoginService>();
		if (loginService != null && loginService.SupportsAccountHealup())
		{
			Log.TemporaryAccount.PrintDebug("Using Login Service for account heal up");
			loginService.HealupCurrentTemporaryAccount(onDismissed);
			return;
		}
		m_healUpTemporaryAccountGameAccountId = BattleNet.GetMyGameAccountId().lo;
		m_handledDisconnect = false;
		m_handledHealUpError = false;
		Network.Get().AddBnetErrorListener(OnBnetError);
		NetCache.Get().DispatchClientOptionsToServer();
		string text = ((m_signUpReason != HealUpReason.WIN_GAME) ? ExternalUrlService.Get().GetAccountHealUpLink(m_signUpReason) : ExternalUrlService.Get().GetAccountHealUpLink(m_signUpReason, GetTotalWins()));
		Log.TemporaryAccount.Print("Opening URL: " + text);
		InGameBrowserManager.Get().SetUrl(text);
		InGameBrowserManager.Get().Show(OnHealUpPageClosed);
		m_signUpReason = HealUpReason.UNKNOWN;
	}

	public void OnHealUpPageClosed()
	{
		Log.TemporaryAccount.Print("Heal Up Page Closed");
		m_healUpTemporaryAccountGameAccountId = 0uL;
		m_handledDisconnect = false;
		m_handledHealUpError = false;
		Network.Get().RemoveBnetErrorListener(OnBnetError);
		OnHealUpProcessCancelled();
	}

	public void HealUpComplete()
	{
		Log.TemporaryAccount.Print("Heal Up Complete - m_handledDisconnect = " + m_handledDisconnect + " m_handledHealUpError = " + m_handledHealUpError + " Heal Up Already Complete:" + m_healUpComplete);
		if (!m_healUpComplete)
		{
			m_healUpTemporaryAccountGameAccountId = 0uL;
			m_healUpComplete = true;
			InGameBrowserManager.Get().Hide();
			BattleNet.RequestCloseAurora();
			HearthstoneApplication.Get().ResetAndForceLogin();
		}
	}

	public void StartShowSwitchAccountMenu(SwitchAccountMenu.OnSwitchAccountLogInPressed handler, bool disableInputBlocker)
	{
		m_onSwithAccountLogInPressedHandler = handler;
		m_disableSwitchAccountMenuInputBlocker = disableInputBlocker;
		CloudStorageManager.Get().StartInitialize(OnCloudStorageInitializedStartShowSwitchAccountMenu, GameStrings.Get("GLUE_CLOUD_STORAGE_CONTEXT_BODY_02"));
	}

	public void ShowSwitchAccountMenu(SwitchAccountMenu.OnSwitchAccountLogInPressed handler, bool disableInputBlocker)
	{
		m_onSwithAccountLogInPressedHandler = handler;
		m_disableSwitchAccountMenuInputBlocker = disableInputBlocker;
		ShowSwitchAccountMenu();
	}

	public void ShowSwitchAccountMenu()
	{
		if ((bool)m_switchAccountMenu)
		{
			if (!m_switchAccountMenu.IsShown())
			{
				m_switchAccountMenu.Show(m_onSwithAccountLogInPressedHandler);
				if (m_disableSwitchAccountMenuInputBlocker)
				{
					m_switchAccountMenu.DisableInputBlocker();
				}
			}
			m_onSwithAccountLogInPressedHandler = null;
			m_disableSwitchAccountMenuInputBlocker = false;
		}
		else if (!m_isSwitchAccountMenuLoading)
		{
			m_isSwitchAccountMenuLoading = true;
			AssetLoader.Get().InstantiatePrefab("SwitchAccountMenu.prefab:bca3c7466980f484fbf25690f6cef4bf", OnSwitchAccountMenuLoaded);
		}
	}

	public ulong GetHealUpTemporaryAccountGameAccountId()
	{
		return m_healUpTemporaryAccountGameAccountId;
	}

	public int GetLastLoginSelectedTemporaryAccountIndex()
	{
		return m_lastLoginSelectedTemporaryAccountIndex;
	}

	public void LoginTemporaryAccount(TemporaryAccountData.TemporaryAccount temporaryAccount)
	{
		Log.TemporaryAccount.Print("Login Temporary Account");
		Get().SetSelectedTemporaryAccount(temporaryAccount.m_temporaryAccountId);
		Options.Get().SetInt(Option.PREFERRED_REGION, temporaryAccount.m_regionId);
		GameUtils.Logout();
	}

	public void LoginTemporaryAccount(int selectedTemporaryAccountIndex)
	{
		if (!m_isTemporaryAccountDataLoaded)
		{
			LoadTemporaryAccountData();
		}
		if (m_temporaryAccountData == null)
		{
			Log.TemporaryAccount.PrintWarning("Unable to load temporary account data!");
			return;
		}
		if (selectedTemporaryAccountIndex == -1 || selectedTemporaryAccountIndex >= m_temporaryAccountData.m_temporaryAccounts.Count)
		{
			Log.TemporaryAccount.PrintError("Invalid selected Temporary Account index! Index = " + selectedTemporaryAccountIndex);
			return;
		}
		TemporaryAccountData.TemporaryAccount temporaryAccount = m_temporaryAccountData.m_temporaryAccounts[selectedTemporaryAccountIndex];
		LoginTemporaryAccount(temporaryAccount);
	}

	public void PrintTemporaryAccountData()
	{
		if (!m_isTemporaryAccountDataLoaded)
		{
			LoadTemporaryAccountData();
		}
		if (m_temporaryAccountData == null)
		{
			Log.TemporaryAccount.Print("m_temporaryAccountData == null");
			return;
		}
		Log.TemporaryAccount.Print("Selected Account = " + m_temporaryAccountData.m_selectedTemporaryAccountIndex + ", m_lastUpdate = " + Convert.ToDateTime(m_temporaryAccountData.m_lastUpdated));
		foreach (TemporaryAccountData.TemporaryAccount temporaryAccount in m_temporaryAccountData.m_temporaryAccounts)
		{
			Log.TemporaryAccount.Print(string.Concat("[m_temporaryAccountId = ", temporaryAccount.m_temporaryAccountId, ", m_battleTag = ", temporaryAccount.m_battleTag, ", m_regionId = ", temporaryAccount.m_regionId, ", m_lastLogin = ", Convert.ToDateTime(temporaryAccount.m_lastLogin), ", m_isHealedUp = ", temporaryAccount.m_isHealedUp.ToString(), ", m_isMinor = ", temporaryAccount.m_isMinor.ToString(), "]"));
		}
		SortPrint();
	}

	public void Test()
	{
		if (!m_isTemporaryAccountDataLoaded)
		{
			LoadTemporaryAccountData();
		}
		if (m_temporaryAccountData == null)
		{
			Log.TemporaryAccount.Print("m_temporaryAccountData == null");
			return;
		}
		m_temporaryAccountData = new TemporaryAccountData();
		TemporaryAccountData.TemporaryAccount temporaryAccount = new TemporaryAccountData.TemporaryAccount();
		temporaryAccount.m_temporaryAccountId = "BLAH BLAH BLAH";
		temporaryAccount.m_battleTag = "BATTLETAG";
		temporaryAccount.m_lastLogin = DateTime.UtcNow.ToString();
		m_temporaryAccountData.m_temporaryAccounts.Add(temporaryAccount);
		temporaryAccount = new TemporaryAccountData.TemporaryAccount();
		temporaryAccount.m_temporaryAccountId = "HEHEHEHEHEHE";
		temporaryAccount.m_battleTag = "ARGAREOJ";
		temporaryAccount.m_lastLogin = DateTime.UtcNow.ToString();
		m_temporaryAccountData.m_temporaryAccounts.Add(temporaryAccount);
		temporaryAccount = new TemporaryAccountData.TemporaryAccount();
		temporaryAccount.m_temporaryAccountId = "Wha?";
		temporaryAccount.m_battleTag = "YE";
		temporaryAccount.m_lastLogin = DateTime.UtcNow.ToString();
		m_temporaryAccountData.m_temporaryAccounts.Add(temporaryAccount);
		temporaryAccount = new TemporaryAccountData.TemporaryAccount();
		temporaryAccount.m_temporaryAccountId = "SUPER_SECRET_ACCOUNT_ID";
		temporaryAccount.m_battleTag = "GoodKnight";
		temporaryAccount.m_lastLogin = DateTime.UtcNow.ToString();
		m_temporaryAccountData.m_temporaryAccounts.Add(temporaryAccount);
		temporaryAccount = new TemporaryAccountData.TemporaryAccount();
		temporaryAccount.m_temporaryAccountId = "SUPER_SECRET_ACCOUNT_ID";
		temporaryAccount.m_battleTag = "GoodKnight";
		temporaryAccount.m_lastLogin = DateTime.UtcNow.ToString();
		m_temporaryAccountData.m_temporaryAccounts.Add(temporaryAccount);
		SaveTemporaryAccountData();
	}

	public void SortPrint()
	{
		IEnumerable<TemporaryAccountData.TemporaryAccount> sortedTemporaryAccounts = GetSortedTemporaryAccounts();
		Log.TemporaryAccount.Print("Sorted!");
		Log.TemporaryAccount.Print("Selected Account = " + m_temporaryAccountData.m_selectedTemporaryAccountIndex + ", m_lastUpdate = " + Convert.ToDateTime(m_temporaryAccountData.m_lastUpdated));
		foreach (TemporaryAccountData.TemporaryAccount item in sortedTemporaryAccounts)
		{
			Log.TemporaryAccount.Print(string.Concat("[m_temporaryAccountId = ", item.m_temporaryAccountId, ", m_battleTag = ", item.m_battleTag, ", m_regionId = ", item.m_regionId, ", m_lastLogin = ", Convert.ToDateTime(item.m_lastLogin), ", m_isHealedUp = ", item.m_isHealedUp.ToString(), ", m_isMinor = ", item.m_isMinor.ToString(), "]"));
		}
	}

	public void HealUpCompleteTest()
	{
		Options.Get().SetBool(Option.CREATED_ACCOUNT, val: true);
		InGameBrowserManager.Get().Hide();
		HearthstoneApplication.Get().Reset();
	}

	public string NagTimeDebugLog()
	{
		long @long = Options.Get().GetLong(Option.LAST_HEAL_UP_EVENT_DATE);
		DateTime now = DateTime.Now;
		if (@long != 0L)
		{
			int totalWins = GetTotalWins();
			DateTime dateTime = new DateTime(@long);
			TimeSpan timeSpan = now - dateTime;
			int num = 1;
			foreach (int key in HEAL_UP_FREQUENCY.Keys)
			{
				if (totalWins >= key)
				{
					num = HEAL_UP_FREQUENCY[key];
				}
			}
			string text = "Last frequency time: " + dateTime;
			if (timeSpan.TotalHours > (double)num)
			{
				return text + " Next event will trigger nag";
			}
			return text + " Next nag in " + ((double)num - timeSpan.TotalHours) + " hours";
		}
		return "No frequency time saved!";
	}

	private void ShowSignUpPopUp(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_signUpPopUp = go.GetComponent<TemporaryAccountSignUpPopUp>();
		m_signUpPopUp.Show(m_popupArgs, OnHealUpProcessCancelled);
		m_isSignUpPopUpLoading = false;
	}

	private void OnHealUpProcessCancelled()
	{
		if (m_onSignUpDismissedHandler != null)
		{
			m_onSignUpDismissedHandler();
			m_onSignUpDismissedHandler = null;
		}
	}

	private void SaveTemporaryAccountData()
	{
	}

	private void AddCreatedTemporaryAccount(string battleTag)
	{
		if (m_createdTemporaryAccountId == null)
		{
			Log.TemporaryAccount.PrintError("Attempting to add new temporary account without ID!");
			return;
		}
		AdTrackingManager.Get().TrackHeadlessAccountCreated();
		Log.TemporaryAccount.Print("Adding Created Temporary Account, updating data...");
		m_createdTemporaryAccount = new TemporaryAccountData.TemporaryAccount();
		m_createdTemporaryAccount.m_temporaryAccountId = m_createdTemporaryAccountId;
		m_createdTemporaryAccount.m_battleTag = battleTag;
		m_createdTemporaryAccount.m_regionId = (int)MobileDeviceLocale.GetCurrentRegionId();
		m_createdTemporaryAccount.m_lastLogin = DateTime.UtcNow.ToString();
		m_createdTemporaryAccountId = null;
		CloudStorageManager.Get().StartInitialize(OnCloudStorageInitializedAddCreatedTemporaryAccount, GameStrings.Get("GLUE_CLOUD_STORAGE_CONTEXT_BODY_01"));
	}

	private void OnCloudStorageInitializedAddCreatedTemporaryAccount()
	{
		if (!m_isTemporaryAccountDataLoaded)
		{
			LoadTemporaryAccountData();
		}
		if (m_temporaryAccountData == null)
		{
			Log.TemporaryAccount.PrintWarning("Unable to load temporary account data!");
			return;
		}
		if (!m_temporaryAccountData.m_temporaryAccounts.Exists((TemporaryAccountData.TemporaryAccount account) => account.m_temporaryAccountId == m_createdTemporaryAccount.m_temporaryAccountId))
		{
			m_temporaryAccountData.m_temporaryAccounts.Add(m_createdTemporaryAccount);
			m_temporaryAccountData.m_selectedTemporaryAccountIndex = m_temporaryAccountData.m_temporaryAccounts.Count - 1;
			SaveTemporaryAccountData();
		}
		else
		{
			Log.TemporaryAccount.PrintInfo("Did not add temporary account to cloud storage as it was already saved");
		}
		m_createdTemporaryAccount = null;
	}

	private void OnCloudStorageInitializedUpdateTemporaryAccountData()
	{
		if (!m_isTemporaryAccountDataLoaded)
		{
			LoadTemporaryAccountData();
		}
		if (m_temporaryAccountData == null)
		{
			Log.TemporaryAccount.PrintWarning("Unable to load temporary account data!");
		}
		else if (string.IsNullOrEmpty(m_createdTemporaryAccountId) && m_temporaryAccountData.m_selectedTemporaryAccountIndex != -1)
		{
			m_temporaryAccountData.m_temporaryAccounts[m_temporaryAccountData.m_selectedTemporaryAccountIndex].m_lastLogin = DateTime.UtcNow.ToString();
			SaveTemporaryAccountData();
		}
	}

	private void OnCloudStorageInitializedStartShowSwitchAccountMenu()
	{
		if (!m_isTemporaryAccountDataLoaded)
		{
			LoadTemporaryAccountData();
		}
		if (m_temporaryAccountData == null || ValidTemporaryAccountCount() == 0 || m_temporaryAccountData.m_selectedTemporaryAccountIndex != -1)
		{
			if (m_onSwithAccountLogInPressedHandler != null)
			{
				m_onSwithAccountLogInPressedHandler(null);
				m_onSwithAccountLogInPressedHandler = null;
			}
			m_disableSwitchAccountMenuInputBlocker = false;
		}
		else
		{
			ShowSwitchAccountMenu();
		}
	}

	private int ValidTemporaryAccountCount()
	{
		if (!m_isTemporaryAccountDataLoaded)
		{
			LoadTemporaryAccountData();
		}
		if (m_temporaryAccountData == null)
		{
			Log.TemporaryAccount.Print("m_temporaryAccountData == null");
			return 0;
		}
		int num = 0;
		foreach (TemporaryAccountData.TemporaryAccount temporaryAccount in m_temporaryAccountData.m_temporaryAccounts)
		{
			if (!temporaryAccount.m_isHealedUp)
			{
				num++;
			}
		}
		return num;
	}

	private IEnumerable<TemporaryAccountData.TemporaryAccount> GetSortedTemporaryAccounts()
	{
		return m_temporaryAccountData.m_temporaryAccounts.OrderByDescending(delegate(TemporaryAccountData.TemporaryAccount temporaryAccount)
		{
			DateTime.TryParse(temporaryAccount.m_lastLogin, out var result);
			return result;
		});
	}

	private void OnPresenceChanged(PresenceUpdate[] updates)
	{
		if (string.IsNullOrEmpty(m_createdTemporaryAccountId))
		{
			BnetPresenceMgr.Get().OnGameAccountPresenceChange -= OnPresenceChanged;
			return;
		}
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		if (myPlayer != null)
		{
			AddCreatedTemporaryAccount(myPlayer.GetBattleTag().GetName());
			BnetPresenceMgr.Get().OnGameAccountPresenceChange -= OnPresenceChanged;
		}
	}

	private void OnSwitchAccountMenuLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_switchAccountMenu = go.GetComponent<SwitchAccountMenu>();
		m_switchAccountMenu.AddTemporaryAccountButtons(GetSortedTemporaryAccounts(), GetSelectedTemporaryAccountId());
		m_switchAccountMenu.Show(m_onSwithAccountLogInPressedHandler);
		if (m_disableSwitchAccountMenuInputBlocker)
		{
			m_switchAccountMenu.DisableInputBlocker();
			m_disableSwitchAccountMenuInputBlocker = false;
		}
		m_onSwithAccountLogInPressedHandler = null;
		m_isSwitchAccountMenuLoading = false;
	}

	private bool OnBnetError(BnetErrorInfo info, object userData)
	{
		if (m_handledDisconnect && m_handledHealUpError)
		{
			return false;
		}
		BattleNetErrors error = info.GetError();
		Log.TemporaryAccount.Print("OnBnetError: " + error);
		switch (error)
		{
		case BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED:
			Log.TemporaryAccount.Print("Handled Disconnect");
			m_handledDisconnect = true;
			return true;
		case BattleNetErrors.ERROR_SESSION_DATA_CHANGED:
		case BattleNetErrors.ERROR_SESSION_ADMIN_KICK:
			Log.TemporaryAccount.Print("Handled Heal Up Error");
			m_handledHealUpError = true;
			return true;
		default:
			return false;
		}
	}

	private int GetTotalWins()
	{
		int num = 0;
		if (NetCache.Get() == null || NetCache.Get().GetNetObject<NetCache.NetCachePlayerRecords>() == null || NetCache.Get().GetNetObject<NetCache.NetCachePlayerRecords>().Records == null)
		{
			return num;
		}
		foreach (NetCache.PlayerRecord record in NetCache.Get().GetNetObject<NetCache.NetCachePlayerRecords>().Records)
		{
			if (record.Data == 0)
			{
				switch (record.RecordType)
				{
				case GameType.GT_VS_AI:
				case GameType.GT_ARENA:
				case GameType.GT_RANKED:
				case GameType.GT_CASUAL:
				case GameType.GT_TAVERNBRAWL:
				case GameType.GT_FSG_BRAWL:
				case GameType.GT_FSG_BRAWL_2P_COOP:
					num += record.Wins;
					break;
				}
			}
		}
		return num;
	}

	private string Encrypt(string toEncrypt)
	{
		byte[] bytes = Encoding.UTF8.GetBytes("v9OJ4mkM9Za*g8gQdw#WA12KA1DGA&Q7");
		byte[] array = Crypto.Rijndael.Encrypt(Encoding.UTF8.GetBytes(toEncrypt), bytes);
		return Convert.ToBase64String(array, 0, array.Length);
	}

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
			Log.TemporaryAccount.Print("Decrypt: {0} {1}", toDecrypt, ex.Message);
		}
		return null;
	}

	private bool IsValid(TemporaryAccountData data)
	{
		if (data == null || data.m_temporaryAccounts == null || string.IsNullOrEmpty(data.m_lastUpdated))
		{
			Log.TemporaryAccount.Print("Invalid data found: null");
			return false;
		}
		if (!DateTime.TryParse(data.m_lastUpdated, out var date))
		{
			Log.TemporaryAccount.Print("Invalid date format in m_lastUpdated: {0}", data.m_lastUpdated);
			return false;
		}
		if (data.m_temporaryAccounts.Any(delegate(TemporaryAccountData.TemporaryAccount a)
		{
			bool flag = DateTime.TryParse(a.m_lastLogin, out date);
			if (!flag)
			{
				Log.TemporaryAccount.Print("Invalid date format in m_lastLogin: {0}", a.m_lastLogin);
			}
			return !flag;
		}))
		{
			return false;
		}
		return true;
	}
}
