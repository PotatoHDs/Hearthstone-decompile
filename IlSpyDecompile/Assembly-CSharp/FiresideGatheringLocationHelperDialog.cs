using System;
using System.Collections.Generic;
using UnityEngine;

public class FiresideGatheringLocationHelperDialog : DialogBase
{
	public class Info
	{
		public Action m_callback;

		public string m_gpsOffIntroText;

		public string m_wifiOffIntroText;

		public string m_waitingForWifiText;

		public string m_wifiConfirmText;

		public bool m_isInnkeeperSetup;

		public bool m_isCheckInFailure;
	}

	private enum DialogState
	{
		INVALID,
		GPS_INTRO,
		SEARCHING_FOR_GPS,
		GPS_SUCCESS,
		GPS_FAILURE,
		WIFI_INTRO,
		WAITING_FOR_WIFI,
		NETWORK_CONFIRM,
		INNKEEPER_SUCCESS,
		COLLECTING_ACCESS_POINTS,
		SETTING_UP_TAVERN,
		UNPACK_FAILED,
		SEARCHING_FOR_FSGS,
		SEARCHING_FOR_FSGS_FAILURE
	}

	private class Layout
	{
		public SpellStateType m_successRingState;

		public bool m_twoButtons;

		public GameObject m_activePanel;

		public string m_continueButtonText = "";

		public string m_choiceOneButtonText = "";

		public string m_choiceTwoButtonText = "";

		public Action m_continueButtonAction;

		public Action m_choiceButtonOneAction;

		public Action m_choiceButtonTwoAction;

		public Action m_onInit;
	}

	public UIBButton m_continueButton;

	public UIBButton m_choiceOneButton;

	public UIBButton m_choiceTwoButton;

	public GameObject m_continueButtonContainer;

	public GameObject m_choiceButtonContainer;

	public UIBButton m_innkeeperSuccessAddAccessPointsButton;

	public UIBButton m_innkeeperSuccessFinishButton;

	public UberText m_innkeeperSuccessText;

	public UIBButton m_searchingForFSGFailureLearnMoreButton;

	public UIBButton m_searchingForFSGFailureOkButton;

	public Spell m_successRingSpell;

	public GameObject m_successRingContainer;

	public GameObject m_gpsOffIntroPanel;

	public GameObject m_gpsSearchingPanel;

	public GameObject m_gpsSuccessPanel;

	public GameObject m_gpsFailurePanel;

	public GameObject m_wifiOffIntroPanel;

	public GameObject m_waitingForWifiPanel;

	public GameObject m_networkConfirmPanel;

	public GameObject m_accessPointPanel;

	public GameObject m_unpackingTavernPanel;

	public GameObject m_unpackFailedPanel;

	public GameObject m_searchingForFSGsPanel;

	public GameObject m_innkeeperSuccessPanel;

	public GameObject m_searchingForFSGFailurePanel;

	public UberText m_gpsIntroText;

	public UberText m_wifiOffIntroText;

	public UberText m_waitingForWifiText;

	public UberText m_networkNameText;

	public UberText m_accessPointsText;

	public UberText m_numAccessPointsText;

	public UberText m_wifiConfirmText;

	public UberText m_searchFailureBodyText;

	private Action m_completedCallback;

	private bool m_isInnkeeperSetup;

	private bool m_isCheckInFailure;

	private bool m_provideWifiForTavern = true;

	private HashSet<string> m_innkeeperCollectedBSSIDS = new HashSet<string>();

	private DialogState m_state;

	private Map<DialogState, Layout> m_stateLayouts = new Map<DialogState, Layout>();

	private List<GameObject> m_panels = new List<GameObject>();

	private double m_searchStartTimestamp = double.MaxValue;

	private float m_wifiCheckTimer;

	private float m_wifiCheckCadence = 5f;

	private float m_fsgSearchTimer;

	private float m_fsgSearchTimeMaximum = 20f;

	private void Start()
	{
		PopulatePanels();
		CreateStateMap();
		FiresideGatheringManager firesideGatheringManager = FiresideGatheringManager.Get();
		firesideGatheringManager.OnNearbyFSGsChanged += OnFSGSearchComplete;
		firesideGatheringManager.OnInnkeeperSetupFinished += OnInnkeeperSetupFinished;
		firesideGatheringManager.PlayerAccountShouldAutoCheckin.Set(newValue: true);
		firesideGatheringManager.RequestFSGNotificationAndCheckinsHalt();
		m_continueButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			m_stateLayouts[m_state].m_continueButtonAction();
		});
		m_choiceOneButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			m_stateLayouts[m_state].m_choiceButtonOneAction();
		});
		m_choiceTwoButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			m_stateLayouts[m_state].m_choiceButtonTwoAction();
		});
		m_innkeeperSuccessAddAccessPointsButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			ChangeState(DialogState.COLLECTING_ACCESS_POINTS);
		});
		m_innkeeperSuccessFinishButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			OnInnkeeperSuccessOk();
		});
		m_searchingForFSGFailureLearnMoreButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			OnSearchFailedLearnMore();
		});
		m_searchingForFSGFailureOkButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			OnSearchFailedOk();
		});
		if (m_isCheckInFailure)
		{
			ChangeState(DialogState.SEARCHING_FOR_FSGS_FAILURE);
		}
		else if (ClientLocationManager.Get().GPSAvailable && FiresideGatheringManager.IsGpsFeatureEnabled)
		{
			if (!ClientLocationManager.Get().GPSServicesEnabled)
			{
				ChangeState(DialogState.GPS_INTRO);
			}
			else
			{
				ChangeState(DialogState.SEARCHING_FOR_GPS);
			}
		}
		else if (!string.IsNullOrEmpty(ClientLocationManager.Get().GetWifiSSID))
		{
			ChangeState(DialogState.NETWORK_CONFIRM);
		}
		else
		{
			ChangeState(DialogState.WIFI_INTRO);
		}
	}

	private void Update()
	{
		if (m_state != DialogState.SEARCHING_FOR_FSGS)
		{
			FiresideGatheringManager.Get().RequestFSGNotificationAndCheckinsHalt();
		}
		else
		{
			m_fsgSearchTimer += Time.deltaTime;
			if (m_fsgSearchTimer > m_fsgSearchTimeMaximum)
			{
				OnFSGSearchComplete();
				m_fsgSearchTimer = 0f;
			}
		}
		if (m_state == DialogState.NETWORK_CONFIRM)
		{
			if (m_wifiCheckTimer > m_wifiCheckCadence)
			{
				m_wifiCheckTimer = 0f;
				DoWifiConnectedCheck();
			}
			else
			{
				m_wifiCheckTimer += Time.deltaTime;
			}
		}
	}

	public override void Show()
	{
		base.Show();
		BnetBar.Get().DisableButtonsByDialog(this);
		SoundManager.Get().LoadAndPlay("Expand_Up.prefab:775d97ea42498c044897f396362b9db3");
		DoShowAnimation();
		DialogBase.DoBlur();
	}

	public override void Hide()
	{
		base.Hide();
		SoundManager.Get().LoadAndPlay("Shrink_Down.prefab:a6d5184049ac041418cd5896e7d9a87a");
		FiresideGatheringManager.Get().OnNearbyFSGsChanged -= OnFSGSearchComplete;
		FiresideGatheringManager.Get().OnInnkeeperSetupFinished -= OnInnkeeperSetupFinished;
		DialogBase.EndBlur();
	}

	public void SetInfo(Info info)
	{
		m_completedCallback = info.m_callback;
		m_gpsIntroText.Text = info.m_gpsOffIntroText;
		m_wifiOffIntroText.Text = info.m_wifiOffIntroText;
		m_waitingForWifiText.Text = info.m_waitingForWifiText;
		m_wifiConfirmText.Text = info.m_wifiConfirmText;
		m_isInnkeeperSetup = info.m_isInnkeeperSetup;
		m_isCheckInFailure = info.m_isCheckInFailure;
		string text = "GLUE_FIRESIDE_GATHERING_SEARCH_FAILURE_BODY";
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			text += "_PHONE";
		}
		m_searchFailureBodyText.Text = GameStrings.Get(text);
	}

	private void PopulatePanels()
	{
		m_panels.Add(m_gpsOffIntroPanel);
		m_panels.Add(m_gpsSearchingPanel);
		m_panels.Add(m_gpsSuccessPanel);
		m_panels.Add(m_gpsFailurePanel);
		m_panels.Add(m_wifiOffIntroPanel);
		m_panels.Add(m_waitingForWifiPanel);
		m_panels.Add(m_networkConfirmPanel);
		m_panels.Add(m_accessPointPanel);
		m_panels.Add(m_unpackingTavernPanel);
		m_panels.Add(m_unpackFailedPanel);
		m_panels.Add(m_searchingForFSGsPanel);
		m_panels.Add(m_innkeeperSuccessPanel);
		m_panels.Add(m_searchingForFSGFailurePanel);
	}

	private void CreateStateMap()
	{
		m_stateLayouts[DialogState.GPS_INTRO] = new Layout
		{
			m_activePanel = m_gpsOffIntroPanel,
			m_continueButtonText = GameStrings.Get("GLOBAL_BUTTON_NEXT"),
			m_continueButtonAction = OnGPSIntroContinue
		};
		m_stateLayouts[DialogState.SEARCHING_FOR_GPS] = new Layout
		{
			m_activePanel = m_gpsSearchingPanel,
			m_successRingState = SpellStateType.BIRTH,
			m_continueButtonText = GameStrings.Get("GLOBAL_CANCEL"),
			m_continueButtonAction = OnSearchingForGPSCancel,
			m_onInit = DoGPSRequest
		};
		m_stateLayouts[DialogState.GPS_SUCCESS] = new Layout
		{
			m_activePanel = m_gpsSuccessPanel,
			m_successRingState = SpellStateType.ACTION,
			m_continueButtonText = GameStrings.Get("GLOBAL_BUTTON_NEXT"),
			m_continueButtonAction = OnGPSSuccessNext
		};
		m_stateLayouts[DialogState.GPS_FAILURE] = new Layout
		{
			m_activePanel = m_gpsFailurePanel,
			m_twoButtons = true,
			m_successRingState = SpellStateType.DEATH,
			m_choiceOneButtonText = GameStrings.Get("GLOBAL_RETRY"),
			m_choiceTwoButtonText = GameStrings.Get("GLOBAL_SKIP"),
			m_choiceButtonOneAction = OnGPSFailureRetry,
			m_choiceButtonTwoAction = OnGPSFailureSkip
		};
		m_stateLayouts[DialogState.WIFI_INTRO] = new Layout
		{
			m_activePanel = m_wifiOffIntroPanel,
			m_continueButtonText = GameStrings.Get("GLOBAL_BUTTON_NEXT"),
			m_continueButtonAction = OnWifiIntroNext
		};
		m_stateLayouts[DialogState.WAITING_FOR_WIFI] = new Layout
		{
			m_activePanel = m_waitingForWifiPanel,
			m_twoButtons = true,
			m_choiceOneButtonText = GameStrings.Get("GLOBAL_SKIP"),
			m_choiceTwoButtonText = GameStrings.Get("GLOBAL_REFRESH"),
			m_choiceButtonOneAction = OnWaitingForWifiSkip,
			m_choiceButtonTwoAction = OnWaitingForWifiRefresh,
			m_onInit = DoWifiRequest
		};
		m_stateLayouts[DialogState.NETWORK_CONFIRM] = new Layout
		{
			m_activePanel = m_networkConfirmPanel,
			m_twoButtons = m_isInnkeeperSetup,
			m_continueButtonText = GameStrings.Get("GLUE_FIRESIDE_GATHERING_CONNECT_TO_WIFI_USE_WIFI_BUTTON"),
			m_choiceOneButtonText = GameStrings.Get("GLUE_FIRESIDE_GATHERING_CONNECT_TO_WIFI_NO_WIFI_BUTTON"),
			m_choiceTwoButtonText = GameStrings.Get("GLUE_FIRESIDE_GATHERING_CONNECT_TO_WIFI_USE_WIFI_BUTTON"),
			m_continueButtonAction = OnNetworkConfirmAccept,
			m_choiceButtonOneAction = OnNetworkConfirmCancel,
			m_choiceButtonTwoAction = OnNetworkConfirmAccept,
			m_onInit = DoWifiConnectedCheck
		};
		m_stateLayouts[DialogState.SETTING_UP_TAVERN] = new Layout
		{
			m_activePanel = m_unpackingTavernPanel,
			m_successRingState = SpellStateType.BIRTH,
			m_continueButtonText = GameStrings.Get("GLOBAL_CANCEL"),
			m_continueButtonAction = OnTavernSetupCancel,
			m_onInit = DoTavernSetup
		};
		m_stateLayouts[DialogState.INNKEEPER_SUCCESS] = new Layout
		{
			m_activePanel = m_innkeeperSuccessPanel,
			m_onInit = InnkeeperSuccessSetup
		};
		m_stateLayouts[DialogState.COLLECTING_ACCESS_POINTS] = new Layout
		{
			m_activePanel = m_accessPointPanel,
			m_continueButtonText = GameStrings.Get("GLOBAL_FINISH"),
			m_continueButtonAction = OnAccessPointsDone,
			m_onInit = DoAccessPointSearch
		};
		m_stateLayouts[DialogState.UNPACK_FAILED] = new Layout
		{
			m_activePanel = m_unpackFailedPanel,
			m_successRingState = SpellStateType.DEATH,
			m_continueButtonText = GameStrings.Get("GLOBAL_OK"),
			m_continueButtonAction = OnUnpackFailureOk
		};
		m_stateLayouts[DialogState.SEARCHING_FOR_FSGS] = new Layout
		{
			m_activePanel = m_searchingForFSGsPanel,
			m_successRingState = SpellStateType.BIRTH,
			m_continueButtonText = GameStrings.Get("GLOBAL_CANCEL"),
			m_continueButtonAction = OnSearchingForFSGsCancel,
			m_onInit = BeginSearchForFSGs
		};
		m_stateLayouts[DialogState.SEARCHING_FOR_FSGS_FAILURE] = new Layout
		{
			m_activePanel = m_searchingForFSGFailurePanel,
			m_onInit = OnFSGSearchFailure
		};
	}

	private void ChangeState(DialogState state)
	{
		if (state != m_state && !(this == null) && !(base.gameObject == null))
		{
			m_state = state;
			LoadState();
		}
	}

	private void LoadState()
	{
		Layout layout = m_stateLayouts[m_state];
		m_continueButton.SetText(layout.m_continueButtonText);
		m_choiceOneButton.SetText(layout.m_choiceOneButtonText);
		m_choiceTwoButton.SetText(layout.m_choiceTwoButtonText);
		m_continueButtonContainer.SetActive(!layout.m_twoButtons);
		m_choiceButtonContainer.SetActive(layout.m_twoButtons);
		m_successRingContainer.SetActive(layout.m_successRingState != SpellStateType.NONE);
		if (layout.m_successRingState != 0)
		{
			m_successRingSpell.ActivateState(layout.m_successRingState);
		}
		for (int i = 0; i < m_panels.Count; i++)
		{
			GameObject obj = m_panels[i];
			obj.SetActive(obj == layout.m_activePanel);
		}
		if (layout.m_onInit != null)
		{
			layout.m_onInit();
		}
	}

	private void DoWifiRequest()
	{
		Log.FiresideGatherings.Print("FiresideGatheringLocationHelperDialog.DoWifiRequest");
		if (!ClientLocationManager.Get().WifiEnabled)
		{
			Log.FiresideGatherings.Print("FiresideGatheringLocationHelperDialog.DoWifiRequest Requesting WIFI permission");
			MobilePermissionsManager.Get().RequestPermission(MobilePermission.WIFI, DoWifiRequest_OnPermissionRequestResponse);
		}
		else
		{
			Log.FiresideGatherings.Print("FiresideGatheringLocationHelperDialog.DoWifiRequest Sent wifi data request");
			SendRequestWifiData();
		}
	}

	private void DoWifiRequest_OnPermissionRequestResponse(MobilePermission permission, bool granted)
	{
		if (granted)
		{
			SendRequestWifiData();
		}
	}

	private void SendRequestWifiData()
	{
		ClientLocationManager.Get().RequestWifiData(OnLocationDataWIFIUpdated, delegate
		{
			if (m_state == DialogState.WAITING_FOR_WIFI && !string.IsNullOrEmpty(ClientLocationManager.Get().GetWifiSSID))
			{
				ChangeState(DialogState.NETWORK_CONFIRM);
			}
			else if (m_state == DialogState.COLLECTING_ACCESS_POINTS)
			{
				DoWifiRequest();
			}
		});
	}

	private void DoGPSRequest()
	{
		if (!ClientLocationManager.Get().GPSServicesEnabled)
		{
			MobilePermissionsManager.Get().RequestPermission(MobilePermission.FINE_LOCATION, DoGPSRequest_OnPermissionRequestResponse);
		}
		else
		{
			SendRequestGPSData();
		}
	}

	private void DoGPSRequest_OnPermissionRequestResponse(MobilePermission permission, bool granted)
	{
		if (granted)
		{
			SendRequestGPSData();
		}
		else
		{
			ChangeState(DialogState.GPS_FAILURE);
		}
	}

	private void SendRequestGPSData()
	{
		m_searchStartTimestamp = TimeUtils.GetElapsedTimeSinceEpoch().TotalSeconds;
		ClientLocationManager.Get().RequestGPSData(OnLocationDataGPSUpdated, delegate
		{
			ClientLocationData bestLocationData = ClientLocationManager.Get().GetBestLocationData();
			if (m_state == DialogState.SEARCHING_FOR_GPS)
			{
				if (bestLocationData.location == null || bestLocationData.location.Timestamp < m_searchStartTimestamp || !FiresideGatheringManager.Get().IsGpsLocationValid)
				{
					ChangeState(DialogState.GPS_FAILURE);
				}
				else
				{
					ChangeState(DialogState.GPS_SUCCESS);
				}
			}
		});
	}

	private void OnLocationDataGPSUpdated(ClientLocationData data)
	{
		FiresideGatheringManager.Get().OnLocationDataGPSUpdate(data);
	}

	private void OnLocationDataWIFIUpdated(ClientLocationData data)
	{
		FiresideGatheringManager.Get().OnLocationDataWIFIUpdate(data);
		if (m_state == DialogState.COLLECTING_ACCESS_POINTS)
		{
			DoAccessPointUpdate();
		}
	}

	private void DoWifiConnectedCheck()
	{
		if (ClientLocationManager.Get().WifiEnabled)
		{
			DoWifiRequest();
			m_networkNameText.Text = ClientLocationManager.Get().GetWifiSSID;
		}
		else
		{
			ChangeState(DialogState.WAITING_FOR_WIFI);
		}
	}

	private void DoAccessPointUpdate(ClientLocationData data = null)
	{
		ClientLocationData bestLocationData = ClientLocationManager.Get().GetBestLocationData();
		FiresideGatheringManager.Get().AddWIFIAccessPoints(bestLocationData);
		foreach (AccessPointInfo accessPointSample in bestLocationData.accessPointSamples)
		{
			if (FiresideGatheringManager.IsValidBSSID(accessPointSample.bssid))
			{
				m_innkeeperCollectedBSSIDS.Add(accessPointSample.bssid);
			}
		}
		int count = m_innkeeperCollectedBSSIDS.Count;
		m_numAccessPointsText.Text = count.ToString();
		string key = "GLUE_FIRESIDE_GATHERING_WIFI_ACCESS_POINTS_SEARCH_TITLE";
		if (count == 1)
		{
			key = "GLUE_FIRESIDE_GATHERING_WIFI_ACCESS_POINT_SEARCH_TITLE";
		}
		m_accessPointsText.Text = GameStrings.Get(key);
	}

	private void DoAccessPointSearch()
	{
		if (m_state == DialogState.COLLECTING_ACCESS_POINTS)
		{
			DoAccessPointUpdate();
			ClientLocationManager.Get().RequestWifiData(DoAccessPointUpdate, DoAccessPointSearch);
		}
	}

	private void DoTavernSetup()
	{
		FiresideGatheringManager.Get().InnkeeperSetupFSG(m_provideWifiForTavern);
	}

	private void InnkeeperSuccessSetup()
	{
		if (!m_provideWifiForTavern)
		{
			m_innkeeperSuccessAddAccessPointsButton.Flip(m_provideWifiForTavern);
		}
		m_innkeeperSuccessAddAccessPointsButton.SetEnabled(m_provideWifiForTavern);
		SoundManager.Get().LoadAndPlay("tavern_crowd_play_reaction_very_positive_3.prefab:30519a2212fbd18499c08fb02ba05c81");
		string text = "GLUE_FIRESIDE_GATHERING_WIFI_SUCCESS";
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			text += "_PHONE";
		}
		string key = (m_provideWifiForTavern ? text : "GLUE_FIRESIDE_GATHERING_NO_WIFI_SUCCESS");
		m_innkeeperSuccessText.Text = GameStrings.Get(key);
	}

	private void BeginSearchForFSGs()
	{
		FiresideGatheringManager.Get().ClearErrorOccuredOnCheckIn();
		FiresideGatheringManager.Get().RequestNearbyFSGs();
	}

	private void OnSearchFailedLearnMore()
	{
		Done();
		FiresideGatheringManager.Get().GotoFSGLink();
	}

	private void OnSearchFailedOk()
	{
		Done();
	}

	private void OnFSGSearchComplete()
	{
		if (m_state == DialogState.SEARCHING_FOR_FSGS)
		{
			if (FiresideGatheringManager.Get().GetFSGs().Count < 1)
			{
				ChangeState(DialogState.SEARCHING_FOR_FSGS_FAILURE);
				return;
			}
			Done();
			FiresideGatheringManager.Get().SetWaitingForCheckIn();
		}
	}

	private void OnInnkeeperSetupFinished(bool success)
	{
		if (!success)
		{
			ChangeState(DialogState.UNPACK_FAILED);
		}
		else
		{
			ChangeState(DialogState.INNKEEPER_SUCCESS);
		}
	}

	private void OnFSGSearchFailure()
	{
		m_continueButtonContainer.SetActive(value: false);
		m_choiceButtonContainer.SetActive(value: false);
	}

	private void Done()
	{
		Hide();
		if (m_completedCallback != null)
		{
			m_completedCallback();
		}
	}

	private void OnGPSIntroContinue()
	{
		ChangeState(DialogState.SEARCHING_FOR_GPS);
	}

	private void OnSearchingForGPSCancel()
	{
		ChangeState(DialogState.GPS_FAILURE);
	}

	private void OnGPSSuccessNext()
	{
		if (!FiresideGatheringManager.IsWifiFeatureEnabled)
		{
			if (m_isInnkeeperSetup)
			{
				ChangeState(DialogState.SETTING_UP_TAVERN);
			}
			else
			{
				ChangeState(DialogState.SEARCHING_FOR_FSGS);
			}
		}
		else if (!string.IsNullOrEmpty(ClientLocationManager.Get().GetWifiSSID))
		{
			ChangeState(DialogState.NETWORK_CONFIRM);
		}
		else if (ClientLocationManager.Get().WifiEnabled)
		{
			ChangeState(DialogState.WAITING_FOR_WIFI);
		}
		else
		{
			ChangeState(DialogState.WIFI_INTRO);
		}
	}

	private void OnGPSFailureRetry()
	{
		ChangeState(DialogState.SEARCHING_FOR_GPS);
	}

	private void OnGPSFailureSkip()
	{
		if (!FiresideGatheringManager.IsWifiFeatureEnabled || MobilePermissionsManager.Get().WifiRequiresLocationPermission())
		{
			if (m_isInnkeeperSetup)
			{
				m_provideWifiForTavern = false;
				ChangeState(DialogState.SETTING_UP_TAVERN);
			}
			else
			{
				ChangeState(DialogState.SEARCHING_FOR_FSGS);
			}
		}
		else if (!string.IsNullOrEmpty(ClientLocationManager.Get().GetWifiSSID))
		{
			ChangeState(DialogState.NETWORK_CONFIRM);
		}
		else if (ClientLocationManager.Get().WifiEnabled)
		{
			ChangeState(DialogState.WAITING_FOR_WIFI);
		}
		else
		{
			ChangeState(DialogState.WIFI_INTRO);
		}
	}

	private void OnWifiIntroNext()
	{
		if (!string.IsNullOrEmpty(ClientLocationManager.Get().GetWifiSSID))
		{
			ChangeState(DialogState.NETWORK_CONFIRM);
		}
		else
		{
			ChangeState(DialogState.WAITING_FOR_WIFI);
		}
	}

	private void OnWaitingForWifiSkip()
	{
		if (m_isInnkeeperSetup)
		{
			m_provideWifiForTavern = false;
			ChangeState(DialogState.SETTING_UP_TAVERN);
		}
		else
		{
			ChangeState(DialogState.SEARCHING_FOR_FSGS);
		}
	}

	private void OnWaitingForWifiRefresh()
	{
		DoWifiRequest();
	}

	private void OnNetworkConfirmCancel()
	{
		if (m_isInnkeeperSetup)
		{
			m_provideWifiForTavern = false;
			ChangeState(DialogState.SETTING_UP_TAVERN);
		}
		else
		{
			ChangeState(DialogState.SEARCHING_FOR_FSGS);
		}
	}

	private void OnNetworkConfirmAccept()
	{
		if (m_isInnkeeperSetup)
		{
			ChangeState(DialogState.SETTING_UP_TAVERN);
		}
		else
		{
			ChangeState(DialogState.SEARCHING_FOR_FSGS);
		}
	}

	private void OnAccessPointsDone()
	{
		ChangeState(DialogState.SETTING_UP_TAVERN);
	}

	private void OnTavernSetupCancel()
	{
		Done();
	}

	private void OnSearchingForFSGsCancel()
	{
		Done();
	}

	private void OnUnpackFailureOk()
	{
		Done();
	}

	private void OnInnkeeperSuccessOk()
	{
		Done();
		FiresideGatheringManager.Get().SetWaitingForCheckIn();
	}
}
