using System;
using PegasusGame;

// Token: 0x0200081E RID: 2078
public class SherazinFakeDeathSpell : OverrideCustomSpawnSpell
{
	// Token: 0x06006FE4 RID: 28644 RVA: 0x002418EC File Offset: 0x0023FAEC
	public override bool AddPowerTargets()
	{
		if (!base.CanAddPowerTargets())
		{
			return false;
		}
		if (this.m_taskList.GetBlockType() != HistoryBlock.Type.TRIGGER)
		{
			return false;
		}
		if (base.GetSourceCard() == null || base.GetSourceCard().GetEntity() == null)
		{
			Log.Spells.PrintError("SherazinFakeDeathSpell.AddPowerTargets(): Failed to find source entity for Sherazin.", Array.Empty<object>());
			return false;
		}
		if (base.GetSourceCard().GetEntity().GetZone() == TAG_ZONE.PLAY)
		{
			this.m_mustPlayFakeDeath = true;
			return true;
		}
		this.m_mustPlayFakeDeath = false;
		return true;
	}

	// Token: 0x06006FE5 RID: 28645 RVA: 0x00241968 File Offset: 0x0023FB68
	protected override void OnAction(SpellStateType prevStateType)
	{
		if (this.m_mustPlayFakeDeath)
		{
			this.m_effectsPendingFinish++;
			base.OnAction(prevStateType);
			base.GetSourceCard().FakeDeath();
			base.GetSourceCard().SetDelayBeforeHideInNullZoneVisuals(this.m_delayBeforeHideActor);
			this.m_effectsPendingFinish--;
			this.OnSpellFinished();
			this.OnStateFinished();
			return;
		}
		base.OnAction(prevStateType);
	}

	// Token: 0x040059BB RID: 22971
	private bool m_mustPlayFakeDeath;

	// Token: 0x040059BC RID: 22972
	public float m_delayBeforeHideActor = 3f;
}
