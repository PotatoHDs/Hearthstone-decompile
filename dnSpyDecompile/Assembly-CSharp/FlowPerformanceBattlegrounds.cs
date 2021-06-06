using System;
using HearthstoneTelemetry;

// Token: 0x02000693 RID: 1683
public class FlowPerformanceBattlegrounds : FlowPerformanceGame
{
	// Token: 0x06005E2B RID: 24107 RVA: 0x001EA16A File Offset: 0x001E836A
	public FlowPerformanceBattlegrounds(ITimeProvider timeProvider, ITelemetryClient telemetryClient, FlowPerformanceGame.GameSetupConfig setupConfig) : base(timeProvider, telemetryClient, setupConfig)
	{
		this.m_numberOfRounds = 0;
	}

	// Token: 0x06005E2C RID: 24108 RVA: 0x001EA17C File Offset: 0x001E837C
	public void OnNewRoundStart()
	{
		this.m_numberOfRounds++;
	}

	// Token: 0x06005E2D RID: 24109 RVA: 0x001EA18C File Offset: 0x001E838C
	protected override void OnStop()
	{
		base.OnStop();
		this.m_telemetryClient.SendFlowPerformanceBattlegrounds(base.GetId(), base.GameUuid, this.m_numberOfRounds);
	}

	// Token: 0x04004F71 RID: 20337
	private int m_numberOfRounds;
}
