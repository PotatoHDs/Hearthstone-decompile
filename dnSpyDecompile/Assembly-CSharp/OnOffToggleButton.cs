using System;

// Token: 0x02000AD7 RID: 2775
public class OnOffToggleButton : CheckBox
{
	// Token: 0x060093D6 RID: 37846 RVA: 0x002FFF6B File Offset: 0x002FE16B
	public override void SetChecked(bool isChecked)
	{
		base.SetChecked(isChecked);
		base.SetButtonText(GameStrings.Get(isChecked ? this.m_onLabel : this.m_offLabel));
	}

	// Token: 0x04007BF1 RID: 31729
	public string m_onLabel = "GLOBAL_ON";

	// Token: 0x04007BF2 RID: 31730
	public string m_offLabel = "GLOBAL_OFF";
}
