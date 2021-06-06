using UnityEngine;

public class SwipeHandler : PegUICustomBehavior
{
	public enum SWIPE_DIRECTION
	{
		RIGHT,
		LEFT
	}

	public delegate void DelSwipeListener(SWIPE_DIRECTION direction);

	public Transform m_upperLeftBone;

	public Transform m_lowerRightBone;

	private const float SWIPE_DETECT_DURATION = 0.1f;

	private const float SWIPE_DETECT_WIDTH = 0.09f;

	private const float SWIPE_FROM_TARGET_PENALTY = 0.035f;

	private float m_swipeDetectTimer;

	private bool m_checkingPossibleSwipe;

	private Vector3 m_swipeStart;

	private PegUIElement m_swipeElement;

	private Rect m_swipeRect;

	private static DelSwipeListener m_swipeListener;

	public override bool UpdateUI()
	{
		if (!UniversalInputManager.Get().IsTouchMode())
		{
			return false;
		}
		return HandleSwipeGesture();
	}

	private bool InSwipeRect(Vector2 v)
	{
		if (v.x >= m_swipeRect.x && v.x <= m_swipeRect.x + m_swipeRect.width && v.y >= m_swipeRect.y)
		{
			return v.y <= m_swipeRect.y + m_swipeRect.height;
		}
		return false;
	}

	private bool CheckSwipe()
	{
		float num = m_swipeStart.x - UniversalInputManager.Get().GetMousePosition().x;
		float num2 = 0.09f + ((m_swipeElement != null) ? 0.035f : 0f);
		float num3 = (float)Screen.width * num2;
		if (Mathf.Abs(num) > num3)
		{
			SWIPE_DIRECTION direction = ((!(num < 0f)) ? SWIPE_DIRECTION.LEFT : SWIPE_DIRECTION.RIGHT);
			if (m_swipeListener != null)
			{
				m_swipeListener(direction);
			}
			return true;
		}
		return false;
	}

	private bool HandleSwipeGesture()
	{
		m_swipeRect = CameraUtils.CreateGUIScreenRect(Camera.main, m_upperLeftBone, m_lowerRightBone);
		if (UniversalInputManager.Get().GetMouseButtonDown(0) && InSwipeRect(UniversalInputManager.Get().GetMousePosition()))
		{
			m_checkingPossibleSwipe = true;
			m_swipeDetectTimer = 0f;
			m_swipeStart = UniversalInputManager.Get().GetMousePosition();
			m_swipeElement = PegUI.Get().FindHitElement();
			return true;
		}
		if (m_checkingPossibleSwipe)
		{
			m_swipeDetectTimer += Time.deltaTime;
			if (UniversalInputManager.Get().GetMouseButtonUp(0))
			{
				m_checkingPossibleSwipe = false;
				if (!CheckSwipe() && m_swipeElement != null && m_swipeElement == PegUI.Get().FindHitElement())
				{
					m_swipeElement.TriggerPress();
					m_swipeElement.TriggerRelease();
				}
				return true;
			}
			if (!(m_swipeDetectTimer >= 0.1f))
			{
				return true;
			}
			m_checkingPossibleSwipe = false;
			if (CheckSwipe())
			{
				return true;
			}
			if (m_swipeElement != null)
			{
				PegUI.Get().DoMouseDown(m_swipeElement, m_swipeStart);
			}
		}
		return false;
	}

	public void RegisterSwipeListener(DelSwipeListener listener)
	{
		m_swipeListener = listener;
	}
}
