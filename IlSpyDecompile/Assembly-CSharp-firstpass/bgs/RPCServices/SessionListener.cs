using bnet.protocol.session.v1;

namespace bgs.RPCServices
{
	public class SessionListener : ServiceDescriptor
	{
		public const uint NOTIFY_SESSION_CREATED_ID = 1u;

		public const uint NOTIFY_SESSION_DESTROYED_ID = 2u;

		public const uint NOTIFY_SESSION_UPDATED_ID = 3u;

		public const uint NOTIFY_SESSION_GAME_TIME_WARNING_ID = 4u;

		public SessionListener()
			: base("bnet.protocol.session.SessionListener")
		{
			base.Exported = true;
			Methods = new MethodDescriptor[5];
			Methods[1] = new MethodDescriptor("bnet.protocol.session.SessionListener.OnSessionCreated", 1u, ProtobufUtil.ParseFromGeneric<SessionCreatedNotification>);
			Methods[2] = new MethodDescriptor("bnet.protocol.session.SessionListener.OnSessionDestroyed", 2u, ProtobufUtil.ParseFromGeneric<SessionDestroyedNotification>);
			Methods[3] = new MethodDescriptor("bnet.protocol.session.SessionListener.OnSessionUpdated", 3u, ProtobufUtil.ParseFromGeneric<SessionUpdatedNotification>);
			Methods[4] = new MethodDescriptor("bnet.protocol.session.SessionListener.SessionGameTimeWarning", 4u, ProtobufUtil.ParseFromGeneric<SessionGameTimeWarningNotification>);
		}
	}
}
