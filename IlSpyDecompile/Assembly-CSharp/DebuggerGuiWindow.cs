using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DebuggerGuiWindow : DebuggerGui
{
	public float? collapsedWidth;

	protected Vector2 m_pos;

	protected Vector2 m_size;

	protected Rect m_window;

	protected int m_windowId;

	protected bool m_spaceIsDirty;

	protected bool m_canClose;

	protected bool m_canResize;

	protected Vector2 m_resizingSide;

	protected Vector3 m_resizeClickStart;

	protected Rect m_resizeInitialWindow;

	public const float PADDING = 5f;

	protected const float RESIZE_HANDLE_SIZE = 10f;

	protected const float MOBILE_RESIZE_HANDLE_SIZE = 20f;

	protected static readonly Vector2 SIZE_PADDING = new Vector2(10f, 34f);

	private const float MIN_WINDOW_WIDTH = 100f;

	private const float MIN_WINDOW_HEIGHT = 48f;

	private const string CLOSE_SYMBOL = "✕";

	internal new static string SERIAL_ID = "[W]";

	public Vector2 Position
	{
		get
		{
			return m_pos;
		}
		set
		{
			if (m_pos != value)
			{
				m_pos = value;
				UpdateWindowSize();
				InvokeOnChanged();
			}
		}
	}

	private Vector2 Size
	{
		get
		{
			return m_size;
		}
		set
		{
			Vector2 size = GetScaledScreen().size;
			value.x = Mathf.Clamp(value.x, 100f, size.x);
			value.y = Mathf.Clamp(value.y, 48f, size.y);
			if (m_size != value)
			{
				m_size = value;
				UpdateWindowSize();
				InvokeOnChanged();
			}
		}
	}

	public DebuggerGuiWindow(string title, LayoutGui onGui, bool canClose = true, bool canResize = true)
		: base(title, onGui)
	{
		m_windowId = title.GetHashCode();
		m_canClose = canClose;
		m_canResize = canResize;
		base.OnChanged += HandleChange;
	}

	public bool IsMouseOver()
	{
		Vector3 scaledMouse = GetScaledMouse();
		return m_window.Contains(scaledMouse);
	}

	public void ResizeToFit(Vector2 dims)
	{
		Size = dims + SIZE_PADDING;
	}

	public void ResizeToFit(float width, float height)
	{
		ResizeToFit(new Vector2(width, height));
	}

	public Rect GetHeaderRect()
	{
		return new Rect(5f, 5f, m_size.x - 10f, 24f);
	}

	public void Layout()
	{
		Layout(new Rect(Position, Size));
	}

	public override Rect Layout(Rect space)
	{
		if (!m_isShown)
		{
			return space;
		}
		Position = space.min;
		Size = space.size;
		UpdateWindowSize();
		m_window = GUI.Window(m_windowId, m_window, WindowFunction, "");
		ConstrainPosition(m_window);
		if (m_canResize && m_isExpanded)
		{
			Vector3 scaledMouse = GetScaledMouse();
			float num = (PlatformSettings.IsMobile() ? 20f : 10f);
			Rect window = m_window;
			window.xMin += num;
			window.yMin += 5f;
			window.xMax -= num;
			window.yMax -= num;
			if (Input.GetMouseButtonDown(0) && m_window.Contains(scaledMouse) && !window.Contains(scaledMouse))
			{
				m_resizingSide.x = ((scaledMouse.x >= window.xMax) ? 1 : 0) + ((scaledMouse.x <= window.xMin) ? (-1) : 0);
				m_resizingSide.y = ((scaledMouse.y >= window.yMax) ? 1 : 0) + ((scaledMouse.y <= window.yMin) ? (-1) : 0);
				m_resizeClickStart = scaledMouse;
				m_resizeInitialWindow = m_window;
			}
			else if (IsResizing())
			{
				if (Input.GetMouseButton(0))
				{
					Vector2 vector = scaledMouse - m_resizeClickStart;
					if (m_resizingSide.x < 0f)
					{
						m_window.xMin = Mathf.Min(m_resizeInitialWindow.xMin + vector.x, m_resizeInitialWindow.xMax - 100f);
					}
					else if (m_resizingSide.x > 0f)
					{
						m_window.xMax = Mathf.Max(m_resizeInitialWindow.xMax + vector.x, m_resizeInitialWindow.xMin + 100f);
					}
					if (m_resizingSide.y < 0f)
					{
						m_window.yMin = Mathf.Min(m_resizeInitialWindow.yMin + vector.y, m_resizeInitialWindow.yMax - 48f);
					}
					else if (m_resizingSide.y > 0f)
					{
						m_window.yMax = Mathf.Max(m_resizeInitialWindow.yMax + vector.y, m_resizeInitialWindow.yMin + 48f);
					}
					m_pos = m_window.min;
					m_size = m_window.size;
					InvokeOnChanged();
				}
				if (Input.GetMouseButtonUp(0))
				{
					m_resizingSide = new Vector2(0f, 0f);
				}
			}
		}
		return new Rect(m_window.xMin, m_window.yMax, space.width, space.height - m_window.height);
	}

	private void HandleChange()
	{
		Rect window = m_window;
		UpdateWindowSize();
		if (!window.Equals(m_window))
		{
			m_spaceIsDirty = true;
		}
	}

	private void UpdateWindowSize()
	{
		m_window = new Rect(m_pos.x, m_pos.y, (m_isExpanded || !collapsedWidth.HasValue) ? m_size.x : collapsedWidth.Value, m_isExpanded ? m_size.y : 34f);
	}

	private void WindowFunction(int windowId)
	{
		m_spaceIsDirty = false;
		Rect space = new Rect(5f, 5f, m_window.width - 10f, m_window.height - 10f);
		if (m_canClose && GUI.Button(new Rect(space.xMax - 24f, space.yMin, 24f, 24f), "✕"))
		{
			base.IsShown = false;
		}
		space = LayoutHeader(space);
		if (!m_spaceIsDirty)
		{
			space = LayoutInternal(space);
			if (!IsResizing())
			{
				GUI.DragWindow();
			}
		}
	}

	protected bool IsResizing()
	{
		return m_resizingSide.sqrMagnitude > 0f;
	}

	protected Rect GetScaledScreen()
	{
		return new Rect(0f, 0f, Math.Max(0f, (float)Screen.width / GUI.matrix.lossyScale.x), Math.Max(0f, (float)Screen.height / GUI.matrix.lossyScale.y));
	}

	protected Vector3 GetScaledMouse()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.y = (float)Screen.height - mousePosition.y;
		mousePosition.x /= GUI.matrix.lossyScale.x;
		mousePosition.y /= GUI.matrix.lossyScale.y;
		return mousePosition;
	}

	private void ConstrainPosition(Rect window)
	{
		Vector2 vector = new Vector2(48f, 24f);
		Rect scaledScreen = GetScaledScreen();
		Position = new Vector2(Mathf.Clamp(window.x, scaledScreen.xMin - window.width + vector.x, scaledScreen.xMax - vector.x), Mathf.Clamp(window.y, scaledScreen.yMin - window.height + vector.y, scaledScreen.yMax - vector.y));
	}

	internal override string SerializeToString()
	{
		string sERIAL_ID = SERIAL_ID;
		Vector2 position = Position;
		Vector2 size = Size;
		return string.Concat(sERIAL_ID + string.Join("x", new List<string>
		{
			Mathf.RoundToInt(position.x).ToString(),
			Mathf.RoundToInt(position.y).ToString(),
			Mathf.RoundToInt(size.x).ToString(),
			Mathf.RoundToInt(size.y).ToString()
		}.ToArray()), base.SerializeToString());
	}

	internal override void DeserializeFromString(string str)
	{
		int num = str.IndexOf(SERIAL_ID);
		int num2 = str.IndexOf(DebuggerGui.SERIAL_ID);
		if (num >= 0)
		{
			num += SERIAL_ID.Length;
			List<string> list = new List<string>();
			string text = str.Substring(num, (num2 > num) ? (num2 - num) : (str.Length - num));
			list.AddRange(text.Split('x'));
			Vector2 position = Position;
			Vector2 size = Size;
			if (float.TryParse(list.ElementAtOrDefault(0), out position.x) && float.TryParse(list.ElementAtOrDefault(1), out position.y))
			{
				Position = position;
			}
			if (m_canResize && float.TryParse(list.ElementAtOrDefault(2), out size.x) && float.TryParse(list.ElementAtOrDefault(3), out size.y))
			{
				Size = size;
			}
			ConstrainPosition(m_window);
		}
		if (num2 >= 0)
		{
			base.DeserializeFromString(str.Substring(num2));
		}
	}
}
