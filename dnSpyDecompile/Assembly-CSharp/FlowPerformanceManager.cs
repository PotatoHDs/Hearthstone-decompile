using System;
using System.Collections.Generic;
using Blizzard.Telemetry.WTCG.Client;

// Token: 0x02000696 RID: 1686
public class FlowPerformanceManager
{
	// Token: 0x06005E34 RID: 24116 RVA: 0x001EA2F0 File Offset: 0x001E84F0
	public FlowPerformanceManager()
	{
		this.m_flowPerformanceFactory = new FlowPerformanceFactory();
		this.m_flowStack = new Stack<global::FlowPerformance>();
	}

	// Token: 0x06005E35 RID: 24117 RVA: 0x001EA319 File Offset: 0x001E8519
	public void LateUpdate()
	{
		if (this.m_flowStack.Count > 0 && this.CanRecordMetrics())
		{
			this.m_flowStack.Peek().Update();
		}
	}

	// Token: 0x06005E36 RID: 24118 RVA: 0x001EA344 File Offset: 0x001E8544
	public void StartPerformanceFlow(global::FlowPerformance.SetupConfig setupConfig)
	{
		if (!this.CanRecordMetrics())
		{
			return;
		}
		this.StopExistingFlow(setupConfig.FlowType);
		this.PauseCurrentFlow();
		global::FlowPerformance flowPerformance = this.m_flowPerformanceFactory.CreatePerformanceFlow(setupConfig);
		flowPerformance.Start();
		this.m_flowStack.Push(flowPerformance);
	}

	// Token: 0x06005E37 RID: 24119 RVA: 0x001EA38C File Offset: 0x001E858C
	public T GetCurrentPerformanceFlow<T>() where T : global::FlowPerformance
	{
		if (this.m_flowStack.Count == 0)
		{
			return default(T);
		}
		return this.m_flowStack.Peek() as T;
	}

	// Token: 0x06005E38 RID: 24120 RVA: 0x001EA3C5 File Offset: 0x001E85C5
	public void StopCurrentFlow()
	{
		if (this.m_flowStack.Count > 0 && this.CanRecordMetrics())
		{
			this.m_flowStack.Pop().Stop();
			this.ResumeCurrentFlow();
		}
	}

	// Token: 0x06005E39 RID: 24121 RVA: 0x001EA3F4 File Offset: 0x001E85F4
	public void PauseCurrentFlow()
	{
		if (this.m_flowStack.Count > 0)
		{
			global::FlowPerformance flowPerformance = this.m_flowStack.Peek();
			if (flowPerformance.IsActive)
			{
				flowPerformance.Pause();
				return;
			}
			this.m_flowStack.Pop();
			this.PauseCurrentFlow();
		}
	}

	// Token: 0x06005E3A RID: 24122 RVA: 0x001EA43C File Offset: 0x001E863C
	public void ResumeCurrentFlow()
	{
		if (this.m_flowStack.Count > 0)
		{
			global::FlowPerformance flowPerformance = this.m_flowStack.Peek();
			if (flowPerformance.IsActive && flowPerformance.IsPaused)
			{
				flowPerformance.Resume();
				return;
			}
			if (!flowPerformance.IsActive)
			{
				this.m_flowStack.Pop();
				this.ResumeCurrentFlow();
			}
		}
	}

	// Token: 0x06005E3B RID: 24123 RVA: 0x001EA494 File Offset: 0x001E8694
	private void StopExistingFlow(Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType flowType)
	{
		foreach (global::FlowPerformance flowPerformance in this.m_flowStack)
		{
			if (flowPerformance.FlowType == flowType)
			{
				Log.FlowPerformance.PrintWarning("A flow of type {0} has been started without finishing the previous one!", new object[]
				{
					flowType
				});
				flowPerformance.Stop();
				break;
			}
		}
	}

	// Token: 0x06005E3C RID: 24124 RVA: 0x001EA510 File Offset: 0x001E8710
	private bool CanRecordMetrics()
	{
		NetCache.NetCacheFeatures value = this.m_guardianVars.Value;
		return value != null && value.Misc.AllowLiveFPSGathering;
	}

	// Token: 0x04004F79 RID: 20345
	private FlowPerformanceFactory m_flowPerformanceFactory;

	// Token: 0x04004F7A RID: 20346
	private Stack<global::FlowPerformance> m_flowStack;

	// Token: 0x04004F7B RID: 20347
	private ReactiveObject<NetCache.NetCacheFeatures> m_guardianVars = ReactiveNetCacheObject<NetCache.NetCacheFeatures>.CreateInstance();
}
