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
	public class GamesAPI : BattleNetAPI
	{
		public class UtilResponse
		{
			public ClientResponse m_response;

			public int m_context;

			public UtilResponse(ClientResponse response, int context)
			{
				m_response = response;
				m_context = context;
			}
		}

		private class CancelGameContext
		{
			private ulong m_gameRequestId;

			public CancelGameContext(ulong gameRequestId)
			{
				m_gameRequestId = gameRequestId;
			}

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
					if (battleNetCSharp.Games.IsMatchmakingPending || (battleNetCSharp.Games.s_currentGameRequestId != 0L && battleNetCSharp.Games.s_currentGameRequestId != m_gameRequestId))
					{
						battleNetCSharp.Games.ApiLog.LogDebug("CancelGameCallback received for id={0} but is not the current gameRequest={1}, ignoring it.", m_gameRequestId, battleNetCSharp.Games.s_currentGameRequestId);
					}
					else if (status != BattleNetErrors.ERROR_GAME_MASTER_CREATION_IN_PROGRESS && status != BattleNetErrors.ERROR_GAME_MASTER_ALREADY_IN_GAME && status != BattleNetErrors.ERROR_GAME_MASTER_INVALID_GAME_REQUEST)
					{
						battleNetCSharp.Games.s_currentGameRequestId = 0uL;
						battleNetCSharp.Games.AddQueueEvent(QueueEvent.Type.QUEUE_CANCEL);
					}
				}
				else
				{
					battleNetCSharp.EnqueueErrorInfo(BnetFeature.Games, BnetFeatureEvent.Games_OnCancelGame, status, context);
				}
			}
		}

		private const int NO_AI_DECK = 0;

		private const bool RANK_NA = true;

		private const bool RANKED = false;

		private const bool UNRANKED = true;

		private Queue<UtilResponse> m_utilPackets = new Queue<UtilResponse>();

		private Queue<QueueEvent> m_queueEvents = new Queue<QueueEvent>();

		private ServiceDescriptor m_gameUtilitiesService = new GameUtilitiesService();

		private ServiceDescriptor m_gameRequestService = new GameRequestService();

		private ServiceDescriptor m_gameRequestListener = new GameRequestListener();

		private ulong s_currentGameRequestId;

		private const ulong INVALID_GAME_REQUEST_ID = 0uL;

		public ServiceDescriptor GameUtilityService => m_gameUtilitiesService;

		public ServiceDescriptor GameRequestService => m_gameRequestService;

		public ServiceDescriptor GameRequestListener => m_gameRequestListener;

		public bool IsMatchmakingPending { get; private set; }

		public GamesAPI(BattleNetCSharp battlenet)
			: base(battlenet, "Games")
		{
		}

		public UtilResponse NextUtilPacket()
		{
			if (m_utilPackets.Count > 0)
			{
				return m_utilPackets.Dequeue();
			}
			return null;
		}

		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
			m_rpcConnection.RegisterServiceMethodListener(m_gameUtilitiesService.Id, 6u, HandleGameUtilityServerRequest);
			m_rpcConnection.RegisterServiceMethodListener(m_gameRequestListener.Id, 1u, HandleOnQueueEntry);
			m_rpcConnection.RegisterServiceMethodListener(m_gameRequestListener.Id, 2u, HandleOnQueueExit);
			m_rpcConnection.RegisterServiceMethodListener(m_gameRequestListener.Id, 3u, HandleOnQueueLeft);
			m_rpcConnection.RegisterServiceMethodListener(m_gameRequestListener.Id, 4u, HandleOnQueueUpdate);
			m_rpcConnection.RegisterServiceMethodListener(m_gameRequestListener.Id, 5u, HandleOnMatchmakingResult);
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void OnDisconnected()
		{
			base.OnDisconnected();
			s_currentGameRequestId = 0uL;
			m_queueEvents.Clear();
			m_utilPackets.Clear();
		}

		private void HandleGameUtilityServerRequest(RPCContext context)
		{
			base.ApiLog.LogDebug("RPC Called: GameUtilityServer");
		}

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
			AddQueueEvent(QueueEvent.Type.QUEUE_DELAY, queueEntryNotification.WaitTimes.MinWait, queueEntryNotification.WaitTimes.MaxWait);
		}

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
			s_currentGameRequestId = 0uL;
			AddQueueEvent(QueueEvent.Type.QUEUE_CANCEL);
		}

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
			}
			else
			{
				base.ApiLog.LogDebug("OnQueueLeft " + queueLeftNotification);
			}
		}

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
			AddQueueEvent(QueueEvent.Type.QUEUE_UPDATE, queueUpdateNotification.WaitTimes.MinWait, queueUpdateNotification.WaitTimes.MaxWait);
		}

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
			s_currentGameRequestId = 0uL;
			base.ApiLog.LogDebug("OnMatchmakingResult " + matchmakingResultNotification.ToHumanReadableString());
			ulong id = matchmakingResultNotification.RequestId.Id;
			BattleNetErrors result = (BattleNetErrors)matchmakingResultNotification.Result;
			if (result != 0)
			{
				if (id == s_currentGameRequestId)
				{
					s_currentGameRequestId = 0uL;
				}
				m_battleNet.EnqueueErrorInfo(BnetFeature.Games, BnetFeatureEvent.Games_OnFindGame, result);
				return;
			}
			_ = matchmakingResultNotification.GameHandle;
			GameServerInfo gameServerInfo = new GameServerInfo();
			gameServerInfo.Address = matchmakingResultNotification.ConnectInfo.Address.Address_;
			gameServerInfo.Port = matchmakingResultNotification.ConnectInfo.Address.Port;
			foreach (bnet.protocol.v2.Attribute attribute in matchmakingResultNotification.ConnectInfo.AttributeList)
			{
				if (attribute.Name.Equals("token") && attribute.Value.HasStringValue)
				{
					gameServerInfo.AuroraPassword = attribute.Value.StringValue;
					continue;
				}
				if (attribute.Name.Equals("version") && attribute.Value.HasStringValue)
				{
					gameServerInfo.Version = attribute.Value.StringValue;
					continue;
				}
				if (attribute.Name.Equals("game") && attribute.Value.HasIntValue)
				{
					gameServerInfo.GameHandle = (uint)attribute.Value.IntValue;
					continue;
				}
				if (attribute.Name.Equals("id") && attribute.Value.HasIntValue)
				{
					gameServerInfo.ClientHandle = (int)attribute.Value.IntValue;
					continue;
				}
				if (attribute.Name.Equals("resumable") && attribute.Value.HasBoolValue)
				{
					gameServerInfo.Resumable = attribute.Value.BoolValue;
					continue;
				}
				if (attribute.Name.Equals("spectator_password") && attribute.Value.HasStringValue)
				{
					gameServerInfo.SpectatorPassword = attribute.Value.StringValue;
					continue;
				}
				StringBuilder stringBuilder = new StringBuilder("Unknown attribute in ConnectInfo : ");
				PrintAttribute(attribute, stringBuilder);
				base.ApiLog.LogInfo(stringBuilder.ToString());
			}
			if (gameServerInfo.Version != BattleNet.GetVersion())
			{
				base.ApiLog.LogWarning("Version mismatched. Client:" + BattleNet.GetVersion() + " Server:" + gameServerInfo.Version);
			}
			AddQueueEvent(QueueEvent.Type.QUEUE_GAME_STARTED, 0, 0, 0, gameServerInfo);
		}

		public void SendUtilPacket(int packetId, UtilSystemId systemId, byte[] bytes, int size, int subID, int context, ulong route)
		{
			ClientRequest clientRequest = CreateClientRequest(packetId, systemId, bytes, route);
			if (m_rpcConnection == null)
			{
				base.ApiLog.LogError("SendUtilPacket could not send, connection not valid : " + clientRequest.ToString());
				return;
			}
			RPCContext rPCContext = m_rpcConnection.QueueRequest(m_gameUtilitiesService, 1u, clientRequest, ClientRequestCallback);
			rPCContext.PacketId = packetId;
			rPCContext.SystemId = systemId;
			rPCContext.Context = context;
		}

		private ClientRequest CreateClientRequest(int type, UtilSystemId systemId, byte[] bs, ulong route)
		{
			ClientRequest clientRequest = new ClientRequest();
			clientRequest.AddAttribute(ProtocolHelper.CreateAttribute("p", bs));
			clientRequest.AddAttribute(ProtocolHelper.CreateAttribute("version", BattleNet.GetVersion()));
			clientRequest.AddAttribute(ProtocolHelper.CreateAttribute("system", BattleNet.GetEnumAsString(systemId)));
			clientRequest.AddAttribute(ProtocolHelper.CreateAttribute("DataVersion", BattleNet.GetDataVersion()));
			if (route != 0L)
			{
				clientRequest.AddAttribute(ProtocolHelper.CreateAttribute("r", route));
			}
			return clientRequest;
		}

		public void QueueMatchmaking(List<bnet.protocol.v2.Attribute> matchmakerAttributesFilter, List<bnet.protocol.v2.Attribute> gameAttributes, params Player[] players)
		{
			if (s_currentGameRequestId != 0L)
			{
				LogAdapter.Log(LogLevel.Warning, "WARNING: QueueMatchmaking called with an active gameRequestId=" + s_currentGameRequestId);
				CancelMatchmaking(s_currentGameRequestId);
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
				PartyInfo[] joinedParties = BnetParty.GetJoinedParties();
				foreach (PartyInfo partyInfo in joinedParties)
				{
					if (partyInfo.Type == PartyType.FRIENDLY_CHALLENGE || partyInfo.Type == PartyType.BATTLEGROUNDS_PARTY)
					{
						queueMatchmakingRequest.SetChannelId(partyInfo.Id.ToChannelId());
						break;
					}
				}
				if (!queueMatchmakingRequest.HasChannelId)
				{
					LogAdapter.Log(LogLevel.Warning, "WARNING: Players attempting to QueueMatchmaking outside of friendly challenge. Required ChannelId not set! gameRequestId=" + s_currentGameRequestId);
				}
			}
			matchmakerAttributesFilter.Insert(0, ProtocolHelper.CreateAttributeV2("version", BattleNet.GetVersion()));
			matchmakerAttributesFilter.Insert(0, ProtocolHelper.CreateAttributeV2("DataVersion", BattleNet.GetDataVersion()));
			queueMatchmakingRequest.Options.SetMatchmakerFilter(new GameMatchmakerFilter());
			queueMatchmakingRequest.Options.MatchmakerFilter.SetAttribute(matchmakerAttributesFilter);
			queueMatchmakingRequest.Options.SetCreationProperties(new GameCreationProperties());
			queueMatchmakingRequest.Options.CreationProperties.SetAttribute(gameAttributes);
			PrintQueueMatchmakingRequest(queueMatchmakingRequest);
			IsMatchmakingPending = true;
			if (m_rpcConnection == null)
			{
				LogAdapter.Log(LogLevel.Error, "QueueMatchmaking called with null RPC Connection");
			}
			else
			{
				m_rpcConnection.QueueRequest(m_gameRequestService, 1u, queueMatchmakingRequest, QueueMatchmakingCallback);
			}
		}

		public void CancelMatchmaking()
		{
			CancelMatchmaking(s_currentGameRequestId);
		}

		private void CancelMatchmaking(ulong gameRequestId)
		{
			CancelMatchmakingRequest cancelMatchmakingRequest = new CancelMatchmakingRequest();
			cancelMatchmakingRequest.SetRequestId(new RequestId());
			cancelMatchmakingRequest.RequestId.Id = gameRequestId;
			CancelGameContext @object = new CancelGameContext(gameRequestId);
			m_rpcConnection.QueueRequest(m_gameRequestService, 3u, cancelMatchmakingRequest, @object.CancelGameCallback);
		}

		public QueueEvent GetQueueEvent()
		{
			QueueEvent result = null;
			lock (m_queueEvents)
			{
				if (m_queueEvents.Count > 0)
				{
					return m_queueEvents.Dequeue();
				}
				return result;
			}
		}

		private void AddQueueEvent(QueueEvent.Type queueType, int minSeconds = 0, int maxSeconds = 0, int bnetError = 0, GameServerInfo gsInfo = null)
		{
			QueueEvent item = new QueueEvent(queueType, minSeconds, maxSeconds, bnetError, gsInfo);
			lock (m_queueEvents)
			{
				m_queueEvents.Enqueue(item);
			}
		}

		private void ClientRequestCallback(RPCContext context)
		{
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != 0)
			{
				m_battleNet.EnqueueErrorInfo(BnetFeature.Games, BnetFeatureEvent.Games_OnClientRequest, status, context);
				return;
			}
			ClientResponse clientResponse = ClientResponse.ParseFrom(context.Payload);
			if (clientResponse.AttributeCount >= 2)
			{
				bnet.protocol.Attribute obj = clientResponse.AttributeList[0];
				bnet.protocol.Attribute attribute2 = clientResponse.AttributeList[1];
				if (!obj.Value.HasIntValue || !attribute2.Value.HasBlobValue)
				{
					base.ApiLog.LogError("Malformed Attribute in Util Packet: incorrect values");
				}
				m_utilPackets.Enqueue(new UtilResponse(clientResponse, context.Context));
			}
			else
			{
				base.ApiLog.LogError("Malformed Attribute in Util Packet: missing values");
			}
		}

		private void QueueMatchmakingCallback(RPCContext context)
		{
			IsMatchmakingPending = false;
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			base.ApiLog.LogDebug("Find Game Callback, status=" + status);
			if (status != 0)
			{
				m_battleNet.EnqueueErrorInfo(BnetFeature.Games, BnetFeatureEvent.Games_OnFindGame, status, context);
				return;
			}
			QueueMatchmakingResponse queueMatchmakingResponse = QueueMatchmakingResponse.ParseFrom(context.Payload);
			if (queueMatchmakingResponse.HasRequestId)
			{
				s_currentGameRequestId = queueMatchmakingResponse.RequestId.Id;
			}
		}

		private void PrintQueueMatchmakingRequest(QueueMatchmakingRequest request)
		{
			StringBuilder stringBuilder = new StringBuilder("QueueMatchmakingRequest: { ");
			if (request.HasOptions)
			{
				if (request.Options.HasMatchmakerFilter)
				{
					stringBuilder.Append("Matchmaker: {");
					PrintAttributes(request.Options.MatchmakerFilter.Attribute, stringBuilder);
					stringBuilder.Append("} ");
				}
				if (request.Options.HasCreationProperties)
				{
					stringBuilder.Append("Creation: {");
					PrintAttributes(request.Options.CreationProperties.Attribute, stringBuilder);
					stringBuilder.Append("} ");
				}
				int playerCount = request.Options.PlayerCount;
				for (int i = 0; i < playerCount; i++)
				{
					Player player = request.Options.Player[i];
					PrintPlayer(player, stringBuilder);
				}
			}
			if (request.HasChannelId)
			{
				stringBuilder.Append("Channel Id: ").Append(request.ChannelId).Append(" ");
			}
			stringBuilder.Append("}");
			base.ApiLog.LogDebug(stringBuilder.ToString());
		}

		private void PrintPlayer(Player player, StringBuilder sb)
		{
			sb.Append("Player: [");
			if (player.HasGameAccount)
			{
				PrintGameAccountHandle(player.GameAccount, sb);
			}
			PrintAttributes(player.Attribute, sb);
		}

		private void PrintGameAccountHandle(GameAccountHandle gameAccountHandle, StringBuilder sb)
		{
			sb.Append("GameAccountHandle: {");
			sb.Append("Id: ").Append(gameAccountHandle.Id).Append(" ");
			sb.Append("Program: ").Append(new FourCC(gameAccountHandle.Program).GetString()).Append(" ");
			sb.Append("Region: ").Append(gameAccountHandle.Region).Append(" ");
			sb.Append("} ");
		}

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

		private void PrintAttributes(List<bnet.protocol.v2.Attribute> attributeList, StringBuilder sb)
		{
			sb.Append("Attributes: [");
			for (int i = 0; i < attributeList.Count; i++)
			{
				bnet.protocol.v2.Attribute attr = attributeList[i];
				PrintAttribute(attr, sb);
			}
			sb.Append("] ");
		}
	}
}
