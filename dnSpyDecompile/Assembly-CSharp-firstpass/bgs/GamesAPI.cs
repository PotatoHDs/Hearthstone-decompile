using System;
using System.Collections.Generic;
using System.Text;
using bgs.RPCServices;
using bgs.types;
using bnet.protocol;
using bnet.protocol.account.v1;
using bnet.protocol.game_utilities.v1;
using bnet.protocol.matchmaking.v1;
using bnet.protocol.v2;

namespace bgs
{
	// Token: 0x02000203 RID: 515
	public class GamesAPI : BattleNetAPI
	{
		// Token: 0x06001FB3 RID: 8115 RVA: 0x0006FB94 File Offset: 0x0006DD94
		public GamesAPI(BattleNetCSharp battlenet) : base(battlenet, "Games")
		{
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x0006FBE4 File Offset: 0x0006DDE4
		public ServiceDescriptor GameUtilityService
		{
			get
			{
				return this.m_gameUtilitiesService;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001FB5 RID: 8117 RVA: 0x0006FBEC File Offset: 0x0006DDEC
		public ServiceDescriptor GameRequestService
		{
			get
			{
				return this.m_gameRequestService;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x0006FBF4 File Offset: 0x0006DDF4
		public ServiceDescriptor GameRequestListener
		{
			get
			{
				return this.m_gameRequestListener;
			}
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x0006FBFC File Offset: 0x0006DDFC
		public GamesAPI.UtilResponse NextUtilPacket()
		{
			if (this.m_utilPackets.Count > 0)
			{
				return this.m_utilPackets.Dequeue();
			}
			return null;
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x0006FC1C File Offset: 0x0006DE1C
		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_gameUtilitiesService.Id, 6U, new RPCContextDelegate(this.HandleGameUtilityServerRequest));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_gameRequestListener.Id, 1U, new RPCContextDelegate(this.HandleOnQueueEntry));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_gameRequestListener.Id, 2U, new RPCContextDelegate(this.HandleOnQueueExit));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_gameRequestListener.Id, 3U, new RPCContextDelegate(this.HandleOnQueueLeft));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_gameRequestListener.Id, 4U, new RPCContextDelegate(this.HandleOnQueueUpdate));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_gameRequestListener.Id, 5U, new RPCContextDelegate(this.HandleOnMatchmakingResult));
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x0006C9FD File Offset: 0x0006ABFD
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x0006FD02 File Offset: 0x0006DF02
		public override void OnDisconnected()
		{
			base.OnDisconnected();
			this.s_currentGameRequestId = 0UL;
			this.m_queueEvents.Clear();
			this.m_utilPackets.Clear();
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x0006FD28 File Offset: 0x0006DF28
		private void HandleGameUtilityServerRequest(RPCContext context)
		{
			base.ApiLog.LogDebug("RPC Called: GameUtilityServer");
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x0006FD3C File Offset: 0x0006DF3C
		private void HandleOnQueueEntry(RPCContext context)
		{
			if (context == null || context.Payload == null)
			{
				base.ApiLog.LogWarning("HandleOnQueueEntry invalid context!");
				return;
			}
			QueueEntryNotification queueEntryNotification = QueueEntryNotification.ParseFrom(context.Payload);
			if (queueEntryNotification == null || !queueEntryNotification.IsInitialized)
			{
				base.ApiLog.LogWarning("HandleOnQueueEntry unable to parse response!");
				return;
			}
			base.ApiLog.LogDebug("OnQueueEntry " + queueEntryNotification);
			this.AddQueueEvent(QueueEvent.Type.QUEUE_DELAY, queueEntryNotification.WaitTimes.MinWait, queueEntryNotification.WaitTimes.MaxWait, 0, null);
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x0006FDC4 File Offset: 0x0006DFC4
		private void HandleOnQueueExit(RPCContext context)
		{
			if (context == null || context.Payload == null)
			{
				base.ApiLog.LogWarning("HandleOnQueueExit invalid context!");
				return;
			}
			QueueExitNotification queueExitNotification = QueueExitNotification.ParseFrom(context.Payload);
			if (queueExitNotification == null || !queueExitNotification.IsInitialized)
			{
				base.ApiLog.LogWarning("HandleOnQueueExit unable to parse response!");
				return;
			}
			base.ApiLog.LogDebug("OnQueueExit " + queueExitNotification);
			this.s_currentGameRequestId = 0UL;
			this.AddQueueEvent(QueueEvent.Type.QUEUE_CANCEL, 0, 0, 0, null);
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x0006FE40 File Offset: 0x0006E040
		private void HandleOnQueueLeft(RPCContext context)
		{
			if (context == null || context.Payload == null)
			{
				base.ApiLog.LogWarning("HandleOnQueueLeft invalid context!");
				return;
			}
			QueueLeftNotification queueLeftNotification = QueueLeftNotification.ParseFrom(context.Payload);
			if (queueLeftNotification == null || !queueLeftNotification.IsInitialized)
			{
				base.ApiLog.LogWarning("HandleOnQueueLeft unable to parse response!");
				return;
			}
			base.ApiLog.LogDebug("OnQueueLeft " + queueLeftNotification);
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x0006FEA8 File Offset: 0x0006E0A8
		private void HandleOnQueueUpdate(RPCContext context)
		{
			if (context == null || context.Payload == null)
			{
				base.ApiLog.LogWarning("HandleOnQueueUpdate invalid context!");
				return;
			}
			QueueUpdateNotification queueUpdateNotification = QueueUpdateNotification.ParseFrom(context.Payload);
			if (queueUpdateNotification == null || !queueUpdateNotification.IsInitialized)
			{
				base.ApiLog.LogWarning("HandleOnQueueUpdate unable to parse response!");
				return;
			}
			base.ApiLog.LogDebug("OnQueueUpdate " + queueUpdateNotification);
			this.AddQueueEvent(QueueEvent.Type.QUEUE_UPDATE, queueUpdateNotification.WaitTimes.MinWait, queueUpdateNotification.WaitTimes.MaxWait, 0, null);
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x0006FF30 File Offset: 0x0006E130
		private void HandleOnMatchmakingResult(RPCContext context)
		{
			if (context == null || context.Payload == null)
			{
				base.ApiLog.LogWarning("HandleOnMatchmakingResult invalid context!");
				return;
			}
			MatchmakingResultNotification matchmakingResultNotification = MatchmakingResultNotification.ParseFrom(context.Payload);
			if (matchmakingResultNotification == null || !matchmakingResultNotification.IsInitialized)
			{
				base.ApiLog.LogWarning("HandleOnMatchmakingResult unable to parse response!");
				return;
			}
			this.s_currentGameRequestId = 0UL;
			base.ApiLog.LogDebug("OnMatchmakingResult " + matchmakingResultNotification.ToHumanReadableString());
			ulong id = matchmakingResultNotification.RequestId.Id;
			BattleNetErrors result = (BattleNetErrors)matchmakingResultNotification.Result;
			if (result != BattleNetErrors.ERROR_OK)
			{
				if (id == this.s_currentGameRequestId)
				{
					this.s_currentGameRequestId = 0UL;
				}
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Games, BnetFeatureEvent.Games_OnFindGame, result, null);
				return;
			}
			GameHandle gameHandle = matchmakingResultNotification.GameHandle;
			GameServerInfo gameServerInfo = new GameServerInfo();
			gameServerInfo.Address = matchmakingResultNotification.ConnectInfo.Address.Address_;
			gameServerInfo.Port = matchmakingResultNotification.ConnectInfo.Address.Port;
			foreach (bnet.protocol.v2.Attribute attribute in matchmakingResultNotification.ConnectInfo.AttributeList)
			{
				if (attribute.Name.Equals("token") && attribute.Value.HasStringValue)
				{
					gameServerInfo.AuroraPassword = attribute.Value.StringValue;
				}
				else if (attribute.Name.Equals("version") && attribute.Value.HasStringValue)
				{
					gameServerInfo.Version = attribute.Value.StringValue;
				}
				else if (attribute.Name.Equals("game") && attribute.Value.HasIntValue)
				{
					gameServerInfo.GameHandle = (uint)attribute.Value.IntValue;
				}
				else if (attribute.Name.Equals("id") && attribute.Value.HasIntValue)
				{
					gameServerInfo.ClientHandle = (long)((int)attribute.Value.IntValue);
				}
				else if (attribute.Name.Equals("resumable") && attribute.Value.HasBoolValue)
				{
					gameServerInfo.Resumable = attribute.Value.BoolValue;
				}
				else if (attribute.Name.Equals("spectator_password") && attribute.Value.HasStringValue)
				{
					gameServerInfo.SpectatorPassword = attribute.Value.StringValue;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder("Unknown attribute in ConnectInfo : ");
					this.PrintAttribute(attribute, stringBuilder);
					base.ApiLog.LogInfo(stringBuilder.ToString());
				}
			}
			if (gameServerInfo.Version != BattleNet.GetVersion())
			{
				base.ApiLog.LogWarning("Version mismatched. Client:" + BattleNet.GetVersion() + " Server:" + gameServerInfo.Version);
			}
			this.AddQueueEvent(QueueEvent.Type.QUEUE_GAME_STARTED, 0, 0, 0, gameServerInfo);
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x00070228 File Offset: 0x0006E428
		public void SendUtilPacket(int packetId, UtilSystemId systemId, byte[] bytes, int size, int subID, int context, ulong route)
		{
			ClientRequest clientRequest = this.CreateClientRequest(packetId, systemId, bytes, route);
			if (this.m_rpcConnection == null)
			{
				base.ApiLog.LogError("SendUtilPacket could not send, connection not valid : " + clientRequest.ToString());
				return;
			}
			RPCContext rpccontext = this.m_rpcConnection.QueueRequest(this.m_gameUtilitiesService, 1U, clientRequest, new RPCContextDelegate(this.ClientRequestCallback), 0U);
			rpccontext.PacketId = packetId;
			rpccontext.SystemId = systemId;
			rpccontext.Context = context;
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0007029C File Offset: 0x0006E49C
		private ClientRequest CreateClientRequest(int type, UtilSystemId systemId, byte[] bs, ulong route)
		{
			ClientRequest clientRequest = new ClientRequest();
			clientRequest.AddAttribute(ProtocolHelper.CreateAttribute("p", bs));
			clientRequest.AddAttribute(ProtocolHelper.CreateAttribute("version", BattleNet.GetVersion()));
			clientRequest.AddAttribute(ProtocolHelper.CreateAttribute("system", BattleNet.GetEnumAsString<UtilSystemId>(systemId)));
			clientRequest.AddAttribute(ProtocolHelper.CreateAttribute("DataVersion", (long)BattleNet.GetDataVersion()));
			if (route != 0UL)
			{
				clientRequest.AddAttribute(ProtocolHelper.CreateAttribute("r", route));
			}
			return clientRequest;
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001FC3 RID: 8131 RVA: 0x00070318 File Offset: 0x0006E518
		// (set) Token: 0x06001FC4 RID: 8132 RVA: 0x00070320 File Offset: 0x0006E520
		public bool IsMatchmakingPending { get; private set; }

		// Token: 0x06001FC5 RID: 8133 RVA: 0x0007032C File Offset: 0x0006E52C
		public void QueueMatchmaking(List<bnet.protocol.v2.Attribute> matchmakerAttributesFilter, List<bnet.protocol.v2.Attribute> gameAttributes, params Player[] players)
		{
			if (this.s_currentGameRequestId != 0UL)
			{
				LogAdapter.Log(LogLevel.Warning, "WARNING: QueueMatchmaking called with an active gameRequestId=" + this.s_currentGameRequestId, "");
				this.CancelMatchmaking(this.s_currentGameRequestId);
				return;
			}
			QueueMatchmakingRequest queueMatchmakingRequest = new QueueMatchmakingRequest();
			queueMatchmakingRequest.SetOptions(new GameMatchmakingOptions());
			for (int i = 0; i < players.Length; i++)
			{
				queueMatchmakingRequest.Options.AddPlayer(players[i]);
			}
			if (players.Length > 1)
			{
				foreach (PartyInfo partyInfo in BnetParty.GetJoinedParties())
				{
					if (partyInfo.Type == PartyType.FRIENDLY_CHALLENGE || partyInfo.Type == PartyType.BATTLEGROUNDS_PARTY)
					{
						queueMatchmakingRequest.SetChannelId(partyInfo.Id.ToChannelId());
						break;
					}
				}
				if (!queueMatchmakingRequest.HasChannelId)
				{
					LogAdapter.Log(LogLevel.Warning, "WARNING: Players attempting to QueueMatchmaking outside of friendly challenge. Required ChannelId not set! gameRequestId=" + this.s_currentGameRequestId, "");
				}
			}
			matchmakerAttributesFilter.Insert(0, ProtocolHelper.CreateAttributeV2("version", BattleNet.GetVersion()));
			matchmakerAttributesFilter.Insert(0, ProtocolHelper.CreateAttributeV2("DataVersion", (long)BattleNet.GetDataVersion()));
			queueMatchmakingRequest.Options.SetMatchmakerFilter(new GameMatchmakerFilter());
			queueMatchmakingRequest.Options.MatchmakerFilter.SetAttribute(matchmakerAttributesFilter);
			queueMatchmakingRequest.Options.SetCreationProperties(new GameCreationProperties());
			queueMatchmakingRequest.Options.CreationProperties.SetAttribute(gameAttributes);
			this.PrintQueueMatchmakingRequest(queueMatchmakingRequest);
			this.IsMatchmakingPending = true;
			if (this.m_rpcConnection == null)
			{
				LogAdapter.Log(LogLevel.Error, "QueueMatchmaking called with null RPC Connection", "");
				return;
			}
			this.m_rpcConnection.QueueRequest(this.m_gameRequestService, 1U, queueMatchmakingRequest, new RPCContextDelegate(this.QueueMatchmakingCallback), 0U);
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x000704C1 File Offset: 0x0006E6C1
		public void CancelMatchmaking()
		{
			this.CancelMatchmaking(this.s_currentGameRequestId);
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x000704D0 File Offset: 0x0006E6D0
		private void CancelMatchmaking(ulong gameRequestId)
		{
			CancelMatchmakingRequest cancelMatchmakingRequest = new CancelMatchmakingRequest();
			cancelMatchmakingRequest.SetRequestId(new RequestId());
			cancelMatchmakingRequest.RequestId.Id = gameRequestId;
			GamesAPI.CancelGameContext @object = new GamesAPI.CancelGameContext(gameRequestId);
			this.m_rpcConnection.QueueRequest(this.m_gameRequestService, 3U, cancelMatchmakingRequest, new RPCContextDelegate(@object.CancelGameCallback), 0U);
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x00070524 File Offset: 0x0006E724
		public QueueEvent GetQueueEvent()
		{
			QueueEvent result = null;
			Queue<QueueEvent> queueEvents = this.m_queueEvents;
			lock (queueEvents)
			{
				if (this.m_queueEvents.Count > 0)
				{
					result = this.m_queueEvents.Dequeue();
				}
			}
			return result;
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x0007057C File Offset: 0x0006E77C
		private void AddQueueEvent(QueueEvent.Type queueType, int minSeconds = 0, int maxSeconds = 0, int bnetError = 0, GameServerInfo gsInfo = null)
		{
			QueueEvent item = new QueueEvent(queueType, minSeconds, maxSeconds, bnetError, gsInfo);
			Queue<QueueEvent> queueEvents = this.m_queueEvents;
			lock (queueEvents)
			{
				this.m_queueEvents.Enqueue(item);
			}
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x000705D0 File Offset: 0x0006E7D0
		private void ClientRequestCallback(RPCContext context)
		{
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != BattleNetErrors.ERROR_OK)
			{
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Games, BnetFeatureEvent.Games_OnClientRequest, status, context);
				return;
			}
			ClientResponse clientResponse = ClientResponse.ParseFrom(context.Payload);
			if (clientResponse.AttributeCount >= 2)
			{
				bnet.protocol.Attribute attribute = clientResponse.AttributeList[0];
				bnet.protocol.Attribute attribute2 = clientResponse.AttributeList[1];
				if (!attribute.Value.HasIntValue || !attribute2.Value.HasBlobValue)
				{
					base.ApiLog.LogError("Malformed Attribute in Util Packet: incorrect values");
				}
				this.m_utilPackets.Enqueue(new GamesAPI.UtilResponse(clientResponse, context.Context));
				return;
			}
			base.ApiLog.LogError("Malformed Attribute in Util Packet: missing values");
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x0007067C File Offset: 0x0006E87C
		private void QueueMatchmakingCallback(RPCContext context)
		{
			this.IsMatchmakingPending = false;
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			base.ApiLog.LogDebug("Find Game Callback, status=" + status);
			if (status != BattleNetErrors.ERROR_OK)
			{
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Games, BnetFeatureEvent.Games_OnFindGame, status, context);
				return;
			}
			QueueMatchmakingResponse queueMatchmakingResponse = QueueMatchmakingResponse.ParseFrom(context.Payload);
			if (queueMatchmakingResponse.HasRequestId)
			{
				this.s_currentGameRequestId = queueMatchmakingResponse.RequestId.Id;
			}
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x000706F0 File Offset: 0x0006E8F0
		private void PrintQueueMatchmakingRequest(QueueMatchmakingRequest request)
		{
			StringBuilder stringBuilder = new StringBuilder("QueueMatchmakingRequest: { ");
			if (request.HasOptions)
			{
				if (request.Options.HasMatchmakerFilter)
				{
					stringBuilder.Append("Matchmaker: {");
					this.PrintAttributes(request.Options.MatchmakerFilter.Attribute, stringBuilder);
					stringBuilder.Append("} ");
				}
				if (request.Options.HasCreationProperties)
				{
					stringBuilder.Append("Creation: {");
					this.PrintAttributes(request.Options.CreationProperties.Attribute, stringBuilder);
					stringBuilder.Append("} ");
				}
				int playerCount = request.Options.PlayerCount;
				for (int i = 0; i < playerCount; i++)
				{
					Player player = request.Options.Player[i];
					this.PrintPlayer(player, stringBuilder);
				}
			}
			if (request.HasChannelId)
			{
				stringBuilder.Append("Channel Id: ").Append(request.ChannelId).Append(" ");
			}
			stringBuilder.Append("}");
			base.ApiLog.LogDebug(stringBuilder.ToString());
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x00070803 File Offset: 0x0006EA03
		private void PrintPlayer(Player player, StringBuilder sb)
		{
			sb.Append("Player: [");
			if (player.HasGameAccount)
			{
				this.PrintGameAccountHandle(player.GameAccount, sb);
			}
			this.PrintAttributes(player.Attribute, sb);
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x00070834 File Offset: 0x0006EA34
		private void PrintGameAccountHandle(GameAccountHandle gameAccountHandle, StringBuilder sb)
		{
			sb.Append("GameAccountHandle: {");
			sb.Append("Id: ").Append(gameAccountHandle.Id).Append(" ");
			sb.Append("Program: ").Append(new FourCC(gameAccountHandle.Program).GetString()).Append(" ");
			sb.Append("Region: ").Append(gameAccountHandle.Region).Append(" ");
			sb.Append("} ");
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x000708C8 File Offset: 0x0006EAC8
		private void PrintAttribute(bnet.protocol.v2.Attribute attr, StringBuilder sb)
		{
			sb.Append("Name: ").Append(attr.Name).Append(" ");
			sb.Append("Value: ");
			if (attr.Value.HasBoolValue)
			{
				sb.Append(attr.Value.BoolValue);
			}
			else if (attr.Value.HasIntValue)
			{
				sb.Append(attr.Value.IntValue);
			}
			else if (attr.Value.HasFloatValue)
			{
				sb.Append(attr.Value.FloatValue);
			}
			else if (attr.Value.HasStringValue)
			{
				sb.Append(attr.Value.StringValue);
			}
			else if (attr.Value.HasBlobValue)
			{
				sb.Append(attr.Value.BlobValue);
			}
			else if (attr.Value.HasUintValue)
			{
				sb.Append(attr.Value.UintValue);
			}
			sb.Append(" ");
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x000709D8 File Offset: 0x0006EBD8
		private void PrintAttributes(List<bnet.protocol.v2.Attribute> attributeList, StringBuilder sb)
		{
			sb.Append("Attributes: [");
			for (int i = 0; i < attributeList.Count; i++)
			{
				bnet.protocol.v2.Attribute attr = attributeList[i];
				this.PrintAttribute(attr, sb);
			}
			sb.Append("] ");
		}

		// Token: 0x04000B78 RID: 2936
		private const int NO_AI_DECK = 0;

		// Token: 0x04000B79 RID: 2937
		private const bool RANK_NA = true;

		// Token: 0x04000B7A RID: 2938
		private const bool RANKED = false;

		// Token: 0x04000B7B RID: 2939
		private const bool UNRANKED = true;

		// Token: 0x04000B7C RID: 2940
		private Queue<GamesAPI.UtilResponse> m_utilPackets = new Queue<GamesAPI.UtilResponse>();

		// Token: 0x04000B7D RID: 2941
		private Queue<QueueEvent> m_queueEvents = new Queue<QueueEvent>();

		// Token: 0x04000B7E RID: 2942
		private ServiceDescriptor m_gameUtilitiesService = new GameUtilitiesService();

		// Token: 0x04000B7F RID: 2943
		private ServiceDescriptor m_gameRequestService = new GameRequestService();

		// Token: 0x04000B80 RID: 2944
		private ServiceDescriptor m_gameRequestListener = new GameRequestListener();

		// Token: 0x04000B81 RID: 2945
		private ulong s_currentGameRequestId;

		// Token: 0x04000B82 RID: 2946
		private const ulong INVALID_GAME_REQUEST_ID = 0UL;

		// Token: 0x020006A1 RID: 1697
		public class UtilResponse
		{
			// Token: 0x06006225 RID: 25125 RVA: 0x001283D7 File Offset: 0x001265D7
			public UtilResponse(ClientResponse response, int context)
			{
				this.m_response = response;
				this.m_context = context;
			}

			// Token: 0x040021E6 RID: 8678
			public ClientResponse m_response;

			// Token: 0x040021E7 RID: 8679
			public int m_context;
		}

		// Token: 0x020006A2 RID: 1698
		private class CancelGameContext
		{
			// Token: 0x06006226 RID: 25126 RVA: 0x001283ED File Offset: 0x001265ED
			public CancelGameContext(ulong gameRequestId)
			{
				this.m_gameRequestId = gameRequestId;
			}

			// Token: 0x06006227 RID: 25127 RVA: 0x001283FC File Offset: 0x001265FC
			public void CancelGameCallback(RPCContext context)
			{
				BattleNetCSharp battleNetCSharp = BattleNet.Get() as BattleNetCSharp;
				if (battleNetCSharp == null || battleNetCSharp.Games == null)
				{
					return;
				}
				BattleNetErrors status = (BattleNetErrors)context.Header.Status;
				battleNetCSharp.Games.ApiLog.LogDebug("CancelGameCallback, status=" + status);
				if (status == BattleNetErrors.ERROR_OK || status == BattleNetErrors.ERROR_GAME_MASTER_CREATION_IN_PROGRESS || status == BattleNetErrors.ERROR_GAME_MASTER_INVALID_GAME || status == BattleNetErrors.ERROR_GAME_MASTER_INVALID_GAME_REQUEST || status == BattleNetErrors.ERROR_GAME_MASTER_ALREADY_IN_GAME)
				{
					if (battleNetCSharp.Games.IsMatchmakingPending || (battleNetCSharp.Games.s_currentGameRequestId != 0UL && battleNetCSharp.Games.s_currentGameRequestId != this.m_gameRequestId))
					{
						battleNetCSharp.Games.ApiLog.LogDebug("CancelGameCallback received for id={0} but is not the current gameRequest={1}, ignoring it.", new object[]
						{
							this.m_gameRequestId,
							battleNetCSharp.Games.s_currentGameRequestId
						});
						return;
					}
					if (status != BattleNetErrors.ERROR_GAME_MASTER_CREATION_IN_PROGRESS && status != BattleNetErrors.ERROR_GAME_MASTER_ALREADY_IN_GAME && status != BattleNetErrors.ERROR_GAME_MASTER_INVALID_GAME_REQUEST)
					{
						battleNetCSharp.Games.s_currentGameRequestId = 0UL;
						battleNetCSharp.Games.AddQueueEvent(QueueEvent.Type.QUEUE_CANCEL, 0, 0, 0, null);
						return;
					}
				}
				else
				{
					battleNetCSharp.EnqueueErrorInfo(BnetFeature.Games, BnetFeatureEvent.Games_OnCancelGame, status, context);
				}
			}

			// Token: 0x040021E8 RID: 8680
			private ulong m_gameRequestId;
		}
	}
}
