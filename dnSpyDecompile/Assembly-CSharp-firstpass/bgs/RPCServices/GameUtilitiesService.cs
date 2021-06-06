using System;
using bnet.protocol;
using bnet.protocol.game_utilities.v1;

namespace bgs.RPCServices
{
	// Token: 0x02000287 RID: 647
	public class GameUtilitiesService : ServiceDescriptor
	{
		// Token: 0x060025FD RID: 9725 RVA: 0x00086E40 File Offset: 0x00085040
		public GameUtilitiesService() : base("bnet.protocol.game_utilities.GameUtilities")
		{
			base.Imported = true;
			this.Methods = new MethodDescriptor[9];
			this.Methods[1] = new MethodDescriptor("bnet.protocol.game_utilities.GameUtilities.ProcessClientRequest", 1U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ClientResponse>));
			this.Methods[2] = new MethodDescriptor("bnet.protocol.game_utilities.GameUtilities.PresenceChannelCreated", 2U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
			this.Methods[3] = new MethodDescriptor("bnet.protocol.game_utilities.GameUtilities.GetPlayerVariables", 3U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<GetPlayerVariablesResponse>));
			this.Methods[6] = new MethodDescriptor("bnet.protocol.game_utilities.GameUtilities.ProcessServerRequest", 6U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ServerResponse>));
			this.Methods[7] = new MethodDescriptor("bnet.protocol.game_utilities.GameUtilities.NotifyGameAccountOnline", 7U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>));
			this.Methods[8] = new MethodDescriptor("bnet.protocol.game_utilities.GameUtilities.NotifyGameAccountOffline", 8U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NORESPONSE>));
		}

		// Token: 0x04001054 RID: 4180
		public const uint PROCESS_CLIENT_REQUEST_ID = 1U;

		// Token: 0x04001055 RID: 4181
		public const uint PRESENCE_CHANNEL_CREATED_ID = 2U;

		// Token: 0x04001056 RID: 4182
		public const uint GET_PLAYER_VARIABLES_ID = 3U;

		// Token: 0x04001057 RID: 4183
		public const uint PROCESS_SERVER_REQUEST_ID = 6U;

		// Token: 0x04001058 RID: 4184
		public const uint NOTIFY_GAME_ACCT_ONLINE_ID = 7U;

		// Token: 0x04001059 RID: 4185
		public const uint NOTIFY_GAME_ACCT_OFFLINE_ID = 8U;
	}
}
