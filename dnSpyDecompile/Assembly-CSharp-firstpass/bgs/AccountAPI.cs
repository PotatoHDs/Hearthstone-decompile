using System;
using System.Collections.Generic;
using bgs.RPCServices;
using bgs.types;
using bnet.protocol;
using bnet.protocol.account.v1;

namespace bgs
{
	// Token: 0x020001F9 RID: 505
	public class AccountAPI : BattleNetAPI
	{
		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001EE5 RID: 7909 RVA: 0x0006BE71 File Offset: 0x0006A071
		// (set) Token: 0x06001EE6 RID: 7910 RVA: 0x0006BE79 File Offset: 0x0006A079
		public AccountAPI.GameSessionInfo LastGameSessionInfo { get; set; }

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x0006BE82 File Offset: 0x0006A082
		// (set) Token: 0x06001EE8 RID: 7912 RVA: 0x0006BE8A File Offset: 0x0006A08A
		public AccountAPI.CAISInfo LastCAISInfo { get; set; }

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x0006BE93 File Offset: 0x0006A093
		// (set) Token: 0x06001EEA RID: 7914 RVA: 0x0006BE9B File Offset: 0x0006A09B
		public int GetPlayerRestrictionAPICount { get; set; }

		// Token: 0x06001EEB RID: 7915 RVA: 0x0006BEA4 File Offset: 0x0006A0A4
		public AccountAPI(BattleNetCSharp battlenet) : base(battlenet, "Account")
		{
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001EEC RID: 7916 RVA: 0x0006BEDA File Offset: 0x0006A0DA
		public ServiceDescriptor AccountService
		{
			get
			{
				return this.m_accountService;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001EED RID: 7917 RVA: 0x0006BEE2 File Offset: 0x0006A0E2
		public ServiceDescriptor AccountNotifyService
		{
			get
			{
				return this.m_accountNotify;
			}
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x0006BEEC File Offset: 0x0006A0EC
		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_accountNotify.Id, 1U, new RPCContextDelegate(this.HandleAccountNotify_AccountStateUpdated));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_accountNotify.Id, 2U, new RPCContextDelegate(this.HandleAccountNotify_GameAccountStateUpdated));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_accountNotify.Id, 3U, new RPCContextDelegate(this.HandleAccountNotify_GameAccountsUpdated));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_accountNotify.Id, 4U, new RPCContextDelegate(this.HandleAccountNotify_GameSessionUpdated));
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x0006BF8C File Offset: 0x0006A18C
		public override void Initialize()
		{
			base.ApiLog.LogDebug("Account API initializing");
			base.Initialize();
			this.GetAccountLevelInfo(this.m_battleNet.AccountId);
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x0006BFB5 File Offset: 0x0006A1B5
		public override void OnDisconnected()
		{
			base.OnDisconnected();
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x0006BFBD File Offset: 0x0006A1BD
		public uint GetPreferredRegion()
		{
			return this.m_preferredRegion;
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x0006BFC5 File Offset: 0x0006A1C5
		public string GetAccountCountry()
		{
			return this.m_accountCountry;
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x0006BFCD File Offset: 0x0006A1CD
		public bool IsHeadlessAccount()
		{
			return this.m_headlessAccount;
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x0006BFD8 File Offset: 0x0006A1D8
		public bool CheckLicense(uint licenseId)
		{
			if (this.m_licenses == null || this.m_licenses.Count == 0)
			{
				return false;
			}
			using (List<AccountLicense>.Enumerator enumerator = this.m_licenses.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Id == licenseId)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001EF5 RID: 7925 RVA: 0x0006C04C File Offset: 0x0006A24C
		public bool HasLicenses
		{
			get
			{
				return this.m_licenses != null && this.m_licenses.Count > 0;
			}
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x0006C068 File Offset: 0x0006A268
		public void GetPlayRestrictions(ref Lockouts restrictions, bool reload)
		{
			if (reload)
			{
				restrictions.loaded = false;
			}
			else
			{
				if (this.LastGameSessionInfo != null && this.LastCAISInfo != null)
				{
					restrictions.loaded = true;
					restrictions.sessionStartTime = this.LastGameSessionInfo.SessionStartTime;
					restrictions.CAISactive = this.LastCAISInfo.CAISactive;
					restrictions.CAISplayed = this.LastCAISInfo.CAISplayed;
					restrictions.CAISrested = this.LastCAISInfo.CAISrested;
					return;
				}
				if (this.GetPlayerRestrictionAPICount > 0)
				{
					return;
				}
			}
			if (restrictions.loading)
			{
				if (this.GetPlayerRestrictionAPICount <= 0)
				{
					this.GetPlayerRestrictionAPICount = 0;
					restrictions.loading = false;
					restrictions.loaded = true;
				}
				return;
			}
			restrictions.loading = true;
			this.LastGameSessionInfo = null;
			int getPlayerRestrictionAPICount = this.GetPlayerRestrictionAPICount + 1;
			this.GetPlayerRestrictionAPICount = getPlayerRestrictionAPICount;
			this.GetGameSessionInfo();
			this.LastCAISInfo = null;
			getPlayerRestrictionAPICount = this.GetPlayerRestrictionAPICount + 1;
			this.GetPlayerRestrictionAPICount = getPlayerRestrictionAPICount;
			this.GetCAISInfo();
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x0006C150 File Offset: 0x0006A350
		private void GetGameSessionInfo()
		{
			GetGameSessionInfoRequest getGameSessionInfoRequest = new GetGameSessionInfoRequest();
			getGameSessionInfoRequest.SetEntityId(this.m_battleNet.GameAccountId);
			AccountAPI.GetGameSessionInfoRequestContext @object = new AccountAPI.GetGameSessionInfoRequestContext(this);
			this.m_rpcConnection.QueueRequest(this.m_accountService, 34U, getGameSessionInfoRequest, new RPCContextDelegate(@object.GetGameSessionInfoRequestContextCallback), 0U);
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x0006C1A0 File Offset: 0x0006A3A0
		private void GetCAISInfo()
		{
			GetGameSessionInfoRequest getGameSessionInfoRequest = new GetGameSessionInfoRequest();
			getGameSessionInfoRequest.SetEntityId(this.m_battleNet.AccountId);
			AccountAPI.GetCAISInfoRequestContext @object = new AccountAPI.GetCAISInfoRequestContext(this);
			this.m_rpcConnection.QueueRequest(this.m_accountService, 35U, getGameSessionInfoRequest, new RPCContextDelegate(@object.GetCAISInfoRequestContextCallback), 0U);
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x0006C1F0 File Offset: 0x0006A3F0
		public void GetAccountState(BnetAccountId accountId, RPCContextDelegate callback = null)
		{
			GetAccountStateRequest getAccountStateRequest = new GetAccountStateRequest();
			getAccountStateRequest.SetEntityId(BnetEntityId.CreateForProtocol(accountId));
			AccountFieldOptions accountFieldOptions = new AccountFieldOptions();
			accountFieldOptions.SetAllFields(true);
			getAccountStateRequest.SetOptions(accountFieldOptions);
			this.m_rpcConnection.QueueRequest(this.m_accountService, 30U, getAccountStateRequest, delegate(RPCContext ctx)
			{
				this.GetAccountStateCallback(ctx, callback);
			}, 0U);
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x0006C25C File Offset: 0x0006A45C
		private void GetAccountStateCallback(RPCContext context, RPCContextDelegate callback)
		{
			if (context == null || context.Payload == null)
			{
				base.ApiLog.LogWarning("GetAccountLevelInfo invalid context!");
				return;
			}
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != BattleNetErrors.ERROR_OK)
			{
				base.ApiLog.LogError("GetAccountLevelInfo failed with error={0}", new object[]
				{
					status.ToString()
				});
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
			if (callback != null)
			{
				callback(context);
			}
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x0006C314 File Offset: 0x0006A514
		private void GetAccountLevelInfo(bnet.protocol.EntityId accountId)
		{
			GetAccountStateRequest getAccountStateRequest = new GetAccountStateRequest();
			getAccountStateRequest.SetEntityId(accountId);
			AccountFieldOptions accountFieldOptions = new AccountFieldOptions();
			accountFieldOptions.SetFieldAccountLevelInfo(true);
			getAccountStateRequest.SetOptions(accountFieldOptions);
			this.m_rpcConnection.QueueRequest(this.m_accountService, 30U, getAccountStateRequest, new RPCContextDelegate(this.GetAccountLevelInfoCallback), 0U);
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x0006C364 File Offset: 0x0006A564
		private void GetAccountLevelInfoCallback(RPCContext context)
		{
			if (context == null || context.Payload == null)
			{
				base.ApiLog.LogWarning("GetAccountLevelInfo invalid context!");
				return;
			}
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != BattleNetErrors.ERROR_OK)
			{
				base.ApiLog.LogError("GetAccountLevelInfo failed with error={0}", new object[]
				{
					status.ToString()
				});
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
			if (getAccountStateRequest != null && getAccountStateRequest.EntityId == this.m_battleNet.AccountId)
			{
				AccountLevelInfo accountLevelInfo = getAccountStateResponse.State.AccountLevelInfo;
				this.m_preferredRegion = accountLevelInfo.PreferredRegion;
				this.m_accountCountry = accountLevelInfo.Country;
				this.m_headlessAccount = accountLevelInfo.HeadlessAccount;
				base.ApiLog.LogDebug("Region (preferred): {0}", new object[]
				{
					this.m_preferredRegion
				});
				base.ApiLog.LogDebug("Country (account): {0}", new object[]
				{
					this.m_accountCountry
				});
				base.ApiLog.LogDebug("Headless (account): {0}", new object[]
				{
					this.m_headlessAccount
				});
				if (accountLevelInfo.LicensesList.Count > 0)
				{
					this.m_licenses.Clear();
					base.ApiLog.LogDebug("Found {0} licenses.", new object[]
					{
						accountLevelInfo.LicensesList.Count
					});
					for (int i = 0; i < accountLevelInfo.LicensesList.Count; i++)
					{
						AccountLicense accountLicense = accountLevelInfo.LicensesList[i];
						this.m_licenses.Add(accountLicense);
						base.ApiLog.LogDebug("Adding license id={0}", new object[]
						{
							accountLicense.Id
						});
					}
				}
				else
				{
					base.ApiLog.LogWarning("No licenses found!");
				}
			}
			base.ApiLog.LogDebug("GetAccountLevelInfo, status=" + status.ToString());
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x0006C5A4 File Offset: 0x0006A7A4
		private void SubscribeToAccountService()
		{
			SubscriptionUpdateRequest subscriptionUpdateRequest = new SubscriptionUpdateRequest();
			SubscriberReference subscriberReference = new SubscriberReference();
			subscriberReference.SetEntityId(this.m_battleNet.AccountId);
			subscriberReference.SetObjectId(0UL);
			AccountFieldOptions accountFieldOptions = new AccountFieldOptions();
			accountFieldOptions.SetAllFields(true);
			subscriberReference.SetAccountOptions(accountFieldOptions);
			subscriptionUpdateRequest.AddRef(subscriberReference);
			subscriberReference = new SubscriberReference();
			subscriberReference.SetEntityId(this.m_battleNet.GameAccountId);
			subscriberReference.SetObjectId(0UL);
			new GameAccountFieldOptions().SetAllFields(true);
			subscriptionUpdateRequest.AddRef(subscriberReference);
			this.m_rpcConnection.QueueRequest(this.m_accountService, 25U, subscriptionUpdateRequest, new RPCContextDelegate(this.SubscribeToAccountServiceCallback), 0U);
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x0006C644 File Offset: 0x0006A844
		private void SubscribeToAccountServiceCallback(RPCContext context)
		{
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != BattleNetErrors.ERROR_OK)
			{
				base.ApiLog.LogError("SubscribeToAccountServiceCallback: " + status.ToString());
				return;
			}
			base.ApiLog.LogDebug("SubscribeToAccountServiceCallback, status=" + status.ToString());
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x0006C6A8 File Offset: 0x0006A8A8
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
				base.ApiLog.LogDebug("HandleAccountNotify_AccountStateUpdated HasState=false, data={0}", new object[]
				{
					accountStateNotification
				});
				return;
			}
			AccountState accountState = accountStateNotification.AccountState;
			if (!accountState.HasAccountLevelInfo)
			{
				base.ApiLog.LogDebug("HandleAccountNotify_AccountStateUpdated HasAccountLevelInfo=false, data={0}", new object[]
				{
					accountStateNotification
				});
				return;
			}
			if (!accountState.AccountLevelInfo.HasPreferredRegion)
			{
				base.ApiLog.LogDebug("HandleAccountNotify_AccountStateUpdated HasPreferredRegion=false, data={0}", new object[]
				{
					accountStateNotification
				});
				return;
			}
			base.ApiLog.LogDebug("HandleAccountNotify_AccountStateUpdated, data={0}", new object[]
			{
				accountStateNotification
			});
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x0006C788 File Offset: 0x0006A988
		private void HandleAccountNotify_GameAccountStateUpdated(RPCContext context)
		{
			GameAccountStateNotification arg = GameAccountStateNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleAccountNotify_GameAccountStateUpdated, data=" + arg);
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x0006C7B8 File Offset: 0x0006A9B8
		private void HandleAccountNotify_GameAccountsUpdated(RPCContext context)
		{
			GameAccountNotification arg = GameAccountNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleAccountNotify_GameAccountsUpdated, data=" + arg);
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x0006C7E8 File Offset: 0x0006A9E8
		private void HandleAccountNotify_GameSessionUpdated(RPCContext context)
		{
			GameAccountSessionNotification arg = GameAccountSessionNotification.ParseFrom(context.Payload);
			base.ApiLog.LogDebug("HandleAccountNotify_GameSessionUpdated, data=" + arg);
		}

		// Token: 0x04000B45 RID: 2885
		private ServiceDescriptor m_accountService = new AccountService();

		// Token: 0x04000B46 RID: 2886
		private ServiceDescriptor m_accountNotify = new AccountNotify();

		// Token: 0x04000B47 RID: 2887
		private uint m_preferredRegion = uint.MaxValue;

		// Token: 0x04000B48 RID: 2888
		private string m_accountCountry;

		// Token: 0x04000B49 RID: 2889
		private bool m_headlessAccount;

		// Token: 0x04000B4A RID: 2890
		private List<AccountLicense> m_licenses = new List<AccountLicense>();

		// Token: 0x02000678 RID: 1656
		public class GameSessionInfo
		{
			// Token: 0x0400218A RID: 8586
			public ulong SessionStartTime;
		}

		// Token: 0x02000679 RID: 1657
		public class CAISInfo
		{
			// Token: 0x0400218B RID: 8587
			public bool CAISactive;

			// Token: 0x0400218C RID: 8588
			public int CAISplayed;

			// Token: 0x0400218D RID: 8589
			public int CAISrested;
		}

		// Token: 0x0200067A RID: 1658
		private class GetGameSessionInfoRequestContext
		{
			// Token: 0x060061DE RID: 25054 RVA: 0x00127BC0 File Offset: 0x00125DC0
			public GetGameSessionInfoRequestContext(AccountAPI parent)
			{
				this.m_parent = parent;
			}

			// Token: 0x060061DF RID: 25055 RVA: 0x00127BD0 File Offset: 0x00125DD0
			public void GetGameSessionInfoRequestContextCallback(RPCContext context)
			{
				AccountAPI parent = this.m_parent;
				int getPlayerRestrictionAPICount = parent.GetPlayerRestrictionAPICount - 1;
				parent.GetPlayerRestrictionAPICount = getPlayerRestrictionAPICount;
				if (context == null || context.Payload == null)
				{
					this.m_parent.ApiLog.LogWarning("GetPlayRestrictions:GetGameSessionInfo invalid context!");
					return;
				}
				BattleNetErrors status = (BattleNetErrors)context.Header.Status;
				if (status != BattleNetErrors.ERROR_OK)
				{
					this.m_parent.ApiLog.LogError("GetPlayRestrictions:GetGameSessionInfo failed with error={0}", new object[]
					{
						status.ToString()
					});
					return;
				}
				GetGameSessionInfoResponse getGameSessionInfoResponse = GetGameSessionInfoResponse.ParseFrom(context.Payload);
				if (getGameSessionInfoResponse == null || !getGameSessionInfoResponse.IsInitialized)
				{
					this.m_parent.ApiLog.LogWarning("GetPlayRestrictions:GetGameSessionInfo unable to parse response!");
					return;
				}
				if (!getGameSessionInfoResponse.HasSessionInfo)
				{
					this.m_parent.ApiLog.LogWarning("GetPlayRestrictions:GetGameSessionInfo response has no data!");
					return;
				}
				this.m_parent.LastGameSessionInfo = new AccountAPI.GameSessionInfo();
				if (getGameSessionInfoResponse.SessionInfo.HasStartTime)
				{
					this.m_parent.LastGameSessionInfo.SessionStartTime = (ulong)getGameSessionInfoResponse.SessionInfo.StartTime;
					return;
				}
				this.m_parent.ApiLog.LogWarning("GetPlayRestrictions:GetGameSessionInfo response has no HasStartTime!");
			}

			// Token: 0x0400218E RID: 8590
			private AccountAPI m_parent;
		}

		// Token: 0x0200067B RID: 1659
		private class GetCAISInfoRequestContext
		{
			// Token: 0x060061E0 RID: 25056 RVA: 0x00127CE8 File Offset: 0x00125EE8
			public GetCAISInfoRequestContext(AccountAPI parent)
			{
				this.m_parent = parent;
			}

			// Token: 0x060061E1 RID: 25057 RVA: 0x00127CF8 File Offset: 0x00125EF8
			public void GetCAISInfoRequestContextCallback(RPCContext context)
			{
				AccountAPI parent = this.m_parent;
				int getPlayerRestrictionAPICount = parent.GetPlayerRestrictionAPICount - 1;
				parent.GetPlayerRestrictionAPICount = getPlayerRestrictionAPICount;
				if (context == null || context.Payload == null)
				{
					this.m_parent.ApiLog.LogWarning("GetPlayRestrictions:GetCAISInfo invalid context!");
					return;
				}
				BattleNetErrors status = (BattleNetErrors)context.Header.Status;
				if (status != BattleNetErrors.ERROR_OK)
				{
					this.m_parent.ApiLog.LogError("GetPlayRestrictions:GetCAISInfo failed with error={0}", new object[]
					{
						status.ToString()
					});
					return;
				}
				GetCAISInfoResponse getCAISInfoResponse = GetCAISInfoResponse.ParseFrom(context.Payload);
				if (getCAISInfoResponse == null || !getCAISInfoResponse.IsInitialized)
				{
					this.m_parent.ApiLog.LogWarning("GetPlayRestrictions:GetCAISInfo unable to parse response!");
					return;
				}
				this.m_parent.LastCAISInfo = new AccountAPI.CAISInfo();
				if (getCAISInfoResponse.HasCaisInfo)
				{
					this.m_parent.LastCAISInfo.CAISactive = (getCAISInfoResponse.CaisInfo.HasPlayedMinutes || getCAISInfoResponse.CaisInfo.HasRestedMinutes);
					this.m_parent.LastCAISInfo.CAISplayed = (int)(getCAISInfoResponse.CaisInfo.HasPlayedMinutes ? getCAISInfoResponse.CaisInfo.PlayedMinutes : 0U);
					this.m_parent.LastCAISInfo.CAISrested = (int)(getCAISInfoResponse.CaisInfo.HasRestedMinutes ? getCAISInfoResponse.CaisInfo.RestedMinutes : 0U);
				}
				this.m_parent.ApiLog.LogDebug("GetCAISInfo hasCaisInfo={0} played={1} rested={2}", new object[]
				{
					getCAISInfoResponse.HasCaisInfo,
					(getCAISInfoResponse.HasCaisInfo && getCAISInfoResponse.CaisInfo.HasPlayedMinutes) ? getCAISInfoResponse.CaisInfo.PlayedMinutes.ToString() : "",
					(getCAISInfoResponse.HasCaisInfo && getCAISInfoResponse.CaisInfo.HasRestedMinutes) ? getCAISInfoResponse.CaisInfo.RestedMinutes.ToString() : ""
				});
			}

			// Token: 0x0400218F RID: 8591
			private AccountAPI m_parent;
		}
	}
}
