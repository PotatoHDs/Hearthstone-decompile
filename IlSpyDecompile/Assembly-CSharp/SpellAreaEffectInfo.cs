using System;

[Serializable]
public class SpellAreaEffectInfo
{
	public bool m_Enabled = true;

	public Spell m_Prefab;

	public bool m_UseSuperSpellLocation;

	public SpellLocation m_Location;

	public bool m_SetParentToLocation;

	public SpellFacing m_Facing;

	public SpellFacingOptions m_FacingOptions;

	public float m_SpawnDelaySecMin;

	public float m_SpawnDelaySecMax;
}
