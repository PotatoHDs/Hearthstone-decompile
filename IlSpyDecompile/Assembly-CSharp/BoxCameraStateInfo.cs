using System;
using UnityEngine;

[Serializable]
public class BoxCameraStateInfo
{
	public GameObject m_ClosedBone;

	public GameObject m_ClosedMinAspectRatioBone;

	public GameObject m_ClosedExtraWideAspectRatioBone;

	public float m_ClosedDelaySec;

	public float m_ClosedMoveSec = 0.7f;

	public iTween.EaseType m_ClosedMoveEaseType = iTween.EaseType.easeOutCubic;

	public GameObject m_ClosedWithDrawerBone;

	public GameObject m_ClosedWithDrawerMinAspectRatioBone;

	public GameObject m_ClosedWithDrawerExtraWideAspectRatioBone;

	public float m_ClosedWithDrawerDelaySec;

	public float m_ClosedWithDrawerMoveSec = 0.7f;

	public iTween.EaseType m_ClosedWithDrawerMoveEaseType = iTween.EaseType.easeOutCubic;

	public GameObject m_OpenedBone;

	public GameObject m_OpenedMinAspectRatioBone;

	public GameObject m_OpenedExtraWideAspectRatioBone;

	public float m_OpenedDelaySec;

	public float m_OpenedMoveSec = 0.7f;

	public iTween.EaseType m_OpenedMoveEaseType = iTween.EaseType.easeOutCubic;
}
