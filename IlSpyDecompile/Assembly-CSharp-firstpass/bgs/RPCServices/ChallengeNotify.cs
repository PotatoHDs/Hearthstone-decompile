using bnet.protocol.challenge.v1;

namespace bgs.RPCServices
{
	public class ChallengeNotify : ServiceDescriptor
	{
		public const uint CHALLENGE_EXTERNAL_REQUEST = 3u;

		public const uint CHALLENGE_EXTERNAL_REQUEST_RESULT = 4u;

		public ChallengeNotify()
			: base("bnet.protocol.challenge.ChallengeNotify")
		{
			base.Exported = true;
			Methods = new MethodDescriptor[5];
			Methods[3] = new MethodDescriptor("bnet.protocol.challenge.ChallengeNotify.OnExternalChallenge", 3u, ProtobufUtil.ParseFromGeneric<ChallengeExternalRequest>);
			Methods[4] = new MethodDescriptor("bnet.protocol.challenge.ChallengeNotify.OnExternalChallengeResult", 4u, ProtobufUtil.ParseFromGeneric<ChallengeExternalResult>);
		}
	}
}
