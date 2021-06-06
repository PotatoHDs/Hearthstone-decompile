using System.Collections.Generic;
using bnet.protocol;
using bnet.protocol.notification.v1;

namespace bgs
{
	public class BroadcastAPI : BattleNetAPI
	{
		public delegate void BroadcastCallback(IList<Attribute> AttributeList);

		private List<BroadcastCallback> m_listenerList = new List<BroadcastCallback>();

		public BroadcastAPI(BattleNetCSharp battlenet)
			: base(battlenet, "Broadcast")
		{
		}

		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void OnDisconnected()
		{
			base.OnDisconnected();
		}

		public void RegisterListener(BroadcastCallback cb)
		{
			if (!m_listenerList.Contains(cb))
			{
				m_listenerList.Add(cb);
			}
		}

		public void OnBroadcast(Notification notification)
		{
			foreach (BroadcastCallback listener in m_listenerList)
			{
				listener(notification.AttributeList);
			}
		}
	}
}
