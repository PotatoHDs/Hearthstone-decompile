using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using bgs.RPCServices;
using bnet.protocol.account.v1;
using bnet.protocol.channel.v2;
using bnet.protocol.channel.v2.membership;
using bnet.protocol.channel.v2.Types;
using bnet.protocol.Types;
using bnet.protocol.v2;

namespace bgs
{
	public class ChannelAPI : BattleNetAPI
	{
		public enum ChannelInformation
		{
			ATTRIBUTES,
			INVITATIONS,
			MEMBERS,
			ROLES
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		public struct ChannelType
		{
			public const string FRIENDLY_GAME = "FriendlyGame";

			public const string SPECTATOR_PARTY = "SpectatorParty";

			public const string BACON_PARTY = "BaconParty";
		}

		private struct ChannelRequestData
		{
			public RPCContextDelegate callback;

			public ChannelId channelId;

			public ChannelRequestData(ChannelId channelId = null, RPCContextDelegate callback = null)
			{
				this.callback = callback;
				this.channelId = channelId;
			}
		}

		private Map<uint, ChannelDescription> m_activeChannels = new Map<uint, ChannelDescription>();

		private Map<ulong, ChannelInvitation> m_receivedInvitations = new Map<ulong, ChannelInvitation>();

		private Map<ulong, ChannelInvitation> m_activeInvitations = new Map<ulong, ChannelInvitation>();

		private List<Suggestion> m_receivedInviteRequests = new List<Suggestion>();

		private ServiceDescriptor m_channelService = new ChannelServiceV2();

		private ServiceDescriptor m_channelMembershipService = new ChannelMembershipService();

		private ServiceDescriptor m_channelListener = new ChannelListener();

		private ServiceDescriptor m_channelMembershipListener = new ChannelMembershipListener();

		public ServiceDescriptor ChannelService => m_channelService;

		public ServiceDescriptor ChannelMembershipService => m_channelMembershipService;

		public ServiceDescriptor ChannelListener => m_channelListener;

		public ServiceDescriptor ChannelMembershipListener => m_channelMembershipListener;

		public ChannelAPI(BattleNetCSharp battlenet)
			: base(battlenet, "Channel")
		{
		}

		public override void OnDisconnected()
		{
			base.OnDisconnected();
			m_activeChannels.Clear();
			Log.Party.Print(LogLevel.Info, "[ChannelAPIv2] All Active Channels have been cleared!");
		}

		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
			m_rpcConnection.RegisterServiceMethodListener(m_channelMembershipListener.Id, 1u, HandleChannelAdded);
			m_rpcConnection.RegisterServiceMethodListener(m_channelMembershipListener.Id, 2u, HandleChannelRemoved);
			m_rpcConnection.RegisterServiceMethodListener(m_channelMembershipListener.Id, 3u, HandleReceivedInvitationAdded);
			m_rpcConnection.RegisterServiceMethodListener(m_channelMembershipListener.Id, 4u, HandleReceivedInvitationRemoved);
			m_rpcConnection.RegisterServiceMethodListener(m_channelListener.Id, 16u, HandleAttributeChanged);
			m_rpcConnection.RegisterServiceMethodListener(m_channelListener.Id, 18u, HandleInvitationAdded);
			m_rpcConnection.RegisterServiceMethodListener(m_channelListener.Id, 19u, HandleInvitationRemoved);
			m_rpcConnection.RegisterServiceMethodListener(m_channelListener.Id, 3u, HandleMemberAdded);
			m_rpcConnection.RegisterServiceMethodListener(m_channelListener.Id, 5u, HandleMemberAttributeChanged);
			m_rpcConnection.RegisterServiceMethodListener(m_channelListener.Id, 4u, HandleMemberRemoved);
			m_rpcConnection.RegisterServiceMethodListener(m_channelListener.Id, 6u, HandleMemberRoleChanged);
			m_rpcConnection.RegisterServiceMethodListener(m_channelListener.Id, 17u, HandlePrivacyLevelChanged);
			m_rpcConnection.RegisterServiceMethodListener(m_channelListener.Id, 10u, HandleSendMessage);
			m_rpcConnection.RegisterServiceMethodListener(m_channelListener.Id, 20u, HandleSuggestionAdded);
			m_rpcConnection.RegisterServiceMethodListener(m_channelListener.Id, 11u, HandleTypingIndicator);
		}

		public void RemoveInviteRequestsFor(ChannelId channelId, GameAccountHandle suggesteeId, uint removeReason)
		{
			if (m_receivedInviteRequests == null || suggesteeId == null)
			{
				return;
			}
			List<Suggestion> list = new List<Suggestion>();
			foreach (Suggestion receivedInviteRequest in m_receivedInviteRequests)
			{
				if (receivedInviteRequest.ChannelId.Equals(channelId) && receivedInviteRequest.Suggestee.Id.Equals(suggesteeId))
				{
					list.Add(receivedInviteRequest);
				}
			}
			foreach (Suggestion item in list)
			{
				m_receivedInviteRequests.Remove(item);
			}
		}

		public ChannelDescription GetChannelDescription(ChannelId channelId)
		{
			if (m_activeChannels.TryGetValue(channelId.Id, out var value))
			{
				return value;
			}
			return null;
		}

		public ChannelInvitation GetReceivedInvite(ulong invitationId)
		{
			if (m_receivedInvitations.TryGetValue(invitationId, out var value))
			{
				return value;
			}
			return null;
		}

		public ChannelInvitation[] GetAllReceivedInvites()
		{
			return m_receivedInvitations.Values.ToArray();
		}

		public Suggestion[] GetReceivedInviteRequestsForChannel(ChannelId channelId)
		{
			return m_receivedInviteRequests.Where((Suggestion i) => i.HasChannelId && i.ChannelId.Equals(channelId)).ToArray();
		}

		public ChannelInvitation GetSentInvite(ulong invitationId)
		{
			if (m_activeInvitations.TryGetValue(invitationId, out var value))
			{
				return value;
			}
			return null;
		}

		public ChannelInvitation[] GetAllSentInvites()
		{
			return m_activeInvitations.Values.ToArray();
		}

		private bool IsNotificationForMe(GameAccountHandle subscriberId, string sourceMethod)
		{
			GameAccountHandle myGameAccountHandle = m_battleNet.GetMyGameAccountHandle();
			if (subscriberId == null)
			{
				base.ApiLog.LogInfo("{0} received request for null subscriberId.", sourceMethod);
				return false;
			}
			if (!subscriberId.Equals(myGameAccountHandle))
			{
				base.ApiLog.LogInfo("{0} received request for subscriberId : {1} - {2}, but this is not the current account.", sourceMethod, subscriberId.Id, subscriberId.Region);
				return false;
			}
			return true;
		}

		private void LoggingCallback(RPCContext context, string message, ChannelRequestData channelRequestData)
		{
			BattleNetErrors error = ((context == null || context.Header == null) ? BattleNetErrors.ERROR_RPC_MALFORMED_RESPONSE : ((BattleNetErrors)context.Header.Status));
			LoggingCallback_Internal(error, message, channelRequestData);
			if (channelRequestData.callback != null)
			{
				channelRequestData.callback(context);
			}
		}

		private void LoggingCallback_Internal(BattleNetErrors error, string message, ChannelRequestData channelRequestData)
		{
			if (error != 0)
			{
				message = ((channelRequestData.channelId == null) ? $"ChannelRequestError: {(int)error} {error.ToString()} {message}" : $"ChannelRequestError: {(int)error} {error.ToString()} {message} channelId=({channelRequestData.channelId.Id}, {channelRequestData.channelId.Host.Label}, {channelRequestData.channelId.Host.Epoch})");
				m_battleNet.Channel.ApiLog.LogError(message);
			}
			else
			{
				message = ((channelRequestData.channelId == null) ? $"ChannelRequest {message} status={error.ToString()}" : $"ChannelRequest {message} status={error.ToString()} channelId=({channelRequestData.channelId.Id}, {channelRequestData.channelId.Host.Label}, {channelRequestData.channelId.Host.Epoch})");
				m_battleNet.Channel.ApiLog.LogDebug(message);
			}
		}

		public void SubscribeMembership()
		{
			bnet.protocol.channel.v2.membership.SubscribeRequest subscribeRequest = new bnet.protocol.channel.v2.membership.SubscribeRequest();
			subscribeRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			m_rpcConnection.QueueRequest(m_channelMembershipService, 1u, subscribeRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "SubscribeMembership", default(ChannelRequestData));
			});
		}

		public void CreateChannel(CreateChannelOptions options)
		{
			CreateChannelRequest createChannelRequest = new CreateChannelRequest();
			createChannelRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			createChannelRequest.SetOptions(options);
			m_rpcConnection.QueueRequest(m_channelService, 2u, createChannelRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "CreateChannel", new ChannelRequestData(null, m_battleNet.Party.PartyCreated));
			});
		}

		public void DissolveChannel(ChannelId channelId)
		{
			DissolveChannelRequest dissolveChannelRequest = new DissolveChannelRequest();
			dissolveChannelRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			dissolveChannelRequest.SetChannelId(channelId);
			m_rpcConnection.QueueRequest(m_channelService, 3u, dissolveChannelRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "DissolveChannel", new ChannelRequestData(channelId));
			});
		}

		public void GetChannel(ChannelId channelId, RPCContextDelegate callback, ChannelInformation[] fetchInformation = null)
		{
			fetchInformation = fetchInformation ?? new ChannelInformation[0];
			GetChannelRequest getChannelRequest = new GetChannelRequest();
			getChannelRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			getChannelRequest.SetChannelId(channelId);
			getChannelRequest.SetFetchAttributes(fetchInformation.Contains(ChannelInformation.ATTRIBUTES));
			getChannelRequest.SetFetchInvitations(fetchInformation.Contains(ChannelInformation.INVITATIONS));
			getChannelRequest.SetFetchMembers(fetchInformation.Contains(ChannelInformation.MEMBERS));
			getChannelRequest.SetFetchRoles(fetchInformation.Contains(ChannelInformation.ROLES));
			m_rpcConnection.QueueRequest(m_channelService, 4u, getChannelRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "GetChannel", new ChannelRequestData(channelId, callback));
			});
		}

		public void GetPublicChannelTypes(UniqueChannelType uniqueChannelType, RPCContextDelegate callback)
		{
			GetPublicChannelTypesRequest getPublicChannelTypesRequest = new GetPublicChannelTypesRequest();
			GetPublicChannelTypesOptions getPublicChannelTypesOptions = new GetPublicChannelTypesOptions();
			getPublicChannelTypesRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			getPublicChannelTypesRequest.SetOptions(getPublicChannelTypesOptions);
			getPublicChannelTypesOptions.SetType(uniqueChannelType);
			m_rpcConnection.QueueRequest(m_channelService, 5u, getPublicChannelTypesRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "GetPublicChannelTypes", new ChannelRequestData(null, callback));
			});
		}

		public void FindChannel(FindChannelOptions options)
		{
			FindChannelRequest findChannelRequest = new FindChannelRequest();
			findChannelRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			findChannelRequest.SetOptions(options);
			m_rpcConnection.QueueRequest(m_channelService, 6u, findChannelRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "FindChannel", default(ChannelRequestData));
			});
		}

		public void Subscribe(ChannelId channelId, RPCContextDelegate callback)
		{
			bnet.protocol.channel.v2.SubscribeRequest subscribeRequest = new bnet.protocol.channel.v2.SubscribeRequest();
			subscribeRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			subscribeRequest.SetChannelId(channelId);
			m_rpcConnection.QueueRequest(m_channelService, 10u, subscribeRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "Subscribe", new ChannelRequestData(channelId, callback));
			});
		}

		public void Unsubscribe(ChannelId channelId)
		{
			bnet.protocol.channel.v2.UnsubscribeRequest unsubscribeRequest = new bnet.protocol.channel.v2.UnsubscribeRequest();
			unsubscribeRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			unsubscribeRequest.SetChannelId(channelId);
			m_rpcConnection.QueueRequest(m_channelService, 11u, unsubscribeRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "Unsubscribe", new ChannelRequestData(channelId));
			});
		}

		public void SetChannelAttributes(ChannelId channelId, List<bnet.protocol.v2.Attribute> attributes)
		{
			SetAttributeRequest setAttributeRequest = new SetAttributeRequest();
			setAttributeRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			setAttributeRequest.SetChannelId(channelId);
			setAttributeRequest.SetAttribute(attributes);
			m_rpcConnection.QueueRequest(m_channelService, 21u, setAttributeRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "SetChannelAttributes", new ChannelRequestData(channelId));
			});
		}

		public void SetPrivacyLevel(ChannelId channelId, bnet.protocol.channel.v2.Types.PrivacyLevel privacyLevel)
		{
			SetPrivacyLevelRequest setPrivacyLevelRequest = new SetPrivacyLevelRequest();
			setPrivacyLevelRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			setPrivacyLevelRequest.SetChannelId(channelId);
			setPrivacyLevelRequest.SetPrivacyLevel(privacyLevel);
			m_rpcConnection.QueueRequest(m_channelService, 22u, setPrivacyLevelRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "SetPrivacyLevel", new ChannelRequestData(channelId));
			});
		}

		public void SendMessage(ChannelId channelId, string message, List<bnet.protocol.v2.Attribute> attributes = null)
		{
			SendMessageRequest sendMessageRequest = new SendMessageRequest();
			SendMessageOptions sendMessageOptions = new SendMessageOptions();
			sendMessageRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			sendMessageRequest.SetChannelId(channelId);
			sendMessageRequest.SetOptions(sendMessageOptions);
			sendMessageOptions.SetAttribute(attributes);
			sendMessageOptions.SetContent(message);
			m_rpcConnection.QueueRequest(m_channelService, 23u, sendMessageRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "SendMessage", new ChannelRequestData(channelId));
			});
		}

		public void SetTypingIndicator(ChannelId channelId, TypingIndicator action)
		{
			SetTypingIndicatorRequest setTypingIndicatorRequest = new SetTypingIndicatorRequest();
			setTypingIndicatorRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			setTypingIndicatorRequest.SetChannelId(channelId);
			setTypingIndicatorRequest.SetAction(action);
			m_rpcConnection.QueueRequest(m_channelService, 24u, setTypingIndicatorRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "SetTypingIndicator", new ChannelRequestData(channelId));
			});
		}

		public void Join(ChannelId channelId)
		{
			JoinRequest joinRequest = new JoinRequest();
			joinRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			joinRequest.SetChannelId(channelId);
			m_rpcConnection.QueueRequest(m_channelService, 30u, joinRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "Join", new ChannelRequestData(channelId));
			});
		}

		public void Leave(ChannelId channelId)
		{
			LeaveRequest leaveRequest = new LeaveRequest();
			leaveRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			leaveRequest.SetChannelId(channelId);
			m_rpcConnection.QueueRequest(m_channelService, 31u, leaveRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "Leave", new ChannelRequestData(channelId));
			});
		}

		public void Kick(ChannelId channelId, GameAccountHandle target)
		{
			KickRequest kickRequest = new KickRequest();
			kickRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			kickRequest.SetChannelId(channelId);
			kickRequest.SetTargetId(target);
			m_rpcConnection.QueueRequest(m_channelService, 32u, kickRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "Kick", new ChannelRequestData(channelId));
			});
		}

		public void SetMemberAttribute(ChannelId channelId, AttributeAssignment assignment)
		{
			SetMemberAttributeRequest setMemberAttributeRequest = new SetMemberAttributeRequest();
			setMemberAttributeRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			setMemberAttributeRequest.SetChannelId(channelId);
			setMemberAttributeRequest.SetAssignment(assignment);
			m_rpcConnection.QueueRequest(m_channelService, 40u, setMemberAttributeRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "SetMemberAttribute", new ChannelRequestData(channelId));
			});
		}

		public void AssignRole(ChannelId channelId, GameAccountHandle target, List<uint> roles)
		{
			AssignRoleRequest assignRoleRequest = new AssignRoleRequest();
			RoleAssignment roleAssignment = new RoleAssignment();
			assignRoleRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			assignRoleRequest.SetChannelId(channelId);
			assignRoleRequest.SetAssignment(roleAssignment);
			roleAssignment.SetMemberId(target);
			roleAssignment.SetRole(roles);
			m_rpcConnection.QueueRequest(m_channelService, 41u, assignRoleRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "AssignRole", new ChannelRequestData(channelId));
			});
		}

		public void UnassignRole(ChannelId channelId, GameAccountHandle target, List<uint> roles)
		{
			UnassignRoleRequest unassignRoleRequest = new UnassignRoleRequest();
			RoleAssignment roleAssignment = new RoleAssignment();
			unassignRoleRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			unassignRoleRequest.SetChannelId(channelId);
			unassignRoleRequest.SetAssignment(roleAssignment);
			roleAssignment.SetMemberId(target);
			roleAssignment.SetRole(roles);
			m_rpcConnection.QueueRequest(m_channelService, 42u, unassignRoleRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "UnassignRole", new ChannelRequestData(channelId));
			});
		}

		public void SendInvitation(ChannelId channelId, GameAccountHandle target, ChannelSlot slot)
		{
			SendInvitationRequest sendInvitationRequest = new SendInvitationRequest();
			SendInvitationOptions sendInvitationOptions = new SendInvitationOptions();
			sendInvitationRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			sendInvitationRequest.SetOptions(sendInvitationOptions);
			sendInvitationOptions.SetChannelId(channelId);
			sendInvitationOptions.SetTargetId(target);
			sendInvitationOptions.SetSlot(slot);
			m_rpcConnection.QueueRequest(m_channelService, 50u, sendInvitationRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "SendInvitation", new ChannelRequestData(channelId));
			});
		}

		public void AcceptInvitation(ChannelId channelId, ulong invitationId)
		{
			AcceptInvitationRequest acceptInvitationRequest = new AcceptInvitationRequest();
			acceptInvitationRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			acceptInvitationRequest.SetChannelId(channelId);
			acceptInvitationRequest.SetInvitationId(invitationId);
			m_rpcConnection.QueueRequest(m_channelService, 51u, acceptInvitationRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "AcceptInvitation", new ChannelRequestData(channelId));
			});
		}

		public void DeclineInvitation(ChannelId channelId, ulong invitationId)
		{
			DeclineInvitationRequest declineInvitationRequest = new DeclineInvitationRequest();
			declineInvitationRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			declineInvitationRequest.SetChannelId(channelId);
			declineInvitationRequest.SetInvitationId(invitationId);
			m_rpcConnection.QueueRequest(m_channelService, 52u, declineInvitationRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "DeclineInvitation", new ChannelRequestData(channelId));
			});
		}

		public void RevokeInvitation(ChannelId channelId, ulong invitationId)
		{
			RevokeInvitationRequest revokeInvitationRequest = new RevokeInvitationRequest();
			revokeInvitationRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			revokeInvitationRequest.SetChannelId(channelId);
			revokeInvitationRequest.SetInvitationId(invitationId);
			m_rpcConnection.QueueRequest(m_channelService, 53u, revokeInvitationRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "RevokeInvitation", new ChannelRequestData(channelId));
			});
		}

		public void SendSuggestion(ChannelId channelId, GameAccountHandle approver, GameAccountHandle target)
		{
			SendSuggestionRequest sendSuggestionRequest = new SendSuggestionRequest();
			SendSuggestionOptions sendSuggestionOptions = new SendSuggestionOptions();
			sendSuggestionRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			sendSuggestionRequest.SetOptions(sendSuggestionOptions);
			sendSuggestionOptions.SetChannelId(channelId);
			sendSuggestionOptions.SetTargetId(target);
			sendSuggestionOptions.SetApprovalId(approver);
			m_rpcConnection.QueueRequest(m_channelService, 60u, sendSuggestionRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "SendSuggestion", new ChannelRequestData(channelId));
			});
		}

		public void GetJoinVoiceToken(ChannelId channelId, RPCContextDelegate callback)
		{
			GetJoinVoiceTokenRequest getJoinVoiceTokenRequest = new GetJoinVoiceTokenRequest();
			getJoinVoiceTokenRequest.SetAgentId(m_battleNet.GetMyGameAccountHandle());
			getJoinVoiceTokenRequest.SetChannelId(channelId);
			m_rpcConnection.QueueRequest(m_channelService, 70u, getJoinVoiceTokenRequest, delegate(RPCContext ctx)
			{
				LoggingCallback(ctx, "SendSuggestion", new ChannelRequestData(channelId, callback));
			});
		}

		private void HandleChannelAdded(RPCContext context)
		{
			ChannelAddedNotification channelAddedNotification = ChannelAddedNotification.ParseFrom(context.Payload);
			if (!IsNotificationForMe(channelAddedNotification.SubscriberId, "HandleChannelAdded"))
			{
				return;
			}
			ChannelDescription channelDescription = channelAddedNotification.Membership;
			try
			{
				m_activeChannels.Add(channelDescription.Id.Id, channelDescription);
			}
			catch (ArgumentException ex)
			{
				StringBuilder stringBuilder = new StringBuilder("[ChannelAPIv2] HandleChannelAdded channel already existed in dictionary. ");
				stringBuilder.Append("Channel Id: ");
				stringBuilder.AppendLine(channelDescription.Id.Id.ToString());
				stringBuilder.Append("Channel Name: ");
				stringBuilder.AppendLine(channelDescription.Name);
				stringBuilder.Append("Privacy Level: ");
				stringBuilder.AppendLine(channelDescription.PrivacyLevel.ToString());
				stringBuilder.Append("Member Count: ");
				stringBuilder.AppendLine(channelDescription.MemberCount.ToString());
				stringBuilder.Append("Original Exception: ");
				stringBuilder.Append(ex.ToString());
				base.ApiLog.LogException(stringBuilder.ToString());
				return;
			}
			Subscribe(channelDescription.Id, delegate(RPCContext ctx)
			{
				bnet.protocol.channel.v2.SubscribeResponse subscribeResponse = bnet.protocol.channel.v2.SubscribeResponse.ParseFrom(ctx.Payload);
				if (subscribeResponse.HasChannel)
				{
					m_battleNet.Party.PartyJoined(channelDescription, subscribeResponse);
					m_battleNet.Party.GetPartySentChannelInvitations(channelDescription.Id, out var invites);
					if (invites != null)
					{
						ChannelInvitation[] array = invites;
						foreach (ChannelInvitation channelInvitation in array)
						{
							m_activeInvitations[channelInvitation.Id] = channelInvitation;
						}
					}
				}
			});
		}

		private void HandleChannelRemoved(RPCContext context)
		{
			ChannelRemovedNotification channelRemovedNotification = ChannelRemovedNotification.ParseFrom(context.Payload);
			if (IsNotificationForMe(channelRemovedNotification.SubscriberId, "HandleChannelRemoved"))
			{
				m_battleNet.Party.PartyLeft(channelRemovedNotification.ChannelId, channelRemovedNotification);
				m_activeChannels.Remove(channelRemovedNotification.ChannelId.Id);
				Log.Party.Print(LogLevel.Info, "[ChannelAPIv2] Removing Channel: {0}", channelRemovedNotification.ChannelId.Id);
			}
		}

		private void HandleReceivedInvitationAdded(RPCContext context)
		{
			ReceivedInvitationAddedNotification receivedInvitationAddedNotification = ReceivedInvitationAddedNotification.ParseFrom(context.Payload);
			if (IsNotificationForMe(receivedInvitationAddedNotification.SubscriberId, "HandleReceivedInvitationAdded"))
			{
				if (!receivedInvitationAddedNotification.HasInvitation)
				{
					base.ApiLog.LogInfo("HandleReceivedInvitationAdded received request with no invitationId");
					return;
				}
				m_receivedInvitations.Add(receivedInvitationAddedNotification.Invitation.Id, receivedInvitationAddedNotification.Invitation);
				m_battleNet.Party.ReceivedInvitationAdded(receivedInvitationAddedNotification, receivedInvitationAddedNotification.Invitation);
			}
		}

		private void HandleReceivedInvitationRemoved(RPCContext context)
		{
			ReceivedInvitationRemovedNotification receivedInvitationRemovedNotification = ReceivedInvitationRemovedNotification.ParseFrom(context.Payload);
			if (!IsNotificationForMe(receivedInvitationRemovedNotification.SubscriberId, "HandleReceivedInvitationRemoved"))
			{
				return;
			}
			if (!receivedInvitationRemovedNotification.HasInvitationId)
			{
				base.ApiLog.LogInfo("HandleReceivedInvitationRemoved received request with no invitationId");
				return;
			}
			ChannelInvitation receivedInvite = GetReceivedInvite(receivedInvitationRemovedNotification.InvitationId);
			if (receivedInvite == null)
			{
				base.ApiLog.LogInfo("HandleReceivedInvitationRemoved received request for invitationId {0}, but that invitation did not exist on client.", receivedInvitationRemovedNotification.InvitationId);
			}
			else
			{
				m_battleNet.Party.ReceivedInvitationRemoved(receivedInvitationRemovedNotification, receivedInvite);
				m_receivedInvitations.Remove(receivedInvitationRemovedNotification.InvitationId);
			}
		}

		private void HandleAttributeChanged(RPCContext context)
		{
			AttributeChangedNotification notification = AttributeChangedNotification.ParseFrom(context.Payload);
			if (!IsNotificationForMe(notification.SubscriberId, "HandleAttributeChanged"))
			{
				return;
			}
			ChannelDescription channelDescription = GetChannelDescription(notification.ChannelId);
			if (channelDescription != null)
			{
				foreach (bnet.protocol.v2.Attribute item in notification.Attribute)
				{
					UpdateAttributeForChannel(channelDescription, item);
				}
			}
			ChannelInvitation channelInvitation = m_battleNet.Channel.GetAllReceivedInvites().FirstOrDefault((ChannelInvitation i) => i.HasChannel && i.Channel.HasId && i.Channel.Id.Equals(notification.ChannelId));
			if (channelInvitation != null && channelInvitation.HasChannel)
			{
				channelDescription = channelInvitation.Channel;
				foreach (bnet.protocol.v2.Attribute item2 in notification.Attribute)
				{
					UpdateAttributeForChannel(channelDescription, item2);
				}
			}
			m_battleNet.Party.PartyAttributeChanged(notification.ChannelId, notification.AttributeList.ToArray());
		}

		private void UpdateAttributeForChannel(ChannelDescription description, bnet.protocol.v2.Attribute attribute)
		{
			bnet.protocol.v2.Attribute attribute2 = description.Attribute.FirstOrDefault((bnet.protocol.v2.Attribute i) => i.HasName && i.Name.Equals(attribute.Name));
			if (attribute2 != null)
			{
				attribute2.Value = attribute.Value;
			}
			else
			{
				description.AddAttribute(attribute);
			}
		}

		private void HandleInvitationAdded(RPCContext context)
		{
			InvitationAddedNotification invitationAddedNotification = InvitationAddedNotification.ParseFrom(context.Payload);
			if (IsNotificationForMe(invitationAddedNotification.SubscriberId, "HandleInvitationAdded"))
			{
				if (!invitationAddedNotification.HasInvitation)
				{
					base.ApiLog.LogInfo("HandleInvitationAdded received request for with no invitationId");
					return;
				}
				m_activeInvitations.Add(invitationAddedNotification.Invitation.Id, invitationAddedNotification.Invitation);
				m_battleNet.Party.PartyInvitationDelta(invitationAddedNotification.ChannelId, invitationAddedNotification.Invitation, null);
			}
		}

		private void HandleInvitationRemoved(RPCContext context)
		{
			InvitationRemovedNotification invitationRemovedNotification = InvitationRemovedNotification.ParseFrom(context.Payload);
			if (!IsNotificationForMe(invitationRemovedNotification.SubscriberId, "HandleAttributeChanged"))
			{
				return;
			}
			if (!invitationRemovedNotification.HasInvitationId)
			{
				base.ApiLog.LogInfo("HandleInvitationRemoved received request with no invitationId");
				return;
			}
			ChannelInvitation receivedInvite = GetReceivedInvite(invitationRemovedNotification.InvitationId);
			ChannelInvitation sentInvite = GetSentInvite(invitationRemovedNotification.InvitationId);
			if (receivedInvite == null && sentInvite == null)
			{
				base.ApiLog.LogInfo("HandleInvitationRemoved received request for invitationId {0}, but that invitation did not exist on client.", invitationRemovedNotification.InvitationId);
			}
			if (receivedInvite != null)
			{
				m_battleNet.Party.PartyInvitationDelta(invitationRemovedNotification.ChannelId, receivedInvite, (uint)invitationRemovedNotification.Reason);
			}
			if (sentInvite != null)
			{
				m_battleNet.Party.PartyInvitationDelta(invitationRemovedNotification.ChannelId, sentInvite, (uint)invitationRemovedNotification.Reason);
			}
			m_activeInvitations.Remove(invitationRemovedNotification.InvitationId);
		}

		private void HandleMemberAdded(RPCContext context)
		{
			MemberAddedNotification memberAddedNotification = MemberAddedNotification.ParseFrom(context.Payload);
			if (IsNotificationForMe(memberAddedNotification.SubscriberId, "HandleMemberAdded"))
			{
				if (m_activeChannels.ContainsKey(memberAddedNotification.ChannelId.Id))
				{
					m_activeChannels[memberAddedNotification.ChannelId.Id].MemberCount++;
				}
				m_battleNet.Party.PartyMemberJoined(memberAddedNotification.ChannelId, memberAddedNotification);
			}
		}

		private void HandleMemberAttributeChanged(RPCContext context)
		{
			MemberAttributeChangedNotification memberAttributeChangedNotification = MemberAttributeChangedNotification.ParseFrom(context.Payload);
			if (IsNotificationForMe(memberAttributeChangedNotification.SubscriberId, "HandleMemberAttributeChanged"))
			{
				m_battleNet.Party.MemberAttributesChanged(memberAttributeChangedNotification.ChannelId, memberAttributeChangedNotification);
			}
		}

		private void HandleMemberRemoved(RPCContext context)
		{
			MemberRemovedNotification memberRemovedNotification = MemberRemovedNotification.ParseFrom(context.Payload);
			if (IsNotificationForMe(memberRemovedNotification.SubscriberId, "HandleMemberRemoved"))
			{
				if (m_activeChannels.ContainsKey(memberRemovedNotification.ChannelId.Id))
				{
					m_activeChannels[memberRemovedNotification.ChannelId.Id].MemberCount--;
				}
				m_battleNet.Party.PartyMemberLeft(memberRemovedNotification.ChannelId, memberRemovedNotification);
			}
		}

		private void HandleMemberRoleChanged(RPCContext context)
		{
			MemberRoleChangedNotification memberRoleChangedNotification = MemberRoleChangedNotification.ParseFrom(context.Payload);
			if (IsNotificationForMe(memberRoleChangedNotification.SubscriberId, "HandleMemberRoleChanged"))
			{
				m_battleNet.Party.MemberRolesChanged(memberRoleChangedNotification.ChannelId, memberRoleChangedNotification);
			}
		}

		private void HandlePrivacyLevelChanged(RPCContext context)
		{
			PrivacyLevelChangedNotification privacyLevelChangedNotification = PrivacyLevelChangedNotification.ParseFrom(context.Payload);
			if (IsNotificationForMe(privacyLevelChangedNotification.SubscriberId, "HandlePrivacyLevelChanged"))
			{
				if (m_activeChannels.ContainsKey(privacyLevelChangedNotification.ChannelId.Id))
				{
					m_activeChannels[privacyLevelChangedNotification.ChannelId.Id].PrivacyLevel = privacyLevelChangedNotification.PrivacyLevel;
				}
				m_battleNet.Party.PartyPrivacyChanged(privacyLevelChangedNotification.ChannelId, privacyLevelChangedNotification.PrivacyLevel);
			}
		}

		private void HandleSendMessage(RPCContext context)
		{
			SendMessageNotification sendMessageNotification = SendMessageNotification.ParseFrom(context.Payload);
			if (IsNotificationForMe(sendMessageNotification.SubscriberId, "HandleSendMessage"))
			{
				m_battleNet.Party.PartyMessageReceived(sendMessageNotification.ChannelId, sendMessageNotification);
			}
		}

		private void HandleSuggestionAdded(RPCContext context)
		{
			SuggestionAddedNotification suggestionAddedNotification = SuggestionAddedNotification.ParseFrom(context.Payload);
			if (IsNotificationForMe(suggestionAddedNotification.SubscriberId, "HandleSuggestionAdded"))
			{
				m_receivedInviteRequests.Add(suggestionAddedNotification.Suggestion);
				m_battleNet.Party.ReceivedInviteRequestDelta(suggestionAddedNotification.Suggestion.ChannelId, suggestionAddedNotification.Suggestion, null);
			}
		}

		private void HandleTypingIndicator(RPCContext context)
		{
			TypingIndicatorNotification typingIndicatorNotification = TypingIndicatorNotification.ParseFrom(context.Payload);
			if (IsNotificationForMe(typingIndicatorNotification.SubscriberId, "HandleTypingIndicator"))
			{
				m_battleNet.Party.PartyTypingIndicatorUpdateReceived(typingIndicatorNotification.ChannelId, typingIndicatorNotification);
			}
		}
	}
}
