using Hearthstone;
using UnityEngine;

[ExecuteAlways]
public class ShowFPS : MonoBehaviour
{
	private float m_UpdateInterval = 0.5f;

	private double m_LastInterval;

	private int frames;

	private bool m_FrameCountActive;

	private float m_FrameCountTime;

	private float m_FrameCountLastTime;

	private int m_FrameCount;

	private bool m_verbose;

	private string m_fpsText;

	private static ShowFPS s_instance;

	private void Awake()
	{
		s_instance = this;
		if (HearthstoneApplication.IsPublic())
		{
			Object.DestroyImmediate(base.gameObject);
		}
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public static ShowFPS Get()
	{
		return s_instance;
	}

	[ContextMenu("Start Frame Count")]
	public void StartFrameCount()
	{
		m_FrameCountLastTime = Time.realtimeSinceStartup;
		m_FrameCountTime = 0f;
		m_FrameCount = 0;
		m_FrameCountActive = true;
	}

	[ContextMenu("Stop Frame Count")]
	public void StopFrameCount()
	{
		m_FrameCountActive = false;
	}

	[ContextMenu("Clear Frame Count")]
	public void ClearFrameCount()
	{
		m_FrameCountLastTime = 0f;
		m_FrameCountTime = 0f;
		m_FrameCount = 0;
		m_FrameCountActive = false;
	}

	private void Start()
	{
		m_LastInterval = Time.realtimeSinceStartup;
		frames = 0;
		UpdateEnabled();
		Options.Get().RegisterChangedListener(Option.HUD, OnHudOptionChanged);
	}

	private void OnDisable()
	{
		Time.captureFramerate = 0;
	}

	private void Update()
	{
		frames++;
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if ((double)realtimeSinceStartup > m_LastInterval + (double)m_UpdateInterval)
		{
			float num = (float)frames / (float)((double)realtimeSinceStartup - m_LastInterval);
			if (m_verbose)
			{
				m_fpsText = $"{num:f2} - {frames} frames over {m_UpdateInterval}sec";
			}
			else
			{
				m_fpsText = $"{num:f2}";
			}
			frames = 0;
			m_LastInterval = realtimeSinceStartup;
		}
		if ((m_FrameCountActive || m_FrameCount > 0) && m_FrameCountActive)
		{
			m_FrameCountTime += (realtimeSinceStartup - m_FrameCountLastTime) / 60f * Time.timeScale;
			if (m_FrameCountLastTime == 0f)
			{
				m_FrameCountLastTime = realtimeSinceStartup;
			}
			m_FrameCount = Mathf.CeilToInt(m_FrameCountTime * 60f);
		}
	}

	private void OnGUI()
	{
		int num = 0;
		Camera[] allCameras = Camera.allCameras;
		for (int i = 0; i < allCameras.Length; i++)
		{
			FullScreenEffects component = allCameras[i].GetComponent<FullScreenEffects>();
			if (!(component == null) && component.IsActive)
			{
				num++;
			}
		}
		string text = m_fpsText;
		if (m_FrameCountActive || m_FrameCount > 0)
		{
			text = $"{text} - Frame Count: {m_FrameCount}";
		}
		if (num > 0)
		{
			text = $"{text} - FSE (x{num})";
		}
		if (HearthstoneServices.TryGet<ScreenEffectsMgr>(out var service))
		{
			int activeScreenEffectsCount = service.GetActiveScreenEffectsCount();
			if (activeScreenEffectsCount > 0 && service.IsActive)
			{
				text = $"{text} - ScreenEffects Active: {activeScreenEffectsCount}";
			}
		}
		GUI.Box(new Rect((float)Screen.width * 0.75f, Screen.height - 20, (float)Screen.width * 0.25f, 20f), text);
	}

	private void OnHudOptionChanged(Option option, object prevValue, bool existed, object userData)
	{
		UpdateEnabled();
	}

	private void UpdateEnabled()
	{
		base.enabled = Options.Get().GetBool(Option.HUD);
	}
}
