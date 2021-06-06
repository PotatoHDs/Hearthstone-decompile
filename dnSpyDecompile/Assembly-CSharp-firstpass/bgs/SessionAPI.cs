using System;
using bgs.RPCServices;
using bnet.protocol.session.v1;

namespace bgs
{
	// Token: 0x0200020E RID: 526
	public class SessionAPI : BattleNetAPI
	{
		// Token: 0x0600209D RID: 8349 RVA: 0x00075E08 File Offset: 0x00074008
		public SessionAPI(BattleNetCSharp battlenet) : base(battlenet, "Session")
		{
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x0600209E RID: 8350 RVA: 0x00075E21 File Offset: 0x00074021
		public ServiceDescriptor SessionNotifyService
		{
			get
			{
				return this.m_sessionListener;
			}
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x00075E2C File Offset: 0x0007402C
		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_sessionListener.Id, 1U, new RPCContextDelegate(this.HandleSessionNotify_OnSessionCreated));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_sessionListener.Id, 3U, new RPCContextDelegate(this.HandleSessionNotify_OnSessionUpdated));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_sessionListener.Id, 2U, new RPCContextDelegate(this.HandleSessionNotify_OnSessionDestroyed));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_sessionListener.Id, 4U, new RPCContextDelegate(this.HandleSessionNotify_OnSessionGameTimeUpdateWarning));
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x00075ECC File Offset: 0x000740CC
		public override void Initialize()
		{
			base.Initialize();
			if (this.m_state == SessionAPI.SessionAPIState.INITIALIZING)
			{
				return;
			}
			this.m_state = SessionAPI.SessionAPIState.INITIALIZING;
			this.m_startTime = BattleNet.GetRealTimeSinceStartup();
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x00075EF0 File Offset: 0x000740F0
		public override void Process()
		{
			base.Process();
			if (this.m_state == SessionAPI.SessionAPIState.INITIALIZING && BattleNet.GetRealTimeSinceStartup() - this.m_startTime >= 30.0)
			{
				this.m_state = SessionAPI.SessionAPIState.FAILED_TO_INITIALIZE;
				base.ApiLog.LogWarning("Battle.net Session API C#: Initialize timed out.");
			}
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x00075F30 File Offset: 0x00074130
		private void HandleSessionNotify_OnSessionCreated(RPCContext context)
		{
			if (((context != null) ? context.Payload : null) == null)
			{
				base.ApiLog.LogWarning("HandleSessionNotify_OnSessionCreated invalid context!");
				this.m_state = SessionAPI.SessionAPIState.FAILED_TO_INITIALIZE;
				return;
			}
			SessionCreatedNotification sessionCreatedNotification = SessionCreatedNotification.ParseFrom(context.Payload);
			if (sessionCreatedNotification == null || !sessionCreatedNotification.IsInitialized)
			{
				base.ApiLog.LogWarning("HandleSessionNotify_OnSessionCreated unable to parse response!");
				this.m_state = SessionAPI.SessionAPIState.FAILED_TO_INITIALIZE;
				return;
			}
			BattleNetErrors reason = (BattleNetErrors)sessionCreatedNotification.Reason;
			if (reason != BattleNetErrors.ERROR_OK)
			{
				this.m_battleNet.EnqueueErrorInfo(BnetFeature.Bnet, BnetFeatureEvent.Notification_OnCreated, reason, context);
				return;
			}
			base.ApiLog.LogWarning(string.Format("HandleSessionNotify_OnSessionCreated, data={0}", sessionCreatedNotification));
			this.m_state = SessionAPI.SessionAPIState.INITIALIZED;
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x00075FCC File Offset: 0x000741CC
		private void HandleSessionNotify_OnSessionUpdated(RPCContext context)
		{
			if (((context != null) ? context.Payload : null) == null)
			{
				base.ApiLog.LogWarning("HandleSessionNotify_OnSessionUpdated invalid context!");
				return;
			}
			SessionUpdatedNotification sessionUpdatedNotification = SessionUpdatedNotification.ParseFrom(context.Payload);
			if (sessionUpdatedNotification == null || !sessionUpdatedNotification.IsInitialized)
			{
				base.ApiLog.LogWarning("HandleSessionNotify_OnSessionUpdated unable to parse response!");
				return;
			}
			base.ApiLog.LogWarning(string.Format("HandleSessionNotify_OnSessionUpdated, data={0}", sessionUpdatedNotification));
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x00076038 File Offset: 0x00074238
		private void HandleSessionNotify_OnSessionDestroyed(RPCContext context)
		{
			if (((context != null) ? context.Payload : null) == null)
			{
				base.ApiLog.LogWarning("HandleSessionNotify_OnSessionDestroyed invalid context!");
				return;
			}
			SessionDestroyedNotification sessionDestroyedNotification = SessionDestroyedNotification.ParseFrom(context.Payload);
			if (sessionDestroyedNotification == null || !sessionDestroyedNotification.IsInitialized)
			{
				base.ApiLog.LogWarning("HandleSessionNotify_OnSessionDestroyed unable to parse response!");
				return;
			}
			base.ApiLog.LogWarning(string.Format("HandleSessionNotify_OnSessionDestroyed, data={0}", sessionDestroyedNotification));
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x000760A4 File Offset: 0x000742A4
		private void HandleSessionNotify_OnSessionGameTimeUpdateWarning(RPCContext context)
		{
			if (((context != null) ? context.Payload : null) == null)
			{
				base.ApiLog.LogWarning("HandleSessionNotify_OnSessionGameTimeUpdateWarning invalid context!");
				return;
			}
			SessionGameTimeWarningNotification sessionGameTimeWarningNotification = SessionGameTimeWarningNotification.ParseFrom(context.Payload);
			if (sessionGameTimeWarningNotification == null || !sessionGameTimeWarningNotification.IsInitialized)
			{
				base.ApiLog.LogWarning("HandleSessionNotify_OnSessionGameTimeUpdateWarning unable to parse response!");
				return;
			}
			base.ApiLog.LogWarning(string.Format("HandleSessionNotify_OnSessionGameTimeUpdateWarning, data={0}", sessionGameTimeWarningNotification));
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060020A6 RID: 8358 RVA: 0x0007610E File Offset: 0x0007430E
		public bool IsInitialized
		{
			get
			{
				return this.m_state == SessionAPI.SessionAPIState.INITIALIZED || this.m_state == SessionAPI.SessionAPIState.FAILED_TO_INITIALIZE;
			}
		}

		// Token: 0x04000BB8 RID: 3000
		private SessionAPI.SessionAPIState m_state;

		// Token: 0x04000BB9 RID: 3001
		private ServiceDescriptor m_sessionListener = new SessionListener();

		// Token: 0x04000BBA RID: 3002
		private double m_startTime;

		// Token: 0x04000BBB RID: 3003
		private const float SESSION_TIMEOUT = 30f;

		// Token: 0x020006B7 RID: 1719
		private enum SessionAPIState
		{
			// Token: 0x04002201 RID: 8705
			NOT_SET,
			// Token: 0x04002202 RID: 8706
			INITIALIZING,
			// Token: 0x04002203 RID: 8707
			INITIALIZED,
			// Token: 0x04002204 RID: 8708
			FAILED_TO_INITIALIZE
		}
	}
}
