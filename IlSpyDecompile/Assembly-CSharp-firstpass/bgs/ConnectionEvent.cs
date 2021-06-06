using System;

namespace bgs
{
	public class ConnectionEvent<T> where T : PacketFormat
	{
		public ConnectionEventTypes Type { get; set; }

		public BattleNetErrors Error { get; set; }

		public T Packet { get; set; }

		public Exception Exception { get; set; }
	}
}
