using System;
using Blizzard.Telemetry.WTCG.Client;
using HearthstoneTelemetry;
using UnityEngine;

public class FlowPerformance
{
	public class SetupConfig
	{
		public Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType FlowType;
	}

	public const float FPS_WARNING_THRESHOLD = 20f;

	protected ITimeProvider m_timeProvider;

	protected ITelemetryClient m_telemetryClient;

	private Guid m_id;

	private float m_startingFlowTime;

	private int m_frameCount;

	private float m_averageFps;

	private bool m_hasReceivedFirstFPS;

	private bool m_thresholdActive;

	private float m_thresholdStartTime;

	private int m_totalTriggeredThreshold;

	private float m_totalTimeUnderThreshold;

	private float m_averageTimeBelowThreshold;

	private float m_maxTimeBelowThreshold;

	private bool m_hasAverage;

	private float m_pauseStartTime;

	private float m_totalPausedTime;

	public Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType FlowType { get; }

	public bool IsActive { get; private set; }

	public bool IsPaused { get; private set; }

	public FlowPerformance(ITimeProvider timeProvider, ITelemetryClient telemetryClient, SetupConfig setupConfig)
	{
		m_timeProvider = timeProvider;
		m_telemetryClient = telemetryClient;
		FlowType = setupConfig.FlowType;
		IsActive = false;
	}

	public void Start()
	{
		Log.FlowPerformance.PrintDebug("Starting flow: {0}", FlowType);
		m_id = Guid.NewGuid();
		IsActive = true;
		m_hasAverage = false;
		m_startingFlowTime = m_timeProvider.TimeSinceStartup;
		m_frameCount = 0;
		m_totalTriggeredThreshold = 0;
		m_totalTimeUnderThreshold = 0f;
		m_averageTimeBelowThreshold = 0f;
		m_maxTimeBelowThreshold = 0f;
		m_thresholdActive = false;
		m_totalPausedTime = 0f;
		OnStart();
	}

	public void Update()
	{
		if (!IsPaused)
		{
			m_frameCount++;
			float currentFps = CalculateFps();
			IncrementAverageFps(currentFps);
			UpdateThresholdValues(currentFps);
			OnUpdate();
		}
	}

	public void Pause()
	{
		Log.FlowPerformance.PrintDebug("Pausing flow: {0}", FlowType);
		if (IsActive)
		{
			IsPaused = true;
			m_pauseStartTime = m_timeProvider.TimeSinceStartup;
			OnPause();
		}
	}

	public void Resume()
	{
		Log.FlowPerformance.PrintDebug("Resuming flow: {0}", FlowType);
		if (IsActive && IsPaused)
		{
			float num = m_timeProvider.TimeSinceStartup - m_pauseStartTime;
			IsPaused = false;
			m_totalPausedTime += num;
			OnResume();
		}
	}

	public void Stop()
	{
		Log.FlowPerformance.PrintDebug("Stopping flow: {0}", FlowType);
		IsActive = false;
		CloseThresholdPeriod();
		float duration = CalculateTotalDuration();
		m_telemetryClient.SendFlowPerformance(m_id.ToString(), FlowType, m_averageFps, duration, 20f, m_totalTriggeredThreshold, m_totalTimeUnderThreshold, m_averageTimeBelowThreshold, m_maxTimeBelowThreshold);
		OnStop();
	}

	protected virtual void OnStart()
	{
	}

	protected virtual void OnUpdate()
	{
	}

	protected virtual void OnPause()
	{
	}

	protected virtual void OnResume()
	{
	}

	protected virtual void OnStop()
	{
	}

	protected string GetId()
	{
		return m_id.ToString();
	}

	private float CalculateFps()
	{
		return 1f / m_timeProvider.UnscaledDeltaTime;
	}

	private void IncrementAverageFps(float currentFps)
	{
		if (!m_hasAverage)
		{
			m_averageFps = currentFps;
			m_hasAverage = true;
		}
		m_averageFps += (currentFps - m_averageFps) / (float)m_frameCount;
	}

	private void UpdateThresholdValues(float currentFps)
	{
		bool flag = currentFps <= 20f;
		if (flag && !m_thresholdActive)
		{
			m_thresholdActive = true;
			m_thresholdStartTime = m_timeProvider.TimeSinceStartup;
			m_totalTriggeredThreshold++;
		}
		else if (!flag && m_thresholdActive)
		{
			CloseThresholdPeriod();
		}
	}

	private void CloseThresholdPeriod()
	{
		if (m_thresholdActive)
		{
			m_thresholdActive = false;
			float num = m_timeProvider.TimeSinceStartup - m_thresholdStartTime;
			m_totalTimeUnderThreshold += num;
			m_averageTimeBelowThreshold += (num - m_averageTimeBelowThreshold) / (float)m_totalTriggeredThreshold;
			m_maxTimeBelowThreshold = Mathf.Max(m_maxTimeBelowThreshold, num);
		}
	}

	private float CalculateTotalDuration()
	{
		if (IsPaused)
		{
			return m_pauseStartTime - m_startingFlowTime;
		}
		return m_timeProvider.TimeSinceStartup - (m_startingFlowTime + m_totalPausedTime);
	}
}
