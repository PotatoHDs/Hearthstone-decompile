using System;

// Token: 0x02000835 RID: 2101
public class UnearthedRaptorSpell : SuperSpell
{
	// Token: 0x0600706C RID: 28780 RVA: 0x002445DA File Offset: 0x002427DA
	public override bool AddPowerTargets()
	{
		return base.AddPowerTargets() && this.m_targets.Count > 0;
	}
}
