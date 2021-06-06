using System.Collections.Generic;
using System.Runtime.InteropServices;
using bgs.RPCServices;
using bgs.types;
using bnet.protocol;
using bnet.protocol.friends.v1;

namespace bgs
{
	public class FriendsAPI : BattleNetAPI
	{
		public enum InviteAction
		{
			INVITE_ACCEPT = 1,
			INVITE_REVOKE,
			INVITE_DECLINE,
			INVITE_IGNORE
		}

		private enum FriendsAPIState
		{
			NOT_SET,
			INITIALIZING,
			INITIALIZED,
			FAILED_TO_INITIALIZE
		}

		private ServiceDescriptor m_friendsService = new FriendsService();

		private ServiceDescriptor m_friendsNotifyService = new FriendsNotify();

		private FriendsAPIState m_state;

		private double m_subscribeStartTime;

		private float m_initializeTimeOut = 5f;

		private uint m_maxFriends;

		private uint m_maxReceivedInvitations;

		private uint m_maxSentInvitations;

		private uint m_friendsCount;

		private List<FriendsUpdate> m_updateList = new List<FriendsUpdate>();

		private Map<BnetEntityId, Map<ulong, bnet.protocol.EntityId>> m_friendEntityId = new Map<BnetEntityId, Map<ulong, bnet.protocol.EntityId>>();

		public ServiceDescriptor FriendsService => m_friendsService;

		public ServiceDescriptor FriendsNotifyService => m_friendsNotifyService;

		public bool IsInitialized
		{
			get
			{
				if (m_state == FriendsAPIState.INITIALIZED)
				{
					return true;
				}
				if (m_state == FriendsAPIState.FAILED_TO_INITIALIZE)
				{
					return true;
				}
				return false;
			}
		}

		public float InitializeTimeOut
		{
			get
			{
				return m_initializeTimeOut;
			}
			set
			{
				m_initializeTimeOut = value;
			}
		}

		public FriendsAPI(BattleNetCSharp battlenet)
			: base(battlenet, "Friends")
		{
		}

		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
			rpcConnection.RegisterServiceMethodListener(m_friendsNotifyService.Id, 1u, NotifyFriendAddedListenerCallback);
			rpcConnection.RegisterServiceMethodListener(m_friendsNotifyService.Id, 2u, NotifyFriendRemovedListenerCallback);
			rpcConnection.RegisterServiceMethodListener(m_friendsNotifyService.Id, 3u, NotifyReceivedInvitationAddedCallback);
			rpcConnection.RegisterServiceMethodListener(m_friendsNotifyService.Id, 4u, NotifyReceivedInvitationRemovedCallback);
			rpcConnection.RegisterServiceMethodListener(m_friendsNotifyService.Id, 5u, NotifySentInvitationAddedCallback);
			rpcConnection.RegisterServiceMethodListener(m_friendsNotifyService.Id, 6u, NotifySentInvitationRemovedCallback);
		}

		public override void Initialize()
		{
			base.Initialize();
			if (m_state != FriendsAPIState.INITIALIZING)
			{
				StartInitialize();
				Subscribe();
			}
		}

		public override void OnDisconnected()
		{
			base.OnDisconnected();
		}

		public override void Process()
		{
			base.Process();
			if (m_state == FriendsAPIState.INITIALIZING && BattleNet.GetRealTimeSinceStartup() - m_subscribeStartTime >= (double)InitializeTimeOut)
			{
				m_state = FriendsAPIState.FAILED_TO_INITIALIZE;
				base.ApiLog.LogWarning("Battle.net Friends API C#: Initialize timed out.");
			}
		}

		public bool IsFriend(BnetEntityId entityId)
		{
			return m_friendEntityId.ContainsKey(entityId);
		}

		public bool GetFriendsActiveGameAccounts(BnetEntityId entityId, [Out] Map<ulong, bnet.protocol.EntityId> gameAccounts)
		{
			if (m_friendEntityId.TryGetValue(entityId, out gameAccounts))
			{
				return true;
			}
			return false;
		}

		public bool AddFriendsActiveGameAccount(BnetEntityId entityId, bnet.protocol.EntityId gameAccount, ulong index)
		{
			if (IsFriend(entityId))
			{
				if (!m_friendEntityId[entityId].ContainsKey(index))
				{
					m_friendEntityId[entityId].Add(index, gameAccount);
					m_battleNet.Presence.PresenceSubscribe(gameAccount);
				}
				return true;
			}
			return false;
		}

		public void RemoveFriendsActiveGameAccount(BnetEntityId entityId, ulong index)
		{
			if (IsFriend(entityId) && m_friendEntityId[entityId].TryGetValue(index, out var value))
			{
				m_battleNet.Presence.PresenceUnsubscribe(value);
				m_friendEntityId[entityId].Remove(index);
			}
		}

		public void GetFriendsInfo(ref FriendsInfo info)
		{
			if (m_state == FriendsAPIState.INITIALIZED)
			{
				info.maxFriends = (int)m_maxFriends;
				info.maxRecvInvites = (int)m_maxReceivedInvitations;
				info.maxSentInvites = (int)m_maxSentInvitations;
				info.friendsSize = (int)m_friendsCount;
				info.updateSize = m_updateList.Count;
			}
		}

		public void ClearFriendsUpdates()
		{
			m_updateList.Clear();
		}

		public void GetFriendsUpdates([Out] FriendsUpdate[] updates)
		{
			m_updateList.CopyTo(updates, 0);
		}

		public void SendFriendInvite(string sender, string target, bool byEmail)
		{
			if (m_state == FriendsAPIState.INITIALIZED)
			{
				SendInvitationRequest sendInvitationRequest = new SendInvitationRequest();
				bnet.protocol.EntityId entityId = new bnet.protocol.EntityId();
				entityId.SetLow(0uL);
				entityId.SetHigh(0uL);
				sendInvitationRequest.SetTargetId(entityId);
				InvitationParams invitationParams = new InvitationParams();
				FriendInvitationParams friendInvitationParams = new FriendInvitationParams();
				if (byEmail)
				{
					friendInvitationParams.SetTargetEmail(target);
					friendInvitationParams.AddRole(2u);
				}
				else
				{
					friendInvitationParams.SetTargetBattleTag(target);
					friendInvitationParams.AddRole(1u);
				}
				invitationParams.SetFriendParams(friendInvitationParams);
				sendInvitationRequest.SetParams(invitationParams);
				SendInvitationRequest sendInvitationRequest2 = sendInvitationRequest;
				if (!sendInvitationRequest2.IsInitialized)
				{
					base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to SendFriendInvite.");
				}
				else
				{
					m_rpcConnection.QueueRequest(m_friendsService, 2u, sendInvitationRequest2, SendInvitationCallback);
				}
			}
		}

		public void ManageFriendInvite(int action, ulong inviteId)
		{
			if (m_state == FriendsAPIState.INITIALIZED)
			{
				switch (action)
				{
				case 1:
					AcceptInvitation(inviteId);
					break;
				case 3:
					DeclineInvitation(inviteId);
					break;
				}
			}
		}

		public void RemoveFriend(BnetAccountId account)
		{
			if (m_state == FriendsAPIState.INITIALIZED)
			{
				bnet.protocol.EntityId entityId = new bnet.protocol.EntityId();
				entityId.SetLow(account.GetLo());
				entityId.SetHigh(account.GetHi());
				RemoveFriendRequest removeFriendRequest = new RemoveFriendRequest();
				removeFriendRequest.SetTargetId(entityId);
				RemoveFriendRequest removeFriendRequest2 = removeFriendRequest;
				if (!removeFriendRequest2.IsInitialized)
				{
					base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to RemoveFriend.");
					m_battleNet.EnqueueErrorInfo(BnetFeature.Friends, BnetFeatureEvent.Friends_OnRemoveFriend, BattleNetErrors.ERROR_API_NOT_READY);
				}
				else
				{
					m_rpcConnection.QueueRequest(m_friendsService, 8u, removeFriendRequest2, RemoveFriendCallback);
				}
			}
		}

		private void Subscribe()
		{
			SubscribeRequest subscribeRequest = new SubscribeRequest();
			subscribeRequest.SetObjectId(0uL);
			SubscribeRequest subscribeRequest2 = subscribeRequest;
			if (!subscribeRequest2.IsInitialized)
			{
				base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to Subscribe.");
			}
			else
			{
				m_rpcConnection.QueueRequest(m_friendsService, 1u, subscribeRequest2, SubscribeToFriendsCallback);
			}
		}

		private void AcceptInvitation(ulong inviteId)
		{
			AcceptInvitationRequest acceptInvitationRequest = new AcceptInvitationRequest();
			acceptInvitationRequest.SetInvitationId(inviteId);
			AcceptInvitationRequest acceptInvitationRequest2 = acceptInvitationRequest;
			if (!acceptInvitationRequest2.IsInitialized)
			{
				base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to AcceptInvitation.");
				m_battleNet.EnqueueErrorInfo(BnetFeature.Friends, BnetFeatureEvent.Friends_OnAcceptInvitation, BattleNetErrors.ERROR_API_NOT_READY);
			}
			else
			{
				m_rpcConnection.QueueRequest(m_friendsService, 3u, acceptInvitationRequest2, AcceptInvitationCallback);
			}
		}

		private void DeclineInvitation(ulong inviteId)
		{
			DeclineInvitationRequest declineInvitationRequest = new DeclineInvitationRequest();
			declineInvitationRequest.SetInvitationId(inviteId);
			DeclineInvitationRequest declineInvitationRequest2 = declineInvitationRequest;
			if (!declineInvitationRequest2.IsInitialized)
			{
				base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to DeclineInvitation.");
				m_battleNet.EnqueueErrorInfo(BnetFeature.Friends, BnetFeatureEvent.Friends_OnDeclineInvitation, BattleNetErrors.ERROR_API_NOT_READY);
			}
			else
			{
				m_rpcConnection.QueueRequest(m_friendsService, 5u, declineInvitationRequest2, DeclineInvitationCallback);
			}
		}

		private void SubscribeToFriendsCallback(RPCContext context)
		{
			if (m_state == FriendsAPIState.INITIALIZING)
			{
				BattleNetErrors status = (BattleNetErrors)context.Header.Status;
				if (status == BattleNetErrors.ERROR_OK)
				{
					m_state = FriendsAPIState.INITIALIZED;
					base.ApiLog.LogDebug("Battle.net Friends API C#: Initialized.");
					SubscribeResponse response = SubscribeResponse.ParseFrom(context.Payload);
					ProcessSubscribeToFriendsResponse(response);
				}
				else
				{
					m_state = FriendsAPIState.FAILED_TO_INITIALIZE;
					base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to initialize: error={0} {1}", (int)status, status);
				}
			}
		}

		private void SendInvitationCallback(RPCContext context)
		{
			if (m_state == FriendsAPIState.INITIALIZED)
			{
				BattleNetErrors status = (BattleNetErrors)context.Header.Status;
				if (status != 0)
				{
					base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to SendInvitation. " + status);
				}
				m_battleNet.EnqueueErrorInfo(BnetFeature.Friends, BnetFeatureEvent.Friends_OnSendInvitation, status, context);
			}
		}

		private void AcceptInvitationCallback(RPCContext context)
		{
			if (m_state == FriendsAPIState.INITIALIZED)
			{
				BattleNetErrors status = (BattleNetErrors)context.Header.Status;
				if (status != 0)
				{
					base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to AcceptInvitation. " + status);
					m_battleNet.EnqueueErrorInfo(BnetFeature.Friends, BnetFeatureEvent.Friends_OnAcceptInvitation, status, context);
				}
			}
		}

		private void DeclineInvitationCallback(RPCContext context)
		{
			if (m_state == FriendsAPIState.INITIALIZED)
			{
				BattleNetErrors status = (BattleNetErrors)context.Header.Status;
				if (status != 0)
				{
					base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to DeclineInvitation. " + status);
					m_battleNet.EnqueueErrorInfo(BnetFeature.Friends, BnetFeatureEvent.Friends_OnDeclineInvitation, status, context);
				}
			}
		}

		private void RemoveFriendCallback(RPCContext context)
		{
			if (m_state == FriendsAPIState.INITIALIZED)
			{
				BattleNetErrors status = (BattleNetErrors)context.Header.Status;
				if (status != 0)
				{
					base.ApiLog.LogWarning("Battle.net Friends API C#: Failed to RemoveFriend. " + status);
					m_battleNet.EnqueueErrorInfo(BnetFeature.Friends, BnetFeatureEvent.Friends_OnRemoveFriend, status, context);
				}
			}
		}

		private void NotifyFriendAddedListenerCallback(RPCContext context)
		{
			if (m_state == FriendsAPIState.INITIALIZED)
			{
				BnetEntityId entityId = ExtractEntityIdFromFriendNotification(context.Payload);
				AddFriendInternal(entityId);
			}
		}

		private void NotifyFriendRemovedListenerCallback(RPCContext context)
		{
			if (m_state == FriendsAPIState.INITIALIZED)
			{
				BnetEntityId entityId = ExtractEntityIdFromFriendNotification(context.Payload);
				RemoveFriendInternal(entityId);
			}
		}

		private void NotifyReceivedInvitationAddedCallback(RPCContext context)
		{
			if (m_state == FriendsAPIState.INITIALIZED)
			{
				ReceivedInvitation invitation = ExtractInvitationFromInvitationNotification(context.Payload);
				AddReceivedInvitationInternal(FriendsUpdate.Action.FRIEND_INVITE, invitation, 0);
			}
		}

		private void NotifyReceivedInvitationRemovedCallback(RPCContext context)
		{
			if (m_state == FriendsAPIState.INITIALIZED)
			{
				ReceivedInvitation invitation = ExtractInvitationFromInvitationNotification(context.Payload);
				AddReceivedInvitationInternal(FriendsUpdate.Action.FRIEND_INVITE_REMOVED, invitation, 0);
			}
		}

		private void NotifySentInvitationAddedCallback(RPCContext context)
		{
		}

		private void NotifySentInvitationRemovedCallback(RPCContext context)
		{
		}

		private void ProcessSubscribeToFriendsResponse(SubscribeResponse response)
		{
			if (response.HasMaxFriends)
			{
				m_maxFriends = response.MaxFriends;
			}
			if (response.HasMaxReceivedInvitations)
			{
				m_maxReceivedInvitations = response.MaxReceivedInvitations;
			}
			if (response.HasMaxSentInvitations)
			{
				m_maxSentInvitations = response.MaxSentInvitations;
			}
			for (int i = 0; i < response.FriendsCount; i++)
			{
				Friend friend = response.Friends[i];
				BnetEntityId bnetEntityId = new BnetEntityId();
				bnetEntityId.SetLo(friend.AccountId.Low);
				bnetEntityId.SetHi(friend.AccountId.High);
				AddFriendInternal(bnetEntityId);
			}
			for (int j = 0; j < response.ReceivedInvitationsCount; j++)
			{
				ReceivedInvitation invitation = response.ReceivedInvitations[j];
				AddReceivedInvitationInternal(FriendsUpdate.Action.FRIEND_INVITE, invitation, 0);
			}
			for (int k = 0; k < response.SentInvitationsCount; k++)
			{
				SentInvitation invitation2 = response.SentInvitations[k];
				AddSentInvitationInternal(FriendsUpdate.Action.FRIEND_SENT_INVITE, invitation2, 0);
			}
		}

		private void StartInitialize()
		{
			m_subscribeStartTime = BattleNet.GetRealTimeSinceStartup();
			m_state = FriendsAPIState.INITIALIZING;
			m_maxFriends = 0u;
			m_maxReceivedInvitations = 0u;
			m_maxSentInvitations = 0u;
			m_friendsCount = 0u;
			m_updateList = new List<FriendsUpdate>();
			m_friendEntityId = new Map<BnetEntityId, Map<ulong, bnet.protocol.EntityId>>();
		}

		private void AddFriendInternal(BnetEntityId entityId)
		{
			if (!(entityId == null))
			{
				FriendsUpdate item = default(FriendsUpdate);
				item.action = 1;
				item.entity1 = entityId;
				m_updateList.Add(item);
				m_battleNet.Presence.PresenceSubscribe(BnetEntityId.CreateForProtocol(entityId));
				m_friendEntityId.Add(entityId, new Map<ulong, bnet.protocol.EntityId>());
				m_friendsCount = (uint)m_friendEntityId.Count;
			}
		}

		private void RemoveFriendInternal(BnetEntityId entityId)
		{
			if (entityId == null)
			{
				return;
			}
			FriendsUpdate item = default(FriendsUpdate);
			item.action = 2;
			item.entity1 = entityId;
			m_updateList.Add(item);
			m_battleNet.Presence.PresenceUnsubscribe(BnetEntityId.CreateForProtocol(entityId));
			if (m_friendEntityId.ContainsKey(entityId))
			{
				foreach (bnet.protocol.EntityId value in m_friendEntityId[entityId].Values)
				{
					m_battleNet.Presence.PresenceUnsubscribe(value);
				}
				m_friendEntityId.Remove(entityId);
			}
			m_friendsCount = (uint)m_friendEntityId.Count;
		}

		private void AddReceivedInvitationInternal(FriendsUpdate.Action action, ReceivedInvitation invitation, int reason)
		{
			if (invitation != null)
			{
				FriendsUpdate item = default(FriendsUpdate);
				item.action = (int)action;
				item.long1 = invitation.Id;
				item.entity1 = GetBnetEntityIdFromIdentity(invitation.InviterIdentity);
				if (invitation.HasInviterName)
				{
					item.string1 = invitation.InviterName;
				}
				item.entity2 = GetBnetEntityIdFromIdentity(invitation.InviteeIdentity);
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
				m_updateList.Add(item);
			}
		}

		private void AddSentInvitationInternal(FriendsUpdate.Action action, SentInvitation invitation, int reason)
		{
			if (invitation != null)
			{
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
				m_updateList.Add(item);
			}
		}

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
				bnetEntityId.SetLo(0uL);
				bnetEntityId.SetHi(0uL);
			}
			return bnetEntityId;
		}

		private BnetEntityId ExtractEntityIdFromFriendNotification(byte[] payload)
		{
			FriendNotification friendNotification = FriendNotification.ParseFrom(payload);
			if (!friendNotification.IsInitialized)
			{
				return null;
			}
			return BnetEntityId.CreateFromProtocol(friendNotification.Target.AccountId);
		}

		private ReceivedInvitation ExtractInvitationFromInvitationNotification(byte[] payload)
		{
			InvitationNotification invitationNotification = InvitationNotification.ParseFrom(payload);
			if (!invitationNotification.IsInitialized)
			{
				return null;
			}
			return invitationNotification.Invitation;
		}
	}
}
