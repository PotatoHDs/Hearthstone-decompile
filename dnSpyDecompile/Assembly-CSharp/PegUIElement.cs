using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Blizzard.T5.Core;
using Hearthstone;
using UnityEngine;

// Token: 0x02000ADC RID: 2780
public class PegUIElement : MonoBehaviour
{
	// Token: 0x17000872 RID: 2162
	// (get) Token: 0x0600940C RID: 37900 RVA: 0x00300FAA File Offset: 0x002FF1AA
	// (set) Token: 0x0600940D RID: 37901 RVA: 0x00300FB2 File Offset: 0x002FF1B2
	public bool DoubleClickEnabled { get; private set; }

	// Token: 0x17000873 RID: 2163
	// (get) Token: 0x0600940F RID: 37903 RVA: 0x00300FC4 File Offset: 0x002FF1C4
	// (set) Token: 0x0600940E RID: 37902 RVA: 0x00300FBB File Offset: 0x002FF1BB
	public float SetEnabledLastCallTime { get; private set; }

	// Token: 0x06009410 RID: 37904 RVA: 0x00300FCC File Offset: 0x002FF1CC
	protected virtual void Awake()
	{
		this.DoubleClickEnabled = (this.DoubleClickEnabled || this.HasOverriddenDoubleClick());
	}

	// Token: 0x06009411 RID: 37905 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnDestroy()
	{
	}

	// Token: 0x06009412 RID: 37906 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnOver(PegUIElement.InteractionState oldState)
	{
	}

	// Token: 0x06009413 RID: 37907 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnOut(PegUIElement.InteractionState oldState)
	{
	}

	// Token: 0x06009414 RID: 37908 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnPress()
	{
	}

	// Token: 0x06009415 RID: 37909 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnTap()
	{
	}

	// Token: 0x06009416 RID: 37910 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnRelease()
	{
	}

	// Token: 0x06009417 RID: 37911 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnReleaseAll(bool mouseIsOver)
	{
	}

	// Token: 0x06009418 RID: 37912 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnDrag()
	{
	}

	// Token: 0x06009419 RID: 37913 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnDoubleClick()
	{
	}

	// Token: 0x0600941A RID: 37914 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnRightClick()
	{
	}

	// Token: 0x0600941B RID: 37915 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnHold()
	{
	}

	// Token: 0x0600941C RID: 37916 RVA: 0x00300FE5 File Offset: 0x002FF1E5
	public void SetInteractionState(PegUIElement.InteractionState state)
	{
		this.m_interactionState = state;
	}

	// Token: 0x0600941D RID: 37917 RVA: 0x00300FF0 File Offset: 0x002FF1F0
	public virtual void TriggerOver()
	{
		if (!this.m_enabled)
		{
			return;
		}
		if (this.m_focused)
		{
			return;
		}
		this.PrintLog("OVER", PegUIElement.PegUILogLevel.ALL_EVENTS, false);
		this.m_focused = true;
		PegUIElement.InteractionState interactionState = this.m_interactionState;
		this.m_interactionState = PegUIElement.InteractionState.Over;
		this.OnOver(interactionState);
		this.DispatchEvent(new UIEvent(UIEventType.ROLLOVER, this));
	}

	// Token: 0x0600941E RID: 37918 RVA: 0x00301048 File Offset: 0x002FF248
	public virtual void TriggerOut()
	{
		if (!this.m_enabled)
		{
			return;
		}
		this.PrintLog("OUT", PegUIElement.PegUILogLevel.ALL_EVENTS, false);
		this.m_focused = false;
		PegUIElement.InteractionState interactionState = this.m_interactionState;
		this.m_interactionState = PegUIElement.InteractionState.Out;
		this.OnOut(interactionState);
		this.DispatchEvent(new UIEvent(UIEventType.ROLLOUT, this));
	}

	// Token: 0x0600941F RID: 37919 RVA: 0x00301094 File Offset: 0x002FF294
	public virtual void TriggerPress()
	{
		if (!this.m_enabled)
		{
			return;
		}
		this.PrintLog("PRESS", PegUIElement.PegUILogLevel.PRESS, false);
		this.m_focused = true;
		this.m_interactionState = PegUIElement.InteractionState.Down;
		this.OnPress();
		this.DispatchEvent(new UIEvent(UIEventType.PRESS, this));
	}

	// Token: 0x06009420 RID: 37920 RVA: 0x003010CD File Offset: 0x002FF2CD
	public virtual void TriggerTap()
	{
		if (!this.m_enabled)
		{
			return;
		}
		this.PrintLog("TAP", PegUIElement.PegUILogLevel.ALL_EVENTS, true);
		this.m_interactionState = PegUIElement.InteractionState.Up;
		this.OnTap();
		this.DispatchEvent(new UIEvent(UIEventType.TAP, this));
	}

	// Token: 0x06009421 RID: 37921 RVA: 0x00301100 File Offset: 0x002FF300
	public virtual void TriggerRelease()
	{
		if (!this.m_enabled)
		{
			return;
		}
		this.PrintLog("RELEASE", PegUIElement.PegUILogLevel.ALL_EVENTS, false);
		this.m_interactionState = PegUIElement.InteractionState.Up;
		if (!this.IsScrolling())
		{
			this.OnRelease();
			this.DispatchEvent(new UIEvent(UIEventType.RELEASE, this));
		}
	}

	// Token: 0x06009422 RID: 37922 RVA: 0x0030113A File Offset: 0x002FF33A
	public void TriggerReleaseAll(bool mouseIsOver)
	{
		if (!this.m_enabled)
		{
			return;
		}
		this.m_interactionState = PegUIElement.InteractionState.Up;
		this.OnReleaseAll(mouseIsOver);
		this.DispatchEvent(new UIReleaseAllEvent(mouseIsOver, this));
	}

	// Token: 0x06009423 RID: 37923 RVA: 0x00301160 File Offset: 0x002FF360
	public void TriggerDrag()
	{
		if (!this.m_enabled)
		{
			return;
		}
		this.PrintLog("DRAG", PegUIElement.PegUILogLevel.ALL_EVENTS, false);
		this.m_interactionState = PegUIElement.InteractionState.Down;
		this.OnDrag();
		this.DispatchEvent(new UIEvent(UIEventType.DRAG, this));
	}

	// Token: 0x06009424 RID: 37924 RVA: 0x00301192 File Offset: 0x002FF392
	public void TriggerHold()
	{
		if (!this.m_enabled)
		{
			return;
		}
		this.PrintLog("HOLD", PegUIElement.PegUILogLevel.ALL_EVENTS, false);
		this.m_interactionState = PegUIElement.InteractionState.Down;
		this.OnHold();
		this.DispatchEvent(new UIEvent(UIEventType.HOLD, this));
	}

	// Token: 0x06009425 RID: 37925 RVA: 0x003011C5 File Offset: 0x002FF3C5
	public void TriggerDoubleClick()
	{
		if (!this.m_enabled)
		{
			return;
		}
		this.PrintLog("DCLICK", PegUIElement.PegUILogLevel.ALL_EVENTS, false);
		this.m_interactionState = PegUIElement.InteractionState.Down;
		this.OnDoubleClick();
		this.DispatchEvent(new UIEvent(UIEventType.DOUBLECLICK, this));
	}

	// Token: 0x06009426 RID: 37926 RVA: 0x003011F7 File Offset: 0x002FF3F7
	public void TriggerRightClick()
	{
		if (!this.m_enabled)
		{
			return;
		}
		this.PrintLog("RCLICK", PegUIElement.PegUILogLevel.ALL_EVENTS, false);
		this.OnRightClick();
		this.DispatchEvent(new UIEvent(UIEventType.RIGHTCLICK, this));
	}

	// Token: 0x06009427 RID: 37927 RVA: 0x00301222 File Offset: 0x002FF422
	public void SetDragTolerance(float newTolerance)
	{
		this.SetDragTolerance(Vector3.one * newTolerance);
	}

	// Token: 0x06009428 RID: 37928 RVA: 0x00301235 File Offset: 0x002FF435
	public void SetDragTolerance(Vector3 newTolerance)
	{
		this.m_dragTolerance = newTolerance;
	}

	// Token: 0x06009429 RID: 37929 RVA: 0x0030123E File Offset: 0x002FF43E
	public Vector3 GetDragTolerance()
	{
		return this.m_dragTolerance;
	}

	// Token: 0x0600942A RID: 37930 RVA: 0x00301248 File Offset: 0x002FF448
	public virtual bool AddEventListener(UIEventType type, UIEvent.Handler handler)
	{
		this.DoubleClickEnabled |= (type == UIEventType.DOUBLECLICK);
		List<UIEvent.Handler> list;
		if (!this.m_eventListeners.TryGetValue(type, out list))
		{
			list = new List<UIEvent.Handler>();
			this.m_eventListeners.Add(type, list);
			list.Add(handler);
			return true;
		}
		if (list.Contains(handler))
		{
			return false;
		}
		list.Add(handler);
		return true;
	}

	// Token: 0x0600942B RID: 37931 RVA: 0x003012A8 File Offset: 0x002FF4A8
	public virtual bool RemoveEventListener(UIEventType type, UIEvent.Handler handler)
	{
		List<UIEvent.Handler> list;
		return this.m_eventListeners.TryGetValue(type, out list) && list.Remove(handler);
	}

	// Token: 0x0600942C RID: 37932 RVA: 0x003012CE File Offset: 0x002FF4CE
	public void ClearEventListeners()
	{
		this.m_eventListeners.Clear();
	}

	// Token: 0x0600942D RID: 37933 RVA: 0x003012DC File Offset: 0x002FF4DC
	public bool HasEventListener(UIEventType type)
	{
		List<UIEvent.Handler> list;
		return this.m_eventListeners.TryGetValue(type, out list) && list.Count > 0;
	}

	// Token: 0x0600942E RID: 37934 RVA: 0x00301304 File Offset: 0x002FF504
	public virtual void SetEnabled(bool enabled, bool isInternal = false)
	{
		if (!isInternal)
		{
			this.SetEnabledLastCallTime = Time.realtimeSinceStartup;
		}
		if (enabled)
		{
			this.PrintLog("ENABLE", PegUIElement.PegUILogLevel.ALL_EVENTS, false);
		}
		else
		{
			this.PrintLog("DISABLE", PegUIElement.PegUILogLevel.ALL_EVENTS, false);
		}
		this.m_enabled = enabled;
		if (this.m_enabled)
		{
			return;
		}
		this.m_focused = false;
	}

	// Token: 0x0600942F RID: 37935 RVA: 0x00301355 File Offset: 0x002FF555
	public bool IsEnabled()
	{
		return this.m_enabled;
	}

	// Token: 0x06009430 RID: 37936 RVA: 0x0030135D File Offset: 0x002FF55D
	public void SetReceiveReleaseWithoutMouseDown(bool receiveReleaseWithoutMouseDown)
	{
		this.m_receiveReleaseWithoutMouseDown = receiveReleaseWithoutMouseDown;
	}

	// Token: 0x06009431 RID: 37937 RVA: 0x00301366 File Offset: 0x002FF566
	public bool GetReceiveReleaseWithoutMouseDown()
	{
		return this.m_receiveReleaseWithoutMouseDown;
	}

	// Token: 0x06009432 RID: 37938 RVA: 0x0030136E File Offset: 0x002FF56E
	public bool GetReceiveOverWithMouseDown()
	{
		return UniversalInputManager.Get().IsTouchMode();
	}

	// Token: 0x06009433 RID: 37939 RVA: 0x0030137F File Offset: 0x002FF57F
	public PegUIElement.InteractionState GetInteractionState()
	{
		return this.m_interactionState;
	}

	// Token: 0x06009434 RID: 37940 RVA: 0x00301387 File Offset: 0x002FF587
	public void SetData(object data)
	{
		this.m_data = data;
	}

	// Token: 0x06009435 RID: 37941 RVA: 0x00301390 File Offset: 0x002FF590
	public object GetData()
	{
		return this.m_data;
	}

	// Token: 0x06009436 RID: 37942 RVA: 0x00301398 File Offset: 0x002FF598
	public void SetOriginalLocalPosition()
	{
		this.SetOriginalLocalPosition(base.transform.localPosition);
	}

	// Token: 0x06009437 RID: 37943 RVA: 0x003013AB File Offset: 0x002FF5AB
	public void SetOriginalLocalPosition(Vector3 pos)
	{
		this.m_originalLocalPosition = pos;
	}

	// Token: 0x06009438 RID: 37944 RVA: 0x003013B4 File Offset: 0x002FF5B4
	public Vector3 GetOriginalLocalPosition()
	{
		return this.m_originalLocalPosition;
	}

	// Token: 0x06009439 RID: 37945 RVA: 0x003013BC File Offset: 0x002FF5BC
	public void SetCursorDown(PegCursor.Mode mode)
	{
		this.m_cursorDownOverride = mode;
	}

	// Token: 0x0600943A RID: 37946 RVA: 0x003013C5 File Offset: 0x002FF5C5
	public PegCursor.Mode GetCursorDown()
	{
		return this.m_cursorDownOverride;
	}

	// Token: 0x0600943B RID: 37947 RVA: 0x003013CD File Offset: 0x002FF5CD
	public void SetCursorOver(PegCursor.Mode mode)
	{
		this.m_cursorOverOverride = mode;
	}

	// Token: 0x0600943C RID: 37948 RVA: 0x003013D6 File Offset: 0x002FF5D6
	public PegCursor.Mode GetCursorOver()
	{
		return this.m_cursorOverOverride;
	}

	// Token: 0x0600943D RID: 37949 RVA: 0x003013E0 File Offset: 0x002FF5E0
	private void DispatchEvent(UIEvent e)
	{
		List<UIEvent.Handler> list;
		if (!this.m_eventListeners.TryGetValue(e.GetEventType(), out list))
		{
			return;
		}
		UIEvent.Handler[] array = list.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](e);
		}
	}

	// Token: 0x0600943E RID: 37950 RVA: 0x00301424 File Offset: 0x002FF624
	private bool HasOverriddenDoubleClick()
	{
		Type type = base.GetType();
		Type typeFromHandle = typeof(PegUIElement);
		typeFromHandle.GetMethod("OnDoubleClick", BindingFlags.Instance | BindingFlags.NonPublic);
		type.GetMethod("OnDoubleClick", BindingFlags.Instance | BindingFlags.NonPublic);
		return GeneralUtils.IsOverriddenMethod(type, typeFromHandle, "OnDoubleClick");
	}

	// Token: 0x0600943F RID: 37951 RVA: 0x0030146C File Offset: 0x002FF66C
	private void PrintLog(string evt, PegUIElement.PegUILogLevel logLevel, bool printOnScreen = false)
	{
		if (this == null || base.gameObject == null || !HearthstoneApplication.IsInternal() || Options.Get().GetInt(Option.PEGUI_DEBUG) < (int)logLevel)
		{
			return;
		}
		string hierarchyPath = DebugUtils.GetHierarchyPath(base.gameObject, '/');
		string text = string.Format("{0,-7} {1}", evt + ":", hierarchyPath);
		Log.All.PrintInfo(text, Array.Empty<object>());
		if (printOnScreen)
		{
			Log.All.ForceScreenPrint(Log.LogLevel.Info, true, text);
		}
	}

	// Token: 0x06009440 RID: 37952 RVA: 0x003014EC File Offset: 0x002FF6EC
	private bool IsScrolling()
	{
		return base.GetComponentsInParent<UIBScrollable.IContent>().Any((UIBScrollable.IContent scrollable) => scrollable.Scrollable.IsTouchDragging());
	}

	// Token: 0x04007C39 RID: 31801
	private MeshFilter m_meshFilter;

	// Token: 0x04007C3A RID: 31802
	private MeshRenderer m_renderer;

	// Token: 0x04007C3B RID: 31803
	private Map<UIEventType, List<UIEvent.Handler>> m_eventListeners = new Map<UIEventType, List<UIEvent.Handler>>();

	// Token: 0x04007C3C RID: 31804
	private bool m_enabled = true;

	// Token: 0x04007C3D RID: 31805
	private bool m_focused;

	// Token: 0x04007C3E RID: 31806
	private bool m_receiveReleaseWithoutMouseDown;

	// Token: 0x04007C3F RID: 31807
	private object m_data;

	// Token: 0x04007C40 RID: 31808
	private Vector3 m_originalLocalPosition;

	// Token: 0x04007C41 RID: 31809
	private PegUIElement.InteractionState m_interactionState;

	// Token: 0x04007C42 RID: 31810
	private Vector3 m_dragTolerance = Vector3.one * 40f;

	// Token: 0x04007C43 RID: 31811
	private PegCursor.Mode m_cursorDownOverride = PegCursor.Mode.NONE;

	// Token: 0x04007C44 RID: 31812
	private PegCursor.Mode m_cursorOverOverride = PegCursor.Mode.NONE;

	// Token: 0x02002714 RID: 10004
	public enum InteractionState
	{
		// Token: 0x0400F355 RID: 62293
		None,
		// Token: 0x0400F356 RID: 62294
		Out,
		// Token: 0x0400F357 RID: 62295
		Over,
		// Token: 0x0400F358 RID: 62296
		Down,
		// Token: 0x0400F359 RID: 62297
		Up,
		// Token: 0x0400F35A RID: 62298
		Disabled
	}

	// Token: 0x02002715 RID: 10005
	public enum PegUILogLevel
	{
		// Token: 0x0400F35C RID: 62300
		NONE,
		// Token: 0x0400F35D RID: 62301
		PRESS,
		// Token: 0x0400F35E RID: 62302
		ALL_EVENTS,
		// Token: 0x0400F35F RID: 62303
		HIT_TEST
	}
}
