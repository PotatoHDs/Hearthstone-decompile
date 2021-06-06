using System;
using UnityEngine;

// Token: 0x0200098B RID: 2443
[Serializable]
public class SpellImpactInfo
{
	// Token: 0x0400714C RID: 29004
	public bool m_Enabled = true;

	// Token: 0x0400714D RID: 29005
	[Tooltip("This spell will be chosen by default if the card deals no damage.")]
	public Spell m_Prefab;

	// Token: 0x0400714E RID: 29006
	[Tooltip("If the card deals damage, the spell in the appropriate damage range will be chosen. If the damage exceeds all ranges, we pick the one with the highest maximum range. If the damage number is not within any specified range, we will use the default spell (see above)")]
	public SpellValueRange[] m_damageAmountImpactSpells;

	// Token: 0x0400714F RID: 29007
	public bool m_UseSuperSpellLocation;

	// Token: 0x04007150 RID: 29008
	public SpellLocation m_Location = SpellLocation.TARGET;

	// Token: 0x04007151 RID: 29009
	public bool m_SetParentToLocation;

	// Token: 0x04007152 RID: 29010
	public float m_SpawnDelaySecMin;

	// Token: 0x04007153 RID: 29011
	public float m_SpawnDelaySecMax;

	// Token: 0x04007154 RID: 29012
	public float m_SpawnOffset;

	// Token: 0x04007155 RID: 29013
	public float m_GameDelaySecMin = 0.5f;

	// Token: 0x04007156 RID: 29014
	public float m_GameDelaySecMax = 0.5f;
}
