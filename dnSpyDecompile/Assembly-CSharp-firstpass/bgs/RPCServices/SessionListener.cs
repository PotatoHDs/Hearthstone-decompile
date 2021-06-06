using System;
using bnet.protocol.session.v1;

namespace bgs.RPCServices
{
	// Token: 0x0200029B RID: 667
	public class SessionListener : ServiceDescriptor
	{
		// Token: 0x06002611 RID: 9745 RVA: 0x00088174 File Offset: 0x00086374
		public SessionListener() : base("bnet.protocol.session.SessionListener")
		{
			base.Exported = true;
			this.Methods = new MethodDescriptor[5];
			this.Methods[1] = new MethodDescriptor("bnet.protocol.session.SessionListener.OnSessionCreated", 1U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SessionCreatedNotification>));
			this.Methods[2] = new MethodDescriptor("bnet.protocol.session.SessionListener.OnSessionDestroyed", 2U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SessionDestroyedNotification>));
			this.Methods[3] = new MethodDescriptor("bnet.protocol.session.SessionListener.OnSessionUpdated", 3U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SessionUpdatedNotification>));
			this.Methods[4] = new MethodDescriptor("bnet.protocol.session.SessionListener.SessionGameTimeWarning", 4U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<SessionGameTimeWarningNotification>));
		}

		// Token: 0x040010D4 RID: 4308
		public const uint NOTIFY_SESSION_CREATED_ID = 1U;

		// Token: 0x040010D5 RID: 4309
		public const uint NOTIFY_SESSION_DESTROYED_ID = 2U;

		// Token: 0x040010D6 RID: 4310
		public const uint NOTIFY_SESSION_UPDATED_ID = 3U;

		// Token: 0x040010D7 RID: 4311
		public const uint NOTIFY_SESSION_GAME_TIME_WARNING_ID = 4U;
	}
}
