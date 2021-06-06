public class CardTextHistoryData
{
	public int m_damageBonus;

	public int m_damageBonusDouble;

	public int m_healingDouble;

	public virtual void SetHistoryData(Entity entity, HistoryInfo historyInfo)
	{
		m_damageBonus = entity.GetDamageBonus();
		m_damageBonusDouble = entity.GetDamageBonusDouble();
		m_healingDouble = entity.GetHealingDouble();
	}
}
