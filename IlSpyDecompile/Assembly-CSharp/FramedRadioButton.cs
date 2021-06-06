using UnityEngine;

public class FramedRadioButton : MonoBehaviour
{
	public enum FrameType
	{
		SINGLE,
		MULTI_LEFT_END,
		MULTI_RIGHT_END,
		MULTI_MIDDLE
	}

	public GameObject m_root;

	public GameObject m_frameEndLeft;

	public GameObject m_frameEndRight;

	public GameObject m_frameLeft;

	public GameObject m_frameFill;

	public RadioButton m_radioButton;

	public UberText m_text;

	private float m_leftEdgeOffset;

	public int GetButtonID()
	{
		return m_radioButton.GetButtonID();
	}

	public float GetLeftEdgeOffset()
	{
		return m_leftEdgeOffset;
	}

	public virtual void Init(FrameType frameType, string text, int buttonID, object userData)
	{
		m_radioButton.SetButtonID(buttonID);
		m_radioButton.SetUserData(userData);
		m_text.Text = text;
		m_text.UpdateNow();
		m_frameFill.SetActive(value: true);
		bool flag = false;
		bool active = false;
		switch (frameType)
		{
		case FrameType.SINGLE:
			flag = true;
			active = true;
			break;
		case FrameType.MULTI_LEFT_END:
			flag = true;
			active = false;
			break;
		case FrameType.MULTI_RIGHT_END:
			flag = false;
			active = true;
			break;
		case FrameType.MULTI_MIDDLE:
			flag = false;
			active = false;
			break;
		}
		m_frameEndLeft.SetActive(flag);
		m_frameLeft.SetActive(!flag);
		m_frameEndRight.SetActive(active);
		Transform transform = (flag ? m_frameEndLeft.transform : m_frameLeft.transform);
		m_leftEdgeOffset = transform.position.x - base.transform.position.x;
	}

	public void Show()
	{
		m_root.SetActive(value: true);
	}

	public void Hide()
	{
		m_root.SetActive(value: false);
	}

	public Bounds GetBounds()
	{
		Bounds bounds = m_frameFill.GetComponent<Renderer>().bounds;
		IncludeBoundsOfGameObject(m_frameEndLeft, ref bounds);
		IncludeBoundsOfGameObject(m_frameEndRight, ref bounds);
		IncludeBoundsOfGameObject(m_frameLeft, ref bounds);
		return bounds;
	}

	private void IncludeBoundsOfGameObject(GameObject go, ref Bounds bounds)
	{
		if (go.activeSelf)
		{
			Bounds bounds2 = go.GetComponent<Renderer>().bounds;
			Vector3 max = Vector3.Max(bounds2.max, bounds.max);
			Vector3 min = Vector3.Min(bounds2.min, bounds.min);
			bounds.SetMinMax(min, max);
		}
	}
}
