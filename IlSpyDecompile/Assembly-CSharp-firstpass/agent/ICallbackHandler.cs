namespace agent
{
	public interface ICallbackHandler
	{
		void OnTelemetry(TelemetryMessage msg);

		void OnPatchOverrideUrlChanged(OverrideUrlChangedMessage msg);

		void OnVersionServiceOverrideUrlChanged(OverrideUrlChangedMessage msg);

		void OnNetworkStatusChangedMessage(NetworkStatusChangedMessage msg);

		void OnDownloadPausedDueToNetworkStatusChange(NetworkStatusChangedMessage msg);

		void OnDownloadResumedDueToNetworkStatusChange(NetworkStatusChangedMessage msg);

		void OnDownloadPausedByUser();

		void OnDownloadResumedByUser();
	}
}
