using System;

namespace agent
{
	// Token: 0x020001F8 RID: 504
	public interface ICallbackHandler
	{
		// Token: 0x06001EDD RID: 7901
		void OnTelemetry(TelemetryMessage msg);

		// Token: 0x06001EDE RID: 7902
		void OnPatchOverrideUrlChanged(OverrideUrlChangedMessage msg);

		// Token: 0x06001EDF RID: 7903
		void OnVersionServiceOverrideUrlChanged(OverrideUrlChangedMessage msg);

		// Token: 0x06001EE0 RID: 7904
		void OnNetworkStatusChangedMessage(NetworkStatusChangedMessage msg);

		// Token: 0x06001EE1 RID: 7905
		void OnDownloadPausedDueToNetworkStatusChange(NetworkStatusChangedMessage msg);

		// Token: 0x06001EE2 RID: 7906
		void OnDownloadResumedDueToNetworkStatusChange(NetworkStatusChangedMessage msg);

		// Token: 0x06001EE3 RID: 7907
		void OnDownloadPausedByUser();

		// Token: 0x06001EE4 RID: 7908
		void OnDownloadResumedByUser();
	}
}
