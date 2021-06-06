namespace bgs
{
	public interface IRpcConnectionFactory
	{
		IRpcConnection CreateRpcConnection(IFileUtil fileUtil, IJsonSerializer jsonSerializer, ISocketEventListener socketEventListener);
	}
}
