using System;
using System.Collections.Generic;
using System.Linq;
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
	// Token: 0x02000201 RID: 513
	public class ChannelAPI : BattleNetAPI
	{
		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x0006D432 File Offset: 0x0006B632
		public ServiceDescriptor ChannelService
		{
			get
			{
				return this.m_channelService;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x0006D43A File Offset: 0x0006B63A
		public ServiceDescriptor ChannelMembershipService
		{
			get
			{
				return this.m_channelMembershipService;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001F4E RID: 8014 RVA: 0x0006D442 File Offset: 0x0006B642
		public ServiceDescriptor ChannelListener
		{
			get
			{
				return this.m_channelListener;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001F4F RID: 8015 RVA: 0x0006D44A File Offset: 0x0006B64A
		public ServiceDescriptor ChannelMembershipListener
		{
			get
			{
				return this.m_channelMembershipListener;
			}
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x0006D454 File Offset: 0x0006B654
		public ChannelAPI(BattleNetCSharp battlenet) : base(battlenet, "Channel")
		{
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x0006D4C5 File Offset: 0x0006B6C5
		public override void OnDisconnected()
		{
			base.OnDisconnected();
			this.m_activeChannels.Clear();
			Log.Party.Print(LogLevel.Info, "[ChannelAPIv2] All Active Channels have been cleared!");
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x0006D4E8 File Offset: 0x0006B6E8
		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelMembershipListener.Id, 1U, new RPCContextDelegate(this.HandleChannelAdded));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelMembershipListener.Id, 2U, new RPCContextDelegate(this.HandleChannelRemoved));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelMembershipListener.Id, 3U, new RPCContextDelegate(this.HandleReceivedInvitationAdded));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelMembershipListener.Id, 4U, new RPCContextDelegate(this.HandleReceivedInvitationRemoved));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelListener.Id, 16U, new RPCContextDelegate(this.HandleAttributeChanged));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelListener.Id, 18U, new RPCContextDelegate(this.HandleInvitationAdded));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelListener.Id, 19U, new RPCContextDelegate(this.HandleInvitationRemoved));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelListener.Id, 3U, new RPCContextDelegate(this.HandleMemberAdded));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelListener.Id, 5U, new RPCContextDelegate(this.HandleMemberAttributeChanged));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelListener.Id, 4U, new RPCContextDelegate(this.HandleMemberRemoved));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelListener.Id, 6U, new RPCContextDelegate(this.HandleMemberRoleChanged));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelListener.Id, 17U, new RPCContextDelegate(this.HandlePrivacyLevelChanged));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelListener.Id, 10U, new RPCContextDelegate(this.HandleSendMessage));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelListener.Id, 20U, new RPCContextDelegate(this.HandleSuggestionAdded));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_channelListener.Id, 11U, new RPCContextDelegate(this.HandleTypingIndicator));
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x0006D710 File Offset: 0x0006B910
		public void RemoveInviteRequestsFor(ChannelId channelId, GameAccountHandle suggesteeId, uint removeReason)
		{
			if (this.m_receivedInviteRequests == null || suggesteeId == null)
			{
				return;
			}
			List<Suggestion> list = new List<Suggestion>();
			foreach (Suggestion suggestion in this.m_receivedInviteRequests)
			{
				if (suggestion.ChannelId.Equals(channelId) && suggestion.Suggestee.Id.Equals(suggesteeId))
				{
					list.Add(suggestion);
				}
			}
			foreach (Suggestion item in list)
			{
				this.m_receivedInviteRequests.Remove(item);
			}
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x0006D7DC File Offset: 0x0006B9DC
		public ChannelDescription GetChannelDescription(ChannelId channelId)
		{
			ChannelDescription result;
			if (this.m_activeChannels.TryGetValue(channelId.Id, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x0006D804 File Offset: 0x0006BA04
		public ChannelInvitation GetReceivedInvite(ulong invitationId)
		{
			ChannelInvitation result;
			if (this.m_receivedInvitations.TryGetValue(invitationId, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x0006D824 File Offset: 0x0006BA24
		public ChannelInvitation[] GetAllReceivedInvites()
		{
			return this.m_receivedInvitations.Values.ToArray<ChannelInvitation>();
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x0006D838 File Offset: 0x0006BA38
		public Suggestion[] GetReceivedInviteRequestsForChannel(ChannelId channelId)
		{
			return (from i in this.m_receivedInviteRequests
			where i.HasChannelId && i.ChannelId.Equals(channelId)
			select i).ToArray<Suggestion>();
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x0006D870 File Offset: 0x0006BA70
		public ChannelInvitation GetSentInvite(ulong invitationId)
		{
			ChannelInvitation result;
			if (this.m_activeInvitations.TryGetValue(invitationId, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x0006D890 File Offset: 0x0006BA90
		public ChannelInvitation[] GetAllSentInvites()
		{
			return this.m_activeInvitations.Values.ToArray<ChannelInvitation>();
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x0006D8A4 File Offset: 0x0006BAA4
		private bool IsNotificationForMe(GameAccountHandle subscriberId, string sourceMethod)
		{
			GameAccountHandle myGameAccountHandle = this.m_battleNet.GetMyGameAccountHandle();
			if (subscriberId == null)
			{
				base.ApiLog.LogInfo("{0} received request for null subscriberId.", new object[]
				{
					sourceMethod
				});
				return false;
			}
			if (!subscriberId.Equals(myGameAccountHandle))
			{
				base.ApiLog.LogInfo("{0} received request for subscriberId : {1} - {2}, but this is not the current account.", new object[]
				{
					sourceMethod,
					subscriberId.Id,
					subscriberId.Region
				});
				return false;
			}
			return true;
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x0006D920 File Offset: 0x0006BB20
		private void LoggingCallback(RPCContext context, string message, ChannelAPI.ChannelRequestData channelRequestData)
		{
			BattleNetErrors error = (BattleNetErrors)((context == null || context.Header == null) ? 3008U : context.Header.Status);
			this.LoggingCallback_Internal(error, message, channelRequestData);
			if (channelRequestData.callback != null)
			{
				channelRequestData.callback(context);
			}
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x0006D968 File Offset: 0x0006BB68
		private void LoggingCallback_Internal(BattleNetErrors error, string message, ChannelAPI.ChannelRequestData channelRequestData)
		{
			if (error != BattleNetErrors.ERROR_OK)
			{
				if (channelRequestData.channelId != null)
				{
					message = string.Format("ChannelRequestError: {0} {1} {2} channelId=({3}, {4}, {5})", new object[]
					{
						(int)error,
						error.ToString(),
						message,
						channelRequestData.channelId.Id,
						channelRequestData.channelId.Host.Label,
						channelRequestData.channelId.Host.Epoch
					});
				}
				else
				{
					message = string.Format("ChannelRequestError: {0} {1} {2}", (int)error, error.ToString(), message);
				}
				this.m_battleNet.Channel.ApiLog.LogError(message);
				return;
			}
			if (channelRequestData.channelId != null)
			{
				message = string.Format("ChannelRequest {0} status={1} channelId=({2}, {3}, {4})", new object[]
				{
					message,
					error.ToString(),
					channelRequestData.channelId.Id,
					channelRequestData.channelId.Host.Label,
					channelRequestData.channelId.Host.Epoch
				});
			}
			else
			{
				message = string.Format("ChannelRequest {0} status={1}", message, error.ToString());
			}
			this.m_battleNet.Channel.ApiLog.LogDebug(message);
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x0006DAD4 File Offset: 0x0006BCD4
		public void SubscribeMembership()
		{
			bnet.protocol.channel.v2.membership.SubscribeRequest subscribeRequest = new bnet.protocol.channel.v2.membership.SubscribeRequest();
			subscribeRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			this.m_rpcConnection.QueueRequest(this.m_channelMembershipService, 1U, subscribeRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "SubscribeMembership", default(ChannelAPI.ChannelRequestData));
			}, 0U);
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x0006DB1C File Offset: 0x0006BD1C
		public void CreateChannel(CreateChannelOptions options)
		{
			CreateChannelRequest createChannelRequest = new CreateChannelRequest();
			createChannelRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			createChannelRequest.SetOptions(options);
			this.m_rpcConnection.QueueRequest(this.m_channelService, 2U, createChannelRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "CreateChannel", new ChannelAPI.ChannelRequestData(null, new RPCContextDelegate(this.m_battleNet.Party.PartyCreated)));
			}, 0U);
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x0006DB68 File Offset: 0x0006BD68
		public void DissolveChannel(ChannelId channelId)
		{
			DissolveChannelRequest dissolveChannelRequest = new DissolveChannelRequest();
			dissolveChannelRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			dissolveChannelRequest.SetChannelId(channelId);
			this.m_rpcConnection.QueueRequest(this.m_channelService, 3U, dissolveChannelRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "DissolveChannel", new ChannelAPI.ChannelRequestData(channelId, null));
			}, 0U);
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x0006DBD0 File Offset: 0x0006BDD0
		public void GetChannel(ChannelId channelId, RPCContextDelegate callback, ChannelAPI.ChannelInformation[] fetchInformation = null)
		{
			fetchInformation = (fetchInformation ?? new ChannelAPI.ChannelInformation[0]);
			GetChannelRequest getChannelRequest = new GetChannelRequest();
			getChannelRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			getChannelRequest.SetChannelId(channelId);
			getChannelRequest.SetFetchAttributes(fetchInformation.Contains(ChannelAPI.ChannelInformation.ATTRIBUTES));
			getChannelRequest.SetFetchInvitations(fetchInformation.Contains(ChannelAPI.ChannelInformation.INVITATIONS));
			getChannelRequest.SetFetchMembers(fetchInformation.Contains(ChannelAPI.ChannelInformation.MEMBERS));
			getChannelRequest.SetFetchRoles(fetchInformation.Contains(ChannelAPI.ChannelInformation.ROLES));
			this.m_rpcConnection.QueueRequest(this.m_channelService, 4U, getChannelRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "GetChannel", new ChannelAPI.ChannelRequestData(channelId, callback));
			}, 0U);
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x0006DC80 File Offset: 0x0006BE80
		public void GetPublicChannelTypes(UniqueChannelType uniqueChannelType, RPCContextDelegate callback)
		{
			GetPublicChannelTypesRequest getPublicChannelTypesRequest = new GetPublicChannelTypesRequest();
			GetPublicChannelTypesOptions getPublicChannelTypesOptions = new GetPublicChannelTypesOptions();
			getPublicChannelTypesRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			getPublicChannelTypesRequest.SetOptions(getPublicChannelTypesOptions);
			getPublicChannelTypesOptions.SetType(uniqueChannelType);
			this.m_rpcConnection.QueueRequest(this.m_channelService, 5U, getPublicChannelTypesRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "GetPublicChannelTypes", new ChannelAPI.ChannelRequestData(null, callback));
			}, 0U);
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x0006DCF0 File Offset: 0x0006BEF0
		public void FindChannel(FindChannelOptions options)
		{
			FindChannelRequest findChannelRequest = new FindChannelRequest();
			findChannelRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			findChannelRequest.SetOptions(options);
			this.m_rpcConnection.QueueRequest(this.m_channelService, 6U, findChannelRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "FindChannel", default(ChannelAPI.ChannelRequestData));
			}, 0U);
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x0006DD3C File Offset: 0x0006BF3C
		public void Subscribe(ChannelId channelId, RPCContextDelegate callback)
		{
			bnet.protocol.channel.v2.SubscribeRequest subscribeRequest = new bnet.protocol.channel.v2.SubscribeRequest();
			subscribeRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			subscribeRequest.SetChannelId(channelId);
			this.m_rpcConnection.QueueRequest(this.m_channelService, 10U, subscribeRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "Subscribe", new ChannelAPI.ChannelRequestData(channelId, callback));
			}, 0U);
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x0006DDAC File Offset: 0x0006BFAC
		public void Unsubscribe(ChannelId channelId)
		{
			bnet.protocol.channel.v2.UnsubscribeRequest unsubscribeRequest = new bnet.protocol.channel.v2.UnsubscribeRequest();
			unsubscribeRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			unsubscribeRequest.SetChannelId(channelId);
			this.m_rpcConnection.QueueRequest(this.m_channelService, 11U, unsubscribeRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "Unsubscribe", new ChannelAPI.ChannelRequestData(channelId, null));
			}, 0U);
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x0006DE14 File Offset: 0x0006C014
		public void SetChannelAttributes(ChannelId channelId, List<bnet.protocol.v2.Attribute> attributes)
		{
			SetAttributeRequest setAttributeRequest = new SetAttributeRequest();
			setAttributeRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			setAttributeRequest.SetChannelId(channelId);
			setAttributeRequest.SetAttribute(attributes);
			this.m_rpcConnection.QueueRequest(this.m_channelService, 21U, setAttributeRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "SetChannelAttributes", new ChannelAPI.ChannelRequestData(channelId, null));
			}, 0U);
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x0006DE84 File Offset: 0x0006C084
		public void SetPrivacyLevel(ChannelId channelId, PrivacyLevel privacyLevel)
		{
			SetPrivacyLevelRequest setPrivacyLevelRequest = new SetPrivacyLevelRequest();
			setPrivacyLevelRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			setPrivacyLevelRequest.SetChannelId(channelId);
			setPrivacyLevelRequest.SetPrivacyLevel(privacyLevel);
			this.m_rpcConnection.QueueRequest(this.m_channelService, 22U, setPrivacyLevelRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "SetPrivacyLevel", new ChannelAPI.ChannelRequestData(channelId, null));
			}, 0U);
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x0006DEF4 File Offset: 0x0006C0F4
		public void SendMessage(ChannelId channelId, string message, List<bnet.protocol.v2.Attribute> attributes = null)
		{
			SendMessageRequest sendMessageRequest = new SendMessageRequest();
			SendMessageOptions sendMessageOptions = new SendMessageOptions();
			sendMessageRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			sendMessageRequest.SetChannelId(channelId);
			sendMessageRequest.SetOptions(sendMessageOptions);
			sendMessageOptions.SetAttribute(attributes);
			sendMessageOptions.SetContent(message);
			this.m_rpcConnection.QueueRequest(this.m_channelService, 23U, sendMessageRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "SendMessage", new ChannelAPI.ChannelRequestData(channelId, null));
			}, 0U);
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x0006DF78 File Offset: 0x0006C178
		public void SetTypingIndicator(ChannelId channelId, TypingIndicator action)
		{
			SetTypingIndicatorRequest setTypingIndicatorRequest = new SetTypingIndicatorRequest();
			setTypingIndicatorRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			setTypingIndicatorRequest.SetChannelId(channelId);
			setTypingIndicatorRequest.SetAction(action);
			this.m_rpcConnection.QueueRequest(this.m_channelService, 24U, setTypingIndicatorRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "SetTypingIndicator", new ChannelAPI.ChannelRequestData(channelId, null));
			}, 0U);
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x0006DFE8 File Offset: 0x0006C1E8
		public void Join(ChannelId channelId)
		{
			JoinRequest joinRequest = new JoinRequest();
			joinRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			joinRequest.SetChannelId(channelId);
			this.m_rpcConnection.QueueRequest(this.m_channelService, 30U, joinRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "Join", new ChannelAPI.ChannelRequestData(channelId, null));
			}, 0U);
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x0006E050 File Offset: 0x0006C250
		public void Leave(ChannelId channelId)
		{
			LeaveRequest leaveRequest = new LeaveRequest();
			leaveRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			leaveRequest.SetChannelId(channelId);
			this.m_rpcConnection.QueueRequest(this.m_channelService, 31U, leaveRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "Leave", new ChannelAPI.ChannelRequestData(channelId, null));
			}, 0U);
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x0006E0B8 File Offset: 0x0006C2B8
		public void Kick(ChannelId channelId, GameAccountHandle target)
		{
			KickRequest kickRequest = new KickRequest();
			kickRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			kickRequest.SetChannelId(channelId);
			kickRequest.SetTargetId(target);
			this.m_rpcConnection.QueueRequest(this.m_channelService, 32U, kickRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "Kick", new ChannelAPI.ChannelRequestData(channelId, null));
			}, 0U);
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x0006E128 File Offset: 0x0006C328
		public void SetMemberAttribute(ChannelId channelId, AttributeAssignment assignment)
		{
			SetMemberAttributeRequest setMemberAttributeRequest = new SetMemberAttributeRequest();
			setMemberAttributeRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			setMemberAttributeRequest.SetChannelId(channelId);
			setMemberAttributeRequest.SetAssignment(assignment);
			this.m_rpcConnection.QueueRequest(this.m_channelService, 40U, setMemberAttributeRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "SetMemberAttribute", new ChannelAPI.ChannelRequestData(channelId, null));
			}, 0U);
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x0006E198 File Offset: 0x0006C398
		public void AssignRole(ChannelId channelId, GameAccountHandle target, List<uint> roles)
		{
			AssignRoleRequest assignRoleRequest = new AssignRoleRequest();
			RoleAssignment roleAssignment = new RoleAssignment();
			assignRoleRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			assignRoleRequest.SetChannelId(channelId);
			assignRoleRequest.SetAssignment(roleAssignment);
			roleAssignment.SetMemberId(target);
			roleAssignment.SetRole(roles);
			this.m_rpcConnection.QueueRequest(this.m_channelService, 41U, assignRoleRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "AssignRole", new ChannelAPI.ChannelRequestData(channelId, null));
			}, 0U);
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x0006E21C File Offset: 0x0006C41C
		public void UnassignRole(ChannelId channelId, GameAccountHandle target, List<uint> roles)
		{
			UnassignRoleRequest unassignRoleRequest = new UnassignRoleRequest();
			RoleAssignment roleAssignment = new RoleAssignment();
			unassignRoleRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			unassignRoleRequest.SetChannelId(channelId);
			unassignRoleRequest.SetAssignment(roleAssignment);
			roleAssignment.SetMemberId(target);
			roleAssignment.SetRole(roles);
			this.m_rpcConnection.QueueRequest(this.m_channelService, 42U, unassignRoleRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "UnassignRole", new ChannelAPI.ChannelRequestData(channelId, null));
			}, 0U);
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x0006E2A0 File Offset: 0x0006C4A0
		public void SendInvitation(ChannelId channelId, GameAccountHandle target, ChannelSlot slot)
		{
			SendInvitationRequest sendInvitationRequest = new SendInvitationRequest();
			SendInvitationOptions sendInvitationOptions = new SendInvitationOptions();
			sendInvitationRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			sendInvitationRequest.SetOptions(sendInvitationOptions);
			sendInvitationOptions.SetChannelId(channelId);
			sendInvitationOptions.SetTargetId(target);
			sendInvitationOptions.SetSlot(slot);
			this.m_rpcConnection.QueueRequest(this.m_channelService, 50U, sendInvitationRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "SendInvitation", new ChannelAPI.ChannelRequestData(channelId, null));
			}, 0U);
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x0006E324 File Offset: 0x0006C524
		public void AcceptInvitation(ChannelId channelId, ulong invitationId)
		{
			AcceptInvitationRequest acceptInvitationRequest = new AcceptInvitationRequest();
			acceptInvitationRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			acceptInvitationRequest.SetChannelId(channelId);
			acceptInvitationRequest.SetInvitationId(invitationId);
			this.m_rpcConnection.QueueRequest(this.m_channelService, 51U, acceptInvitationRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "AcceptInvitation", new ChannelAPI.ChannelRequestData(channelId, null));
			}, 0U);
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x0006E394 File Offset: 0x0006C594
		public void DeclineInvitation(ChannelId channelId, ulong invitationId)
		{
			DeclineInvitationRequest declineInvitationRequest = new DeclineInvitationRequest();
			declineInvitationRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			declineInvitationRequest.SetChannelId(channelId);
			declineInvitationRequest.SetInvitationId(invitationId);
			this.m_rpcConnection.QueueRequest(this.m_channelService, 52U, declineInvitationRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "DeclineInvitation", new ChannelAPI.ChannelRequestData(channelId, null));
			}, 0U);
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x0006E404 File Offset: 0x0006C604
		public void RevokeInvitation(ChannelId channelId, ulong invitationId)
		{
			RevokeInvitationRequest revokeInvitationRequest = new RevokeInvitationRequest();
			revokeInvitationRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			revokeInvitationRequest.SetChannelId(channelId);
			revokeInvitationRequest.SetInvitationId(invitationId);
			this.m_rpcConnection.QueueRequest(this.m_channelService, 53U, revokeInvitationRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "RevokeInvitation", new ChannelAPI.ChannelRequestData(channelId, null));
			}, 0U);
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x0006E474 File Offset: 0x0006C674
		public void SendSuggestion(ChannelId channelId, GameAccountHandle approver, GameAccountHandle target)
		{
			SendSuggestionRequest sendSuggestionRequest = new SendSuggestionRequest();
			SendSuggestionOptions sendSuggestionOptions = new SendSuggestionOptions();
			sendSuggestionRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			sendSuggestionRequest.SetOptions(sendSuggestionOptions);
			sendSuggestionOptions.SetChannelId(channelId);
			sendSuggestionOptions.SetTargetId(target);
			sendSuggestionOptions.SetApprovalId(approver);
			this.m_rpcConnection.QueueRequest(this.m_channelService, 60U, sendSuggestionRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "SendSuggestion", new ChannelAPI.ChannelRequestData(channelId, null));
			}, 0U);
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x0006E4F8 File Offset: 0x0006C6F8
		public void GetJoinVoiceToken(ChannelId channelId, RPCContextDelegate callback)
		{
			GetJoinVoiceTokenRequest getJoinVoiceTokenRequest = new GetJoinVoiceTokenRequest();
			getJoinVoiceTokenRequest.SetAgentId(this.m_battleNet.GetMyGameAccountHandle());
			getJoinVoiceTokenRequest.SetChannelId(channelId);
			this.m_rpcConnection.QueueRequest(this.m_channelService, 70U, getJoinVoiceTokenRequest, delegate(RPCContext ctx)
			{
				this.LoggingCallback(ctx, "SendSuggestion", new ChannelAPI.ChannelRequestData(channelId, callback));
			}, 0U);
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x0006E568 File Offset: 0x0006C768
		private void HandleChannelAdded(RPCContext context)
		{
			ChannelAddedNotification channelAddedNotification = ChannelAddedNotification.ParseFrom(context.Payload);
			if (!this.IsNotificationForMe(channelAddedNotification.SubscriberId, "HandleChannelAdded"))
			{
				return;
			}
			ChannelDescription channelDescription = channelAddedNotification.Membership;
			try
			{
				this.m_activeChannels.Add(channelDescription.Id.Id, channelDescription);
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
				base.ApiLog.LogException(stringBuilder.ToString(), Array.Empty<object>());
				return;
			}
			this.Subscribe(channelDescription.Id, delegate(RPCContext ctx)
			{
				bnet.protocol.channel.v2.SubscribeResponse subscribeResponse = bnet.protocol.channel.v2.SubscribeResponse.ParseFrom(ctx.Payload);
				if (!subscribeResponse.HasChannel)
				{
					return;
				}
				this.m_battleNet.Party.PartyJoined(channelDescription, subscribeResponse);
				ChannelInvitation[] array;
				this.m_battleNet.Party.GetPartySentChannelInvitations(channelDescription.Id, out array);
				if (array != null)
				{
					foreach (ChannelInvitation channelInvitation in array)
					{
						this.m_activeInvitations[channelInvitation.Id] = channelInvitation;
					}
				}
			});
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x0006E6DC File Offset: 0x0006C8DC
		private void HandleChannelRemoved(RPCContext context)
		{
			ChannelRemovedNotification channelRemovedNotification = ChannelRemovedNotification.ParseFrom(context.Payload);
			if (!this.IsNotificationForMe(channelRemovedNotification.SubscriberId, "HandleChannelRemoved"))
			{
				return;
			}
			this.m_battleNet.Party.PartyLeft(channelRemovedNotification.ChannelId, channelRemovedNotification);
			this.m_activeChannels.Remove(channelRemovedNotification.ChannelId.Id);
			Log.Party.Print(LogLevel.Info, "[ChannelAPIv2] Removing Channel: {0}", new object[]
			{
				channelRemovedNotification.ChannelId.Id
			});
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x0006E760 File Offset: 0x0006C960
		private void HandleReceivedInvitationAdded(RPCContext context)
		{
			ReceivedInvitationAddedNotification receivedInvitationAddedNotification = ReceivedInvitationAddedNotification.ParseFrom(context.Payload);
			if (!this.IsNotificationForMe(receivedInvitationAddedNotification.SubscriberId, "HandleReceivedInvitationAdded"))
			{
				return;
			}
			if (!receivedInvitationAddedNotification.HasInvitation)
			{
				base.ApiLog.LogInfo("HandleReceivedInvitationAdded received request with no invitationId");
				return;
			}
			this.m_receivedInvitations.Add(receivedInvitationAddedNotification.Invitation.Id, receivedInvitationAddedNotification.Invitation);
			this.m_battleNet.Party.ReceivedInvitationAdded(receivedInvitationAddedNotification, receivedInvitationAddedNotification.Invitation);
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x0006E7DC File Offset: 0x0006C9DC
		private void HandleReceivedInvitationRemoved(RPCContext context)
		{
			ReceivedInvitationRemovedNotification receivedInvitationRemovedNotification = ReceivedInvitationRemovedNotification.ParseFrom(context.Payload);
			if (!this.IsNotificationForMe(receivedInvitationRemovedNotification.SubscriberId, "HandleReceivedInvitationRemoved"))
			{
				return;
			}
			if (!receivedInvitationRemovedNotification.HasInvitationId)
			{
				base.ApiLog.LogInfo("HandleReceivedInvitationRemoved received request with no invitationId");
				return;
			}
			ChannelInvitation receivedInvite = this.GetReceivedInvite(receivedInvitationRemovedNotification.InvitationId);
			if (receivedInvite == null)
			{
				base.ApiLog.LogInfo("HandleReceivedInvitationRemoved received request for invitationId {0}, but that invitation did not exist on client.", new object[]
				{
					receivedInvitationRemovedNotification.InvitationId
				});
				return;
			}
			this.m_battleNet.Party.ReceivedInvitationRemoved(receivedInvitationRemovedNotification, receivedInvite);
			this.m_receivedInvitations.Remove(receivedInvitationRemovedNotification.InvitationId);
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x0006E87C File Offset: 0x0006CA7C
		private void HandleAttributeChanged(RPCContext context)
		{
			AttributeChangedNotification notification = AttributeChangedNotification.ParseFrom(context.Payload);
			if (!this.IsNotificationForMe(notification.SubscriberId, "HandleAttributeChanged"))
			{
				return;
			}
			ChannelDescription channelDescription = this.GetChannelDescription(notification.ChannelId);
			if (channelDescription != null)
			{
				foreach (bnet.protocol.v2.Attribute attribute in notification.Attribute)
				{
					this.UpdateAttributeForChannel(channelDescription, attribute);
				}
			}
			ChannelInvitation channelInvitation = this.m_battleNet.Channel.GetAllReceivedInvites().FirstOrDefault((ChannelInvitation i) => i.HasChannel && i.Channel.HasId && i.Channel.Id.Equals(notification.ChannelId));
			if (channelInvitation != null && channelInvitation.HasChannel)
			{
				channelDescription = channelInvitation.Channel;
				foreach (bnet.protocol.v2.Attribute attribute2 in notification.Attribute)
				{
					this.UpdateAttributeForChannel(channelDescription, attribute2);
				}
			}
			this.m_battleNet.Party.PartyAttributeChanged(notification.ChannelId, notification.AttributeList.ToArray());
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x0006E9C8 File Offset: 0x0006CBC8
		private void UpdateAttributeForChannel(ChannelDescription description, bnet.protocol.v2.Attribute attribute)
		{
			bnet.protocol.v2.Attribute attribute2 = description.Attribute.FirstOrDefault((bnet.protocol.v2.Attribute i) => i.HasName && i.Name.Equals(attribute.Name));
			if (attribute2 != null)
			{
				attribute2.Value = attribute.Value;
				return;
			}
			description.AddAttribute(attribute);
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x0006EA1C File Offset: 0x0006CC1C
		private void HandleInvitationAdded(RPCContext context)
		{
			InvitationAddedNotification invitationAddedNotification = InvitationAddedNotification.ParseFrom(context.Payload);
			if (!this.IsNotificationForMe(invitationAddedNotification.SubscriberId, "HandleInvitationAdded"))
			{
				return;
			}
			if (!invitationAddedNotification.HasInvitation)
			{
				base.ApiLog.LogInfo("HandleInvitationAdded received request for with no invitationId");
				return;
			}
			this.m_activeInvitations.Add(invitationAddedNotification.Invitation.Id, invitationAddedNotification.Invitation);
			this.m_battleNet.Party.PartyInvitationDelta(invitationAddedNotification.ChannelId, invitationAddedNotification.Invitation, null);
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x0006EAA4 File Offset: 0x0006CCA4
		private void HandleInvitationRemoved(RPCContext context)
		{
			InvitationRemovedNotification invitationRemovedNotification = InvitationRemovedNotification.ParseFrom(context.Payload);
			if (!this.IsNotificationForMe(invitationRemovedNotification.SubscriberId, "HandleAttributeChanged"))
			{
				return;
			}
			if (!invitationRemovedNotification.HasInvitationId)
			{
				base.ApiLog.LogInfo("HandleInvitationRemoved received request with no invitationId");
				return;
			}
			ChannelInvitation receivedInvite = this.GetReceivedInvite(invitationRemovedNotification.InvitationId);
			ChannelInvitation sentInvite = this.GetSentInvite(invitationRemovedNotification.InvitationId);
			if (receivedInvite == null && sentInvite == null)
			{
				base.ApiLog.LogInfo("HandleInvitationRemoved received request for invitationId {0}, but that invitation did not exist on client.", new object[]
				{
					invitationRemovedNotification.InvitationId
				});
			}
			if (receivedInvite != null)
			{
				this.m_battleNet.Party.PartyInvitationDelta(invitationRemovedNotification.ChannelId, receivedInvite, new uint?((uint)invitationRemovedNotification.Reason));
			}
			if (sentInvite != null)
			{
				this.m_battleNet.Party.PartyInvitationDelta(invitationRemovedNotification.ChannelId, sentInvite, new uint?((uint)invitationRemovedNotification.Reason));
			}
			this.m_activeInvitations.Remove(invitationRemovedNotification.InvitationId);
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x0006EB8C File Offset: 0x0006CD8C
		private void HandleMemberAdded(RPCContext context)
		{
			MemberAddedNotification memberAddedNotification = MemberAddedNotification.ParseFrom(context.Payload);
			if (!this.IsNotificationForMe(memberAddedNotification.SubscriberId, "HandleMemberAdded"))
			{
				return;
			}
			if (this.m_activeChannels.ContainsKey(memberAddedNotification.ChannelId.Id))
			{
				this.m_activeChannels[memberAddedNotification.ChannelId.Id].MemberCount += 1U;
			}
			this.m_battleNet.Party.PartyMemberJoined(memberAddedNotification.ChannelId, memberAddedNotification);
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x0006EC0C File Offset: 0x0006CE0C
		private void HandleMemberAttributeChanged(RPCContext context)
		{
			MemberAttributeChangedNotification memberAttributeChangedNotification = MemberAttributeChangedNotification.ParseFrom(context.Payload);
			if (!this.IsNotificationForMe(memberAttributeChangedNotification.SubscriberId, "HandleMemberAttributeChanged"))
			{
				return;
			}
			this.m_battleNet.Party.MemberAttributesChanged(memberAttributeChangedNotification.ChannelId, memberAttributeChangedNotification);
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x0006EC50 File Offset: 0x0006CE50
		private void HandleMemberRemoved(RPCContext context)
		{
			MemberRemovedNotification memberRemovedNotification = MemberRemovedNotification.ParseFrom(context.Payload);
			if (!this.IsNotificationForMe(memberRemovedNotification.SubscriberId, "HandleMemberRemoved"))
			{
				return;
			}
			if (this.m_activeChannels.ContainsKey(memberRemovedNotification.ChannelId.Id))
			{
				this.m_activeChannels[memberRemovedNotification.ChannelId.Id].MemberCount -= 1U;
			}
			this.m_battleNet.Party.PartyMemberLeft(memberRemovedNotification.ChannelId, memberRemovedNotification);
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x0006ECD0 File Offset: 0x0006CED0
		private void HandleMemberRoleChanged(RPCContext context)
		{
			MemberRoleChangedNotification memberRoleChangedNotification = MemberRoleChangedNotification.ParseFrom(context.Payload);
			if (!this.IsNotificationForMe(memberRoleChangedNotification.SubscriberId, "HandleMemberRoleChanged"))
			{
				return;
			}
			this.m_battleNet.Party.MemberRolesChanged(memberRoleChangedNotification.ChannelId, memberRoleChangedNotification);
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x0006ED14 File Offset: 0x0006CF14
		private void HandlePrivacyLevelChanged(RPCContext context)
		{
			PrivacyLevelChangedNotification privacyLevelChangedNotification = PrivacyLevelChangedNotification.ParseFrom(context.Payload);
			if (!this.IsNotificationForMe(privacyLevelChangedNotification.SubscriberId, "HandlePrivacyLevelChanged"))
			{
				return;
			}
			if (this.m_activeChannels.ContainsKey(privacyLevelChangedNotification.ChannelId.Id))
			{
				this.m_activeChannels[privacyLevelChangedNotification.ChannelId.Id].PrivacyLevel = privacyLevelChangedNotification.PrivacyLevel;
			}
			this.m_battleNet.Party.PartyPrivacyChanged(privacyLevelChangedNotification.ChannelId, privacyLevelChangedNotification.PrivacyLevel);
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x0006ED98 File Offset: 0x0006CF98
		private void HandleSendMessage(RPCContext context)
		{
			SendMessageNotification sendMessageNotification = SendMessageNotification.ParseFrom(context.Payload);
			if (!this.IsNotificationForMe(sendMessageNotification.SubscriberId, "HandleSendMessage"))
			{
				return;
			}
			this.m_battleNet.Party.PartyMessageReceived(sendMessageNotification.ChannelId, sendMessageNotification);
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x0006EDDC File Offset: 0x0006CFDC
		private void HandleSuggestionAdded(RPCContext context)
		{
			SuggestionAddedNotification suggestionAddedNotification = SuggestionAddedNotification.ParseFrom(context.Payload);
			if (!this.IsNotificationForMe(suggestionAddedNotification.SubscriberId, "HandleSuggestionAdded"))
			{
				return;
			}
			this.m_receivedInviteRequests.Add(suggestionAddedNotification.Suggestion);
			this.m_battleNet.Party.ReceivedInviteRequestDelta(suggestionAddedNotification.Suggestion.ChannelId, suggestionAddedNotification.Suggestion, null);
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x0006EE44 File Offset: 0x0006D044
		private void HandleTypingIndicator(RPCContext context)
		{
			TypingIndicatorNotification typingIndicatorNotification = TypingIndicatorNotification.ParseFrom(context.Payload);
			if (!this.IsNotificationForMe(typingIndicatorNotification.SubscriberId, "HandleTypingIndicator"))
			{
				return;
			}
			this.m_battleNet.Party.PartyTypingIndicatorUpdateReceived(typingIndicatorNotification.ChannelId, typingIndicatorNotification);
		}

		// Token: 0x04000B65 RID: 2917
		private Map<uint, ChannelDescription> m_activeChannels = new Map<uint, ChannelDescription>();

		// Token: 0x04000B66 RID: 2918
		private Map<ulong, ChannelInvitation> m_receivedInvitations = new Map<ulong, ChannelInvitation>();

		// Token: 0x04000B67 RID: 2919
		private Map<ulong, ChannelInvitation> m_activeInvitations = new Map<ulong, ChannelInvitation>();

		// Token: 0x04000B68 RID: 2920
		private List<Suggestion> m_receivedInviteRequests = new List<Suggestion>();

		// Token: 0x04000B69 RID: 2921
		private ServiceDescriptor m_channelService = new ChannelServiceV2();

		// Token: 0x04000B6A RID: 2922
		private ServiceDescriptor m_channelMembershipService = new ChannelMembershipService();

		// Token: 0x04000B6B RID: 2923
		private ServiceDescriptor m_channelListener = new ChannelListener();

		// Token: 0x04000B6C RID: 2924
		private ServiceDescriptor m_channelMembershipListener = new ChannelMembershipListener();

		// Token: 0x02000683 RID: 1667
		public enum ChannelInformation
		{
			// Token: 0x040021A1 RID: 8609
			ATTRIBUTES,
			// Token: 0x040021A2 RID: 8610
			INVITATIONS,
			// Token: 0x040021A3 RID: 8611
			MEMBERS,
			// Token: 0x040021A4 RID: 8612
			ROLES
		}

		// Token: 0x02000684 RID: 1668
		public struct ChannelType
		{
			// Token: 0x040021A5 RID: 8613
			public const string FRIENDLY_GAME = "FriendlyGame";

			// Token: 0x040021A6 RID: 8614
			public const string SPECTATOR_PARTY = "SpectatorParty";

			// Token: 0x040021A7 RID: 8615
			public const string BACON_PARTY = "BaconParty";
		}

		// Token: 0x02000685 RID: 1669
		private struct ChannelRequestData
		{
			// Token: 0x060061F2 RID: 25074 RVA: 0x00128026 File Offset: 0x00126226
			public ChannelRequestData(ChannelId channelId = null, RPCContextDelegate callback = null)
			{
				this.callback = callback;
				this.channelId = channelId;
			}

			// Token: 0x040021A8 RID: 8616
			public RPCContextDelegate callback;

			// Token: 0x040021A9 RID: 8617
			public ChannelId channelId;
		}
	}
}
