using UnityEngine;

public class Scrollbar : MonoBehaviour
{
	public ScrollBarThumb m_thumb;

	public GameObject m_track;

	public Vector3 m_sliderStartLocalPos;

	public Vector3 m_sliderEndLocalPos;

	public GameObject m_scrollArea;

	public BoxCollider m_scrollWindow;

	protected bool m_isActive = true;

	protected bool m_isDragging;

	protected float m_scrollValue;

	protected Vector3 m_scrollAreaStartPos;

	protected Vector3 m_scrollAreaEndPos;

	protected float m_stepSize;

	protected Vector3 m_thumbPosition;

	protected Bounds m_childrenBounds;

	protected float m_scrollWindowHeight;

	public float ScrollValue => m_scrollValue;

	protected virtual void Awake()
	{
		m_scrollWindowHeight = m_scrollWindow.size.z;
		m_scrollWindow.enabled = false;
	}

	public bool IsActive()
	{
		return m_isActive;
	}

	private void Update()
	{
		if (!m_isActive)
		{
			return;
		}
		if (InputIsOver())
		{
			if (Input.GetAxis("Mouse ScrollWheel") < 0f)
			{
				ScrollDown();
			}
			if (Input.GetAxis("Mouse ScrollWheel") > 0f)
			{
				ScrollUp();
			}
		}
		if (m_thumb.IsDragging())
		{
			Drag();
		}
	}

	public void Drag()
	{
		Vector3 min = m_track.GetComponent<BoxCollider>().bounds.min;
		Camera camera = CameraUtils.FindFirstByLayer(m_track.layer);
		Plane plane = new Plane(-camera.transform.forward, min);
		Ray ray = camera.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition());
		if (plane.Raycast(ray, out var enter))
		{
			Vector3 vector = base.transform.InverseTransformPoint(ray.GetPoint(enter));
			TransformUtil.SetLocalPosZ(m_thumb.gameObject, Mathf.Clamp(vector.z, m_sliderStartLocalPos.z, m_sliderEndLocalPos.z));
			m_scrollValue = Mathf.Clamp01((vector.z - m_sliderStartLocalPos.z) / (m_sliderEndLocalPos.z - m_sliderStartLocalPos.z));
			UpdateScrollAreaPosition(tween: false);
		}
	}

	public virtual void Show()
	{
		m_isActive = true;
		ShowThumb(show: true);
		base.gameObject.SetActive(value: true);
	}

	public virtual void Hide()
	{
		m_isActive = false;
		ShowThumb(show: false);
		base.gameObject.SetActive(value: false);
	}

	public void Init()
	{
		m_scrollValue = 0f;
		m_stepSize = 1f;
		m_thumb.transform.localPosition = m_sliderStartLocalPos;
		m_scrollAreaStartPos = m_scrollArea.transform.position;
		UpdateScrollAreaBounds();
	}

	public void UpdateScrollAreaBounds()
	{
		GetBoundsOfChildren(m_scrollArea);
		float num = m_childrenBounds.size.z - m_scrollWindowHeight;
		m_scrollAreaEndPos = m_scrollAreaStartPos;
		if (num <= 0f)
		{
			m_scrollValue = 0f;
			Hide();
		}
		else
		{
			int num2 = (int)Mathf.Ceil(num / 5f);
			m_stepSize = 1f / (float)num2;
			m_scrollAreaEndPos.z += num;
			Show();
		}
		UpdateThumbPosition();
		UpdateScrollAreaPosition(tween: false);
	}

	public virtual bool InputIsOver()
	{
		return UniversalInputManager.Get().InputIsOver(base.gameObject);
	}

	protected virtual void GetBoundsOfChildren(GameObject go)
	{
		m_childrenBounds = TransformUtil.GetBoundsOfChildren(go);
	}

	public void OverrideScrollWindowHeight(float scrollWindowHeight)
	{
		m_scrollWindowHeight = scrollWindowHeight;
	}

	protected void ShowThumb(bool show)
	{
		if (m_thumb != null)
		{
			m_thumb.gameObject.SetActive(show);
		}
	}

	private void UpdateThumbPosition()
	{
		m_thumbPosition = Vector3.Lerp(m_sliderStartLocalPos, m_sliderEndLocalPos, Mathf.Clamp01(m_scrollValue));
		m_thumb.transform.localPosition = m_thumbPosition;
		m_thumb.transform.localScale = Vector3.one;
		if (m_scrollValue < 0f || m_scrollValue > 1f)
		{
			float num = 1f / ((m_scrollValue < 0f) ? (0f - m_scrollValue + 1f) : m_scrollValue);
			float z = m_thumb.transform.parent.InverseTransformPoint((m_scrollValue < 0f) ? m_thumb.GetComponent<Renderer>().bounds.max : m_thumb.GetComponent<Renderer>().bounds.min).z;
			float num2 = (m_thumbPosition.z - z) * (num - 1f);
			TransformUtil.SetLocalPosZ(m_thumb, m_thumbPosition.z + num2);
			TransformUtil.SetLocalScaleZ(m_thumb, num);
		}
	}

	private void UpdateScrollAreaPosition(bool tween)
	{
		if (!(m_scrollArea == null))
		{
			Vector3 vector = m_scrollAreaStartPos + m_scrollValue * (m_scrollAreaEndPos - m_scrollAreaStartPos);
			if (tween)
			{
				iTween.MoveTo(m_scrollArea, iTween.Hash("position", vector, "time", 0.2f, "isLocal", false));
			}
			else
			{
				m_scrollArea.transform.position = vector;
			}
		}
	}

	public void ScrollTo(float value, bool clamp = true, bool lerp = true)
	{
		m_scrollValue = (clamp ? Mathf.Clamp01(value) : value);
		UpdateThumbPosition();
		UpdateScrollAreaPosition(lerp);
	}

	private void ScrollUp()
	{
		Scroll(0f - m_stepSize);
	}

	public void Scroll(float amount, bool lerp = true)
	{
		m_scrollValue = Mathf.Clamp01(m_scrollValue + amount);
		UpdateThumbPosition();
		UpdateScrollAreaPosition(lerp);
	}

	private void ScrollDown()
	{
		Scroll(m_stepSize);
	}
}
