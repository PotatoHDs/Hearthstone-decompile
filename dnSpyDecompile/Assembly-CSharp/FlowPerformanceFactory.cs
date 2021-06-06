using System;
using Blizzard.Telemetry.WTCG.Client;
using HearthstoneTelemetry;
using PegasusShared;

// Token: 0x02000694 RID: 1684
public class FlowPerformanceFactory
{
	// Token: 0x06005E2E RID: 24110 RVA: 0x001EA1B1 File Offset: 0x001E83B1
	public FlowPerformanceFactory()
	{
		this.m_timeProvider = new UnityTimeProvider();
		this.m_telemetryClient = TelemetryManager.Client();
	}

	// Token: 0x06005E2F RID: 24111 RVA: 0x001EA1D0 File Offset: 0x001E83D0
	public global::FlowPerformance CreatePerformanceFlow(global::FlowPerformance.SetupConfig setupConfig)
	{
		if (this.m_telemetryClient == null)
		{
			ITelemetryClient telemetryClient = TelemetryManager.Client();
			if (telemetryClient == null)
			{
				return null;
			}
			this.m_telemetryClient = telemetryClient;
		}
		Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType flowType = setupConfig.FlowType;
		global::FlowPerformance result;
		if (flowType != Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType.SHOP)
		{
			if (flowType != Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType.GAME)
			{
				result = new global::FlowPerformance(this.m_timeProvider, this.m_telemetryClient, setupConfig);
			}
			else
			{
				global::FlowPerformanceGame.GameSetupConfig gameSetupConfig = setupConfig as global::FlowPerformanceGame.GameSetupConfig;
				if (gameSetupConfig.GameType == PegasusShared.GameType.GT_BATTLEGROUNDS)
				{
					result = new global::FlowPerformanceBattlegrounds(this.m_timeProvider, this.m_telemetryClient, gameSetupConfig);
				}
				else
				{
					result = new global::FlowPerformanceGame(this.m_timeProvider, this.m_telemetryClient, gameSetupConfig);
				}
			}
		}
		else
		{
			result = new global::FlowPerformanceShop(this.m_timeProvider, this.m_telemetryClient, setupConfig as global::FlowPerformanceShop.ShopSetupConfig);
		}
		return result;
	}

	// Token: 0x04004F72 RID: 20338
	private ITimeProvider m_timeProvider;

	// Token: 0x04004F73 RID: 20339
	private ITelemetryClient m_telemetryClient;
}
