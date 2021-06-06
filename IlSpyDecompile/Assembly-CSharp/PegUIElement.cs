using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Blizzard.T5.Core;
using Hearthstone;
using UnityEngine;

public class PegUIElement : MonoBehaviour
{
	public enum InteractionState
	{
		None,
		Out,
		Over,
		Down,
		Up,
		Disabled
	}

	public enum PegUILogLevel
	{
		NONE,
		PRESS,
		ALL_EVENTS,
		HIT_TEST
	}

	private MeshFilter m_meshFilter;

	private MeshRenderer m_renderer;

	private Map<UIEventType, List<UIEvent.Handler>> m_eventListeners = new Map<UIEventType, List<UIEvent.Handler>>();

	private bool m_enabled = true;

	private bool m_focused;

	private bool m_receiveReleaseWithoutMouseDown;

	private object m_data;

	private Vector3 m_originalLocalPosition;

	private InteractionState m_interactionState;

	private Vector3 m_dragTolerance = Vector3.one * 40f;

	private PegCursor.Mode m_cursorDownOverride = PegCursor.Mode.NONE;

	private PegCursor.Mode m_cursorOverOverride = PegCursor.Mode.NONE;

	public bool DoubleClickEnabled { get; private set; }

	public float SetEnabledLastCallTime { get; private set; }

	protected virtual void Awake()
	{
		DoubleClickEnabled = DoubleClickEnabled || HasOverriddenDoubleClick();
	}

	protected virtual void OnDestroy()
	{
	}

	protected virtual void OnOver(InteractionState oldState)
	{
	}

	protected virtual void OnOut(InteractionState oldState)
	{
	}

	protected virtual void OnPress()
	{
	}

	protected virtual void OnTap()
	{
	}

	protected virtual void OnRelease()
	{
	}

	protected virtual void OnReleaseAll(bool mouseIsOver)
	{
	}

	protected virtual void OnDrag()
	{
	}

	protected virtual void OnDoubleClick()
	{
	}

	protected virtual void OnRightClick()
	{
	}

	protected virtual void OnHold()
	{
	}

	public void SetInteractionState(InteractionState state)
	{
		m_interactionState = state;
	}

	public virtual void TriggerOver()
	{
		if (m_enabled && !m_focused)
		{
			PrintLog("OVER", PegUILogLevel.ALL_EVENTS);
			m_focused = true;
			InteractionState interactionState = m_interactionState;
			m_interactionState = InteractionState.Over;
			OnOver(interactionState);
			DispatchEvent(new UIEvent(UIEventType.ROLLOVER, this));
		}
	}

	public virtual void TriggerOut()
	{
		if (m_enabled)
		{
			PrintLog("OUT", PegUILogLevel.ALL_EVENTS);
			m_focused = false;
			InteractionState interactionState = m_interactionState;
			m_interactionState = InteractionState.Out;
			OnOut(interactionState);
			DispatchEvent(new UIEvent(UIEventType.ROLLOUT, this));
		}
	}

	public virtual void TriggerPress()
	{
		if (m_enabled)
		{
			PrintLog("PRESS", PegUILogLevel.PRESS);
			m_focused = true;
			m_interactionState = InteractionState.Down;
			OnPress();
			DispatchEvent(new UIEvent(UIEventType.PRESS, this));
		}
	}

	public virtual void TriggerTap()
	{
		if (m_enabled)
		{
			PrintLog("TAP", PegUILogLevel.ALL_EVENTS, printOnScreen: true);
			m_interactionState = InteractionState.Up;
			OnTap();
			DispatchEvent(new UIEvent(UIEventType.TAP, this));
		}
	}

	public virtual void TriggerRelease()
	{
		if (m_enabled)
		{
			PrintLog("RELEASE", PegUILogLevel.ALL_EVENTS);
			m_interactionState = InteractionState.Up;
			if (!IsScrolling())
			{
				OnRelease();
				DispatchEvent(new UIEvent(UIEventType.RELEASE, this));
			}
		}
	}

	public void TriggerReleaseAll(bool mouseIsOver)
	{
		if (m_enabled)
		{
			m_interactionState = InteractionState.Up;
			OnReleaseAll(mouseIsOver);
			DispatchEvent(new UIReleaseAllEvent(mouseIsOver, this));
		}
	}

	public void TriggerDrag()
	{
		if (m_enabled)
		{
			PrintLog("DRAG", PegUILogLevel.ALL_EVENTS);
			m_interactionState = InteractionState.Down;
			OnDrag();
			DispatchEvent(new UIEvent(UIEventType.DRAG, this));
		}
	}

	public void TriggerHold()
	{
		if (m_enabled)
		{
			PrintLog("HOLD", PegUILogLevel.ALL_EVENTS);
			m_interactionState = InteractionState.Down;
			OnHold();
			DispatchEvent(new UIEvent(UIEventType.HOLD, this));
		}
	}

	public void TriggerDoubleClick()
	{
		if (m_enabled)
		{
			PrintLog("DCLICK", PegUILogLevel.ALL_EVENTS);
			m_interactionState = InteractionState.Down;
			OnDoubleClick();
			DispatchEvent(new UIEvent(UIEventType.DOUBLECLICK, this));
		}
	}

	public void TriggerRightClick()
	{
		if (m_enabled)
		{
			PrintLog("RCLICK", PegUILogLevel.ALL_EVENTS);
			OnRightClick();
			DispatchEvent(new UIEvent(UIEventType.RIGHTCLICK, this));
		}
	}

	public void SetDragTolerance(float newTolerance)
	{
		SetDragTolerance(Vector3.one * newTolerance);
	}

	public void SetDragTolerance(Vector3 newTolerance)
	{
		m_dragTolerance = newTolerance;
	}

	public Vector3 GetDragTolerance()
	{
		return m_dragTolerance;
	}

	public virtual bool AddEventListener(UIEventType type, UIEvent.Handler handler)
	{
		DoubleClickEnabled |= type == UIEventType.DOUBLECLICK;
		if (!m_eventListeners.TryGetValue(type, out var value))
		{
			value = new List<UIEvent.Handler>();
			m_eventListeners.Add(type, value);
			value.Add(handler);
			return true;
		}
		if (value.Contains(handler))
		{
			return false;
		}
		value.Add(handler);
		return true;
	}

	public virtual bool RemoveEventListener(UIEventType type, UIEvent.Handler handler)
	{
		if (!m_eventListeners.TryGetValue(type, out var value))
		{
			return false;
		}
		return value.Remove(handler);
	}

	public void ClearEventListeners()
	{
		m_eventListeners.Clear();
	}

	public bool HasEventListener(UIEventType type)
	{
		if (!m_eventListeners.TryGetValue(type, out var value))
		{
			return false;
		}
		return value.Count > 0;
	}

	public virtual void SetEnabled(bool enabled, bool isInternal = false)
	{
		if (!isInternal)
		{
			SetEnabledLastCallTime = Time.realtimeSinceStartup;
		}
		if (enabled)
		{
			PrintLog("ENABLE", PegUILogLevel.ALL_EVENTS);
		}
		else
		{
			PrintLog("DISABLE", PegUILogLevel.ALL_EVENTS);
		}
		m_enabled = enabled;
		if (!m_enabled)
		{
			m_focused = false;
		}
	}

	public bool IsEnabled()
	{
		return m_enabled;
	}

	public void SetReceiveReleaseWithoutMouseDown(bool receiveReleaseWithoutMouseDown)
	{
		m_receiveReleaseWithoutMouseDown = receiveReleaseWithoutMouseDown;
	}

	public bool GetReceiveReleaseWithoutMouseDown()
	{
		return m_receiveReleaseWithoutMouseDown;
	}

	public bool GetReceiveOverWithMouseDown()
	{
		if (!UniversalInputManager.Get().IsTouchMode())
		{
			return false;
		}
		return true;
	}

	public InteractionState GetInteractionState()
	{
		return m_interactionState;
	}

	public void SetData(object data)
	{
		m_data = data;
	}

	public object GetData()
	{
		return m_data;
	}

	public void SetOriginalLocalPosition()
	{
		SetOriginalLocalPosition(base.transform.localPosition);
	}

	public void SetOriginalLocalPosition(Vector3 pos)
	{
		m_originalLocalPosition = pos;
	}

	public Vector3 GetOriginalLocalPosition()
	{
		return m_originalLocalPosition;
	}

	public void SetCursorDown(PegCursor.Mode mode)
	{
		m_cursorDownOverride = mode;
	}

	public PegCursor.Mode GetCursorDown()
	{
		return m_cursorDownOverride;
	}

	public void SetCursorOver(PegCursor.Mode mode)
	{
		m_cursorOverOverride = mode;
	}

	public PegCursor.Mode GetCursorOver()
	{
		return m_cursorOverOverride;
	}

	private void DispatchEvent(UIEvent e)
	{
		if (m_eventListeners.TryGetValue(e.GetEventType(), out var value))
		{
			UIEvent.Handler[] array = value.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i](e);
			}
		}
	}

	private bool HasOverriddenDoubleClick()
	{
		Type type = GetType();
		Type typeFromHandle = typeof(PegUIElement);
		typeFromHandle.GetMethod("OnDoubleClick", BindingFlags.Instance | BindingFlags.NonPublic);
		type.GetMethod("OnDoubleClick", BindingFlags.Instance | BindingFlags.NonPublic);
		return GeneralUtils.IsOverriddenMethod(type, typeFromHandle, "OnDoubleClick");
	}

	private void PrintLog(string evt, PegUILogLevel logLevel, bool printOnScreen = false)
	{
		if (!(this == null) && !(base.gameObject == null) && HearthstoneApplication.IsInternal() && Options.Get().GetInt(Option.PEGUI_DEBUG) >= (int)logLevel)
		{
			string hierarchyPath = DebugUtils.GetHierarchyPath(base.gameObject, '/');
			string text = string.Format("{0,-7} {1}", evt + ":", hierarchyPath);
			Log.All.PrintInfo(text);
			if (printOnScreen)
			{
				Log.All.ForceScreenPrint(Log.LogLevel.Info, verbose: true, text);
			}
		}
	}

	private bool IsScrolling()
	{
		return GetComponentsInParent<UIBScrollable.IContent>().Any((UIBScrollable.IContent scrollable) => scrollable.Scrollable.IsTouchDragging());
	}
}
