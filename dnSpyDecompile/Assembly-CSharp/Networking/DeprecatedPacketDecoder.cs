using System;
using UnityEngine;

namespace Networking
{
	// Token: 0x02000FB4 RID: 4020
	internal class DeprecatedPacketDecoder : IPacketDecoder
	{
		// Token: 0x0600AFF3 RID: 45043 RVA: 0x0036627B File Offset: 0x0036447B
		public PegasusPacket DecodePacket(PegasusPacket packet)
		{
			Debug.LogWarning("Dropping deprecated packet of type: " + packet.Type);
			return null;
		}
	}
}
