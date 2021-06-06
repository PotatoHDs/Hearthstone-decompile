using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002E9 RID: 745
public class FiresideGatheringLocationHelperDialog : DialogBase
{
	// Token: 0x060026E0 RID: 9952 RVA: 0x000C2958 File Offset: 0x000C0B58
	private void Start()
	{
		this.PopulatePanels();
		this.CreateStateMap();
		FiresideGatheringManager firesideGatheringManager = FiresideGatheringManager.Get();
		firesideGatheringManager.OnNearbyFSGsChanged += this.OnFSGSearchComplete;
		firesideGatheringManager.OnInnkeeperSetupFinished += this.OnInnkeeperSetupFinished;
		firesideGatheringManager.PlayerAccountShouldAutoCheckin.Set(true);
		firesideGatheringManager.RequestFSGNotificationAndCheckinsHalt();
		this.m_continueButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.m_stateLayouts[this.m_state].m_continueButtonAction();
		});
		this.m_choiceOneButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.m_stateLayouts[this.m_state].m_choiceButtonOneAction();
		});
		this.m_choiceTwoButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.m_stateLayouts[this.m_state].m_choiceButtonTwoAction();
		});
		this.m_innkeeperSuccessAddAccessPointsButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.COLLECTING_ACCESS_POINTS);
		});
		this.m_innkeeperSuccessFinishButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.OnInnkeeperSuccessOk();
		});
		this.m_searchingForFSGFailureLearnMoreButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.OnSearchFailedLearnMore();
		});
		this.m_searchingForFSGFailureOkButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.OnSearchFailedOk();
		});
		if (this.m_isCheckInFailure)
		{
			this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.SEARCHING_FOR_FSGS_FAILURE);
			return;
		}
		if (ClientLocationManager.Get().GPSAvailable && FiresideGatheringManager.IsGpsFeatureEnabled)
		{
			if (!ClientLocationManager.Get().GPSServicesEnabled)
			{
				this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.GPS_INTRO);
				return;
			}
			this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.SEARCHING_FOR_GPS);
			return;
		}
		else
		{
			if (!string.IsNullOrEmpty(ClientLocationManager.Get().GetWifiSSID))
			{
				this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.NETWORK_CONFIRM);
				return;
			}
			this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.WIFI_INTRO);
			return;
		}
	}

	// Token: 0x060026E1 RID: 9953 RVA: 0x000C2ABC File Offset: 0x000C0CBC
	private void Update()
	{
		if (this.m_state != FiresideGatheringLocationHelperDialog.DialogState.SEARCHING_FOR_FSGS)
		{
			FiresideGatheringManager.Get().RequestFSGNotificationAndCheckinsHalt();
		}
		else
		{
			this.m_fsgSearchTimer += Time.deltaTime;
			if (this.m_fsgSearchTimer > this.m_fsgSearchTimeMaximum)
			{
				this.OnFSGSearchComplete();
				this.m_fsgSearchTimer = 0f;
			}
		}
		if (this.m_state == FiresideGatheringLocationHelperDialog.DialogState.NETWORK_CONFIRM)
		{
			if (this.m_wifiCheckTimer > this.m_wifiCheckCadence)
			{
				this.m_wifiCheckTimer = 0f;
				this.DoWifiConnectedCheck();
				return;
			}
			this.m_wifiCheckTimer += Time.deltaTime;
		}
	}

	// Token: 0x060026E2 RID: 9954 RVA: 0x000C2782 File Offset: 0x000C0982
	public override void Show()
	{
		base.Show();
		BnetBar.Get().DisableButtonsByDialog(this);
		SoundManager.Get().LoadAndPlay("Expand_Up.prefab:775d97ea42498c044897f396362b9db3");
		this.DoShowAnimation();
		DialogBase.DoBlur();
	}

	// Token: 0x060026E3 RID: 9955 RVA: 0x000C2B4C File Offset: 0x000C0D4C
	public override void Hide()
	{
		base.Hide();
		SoundManager.Get().LoadAndPlay("Shrink_Down.prefab:a6d5184049ac041418cd5896e7d9a87a");
		FiresideGatheringManager.Get().OnNearbyFSGsChanged -= this.OnFSGSearchComplete;
		FiresideGatheringManager.Get().OnInnkeeperSetupFinished -= this.OnInnkeeperSetupFinished;
		DialogBase.EndBlur();
	}

	// Token: 0x060026E4 RID: 9956 RVA: 0x000C2BA4 File Offset: 0x000C0DA4
	public void SetInfo(FiresideGatheringLocationHelperDialog.Info info)
	{
		this.m_completedCallback = info.m_callback;
		this.m_gpsIntroText.Text = info.m_gpsOffIntroText;
		this.m_wifiOffIntroText.Text = info.m_wifiOffIntroText;
		this.m_waitingForWifiText.Text = info.m_waitingForWifiText;
		this.m_wifiConfirmText.Text = info.m_wifiConfirmText;
		this.m_isInnkeeperSetup = info.m_isInnkeeperSetup;
		this.m_isCheckInFailure = info.m_isCheckInFailure;
		string text = "GLUE_FIRESIDE_GATHERING_SEARCH_FAILURE_BODY";
		if (UniversalInputManager.UsePhoneUI)
		{
			text += "_PHONE";
		}
		this.m_searchFailureBodyText.Text = GameStrings.Get(text);
	}

	// Token: 0x060026E5 RID: 9957 RVA: 0x000C2C48 File Offset: 0x000C0E48
	private void PopulatePanels()
	{
		this.m_panels.Add(this.m_gpsOffIntroPanel);
		this.m_panels.Add(this.m_gpsSearchingPanel);
		this.m_panels.Add(this.m_gpsSuccessPanel);
		this.m_panels.Add(this.m_gpsFailurePanel);
		this.m_panels.Add(this.m_wifiOffIntroPanel);
		this.m_panels.Add(this.m_waitingForWifiPanel);
		this.m_panels.Add(this.m_networkConfirmPanel);
		this.m_panels.Add(this.m_accessPointPanel);
		this.m_panels.Add(this.m_unpackingTavernPanel);
		this.m_panels.Add(this.m_unpackFailedPanel);
		this.m_panels.Add(this.m_searchingForFSGsPanel);
		this.m_panels.Add(this.m_innkeeperSuccessPanel);
		this.m_panels.Add(this.m_searchingForFSGFailurePanel);
	}

	// Token: 0x060026E6 RID: 9958 RVA: 0x000C2D34 File Offset: 0x000C0F34
	private void CreateStateMap()
	{
		this.m_stateLayouts[FiresideGatheringLocationHelperDialog.DialogState.GPS_INTRO] = new FiresideGatheringLocationHelperDialog.Layout
		{
			m_activePanel = this.m_gpsOffIntroPanel,
			m_continueButtonText = GameStrings.Get("GLOBAL_BUTTON_NEXT"),
			m_continueButtonAction = new Action(this.OnGPSIntroContinue)
		};
		this.m_stateLayouts[FiresideGatheringLocationHelperDialog.DialogState.SEARCHING_FOR_GPS] = new FiresideGatheringLocationHelperDialog.Layout
		{
			m_activePanel = this.m_gpsSearchingPanel,
			m_successRingState = SpellStateType.BIRTH,
			m_continueButtonText = GameStrings.Get("GLOBAL_CANCEL"),
			m_continueButtonAction = new Action(this.OnSearchingForGPSCancel),
			m_onInit = new Action(this.DoGPSRequest)
		};
		this.m_stateLayouts[FiresideGatheringLocationHelperDialog.DialogState.GPS_SUCCESS] = new FiresideGatheringLocationHelperDialog.Layout
		{
			m_activePanel = this.m_gpsSuccessPanel,
			m_successRingState = SpellStateType.ACTION,
			m_continueButtonText = GameStrings.Get("GLOBAL_BUTTON_NEXT"),
			m_continueButtonAction = new Action(this.OnGPSSuccessNext)
		};
		this.m_stateLayouts[FiresideGatheringLocationHelperDialog.DialogState.GPS_FAILURE] = new FiresideGatheringLocationHelperDialog.Layout
		{
			m_activePanel = this.m_gpsFailurePanel,
			m_twoButtons = true,
			m_successRingState = SpellStateType.DEATH,
			m_choiceOneButtonText = GameStrings.Get("GLOBAL_RETRY"),
			m_choiceTwoButtonText = GameStrings.Get("GLOBAL_SKIP"),
			m_choiceButtonOneAction = new Action(this.OnGPSFailureRetry),
			m_choiceButtonTwoAction = new Action(this.OnGPSFailureSkip)
		};
		this.m_stateLayouts[FiresideGatheringLocationHelperDialog.DialogState.WIFI_INTRO] = new FiresideGatheringLocationHelperDialog.Layout
		{
			m_activePanel = this.m_wifiOffIntroPanel,
			m_continueButtonText = GameStrings.Get("GLOBAL_BUTTON_NEXT"),
			m_continueButtonAction = new Action(this.OnWifiIntroNext)
		};
		this.m_stateLayouts[FiresideGatheringLocationHelperDialog.DialogState.WAITING_FOR_WIFI] = new FiresideGatheringLocationHelperDialog.Layout
		{
			m_activePanel = this.m_waitingForWifiPanel,
			m_twoButtons = true,
			m_choiceOneButtonText = GameStrings.Get("GLOBAL_SKIP"),
			m_choiceTwoButtonText = GameStrings.Get("GLOBAL_REFRESH"),
			m_choiceButtonOneAction = new Action(this.OnWaitingForWifiSkip),
			m_choiceButtonTwoAction = new Action(this.OnWaitingForWifiRefresh),
			m_onInit = new Action(this.DoWifiRequest)
		};
		this.m_stateLayouts[FiresideGatheringLocationHelperDialog.DialogState.NETWORK_CONFIRM] = new FiresideGatheringLocationHelperDialog.Layout
		{
			m_activePanel = this.m_networkConfirmPanel,
			m_twoButtons = this.m_isInnkeeperSetup,
			m_continueButtonText = GameStrings.Get("GLUE_FIRESIDE_GATHERING_CONNECT_TO_WIFI_USE_WIFI_BUTTON"),
			m_choiceOneButtonText = GameStrings.Get("GLUE_FIRESIDE_GATHERING_CONNECT_TO_WIFI_NO_WIFI_BUTTON"),
			m_choiceTwoButtonText = GameStrings.Get("GLUE_FIRESIDE_GATHERING_CONNECT_TO_WIFI_USE_WIFI_BUTTON"),
			m_continueButtonAction = new Action(this.OnNetworkConfirmAccept),
			m_choiceButtonOneAction = new Action(this.OnNetworkConfirmCancel),
			m_choiceButtonTwoAction = new Action(this.OnNetworkConfirmAccept),
			m_onInit = new Action(this.DoWifiConnectedCheck)
		};
		this.m_stateLayouts[FiresideGatheringLocationHelperDialog.DialogState.SETTING_UP_TAVERN] = new FiresideGatheringLocationHelperDialog.Layout
		{
			m_activePanel = this.m_unpackingTavernPanel,
			m_successRingState = SpellStateType.BIRTH,
			m_continueButtonText = GameStrings.Get("GLOBAL_CANCEL"),
			m_continueButtonAction = new Action(this.OnTavernSetupCancel),
			m_onInit = new Action(this.DoTavernSetup)
		};
		this.m_stateLayouts[FiresideGatheringLocationHelperDialog.DialogState.INNKEEPER_SUCCESS] = new FiresideGatheringLocationHelperDialog.Layout
		{
			m_activePanel = this.m_innkeeperSuccessPanel,
			m_onInit = new Action(this.InnkeeperSuccessSetup)
		};
		this.m_stateLayouts[FiresideGatheringLocationHelperDialog.DialogState.COLLECTING_ACCESS_POINTS] = new FiresideGatheringLocationHelperDialog.Layout
		{
			m_activePanel = this.m_accessPointPanel,
			m_continueButtonText = GameStrings.Get("GLOBAL_FINISH"),
			m_continueButtonAction = new Action(this.OnAccessPointsDone),
			m_onInit = new Action(this.DoAccessPointSearch)
		};
		this.m_stateLayouts[FiresideGatheringLocationHelperDialog.DialogState.UNPACK_FAILED] = new FiresideGatheringLocationHelperDialog.Layout
		{
			m_activePanel = this.m_unpackFailedPanel,
			m_successRingState = SpellStateType.DEATH,
			m_continueButtonText = GameStrings.Get("GLOBAL_OK"),
			m_continueButtonAction = new Action(this.OnUnpackFailureOk)
		};
		this.m_stateLayouts[FiresideGatheringLocationHelperDialog.DialogState.SEARCHING_FOR_FSGS] = new FiresideGatheringLocationHelperDialog.Layout
		{
			m_activePanel = this.m_searchingForFSGsPanel,
			m_successRingState = SpellStateType.BIRTH,
			m_continueButtonText = GameStrings.Get("GLOBAL_CANCEL"),
			m_continueButtonAction = new Action(this.OnSearchingForFSGsCancel),
			m_onInit = new Action(this.BeginSearchForFSGs)
		};
		this.m_stateLayouts[FiresideGatheringLocationHelperDialog.DialogState.SEARCHING_FOR_FSGS_FAILURE] = new FiresideGatheringLocationHelperDialog.Layout
		{
			m_activePanel = this.m_searchingForFSGFailurePanel,
			m_onInit = new Action(this.OnFSGSearchFailure)
		};
	}

	// Token: 0x060026E7 RID: 9959 RVA: 0x000C3191 File Offset: 0x000C1391
	private void ChangeState(FiresideGatheringLocationHelperDialog.DialogState state)
	{
		if (state == this.m_state || this == null || base.gameObject == null)
		{
			return;
		}
		this.m_state = state;
		this.LoadState();
	}

	// Token: 0x060026E8 RID: 9960 RVA: 0x000C31C4 File Offset: 0x000C13C4
	private void LoadState()
	{
		FiresideGatheringLocationHelperDialog.Layout layout = this.m_stateLayouts[this.m_state];
		this.m_continueButton.SetText(layout.m_continueButtonText);
		this.m_choiceOneButton.SetText(layout.m_choiceOneButtonText);
		this.m_choiceTwoButton.SetText(layout.m_choiceTwoButtonText);
		this.m_continueButtonContainer.SetActive(!layout.m_twoButtons);
		this.m_choiceButtonContainer.SetActive(layout.m_twoButtons);
		this.m_successRingContainer.SetActive(layout.m_successRingState > SpellStateType.NONE);
		if (layout.m_successRingState != SpellStateType.NONE)
		{
			this.m_successRingSpell.ActivateState(layout.m_successRingState);
		}
		for (int i = 0; i < this.m_panels.Count; i++)
		{
			GameObject gameObject = this.m_panels[i];
			gameObject.SetActive(gameObject == layout.m_activePanel);
		}
		if (layout.m_onInit != null)
		{
			layout.m_onInit();
		}
	}

	// Token: 0x060026E9 RID: 9961 RVA: 0x000C32B0 File Offset: 0x000C14B0
	private void DoWifiRequest()
	{
		Log.FiresideGatherings.Print("FiresideGatheringLocationHelperDialog.DoWifiRequest", Array.Empty<object>());
		if (!ClientLocationManager.Get().WifiEnabled)
		{
			Log.FiresideGatherings.Print("FiresideGatheringLocationHelperDialog.DoWifiRequest Requesting WIFI permission", Array.Empty<object>());
			MobilePermissionsManager.Get().RequestPermission(MobilePermission.WIFI, new MobilePermissionsManager.PermissionResultCallback(this.DoWifiRequest_OnPermissionRequestResponse));
			return;
		}
		Log.FiresideGatherings.Print("FiresideGatheringLocationHelperDialog.DoWifiRequest Sent wifi data request", Array.Empty<object>());
		this.SendRequestWifiData();
	}

	// Token: 0x060026EA RID: 9962 RVA: 0x000C3323 File Offset: 0x000C1523
	private void DoWifiRequest_OnPermissionRequestResponse(MobilePermission permission, bool granted)
	{
		if (granted)
		{
			this.SendRequestWifiData();
		}
	}

	// Token: 0x060026EB RID: 9963 RVA: 0x000C332E File Offset: 0x000C152E
	private void SendRequestWifiData()
	{
		ClientLocationManager.Get().RequestWifiData(new Action<ClientLocationData>(this.OnLocationDataWIFIUpdated), delegate
		{
			if (this.m_state == FiresideGatheringLocationHelperDialog.DialogState.WAITING_FOR_WIFI && !string.IsNullOrEmpty(ClientLocationManager.Get().GetWifiSSID))
			{
				this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.NETWORK_CONFIRM);
				return;
			}
			if (this.m_state == FiresideGatheringLocationHelperDialog.DialogState.COLLECTING_ACCESS_POINTS)
			{
				this.DoWifiRequest();
			}
		});
	}

	// Token: 0x060026EC RID: 9964 RVA: 0x000C3352 File Offset: 0x000C1552
	private void DoGPSRequest()
	{
		if (!ClientLocationManager.Get().GPSServicesEnabled)
		{
			MobilePermissionsManager.Get().RequestPermission(MobilePermission.FINE_LOCATION, new MobilePermissionsManager.PermissionResultCallback(this.DoGPSRequest_OnPermissionRequestResponse));
			return;
		}
		this.SendRequestGPSData();
	}

	// Token: 0x060026ED RID: 9965 RVA: 0x000C337E File Offset: 0x000C157E
	private void DoGPSRequest_OnPermissionRequestResponse(MobilePermission permission, bool granted)
	{
		if (granted)
		{
			this.SendRequestGPSData();
			return;
		}
		this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.GPS_FAILURE);
	}

	// Token: 0x060026EE RID: 9966 RVA: 0x000C3394 File Offset: 0x000C1594
	private void SendRequestGPSData()
	{
		this.m_searchStartTimestamp = TimeUtils.GetElapsedTimeSinceEpoch(null).TotalSeconds;
		ClientLocationManager.Get().RequestGPSData(new Action<ClientLocationData>(this.OnLocationDataGPSUpdated), delegate
		{
			ClientLocationData bestLocationData = ClientLocationManager.Get().GetBestLocationData();
			if (this.m_state == FiresideGatheringLocationHelperDialog.DialogState.SEARCHING_FOR_GPS)
			{
				if (bestLocationData.location == null || bestLocationData.location.Timestamp < this.m_searchStartTimestamp || !FiresideGatheringManager.Get().IsGpsLocationValid)
				{
					this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.GPS_FAILURE);
					return;
				}
				this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.GPS_SUCCESS);
			}
		});
	}

	// Token: 0x060026EF RID: 9967 RVA: 0x000C33DF File Offset: 0x000C15DF
	private void OnLocationDataGPSUpdated(ClientLocationData data)
	{
		FiresideGatheringManager.Get().OnLocationDataGPSUpdate(data);
	}

	// Token: 0x060026F0 RID: 9968 RVA: 0x000C33EC File Offset: 0x000C15EC
	private void OnLocationDataWIFIUpdated(ClientLocationData data)
	{
		FiresideGatheringManager.Get().OnLocationDataWIFIUpdate(data);
		if (this.m_state == FiresideGatheringLocationHelperDialog.DialogState.COLLECTING_ACCESS_POINTS)
		{
			this.DoAccessPointUpdate(null);
		}
	}

	// Token: 0x060026F1 RID: 9969 RVA: 0x000C340A File Offset: 0x000C160A
	private void DoWifiConnectedCheck()
	{
		if (ClientLocationManager.Get().WifiEnabled)
		{
			this.DoWifiRequest();
			this.m_networkNameText.Text = ClientLocationManager.Get().GetWifiSSID;
			return;
		}
		this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.WAITING_FOR_WIFI);
	}

	// Token: 0x060026F2 RID: 9970 RVA: 0x000C343C File Offset: 0x000C163C
	private void DoAccessPointUpdate(ClientLocationData data = null)
	{
		ClientLocationData bestLocationData = ClientLocationManager.Get().GetBestLocationData();
		FiresideGatheringManager.Get().AddWIFIAccessPoints(bestLocationData);
		foreach (AccessPointInfo accessPointInfo in bestLocationData.accessPointSamples)
		{
			if (FiresideGatheringManager.IsValidBSSID(accessPointInfo.bssid))
			{
				this.m_innkeeperCollectedBSSIDS.Add(accessPointInfo.bssid);
			}
		}
		int count = this.m_innkeeperCollectedBSSIDS.Count;
		this.m_numAccessPointsText.Text = count.ToString();
		string key = "GLUE_FIRESIDE_GATHERING_WIFI_ACCESS_POINTS_SEARCH_TITLE";
		if (count == 1)
		{
			key = "GLUE_FIRESIDE_GATHERING_WIFI_ACCESS_POINT_SEARCH_TITLE";
		}
		this.m_accessPointsText.Text = GameStrings.Get(key);
	}

	// Token: 0x060026F3 RID: 9971 RVA: 0x000C3500 File Offset: 0x000C1700
	private void DoAccessPointSearch()
	{
		if (this.m_state != FiresideGatheringLocationHelperDialog.DialogState.COLLECTING_ACCESS_POINTS)
		{
			return;
		}
		this.DoAccessPointUpdate(null);
		ClientLocationManager.Get().RequestWifiData(new Action<ClientLocationData>(this.DoAccessPointUpdate), new Action(this.DoAccessPointSearch));
	}

	// Token: 0x060026F4 RID: 9972 RVA: 0x000C3536 File Offset: 0x000C1736
	private void DoTavernSetup()
	{
		FiresideGatheringManager.Get().InnkeeperSetupFSG(this.m_provideWifiForTavern);
	}

	// Token: 0x060026F5 RID: 9973 RVA: 0x000C3548 File Offset: 0x000C1748
	private void InnkeeperSuccessSetup()
	{
		if (!this.m_provideWifiForTavern)
		{
			this.m_innkeeperSuccessAddAccessPointsButton.Flip(this.m_provideWifiForTavern, false);
		}
		this.m_innkeeperSuccessAddAccessPointsButton.SetEnabled(this.m_provideWifiForTavern, false);
		SoundManager.Get().LoadAndPlay("tavern_crowd_play_reaction_very_positive_3.prefab:30519a2212fbd18499c08fb02ba05c81");
		string text = "GLUE_FIRESIDE_GATHERING_WIFI_SUCCESS";
		if (UniversalInputManager.UsePhoneUI)
		{
			text += "_PHONE";
		}
		string key = this.m_provideWifiForTavern ? text : "GLUE_FIRESIDE_GATHERING_NO_WIFI_SUCCESS";
		this.m_innkeeperSuccessText.Text = GameStrings.Get(key);
	}

	// Token: 0x060026F6 RID: 9974 RVA: 0x000C35D5 File Offset: 0x000C17D5
	private void BeginSearchForFSGs()
	{
		FiresideGatheringManager.Get().ClearErrorOccuredOnCheckIn();
		FiresideGatheringManager.Get().RequestNearbyFSGs(false);
	}

	// Token: 0x060026F7 RID: 9975 RVA: 0x000C35EC File Offset: 0x000C17EC
	private void OnSearchFailedLearnMore()
	{
		this.Done();
		FiresideGatheringManager.Get().GotoFSGLink();
	}

	// Token: 0x060026F8 RID: 9976 RVA: 0x000C35FE File Offset: 0x000C17FE
	private void OnSearchFailedOk()
	{
		this.Done();
	}

	// Token: 0x060026F9 RID: 9977 RVA: 0x000C3606 File Offset: 0x000C1806
	private void OnFSGSearchComplete()
	{
		if (this.m_state != FiresideGatheringLocationHelperDialog.DialogState.SEARCHING_FOR_FSGS)
		{
			return;
		}
		if (FiresideGatheringManager.Get().GetFSGs().Count < 1)
		{
			this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.SEARCHING_FOR_FSGS_FAILURE);
			return;
		}
		this.Done();
		FiresideGatheringManager.Get().SetWaitingForCheckIn();
	}

	// Token: 0x060026FA RID: 9978 RVA: 0x000C363E File Offset: 0x000C183E
	private void OnInnkeeperSetupFinished(bool success)
	{
		if (!success)
		{
			this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.UNPACK_FAILED);
			return;
		}
		this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.INNKEEPER_SUCCESS);
	}

	// Token: 0x060026FB RID: 9979 RVA: 0x000C3653 File Offset: 0x000C1853
	private void OnFSGSearchFailure()
	{
		this.m_continueButtonContainer.SetActive(false);
		this.m_choiceButtonContainer.SetActive(false);
	}

	// Token: 0x060026FC RID: 9980 RVA: 0x000C366D File Offset: 0x000C186D
	private void Done()
	{
		this.Hide();
		if (this.m_completedCallback != null)
		{
			this.m_completedCallback();
		}
	}

	// Token: 0x060026FD RID: 9981 RVA: 0x000C3688 File Offset: 0x000C1888
	private void OnGPSIntroContinue()
	{
		this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.SEARCHING_FOR_GPS);
	}

	// Token: 0x060026FE RID: 9982 RVA: 0x000C3691 File Offset: 0x000C1891
	private void OnSearchingForGPSCancel()
	{
		this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.GPS_FAILURE);
	}

	// Token: 0x060026FF RID: 9983 RVA: 0x000C369C File Offset: 0x000C189C
	private void OnGPSSuccessNext()
	{
		if (!FiresideGatheringManager.IsWifiFeatureEnabled)
		{
			if (this.m_isInnkeeperSetup)
			{
				this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.SETTING_UP_TAVERN);
				return;
			}
			this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.SEARCHING_FOR_FSGS);
			return;
		}
		else
		{
			if (!string.IsNullOrEmpty(ClientLocationManager.Get().GetWifiSSID))
			{
				this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.NETWORK_CONFIRM);
				return;
			}
			if (ClientLocationManager.Get().WifiEnabled)
			{
				this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.WAITING_FOR_WIFI);
				return;
			}
			this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.WIFI_INTRO);
			return;
		}
	}

	// Token: 0x06002700 RID: 9984 RVA: 0x000C3688 File Offset: 0x000C1888
	private void OnGPSFailureRetry()
	{
		this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.SEARCHING_FOR_GPS);
	}

	// Token: 0x06002701 RID: 9985 RVA: 0x000C3700 File Offset: 0x000C1900
	private void OnGPSFailureSkip()
	{
		if (!FiresideGatheringManager.IsWifiFeatureEnabled || MobilePermissionsManager.Get().WifiRequiresLocationPermission())
		{
			if (this.m_isInnkeeperSetup)
			{
				this.m_provideWifiForTavern = false;
				this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.SETTING_UP_TAVERN);
				return;
			}
			this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.SEARCHING_FOR_FSGS);
			return;
		}
		else
		{
			if (!string.IsNullOrEmpty(ClientLocationManager.Get().GetWifiSSID))
			{
				this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.NETWORK_CONFIRM);
				return;
			}
			if (ClientLocationManager.Get().WifiEnabled)
			{
				this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.WAITING_FOR_WIFI);
				return;
			}
			this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.WIFI_INTRO);
			return;
		}
	}

	// Token: 0x06002702 RID: 9986 RVA: 0x000C3775 File Offset: 0x000C1975
	private void OnWifiIntroNext()
	{
		if (!string.IsNullOrEmpty(ClientLocationManager.Get().GetWifiSSID))
		{
			this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.NETWORK_CONFIRM);
			return;
		}
		this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.WAITING_FOR_WIFI);
	}

	// Token: 0x06002703 RID: 9987 RVA: 0x000C3797 File Offset: 0x000C1997
	private void OnWaitingForWifiSkip()
	{
		if (this.m_isInnkeeperSetup)
		{
			this.m_provideWifiForTavern = false;
			this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.SETTING_UP_TAVERN);
			return;
		}
		this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.SEARCHING_FOR_FSGS);
	}

	// Token: 0x06002704 RID: 9988 RVA: 0x000C37B9 File Offset: 0x000C19B9
	private void OnWaitingForWifiRefresh()
	{
		this.DoWifiRequest();
	}

	// Token: 0x06002705 RID: 9989 RVA: 0x000C3797 File Offset: 0x000C1997
	private void OnNetworkConfirmCancel()
	{
		if (this.m_isInnkeeperSetup)
		{
			this.m_provideWifiForTavern = false;
			this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.SETTING_UP_TAVERN);
			return;
		}
		this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.SEARCHING_FOR_FSGS);
	}

	// Token: 0x06002706 RID: 9990 RVA: 0x000C37C1 File Offset: 0x000C19C1
	private void OnNetworkConfirmAccept()
	{
		if (this.m_isInnkeeperSetup)
		{
			this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.SETTING_UP_TAVERN);
			return;
		}
		this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.SEARCHING_FOR_FSGS);
	}

	// Token: 0x06002707 RID: 9991 RVA: 0x000C37DC File Offset: 0x000C19DC
	private void OnAccessPointsDone()
	{
		this.ChangeState(FiresideGatheringLocationHelperDialog.DialogState.SETTING_UP_TAVERN);
	}

	// Token: 0x06002708 RID: 9992 RVA: 0x000C35FE File Offset: 0x000C17FE
	private void OnTavernSetupCancel()
	{
		this.Done();
	}

	// Token: 0x06002709 RID: 9993 RVA: 0x000C35FE File Offset: 0x000C17FE
	private void OnSearchingForFSGsCancel()
	{
		this.Done();
	}

	// Token: 0x0600270A RID: 9994 RVA: 0x000C35FE File Offset: 0x000C17FE
	private void OnUnpackFailureOk()
	{
		this.Done();
	}

	// Token: 0x0600270B RID: 9995 RVA: 0x000C37E6 File Offset: 0x000C19E6
	private void OnInnkeeperSuccessOk()
	{
		this.Done();
		FiresideGatheringManager.Get().SetWaitingForCheckIn();
	}

	// Token: 0x04001618 RID: 5656
	public UIBButton m_continueButton;

	// Token: 0x04001619 RID: 5657
	public UIBButton m_choiceOneButton;

	// Token: 0x0400161A RID: 5658
	public UIBButton m_choiceTwoButton;

	// Token: 0x0400161B RID: 5659
	public GameObject m_continueButtonContainer;

	// Token: 0x0400161C RID: 5660
	public GameObject m_choiceButtonContainer;

	// Token: 0x0400161D RID: 5661
	public UIBButton m_innkeeperSuccessAddAccessPointsButton;

	// Token: 0x0400161E RID: 5662
	public UIBButton m_innkeeperSuccessFinishButton;

	// Token: 0x0400161F RID: 5663
	public UberText m_innkeeperSuccessText;

	// Token: 0x04001620 RID: 5664
	public UIBButton m_searchingForFSGFailureLearnMoreButton;

	// Token: 0x04001621 RID: 5665
	public UIBButton m_searchingForFSGFailureOkButton;

	// Token: 0x04001622 RID: 5666
	public Spell m_successRingSpell;

	// Token: 0x04001623 RID: 5667
	public GameObject m_successRingContainer;

	// Token: 0x04001624 RID: 5668
	public GameObject m_gpsOffIntroPanel;

	// Token: 0x04001625 RID: 5669
	public GameObject m_gpsSearchingPanel;

	// Token: 0x04001626 RID: 5670
	public GameObject m_gpsSuccessPanel;

	// Token: 0x04001627 RID: 5671
	public GameObject m_gpsFailurePanel;

	// Token: 0x04001628 RID: 5672
	public GameObject m_wifiOffIntroPanel;

	// Token: 0x04001629 RID: 5673
	public GameObject m_waitingForWifiPanel;

	// Token: 0x0400162A RID: 5674
	public GameObject m_networkConfirmPanel;

	// Token: 0x0400162B RID: 5675
	public GameObject m_accessPointPanel;

	// Token: 0x0400162C RID: 5676
	public GameObject m_unpackingTavernPanel;

	// Token: 0x0400162D RID: 5677
	public GameObject m_unpackFailedPanel;

	// Token: 0x0400162E RID: 5678
	public GameObject m_searchingForFSGsPanel;

	// Token: 0x0400162F RID: 5679
	public GameObject m_innkeeperSuccessPanel;

	// Token: 0x04001630 RID: 5680
	public GameObject m_searchingForFSGFailurePanel;

	// Token: 0x04001631 RID: 5681
	public UberText m_gpsIntroText;

	// Token: 0x04001632 RID: 5682
	public UberText m_wifiOffIntroText;

	// Token: 0x04001633 RID: 5683
	public UberText m_waitingForWifiText;

	// Token: 0x04001634 RID: 5684
	public UberText m_networkNameText;

	// Token: 0x04001635 RID: 5685
	public UberText m_accessPointsText;

	// Token: 0x04001636 RID: 5686
	public UberText m_numAccessPointsText;

	// Token: 0x04001637 RID: 5687
	public UberText m_wifiConfirmText;

	// Token: 0x04001638 RID: 5688
	public UberText m_searchFailureBodyText;

	// Token: 0x04001639 RID: 5689
	private Action m_completedCallback;

	// Token: 0x0400163A RID: 5690
	private bool m_isInnkeeperSetup;

	// Token: 0x0400163B RID: 5691
	private bool m_isCheckInFailure;

	// Token: 0x0400163C RID: 5692
	private bool m_provideWifiForTavern = true;

	// Token: 0x0400163D RID: 5693
	private HashSet<string> m_innkeeperCollectedBSSIDS = new HashSet<string>();

	// Token: 0x0400163E RID: 5694
	private FiresideGatheringLocationHelperDialog.DialogState m_state;

	// Token: 0x0400163F RID: 5695
	private Map<FiresideGatheringLocationHelperDialog.DialogState, FiresideGatheringLocationHelperDialog.Layout> m_stateLayouts = new Map<FiresideGatheringLocationHelperDialog.DialogState, FiresideGatheringLocationHelperDialog.Layout>();

	// Token: 0x04001640 RID: 5696
	private List<GameObject> m_panels = new List<GameObject>();

	// Token: 0x04001641 RID: 5697
	private double m_searchStartTimestamp = double.MaxValue;

	// Token: 0x04001642 RID: 5698
	private float m_wifiCheckTimer;

	// Token: 0x04001643 RID: 5699
	private float m_wifiCheckCadence = 5f;

	// Token: 0x04001644 RID: 5700
	private float m_fsgSearchTimer;

	// Token: 0x04001645 RID: 5701
	private float m_fsgSearchTimeMaximum = 20f;

	// Token: 0x020015FF RID: 5631
	public class Info
	{
		// Token: 0x0400AF86 RID: 44934
		public Action m_callback;

		// Token: 0x0400AF87 RID: 44935
		public string m_gpsOffIntroText;

		// Token: 0x0400AF88 RID: 44936
		public string m_wifiOffIntroText;

		// Token: 0x0400AF89 RID: 44937
		public string m_waitingForWifiText;

		// Token: 0x0400AF8A RID: 44938
		public string m_wifiConfirmText;

		// Token: 0x0400AF8B RID: 44939
		public bool m_isInnkeeperSetup;

		// Token: 0x0400AF8C RID: 44940
		public bool m_isCheckInFailure;
	}

	// Token: 0x02001600 RID: 5632
	private enum DialogState
	{
		// Token: 0x0400AF8E RID: 44942
		INVALID,
		// Token: 0x0400AF8F RID: 44943
		GPS_INTRO,
		// Token: 0x0400AF90 RID: 44944
		SEARCHING_FOR_GPS,
		// Token: 0x0400AF91 RID: 44945
		GPS_SUCCESS,
		// Token: 0x0400AF92 RID: 44946
		GPS_FAILURE,
		// Token: 0x0400AF93 RID: 44947
		WIFI_INTRO,
		// Token: 0x0400AF94 RID: 44948
		WAITING_FOR_WIFI,
		// Token: 0x0400AF95 RID: 44949
		NETWORK_CONFIRM,
		// Token: 0x0400AF96 RID: 44950
		INNKEEPER_SUCCESS,
		// Token: 0x0400AF97 RID: 44951
		COLLECTING_ACCESS_POINTS,
		// Token: 0x0400AF98 RID: 44952
		SETTING_UP_TAVERN,
		// Token: 0x0400AF99 RID: 44953
		UNPACK_FAILED,
		// Token: 0x0400AF9A RID: 44954
		SEARCHING_FOR_FSGS,
		// Token: 0x0400AF9B RID: 44955
		SEARCHING_FOR_FSGS_FAILURE
	}

	// Token: 0x02001601 RID: 5633
	private class Layout
	{
		// Token: 0x0400AF9C RID: 44956
		public SpellStateType m_successRingState;

		// Token: 0x0400AF9D RID: 44957
		public bool m_twoButtons;

		// Token: 0x0400AF9E RID: 44958
		public GameObject m_activePanel;

		// Token: 0x0400AF9F RID: 44959
		public string m_continueButtonText = "";

		// Token: 0x0400AFA0 RID: 44960
		public string m_choiceOneButtonText = "";

		// Token: 0x0400AFA1 RID: 44961
		public string m_choiceTwoButtonText = "";

		// Token: 0x0400AFA2 RID: 44962
		public Action m_continueButtonAction;

		// Token: 0x0400AFA3 RID: 44963
		public Action m_choiceButtonOneAction;

		// Token: 0x0400AFA4 RID: 44964
		public Action m_choiceButtonTwoAction;

		// Token: 0x0400AFA5 RID: 44965
		public Action m_onInit;
	}
}
