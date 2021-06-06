using UnityEngine;

public class PegCursor : MonoBehaviour
{
	public enum Mode
	{
		UP,
		DOWN,
		OVER,
		DRAG,
		STOPDRAG,
		WAITING,
		STOPWAITING,
		LEFTARROW,
		RIGHTARROW,
		UPARROW,
		DOWNARROW,
		NONE
	}

	public Texture2D m_cursorUp;

	public Vector2 m_cursorUpHotspot = Vector2.zero;

	public Texture2D m_cursorDown;

	public Vector2 m_cursorDownHotspot = Vector2.zero;

	public Texture2D m_cursorDrag;

	public Vector2 m_cursorDragHotspot = Vector2.zero;

	public Texture2D m_cursorOver;

	public Vector2 m_cursorOverHotspot = Vector2.zero;

	public Texture2D m_cursorWaiting;

	public Vector2 m_cursorWaitingHotspot = Vector2.zero;

	public Texture2D m_cursorWaitingDown;

	public Vector2 m_cursorWaitingDownHotspot = Vector2.zero;

	public Texture2D m_cursorWaitingUp;

	public Vector2 m_cursorWaitingUpHotspot = Vector2.zero;

	public Texture2D m_leftArrow;

	public Vector2 m_leftArrowHotspot = Vector2.zero;

	public Texture2D m_rightArrow;

	public Vector2 m_rightArrowHotspot = Vector2.zero;

	public Texture2D m_upArrow;

	public Vector2 m_upArrowHotspot = Vector2.zero;

	public Texture2D m_downArrow;

	public Vector2 m_downArrowHotspot = Vector2.zero;

	public Texture2D m_cursorUp64;

	public Vector2 m_cursorUpHotspot64 = Vector2.zero;

	public Texture2D m_cursorDown64;

	public Vector2 m_cursorDownHotspot64 = Vector2.zero;

	public Texture2D m_cursorDrag64;

	public Vector2 m_cursorDragHotspot64 = Vector2.zero;

	public Texture2D m_cursorOver64;

	public Vector2 m_cursorOverHotspot64 = Vector2.zero;

	public Texture2D m_cursorWaiting64;

	public Vector2 m_cursorWaitingHotspot64 = Vector2.zero;

	public Texture2D m_cursorWaitingDown64;

	public Vector2 m_cursorWaitingDownHotspot64 = Vector2.zero;

	public Texture2D m_cursorWaitingUp64;

	public Vector2 m_cursorWaitingUpHotspot64 = Vector2.zero;

	public Texture2D m_leftArrow64;

	public Vector2 m_leftArrowHotspot64 = Vector2.zero;

	public Texture2D m_rightArrow64;

	public Vector2 m_rightArrowHotspot64 = Vector2.zero;

	public Texture2D m_upArrow64;

	public Vector2 m_upArrowHotspot64 = Vector2.zero;

	public Texture2D m_downArrow64;

	public Vector2 m_downArrowHotspot64 = Vector2.zero;

	public GameObject m_explosionPrefab;

	private Texture2D m_cursorTexture;

	private Mode m_currentMode;

	private static PegCursor s_instance;

	private void Awake()
	{
		s_instance = this;
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public static PegCursor Get()
	{
		return s_instance;
	}

	public void Show()
	{
		Cursor.visible = true;
	}

	public void Hide()
	{
		Cursor.visible = false;
	}

	public void SetMode(Mode mode)
	{
		bool flag = false;
		if (m_currentMode == Mode.WAITING)
		{
			switch (mode)
			{
			case Mode.DOWN:
				if (flag)
				{
					Cursor.SetCursor(m_cursorWaitingDown64, m_cursorWaitingDownHotspot64, CursorMode.Auto);
				}
				else
				{
					Cursor.SetCursor(m_cursorWaitingDown, m_cursorWaitingDownHotspot, CursorMode.Auto);
				}
				return;
			case Mode.UP:
				if (flag)
				{
					Cursor.SetCursor(m_cursorWaiting64, m_cursorWaitingHotspot64, CursorMode.Auto);
				}
				else
				{
					Cursor.SetCursor(m_cursorWaiting, m_cursorWaitingHotspot, CursorMode.Auto);
				}
				return;
			default:
				return;
			case Mode.STOPWAITING:
				break;
			}
		}
		if (m_currentMode == Mode.DRAG && mode != Mode.STOPDRAG)
		{
			return;
		}
		m_currentMode = mode;
		if (flag)
		{
			switch (mode)
			{
			case Mode.UP:
				Cursor.SetCursor(m_cursorUp64, m_cursorUpHotspot64, CursorMode.Auto);
				break;
			case Mode.DOWN:
				Cursor.SetCursor(m_cursorDown64, m_cursorDownHotspot64, CursorMode.Auto);
				break;
			case Mode.OVER:
				Cursor.SetCursor(m_cursorUp64, m_cursorUpHotspot64, CursorMode.Auto);
				break;
			case Mode.DRAG:
				Cursor.SetCursor(m_cursorDrag64, m_cursorDragHotspot64, CursorMode.Auto);
				break;
			case Mode.WAITING:
				Cursor.SetCursor(m_cursorWaiting64, m_cursorWaitingHotspot64, CursorMode.Auto);
				break;
			case Mode.STOPDRAG:
			case Mode.STOPWAITING:
				Cursor.SetCursor(m_cursorUp64, m_cursorUpHotspot64, CursorMode.Auto);
				break;
			case Mode.LEFTARROW:
				Cursor.SetCursor(m_leftArrow64, m_leftArrowHotspot64, CursorMode.Auto);
				break;
			case Mode.RIGHTARROW:
				Cursor.SetCursor(m_rightArrow64, m_rightArrowHotspot64, CursorMode.Auto);
				break;
			case Mode.UPARROW:
				Cursor.SetCursor(m_upArrow64, m_upArrowHotspot64, CursorMode.Auto);
				break;
			case Mode.DOWNARROW:
				Cursor.SetCursor(m_downArrow64, m_downArrowHotspot64, CursorMode.Auto);
				break;
			}
		}
		else
		{
			switch (mode)
			{
			case Mode.UP:
				Cursor.SetCursor(m_cursorUp, m_cursorUpHotspot, CursorMode.Auto);
				break;
			case Mode.DOWN:
				Cursor.SetCursor(m_cursorDown, m_cursorDownHotspot, CursorMode.Auto);
				break;
			case Mode.OVER:
				Cursor.SetCursor(m_cursorUp, m_cursorUpHotspot, CursorMode.Auto);
				break;
			case Mode.DRAG:
				Cursor.SetCursor(m_cursorDrag, m_cursorDragHotspot, CursorMode.Auto);
				break;
			case Mode.WAITING:
				Cursor.SetCursor(m_cursorWaiting, m_cursorWaitingHotspot, CursorMode.Auto);
				break;
			case Mode.STOPDRAG:
			case Mode.STOPWAITING:
				Cursor.SetCursor(m_cursorUp, m_cursorUpHotspot, CursorMode.Auto);
				break;
			case Mode.LEFTARROW:
				Cursor.SetCursor(m_leftArrow, m_leftArrowHotspot, CursorMode.Auto);
				break;
			case Mode.RIGHTARROW:
				Cursor.SetCursor(m_rightArrow, m_rightArrowHotspot, CursorMode.Auto);
				break;
			case Mode.UPARROW:
				Cursor.SetCursor(m_upArrow, m_upArrowHotspot, CursorMode.Auto);
				break;
			case Mode.DOWNARROW:
				Cursor.SetCursor(m_downArrow, m_downArrowHotspot, CursorMode.Auto);
				break;
			}
		}
	}

	public Mode GetMode()
	{
		return m_currentMode;
	}

	public GameObject GetExplosionPrefab()
	{
		return m_explosionPrefab;
	}
}
