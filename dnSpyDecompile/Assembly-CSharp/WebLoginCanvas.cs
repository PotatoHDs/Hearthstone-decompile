using System;
using System.Collections.Generic;
using bgs;
using Hearthstone;
using Hearthstone.Login;
using UnityEngine;

// Token: 0x020006E6 RID: 1766
public class WebLoginCanvas : MonoBehaviour
{
	// Token: 0x0600624D RID: 25165 RVA: 0x00200F6C File Offset: 0x001FF16C
	private void Awake()
	{
		OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
		bool flag = PlatformSettings.LocaleVariant != LocaleVariant.China || (HearthstoneApplication.IsInternal() && HearthstoneApplication.GetMobileEnvironment() == MobileEnv.DEVELOPMENT);
		if (flag)
		{
			this.InitRegionSelection();
		}
		else if (this.m_regionButton != null)
		{
			this.m_regionButton.gameObject.SetActive(false);
		}
		this.m_backButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBackPressed));
		if (!WebLoginCanvas.RequiredToLogin() || WebLoginCanvas.WasPreviouslyLoggedIntoTemporaryAccount())
		{
			this.m_backButton.gameObject.SetActive(true);
		}
	}

	// Token: 0x0600624E RID: 25166 RVA: 0x0020100F File Offset: 0x001FF20F
	private void Start()
	{
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
	}

	// Token: 0x0600624F RID: 25167 RVA: 0x00201024 File Offset: 0x001FF224
	private void Update()
	{
		if (this.KOBOLD_SHOWN_ON_ACCOUNT_CREATION)
		{
			if (this.m_acFlipbookCur < this.m_acFlipbookSwap)
			{
				this.m_acFlipbookCur += Time.deltaTime * 60f;
				return;
			}
			Material sharedMaterial = this.m_flipbook.m_acFlipbook.GetComponent<Renderer>().GetSharedMaterial();
			if (sharedMaterial.mainTexture == this.m_flipbook.m_acFlipbookTextures[0])
			{
				sharedMaterial.mainTexture = this.m_flipbook.m_acFlipbookTextures[1];
				this.m_acFlipbookSwap = this.m_flipbook.m_acFlipbookTimeAlt;
			}
			else
			{
				sharedMaterial.mainTexture = this.m_flipbook.m_acFlipbookTextures[0];
				this.m_acFlipbookSwap = UnityEngine.Random.Range(this.m_flipbook.m_acFlipbookTimeMin, this.m_flipbook.m_acFlipbookTimeMax);
			}
			this.m_acFlipbookCur = 0f;
		}
	}

	// Token: 0x06006250 RID: 25168 RVA: 0x002010FD File Offset: 0x001FF2FD
	private void OnDestroy()
	{
		if (this.m_regionSelectTooltip != null)
		{
			UnityEngine.Object.Destroy(this.m_regionSelectTooltip.gameObject);
		}
	}

	// Token: 0x06006251 RID: 25169 RVA: 0x00201120 File Offset: 0x001FF320
	public void WebViewDidFinishLoad(string pageState)
	{
		Debug.Log("web view page state: " + pageState);
		if (pageState == null)
		{
			return;
		}
		string[] array = pageState.Split(new string[]
		{
			"|"
		}, StringSplitOptions.None);
		if (array.Length < 2)
		{
			Debug.LogWarning(string.Format("WebViewDidFinishLoad() - Invalid parsed pageState ({0})", pageState));
			return;
		}
		this.m_canGoBack = array[array.Length - 1].Equals("canGoBack");
		bool active = false;
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < array.Length - 1; i++)
		{
			string text = array[i];
			if (text.Equals("STATE_ACCOUNT_CREATION", StringComparison.InvariantCultureIgnoreCase))
			{
				active = true;
			}
			if (text.Equals("STATE_ACCOUNT_CREATED", StringComparison.InvariantCultureIgnoreCase))
			{
				flag = true;
			}
			if (text.Equals("STATE_NO_BACK", StringComparison.InvariantCultureIgnoreCase))
			{
				flag2 = true;
			}
		}
		if (this.KOBOLD_SHOWN_ON_ACCOUNT_CREATION)
		{
			this.m_accountCreation.SetActive(active);
		}
		flag2 = (flag2 || flag);
		if (flag)
		{
			Options.Get().SetBool(Option.CREATED_ACCOUNT, true);
		}
		this.m_backButton.gameObject.SetActive(!flag2 && (this.m_canGoBack || !WebLoginCanvas.RequiredToLogin() || WebLoginCanvas.WasPreviouslyLoggedIntoTemporaryAccount()));
	}

	// Token: 0x06006252 RID: 25170 RVA: 0x00004EB5 File Offset: 0x000030B5
	public void WebViewBackButtonPressed(string dummyState)
	{
		Navigation.GoBack();
	}

	// Token: 0x06006253 RID: 25171 RVA: 0x0020122C File Offset: 0x001FF42C
	private void InitRegionSelection()
	{
		bool flag = HearthstoneApplication.GetMobileEnvironment() == MobileEnv.DEVELOPMENT;
		this.m_regionNames = new global::Map<constants.BnetRegion, string>();
		foreach (constants.BnetRegion key in WebLoginCanvas.s_regionStringNames.Keys)
		{
			if (flag)
			{
				this.m_regionNames[key] = GameStrings.Get(WebLoginCanvas.s_regionStringNames[key]).Split(new char[]
				{
					' '
				})[0];
			}
			else
			{
				this.m_regionNames[key] = GameStrings.Get(WebLoginCanvas.s_regionStringNames[key]);
			}
		}
		if (this.USE_REGION_DROPDOWN)
		{
			this.SetUpRegionDropdown();
		}
		else
		{
			this.SetUpRegionButton();
		}
		if (UniversalInputManager.UsePhoneUI && flag)
		{
			this.SetUpRegionDropdown();
		}
	}

	// Token: 0x06006254 RID: 25172 RVA: 0x00201310 File Offset: 0x001FF510
	private void SetUpRegionButton()
	{
		if (this.m_regionButton != null)
		{
			this.m_regionButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.ShowRegionMenu();
			});
			string text = this.onRegionText(MobileDeviceLocale.GetCurrentRegionId());
			this.m_regionButton.SetText(text);
		}
	}

	// Token: 0x06006255 RID: 25173 RVA: 0x00201364 File Offset: 0x001FF564
	private void SetUpRegionDropdown()
	{
		bool flag = HearthstoneApplication.GetMobileEnvironment() == MobileEnv.DEVELOPMENT;
		this.m_regionSelector = UnityEngine.Object.Instantiate<DropdownControl>(this.m_regionSelectDropdownPrefab);
		this.m_regionSelector.gameObject.SetActive(true);
		this.m_regionSelector.transform.parent = base.gameObject.transform;
		TransformUtil.CopyLocal(this.m_regionSelector.transform, this.m_regionSelectDropdownBone.transform);
		SceneUtils.SetLayer(this.m_regionSelector, GameLayer.HighPriorityUI);
		this.m_regionSelector.clearItems();
		this.m_regionSelector.setItemTextCallback(new DropdownControl.itemTextCallback(this.onRegionText));
		this.m_regionSelector.setMenuShownCallback(new DropdownControl.menuShownCallback(this.onMenuShown));
		this.m_regionSelector.setItemChosenCallback(new DropdownControl.itemChosenCallback(this.onRegionWarning));
		if (flag)
		{
			foreach (constants.BnetRegion bnetRegion in MobileDeviceLocale.s_regionIdToDevIP.Keys)
			{
				this.m_regionSelector.addItem(bnetRegion);
			}
			TransformUtil.CopyLocal(this.m_regionSelector.transform, this.m_lowerRegionSelectDropdownBone.transform);
		}
		else
		{
			this.m_regionSelector.addItem(constants.BnetRegion.REGION_US);
			this.m_regionSelector.addItem(constants.BnetRegion.REGION_EU);
			this.m_regionSelector.addItem(constants.BnetRegion.REGION_KR);
		}
		constants.BnetRegion currentRegionId = MobileDeviceLocale.GetCurrentRegionId();
		this.m_regionSelector.setSelection(currentRegionId);
		if (MobileDeviceLocale.UseClientConfigForEnv())
		{
			this.m_regionSelector.gameObject.SetActive(false);
		}
		this.m_regionSelectTooltip = TooltipPanelManager.Get().CreateKeywordPanel(0);
		this.m_regionSelectTooltip.Reset();
		this.m_regionSelectTooltip.Initialize(GameStrings.Get("GLUE_MOBILE_REGION_SELECT_TOOLTIP_HEADER"), GameStrings.Get("GLUE_MOBILE_REGION_SELECT_TOOLTIP"));
		this.m_regionSelectTooltip.transform.position = this.m_regionSelectTooltipBone.transform.position;
		this.m_regionSelectTooltip.transform.localScale = this.m_regionSelectTooltipBone.transform.localScale;
		this.m_regionSelectTooltip.transform.eulerAngles = new Vector3(0f, 0f, 0f);
		SceneUtils.SetLayer(this.m_regionSelectTooltip.gameObject, GameLayer.HighPriorityUI);
		this.m_regionSelectTooltip.gameObject.SetActive(false);
	}

	// Token: 0x06006256 RID: 25174 RVA: 0x002015C8 File Offset: 0x001FF7C8
	private void onMenuShown(bool shown)
	{
		if (shown)
		{
			this.m_regionSelectTooltip.gameObject.SetActive(true);
			WebAuth.UpdateRegionSelectVisualState(true);
			if (UniversalInputManager.UsePhoneUI)
			{
				SplashScreen.Get().HideWebAuth();
				return;
			}
		}
		else
		{
			this.m_regionSelectTooltip.gameObject.SetActive(false);
			WebAuth.UpdateRegionSelectVisualState(false);
			if (UniversalInputManager.UsePhoneUI)
			{
				SplashScreen.Get().UnHideWebAuth();
			}
		}
	}

	// Token: 0x06006257 RID: 25175 RVA: 0x00201634 File Offset: 0x001FF834
	private void onRegionChange(object selection, object prevSelection)
	{
		if (selection == prevSelection)
		{
			return;
		}
		constants.BnetRegion val = (constants.BnetRegion)selection;
		Options.Get().SetInt(Option.PREFERRED_REGION, (int)val);
		this.CauseReconnect();
	}

	// Token: 0x06006258 RID: 25176 RVA: 0x00201660 File Offset: 0x001FF860
	private void onRegionChangeCB(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CONFIRM)
		{
			this.onRegionChange(this.m_selection, this.m_prevSelection);
		}
		else
		{
			if (this.m_regionSelector != null)
			{
				this.m_regionSelector.setSelection(this.m_prevSelection);
			}
			SplashScreen.Get().UnHideWebAuth();
		}
		this.m_regionSelectContents.SetActive(false);
	}

	// Token: 0x06006259 RID: 25177 RVA: 0x002016BC File Offset: 0x001FF8BC
	private void ShowRegionMenu()
	{
		if (this.m_regionMenu != null)
		{
			this.m_regionMenu.Show();
			return;
		}
		GameObject gameObject = (GameObject)GameUtils.InstantiateGameObject("RegionMenu.prefab:81394e6ea3adb1140a29ff4b44744891", null, false);
		this.m_regionMenu = gameObject.GetComponent<RegionMenu>();
		List<UIBButton> buttons = new List<UIBButton>();
		Debug.Log("creating region menu..");
		this.AddButtonForRegion(buttons, constants.BnetRegion.REGION_US);
		this.AddButtonForRegion(buttons, constants.BnetRegion.REGION_EU);
		this.AddButtonForRegion(buttons, constants.BnetRegion.REGION_KR);
		this.m_regionMenu.SetButtons(buttons);
		this.m_regionMenu.Show();
	}

	// Token: 0x0600625A RID: 25178 RVA: 0x00201740 File Offset: 0x001FF940
	private void AddButtonForRegion(List<UIBButton> buttons, constants.BnetRegion region)
	{
		constants.BnetRegion currentRegion = MobileDeviceLocale.GetCurrentRegionId();
		buttons.Add(this.m_regionMenu.CreateMenuButton(null, this.onRegionText(region), delegate(UIEvent e)
		{
			this.m_regionMenu.Hide();
			this.onRegionWarning(region, currentRegion);
		}));
	}

	// Token: 0x0600625B RID: 25179 RVA: 0x0020179C File Offset: 0x001FF99C
	private string onRegionText(object val)
	{
		constants.BnetRegion bnetRegion = (constants.BnetRegion)val;
		string text = string.Empty;
		this.m_regionNames.TryGetValue(bnetRegion, out text);
		if (HearthstoneApplication.GetMobileEnvironment() == MobileEnv.DEVELOPMENT)
		{
			MobileDeviceLocale.ConnectionData connectionDataFromRegionId = MobileDeviceLocale.GetConnectionDataFromRegionId(bnetRegion, true);
			string text2 = connectionDataFromRegionId.name;
			if (string.IsNullOrEmpty(text2))
			{
				text2 = string.Format("{0}:{1}:{2}", connectionDataFromRegionId.address.Split(new char[]
				{
					'-'
				})[0], connectionDataFromRegionId.port, connectionDataFromRegionId.version);
			}
			if (string.IsNullOrEmpty(text))
			{
				text = text2;
			}
			else
			{
				text = string.Format("{0} ({1})", text2, text);
			}
		}
		return text;
	}

	// Token: 0x0600625C RID: 25180 RVA: 0x00201834 File Offset: 0x001FFA34
	private void onRegionWarning(object selection, object prevSelection)
	{
		this.m_selection = selection;
		this.m_prevSelection = prevSelection;
		if (selection.Equals(prevSelection))
		{
			return;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_MOBILE_REGION_SELECT_WARNING_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_MOBILE_REGION_SELECT_WARNING");
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.onRegionChangeCB);
		popupInfo.m_padding = 60f;
		if (UniversalInputManager.UsePhoneUI)
		{
			popupInfo.m_padding = 80f;
		}
		popupInfo.m_layerToUse = new GameLayer?(GameLayer.HighPriorityUI);
		SplashScreen.Get().HideWebAuth();
		DialogManager.Get().ShowPopup(popupInfo, new DialogManager.DialogProcessCallback(this.OnDialogProcess));
	}

	// Token: 0x0600625D RID: 25181 RVA: 0x002018EF File Offset: 0x001FFAEF
	private bool OnDialogProcess(DialogBase dialog, object userData)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_regionSelectContents);
		TransformUtil.AttachAndPreserveLocalTransform(gameObject.transform, dialog.transform);
		gameObject.SetActive(true);
		return true;
	}

	// Token: 0x0600625E RID: 25182 RVA: 0x00004EB5 File Offset: 0x000030B5
	private void OnBackPressed(UIEvent e)
	{
		Navigation.GoBack();
	}

	// Token: 0x0600625F RID: 25183 RVA: 0x00201914 File Offset: 0x001FFB14
	private bool OnNavigateBack()
	{
		if (this.m_canGoBack)
		{
			WebAuth.GoBackWebPage();
		}
		else
		{
			if (!WebLoginCanvas.RequiredToLogin())
			{
				WebLoginCanvas.CloseWebAuth();
				HearthstoneApplication.Get().Reset();
				return true;
			}
			if (WebLoginCanvas.WasPreviouslyLoggedIntoTemporaryAccount())
			{
				WebLoginCanvas.CloseWebAuth();
				WebLoginCanvas.LogIntoTempAccount();
				return true;
			}
		}
		return false;
	}

	// Token: 0x06006260 RID: 25184 RVA: 0x00201951 File Offset: 0x001FFB51
	private static bool RequiredToLogin()
	{
		return Options.Get().GetBool(Option.CONNECT_TO_AURORA);
	}

	// Token: 0x06006261 RID: 25185 RVA: 0x0020195F File Offset: 0x001FFB5F
	private static void CloseWebAuth()
	{
		WebAuthDisplay.CloseWebAuth();
	}

	// Token: 0x06006262 RID: 25186 RVA: 0x00201968 File Offset: 0x001FFB68
	private static bool WasPreviouslyLoggedIntoTemporaryAccount()
	{
		TemporaryAccountManager temporaryAccountManager = TemporaryAccountManager.Get();
		return temporaryAccountManager != null && temporaryAccountManager.GetLastLoginSelectedTemporaryAccountIndex() != -1;
	}

	// Token: 0x06006263 RID: 25187 RVA: 0x0020198C File Offset: 0x001FFB8C
	private static void LogIntoTempAccount()
	{
		TemporaryAccountManager temporaryAccountManager = TemporaryAccountManager.Get();
		if (temporaryAccountManager == null)
		{
			global::Log.Login.PrintError("Could not return back to previous temp account from web view, TemporaryAccountManager was null!", Array.Empty<object>());
			return;
		}
		int lastLoginSelectedTemporaryAccountIndex = temporaryAccountManager.GetLastLoginSelectedTemporaryAccountIndex();
		temporaryAccountManager.LoginTemporaryAccount(lastLoginSelectedTemporaryAccountIndex);
	}

	// Token: 0x06006264 RID: 25188 RVA: 0x002019C5 File Offset: 0x001FFBC5
	private void CauseReconnect()
	{
		WebAuth.ClearLoginData();
		BattleNet.RequestCloseAurora();
		HearthstoneApplication.Get().ResetAndForceLogin();
	}

	// Token: 0x040051BC RID: 20924
	public DropdownControl m_regionSelectDropdownPrefab;

	// Token: 0x040051BD RID: 20925
	public GameObject m_regionSelectDropdownBone;

	// Token: 0x040051BE RID: 20926
	public GameObject m_lowerRegionSelectDropdownBone;

	// Token: 0x040051BF RID: 20927
	public GameObject m_regionSelectContents;

	// Token: 0x040051C0 RID: 20928
	public GameObject m_accountCreation;

	// Token: 0x040051C1 RID: 20929
	public AccountCreationFlipbook m_flipbook;

	// Token: 0x040051C2 RID: 20930
	public PegUIElement m_backButton;

	// Token: 0x040051C3 RID: 20931
	public GameObject m_regionSelectTooltipBone;

	// Token: 0x040051C4 RID: 20932
	public GameObject m_topLeftBone;

	// Token: 0x040051C5 RID: 20933
	public GameObject m_bottomRightBone;

	// Token: 0x040051C6 RID: 20934
	public UIBButton m_regionButton;

	// Token: 0x040051C7 RID: 20935
	private object m_selection;

	// Token: 0x040051C8 RID: 20936
	private object m_prevSelection;

	// Token: 0x040051C9 RID: 20937
	private float m_acFlipbookSwap = 30f;

	// Token: 0x040051CA RID: 20938
	private float m_acFlipbookCur;

	// Token: 0x040051CB RID: 20939
	private bool m_canGoBack;

	// Token: 0x040051CC RID: 20940
	private DropdownControl m_regionSelector;

	// Token: 0x040051CD RID: 20941
	private TooltipPanel m_regionSelectTooltip;

	// Token: 0x040051CE RID: 20942
	private static global::Map<constants.BnetRegion, string> s_regionStringNames = new global::Map<constants.BnetRegion, string>
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

	// Token: 0x040051CF RID: 20943
	private PlatformDependentValue<bool> USE_REGION_DROPDOWN = new PlatformDependentValue<bool>(PlatformCategory.Screen)
	{
		PC = true,
		Tablet = true,
		Phone = false
	};

	// Token: 0x040051D0 RID: 20944
	private global::Map<constants.BnetRegion, string> m_regionNames;

	// Token: 0x040051D1 RID: 20945
	private RegionMenu m_regionMenu;

	// Token: 0x040051D2 RID: 20946
	private PlatformDependentValue<bool> KOBOLD_SHOWN_ON_ACCOUNT_CREATION = new PlatformDependentValue<bool>(PlatformCategory.Screen)
	{
		PC = true,
		Tablet = true,
		Phone = false
	};
}
