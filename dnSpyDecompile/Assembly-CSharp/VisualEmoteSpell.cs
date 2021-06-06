using System;
using UnityEngine;

// Token: 0x0200099A RID: 2458
public class VisualEmoteSpell : Spell
{
	// Token: 0x0600865C RID: 34396 RVA: 0x002B6800 File Offset: 0x002B4A00
	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.OnBirth(prevStateType);
		Spell spell = null;
		Card sourceCard = base.GetSourceCard();
		if (sourceCard != null)
		{
			Player controller = sourceCard.GetController();
			if (controller != null)
			{
				if (controller.IsFriendlySide())
				{
					spell = this.m_FriendlySpellPrefab;
				}
				else if (!controller.IsFriendlySide())
				{
					spell = this.m_OpponentSpellPrefab;
				}
			}
		}
		if (spell != null)
		{
			Spell spell2 = UnityEngine.Object.Instantiate<Spell>(spell);
			spell2.SetSource(base.GetSource());
			spell2.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnSpellStateFinished));
			spell2.AddFinishedCallback(new Spell.FinishedCallback(this.OnSpellEffectFinished));
			this.m_effectsPendingFinish++;
			spell2.Activate();
		}
		if (!base.HasStateContent(SpellStateType.BIRTH))
		{
			this.OnStateFinished();
		}
	}

	// Token: 0x0600865D RID: 34397 RVA: 0x002B68B1 File Offset: 0x002B4AB1
	private void FinishIfPossible()
	{
		if (this.m_effectsPendingFinish == 0)
		{
			base.OnSpellFinished();
		}
	}

	// Token: 0x0600865E RID: 34398 RVA: 0x002B68C1 File Offset: 0x002B4AC1
	public override void OnSpellFinished()
	{
		this.FinishIfPossible();
	}

	// Token: 0x0600865F RID: 34399 RVA: 0x002B68C9 File Offset: 0x002B4AC9
	private void OnSpellEffectFinished(Spell spell, object userData)
	{
		this.m_effectsPendingFinish--;
		this.FinishIfPossible();
	}

	// Token: 0x06008660 RID: 34400 RVA: 0x002B68DF File Offset: 0x002B4ADF
	private void OnSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		UnityEngine.Object.Destroy(spell);
	}

	// Token: 0x040071ED RID: 29165
	public Spell m_FriendlySpellPrefab;

	// Token: 0x040071EE RID: 29166
	public Spell m_OpponentSpellPrefab;

	// Token: 0x040071EF RID: 29167
	public bool m_PositionOnSpeechBubble;

	// Token: 0x040071F0 RID: 29168
	protected int m_effectsPendingFinish;
}
