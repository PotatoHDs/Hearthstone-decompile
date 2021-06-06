namespace Hearthstone.Streaming
{
	public enum InterruptionReason
	{
		None,
		Unknown,
		Error,
		Disabled,
		Paused,
		AwaitingWifi,
		DiskFull,
		AgentImpeded,
		Fetching
	}
}
