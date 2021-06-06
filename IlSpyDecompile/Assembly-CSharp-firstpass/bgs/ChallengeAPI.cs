using System.Text;
using bgs.RPCServices;
using bnet.protocol.challenge.v1;

namespace bgs
{
	public class ChallengeAPI : BattleNetAPI
	{
		private ServiceDescriptor m_challengeNotifyService = new ChallengeNotify();

		private ExternalChallenge m_nextExternalChallenge;

		public ServiceDescriptor ChallengeNotifyService => m_challengeNotifyService;

		public ChallengeAPI(BattleNetCSharp battlenet)
			: base(battlenet, "Challenge")
		{
		}

		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
			rpcConnection.RegisterServiceMethodListener(m_challengeNotifyService.Id, 3u, OnExternalChallengeCallback);
			rpcConnection.RegisterServiceMethodListener(m_challengeNotifyService.Id, 4u, OnExternalChallengeResultCallback);
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void OnDisconnected()
		{
			base.OnDisconnected();
		}

		public ExternalChallenge GetNextExternalChallenge()
		{
			ExternalChallenge nextExternalChallenge = m_nextExternalChallenge;
			if (m_nextExternalChallenge != null)
			{
				m_nextExternalChallenge = m_nextExternalChallenge.Next;
			}
			return nextExternalChallenge;
		}

		public bool HasExternalChallenge()
		{
			return m_nextExternalChallenge != null;
		}

		private void OnExternalChallengeCallback(RPCContext context)
		{
			ChallengeExternalRequest challengeExternalRequest = ChallengeExternalRequest.ParseFrom(context.Payload);
			if (!challengeExternalRequest.IsInitialized || !challengeExternalRequest.HasPayload)
			{
				base.ApiLog.LogWarning("Bad ChallengeExternalRequest received IsInitialized={0} HasRequestToken={1} HasPayload={2} HasPayloadType={3}", challengeExternalRequest.IsInitialized, challengeExternalRequest.HasRequestToken, challengeExternalRequest.HasPayload, challengeExternalRequest.HasPayloadType);
				return;
			}
			if (challengeExternalRequest.PayloadType != "web_auth_url")
			{
				base.ApiLog.LogWarning("Received a PayloadType we don't know how to handle PayloadType={0}", challengeExternalRequest.PayloadType);
				return;
			}
			ExternalChallenge externalChallenge = new ExternalChallenge();
			externalChallenge.PayLoadType = challengeExternalRequest.PayloadType;
			externalChallenge.URL = Encoding.ASCII.GetString(challengeExternalRequest.Payload);
			base.ApiLog.LogDebug("Received external challenge PayLoadType={0} URL={1}", externalChallenge.PayLoadType, externalChallenge.URL);
			if (m_nextExternalChallenge == null)
			{
				m_nextExternalChallenge = externalChallenge;
			}
			else
			{
				m_nextExternalChallenge.Next = externalChallenge;
			}
		}

		private void OnExternalChallengeResultCallback(RPCContext context)
		{
		}
	}
}
