namespace bgs
{
	public class BattleNetAPI
	{
		private delegate void LogDelegate(string format, params object[] args);

		private LogDelegate[] m_logDelegates;

		protected BattleNetCSharp m_battleNet;

		protected IRpcConnection m_rpcConnection;

		public BattleNetLogSource m_logSource;

		public BattleNetLogSource ApiLog => m_logSource;

		protected BattleNetAPI(BattleNetCSharp battlenet, string logSourceName)
		{
			m_battleNet = battlenet;
			m_logSource = new BattleNetLogSource(logSourceName);
			m_logDelegates = new LogDelegate[2] { m_logSource.LogDebug, m_logSource.LogError };
		}

		public virtual void Initialize()
		{
		}

		public virtual void InitRPCListeners(IRpcConnection rpcConnection)
		{
			m_rpcConnection = rpcConnection;
		}

		public virtual void OnConnected(BattleNetErrors error)
		{
		}

		public virtual void OnDisconnected()
		{
		}

		public virtual void OnLogon()
		{
		}

		public virtual void OnGameAccountSelected()
		{
		}

		public virtual void Process()
		{
		}

		public bool CheckRPCCallback(string name, RPCContext context)
		{
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			int num = ((status != 0) ? 1 : 0);
			m_logDelegates[num]("Callback invoked, status = {0}, name = {1}, request = {2}", status, string.IsNullOrEmpty(name) ? "<null>" : name, (context.Request != null) ? context.Request.ToString() : "<null>");
			return status == BattleNetErrors.ERROR_OK;
		}
	}
}
