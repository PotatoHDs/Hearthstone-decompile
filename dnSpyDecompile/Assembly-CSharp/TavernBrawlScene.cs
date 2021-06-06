using System;
using System.Collections;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x0200073B RID: 1851
[CustomEditClass]
public class TavernBrawlScene : PegasusScene
{
	// Token: 0x060068C7 RID: 26823 RVA: 0x0022245D File Offset: 0x0022065D
	protected override void Awake()
	{
		base.Awake();
	}

	// Token: 0x060068C8 RID: 26824 RVA: 0x00222465 File Offset: 0x00220665
	private void Start()
	{
		Network.Get().RegisterNetHandler(TavernBrawlRequestSessionBeginResponse.PacketID.ID, new Network.NetHandler(this.OnSessionBeginResponse), null);
		TavernBrawlManager.Get().EnsureAllDataReady(new TavernBrawlManager.CallbackEnsureServerDataReady(this.OnServerDataReady));
	}

	// Token: 0x060068C9 RID: 26825 RVA: 0x00019DD3 File Offset: 0x00017FD3
	private void Update()
	{
		Network.Get().ProcessNetwork();
	}

	// Token: 0x060068CA RID: 26826 RVA: 0x0022249F File Offset: 0x0022069F
	public override bool IsUnloading()
	{
		return this.m_unloading;
	}

	// Token: 0x060068CB RID: 26827 RVA: 0x002224A8 File Offset: 0x002206A8
	public override void Unload()
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			BnetBar.Get().ToggleActive(true);
		}
		this.m_unloading = true;
		if (CollectionManager.Get().GetCollectibleDisplay() != null)
		{
			CollectionManager.Get().GetCollectibleDisplay().Unload();
		}
		if (TavernBrawlDisplay.Get() != null)
		{
			TavernBrawlDisplay.Get().Unload();
		}
		Network.Get().SendAckCardsSeen();
		StoreManager.Get().RemoveSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnTavernBrawlTicketPurchaseAck));
		this.m_unloading = false;
	}

	// Token: 0x060068CC RID: 26828 RVA: 0x00222534 File Offset: 0x00220734
	private void OnServerDataReady()
	{
		if (TavernBrawlManager.Get().PlayerStatus == TavernBrawlStatus.TB_STATUS_INVALID)
		{
			if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
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
				popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
				{
					TavernBrawlManager.Get().RefreshServerData(BrawlType.BRAWL_TYPE_UNKNOWN);
				};
				popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
				popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
				DialogManager.Get().ShowPopup(popupInfo);
			}
			return;
		}
		CollectionDeck collectionDeck = TavernBrawlManager.Get().CurrentDeck();
		if (TavernBrawlManager.Get().CurrentSession != null && collectionDeck != null)
		{
			collectionDeck.Locked = TavernBrawlManager.Get().CurrentSession.DeckLocked;
		}
		this.m_collectionManagerNeeded = (TavernBrawlManager.Get().CurrentMission() != null && TavernBrawlManager.Get().CurrentMission().canEditDeck);
		bool flag = SceneMgr.Get().GetMode() != SceneMgr.Mode.FIRESIDE_GATHERING;
		if (this.m_collectionManagerNeeded)
		{
			AssetLoader.Get().InstantiatePrefab(this.m_TavernBrawlPrefab, new PrefabCallback<GameObject>(this.OnTavernBrawlLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
			AssetLoader.Get().InstantiatePrefab(this.m_CollectionManagerPrefab, new PrefabCallback<GameObject>(this.OnCollectionManagerLoaded), null, flag ? AssetLoadingOptions.None : AssetLoadingOptions.IgnorePrefabPosition);
		}
		else
		{
			AssetLoader.Get().InstantiatePrefab(this.m_TavernBrawlNoDeckPrefab, new PrefabCallback<GameObject>(this.OnTavernBrawlLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
		}
		if (TavernBrawlManager.Get().PlayerStatus == TavernBrawlStatus.TB_STATUS_TICKET_REQUIRED && !TavernBrawlManager.Get().IsEligibleForFreeTicket())
		{
			this.m_pendingSessionBegin = true;
			Network.Get().RequestTavernBrawlSessionBegin();
		}
		base.StartCoroutine(this.NotifySceneLoadedWhenReady());
	}

	// Token: 0x060068CD RID: 26829 RVA: 0x00222739 File Offset: 0x00220939
	private IEnumerator NotifySceneLoadedWhenReady()
	{
		while (!this.m_tavernBrawlPrefabLoaded)
		{
			yield return 0;
		}
		while (this.m_collectionManagerNeeded)
		{
			if (this.m_collectionManagerPrefabLoaded && CollectionManager.Get().GetCollectibleDisplay().IsReady())
			{
				break;
			}
			yield return 0;
		}
		while (this.m_pendingSessionBegin)
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
		StoreManager.Get().RegisterSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnTavernBrawlTicketPurchaseAck));
		SceneMgr.Get().NotifySceneLoaded();
		yield break;
	}

	// Token: 0x060068CE RID: 26830 RVA: 0x00222748 File Offset: 0x00220948
	private void OnCollectionManagerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_collectionManagerPrefabLoaded = true;
		if (go == null)
		{
			Debug.LogError(string.Format("TavernBrawlScene.OnCollectionManagerLoaded() - failed to load screen {0}", assetRef));
			return;
		}
	}

	// Token: 0x060068CF RID: 26831 RVA: 0x0022276C File Offset: 0x0022096C
	private void OnTavernBrawlLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_tavernBrawlPrefabLoaded = true;
		if (go == null)
		{
			Debug.LogError(string.Format("TavernBrawlScene.OnTavernBrawlLoaded() - failed to load screen {0}", assetRef));
			return;
		}
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.FIRESIDE_GATHERING)
		{
			go.transform.position = Vector3.zero;
		}
	}

	// Token: 0x060068D0 RID: 26832 RVA: 0x002227B8 File Offset: 0x002209B8
	private void OnSessionBeginResponse()
	{
		this.m_pendingSessionBegin = false;
	}

	// Token: 0x060068D1 RID: 26833 RVA: 0x002227C4 File Offset: 0x002209C4
	private void OnTavernBrawlTicketPurchaseAck(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		Log.TavernBrawl.Print("TavernBrawlScene.OnTavernBrawlTicketPurchaseAck", Array.Empty<object>());
		if (bundle == null || bundle.Items == null)
		{
			return;
		}
		foreach (Network.BundleItem bundleItem in bundle.Items)
		{
			if ((bundleItem != null && bundleItem.ItemType == ProductType.PRODUCT_TYPE_TAVERN_BRAWL_TICKET && SceneMgr.Get().IsModeRequested(SceneMgr.Mode.TAVERN_BRAWL)) || SceneMgr.Get().IsModeRequested(SceneMgr.Mode.FIRESIDE_GATHERING))
			{
				TavernBrawlManager.Get().RequestSessionBegin();
				return;
			}
		}
		Log.TavernBrawl.PrintError("TavernBrawlScene.OnTavernBrawlTicketPurchaseAck ERROR: Got a purchase ack in the Tavern Brawl scene for a product we don't recognize", Array.Empty<object>());
	}

	// Token: 0x040055D9 RID: 21977
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_CollectionManagerPrefab;

	// Token: 0x040055DA RID: 21978
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_TavernBrawlPrefab;

	// Token: 0x040055DB RID: 21979
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_TavernBrawlNoDeckPrefab;

	// Token: 0x040055DC RID: 21980
	private bool m_unloading;

	// Token: 0x040055DD RID: 21981
	private bool m_tavernBrawlPrefabLoaded;

	// Token: 0x040055DE RID: 21982
	private bool m_collectionManagerNeeded;

	// Token: 0x040055DF RID: 21983
	private bool m_collectionManagerPrefabLoaded;

	// Token: 0x040055E0 RID: 21984
	private bool m_pendingSessionBegin;
}
