using System;

// Token: 0x0200078B RID: 1931
public class CardTextHistoryData
{
	// Token: 0x06006C48 RID: 27720 RVA: 0x00230ADA File Offset: 0x0022ECDA
	public virtual void SetHistoryData(Entity entity, HistoryInfo historyInfo)
	{
		this.m_damageBonus = entity.GetDamageBonus();
		this.m_damageBonusDouble = entity.GetDamageBonusDouble();
		this.m_healingDouble = entity.GetHealingDouble();
	}

	// Token: 0x040057A6 RID: 22438
	public int m_damageBonus;

	// Token: 0x040057A7 RID: 22439
	public int m_damageBonusDouble;

	// Token: 0x040057A8 RID: 22440
	public int m_healingDouble;
}
