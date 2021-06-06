using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Content.Delivery;
using Hearthstone;
using Hearthstone.Http;
using MiniJSON;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x02000631 RID: 1585
public class InnKeepersSpecial : MonoBehaviour
{
	// Token: 0x060058FF RID: 22783 RVA: 0x001D0834 File Offset: 0x001CEA34
	public static InnKeepersSpecial Get()
	{
		InnKeepersSpecial.Init();
		return InnKeepersSpecial.s_instance;
	}

	// Token: 0x1700052E RID: 1326
	// (get) Token: 0x06005900 RID: 22784 RVA: 0x001D0840 File Offset: 0x001CEA40
	public InnKeepersSpecialAd AdToDisplay
	{
		get
		{
			if (!this.m_allAdsFromServer.Any<InnKeepersSpecialAd>())
			{
				return new InnKeepersSpecialAd();
			}
			return this.m_allAdsFromServer[0];
		}
	}

	// Token: 0x06005901 RID: 22785 RVA: 0x001D0864 File Offset: 0x001CEA64
	public static void Init()
	{
		if (InnKeepersSpecial.s_instance == null)
		{
			InnKeepersSpecial.s_instance = AssetLoader.Get().InstantiatePrefab("InnKeepersSpecial.prefab:fe19b8065e74440e4bf42d73cbbf3662", AssetLoadingOptions.None).GetComponent<InnKeepersSpecial>();
			OverlayUI.Get().AddGameObject(InnKeepersSpecial.s_instance.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
			InnKeepersSpecial.s_instance.m_forceShowIks = Options.Get().GetBool(Option.FORCE_SHOW_IKS);
			InnKeepersSpecial.s_instance.m_titleOrgPos = InnKeepersSpecial.s_instance.adTitle.transform.localPosition;
			InnKeepersSpecial.s_instance.m_subtitleOrgPos = InnKeepersSpecial.s_instance.adSubtitle.transform.localPosition;
		}
	}

	// Token: 0x06005902 RID: 22786 RVA: 0x001D0909 File Offset: 0x001CEB09
	public bool LoadedSuccessfully()
	{
		return this.m_loadedSuccessfully;
	}

	// Token: 0x1700052F RID: 1327
	// (get) Token: 0x06005903 RID: 22787 RVA: 0x001D0911 File Offset: 0x001CEB11
	public bool IsShown
	{
		get
		{
			return this.m_isShown;
		}
	}

	// Token: 0x17000530 RID: 1328
	// (get) Token: 0x06005904 RID: 22788 RVA: 0x001D0919 File Offset: 0x001CEB19
	// (set) Token: 0x06005905 RID: 22789 RVA: 0x001D0921 File Offset: 0x001CEB21
	public bool ProcessingResponse { get; set; }

	// Token: 0x06005906 RID: 22790 RVA: 0x001D092A File Offset: 0x001CEB2A
	public void InitializeURLAndUpdate()
	{
		this.Hide();
		InnKeepersSpecial.MigrationIKSOptions();
		this.InitializeJsonURL(string.Empty);
		this.adButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.Click));
		this.RegisterAllDependencyListeners();
		this.Update();
	}

	// Token: 0x06005907 RID: 22791 RVA: 0x001D0967 File Offset: 0x001CEB67
	public void InitializeJsonURL(string customURL)
	{
		this.m_contentHandler.InitializeJsonURL(customURL);
	}

	// Token: 0x06005908 RID: 22792 RVA: 0x001D0975 File Offset: 0x001CEB75
	public void ResetAdUrl()
	{
		this.m_forceOnetime = true;
	}

	// Token: 0x06005909 RID: 22793 RVA: 0x001D097E File Offset: 0x001CEB7E
	private void Start()
	{
		this.Hide();
	}

	// Token: 0x0600590A RID: 22794 RVA: 0x001D0986 File Offset: 0x001CEB86
	private static void MigrationIKSOptions()
	{
		Options.Get().DeleteOption(Option.IKS_LAST_DOWNLOAD_TIME);
		Options.Get().DeleteOption(Option.IKS_CACHE_AGE);
		Options.Get().DeleteOption(Option.IKS_LAST_DOWNLOAD_RESPONSE);
	}

	// Token: 0x0600590B RID: 22795 RVA: 0x001D09AC File Offset: 0x001CEBAC
	private void RegisterAllDependencyListeners()
	{
		Network network = Network.Get();
		if (network == null)
		{
			return;
		}
		network.RegisterNetHandler(TavernBrawlInfo.PacketID.ID, new Network.NetHandler(this.TavernBrawlInfoReceivedCallback), null);
		network.RegisterNetHandler(TavernBrawlPlayerRecordResponse.PacketID.ID, new Network.NetHandler(this.TavernBrawlPlayerRecordReceivedCallback), null);
		network.RegisterNetHandler(RecruitAFriendDataResponse.PacketID.ID, new Network.NetHandler(this.RecruitProgressReceivedCallback), null);
		network.RegisterNetHandler(AccountLicensesInfoResponse.PacketID.ID, new Network.NetHandler(this.AccountLicensesInfoResponseReceivedCallback), null);
		CollectionManager.Get().RegisterOnInitialCollectionReceivedListener(new Action(this.CollectionProgressReceivedCallback));
	}

	// Token: 0x0600590C RID: 22796 RVA: 0x001D0A54 File Offset: 0x001CEC54
	private void RemoveAllDependencyListeners()
	{
		Network network = Network.Get();
		if (network == null)
		{
			return;
		}
		network.RemoveNetHandler(TavernBrawlInfo.PacketID.ID, new Network.NetHandler(this.TavernBrawlInfoReceivedCallback));
		network.RemoveNetHandler(TavernBrawlPlayerRecordResponse.PacketID.ID, new Network.NetHandler(this.TavernBrawlPlayerRecordReceivedCallback));
		network.RemoveNetHandler(RecruitAFriendDataResponse.PacketID.ID, new Network.NetHandler(this.RecruitProgressReceivedCallback));
		network.RemoveNetHandler(AccountLicensesInfoResponse.PacketID.ID, new Network.NetHandler(this.AccountLicensesInfoResponseReceivedCallback));
		CollectionManager.Get().RemoveOnInitialCollectionReceivedListener(new Action(this.CollectionProgressReceivedCallback));
	}

	// Token: 0x0600590D RID: 22797 RVA: 0x001D0AF8 File Offset: 0x001CECF8
	private void RequestDataForDependencies()
	{
		Network network = Network.Get();
		if (this.m_adsDependOnTavernBrawlProgress && !this.m_tavernBrawlInfoReceived)
		{
			network.RequestTavernBrawlInfo(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
		}
		if (this.m_adsDependOnTavernBrawlProgress && !this.m_tavernBrawlPlayerRecordReceived)
		{
			network.RequestTavernBrawlPlayerRecord(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
		}
		if (this.m_adsDependOnRecruitProgress && !this.m_recruitProgressReceived)
		{
			network.RequestRecruitAFriendData();
		}
		if (this.m_adsDependOnAccountLicenseInfo && !this.m_accountLicenseInfoReceived)
		{
			NetCache.Get().RefreshNetObject<NetCache.NetCacheAccountLicenses>();
		}
		if (this.m_adventureClientGameSaveKey != (GameSaveKeyId)0)
		{
			GameSaveDataManager.Get().Request(this.m_adventureClientGameSaveKey, new GameSaveDataManager.OnRequestDataResponseDelegate(this.AdventureGameSaveDataReceivedCallback));
		}
	}

	// Token: 0x0600590E RID: 22798 RVA: 0x001D0B8D File Offset: 0x001CED8D
	private void AdventureGameSaveDataReceivedCallback(bool success)
	{
		this.m_adventureGameSaveDataReceived = true;
		if (this.m_adsDependOnAdventureGameSaveData)
		{
			this.CheckReadyToDisplay();
		}
	}

	// Token: 0x0600590F RID: 22799 RVA: 0x001D0BA4 File Offset: 0x001CEDA4
	private void TavernBrawlInfoReceivedCallback()
	{
		this.m_tavernBrawlInfoReceived = true;
		Network.Get().RemoveNetHandler(TavernBrawlInfo.PacketID.ID, new Network.NetHandler(this.TavernBrawlInfoReceivedCallback));
		if (this.m_adsDependOnTavernBrawlProgress)
		{
			this.CheckReadyToDisplay();
		}
	}

	// Token: 0x06005910 RID: 22800 RVA: 0x001D0BDC File Offset: 0x001CEDDC
	private void TavernBrawlPlayerRecordReceivedCallback()
	{
		this.m_tavernBrawlPlayerRecordReceived = true;
		Network.Get().RemoveNetHandler(TavernBrawlPlayerRecordResponse.PacketID.ID, new Network.NetHandler(this.TavernBrawlPlayerRecordReceivedCallback));
		if (this.m_adsDependOnTavernBrawlProgress)
		{
			this.CheckReadyToDisplay();
		}
	}

	// Token: 0x06005911 RID: 22801 RVA: 0x001D0C14 File Offset: 0x001CEE14
	private void RecruitProgressReceivedCallback()
	{
		this.m_recruitProgressReceived = true;
		Network.Get().RemoveNetHandler(RecruitAFriendDataResponse.PacketID.ID, new Network.NetHandler(this.RecruitProgressReceivedCallback));
		if (this.m_adsDependOnRecruitProgress)
		{
			this.CheckReadyToDisplay();
		}
	}

	// Token: 0x06005912 RID: 22802 RVA: 0x001D0C4C File Offset: 0x001CEE4C
	private void AccountLicensesInfoResponseReceivedCallback()
	{
		this.m_accountLicenseInfoReceived = true;
		Network.Get().RemoveNetHandler(AccountLicensesInfoResponse.PacketID.ID, new Network.NetHandler(this.AccountLicensesInfoResponseReceivedCallback));
		if (this.m_adsDependOnAccountLicenseInfo)
		{
			this.CheckReadyToDisplay();
		}
	}

	// Token: 0x06005913 RID: 22803 RVA: 0x001D0C84 File Offset: 0x001CEE84
	private void CollectionProgressReceivedCallback()
	{
		this.m_collectionProgressReceived = true;
		CollectionManager.Get().RemoveOnInitialCollectionReceivedListener(new Action(this.CollectionProgressReceivedCallback));
		if (this.m_adsDependOnCollectionProgress)
		{
			this.CheckReadyToDisplay();
		}
	}

	// Token: 0x06005914 RID: 22804 RVA: 0x001D0CB4 File Offset: 0x001CEEB4
	private void CheckReadyToDisplay()
	{
		this.m_readyToDisplay = ((!this.m_adsDependOnAdventureGameSaveData || this.m_adventureGameSaveDataReceived) && (!this.m_adsDependOnAccountLicenseInfo || this.m_accountLicenseInfoReceived) && (!this.m_adsDependOnRecruitProgress || this.m_recruitProgressReceived) && (!this.m_adsDependOnTavernBrawlProgress || (this.m_tavernBrawlInfoReceived && this.m_tavernBrawlPlayerRecordReceived)) && (!this.m_adsDependOnCollectionProgress || this.m_collectionProgressReceived));
		if (this.m_readyToDisplay)
		{
			Action[] array = this.m_readyToDisplayListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i]();
			}
		}
	}

	// Token: 0x06005915 RID: 22805 RVA: 0x001D0D4D File Offset: 0x001CEF4D
	public void RegisterReadyToDisplayCallback(Action callback)
	{
		if (!this.m_readyToDisplayListeners.Contains(callback))
		{
			this.m_readyToDisplayListeners.Add(callback);
		}
		if (this.m_readyToDisplay)
		{
			callback();
		}
	}

	// Token: 0x06005916 RID: 22806 RVA: 0x001D0D77 File Offset: 0x001CEF77
	public void RegisterLoadedSuccessfullyCallback(Action callback)
	{
		if (!this.m_loadedSuccessfullyListeners.Contains(callback))
		{
			this.m_loadedSuccessfullyListeners.Add(callback);
		}
		if (this.m_loadedSuccessfully)
		{
			callback();
		}
	}

	// Token: 0x06005917 RID: 22807 RVA: 0x001D0DA4 File Offset: 0x001CEFA4
	public static bool CheckShow(Action callback)
	{
		if (InnKeepersSpecial.s_instance == null)
		{
			return false;
		}
		InnKeepersSpecial.s_instance.m_callback = callback;
		if (!InnKeepersSpecial.s_instance.LoadedSuccessfully())
		{
			Log.InnKeepersSpecial.Print("Skipping IKS! IKS Views not incremented. loadedSuccessfully={0}", new object[]
			{
				InnKeepersSpecial.Get().LoadedSuccessfully()
			});
			return false;
		}
		int num = Options.Get().GetInt(Option.IKS_VIEW_ATTEMPTS, 0);
		num++;
		Options.Get().SetInt(Option.IKS_VIEW_ATTEMPTS, num);
		bool flag = num > 3;
		int num2 = 0;
		bool @bool = Options.Get().GetBool(Option.FORCE_SHOW_IKS);
		if (ReturningPlayerMgr.Get().SuppressOldPopups)
		{
			Log.InnKeepersSpecial.Print("Skipping IKS! ReturningPlayerMgr.Get().SuppressOldPopups={1}!", new object[]
			{
				ReturningPlayerMgr.Get().SuppressOldPopups
			});
			return false;
		}
		if (!flag && !@bool)
		{
			Log.InnKeepersSpecial.Print("Skipping IKS! views={0} lastShownViews={1}", new object[]
			{
				num,
				num2
			});
			return false;
		}
		Log.InnKeepersSpecial.Print("Showing IKS!", Array.Empty<object>());
		InnKeepersSpecial.s_instance.LockBnetButtons();
		InnKeepersSpecial.s_instance.ShowAdAndIncrementViewCountWhenReady();
		return true;
	}

	// Token: 0x06005918 RID: 22808 RVA: 0x001D0EBF File Offset: 0x001CF0BF
	public void ShowAdAndIncrementViewCountWhenReady()
	{
		if (this.m_allAdsFromServer == null || !this.m_allAdsFromServer.Any<InnKeepersSpecialAd>())
		{
			this.Hide();
			return;
		}
		this.RegisterReadyToDisplayCallback(delegate
		{
			if (this.m_allAdsFromServer.Any<InnKeepersSpecialAd>())
			{
				this.RegisterLoadedSuccessfullyCallback(delegate
				{
					this.IncremenetViewCountOfDisplayedAdInStorage();
					this.Show();
				});
			}
		});
	}

	// Token: 0x06005919 RID: 22809 RVA: 0x001D0EF0 File Offset: 0x001CF0F0
	public void Show()
	{
		float num = 0.5f;
		this.content.SetActive(true);
		Material material = this.adImage.gameObject.GetComponent<Renderer>().GetMaterial();
		Color color = material.color;
		color.a = 0f;
		material.color = color;
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			1f,
			"time",
			num,
			"easeType",
			iTween.EaseType.linear
		});
		iTween.FadeTo(this.adImage.gameObject, args);
		this.adTitle.Show();
		Hashtable args2 = iTween.Hash(new object[]
		{
			"from",
			0f,
			"to",
			1f,
			"time",
			num,
			"easeType",
			iTween.EaseType.linear,
			"onupdate",
			new Action<object>(delegate(object newVal)
			{
				this.adTitle.TextAlpha = (float)newVal;
			})
		});
		iTween.ValueTo(this.adTitle.gameObject, args2);
		this.adSubtitle.Show();
		Hashtable args3 = iTween.Hash(new object[]
		{
			"from",
			0f,
			"to",
			1f,
			"time",
			num,
			"easeType",
			iTween.EaseType.linear,
			"onupdate",
			new Action<object>(delegate(object newVal)
			{
				this.adSubtitle.TextAlpha = (float)newVal;
			})
		});
		iTween.ValueTo(this.adSubtitle.gameObject, args3);
		this.m_isShown = true;
		this.m_wasInteractedWith = false;
	}

	// Token: 0x0600591A RID: 22810 RVA: 0x001D10BE File Offset: 0x001CF2BE
	public void Hide()
	{
		this.content.SetActive(false);
		this.adTitle.Hide();
		this.adSubtitle.Hide();
		this.m_isShown = false;
	}

	// Token: 0x0600591B RID: 22811 RVA: 0x001D10E9 File Offset: 0x001CF2E9
	public static void Close()
	{
		if (InnKeepersSpecial.s_instance != null)
		{
			InnKeepersSpecial.s_instance.CloseInternal();
		}
	}

	// Token: 0x0600591C RID: 22812 RVA: 0x001D1104 File Offset: 0x001CF304
	private void CloseInternal()
	{
		if (this.m_isShown && !this.m_wasInteractedWith)
		{
			TelemetryManager.Client().SendIKSIgnored(this.AdToDisplay.CampaignName, this.AdToDisplay.ImageUrl);
		}
		this.Hide();
		this.UnlockBnetButtons();
		this.RemoveAllDependencyListeners();
		this.m_readyToDisplayListeners.Clear();
		this.m_loadedSuccessfullyListeners.Clear();
		UnityEngine.Object.Destroy(base.gameObject);
		InnKeepersSpecial.s_instance = null;
	}

	// Token: 0x0600591D RID: 22813 RVA: 0x001D117C File Offset: 0x001CF37C
	private void Click(UIEvent e)
	{
		Log.InnKeepersSpecial.Print("IKS on release! Link: " + this.AdToDisplay.Link + " Game Action: " + this.AdToDisplay.GameAction, Array.Empty<object>());
		this.m_wasInteractedWith = true;
		TelemetryManager.Client().SendIKSClicked(this.AdToDisplay.CampaignName, this.AdToDisplay.ImageUrl);
		this.SetAdViewCountInStorage(this.AdToDisplay.GetHash(), this.AdToDisplay.MaxViewCount + 1);
		if (!string.IsNullOrEmpty(this.AdToDisplay.GameAction))
		{
			DeepLinkManager.ExecuteDeepLink(this.AdToDisplay.GameAction.Split(new char[]
			{
				' '
			}), DeepLinkManager.DeepLinkSource.INNKEEPERS_SPECIAL, false);
			WelcomeQuests.OnNavigateBack();
			this.Hide();
		}
		else if (!string.IsNullOrEmpty(this.AdToDisplay.Link))
		{
			if (PlatformSettings.IsMobile())
			{
				AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
				popupInfo.m_showAlertIcon = false;
				popupInfo.m_headerText = GameStrings.Format("GLUE_INNKEEPERS_SPECIAL_CONFIRM_POPUP_HEADER", Array.Empty<object>());
				popupInfo.m_text = GameStrings.Get("GLUE_INNKEEPERS_SPECIAL_CONFIRM_POPUP_MESSAGE");
				popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
				popupInfo.m_disableBnetBar = true;
				AlertPopup.ResponseCallback responseCallback = delegate(AlertPopup.Response response, object userdata)
				{
					if (response == AlertPopup.Response.CONFIRM)
					{
						Application.OpenURL(this.AdToDisplay.Link);
					}
				};
				popupInfo.m_responseCallback = responseCallback;
				DialogManager.Get().ShowPopup(popupInfo);
			}
			else
			{
				Application.OpenURL(this.AdToDisplay.Link);
			}
		}
		else
		{
			Debug.LogWarning("InnKeepersSpecial Ad has no Game Action and Link is null or empty.");
		}
		Action callback = this.m_callback;
		if (callback == null)
		{
			return;
		}
		callback();
	}

	// Token: 0x0600591E RID: 22814 RVA: 0x001D12F0 File Offset: 0x001CF4F0
	private void UpdateAdJson(string jsonResponse, object param)
	{
		if (!string.IsNullOrEmpty(jsonResponse))
		{
			JsonNode response;
			try
			{
				response = (Json.Deserialize(jsonResponse) as JsonNode);
			}
			catch (Exception ex)
			{
				response = null;
				Log.ContentConnect.PrintWarning("Aborting because of an invalid json response:\n{0}", new object[]
				{
					jsonResponse
				});
				Debug.LogError(ex.StackTrace);
			}
			this.m_allAdsFromServer = this.GetAllAdsFromJsonResponse(response);
			if (this.m_allAdsFromServer.Any<InnKeepersSpecialAd>())
			{
				this.CheckAdDependenciesAndRequestData(this.AdToDisplay.GameAction);
				this.RegisterReadyToDisplayCallback(new Action(this.VerifyAdToDisplayBasedOnResponses));
			}
		}
		this.ProcessingResponse = false;
	}

	// Token: 0x0600591F RID: 22815 RVA: 0x001D1390 File Offset: 0x001CF590
	private JsonList GetRootListNode(JsonNode response)
	{
		return this.m_contentHandler.GetRootListNode(response);
	}

	// Token: 0x06005920 RID: 22816 RVA: 0x001D13A0 File Offset: 0x001CF5A0
	private List<InnKeepersSpecialAd> GetAllAdsFromJsonResponse(JsonNode response)
	{
		if (response == null)
		{
			return new List<InnKeepersSpecialAd>();
		}
		List<InnKeepersSpecialAd> list = new List<InnKeepersSpecialAd>();
		List<InnKeepersSpecialAd> result;
		try
		{
			JsonList rootListNode = this.GetRootListNode(response);
			if (rootListNode == null)
			{
				result = new List<InnKeepersSpecialAd>();
			}
			else
			{
				Dictionary<string, int> viewCountOfAdsFromStorage = this.GetViewCountOfAdsFromStorage();
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				foreach (object obj in rootListNode)
				{
					JsonNode jsonNode = obj as JsonNode;
					InnKeepersSpecialAd innKeepersSpecialAd = this.m_contentHandler.ReadInnKeepersSpecialAd(jsonNode);
					string hash = innKeepersSpecialAd.GetHash();
					int num = viewCountOfAdsFromStorage.TryGetValue(hash, out num) ? num : 0;
					dictionary[hash] = num;
					innKeepersSpecialAd.CurrentViewCount = num;
					if (this.m_forceShowIks || num < innKeepersSpecialAd.MaxViewCount)
					{
						if (!string.IsNullOrEmpty(innKeepersSpecialAd.ClientVersion) && !this.m_forceShowIks && !StringUtils.CompareIgnoreCase(innKeepersSpecialAd.ClientVersion, "20.4"))
						{
							Log.InnKeepersSpecial.Print("Skipping IKS: {0}, mis-matched client version {0} != {1}", new object[]
							{
								innKeepersSpecialAd.CampaignName,
								innKeepersSpecialAd.ClientVersion,
								"20.4"
							});
						}
						else
						{
							if (!string.IsNullOrEmpty(innKeepersSpecialAd.Platform))
							{
								string[] array = innKeepersSpecialAd.Platform.Trim().Split(new char[]
								{
									','
								});
								bool flag = false;
								string[] array2 = array;
								for (int i = 0; i < array2.Length; i++)
								{
									if (StringUtils.CompareIgnoreCase(array2[i].Trim(), PlatformSettings.OS.ToString()))
									{
										flag = true;
									}
								}
								if (!this.m_forceShowIks && !flag)
								{
									Log.InnKeepersSpecial.Print("Skipping IKS: {0}, supported on: {1}; current platform is {2}", new object[]
									{
										innKeepersSpecialAd.CampaignName,
										innKeepersSpecialAd.Platform,
										PlatformSettings.OS.ToString()
									});
									continue;
								}
							}
							if (!string.IsNullOrEmpty(innKeepersSpecialAd.AndroidStore))
							{
								string[] array3 = innKeepersSpecialAd.AndroidStore.Trim().Split(new char[]
								{
									','
								});
								bool flag2 = false;
								string text = AndroidDeviceSettings.Get().GetAndroidStore().ToString();
								string[] array2 = array3;
								for (int i = 0; i < array2.Length; i++)
								{
									if (StringUtils.CompareIgnoreCase(array2[i].Trim(), text))
									{
										flag2 = true;
									}
								}
								if (!this.m_forceShowIks && !flag2)
								{
									Log.InnKeepersSpecial.Print("Skipping IKS: {0}, supported on: {1}; current android store is {2}", new object[]
									{
										innKeepersSpecialAd.CampaignName,
										innKeepersSpecialAd.AndroidStore,
										text
									});
									continue;
								}
							}
							if (!this.m_forceShowIks && HearthstoneApplication.IsPublic() && !innKeepersSpecialAd.Visibility)
							{
								Log.InnKeepersSpecial.Print("Skipping IKS: {0}, not flagged as publicly visible", new object[]
								{
									(string)jsonNode["campaignName"]
								});
							}
							else
							{
								list.Add(innKeepersSpecialAd);
							}
						}
					}
				}
				this.WriteViewCountOfAdsToStorage(dictionary);
				list.Sort(new Comparison<InnKeepersSpecialAd>(InnKeepersSpecialAd.ComparisonDescending));
				result = list;
			}
		}
		catch (Exception arg)
		{
			Debug.LogError("Failed to get correct advertisement: " + arg);
			result = new List<InnKeepersSpecialAd>();
		}
		return result;
	}

	// Token: 0x06005921 RID: 22817 RVA: 0x001D16F8 File Offset: 0x001CF8F8
	private void VerifyAdToDisplayBasedOnResponses()
	{
		if (this == null)
		{
			return;
		}
		if (!this.m_allAdsFromServer.Any<InnKeepersSpecialAd>())
		{
			return;
		}
		if (!this.m_forceShowIks && this.HasInteractedWithAdvertisedProduct(this.AdToDisplay.GameAction))
		{
			Log.InnKeepersSpecial.Print("Player has interacted with the advertised product. Skipping ad: " + this.AdToDisplay.GameAction, Array.Empty<object>());
			this.DiscardCurrentAdAndRequestNextAdData();
			return;
		}
		Log.InnKeepersSpecial.Print("Ad to display :" + this.AdToDisplay.Link, Array.Empty<object>());
		base.StartCoroutine(this.UpdateAdTexture());
	}

	// Token: 0x06005922 RID: 22818 RVA: 0x001D1794 File Offset: 0x001CF994
	private void DiscardCurrentAdAndRequestNextAdData()
	{
		if (!this.m_allAdsFromServer.Any<InnKeepersSpecialAd>())
		{
			return;
		}
		this.m_allAdsFromServer.RemoveAt(0);
		if (!this.m_allAdsFromServer.Any<InnKeepersSpecialAd>())
		{
			return;
		}
		this.CheckAdDependenciesAndRequestData(this.AdToDisplay.GameAction);
	}

	// Token: 0x06005923 RID: 22819 RVA: 0x001D17D0 File Offset: 0x001CF9D0
	private void Update()
	{
		if ((!this.m_calledOnInit || this.m_forceOnetime) && this.m_contentHandler.Ready)
		{
			this.Hide();
			this.ProcessingResponse = true;
			base.StartCoroutine(this.m_contentHandler.GetQuery(new ResponseProcessHandler(this.UpdateAdJson), null, this.m_forceOnetime));
			this.m_forceOnetime = false;
			this.m_calledOnInit = true;
		}
	}

	// Token: 0x06005924 RID: 22820 RVA: 0x001D183A File Offset: 0x001CFA3A
	private IEnumerator UpdateAdTexture()
	{
		if (!string.IsNullOrEmpty(this.AdToDisplay.Title))
		{
			this.adTitle.Text = this.AdToDisplay.Title.Replace("\\n", "\n");
		}
		if (!string.IsNullOrEmpty(this.AdToDisplay.SubTitle))
		{
			this.adSubtitle.Text = this.AdToDisplay.SubTitle.Replace("\\n", "\n");
		}
		string imageUrl = this.AdToDisplay.ImageUrl;
		if (!string.IsNullOrEmpty(this.AdToDisplay.ImageUrl) && this.AdToDisplay.ImageUrl.StartsWith("//"))
		{
			imageUrl = "http:" + this.AdToDisplay.ImageUrl;
		}
		Log.InnKeepersSpecial.Print("image url is " + imageUrl, Array.Empty<object>());
		IHttpRequest textureHttpRequest = HttpRequestFactory.Get().CreateGetTextureRequest(imageUrl);
		yield return textureHttpRequest.SendRequest();
		if (textureHttpRequest.IsNetworkError || textureHttpRequest.IsHttpError)
		{
			Debug.LogError("Failed to download image for Innkeeper's Special: " + imageUrl);
			Debug.LogError(textureHttpRequest.ErrorString);
			this.DiscardCurrentAdAndRequestNextAdData();
			yield break;
		}
		Texture responseAsTexture = textureHttpRequest.ResponseAsTexture;
		if (responseAsTexture.width == 8 && responseAsTexture.height == 8)
		{
			Debug.LogError("Failed to download image for Innkeeper's Special (got 8x8 dummy image): " + imageUrl);
			this.DiscardCurrentAdAndRequestNextAdData();
			yield break;
		}
		Material material = this.adImage.GetComponent<Renderer>().GetMaterial();
		material.mainTexture = responseAsTexture;
		material.mainTexture.wrapMode = TextureWrapMode.Clamp;
		this.UpdateText();
		this.m_loadedSuccessfully = true;
		Action[] array = this.m_loadedSuccessfullyListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
		yield break;
	}

	// Token: 0x06005925 RID: 22821 RVA: 0x001D184C File Offset: 0x001CFA4C
	private void UpdateText()
	{
		if (!string.IsNullOrEmpty(this.AdToDisplay.ButtonText))
		{
			this.adButtonText.GameStringLookup = false;
			this.adButtonText.Text = this.AdToDisplay.ButtonText;
		}
		Vector3 titleOrgPos = this.m_titleOrgPos;
		titleOrgPos.x += (float)this.AdToDisplay.TitleOffsetX;
		titleOrgPos.y += (float)this.AdToDisplay.TitleOffsetY;
		this.adTitle.transform.localPosition = titleOrgPos;
		Vector3 subtitleOrgPos = this.m_subtitleOrgPos;
		subtitleOrgPos.x += (float)this.AdToDisplay.SubTitleOffsetX;
		subtitleOrgPos.y += (float)this.AdToDisplay.SubTitleOffsetY;
		this.adSubtitle.transform.localPosition = subtitleOrgPos;
		this.adTitle.FontSize = this.AdToDisplay.TitleFontSize;
		this.adSubtitle.FontSize = this.AdToDisplay.SubTitleFontSize;
	}

	// Token: 0x06005926 RID: 22822 RVA: 0x001D1948 File Offset: 0x001CFB48
	public bool HasInteractedWithAdvertisedProduct(string gameAction)
	{
		if (string.IsNullOrEmpty(gameAction))
		{
			Log.InnKeepersSpecial.Print("IKS unable to check interaction for product with null gameAction.", Array.Empty<object>());
			return false;
		}
		string[] array = gameAction.Split(new char[]
		{
			' '
		});
		if (array[0].Equals("store", StringComparison.OrdinalIgnoreCase))
		{
			if (array.Length > 1)
			{
				string str = array[1];
				AdventureDbId adventureDbId = EnumUtils.SafeParse<AdventureDbId>(str, AdventureDbId.INVALID, true);
				HeroDbId heroDbId = EnumUtils.SafeParse<HeroDbId>(str, HeroDbId.INVALID, true);
				StorePackType storePackType;
				int boosterId;
				DeepLinkManager.GetBoosterAndStorePackTypeFromGameAction(array, out boosterId, out storePackType);
				if (boosterId != 0)
				{
					if (storePackType == StorePackType.BOOSTER && boosterId == 181)
					{
						return StoreManager.IsFirstPurchaseBundleOwned();
					}
					if (storePackType == StorePackType.MODULAR_BUNDLE)
					{
						return GameDbf.ModularBundleLayout.GetRecords((ModularBundleLayoutDbfRecord r) => r.ModularBundleId == boosterId, -1).Any((ModularBundleLayoutDbfRecord r) => StoreManager.IsHiddenLicenseBundleOwned(r.HiddenLicenseId));
					}
				}
				else if (adventureDbId != AdventureDbId.INVALID)
				{
					if (this.m_adventureClientGameSaveKey != (GameSaveKeyId)0)
					{
						long num;
						GameSaveDataManager.Get().GetSubkeyValue(this.m_adventureClientGameSaveKey, GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_ADVENTURE, out num);
						return num == 1L;
					}
					return false;
				}
				else if (heroDbId != HeroDbId.INVALID)
				{
					string cardIdFromHeroDbId = GameUtils.GetCardIdFromHeroDbId((int)heroDbId);
					return CollectionManager.Get().IsCardInCollection(cardIdFromHeroDbId, TAG_PREMIUM.NORMAL);
				}
				return false;
			}
			return false;
		}
		else
		{
			if (array[0].Equals("recruitafriend", StringComparison.OrdinalIgnoreCase))
			{
				return RAFManager.Get().GetTotalRecruitCount() > 0U;
			}
			if (array[0].Equals("tavernbrawl", StringComparison.OrdinalIgnoreCase))
			{
				return TavernBrawlManager.Get().GamesPlayed > 0 || !TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
			}
			if (!array[0].Equals("adventure", StringComparison.OrdinalIgnoreCase))
			{
				Log.InnKeepersSpecial.Print("IKS unrecognized game action: " + gameAction + " Unable to determine if the player has interacted with it previously. ", Array.Empty<object>());
				return false;
			}
			if (array.Length > 1)
			{
				AdventureDbId adventureDbId2 = EnumUtils.SafeParse<AdventureDbId>(array[1], AdventureDbId.INVALID, true);
				return adventureDbId2 != AdventureDbId.INVALID && AdventureProgressMgr.Get().OwnsOneOrMoreAdventureWings(adventureDbId2);
			}
			return false;
		}
	}

	// Token: 0x06005927 RID: 22823 RVA: 0x001D1B1C File Offset: 0x001CFD1C
	private void CheckAdDependenciesAndRequestData(string gameAction)
	{
		if (string.IsNullOrEmpty(gameAction))
		{
			this.CheckReadyToDisplay();
			return;
		}
		string[] array = gameAction.Split(new char[]
		{
			' '
		});
		if (array[0].Equals("store", StringComparison.OrdinalIgnoreCase))
		{
			if (array.Length > 1)
			{
				string str = array[1];
				AdventureDbId adventureDbId = EnumUtils.SafeParse<AdventureDbId>(str, AdventureDbId.INVALID, true);
				HeroDbId heroDbId = EnumUtils.SafeParse<HeroDbId>(str, HeroDbId.INVALID, true);
				int num;
				StorePackType storePackType;
				DeepLinkManager.GetBoosterAndStorePackTypeFromGameAction(array, out num, out storePackType);
				if (num != 0)
				{
					if ((storePackType == StorePackType.BOOSTER && num == 181) || storePackType == StorePackType.MODULAR_BUNDLE)
					{
						this.m_adsDependOnAccountLicenseInfo = true;
					}
				}
				else if (adventureDbId != AdventureDbId.INVALID)
				{
					this.m_adsDependOnAdventureGameSaveData = true;
				}
				else if (heroDbId != HeroDbId.INVALID)
				{
					this.m_adsDependOnCollectionProgress = true;
				}
			}
		}
		else if (array[0].Equals("recruitafriend", StringComparison.OrdinalIgnoreCase))
		{
			this.m_adsDependOnRecruitProgress = true;
		}
		else if (array[0].Equals("tavernbrawl", StringComparison.OrdinalIgnoreCase))
		{
			this.m_adsDependOnTavernBrawlProgress = true;
		}
		else if (array[0].Equals("adventure", StringComparison.OrdinalIgnoreCase))
		{
			this.m_adsDependOnAdventureGameSaveData = true;
			if (array.Length > 1)
			{
				string str2 = array[1];
				this.m_adventureDbId = EnumUtils.SafeParse<AdventureDbId>(str2, AdventureDbId.INVALID, true);
				AdventureDataDbfRecord record = GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == (int)this.m_adventureDbId);
				if (record != null)
				{
					this.m_adventureClientGameSaveKey = (GameSaveKeyId)record.GameSaveDataClientKey;
				}
			}
		}
		this.RequestDataForDependencies();
		this.CheckReadyToDisplay();
	}

	// Token: 0x06005928 RID: 22824 RVA: 0x001D1C60 File Offset: 0x001CFE60
	public void IncremenetViewCountOfDisplayedAdInStorage()
	{
		if (!this.m_allAdsFromServer.Any<InnKeepersSpecialAd>())
		{
			return;
		}
		string hash = this.AdToDisplay.GetHash();
		InnKeepersSpecialAd adToDisplay = this.AdToDisplay;
		int num = adToDisplay.CurrentViewCount + 1;
		adToDisplay.CurrentViewCount = num;
		this.SetAdViewCountInStorage(hash, num);
	}

	// Token: 0x06005929 RID: 22825 RVA: 0x001D1CA4 File Offset: 0x001CFEA4
	private void SetAdViewCountInStorage(string adHash, int count)
	{
		if (string.IsNullOrEmpty(adHash))
		{
			return;
		}
		Dictionary<string, int> viewCountOfAdsFromStorage = this.GetViewCountOfAdsFromStorage();
		viewCountOfAdsFromStorage[adHash] = count;
		this.WriteViewCountOfAdsToStorage(viewCountOfAdsFromStorage);
	}

	// Token: 0x0600592A RID: 22826 RVA: 0x001D1CD0 File Offset: 0x001CFED0
	private Dictionary<string, int> GetViewCountOfAdsFromStorage()
	{
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		string @string = Options.Get().GetString(Option.IKS_LAST_SHOWN_AD);
		if (string.IsNullOrEmpty(@string))
		{
			return dictionary;
		}
		string[] array = @string.Split(new char[]
		{
			';'
		});
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(new char[]
			{
				','
			});
			if (array2.Length == 2)
			{
				string key = array2[0];
				int num = int.TryParse(array2[1], out num) ? num : 0;
				if (!dictionary.ContainsKey(key))
				{
					dictionary.Add(key, num);
				}
			}
		}
		return dictionary;
	}

	// Token: 0x0600592B RID: 22827 RVA: 0x001D1D68 File Offset: 0x001CFF68
	private void WriteViewCountOfAdsToStorage(Dictionary<string, int> values)
	{
		string val = string.Join(';'.ToString(), (from kvp in values
		select kvp.Key + "," + kvp.Value).ToArray<string>());
		Options.Get().SetString(Option.IKS_LAST_SHOWN_AD, val);
	}

	// Token: 0x0600592C RID: 22828 RVA: 0x001D1DBC File Offset: 0x001CFFBC
	private void LockBnetButtons()
	{
		if (BaseUI.Get() == null || this.m_bnetButtonsLocked)
		{
			return;
		}
		BaseUI.Get().m_BnetBar.RequestDisableButtons();
		this.m_bnetButtonsLocked = true;
	}

	// Token: 0x0600592D RID: 22829 RVA: 0x001D1DEA File Offset: 0x001CFFEA
	private void UnlockBnetButtons()
	{
		if (BaseUI.Get() == null || !this.m_bnetButtonsLocked)
		{
			return;
		}
		BaseUI.Get().m_BnetBar.CancelRequestToDisableButtons();
		this.m_bnetButtonsLocked = false;
	}

	// Token: 0x04004C2C RID: 19500
	public GameObject adImage;

	// Token: 0x04004C2D RID: 19501
	public GameObject adBackground;

	// Token: 0x04004C2E RID: 19502
	public PegUIElement adButton;

	// Token: 0x04004C2F RID: 19503
	public UberText adButtonText;

	// Token: 0x04004C30 RID: 19504
	public UberText adTitle;

	// Token: 0x04004C31 RID: 19505
	public UberText adSubtitle;

	// Token: 0x04004C32 RID: 19506
	public GameObject content;

	// Token: 0x04004C33 RID: 19507
	private Vector3 m_titleOrgPos;

	// Token: 0x04004C34 RID: 19508
	private Vector3 m_subtitleOrgPos;

	// Token: 0x04004C35 RID: 19509
	private List<InnKeepersSpecialAd> m_allAdsFromServer;

	// Token: 0x04004C36 RID: 19510
	private const char KEY_VALUE_PAIR_OPTIONS_SEPARATOR = ';';

	// Token: 0x04004C37 RID: 19511
	private const char HASH_COUNT_OPTIONS_SEPARATOR = ',';

	// Token: 0x04004C38 RID: 19512
	private const int DEFAULT_MAX_MESSAGE_VIEW_COUNT = 3;

	// Token: 0x04004C39 RID: 19513
	private string m_url;

	// Token: 0x04004C3A RID: 19514
	private GeneralStoreMode m_storeMode;

	// Token: 0x04004C3B RID: 19515
	private AdventureDbId m_adventureDbId;

	// Token: 0x04004C3C RID: 19516
	private AdventureModeDbId m_adventureModeDbId;

	// Token: 0x04004C3D RID: 19517
	private GameSaveKeyId m_adventureClientGameSaveKey;

	// Token: 0x04004C3E RID: 19518
	private static InnKeepersSpecial s_instance;

	// Token: 0x04004C3F RID: 19519
	private bool m_loadedSuccessfully;

	// Token: 0x04004C40 RID: 19520
	private bool m_forceShowIks;

	// Token: 0x04004C41 RID: 19521
	private bool m_forceOnetime;

	// Token: 0x04004C42 RID: 19522
	private bool m_calledOnInit;

	// Token: 0x04004C43 RID: 19523
	private bool m_isShown;

	// Token: 0x04004C44 RID: 19524
	private bool m_wasInteractedWith;

	// Token: 0x04004C45 RID: 19525
	private bool m_adsDependOnAdventureGameSaveData;

	// Token: 0x04004C46 RID: 19526
	private bool m_adsDependOnTavernBrawlProgress;

	// Token: 0x04004C47 RID: 19527
	private bool m_adsDependOnRecruitProgress;

	// Token: 0x04004C48 RID: 19528
	private bool m_adsDependOnAccountLicenseInfo;

	// Token: 0x04004C49 RID: 19529
	private bool m_adsDependOnCollectionProgress;

	// Token: 0x04004C4A RID: 19530
	private bool m_adventureGameSaveDataReceived;

	// Token: 0x04004C4B RID: 19531
	private bool m_tavernBrawlInfoReceived;

	// Token: 0x04004C4C RID: 19532
	private bool m_tavernBrawlPlayerRecordReceived;

	// Token: 0x04004C4D RID: 19533
	private bool m_recruitProgressReceived;

	// Token: 0x04004C4E RID: 19534
	private bool m_accountLicenseInfoReceived;

	// Token: 0x04004C4F RID: 19535
	private bool m_collectionProgressReceived;

	// Token: 0x04004C50 RID: 19536
	private bool m_bnetButtonsLocked;

	// Token: 0x04004C51 RID: 19537
	private bool m_readyToDisplay;

	// Token: 0x04004C52 RID: 19538
	private List<Action> m_readyToDisplayListeners = new List<Action>();

	// Token: 0x04004C53 RID: 19539
	private List<Action> m_loadedSuccessfullyListeners = new List<Action>();

	// Token: 0x04004C54 RID: 19540
	private BaseIKSContentProvider m_contentHandler = new ContentStackIKSContentProvider();

	// Token: 0x04004C55 RID: 19541
	private Action m_callback;
}
