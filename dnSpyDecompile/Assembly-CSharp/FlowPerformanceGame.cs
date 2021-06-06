using System;
using Blizzard.Telemetry.WTCG.Client;
using HearthstoneTelemetry;
using PegasusShared;

// Token: 0x02000695 RID: 1685
public class FlowPerformanceGame : global::FlowPerformance
{
	// Token: 0x17000596 RID: 1430
	// (get) Token: 0x06005E30 RID: 24112 RVA: 0x001EA273 File Offset: 0x001E8473
	// (set) Token: 0x06005E31 RID: 24113 RVA: 0x001EA27B File Offset: 0x001E847B
	public string GameUuid { get; set; }

	// Token: 0x06005E32 RID: 24114 RVA: 0x001EA284 File Offset: 0x001E8484
	public FlowPerformanceGame(ITimeProvider timeProvider, ITelemetryClient telemetryClient, global::FlowPerformanceGame.GameSetupConfig setupConfig) : base(timeProvider, telemetryClient, setupConfig)
	{
		this.GameType = setupConfig.GameType;
		this.FormatType = setupConfig.FormatType;
		this.BoardId = setupConfig.BoardId;
		this.ScenarioId = setupConfig.ScenarioId;
	}

	// Token: 0x06005E33 RID: 24115 RVA: 0x001EA2BF File Offset: 0x001E84BF
	protected override void OnStop()
	{
		this.m_telemetryClient.SendFlowPerformanceGame(base.GetId(), this.GameUuid, (Blizzard.Telemetry.WTCG.Client.GameType)this.GameType, (Blizzard.Telemetry.WTCG.Client.FormatType)this.FormatType, this.BoardId, this.ScenarioId);
	}

	// Token: 0x04004F75 RID: 20341
	private PegasusShared.GameType GameType;

	// Token: 0x04004F76 RID: 20342
	private PegasusShared.FormatType FormatType;

	// Token: 0x04004F77 RID: 20343
	private int BoardId;

	// Token: 0x04004F78 RID: 20344
	private int ScenarioId;

	// Token: 0x020021BF RID: 8639
	public class GameSetupConfig : global::FlowPerformance.SetupConfig
	{
		// Token: 0x060124A6 RID: 74918 RVA: 0x00503D29 File Offset: 0x00501F29
		public GameSetupConfig()
		{
			this.FlowType = Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType.GAME;
		}

		// Token: 0x0400E135 RID: 57653
		public PegasusShared.GameType GameType;

		// Token: 0x0400E136 RID: 57654
		public PegasusShared.FormatType FormatType;

		// Token: 0x0400E137 RID: 57655
		public int BoardId;

		// Token: 0x0400E138 RID: 57656
		public int ScenarioId;
	}
}
