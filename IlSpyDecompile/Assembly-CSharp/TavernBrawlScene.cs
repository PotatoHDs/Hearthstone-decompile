using System.Collections;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

[CustomEditClass]
public class TavernBrawlScene : PegasusScene
{
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_CollectionManagerPrefab;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_TavernBrawlPrefab;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_TavernBrawlNoDeckPrefab;

	private bool m_unloading;

	private bool m_tavernBrawlPrefabLoaded;

	private bool m_collectionManagerNeeded;

	private bool m_collectionManagerPrefabLoaded;

	private bool m_pendingSessionBegin;

	protected override void Awake()
	{
		base.Awake();
	}

	private void Start()
	{
		Network.Get().RegisterNetHandler(TavernBrawlRequestSessionBeginResponse.PacketID.ID, OnSessionBeginResponse);
		TavernBrawlManager.Get().EnsureAllDataReady(OnServerDataReady);
	}

	private void Update()
	{
		Network.Get().ProcessNetwork();
	}

	public override bool IsUnloading()
	{
		return m_unloading;
	}

	public override void Unload()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			BnetBar.Get().ToggleActive(active: true);
		}
		m_unloading = true;
		if (CollectionManager.Get().GetCollectibleDisplay() != null)
		{
			CollectionManager.Get().GetCollectibleDisplay().Unload();
		}
		if (TavernBrawlDisplay.Get() != null)
		{
			TavernBrawlDisplay.Get().Unload();
		}
		Network.Get().SendAckCardsSeen();
		StoreManager.Get().RemoveSuccessfulPurchaseAckListener(OnTavernBrawlTicketPurchaseAck);
		m_unloading = false;
	}

	private void OnServerDataReady()
	{
		if (TavernBrawlManager.Get().PlayerStatus == TavernBrawlStatus.TB_STATUS_INVALID)
		{
			if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
			}
			if (TavernBrawlManager.Get().IsCurrentBrawlTypeActive)
			{
				AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
				if (TavernBrawlManager.Get().CurrentMission().brawlMode == TavernBrawlMode.TB_MODE_HEROIC)
				{
					popupInfo.m_headerText = GameStrings.Get("GLUE_HEROIC_BRAWL_SESSION_ERROR_TITLE");
					popupInfo.m_text = GameStrings.Get("GLUE_HEROIC_BRAWL_SESSION_ERROR");
				}
				else
				{
					popupInfo.m_headerText = GameStrings.Get("GLUE_BRAWLISEUM_SESSION_ERROR_TITLE");
					popupInfo.m_text = GameStrings.Get("GLUE_BRAWLISEUM_SESSION_ERROR");
				}
				popupInfo.m_responseCallback = delegate
				{
					TavernBrawlManager.Get().RefreshServerData();
				};
				popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
				popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
				DialogManager.Get().ShowPopup(popupInfo);
			}
		}
		else
		{
			CollectionDeck collectionDeck = TavernBrawlManager.Get().CurrentDeck();
			if (TavernBrawlManager.Get().CurrentSession != null && collectionDeck != null)
			{
				collectionDeck.Locked = TavernBrawlManager.Get().CurrentSession.DeckLocked;
			}
			m_collectionManagerNeeded = TavernBrawlManager.Get().CurrentMission() != null && TavernBrawlManager.Get().CurrentMission().canEditDeck;
			bool flag = SceneMgr.Get().GetMode() != SceneMgr.Mode.FIRESIDE_GATHERING;
			if (m_collectionManagerNeeded)
			{
				AssetLoader.Get().InstantiatePrefab((string)m_TavernBrawlPrefab, OnTavernBrawlLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
				AssetLoader.Get().InstantiatePrefab((string)m_CollectionManagerPrefab, OnCollectionManagerLoaded, null, (!flag) ? AssetLoadingOptions.IgnorePrefabPosition : AssetLoadingOptions.None);
			}
			else
			{
				AssetLoader.Get().InstantiatePrefab((string)m_TavernBrawlNoDeckPrefab, OnTavernBrawlLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
			}
			if (TavernBrawlManager.Get().PlayerStatus == TavernBrawlStatus.TB_STATUS_TICKET_REQUIRED && !TavernBrawlManager.Get().IsEligibleForFreeTicket())
			{
				m_pendingSessionBegin = true;
				Network.Get().RequestTavernBrawlSessionBegin();
			}
			StartCoroutine(NotifySceneLoadedWhenReady());
		}
	}

	private IEnumerator NotifySceneLoadedWhenReady()
	{
		while (!m_tavernBrawlPrefabLoaded)
		{
			yield return 0;
		}
		while (m_collectionManagerNeeded && (!m_collectionManagerPrefabLoaded || !CollectionManager.Get().GetCollectibleDisplay().IsReady()))
		{
			yield return 0;
		}
		while (m_pendingSessionBegin)
		{
			yield return 0;
		}
		TavernBrawlMission tavernBrawlMission = TavernBrawlManager.Get().CurrentMission();
		CollectionDeck collectionDeck = TavernBrawlManager.Get().CurrentDeck();
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (tavernBrawlMission != null && tavernBrawlMission.canCreateDeck && collectionDeck != null && collectionManagerDisplay != null)
		{
			collectionManagerDisplay.ShowTavernBrawlDeck(collectionDeck.ID);
		}
		StoreManager.Get().RegisterSuccessfulPurchaseAckListener(OnTavernBrawlTicketPurchaseAck);
		SceneMgr.Get().NotifySceneLoaded();
	}

	private void OnCollectionManagerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_collectionManagerPrefabLoaded = true;
		if (go == null)
		{
			Debug.LogError($"TavernBrawlScene.OnCollectionManagerLoaded() - failed to load screen {assetRef}");
		}
	}

	private void OnTavernBrawlLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_tavernBrawlPrefabLoaded = true;
		if (go == null)
		{
			Debug.LogError($"TavernBrawlScene.OnTavernBrawlLoaded() - failed to load screen {assetRef}");
		}
		else if (SceneMgr.Get().GetMode() != SceneMgr.Mode.FIRESIDE_GATHERING)
		{
			go.transform.position = Vector3.zero;
		}
	}

	private void OnSessionBeginResponse()
	{
		m_pendingSessionBegin = false;
	}

	private void OnTavernBrawlTicketPurchaseAck(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		Log.TavernBrawl.Print("TavernBrawlScene.OnTavernBrawlTicketPurchaseAck");
		if (bundle == null || bundle.Items == null)
		{
			return;
		}
		foreach (Network.BundleItem item in bundle.Items)
		{
			if ((item != null && item.ItemType == ProductType.PRODUCT_TYPE_TAVERN_BRAWL_TICKET && SceneMgr.Get().IsModeRequested(SceneMgr.Mode.TAVERN_BRAWL)) || SceneMgr.Get().IsModeRequested(SceneMgr.Mode.FIRESIDE_GATHERING))
			{
				TavernBrawlManager.Get().RequestSessionBegin();
				return;
			}
		}
		Log.TavernBrawl.PrintError("TavernBrawlScene.OnTavernBrawlTicketPurchaseAck ERROR: Got a purchase ack in the Tavern Brawl scene for a product we don't recognize");
	}
}
