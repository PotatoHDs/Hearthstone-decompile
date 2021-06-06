using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Attribution;
using Hearthstone.CRM;
using PegasusShared;
using UnityEngine;

// Token: 0x0200074B RID: 1867
public class AdTrackingManager : IService
{
	// Token: 0x06006983 RID: 27011 RVA: 0x00226744 File Offset: 0x00224944
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		NetCache netCache = serviceLocator.Get<NetCache>();
		try
		{
			GameState.RegisterGameStateInitializedListener(new GameState.GameStateInitializedCallback(this.HandleGameCreated), null);
			netCache.RegisterUpdatedListener(typeof(NetCache.NetCacheGoldBalance), new Action(this.TrackGoldBalanceChanges));
			netCache.RegisterUpdatedListener(typeof(NetCache.NetCacheArcaneDustBalance), new Action(this.TrackDustBalanceChanges));
			serviceLocator.Get<LoginManager>().OnFullLoginFlowComplete += delegate()
			{
				this.m_isLoginFlowCompleted = true;
			};
			HearthstoneApplication.Get().WillReset += delegate()
			{
				this.m_isLoginFlowCompleted = false;
			};
			StoreManager.Get().RegisterSuccessfulPurchaseListener(new Action<Network.Bundle, PaymentMethod>(this.HandleItemPurchase));
			yield break;
		}
		catch (Exception message)
		{
			Debug.LogError(message);
			yield break;
		}
		yield break;
	}

	// Token: 0x06006984 RID: 27012 RVA: 0x0022675A File Offset: 0x0022495A
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(NetCache),
			typeof(LoginManager)
		};
	}

	// Token: 0x06006985 RID: 27013 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x06006986 RID: 27014 RVA: 0x0022677C File Offset: 0x0022497C
	public static AdTrackingManager Get()
	{
		return HearthstoneServices.Get<AdTrackingManager>();
	}

	// Token: 0x06006987 RID: 27015 RVA: 0x00226783 File Offset: 0x00224983
	public void TrackLogin()
	{
		BlizzardAttributionManager.Get().SendEvent_Login();
		BlizzardCRMManager.Get().SendEvent_SessionStart(null);
	}

	// Token: 0x06006988 RID: 27016 RVA: 0x0022679A File Offset: 0x0022499A
	public void TrackFirstLogin()
	{
		BlizzardAttributionManager.Get().SendEvent_FirstLogin();
	}

	// Token: 0x06006989 RID: 27017 RVA: 0x002267A6 File Offset: 0x002249A6
	public void TrackAccountCreated()
	{
		BlizzardAttributionManager.Get().SendEvent_Registration();
	}

	// Token: 0x0600698A RID: 27018 RVA: 0x002267B2 File Offset: 0x002249B2
	public void TrackHeadlessAccountCreated()
	{
		BlizzardAttributionManager.Get().SendEvent_HeadlessAccountCreated();
	}

	// Token: 0x0600698B RID: 27019 RVA: 0x002267BE File Offset: 0x002249BE
	public void TrackHeadlessAccountHealedUp(string temporaryGameAccountId)
	{
		BlizzardAttributionManager.Get().SendEvent_HeadlessAccountHealedUp(temporaryGameAccountId);
	}

	// Token: 0x0600698C RID: 27020 RVA: 0x002267CC File Offset: 0x002249CC
	public void TrackAdventureProgress(string description)
	{
		Log.AdTracking.Print("Adventure Progress=" + description, Array.Empty<object>());
		string contentId = string.Format("Adventure_{0}", description);
		BlizzardAttributionManager.Get().SendEvent_ContentUnlocked(contentId);
	}

	// Token: 0x0600698D RID: 27021 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void TrackTutorialProgress(TutorialProgress description, bool isVictory = true)
	{
	}

	// Token: 0x0600698E RID: 27022 RVA: 0x0022680A File Offset: 0x00224A0A
	public void TrackSale(double price, string currencyCode, string productId, string transactionId)
	{
		BlizzardAttributionManager.Get().SendEvent_Purchase(productId, transactionId, 1, currencyCode, false, (float)price);
		BlizzardCRMManager.Get().SendEvent_RealMoneyTransaction(productId, transactionId, 1, currencyCode, (float)price);
	}

	// Token: 0x0600698F RID: 27023 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void TrackCreditsLaunch()
	{
	}

	// Token: 0x06006990 RID: 27024 RVA: 0x00226830 File Offset: 0x00224A30
	private void TrackGoldBalanceChanges()
	{
		NetCache.NetCacheGoldBalance balanceObject = NetCache.Get().GetNetObject<NetCache.NetCacheGoldBalance>();
		if (balanceObject != null)
		{
			this.TrackGenericBalanceChanges("gold", ref AdTrackingManager.s_lastTrackedGoldBalanceThisSession, () => balanceObject.GetTotal());
		}
	}

	// Token: 0x06006991 RID: 27025 RVA: 0x00226878 File Offset: 0x00224A78
	private void TrackDustBalanceChanges()
	{
		NetCache.NetCacheArcaneDustBalance balanceObject = NetCache.Get().GetNetObject<NetCache.NetCacheArcaneDustBalance>();
		if (balanceObject != null)
		{
			this.TrackGenericBalanceChanges("dust", ref AdTrackingManager.s_lastTrackedDustBalanceThisSession, () => balanceObject.Balance);
		}
	}

	// Token: 0x06006992 RID: 27026 RVA: 0x002268C0 File Offset: 0x00224AC0
	private void TrackGenericBalanceChanges(string currencyName, ref long lastTrackedBalance, Func<long> balanceGetter)
	{
		long num = balanceGetter();
		if (!this.m_isLoginFlowCompleted)
		{
			lastTrackedBalance = num;
			return;
		}
		long num2 = num - lastTrackedBalance;
		if (num2 != 0L)
		{
			BlizzardAttributionManager.Get().SendEvent_VirtualCurrencyTransaction((int)num2, currencyName);
		}
		lastTrackedBalance = num;
	}

	// Token: 0x06006993 RID: 27027 RVA: 0x002268F8 File Offset: 0x00224AF8
	private void HandleItemPurchase(Network.Bundle bundle, PaymentMethod purchaseMethod)
	{
		StorePackId currentlySelectedId = StoreManager.Get().CurrentlySelectedId;
		if (currentlySelectedId.Type == StorePackType.BOOSTER && purchaseMethod != PaymentMethod.MONEY)
		{
			BlizzardAttributionManager.Get().SendEvent_Purchase(currentlySelectedId.Id.ToString(), "", 1, "gold", true, 100f);
			BlizzardCRMManager.Get().SendEvent_VirtualCurrencyTransaction(currentlySelectedId.Id.ToString(), 100, 1, "gold", null);
		}
	}

	// Token: 0x06006994 RID: 27028 RVA: 0x00226964 File Offset: 0x00224B64
	private void HandleGameCreated(GameState instance, object userData)
	{
		try
		{
			instance.RegisterGameOverListener(new GameState.GameOverCallback(this.HandleGameEnded), null);
			FormatType formatType = GameMgr.Get().GetFormatType();
			BlizzardAttributionManager.Get().SendEvent_GameRoundStart(GameMgr.Get().GetGameType().ToString(), formatType);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	// Token: 0x06006995 RID: 27029 RVA: 0x002269CC File Offset: 0x00224BCC
	private void HandleGameEnded(TAG_PLAYSTATE playState, object userData)
	{
		try
		{
			GameState gameState = GameState.Get();
			if (GameMgr.Get().IsAI())
			{
				int bossId = 0;
				Player opposingSidePlayer = gameState.GetOpposingSidePlayer();
				if (opposingSidePlayer != null)
				{
					Card heroCard = opposingSidePlayer.GetHeroCard();
					if (heroCard != null && heroCard.GetEntity() != null)
					{
						bossId = GameUtils.TranslateCardIdToDbId(heroCard.GetEntity().GetCardId(), false);
					}
				}
				BlizzardAttributionManager.Get().SendEvent_ScenarioResult(GameMgr.Get().GetMissionId(), playState.ToString(), bossId);
			}
			else
			{
				FormatType formatType = GameMgr.Get().GetFormatType();
				BlizzardAttributionManager.Get().SendEvent_GameRoundEnd(GameMgr.Get().GetGameType().ToString(), playState.ToString(), formatType);
			}
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	// Token: 0x04005660 RID: 22112
	private static long s_lastTrackedGoldBalanceThisSession;

	// Token: 0x04005661 RID: 22113
	private static long s_lastTrackedDustBalanceThisSession;

	// Token: 0x04005662 RID: 22114
	private bool m_isLoginFlowCompleted;
}
