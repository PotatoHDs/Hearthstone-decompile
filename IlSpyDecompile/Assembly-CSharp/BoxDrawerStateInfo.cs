using System;
using UnityEngine;

[Serializable]
public class BoxDrawerStateInfo
{
	public GameObject m_ClosedBone;

	public float m_ClosedDelaySec;

	public float m_ClosedMoveSec = 1f;

	public iTween.EaseType m_ClosedMoveEaseType = iTween.EaseType.linear;

	public GameObject m_ClosedBoxOpenedBone;

	public float m_ClosedBoxOpenedDelaySec;

	public float m_ClosedBoxOpenedMoveSec = 1f;

	public iTween.EaseType m_ClosedBoxOpenedMoveEaseType = iTween.EaseType.linear;

	public GameObject m_OpenedBone;

	public float m_OpenedDelaySec;

	public float m_OpenedMoveSec = 1f;

	public iTween.EaseType m_OpenedMoveEaseType = iTween.EaseType.easeOutBounce;
}
