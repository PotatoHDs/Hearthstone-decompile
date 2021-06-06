using System;
using Blizzard.Telemetry.WTCG.Client;
using HearthstoneTelemetry;
using UnityEngine;

// Token: 0x02000692 RID: 1682
public class FlowPerformance
{
	// Token: 0x17000593 RID: 1427
	// (get) Token: 0x06005E15 RID: 24085 RVA: 0x001E9D83 File Offset: 0x001E7F83
	public Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType FlowType { get; }

	// Token: 0x17000594 RID: 1428
	// (get) Token: 0x06005E16 RID: 24086 RVA: 0x001E9D8B File Offset: 0x001E7F8B
	// (set) Token: 0x06005E17 RID: 24087 RVA: 0x001E9D93 File Offset: 0x001E7F93
	public bool IsActive { get; private set; }

	// Token: 0x17000595 RID: 1429
	// (get) Token: 0x06005E18 RID: 24088 RVA: 0x001E9D9C File Offset: 0x001E7F9C
	// (set) Token: 0x06005E19 RID: 24089 RVA: 0x001E9DA4 File Offset: 0x001E7FA4
	public bool IsPaused { get; private set; }

	// Token: 0x06005E1A RID: 24090 RVA: 0x001E9DAD File Offset: 0x001E7FAD
	public FlowPerformance(ITimeProvider timeProvider, ITelemetryClient telemetryClient, global::FlowPerformance.SetupConfig setupConfig)
	{
		this.m_timeProvider = timeProvider;
		this.m_telemetryClient = telemetryClient;
		this.FlowType = setupConfig.FlowType;
		this.IsActive = false;
	}

	// Token: 0x06005E1B RID: 24091 RVA: 0x001E9DD8 File Offset: 0x001E7FD8
	public void Start()
	{
		Log.FlowPerformance.PrintDebug("Starting flow: {0}", new object[]
		{
			this.FlowType
		});
		this.m_id = Guid.NewGuid();
		this.IsActive = true;
		this.m_hasAverage = false;
		this.m_startingFlowTime = this.m_timeProvider.TimeSinceStartup;
		this.m_frameCount = 0;
		this.m_totalTriggeredThreshold = 0;
		this.m_totalTimeUnderThreshold = 0f;
		this.m_averageTimeBelowThreshold = 0f;
		this.m_maxTimeBelowThreshold = 0f;
		this.m_thresholdActive = false;
		this.m_totalPausedTime = 0f;
		this.OnStart();
	}

	// Token: 0x06005E1C RID: 24092 RVA: 0x001E9E7C File Offset: 0x001E807C
	public void Update()
	{
		if (this.IsPaused)
		{
			return;
		}
		this.m_frameCount++;
		float currentFps = this.CalculateFps();
		this.IncrementAverageFps(currentFps);
		this.UpdateThresholdValues(currentFps);
		this.OnUpdate();
	}

	// Token: 0x06005E1D RID: 24093 RVA: 0x001E9EBC File Offset: 0x001E80BC
	public void Pause()
	{
		Log.FlowPerformance.PrintDebug("Pausing flow: {0}", new object[]
		{
			this.FlowType
		});
		if (!this.IsActive)
		{
			return;
		}
		this.IsPaused = true;
		this.m_pauseStartTime = this.m_timeProvider.TimeSinceStartup;
		this.OnPause();
	}

	// Token: 0x06005E1E RID: 24094 RVA: 0x001E9F14 File Offset: 0x001E8114
	public void Resume()
	{
		Log.FlowPerformance.PrintDebug("Resuming flow: {0}", new object[]
		{
			this.FlowType
		});
		if (!this.IsActive || !this.IsPaused)
		{
			return;
		}
		float num = this.m_timeProvider.TimeSinceStartup - this.m_pauseStartTime;
		this.IsPaused = false;
		this.m_totalPausedTime += num;
		this.OnResume();
	}

	// Token: 0x06005E1F RID: 24095 RVA: 0x001E9F84 File Offset: 0x001E8184
	public void Stop()
	{
		Log.FlowPerformance.PrintDebug("Stopping flow: {0}", new object[]
		{
			this.FlowType
		});
		this.IsActive = false;
		this.CloseThresholdPeriod();
		float duration = this.CalculateTotalDuration();
		this.m_telemetryClient.SendFlowPerformance(this.m_id.ToString(), this.FlowType, this.m_averageFps, duration, 20f, this.m_totalTriggeredThreshold, this.m_totalTimeUnderThreshold, this.m_averageTimeBelowThreshold, this.m_maxTimeBelowThreshold);
		this.OnStop();
	}

	// Token: 0x06005E20 RID: 24096 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnStart()
	{
	}

	// Token: 0x06005E21 RID: 24097 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnUpdate()
	{
	}

	// Token: 0x06005E22 RID: 24098 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnPause()
	{
	}

	// Token: 0x06005E23 RID: 24099 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnResume()
	{
	}

	// Token: 0x06005E24 RID: 24100 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnStop()
	{
	}

	// Token: 0x06005E25 RID: 24101 RVA: 0x001EA014 File Offset: 0x001E8214
	protected string GetId()
	{
		return this.m_id.ToString();
	}

	// Token: 0x06005E26 RID: 24102 RVA: 0x001EA027 File Offset: 0x001E8227
	private float CalculateFps()
	{
		return 1f / this.m_timeProvider.UnscaledDeltaTime;
	}

	// Token: 0x06005E27 RID: 24103 RVA: 0x001EA03A File Offset: 0x001E823A
	private void IncrementAverageFps(float currentFps)
	{
		if (!this.m_hasAverage)
		{
			this.m_averageFps = currentFps;
			this.m_hasAverage = true;
		}
		this.m_averageFps += (currentFps - this.m_averageFps) / (float)this.m_frameCount;
	}

	// Token: 0x06005E28 RID: 24104 RVA: 0x001EA070 File Offset: 0x001E8270
	private void UpdateThresholdValues(float currentFps)
	{
		bool flag = currentFps <= 20f;
		if (flag && !this.m_thresholdActive)
		{
			this.m_thresholdActive = true;
			this.m_thresholdStartTime = this.m_timeProvider.TimeSinceStartup;
			this.m_totalTriggeredThreshold++;
			return;
		}
		if (!flag && this.m_thresholdActive)
		{
			this.CloseThresholdPeriod();
		}
	}

	// Token: 0x06005E29 RID: 24105 RVA: 0x001EA0CC File Offset: 0x001E82CC
	private void CloseThresholdPeriod()
	{
		if (!this.m_thresholdActive)
		{
			return;
		}
		this.m_thresholdActive = false;
		float num = this.m_timeProvider.TimeSinceStartup - this.m_thresholdStartTime;
		this.m_totalTimeUnderThreshold += num;
		this.m_averageTimeBelowThreshold += (num - this.m_averageTimeBelowThreshold) / (float)this.m_totalTriggeredThreshold;
		this.m_maxTimeBelowThreshold = Mathf.Max(this.m_maxTimeBelowThreshold, num);
	}

	// Token: 0x06005E2A RID: 24106 RVA: 0x001EA139 File Offset: 0x001E8339
	private float CalculateTotalDuration()
	{
		if (this.IsPaused)
		{
			return this.m_pauseStartTime - this.m_startingFlowTime;
		}
		return this.m_timeProvider.TimeSinceStartup - (this.m_startingFlowTime + this.m_totalPausedTime);
	}

	// Token: 0x04004F5D RID: 20317
	public const float FPS_WARNING_THRESHOLD = 20f;

	// Token: 0x04004F61 RID: 20321
	protected ITimeProvider m_timeProvider;

	// Token: 0x04004F62 RID: 20322
	protected ITelemetryClient m_telemetryClient;

	// Token: 0x04004F63 RID: 20323
	private Guid m_id;

	// Token: 0x04004F64 RID: 20324
	private float m_startingFlowTime;

	// Token: 0x04004F65 RID: 20325
	private int m_frameCount;

	// Token: 0x04004F66 RID: 20326
	private float m_averageFps;

	// Token: 0x04004F67 RID: 20327
	private bool m_hasReceivedFirstFPS;

	// Token: 0x04004F68 RID: 20328
	private bool m_thresholdActive;

	// Token: 0x04004F69 RID: 20329
	private float m_thresholdStartTime;

	// Token: 0x04004F6A RID: 20330
	private int m_totalTriggeredThreshold;

	// Token: 0x04004F6B RID: 20331
	private float m_totalTimeUnderThreshold;

	// Token: 0x04004F6C RID: 20332
	private float m_averageTimeBelowThreshold;

	// Token: 0x04004F6D RID: 20333
	private float m_maxTimeBelowThreshold;

	// Token: 0x04004F6E RID: 20334
	private bool m_hasAverage;

	// Token: 0x04004F6F RID: 20335
	private float m_pauseStartTime;

	// Token: 0x04004F70 RID: 20336
	private float m_totalPausedTime;

	// Token: 0x020021BE RID: 8638
	public class SetupConfig
	{
		// Token: 0x0400E134 RID: 57652
		public Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType FlowType;
	}
}
