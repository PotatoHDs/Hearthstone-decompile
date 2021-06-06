namespace bgs
{
	public class RpcConnectionFactory : IRpcConnectionFactory
	{
		public IRpcConnection CreateRpcConnection(IFileUtil fileUtil, IJsonSerializer jsonSerializer, ISocketEventListener socketEventListener)
		{
			return new RPCConnection(fileUtil, jsonSerializer, socketEventListener);
		}
	}
}
