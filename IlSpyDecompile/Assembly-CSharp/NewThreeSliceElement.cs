using UnityEngine;

[ExecuteAlways]
public class NewThreeSliceElement : MonoBehaviour
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

	public enum PlaneAxis
	{
		XY,
		XZ
	}

	public GameObject m_leftOrTop;

	public GameObject m_middle;

	public GameObject m_rightOrBottom;

	public PinnedPoint m_pinnedPoint;

	public PlaneAxis m_planeAxis = PlaneAxis.XZ;

	public Vector3 m_pinnedPointOffset;

	public Direction m_direction;

	public Vector3 m_middleScale = Vector3.one;

	public Vector3 m_leftOffset;

	public Vector3 m_middleOffset;

	public Vector3 m_rightOffset;

	private Vector3 m_leftAnchor;

	private Vector3 m_rightAnchor;

	private Vector3 m_topAnchor;

	private Vector3 m_bottomAnchor;

	private Transform m_identity;

	private void Awake()
	{
		SetSize(m_middleScale);
	}

	private void OnDestroy()
	{
		if (m_identity != null && m_identity.gameObject != null)
		{
			Object.DestroyImmediate(m_identity.gameObject);
		}
	}

	public virtual void SetSize(Vector3 size)
	{
		m_middle.transform.localScale = size;
		if (m_identity == null)
		{
			m_identity = new GameObject().transform;
		}
		m_identity.position = Vector3.zero;
		if (m_planeAxis == PlaneAxis.XZ)
		{
			m_leftAnchor = new Vector3(0f, 0f, 0.5f);
			m_rightAnchor = new Vector3(1f, 0f, 0.5f);
			m_topAnchor = new Vector3(0.5f, 0f, 1f);
			m_bottomAnchor = new Vector3(0.5f, 0f, 0f);
		}
		else
		{
			m_leftAnchor = new Vector3(0f, 0.5f, 0f);
			m_rightAnchor = new Vector3(1f, 0.5f, 0f);
			m_topAnchor = new Vector3(0.5f, 0f, 0f);
			m_bottomAnchor = new Vector3(0.5f, 1f, 0f);
		}
		switch (m_direction)
		{
		case Direction.X:
			DisplayOnXAxis();
			break;
		case Direction.Z:
			DisplayOnZAxis();
			break;
		case Direction.Y:
			break;
		}
	}

	private void DisplayOnXAxis()
	{
		switch (m_pinnedPoint)
		{
		case PinnedPoint.RIGHT:
			m_rightOrBottom.transform.localPosition = m_pinnedPointOffset;
			TransformUtil.SetPoint(m_middle, m_rightAnchor, m_rightOrBottom, m_leftAnchor, m_identity.transform.TransformPoint(m_middleOffset));
			TransformUtil.SetPoint(m_leftOrTop, m_rightAnchor, m_middle, m_leftAnchor, m_identity.transform.TransformPoint(m_leftOffset));
			break;
		case PinnedPoint.LEFT:
			m_leftOrTop.transform.localPosition = m_pinnedPointOffset;
			TransformUtil.SetPoint(m_middle, m_leftAnchor, m_leftOrTop, m_rightAnchor, m_identity.transform.TransformPoint(m_middleOffset));
			TransformUtil.SetPoint(m_rightOrBottom, m_leftAnchor, m_middle, m_rightAnchor, m_identity.transform.TransformPoint(m_rightOffset));
			break;
		case PinnedPoint.MIDDLE:
			m_middle.transform.localPosition = m_pinnedPointOffset;
			TransformUtil.SetPoint(m_leftOrTop, m_rightAnchor, m_middle, m_leftAnchor, m_identity.transform.TransformPoint(m_leftOffset));
			TransformUtil.SetPoint(m_rightOrBottom, m_leftAnchor, m_middle, m_rightAnchor, m_identity.transform.TransformPoint(m_rightOffset));
			break;
		}
	}

	private void DisplayOnYAxis()
	{
	}

	private void DisplayOnZAxis()
	{
		switch (m_pinnedPoint)
		{
		case PinnedPoint.TOP:
			m_leftOrTop.transform.localPosition = m_pinnedPointOffset;
			TransformUtil.SetPoint(m_middle, m_topAnchor, m_leftOrTop, m_bottomAnchor, m_identity.transform.TransformPoint(m_middleOffset));
			TransformUtil.SetPoint(m_rightOrBottom, m_topAnchor, m_middle, m_bottomAnchor, m_identity.transform.TransformPoint(m_rightOffset));
			break;
		case PinnedPoint.BOTTOM:
			m_rightOrBottom.transform.localPosition = m_pinnedPointOffset;
			TransformUtil.SetPoint(m_middle, m_bottomAnchor, m_rightOrBottom, m_topAnchor, m_identity.transform.TransformPoint(m_middleOffset));
			TransformUtil.SetPoint(m_leftOrTop, m_bottomAnchor, m_middle, m_topAnchor, m_identity.transform.TransformPoint(m_leftOffset));
			break;
		case PinnedPoint.MIDDLE:
			m_middle.transform.localPosition = m_pinnedPointOffset;
			TransformUtil.SetPoint(m_leftOrTop, m_bottomAnchor, m_middle, m_topAnchor, m_identity.transform.TransformPoint(m_leftOffset));
			TransformUtil.SetPoint(m_rightOrBottom, m_topAnchor, m_middle, m_bottomAnchor, m_identity.transform.TransformPoint(m_rightOffset));
			break;
		case PinnedPoint.RIGHT:
			break;
		}
	}
}
