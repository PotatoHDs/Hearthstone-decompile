using System;
using bnet.protocol.challenge.v1;

namespace bgs.RPCServices
{
	// Token: 0x02000298 RID: 664
	public class ChallengeNotify : ServiceDescriptor
	{
		// Token: 0x0600260E RID: 9742 RVA: 0x00087EC8 File Offset: 0x000860C8
		public ChallengeNotify() : base("bnet.protocol.challenge.ChallengeNotify")
		{
			base.Exported = true;
			this.Methods = new MethodDescriptor[5];
			this.Methods[3] = new MethodDescriptor("bnet.protocol.challenge.ChallengeNotify.OnExternalChallenge", 3U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ChallengeExternalRequest>));
			this.Methods[4] = new MethodDescriptor("bnet.protocol.challenge.ChallengeNotify.OnExternalChallengeResult", 4U, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ChallengeExternalResult>));
		}

		// Token: 0x040010C3 RID: 4291
		public const uint CHALLENGE_EXTERNAL_REQUEST = 3U;

		// Token: 0x040010C4 RID: 4292
		public const uint CHALLENGE_EXTERNAL_REQUEST_RESULT = 4U;
	}
}
