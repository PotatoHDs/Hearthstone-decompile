using System;

// Token: 0x0200098C RID: 2444
[Serializable]
public class SpellAreaEffectInfo
{
	// Token: 0x04007157 RID: 29015
	public bool m_Enabled = true;

	// Token: 0x04007158 RID: 29016
	public Spell m_Prefab;

	// Token: 0x04007159 RID: 29017
	public bool m_UseSuperSpellLocation;

	// Token: 0x0400715A RID: 29018
	public SpellLocation m_Location;

	// Token: 0x0400715B RID: 29019
	public bool m_SetParentToLocation;

	// Token: 0x0400715C RID: 29020
	public SpellFacing m_Facing;

	// Token: 0x0400715D RID: 29021
	public SpellFacingOptions m_FacingOptions;

	// Token: 0x0400715E RID: 29022
	public float m_SpawnDelaySecMin;

	// Token: 0x0400715F RID: 29023
	public float m_SpawnDelaySecMax;
}
