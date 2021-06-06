using UnityEngine;

public class BoxEventMgr : MonoBehaviour
{
	public BoxEventInfo m_EventInfo;

	private Map<BoxEventType, Spell> m_eventMap = new Map<BoxEventType, Spell>();

	private void Awake()
	{
		m_eventMap.Add(BoxEventType.STARTUP_HUB, m_EventInfo.m_StartupHub);
		m_eventMap.Add(BoxEventType.STARTUP_TUTORIAL, m_EventInfo.m_StartupTutorial);
		m_eventMap.Add(BoxEventType.TUTORIAL_PLAY, m_EventInfo.m_TutorialPlay);
		m_eventMap.Add(BoxEventType.DISK_LOADING, m_EventInfo.m_DiskLoading);
		m_eventMap.Add(BoxEventType.DISK_MAIN_MENU, m_EventInfo.m_DiskMainMenu);
		m_eventMap.Add(BoxEventType.DOORS_CLOSE, m_EventInfo.m_DoorsClose);
		m_eventMap.Add(BoxEventType.DOORS_OPEN, m_EventInfo.m_DoorsOpen);
		m_eventMap.Add(BoxEventType.DRAWER_OPEN, m_EventInfo.m_DrawerOpen);
		m_eventMap.Add(BoxEventType.DRAWER_CLOSE, m_EventInfo.m_DrawerClose);
		m_eventMap.Add(BoxEventType.SHADOW_FADE_IN, m_EventInfo.m_ShadowFadeIn);
		m_eventMap.Add(BoxEventType.SHADOW_FADE_OUT, m_EventInfo.m_ShadowFadeOut);
		m_eventMap.Add(BoxEventType.STARTUP_SET_ROTATION, m_EventInfo.m_StartupSetRotation);
	}

	public Spell GetEventSpell(BoxEventType eventType)
	{
		Spell value = null;
		m_eventMap.TryGetValue(eventType, out value);
		return value;
	}
}
