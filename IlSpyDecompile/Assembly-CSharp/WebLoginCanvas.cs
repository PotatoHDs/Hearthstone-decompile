using System;
using System.Collections.Generic;
using bgs;
using Hearthstone;
using Hearthstone.Login;
using UnityEngine;

public class WebLoginCanvas : MonoBehaviour
{
	public DropdownControl m_regionSelectDropdownPrefab;

	public GameObject m_regionSelectDropdownBone;

	public GameObject m_lowerRegionSelectDropdownBone;

	public GameObject m_regionSelectContents;

	public GameObject m_accountCreation;

	public AccountCreationFlipbook m_flipbook;

	public PegUIElement m_backButton;

	public GameObject m_regionSelectTooltipBone;

	public GameObject m_topLeftBone;

	public GameObject m_bottomRightBone;

	public UIBButton m_regionButton;

	private object m_selection;

	private object m_prevSelection;

	private float m_acFlipbookSwap = 30f;

	private float m_acFlipbookCur;

	private bool m_canGoBack;

	private DropdownControl m_regionSelector;

	private TooltipPanel m_regionSelectTooltip;

	private static Map<constants.BnetRegion, string> s_regionStringNames = new Map<constants.BnetRegion, string>
	{
		{
			constants.BnetRegion.REGION_UNKNOWN,
			"Cuba"
		},
		{
			constants.BnetRegion.REGION_US,
			"GLOBAL_REGION_AMERICAS"
		},
		{
			constants.BnetRegion.REGION_EU,
			"GLOBAL_REGION_EUROPE"
		},
		{
			constants.BnetRegion.REGION_KR,
			"GLOBAL_REGION_ASIA"
		},
		{
			constants.BnetRegion.REGION_TW,
			"Taiwan"
		},
		{
			constants.BnetRegion.REGION_CN,
			"GLOBAL_REGION_CHINA"
		},
		{
			constants.BnetRegion.REGION_LIVE_VERIFICATION,
			"LiveVerif"
		}
	};

	private PlatformDependentValue<bool> USE_REGION_DROPDOWN = new PlatformDependentValue<bool>(PlatformCategory.Screen)
	{
		PC = true,
		Tablet = true,
		Phone = false
	};

	private Map<constants.BnetRegion, string> m_regionNames;

	private RegionMenu m_regionMenu;

	private PlatformDependentValue<bool> KOBOLD_SHOWN_ON_ACCOUNT_CREATION = new PlatformDependentValue<bool>(PlatformCategory.Screen)
	{
		PC = true,
		Tablet = true,
		Phone = false
	};

	private void Awake()
	{
		bool flag = false;
		OverlayUI.Get().AddGameObject(base.gameObject);
		if (PlatformSettings.LocaleVariant != LocaleVariant.China || (HearthstoneApplication.IsInternal() && HearthstoneApplication.GetMobileEnvironment() == MobileEnv.DEVELOPMENT))
		{
			InitRegionSelection();
		}
		else if (m_regionButton != null)
		{
			m_regionButton.gameObject.SetActive(value: false);
		}
		m_backButton.AddEventListener(UIEventType.RELEASE, OnBackPressed);
		if (!RequiredToLogin() || WasPreviouslyLoggedIntoTemporaryAccount())
		{
			m_backButton.gameObject.SetActive(value: true);
		}
	}

	private void Start()
	{
		Navigation.Push(OnNavigateBack);
	}

	private void Update()
	{
		if (!KOBOLD_SHOWN_ON_ACCOUNT_CREATION)
		{
			return;
		}
		if (m_acFlipbookCur < m_acFlipbookSwap)
		{
			m_acFlipbookCur += Time.deltaTime * 60f;
			return;
		}
		Material sharedMaterial = m_flipbook.m_acFlipbook.GetComponent<Renderer>().GetSharedMaterial();
		if (sharedMaterial.mainTexture == m_flipbook.m_acFlipbookTextures[0])
		{
			sharedMaterial.mainTexture = m_flipbook.m_acFlipbookTextures[1];
			m_acFlipbookSwap = m_flipbook.m_acFlipbookTimeAlt;
		}
		else
		{
			sharedMaterial.mainTexture = m_flipbook.m_acFlipbookTextures[0];
			m_acFlipbookSwap = UnityEngine.Random.Range(m_flipbook.m_acFlipbookTimeMin, m_flipbook.m_acFlipbookTimeMax);
		}
		m_acFlipbookCur = 0f;
	}

	private void OnDestroy()
	{
		if (m_regionSelectTooltip != null)
		{
			UnityEngine.Object.Destroy(m_regionSelectTooltip.gameObject);
		}
	}

	public void WebViewDidFinishLoad(string pageState)
	{
		Debug.Log("web view page state: " + pageState);
		if (pageState == null)
		{
			return;
		}
		string[] array = pageState.Split(new string[1] { "|" }, StringSplitOptions.None);
		if (array.Length < 2)
		{
			Debug.LogWarning($"WebViewDidFinishLoad() - Invalid parsed pageState ({pageState})");
			return;
		}
		m_canGoBack = array[array.Length - 1].Equals("canGoBack");
		bool active = false;
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < array.Length - 1; i++)
		{
			string obj = array[i];
			if (obj.Equals("STATE_ACCOUNT_CREATION", StringComparison.InvariantCultureIgnoreCase))
			{
				active = true;
			}
			if (obj.Equals("STATE_ACCOUNT_CREATED", StringComparison.InvariantCultureIgnoreCase))
			{
				flag = true;
			}
			if (obj.Equals("STATE_NO_BACK", StringComparison.InvariantCultureIgnoreCase))
			{
				flag2 = true;
			}
		}
		if ((bool)KOBOLD_SHOWN_ON_ACCOUNT_CREATION)
		{
			m_accountCreation.SetActive(active);
		}
		flag2 = flag2 || flag;
		if (flag)
		{
			Options.Get().SetBool(Option.CREATED_ACCOUNT, val: true);
		}
		m_backButton.gameObject.SetActive(!flag2 && (m_canGoBack || !RequiredToLogin() || WasPreviouslyLoggedIntoTemporaryAccount()));
	}

	public void WebViewBackButtonPressed(string dummyState)
	{
		Navigation.GoBack();
	}

	private void InitRegionSelection()
	{
		bool flag = HearthstoneApplication.GetMobileEnvironment() == MobileEnv.DEVELOPMENT;
		m_regionNames = new Map<constants.BnetRegion, string>();
		foreach (constants.BnetRegion key in s_regionStringNames.Keys)
		{
			if (flag)
			{
				m_regionNames[key] = GameStrings.Get(s_regionStringNames[key]).Split(' ')[0];
			}
			else
			{
				m_regionNames[key] = GameStrings.Get(s_regionStringNames[key]);
			}
		}
		if ((bool)USE_REGION_DROPDOWN)
		{
			SetUpRegionDropdown();
		}
		else
		{
			SetUpRegionButton();
		}
		if ((bool)UniversalInputManager.UsePhoneUI && flag)
		{
			SetUpRegionDropdown();
		}
	}

	private void SetUpRegionButton()
	{
		if (m_regionButton != null)
		{
			m_regionButton.AddEventListener(UIEventType.RELEASE, delegate
			{
				ShowRegionMenu();
			});
			string text = onRegionText(MobileDeviceLocale.GetCurrentRegionId());
			m_regionButton.SetText(text);
		}
	}

	private void SetUpRegionDropdown()
	{
		bool num = HearthstoneApplication.GetMobileEnvironment() == MobileEnv.DEVELOPMENT;
		m_regionSelector = UnityEngine.Object.Instantiate(m_regionSelectDropdownPrefab);
		m_regionSelector.gameObject.SetActive(value: true);
		m_regionSelector.transform.parent = base.gameObject.transform;
		TransformUtil.CopyLocal(m_regionSelector.transform, m_regionSelectDropdownBone.transform);
		SceneUtils.SetLayer(m_regionSelector, GameLayer.HighPriorityUI);
		m_regionSelector.clearItems();
		m_regionSelector.setItemTextCallback(onRegionText);
		m_regionSelector.setMenuShownCallback(onMenuShown);
		m_regionSelector.setItemChosenCallback(onRegionWarning);
		if (num)
		{
			foreach (constants.BnetRegion key in MobileDeviceLocale.s_regionIdToDevIP.Keys)
			{
				m_regionSelector.addItem(key);
			}
			TransformUtil.CopyLocal(m_regionSelector.transform, m_lowerRegionSelectDropdownBone.transform);
		}
		else
		{
			m_regionSelector.addItem(constants.BnetRegion.REGION_US);
			m_regionSelector.addItem(constants.BnetRegion.REGION_EU);
			m_regionSelector.addItem(constants.BnetRegion.REGION_KR);
		}
		constants.BnetRegion currentRegionId = MobileDeviceLocale.GetCurrentRegionId();
		m_regionSelector.setSelection(currentRegionId);
		if (MobileDeviceLocale.UseClientConfigForEnv())
		{
			m_regionSelector.gameObject.SetActive(value: false);
		}
		m_regionSelectTooltip = TooltipPanelManager.Get().CreateKeywordPanel(0);
		m_regionSelectTooltip.Reset();
		m_regionSelectTooltip.Initialize(GameStrings.Get("GLUE_MOBILE_REGION_SELECT_TOOLTIP_HEADER"), GameStrings.Get("GLUE_MOBILE_REGION_SELECT_TOOLTIP"));
		m_regionSelectTooltip.transform.position = m_regionSelectTooltipBone.transform.position;
		m_regionSelectTooltip.transform.localScale = m_regionSelectTooltipBone.transform.localScale;
		m_regionSelectTooltip.transform.eulerAngles = new Vector3(0f, 0f, 0f);
		SceneUtils.SetLayer(m_regionSelectTooltip.gameObject, GameLayer.HighPriorityUI);
		m_regionSelectTooltip.gameObject.SetActive(value: false);
	}

	private void onMenuShown(bool shown)
	{
		if (shown)
		{
			m_regionSelectTooltip.gameObject.SetActive(value: true);
			WebAuth.UpdateRegionSelectVisualState(isVisible: true);
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				SplashScreen.Get().HideWebAuth();
			}
		}
		else
		{
			m_regionSelectTooltip.gameObject.SetActive(value: false);
			WebAuth.UpdateRegionSelectVisualState(isVisible: false);
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				SplashScreen.Get().UnHideWebAuth();
			}
		}
	}

	private void onRegionChange(object selection, object prevSelection)
	{
		if (selection != prevSelection)
		{
			constants.BnetRegion val = (constants.BnetRegion)selection;
			Options.Get().SetInt(Option.PREFERRED_REGION, (int)val);
			CauseReconnect();
		}
	}

	private void onRegionChangeCB(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CONFIRM)
		{
			onRegionChange(m_selection, m_prevSelection);
		}
		else
		{
			if (m_regionSelector != null)
			{
				m_regionSelector.setSelection(m_prevSelection);
			}
			SplashScreen.Get().UnHideWebAuth();
		}
		m_regionSelectContents.SetActive(value: false);
	}

	private void ShowRegionMenu()
	{
		if (m_regionMenu != null)
		{
			m_regionMenu.Show();
			return;
		}
		GameObject gameObject = (GameObject)GameUtils.InstantiateGameObject("RegionMenu.prefab:81394e6ea3adb1140a29ff4b44744891");
		m_regionMenu = gameObject.GetComponent<RegionMenu>();
		List<UIBButton> buttons = new List<UIBButton>();
		Debug.Log("creating region menu..");
		AddButtonForRegion(buttons, constants.BnetRegion.REGION_US);
		AddButtonForRegion(buttons, constants.BnetRegion.REGION_EU);
		AddButtonForRegion(buttons, constants.BnetRegion.REGION_KR);
		m_regionMenu.SetButtons(buttons);
		m_regionMenu.Show();
	}

	private void AddButtonForRegion(List<UIBButton> buttons, constants.BnetRegion region)
	{
		constants.BnetRegion currentRegion = MobileDeviceLocale.GetCurrentRegionId();
		buttons.Add(m_regionMenu.CreateMenuButton(null, onRegionText(region), delegate
		{
			m_regionMenu.Hide();
			onRegionWarning(region, currentRegion);
		}));
	}

	private string onRegionText(object val)
	{
		constants.BnetRegion bnetRegion = (constants.BnetRegion)val;
		string value = string.Empty;
		m_regionNames.TryGetValue(bnetRegion, out value);
		if (HearthstoneApplication.GetMobileEnvironment() == MobileEnv.DEVELOPMENT)
		{
			MobileDeviceLocale.ConnectionData connectionDataFromRegionId = MobileDeviceLocale.GetConnectionDataFromRegionId(bnetRegion, isDev: true);
			string text = connectionDataFromRegionId.name;
			if (string.IsNullOrEmpty(text))
			{
				text = $"{connectionDataFromRegionId.address.Split('-')[0]}:{connectionDataFromRegionId.port}:{connectionDataFromRegionId.version}";
			}
			value = ((!string.IsNullOrEmpty(value)) ? $"{text} ({value})" : text);
		}
		return value;
	}

	private void onRegionWarning(object selection, object prevSelection)
	{
		m_selection = selection;
		m_prevSelection = prevSelection;
		if (!selection.Equals(prevSelection))
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_MOBILE_REGION_SELECT_WARNING_HEADER");
			popupInfo.m_text = GameStrings.Get("GLUE_MOBILE_REGION_SELECT_WARNING");
			popupInfo.m_showAlertIcon = false;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
			popupInfo.m_responseCallback = onRegionChangeCB;
			popupInfo.m_padding = 60f;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				popupInfo.m_padding = 80f;
			}
			popupInfo.m_layerToUse = GameLayer.HighPriorityUI;
			SplashScreen.Get().HideWebAuth();
			DialogManager.Get().ShowPopup(popupInfo, OnDialogProcess);
		}
	}

	private bool OnDialogProcess(DialogBase dialog, object userData)
	{
		GameObject obj = UnityEngine.Object.Instantiate(m_regionSelectContents);
		TransformUtil.AttachAndPreserveLocalTransform(obj.transform, dialog.transform);
		obj.SetActive(value: true);
		return true;
	}

	private void OnBackPressed(UIEvent e)
	{
		Navigation.GoBack();
	}

	private bool OnNavigateBack()
	{
		if (m_canGoBack)
		{
			WebAuth.GoBackWebPage();
		}
		else
		{
			if (!RequiredToLogin())
			{
				CloseWebAuth();
				HearthstoneApplication.Get().Reset();
				return true;
			}
			if (WasPreviouslyLoggedIntoTemporaryAccount())
			{
				CloseWebAuth();
				LogIntoTempAccount();
				return true;
			}
		}
		return false;
	}

	private static bool RequiredToLogin()
	{
		return Options.Get().GetBool(Option.CONNECT_TO_AURORA);
	}

	private static void CloseWebAuth()
	{
		WebAuthDisplay.CloseWebAuth();
	}

	private static bool WasPreviouslyLoggedIntoTemporaryAccount()
	{
		TemporaryAccountManager temporaryAccountManager = TemporaryAccountManager.Get();
		if (temporaryAccountManager == null)
		{
			return false;
		}
		return temporaryAccountManager.GetLastLoginSelectedTemporaryAccountIndex() != -1;
	}

	private static void LogIntoTempAccount()
	{
		TemporaryAccountManager temporaryAccountManager = TemporaryAccountManager.Get();
		if (temporaryAccountManager == null)
		{
			Log.Login.PrintError("Could not return back to previous temp account from web view, TemporaryAccountManager was null!");
			return;
		}
		int lastLoginSelectedTemporaryAccountIndex = temporaryAccountManager.GetLastLoginSelectedTemporaryAccountIndex();
		temporaryAccountManager.LoginTemporaryAccount(lastLoginSelectedTemporaryAccountIndex);
	}

	private void CauseReconnect()
	{
		WebAuth.ClearLoginData();
		BattleNet.RequestCloseAurora();
		HearthstoneApplication.Get().ResetAndForceLogin();
	}
}
