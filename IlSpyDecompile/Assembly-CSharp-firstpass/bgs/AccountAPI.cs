using System.Collections.Generic;
using bgs.RPCServices;
using bgs.types;
using bnet.protocol;
using bnet.protocol.account.v1;

namespace bgs
{
	public class AccountAPI : BattleNetAPI
	{
		public class GameSessionInfo
		{
			public ulong SessionStartTime;
		}

		public class CAISInfo
		{
			public bool CAISactive;

			public int CAISplayed;

			public int CAISrested;
		}

		private class GetGameSessionInfoRequestContext
		{
			private AccountAPI m_parent;

			public GetGameSessionInfoRequestContext(AccountAPI parent)
			{
				m_parent = parent;
			}

			public void GetGameSessionInfoRequestContextCallback(RPCContext context)
			{
				m_parent.GetPlayerRestrictionAPICount--;
				if (context == null || context.Payload == null)
				{
					m_parent.ApiLog.LogWarning("GetPlayRestrictions:GetGameSessionInfo invalid context!");
					return;
				}
				BattleNetErrors status = (BattleNetErrors)context.Header.Status;
				if (status != 0)
				{
					m_parent.ApiLog.LogError("GetPlayRestrictions:GetGameSessionInfo failed with error={0}", status.ToString());
					return;
				}
				GetGameSessionInfoResponse getGameSessionInfoResponse = GetGameSessionInfoResponse.ParseFrom(context.Payload);
				if (getGameSessionInfoResponse == null || !getGameSessionInfoResponse.IsInitialized)
				{
					m_parent.ApiLog.LogWarning("GetPlayRestrictions:GetGameSessionInfo unable to parse response!");
					return;
				}
				if (!getGameSessionInfoResponse.HasSessionInfo)
				{
					m_parent.ApiLog.LogWarning("GetPlayRestrictions:GetGameSessionInfo response has no data!");
					return;
				}
				m_parent.LastGameSessionInfo = new GameSessionInfo();
				if (getGameSessionInfoResponse.SessionInfo.HasStartTime)
				{
					m_parent.LastGameSessionInfo.SessionStartTime = getGameSessionInfoResponse.SessionInfo.StartTime;
				}
				else
				{
					m_parent.ApiLog.LogWarning("GetPlayRestrictions:GetGameSessionInfo response has no HasStartTime!");
				}
			}
		}

		private class GetCAISInfoRequestContext
		{
			private AccountAPI m_parent;

			public GetCAISInfoRequestContext(AccountAPI parent)
			{
				m_parent = parent;
			}

			public void GetCAISInfoRequestContextCallback(RPCContext context)
			{
				m_parent.GetPlayerRestrictionAPICount--;
				if (context == null || context.Payload == null)
				{
					m_parent.ApiLog.LogWarning("GetPlayRestrictions:GetCAISInfo invalid context!");
					return;
				}
				BattleNetErrors status = (BattleNetErrors)context.Header.Status;
				if (status != 0)
				{
					m_parent.ApiLog.LogError("GetPlayRestrictions:GetCAISInfo failed with error={0}", status.ToString());
					return;
				}
				GetCAISInfoResponse getCAISInfoResponse = GetCAISInfoResponse.ParseFrom(context.Payload);
				if (getCAISInfoResponse == null || !getCAISInfoResponse.IsInitialized)
				{
					m_parent.ApiLog.LogWarning("GetPlayRestrictions:GetCAISInfo unable to parse response!");
					return;
				}
				m_parent.LastCAISInfo = new CAISInfo();
				if (getCAISInfoResponse.HasCaisInfo)
				{
					m_parent.LastCAISInfo.CAISactive = getCAISInfoResponse.CaisInfo.HasPlayedMinutes || getCAISInfoResponse.CaisInfo.HasRestedMinutes;
					m_parent.LastCAISInfo.CAISplayed = (int)(getCAISInfoResponse.CaisInfo.HasPlayedMinutes ? getCAISInfoResponse.CaisInfo.PlayedMinutes : 0);
					m_parent.LastCAISInfo.CAISrested = (int)(getCAISInfoResponse.CaisInfo.HasRestedMinutes ? getCAISInfoResponse.CaisInfo.RestedMinutes : 0);
				}
				m_parent.ApiLog.LogDebug("GetCAISInfo hasCaisInfo={0} played={1} rested={2}", getCAISInfoResponse.HasCaisInfo, (getCAISInfoResponse.HasCaisInfo && getCAISInfoResponse.CaisInfo.HasPlayedMinutes) ? getCAISInfoResponse.CaisInfo.PlayedMinutes.ToString() : "", (getCAISInfoResponse.HasCaisInfo && getCAISInfoResponse.CaisInfo.HasRestedMinutes) ? getCAISInfoResponse.CaisInfo.RestedMinutes.ToString() : "");
			}
		}

		private ServiceDescriptor m_accountService = new AccountService();

		private ServiceDescriptor m_accountNotify = new AccountNotify();

		private uint m_preferredRegion = uint.MaxValue;

		private string m_accountCountry;

		private bool m_headlessAccount;

		private List<AccountLicense> m_licenses = new List<AccountLicense>();

		public GameSessionInfo LastGameSessionInfo { get; set; }

		public CAISInfo LastCAISInfo { get; set; }

		public int GetPlayerRestrictionAPICount { get; set; }

		public ServiceDescriptor AccountService => m_accountService;

		public ServiceDescriptor AccountNotifyService => m_accountNotify;

		public bool HasLicenses
		{
			get
			{
				if (m_licenses == null)
				{
					return false;
				}
				return m_licenses.Count > 0;
			}
		}

		public AccountAPI(BattleNetCSharp battlenet)
			: base(battlenet, "Account")
		{
		}

		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
			m_rpcConnection.RegisterServiceMethodListener(m_accountNotify.Id, 1u, HandleAccountNotify_AccountStateUpdated);
			m_rpcConnection.RegisterServiceMethodListener(m_accountNotify.Id, 2u, HandleAccountNotify_GameAccountStateUpdated);
			m_rpcConnection.RegisterServiceMethodListener(m_accountNotify.Id, 3u, HandleAccountNotify_GameAccountsUpdated);
			m_rpcConnection.RegisterServiceMethodListener(m_accountNotify.Id, 4u, HandleAccountNotify_GameSessionUpdated);
		}

		public override void Initialize()
		{
			base.ApiLog.LogDebug("Account API initializing");
			base.Initialize();
			GetAccountLevelInfo(m_battleNet.AccountId);
		}

		public override void OnDisconnected()
		{
			base.OnDisconnected();
		}

		public uint GetPreferredRegion()
		{
			return m_preferredRegion;
		}

		public string GetAccountCountry()
		{
			return m_accountCountry;
		}

		public bool IsHeadlessAccount()
		{
			return m_headlessAccount;
		}

		public bool CheckLicense(uint licenseId)
		{
			if (m_licenses == null || m_licenses.Count == 0)
			{
				return false;
			}
			foreach (AccountLicense license in m_licenses)
			{
				if (license.Id == licenseId)
				{
					return true;
				}
			}
			return false;
		}

		public void GetPlayRestrictions(ref Lockouts restrictions, bool reload)
		{
			if (reload)
			{
				restrictions.loaded = false;
			}
			else
			{
				if (LastGameSessionInfo != null && LastCAISInfo != null)
				{
					restrictions.loaded = true;
					restrictions.sessionStartTime = LastGameSessionInfo.SessionStartTime;
					restrictions.CAISactive = LastCAISInfo.CAISactive;
					restrictions.CAISplayed = LastCAISInfo.CAISplayed;
					restrictions.CAISrested = LastCAISInfo.CAISrested;
					return;
				}
				if (GetPlayerRestrictionAPICount > 0)
				{
					return;
				}
			}
			if (restrictions.loading)
			{
				if (GetPlayerRestrictionAPICount <= 0)
				{
					GetPlayerRestrictionAPICount = 0;
					restrictions.loading = false;
					restrictions.loaded = true;
				}
			}
			else
			{
				restrictions.loading = true;
				LastGameSessionInfo = null;
				GetPlayerRestrictionAPICount++;
				GetGameSessionInfo();
				LastCAISInfo = null;
				GetPlayerRestrictionAPICount++;
				GetCAISInfo();
			}
		}

		private void GetGameSessionInfo()
		{
			GetGameSessionInfoRequest getGameSessionInfoRequest = new GetGameSessionInfoRequest();
			getGameSessionInfoRequest.SetEntityId(m_battleNet.GameAccountId);
			GetGameSessionInfoRequestContext @object = new GetGameSessionInfoRequestContext(this);
			m_rpcConnection.QueueRequest(m_accountService, 34u, getGameSessionInfoRequest, @object.GetGameSessionInfoRequestContextCallback);
		}

		private void GetCAISInfo()
		{
			GetGameSessionInfoRequest getGameSessionInfoRequest = new GetGameSessionInfoRequest();
			getGameSessionInfoRequest.SetEntityId(m_battleNet.AccountId);
			GetCAISInfoRequestContext @object = new GetCAISInfoRequestContext(this);
			m_rpcConnection.QueueRequest(m_accountService, 35u, getGameSessionInfoRequest, @object.GetCAISInfoRequestContextCallback);
		}

		public void GetAccountState(BnetAccountId accountId, RPCContextDelegate callback = null)
		{
			GetAccountStateRequest getAccountStateRequest = new GetAccountStateRequest();
			getAccountStateRequest.SetEntityId(BnetEntityId.CreateForProtocol(accountId));
			AccountFieldOptions accountFieldOptions = new AccountFieldOptions();
			accountFieldOptions.SetAllFields(val: true);
			getAccountStateRequest.SetOptions(accountFieldOptions);
			m_rpcConnection.QueueRequest(m_accountService, 30u, getAccountStateRequest, delegate(RPCContext ctx)
			{
				GetAccountStateCallback(ctx, callback);
			});
		}

		private void GetAccountStateCallback(RPCContext context, RPCContextDelegate callback)
		{
			if (context == null || context.Payload == null)
			{
				base.ApiLog.LogWarning("GetAccountLevelInfo invalid context!");
				return;
			}
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != 0)
			{
				base.ApiLog.LogError("GetAccountLevelInfo failed with error={0}", status.ToString());
				return;
			}
			GetAccountStateResponse getAccountStateResponse = GetAccountStateResponse.ParseFrom(context.Payload);
			if (getAccountStateResponse == null || !getAccountStateResponse.IsInitialized)
			{
				base.ApiLog.LogWarning("GetAccountStateCallback unable to parse response!");
			}
			else if (!getAccountStateResponse.HasState || !getAccountStateResponse.State.HasAccountLevelInfo)
			{
				base.ApiLog.LogWarning("GetAccountStateCallback response has no data!");
			}
			else
			{
				callback?.Invoke(context);
			}
		}

		private void GetAccountLevelInfo(bnet.protocol.EntityId accountId)
		{
			GetAccountStateRequest getAccountStateRequest = new GetAccountStateRequest();
			getAccountStateRequest.SetEntityId(accountId);
			AccountFieldOptions accountFieldOptions = new AccountFieldOptions();
			accountFieldOptions.SetFieldAccountLevelInfo(val: true);
			getAccountStateRequest.SetOptions(accountFieldOptions);
			m_rpcConnection.QueueRequest(m_accountService, 30u, getAccountStateRequest, GetAccountLevelInfoCallback);
		}

		private void GetAccountLevelInfoCallback(RPCContext context)
		{
			if (context == null || context.Payload == null)
			{
				base.ApiLog.LogWarning("GetAccountLevelInfo invalid context!");
				return;
			}
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != 0)
			{
				base.ApiLog.LogError("GetAccountLevelInfo failed with error={0}", status.ToString());
				return;
			}
			GetAccountStateResponse getAccountStateResponse = GetAccountStateResponse.ParseFrom(context.Payload);
			if (getAccountStateResponse == null || !getAccountStateResponse.IsInitialized)
			{
				base.ApiLog.LogWarning("GetAccountStateCallback unable to parse response!");
				return;
			}
			if (!getAccountStateResponse.HasState || !getAccountStateResponse.State.HasAccountLevelInfo)
			{
				base.ApiLog.LogWarning("GetAccountStateCallback response has no data!");
				return;
			}
			GetAccountStateRequest getAccountStateRequest = (GetAccountStateRequest)context.Request;
			if (getAccountStateRequest != null && getAccountStateRequest.EntityId == m_battleNet.AccountId)
			{
				AccountLevelInfo accountLevelInfo = getAccountStateResponse.State.AccountLevelInfo;
				m_preferredRegion = accountLevelInfo.PreferredRegion;
				m_accountCountry = accountLevelInfo.Country;
				m_headlessAccount = accountLevelInfo.HeadlessAccount;
				base.ApiLog.LogDebug("Region (preferred): {0}", m_preferredRegion);
				base.ApiLog.LogDebug("Country (account): {0}", m_accountCountry);
				base.ApiLog.LogDebug("Headless (account): {0}", m_headlessAccount);
				if (accountLevelInfo.LicensesList.Count > 0)
				{
					m_licenses.Clear();
					base.ApiLog.LogDebug("Found {0} licenses.", accountLevelInfo.LicensesList.Count);
					for (int i = 0; i < accountLevelInfo.LicensesList.Count; i++)
					{
						AccountLicense accountLicense = accountLevelInfo.LicensesList[i];
						m_licenses.Add(accountLicense);
						base.ApiLog.LogDebug("Adding license id={0}", accountLicense.Id);
					}
				}
				else
				{
					base.ApiLog.LogWarning("No licenses found!");
				}
			}
			base.ApiLog.LogDebug("GetAccountLevelInfo, status=" + status);
		}

		private void SubscribeToAccountService()
		{
			SubscriptionUpdateRequest subscriptionUpdateRequest = new SubscriptionUpdateRequest();
			SubscriberReference subscriberReference = new SubscriberReference();
			subscriberReference.SetEntityId(m_battleNet.AccountId);
			subscriberReference.SetObjectId(0uL);
			AccountFieldOptions accountFieldOptions = new AccountFieldOptions();
			accountFieldOptions.SetAllFields(val: true);
			subscriberReference.SetAccountOptions(accountFieldOptions);
			subscriptionUpdateRequest.AddRef(subscriberReference);
			subscriberReference = new SubscriberReference();
			subscriberReference.SetEntityId(m_battleNet.GameAccountId);
			subscriberReference.SetObjectId(0uL);
			new GameAccountFieldOptions().SetAllFields(val: true);
			subscriptionUpdateRequest.AddRef(subscriberReference);
			m_rpcConnection.QueueRequest(m_accountService, 25u, subscriptionUpdateRequest, SubscribeToAccountServiceCallback);
		}

		private void SubscribeToAccountServiceCallback(RPCContext context)
		{
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != 0)
			{
				base.ApiLog.LogError("SubscribeToAccountServiceCallback: " + status);
			}
			else
			{
				base.ApiLog.LogDebug("SubscribeToAccountServiceCallback, status=" + status);
			}
		}

		private void HandleAccountNotify_AccountStateUpdated(RPCContext context)
		{
			if (context == null || context.Payload == null)
			{
				base.ApiLog.LogWarning("HandleAccountNotify_AccountStateUpdated invalid context!");
				return;
			}
			AccountStateNotification accountStateNotification = AccountStateNotification.ParseFrom(context.Payload);
			if (accountStateNotification == null || !accountStateNotification.IsInitialized)
			{
				base.ApiLog.LogWarning("HandleAccountNotify_AccountStateUpdated unable to parse response!");
				return;
			}
			if (!accountStateNotification.HasAccountState)
			{
				base.ApiLog.LogDebug("HandleAccountNotify_AccountStateUpdated HasState=false, data={0}", accountStateNotification);
				return;
			}
			AccountState accountState = accountStateNotification.AccountState;
			if (!accountState.HasAccountLevelInfo)
			{
				base.ApiLog.LogDebug("HandleAccountNotify_AccountStateUpdated HasAccountLevelInfo=false, data={0}", accountStateNotification);
			}
			else if (!accountState.AccountLevelInfo.HasPreferredRegion)
			{
				base.ApiLog.LogDebug("HandleAccountNotify_AccountStateUpdated HasPreferredRegion=false, data={0}", accountStateNotification);
			}
			else
			{
				base.ApiLog.LogDebug("HandleAccountNotify_AccountStateUpdated, data={0}", accountStateNotification);
			}
		}

		private void HandleAccountNotify_GameAccountStateUpdated(RPCContext context)
		{
			GameAccountStateNotification gameAccountStateNotification = GameAccountStateNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleAccountNotify_GameAccountStateUpdated, data=" + gameAccountStateNotification);
		}

		private void HandleAccountNotify_GameAccountsUpdated(RPCContext context)
		{
			GameAccountNotification gameAccountNotification = GameAccountNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleAccountNotify_GameAccountsUpdated, data=" + gameAccountNotification);
		}

		private void HandleAccountNotify_GameSessionUpdated(RPCContext context)
		{
			GameAccountSessionNotification gameAccountSessionNotification = GameAccountSessionNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleAccountNotify_GameSessionUpdated, data=" + gameAccountSessionNotification);
		}
	}
}
