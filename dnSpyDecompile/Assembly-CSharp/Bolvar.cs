using System;

// Token: 0x020007CF RID: 1999
public class Bolvar : SuperSpell
{
	// Token: 0x06006E01 RID: 28161 RVA: 0x00237538 File Offset: 0x00235738
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		Card sourceCard = base.GetSourceCard();
		Entity entity = sourceCard.GetEntity();
		Spell prefab = this.DetermineRangePrefab(entity.GetATK());
		this.m_effectsPendingFinish++;
		Spell spell = base.CloneSpell(prefab, null, null);
		spell.SetSource(sourceCard.gameObject);
		spell.Activate();
		this.m_effectsPendingFinish--;
		base.FinishIfPossible();
	}

	// Token: 0x06006E02 RID: 28162 RVA: 0x002375BC File Offset: 0x002357BC
	private Spell DetermineRangePrefab(int atk)
	{
		SpellValueRange appropriateElementAccordingToRanges = SpellUtils.GetAppropriateElementAccordingToRanges<SpellValueRange>(this.m_atkPrefabs, (SpellValueRange x) => x.m_range, atk);
		if (appropriateElementAccordingToRanges != null)
		{
			return appropriateElementAccordingToRanges.m_spellPrefab;
		}
		return null;
	}

	// Token: 0x04005820 RID: 22560
	public SpellValueRange[] m_atkPrefabs;
}
