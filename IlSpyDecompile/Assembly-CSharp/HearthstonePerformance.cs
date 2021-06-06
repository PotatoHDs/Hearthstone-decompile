using UnityEngine;

public class HearthstonePerformance
{
	private static HearthstonePerformance s_instance;

	private FlowPerformanceManager m_flowPerformanceManager;

	private string m_testType;

	private string m_changelist;

	private bool m_hasAppStartTime;

	private bool m_hasAppInitializedTime;

	private bool m_hasBoxInteractableTime;

	public float AppStartTime { get; private set; }

	public float AppInitializedTime { get; private set; }

	public float BoxInteractableTime { get; private set; }

	private HearthstonePerformance(string testType, string changelist)
	{
		m_testType = testType;
		m_changelist = changelist;
		m_flowPerformanceManager = new FlowPerformanceManager();
	}

	public static HearthstonePerformance Get()
	{
		return s_instance;
	}

	public static void Initialize(string testType, string changelist)
	{
		s_instance = new HearthstonePerformance(testType, changelist);
	}

	public static void Shutdown()
	{
		s_instance = null;
	}

	public void DoLateUpdate()
	{
		m_flowPerformanceManager.LateUpdate();
	}

	public void CaptureAppStartTime()
	{
		if (!m_hasAppStartTime)
		{
			AppStartTime = Time.realtimeSinceStartup;
			TelemetryManager.Client().SendAppStart(m_testType, AppStartTime, m_changelist);
			m_hasAppStartTime = true;
		}
	}

	public void CaptureAppInitializedTime()
	{
		if (!m_hasAppInitializedTime)
		{
			AppInitializedTime = Time.realtimeSinceStartup - AppStartTime;
			TelemetryManager.Client().SendAppInitialized(m_testType, AppInitializedTime, m_changelist);
			m_hasAppInitializedTime = true;
		}
	}

	public void CaptureBoxInteractableTime()
	{
		if (!m_hasBoxInteractableTime)
		{
			BoxInteractableTime = Time.realtimeSinceStartup - AppStartTime;
			TelemetryManager.Client().SendBoxInteractable(m_testType, BoxInteractableTime, m_changelist);
			m_hasBoxInteractableTime = true;
		}
	}

	public void SendCustomEvent(string eventName)
	{
	}

	public void StartPerformanceFlow(FlowPerformance.SetupConfig setupConfig)
	{
		m_flowPerformanceManager.StartPerformanceFlow(setupConfig);
	}

	public T GetCurrentPerformanceFlow<T>() where T : FlowPerformance
	{
		return m_flowPerformanceManager.GetCurrentPerformanceFlow<T>();
	}

	public void StopCurrentFlow()
	{
		m_flowPerformanceManager.StopCurrentFlow();
	}

	public void OnApplicationPause()
	{
		m_flowPerformanceManager.PauseCurrentFlow();
	}

	public void OnApplicationResume()
	{
		m_flowPerformanceManager.ResumeCurrentFlow();
	}
}
