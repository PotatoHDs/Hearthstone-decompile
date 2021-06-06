using Blizzard.Telemetry.WTCG.Client;
using HearthstoneTelemetry;
using PegasusShared;

public class FlowPerformanceGame : FlowPerformance
{
	public class GameSetupConfig : SetupConfig
	{
		public PegasusShared.GameType GameType;

		public PegasusShared.FormatType FormatType;

		public int BoardId;

		public int ScenarioId;

		public GameSetupConfig()
		{
			FlowType = Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType.GAME;
		}
	}

	private PegasusShared.GameType GameType;

	private PegasusShared.FormatType FormatType;

	private int BoardId;

	private int ScenarioId;

	public string GameUuid { get; set; }

	public FlowPerformanceGame(ITimeProvider timeProvider, ITelemetryClient telemetryClient, GameSetupConfig setupConfig)
		: base(timeProvider, telemetryClient, setupConfig)
	{
		GameType = setupConfig.GameType;
		FormatType = setupConfig.FormatType;
		BoardId = setupConfig.BoardId;
		ScenarioId = setupConfig.ScenarioId;
	}

	protected override void OnStop()
	{
		m_telemetryClient.SendFlowPerformanceGame(GetId(), GameUuid, (Blizzard.Telemetry.WTCG.Client.GameType)GameType, (Blizzard.Telemetry.WTCG.Client.FormatType)FormatType, BoardId, ScenarioId);
	}
}
