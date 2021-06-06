using System.Collections.Generic;
using Blizzard.Telemetry.WTCG.Client;

public class FlowPerformanceManager
{
	private FlowPerformanceFactory m_flowPerformanceFactory;

	private Stack<FlowPerformance> m_flowStack;

	private ReactiveObject<NetCache.NetCacheFeatures> m_guardianVars = ReactiveNetCacheObject<NetCache.NetCacheFeatures>.CreateInstance();

	public FlowPerformanceManager()
	{
		m_flowPerformanceFactory = new FlowPerformanceFactory();
		m_flowStack = new Stack<FlowPerformance>();
	}

	public void LateUpdate()
	{
		if (m_flowStack.Count > 0 && CanRecordMetrics())
		{
			m_flowStack.Peek().Update();
		}
	}

	public void StartPerformanceFlow(FlowPerformance.SetupConfig setupConfig)
	{
		if (CanRecordMetrics())
		{
			StopExistingFlow(setupConfig.FlowType);
			PauseCurrentFlow();
			FlowPerformance flowPerformance = m_flowPerformanceFactory.CreatePerformanceFlow(setupConfig);
			flowPerformance.Start();
			m_flowStack.Push(flowPerformance);
		}
	}

	public T GetCurrentPerformanceFlow<T>() where T : FlowPerformance
	{
		if (m_flowStack.Count == 0)
		{
			return null;
		}
		return m_flowStack.Peek() as T;
	}

	public void StopCurrentFlow()
	{
		if (m_flowStack.Count > 0 && CanRecordMetrics())
		{
			m_flowStack.Pop().Stop();
			ResumeCurrentFlow();
		}
	}

	public void PauseCurrentFlow()
	{
		if (m_flowStack.Count > 0)
		{
			FlowPerformance flowPerformance = m_flowStack.Peek();
			if (flowPerformance.IsActive)
			{
				flowPerformance.Pause();
				return;
			}
			m_flowStack.Pop();
			PauseCurrentFlow();
		}
	}

	public void ResumeCurrentFlow()
	{
		if (m_flowStack.Count > 0)
		{
			FlowPerformance flowPerformance = m_flowStack.Peek();
			if (flowPerformance.IsActive && flowPerformance.IsPaused)
			{
				flowPerformance.Resume();
			}
			else if (!flowPerformance.IsActive)
			{
				m_flowStack.Pop();
				ResumeCurrentFlow();
			}
		}
	}

	private void StopExistingFlow(Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType flowType)
	{
		foreach (FlowPerformance item in m_flowStack)
		{
			if (item.FlowType == flowType)
			{
				Log.FlowPerformance.PrintWarning("A flow of type {0} has been started without finishing the previous one!", flowType);
				item.Stop();
				break;
			}
		}
	}

	private bool CanRecordMetrics()
	{
		return m_guardianVars.Value?.Misc.AllowLiveFPSGathering ?? false;
	}
}
