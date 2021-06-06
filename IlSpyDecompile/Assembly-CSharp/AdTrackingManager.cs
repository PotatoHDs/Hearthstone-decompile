using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Attribution;
using Hearthstone.CRM;
using PegasusShared;
using UnityEngine;

public class AdTrackingManager : IService
{
	private static long s_lastTrackedGoldBalanceThisSession;

	private static long s_lastTrackedDustBalanceThisSession;

	private bool m_isLoginFlowCompleted;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		NetCache netCache = serviceLocator.Get<NetCache>();
		try
		{
			GameState.RegisterGameStateInitializedListener(HandleGameCreated);
			netCache.RegisterUpdatedListener(typeof(NetCache.NetCacheGoldBalance), TrackGoldBalanceChanges);
			netCache.RegisterUpdatedListener(typeof(NetCache.NetCacheArcaneDustBalance), TrackDustBalanceChanges);
			serviceLocator.Get<LoginManager>().OnFullLoginFlowComplete += delegate
			{
				m_isLoginFlowCompleted = true;
			};
			HearthstoneApplication.Get().WillReset += delegate
			{
				m_isLoginFlowCompleted = false;
			};
			StoreManager.Get().RegisterSuccessfulPurchaseListener(HandleItemPurchase);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[2]
		{
			typeof(NetCache),
			typeof(LoginManager)
		};
	}

	public void Shutdown()
	{
	}

	public static AdTrackingManager Get()
	{
		return HearthstoneServices.Get<AdTrackingManager>();
	}

	public void TrackLogin()
	{
		BlizzardAttributionManager.Get().SendEvent_Login();
		BlizzardCRMManager.Get().SendEvent_SessionStart(null);
	}

	public void TrackFirstLogin()
	{
		BlizzardAttributionManager.Get().SendEvent_FirstLogin();
	}

	public void TrackAccountCreated()
	{
		BlizzardAttributionManager.Get().SendEvent_Registration();
	}

	public void TrackHeadlessAccountCreated()
	{
		BlizzardAttributionManager.Get().SendEvent_HeadlessAccountCreated();
	}

	public void TrackHeadlessAccountHealedUp(string temporaryGameAccountId)
	{
		BlizzardAttributionManager.Get().SendEvent_HeadlessAccountHealedUp(temporaryGameAccountId);
	}

	public void TrackAdventureProgress(string description)
	{
		Log.AdTracking.Print("Adventure Progress=" + description);
		string contentId = $"Adventure_{description}";
		BlizzardAttributionManager.Get().SendEvent_ContentUnlocked(contentId);
	}

	public void TrackTutorialProgress(TutorialProgress description, bool isVictory = true)
	{
	}

	public void TrackSale(double price, string currencyCode, string productId, string transactionId)
	{
		BlizzardAttributionManager.Get().SendEvent_Purchase(productId, transactionId, 1, currencyCode, isVirtualCurrency: false, (float)price);
		BlizzardCRMManager.Get().SendEvent_RealMoneyTransaction(productId, transactionId, 1, currencyCode, (float)price);
	}

	public void TrackCreditsLaunch()
	{
	}

	private void TrackGoldBalanceChanges()
	{
		NetCache.NetCacheGoldBalance balanceObject = NetCache.Get().GetNetObject<NetCache.NetCacheGoldBalance>();
		if (balanceObject != null)
		{
			TrackGenericBalanceChanges("gold", ref s_lastTrackedGoldBalanceThisSession, () => balanceObject.GetTotal());
		}
	}

	private void TrackDustBalanceChanges()
	{
		NetCache.NetCacheArcaneDustBalance balanceObject = NetCache.Get().GetNetObject<NetCache.NetCacheArcaneDustBalance>();
		if (balanceObject != null)
		{
			TrackGenericBalanceChanges("dust", ref s_lastTrackedDustBalanceThisSession, () => balanceObject.Balance);
		}
	}

	private void TrackGenericBalanceChanges(string currencyName, ref long lastTrackedBalance, Func<long> balanceGetter)
	{
		long num = balanceGetter();
		if (!m_isLoginFlowCompleted)
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

	private void HandleItemPurchase(Network.Bundle bundle, PaymentMethod purchaseMethod)
	{
		StorePackId currentlySelectedId = StoreManager.Get().CurrentlySelectedId;
		if (currentlySelectedId.Type == StorePackType.BOOSTER && purchaseMethod != 0)
		{
			BlizzardAttributionManager.Get().SendEvent_Purchase(currentlySelectedId.Id.ToString(), "", 1, "gold", isVirtualCurrency: true, 100f);
			BlizzardCRMManager.Get().SendEvent_VirtualCurrencyTransaction(currentlySelectedId.Id.ToString(), 100, 1, "gold", null);
		}
	}

	private void HandleGameCreated(GameState instance, object userData)
	{
		try
		{
			instance.RegisterGameOverListener(HandleGameEnded);
			FormatType formatType = GameMgr.Get().GetFormatType();
			BlizzardAttributionManager.Get().SendEvent_GameRoundStart(GameMgr.Get().GetGameType().ToString(), formatType);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

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
						bossId = GameUtils.TranslateCardIdToDbId(heroCard.GetEntity().GetCardId());
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
}
