using UnityEngine;

public class DragRotator : MonoBehaviour
{
	private const float EPSILON = 0.0001f;

	private const float SMOOTH_DAMP_SEC_FUDGE = 0.1f;

	private DragRotatorInfo m_info;

	private float m_pitchDeg;

	private float m_rollDeg;

	private float m_pitchVel;

	private float m_rollVel;

	private Vector3 m_prevPos;

	private Vector3 m_originalAngles;

	private void Awake()
	{
		m_prevPos = base.transform.position;
		m_originalAngles = base.transform.localRotation.eulerAngles;
	}

	private void Update()
	{
		Vector3 position = base.transform.position;
		Vector3 vector = position - m_prevPos;
		if (vector.sqrMagnitude > 0.0001f)
		{
			m_pitchDeg += vector.z * m_info.m_PitchInfo.m_ForceMultiplier;
			m_pitchDeg = Mathf.Clamp(m_pitchDeg, m_info.m_PitchInfo.m_MinDegrees, m_info.m_PitchInfo.m_MaxDegrees);
			m_rollDeg -= vector.x * m_info.m_RollInfo.m_ForceMultiplier;
			m_rollDeg = Mathf.Clamp(m_rollDeg, m_info.m_RollInfo.m_MinDegrees, m_info.m_RollInfo.m_MaxDegrees);
		}
		m_pitchDeg = Mathf.SmoothDamp(m_pitchDeg, 0f, ref m_pitchVel, m_info.m_PitchInfo.m_RestSeconds * 0.1f);
		m_rollDeg = Mathf.SmoothDamp(m_rollDeg, 0f, ref m_rollVel, m_info.m_RollInfo.m_RestSeconds * 0.1f);
		base.transform.localRotation = Quaternion.Euler(m_originalAngles.x + m_pitchDeg, m_originalAngles.y, m_originalAngles.z + m_rollDeg);
		m_prevPos = position;
	}

	public DragRotatorInfo GetInfo()
	{
		return m_info;
	}

	public void SetInfo(DragRotatorInfo info)
	{
		m_info = info;
	}

	public void Reset()
	{
		m_prevPos = base.transform.position;
		base.transform.localRotation = Quaternion.Euler(m_originalAngles);
		m_rollDeg = 0f;
		m_rollVel = 0f;
		m_pitchDeg = 0f;
		m_pitchVel = 0f;
	}
}
