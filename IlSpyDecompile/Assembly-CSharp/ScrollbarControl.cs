using UnityEngine;

public class ScrollbarControl : MonoBehaviour
{
	public delegate void UpdateHandler(float val);

	public delegate void FinishHandler();

	public GameObject m_Thumb;

	public PegUIElement m_PressElement;

	public Collider m_DragCollider;

	public Transform m_LeftBone;

	public Transform m_RightBone;

	private bool m_dragging;

	private float m_thumbUnitPos;

	private float m_prevThumbUnitPos;

	private UpdateHandler m_updateHandler;

	private FinishHandler m_finishHandler;

	private void Awake()
	{
		m_PressElement.AddEventListener(UIEventType.PRESS, OnPressElementPress);
		m_PressElement.AddEventListener(UIEventType.RELEASEALL, OnPressElementReleaseAll);
		m_DragCollider.enabled = false;
	}

	private void Update()
	{
		UpdateDrag();
	}

	private void OnDestroy()
	{
		if (UniversalInputManager.Get() != null)
		{
			UniversalInputManager.Get().UnregisterMouseOnOrOffScreenListener(OnMouseOnOrOffScreen);
		}
	}

	public float GetValue()
	{
		return m_thumbUnitPos;
	}

	public void SetValue(float val)
	{
		m_thumbUnitPos = Mathf.Clamp01(val);
		m_prevThumbUnitPos = m_thumbUnitPos;
		UpdateThumb();
	}

	public UpdateHandler GetUpdateHandler()
	{
		return m_updateHandler;
	}

	public void SetUpdateHandler(UpdateHandler handler)
	{
		m_updateHandler = handler;
	}

	public FinishHandler GetFinishHandler()
	{
		return m_finishHandler;
	}

	public void SetFinishHandler(FinishHandler handler)
	{
		m_finishHandler = handler;
	}

	private void OnPressElementPress(UIEvent e)
	{
		HandlePress();
	}

	private void OnPressElementReleaseAll(UIEvent e)
	{
		HandleRelease();
		FireFinishEvent();
	}

	private void OnMouseOnOrOffScreen(bool onScreen)
	{
		if (!onScreen)
		{
			HandleOutOfBounds();
		}
	}

	private void UpdateDrag()
	{
		if (m_dragging)
		{
			if (UniversalInputManager.Get().GetInputHitInfo(1 << m_DragCollider.gameObject.layer, out var hitInfo) && hitInfo.collider == m_DragCollider)
			{
				float x = m_LeftBone.position.x;
				float num = m_RightBone.position.x - x;
				m_thumbUnitPos = Mathf.Clamp01((hitInfo.point.x - x) / num);
				UpdateThumb();
				HandleThumbUpdate();
			}
			else
			{
				m_thumbUnitPos = m_prevThumbUnitPos;
				HandleOutOfBounds();
			}
		}
	}

	private void UpdateThumb()
	{
		m_Thumb.transform.localPosition = Vector3.Lerp(m_LeftBone.localPosition, m_RightBone.localPosition, m_thumbUnitPos);
	}

	private void HandlePress()
	{
		m_dragging = true;
		UniversalInputManager.Get().RegisterMouseOnOrOffScreenListener(OnMouseOnOrOffScreen);
		m_PressElement.AddEventListener(UIEventType.RELEASEALL, OnPressElementReleaseAll);
		m_PressElement.GetComponent<Collider>().enabled = false;
		m_DragCollider.enabled = true;
	}

	private void HandleRelease()
	{
		m_DragCollider.enabled = false;
		m_PressElement.GetComponent<Collider>().enabled = true;
		m_PressElement.RemoveEventListener(UIEventType.RELEASEALL, OnPressElementReleaseAll);
		UniversalInputManager.Get().UnregisterMouseOnOrOffScreenListener(OnMouseOnOrOffScreen);
		m_dragging = false;
	}

	private void HandleThumbUpdate()
	{
		float prevThumbUnitPos = m_prevThumbUnitPos;
		m_prevThumbUnitPos = m_thumbUnitPos;
		if (!Mathf.Approximately(m_thumbUnitPos, prevThumbUnitPos))
		{
			FireUpdateEvent();
		}
	}

	private void HandleOutOfBounds()
	{
		UpdateThumb();
		HandleThumbUpdate();
		HandleRelease();
		FireFinishEvent();
	}

	private void FireUpdateEvent()
	{
		if (m_updateHandler != null)
		{
			m_updateHandler(GetValue());
		}
	}

	private void FireFinishEvent()
	{
		if (m_finishHandler != null)
		{
			m_finishHandler();
		}
	}
}
