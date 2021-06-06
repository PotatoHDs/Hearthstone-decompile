using HearthstoneTelemetry;

public class FlowPerformanceBattlegrounds : FlowPerformanceGame
{
	private int m_numberOfRounds;

	public FlowPerformanceBattlegrounds(ITimeProvider timeProvider, ITelemetryClient telemetryClient, GameSetupConfig setupConfig)
		: base(timeProvider, telemetryClient, setupConfig)
	{
		m_numberOfRounds = 0;
	}

	public void OnNewRoundStart()
	{
		m_numberOfRounds++;
	}

	protected override void OnStop()
	{
		base.OnStop();
		m_telemetryClient.SendFlowPerformanceBattlegrounds(GetId(), base.GameUuid, m_numberOfRounds);
	}
}
