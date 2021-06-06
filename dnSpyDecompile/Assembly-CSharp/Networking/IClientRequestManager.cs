using System;
using bgs;

namespace Networking
{
	// Token: 0x02000FA6 RID: 4006
	public interface IClientRequestManager
	{
		// Token: 0x0600AE34 RID: 44596
		bool SendClientRequest(int type, IProtoBuf body, ClientRequestManager.ClientRequestConfig clientRequestConfig, RequestPhase requestPhase, int subID);

		// Token: 0x0600AE35 RID: 44597
		void EnsureSubscribedTo(UtilSystemId system);

		// Token: 0x0600AE36 RID: 44598
		void NotifyResponseReceived(PegasusPacket packet);

		// Token: 0x0600AE37 RID: 44599
		void NotifyStartupSequenceComplete();

		// Token: 0x0600AE38 RID: 44600
		bool HasPendingDeliveryPackets();

		// Token: 0x0600AE39 RID: 44601
		int PeekNetClientRequestType();

		// Token: 0x0600AE3A RID: 44602
		ResponseWithRequest GetNextClientRequest();

		// Token: 0x0600AE3B RID: 44603
		void DropNextClientRequest();

		// Token: 0x0600AE3C RID: 44604
		void NotifyLoginSequenceCompleted();

		// Token: 0x0600AE3D RID: 44605
		bool ShouldIgnoreError(BnetErrorInfo errorInfo);

		// Token: 0x0600AE3E RID: 44606
		void ScheduleResubscribe();

		// Token: 0x0600AE3F RID: 44607
		void Terminate();

		// Token: 0x0600AE40 RID: 44608
		void SetDisconnectedFromBattleNet();

		// Token: 0x0600AE41 RID: 44609
		void Update();

		// Token: 0x0600AE42 RID: 44610
		bool HasErrors();
	}
}
