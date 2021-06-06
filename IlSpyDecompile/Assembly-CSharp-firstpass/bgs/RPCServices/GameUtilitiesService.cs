using bnet.protocol;
using bnet.protocol.game_utilities.v1;

namespace bgs.RPCServices
{
	public class GameUtilitiesService : ServiceDescriptor
	{
		public const uint PROCESS_CLIENT_REQUEST_ID = 1u;

		public const uint PRESENCE_CHANNEL_CREATED_ID = 2u;

		public const uint GET_PLAYER_VARIABLES_ID = 3u;

		public const uint PROCESS_SERVER_REQUEST_ID = 6u;

		public const uint NOTIFY_GAME_ACCT_ONLINE_ID = 7u;

		public const uint NOTIFY_GAME_ACCT_OFFLINE_ID = 8u;

		public GameUtilitiesService()
			: base("bnet.protocol.game_utilities.GameUtilities")
		{
			base.Imported = true;
			Methods = new MethodDescriptor[9];
			Methods[1] = new MethodDescriptor("bnet.protocol.game_utilities.GameUtilities.ProcessClientRequest", 1u, ProtobufUtil.ParseFromGeneric<ClientResponse>);
			Methods[2] = new MethodDescriptor("bnet.protocol.game_utilities.GameUtilities.PresenceChannelCreated", 2u, ProtobufUtil.ParseFromGeneric<NoData>);
			Methods[3] = new MethodDescriptor("bnet.protocol.game_utilities.GameUtilities.GetPlayerVariables", 3u, ProtobufUtil.ParseFromGeneric<GetPlayerVariablesResponse>);
			Methods[6] = new MethodDescriptor("bnet.protocol.game_utilities.GameUtilities.ProcessServerRequest", 6u, ProtobufUtil.ParseFromGeneric<ServerResponse>);
			Methods[7] = new MethodDescriptor("bnet.protocol.game_utilities.GameUtilities.NotifyGameAccountOnline", 7u, ProtobufUtil.ParseFromGeneric<NORESPONSE>);
			Methods[8] = new MethodDescriptor("bnet.protocol.game_utilities.GameUtilities.NotifyGameAccountOffline", 8u, ProtobufUtil.ParseFromGeneric<NORESPONSE>);
		}
	}
}
