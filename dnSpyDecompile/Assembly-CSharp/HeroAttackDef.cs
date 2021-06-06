using System;

// Token: 0x020006C2 RID: 1730
[Serializable]
public class HeroAttackDef
{
	// Token: 0x040050F8 RID: 20728
	public float m_MoveToTargetDuration = 0.12f;

	// Token: 0x040050F9 RID: 20729
	public iTween.EaseType m_MoveToTargetEaseType = iTween.EaseType.linear;

	// Token: 0x040050FA RID: 20730
	public float m_OrientToTargetDuration = 0.3f;

	// Token: 0x040050FB RID: 20731
	public iTween.EaseType m_OrientToTargetEaseType = iTween.EaseType.linear;

	// Token: 0x040050FC RID: 20732
	public float m_MoveBackDuration = 0.15f;

	// Token: 0x040050FD RID: 20733
	public iTween.EaseType m_MoveBackEaseType = iTween.Defaults.easeType;

	// Token: 0x040050FE RID: 20734
	public float m_OrientBackDuration = 0.3f;

	// Token: 0x040050FF RID: 20735
	public iTween.EaseType m_OrientBackEaseType = iTween.EaseType.linear;
}
