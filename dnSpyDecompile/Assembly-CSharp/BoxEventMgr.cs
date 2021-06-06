using System;
using UnityEngine;

// Token: 0x020000C6 RID: 198
public class BoxEventMgr : MonoBehaviour
{
	// Token: 0x06000C57 RID: 3159 RVA: 0x000487E8 File Offset: 0x000469E8
	private void Awake()
	{
		this.m_eventMap.Add(BoxEventType.STARTUP_HUB, this.m_EventInfo.m_StartupHub);
		this.m_eventMap.Add(BoxEventType.STARTUP_TUTORIAL, this.m_EventInfo.m_StartupTutorial);
		this.m_eventMap.Add(BoxEventType.TUTORIAL_PLAY, this.m_EventInfo.m_TutorialPlay);
		this.m_eventMap.Add(BoxEventType.DISK_LOADING, this.m_EventInfo.m_DiskLoading);
		this.m_eventMap.Add(BoxEventType.DISK_MAIN_MENU, this.m_EventInfo.m_DiskMainMenu);
		this.m_eventMap.Add(BoxEventType.DOORS_CLOSE, this.m_EventInfo.m_DoorsClose);
		this.m_eventMap.Add(BoxEventType.DOORS_OPEN, this.m_EventInfo.m_DoorsOpen);
		this.m_eventMap.Add(BoxEventType.DRAWER_OPEN, this.m_EventInfo.m_DrawerOpen);
		this.m_eventMap.Add(BoxEventType.DRAWER_CLOSE, this.m_EventInfo.m_DrawerClose);
		this.m_eventMap.Add(BoxEventType.SHADOW_FADE_IN, this.m_EventInfo.m_ShadowFadeIn);
		this.m_eventMap.Add(BoxEventType.SHADOW_FADE_OUT, this.m_EventInfo.m_ShadowFadeOut);
		this.m_eventMap.Add(BoxEventType.STARTUP_SET_ROTATION, this.m_EventInfo.m_StartupSetRotation);
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x00048910 File Offset: 0x00046B10
	public Spell GetEventSpell(BoxEventType eventType)
	{
		Spell result = null;
		this.m_eventMap.TryGetValue(eventType, out result);
		return result;
	}

	// Token: 0x0400087D RID: 2173
	public BoxEventInfo m_EventInfo;

	// Token: 0x0400087E RID: 2174
	private Map<BoxEventType, Spell> m_eventMap = new Map<BoxEventType, Spell>();
}
