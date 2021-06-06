using System;
using bnet.protocol;

namespace bgs.RPCServices
{
	// Token: 0x0200029C RID: 668
	public class ResourcesService : ServiceDescriptor
	{
		// Token: 0x06002612 RID: 9746 RVA: 0x0008821B File Offset: 0x0008641B
		public ResourcesService() : base("bnet.protocol.resources.Resources")
		{
			base.Imported = true;
			this.Methods = new MethodDescriptor[2];
			this.Methods[1] = new MethodDescriptor("bnet.protocol.resources.Resources.GetContentHandle", 1U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ContentHandle>));
		}

		// Token: 0x040010D8 RID: 4312
		public const uint GET_CONTENT_HANDLE = 1U;
	}
}
