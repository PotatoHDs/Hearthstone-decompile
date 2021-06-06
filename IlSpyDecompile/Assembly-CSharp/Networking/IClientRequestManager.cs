using bgs;

namespace Networking
{
	public interface IClientRequestManager
	{
		bool SendClientRequest(int type, IProtoBuf body, ClientRequestManager.ClientRequestConfig clientRequestConfig, RequestPhase requestPhase, int subID);

		void EnsureSubscribedTo(UtilSystemId system);

		void NotifyResponseReceived(PegasusPacket packet);

		void NotifyStartupSequenceComplete();

		bool HasPendingDeliveryPackets();

		int PeekNetClientRequestType();

		ResponseWithRequest GetNextClientRequest();

		void DropNextClientRequest();

		void NotifyLoginSequenceCompleted();

		bool ShouldIgnoreError(BnetErrorInfo errorInfo);

		void ScheduleResubscribe();

		void Terminate();

		void SetDisconnectedFromBattleNet();

		void Update();

		bool HasErrors();
	}
}
