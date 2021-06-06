using System;
using System.Text;
using bgs.RPCServices;
using bnet.protocol.challenge.v1;

namespace bgs
{
	// Token: 0x02000200 RID: 512
	public class ChallengeAPI : BattleNetAPI
	{
		// Token: 0x06001F43 RID: 8003 RVA: 0x0006D27F File Offset: 0x0006B47F
		public ChallengeAPI(BattleNetCSharp battlenet) : base(battlenet, "Challenge")
		{
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001F44 RID: 8004 RVA: 0x0006D298 File Offset: 0x0006B498
		public ServiceDescriptor ChallengeNotifyService
		{
			get
			{
				return this.m_challengeNotifyService;
			}
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x0006D2A0 File Offset: 0x0006B4A0
		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
			rpcConnection.RegisterServiceMethodListener(this.m_challengeNotifyService.Id, 3U, new RPCContextDelegate(this.OnExternalChallengeCallback));
			rpcConnection.RegisterServiceMethodListener(this.m_challengeNotifyService.Id, 4U, new RPCContextDelegate(this.OnExternalChallengeResultCallback));
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x0006C9FD File Offset: 0x0006ABFD
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x0006BFB5 File Offset: 0x0006A1B5
		public override void OnDisconnected()
		{
			base.OnDisconnected();
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x0006D2F0 File Offset: 0x0006B4F0
		public ExternalChallenge GetNextExternalChallenge()
		{
			ExternalChallenge nextExternalChallenge = this.m_nextExternalChallenge;
			if (this.m_nextExternalChallenge != null)
			{
				this.m_nextExternalChallenge = this.m_nextExternalChallenge.Next;
			}
			return nextExternalChallenge;
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x0006D311 File Offset: 0x0006B511
		public bool HasExternalChallenge()
		{
			return this.m_nextExternalChallenge != null;
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x0006D31C File Offset: 0x0006B51C
		private void OnExternalChallengeCallback(RPCContext context)
		{
			ChallengeExternalRequest challengeExternalRequest = ChallengeExternalRequest.ParseFrom(context.Payload);
			if (!challengeExternalRequest.IsInitialized || !challengeExternalRequest.HasPayload)
			{
				base.ApiLog.LogWarning("Bad ChallengeExternalRequest received IsInitialized={0} HasRequestToken={1} HasPayload={2} HasPayloadType={3}", new object[]
				{
					challengeExternalRequest.IsInitialized,
					challengeExternalRequest.HasRequestToken,
					challengeExternalRequest.HasPayload,
					challengeExternalRequest.HasPayloadType
				});
				return;
			}
			if (challengeExternalRequest.PayloadType != "web_auth_url")
			{
				base.ApiLog.LogWarning("Received a PayloadType we don't know how to handle PayloadType={0}", new object[]
				{
					challengeExternalRequest.PayloadType
				});
				return;
			}
			ExternalChallenge externalChallenge = new ExternalChallenge();
			externalChallenge.PayLoadType = challengeExternalRequest.PayloadType;
			externalChallenge.URL = Encoding.ASCII.GetString(challengeExternalRequest.Payload);
			base.ApiLog.LogDebug("Received external challenge PayLoadType={0} URL={1}", new object[]
			{
				externalChallenge.PayLoadType,
				externalChallenge.URL
			});
			if (this.m_nextExternalChallenge == null)
			{
				this.m_nextExternalChallenge = externalChallenge;
				return;
			}
			this.m_nextExternalChallenge.Next = externalChallenge;
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x00003FD0 File Offset: 0x000021D0
		private void OnExternalChallengeResultCallback(RPCContext context)
		{
		}

		// Token: 0x04000B63 RID: 2915
		private ServiceDescriptor m_challengeNotifyService = new ChallengeNotify();

		// Token: 0x04000B64 RID: 2916
		private ExternalChallenge m_nextExternalChallenge;
	}
}
