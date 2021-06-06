using System;

// Token: 0x020005BC RID: 1468
public class TB_NoMulligan : MissionEntity
{
	// Token: 0x060050FA RID: 20730 RVA: 0x000052EC File Offset: 0x000034EC
	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}
}
