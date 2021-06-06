using UnityEngine;

[ExecuteAlways]
public class ThreeSliceElement : MonoBehaviour
{
	public enum PinnedPoint
	{
		LEFT,
		MIDDLE,
		RIGHT,
		TOP,
		BOTTOM
	}

	public enum Direction
	{
		X,
		Y,
		Z
	}

	public GameObject m_left;

	public GameObject m_middle;

	public GameObject m_right;

	public PinnedPoint m_pinnedPoint;

	public Vector3 m_pinnedPointOffset;

	public Direction m_direction;

	public float m_width;

	public float m_middleScale = 1f;

	public Vector3 m_leftOffset;

	public Vector3 m_middleOffset;

	public Vector3 m_rightOffset;

	private Bounds m_initialMiddleBounds;

	private Vector3 m_initialScale = Vector3.zero;

	private void Awake()
	{
		if ((bool)m_middle)
		{
			SetInitialValues();
		}
	}

	public void UpdateDisplay()
	{
		if (base.enabled)
		{
			if (m_initialMiddleBounds.size == Vector3.zero)
			{
				m_initialMiddleBounds = m_middle.GetComponent<Renderer>().bounds;
			}
			float num = m_width - (m_left.GetComponent<Renderer>().bounds.size.x + m_right.GetComponent<Renderer>().bounds.size.x);
			switch (m_direction)
			{
			case Direction.X:
			{
				Vector3 scale = TransformUtil.ComputeWorldScale(m_middle.transform);
				scale.x = m_initialScale.x * num / m_initialMiddleBounds.size.x;
				TransformUtil.SetWorldScale(m_middle.transform, scale);
				break;
			}
			}
			switch (m_pinnedPoint)
			{
			case PinnedPoint.RIGHT:
				m_right.transform.localPosition = m_pinnedPointOffset;
				TransformUtil.SetPoint(m_middle, Anchor.RIGHT, m_right, Anchor.LEFT, m_middleOffset);
				TransformUtil.SetPoint(m_left, Anchor.RIGHT, m_middle, Anchor.LEFT, m_leftOffset);
				break;
			case PinnedPoint.LEFT:
				m_left.transform.localPosition = m_pinnedPointOffset;
				TransformUtil.SetPoint(m_middle, Anchor.LEFT, m_left, Anchor.RIGHT, m_middleOffset);
				TransformUtil.SetPoint(m_right, Anchor.LEFT, m_middle, Anchor.RIGHT, m_rightOffset);
				break;
			case PinnedPoint.MIDDLE:
				m_middle.transform.localPosition = m_pinnedPointOffset;
				TransformUtil.SetPoint(m_left, Anchor.RIGHT, m_middle, Anchor.LEFT, m_leftOffset);
				TransformUtil.SetPoint(m_right, Anchor.LEFT, m_middle, Anchor.RIGHT, m_rightOffset);
				break;
			}
		}
	}

	public void SetWidth(float globalWidth)
	{
		m_width = globalWidth;
		UpdateDisplay();
	}

	public void SetMiddleWidth(float globalWidth)
	{
		m_width = globalWidth + m_left.GetComponent<Renderer>().bounds.size.x + m_right.GetComponent<Renderer>().bounds.size.x;
		UpdateDisplay();
	}

	public Vector3 GetMiddleSize()
	{
		return m_middle.GetComponent<Renderer>().bounds.size;
	}

	public Vector3 GetSize()
	{
		return GetSize(zIsHeight: true);
	}

	public Vector3 GetSize(bool zIsHeight)
	{
		Vector3 size = m_left.GetComponent<Renderer>().bounds.size;
		Vector3 size2 = m_middle.GetComponent<Renderer>().bounds.size;
		Vector3 size3 = m_right.GetComponent<Renderer>().bounds.size;
		float x = size.x + size3.x + size2.x;
		float num = Mathf.Max(Mathf.Max(size.z, size2.z), size3.z);
		float num2 = Mathf.Max(Mathf.Max(size.y, size2.y), size3.y);
		if (zIsHeight)
		{
			return new Vector3(x, num, num2);
		}
		return new Vector3(x, num2, num);
	}

	public void SetInitialValues()
	{
		m_initialMiddleBounds = m_middle.GetComponent<Renderer>().bounds;
		m_initialScale = m_middle.transform.lossyScale;
		m_width = m_middle.GetComponent<Renderer>().bounds.size.x + m_left.GetComponent<Renderer>().bounds.size.x + m_right.GetComponent<Renderer>().bounds.size.x;
	}
}
