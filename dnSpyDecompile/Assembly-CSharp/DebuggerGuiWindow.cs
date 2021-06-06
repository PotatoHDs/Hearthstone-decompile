using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000685 RID: 1669
public class DebuggerGuiWindow : DebuggerGui
{
	// Token: 0x06005D65 RID: 23909 RVA: 0x001E5FA1 File Offset: 0x001E41A1
	public DebuggerGuiWindow(string title, DebuggerGui.LayoutGui onGui, bool canClose = true, bool canResize = true) : base(title, onGui, true, false)
	{
		this.m_windowId = title.GetHashCode();
		this.m_canClose = canClose;
		this.m_canResize = canResize;
		base.OnChanged += this.HandleChange;
	}

	// Token: 0x1700058E RID: 1422
	// (get) Token: 0x06005D66 RID: 23910 RVA: 0x001E5FDA File Offset: 0x001E41DA
	// (set) Token: 0x06005D67 RID: 23911 RVA: 0x001E5FE2 File Offset: 0x001E41E2
	public Vector2 Position
	{
		get
		{
			return this.m_pos;
		}
		set
		{
			if (this.m_pos != value)
			{
				this.m_pos = value;
				this.UpdateWindowSize();
				base.InvokeOnChanged();
			}
		}
	}

	// Token: 0x1700058F RID: 1423
	// (get) Token: 0x06005D68 RID: 23912 RVA: 0x001E6005 File Offset: 0x001E4205
	// (set) Token: 0x06005D69 RID: 23913 RVA: 0x001E6010 File Offset: 0x001E4210
	private Vector2 Size
	{
		get
		{
			return this.m_size;
		}
		set
		{
			Vector2 size = this.GetScaledScreen().size;
			value.x = Mathf.Clamp(value.x, 100f, size.x);
			value.y = Mathf.Clamp(value.y, 48f, size.y);
			if (this.m_size != value)
			{
				this.m_size = value;
				this.UpdateWindowSize();
				base.InvokeOnChanged();
			}
		}
	}

	// Token: 0x06005D6A RID: 23914 RVA: 0x001E6088 File Offset: 0x001E4288
	public bool IsMouseOver()
	{
		Vector3 scaledMouse = this.GetScaledMouse();
		return this.m_window.Contains(scaledMouse);
	}

	// Token: 0x06005D6B RID: 23915 RVA: 0x001E60A8 File Offset: 0x001E42A8
	public void ResizeToFit(Vector2 dims)
	{
		this.Size = dims + DebuggerGuiWindow.SIZE_PADDING;
	}

	// Token: 0x06005D6C RID: 23916 RVA: 0x001E60BB File Offset: 0x001E42BB
	public void ResizeToFit(float width, float height)
	{
		this.ResizeToFit(new Vector2(width, height));
	}

	// Token: 0x06005D6D RID: 23917 RVA: 0x001E60CA File Offset: 0x001E42CA
	public Rect GetHeaderRect()
	{
		return new Rect(5f, 5f, this.m_size.x - 10f, 24f);
	}

	// Token: 0x06005D6E RID: 23918 RVA: 0x001E60F1 File Offset: 0x001E42F1
	public void Layout()
	{
		this.Layout(new Rect(this.Position, this.Size));
	}

	// Token: 0x06005D6F RID: 23919 RVA: 0x001E610C File Offset: 0x001E430C
	public override Rect Layout(Rect space)
	{
		if (!this.m_isShown)
		{
			return space;
		}
		this.Position = space.min;
		this.Size = space.size;
		this.UpdateWindowSize();
		this.m_window = GUI.Window(this.m_windowId, this.m_window, new GUI.WindowFunction(this.WindowFunction), "");
		this.ConstrainPosition(this.m_window);
		if (this.m_canResize && this.m_isExpanded)
		{
			Vector3 scaledMouse = this.GetScaledMouse();
			float num = PlatformSettings.IsMobile() ? 20f : 10f;
			Rect window = this.m_window;
			window.xMin += num;
			window.yMin += 5f;
			window.xMax -= num;
			window.yMax -= num;
			if (Input.GetMouseButtonDown(0) && this.m_window.Contains(scaledMouse) && !window.Contains(scaledMouse))
			{
				this.m_resizingSide.x = (float)(((scaledMouse.x >= window.xMax) ? 1 : 0) + ((scaledMouse.x <= window.xMin) ? -1 : 0));
				this.m_resizingSide.y = (float)(((scaledMouse.y >= window.yMax) ? 1 : 0) + ((scaledMouse.y <= window.yMin) ? -1 : 0));
				this.m_resizeClickStart = scaledMouse;
				this.m_resizeInitialWindow = this.m_window;
			}
			else if (this.IsResizing())
			{
				if (Input.GetMouseButton(0))
				{
					Vector2 vector = scaledMouse - this.m_resizeClickStart;
					if (this.m_resizingSide.x < 0f)
					{
						this.m_window.xMin = Mathf.Min(this.m_resizeInitialWindow.xMin + vector.x, this.m_resizeInitialWindow.xMax - 100f);
					}
					else if (this.m_resizingSide.x > 0f)
					{
						this.m_window.xMax = Mathf.Max(this.m_resizeInitialWindow.xMax + vector.x, this.m_resizeInitialWindow.xMin + 100f);
					}
					if (this.m_resizingSide.y < 0f)
					{
						this.m_window.yMin = Mathf.Min(this.m_resizeInitialWindow.yMin + vector.y, this.m_resizeInitialWindow.yMax - 48f);
					}
					else if (this.m_resizingSide.y > 0f)
					{
						this.m_window.yMax = Mathf.Max(this.m_resizeInitialWindow.yMax + vector.y, this.m_resizeInitialWindow.yMin + 48f);
					}
					this.m_pos = this.m_window.min;
					this.m_size = this.m_window.size;
					base.InvokeOnChanged();
				}
				if (Input.GetMouseButtonUp(0))
				{
					this.m_resizingSide = new Vector2(0f, 0f);
				}
			}
		}
		return new Rect(this.m_window.xMin, this.m_window.yMax, space.width, space.height - this.m_window.height);
	}

	// Token: 0x06005D70 RID: 23920 RVA: 0x001E6450 File Offset: 0x001E4650
	private void HandleChange()
	{
		Rect window = this.m_window;
		this.UpdateWindowSize();
		if (!window.Equals(this.m_window))
		{
			this.m_spaceIsDirty = true;
		}
	}

	// Token: 0x06005D71 RID: 23921 RVA: 0x001E6480 File Offset: 0x001E4680
	private void UpdateWindowSize()
	{
		this.m_window = new Rect(this.m_pos.x, this.m_pos.y, (this.m_isExpanded || this.collapsedWidth == null) ? this.m_size.x : this.collapsedWidth.Value, this.m_isExpanded ? this.m_size.y : 34f);
	}

	// Token: 0x06005D72 RID: 23922 RVA: 0x001E64F8 File Offset: 0x001E46F8
	private void WindowFunction(int windowId)
	{
		this.m_spaceIsDirty = false;
		Rect space = new Rect(5f, 5f, this.m_window.width - 10f, this.m_window.height - 10f);
		if (this.m_canClose && GUI.Button(new Rect(space.xMax - 24f, space.yMin, 24f, 24f), "✕"))
		{
			base.IsShown = false;
		}
		space = base.LayoutHeader(space);
		if (this.m_spaceIsDirty)
		{
			return;
		}
		space = base.LayoutInternal(space);
		if (!this.IsResizing())
		{
			GUI.DragWindow();
		}
	}

	// Token: 0x06005D73 RID: 23923 RVA: 0x001E65A3 File Offset: 0x001E47A3
	protected bool IsResizing()
	{
		return this.m_resizingSide.sqrMagnitude > 0f;
	}

	// Token: 0x06005D74 RID: 23924 RVA: 0x001E65B8 File Offset: 0x001E47B8
	protected Rect GetScaledScreen()
	{
		return new Rect(0f, 0f, Math.Max(0f, (float)Screen.width / GUI.matrix.lossyScale.x), Math.Max(0f, (float)Screen.height / GUI.matrix.lossyScale.y));
	}

	// Token: 0x06005D75 RID: 23925 RVA: 0x001E661C File Offset: 0x001E481C
	protected Vector3 GetScaledMouse()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.y = (float)Screen.height - mousePosition.y;
		mousePosition.x /= GUI.matrix.lossyScale.x;
		mousePosition.y /= GUI.matrix.lossyScale.y;
		return mousePosition;
	}

	// Token: 0x06005D76 RID: 23926 RVA: 0x001E6680 File Offset: 0x001E4880
	private void ConstrainPosition(Rect window)
	{
		Vector2 vector = new Vector2(48f, 24f);
		Rect scaledScreen = this.GetScaledScreen();
		this.Position = new Vector2(Mathf.Clamp(window.x, scaledScreen.xMin - window.width + vector.x, scaledScreen.xMax - vector.x), Mathf.Clamp(window.y, scaledScreen.yMin - window.height + vector.y, scaledScreen.yMax - vector.y));
	}

	// Token: 0x06005D77 RID: 23927 RVA: 0x001E6710 File Offset: 0x001E4910
	internal override string SerializeToString()
	{
		string serial_ID = DebuggerGuiWindow.SERIAL_ID;
		Vector2 position = this.Position;
		Vector2 size = this.Size;
		return serial_ID + string.Join("x", new List<string>
		{
			Mathf.RoundToInt(position.x).ToString(),
			Mathf.RoundToInt(position.y).ToString(),
			Mathf.RoundToInt(size.x).ToString(),
			Mathf.RoundToInt(size.y).ToString()
		}.ToArray()) + base.SerializeToString();
	}

	// Token: 0x06005D78 RID: 23928 RVA: 0x001E67BC File Offset: 0x001E49BC
	internal override void DeserializeFromString(string str)
	{
		int num = str.IndexOf(DebuggerGuiWindow.SERIAL_ID);
		int num2 = str.IndexOf(DebuggerGui.SERIAL_ID);
		if (num >= 0)
		{
			num += DebuggerGuiWindow.SERIAL_ID.Length;
			List<string> list = new List<string>();
			string text = str.Substring(num, (num2 > num) ? (num2 - num) : (str.Length - num));
			list.AddRange(text.Split(new char[]
			{
				'x'
			}));
			Vector2 position = this.Position;
			Vector2 size = this.Size;
			if (float.TryParse(list.ElementAtOrDefault(0), out position.x) && float.TryParse(list.ElementAtOrDefault(1), out position.y))
			{
				this.Position = position;
			}
			if (this.m_canResize && float.TryParse(list.ElementAtOrDefault(2), out size.x) && float.TryParse(list.ElementAtOrDefault(3), out size.y))
			{
				this.Size = size;
			}
			this.ConstrainPosition(this.m_window);
		}
		if (num2 >= 0)
		{
			base.DeserializeFromString(str.Substring(num2));
		}
	}

	// Token: 0x04004EF5 RID: 20213
	public float? collapsedWidth;

	// Token: 0x04004EF6 RID: 20214
	protected Vector2 m_pos;

	// Token: 0x04004EF7 RID: 20215
	protected Vector2 m_size;

	// Token: 0x04004EF8 RID: 20216
	protected Rect m_window;

	// Token: 0x04004EF9 RID: 20217
	protected int m_windowId;

	// Token: 0x04004EFA RID: 20218
	protected bool m_spaceIsDirty;

	// Token: 0x04004EFB RID: 20219
	protected bool m_canClose;

	// Token: 0x04004EFC RID: 20220
	protected bool m_canResize;

	// Token: 0x04004EFD RID: 20221
	protected Vector2 m_resizingSide;

	// Token: 0x04004EFE RID: 20222
	protected Vector3 m_resizeClickStart;

	// Token: 0x04004EFF RID: 20223
	protected Rect m_resizeInitialWindow;

	// Token: 0x04004F00 RID: 20224
	public const float PADDING = 5f;

	// Token: 0x04004F01 RID: 20225
	protected const float RESIZE_HANDLE_SIZE = 10f;

	// Token: 0x04004F02 RID: 20226
	protected const float MOBILE_RESIZE_HANDLE_SIZE = 20f;

	// Token: 0x04004F03 RID: 20227
	protected static readonly Vector2 SIZE_PADDING = new Vector2(10f, 34f);

	// Token: 0x04004F04 RID: 20228
	private const float MIN_WINDOW_WIDTH = 100f;

	// Token: 0x04004F05 RID: 20229
	private const float MIN_WINDOW_HEIGHT = 48f;

	// Token: 0x04004F06 RID: 20230
	private const string CLOSE_SYMBOL = "✕";

	// Token: 0x04004F07 RID: 20231
	internal new static string SERIAL_ID = "[W]";
}
