public class Bolvar : SuperSpell
{
	public SpellValueRange[] m_atkPrefabs;

	protected override void OnAction(SpellStateType prevStateType)
	{
		m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		Card sourceCard = GetSourceCard();
		Entity entity = sourceCard.GetEntity();
		Spell prefab = DetermineRangePrefab(entity.GetATK());
		m_effectsPendingFinish++;
		Spell spell = CloneSpell(prefab);
		spell.SetSource(sourceCard.gameObject);
		spell.Activate();
		m_effectsPendingFinish--;
		FinishIfPossible();
	}

	private Spell DetermineRangePrefab(int atk)
	{
		return SpellUtils.GetAppropriateElementAccordingToRanges(m_atkPrefabs, (SpellValueRange x) => x.m_range, atk)?.m_spellPrefab;
	}
}
