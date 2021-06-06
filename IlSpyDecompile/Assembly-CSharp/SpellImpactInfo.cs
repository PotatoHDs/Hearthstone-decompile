using System;
using UnityEngine;

[Serializable]
public class SpellImpactInfo
{
	public bool m_Enabled = true;

	[Tooltip("This spell will be chosen by default if the card deals no damage.")]
	public Spell m_Prefab;

	[Tooltip("If the card deals damage, the spell in the appropriate damage range will be chosen. If the damage exceeds all ranges, we pick the one with the highest maximum range. If the damage number is not within any specified range, we will use the default spell (see above)")]
	public SpellValueRange[] m_damageAmountImpactSpells;

	public bool m_UseSuperSpellLocation;

	public SpellLocation m_Location = SpellLocation.TARGET;

	public bool m_SetParentToLocation;

	public float m_SpawnDelaySecMin;

	public float m_SpawnDelaySecMax;

	public float m_SpawnOffset;

	public float m_GameDelaySecMin = 0.5f;

	public float m_GameDelaySecMax = 0.5f;
}
