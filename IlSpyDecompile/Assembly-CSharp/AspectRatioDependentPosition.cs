using UnityEngine;

public class AspectRatioDependentPosition : MonoBehaviour
{
	public Vector3 m_minLocalPosition;

	public Vector3 m_wideLocalPosition;

	public Vector3 m_extraWideLocalPosition;

	private void Awake()
	{
		base.transform.localPosition = TransformUtil.GetAspectRatioDependentPosition(m_minLocalPosition, m_wideLocalPosition, m_extraWideLocalPosition);
	}
}
