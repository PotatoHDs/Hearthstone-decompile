using System;

namespace bgs
{
	// Token: 0x020001FB RID: 507
	public class BattleNetAPI
	{
		// Token: 0x06001F1D RID: 7965 RVA: 0x0006CF68 File Offset: 0x0006B168
		protected BattleNetAPI(BattleNetCSharp battlenet, string logSourceName)
		{
			this.m_battleNet = battlenet;
			this.m_logSource = new BattleNetLogSource(logSourceName);
			this.m_logDelegates = new BattleNetAPI.LogDelegate[]
			{
				new BattleNetAPI.LogDelegate(this.m_logSource.LogDebug),
				new BattleNetAPI.LogDelegate(this.m_logSource.LogError)
			};
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x00003FD0 File Offset: 0x000021D0
		public virtual void Initialize()
		{
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x0006CFC2 File Offset: 0x0006B1C2
		public virtual void InitRPCListeners(IRpcConnection rpcConnection)
		{
			this.m_rpcConnection = rpcConnection;
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x00003FD0 File Offset: 0x000021D0
		public virtual void OnConnected(BattleNetErrors error)
		{
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x00003FD0 File Offset: 0x000021D0
		public virtual void OnDisconnected()
		{
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x00003FD0 File Offset: 0x000021D0
		public virtual void OnLogon()
		{
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x00003FD0 File Offset: 0x000021D0
		public virtual void OnGameAccountSelected()
		{
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x00003FD0 File Offset: 0x000021D0
		public virtual void Process()
		{
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001F25 RID: 7973 RVA: 0x0006CFCB File Offset: 0x0006B1CB
		public BattleNetLogSource ApiLog
		{
			get
			{
				return this.m_logSource;
			}
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x0006CFD4 File Offset: 0x0006B1D4
		public bool CheckRPCCallback(string name, RPCContext context)
		{
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			int num = (status == BattleNetErrors.ERROR_OK) ? 0 : 1;
			this.m_logDelegates[num]("Callback invoked, status = {0}, name = {1}, request = {2}", new object[]
			{
				status,
				string.IsNullOrEmpty(name) ? "<null>" : name,
				(context.Request != null) ? context.Request.ToString() : "<null>"
			});
			return status == BattleNetErrors.ERROR_OK;
		}

		// Token: 0x04000B59 RID: 2905
		private BattleNetAPI.LogDelegate[] m_logDelegates;

		// Token: 0x04000B5A RID: 2906
		protected BattleNetCSharp m_battleNet;

		// Token: 0x04000B5B RID: 2907
		protected IRpcConnection m_rpcConnection;

		// Token: 0x04000B5C RID: 2908
		public BattleNetLogSource m_logSource;

		// Token: 0x02000681 RID: 1665
		// (Invoke) Token: 0x060061EB RID: 25067
		private delegate void LogDelegate(string format, params object[] args);
	}
}
