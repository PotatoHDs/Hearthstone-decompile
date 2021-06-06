using System;

// Token: 0x02000987 RID: 2439
[Serializable]
public class SpellTargetInfo
{
	// Token: 0x04007127 RID: 28967
	public SpellTargetBehavior m_Behavior;

	// Token: 0x04007128 RID: 28968
	public int m_RandomTargetCountMin = 8;

	// Token: 0x04007129 RID: 28969
	public int m_RandomTargetCountMax = 10;

	// Token: 0x0400712A RID: 28970
	public bool m_SuppressPlaySounds;
}
