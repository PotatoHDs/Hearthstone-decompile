using UnityEngine;

namespace Networking
{
	internal class DeprecatedPacketDecoder : IPacketDecoder
	{
		public PegasusPacket DecodePacket(PegasusPacket packet)
		{
			Debug.LogWarning("Dropping deprecated packet of type: " + packet.Type);
			return null;
		}
	}
}
