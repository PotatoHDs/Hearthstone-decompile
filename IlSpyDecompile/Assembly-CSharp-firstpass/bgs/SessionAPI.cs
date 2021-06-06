using bgs.RPCServices;
using bnet.protocol.session.v1;

namespace bgs
{
	public class SessionAPI : BattleNetAPI
	{
		private enum SessionAPIState
		{
			NOT_SET,
			INITIALIZING,
			INITIALIZED,
			FAILED_TO_INITIALIZE
		}

		private SessionAPIState m_state;

		private ServiceDescriptor m_sessionListener = new SessionListener();

		private double m_startTime;

		private const float SESSION_TIMEOUT = 30f;

		public ServiceDescriptor SessionNotifyService => m_sessionListener;

		public bool IsInitialized
		{
			get
			{
				if (m_state == SessionAPIState.INITIALIZED)
				{
					return true;
				}
				if (m_state == SessionAPIState.FAILED_TO_INITIALIZE)
				{
					return true;
				}
				return false;
			}
		}

		public SessionAPI(BattleNetCSharp battlenet)
			: base(battlenet, "Session")
		{
		}

		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
			m_rpcConnection.RegisterServiceMethodListener(m_sessionListener.Id, 1u, HandleSessionNotify_OnSessionCreated);
			m_rpcConnection.RegisterServiceMethodListener(m_sessionListener.Id, 3u, HandleSessionNotify_OnSessionUpdated);
			m_rpcConnection.RegisterServiceMethodListener(m_sessionListener.Id, 2u, HandleSessionNotify_OnSessionDestroyed);
			m_rpcConnection.RegisterServiceMethodListener(m_sessionListener.Id, 4u, HandleSessionNotify_OnSessionGameTimeUpdateWarning);
		}

		public override void Initialize()
		{
			base.Initialize();
			if (m_state != SessionAPIState.INITIALIZING)
			{
				m_state = SessionAPIState.INITIALIZING;
				m_startTime = BattleNet.GetRealTimeSinceStartup();
			}
		}

		public override void Process()
		{
			base.Process();
			if (m_state == SessionAPIState.INITIALIZING && BattleNet.GetRealTimeSinceStartup() - m_startTime >= 30.0)
			{
				m_state = SessionAPIState.FAILED_TO_INITIALIZE;
				base.ApiLog.LogWarning("Battle.net Session API C#: Initialize timed out.");
			}
		}

		private void HandleSessionNotify_OnSessionCreated(RPCContext context)
		{
			if (context?.Payload == null)
			{
				base.ApiLog.LogWarning("HandleSessionNotify_OnSessionCreated invalid context!");
				m_state = SessionAPIState.FAILED_TO_INITIALIZE;
				return;
			}
			SessionCreatedNotification sessionCreatedNotification = SessionCreatedNotification.ParseFrom(context.Payload);
			if (sessionCreatedNotification == null || !sessionCreatedNotification.IsInitialized)
			{
				base.ApiLog.LogWarning("HandleSessionNotify_OnSessionCreated unable to parse response!");
				m_state = SessionAPIState.FAILED_TO_INITIALIZE;
				return;
			}
			BattleNetErrors reason = (BattleNetErrors)sessionCreatedNotification.Reason;
			if (reason != 0)
			{
				m_battleNet.EnqueueErrorInfo(BnetFeature.Bnet, BnetFeatureEvent.Notification_OnCreated, reason, context);
				return;
			}
			base.ApiLog.LogWarning($"HandleSessionNotify_OnSessionCreated, data={sessionCreatedNotification}");
			m_state = SessionAPIState.INITIALIZED;
		}

		private void HandleSessionNotify_OnSessionUpdated(RPCContext context)
		{
			if (context?.Payload == null)
			{
				base.ApiLog.LogWarning("HandleSessionNotify_OnSessionUpdated invalid context!");
				return;
			}
			SessionUpdatedNotification sessionUpdatedNotification = SessionUpdatedNotification.ParseFrom(context.Payload);
			if (sessionUpdatedNotification == null || !sessionUpdatedNotification.IsInitialized)
			{
				base.ApiLog.LogWarning("HandleSessionNotify_OnSessionUpdated unable to parse response!");
			}
			else
			{
				base.ApiLog.LogWarning($"HandleSessionNotify_OnSessionUpdated, data={sessionUpdatedNotification}");
			}
		}

		private void HandleSessionNotify_OnSessionDestroyed(RPCContext context)
		{
			if (context?.Payload == null)
			{
				base.ApiLog.LogWarning("HandleSessionNotify_OnSessionDestroyed invalid context!");
				return;
			}
			SessionDestroyedNotification sessionDestroyedNotification = SessionDestroyedNotification.ParseFrom(context.Payload);
			if (sessionDestroyedNotification == null || !sessionDestroyedNotification.IsInitialized)
			{
				base.ApiLog.LogWarning("HandleSessionNotify_OnSessionDestroyed unable to parse response!");
			}
			else
			{
				base.ApiLog.LogWarning($"HandleSessionNotify_OnSessionDestroyed, data={sessionDestroyedNotification}");
			}
		}

		private void HandleSessionNotify_OnSessionGameTimeUpdateWarning(RPCContext context)
		{
			if (context?.Payload == null)
			{
				base.ApiLog.LogWarning("HandleSessionNotify_OnSessionGameTimeUpdateWarning invalid context!");
				return;
			}
			SessionGameTimeWarningNotification sessionGameTimeWarningNotification = SessionGameTimeWarningNotification.ParseFrom(context.Payload);
			if (sessionGameTimeWarningNotification == null || !sessionGameTimeWarningNotification.IsInitialized)
			{
				base.ApiLog.LogWarning("HandleSessionNotify_OnSessionGameTimeUpdateWarning unable to parse response!");
			}
			else
			{
				base.ApiLog.LogWarning($"HandleSessionNotify_OnSessionGameTimeUpdateWarning, data={sessionGameTimeWarningNotification}");
			}
		}
	}
}
