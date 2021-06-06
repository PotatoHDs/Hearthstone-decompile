using Blizzard.Telemetry.WTCG.Client;
using HearthstoneTelemetry;
using PegasusShared;

public class FlowPerformanceFactory
{
	private ITimeProvider m_timeProvider;

	private ITelemetryClient m_telemetryClient;

	public FlowPerformanceFactory()
	{
		m_timeProvider = new UnityTimeProvider();
		m_telemetryClient = TelemetryManager.Client();
	}

	public FlowPerformance CreatePerformanceFlow(FlowPerformance.SetupConfig setupConfig)
	{
		if (m_telemetryClient == null)
		{
			ITelemetryClient telemetryClient = TelemetryManager.Client();
			if (telemetryClient == null)
			{
				return null;
			}
			m_telemetryClient = telemetryClient;
		}
		FlowPerformance flowPerformance = null;
		switch (setupConfig.FlowType)
		{
		case Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType.SHOP:
			return new FlowPerformanceShop(m_timeProvider, m_telemetryClient, setupConfig as FlowPerformanceShop.ShopSetupConfig);
		case Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType.GAME:
		{
			FlowPerformanceGame.GameSetupConfig gameSetupConfig = setupConfig as FlowPerformanceGame.GameSetupConfig;
			if (gameSetupConfig.GameType == PegasusShared.GameType.GT_BATTLEGROUNDS)
			{
				return new FlowPerformanceBattlegrounds(m_timeProvider, m_telemetryClient, gameSetupConfig);
			}
			return new FlowPerformanceGame(m_timeProvider, m_telemetryClient, gameSetupConfig);
		}
		default:
			return new FlowPerformance(m_timeProvider, m_telemetryClient, setupConfig);
		}
	}
}
