using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone;
using Hearthstone.Http;
using MiniJSON;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

public class InnKeepersSpecial : MonoBehaviour
{
	public GameObject adImage;

	public GameObject adBackground;

	public PegUIElement adButton;

	public UberText adButtonText;

	public UberText adTitle;

	public UberText adSubtitle;

	public GameObject content;

	private Vector3 m_titleOrgPos;

	private Vector3 m_subtitleOrgPos;

	private List<InnKeepersSpecialAd> m_allAdsFromServer;

	private const char KEY_VALUE_PAIR_OPTIONS_SEPARATOR = ';';

	private const char HASH_COUNT_OPTIONS_SEPARATOR = ',';

	private const int DEFAULT_MAX_MESSAGE_VIEW_COUNT = 3;

	private string m_url;

	private GeneralStoreMode m_storeMode;

	private AdventureDbId m_adventureDbId;

	private AdventureModeDbId m_adventureModeDbId;

	private GameSaveKeyId m_adventureClientGameSaveKey;

	private static InnKeepersSpecial s_instance;

	private bool m_loadedSuccessfully;

	private bool m_forceShowIks;

	private bool m_forceOnetime;

	private bool m_calledOnInit;

	private bool m_isShown;

	private bool m_wasInteractedWith;

	private bool m_adsDependOnAdventureGameSaveData;

	private bool m_adsDependOnTavernBrawlProgress;

	private bool m_adsDependOnRecruitProgress;

	private bool m_adsDependOnAccountLicenseInfo;

	private bool m_adsDependOnCollectionProgress;

	private bool m_adventureGameSaveDataReceived;

	private bool m_tavernBrawlInfoReceived;

	private bool m_tavernBrawlPlayerRecordReceived;

	private bool m_recruitProgressReceived;

	private bool m_accountLicenseInfoReceived;

	private bool m_collectionProgressReceived;

	private bool m_bnetButtonsLocked;

	private bool m_readyToDisplay;

	private List<Action> m_readyToDisplayListeners = new List<Action>();

	private List<Action> m_loadedSuccessfullyListeners = new List<Action>();

	private BaseIKSContentProvider m_contentHandler = new ContentStackIKSContentProvider();

	private Action m_callback;

	public InnKeepersSpecialAd AdToDisplay
	{
		get
		{
			if (!m_allAdsFromServer.Any())
			{
				return new InnKeepersSpecialAd();
			}
			return m_allAdsFromServer[0];
		}
	}

	public bool IsShown => m_isShown;

	public bool ProcessingResponse { get; set; }

	public static InnKeepersSpecial Get()
	{
		Init();
		return s_instance;
	}

	public static void Init()
	{
		if (s_instance == null)
		{
			s_instance = AssetLoader.Get().InstantiatePrefab("InnKeepersSpecial.prefab:fe19b8065e74440e4bf42d73cbbf3662").GetComponent<InnKeepersSpecial>();
			OverlayUI.Get().AddGameObject(s_instance.gameObject);
			s_instance.m_forceShowIks = Options.Get().GetBool(Option.FORCE_SHOW_IKS);
			s_instance.m_titleOrgPos = s_instance.adTitle.transform.localPosition;
			s_instance.m_subtitleOrgPos = s_instance.adSubtitle.transform.localPosition;
		}
	}

	public bool LoadedSuccessfully()
	{
		return m_loadedSuccessfully;
	}

	public void InitializeURLAndUpdate()
	{
		Hide();
		MigrationIKSOptions();
		InitializeJsonURL(string.Empty);
		adButton.AddEventListener(UIEventType.RELEASE, Click);
		RegisterAllDependencyListeners();
		Update();
	}

	public void InitializeJsonURL(string customURL)
	{
		m_contentHandler.InitializeJsonURL(customURL);
	}

	public void ResetAdUrl()
	{
		m_forceOnetime = true;
	}

	private void Start()
	{
		Hide();
	}

	private static void MigrationIKSOptions()
	{
		Options.Get().DeleteOption(Option.IKS_LAST_DOWNLOAD_TIME);
		Options.Get().DeleteOption(Option.IKS_CACHE_AGE);
		Options.Get().DeleteOption(Option.IKS_LAST_DOWNLOAD_RESPONSE);
	}

	private void RegisterAllDependencyListeners()
	{
		Network network = Network.Get();
		if (network != null)
		{
			network.RegisterNetHandler(TavernBrawlInfo.PacketID.ID, TavernBrawlInfoReceivedCallback);
			network.RegisterNetHandler(TavernBrawlPlayerRecordResponse.PacketID.ID, TavernBrawlPlayerRecordReceivedCallback);
			network.RegisterNetHandler(RecruitAFriendDataResponse.PacketID.ID, RecruitProgressReceivedCallback);
			network.RegisterNetHandler(AccountLicensesInfoResponse.PacketID.ID, AccountLicensesInfoResponseReceivedCallback);
			CollectionManager.Get().RegisterOnInitialCollectionReceivedListener(CollectionProgressReceivedCallback);
		}
	}

	private void RemoveAllDependencyListeners()
	{
		Network network = Network.Get();
		if (network != null)
		{
			network.RemoveNetHandler(TavernBrawlInfo.PacketID.ID, TavernBrawlInfoReceivedCallback);
			network.RemoveNetHandler(TavernBrawlPlayerRecordResponse.PacketID.ID, TavernBrawlPlayerRecordReceivedCallback);
			network.RemoveNetHandler(RecruitAFriendDataResponse.PacketID.ID, RecruitProgressReceivedCallback);
			network.RemoveNetHandler(AccountLicensesInfoResponse.PacketID.ID, AccountLicensesInfoResponseReceivedCallback);
			CollectionManager.Get().RemoveOnInitialCollectionReceivedListener(CollectionProgressReceivedCallback);
		}
	}

	private void RequestDataForDependencies()
	{
		Network network = Network.Get();
		if (m_adsDependOnTavernBrawlProgress && !m_tavernBrawlInfoReceived)
		{
			network.RequestTavernBrawlInfo(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
		}
		if (m_adsDependOnTavernBrawlProgress && !m_tavernBrawlPlayerRecordReceived)
		{
			network.RequestTavernBrawlPlayerRecord(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
		}
		if (m_adsDependOnRecruitProgress && !m_recruitProgressReceived)
		{
			network.RequestRecruitAFriendData();
		}
		if (m_adsDependOnAccountLicenseInfo && !m_accountLicenseInfoReceived)
		{
			NetCache.Get().RefreshNetObject<NetCache.NetCacheAccountLicenses>();
		}
		if (m_adventureClientGameSaveKey != 0)
		{
			GameSaveDataManager.Get().Request(m_adventureClientGameSaveKey, AdventureGameSaveDataReceivedCallback);
		}
	}

	private void AdventureGameSaveDataReceivedCallback(bool success)
	{
		m_adventureGameSaveDataReceived = true;
		if (m_adsDependOnAdventureGameSaveData)
		{
			CheckReadyToDisplay();
		}
	}

	private void TavernBrawlInfoReceivedCallback()
	{
		m_tavernBrawlInfoReceived = true;
		Network.Get().RemoveNetHandler(TavernBrawlInfo.PacketID.ID, TavernBrawlInfoReceivedCallback);
		if (m_adsDependOnTavernBrawlProgress)
		{
			CheckReadyToDisplay();
		}
	}

	private void TavernBrawlPlayerRecordReceivedCallback()
	{
		m_tavernBrawlPlayerRecordReceived = true;
		Network.Get().RemoveNetHandler(TavernBrawlPlayerRecordResponse.PacketID.ID, TavernBrawlPlayerRecordReceivedCallback);
		if (m_adsDependOnTavernBrawlProgress)
		{
			CheckReadyToDisplay();
		}
	}

	private void RecruitProgressReceivedCallback()
	{
		m_recruitProgressReceived = true;
		Network.Get().RemoveNetHandler(RecruitAFriendDataResponse.PacketID.ID, RecruitProgressReceivedCallback);
		if (m_adsDependOnRecruitProgress)
		{
			CheckReadyToDisplay();
		}
	}

	private void AccountLicensesInfoResponseReceivedCallback()
	{
		m_accountLicenseInfoReceived = true;
		Network.Get().RemoveNetHandler(AccountLicensesInfoResponse.PacketID.ID, AccountLicensesInfoResponseReceivedCallback);
		if (m_adsDependOnAccountLicenseInfo)
		{
			CheckReadyToDisplay();
		}
	}

	private void CollectionProgressReceivedCallback()
	{
		m_collectionProgressReceived = true;
		CollectionManager.Get().RemoveOnInitialCollectionReceivedListener(CollectionProgressReceivedCallback);
		if (m_adsDependOnCollectionProgress)
		{
			CheckReadyToDisplay();
		}
	}

	private void CheckReadyToDisplay()
	{
		m_readyToDisplay = (!m_adsDependOnAdventureGameSaveData || m_adventureGameSaveDataReceived) && (!m_adsDependOnAccountLicenseInfo || m_accountLicenseInfoReceived) && (!m_adsDependOnRecruitProgress || m_recruitProgressReceived) && (!m_adsDependOnTavernBrawlProgress || (m_tavernBrawlInfoReceived && m_tavernBrawlPlayerRecordReceived)) && (!m_adsDependOnCollectionProgress || m_collectionProgressReceived);
		if (m_readyToDisplay)
		{
			Action[] array = m_readyToDisplayListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i]();
			}
		}
	}

	public void RegisterReadyToDisplayCallback(Action callback)
	{
		if (!m_readyToDisplayListeners.Contains(callback))
		{
			m_readyToDisplayListeners.Add(callback);
		}
		if (m_readyToDisplay)
		{
			callback();
		}
	}

	public void RegisterLoadedSuccessfullyCallback(Action callback)
	{
		if (!m_loadedSuccessfullyListeners.Contains(callback))
		{
			m_loadedSuccessfullyListeners.Add(callback);
		}
		if (m_loadedSuccessfully)
		{
			callback();
		}
	}

	public static bool CheckShow(Action callback)
	{
		if (s_instance == null)
		{
			return false;
		}
		s_instance.m_callback = callback;
		if (!s_instance.LoadedSuccessfully())
		{
			Log.InnKeepersSpecial.Print("Skipping IKS! IKS Views not incremented. loadedSuccessfully={0}", Get().LoadedSuccessfully());
			return false;
		}
		int @int = Options.Get().GetInt(Option.IKS_VIEW_ATTEMPTS, 0);
		@int++;
		Options.Get().SetInt(Option.IKS_VIEW_ATTEMPTS, @int);
		bool flag = @int > 3;
		int num = 0;
		bool @bool = Options.Get().GetBool(Option.FORCE_SHOW_IKS);
		if (ReturningPlayerMgr.Get().SuppressOldPopups)
		{
			Log.InnKeepersSpecial.Print("Skipping IKS! ReturningPlayerMgr.Get().SuppressOldPopups={1}!", ReturningPlayerMgr.Get().SuppressOldPopups);
			return false;
		}
		if (!(flag || @bool))
		{
			Log.InnKeepersSpecial.Print("Skipping IKS! views={0} lastShownViews={1}", @int, num);
			return false;
		}
		Log.InnKeepersSpecial.Print("Showing IKS!");
		s_instance.LockBnetButtons();
		s_instance.ShowAdAndIncrementViewCountWhenReady();
		return true;
	}

	public void ShowAdAndIncrementViewCountWhenReady()
	{
		if (m_allAdsFromServer == null || !m_allAdsFromServer.Any())
		{
			Hide();
			return;
		}
		RegisterReadyToDisplayCallback(delegate
		{
			if (m_allAdsFromServer.Any())
			{
				RegisterLoadedSuccessfullyCallback(delegate
				{
					IncremenetViewCountOfDisplayedAdInStorage();
					Show();
				});
			}
		});
	}

	public void Show()
	{
		float num = 0.5f;
		content.SetActive(value: true);
		Material material = adImage.gameObject.GetComponent<Renderer>().GetMaterial();
		Color color = material.color;
		color.a = 0f;
		material.color = color;
		Hashtable args = iTween.Hash("amount", 1f, "time", num, "easeType", iTween.EaseType.linear);
		iTween.FadeTo(adImage.gameObject, args);
		adTitle.Show();
		Hashtable args2 = iTween.Hash("from", 0f, "to", 1f, "time", num, "easeType", iTween.EaseType.linear, "onupdate", (Action<object>)delegate(object newVal)
		{
			adTitle.TextAlpha = (float)newVal;
		});
		iTween.ValueTo(adTitle.gameObject, args2);
		adSubtitle.Show();
		Hashtable args3 = iTween.Hash("from", 0f, "to", 1f, "time", num, "easeType", iTween.EaseType.linear, "onupdate", (Action<object>)delegate(object newVal)
		{
			adSubtitle.TextAlpha = (float)newVal;
		});
		iTween.ValueTo(adSubtitle.gameObject, args3);
		m_isShown = true;
		m_wasInteractedWith = false;
	}

	public void Hide()
	{
		content.SetActive(value: false);
		adTitle.Hide();
		adSubtitle.Hide();
		m_isShown = false;
	}

	public static void Close()
	{
		if (s_instance != null)
		{
			s_instance.CloseInternal();
		}
	}

	private void CloseInternal()
	{
		if (m_isShown && !m_wasInteractedWith)
		{
			TelemetryManager.Client().SendIKSIgnored(AdToDisplay.CampaignName, AdToDisplay.ImageUrl);
		}
		Hide();
		UnlockBnetButtons();
		RemoveAllDependencyListeners();
		m_readyToDisplayListeners.Clear();
		m_loadedSuccessfullyListeners.Clear();
		UnityEngine.Object.Destroy(base.gameObject);
		s_instance = null;
	}

	private void Click(UIEvent e)
	{
		Log.InnKeepersSpecial.Print("IKS on release! Link: " + AdToDisplay.Link + " Game Action: " + AdToDisplay.GameAction);
		m_wasInteractedWith = true;
		TelemetryManager.Client().SendIKSClicked(AdToDisplay.CampaignName, AdToDisplay.ImageUrl);
		SetAdViewCountInStorage(AdToDisplay.GetHash(), AdToDisplay.MaxViewCount + 1);
		if (!string.IsNullOrEmpty(AdToDisplay.GameAction))
		{
			DeepLinkManager.ExecuteDeepLink(AdToDisplay.GameAction.Split(' '), DeepLinkManager.DeepLinkSource.INNKEEPERS_SPECIAL, fromUnpause: false);
			WelcomeQuests.OnNavigateBack();
			Hide();
		}
		else if (!string.IsNullOrEmpty(AdToDisplay.Link))
		{
			if (PlatformSettings.IsMobile())
			{
				AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
				popupInfo.m_showAlertIcon = false;
				popupInfo.m_headerText = GameStrings.Format("GLUE_INNKEEPERS_SPECIAL_CONFIRM_POPUP_HEADER");
				popupInfo.m_text = GameStrings.Get("GLUE_INNKEEPERS_SPECIAL_CONFIRM_POPUP_MESSAGE");
				popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
				popupInfo.m_disableBnetBar = true;
				AlertPopup.ResponseCallback responseCallback = (popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userdata)
				{
					if (response == AlertPopup.Response.CONFIRM)
					{
						Application.OpenURL(AdToDisplay.Link);
					}
				});
				DialogManager.Get().ShowPopup(popupInfo);
			}
			else
			{
				Application.OpenURL(AdToDisplay.Link);
			}
		}
		else
		{
			Debug.LogWarning("InnKeepersSpecial Ad has no Game Action and Link is null or empty.");
		}
		m_callback?.Invoke();
	}

	private void UpdateAdJson(string jsonResponse, object param)
	{
		if (!string.IsNullOrEmpty(jsonResponse))
		{
			JsonNode response;
			try
			{
				response = Json.Deserialize(jsonResponse) as JsonNode;
			}
			catch (Exception ex)
			{
				response = null;
				Log.ContentConnect.PrintWarning("Aborting because of an invalid json response:\n{0}", jsonResponse);
				Debug.LogError(ex.StackTrace);
			}
			m_allAdsFromServer = GetAllAdsFromJsonResponse(response);
			if (m_allAdsFromServer.Any())
			{
				CheckAdDependenciesAndRequestData(AdToDisplay.GameAction);
				RegisterReadyToDisplayCallback(VerifyAdToDisplayBasedOnResponses);
			}
		}
		ProcessingResponse = false;
	}

	private JsonList GetRootListNode(JsonNode response)
	{
		return m_contentHandler.GetRootListNode(response);
	}

	private List<InnKeepersSpecialAd> GetAllAdsFromJsonResponse(JsonNode response)
	{
		if (response == null)
		{
			return new List<InnKeepersSpecialAd>();
		}
		List<InnKeepersSpecialAd> list = new List<InnKeepersSpecialAd>();
		try
		{
			JsonList rootListNode = GetRootListNode(response);
			if (rootListNode == null)
			{
				return new List<InnKeepersSpecialAd>();
			}
			Dictionary<string, int> viewCountOfAdsFromStorage = GetViewCountOfAdsFromStorage();
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			foreach (object item in rootListNode)
			{
				JsonNode jsonNode = item as JsonNode;
				InnKeepersSpecialAd innKeepersSpecialAd = m_contentHandler.ReadInnKeepersSpecialAd(jsonNode);
				string hash = innKeepersSpecialAd.GetHash();
				int value = (innKeepersSpecialAd.CurrentViewCount = (dictionary[hash] = (viewCountOfAdsFromStorage.TryGetValue(hash, out value) ? value : 0)));
				if (!m_forceShowIks && value >= innKeepersSpecialAd.MaxViewCount)
				{
					continue;
				}
				if (!string.IsNullOrEmpty(innKeepersSpecialAd.ClientVersion) && !m_forceShowIks && !StringUtils.CompareIgnoreCase(innKeepersSpecialAd.ClientVersion, "20.4"))
				{
					Log.InnKeepersSpecial.Print("Skipping IKS: {0}, mis-matched client version {0} != {1}", innKeepersSpecialAd.CampaignName, innKeepersSpecialAd.ClientVersion, "20.4");
					continue;
				}
				if (!string.IsNullOrEmpty(innKeepersSpecialAd.Platform))
				{
					string[] array = innKeepersSpecialAd.Platform.Trim().Split(',');
					bool flag = false;
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						if (StringUtils.CompareIgnoreCase(array2[i].Trim(), PlatformSettings.OS.ToString()))
						{
							flag = true;
						}
					}
					if (!m_forceShowIks && !flag)
					{
						Log.InnKeepersSpecial.Print("Skipping IKS: {0}, supported on: {1}; current platform is {2}", innKeepersSpecialAd.CampaignName, innKeepersSpecialAd.Platform, PlatformSettings.OS.ToString());
						continue;
					}
				}
				if (!string.IsNullOrEmpty(innKeepersSpecialAd.AndroidStore))
				{
					string[] array3 = innKeepersSpecialAd.AndroidStore.Trim().Split(',');
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
					if (!m_forceShowIks && !flag2)
					{
						Log.InnKeepersSpecial.Print("Skipping IKS: {0}, supported on: {1}; current android store is {2}", innKeepersSpecialAd.CampaignName, innKeepersSpecialAd.AndroidStore, text);
						continue;
					}
				}
				if (!m_forceShowIks && HearthstoneApplication.IsPublic() && !innKeepersSpecialAd.Visibility)
				{
					Log.InnKeepersSpecial.Print("Skipping IKS: {0}, not flagged as publicly visible", (string)jsonNode["campaignName"]);
				}
				else
				{
					list.Add(innKeepersSpecialAd);
				}
			}
			WriteViewCountOfAdsToStorage(dictionary);
			list.Sort(InnKeepersSpecialAd.ComparisonDescending);
			return list;
		}
		catch (Exception ex)
		{
			Debug.LogError("Failed to get correct advertisement: " + ex);
			return new List<InnKeepersSpecialAd>();
		}
	}

	private void VerifyAdToDisplayBasedOnResponses()
	{
		if (!(this == null) && m_allAdsFromServer.Any())
		{
			if (!m_forceShowIks && HasInteractedWithAdvertisedProduct(AdToDisplay.GameAction))
			{
				Log.InnKeepersSpecial.Print("Player has interacted with the advertised product. Skipping ad: " + AdToDisplay.GameAction);
				DiscardCurrentAdAndRequestNextAdData();
			}
			else
			{
				Log.InnKeepersSpecial.Print("Ad to display :" + AdToDisplay.Link);
				StartCoroutine(UpdateAdTexture());
			}
		}
	}

	private void DiscardCurrentAdAndRequestNextAdData()
	{
		if (m_allAdsFromServer.Any())
		{
			m_allAdsFromServer.RemoveAt(0);
			if (m_allAdsFromServer.Any())
			{
				CheckAdDependenciesAndRequestData(AdToDisplay.GameAction);
			}
		}
	}

	private void Update()
	{
		if ((!m_calledOnInit || m_forceOnetime) && m_contentHandler.Ready)
		{
			Hide();
			ProcessingResponse = true;
			StartCoroutine(m_contentHandler.GetQuery(UpdateAdJson, null, m_forceOnetime));
			m_forceOnetime = false;
			m_calledOnInit = true;
		}
	}

	private IEnumerator UpdateAdTexture()
	{
		if (!string.IsNullOrEmpty(AdToDisplay.Title))
		{
			adTitle.Text = AdToDisplay.Title.Replace("\\n", "\n");
		}
		if (!string.IsNullOrEmpty(AdToDisplay.SubTitle))
		{
			adSubtitle.Text = AdToDisplay.SubTitle.Replace("\\n", "\n");
		}
		string imageUrl = AdToDisplay.ImageUrl;
		if (!string.IsNullOrEmpty(AdToDisplay.ImageUrl) && AdToDisplay.ImageUrl.StartsWith("//"))
		{
			imageUrl = "http:" + AdToDisplay.ImageUrl;
		}
		Log.InnKeepersSpecial.Print("image url is " + imageUrl);
		IHttpRequest textureHttpRequest = HttpRequestFactory.Get().CreateGetTextureRequest(imageUrl);
		yield return textureHttpRequest.SendRequest();
		if (textureHttpRequest.IsNetworkError || textureHttpRequest.IsHttpError)
		{
			Debug.LogError("Failed to download image for Innkeeper's Special: " + imageUrl);
			Debug.LogError(textureHttpRequest.ErrorString);
			DiscardCurrentAdAndRequestNextAdData();
			yield break;
		}
		Texture responseAsTexture = textureHttpRequest.ResponseAsTexture;
		if (responseAsTexture.width == 8 && responseAsTexture.height == 8)
		{
			Debug.LogError("Failed to download image for Innkeeper's Special (got 8x8 dummy image): " + imageUrl);
			DiscardCurrentAdAndRequestNextAdData();
			yield break;
		}
		Material material = adImage.GetComponent<Renderer>().GetMaterial();
		material.mainTexture = responseAsTexture;
		material.mainTexture.wrapMode = TextureWrapMode.Clamp;
		UpdateText();
		m_loadedSuccessfully = true;
		Action[] array = m_loadedSuccessfullyListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	private void UpdateText()
	{
		if (!string.IsNullOrEmpty(AdToDisplay.ButtonText))
		{
			adButtonText.GameStringLookup = false;
			adButtonText.Text = AdToDisplay.ButtonText;
		}
		Vector3 titleOrgPos = m_titleOrgPos;
		titleOrgPos.x += AdToDisplay.TitleOffsetX;
		titleOrgPos.y += AdToDisplay.TitleOffsetY;
		adTitle.transform.localPosition = titleOrgPos;
		Vector3 subtitleOrgPos = m_subtitleOrgPos;
		subtitleOrgPos.x += AdToDisplay.SubTitleOffsetX;
		subtitleOrgPos.y += AdToDisplay.SubTitleOffsetY;
		adSubtitle.transform.localPosition = subtitleOrgPos;
		adTitle.FontSize = AdToDisplay.TitleFontSize;
		adSubtitle.FontSize = AdToDisplay.SubTitleFontSize;
	}

	public bool HasInteractedWithAdvertisedProduct(string gameAction)
	{
		if (string.IsNullOrEmpty(gameAction))
		{
			Log.InnKeepersSpecial.Print("IKS unable to check interaction for product with null gameAction.");
			return false;
		}
		string[] array = gameAction.Split(' ');
		if (array[0].Equals("store", StringComparison.OrdinalIgnoreCase))
		{
			if (array.Length > 1)
			{
				string str = array[1];
				AdventureDbId adventureDbId = EnumUtils.SafeParse(str, AdventureDbId.INVALID, ignoreCase: true);
				HeroDbId heroDbId = EnumUtils.SafeParse(str, HeroDbId.INVALID, ignoreCase: true);
				DeepLinkManager.GetBoosterAndStorePackTypeFromGameAction(array, out var boosterId, out var storePackType);
				if (boosterId != 0)
				{
					if (storePackType == StorePackType.BOOSTER && boosterId == 181)
					{
						return StoreManager.IsFirstPurchaseBundleOwned();
					}
					if (storePackType == StorePackType.MODULAR_BUNDLE)
					{
						return GameDbf.ModularBundleLayout.GetRecords((ModularBundleLayoutDbfRecord r) => r.ModularBundleId == boosterId).Any((ModularBundleLayoutDbfRecord r) => StoreManager.IsHiddenLicenseBundleOwned(r.HiddenLicenseId));
					}
				}
				else
				{
					if (adventureDbId != 0)
					{
						if (m_adventureClientGameSaveKey != 0)
						{
							GameSaveDataManager.Get().GetSubkeyValue(m_adventureClientGameSaveKey, GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_ADVENTURE, out long value);
							return value == 1;
						}
						return false;
					}
					if (heroDbId != 0)
					{
						string cardIdFromHeroDbId = GameUtils.GetCardIdFromHeroDbId((int)heroDbId);
						return CollectionManager.Get().IsCardInCollection(cardIdFromHeroDbId, TAG_PREMIUM.NORMAL);
					}
				}
				return false;
			}
			return false;
		}
		if (array[0].Equals("recruitafriend", StringComparison.OrdinalIgnoreCase))
		{
			return RAFManager.Get().GetTotalRecruitCount() != 0;
		}
		if (array[0].Equals("tavernbrawl", StringComparison.OrdinalIgnoreCase))
		{
			if (TavernBrawlManager.Get().GamesPlayed <= 0)
			{
				return !TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
			}
			return true;
		}
		if (array[0].Equals("adventure", StringComparison.OrdinalIgnoreCase))
		{
			if (array.Length > 1)
			{
				AdventureDbId adventureDbId2 = EnumUtils.SafeParse(array[1], AdventureDbId.INVALID, ignoreCase: true);
				if (adventureDbId2 != 0)
				{
					return AdventureProgressMgr.Get().OwnsOneOrMoreAdventureWings(adventureDbId2);
				}
				return false;
			}
			return false;
		}
		Log.InnKeepersSpecial.Print("IKS unrecognized game action: " + gameAction + " Unable to determine if the player has interacted with it previously. ");
		return false;
	}

	private void CheckAdDependenciesAndRequestData(string gameAction)
	{
		if (string.IsNullOrEmpty(gameAction))
		{
			CheckReadyToDisplay();
			return;
		}
		string[] array = gameAction.Split(' ');
		if (array[0].Equals("store", StringComparison.OrdinalIgnoreCase))
		{
			if (array.Length > 1)
			{
				string str = array[1];
				AdventureDbId adventureDbId = EnumUtils.SafeParse(str, AdventureDbId.INVALID, ignoreCase: true);
				HeroDbId heroDbId = EnumUtils.SafeParse(str, HeroDbId.INVALID, ignoreCase: true);
				DeepLinkManager.GetBoosterAndStorePackTypeFromGameAction(array, out var boosterId, out var storePackType);
				if (boosterId != 0)
				{
					if ((storePackType == StorePackType.BOOSTER && boosterId == 181) || storePackType == StorePackType.MODULAR_BUNDLE)
					{
						m_adsDependOnAccountLicenseInfo = true;
					}
				}
				else if (adventureDbId != 0)
				{
					m_adsDependOnAdventureGameSaveData = true;
				}
				else if (heroDbId != 0)
				{
					m_adsDependOnCollectionProgress = true;
				}
			}
		}
		else if (array[0].Equals("recruitafriend", StringComparison.OrdinalIgnoreCase))
		{
			m_adsDependOnRecruitProgress = true;
		}
		else if (array[0].Equals("tavernbrawl", StringComparison.OrdinalIgnoreCase))
		{
			m_adsDependOnTavernBrawlProgress = true;
		}
		else if (array[0].Equals("adventure", StringComparison.OrdinalIgnoreCase))
		{
			m_adsDependOnAdventureGameSaveData = true;
			if (array.Length > 1)
			{
				string str2 = array[1];
				m_adventureDbId = EnumUtils.SafeParse(str2, AdventureDbId.INVALID, ignoreCase: true);
				AdventureDataDbfRecord record = GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == (int)m_adventureDbId);
				if (record != null)
				{
					m_adventureClientGameSaveKey = (GameSaveKeyId)record.GameSaveDataClientKey;
				}
			}
		}
		RequestDataForDependencies();
		CheckReadyToDisplay();
	}

	public void IncremenetViewCountOfDisplayedAdInStorage()
	{
		if (m_allAdsFromServer.Any())
		{
			SetAdViewCountInStorage(AdToDisplay.GetHash(), ++AdToDisplay.CurrentViewCount);
		}
	}

	private void SetAdViewCountInStorage(string adHash, int count)
	{
		if (!string.IsNullOrEmpty(adHash))
		{
			Dictionary<string, int> viewCountOfAdsFromStorage = GetViewCountOfAdsFromStorage();
			viewCountOfAdsFromStorage[adHash] = count;
			WriteViewCountOfAdsToStorage(viewCountOfAdsFromStorage);
		}
	}

	private Dictionary<string, int> GetViewCountOfAdsFromStorage()
	{
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		string @string = Options.Get().GetString(Option.IKS_LAST_SHOWN_AD);
		if (string.IsNullOrEmpty(@string))
		{
			return dictionary;
		}
		string[] array = @string.Split(';');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(',');
			if (array2.Length == 2)
			{
				string key = array2[0];
				int result = (int.TryParse(array2[1], out result) ? result : 0);
				if (!dictionary.ContainsKey(key))
				{
					dictionary.Add(key, result);
				}
			}
		}
		return dictionary;
	}

	private void WriteViewCountOfAdsToStorage(Dictionary<string, int> values)
	{
		string val = string.Join(';'.ToString(), values.Select((KeyValuePair<string, int> kvp) => kvp.Key + "," + kvp.Value).ToArray());
		Options.Get().SetString(Option.IKS_LAST_SHOWN_AD, val);
	}

	private void LockBnetButtons()
	{
		if (!(BaseUI.Get() == null) && !m_bnetButtonsLocked)
		{
			BaseUI.Get().m_BnetBar.RequestDisableButtons();
			m_bnetButtonsLocked = true;
		}
	}

	private void UnlockBnetButtons()
	{
		if (!(BaseUI.Get() == null) && m_bnetButtonsLocked)
		{
			BaseUI.Get().m_BnetBar.CancelRequestToDisableButtons();
			m_bnetButtonsLocked = false;
		}
	}
}
