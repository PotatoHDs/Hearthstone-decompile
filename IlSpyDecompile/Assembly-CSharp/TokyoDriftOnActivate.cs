using UnityEngine;

public class TokyoDriftOnActivate : MonoBehaviour
{
	public Transform m_DriftTarget;

	public float m_DriftDuration = 0.5f;

	public float m_DriftScale = 1f;

	private Vector3 m_originalLocalPosition;

	private Vector3 m_originalWorldPosition;

	private Vector3 m_originalLocalScale;

	private void OnDisable()
	{
		base.transform.localPosition = m_originalLocalPosition;
		base.transform.localScale = m_originalLocalScale;
	}

	private void OnEnable()
	{
		m_originalLocalPosition = base.transform.localPosition;
		m_originalWorldPosition = base.transform.position;
		m_originalLocalScale = base.transform.localScale;
		AnimationUtil.GrowThenDrift(base.gameObject, m_originalWorldPosition, m_DriftScale);
	}
}
