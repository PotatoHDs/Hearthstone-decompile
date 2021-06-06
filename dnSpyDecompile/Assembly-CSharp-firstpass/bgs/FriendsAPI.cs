using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using bgs.RPCServices;
using bgs.types;
using bnet.protocol;
using bnet.protocol.friends.v1;

namespace bgs
{
	// Token: 0x02000202 RID: 514
	public class FriendsAPI : BattleNetAPI
	{
		// Token: 0x06001F88 RID: 8072 RVA: 0x0006EEF8 File Offset: 0x0006D0F8
		public FriendsAPI(BattleNetCSharp battlenet) : base(battlenet, "Friends")
		{
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001F89 RID: 8073 RVA: 0x0006EF48 File Offset: 0x0006D148
		public ServiceDescriptor FriendsService
		{
			get
			{
				return this.m_friendsService;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001F8A RID: 8074 RVA: 0x0006EF50 File Offset: 0x0006D150
		public ServiceDescriptor FriendsNotifyService
		{
			get
			{
				return this.m_friendsNotifyService;
			}
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x0006EF58 File Offset: 0x0006D158
		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
			rpcConnection.RegisterServiceMethodListener(this.m_friendsNotifyService.Id, 1U, new RPCContextDelegate(this.NotifyFriendAddedListenerCallback));
			rpcConnection.RegisterServiceMethodListener(this.m_friendsNotifyService.Id, 2U, new RPCContextDelegate(this.NotifyFriendRemovedListenerCallback));
			rpcConnection.RegisterServiceMethodListener(this.m_friendsNotifyService.Id, 3U, new RPCContextDelegate(this.NotifyReceivedInvitationAddedCallback));
			rpcConnection.RegisterServiceMethodListener(this.m_friendsNotifyService.Id, 4U, new RPCContextDelegate(this.NotifyReceivedInvitationRemovedCallback));
			rpcConnection.RegisterServiceMethodListener(this.m_friendsNotifyService.Id, 5U, new RPCContextDelegate(this.NotifySentInvitationAddedCallback));
			rpcConnection.RegisterServiceMethodListener(this.m_friendsNotifyService.Id, 6U, new RPCContextDelegate(this.NotifySentInvitationRemovedCallback));
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x0006F020 File Offset: 0x0006D220
		public override void Initialize()
		{
			base.Initialize();
			if (this.m_state == FriendsAPI.FriendsAPIState.INITIALIZING)
			{
				return;
			}
			this.StartInitialize();
			this.Subscribe();
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x0006BFB5 File Offset: 0x0006A1B5
		public override void OnDisconnected()
		{
			base.OnDisconnected();
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001F8E RID: 8078 RVA: 0x0006F03E File Offset: 0x0006D23E
		public bool IsInitialized
		{
			get
			{
				return this.m_state == FriendsAPI.FriendsAPIState.INITIALIZED || this.m_state == FriendsAPI.FriendsAPIState.FAILED_TO_INITIALIZE;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001F8F RID: 8079 RVA: 0x0006F057 File Offset: 0x0006D257
		// (set) Token: 0x06001F90 RID: 8080 RVA: 0x0006F05F File Offset: 0x0006D25F
		public float InitializeTimeOut
		{
			get
			{
				return this.m_initializeTimeOut;
			}
			set
			{
				this.m_initializeTimeOut = value;
			}
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x0006F068 File Offset: 0x0006D268
		public override void Process()
		{
			base.Process();
			if (this.m_state == FriendsAPI.FriendsAPIState.INITIALIZING && BattleNet.GetRealTimeSinceStartup() - this.m_subscribeStartTime >= (double)this.InitializeTimeOut)
			{
				this.m_state = FriendsAPI.FriendsAPIState.FAILED_TO_INITIALIZE;
				base.ApiLog.LogWarning("Battle.net Friends API C#: Initialize timed out.");
			}
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x0006F0A5 File Offset: 0x0006D2A5
		public bool IsFriend(BnetEntityId entityId)
		{
			return this.m_friendEntityId.ContainsKey(entityId);
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x0006F0B3 File Offset: 0x0006D2B3
		public bool GetFriendsActiveGameAccounts(BnetEntityId entityId, [Out] Map<ulong, bnet.protocol.EntityId> gameAccounts)
		{
			return this.m_friendEntityId.TryGetValue(entityId, out gameAccounts);
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x0006F0C8 File Offset: 0x0006D2C8
		public bool AddFriendsActiveGameAccount(BnetEntityId entityId, bnet.protocol.EntityId gameAccount, ulong index)
		{
			if (this.IsFriend(entityId))
			{
				if (!this.m_friendEntityId[entityId].ContainsKey(index))
				{
					this.m_friendEntityId[entityId].Add(index, gameAccount);
					this.m_battleNet.Presence.PresenceSubscribe(gameAccount);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x0006F11C File Offset: 0x0006D31C
		public void RemoveFriendsActiveGameAccount(BnetEntityId entityId, ulong index)
		{
			bnet.protocol.EntityId entityId2;
			if (this.IsFriend(entityId) && this.m_friendEntityId[entityId].TryGetValue(index, out entityId2))
			{
				this.m_battleNet.Presence.PresenceUnsubscribe(entityId2);
				this.m_friendEntityId[entityId].Remove(index);
			}
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x0006F16C File Offset: 0x0006D36C
		public void GetFriendsInfo(ref FriendsInfo info)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			info.maxFriends = (int)this.m_maxFriends;
			info.maxRecvInvites = (int)this.m_maxReceivedInvitations;
			info.maxSentInvites = (int)this.m_maxSentInvitations;
			info.friendsSize = (int)this.m_friendsCount;
			info.updateSize = this.m_updateList.Count;
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x0006F1C4 File Offset: 0x0006D3C4
		public void ClearFriendsUpdates()
		{
			this.m_updateList.Clear();
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x0006F1D1 File Offset: 0x0006D3D1
		public void GetFriendsUpdates([Out] FriendsUpdate[] updates)
		{
			this.m_updateList.CopyTo(updates, 0);
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x0006F1E0 File Offset: 0x0006D3E0
		public void SendFriendInvite(string sender, string target, bool byEmail)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			SendInvitationRequest sendInvitationRequest = new SendInvitationRequest();
			bnet.protocol.EntityId entityId = new bnet.protocol.EntityId();
			entityId.SetLow(0UL);
			entityId.SetHigh(0UL);
			sendInvitationRequest.SetTargetId(entityId);
			InvitationParams invitationParams = new InvitationParams();
			FriendInvitationParams friendInvitationParams = new FriendInvitationParams();
			if (byEmail)
			{
				friendInvitationParams.SetTargetEmail(target);
				friendInvitationParams.AddRole(2U);
			}
			else
			{
				friendInvitationParams.SetTargetBattleTag(target);
				friendInvitationParams.AddRole(1U);
			}
			invitationParams.SetFriendParams(friendInvitationParams);
			sendInvitationRequest.SetParams(invitationParams);
			SendInvitationRequest sendInvitationRequest2 = sendInvitationRequest;
			if (!sendInvitationRequest2.IsInitialized)
			{
				base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to SendFriendInvite.");
				return;
			}
			this.m_rpcConnection.QueueRequest(this.m_friendsService, 2U, sendInvitationRequest2, new RPCContextDelegate(this.SendInvitationCallback), 0U);
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x0006F290 File Offset: 0x0006D490
		public void ManageFriendInvite(int action, ulong inviteId)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			if (action == 1)
			{
				this.AcceptInvitation(inviteId);
				return;
			}
			if (action != 3)
			{
				return;
			}
			this.DeclineInvitation(inviteId);
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x0006F2C4 File Offset: 0x0006D4C4
		public void RemoveFriend(BnetAccountId account)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			bnet.protocol.EntityId entityId = new bnet.protocol.EntityId();
			entityId.SetLow(account.GetLo());
			entityId.SetHigh(account.GetHi());
			RemoveFriendRequest removeFriendRequest = new RemoveFriendRequest();
			removeFriendRequest.SetTargetId(entityId);
			RemoveFriendRequest removeFriendRequest2 = removeFriendRequest;
			if (!removeFriendRequest2.IsInitialized)
			{
				base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to RemoveFriend.");
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Friends, BnetFeatureEvent.Friends_OnRemoveFriend, BattleNetErrors.ERROR_API_NOT_READY, null);
				return;
			}
			this.m_rpcConnection.QueueRequest(this.m_friendsService, 8U, removeFriendRequest2, new RPCContextDelegate(this.RemoveFriendCallback), 0U);
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x0006F354 File Offset: 0x0006D554
		private void Subscribe()
		{
			SubscribeRequest subscribeRequest = new SubscribeRequest();
			subscribeRequest.SetObjectId(0UL);
			SubscribeRequest subscribeRequest2 = subscribeRequest;
			if (!subscribeRequest2.IsInitialized)
			{
				base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to Subscribe.");
				return;
			}
			this.m_rpcConnection.QueueRequest(this.m_friendsService, 1U, subscribeRequest2, new RPCContextDelegate(this.SubscribeToFriendsCallback), 0U);
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x0006F3AC File Offset: 0x0006D5AC
		private void AcceptInvitation(ulong inviteId)
		{
			AcceptInvitationRequest acceptInvitationRequest = new AcceptInvitationRequest();
			acceptInvitationRequest.SetInvitationId(inviteId);
			AcceptInvitationRequest acceptInvitationRequest2 = acceptInvitationRequest;
			if (!acceptInvitationRequest2.IsInitialized)
			{
				base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to AcceptInvitation.");
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Friends, BnetFeatureEvent.Friends_OnAcceptInvitation, BattleNetErrors.ERROR_API_NOT_READY, null);
				return;
			}
			this.m_rpcConnection.QueueRequest(this.m_friendsService, 3U, acceptInvitationRequest2, new RPCContextDelegate(this.AcceptInvitationCallback), 0U);
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x0006F410 File Offset: 0x0006D610
		private void DeclineInvitation(ulong inviteId)
		{
			DeclineInvitationRequest declineInvitationRequest = new DeclineInvitationRequest();
			declineInvitationRequest.SetInvitationId(inviteId);
			DeclineInvitationRequest declineInvitationRequest2 = declineInvitationRequest;
			if (!declineInvitationRequest2.IsInitialized)
			{
				base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to DeclineInvitation.");
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Friends, BnetFeatureEvent.Friends_OnDeclineInvitation, BattleNetErrors.ERROR_API_NOT_READY, null);
				return;
			}
			this.m_rpcConnection.QueueRequest(this.m_friendsService, 5U, declineInvitationRequest2, new RPCContextDelegate(this.DeclineInvitationCallback), 0U);
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x0006F474 File Offset: 0x0006D674
		private void SubscribeToFriendsCallback(RPCContext context)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZING)
			{
				return;
			}
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status == BattleNetErrors.ERROR_OK)
			{
				this.m_state = FriendsAPI.FriendsAPIState.INITIALIZED;
				base.ApiLog.LogDebug("Battle.net Friends API C#: Initialized.");
				SubscribeResponse response = SubscribeResponse.ParseFrom(context.Payload);
				this.ProcessSubscribeToFriendsResponse(response);
				return;
			}
			this.m_state = FriendsAPI.FriendsAPIState.FAILED_TO_INITIALIZE;
			base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to initialize: error={0} {1}", new object[]
			{
				(int)status,
				status
			});
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x0006F4F4 File Offset: 0x0006D6F4
		private void SendInvitationCallback(RPCContext context)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != BattleNetErrors.ERROR_OK)
			{
				base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to SendInvitation. " + status);
			}
			this.m_battleNet.EnqueueErrorInfo(BnetFeature.Friends, BnetFeatureEvent.Friends_OnSendInvitation, status, context);
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x0006F548 File Offset: 0x0006D748
		private void AcceptInvitationCallback(RPCContext context)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != BattleNetErrors.ERROR_OK)
			{
				base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to AcceptInvitation. " + status);
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Friends, BnetFeatureEvent.Friends_OnAcceptInvitation, status, context);
			}
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x0006F598 File Offset: 0x0006D798
		private void DeclineInvitationCallback(RPCContext context)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != BattleNetErrors.ERROR_OK)
			{
				base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to DeclineInvitation. " + status);
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Friends, BnetFeatureEvent.Friends_OnDeclineInvitation, status, context);
			}
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x0006F5E8 File Offset: 0x0006D7E8
		private void RemoveFriendCallback(RPCContext context)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != BattleNetErrors.ERROR_OK)
			{
				base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to RemoveFriend. " + status);
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Friends, BnetFeatureEvent.Friends_OnRemoveFriend, status, context);
			}
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x0006F63C File Offset: 0x0006D83C
		private void NotifyFriendAddedListenerCallback(RPCContext context)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			BnetEntityId entityId = this.ExtractEntityIdFromFriendNotification(context.Payload);
			this.AddFriendInternal(entityId);
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x0006F668 File Offset: 0x0006D868
		private void NotifyFriendRemovedListenerCallback(RPCContext context)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			BnetEntityId entityId = this.ExtractEntityIdFromFriendNotification(context.Payload);
			this.RemoveFriendInternal(entityId);
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x0006F694 File Offset: 0x0006D894
		private void NotifyReceivedInvitationAddedCallback(RPCContext context)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			ReceivedInvitation invitation = this.ExtractInvitationFromInvitationNotification(context.Payload);
			this.AddReceivedInvitationInternal(FriendsUpdate.Action.FRIEND_INVITE, invitation, 0);
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x0006F6C4 File Offset: 0x0006D8C4
		private void NotifyReceivedInvitationRemovedCallback(RPCContext context)
		{
			if (this.m_state != FriendsAPI.FriendsAPIState.INITIALIZED)
			{
				return;
			}
			ReceivedInvitation invitation = this.ExtractInvitationFromInvitationNotification(context.Payload);
			this.AddReceivedInvitationInternal(FriendsUpdate.Action.FRIEND_INVITE_REMOVED, invitation, 0);
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x00003FD0 File Offset: 0x000021D0
		private void NotifySentInvitationAddedCallback(RPCContext context)
		{
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x00003FD0 File Offset: 0x000021D0
		private void NotifySentInvitationRemovedCallback(RPCContext context)
		{
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x0006F6F4 File Offset: 0x0006D8F4
		private void ProcessSubscribeToFriendsResponse(SubscribeResponse response)
		{
			if (response.HasMaxFriends)
			{
				this.m_maxFriends = response.MaxFriends;
			}
			if (response.HasMaxReceivedInvitations)
			{
				this.m_maxReceivedInvitations = response.MaxReceivedInvitations;
			}
			if (response.HasMaxSentInvitations)
			{
				this.m_maxSentInvitations = response.MaxSentInvitations;
			}
			for (int i = 0; i < response.FriendsCount; i++)
			{
				Friend friend = response.Friends[i];
				BnetEntityId bnetEntityId = new BnetEntityId();
				bnetEntityId.SetLo(friend.AccountId.Low);
				bnetEntityId.SetHi(friend.AccountId.High);
				this.AddFriendInternal(bnetEntityId);
			}
			for (int j = 0; j < response.ReceivedInvitationsCount; j++)
			{
				ReceivedInvitation invitation = response.ReceivedInvitations[j];
				this.AddReceivedInvitationInternal(FriendsUpdate.Action.FRIEND_INVITE, invitation, 0);
			}
			for (int k = 0; k < response.SentInvitationsCount; k++)
			{
				SentInvitation invitation2 = response.SentInvitations[k];
				this.AddSentInvitationInternal(FriendsUpdate.Action.FRIEND_SENT_INVITE, invitation2, 0);
			}
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x0006F7E4 File Offset: 0x0006D9E4
		private void StartInitialize()
		{
			this.m_subscribeStartTime = BattleNet.GetRealTimeSinceStartup();
			this.m_state = FriendsAPI.FriendsAPIState.INITIALIZING;
			this.m_maxFriends = 0U;
			this.m_maxReceivedInvitations = 0U;
			this.m_maxSentInvitations = 0U;
			this.m_friendsCount = 0U;
			this.m_updateList = new List<FriendsUpdate>();
			this.m_friendEntityId = new Map<BnetEntityId, Map<ulong, bnet.protocol.EntityId>>();
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x0006F838 File Offset: 0x0006DA38
		private void AddFriendInternal(BnetEntityId entityId)
		{
			if (entityId == null)
			{
				return;
			}
			FriendsUpdate item = default(FriendsUpdate);
			item.action = 1;
			item.entity1 = entityId;
			this.m_updateList.Add(item);
			this.m_battleNet.Presence.PresenceSubscribe(BnetEntityId.CreateForProtocol(entityId));
			this.m_friendEntityId.Add(entityId, new Map<ulong, bnet.protocol.EntityId>());
			this.m_friendsCount = (uint)this.m_friendEntityId.Count;
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x0006F8AC File Offset: 0x0006DAAC
		private void RemoveFriendInternal(BnetEntityId entityId)
		{
			if (entityId == null)
			{
				return;
			}
			FriendsUpdate item = default(FriendsUpdate);
			item.action = 2;
			item.entity1 = entityId;
			this.m_updateList.Add(item);
			this.m_battleNet.Presence.PresenceUnsubscribe(BnetEntityId.CreateForProtocol(entityId));
			if (this.m_friendEntityId.ContainsKey(entityId))
			{
				foreach (bnet.protocol.EntityId entityId2 in this.m_friendEntityId[entityId].Values)
				{
					this.m_battleNet.Presence.PresenceUnsubscribe(entityId2);
				}
				this.m_friendEntityId.Remove(entityId);
			}
			this.m_friendsCount = (uint)this.m_friendEntityId.Count;
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x0006F984 File Offset: 0x0006DB84
		private void AddReceivedInvitationInternal(FriendsUpdate.Action action, ReceivedInvitation invitation, int reason)
		{
			if (invitation == null)
			{
				return;
			}
			FriendsUpdate item = default(FriendsUpdate);
			item.action = (int)action;
			item.long1 = invitation.Id;
			item.entity1 = this.GetBnetEntityIdFromIdentity(invitation.InviterIdentity);
			if (invitation.HasInviterName)
			{
				item.string1 = invitation.InviterName;
			}
			item.entity2 = this.GetBnetEntityIdFromIdentity(invitation.InviteeIdentity);
			if (invitation.HasInviteeName)
			{
				item.string2 = invitation.InviteeName;
			}
			if (invitation.HasInvitationMessage)
			{
				item.string3 = invitation.InvitationMessage;
			}
			item.bool1 = false;
			if (invitation.HasCreationTime)
			{
				item.long2 = invitation.CreationTime;
			}
			if (invitation.HasExpirationTime)
			{
				item.long3 = invitation.ExpirationTime;
			}
			this.m_updateList.Add(item);
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x0006FA58 File Offset: 0x0006DC58
		private void AddSentInvitationInternal(FriendsUpdate.Action action, SentInvitation invitation, int reason)
		{
			if (invitation == null)
			{
				return;
			}
			FriendsUpdate item = default(FriendsUpdate);
			item.action = (int)action;
			item.long1 = invitation.Id;
			if (invitation.HasTargetName)
			{
				item.string2 = invitation.TargetName;
			}
			item.bool1 = false;
			if (invitation.HasCreationTime)
			{
				item.long2 = invitation.CreationTime;
			}
			this.m_updateList.Add(item);
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x0006FAC4 File Offset: 0x0006DCC4
		private BnetEntityId GetBnetEntityIdFromIdentity(Identity identity)
		{
			BnetEntityId bnetEntityId = new BnetEntityId();
			if (identity.HasAccountId)
			{
				bnetEntityId.SetLo(identity.AccountId.Low);
				bnetEntityId.SetHi(identity.AccountId.High);
			}
			else if (identity.HasGameAccountId)
			{
				bnetEntityId.SetLo(identity.GameAccountId.Low);
				bnetEntityId.SetHi(identity.GameAccountId.High);
			}
			else
			{
				bnetEntityId.SetLo(0UL);
				bnetEntityId.SetHi(0UL);
			}
			return bnetEntityId;
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x0006FB40 File Offset: 0x0006DD40
		private BnetEntityId ExtractEntityIdFromFriendNotification(byte[] payload)
		{
			FriendNotification friendNotification = FriendNotification.ParseFrom(payload);
			if (!friendNotification.IsInitialized)
			{
				return null;
			}
			return BnetEntityId.CreateFromProtocol(friendNotification.Target.AccountId);
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x0006FB70 File Offset: 0x0006DD70
		private ReceivedInvitation ExtractInvitationFromInvitationNotification(byte[] payload)
		{
			InvitationNotification invitationNotification = InvitationNotification.ParseFrom(payload);
			if (!invitationNotification.IsInitialized)
			{
				return null;
			}
			return invitationNotification.Invitation;
		}

		// Token: 0x04000B6D RID: 2925
		private ServiceDescriptor m_friendsService = new FriendsService();

		// Token: 0x04000B6E RID: 2926
		private ServiceDescriptor m_friendsNotifyService = new FriendsNotify();

		// Token: 0x04000B6F RID: 2927
		private FriendsAPI.FriendsAPIState m_state;

		// Token: 0x04000B70 RID: 2928
		private double m_subscribeStartTime;

		// Token: 0x04000B71 RID: 2929
		private float m_initializeTimeOut = 5f;

		// Token: 0x04000B72 RID: 2930
		private uint m_maxFriends;

		// Token: 0x04000B73 RID: 2931
		private uint m_maxReceivedInvitations;

		// Token: 0x04000B74 RID: 2932
		private uint m_maxSentInvitations;

		// Token: 0x04000B75 RID: 2933
		private uint m_friendsCount;

		// Token: 0x04000B76 RID: 2934
		private List<FriendsUpdate> m_updateList = new List<FriendsUpdate>();

		// Token: 0x04000B77 RID: 2935
		private Map<BnetEntityId, Map<ulong, bnet.protocol.EntityId>> m_friendEntityId = new Map<BnetEntityId, Map<ulong, bnet.protocol.EntityId>>();

		// Token: 0x0200069F RID: 1695
		public enum InviteAction
		{
			// Token: 0x040021DD RID: 8669
			INVITE_ACCEPT = 1,
			// Token: 0x040021DE RID: 8670
			INVITE_REVOKE,
			// Token: 0x040021DF RID: 8671
			INVITE_DECLINE,
			// Token: 0x040021E0 RID: 8672
			INVITE_IGNORE
		}

		// Token: 0x020006A0 RID: 1696
		private enum FriendsAPIState
		{
			// Token: 0x040021E2 RID: 8674
			NOT_SET,
			// Token: 0x040021E3 RID: 8675
			INITIALIZING,
			// Token: 0x040021E4 RID: 8676
			INITIALIZED,
			// Token: 0x040021E5 RID: 8677
			FAILED_TO_INITIALIZE
		}
	}
}
