using System;

namespace bgs
{
	// Token: 0x02000227 RID: 551
	public interface IRpcConnectionFactory
	{
		// Token: 0x0600231F RID: 8991
		IRpcConnection CreateRpcConnection(IFileUtil fileUtil, IJsonSerializer jsonSerializer, ISocketEventListener socketEventListener);
	}
}
