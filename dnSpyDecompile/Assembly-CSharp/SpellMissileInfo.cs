using System;
using UnityEngine;

// Token: 0x0200098A RID: 2442
[Serializable]
public class SpellMissileInfo
{
	// Token: 0x04007132 RID: 28978
	public bool m_Enabled = true;

	// Token: 0x04007133 RID: 28979
	public Spell m_Prefab;

	// Token: 0x04007134 RID: 28980
	public Spell m_ReversePrefab;

	// Token: 0x04007135 RID: 28981
	public float m_reverseDelay;

	// Token: 0x04007136 RID: 28982
	public bool m_UseSuperSpellLocation = true;

	// Token: 0x04007137 RID: 28983
	public float m_SpawnDelaySecMin;

	// Token: 0x04007138 RID: 28984
	public float m_SpawnDelaySecMax;

	// Token: 0x04007139 RID: 28985
	public bool m_SpawnInSequence;

	// Token: 0x0400713A RID: 28986
	public float m_SpawnOffset;

	// Token: 0x0400713B RID: 28987
	public float m_PathDurationMin = 0.5f;

	// Token: 0x0400713C RID: 28988
	public float m_PathDurationMax = 1f;

	// Token: 0x0400713D RID: 28989
	public iTween.EaseType m_PathEaseType = iTween.EaseType.linear;

	// Token: 0x0400713E RID: 28990
	public bool m_OrientToPath;

	// Token: 0x0400713F RID: 28991
	public float m_CenterOffsetPercent = 50f;

	// Token: 0x04007140 RID: 28992
	public float m_CenterPointHeightMin;

	// Token: 0x04007141 RID: 28993
	public float m_CenterPointHeightMax;

	// Token: 0x04007142 RID: 28994
	public float m_RightMin;

	// Token: 0x04007143 RID: 28995
	public float m_RightMax;

	// Token: 0x04007144 RID: 28996
	public float m_LeftMin;

	// Token: 0x04007145 RID: 28997
	public float m_LeftMax;

	// Token: 0x04007146 RID: 28998
	public bool m_DebugForceMax;

	// Token: 0x04007147 RID: 28999
	public float m_DistanceScaleFactor = 8f;

	// Token: 0x04007148 RID: 29000
	public string m_TargetJoint = "TargetJoint";

	// Token: 0x04007149 RID: 29001
	public float m_TargetHeightOffset = 0.5f;

	// Token: 0x0400714A RID: 29002
	public Vector3 m_JointUpVector = Vector3.up;

	// Token: 0x0400714B RID: 29003
	public bool m_UseTargetCardPositionInsteadOfHandSlot;
}
