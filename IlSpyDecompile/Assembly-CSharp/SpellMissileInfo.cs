using System;
using UnityEngine;

[Serializable]
public class SpellMissileInfo
{
	public bool m_Enabled = true;

	public Spell m_Prefab;

	public Spell m_ReversePrefab;

	public float m_reverseDelay;

	public bool m_UseSuperSpellLocation = true;

	public float m_SpawnDelaySecMin;

	public float m_SpawnDelaySecMax;

	public bool m_SpawnInSequence;

	public float m_SpawnOffset;

	public float m_PathDurationMin = 0.5f;

	public float m_PathDurationMax = 1f;

	public iTween.EaseType m_PathEaseType = iTween.EaseType.linear;

	public bool m_OrientToPath;

	public float m_CenterOffsetPercent = 50f;

	public float m_CenterPointHeightMin;

	public float m_CenterPointHeightMax;

	public float m_RightMin;

	public float m_RightMax;

	public float m_LeftMin;

	public float m_LeftMax;

	public bool m_DebugForceMax;

	public float m_DistanceScaleFactor = 8f;

	public string m_TargetJoint = "TargetJoint";

	public float m_TargetHeightOffset = 0.5f;

	public Vector3 m_JointUpVector = Vector3.up;

	public bool m_UseTargetCardPositionInsteadOfHandSlot;
}
