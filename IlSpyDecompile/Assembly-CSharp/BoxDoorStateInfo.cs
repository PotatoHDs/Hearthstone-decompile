using System;
using UnityEngine;

[Serializable]
public class BoxDoorStateInfo
{
	public Vector3 m_OpenedRotation = new Vector3(0f, 0f, 180f);

	public float m_OpenedDelaySec;

	public float m_OpenedRotateSec = 0.35f;

	public iTween.EaseType m_OpenedRotateEaseType;

	public Vector3 m_ClosedRotation = Vector3.zero;

	public float m_ClosedDelaySec;

	public float m_ClosedRotateSec = 0.35f;

	public iTween.EaseType m_ClosedRotateEaseType;
}
