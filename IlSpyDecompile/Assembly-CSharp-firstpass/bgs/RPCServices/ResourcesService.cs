using bnet.protocol;

namespace bgs.RPCServices
{
	public class ResourcesService : ServiceDescriptor
	{
		public const uint GET_CONTENT_HANDLE = 1u;

		public ResourcesService()
			: base("bnet.protocol.resources.Resources")
		{
			base.Imported = true;
			Methods = new MethodDescriptor[2];
			Methods[1] = new MethodDescriptor("bnet.protocol.resources.Resources.GetContentHandle", 1u, ProtobufUtil.ParseFromGeneric<bnet.protocol.ContentHandle>);
		}
	}
}
