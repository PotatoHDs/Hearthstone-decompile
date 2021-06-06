using System;
using UnityEngine;

// Token: 0x02000698 RID: 1688
public class HearthstonePerformance
{
	// Token: 0x17000597 RID: 1431
	// (get) Token: 0x06005E40 RID: 24128 RVA: 0x001EA5D2 File Offset: 0x001E87D2
	// (set) Token: 0x06005E41 RID: 24129 RVA: 0x001EA5DA File Offset: 0x001E87DA
	public float AppStartTime { get; private set; }

	// Token: 0x17000598 RID: 1432
	// (get) Token: 0x06005E42 RID: 24130 RVA: 0x001EA5E3 File Offset: 0x001E87E3
	// (set) Token: 0x06005E43 RID: 24131 RVA: 0x001EA5EB File Offset: 0x001E87EB
	public float AppInitializedTime { get; private set; }

	// Token: 0x17000599 RID: 1433
	// (get) Token: 0x06005E44 RID: 24132 RVA: 0x001EA5F4 File Offset: 0x001E87F4
	// (set) Token: 0x06005E45 RID: 24133 RVA: 0x001EA5FC File Offset: 0x001E87FC
	public float BoxInteractableTime { get; private set; }

	// Token: 0x06005E46 RID: 24134 RVA: 0x001EA605 File Offset: 0x001E8805
	private HearthstonePerformance(string testType, string changelist)
	{
		this.m_testType = testType;
		this.m_changelist = changelist;
		this.m_flowPerformanceManager = new FlowPerformanceManager();
	}

	// Token: 0x06005E47 RID: 24135 RVA: 0x001EA626 File Offset: 0x001E8826
	public static HearthstonePerformance Get()
	{
		return HearthstonePerformance.s_instance;
	}

	// Token: 0x06005E48 RID: 24136 RVA: 0x001EA62D File Offset: 0x001E882D
	public static void Initialize(string testType, string changelist)
	{
		HearthstonePerformance.s_instance = new HearthstonePerformance(testType, changelist);
	}

	// Token: 0x06005E49 RID: 24137 RVA: 0x001EA63B File Offset: 0x001E883B
	public static void Shutdown()
	{
		HearthstonePerformance.s_instance = null;
	}

	// Token: 0x06005E4A RID: 24138 RVA: 0x001EA643 File Offset: 0x001E8843
	public void DoLateUpdate()
	{
		this.m_flowPerformanceManager.LateUpdate();
	}

	// Token: 0x06005E4B RID: 24139 RVA: 0x001EA650 File Offset: 0x001E8850
	public void CaptureAppStartTime()
	{
		if (!this.m_hasAppStartTime)
		{
			this.AppStartTime = Time.realtimeSinceStartup;
			TelemetryManager.Client().SendAppStart(this.m_testType, this.AppStartTime, this.m_changelist);
			this.m_hasAppStartTime = true;
		}
	}

	// Token: 0x06005E4C RID: 24140 RVA: 0x001EA688 File Offset: 0x001E8888
	public void CaptureAppInitializedTime()
	{
		if (!this.m_hasAppInitializedTime)
		{
			this.AppInitializedTime = Time.realtimeSinceStartup - this.AppStartTime;
			TelemetryManager.Client().SendAppInitialized(this.m_testType, this.AppInitializedTime, this.m_changelist);
			this.m_hasAppInitializedTime = true;
		}
	}

	// Token: 0x06005E4D RID: 24141 RVA: 0x001EA6C7 File Offset: 0x001E88C7
	public void CaptureBoxInteractableTime()
	{
		if (!this.m_hasBoxInteractableTime)
		{
			this.BoxInteractableTime = Time.realtimeSinceStartup - this.AppStartTime;
			TelemetryManager.Client().SendBoxInteractable(this.m_testType, this.BoxInteractableTime, this.m_changelist);
			this.m_hasBoxInteractableTime = true;
		}
	}

	// Token: 0x06005E4E RID: 24142 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void SendCustomEvent(string eventName)
	{
	}

	// Token: 0x06005E4F RID: 24143 RVA: 0x001EA706 File Offset: 0x001E8906
	public void StartPerformanceFlow(FlowPerformance.SetupConfig setupConfig)
	{
		this.m_flowPerformanceManager.StartPerformanceFlow(setupConfig);
	}

	// Token: 0x06005E50 RID: 24144 RVA: 0x001EA714 File Offset: 0x001E8914
	public T GetCurrentPerformanceFlow<T>() where T : FlowPerformance
	{
		return this.m_flowPerformanceManager.GetCurrentPerformanceFlow<T>();
	}

	// Token: 0x06005E51 RID: 24145 RVA: 0x001EA721 File Offset: 0x001E8921
	public void StopCurrentFlow()
	{
		this.m_flowPerformanceManager.StopCurrentFlow();
	}

	// Token: 0x06005E52 RID: 24146 RVA: 0x001EA72E File Offset: 0x001E892E
	public void OnApplicationPause()
	{
		this.m_flowPerformanceManager.PauseCurrentFlow();
	}

	// Token: 0x06005E53 RID: 24147 RVA: 0x001EA73B File Offset: 0x001E893B
	public void OnApplicationResume()
	{
		this.m_flowPerformanceManager.ResumeCurrentFlow();
	}

	// Token: 0x04004F7D RID: 20349
	private static HearthstonePerformance s_instance;

	// Token: 0x04004F7E RID: 20350
	private FlowPerformanceManager m_flowPerformanceManager;

	// Token: 0x04004F7F RID: 20351
	private string m_testType;

	// Token: 0x04004F80 RID: 20352
	private string m_changelist;

	// Token: 0x04004F81 RID: 20353
	private bool m_hasAppStartTime;

	// Token: 0x04004F82 RID: 20354
	private bool m_hasAppInitializedTime;

	// Token: 0x04004F83 RID: 20355
	private bool m_hasBoxInteractableTime;
}
