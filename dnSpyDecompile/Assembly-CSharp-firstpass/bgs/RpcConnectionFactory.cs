using System;

namespace bgs
{
	// Token: 0x02000233 RID: 563
	public class RpcConnectionFactory : IRpcConnectionFactory
	{
		// Token: 0x06002392 RID: 9106 RVA: 0x0007D2B0 File Offset: 0x0007B4B0
		public IRpcConnection CreateRpcConnection(IFileUtil fileUtil, IJsonSerializer jsonSerializer, ISocketEventListener socketEventListener)
		{
			return new RPCConnection(fileUtil, jsonSerializer, socketEventListener);
		}
	}
}
